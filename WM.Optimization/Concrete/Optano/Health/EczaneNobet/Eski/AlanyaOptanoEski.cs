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
    public class AlanyaOptanoEski : IEczaneNobetAlanyaOptimization
    {
        //Karar değişkeni model çalıştıktan sonra değer aldığından burada tanımlandı
        private VariableCollection<EczaneNobetTarihAralik> _x { get; set; }

        private Model Model(AlanyaDataModelEski data)
        {
            var model = new Model() { Name = "Alanya Eczane Nöbet" };

            #region Veriler
            //int BigM = 1000;
            var bayramlar = data.TarihAraligi
                .Where(w => w.NobetGunKuralId > 7)
                .Select(s => s.NobetGunKuralId).Distinct().ToList();

            var eczaneNobetSonuclar = data.EczaneNobetSonuclarSonIkiAy;
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

            #region Amaç Fonksiyonu
            var amac = new Objective(Expression.Sum(
                            data.EczaneNobetTarihAralik.Select(i => _x[i])
                                                   ),
                                                   "Sum of all item-values: ",
                                                    ObjectiveSense.Minimize);
            model.AddObjective(amac);
            #endregion

            #region Kısıtlar

            //a2: sıralı gün varsa bu kısıt devre dışı kalır--o gün bayram olarak sayılmaz
            //int siraliGun;

            foreach (var nobetGrup in data.NobetGruplar)
            {
                #region ön hazırlama

                var gruptakiNobetciSayisi = 0;
                //data.EczaneNobetGruplar
                //    .Where(s => s.NobetGrupId == nobetGrup.Id)
                //    .Select(s => s.EczaneId).Count();

                var r = new Random();
                var eczaneler = data.EczaneKumulatifHedefler.Where(w => w.NobetGrupId == nobetGrup.Id).OrderBy(o => r.NextDouble()).ToList();

                #region Nöbet gün kurallar

                var nobetGunKural = data.NobetUstGrupKisitlar
                                                        .Where(s => s.KisitAdi == "nobetGunKural"
                                                                 && s.NobetUstGrupId == data.NobetUstGrupId)
                                                        .Select(w => w.PasifMi == false).SingleOrDefault();

                var nobetGunKuralDetaylar = new List<NobetGunKuralDetay>();

                if (nobetGunKural)
                {
                    var gunDegerler = data.TarihAraligi.Select(s => s.NobetGunKuralId).Distinct().ToList();

                    var gunKurallar = data.NobetGrupGunKurallar
                                                    .Where(s => s.NobetGrupId == nobetGrup.Id
                                                            && gunDegerler.Contains(s.NobetGunKuralId))
                                                    .Select(s => s.NobetGunKuralId);

                    foreach (var gunKuralId in gunKurallar)
                    {
                        var cozulenAydakiNobetSayisi = data.TarihAraligi.Where(w => w.NobetGunKuralId == gunKuralId).Count();

                        nobetGunKuralDetaylar.Add(GetNobetGunKural(gunKuralId, data, nobetGrup.Id, gruptakiNobetciSayisi, cozulenAydakiNobetSayisi));
                    }
                }
                #endregion 
                #endregion

                #region Eczane bazlı kısıtlar

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

                foreach (var hedef in eczaneler)
                {
                    #region Ay içinde peşpeşe görev yazılmasın

                    var pespeseGorev = data.NobetUstGrupKisitlar
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
                            model.AddConstraint(
                              Expression.Sum(data.EczaneNobetTarihAralik
                                               .Where(e => e.EczaneNobetGrupId == hedef.EczaneNobetGrupId
                                                           && (e.TakvimId >= g.TakvimId && e.TakvimId <= (g.TakvimId + pespeseNobetSayisi))
                                                           && e.NobetGrupId == nobetGrup.Id
                                                           )
                                               .Select(m => _x[m])) <= 1,
                                               $"eczanelere pespese nobet yazilmasin, {hedef}");
                        }
                    }
                    #endregion

                    #region Pazar günleri 2 aydan önce 7 aydan sonra görev yazılmasın

                    var pazarPespeseGorev = data.NobetUstGrupKisitlar
                                            .Where(s => s.KisitAdi == "pazarPespeseGorev"
                                                     && s.NobetUstGrupId == data.NobetUstGrupId)
                                            .Select(w => w.PasifMi == false).SingleOrDefault();

                    if (pazarPespeseGorev)
                    {
                        var enSonPazarTuttuguNobetAyi = data.EczaneNobetSonuclar
                            .Where(w => w.EczaneId == hedef.EczaneId
                                     && w.NobetGunKuralId == 1)
                            .Select(s => s.Tarih.Month).OrderByDescending(o => o).FirstOrDefault();

                        var birSonrakiPazarNobetiYazilabilecekAy = 3; //3 

                        var yazilabilecekIlkTarih = enSonPazarTuttuguNobetAyi + birSonrakiPazarNobetiYazilabilecekAy;
                        var yazilabilecekSonTarih = yazilabilecekIlkTarih + 3;//4,3

                        if (data.CalismaSayisi == 5) yazilabilecekSonTarih = yazilabilecekSonTarih++;

                        if (enSonPazarTuttuguNobetAyi > 0)
                        {
                            foreach (var g in data.TarihAraligi.Where(w => !(w.Ay >= yazilabilecekIlkTarih && w.Ay <= yazilabilecekSonTarih)
                                                                            && w.NobetGunKuralId == 1))
                            {
                                model.AddConstraint(
                                  Expression.Sum(data.EczaneNobetTarihAralik
                                                   .Where(e => e.EczaneNobetGrupId == hedef.EczaneNobetGrupId
                                                               && e.TakvimId == g.TakvimId
                                                               && e.NobetGrupId == nobetGrup.Id
                                                               )
                                                   .Select(m => _x[m])) == 0,
                                                   $"eczanelere_farkli_aylarda_pespese_nobet_yazilmasin, {hedef}");
                            }
                        }
                    }
                    #endregion

                    #region Farklı aylar peşpeşe görev yazılmasın (3/5)

                    var farkliAyPespeseGorev = data.NobetUstGrupKisitlar
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
                                                   .Where(e => e.EczaneNobetGrupId == hedef.EczaneNobetGrupId
                                                               && e.TakvimId == g.TakvimId
                                                               && e.NobetGrupId == nobetGrup.Id
                                                               )
                                                   .Select(m => _x[m])) == 0,
                                                   $"eczanelere_farkli_aylarda_pespese_nobet_yazilmasin, {hedef}");
                            }
                        }
                    }
                    #endregion

                    #region Toplam max hedefler

                    var toplamMaxHedef = data.NobetUstGrupKisitlar
                                                        .Where(s => s.KisitAdi == "toplamMaxHedef"
                                                                 && s.NobetUstGrupId == data.NobetUstGrupId)
                                                        .Select(w => w.PasifMi == false).SingleOrDefault();
                    if (toplamMaxHedef)
                    {
                        var maxToplam = hedef.Toplam;

                        if (maxToplam > 2) maxToplam = 2;

                        if (data.CalismaSayisi == 2 || data.CalismaSayisi == 4)
                        {
                            maxToplam = maxToplam <= 2 ? maxToplam + 1 : 2;
                            //maxToplam + 2;
                        }

                        model.AddConstraint(
                         Expression.Sum(data.EczaneNobetTarihAralik
                                          .Where(e => e.EczaneNobetGrupId == hedef.EczaneNobetGrupId
                                                   && e.NobetGorevTipId == hedef.NobetGorevTipId)
                                          .Select(m => _x[m])) <= maxToplam,
                                          $"her eczaneye bir ayda en çok nobet grubunun hedefi kadar nobet yazilmali, {hedef}");
                    }
                    #endregion

                    #region Toplam min hedefler

                    var toplamMinHedef = data.NobetUstGrupKisitlar
                                                        .Where(s => s.KisitAdi == "toplamMinHedef"
                                                                 && s.NobetUstGrupId == data.NobetUstGrupId)
                                                        .Select(w => w.PasifMi == false).SingleOrDefault();
                    if (toplamMinHedef)
                    {
                        var minToplam = hedef.Toplam - 1;

                        minToplam = minToplam > 1 ? 1 : 0;

                        model.AddConstraint(
                          Expression.Sum(data.EczaneNobetTarihAralik
                                            .Where(e => e.EczaneNobetGrupId == hedef.EczaneNobetGrupId
                                                     && e.NobetGorevTipId == hedef.NobetGorevTipId)
                                            .Select(m => _x[m])) >= minToplam,
                                            $"her eczaneye bir ayda en az nobet grubunun hedefinden bir eksik nobet yazilmali, {hedef}");
                    }
                    #endregion

                    #region Her ay en az 1 görev

                    var herAyEnaz1Gorev = data.NobetUstGrupKisitlar
                                                        .Where(s => s.KisitAdi == "herAyEnaz1Gorev"
                                                                 && s.NobetUstGrupId == data.NobetUstGrupId)
                                                        .Select(w => w.PasifMi == false).SingleOrDefault();
                    if (herAyEnaz1Gorev)
                    {//her ay en az 1 tane yazılsın
                        model.AddConstraint(
                              Expression.Sum(data.EczaneNobetTarihAralik
                                                .Where(e => e.EczaneNobetGrupId == hedef.EczaneNobetGrupId
                                                         && e.NobetGorevTipId == hedef.NobetGorevTipId)
                                                .Select(m => _x[m])) >= 1,
                                                $"her eczaneye bir ayda en az nobet grubunun hedefinden bir eksik nobet yazilmali, {hedef}");
                    }
                    #endregion

                    #region Her ay en fazla 1 Pazar

                    var herAyEnFazla1Pazar = data.NobetUstGrupKisitlar
                                                        .Where(s => s.KisitAdi == "herAyEnFazla1Pazar"
                                                                 && s.NobetUstGrupId == data.NobetUstGrupId)
                                                        .Select(w => w.PasifMi == false).SingleOrDefault();
                    if (herAyEnFazla1Pazar)
                    {
                        model.AddConstraint(
                              Expression.Sum(data.EczaneNobetTarihAralik
                                                .Where(e => e.EczaneNobetGrupId == hedef.EczaneNobetGrupId
                                                         && e.NobetGorevTipId == hedef.NobetGorevTipId
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

                            var gunKuralNobetSayisi = data.EczaneNobetSonuclar
                                .Where(w => w.EczaneNobetGrupId == hedef.EczaneNobetGrupId
                                         && w.NobetGunKuralId == gunKuralId)
                                .Select(s => s.TakvimId).Count();

                            var Std = item.KuralSTD;

                            if (!item.FazlaNobetTutacakEczaneler.Contains(hedef.EczaneNobetGrupId) && item.FazlaNobetTutacakEczaneler.Count > 0)
                            //&& item.EnCokNobetTutulanNobetSayisi < item.OrtalamaNobetSayisi)
                            {
                                Std = item.KuralSTD - 1;
                            }

                            //eczanelerin nöbet tutacakları günler
                            var tarihler = data.TarihAraligi.Where(w => w.NobetGunKuralId == gunKuralId).ToList();

                            if (gunKuralNobetSayisi <= Std)
                            {
                                model.AddConstraint(
                                   Expression.Sum(data.EczaneNobetTarihAralik
                                                    .Where(e => e.EczaneNobetGrupId == hedef.EczaneNobetGrupId
                                                                && tarihler.Select(s => s.TakvimId).Contains(e.TakvimId)
                                                                && e.NobetGorevTipId == hedef.NobetGorevTipId
                                                                )
                                                    .Select(m => _x[m])) + gunKuralNobetSayisi <= Std,
                                                    $"eczanelere_gun_kurallarini_periyodik_yaz, {item}-{hedef}");
                            }
                            else
                            {
                                model.AddConstraint(
                                   Expression.Sum(data.EczaneNobetTarihAralik
                                                    .Where(e => e.EczaneNobetGrupId == hedef.EczaneNobetGrupId
                                                                && tarihler.Select(s => s.TakvimId).Contains(e.TakvimId)
                                                                && e.NobetGorevTipId == hedef.NobetGorevTipId
                                                                )
                                                    .Select(m => _x[m])) == 0,
                                                    $"eczanelere_gun_kurallarini_periyodik_yaz, {item}-{hedef}");
                            }
                        }
                    }
                    #endregion

                    #region Toplam Cuma Max Hedef

                    var toplamCumaMaxHedef = data.NobetUstGrupKisitlar
                                                .Where(s => s.KisitAdi == "toplamCumaMaxHedef"
                                                         && s.NobetUstGrupId == data.NobetUstGrupId)
                                                .Select(w => w.PasifMi == false).SingleOrDefault();

                    if (toplamCumaMaxHedef)
                    {
                        var Std = data.NobetUstGrupKisitlar
                                                        .Where(s => s.KisitAdi == "toplamCumaMaxHedef"
                                                                 && s.NobetUstGrupId == data.NobetUstGrupId)
                                                        .Select(w => w.SagTarafDegeri).SingleOrDefault();

                        if (data.CalismaSayisi == 8) Std = Std++;

                        var toplamCumaSayisi = data.EczaneNobetSonuclar
                            .Where(w => w.EczaneId == hedef.EczaneId
                                     && w.NobetGunKuralId == 6)
                            .Select(s => s.TakvimId).Count();

                        foreach (var g in data.TarihAraligi.Where(w => w.NobetGunKuralId == 6))
                        {
                            model.AddConstraint(
                              Expression.Sum(data.EczaneNobetTarihAralik
                                               .Where(e => e.EczaneNobetGrupId == hedef.EczaneNobetGrupId
                                                           && e.TakvimId == g.TakvimId
                                                           && e.NobetGrupId == nobetGrup.Id
                                                           )
                                               .Select(m => _x[m])) + toplamCumaSayisi <= Std,
                                               $"eczanelere_cuma_gunu_encok_yilda_3_gorev_yaz, {hedef}");
                        }
                    }
                    #endregion

                    #region Toplam Cumartesi Max Hedef

                    var toplamCumartesiMaxHedef = data.NobetUstGrupKisitlar
                                                    .Where(s => s.KisitAdi == "toplamCumartesiMaxHedef"
                                                             && s.NobetUstGrupId == data.NobetUstGrupId)
                                                    .Select(w => w.PasifMi == false).SingleOrDefault();

                    if (toplamCumartesiMaxHedef)
                    {
                        var Std = data.NobetUstGrupKisitlar
                                                            .Where(s => s.KisitAdi == "toplamCumartesiMaxHedef"
                                                                     && s.NobetUstGrupId == data.NobetUstGrupId)
                                                            .Select(w => w.SagTarafDegeri).SingleOrDefault();

                        if (data.CalismaSayisi == 8) Std = Std++;

                        var toplamCumaSayisi = data.EczaneNobetSonuclar
                            .Where(w => w.EczaneId == hedef.EczaneId
                                     && w.NobetGunKuralId == 7)
                            .Select(s => s.TakvimId).Count();

                        foreach (var g in data.TarihAraligi.Where(w => w.NobetGunKuralId == 7))
                        {
                            model.AddConstraint(
                              Expression.Sum(data.EczaneNobetTarihAralik
                                               .Where(e => e.EczaneNobetGrupId == hedef.EczaneNobetGrupId
                                                           && e.TakvimId == g.TakvimId
                                                           && e.NobetGrupId == nobetGrup.Id
                                                           )
                                               .Select(m => _x[m])) + toplamCumaSayisi <= Std,
                                               $"eczanelere_cumartesi_gunu_encok_yilda_3_gorev_yaz, {hedef}");
                        }
                    }
                    #endregion


                    #region Bayram Toplam Hedefleri

                    if (bayramlar.Count() > 0)
                    {
                        var temp = hedef.ToplamBayram - 1;
                        if (temp < 0) temp = 0;

                        var bayramToplamMaxHedef = data.NobetUstGrupKisitlar
                                                    .Where(s => s.KisitAdi == "bayramToplamMaxHedef"
                                                             && s.NobetUstGrupId == data.NobetUstGrupId)
                                                    .Select(w => w.PasifMi == false).SingleOrDefault();
                        if (bayramToplamMaxHedef)
                        {
                            model.AddConstraint(
                              Expression.Sum(data.EczaneNobetTarihAralik
                                            .Where(e => e.EczaneNobetGrupId == hedef.EczaneNobetGrupId
                                                    //&& e.HaftaninGunu != siraliGun //a2 
                                                    && e.NobetGunKuralId > 7)
                                            .Select(m => _x[m])) <= hedef.ToplamBayram,
                                            $"her eczaneye bir ayda nobet grubunun hedefi kadar toplam bayram nobeti yazilmali, {hedef}");
                        }

                        var bayramToplamMinHedef = data.NobetUstGrupKisitlar
                                                    .Where(s => s.KisitAdi == "bayramToplamMinHedef"
                                                             && s.NobetUstGrupId == data.NobetUstGrupId)
                                                    .Select(w => w.PasifMi == false).SingleOrDefault();
                        if (bayramToplamMinHedef)
                        {
                            model.AddConstraint(
                              Expression.Sum(data.EczaneNobetTarihAralik
                                            .Where(e => e.EczaneNobetGrupId == hedef.EczaneNobetGrupId
                                                    //&& e.HaftaninGunu != siraliGun //a2
                                                    && e.NobetGunKuralId > 7)
                                            .Select(m => _x[m])) >= temp,
                                            $"her eczaneye bir ayda nobet grubunun hedefi kadar toplam bayram nobeti yazilmali, {hedef}");
                        }
                    }
                    #endregion

                    #region Hafta içi toplam max hedefleri

                    var haftaIciToplamMaxHedef = data.NobetUstGrupKisitlar
                                                .Where(s => s.KisitAdi == "haftaIciToplamMaxHedef"
                                                         && s.NobetUstGrupId == data.NobetUstGrupId)
                                                .Select(w => w.PasifMi == false).SingleOrDefault();

                    if (data.CalismaSayisi == 3 || data.CalismaSayisi == 4)
                    {
                        haftaIciToplamMaxHedef = false;
                    }

                    if (haftaIciToplamMaxHedef)
                    {
                        var haftaIciToplam = hedef.ToplamHaftaIci;
                        model.AddConstraint(
                          Expression.Sum(data.EczaneNobetTarihAralik
                                        .Where(e => e.EczaneNobetGrupId == hedef.EczaneNobetGrupId
                                                && (e.NobetGunKuralId >= 2 && e.NobetGunKuralId <= 7))
                                        .Select(m => _x[m])) <= haftaIciToplam,
                                        $"her eczaneye bir ayda nobet grubunun hedefi kadar toplam hafta içi nobeti yazilmali, {hedef}");
                    }
                    #endregion

                    #region Hafta içi toplam min hedefleri

                    var haftaIciToplamMinHedef = data.NobetUstGrupKisitlar
                                                         .Where(s => s.KisitAdi == "haftaIciToplamMinHedef"
                                                                  && s.NobetUstGrupId == data.NobetUstGrupId)
                                                         .Select(w => w.PasifMi == false).SingleOrDefault();

                    if (data.CalismaSayisi == 2 || data.CalismaSayisi == 4)
                    {
                        haftaIciToplamMinHedef = false;
                    }

                    if (haftaIciToplamMinHedef)
                    {
                        var haftaIciToplam = hedef.ToplamHaftaIci - 1;

                        model.AddConstraint(
                          Expression.Sum(data.EczaneNobetTarihAralik
                                        .Where(e => e.EczaneNobetGrupId == hedef.EczaneNobetGrupId
                                                && (e.NobetGunKuralId >= 2 && e.NobetGunKuralId <= 7))
                                        .Select(m => _x[m])) >= haftaIciToplam,
                                        $"her eczaneye bir ayda nobet grubunun hedefi kadar toplam hafta içi nobeti yazilmali, {hedef}");
                    }
                    #endregion                    
                }
                #endregion

                #region Eczane grup kısıtı

                //Birbiri ile coğrafi yakınlık ya da eş durumu gibi nedenlerle farklı gruplardaki eczanelere sürekli aynı gün nöbet yazılmasın
                var eczaneGrup = data.NobetUstGrupKisitlar
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

                #region Yeni 4-6

                bool dene = true;

                if (data.Ay > 5 && dene)
                {
                    var nobetGunKuralId = 1;
                    var ustSinir = data.Ay - 5;

                    for (int ay = 1; ay <= ustSinir; ay++)
                    {
                        //ilk ayda nöbet tutanlara en erken 4 ay sonra 5.ayda pazar nöbeti yazılacak.
                        //yazılamamış olanlara 6. ayda o da olmazsa 7.ayda pazar nöbeti yazılması sağlanacak. 
                        //ilk pazar günü nöbet tutanlar..(1.aydan başlıyor)
                        var enSonPazarTutanlar = data.EczaneNobetSonuclar
                                .Where(w => w.NobetGunKuralId == nobetGunKuralId && w.Ay == ay
                                         && w.NobetGrupId == nobetGrup.Id)
                                .Select(s => s.EczaneNobetGrupId);

                        var ilkAy = ay + 4;
                        var sonAy = ay + 5;
                        var fark = data.Ay - sonAy + 1;

                        var enSonPazarTutanlarKontrol = data.EczaneNobetSonuclar
                            .Where(w => w.NobetGunKuralId == nobetGunKuralId && (w.Ay >= ilkAy && w.Ay <= sonAy)
                                     && w.NobetGrupId == nobetGrup.Id)
                            .Select(s => s.EczaneNobetGrupId);

                        var eksikKalan = enSonPazarTutanlar.Where(w => !enSonPazarTutanlarKontrol.Contains(w));

                        //4-6 ay içinde ve hala pazar eksik olan eczane varsa..
                        if (fark < 3 && eksikKalan.Count() > 0)
                        {
                            //4.ayı kendine bıraktık.
                            //5.ayda 1 tanesi
                            //6.ayda kaldıysa hepsi
                            if (data.Ay - 1 == ilkAy)
                            {
                                if (eksikKalan.Count() > 0)
                                {
                                    model.AddConstraint(
                                      Expression.Sum(data.EczaneNobetTarihAralik
                                                       .Where(e => eksikKalan.Take(1).Contains(e.EczaneNobetGrupId)
                                                                   && e.NobetGunKuralId == nobetGunKuralId
                                                             )
                                                       .Select(m => _x[m])) == 1,
                                                       $"istege nobet yaz, {45}");
                                }
                            }
                            else
                            {
                                if (eksikKalan.Count() > 0)
                                {
                                    model.AddConstraint(
                                      Expression.Sum(data.EczaneNobetTarihAralik
                                                       .Where(e => eksikKalan.Contains(e.EczaneNobetGrupId)
                                                                   && e.NobetGunKuralId == nobetGunKuralId
                                                             )
                                                       .Select(m => _x[m])) == 1,
                                                       $"istege nobet yaz, {45}");
                                }
                            }
                        }
                    }
                }

                #endregion

                #region İsteğe Görev Yazılsın

                var istek = data.NobetUstGrupKisitlar
                                .Where(s => s.KisitAdi == "istek"
                                            && s.NobetUstGrupId == data.NobetUstGrupId)
                                .Select(w => w.PasifMi == false).SingleOrDefault();

                if (istek)
                {
                    foreach (var f in data.EczaneNobetIstekListe.Where(x => x.NobetGrupId == nobetGrup.Id))
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

                var mazeret = data.NobetUstGrupKisitlar
                                .Where(s => s.KisitAdi == "mazeret"
                                            && s.NobetUstGrupId == data.NobetUstGrupId)
                                .Select(w => w.PasifMi == false).SingleOrDefault();
                if (mazeret)
                {
                    foreach (var f in data.EczaneNobetMazeretListe.Where(x => x.NobetGrupId == nobetGrup.Id))
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

                var bayram = data.NobetUstGrupKisitlar
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
                            //foreach (var g in data.TarihAraligi.Where(w => w.NobetGunKuralId > 7))
                            //{
                            //    model.AddConstraint(
                            //      Expression.Sum(data.EczaneNobetTarihAralik
                            //                       .Where(e => e.EczaneNobetGrupId == f.EczaneNobetGrupId
                            //                                   && e.Gun == g.Gun
                            //                             )
                            //                       .Select(m => _x[m])) <= 1,
                            //                       $"bayram nobeti sinirla, {f}");
                            //}
                        }
                    }
                }
                #endregion
            }

            #region Yılda en fazla aynı gün 3'ten fazla nöbet tutulmasın

            #region yil içinde en fazla 3 kez aynı gün nöbet tutulsun

            var yildaEncokUcKezGrup = data.NobetUstGrupKisitlar
                                        .Where(s => s.KisitAdi == "yildaEncokUcKezGrup"
                                                    && s.NobetUstGrupId == data.NobetUstGrupId)
                                        .Select(w => w.PasifMi == false).SingleOrDefault();

            //if (data.CalismaSayisi == 5) sonIkiAydakiGrup = false;

            if (yildaEncokUcKezGrup)
            {
                foreach (var g in data.YilIcindeAyniGunNobetTutanEczaneler.Select(s => s.Id).Distinct().ToList())
                {
                    var ikiliEczaneler = data.YilIcindeAyniGunNobetTutanEczaneler
                                                    .Where(w => w.Id == g
                                                    && data.EczaneNobetGruplar.Select(s => s.EczaneId).Contains(w.EczaneId))
                                                    .Select(s => s.EczaneId).ToList();

                    var ikiliEczaneler2 = data.YilIcindeAyniGunNobetTutanEczaneler
                                                    .Where(w => w.Id == g
                                                    && !data.EczaneNobetGruplar.Select(s => s.EczaneId).Contains(w.EczaneId))
                                                    .Select(s => s.EczaneId).ToList();

                    var nobetTutulanGunler = data.EczaneNobetSonuclarOncekiAylar
                        .Where(w => ikiliEczaneler2.Contains(w.EczaneId)
                                 && w.Tarih.Month == data.Ay)
                        .Select(s => s.TakvimId).ToList();

                    if (nobetTutulanGunler.Count > 0 && ikiliEczaneler.Count > 0)
                    {
                        foreach (var tarih in data.TarihAraligi.Where(w => nobetTutulanGunler.Contains(w.TakvimId)))
                        {
                            model.AddConstraint(
                                  Expression.Sum(data.EczaneNobetTarihAralik
                                                   .Where(e => ikiliEczaneler.Contains(e.EczaneId)
                                                            && e.TakvimId == tarih.TakvimId)
                                                   .Select(m => _x[m])) == 0,
                                                   $"ay icinde diger gruptaki eczaneler ile en fazla bir kez nobet tutulsun, {g}");
                        }
                    }

                }

            }
            #endregion

            #region son iki ayda aynı gün nöbet tutanlar çözüm ayında aynı gün nöbet tutmasın

            var sonIkiAydakiGrup = data.NobetUstGrupKisitlar
                                        .Where(s => s.KisitAdi == "sonIkiAydakiGrup"
                                                    && s.NobetUstGrupId == data.NobetUstGrupId)
                                        .Select(w => w.PasifMi == false).SingleOrDefault();

            //if (data.CalismaSayisi == 5) sonIkiAydakiGrup = false;

            if (sonIkiAydakiGrup)
            {
                foreach (var g in data.SonIkiAyAyniGunNobetTutanEczaneler.Select(s => s.Id).Distinct().ToList())
                {
                    //nöbet yazılacak eczaneler
                    var ikiliEczaneler = data.SonIkiAyAyniGunNobetTutanEczaneler
                                                    .Where(w => w.Id == g
                                                    && data.EczaneNobetGruplar.Select(s => s.EczaneId).Contains(w.EczaneId))
                                                    .Select(s => s.EczaneId).ToList();

                    //nöbet yazılmış eczaneler
                    var ikiliEczaneler2 = data.SonIkiAyAyniGunNobetTutanEczaneler
                                                    .Where(w => w.Id == g
                                                    && !data.EczaneNobetGruplar.Select(s => s.EczaneId).Contains(w.EczaneId))
                                                    .Select(s => s.EczaneId).ToList();

                    //nöbet yazılmış eczanelerin nöbet günleri
                    var nobetTutulanGunler = eczaneNobetSonuclar
                        .Where(w => ikiliEczaneler2.Contains(w.EczaneId)
                                 && w.Tarih.Month == data.Ay)
                        .Select(s => s.TakvimId).ToList();

                    if (nobetTutulanGunler.Count > 0 && ikiliEczaneler.Count > 0)
                    {
                        model.AddConstraint(
                              Expression.Sum(data.EczaneNobetTarihAralik
                                               .Where(e => ikiliEczaneler.Contains(e.EczaneId)
                                                        && nobetTutulanGunler.Contains(e.TakvimId))
                                               .Select(m => _x[m])) == 0,
                                               $"ay icinde diger gruptaki eczaneler ile en fazla bir kez nobet tutulsun, {g}");
                    }


                    //if (nobetTutulanGunler.Count > 0 && ikiliEczaneler.Count > 0)
                    //{
                    //    foreach (var tarih in data.TarihAraligi.Where(w => nobetTutulanGunler.Contains(w.TakvimId)))
                    //    {
                    //        model.AddConstraint(
                    //              Expression.Sum(data.EczaneNobetTarihAralik
                    //                               .Where(e => ikiliEczaneler.Contains(e.EczaneId)
                    //                                        && e.TakvimId == tarih.TakvimId)
                    //                               .Select(m => _x[m])) == 0,
                    //                               $"ay icinde diger gruptaki eczaneler ile en fazla bir kez nobet tutulsun, {g}");
                    //    }
                    //}

                }

            }
            #endregion

            #region ay içinde en fazla bir kez aynı gün nöbet tutulsun

            //ay içinde iki kez nöbet tutan eczane çiftleri oluştuğunda bu çiftler kısıt olacak şekilde yeniden çözüm yapılıyor.  (recursive)
            //Yeni modelde bu çiftlerin herhangi iki gündeki toplamları 3'ten küçük olma kısıtı eklenince sadece 1 kez çift olmaları sağlanıyor.
            var ayIcindeAyniGunNobet = data.NobetUstGrupKisitlar
                            .Where(s => s.KisitAdi == "ayIcindeAyniGunNobet"
                                        && s.NobetUstGrupId == data.NobetUstGrupId)
                            .Select(w => w.PasifMi == false).SingleOrDefault();

            if (data.CalismaSayisi == 5)
            {
                ayIcindeAyniGunNobet = true;
            }

            if (ayIcindeAyniGunNobet)
            {
                foreach (var g in data.AyIcindeAyniGunNobetTutanEczaneler.Select(s => s.Id).Distinct().ToList())
                {
                    var ikiliEczaneler = data.AyIcindeAyniGunNobetTutanEczaneler
                                                    .Where(w => w.Id == g)
                                                    .Select(s => s.EczaneId).ToList();

                    var nobetTutulanGunler = eczaneNobetSonuclar
                        .Where(w => ikiliEczaneler.Contains(w.EczaneId)
                                 && w.Tarih.Month == data.Ay)
                        .Select(s => s.TakvimId).ToList();

                    foreach (var tarih in data.TarihAraligi.Where(w => nobetTutulanGunler.Contains(w.TakvimId)))
                    {
                        model.AddConstraint(
                              Expression.Sum(data.EczaneNobetTarihAralik
                                               .Where(e => ikiliEczaneler.Contains(e.EczaneId)
                                                        && e.TakvimId == tarih.TakvimId)
                                               .Select(m => _x[m])) == 0,
                                               $"ay icinde diger gruptaki eczaneler ile en fazla bir kez nobet tutulsun, {g}");

                    }
                }
            }
            #endregion

            #endregion

            #endregion

            return model;
        }

        public EczaneNobetSonucModel Solve(AlanyaDataModelEski data)
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
                    //var gap = solution.Gap;
                    //var numberOfExploredNodes = solution.NumberOfExploredNodes;

                    //var conflicts = solution.ConflictingSet.ToString();
                    //var confilicts = new ConflictingSet();
                    //confilicts = solution.ConflictingSet;
                    //ConstraintsUB = new IEnumerable<Constraint>();
                    //ConstraintsUB = confilicts.ConstraintsUB;

                    if (modelStatus != ModelStatus.Feasible)
                    {
                        data.CalismaSayisi++;
                        throw new Exception("Uygun çözüm bulunamadı! ");
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
                            mesaj += $"1: Kendini tekrar çağırdı!";
                            break;
                        case 2:
                            mesaj += $"2: Toplam max hedef gevşetildi!";
                            break;
                        case 3:
                            mesaj += $"3: Toplam min hedef gevşetildi!";
                            break;
                        case 4:
                            mesaj += $"4: Toplam max ve min hedef gevşetildi!";
                            break;
                        case 5:
                            mesaj += $"5: Ayda en fazla 1 gorev kaldırıldı!";
                            break;
                        case 6:
                            mesaj += $"6: Farklı ay peşpeşe görev gevşetildi!";
                            break;
                        case 7:
                            mesaj += $"7: Ayda en fazla 1 gorev kaldırıldı ve Farklı Ay Peşpeşe Görev gevşetildi!";
                            break;
                        case 8:
                            mesaj += $"8: Cuma ve cumartesi en fazla 3 olmadı 4 olarak gevşetildi!";
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
                            mesaj += $"{data.NobetGruplar.Select(s => s.Adi).FirstOrDefault()} {data.Yil} {data.Ay}.ayda bazı kısıtlar ({mesaj}) gevşetilerek {data.CalismaSayisi} kez çözüm denendi.{iterasyonMesaj} Bu grup için çözüm uygun çözüm alanı bulunmamaktadır. Lütfen kısıtları kontrol edip tekrar deneyiniz..";
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

        private NobetGunKuralDetay GetNobetGunKural(int gunKuralId, AlanyaDataModelEski data, int nobetGrupId, int gruptakiNobetciSayisi, int cozulenAydakiNobetSayisi)
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
                                 && w.EnSonNobetTuttuguAy < (data.Ay - 3))
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
                                 && w.EnSonNobetTuttuguAy < (data.Ay - 3))
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
//Birbiri ile coğrafi yakınlık ya da eş durumu gibi nedenlerle farklı gruplardaki eczanelere sürekli aynı gün nöbet yazılmasın
//var eczaneGrup = data.NobetUstGrupKisitlar
//                .Where(s => s.KisitAdi == "eczaneGrup"
//                            && s.NobetUstGrupId == data.NobetUstGrupId)
//                .Select(w => w.PasifMi == false).SingleOrDefault();
//            if (eczaneGrup)
//            {
//                foreach (var g in data.EczaneGrupTanimlar)
//                {
//                    var eczaneler = data.EczaneGruplar
//                                            .Where(x => x.EczaneGrupTanimId == g.Id)
//                                            .Select(s => s.EczaneId).Distinct().ToList();

//var nobetGruplar = data.EczaneNobetGruplar
//    .Where(s => eczaneler.Contains(s.EczaneId))
//    .Select(w => w.NobetGrupId).Distinct();

////a1: sıralı gün varsa bu kısıt devre dışı kalır--o gün bayram olarak sayılmaz
//var siraliGunKural = data.NobetGrupKurallar
//                    .Where(s => nobetGruplar.Contains(s.NobetGrupId)
//                             && s.NobetKuralId == 4) //s.NobetKuralId == 4:sıralı gün kuralı
//                    .Select(s => s.Deger).FirstOrDefault();

//                    if (siraliGunKural != null)
//                    {
//                        siraliGun = (int) siraliGunKural;
//                    }
//                    else
//                    {
//                        siraliGun = 0;
//                    }

//                    foreach (var tarih in data.TarihAraligi.Take(data.TarihAraligi.Count() - g.ArdisikNobetSayisi))
//                    {
//                        var grupTanimlar = Expression.Sum(data.EczaneNobetTarihAralik
//                                        .Where(e => eczaneler.Contains(e.EczaneId)
//                                                    && (e.GunId >= tarih.GunId && e.GunId <= tarih.GunId + g.ArdisikNobetSayisi)
//                                                //&& e.HaftaninGunu != siraliGun //a1
//                                                )
//                                        .Select(m => _x[m])) <= 1;
//model.AddConstraint(grupTanimlar,
//                                                    $"herbir_eczaneGrupTanimdaki_eczaneler_beraber_nobet_tutmasin, {tarih.GunId}");
//                    }
//                }
//            }
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

//bu iptal

#region yil içinde en fazla 3 kez aynı gün nöbet tutulsun

//var yildaEncokUcKezGrup = data.NobetUstGrupKisitlar
//                            .Where(s => s.KisitAdi == "yildaEncokUcKezGrup"
//                                        && s.NobetUstGrupId == data.NobetUstGrupId)
//                            .Select(w => w.PasifMi == false).SingleOrDefault();
//if (yildaEncokUcKezGrup)
//{
//    foreach (var ciftGrup in data.YilIcindeAyniGunNobetTutanEczaneler.Select(s => s.Id).Distinct().ToList())
//    {
//        var eczaneler = data.YilIcindeAyniGunNobetTutanEczaneler
//            .Where(s => s.Id == ciftGrup).Select(w => w.EczaneId).ToList();

//        //int siraliGun;
//        siraliGun = 1;
//        //siraliGun = (int)data.NobetGrupKurallar
//        //    .Where(s => s.NobetGrupId == nobetGrupId
//        //             && s.NobetKuralId == 4)
//        //    .Select(s => s.Deger).FirstOrDefault();

//        foreach (var tarih in data.TarihAraligi
//            //.Where(s => s.HaftaninGunu != siraliGun)
//            )
//        {
//            model.AddConstraint(
//              Expression.Sum(data.EczaneNobetTarihAralik
//                               .Where(e => eczaneler.Contains(e.EczaneId)
//                                        && e.TakvimId == tarih.TakvimId)
//                               .Select(m => _x[m])) <= 1,
//                               $"eczaneler ay icinde iki kez ayni grup olmasin, {tarih}");
//        }
//    }
//}
#endregion

//bu iptal
#region son iki ayda aynı gün nöbet tutanlar çözüm ayında aynı gün nöbet tutmasın

//var sonIkiAydakiGrup = data.NobetUstGrupKisitlar
//                            .Where(s => s.KisitAdi == "sonIkiAydakiGrup"
//                                        && s.NobetUstGrupId == data.NobetUstGrupId)
//                            .Select(w => w.PasifMi == false).SingleOrDefault();
//if (sonIkiAydakiGrup)
//{
//    foreach (var ciftGrup in data.SonIkiAyAyniGunNobetTutanEczaneler.Select(s => s.Id).Distinct().ToList())
//    {
//        var eczaneler = data.SonIkiAyAyniGunNobetTutanEczaneler
//            .Where(s => s.Id == ciftGrup).Select(w => w.EczaneId).ToList();

//        //var eczaneler2 = data.EczaneNobetGruplar.Where(s => eczaneler.Contains(s.EczaneId)).Select(e=> new { e.NobetGrupId, e.EczaneId }).ToList();

//        //int siraliGun;
//        siraliGun = 1;
//        //siraliGun = (int)data.NobetGrupKurallar
//        //    .Where(s => s.NobetGrupId == nobetGrupId
//        //             && s.NobetKuralId == 4)
//        //    .Select(s => s.Deger).FirstOrDefault();

//        foreach (var tarih in data.TarihAraligi
//            //.Where(s => s.HaftaninGunu != siraliGun)
//            )
//        {
//            model.AddConstraint(
//              Expression.Sum(data.EczaneNobetTarihAralik
//                               .Where(e => eczaneler.Contains(e.EczaneId)
//                                        && e.TakvimId == tarih.TakvimId
//                                        )
//                               .Select(m => _x[m])) <= 1,
//                               $"eczaneler son uc ay ayni grup olmasin, {tarih}");
//        }
//    }
//}
#endregion

#endregion

//bu iptal
#region Toplam Pazar Max Hedef

//var toplamPazarMaxHedef = data.NobetUstGrupKisitlar
//                            .Where(s => s.KisitAdi == "toplamPazarMaxHedef"
//                                     && s.NobetUstGrupId == data.NobetUstGrupId)
//                            .Select(w => w.PasifMi == false).SingleOrDefault();

//if (toplamPazarMaxHedef)
//{
//    var Std = data.NobetUstGrupKisitlar
//                                    .Where(s => s.KisitAdi == "toplamPazarMaxHedef"
//                                             && s.NobetUstGrupId == data.NobetUstGrupId)
//                                    .Select(w => w.SagTarafDegeri).SingleOrDefault();

//    //if (data.CalismaSayisi == 8) Std = Std++;

//    var toplamPazarSayisi = data.EczaneNobetSonuclar
//        .Where(w => w.EczaneId == hedef.EczaneId
//                 && w.GunKuralId == 1)
//        .Select(s => s.TakvimId).Count();

//    foreach (var g in data.TarihAraligi.Where(w => w.GunDegerId == 1))
//    {
//        model.AddConstraint(
//          Expression.Sum(data.EczaneNobetTarihAralik
//                           .Where(e => e.EczaneNobetGrupId == hedef.EczaneNobetGrupId
//                                       && e.TakvimId == g.TakvimId
//                                       && e.NobetGrupId == nobetGrup.Id
//                                       )
//                           .Select(m => _x[m])) + toplamPazarSayisi <= Std,
//                           $"eczanelere_pazar_gunu_encok_yilda_3_gorev_yaz, {hedef}");
//    }
//}
#endregion

/*
                     #region Sıralı Gün (istek-hariç)
                var siraliGunKural = data.NobetGrupKurallar
                                .Where(s => s.NobetGrupId == nobetGrup.Id
                                         && s.NobetKuralId == 4)
                                .Select(s => s.Deger).FirstOrDefault();

                if (siraliGunKural != null)
                {
                    siraliGun = (int)siraliGunKural;
                }
                else
                {
                    siraliGun = 0;
                }
                #endregion

    #region Diğer Günlerin Hedefleri

                    //var gunDegerler = data.TarihAraligi.Select(s => s.GunDegerId).Distinct().ToList();
                    //var gunKurallar = data.NobetGrupGunKurallar
                    //                            .Where(s => //s.NobetGunKuralId != siraliGun
                    //                                      s.NobetGrupId == nobetGrup.Id).Select(s => s.NobetGunKuralId);

                    //foreach (var gunDeger in gunDegerler.Where(s => gunKurallar.Contains(s)))
                    //{
                    //    GetEczaneGunHedef(hedef, out double maxArz, out double minArz, gunDeger);

                    //    #region Diğer günler max hedef

                    //    var digerGunlerMaxHedef = data.NobetUstGrupKisitlar
                    //                                               .Where(s => s.KisitAdi == "digerGunlerMaxHedef"
                    //                                                        && s.NobetUstGrupId == data.NobetUstGrupId)
                    //                                               .Select(w => w.PasifMi == false).SingleOrDefault();
                    //    if (digerGunlerMaxHedef)
                    //    {
                    //        if (gunDeger == 1) maxArz = 1;

                    //        model.AddConstraint(
                    //          Expression.Sum(data.EczaneNobetTarihAralik
                    //                           .Where(e => e.EczaneNobetGrupId == hedef.EczaneNobetGrupId
                    //                                       && e.GunDegerId == gunDeger
                    //                                       && e.NobetGrupId == nobetGrup.Id)
                    //                           .Select(m => _x[m])) <= maxArz,
                    //                           $"her eczaneye bir ayda nobet grubunun {gunDeger} hedefi kadar nobet yazilmali, {hedef}");
                    //    }
                    //    #endregion

                    //    #region Diğer günler min hedef

                    //    var digerGunlerMinHedef = data.NobetUstGrupKisitlar
                    //                                                .Where(s => s.KisitAdi == "digerGunlerMinHedef"
                    //                                                         && s.NobetUstGrupId == data.NobetUstGrupId)
                    //                                                .Select(w => w.PasifMi == false).SingleOrDefault();
                    //    if (digerGunlerMinHedef)
                    //    {
                    //        //if (gunDeger == 1)
                    //        //{
                    //        //    minArz = 1;

                    //        //    var enSonPazarNobetTarihi = data.EczaneNobetSonuclar
                    //        //            .Where(w => w.EczaneId == hedef.EczaneId
                    //        //                     && w.GunKuralId == 1)
                    //        //            .Select(s => s.Tarih.Month).OrderByDescending(o => o).FirstOrDefault();

                    //        //    foreach (var g in data.TarihAraligi.Where(w => (w.Ay >= enSonPazarNobetTarihi + 4 && w.Ay <= enSonPazarNobetTarihi + 6) && w.GunDegerId == gunDeger))
                    //        //    {
                    //        //        model.AddConstraint(
                    //        //          Expression.Sum(data.EczaneNobetTarihAralik
                    //        //                           .Where(e => e.EczaneNobetGrupId == hedef.EczaneNobetGrupId
                    //        //                                       && e.TakvimId == g.TakvimId
                    //        //                                       && e.NobetGrupId == nobetGrup.Id
                    //        //                                       )
                    //        //                           .Select(m => _x[m])) >= minArz,
                    //        //                           $"eczanelere_farkli_aylarda_pespese_nobet_yazilmasin, {hedef}");
                    //        //    }
                    //        //}

                    //        model.AddConstraint(
                    //            Expression.Sum(data.EczaneNobetTarihAralik
                    //                            .Where(e => e.EczaneNobetGrupId == hedef.EczaneNobetGrupId
                    //                                        && e.GunDegerId == gunDeger
                    //                                        && e.NobetGrupId == nobetGrup.Id
                    //                                        //&& e.EczaneNobetGrupId != pazarTutanEczane
                    //                                        )
                    //                            .Select(m => _x[m])) >= minArz,
                    //                            $"her eczaneye bir ayda nobet grubunun {gunDeger} hedefi kadar nobet yazilmali, {hedef}");
                    //    }
                    //    #endregion
                    //}
                    #endregion
 */
