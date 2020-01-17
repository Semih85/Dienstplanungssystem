using OPTANO.Modeling.Common;
using OPTANO.Modeling.Optimization;
using OPTANO.Modeling.Optimization.Configuration;
using OPTANO.Modeling.Optimization.Enums;
using OPTANO.Modeling.Optimization.Solver;
using OPTANO.Modeling.Optimization.Solver.Cplex128;
using System;
using System.Collections.Generic;
using System.Linq;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;
using WM.Northwind.Entities.Concrete.Optimization.EczaneNobet;
using WM.Optimization.Abstract.Health;

namespace WM.Optimization.Concrete.Optano.Health.EczaneNobet.Eski
{
    public class AntalyaMerkezOptanoEski : IEczaneNobetAntalyaMerkezOptimization
    {
        //Karar değişkeni model çalıştıktan sonra değer aldığından burada tanımlandı
        private VariableCollection<EczaneNobetTarihAralik> _x { get; set; }

        private Model Model(AntalyaMerkezDataModel data)
        {
            var model = new Model() { Name = "Antalya Merkez Eczane Nöbet" };

            #region Veriler
            //int BigM = 1000;
            var bayramlar = data.TarihAraligi.Where(w => w.NobetGunKuralId > 7).Select(s => s.NobetGunKuralId).Distinct().ToList();
            #endregion

            #region Karar Değişkenleri
            _x = new VariableCollection<EczaneNobetTarihAralik>(
                    model,
                    data.EczaneNobetTarihAralik,
                    "_x", null,
                    h => data.LowerBound,
                    h => data.UpperBound,
                    a => VariableType.Binary);
            #endregion

            #region Kısıtlar

            foreach (var nobetGrup in data.NobetGruplar)
            {
                #region ön hazırlama

                #region ilk ayda fazla yazılacak eczaneler

                var gruptakiNobetciSayisi = data.EczaneNobetGruplar
                                .Where(s => s.NobetGrupId == nobetGrup.Id)
                                .Select(s => s.EczaneId).Count();

                var r = new Random();
                var eczaneler = data.EczaneNobetGruplar
                    .Where(w => w.NobetGrupId == nobetGrup.Id)
                    .OrderBy(x => r.NextDouble()).ToList();

                var eczaneSayisi = eczaneler.Count();
                var gunSayisi = data.TarihAraligi.Count();

                var fazlaYazilacakEczaneSayisi = gunSayisi - eczaneSayisi;
                var fazlaYazilacakEczaneler = new List<EczaneNobetGrupDetay>();

                //ilk ayda eczane sayısı gün sayısından azsa, eksik sayı kadar rasgele seçilen eczaneye 1 fazla nöbet yazılır. toplam hedeflerde
                if (data.Yil == 2018 && data.Ay == 1) //&& fazlaYazilacakEczaneSayisi > 0)
                {
                    fazlaYazilacakEczaneler = eczaneler.OrderBy(x => r.NextDouble()).Take(fazlaYazilacakEczaneSayisi).ToList();
                }
                #endregion

                #region Nöbet gün kurallar

                var nobetGunKural = data.Kisitlar
                                                        .Where(s => s.KisitAdi == "nobetGunKural"
                                                                 && s.NobetUstGrupId == data.NobetUstGrupId)
                                                        .Select(w => w.PasifMi == false).SingleOrDefault();

                var nobetGunKuralDetaylar = new List<NobetGunKuralDetay>();

                if (nobetGunKural)
                {
                    var nobetGunKurallar = data.TarihAraligi.Select(s => s.NobetGunKuralId).Distinct().ToList();

                    var nobetGrupGunKurallar = data.NobetGrupGunKurallar
                                                    .Where(s => s.NobetGrupId == nobetGrup.Id
                                                             && nobetGunKurallar.Contains(s.NobetGunKuralId))
                                                    .Select(s => s.NobetGunKuralId);

                    foreach (var gunKuralId in nobetGrupGunKurallar)
                    {
                        var cozulenAydakiNobetSayisi = data.TarihAraligi.Where(w => w.NobetGunKuralId == gunKuralId).Count();

                        nobetGunKuralDetaylar.Add(GetNobetGunKural(gunKuralId, data, nobetGrup.Id, gruptakiNobetciSayisi, cozulenAydakiNobetSayisi));
                    }
                }
                #endregion

                #endregion

                #region Talep Kısıtları

                foreach (var nobetGrupGorevTip in data.NobetGrupGorevTipler.Where(w => w.NobetGrupId == nobetGrup.Id))
                {
                    foreach (var d in data.TarihAraligi)
                    {
                        int gunlukNobetciSayisi = (int)data.NobetGrupKurallar
                                .Where(s => s.NobetGrupId == nobetGrupGorevTip.NobetGrupId
                                         && s.NobetKuralId == 3)
                                .Select(s => s.Deger).SingleOrDefault();

                        var talepFarkli = data.NobetGrupTalepler
                                            .Where(s => s.NobetGrupGorevTipId == nobetGrupGorevTip.Id
                                                     && s.TakvimId == d.TakvimId)
                                            .Select(s => s.NobetciSayisi).SingleOrDefault();

                        if (talepFarkli > 0)
                            gunlukNobetciSayisi = talepFarkli;

                        model.AddConstraint(
                                   Expression.Sum(data.EczaneNobetTarihAralik
                                                    .Where(k => k.TakvimId == d.TakvimId
                                                             && k.NobetGrupId == nobetGrupGorevTip.NobetGrupId
                                                             && k.NobetGorevTipId == nobetGrupGorevTip.NobetGorevTipId
                                                    )
                                                    .Select(m => _x[m])) == gunlukNobetciSayisi,
                                                    $"her gune talep kadar eczane atanmali: {gunlukNobetciSayisi}");
                    }
                }

                #endregion

                #region Eczane bazlı kısıtlar

                foreach (var hedef in eczaneler)
                {
                    #region Ay içinde peşpeşe görev yazılmasın

                    var pespeseGorev = data.Kisitlar
                                .Where(s => s.KisitAdi == "pespeseGorev"
                                         && s.NobetUstGrupId == data.NobetUstGrupId)
                                .Select(w => w.PasifMi == false).SingleOrDefault();

                    if (pespeseGorev)
                    {
                        int pespeseNobetSayisi = (int)data.NobetGrupKurallar
                                .Where(s => s.NobetGrupId == nobetGrup.Id
                                         && s.NobetKuralId == 1)
                                .Select(s => s.Deger).SingleOrDefault();

                        foreach (var g in data.TarihAraligi.Take(data.TarihAraligi.Count() - pespeseNobetSayisi))
                        {
                            //model.AddConstraint(
                            //  Expression.Sum(data.EczaneNobetTarihAralik
                            //                   .Where(e => e.EczaneNobetGrupId == hedef.Id
                            //                               && (e.Gun >= g.Gun && e.Gun <= g.Gun + pespeseNobetSayisi)
                            //                               && e.NobetGrupId == nobetGrup.Id
                            //                               )
                            //                   .Select(m => _x[m])) <= 1,
                            //                   $"eczanelere_pespese_nobet_yazilmasin, {hedef}");
                        }

                    }
                    #endregion

                    #region Farklı aylar peşpeşe görev yazılmasın

                    var farkliAyPespeseGorev = data.Kisitlar
                                            .Where(s => s.KisitAdi == "farkliAyPespeseGorev"
                                                     && s.NobetUstGrupId == data.NobetUstGrupId)
                                            .Select(w => w.PasifMi == false).SingleOrDefault();

                    if (farkliAyPespeseGorev)
                    {

                        var farkliAyPespeseGorevAraligi = gruptakiNobetciSayisi * 0.6;

                        if (data.CalismaSayisi == 6 || data.CalismaSayisi == 7)
                        {
                            farkliAyPespeseGorevAraligi = gruptakiNobetciSayisi * 0.3;
                        }
                        else if (data.CalismaSayisi == 9)
                        {
                            farkliAyPespeseGorevAraligi = 5;
                        }

                        var pazarVeBayramlar = new List<int> { 1, 8, 9 };

                        var enSonNobetTarihi = data.EczaneNobetSonuclar
                            .Where(w => w.EczaneId == hedef.EczaneId
                                    && !pazarVeBayramlar.Contains(w.NobetGunKuralId))
                            .Select(s => s.TakvimId).OrderByDescending(o => o).FirstOrDefault();

                        var yazilabilecekIlkTarih = enSonNobetTarihi + farkliAyPespeseGorevAraligi;

                        if (enSonNobetTarihi > 0)
                        {
                            foreach (var g in data.TarihAraligi.Where(w => w.TakvimId < yazilabilecekIlkTarih))
                            {
                                model.AddConstraint(
                                  Expression.Sum(data.EczaneNobetTarihAralik
                                                   .Where(e => e.EczaneNobetGrupId == hedef.Id
                                                               && e.TakvimId == g.TakvimId
                                                               && e.NobetGrupId == nobetGrup.Id
                                                               )
                                                   .Select(m => _x[m])) == 0,
                                                   $"eczanelere_farkli_aylarda_pespese_nobet_yazilmasin, {hedef}");
                            }
                        }
                    }
                    #endregion                 

                    #region Toplam Cuma Max Hedef

                    var toplamCumaMaxHedef = data.Kisitlar
                                                .Where(s => s.KisitAdi == "toplamCumaMaxHedef"
                                                         && s.NobetUstGrupId == data.NobetUstGrupId)
                                                .Select(w => w.PasifMi == false).SingleOrDefault();

                    if (toplamCumaMaxHedef)
                    {
                        var toplamCumaMaxHedefSTD = data.Kisitlar
                                                        .Where(s => s.KisitAdi == "toplamCumaMaxHedef"
                                                                 && s.NobetUstGrupId == data.NobetUstGrupId)
                                                        .Select(w => w.SagTarafDegeri).SingleOrDefault();

                        if (data.CalismaSayisi == 8) toplamCumaMaxHedefSTD = toplamCumaMaxHedefSTD++;

                        var toplamCumaSayisi = data.EczaneNobetSonuclar
                            .Where(w => w.EczaneId == hedef.EczaneId
                                     && w.NobetGunKuralId == 6)
                            .Select(s => s.TakvimId).Count();

                        foreach (var g in data.TarihAraligi.Where(w => w.NobetGunKuralId == 6))
                        {
                            model.AddConstraint(
                              Expression.Sum(data.EczaneNobetTarihAralik
                                               .Where(e => e.EczaneNobetGrupId == hedef.Id
                                                           && e.TakvimId == g.TakvimId
                                                           && e.NobetGrupId == nobetGrup.Id
                                                           )
                                               .Select(m => _x[m])) + toplamCumaSayisi <= toplamCumaMaxHedefSTD,
                                               $"eczanelere_cuma_gunu_encok_yilda_3_gorev_yaz, {hedef}");
                        }
                    }
                    #endregion

                    #region Toplam Cumartesi Max Hedef

                    var toplamCumartesiMaxHedef = data.Kisitlar
                                                    .Where(s => s.KisitAdi == "toplamCumartesiMaxHedef"
                                                             && s.NobetUstGrupId == data.NobetUstGrupId)
                                                    .Select(w => w.PasifMi == false).SingleOrDefault();

                    if (toplamCumartesiMaxHedef)
                    {
                        var toplamCumartesiMaxHedefSTD = data.Kisitlar
                                                            .Where(s => s.KisitAdi == "toplamCumartesiMaxHedef"
                                                                     && s.NobetUstGrupId == data.NobetUstGrupId)
                                                            .Select(w => w.SagTarafDegeri).SingleOrDefault();

                        if (data.CalismaSayisi == 8) toplamCumartesiMaxHedefSTD = toplamCumartesiMaxHedefSTD++;

                        var toplamCumaSayisi = data.EczaneNobetSonuclar
                            .Where(w => w.EczaneId == hedef.EczaneId
                                     && w.NobetGunKuralId == 7)
                            .Select(s => s.TakvimId).Count();

                        foreach (var g in data.TarihAraligi.Where(w => w.NobetGunKuralId == 7))
                        {
                            model.AddConstraint(
                              Expression.Sum(data.EczaneNobetTarihAralik
                                               .Where(e => e.EczaneNobetGrupId == hedef.Id
                                                           && e.TakvimId == g.TakvimId
                                                           && e.NobetGrupId == nobetGrup.Id
                                                           )
                                               .Select(m => _x[m])) + toplamCumaSayisi <= toplamCumartesiMaxHedefSTD,
                                               $"eczanelere_cumartesi_gunu_encok_yilda_3_gorev_yaz, {hedef}");
                        }
                    }
                    #endregion

                    #region Toplam max

                    var toplamMaxHedef = data.Kisitlar
                                                        .Where(s => s.KisitAdi == "toplamMaxHedef"
                                                                 && s.NobetUstGrupId == data.NobetUstGrupId)
                                                        .Select(w => w.PasifMi == false).SingleOrDefault();
                    if (toplamMaxHedef)
                    {
                        var hedefler = data.EczaneKumulatifHedefler.Where(w => w.EczaneNobetGrupId == hedef.Id).SingleOrDefault();
                        var maxToplam = hedefler.Toplam;

                        if (data.Ay == 1 && fazlaYazilacakEczaneSayisi > 0)
                        {
                            if (!fazlaYazilacakEczaneler.Contains(hedef))
                            {
                                maxToplam = hedefler.Toplam - 1;
                            }
                        }

                        if (data.CalismaSayisi == 2 || data.CalismaSayisi == 4)
                        {
                            maxToplam = hedefler.Toplam <= 2 ? hedefler.Toplam + 1 : hedefler.Toplam + 2;
                        }

                        model.AddConstraint(
                         Expression.Sum(data.EczaneNobetTarihAralik
                                          .Where(e => e.EczaneNobetGrupId == hedef.Id
                                                   && e.NobetGorevTipId == 1//hedef.NobetGorevTipId
                                                   )
                                          .Select(m => _x[m])) <= maxToplam,
                                          $"her_eczaneye_ayda_en_cok_nobet_grubunun_hedefi_kadar_nobet_yazilmali, {hedef}");
                    }
                    #endregion

                    #region Toplam min

                    var toplamMinHedef = data.Kisitlar
                                                        .Where(s => s.KisitAdi == "toplamMinHedef"
                                                                 && s.NobetUstGrupId == data.NobetUstGrupId)
                                                        .Select(w => w.PasifMi == false).SingleOrDefault();
                    if (toplamMinHedef)
                    {
                        var hedefler = data.EczaneKumulatifHedefler.Where(w => w.EczaneNobetGrupId == hedef.Id).SingleOrDefault();
                        var minToplam = hedefler.Toplam - 1;

                        if (data.CalismaSayisi == 3 || data.CalismaSayisi == 4)
                        {
                            minToplam = hedefler.Toplam - 1 > 1 ? 1 : 0;
                        }

                        model.AddConstraint(
                          Expression.Sum(data.EczaneNobetTarihAralik
                                            .Where(e => e.EczaneNobetGrupId == hedef.Id
                                                     && e.NobetGorevTipId == 1//hedef.NobetGorevTipId
                                                     )
                                            .Select(m => _x[m])) >= minToplam,
                                            $"her_eczaneye_ayda_en_az_nobet_grubunun_hedefinden_1_eksik_nobet_yazilmali, {hedef}");
                    }
                    #endregion

                    #region Her ay en az 1 görev

                    var herAyEnaz1Gorev = data.Kisitlar
                                    .Where(s => s.KisitAdi == "herAyEnaz1Gorev"
                                             && s.NobetUstGrupId == data.NobetUstGrupId)
                                    .Select(w => w.PasifMi == false).SingleOrDefault();

                    if (herAyEnaz1Gorev && gruptakiNobetciSayisi < data.TarihAraligi.Count)
                    {
                        var herAyEnaz1GorevSTD = data.Kisitlar
                                                .Where(s => s.KisitAdi == "herAyEnaz1Gorev"
                                                         && s.NobetUstGrupId == data.NobetUstGrupId)
                                                .Select(w => w.SagTarafDegeri).SingleOrDefault();

                        model.AddConstraint(
                              Expression.Sum(data.EczaneNobetTarihAralik
                                                .Where(e => e.EczaneNobetGrupId == hedef.Id
                                                         && e.NobetGorevTipId == 1//hedef.NobetGorevTipId
                                                         )
                                                .Select(m => _x[m])) >= herAyEnaz1GorevSTD,
                                                $"her eczaneye bir ayda en az nobet grubunun hedefinden bir eksik nobet yazilmali, {hedef}");
                    }
                    #endregion

                    #region Her ay en fazla 1 Pazar

                    var herAyEnFazla1Pazar = data.Kisitlar
                                                        .Where(s => s.KisitAdi == "herAyEnFazla1Pazar"
                                                                 && s.NobetUstGrupId == data.NobetUstGrupId)
                                                        .Select(w => w.PasifMi == false).SingleOrDefault();
                    if (herAyEnFazla1Pazar)
                    {
                        model.AddConstraint(
                              Expression.Sum(data.EczaneNobetTarihAralik
                                                .Where(e => e.EczaneNobetGrupId == hedef.Id
                                                         && e.NobetGorevTipId == 1//hedef.NobetGorevTipId
                                                         && e.NobetGunKuralId == 1)
                                                .Select(m => _x[m])) <= 1,
                                                $"her eczaneye bir ayda en az nobet grubunun hedefinden bir eksik nobet yazilmali, {hedef}");
                    }
                    #endregion

                    #region Nöbet gün kurallar

                    if (nobetGunKural)
                    {
                        foreach (var item in nobetGunKuralDetaylar)
                        {
                            var gunKuralId = item.GunKuralId;

                            if (data.CalismaSayisi == 2)
                            {
                                if (gunKuralId == 2) continue;
                            }
                            else if (data.CalismaSayisi == 3)
                            {
                                if (gunKuralId == 3) continue;
                            }
                            else if (data.CalismaSayisi == 4)
                            {
                                if (gunKuralId == 4) continue;
                            }
                            else if (data.CalismaSayisi == 5)
                            {
                                if (gunKuralId == 5) continue;
                            }

                            var gunKuralNobetSayisi = data.EczaneNobetSonuclar
                                .Where(w => w.EczaneNobetGrupId == hedef.Id
                                         && w.NobetGunKuralId == gunKuralId)
                                .Select(s => s.TakvimId).Count();

                            var Std = item.KuralSTD;

                            if (!item.FazlaNobetTutacakEczaneler.Contains(hedef.Id) && item.FazlaNobetTutacakEczaneler.Count > 0)
                            {
                                Std = item.KuralSTD - 1;
                            }

                            //eczanelerin nöbet tutacakları günler
                            var tarihler = data.TarihAraligi.Where(w => w.NobetGunKuralId == gunKuralId).Select(s => s.TakvimId).ToList();

                            if (gunKuralNobetSayisi <= Std)
                            {
                                model.AddConstraint(
                                   Expression.Sum(data.EczaneNobetTarihAralik
                                                    .Where(e => e.EczaneNobetGrupId == hedef.Id
                                                                && tarihler.Contains(e.TakvimId)
                                                                && e.NobetGorevTipId == 1//hedef.NobetGorevTipId
                                                                )
                                                    .Select(m => _x[m])) + gunKuralNobetSayisi <= Std,
                                                    $"eczanelere_gun_kurallarini_periyodik_yaz, {item}-{hedef}");
                            }
                            else
                            {
                                model.AddConstraint(
                                   Expression.Sum(data.EczaneNobetTarihAralik
                                                    .Where(e => e.EczaneNobetGrupId == hedef.Id
                                                                && tarihler.Contains(e.TakvimId)
                                                                && e.NobetGorevTipId == 1//hedef.NobetGorevTipId
                                                                )
                                                    .Select(m => _x[m])) == 0,
                                                    $"eczanelere_gun_kurallarini_periyodik_yaz, {item}-{hedef}");
                            }
                        }
                    }
                    #endregion

                    #region Her ay en fazla 1 görev

                    var herAyEnFazla1Gorev = data.Kisitlar
                                    .Where(s => s.KisitAdi == "herAyEnFazla1Gorev"
                                             && s.NobetUstGrupId == data.NobetUstGrupId)
                                    .Select(w => w.PasifMi == false).SingleOrDefault();

                    if (data.CalismaSayisi == 5 || data.CalismaSayisi == 7)
                    {
                        //herAyEnFazla1Gorev = false;
                    }

                    if (herAyEnFazla1Gorev)
                    {
                        if (gruptakiNobetciSayisi >= data.TarihAraligi.Count)
                        {
                            model.AddConstraint(
                              Expression.Sum(data.EczaneNobetTarihAralik
                                                .Where(e => e.EczaneNobetGrupId == hedef.Id
                                                         && e.NobetGorevTipId == 1//hedef.NobetGorevTipId
                                                         )
                                                .Select(m => _x[m])) <= 1,
                                                $"her_eczaneye_bir_ayda_en_fazla_bir_nobet_yazilmali, {hedef}");
                        }
                    }
                    #endregion              

                    #region Bayram toplam hedefleri

                    if (bayramlar.Count() > 0)
                    {
                        var bayramToplamMaxHedef = data.Kisitlar
                                                    .Where(s => s.KisitAdi == "bayramToplamMaxHedef"
                                                             && s.NobetUstGrupId == data.NobetUstGrupId)
                                                    .Select(w => w.PasifMi == false).SingleOrDefault();
                        if (bayramToplamMaxHedef)
                        {
                            var hedefler = data.EczaneKumulatifHedefler.Where(w => w.EczaneNobetGrupId == hedef.Id).SingleOrDefault();
                            var toplamBayramMax = hedefler.ToplamBayram;
                            if (toplamBayramMax < 1) toplamBayramMax = 0;

                            model.AddConstraint(
                              Expression.Sum(data.EczaneNobetTarihAralik
                                            .Where(e => e.EczaneNobetGrupId == hedef.Id
                                                    && e.NobetGunKuralId > 7)
                                            .Select(m => _x[m])) <= toplamBayramMax,
                                            $"her eczaneye bir ayda nobet grubunun hedefi kadar toplam bayram nobeti yazilmali, {hedef}");
                        }

                        var bayramToplamMinHedef = data.Kisitlar
                                                    .Where(s => s.KisitAdi == "bayramToplamMinHedef"
                                                             && s.NobetUstGrupId == data.NobetUstGrupId)
                                                    .Select(w => w.PasifMi == false).SingleOrDefault();
                        if (bayramToplamMinHedef)
                        {
                            var hedefler = data.EczaneKumulatifHedefler.Where(w => w.EczaneNobetGrupId == hedef.Id).SingleOrDefault();
                            var toplamBayramMin = hedefler.ToplamBayram - 1;
                            if (toplamBayramMin < 1) toplamBayramMin = 0;

                            model.AddConstraint(
                              Expression.Sum(data.EczaneNobetTarihAralik
                                            .Where(e => e.EczaneNobetGrupId == hedef.Id
                                                    && e.NobetGunKuralId > 7)
                                            .Select(m => _x[m])) >= toplamBayramMin,
                                            $"her eczaneye bir ayda nobet grubunun hedefi kadar toplam bayram nobeti yazilmali, {hedef}");
                        }
                    }
                    #endregion

                    #region Hafta içi toplam max hedefler

                    var haftaIciToplamMaxHedef = data.Kisitlar
                                                .Where(s => s.KisitAdi == "haftaIciToplamMaxHedef"
                                                         && s.NobetUstGrupId == data.NobetUstGrupId)
                                                .Select(w => w.PasifMi == false).SingleOrDefault();
                    if (haftaIciToplamMaxHedef)
                    {
                        var hedefler = data.EczaneKumulatifHedefler.Where(w => w.EczaneNobetGrupId == hedef.Id).SingleOrDefault();
                        var haftaIciToplam = hedefler.ToplamHaftaIci;
                        model.AddConstraint(
                          Expression.Sum(data.EczaneNobetTarihAralik
                                        .Where(e => e.EczaneNobetGrupId == hedef.Id
                                                && (e.NobetGunKuralId >= 2 && e.NobetGunKuralId <= 7))
                                        .Select(m => _x[m])) <= haftaIciToplam,
                                        $"her eczaneye bir ayda nobet grubunun hedefi kadar toplam hafta içi nobeti yazilmali, {hedef}");
                    }
                    #endregion

                    #region Hafta içi toplam min hedefler

                    var haftaIciToplamMinHedef = data.Kisitlar
                                        .Where(s => s.KisitAdi == "haftaIciToplamMinHedef"
                                                 && s.NobetUstGrupId == data.NobetUstGrupId)
                                        .Select(w => w.PasifMi == false).SingleOrDefault();

                    if (data.CalismaSayisi == 3 || data.CalismaSayisi == 4)
                    {
                        haftaIciToplamMinHedef = false;
                    }

                    if (haftaIciToplamMinHedef)
                    {
                        var hedefler = data.EczaneKumulatifHedefler.Where(w => w.EczaneNobetGrupId == hedef.Id).SingleOrDefault();
                        var haftaIciToplam = hedefler.ToplamHaftaIci - 1;

                        model.AddConstraint(
                          Expression.Sum(data.EczaneNobetTarihAralik
                                        .Where(e => e.EczaneNobetGrupId == hedef.Id
                                                && (e.NobetGunKuralId >= 2 && e.NobetGunKuralId <= 7))
                                        .Select(m => _x[m])) >= haftaIciToplam,
                                        $"her eczaneye bir ayda nobet grubunun hedefi kadar toplam hafta içi nobeti yazilmali, {hedef}");
                    }
                    #endregion

                    #region Toplam cuma cumartesi max hedefler

                    var toplamCumaCumartesiMaxHedef = data.Kisitlar
                                                .Where(s => s.KisitAdi == "toplamCumaCumartesiMaxHedef"
                                                         && s.NobetUstGrupId == data.NobetUstGrupId)
                                                .Select(w => w.PasifMi == false).SingleOrDefault();

                    if (toplamCumaCumartesiMaxHedef)
                    {
                        var hedefler = data.EczaneKumulatifHedefler.Where(w => w.EczaneNobetGrupId == hedef.Id).SingleOrDefault();
                        var cumaCumartesiToplam = hedefler.ToplamCumaCumartesi;

                        if (data.CalismaSayisi == 2 || data.CalismaSayisi == 4)
                        {
                            cumaCumartesiToplam = cumaCumartesiToplam++;
                        }

                        model.AddConstraint(
                          Expression.Sum(data.EczaneNobetTarihAralik
                                        .Where(e => e.EczaneNobetGrupId == hedef.Id
                                                && (e.NobetGunKuralId >= 6 && e.NobetGunKuralId <= 7))
                                        .Select(m => _x[m])) <= cumaCumartesiToplam,
                                        $"her eczaneye bir ayda nobet grubunun hedefi kadar toplam hafta içi nobeti yazilmali, {hedef}");
                    }
                    #endregion

                    #region Toplam cuma cumartesi min hedefler

                    var toplamCumaCumartesiMinHedef = data.Kisitlar
                                        .Where(s => s.KisitAdi == "toplamCumaCumartesiMinHedef"
                                                 && s.NobetUstGrupId == data.NobetUstGrupId)
                                        .Select(w => w.PasifMi == false).SingleOrDefault();

                    if (toplamCumaCumartesiMinHedef)
                    {
                        var hedefler = data.EczaneKumulatifHedefler.Where(w => w.EczaneNobetGrupId == hedef.Id).SingleOrDefault();
                        var cumaCumartesiToplam = hedefler.ToplamCumaCumartesi - 1;

                        if (data.CalismaSayisi == 2 || data.CalismaSayisi == 4)
                        {
                            cumaCumartesiToplam = cumaCumartesiToplam--;
                        }

                        model.AddConstraint(
                          Expression.Sum(data.EczaneNobetTarihAralik
                                        .Where(e => e.EczaneNobetGrupId == hedef.Id
                                                && (e.NobetGunKuralId >= 6 && e.NobetGunKuralId <= 7))
                                        .Select(m => _x[m])) >= cumaCumartesiToplam,
                                        $"her eczaneye bir ayda nobet grubunun hedefi kadar toplam hafta içi nobeti yazilmali, {hedef}");
                    }
                    #endregion

                    #region Diğer Günlerin Hedefleri

                    //gunDegerler = data.TarihAraligi.Select(s => s.GunDegerId).Distinct().ToList();
                    //gunKurallar = data.NobetGrupGunKurallar
                    //                            .Where(s => s.NobetGrupId == nobetGrup.Id)
                    //                            .Select(s => s.NobetGunKuralId);

                    //var gunler = gunDegerler.Where(s => gunKurallar.Contains(s)).ToList();

                    //foreach (var gunDeger in gunler)
                    //{
                    //    GetEczaneGunHedef(hedef, out double maxArz, out double minArz, gunDeger);

                    //    var digerGunlerMaxHedef = data.NobetUstGrupKisitlar
                    //                                .Where(s => s.KisitAdi == "digerGunlerMaxHedef"
                    //                                         && s.NobetUstGrupId == data.NobetUstGrupId)
                    //                                .Select(w => w.PasifMi == false).SingleOrDefault();

                    //    if (digerGunlerMaxHedef)
                    //    {
                    //        if (maxArz < 1) maxArz = 0;

                    //        //if (gunDeger == 1 && pazarSayisi > pazarEksikEczaneler.Count)
                    //        //{
                    //        //    //pazar günü yazılacak eczane sayısı azsa, ihtiyaç kadar eczaneye 1 pazar ekle
                    //        //    if (pazarYazilacakEczaneler.Contains(hedef))
                    //        //    {
                    //        //        maxArz = hedef.Pazar + 1;
                    //        //    }

                    //        //}

                    //        model.AddConstraint(
                    //          Expression.Sum(data.EczaneNobetTarihAralik
                    //                           .Where(e => e.EczaneNobetGrupId == hedef.Id
                    //                                       && e.GunDegerId == gunDeger
                    //                                       && e.NobetGrupId == nobetGrup.Id)
                    //                           .Select(m => _x[m])) <= maxArz,
                    //                           $"her eczaneye bir ayda nobet grubunun {gunDeger} hedefi kadar nobet yazilmali, {hedef}");
                    //    }

                    //    var digerGunlerMinHedef = data.NobetUstGrupKisitlar
                    //                                .Where(s => s.KisitAdi == "digerGunlerMinHedef"
                    //                                         && s.NobetUstGrupId == data.NobetUstGrupId)
                    //                                .Select(w => w.PasifMi == false).SingleOrDefault();

                    //    if (digerGunlerMinHedef)
                    //    {

                    //        if (minArz < 1) minArz = 0;

                    //        model.AddConstraint(
                    //            Expression.Sum(data.EczaneNobetTarihAralik
                    //                            .Where(e => e.EczaneNobetGrupId == hedef.Id
                    //                                        && e.GunDegerId == gunDeger
                    //                                        && e.NobetGrupId == nobetGrup.Id
                    //                                        )
                    //                            .Select(m => _x[m])) >= minArz,
                    //                            $"her eczaneye bir ayda nobet grubunun {gunDeger} hedefi kadar nobet yazilmali, {hedef}");
                    //    }
                    //}
                    #endregion
                }
                #endregion

                #region Eczane grup kısıtı

                var eczaneGrup = data.Kisitlar
                                            .Where(s => s.KisitAdi == "eczaneGrup"
                                                        && s.NobetUstGrupId == data.NobetUstGrupId)
                                            .Select(w => w.PasifMi == false).SingleOrDefault();

                if (eczaneGrup)
                {
                    foreach (var g in data.EczaneGrupTanimlar)
                    {
                        var eczaneGruplar = data.EczaneGruplar
                                                .Where(x => x.EczaneGrupTanimId == g.Id)
                                                .Select(s => s.EczaneId).Distinct().ToList();

                        var gunler = data.EczaneGrupNobetSonuclar.Where(w => eczaneGruplar.Contains(w.EczaneId)).ToList();

                        //değişkendeki eczaneleri filtrelemek için
                        var eczaneGruplar2 = data.EczaneGruplar
                                                .Where(x => x.EczaneGrupTanimId == g.Id
                                                         && x.NobetGrupId == nobetGrup.Id)
                                                .Select(s => s.EczaneId).Distinct().ToList();

                        foreach (var tarih in gunler)
                        {
                            var grupTanimlar = Expression.Sum(data.EczaneNobetTarihAralik
                                            .Where(e => eczaneGruplar2.Contains(e.EczaneId)
                                                    && e.TakvimId == tarih.TakvimId
                                                    )
                                            .Select(m => _x[m])) == 0;
                            model.AddConstraint(grupTanimlar,
                                                        $"eczaneGrupTanimdaki_eczaneler_birlikte_nobet_tutmasin, {tarih.TakvimId}");
                        }
                    }
                }
                #endregion

                #region İsteğe Görev Yazılsın

                var istek = data.Kisitlar
                                .Where(s => s.KisitAdi == "istek"
                                            && s.NobetUstGrupId == data.NobetUstGrupId)
                                .Select(w => w.PasifMi == false).SingleOrDefault();

                if (istek)
                {
                    foreach (var f in data.EczaneNobetIstekler.Where(x => x.NobetGrupId == nobetGrup.Id))
                    {
                        model.AddConstraint(
                                  Expression.Sum(data.EczaneNobetTarihAralik
                                                   .Where(e => e.EczaneNobetGrupId == f.EczaneNobetGrupId
                                                            && e.TakvimId == f.TakvimId)
                                                   .Select(m => _x[m])) == 1,
                                                   $"istege nobet yaz, {f}");
                    }
                }
                #endregion

                #region Mazerete Görev Yazılmasın

                var mazeret = data.Kisitlar
                                .Where(s => s.KisitAdi == "mazeret"
                                            && s.NobetUstGrupId == data.NobetUstGrupId)
                                .Select(w => w.PasifMi == false).SingleOrDefault();
                if (mazeret)
                {
                    foreach (var f in data.EczaneNobetMazeretler.Where(x => x.NobetGrupId == nobetGrup.Id))
                    {
                        model.AddConstraint(
                                  Expression.Sum(data.EczaneNobetTarihAralik
                                                   .Where(e => e.EczaneNobetGrupId == f.EczaneNobetGrupId
                                                            && e.TakvimId == f.TakvimId)
                                                   .Select(m => _x[m])) == 0,
                                                   $"mazerete nobet yazma, {f}");
                    }
                }
                #endregion

                #region Bayramlarda en fazla 1 görev yazılsın.

                var bayram = data.Kisitlar
                                .Where(s => s.KisitAdi == "bayram"
                                            && s.NobetUstGrupId == data.NobetUstGrupId)
                                .Select(w => w.PasifMi == false).SingleOrDefault();
                if (bayram)
                {
                    int pespeseNobetSayisi = (int)data.NobetGrupKurallar
                            .Where(s => s.NobetGrupId == nobetGrup.Id
                                     && s.NobetKuralId == 1)
                            .Select(s => s.Deger).SingleOrDefault();

                    if (data.TarihAraligi.Where(w => w.NobetGunKuralId > 7).Count() > pespeseNobetSayisi)
                    {//eğer bayram günleri peşpeşe nöbet sayısından fazlaysa bayramlarda en fazla 1 görev yazılsın
                        foreach (var f in eczaneler)
                        {
                            foreach (var g in data.TarihAraligi.Where(w => w.NobetGunKuralId > 7))
                            {
                                //model.AddConstraint(
                                //  Expression.Sum(data.EczaneNobetTarihAralik
                                //                   .Where(e => e.EczaneNobetGrupId == f.Id
                                //                               && e.Gun == g.Gun
                                //                         )
                                //                   .Select(m => _x[m])) <= 1,
                                //                   $"bayram nobeti sinirla, {f}");
                            }
                        }
                    }
                }
                #endregion
            }

            #endregion

            return model;
        }

        public EczaneNobetSonucModel Solve(AntalyaMerkezDataModel data)
        {
            EczaneNobetSonucModel Results;

            var config = new Configuration
            {
                NameHandling = NameHandlingStyle.UniqueLongNames,
                ComputeRemovedVariables = true
            };

            try
            {
                using (var scope = new ModelScope(config))
                {
                    var model = Model(data);
                    // Get a solver instance, change your solver
                    var solver = new CplexSolver();

                    // solve the model
                    var solution = solver.Solve(model);
                    var modelStatus = solution.ModelStatus;
                    var solutionStatus = solution.Status;
                    var modelName = solution.ModelName;

                    //var confilicts = new ConflictingSet();
                    //confilicts = solution.ConflictingSet;
                    //ConstraintsUB = new IEnumerable<Constraint>();
                    //ConstraintsUB = confilicts.ConstraintsUB;

                    if (modelStatus != ModelStatus.Feasible)
                    {
                        data.CalismaSayisi++;
                        throw new Exception("Uygun çözüm bulunamadı!");
                    }
                    else
                    {
                        // import the results back into the model 
                        model.VariableCollections.ForEach(vc => vc.SetVariableValues(solution.VariableValues));
                        var objective = solution.ObjectiveValues.Single();
                        var sure = solution.OverallWallTime;
                        var bestBound = solution.BestBound;

                        Results = new EczaneNobetSonucModel
                        {
                            CozumSuresi = sure,
                            ObjectiveValue = objective.Value,
                            ResultModel = new List<EczaneNobetCozum>()
                        };

                        foreach (var r in data.EczaneNobetTarihAralik.Where(s => _x[s].Value == 1))
                        {
                            Results.ResultModel.Add(new EczaneNobetCozum()
                            {
                                TakvimId = r.TakvimId,
                                EczaneNobetGrupId = r.EczaneNobetGrupId,
                                NobetGorevTipId = r.NobetGorevTipId
                            });
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                if (data.CalismaSayisi < 10)
                {
                    Results = Solve(data);
                }
                else
                {//çözüm yok
                    var mesaj = ex.Message;

                    switch (data.CalismaSayisi)
                    {
                        case 1:
                            mesaj += $"1: Kendini tekrar çağırdı! ";
                            break;
                        case 2:
                            mesaj += $"2: Toplam max hedef gevşetildi! ";
                            break;
                        case 3:
                            mesaj += $"3: Toplam min hedef gevşetildi! ";
                            break;
                        case 4:
                            mesaj += $"4: Toplam max ve min hedef gevşetildi! ";
                            break;
                        case 5:
                            mesaj += $"5: Ayda en fazla 1 gorev kaldırıldı! ";
                            break;
                        case 6:
                            mesaj += $"6: Farklı ay peşpeşe görev gevşetildi! ";
                            break;
                        case 7:
                            mesaj += $"7: Ayda en fazla 1 gorev kaldırıldı ve Farklı Ay Peşpeşe Görev gevşetildi! ";
                            break;
                        case 8:
                            mesaj += $"8: Cuma ve cumartesi en fazla 3 olmadı 4 olarak gevşetildi! ";
                            break;
                        case 9:
                            mesaj += $"9: Farklı ay peşpeşe görev sayısı en çok 5 olarak gevşetildi! ";
                            break;
                        default:
                            var iterasyonMesaj = "";
                            if (data.CozumItereasyon.IterasyonSayisi > 0)
                            {
                                iterasyonMesaj = $"\r\n Ay içinde aynı gün nöbet tutan eczaneler {data.CozumItereasyon.IterasyonSayisi} adet dinamik iterasyon yapılarak engellendi.";
                            }
                            mesaj += $"{data.NobetGruplar.Select(s => s.Adi).FirstOrDefault()} {data.Yil} {data.Ay}.ayda bazı kısıtlar ({mesaj}) gevşetilerek {data.CalismaSayisi} kez çözüm denendi.{iterasyonMesaj} Bu grup çözüm uygun çözüm alanı bulunmamaktadır. Lütfen kısıtları kontrol edip tekrar deneyiniz..";
                            break;
                    }

                    throw new Exception(mesaj);
                }
            }
            return Results;
        }

        private void GetEczaneGunHedef(EczaneNobetIstatistik hedef, out double maxArz, out double minArz, int gunDeger)
        {
            switch (gunDeger)
            {
                case 1:
                    maxArz = hedef.Pazar;
                    minArz = maxArz - 1;
                    if (minArz < 0) minArz = 0;

                    break;
                case 2:
                    maxArz = hedef.Pazartesi;
                    minArz = maxArz - 1;
                    if (minArz < 0) minArz = 0;
                    break;
                case 3:
                    maxArz = hedef.Sali;
                    minArz = maxArz - 1;
                    if (minArz < 0) minArz = 0;
                    break;
                case 4:
                    maxArz = hedef.Carsamba;
                    minArz = maxArz - 1;
                    if (minArz < 0) minArz = 0;
                    break;
                case 5:
                    maxArz = hedef.Persembe;
                    minArz = maxArz - 1;
                    if (minArz < 0) minArz = 0;
                    break;
                case 6:
                    maxArz = hedef.Cuma;
                    minArz = maxArz - 1;
                    if (minArz < 0) minArz = 0;
                    break;
                case 7:
                    maxArz = hedef.Cumartesi;
                    minArz = maxArz - 1;
                    if (minArz < 0) minArz = 0;
                    break;
                case 8:
                    maxArz = hedef.DiniBayram;
                    minArz = maxArz - 1;
                    if (minArz < 0) minArz = 0;
                    break;
                case 9:
                    maxArz = hedef.MilliBayram;
                    minArz = maxArz - 1;
                    if (minArz < 0) minArz = 0;
                    break;
                default:
                    maxArz = hedef.Toplam;
                    minArz = maxArz - 1;
                    if (minArz < 0) minArz = 0;
                    break;
            }
        }

        private class NobetGunKuralDetay
        {
            public List<int> FazlaNobetTutacakEczaneler { get; set; }
            public double KuralSTD { get; set; }
            public double GunKuralId { get; set; }
            public double OrtalamaNobetSayisi { get; set; }
            public double EnCokNobetTutulanNobetSayisi { get; set; }
        }

        private NobetGunKuralDetay GetNobetGunKural(int gunKuralId, AntalyaMerkezDataModel data, int nobetGrupId, int gruptakiNobetciSayisi, int cozulenAydakiNobetSayisi)
        {
            var r = new Random();

            var eczaneler = data.EczaneKumulatifHedefler
                  .Where(w => w.NobetGrupId == nobetGrupId)
                  .OrderBy(x => r.NextDouble()).ToList();

            //eczanelerin nöbet sayıları
            var nobetTutanEczaneler = data.EczaneNobetSonuclar
                .Where(w => w.NobetGunKuralId == gunKuralId
                         && w.NobetGrupId == nobetGrupId)
                .GroupBy(g => g.EczaneNobetGrupId)
                .Select(s => new
                {
                    EczaneNobetGrupId = s.Key,
                    NobetSayisi = s.Count(),
                    EnSonNobetTuttuguAy = s.Max(c => c.Tarih.Month)
                }).ToList();

            var oncekiAydakiNobetSayisi = nobetTutanEczaneler.Count();
            var toplamNobetSayisi = oncekiAydakiNobetSayisi + cozulenAydakiNobetSayisi;
            var ortalamaNobetSayisi = (double)(toplamNobetSayisi) / gruptakiNobetciSayisi;

            var kuralStd = Math.Ceiling(ortalamaNobetSayisi);

            var tumEczaneler = eczaneler
                .Select(s => new
                {
                    s.EczaneNobetGrupId,
                    NobetSayisi = nobetTutanEczaneler
                        .Where(w => w.EczaneNobetGrupId == s.EczaneNobetGrupId)
                        .Select(f => f.NobetSayisi).FirstOrDefault(),
                    EnSonNobetTuttuguAy = nobetTutanEczaneler
                        .Where(w => w.EczaneNobetGrupId == s.EczaneNobetGrupId)
                        .Select(f => f.EnSonNobetTuttuguAy).FirstOrDefault()
                }).ToList();

            var eczaneNobetIstatistik = tumEczaneler//nobetTutanEczaneler
                .Where(w => w.NobetSayisi == kuralStd - 1)
                .Select(s => s.EczaneNobetGrupId).ToList();

            var cozulenAydakiEksikNobetiOlanSayisi = cozulenAydakiNobetSayisi - eczaneNobetIstatistik.Count;//nobetTutanEczaneNobetGruplar.Count();

            if (cozulenAydakiEksikNobetiOlanSayisi > 0) kuralStd++;

            var fazlaNobetTutacakEczaneler = new List<int>();

            double enCokNobetTutulanNobetSayisi;

            //periyot başlamadan buraya girmemeli (nobetTutanEczaneler.Count() == 0)
            //if (data.Ay == 1) ayicindeEnCokNobetTutanEczaneSayisi = 100;

            if (nobetTutanEczaneler.Count() > 0)
            {
                enCokNobetTutulanNobetSayisi = nobetTutanEczaneler.Select(s => s.NobetSayisi).Max();
            }
            else
            {
                enCokNobetTutulanNobetSayisi = 100;
            }

            //hiç nöbet tutan yoksa bu işlem yapılmaz
            if (enCokNobetTutulanNobetSayisi < kuralStd)
            {
                var nobetiEksikEczaneNobetGruplar = eczaneler
                    .Where(w => !eczaneNobetIstatistik.Contains(w.EczaneNobetGrupId));//!nobetTutanEczaneNobetGruplar.Contains(w.EczaneNobetGrupId));

                var fazlaNobetTutacakEczaneSayisi = cozulenAydakiNobetSayisi - nobetiEksikEczaneNobetGruplar.Count();

                if (fazlaNobetTutacakEczaneSayisi > 0)
                {
                    if (gunKuralId == 1)
                    {
                        fazlaNobetTutacakEczaneler = nobetTutanEczaneler
                        .Where(w => w.NobetSayisi == kuralStd - 1
                                 //&& w.EnSonNobetTuttuguAy < (data.Ay - 3)
                                 )
                        .OrderBy(o => r.NextDouble())
                        .Select(s => s.EczaneNobetGrupId).Take(fazlaNobetTutacakEczaneSayisi).ToList();
                    }
                    else
                    {
                        fazlaNobetTutacakEczaneler = nobetTutanEczaneler
                        .Where(w => w.NobetSayisi == kuralStd - 1)
                        .OrderBy(o => r.NextDouble())
                        .Select(s => s.EczaneNobetGrupId).Take(fazlaNobetTutacakEczaneSayisi).ToList();
                    }
                }

                if (cozulenAydakiEksikNobetiOlanSayisi > 0)
                {
                    if (gunKuralId == 1)
                    {
                        fazlaNobetTutacakEczaneler = nobetTutanEczaneler
                        .Where(w => w.NobetSayisi == kuralStd - 1
                                 //&& w.EnSonNobetTuttuguAy < (data.Ay - 3)
                                 )
                        .OrderBy(o => r.NextDouble())
                        .Select(s => s.EczaneNobetGrupId).Take(cozulenAydakiEksikNobetiOlanSayisi).ToList();
                    }
                    else
                    {
                        fazlaNobetTutacakEczaneler = nobetTutanEczaneler
                        .Where(w => w.NobetSayisi == kuralStd - 1)
                        .OrderBy(o => r.NextDouble())
                        .Select(s => s.EczaneNobetGrupId).Take(cozulenAydakiEksikNobetiOlanSayisi).ToList();
                    }

                }
            }

            var nobetGunKuralDetay = new NobetGunKuralDetay
            {
                FazlaNobetTutacakEczaneler = fazlaNobetTutacakEczaneler,
                KuralSTD = kuralStd,
                GunKuralId = gunKuralId,
                OrtalamaNobetSayisi = ortalamaNobetSayisi,
                EnCokNobetTutulanNobetSayisi = enCokNobetTutulanNobetSayisi
            };

            return nobetGunKuralDetay;
        }
    }
}

#region Eczane grup kısıtı
//Birbiri ile coğrafi yakınlık ya da eş durumu gibi nedenlerle eşleştirilen farklı gruplardaki eczanelere aynı gün aralığında nöbet yazılmasın
//var eczaneGrup = data.NobetUstGrupKisitlar
//                .Where(s => s.KisitAdi == "eczaneGrup"
//                            && s.NobetUstGrupId == data.NobetUstGrupId)
//                .Select(w => w.PasifMi == false).SingleOrDefault();
//if (eczaneGrup)
//{
//foreach (var g in data.EczaneGrupTanimlar)
//{
//    var eczaneler = data.EczaneGruplar
//                            .Where(x => x.EczaneGrupTanimId == g.Id)
//                            .Select(s => s.EczaneId).Distinct().ToList();

//    //var nobetGruplar = data.EczaneNobetGruplar
//    //    .Where(s => eczaneler.Contains(s.EczaneId))
//    //    .Select(w => w.NobetGrupId).Distinct();

//    foreach (var tarih in data.TarihAraligi.Take(data.TarihAraligi.Count() - g.ArdisikNobetSayisi))
//    {
//        var grupTanimlar = Expression.Sum(data.EczaneNobetTarihAralik
//                        .Where(e => eczaneler.Contains(e.EczaneId)
//                                    && (e.GunId >= tarih.GunId && e.GunId <= tarih.GunId + g.ArdisikNobetSayisi)
//                                )
//                        .Select(m => _x[m])) <= 1;
//        model.AddConstraint(grupTanimlar,
//                                    $"eczaneGrupTanimdaki_eczaneler_birlikte_nobet_tutmasin, {tarih.GunId}");
//    }
//}
//}
#endregion
//Her eczane bir ayda diğer gruptaki bir eczane ile en fazla bir kez nöbet tutsun
#region v1
//foreach (var hedef in data.EczaneKumulatifHedefler)
//{
//    var digerEczaneler = data.EczaneKumulatifHedefler.Where(s => s.NobetGrupId > hedef.NobetGrupId).ToList();

//    foreach (var hedef2 in digerEczaneler)
//    {
//        var eczaneler = new List<int>
//            {
//                hedef.EczaneId,
//                hedef2.EczaneId
//            };

//        foreach (var tarih in data.TarihAraligi)
//        {
//            foreach (var tarih2 in data.TarihAraligi.Where(s => s.TakvimId > tarih.TakvimId))
//            {
//                var sonuc = data.EczaneNobetTarihAralik
//                                        .Where(e => eczaneler.Contains(e.EczaneId)
//                                                 && (e.TakvimId == tarih.TakvimId || e.TakvimId == tarih2.TakvimId))
//                                        .Select(m => _x[m]).ToList();

//                _modelCokGrup.AddConstraint(
//                      Expression.Sum(data.EczaneNobetTarihAralik
//                                       .Where(e => eczaneler.Contains(e.EczaneId)
//                                                && (e.TakvimId == tarih.TakvimId || e.TakvimId == tarih2.TakvimId))
//                                       .Select(m => _x[m])) <= 3,
//                                       $"her eczane bir ayda diğer gruptaki bir eczane ile en fazla bir kez nöbet tutsun, {hedef}");
//            }
//        }
//    }
//}
#endregion

#region v2
//foreach (var eczane1 in data.EczaneKumulatifHedefler)
//{
//    var digerEczaneler = data.EczaneKumulatifHedefler.Where(s => s.NobetGrupId > eczane1.NobetGrupId).ToList();

//    foreach (var eczane2 in digerEczaneler)
//    {
//        var eczaneler = new List<int>
//            {
//                eczane1.EczaneId,
//                eczane2.EczaneId
//            };

//        foreach (var tarih in data.TarihAraligi)
//        {
//            _modelCokGrup.AddConstraint(
//                  Expression.Sum(data.EczaneNobetTarihAralik
//                                   .Where(e => eczaneler.Contains(e.EczaneId)
//                                            && (e.TakvimId == tarih.TakvimId))
//                                   .Select(m => _x[m])) <= 2 + ((data.EczaneNobetTarihAralik
//                                                                            .Where(e => e.EczaneId == eczane1.EczaneId
//                                                                                    && e.TakvimId == tarih.TakvimId)
//                                                                            .Select(m => _y[m])).SingleOrDefault() - 1),
//                            $"her eczane bir ayda diğer gruptaki bir eczane ile en fazla bir kez nöbet tutsun, {eczane1}");
//        }
//    }

//    _modelCokGrup.AddConstraint(
//                  Expression.Sum(data.EczaneNobetTarihAralik
//                                   .Where(e => e.EczaneId == eczane1.EczaneId)
//                                   .Select(m => _y[m])) <= 1 ,
//                                   $"her eczane bir ayda diğer gruptaki bir eczane ile en fazla bir kez nöbet tutsun2, {eczane1}");
//} 
#endregion

#region v3

//var _ayIcindekiCiftGrupluEczaneler = data.AyIcindekiCiftGrupOlanEczaneler.Select(s => s.Id).Distinct().ToList();

//foreach (var g in _ayIcindekiCiftGrupluEczaneler)
//{
//    foreach (var tarih in data.TarihAraligi)
//    {
//        _modelCokGrup.AddConstraint(
//                 Expression.Sum(data.EczaneNobetTarihAralik
//                                  .Where(e => data.AyIcindekiCiftGrupOlanEczaneler
//                                                .Where(w => w.Id == g)
//                                                .Select(s => s.EczaneId).Contains(e.EczaneId)
//                                           && e.TakvimId == tarih.TakvimId)
//                                  .Select(m => _x[m])) <= 1,
//                                  $"her eczaneye bir ayda nöbet grubunun hedefi kadar nöbet yazılmalı, {tarih.GunId}");
//    }
//}
#endregion

#region v5
//ay içinde iki kez nöbet tutan eczane çiftleri nedeniyle çözüm yeniden yapılıyor.  
//Yeni modelde bu çiftlerin herhangi iki gündeki toplamları 3'ten küçük olma kısıtı eklenince 
//sadece 1 kez çift olmaları sağlanıyor.

//var _ayIcindekiCiftGrupluEczaneler = data.AyIcindekiCiftGrupOlanEczaneler.Select(s => s.Id).Distinct().ToList();

//foreach (var g in eczaneCiftGruplar2)
//{
//    var ikiliEczaneler = data.CiftGrupOlanEczaneler2
//                                    .Where(w => w.Id == g)
//                                    .Select(s => s.EczaneId).ToList();

//    foreach (var tarih in data.TarihAraligi.Take(data.TarihAraligi.Count() - 1))
//    {
//        foreach (var tarih2 in data.TarihAraligi.Where(s => s.TakvimId > tarih.TakvimId))
//        {
//            _modelCokGrup.AddConstraint(
//                  Expression.Sum(data.EczaneNobetTarihAralik
//                                   .Where(e => ikiliEczaneler.Contains(e.EczaneId)
//                                            && (e.TakvimId == tarih.TakvimId || e.TakvimId == tarih2.TakvimId))
//                                   .Select(m => _x[m])) <= 3,
//                                   $"her eczane bir ayda diğer gruptaki bir eczane ile en fazla bir kez nöbet tutsun, {g}");
//        }
//    }
//}
#endregion

#region bir önceki aya kadar 3 ay içinde birlikte nöbet tutanların engellenmesi
//bir önceki aya kadar 3 ay içinde birlikte nöbet tutanların engellenmesi
//bu kısıta gerek yok "son iki ayda aynı gün nöbet tutanlar çözüm ayında aynı gün nöbet tutmasın" kısıtı bunu zaten sağlıyor.
//foreach (var ciftGrup in data.OncekiAylardanAyniGunNobetTutanEczaneler.Select(s => s.Id).Distinct().ToList())
//{
//    var eczaneler = data.OncekiAylardanAyniGunNobetTutanEczaneler
//        .Where(s => s.Id == ciftGrup).Select(w => w.EczaneId).ToList();

//    foreach (var tarih in data.TarihAraligi)
//    {
//        model.AddConstraint(
//          Expression.Sum(data.EczaneNobetTarihAralik
//                           .Where(e => eczaneler.Contains(e.EczaneId)
//                                    && e.TakvimId == tarih.TakvimId)
//                           .Select(m => _x[m])) <= 1,
//                           $"eczaneler son uc ay ayni grup olmasin, {tarih}");
//    }
//}
#endregion