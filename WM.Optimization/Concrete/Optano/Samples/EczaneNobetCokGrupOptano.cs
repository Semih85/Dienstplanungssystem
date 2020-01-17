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

namespace WM.Optimization.Concrete.Optano.Samples
{
    public class EczaneNobetCokGrupOptano : IEczaneNobetCokGrupOptimization
    {
        private IEczaneNobetCokGrupAltOptimization _eczaneNobetCokGrupAltOptimization;

        public EczaneNobetCokGrupOptano(IEczaneNobetCokGrupAltOptimization eczaneNobetCokGrupAltOptimization)
        {
            _eczaneNobetCokGrupAltOptimization = eczaneNobetCokGrupAltOptimization;
        }

        #region Değişkenler
        //karar değişkeni model çalıştıktan sonra değer aldığından burada tanımlandı
        private VariableCollection<EczaneNobetTarihAralik> _x { get; set; }
        public EczaneNobetSonucModel Results { get; set; }
        #endregion

        private Model Model(EczaneNobetCokGrupDataModel data)
        {
            var model = new Model() { Name = "Eczane Nöbet Çoklu Model" };

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

            #region Amaç Fonksiyonu
            var amac = new Objective(Expression.Sum((data.EczaneNobetTarihAralik
                                           .Select(i => _x[i]))),
                                             "Sum of all item-values: ",
                                             ObjectiveSense.Minimize);
            model.AddObjective(amac);
            #endregion

            #region Kısıtlar

            #region Talep Kısıtları
            //nöbet grubu bazında olacak
            foreach (var nobetGrupGorevTip in data.NobetGrupGorevTipler)
            {
                foreach (var d in data.TarihAraligi)
                {
                    int gunlukNobetciSayisi = (int)data.NobetGrupKurallar
                            //.Where(s => s.NobetGrupId == nobetGrupGorevTip.NobetGrupId
                            //         && s.NobetKuralId == 3)
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

            #region Arz Kısıtları

            #region Peşpeşe Görev Yazılmasın
            var pespeseGorev = data.NobetUstGrupKisitlar
                                .Where(s => s.KisitAdi == "pespeseGorev"
                                         && s.NobetUstGrupId == data.NobetUstGrupId)
                                .Select(w => w.PasifMi == false).SingleOrDefault();
            if (pespeseGorev)
            {
                foreach (var nobetGrup in data.NobetGruplar)
                {
                    int pespeseNobetSayisi = (int)data.NobetGrupKurallar
                            //.Where(s => s.NobetGrupId == nobetGrup.Id
                            //         && s.NobetKuralId == 1)
                            .Select(s => s.Deger).SingleOrDefault();

                    foreach (var f in data.EczaneKumulatifHedefler.Where(w => w.NobetGrupId == nobetGrup.Id))
                    {
                        foreach (var g in data.TarihAraligi.Take(data.TarihAraligi.Count() - pespeseNobetSayisi))
                        {
                            //model.AddConstraint(
                            //  Expression.Sum(data.EczaneNobetTarihAralik
                            //                   .Where(e => e.EczaneNobetGrupId == f.EczaneNobetGrupId
                            //                               && (e.Gun >= g.Gun && e.Gun <= g.Gun + pespeseNobetSayisi)
                            //                               && e.NobetGrupId == nobetGrup.Id
                            //                               )
                            //                   .Select(m => _x[m])) <= 1,
                            //                   $"eczanelere pespese nobet yazilmasin, {f}");
                        }
                    }
                }
            }
            #endregion

            //a2: sıralı gün varsa bu kısıt devre dışı kalır--o gün bayram olarak sayılmaz
            int siraliGun;

            #region Her eczaneye yazılması gereken hedefler
            foreach (var nobetGrup in data.NobetGruplar)
            {
                foreach (var hedef in data.EczaneKumulatifHedefler.Where(w => w.NobetGrupId == nobetGrup.Id))
                {
                    #region Toplam Hedefler
                    var toplamMaxHedef = data.NobetUstGrupKisitlar
                                            .Where(s => s.KisitAdi == "toplamMaxHedef"
                                                     && s.NobetUstGrupId == data.NobetUstGrupId)
                                            .Select(w => w.PasifMi == false).SingleOrDefault();
                    if (toplamMaxHedef)
                    {
                        model.AddConstraint(
                         Expression.Sum(data.EczaneNobetTarihAralik
                                          .Where(e => e.EczaneNobetGrupId == hedef.EczaneNobetGrupId
                                                   && e.NobetGorevTipId == hedef.NobetGorevTipId)
                                          .Select(m => _x[m])) <= hedef.Toplam,
                                          $"her eczaneye bir ayda en çok nobet grubunun hedefi kadar nobet yazilmali, {hedef}");
                    }

                    var toplamMinHedef = data.NobetUstGrupKisitlar
                                            .Where(s => s.KisitAdi == "toplamMinHedef"
                                                     && s.NobetUstGrupId == data.NobetUstGrupId)
                                            .Select(w => w.PasifMi == false).SingleOrDefault();
                    if (toplamMinHedef)
                    {
                        model.AddConstraint(
                          Expression.Sum(data.EczaneNobetTarihAralik
                                            .Where(e => e.EczaneNobetGrupId == hedef.EczaneNobetGrupId
                                                     && e.NobetGorevTipId == hedef.NobetGorevTipId)
                                            .Select(m => _x[m])) >= hedef.Toplam - 1,
                                            $"her eczaneye bir ayda en az nobet grubunun hedefinden bir eksik nobet yazilmali, {hedef}");
                    }

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

                    #region Sıralı Gün (istek-hariç)
                    var siraliGunKural = data.NobetGrupKurallar
                                    //.Where(s => s.NobetGrupId == nobetGrup.Id
                                    //         && s.NobetKuralId == 4)
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

                    var gunDegerler = data.TarihAraligi.Select(s => s.NobetGunKuralId).Distinct().ToList();
                    var gunKurallar = data.NobetGrupGunKurallar
                                                .Where(s => //s.NobetGunKuralId != siraliGun
                                                          s.NobetGrupId == nobetGrup.Id).Select(s => s.NobetGunKuralId);
                    #region Diğer Günlerin Hedefleri
                    foreach (var gunDeger in gunDegerler.Where(s => gunKurallar.Contains(s)))
                    {
                        GetEczaneGunHedef(hedef, out double maxArz, out double minArz, gunDeger);

                        var digerGunlerMaxHedef = data.NobetUstGrupKisitlar
                                                    .Where(s => s.KisitAdi == "digerGunlerMaxHedef"
                                                             && s.NobetUstGrupId == data.NobetUstGrupId)
                                                    .Select(w => w.PasifMi == false).SingleOrDefault();
                        if (digerGunlerMaxHedef)
                        {
                            model.AddConstraint(
                              Expression.Sum(data.EczaneNobetTarihAralik
                                               .Where(e => e.EczaneNobetGrupId == hedef.EczaneNobetGrupId
                                                           && e.NobetGunKuralId == gunDeger
                                                           && e.NobetGrupId == nobetGrup.Id)
                                               .Select(m => _x[m])) <= maxArz,
                                               $"her eczaneye bir ayda nobet grubunun {gunDeger} hedefi kadar nobet yazilmali, {hedef}");
                        }

                        var digerGunlerMinHedef = data.NobetUstGrupKisitlar
                                                    .Where(s => s.KisitAdi == "digerGunlerMinHedef"
                                                             && s.NobetUstGrupId == data.NobetUstGrupId)
                                                    .Select(w => w.PasifMi == false).SingleOrDefault();
                        if (digerGunlerMinHedef)
                        {
                            model.AddConstraint(
                                Expression.Sum(data.EczaneNobetTarihAralik
                                                .Where(e => e.EczaneNobetGrupId == hedef.EczaneNobetGrupId
                                                            && e.NobetGunKuralId == gunDeger
                                                            && e.NobetGrupId == nobetGrup.Id
                                                            //&& e.EczaneNobetGrupId != pazarTutanEczane
                                                            )
                                                .Select(m => _x[m])) >= minArz,
                                                $"her eczaneye bir ayda nobet grubunun {gunDeger} hedefi kadar nobet yazilmali, {hedef}");
                        }
                    }
                    #endregion

                    #region son uc ayda pazar günü nöbet tutanlar çözüm ayında pazar tutmasın
                    //if (data.Ay > 1)
                    //{
                    //    //foreach (var eczaneNobetGrupId in data.SonUcAydaPazarGunuNobetTutanEczaneler.Where(s => hedef.EczaneNobetGrupId == s))
                    //    //{
                    //    var eczaneNobetGrupId = data.SonUcAydaPazarGunuNobetTutanEczaneler.Where(s => hedef.EczaneNobetGrupId == s).FirstOrDefault();
                    //    if (eczaneNobetGrupId > 0)
                    //    {
                    //        var pazarlar = data.TarihAraligi.Where(s => s.HaftaninGunu == 1);
                    //        foreach (var tarih in pazarlar)
                    //        {
                    //            model.AddConstraint(
                    //              Expression.Sum(data.EczaneNobetTarihAralik
                    //                               .Where(e => e.EczaneNobetGrupId == eczaneNobetGrupId
                    //                                        && e.TakvimId == tarih.TakvimId)
                    //                               .Select(m => _x[m])) == 0,
                    //                               $"son uc ayda pazar gunu nobet tutanlar cozum ayinda pazar tutmasin, {tarih}");
                    //        }
                    //    }
                    //    //}
                    //}

                    #endregion
                }
            }
            #endregion

            #region Yılda en fazla aynı gün 3'ten fazla nöbet tutulmasın

            #region yil içinde en fazla 3 kez aynı gün nöbet tutulsun
            var yildaEncokUcKezGrup = data.NobetUstGrupKisitlar
                                        .Where(s => s.KisitAdi == "yildaEncokUcKezGrup"
                                                    && s.NobetUstGrupId == data.NobetUstGrupId)
                                        .Select(w => w.PasifMi == false).SingleOrDefault();
            if (yildaEncokUcKezGrup)
            {
                foreach (var ciftGrup in data.YilIcindeAyniGunNobetTutanEczaneler.Select(s => s.Id).Distinct().ToList())
                {
                    var eczaneler = data.YilIcindeAyniGunNobetTutanEczaneler
                        .Where(s => s.Id == ciftGrup).Select(w => w.EczaneId).ToList();

                    //int siraliGun;
                    siraliGun = 1;
                    //siraliGun = (int)data.NobetGrupKurallar
                    //    .Where(s => s.NobetGrupId == nobetGrupId
                    //             && s.NobetKuralId == 4)
                    //    .Select(s => s.Deger).FirstOrDefault();

                    foreach (var tarih in data.TarihAraligi.Where(s => s.HaftaninGunu != siraliGun))
                    {
                        model.AddConstraint(
                          Expression.Sum(data.EczaneNobetTarihAralik
                                           .Where(e => eczaneler.Contains(e.EczaneId)
                                                    && e.TakvimId == tarih.TakvimId)
                                           .Select(m => _x[m])) <= 1,
                                           $"eczaneler ay icinde iki kez ayni grup olmasin, {tarih}");
                    }
                }
            }
            #endregion

            #region son iki ayda aynı gün nöbet tutanlar çözüm ayında aynı gün nöbet tutmasın
            var sonIkiAydakiGrup = data.NobetUstGrupKisitlar
                                        .Where(s => s.KisitAdi == "sonIkiAydakiGrup"
                                                    && s.NobetUstGrupId == data.NobetUstGrupId)
                                        .Select(w => w.PasifMi == false).SingleOrDefault();
            if (sonIkiAydakiGrup)
            {
                foreach (var ciftGrup in data.SonIkiAyAyniGunNobetTutanEczaneler.Select(s => s.Id).Distinct().ToList())
                {
                    var eczaneler = data.SonIkiAyAyniGunNobetTutanEczaneler
                        .Where(s => s.Id == ciftGrup).Select(w => w.EczaneId).ToList();

                    //var eczaneler2 = data.EczaneNobetGruplar.Where(s => eczaneler.Contains(s.EczaneId)).Select(e=> new { e.NobetGrupId, e.EczaneId }).ToList();

                    //int siraliGun;
                    siraliGun = 1;
                    //siraliGun = (int)data.NobetGrupKurallar
                    //    .Where(s => s.NobetGrupId == nobetGrupId
                    //             && s.NobetKuralId == 4)
                    //    .Select(s => s.Deger).FirstOrDefault();

                    foreach (var tarih in data.TarihAraligi.Where(s => s.HaftaninGunu != siraliGun))
                    {
                        model.AddConstraint(
                          Expression.Sum(data.EczaneNobetTarihAralik
                                           .Where(e => eczaneler.Contains(e.EczaneId)
                                                    && e.TakvimId == tarih.TakvimId
                                                    )
                                           .Select(m => _x[m])) <= 1,
                                           $"eczaneler son uc ay ayni grup olmasin, {tarih}");
                    }
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
            if (ayIcindeAyniGunNobet)
            {
                foreach (var g in data.AyIcindeAyniGunNobetTutanEczaneler.Select(s => s.Id).Distinct().ToList())
                {
                    var ikiliEczaneler = data.AyIcindeAyniGunNobetTutanEczaneler
                                                    .Where(w => w.Id == g)
                                                    .Select(s => s.EczaneId).ToList();

                    foreach (var tarih in data.TarihAraligi.Take(data.TarihAraligi.Count() - 1))
                    {
                        foreach (var tarih2 in data.TarihAraligi.Where(s => s.TakvimId > tarih.TakvimId))
                        {
                            model.AddConstraint(
                                  Expression.Sum(data.EczaneNobetTarihAralik
                                                   .Where(e => ikiliEczaneler.Contains(e.EczaneId)
                                                            && (e.TakvimId == tarih.TakvimId || e.TakvimId == tarih2.TakvimId))
                                                   .Select(m => _x[m])) <= 3,
                                                   $"ay icinde diger gruptaki eczaneler ile en fazla bir kez nobet tutulsun, {g}");
                        }
                    }
                }
            }
            #endregion

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
                    var eczaneler = data.EczaneGruplar
                                            .Where(x => x.EczaneGrupTanimId == g.Id)
                                            .Select(s => s.EczaneId).Distinct().ToList();

                    var nobetGruplar = data.EczaneNobetGruplar
                        .Where(s => eczaneler.Contains(s.EczaneId))
                        .Select(w => w.NobetGrupGorevTipId //w.NobetGrupId
                        ).Distinct();

                    //a1: sıralı gün varsa bu kısıt devre dışı kalır--o gün bayram olarak sayılmaz
                    var siraliGunKural = data.NobetGrupKurallar
                                        //.Where(s => nobetGruplar.Contains(s.NobetGrupId)
                                        //         && s.NobetKuralId == 4) //s.NobetKuralId == 4:sıralı gün kuralı
                                        .Select(s => s.Deger).FirstOrDefault();

                    if (siraliGunKural != null)
                    {
                        siraliGun = (int)siraliGunKural;
                    }
                    else
                    {
                        siraliGun = 0;
                    }

                    //foreach (var tarih in data.TarihAraligi.Take(data.TarihAraligi.Count() - g.ArdisikNobetSayisi))
                    //{
                    //    var grupTanimlar = Expression.Sum(data.EczaneNobetTarihAralik
                    //                    .Where(e => eczaneler.Contains(e.EczaneId)
                    //                                && (e.Gun >= tarih.Gun && e.Gun <= tarih.Gun + g.ArdisikNobetSayisi)
                    //                                && e.HaftaninGunu != siraliGun //a1
                    //                            )
                    //                    .Select(m => _x[m])) <= 1;
                    //    model.AddConstraint(grupTanimlar,
                    //                                $"herbir_eczaneGrupTanimdaki_eczaneler_beraber_nobet_tutmasin, {tarih.Gun}");
                    //}
                }
            }
            #endregion

            #region İsteğe Görev Yazılsın
            var istek = data.NobetUstGrupKisitlar
                            .Where(s => s.KisitAdi == "istek"
                                        && s.NobetUstGrupId == data.NobetUstGrupId)
                            .Select(w => w.PasifMi == false).SingleOrDefault();
            if (istek && data.Ay < 6)
            {
                foreach (var f in data.EczaneNobetIstekListe)
                {
                    model.AddConstraint(
                              Expression.Sum(data.EczaneNobetTarihAralik
                                               .Where(e => e.EczaneId == f.EczaneId
                                                        && e.NobetGrupId == f.NobetGrupId
                                                        && e.TakvimId == f.TakvimId
                                                     )
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
                foreach (var f in data.EczaneNobetMazeretListe)
                {
                    model.AddConstraint(
                              Expression.Sum(data.EczaneNobetTarihAralik
                                               .Where(e => e.EczaneId == f.EczaneId
                                                        && e.NobetGrupId == f.NobetGrupId
                                                        && e.TakvimId == f.TakvimId
                                                     )
                                               .Select(m => _x[m])) == 0,
                                               $"mazerete nobet yazma, {f}");
                }
            }
            #endregion

            #region Bayram günlerinde en fazla 1 görev yazılsın.
            //eğer bayram günleri ardışık günlerden fazlaysa bayram süresince 2 görev yazılmasın
            var bayram = data.NobetUstGrupKisitlar
                            .Where(s => s.KisitAdi == "bayram"
                                        && s.NobetUstGrupId == data.NobetUstGrupId)
                            .Select(w => w.PasifMi == false).SingleOrDefault();
            if (bayram)
            {
                foreach (var nobetGrup in data.NobetGruplar)
                {
                    int pespeseNobetSayisi = (int)data.NobetGrupKurallar
                            //.Where(s => s.NobetGrupId == nobetGrup.Id
                            //         && s.NobetKuralId == 1)
                            .Select(s => s.Deger).SingleOrDefault();

                    if (data.TarihAraligi.Where(w => w.NobetGunKuralId > 7).Count() > pespeseNobetSayisi)
                    {
                        foreach (var f in data.EczaneKumulatifHedefler.Where(x => x.NobetGrupId == nobetGrup.Id))
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
            }
            #endregion

            #endregion
            #endregion

            return model;
        }

        /// <summary>
        /// Eş durumu gibi nedenlerle birbirine bağlı grupların birlikte çözümü için nöbetçi eczane optimizasyon modelli
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public EczaneNobetSonucModel Solve(EczaneNobetCokGrupDataModel data)
        {
            var config = new Configuration
            {
                NameHandling = NameHandlingStyle.UniqueLongNames,
                ComputeRemovedVariables = true
            };

            using (var scope = new ModelScope(config))
            {
                var model = Model(data);
                // Get a solver instance, change your solver
                var solver = new CplexSolver();

                try
                {
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
                catch (Exception ex)
                {
                    var mesaj = ex.Message;
                    Results = _eczaneNobetCokGrupAltOptimization.Solve(data);
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
    }
}

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