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
using WM.Northwind.Entities.Concrete.Optimization.EczaneNobet;
using WM.Optimization.Abstract.Health;

namespace WM.Optimization.Concrete.Optano.Health.EczaneNobet.HedefProgramlama
{
    public class HpMersinMerkezOptanoV2 : IEczaneNobetMersinMerkezOptimizationV2
    {
        #region Değişkenler
        //Karar değişkeni model çalıştıktan sonra değer aldığından burada tanımlandı
        private VariableCollection<EczaneNobetTarihAralik> x { get; set; }
        private VariableCollection<EczaneNobetAltGrupTarihAralik> y { get; set; }

        #region sapma değişkenleri
        //private VariableCollection<EczaneNobetGrupDetay> _hPazartesiPS { get; set; }
        //private VariableCollection<EczaneNobetGrupDetay> _hPazartesiNS { get; set; }
        //private VariableCollection<EczaneNobetGrupDetay> _hSaliPS { get; set; }
        //private VariableCollection<EczaneNobetGrupDetay> _hSaliNS { get; set; }
        //private VariableCollection<EczaneNobetGrupDetay> _hCarsambaPS { get; set; }
        //private VariableCollection<EczaneNobetGrupDetay> _hCarsambaNS { get; set; }
        //private VariableCollection<EczaneNobetGrupDetay> _hPersembePS { get; set; }
        //private VariableCollection<EczaneNobetGrupDetay> _hPersembeNS { get; set; }
        //private VariableCollection<EczaneNobetGrupDetay> _hCumaPS { get; set; }
        //private VariableCollection<EczaneNobetGrupDetay> _hCumaNS { get; set; }
        //private VariableCollection<EczaneNobetGrupDetay> _hCumartesiPS { get; set; }
        //private VariableCollection<EczaneNobetGrupDetay> _hCumartesiNS { get; set; }
        //private VariableCollection<EczaneNobetGrupDetay> _hHiciPS { get; set; }
        //private VariableCollection<EczaneNobetGrupDetay> _hHiciNS { get; set; }
        //private VariableCollection<EczaneNobetGrupDetay> _hHiciPespesePS { get; set; }
        //private VariableCollection<EczaneNobetGrupDetay> _hHiciPespeseNS { get; set; } 
        #endregion
        #endregion

        private CplexSolverConfiguration _solverConfig;
        private CplexSolver _solver;

        public HpMersinMerkezOptanoV2()
        {
            _solverConfig = new CplexSolverConfiguration() { ComputeIIS = true };

            _solver = new CplexSolver(_solverConfig);
        }

        private Model Model(MersinMerkezDataModelV2 data)
        {
            var model = new Model() { Name = "Mersin Merkez Eczane Nöbet" };

            #region tur çevrim katsayıları            

            int haftaIciCevrim = 500;
            int cumartesiCevrim = 900;
            int bayramCevrim = 8000;
            int pazarCevrim = 1000;

            //var knt = data.EczaneNobetGrupGunKuralIstatistikler
            //    .Where(w => w.EczaneNobetGrupId == 415).ToList();
            #endregion

            #region Karar Değişkenleri
            x = new VariableCollection<EczaneNobetTarihAralik>(
                    model,
                    data.EczaneNobetTarihAralik,
                    "x",
                    null,
                    h => 0,
                    h => 1,
                    a => VariableType.Binary);

            y = new VariableCollection<EczaneNobetAltGrupTarihAralik>(
                    model,
                    data.EczaneNobetAltGrupTarihAralik,
                    "y",
                    null,
                    h => 0,
                    h => 1,
                    a => VariableType.Binary);

            #region sapma değişkenleri
            //_hPazartesiPS = new VariableCollection<EczaneNobetGrupDetay>(
            //                        model,
            //                        data.EczaneNobetGruplar,
            //                        "_hPazartesiPS",
            //                        null,
            //                        h => data.LowerBound,
            //                        h => 10,
            //                        a => VariableType.Continuous);

            //_hPazartesiNS = new VariableCollection<EczaneNobetGrupDetay>(
            //                    model,
            //                    data.EczaneNobetGruplar,
            //                    "_hPazartesiNS",
            //                    null,
            //                    h => data.LowerBound,
            //                    h => 10,
            //                    a => VariableType.Continuous);

            //_hSaliPS = new VariableCollection<EczaneNobetGrupDetay>(
            //                    model,
            //                    data.EczaneNobetGruplar,
            //                    "_hSaliPS",
            //                    null,
            //                    h => data.LowerBound,
            //                    h => 10,
            //                    a => VariableType.Continuous);

            //_hSaliNS = new VariableCollection<EczaneNobetGrupDetay>(
            //                    model,
            //                    data.EczaneNobetGruplar,
            //                    "_hSaliNS",
            //                    null,
            //                    h => data.LowerBound,
            //                    h => 10,
            //                    a => VariableType.Continuous);

            //_hCarsambaPS = new VariableCollection<EczaneNobetGrupDetay>(
            //        model,
            //        data.EczaneNobetGruplar,
            //        "_hCarsambaPS",
            //        null,
            //        h => data.LowerBound,
            //        h => 10,
            //        a => VariableType.Continuous);

            //_hCarsambaNS = new VariableCollection<EczaneNobetGrupDetay>(
            //                    model,
            //                    data.EczaneNobetGruplar,
            //                    "_hCarsambaNS",
            //                    null,
            //                    h => data.LowerBound,
            //                    h => 10,
            //                    a => VariableType.Continuous);

            //_hPersembePS = new VariableCollection<EczaneNobetGrupDetay>(
            //        model,
            //        data.EczaneNobetGruplar,
            //        "_hPersembePS",
            //        null,
            //        h => data.LowerBound,
            //        h => 10,
            //        a => VariableType.Continuous);

            //_hPersembeNS = new VariableCollection<EczaneNobetGrupDetay>(
            //                    model,
            //                    data.EczaneNobetGruplar,
            //                    "_hPersembeNS",
            //                    null,
            //                    h => data.LowerBound,
            //                    h => 10,
            //                    a => VariableType.Continuous);

            //_hCumaPS = new VariableCollection<EczaneNobetGrupDetay>(
            //        model,
            //        data.EczaneNobetGruplar,
            //        "_hCumaPS",
            //        null,
            //        h => data.LowerBound,
            //        h => 10,
            //        a => VariableType.Continuous);

            //_hCumaNS = new VariableCollection<EczaneNobetGrupDetay>(
            //                    model,
            //                    data.EczaneNobetGruplar,
            //                    "_hCumaNS",
            //                    null,
            //                    h => data.LowerBound,
            //                    h => 10,
            //                    a => VariableType.Continuous);

            //_hCumartesiPS = new VariableCollection<EczaneNobetGrupDetay>(
            //    model,
            //    data.EczaneNobetGruplar,
            //    "_hCumartesiPS",
            //    null,
            //    h => data.LowerBound,
            //    h => 10,
            //    a => VariableType.Continuous);

            //_hCumartesiNS = new VariableCollection<EczaneNobetGrupDetay>(
            //                    model,
            //                    data.EczaneNobetGruplar,
            //                    "_hCumartesiNS",
            //                    null,
            //                    h => data.LowerBound,
            //                    h => 10,
            //                    a => VariableType.Continuous);

            //_hHiciPS = new VariableCollection<EczaneNobetGrupDetay>(
            //                    model,
            //                    data.EczaneNobetGruplar,
            //                    "_hHiciPS",
            //                    null,
            //                    h => data.LowerBound,
            //                    h => 10,
            //                    a => VariableType.Continuous);

            //_hHiciNS = new VariableCollection<EczaneNobetGrupDetay>(
            //                    model,
            //                    data.EczaneNobetGruplar,
            //                    "_hHiciNS",
            //                    null,
            //                    h => data.LowerBound,
            //                    h => 10,
            //                    a => VariableType.Continuous);

            //_hHiciPespesePS = new VariableCollection<EczaneNobetGrupDetay>(
            //        model,
            //        data.EczaneNobetGruplar,
            //        "_hHiciPespesePS",
            //        null,
            //        h => data.LowerBound,
            //        h => 10,
            //        a => VariableType.Continuous);

            //_hHiciPespeseNS = new VariableCollection<EczaneNobetGrupDetay>(
            //                    model,
            //                    data.EczaneNobetGruplar,
            //                    "_hHiciPespeseNS",
            //                    null,
            //                    h => data.LowerBound,
            //                    h => 10,
            //                    a => VariableType.Continuous); 
            #endregion

            #endregion

            #region Amaç Fonksiyonu

            var pespeseHaftaIciAyniGunNobet = data.Kisitlar
                                          .Where(s => s.KisitAdi == "pespeseHaftaIciAyniGunNobet"
                                                   && s.NobetUstGrupId == data.NobetUstGrupId)
                                          .Select(w => w.PasifMi == false).SingleOrDefault();

            var amac1 = Expression.Sum(
                                (from i in data.EczaneNobetTarihAralik
                                 from p in data.EczaneNobetGrupGunKuralIstatistikYatay
                                 where i.EczaneNobetGrupId == p.EczaneNobetGrupId
                                    && i.NobetGorevTipId == p.NobetGorevTipId
                                 select (x[i]
                                        //ilk yazılan nöbet öncelikli olsun:
                                        + x[i] * Convert.ToInt32(i.BayramMi)
                                                * (bayramCevrim + bayramCevrim / Math.Sqrt((i.Tarih - p.SonNobetTarihiBayram).TotalDays))

                                        + x[i] * Convert.ToInt32(i.CumartesiGunuMu)
                                                * (cumartesiCevrim + cumartesiCevrim / Math.Sqrt((i.Tarih - p.SonNobetTarihiCumartesi).TotalDays * Math.Ceiling((double)i.Tarih.Day / 7)))

                                        + x[i] * Convert.ToInt32(i.PazarGunuMu)
                                                * (pazarCevrim + pazarCevrim / Math.Sqrt((i.Tarih - p.SonNobetTarihiPazar).TotalDays * Math.Ceiling((double)i.Tarih.Day / 7)))

                                        + x[i] * Convert.ToInt32(i.HaftaIciMi)
                                                * (haftaIciCevrim + haftaIciCevrim / Math.Sqrt((i.Tarih - p.SonNobetTarihiHaftaIci).TotalDays * i.Tarih.Day)
                                                * (pespeseHaftaIciAyniGunNobet == true
                                                ? (i.Tarih.DayOfWeek == p.SonNobetTarihiHaftaIci.DayOfWeek ? 1 : 0.2)
                                                : 1)//aynı gün peşpeşe gelmesin
                                                )
                                        )));

            //var amac2 = Expression.Sum((from i in data.EczaneNobetAltGrupTarihAralik
            //                            select y[i] * 9000
            //                            )
            //                           );

            var amac = new Objective(amac1,
                        "Sum of all item-values: ",
                        ObjectiveSense.Minimize);

            model.AddObjective(amac);
            #endregion

            #region Kısıtlar

            foreach (var nobetGrup in data.NobetGruplar)
            {
                #region ön hazırlama

                var tarihler = data.TarihAraligi.Where(w => w.NobetGrupId == nobetGrup.Id).ToList();
                var bayramlar = data.TarihAraligi.Where(w => w.NobetGunKuralId > 7 && w.NobetGrupId == nobetGrup.Id).OrderBy(o => o.Tarih).ToList();
                //var bayramVePazarlar = data.TarihAraligi.Where(w => (w.NobetGunKuralId > 7 || w.NobetGunKuralId == 1) && w.NobetGrupId == nobetGrup.Id).OrderBy(o => o.Tarih).ToList();
                var cumartesiGunleri = data.TarihAraligi.Where(w => w.NobetGunKuralId == 7 && w.NobetGrupId == nobetGrup.Id).OrderBy(o => o.Tarih).ToList();
                var pazarGunleri = data.TarihAraligi.Where(w => w.NobetGunKuralId == 1 && w.NobetGrupId == nobetGrup.Id).OrderBy(o => o.Tarih).ToList();
                var haftaIciGunleri = data.TarihAraligi.Where(w => (w.NobetGunKuralId >= 2 && w.NobetGunKuralId < 7) && w.NobetGrupId == nobetGrup.Id).OrderBy(o => o.Tarih).ToList();

                var nobetGunKurallar = data.TarihAraligi.Where(w => w.NobetGrupId == nobetGrup.Id).Select(s => s.NobetGunKuralId).Distinct().ToList();

                var r = new Random();

                var eczaneNobetGruplar = data.EczaneNobetGruplar
                    .Where(w => w.NobetGrupId == nobetGrup.Id)
                    .OrderBy(x => //x.EczaneAdi
                                  r.NextDouble()
                            ).ToList();

                var nobetGrupGunKurallar = data.NobetGrupGunKurallar
                                                    .Where(s => s.NobetGrupId == nobetGrup.Id
                                                             && nobetGunKurallar.Contains(s.NobetGunKuralId))
                                                    .Select(s => s.NobetGunKuralId).ToList();

                var gruptakiNobetciSayisi = eczaneNobetGruplar.Select(s => s.EczaneId).Count();
                var gunSayisi = tarihler.Count();

                #region en fazla görev sayısı

                var herAyEnFazlaGorevSayisi = Math.Ceiling((double)gunSayisi / gruptakiNobetciSayisi);

                var herAyEnFazlaGorevStd = data.Kisitlar
                                         .Where(s => s.KisitAdi == "herAyEnFazlaGorev"
                                                  && s.NobetUstGrupId == data.NobetUstGrupId)
                                         .Select(w => w.SagTarafDegeri).SingleOrDefault();

                if (herAyEnFazlaGorevStd > 0)
                    herAyEnFazlaGorevSayisi += herAyEnFazlaGorevStd;
                #endregion

                #region hafta içi görev sayısı

                var haftaIciOrtamalaNobetSayisi = Math.Ceiling((double)haftaIciGunleri.Count / gruptakiNobetciSayisi);

                var herAyEnFazlaHaftaIciStd = data.Kisitlar
                                              .Where(s => s.KisitAdi == "herAyEnFazlaHaftaIci"
                                                       && s.NobetUstGrupId == data.NobetUstGrupId)
                                              .Select(w => w.SagTarafDegeri).SingleOrDefault();

                if (herAyEnFazlaHaftaIciStd > 0)
                    haftaIciOrtamalaNobetSayisi += (int)herAyEnFazlaHaftaIciStd;

                #endregion

                #region ilk ayda fazla yazılacak eczaneler

                var fazlaYazilacakEczaneSayisi = gunSayisi - gruptakiNobetciSayisi;
                var fazlaYazilacakEczaneler = new List<EczaneNobetGrupDetay>();

                //ilk ayda eczane sayısı gün sayısından azsa, eksik sayı kadar rasgele seçilen eczaneye 1 fazla nöbet yazılır. toplam hedeflerde
                if (data.Yil == 2018 && data.Ay == 1) //&& fazlaYazilacakEczaneSayisi > 0)
                {
                    fazlaYazilacakEczaneler = eczaneNobetGruplar.OrderBy(x => r.NextDouble()).Take(fazlaYazilacakEczaneSayisi).ToList();
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
                    foreach (var gunKuralId in nobetGrupGunKurallar)
                    {
                        var cozulenAydakiNobetSayisi = tarihler.Where(w => w.NobetGunKuralId == gunKuralId).Count();

                        nobetGunKuralDetaylar.Add(GetNobetGunKural(gunKuralId, data, nobetGrup.Id, gruptakiNobetciSayisi, cozulenAydakiNobetSayisi));
                    }
                }

                //var cozulenAydakiHaftaIciToplamNobetSayisi = haftaIciGunleri.Count();

                //nobetGunKuralDetaylar.Add(GetNobetGunKuralHaftaIciToplam(data, nobetGrup.Id, gruptakiNobetciSayisi, cozulenAydakiHaftaIciToplamNobetSayisi));
                #endregion

                #endregion

                #region Talep Kısıtları

                foreach (var nobetGrupGorevTip in data.NobetGrupGorevTipler.Where(w => w.NobetGrupId == nobetGrup.Id))
                {
                    foreach (var d in tarihler)
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
                                                    ).Select(m => x[m])) == gunlukNobetciSayisi,
                                                    $"her_gun_talep_kadar_eczane_atanmali: {gunlukNobetciSayisi}");
                    }
                }

                #endregion

                #region Eczane bazlı kısıtlar

                foreach (var eczaneNobetGrup in eczaneNobetGruplar)
                {
                    #region kontrol

                    var kontrol = false;

                    if (kontrol)
                    {
                        var kontrolEdilecekEczaneler = new string[] { "NURLU" };

                        if (kontrolEdilecekEczaneler.Contains(eczaneNobetGrup.EczaneAdi))
                        {
                        }
                    } 
                    #endregion

                    #region aktif kısıtlar

                    #region Ay içinde peşpeşe görev yazılmasın

                    var herAyPespeseGorev = data.Kisitlar
                                .Where(s => s.KisitAdi == "herAyPespeseGorev"
                                         && s.NobetUstGrupId == data.NobetUstGrupId)
                                .Select(w => w.PasifMi == false).SingleOrDefault();

                    if (herAyPespeseGorev)
                    {
                        var pespeseNobetSayisi = (int)data.NobetGrupKurallar
                                .Where(s => s.NobetGrupId == nobetGrup.Id
                                         && s.NobetKuralId == 1)
                                .Select(s => s.Deger).SingleOrDefault();

                        foreach (var g in tarihler.Take(tarihler.Count() - pespeseNobetSayisi))
                        {
                            //model.AddConstraint(
                            //  Expression.Sum(data.EczaneNobetTarihAralik
                            //                   .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                            //                               && (e.Gun >= g.Gun && e.Gun <= g.Gun + pespeseNobetSayisi))
                            //                   .Select(m => x[m])) <= 1,
                            //                   $"eczanelere_pespese_nobet_yazilmasin, {eczaneNobetGrup}");
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
                        #region 3/5 olursa
                        //var farkliAyPespeseGorevAraligi = gruptakiNobetciSayisi * 0.6;

                        //if (data.CalismaSayisi == 6 || data.CalismaSayisi == 7)
                        //{
                        //    farkliAyPespeseGorevAraligi = gruptakiNobetciSayisi * 0.3;
                        //}
                        //else if (data.CalismaSayisi == 8)
                        //{
                        //    farkliAyPespeseGorevAraligi = 8;
                        //}
                        //else if (data.CalismaSayisi == 9)
                        //{
                        //    farkliAyPespeseGorevAraligi = 5;
                        //}

                        //var pazarVeBayramlar = new List<int> { 1, 8, 9 };
                        //var farkliAyPespeseGorevAraligifStd = data.NobetUstGrupKisitlar
                        //                                    .Where(s => s.KisitAdi == "farkliAyPespeseGorev"
                        //                                             && s.NobetUstGrupId == data.NobetUstGrupId)
                        //                                    .Select(w => w.SagTarafDegeri).SingleOrDefault();

                        //if (farkliAyPespeseGorevAraligifStd > 0)
                        //{
                        //    farkliAyPespeseGorevAraligi = farkliAyPespeseGorevAraligifStd;
                        //} 
                        #endregion

                        var pespeseNobetSayisi = (int)data.NobetGrupKurallar
                                .Where(s => s.NobetGrupId == nobetGrup.Id
                                         && s.NobetKuralId == 1)
                                .Select(s => s.Deger).SingleOrDefault();

                        var enSonNobetTarihi = data.EczaneNobetSonuclar
                            .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id)
                            .Select(s => s.Tarih).OrderByDescending(o => o).FirstOrDefault();

                        var yazilabilecekIlkTarih = enSonNobetTarihi.AddDays(pespeseNobetSayisi);//farkliAyPespeseGorevAraligi);

                        if (enSonNobetTarihi != null)
                        {
                            foreach (var g in tarihler.Where(w => w.Tarih <= yazilabilecekIlkTarih))
                            {
                                model.AddConstraint(
                                  Expression.Sum(data.EczaneNobetTarihAralik
                                                   .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                               && e.TakvimId == g.TakvimId
                                                               && e.NobetGorevTipId == 1)
                                                   .Select(m => x[m])) == 0,
                                                   $"eczanelere_farkli_aylarda_pespese_nobet_yazilmasin, {eczaneNobetGrup}");
                            }
                        }
                    }
                    #endregion              

                    #region Farklı aylar peşpeşe görev yazılmasın (Pazar peşpeşe en az)

                    var pazarPespeseGorevEnAz = data.Kisitlar
                                                    .Where(s => s.KisitAdi == "pazarPespeseGorevEnAz"
                                                             && s.NobetUstGrupId == data.NobetUstGrupId)
                                                    .Select(w => w.PasifMi == false).SingleOrDefault();

                    if (pazarPespeseGorevEnAz)
                    {
                        var enSonPazarTuttuguTarih1 = data.EczaneNobetSonuclar
                            .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id
                                     && w.NobetGunKuralId == 1)
                            .Select(s => s.TakvimId).OrderByDescending(o => o).FirstOrDefault();

                        var kontrolListesi = new int[] { 154 };
                        if (kontrolListesi.Contains(eczaneNobetGrup.Id))
                        {
                        }

                        if (enSonPazarTuttuguTarih1 > 0) //enSonPazarTuttuguNobetAyi
                        {
                            var enSonPazarTuttuguTarih = data.EczaneNobetSonuclar
                            .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id
                                     && w.NobetGunKuralId == 1)
                            .Select(s => new { s.Tarih, s.TakvimId }).OrderByDescending(o => o.Tarih).FirstOrDefault();

                            var pazarNobetiYazilabilecekIlkAy = (int)Math.Ceiling((double)gruptakiNobetciSayisi / 5) - 1;

                            var yazilabilecekIlkTarih = enSonPazarTuttuguTarih.Tarih.AddMonths(pazarNobetiYazilabilecekIlkAy);

                            var tarihlerGruplu = pazarGunleri
                                .GroupBy(g => new { g.Yil, g.Ay })
                                .Select(s => new
                                {
                                    s.Key.Yil,
                                    s.Key.Ay,
                                    IlkTarih = s.Min(m => m.Tarih),
                                    SonTarih = s.Max(m => m.Tarih)
                                }).ToList();

                            var tarihAralik = (from t1 in pazarGunleri
                                               from t2 in tarihlerGruplu
                                               where t1.Yil == t2.Yil
                                               && t1.Yil == t2.Yil
                                               && t2.IlkTarih < yazilabilecekIlkTarih
                                               select new { t1.TakvimId, t1.Tarih, t2.IlkTarih, t2.SonTarih }).ToList();

                            foreach (var g in tarihAralik)
                            {
                                model.AddConstraint(
                                  Expression.Sum(data.EczaneNobetTarihAralik
                                                   .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                               && e.TakvimId == g.TakvimId
                                                               && e.NobetGrupId == nobetGrup.Id)
                                                   .Select(m => x[m])) == 0,
                                                   $"eczanelere_farkli_aylarda_pazar_gunu_pespese_nobet_yazilmasin_enaz, {eczaneNobetGrup.Id}");
                            }
                        }
                    }
                    #endregion

                    #region Farklı aylar peşpeşe görev yazılmasın (Cumartesi peşpeşe en az)

                    var cumartesiPespeseGorevEnAz = data.Kisitlar
                                                    .Where(s => s.KisitAdi == "cumartesiPespeseGorevEnAz"
                                                             && s.NobetUstGrupId == data.NobetUstGrupId)
                                                    .Select(w => w.PasifMi == false).SingleOrDefault();

                    if (cumartesiPespeseGorevEnAz)
                    {
                        var enSonCumartesiTuttuguTarih1 = data.EczaneNobetSonuclar
                            .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id
                                     && w.NobetGunKuralId == 7)
                            .Select(s => s.TakvimId)
                            .OrderByDescending(o => o).FirstOrDefault();

                        var kontrolListesi = new int[] { 154 };
                        if (kontrolListesi.Contains(eczaneNobetGrup.Id))
                        {
                        }

                        if (enSonCumartesiTuttuguTarih1 > 0) //enSonPazarTuttuguNobetAyi
                        {
                            var enSonCumartesiTuttuguTarih = data.EczaneNobetSonuclar
                                .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id
                                         && w.NobetGunKuralId == 7)
                                .Select(s => new { s.Tarih, s.TakvimId })
                                .OrderByDescending(o => o.Tarih).FirstOrDefault();

                            var cumartesiNobetiYazilabilecekIlkAy = (int)Math.Ceiling((double)gruptakiNobetciSayisi / 5) - 1;

                            var yazilabilecekIlkTarih = enSonCumartesiTuttuguTarih.Tarih.AddMonths(cumartesiNobetiYazilabilecekIlkAy);

                            var tarihlerGruplu = cumartesiGunleri
                                .GroupBy(g => new { g.Yil, g.Ay })
                                .Select(s => new
                                {
                                    s.Key.Yil,
                                    s.Key.Ay,
                                    IlkTarih = s.Min(m => m.Tarih),
                                    SonTarih = s.Max(m => m.Tarih)
                                }).ToList();

                            var tarihAralik = (from t1 in cumartesiGunleri
                                               from t2 in tarihlerGruplu
                                               where t1.Yil == t2.Yil
                                               && t1.Yil == t2.Yil
                                               && t2.IlkTarih < yazilabilecekIlkTarih
                                               select new { t1.TakvimId, t1.Tarih, t2.IlkTarih, t2.SonTarih }).ToList();

                            foreach (var g in tarihAralik)
                            {
                                model.AddConstraint(
                                  Expression.Sum(data.EczaneNobetTarihAralik
                                                   .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                               && e.TakvimId == g.TakvimId
                                                               && e.NobetGrupId == nobetGrup.Id)
                                                   .Select(m => x[m])) == 0,
                                                   $"eczanelere_farkli_aylarda_pazar_gunu_pespese_nobet_yazilmasin_enaz, {eczaneNobetGrup.Id}");
                            }
                        }
                    }
                    #endregion

                    #region Farklı aylar peşpeşe görev yazılmasın (hafta içi peşpeşe en az)

                    var haftaIciPespeseGorevEnAz = data.Kisitlar
                                            .Where(s => s.KisitAdi == "haftaIciPespeseGorevEnAz"
                                                     && s.NobetUstGrupId == data.NobetUstGrupId)
                                            .Select(w => w.PasifMi == false).SingleOrDefault();

                    var farkliAyPespeseGorevAraligi = (gruptakiNobetciSayisi * 1.2);
                    var altLimit = farkliAyPespeseGorevAraligi * 0.7666; //0.95
                    var ustLimit = farkliAyPespeseGorevAraligi + farkliAyPespeseGorevAraligi * 0.6667; //77;
                    var ustLimitKontrol = ustLimit * 0.95; //0.81

                    if (haftaIciPespeseGorevEnAz)
                    {
                        var enSonNobetTarihi = data.EczaneNobetSonuclar
                            .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id
                                     && haftaIciGunleri.Select(s => s.NobetGunKuralId).Contains(w.NobetGunKuralId))
                            .Select(s => s.Tarih).OrderByDescending(o => o).FirstOrDefault();

                        var yazilabilecekIlkTarih = enSonNobetTarihi.AddDays(altLimit);
                        var yazilabilecekKontrolTarihi = enSonNobetTarihi.AddDays(ustLimitKontrol);
                        var yazilabilecekSonTarih = enSonNobetTarihi.AddDays(ustLimit);

                        if (enSonNobetTarihi != null)
                        {
                            var haftaIciGunler = haftaIciGunleri
                                .Where(w => w.Tarih <= yazilabilecekIlkTarih)
                                .Select(s => s.TakvimId).ToList();

                            if (haftaIciGunler.Count() > 0)
                            {
                                model.AddConstraint(
                                 Expression.Sum(from i in data.EczaneNobetTarihAralik
                                                from j in data.EczaneNobetGruplar
                                                where i.EczaneNobetGrupId == j.Id
                                                      && i.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                      && i.NobetGorevTipId == 1//hedef.NobetGorevTipId
                                                      && haftaIciGunler.Contains(i.TakvimId)
                                                select (x[i] //+ _hHiciPespeseNS[j] - _hHiciPespesePS[j]
                                                                )) == 0,
                                                 $"her eczaneye bir ayda nöbet grubunun {1} hedefi kadar toplam hafta içi nobeti yazılmalı, {eczaneNobetGrup.Id}");
                            }
                        }

                        #region Ay içinde peşpeşe görev yazılmasın en az

                        if (haftaIciOrtamalaNobetSayisi > 1)
                        {
                            var pespeseNobetSayisi = (int)altLimit;

                            if (pespeseNobetSayisi >= data.TarihAraligi.Count)
                            {
                                model.AddConstraint(
                                  Expression.Sum(data.EczaneNobetTarihAralik
                                                   .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                               && haftaIciGunleri.Select(s => s.TakvimId).Contains(e.TakvimId)
                                                               )
                                                   .Select(m => x[m])) <= 1,
                                                   $"eczanelere_haftaici_pespese_nobet_yazilmasin_1, {eczaneNobetGrup}");
                            }
                            else
                            {
                                foreach (var g in data.TarihAraligi.Take(data.TarihAraligi.Count() - pespeseNobetSayisi))
                                {
                                    //model.AddConstraint(
                                    //  Expression.Sum(data.EczaneNobetTarihAralik
                                    //                   .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                                    //                               && (e.Gun >= g.Gun && e.Gun <= g.Gun + pespeseNobetSayisi)
                                    //                               && haftaIciGunleri.Select(s => s.TakvimId).Contains(e.TakvimId)
                                    //                               )
                                    //                   .Select(m => x[m])) <= 1,
                                    //                   $"eczanelere_haftaici_pespese_nobet_yazilmasin, {eczaneNobetGrup}");
                                }
                            }
                        }
                        #endregion

                        #region belirlenen tarih aralığında yazmadığında nöbet yazılacak

                        var haftaIciPespeseGorevUstLimitKontrol = data.Kisitlar
                                            .Where(s => s.KisitAdi == "haftaIciPespeseGorevUstLimitKontrol"
                                                     && s.NobetUstGrupId == data.NobetUstGrupId)
                                            .Select(w => w.PasifMi == false).SingleOrDefault();

                        if (haftaIciPespeseGorevUstLimitKontrol)
                        {
                            var haftaIciGunler = haftaIciGunleri
                                .Where(w => (w.Tarih <= yazilabilecekSonTarih))
                                .Select(s => s.TakvimId).ToList();

                            var kontrolPeriyodundakitHaftaIciNobetSayisi = data.EczaneNobetSonuclar
                                           .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                    && w.NobetGorevTipId == 1//eczaneNobetGrup.NobetGorevTipId
                                                    && (w.Tarih >= yazilabilecekIlkTarih && w.Tarih <= yazilabilecekKontrolTarihi)
                                                    && haftaIciGunleri.Select(s => s.NobetGunKuralId).Contains(w.NobetGunKuralId))
                                           .Select(s => s.TakvimId).Count();

                            if (haftaIciGunler.Count() > 3)
                            {
                                if (kontrolPeriyodundakitHaftaIciNobetSayisi == 0)
                                {
                                    model.AddConstraint(
                                      Expression.Sum(data.EczaneNobetTarihAralik
                                                       .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                                   && e.NobetGorevTipId == 1//eczaneNobetGrup.NobetGorevTipId
                                                                   && haftaIciGunler.Contains(e.TakvimId))
                                                       .Select(m => x[m])) == 1,
                                                       $"eczanelere_farkli_aylarda_pespese_nobet_yazilmasin, {eczaneNobetGrup}");
                                }
                            }
                        }

                        #endregion

                        #region üst limit
                        //bu kısıt açıldığında belirlenen son tarihi geçen bir eczane olursa, o eczaneye bu tarihten sonra bir daha nöbet yazılmıyor.
                        //o nedenle tercih edilmesi çok zor bir kısıttır.
                        var haftaIciPespeseGorevUstLimit = data.Kisitlar
                                            .Where(s => s.KisitAdi == "haftaIciPespeseGorevUstLimit"
                                                     && s.NobetUstGrupId == data.NobetUstGrupId)
                                            .Select(w => w.PasifMi == false).SingleOrDefault();

                        if (haftaIciPespeseGorevUstLimit)
                        {
                            var haftaIciGunler = haftaIciGunleri
                                .Where(w => (w.Tarih >= yazilabilecekSonTarih))
                                .Select(s => s.TakvimId).ToList();

                            var kontrolPeriyodundakitHaftaIciNobetSayisi = data.EczaneNobetSonuclar
                                           .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                    && w.NobetGorevTipId == 1//eczaneNobetGrup.NobetGorevTipId
                                                    && w.Tarih >= yazilabilecekSonTarih
                                                    && haftaIciGunleri.Select(s => s.NobetGunKuralId).Contains(w.NobetGunKuralId))
                                           .Select(s => s.TakvimId).Count();

                            if (haftaIciGunler.Count > 0)
                            {
                                model.AddConstraint(
                                  Expression.Sum(data.EczaneNobetTarihAralik
                                                   .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                               && e.NobetGorevTipId == 1//eczaneNobetGrup.NobetGorevTipId
                                                               && haftaIciGunler.Contains(e.TakvimId)
                                                               )
                                                   .Select(m => x[m])) == 0,
                                                   $"eczanelere_farkli_aylarda_pespese_nobet_yazilmasin, {eczaneNobetGrup}");
                            }
                        }

                        #endregion
                    }
                    #endregion

                    #region Gün kümülatif toplam Max Hedef

                    var gunKumulatifToplamEnFazla = data.Kisitlar
                                                .Where(s => s.KisitAdi == "gunKumulatifToplamEnFazla"
                                                         && s.NobetUstGrupId == data.NobetUstGrupId)
                                                .Select(w => w.PasifMi == false).SingleOrDefault();

                    if (gunKumulatifToplamEnFazla)
                    {
                        var gunKumulatifToplamMaxHedefSTD = data.Kisitlar
                                                        .Where(s => s.KisitAdi == "gunKumulatifToplamEnFazla"
                                                                 && s.NobetUstGrupId == data.NobetUstGrupId)
                                                        .Select(w => w.SagTarafDegeri).SingleOrDefault();
                        #region hafta içi

                        var nobetGunKuralIstatistikler = data.TakvimNobetGrupGunDegerIstatistikler
                                                                            .Where(w => w.NobetGrupId == eczaneNobetGrup.NobetGrupId
                                                                                     && haftaIciGunleri.Select(s => s.NobetGunKuralId).Distinct().Contains(w.NobetGunKuralId)).ToList();

                        foreach (var gunKural in nobetGunKuralIstatistikler)
                        {
                            var kumulatifOrtalamaGunKuralSayisi = Math.Ceiling((double)gunKural.GunSayisi / gruptakiNobetciSayisi);

                            var tumHaftaIciNobetleri = data.EczaneNobetSonuclar
                                                        .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                                 && w.NobetGunKuralId == gunKural.NobetGunKuralId
                                                                 && w.Tarih >= data.NobetUstGrupBaslangicTarihi)
                                                        .Select(s => s.TakvimId);

                            var toplamHaftaIciNobetSayisi = tumHaftaIciNobetleri.Count();

                            if (!nobetGrupGunKurallar.Contains(gunKural.NobetGunKuralId))
                            {
                                kumulatifOrtalamaGunKuralSayisi++;
                            }

                            if (gunKumulatifToplamMaxHedefSTD > 0)
                            {
                                kumulatifOrtalamaGunKuralSayisi = gunKumulatifToplamMaxHedefSTD;
                            }

                            if (toplamHaftaIciNobetSayisi > kumulatifOrtalamaGunKuralSayisi)
                            {
                                kumulatifOrtalamaGunKuralSayisi = toplamHaftaIciNobetSayisi;
                            }

                            if (data.CalismaSayisi == 2)
                                kumulatifOrtalamaGunKuralSayisi++;

                            var tarihlerFiltreli = tarihler.Where(w => w.NobetGunKuralId == gunKural.NobetGunKuralId);

                            model.AddConstraint(
                              Expression.Sum(data.EczaneNobetTarihAralik
                                               .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                           && tarihlerFiltreli.Select(s => s.TakvimId).Contains(e.TakvimId))
                                               .Select(m => x[m])) + toplamHaftaIciNobetSayisi <= kumulatifOrtalamaGunKuralSayisi,
                                               $"eczanelere_herbir_hafta_icinde_encok_yilda_ortalama_kadar_gorev_yaz, {eczaneNobetGrup}");
                        }
                        #endregion

                        #region Cumartesi

                        if (nobetGrupGunKurallar.Contains(7))
                        {
                            var kumulatifCumartesiSayisi = data.TakvimNobetGrupGunDegerIstatistikler
                                                                .Where(w => w.NobetGrupId == eczaneNobetGrup.NobetGrupId
                                                                         && w.NobetGunKuralId == 7).SingleOrDefault();


                            var kumulatifOrtalamaCumartesiSayisi = Math.Ceiling((double)kumulatifCumartesiSayisi.GunSayisi / gruptakiNobetciSayisi);

                            var toplamCumartesiNobetSayisi = data.EczaneNobetSonuclar
                                                        .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                                 && w.NobetGunKuralId == 7
                                                                 && w.Tarih >= data.NobetUstGrupBaslangicTarihi)
                                                        .Select(s => s.TakvimId).Count();

                            if (toplamCumartesiNobetSayisi > kumulatifOrtalamaCumartesiSayisi)
                                kumulatifOrtalamaCumartesiSayisi = toplamCumartesiNobetSayisi;

                            model.AddConstraint(
                              Expression.Sum(data.EczaneNobetTarihAralik
                                               .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                           && cumartesiGunleri.Select(s => s.TakvimId).Contains(e.TakvimId))
                                               .Select(m => x[m])) + toplamCumartesiNobetSayisi <= kumulatifOrtalamaCumartesiSayisi,
                                               $"eczanelere_herbir_hafta_icinde_encok_yilda_ortalama_kadar_gorev_yaz, {eczaneNobetGrup}");
                        }
                        #endregion

                        #region Pazar

                        if (nobetGrupGunKurallar.Contains(1))
                        {
                            var kumulatifPazarSayisi = data.TakvimNobetGrupGunDegerIstatistikler
                                                                .Where(w => w.NobetGrupId == eczaneNobetGrup.NobetGrupId
                                                                         && w.NobetGunKuralId == 1).SingleOrDefault();

                            var kumulatifOrtalamaPazarSayisi = Math.Ceiling((double)kumulatifPazarSayisi.GunSayisi / gruptakiNobetciSayisi);

                            var toplamPazarNobetSayisi = data.EczaneNobetSonuclar
                                                            .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                                     && w.NobetGunKuralId == 1
                                                                     && w.Tarih >= data.NobetUstGrupBaslangicTarihi)
                                                            .Select(s => s.TakvimId).Count();

                            if (toplamPazarNobetSayisi > kumulatifOrtalamaPazarSayisi)
                                kumulatifOrtalamaPazarSayisi = toplamPazarNobetSayisi;

                            model.AddConstraint(
                              Expression.Sum(data.EczaneNobetTarihAralik
                                               .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                           && pazarGunleri.Select(s => s.TakvimId).Contains(e.TakvimId))
                                               .Select(m => x[m])) + toplamPazarNobetSayisi <= kumulatifOrtalamaPazarSayisi,
                                               $"eczanelere_herbir_hafta_icinde_encok_yilda_ortalama_kadar_gorev_yaz, {eczaneNobetGrup}");
                        }
                        #endregion
                    }
                    #endregion                    

                    #region Her ay en fazla 1 Cumartesi

                    var herAyEnFazla1Cumartesi = data.Kisitlar
                                                        .Where(s => s.KisitAdi == "herAyEnFazla1Cumartesi"
                                                                 && s.NobetUstGrupId == data.NobetUstGrupId)
                                                        .Select(w => w.PasifMi == false).SingleOrDefault();
                    if (herAyEnFazla1Cumartesi)
                    {
                        model.AddConstraint(
                              Expression.Sum(data.EczaneNobetTarihAralik
                                                .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                         && e.NobetGorevTipId == 1//eczaneNobetGrup.NobetGorevTipId
                                                         && e.NobetGunKuralId == 7)
                                                .Select(m => x[m])) <= 1,
                                                $"her_eczaneye_bir_ayda_en_fazla_1_cumartesi_nobeti_yazilsin, {eczaneNobetGrup}");
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
                                                .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                         && e.NobetGorevTipId == 1//eczaneNobetGrup.NobetGorevTipId
                                                         && e.NobetGunKuralId == 1)
                                                .Select(m => x[m])) <= 1,
                                                $"her eczaneye bir ayda en az nobet grubunun hedefinden bir eksik nobet yazilmali, {eczaneNobetGrup}");
                    }
                    #endregion

                    #region Her ay en fazla hafta içi
                    //(ortalama kadar)
                    var herAyEnFazlaHaftaIci = data.Kisitlar
                                                        .Where(s => s.KisitAdi == "herAyEnFazlaHaftaIci"
                                                                 && s.NobetUstGrupId == data.NobetUstGrupId)
                                                        .Select(w => w.PasifMi == false).SingleOrDefault();

                    if (herAyEnFazlaHaftaIci)
                    {
                        model.AddConstraint(
                              Expression.Sum(data.EczaneNobetTarihAralik
                                                .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                         && e.NobetGorevTipId == 1//eczaneNobetGrup.NobetGorevTipId
                                                         && haftaIciGunleri.Select(s => s.TakvimId).Contains(e.TakvimId))
                                                .Select(m => x[m])) <= haftaIciOrtamalaNobetSayisi,
                                                $"her eczaneye bir ayda en fazla nobet grubunun hafta ici ortalamasi kadar nobet yazilmali, {eczaneNobetGrup}");                        
                    }
                    #endregion

                    #region Her ay en fazla görev

                    var herAyEnFazlaGorev = data.Kisitlar
                                    .Where(s => s.KisitAdi == "herAyEnFazlaGorev"
                                             && s.NobetUstGrupId == data.NobetUstGrupId)
                                    .Select(w => w.PasifMi == false).SingleOrDefault();

                    if (herAyEnFazlaGorev)
                    {
                        model.AddConstraint(
                          Expression.Sum(data.EczaneNobetTarihAralik
                                            .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                     && e.NobetGorevTipId == 1)
                                            .Select(m => x[m])) <= herAyEnFazlaGorevSayisi,
                                            $"her_eczaneye_bir_ayda_en_fazla_bir_nobet_yazilmali, {eczaneNobetGrup.Id}");
                    }

                    #endregion

                    #region Bayram toplam en fazla ve farklı tür bayram

                    if (bayramlar.Count() > 0)
                    {
                        var bayramToplamEnFazla = data.Kisitlar
                                                    .Where(s => s.KisitAdi == "bayramToplamEnFazla"
                                                             && s.NobetUstGrupId == data.NobetUstGrupId)
                                                    .Select(w => w.PasifMi == false).SingleOrDefault();

                        var bayramNobetleri = data.EczaneNobetSonuclar
                                .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id
                                         && w.NobetGunKuralId > 7
                                         && w.Tarih >= data.NobetUstGrupBaslangicTarihi)
                                .Select(s => new { s.TakvimId, s.NobetGunKuralId }).ToList();

                        var bayramNobetleriAnahtarli = data.EczaneNobetSonuclar
                                .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id
                                         && w.NobetGunKuralId > 7)
                                .Select(s => new { s.TakvimId, s.NobetGunKuralId }).ToList();

                        var toplamBayramNobetSayisi = bayramNobetleri.Count();

                        //buraya bak.24/04/18 22.14 (haftaIciGunlerKumulatifToplamMaxHedef'teki gibi uyarla)
                        if (bayramToplamEnFazla)
                        {
                            var bayramGunKuralIstatistikler = data.TakvimNobetGrupGunDegerIstatistikler
                                                            .Where(w => w.NobetGrupId == eczaneNobetGrup.NobetGrupId
                                                                      && w.NobetGunKuralId > 7
                                                                     //&& bayramlar.Select(s => s.NobetGunKuralId).Distinct().Contains(w.NobetGunKuralId)
                                                                     ).ToList();

                            var bayramNobetleriGrupOrtalamasi = data.EczaneNobetSonuclar
                                    .Where(w => w.NobetGrupId == eczaneNobetGrup.NobetGrupId
                                             && w.NobetGunKuralId > 7
                                             && w.Tarih >= data.NobetUstGrupBaslangicTarihi)
                                    .Select(s => new { s.TakvimId, s.NobetGunKuralId }).ToList();

                            var toplamBayramGrupToplamNobetSayisi = bayramNobetleriGrupOrtalamasi.Count();

                            var kumulatifBayramSayisi = bayramGunKuralIstatistikler.Sum(x => x.GunSayisi);

                            var yillikOrtalamaGunKuralSayisi = Math.Ceiling((double)kumulatifBayramSayisi / gruptakiNobetciSayisi);

                            if (kumulatifBayramSayisi == gruptakiNobetciSayisi)
                            {
                                //yillikOrtalamaGunKuralSayisi++;
                            }

                            if (toplamBayramGrupToplamNobetSayisi == gruptakiNobetciSayisi)
                            {
                                //yillikOrtalamaGunKuralSayisi++;
                            }

                            if (yillikOrtalamaGunKuralSayisi < 1) yillikOrtalamaGunKuralSayisi = 0;

                            var bayramToplamMaxHedefSTD = (int)data.Kisitlar
                                                        .Where(s => s.KisitAdi == "bayramToplamEnFazla"
                                                                 && s.NobetUstGrupId == data.NobetUstGrupId)
                                                        .Select(w => w.SagTarafDegeri).SingleOrDefault();

                            if (toplamBayramNobetSayisi > yillikOrtalamaGunKuralSayisi)
                            {
                                yillikOrtalamaGunKuralSayisi = toplamBayramNobetSayisi;
                            }

                            if (bayramToplamMaxHedefSTD > 0)
                            {
                                yillikOrtalamaGunKuralSayisi = bayramToplamMaxHedefSTD;
                            }

                            var sonBayram = bayramNobetleriAnahtarli.LastOrDefault();

                            if (sonBayram != null)
                            {
                                var sonBayramTuru = bayramNobetleriAnahtarli.LastOrDefault().NobetGunKuralId;

                                var bayramPespeseFarkliTur = data.Kisitlar
                                                   .Where(s => s.KisitAdi == "bayramPespeseFarkliTur"
                                                            && s.NobetUstGrupId == data.NobetUstGrupId)
                                                   .Select(w => w.PasifMi == false).SingleOrDefault();

                                if (bayramlar.Select(s => s.NobetGunKuralId).Contains(sonBayramTuru) && bayramPespeseFarkliTur)
                                {
                                    foreach (var gun in bayramlar.Where(w => w.NobetGunKuralId == sonBayramTuru))
                                    {
                                        model.AddConstraint(
                                          Expression.Sum(data.EczaneNobetTarihAralik
                                                        .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                                 && e.TakvimId == gun.TakvimId)
                                                        .Select(m => x[m])) == 0,
                                                        $"son_yazilan_bayram_oncekinden_farkli_tur_olsun, {eczaneNobetGrup}");
                                    }
                                }
                                else
                                {
                                    foreach (var gun in bayramlar)
                                    {
                                        model.AddConstraint(
                                          Expression.Sum(data.EczaneNobetTarihAralik
                                                        .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                                 && e.TakvimId == gun.TakvimId)
                                                        .Select(m => x[m])) + toplamBayramNobetSayisi <= yillikOrtalamaGunKuralSayisi,
                                                        $"her_eczane_pespese_farkli_tur_bayram_yazilsin, {eczaneNobetGrup}");
                                    }
                                }

                            }

                            //model.AddConstraint(
                            //  Expression.Sum(data.EczaneNobetTarihAralik
                            //                .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                            //                         && e.GunDegerId > 7)
                            //                .Select(m => x[m])) + toplamBayramNobetSayisi <= toplamBayramMax,
                            //                $"her eczaneye bir ayda nobet grubunun hedefi kadar toplam bayram nobeti yazilmali, {eczaneNobetGrup}");
                        }
                    }
                    #endregion                    

                    #endregion

                    #region pasif kısıtlar

                    #region Bayram toplam en az

                    if (bayramlar.Count() > 0)
                    {
                        var bayramNobetleri = data.EczaneNobetSonuclar
                                .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id
                                         && w.NobetGunKuralId > 7
                                         && w.Tarih >= data.NobetUstGrupBaslangicTarihi)
                                .Select(s => new { s.TakvimId, s.NobetGunKuralId }).ToList();

                        var bayramNobetleriAnahtarli = data.EczaneNobetSonuclar
                                .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id
                                         && w.NobetGunKuralId > 7)
                                .Select(s => new { s.TakvimId, s.NobetGunKuralId }).ToList();

                        var toplamBayramNobetSayisi = bayramNobetleri.Count();

                        var bayramToplamMinHedef = data.Kisitlar
                                                    .Where(s => s.KisitAdi == "bayramToplamMinHedef"
                                                             && s.NobetUstGrupId == data.NobetUstGrupId)
                                                    .Select(w => w.PasifMi == false).SingleOrDefault();
                        if (bayramToplamMinHedef)
                        {
                            var hedef = data.EczaneKumulatifHedefler.Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id).SingleOrDefault();
                            var toplamBayramMin = hedef.ToplamBayram - 1;
                            if (toplamBayramMin < 1) toplamBayramMin = 0;

                            var bayramToplamMinHedefSTD = (int)data.Kisitlar
                                                        .Where(s => s.KisitAdi == "bayramToplamMinHedef"
                                                                 && s.NobetUstGrupId == data.NobetUstGrupId)
                                                        .Select(w => w.SagTarafDegeri).SingleOrDefault();

                            if (bayramToplamMinHedefSTD > 0)
                            {
                                toplamBayramMin = bayramToplamMinHedefSTD;
                            }

                            model.AddConstraint(
                              Expression.Sum(data.EczaneNobetTarihAralik
                                            .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                    && e.NobetGunKuralId > 7)
                                            .Select(m => x[m])) + toplamBayramNobetSayisi >= toplamBayramMin,
                                            $"her eczaneye bir ayda nobet grubunun hedefi kadar toplam bayram nobeti yazilmali, {eczaneNobetGrup}");
                        }
                    }
                    #endregion

                    #region Hafta içi toplam max hedefler

                    var haftaIciToplamMaxHedef = data.Kisitlar
                                                .Where(s => s.KisitAdi == "haftaIciToplamMaxHedef"
                                                         && s.NobetUstGrupId == data.NobetUstGrupId)
                                                .Select(w => w.PasifMi == false).SingleOrDefault();

                    var haftaIciToplamNobetSayisi = data.EczaneNobetSonuclar
                         .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id
                                  && w.NobetGorevTipId == 1//eczaneNobetGrup.NobetGorevTipId
                                  && (w.NobetGunKuralId >= 2 && w.NobetGunKuralId <= 7))
                         .Select(s => s.TakvimId).Count();

                    if (haftaIciToplamMaxHedef)
                    {
                        var haftaIciDurum = nobetGunKuralDetaylar.Where(w => w.GunKuralId == 0).FirstOrDefault();

                        var Std = haftaIciDurum.KuralSTD;

                        //kontrol edilecek eczane nöbet grup id'ler
                        var kontrolListesi = new int[] { 135, 146 };

                        if (kontrolListesi.Contains(eczaneNobetGrup.Id))
                        {
                        }

                        if (!haftaIciDurum.FazlaNobetTutacakEczaneler.Contains(eczaneNobetGrup.Id) && haftaIciDurum.FazlaNobetTutacakEczaneler.Count > 0)
                        {
                            Std = haftaIciDurum.KuralSTD - 1;
                        }

                        var haftaIciToplamMaxHedefStd = data.Kisitlar
                            .Where(s => s.KisitAdi == "haftaIciToplamMaxHedef"
                                     && s.NobetUstGrupId == data.NobetUstGrupId)
                            .Select(w => w.SagTarafDegeri).SingleOrDefault();

                        if (haftaIciToplamMaxHedefStd > 0) Std = haftaIciToplamMaxHedefStd;

                        if (haftaIciToplamNobetSayisi <= Std) //&& !haftaIciDurum.FazlaNobetTutacakEczaneler.Contains(eczaneNobetGrup.Id)
                        {
                            //if (data.CalismaSayisi < 7)
                            //{
                            model.AddConstraint(
                                 Expression.Sum(data.EczaneNobetTarihAralik
                                           .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                   && e.NobetGorevTipId == 1//eczaneNobetGrup.NobetGorevTipId
                                                   && haftaIciGunleri.Select(s => s.TakvimId).Contains(e.TakvimId))
                                           .Select(m => x[m])) + haftaIciToplamNobetSayisi <= Std,
                                           $"her eczaneye bir ayda nobet grubunun hedefi kadar toplam hafta içi nobeti yazilmali, {eczaneNobetGrup}");
                            //}
                            //else
                            //{
                            //    model.AddConstraint(
                            //     Expression.Sum(from i in data.EczaneNobetTarihAralik
                            //                    from j in data.EczaneNobetGruplar
                            //                    where i.EczaneNobetGrupId == j.Id
                            //                          && i.EczaneNobetGrupId == hedef.EczaneNobetGrupId
                            //                          && i.NobetGorevTipId == hedef.NobetGorevTipId
                            //                          && tarihlerHaftaIci.Select(s => s.TakvimId).Contains(i.TakvimId)
                            //                    select (x[i] + _hHiciNS[j] - _hHiciPS[j])) + haftaIciToplamNobetSayisi == Std,
                            //                     $"her eczaneye bir ayda nöbet grubunun {1} hedefi kadar toplam hafta içi nobeti yazılmalı, {haftaIciToplam}");
                            //}
                        }
                        else
                        {
                            model.AddConstraint(
                                 Expression.Sum(from i in data.EczaneNobetTarihAralik
                                                where i.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                      && i.NobetGorevTipId == 1//eczaneNobetGrup.NobetGorevTipId
                                                      && haftaIciGunleri.Select(s => s.TakvimId).Contains(i.TakvimId)
                                                select (x[i])) == 0,
                                                 $"her eczaneye bir ayda nöbet grubunun {1} hedefi kadar toplam hafta içi nobeti yazılmalı, {eczaneNobetGrup.Id}");
                        }
                    }
                    #endregion

                    #region Hafta içi toplam min hedefler

                    var haftaIciToplamMinHedef = data.Kisitlar
                                        .Where(s => s.KisitAdi == "haftaIciToplamMinHedef"
                                                 && s.NobetUstGrupId == data.NobetUstGrupId)
                                        .Select(w => w.PasifMi == false).SingleOrDefault();

                    if (haftaIciToplamMinHedef)
                    {
                        var hedef = data.EczaneKumulatifHedefler.Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id).SingleOrDefault();
                        var haftaIciMinToplam = hedef.ToplamHaftaIci - 1;

                        var haftaIciToplamMinHedefStd = (int)data.Kisitlar
                              .Where(s => s.KisitAdi == "haftaIciToplamMinHedef"
                                       && s.NobetUstGrupId == data.NobetUstGrupId)
                              .Select(w => w.SagTarafDegeri).SingleOrDefault();

                        if (haftaIciToplamMinHedefStd > 0) haftaIciMinToplam = haftaIciToplamMinHedefStd;

                        model.AddConstraint(
                          Expression.Sum(data.EczaneNobetTarihAralik
                                        .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                && haftaIciGunleri.Select(s => s.TakvimId).Contains(e.TakvimId)
                                                )
                                        .Select(m => x[m])) + haftaIciToplamNobetSayisi >= haftaIciMinToplam,
                                        $"her eczaneye bir ayda nobet grubunun hedefi kadar toplam hafta içi nobeti yazilmali, {hedef}");
                    }
                    #endregion

                    #region Her ay en az 1 görev

                    var herAyEnaz1Gorev = data.Kisitlar
                                    .Where(s => s.KisitAdi == "herAyEnaz1Gorev"
                                             && s.NobetUstGrupId == data.NobetUstGrupId)
                                    .Select(w => w.PasifMi == false).SingleOrDefault();

                    if (herAyEnaz1Gorev && gruptakiNobetciSayisi < tarihler.Count)
                    {
                        var herAyEnaz1GorevSTD = data.Kisitlar
                                                .Where(s => s.KisitAdi == "herAyEnaz1Gorev"
                                                         && s.NobetUstGrupId == data.NobetUstGrupId)
                                                .Select(w => w.SagTarafDegeri).SingleOrDefault();

                        model.AddConstraint(
                              Expression.Sum(data.EczaneNobetTarihAralik
                                                .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                         && e.NobetGorevTipId == 1//eczaneNobetGrup.NobetGorevTipId
                                                         )
                                                .Select(m => x[m])) >= herAyEnaz1GorevSTD,
                                                $"her eczaneye bir ayda en az nobet grubunun hedefinden bir eksik nobet yazilmali, {eczaneNobetGrup}");
                    }
                    #endregion

                    #region Farklı aylar peşpeşe görev yazılmasın (Pazar peşpeşe)

                    var pazarPespeseGorev0 = data.Kisitlar
                                            .Where(s => s.KisitAdi == "pazarPespeseGorev0"
                                                     && s.NobetUstGrupId == data.NobetUstGrupId)
                                            .Select(w => w.PasifMi == false).SingleOrDefault();

                    if (pazarPespeseGorev0)
                    {
                        var enSonPazarTuttuguNobetAyi = data.EczaneNobetSonuclar
                            .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id
                                     && w.NobetGunKuralId == 1)
                            .Select(s => s.Tarih.Month).OrderByDescending(o => o).FirstOrDefault();

                        var pazarNobetiYazilabilecekIlkAy = Math.Ceiling((double)gruptakiNobetciSayisi / 5) - 1;
                        var pazarNobetiYazilabilecekSonAy = Math.Ceiling((double)gruptakiNobetciSayisi / 4) + 1;//+2

                        var yazilabilecekIlkTarih = enSonPazarTuttuguNobetAyi + pazarNobetiYazilabilecekIlkAy;
                        var yazilabilecekSonTarih = enSonPazarTuttuguNobetAyi + pazarNobetiYazilabilecekSonAy;

                        //if (data.CalismaSayisi == 5) yazilabilecekSonTarih = yazilabilecekSonTarih++;

                        var kontrolListesi = new int[] { 412, 326 };
                        if (kontrolListesi.Contains(eczaneNobetGrup.Id))
                        {
                        }

                        if (enSonPazarTuttuguNobetAyi > 0)
                        {
                            foreach (var g in tarihler.Where(w => !(w.Ay >= yazilabilecekIlkTarih && w.Ay <= yazilabilecekSonTarih)
                                                                            && w.NobetGunKuralId == 1))
                            {
                                model.AddConstraint(
                                  Expression.Sum(data.EczaneNobetTarihAralik
                                                   .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                               && e.TakvimId == g.TakvimId
                                                               && e.NobetGrupId == nobetGrup.Id
                                                               )
                                                   .Select(m => x[m])) == 0,
                                                   $"eczanelere_farkli_aylarda_pazar_gunleri_pespese_nobet_yazilmasin, {eczaneNobetGrup.Id}");
                            }

                            var periyottakiNobetSayisi = data.EczaneNobetSonuclar
                                       .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                && w.NobetGorevTipId == 1//eczaneNobetGrup.NobetGorevTipId
                                                && (w.Tarih.Month >= yazilabilecekIlkTarih + 1 && w.Tarih.Month <= yazilabilecekSonTarih)
                                                && w.NobetGunKuralId == 1)
                                       .Select(s => s.TakvimId).Count();

                            //periyodu başladıktan 1 ay sonra hala pazar nöbeti yazılmamışsa yazılsın.
                            if (enSonPazarTuttuguNobetAyi < data.Ay - yazilabilecekIlkTarih //- 1
                                && periyottakiNobetSayisi == 0
                                )
                            {
                                model.AddConstraint(
                                  Expression.Sum(data.EczaneNobetTarihAralik
                                                   .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                               && e.NobetGorevTipId == 1//eczaneNobetGrup.NobetGorevTipId
                                                               && pazarGunleri.Select(s => s.TakvimId).Contains(e.TakvimId)
                                                               )
                                                   .Select(m => x[m])) == 1,
                                                   $"eczanelere_farkli_aylarda_pazar_gunu_pespese_nobet_yazilmasin, {eczaneNobetGrup}");
                            }
                        }
                    }
                    #endregion

                    #region Farklı aylar peşpeşe görev yazılmasın (Pazar peşpeşe en fazla)

                    var pazarPespeseGorevEnFazla = data.Kisitlar
                                            .Where(s => s.KisitAdi == "pazarPespeseGorevEnFazla"
                                                     && s.NobetUstGrupId == data.NobetUstGrupId)
                                            .Select(w => w.PasifMi == false).SingleOrDefault();

                    if (pazarPespeseGorevEnFazla)
                    {
                        var enSonPazarTuttuguTarih1 = data.EczaneNobetSonuclar
                            .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id
                                     && w.NobetGunKuralId == 1)
                            .Select(s => s.TakvimId).OrderByDescending(o => o).FirstOrDefault();

                        //var enSonPazarTuttuguNobetAyi = enSonPazarTuttuguTarih1.Month;

                        //if (data.CalismaSayisi == 5) yazilabilecekSonTarih = yazilabilecekSonTarih++;

                        var kontrolListesi = new int[] { 154 };
                        if (kontrolListesi.Contains(eczaneNobetGrup.Id))
                        {
                        }

                        if (enSonPazarTuttuguTarih1 > 0) //enSonPazarTuttuguNobetAyi
                        {
                            var enSonPazarTuttuguTarih = data.EczaneNobetSonuclar
                            .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id
                                     && w.NobetGunKuralId == 1)
                            .Select(s => new { s.Tarih, s.TakvimId }).OrderByDescending(o => o.Tarih).FirstOrDefault();

                            //var pazarNobetiYazilabilecekIlkAy = (int)Math.Ceiling((double)gruptakiNobetciSayisi / 5) - 1;
                            var pazarNobetiYazilabilecekSonAy = (int)Math.Ceiling((double)gruptakiNobetciSayisi / 4) + 1;

                            //var yazilabilecekIlkTarih = enSonPazarTuttuguTarih.Tarih.AddMonths(pazarNobetiYazilabilecekIlkAy);
                            var yazilabilecekSonTarih = enSonPazarTuttuguTarih.Tarih.AddMonths(pazarNobetiYazilabilecekSonAy);

                            var tarihlerGruplu = pazarGunleri
                                .GroupBy(g => new { g.Yil, g.Ay })
                                .Select(s => new
                                {
                                    s.Key.Yil,
                                    s.Key.Ay,
                                    IlkTarih = s.Min(m => m.Tarih),
                                    SonTarih = s.Max(m => m.Tarih)
                                }).ToList();

                            var tarihAralik = (from t1 in pazarGunleri
                                               from t2 in tarihlerGruplu
                                               where t1.Yil == t2.Yil
                                               && t1.Yil == t2.Yil
                                               //&& !(t2.IlkTarih > yazilabilecekIlkTarih && t2.SonTarih <= yazilabilecekSonTarih)
                                               && t2.SonTarih > yazilabilecekSonTarih
                                               select new { t1.TakvimId, t1.Tarih, t2.IlkTarih, t2.SonTarih }).ToList();

                            foreach (var g in tarihAralik)
                            {
                                model.AddConstraint(
                                  Expression.Sum(data.EczaneNobetTarihAralik
                                                   .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                               && e.TakvimId == g.TakvimId
                                                               && e.NobetGrupId == nobetGrup.Id
                                                               )
                                                   .Select(m => x[m])) == 0,
                                                   $"eczanelere_farkli_aylarda_pazar_gunu_pespese_nobet_yazilmasin, {eczaneNobetGrup.Id}");
                            }

                            //var tarihler = data.TarihAraligi
                            //    .Where(w => w.NobetGunKuralId == 1)
                            //    .Select(s => s.TakvimId).ToList();

                            //var periyottakiNobetSayisi = data.EczaneNobetSonuclar
                            //           .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id
                            //                    && w.NobetGorevTipId == 1//eczaneNobetGrup.NobetGorevTipId
                            //                    && (w.Tarih >= yazilabilecekIlkTarih.AddMonths(1) && w.Tarih <= yazilabilecekSonTarih)
                            //                    && w.NobetGunKuralId == 1)
                            //           .Select(s => s.TakvimId).Count();

                            //var nobetYazilanTarih = new DateTime(data.Yil, data.Ay, 1);

                            //var ilkTarihKontrol = nobetYazilanTarih.AddMonths(-pazarNobetiYazilabilecekIlkAy - 2);
                            ////periyodu başladıktan 1 ay sonra hala pazar nöbeti yazılmamışsa yazılsın.
                            //if (enSonPazarTuttuguTarih.Tarih < ilkTarihKontrol //data.Ay - yazilabilecekIlkTarih //- 1
                            //    && periyottakiNobetSayisi == 0
                            //    )
                            //{
                            //    model.AddConstraint(
                            //      Expression.Sum(data.EczaneNobetTarihAralik
                            //                       .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                            //                                   && e.NobetGorevTipId == 1//eczaneNobetGrup.NobetGorevTipId
                            //                                   && tarihler.Contains(e.TakvimId)
                            //                                   )
                            //                       .Select(m => x[m])) == 1,
                            //                       $"eczanelere_farkli_aylarda_pazar_gunu_pespese_nobet_yazilmasin, {eczaneNobetGrup}");
                            //}
                        }
                    }
                    #endregion

                    #region Toplam cuma cumartesi max hedefler

                    var toplamCumaCumartesiMaxHedef = data.Kisitlar
                                                .Where(s => s.KisitAdi == "toplamCumaCumartesiMaxHedef"
                                                         && s.NobetUstGrupId == data.NobetUstGrupId)
                                                .Select(w => w.PasifMi == false).SingleOrDefault();

                    if (toplamCumaCumartesiMaxHedef)
                    {
                        var hedef = data.EczaneKumulatifHedefler.Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id).SingleOrDefault();
                        var cumaCumartesiToplam = hedef.ToplamCumaCumartesi;

                        if (data.CalismaSayisi == 2 || data.CalismaSayisi == 4)
                        {
                            cumaCumartesiToplam = cumaCumartesiToplam++;
                        }

                        model.AddConstraint(
                          Expression.Sum(data.EczaneNobetTarihAralik
                                        .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                && (e.NobetGunKuralId >= 6 && e.NobetGunKuralId <= 7))
                                        .Select(m => x[m])) <= cumaCumartesiToplam,
                                        $"her eczaneye bir ayda nobet grubunun hedefi kadar toplam hafta içi nobeti yazilmali, {eczaneNobetGrup}");
                    }
                    #endregion

                    #region Toplam cuma cumartesi min hedefler

                    var toplamCumaCumartesiMinHedef = data.Kisitlar
                                        .Where(s => s.KisitAdi == "toplamCumaCumartesiMinHedef"
                                                 && s.NobetUstGrupId == data.NobetUstGrupId)
                                        .Select(w => w.PasifMi == false).SingleOrDefault();

                    if (toplamCumaCumartesiMinHedef)
                    {
                        var hedef = data.EczaneKumulatifHedefler.Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id).SingleOrDefault();
                        var cumaCumartesiToplam = hedef.ToplamCumaCumartesi - 1;

                        if (data.CalismaSayisi == 2 || data.CalismaSayisi == 4)
                        {
                            cumaCumartesiToplam = cumaCumartesiToplam--;
                        }

                        model.AddConstraint(
                          Expression.Sum(data.EczaneNobetTarihAralik
                                        .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                && (e.NobetGunKuralId >= 6 && e.NobetGunKuralId <= 7))
                                        .Select(m => x[m])) >= cumaCumartesiToplam,
                                        $"her eczaneye bir ayda nobet grubunun hedefi kadar toplam hafta içi nobeti yazilmali, {eczaneNobetGrup}");
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

                        // == 8
                        if (data.CalismaSayisi == 2) toplamCumaMaxHedefSTD = toplamCumaMaxHedefSTD++;

                        var toplamCumaSayisi = data.EczaneNobetSonuclar
                            .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id
                                     && w.NobetGunKuralId == 6)
                            .Select(s => s.TakvimId).Count();

                        foreach (var g in tarihler.Where(w => w.NobetGunKuralId == 6))
                        {
                            model.AddConstraint(
                              Expression.Sum(data.EczaneNobetTarihAralik
                                               .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                           && e.TakvimId == g.TakvimId
                                                           )
                                               .Select(m => x[m])) + toplamCumaSayisi <= toplamCumaMaxHedefSTD,
                                               $"eczanelere_cuma_gunu_encok_yilda_3_gorev_yaz, {eczaneNobetGrup}");
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

                        //if (data.CalismaSayisi == 8) toplamCumartesiMaxHedefSTD = toplamCumartesiMaxHedefSTD++;

                        var toplamCumartesiSayisi = data.EczaneNobetSonuclar
                            .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id
                                     && w.NobetGunKuralId == 7)
                            .Select(s => s.TakvimId).Count();

                        foreach (var g in cumartesiGunleri)
                        {
                            model.AddConstraint(
                              Expression.Sum(data.EczaneNobetTarihAralik
                                               .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                           && e.TakvimId == g.TakvimId)
                                               .Select(m => x[m])) + toplamCumartesiSayisi <= toplamCumartesiMaxHedefSTD,
                                               $"eczanelere_cumartesi_gunu_encok_yilda_3_gorev_yaz, {eczaneNobetGrup}");
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
                        var hedef = data.EczaneKumulatifHedefler.Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id).SingleOrDefault();
                        var maxToplam = hedef.Toplam;

                        if (data.Ay == 1 && fazlaYazilacakEczaneSayisi > 0)
                        {
                            if (!fazlaYazilacakEczaneler.Contains(eczaneNobetGrup))
                            {
                                maxToplam = hedef.Toplam - 1;
                            }
                        }

                        if (data.CalismaSayisi == 2 || data.CalismaSayisi == 4 || data.CalismaSayisi > 6)
                        {
                            //maxToplam++; //hedef.Toplam <= 2 ? hedef.Toplam + 1 : hedef.Toplam + 2;
                        }

                        var toplamNobetSayisi = data.EczaneNobetSonuclar
                           .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id
                                    //&& w.NobetGunKuralId == 7
                                    )
                           .Select(s => s.TakvimId).Count();

                        var toplamMaxHedefStd = data.Kisitlar
                                                            .Where(s => s.KisitAdi == "toplamMaxHedef"
                                                                     && s.NobetUstGrupId == data.NobetUstGrupId)
                                                            .Select(w => w.SagTarafDegeri).SingleOrDefault();

                        if (toplamMaxHedefStd > 0)
                        {
                            maxToplam = (int)toplamMaxHedefStd;
                        }

                        model.AddConstraint(
                         Expression.Sum(data.EczaneNobetTarihAralik
                                          .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                   && e.NobetGorevTipId == 1//hedef.NobetGorevTipId
                                                   )
                                          .Select(m => x[m])) + toplamNobetSayisi <= maxToplam,
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
                        var hedef = data.EczaneKumulatifHedefler.Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id).SingleOrDefault();
                        var minToplam = hedef.Toplam - 1;

                        if (data.CalismaSayisi == 3 || data.CalismaSayisi == 4)
                        {
                            //minToplam = hedef.Toplam - 1 > 1 ? 1 : 0;
                        }
                        var toplamNobetSayisi = data.EczaneNobetSonuclar
                           .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id)
                           .Select(s => s.TakvimId).Count();

                        var toplamMinHedefStd = data.Kisitlar
                                    .Where(s => s.KisitAdi == "toplamMinHedef"
                                             && s.NobetUstGrupId == data.NobetUstGrupId)
                                    .Select(w => w.SagTarafDegeri).SingleOrDefault();

                        if (toplamMinHedefStd > 0)
                        {
                            minToplam = (int)toplamMinHedefStd;
                        }

                        model.AddConstraint(
                          Expression.Sum(data.EczaneNobetTarihAralik
                                            .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                     && e.NobetGorevTipId == 1//hedef.NobetGorevTipId
                                                     )
                                            .Select(m => x[m])) + toplamNobetSayisi >= minToplam,
                                            $"her_eczaneye_ayda_en_az_nobet_grubunun_hedefinden_1_eksik_nobet_yazilmali, {hedef}");
                    }
                    #endregion

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
                    foreach (var eczaneGrupTanim in data.EczaneGrupTanimlar)
                    {
                        if (eczaneGrupTanim.Id == 143)//g.Adi.Contains("EŞ-SEMİH-SERRA BALTA"))
                        {
                        }

                        var eczaneGruplar = data.EczaneGruplar
                                                .Where(x => x.EczaneGrupTanimId == eczaneGrupTanim.Id)
                                                .Select(s => new { s.EczaneId, s.EczaneAdi, s.EczaneGrupTanimTipId }).Distinct().ToList();

                        //çözülen dönemdeki eczanelerin mevcut nöbetleri
                        var gruptakiEczanelerinNobetTarihleri = data.EczaneGrupNobetSonuclar
                            .Where(w => eczaneGruplar.Select(s => s.EczaneId).Contains(w.EczaneId)).ToList();

                        //değişkendeki eczaneler
                        var eczaneGruplar2 = data.EczaneGruplar
                                                .Where(x => x.EczaneGrupTanimId == eczaneGrupTanim.Id
                                                         && x.NobetGrupId == nobetGrup.Id)
                                                .Select(s => new { s.EczaneId, s.EczaneAdi, s.EczaneGrupTanimTipId }).Distinct().ToList();

                        var ardisikNobetSayisi = eczaneGrupTanim.ArdisikNobetSayisi;

                        var tarihler2 = tarihler.Take(tarihler.Count() - ardisikNobetSayisi).ToList();

                        //peşpeşe gelmesin
                        if (eczaneGruplar2.Count > 0)
                        {
                            //(ardisikNobetSayisi=0 ise bu gereksiz bir kısıt. çünkü aynı grupta zaten aynı gün birden fazla nöbetçi bulunmuyor.)
                            if (ardisikNobetSayisi > 1)
                            {//aynı gruptaki eşler 
                                foreach (var tarih in tarihler2)
                                {
                                    //model.AddConstraint(Expression.Sum(data.EczaneNobetTarihAralik
                                    //                        .Where(e => eczaneGruplar2.Select(s => s.EczaneId).Contains(e.EczaneId)
                                    //                                && (e.Gun >= tarih.Gun && e.Gun <= tarih.Gun + ardisikNobetSayisi))
                                    //                        .Select(m => x[m])) <= 1,
                                    //                            $"eczaneGrupTanimda_ayni_gruptaki_eczaneler_birlikte_nobet_tutmasin, {tarih.TakvimId}");
                                }
                            }

                            if (gruptakiEczanelerinNobetTarihleri.Count > 0)
                            {//farklı gruplardaki eşler
                                if (ardisikNobetSayisi > 0)
                                {
                                    foreach (var tarih in gruptakiEczanelerinNobetTarihleri)
                                    {
                                        //model.AddConstraint(Expression.Sum(data.EczaneNobetTarihAralik
                                        //                        .Where(e => eczaneGruplar2.Select(s => s.EczaneId).Contains(e.EczaneId)
                                        //                                && (e.Gun >= tarih.Tarih.Day - ardisikNobetSayisi && e.Gun <= tarih.Tarih.Day + ardisikNobetSayisi))
                                        //                        .Select(m => x[m])) == 0,
                                        //                            $"eczaneGrupTanimdaki_eczaneler_birlikte_nobet_tutmasin, {tarih.TakvimId}");
                                    }
                                }
                                else
                                {
                                    foreach (var tarih in gruptakiEczanelerinNobetTarihleri)
                                    {
                                        model.AddConstraint(Expression.Sum(data.EczaneNobetTarihAralik
                                                                .Where(e => eczaneGruplar2.Select(s => s.EczaneId).Contains(e.EczaneId)
                                                                         && e.TakvimId == tarih.TakvimId)
                                                                .Select(m => x[m])) == 0,
                                                                    $"eczaneGrupTanimdaki_eczaneler_birlikte_nobet_tutmasin, {tarih.TakvimId}");
                                    }
                                }
                            }
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
                    var istekler = data.EczaneNobetIstekler.Where(x => x.NobetGrupId == nobetGrup.Id && tarihler.Select(s => s.TakvimId).Contains(x.TakvimId));

                    foreach (var f in istekler)
                    {
                        model.AddConstraint(
                                  Expression.Sum(data.EczaneNobetTarihAralik
                                                   .Where(e => e.EczaneNobetGrupId == f.EczaneNobetGrupId
                                                            && e.TakvimId == f.TakvimId)
                                                   .Select(m => x[m])) == 1,
                                                   $"istege nobet yaz, {f}");
                    }
                }

                var aa = true;
                if (aa)
                {
                    var pespeseNobetSayisi = (int)data.NobetGrupKurallar
                                .Where(s => s.NobetGrupId == nobetGrup.Id
                                         && s.NobetKuralId == 1)
                                .Select(s => s.Deger).SingleOrDefault();

                    foreach (var f in data.EczaneNobetIstekler.Where(x => x.NobetGrupId == nobetGrup.Id))
                    {
                        var sonGun = tarihler.LastOrDefault().Tarih;

                        var istekTarihi = f.Tarih;
                        var istekOncesiTarih = istekTarihi.AddDays(-pespeseNobetSayisi); //pespeseNobetSayisi

                        if (istekTarihi > sonGun)
                        {
                            var istekOncesiTarihAralik = tarihler.Where(w => w.Tarih >= istekOncesiTarih);

                            if (istekOncesiTarihAralik.Count() > 0)
                            {
                                model.AddConstraint(
                                      Expression.Sum(data.EczaneNobetTarihAralik
                                                       .Where(e => e.EczaneNobetGrupId == f.EczaneNobetGrupId
                                                                   //&& tarih.TakvimId == e.TakvimId
                                                                   && istekOncesiTarihAralik.Select(s => s.TakvimId).Contains(e.TakvimId)
                                                                   && e.NobetGorevTipId == 1)
                                                       .Select(m => x[m])) == 0,
                                                       $"eczanelere_farkli_aylarda_pespese_nobet_yazilmasin, {f}");
                            }
                        }
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
                                                   .Select(m => x[m])) == 0,
                                                   $"mazerete nobet yazma, {f}");
                    }
                }
                #endregion

                #region Bayramlarda en fazla 1 görev yazılsın

                var bayram = data.Kisitlar
                                .Where(s => s.KisitAdi == "herAyEnFazla1Bayram"
                                            && s.NobetUstGrupId == data.NobetUstGrupId)
                                .Select(w => w.PasifMi == false).SingleOrDefault();
                if (bayram)
                {
                    int pespeseNobetSayisi = (int)data.NobetGrupKurallar
                            .Where(s => s.NobetGrupId == nobetGrup.Id
                                     && s.NobetKuralId == 1)
                            .Select(s => s.Deger).SingleOrDefault();

                    //if (data.TarihAraligi.Where(w => w.NobetGunKuralId > 7).Count() > pespeseNobetSayisi)
                    //{//eğer bayram günleri peşpeşe nöbet sayısından fazlaysa bayramlarda en fazla 1 görev yazılsın
                    if (bayramlar.Count > 0)
                    {
                        foreach (var f in eczaneNobetGruplar)
                        {
                            model.AddConstraint(
                              Expression.Sum(data.EczaneNobetTarihAralik
                                               .Where(e => e.EczaneNobetGrupId == f.Id
                                                           && bayramlar.Select(s => s.TakvimId).Contains(e.TakvimId)
                                                     )
                                               .Select(m => x[m])) <= 1,
                                               $"bayram nobeti sinirla, {f}");
                        }
                    }
                }
                #endregion

                #region Alt gruplarla eşit sayıda nöbet tutulsun

                var altGruplarlaAyniGunNobetTutma = data.Kisitlar
                                                            .Where(s => s.KisitAdi == "altGruplarlaAyniGunNobetTutma"
                                                                        && s.NobetUstGrupId == data.NobetUstGrupId)
                                                            .Select(w => w.PasifMi == false).SingleOrDefault();

                var ayniGunNobetTutmasiTakipEdilecekGruplar = new List<int>
                {
                    20,//Yenişehir-1,
                    22 //Yenişehir-3 (M.Ü. Hastanesi)
                };

                //var nobetGruplar = data.NobetGruplar.Where(w => ayniGunNobetTutmasiTakipEdilecekGruplar.Contains(w.Id)).Select(s => s.Id).ToList();

                if (altGruplarlaAyniGunNobetTutma && ayniGunNobetTutmasiTakipEdilecekGruplar.Contains(nobetGrup.Id) //nobetGruplar.Count() > 0
                    )
                {
                    var yeniSehir1Ve3tekiEczaneler = data.EczaneNobetGruplar
                        .Where(w => ayniGunNobetTutmasiTakipEdilecekGruplar.Contains(w.NobetGrupId))
                        .Select(s => new
                        {
                            s.EczaneId,
                            s.EczaneAdi,
                            s.Id,
                            s.NobetGrupId,
                            s.NobetGrupAdi
                        }).ToList();

                    var altGruplarlaAyniGunNobetTutmaStd = data.Kisitlar
                                                                       .Where(s => s.KisitAdi == "altGruplarlaAyniGunNobetTutma"
                                                                                && s.NobetUstGrupId == data.NobetUstGrupId)
                                                                       .Select(w => w.SagTarafDegeri).SingleOrDefault();

                    var altGrubuOlanNobetGruplar = new List<int>
                    {
                         21//Yenişehir-2
                    };

                    var nobetAltGruplar = data.EczaneGrupNobetSonuclarTumu
                           .Where(w => altGrubuOlanNobetGruplar.Contains(w.NobetGrupId))
                           .Select(s => s.NobetAltGrupId).Distinct()
                           .OrderBy(o => o).ToList();

                    foreach (var eczaneNobetGrup in yeniSehir1Ve3tekiEczaneler)
                    {
                        var kontrolEdilecekEczaneler = new string[] { "NURLU" };

                        if (kontrolEdilecekEczaneler.Contains(eczaneNobetGrup.EczaneAdi))
                        {
                        }

                        var bakilanEczaneninSonuclari = data.EczaneNobetSonuclar
                            .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id
                                     && w.Tarih >= data.NobetUstGrupBaslangicTarihi
                                     )
                            .Select(s => new
                            {
                                s.TakvimId,
                                s.EczaneId,
                                s.EczaneAdi,
                                s.NobetGrupId,
                                s.NobetGrupAdi,
                                s.Tarih,
                                s.EczaneNobetGrupId
                            }).ToList();

                        var bakilanEczaneninToplamNobetSayisi = bakilanEczaneninSonuclari.Count();

                        var bolum = Math.Round((double)bakilanEczaneninToplamNobetSayisi / nobetAltGruplar.Count(), 0);

                        var altGrupIleTutulacakNobetSayisiUstLimiti = bolum + 1;

                        if (altGruplarlaAyniGunNobetTutmaStd > 0)
                            altGrupIleTutulacakNobetSayisiUstLimiti += altGruplarlaAyniGunNobetTutmaStd;

                        var yeniSehir2dekiEczaneninSonuclari = data.EczaneGrupNobetSonuclarTumu
                            .Where(w => altGrubuOlanNobetGruplar.Contains(w.NobetGrupId)
                                     && w.Tarih >= data.NobetUstGrupBaslangicTarihi)
                            .Select(s => new
                            {
                                s.TakvimId,
                                s.NobetAltGrupId,
                                s.EczaneId,
                                s.EczaneAdi,
                                s.NobetGrupId,
                                s.NobetGrupAdi,
                                s.Tarih,
                                s.EczaneNobetGrupId
                            }).ToList();

                        var altGrupluEczaneler = data.EczaneNobetGrupAltGruplar
                           .Select(s => new
                           {
                               s.NobetAltGrupId,
                               s.EczaneId,
                               s.EczaneAdi,
                               s.NobetGrupId,
                               s.NobetGrupAdi,
                               s.EczaneNobetGrupId
                           }).ToList();

                        var ayniGunTutulanNobetler = (from g1 in bakilanEczaneninSonuclari
                                                      from g2 in yeniSehir2dekiEczaneninSonuclari
                                                      where g1.TakvimId == g2.TakvimId
                                                      select new AltGrupIleAyniGunNobetDurumu
                                                      {
                                                          TakvimId = g1.TakvimId,
                                                          Tarih = g1.Tarih,
                                                          NobetAltGrupId = g2.NobetAltGrupId,
                                                          EczaneNobetGrupIdAltGrubuOlmayan = g1.EczaneNobetGrupId,
                                                          EczaneNobetGrupIdAltGruplu = g2.EczaneNobetGrupId,
                                                          EczaneIdAltGrubuOlmayan = g1.EczaneId,
                                                          EczaneIdAltGruplu = g2.EczaneId,
                                                          EczaneAdiAltGrubuOlmayan = g1.EczaneAdi,
                                                          EczaneAdiAltGruplu = g2.EczaneAdi,
                                                          NobetGrupAdiAltGrubuOlmayan = g1.NobetGrupAdi,
                                                          NobetGrupAdiAltGruplu = g2.NobetGrupAdi,
                                                          NobetGrupIdAltGrubuOlmayan = g1.NobetGrupId,
                                                          NobetGrupIdAltGruplu = g2.NobetGrupId
                                                      }).ToList();

                        if (bakilanEczaneninToplamNobetSayisi < 2)
                        {
                            //bakilanEczaneninToplamNobetSayisi = 1;
                        }

                        //var l1eczaneSayisi = data.EczaneNobetTarihAralik.Select(s => s.EczaneNobetGrupId).Distinct().OrderBy(o => o).ToList();
                        //var l2eczaneSayisi = data.EczaneNobetAltGrupTarihAralik.Select(s => s.EczaneNobetGrupId).Distinct().OrderBy(o => o).ToList();

                        #region v1

                        foreach (var altGrup in nobetAltGruplar)
                        {
                            var birlikteNobetTutmayacakEczaneler = new List<int> { eczaneNobetGrup.Id };

                            var birlikteNobetTutulmayacakAltGruptakiEczaneler = altGrupluEczaneler.Where(w => w.NobetAltGrupId == altGrup).ToList();

                            birlikteNobetTutulmayacakAltGruptakiEczaneler.ForEach(x => birlikteNobetTutmayacakEczaneler.Add(x.EczaneNobetGrupId));

                            var tarihler2 = data.TarihAraligi.Select(s => new { s.TakvimId, s.Tarih }).Distinct().ToList();

                            foreach (var g in tarihler2)
                            {
                                var kararDegiskeniNobetciler = data.EczaneNobetTarihAralik
                                                   .Where(e => birlikteNobetTutmayacakEczaneler.Contains(e.EczaneNobetGrupId)
                                                            && e.TakvimId == g.TakvimId)
                                                   .Select(m => new
                                                   {
                                                       kd = x[m],
                                                       m.Tarih,
                                                       m.TakvimId,
                                                       m.EczaneAdi,
                                                       m.NobetGrupAdi
                                                   }).ToList();

                                var kararDegiskeniAltGruplaIliskiliEczaneler = data.EczaneNobetAltGrupTarihAralik
                                                                                           .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                                                                    && e.NobetGrupAltId == altGrup
                                                                                                    && e.TakvimId == g.TakvimId).FirstOrDefault();

                                #region bakılan eczane ve ilgili alt grup aynı gün nöbetçi olursa

                                model.AddConstraint(
                                     Expression.Sum(kararDegiskeniNobetciler.Select(s => s.kd)) >= 2 - 2 * (1 - y[kararDegiskeniAltGruplaIliskiliEczaneler]),
                                                     $"alt_gruptaki_eczaneler_ile_enfazla_tutulacak_nobet_sayisi, {eczaneNobetGrup.Id}");


                                model.AddConstraint(
                                     Expression.Sum(kararDegiskeniNobetciler.Select(s => s.kd)) <= 1 + 2 * y[kararDegiskeniAltGruplaIliskiliEczaneler],
                                                     $"alt_gruptaki_eczaneler_ile_enfazla_tutulacak_nobet_sayisi, {eczaneNobetGrup.Id}");
                                #endregion
                            }

                            var kararDegiskeniAltGruplaIliskiliEczanelerLimit = data.EczaneNobetAltGrupTarihAralik
                                                                               .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                                                        && e.NobetGrupAltId == altGrup)
                                                                               .Select(m => new
                                                                               {
                                                                                   kd2 = y[m],
                                                                                   m.TakvimId,
                                                                                   m.Tarih,
                                                                                   m.NobetGrupAdi,
                                                                                   m.EczaneAdi,
                                                                                   m.NobetAltGrupAdi
                                                                               }).ToList();

                            var altGruplabirlikteTutulanNobetler = ayniGunTutulanNobetler.Where(w => w.NobetAltGrupId == altGrup).ToList();

                            var altGruplabirlikteBirlikteNobetSayisi = altGruplabirlikteTutulanNobetler.Count();

                            if (altGrupIleTutulacakNobetSayisiUstLimiti - altGruplabirlikteBirlikteNobetSayisi < 2)
                            {
                                //altGrupIleTutulacakNobetSayisiUstLimiti = 2;
                            }

                            var kararDegiskeniNobetSayisi = data.EczaneNobetTarihAralik
                                   .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id)
                                   .Select(m => new
                                   {
                                       kd = x[m],
                                       m.Tarih,
                                       m.TakvimId,
                                       m.EczaneAdi,
                                       m.NobetGrupAdi
                                   }).ToList();

                            var ayIcındekiAltGruplaBirlikteNobetSayisi = 1;

                            if (data.CalismaSayisi == 2)
                            {
                                ayIcındekiAltGruplaBirlikteNobetSayisi++;
                            }
                            else if (data.CalismaSayisi == 3)
                            {
                                ayIcındekiAltGruplaBirlikteNobetSayisi = ayIcındekiAltGruplaBirlikteNobetSayisi + 2;
                            }

                            var ayIciSinir = true;

                            if (ayIciSinir)
                            {
                                model.AddConstraint(Expression.Sum(kararDegiskeniAltGruplaIliskiliEczanelerLimit.Select(s => s.kd2)) <= ayIcındekiAltGruplaBirlikteNobetSayisi, //1
                                                 $"alt_gruptaki_eczaneler_ile_enfazla_tutulacak_nobet_sayisi2, {eczaneNobetGrup.Id}");
                            }

                            var tumZamanlar = true;

                            if (tumZamanlar)
                            {
                                //if (nobetGrup.Id == 22)                                
                                //    altGrupIleTutulacakNobetSayisiUstLimiti = 2;                                

                                model.AddConstraint(Expression.Sum(kararDegiskeniAltGruplaIliskiliEczanelerLimit.Select(s => s.kd2)) + altGruplabirlikteBirlikteNobetSayisi <= altGrupIleTutulacakNobetSayisiUstLimiti, //1
                                                   $"alt_gruptaki_eczaneler_ile_enfazla_tutulacak_nobet_sayisi2, {eczaneNobetGrup.Id}");

                                //model.AddConstraint(Expression.Sum(kararDegiskeniAltGruplaIliskiliEczanelerLimit.Select(s => s.kd2)) + altGruplabirlikteBirlikteNobetSayisi <=
                                //                                                            Math.Ceiling((herAyEnFazlaGorevSayisi + bakilanEczaneninToplamNobetSayisi) / 3) + altGruplarlaAyniGunNobetTutmaStd, //1
                                //                  $"alt_gruptaki_eczaneler_ile_enfazla_tutulacak_nobet_sayisi2, {eczaneNobetGrup.Id}");
                            }

                            //model.AddConstraint(
                            //       Expression.Sum(data.EczaneNobetTarihAralik
                            //                        .Where(e => birlikteNobetTutmayacakEczaneler.Contains(e.EczaneNobetGrupId))
                            //                        .Select(m => x[m])) <= altGrupIleTutulacakNobetSayisiUstLimiti,
                            //                        $"alt_gruptaki_eczaneler_ile_enfazla_tutulacak_nobet_sayisi, {eczaneNobetGrup.Id}");
                        }
                        #endregion

                        #region v2
                        //foreach (var eczaneNobetAltGrupTarihAralik in data.EczaneNobetAltGrupTarihAralik)
                        //{
                        //    var birlikteNobetTutmayacakEczaneler = new List<int> { eczaneNobetGrup.Id };

                        //    var birlikteNobetTutulmayacakAltGruptakiEczaneler = altGrupluEczaneler.Where(w => w.NobetAltGrupId == eczaneNobetAltGrupTarihAralik.NobetGrupAltId).ToList();

                        //    birlikteNobetTutulmayacakAltGruptakiEczaneler.ForEach(x => birlikteNobetTutmayacakEczaneler.Add(x.EczaneNobetGrupId));

                        //    var kararDegiskeniNobetciler = data.EczaneNobetTarihAralik
                        //                      .Where(e => birlikteNobetTutmayacakEczaneler.Contains(e.EczaneNobetGrupId)
                        //                               && e.TakvimId == eczaneNobetAltGrupTarihAralik.TakvimId
                        //                               )
                        //                      .Select(m => new
                        //                      {
                        //                          kd = x[m],
                        //                          m.Tarih,
                        //                          m.TakvimId,
                        //                          m.EczaneAdi,
                        //                          m.NobetGrupAdi
                        //                      }).ToList();

                        //    #region bakılan eczane ve ilgili alt grup aynı gün nöbetçi olursa

                        //    model.AddConstraint(
                        //         Expression.Sum(kararDegiskeniNobetciler.Select(s => s.kd)) >= 2 - 2 * (1 - y[eczaneNobetAltGrupTarihAralik]),
                        //                         $"alt_gruptaki_eczaneler_ile_enfazla_tutulacak_nobet_sayisi, {eczaneNobetGrup.Id}");


                        //    model.AddConstraint(
                        //         Expression.Sum(kararDegiskeniNobetciler.Select(s => s.kd)) <= 1 + 2 * y[eczaneNobetAltGrupTarihAralik],
                        //                         $"alt_gruptaki_eczaneler_ile_enfazla_tutulacak_nobet_sayisi_2, {eczaneNobetGrup.Id}");
                        //    #endregion
                        //}

                        //foreach (var altGrup in nobetAltGruplar)
                        //{
                        //    var kararDegiskeniAltGruplaIliskiliEczanelerLimit = data.EczaneNobetAltGrupTarihAralik
                        //                               .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                        //                                        && e.NobetGrupAltId == altGrup)
                        //                               .Select(m => new
                        //                               {
                        //                                   kd2 = y[m],
                        //                                   m.TakvimId,
                        //                                   m.Tarih,
                        //                                   m.NobetGrupAdi,
                        //                                   m.EczaneAdi,
                        //                                   m.NobetAltGrupAdi
                        //                               }).ToList();

                        //    var altGruplabirlikteTutulanNobetler = ayniGunTutulanNobetler.Where(w => w.NobetAltGrupId == altGrup).ToList();

                        //    var birlikteNobetSayisi = altGruplabirlikteTutulanNobetler.Count();

                        //    var ayIciSinir = false;

                        //    if (ayIciSinir)
                        //    {//ay içi
                        //        model.AddConstraint(Expression.Sum(kararDegiskeniAltGruplaIliskiliEczanelerLimit.Select(s => s.kd2)) <= 1, //1
                        //                         $"alt_gruptaki_eczaneler_ile_enfazla_tutulacak_nobet_sayisi2, {eczaneNobetGrup.Id}");
                        //    }
                        //    else
                        //    {//tüm zamanlar
                        //        model.AddConstraint(Expression.Sum(kararDegiskeniAltGruplaIliskiliEczanelerLimit.Select(s => s.kd2)) + birlikteNobetSayisi <= altGrupIleTutulacakNobetSayisiUstLimiti, //1
                        //                           $"alt_gruptaki_eczaneler_ile_enfazla_tutulacak_nobet_sayisi2, {eczaneNobetGrup.Id}");
                        //    }
                        //} 
                        #endregion

                        var siraliCoz = false;

                        #region alt gruplarla sıralı

                        if (ayniGunTutulanNobetler.Count > 0 && siraliCoz)
                        {
                            var sonNobetTarih1 = ayniGunTutulanNobetler.Max(m => m.Tarih);
                            var birlikteNobetTutulanNobetAltGrupId = (from g1 in ayniGunTutulanNobetler
                                                                      where g1.Tarih >= sonNobetTarih1
                                                                      select g1.NobetAltGrupId).ToList();
                            if (ayniGunTutulanNobetler.Count > 1)
                            {
                                var sonNobetTarih2 = ayniGunTutulanNobetler.Where(w => w.Tarih < sonNobetTarih1).Max(m => m.Tarih);

                                birlikteNobetTutulanNobetAltGrupId = (from g1 in ayniGunTutulanNobetler
                                                                      where g1.Tarih >= sonNobetTarih2
                                                                      select g1.NobetAltGrupId).ToList();
                            }

                            //daha önce aynı gün nöbet tuttuğu eczanelerin altgrupları listede sıfır yapılır
                            foreach (var nobetAltGrupId in birlikteNobetTutulanNobetAltGrupId)
                            {
                                var kontrol = false;
                                if (kontrol)
                                {
                                    kontrolEdilecekEczaneler = new string[] { "GÖNÜLCE" };

                                    if (kontrolEdilecekEczaneler.Contains(eczaneNobetGrup.EczaneAdi))
                                    {
                                    }
                                }

                                var birlikteNobetTutulmayacakAltGruptakiEczaneler = altGrupluEczaneler.Where(w => w.NobetAltGrupId == nobetAltGrupId).ToList();

                                //bakılan eczane
                                var birlikteNobetTutmayacakEczaneler = new List<int> { eczaneNobetGrup.Id };

                                //alt gruptaki eczaneler
                                birlikteNobetTutulmayacakAltGruptakiEczaneler.ForEach(x => birlikteNobetTutmayacakEczaneler.Add(x.EczaneNobetGrupId));

                                foreach (var g in data.TarihAraligi)
                                {
                                    model.AddConstraint(
                                      Expression.Sum(data.EczaneNobetTarihAralik
                                                       .Where(e => birlikteNobetTutmayacakEczaneler.Contains(e.EczaneNobetGrupId)
                                                                && e.TakvimId == g.TakvimId)
                                                       .Select(m => x[m])) <= 1,
                                                       $"alt_gruptaki_eczaneler_ile_enfazla_tutulacak_nobet_sayisi, {eczaneNobetGrup.Id}");
                                }
                            }

                            //var birlikteNobetSayisi = ayniGunTutulanNobetler.Count();

                            //model.AddConstraint(
                            //      Expression.Sum(data.EczaneNobetTarihAralik
                            //                       .Where(e => e.EczaneId == eczaneId)
                            //                       .Select(m => x[m])) + birlikteNobetSayisi <= altGruplarlaAyniGunNobetTutmaStd,
                            //                       $"ay icinde diger gruptaki eczaneler ile en fazla bir kez nobet tutulsun, {eczaneId}");
                            //}
                        }
                        #endregion
                    }
                }
                #endregion
            }

            #region Eczane grup kısıtı - çoklu çözüm için

            var eczaneGrupCokluCozum = data.Kisitlar
                                        .Where(s => s.KisitAdi == "eczaneGrup"
                                                    && s.NobetUstGrupId == data.NobetUstGrupId)
                                        .Select(w => w.PasifMi == false).SingleOrDefault();

            if (eczaneGrupCokluCozum)
            {
                foreach (var eczaneGrupTanim in data.EczaneGrupTanimlar)
                {
                    var eczaneGruplar = data.EczaneGruplar
                                            .Where(x => x.EczaneGrupTanimId == eczaneGrupTanim.Id)
                                            .Select(s => new
                                            {
                                                s.EczaneId,
                                                s.EczaneAdi,
                                                s.EczaneGrupTanimTipId
                                            }).Distinct().ToList();
                    #region kontrol
                    var kontol = false;

                    if (kontol)
                    {
                        if (eczaneGrupTanim.Id == 143)//g.Adi.Contains("EŞ-SEMİH-SERRA BALTA"))
                        {
                        }

                        var kontrolEdilecekGruptakiEczaneler = new string[] { "BAŞGÖR", "AVKAN" };

                        if (eczaneGruplar.Where(w => kontrolEdilecekGruptakiEczaneler.Contains(w.EczaneAdi)).Count() > 0)
                        {
                        }
                    } 
                    #endregion

                    //değişkendeki eczaneler
                    var eczaneGruplar2 = data.EczaneGruplar
                                            .Where(x => x.EczaneGrupTanimId == eczaneGrupTanim.Id
                                                     && data.NobetGruplar.Select(s => s.Id).Contains(x.NobetGrupId))
                                            .Select(s => new
                                            {
                                                s.EczaneId,
                                                s.EczaneAdi,
                                                s.EczaneGrupTanimTipId
                                            }).Distinct().ToList();

                    var ardisikNobetSayisi = eczaneGrupTanim.ArdisikNobetSayisi;

                    var tarihAraligi = data.TarihAraligi
                        .Select(s => new
                        {
                            s.TakvimId,
                            s.Tarih,
                            s.Gun
                        }).Distinct().ToList();

                    var tarihler = tarihAraligi.Take(tarihAraligi.Count() - ardisikNobetSayisi).ToList();

                    //peşpeşe gelmesin
                    if (eczaneGruplar2.Count > 0)
                    {
                        //ardisikNobetSayisi=0 ise; bu kısıt aynı nöbet grubundaki eş olan eczaneler için gereksizdir. 
                        //çünkü aynı grupta zaten aynı gün birden fazla nöbetçi bulunmuyor.
                        //ancak farklı gruptakiler için gereklidir.

                        if (ardisikNobetSayisi == 0)
                        {
                            foreach (var tarih in tarihler)
                            {
                                model.AddConstraint(Expression.Sum(data.EczaneNobetTarihAralik
                                                        .Where(e => eczaneGruplar2.Select(s => s.EczaneId).Contains(e.EczaneId)
                                                                && e.TakvimId == tarih.TakvimId)
                                                        .Select(m => x[m])) <= 1,
                                                            $"ayni_es_gruptaki_eczaneler_ayni_gun_birlikte_nobet_tutmasin, {tarih.TakvimId}");
                            }
                        }
                        else
                        {
                            foreach (var tarih in tarihler)
                            {
                                //model.AddConstraint(Expression.Sum(data.EczaneNobetTarihAralik
                                //                        .Where(e => eczaneGruplar2.Select(s => s.EczaneId).Contains(e.EczaneId)
                                //                                && (e.Gun >= tarih.Gun && e.Gun <= tarih.Gun + ardisikNobetSayisi))
                                //                        .Select(m => x[m])) <= 1,
                                //                            $"ayni_es_gruptaki_eczaneler_ayni_gunlerde_birlikte_nobet_tutmasin, {tarih.TakvimId}");
                            }
                        }
                    }
                }
            }
            #endregion

            #endregion

            return model;
        }

        public EczaneNobetSonucModel Solve(MersinMerkezDataModelV2 data)
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
                        var bakilanDugumSayisi = solution.NumberOfExploredNodes;
                        var kisitSayisi = model.ConstraintsCount;

                        Results = new EczaneNobetSonucModel
                        {
                            CozumSuresi = sure,
                            ObjectiveValue = objective.Value,
                            BakilanDugumSayisi = bakilanDugumSayisi,
                            KisitSayisi = kisitSayisi,
                            ResultModel = new List<EczaneNobetCozum>(),
                            KararDegikeniSayisi = data.EczaneNobetTarihAralik.Count,
                            CalismaSayisi = data.CalismaSayisi
                        };

                        var sonuclar = data.EczaneNobetTarihAralik.Where(s => x[s].Value == 1).ToList();
                        var sonuclar2 = data.EczaneNobetTarihAralik.Where(s => x[s].Value != 1).ToList();

                        var nobetGrupTarihler = data.EczaneNobetTarihAralik.Select(s => new { s.NobetGrupId, s.Tarih, s.NobetGorevTipId }).Distinct().ToList();

                        if (sonuclar.Count != nobetGrupTarihler.Count)
                        {
                            throw new Exception("Talebi karşılanmayan günler var");
                        }

                        foreach (var r in sonuclar)
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
                            mesaj = $"{data.NobetGruplar.Select(s => s.Adi).FirstOrDefault()} için {data.Yil} {data.Ay}.ayda bazı kısıtlar gevşetilerek {data.CalismaSayisi} farklı şekilde çözüm denendi.{iterasyonMesaj} Seçili kısıtlarla bu grup için uygun çözüm alanı bulunmamaktadır. Lütfen Nöbet Kural>Nöbet Üst Grup Kısıt sayfasındaki kısıtları kontrol edip tekrar deneyiniz..";
                            break;
                    }

                    throw new Exception(mesaj);
                }
            }
            return Results;
        }

        public void ModeliKapat()
        {
            _solver.Abort();
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

        private NobetGunKuralDetay GetNobetGunKural(int gunKuralId, MersinMerkezDataModelV2 data, int nobetGrupId, int gruptakiNobetciSayisi, int cozulenAydakiNobetSayisi)
        {
            var r = new Random();

            var eczaneler = data.EczaneNobetIstatistikler
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

            var oncekiAydakiNobetSayisi = nobetTutanEczaneler.Select(s => s.NobetSayisi).Sum();
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
                        var pazarNobetiYazilabilecekIlkAy = (int)Math.Ceiling((double)gruptakiNobetciSayisi / 5) - 1;

                        fazlaNobetTutacakEczaneler = nobetTutanEczaneler
                        .Where(w => w.NobetSayisi == kuralStd - 1
                                 && w.EnSonNobetTuttuguAy < (data.Ay - pazarNobetiYazilabilecekIlkAy)//5
                                 )
                        .OrderBy(o => o.EnSonNobetTuttuguAy)
                        //.OrderBy(o => r.NextDouble())
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
                        var pazarNobetiYazilabilecekIlkAy = (int)Math.Ceiling((double)gruptakiNobetciSayisi / 5) - 1;

                        fazlaNobetTutacakEczaneler = nobetTutanEczaneler
                        .Where(w => w.NobetSayisi == kuralStd - 1
                                 && w.EnSonNobetTuttuguAy < (data.Ay - pazarNobetiYazilabilecekIlkAy)
                                 )
                        .OrderBy(o => o.EnSonNobetTuttuguAy)
                        //.OrderBy(o => r.NextDouble())
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

        private NobetGunKuralDetay GetNobetGunKuralHaftaIciToplam(AntalyaMerkezDataModel data, int nobetGrupId, int gruptakiNobetciSayisi, int cozulenAydakiNobetSayisi)
        {
            var r = new Random();

            var eczaneler = data.EczaneNobetIstatistikler
                  .Where(w => w.NobetGrupId == nobetGrupId)
                  .OrderBy(x => r.NextDouble()).ToList();

            //eczanelerin nöbet sayıları
            var nobetTutanEczaneler = data.EczaneNobetSonuclar
                .Where(w => (w.NobetGunKuralId >= 2 && w.NobetGunKuralId <= 7)
                          && w.NobetGrupId == nobetGrupId
                          && eczaneler.Select(s => s.EczaneNobetGrupId).Contains(w.EczaneNobetGrupId)
                         )
                .GroupBy(g => g.EczaneNobetGrupId)
                .Select(s => new
                {
                    EczaneNobetGrupId = s.Key,
                    NobetSayisi = s.Count(),
                    EnSonNobetTuttuguAy = s.Max(c => c.Tarih.Month)
                }).ToList();

            var oncekiAydakiNobetSayisi = nobetTutanEczaneler.Select(s => s.NobetSayisi).Sum();
            var toplamNobetSayisi = oncekiAydakiNobetSayisi + cozulenAydakiNobetSayisi;
            var ortalamaNobetSayisi = (double)(toplamNobetSayisi) / gruptakiNobetciSayisi;

            var kuralStd = Math.Ceiling(ortalamaNobetSayisi);

            var tumEczaneler = eczaneler
                .Select(s => new
                {
                    s.EczaneNobetGrupId,
                    NobetSayisi = nobetTutanEczaneler
                        .Where(w => w.EczaneNobetGrupId == s.EczaneNobetGrupId)
                        .Select(f => f.NobetSayisi).SingleOrDefault(),
                    EnSonNobetTuttuguAy = nobetTutanEczaneler
                        .Where(w => w.EczaneNobetGrupId == s.EczaneNobetGrupId)
                        .Select(f => f.EnSonNobetTuttuguAy).SingleOrDefault()
                }).ToList();

            var nobetFrekans = tumEczaneler
                .GroupBy(g => g.NobetSayisi)
                .Select(s => new
                {
                    NobetSayisi = s.Key,
                    EczaneSayisi = s.Count()
                }).ToList();

            var eczaneNobetIstatistik = tumEczaneler
                .Where(w => w.NobetSayisi < kuralStd)
                .Select(s => s.EczaneNobetGrupId).ToList();

            var cozulenAydakiEksikNobetiOlanSayisi = eczaneNobetIstatistik.Count; //cozulenAydakiNobetSayisi - eczaneNobetIstatistik.Count;

            if (cozulenAydakiNobetSayisi - eczaneNobetIstatistik.Count < 1)
            {
                cozulenAydakiEksikNobetiOlanSayisi = cozulenAydakiNobetSayisi;
            }
            else
            {
                //gruptakiNobetciSayisi != 29
                var bakilmayacakNobetGruplar = new int[3] { 3 + 7, 3 + 9, 3 + 10 };
                if (!bakilmayacakNobetGruplar.Contains(nobetGrupId))
                {
                    kuralStd++;
                }
            }

            if (cozulenAydakiEksikNobetiOlanSayisi == 0) kuralStd++;

            var fazlaNobetTutacakEczaneler = new List<int>();

            double enCokNobetTutulanNobetSayisi;

            //periyot başlamadan buraya girmemeli (nobetTutanEczaneler.Count() == 0)
            if (nobetTutanEczaneler.Count() > 0)
            {
                enCokNobetTutulanNobetSayisi = nobetTutanEczaneler.Select(s => s.NobetSayisi).Max();
            }
            else
            {
                enCokNobetTutulanNobetSayisi = 100;
            }

            //hiç nöbet tutan yoksa bu işlem yapılmaz
            if (enCokNobetTutulanNobetSayisi <= kuralStd)
            {
                var nobetiEksikEczaneler = eczaneler
                    .Where(w => !eczaneNobetIstatistik.Contains(w.EczaneNobetGrupId)).ToList();//!nobetTutanEczaneNobetGruplar.Contains(w.EczaneNobetGrupId));

                var hicTutmayanlar = new List<int>();
                var eksigiOlanlar = new List<int>();

                if (nobetFrekans.Count() > 1)
                {
                    hicTutmayanlar = tumEczaneler
                            .Where(w => w.NobetSayisi == nobetFrekans.Select(s => s.NobetSayisi).Min())
                            .Select(s => s.EczaneNobetGrupId).ToList();

                    hicTutmayanlar.ForEach(x => eksigiOlanlar.Add(x));
                }

                if (cozulenAydakiEksikNobetiOlanSayisi > 0)
                {
                    tumEczaneler
                        .Where(w => w.NobetSayisi == kuralStd - 2)
                        .Select(s => s.EczaneNobetGrupId).ForEach(x => eksigiOlanlar.Add(x));

                }

                var eksikler = eksigiOlanlar.Distinct();
                var fazlaNobetTutacakEczaneSayisi = cozulenAydakiNobetSayisi - eksikler.Count();

                //10.grup 3. ayda açılıyor
                //var nobetTutacakYeterliEczaneSayisi = cozulenAydakiNobetSayisi - (eksikler.Count() * 2) - tumEczaneler
                //                                            .Where(w => w.NobetSayisi == kuralStd - 1)
                //                                            .Select(s => s.EczaneNobetGrupId).Count();

                if (fazlaNobetTutacakEczaneSayisi > 0
                    //&& nobetTutacakYeterliEczaneSayisi > 0
                    )
                {
                    if (nobetGrupId == 4)
                    {
                        fazlaNobetTutacakEczaneler = tumEczaneler
                            .Where(w => w.NobetSayisi == nobetFrekans.Select(s => s.NobetSayisi).Max()
                                     && !eksigiOlanlar.Contains(w.EczaneNobetGrupId))
                            .OrderBy(o => r.NextDouble())
                            .Select(s => s.EczaneNobetGrupId)
                            .Take(fazlaNobetTutacakEczaneSayisi).ToList();
                    }
                    else
                    {
                        fazlaNobetTutacakEczaneler = tumEczaneler
                            .Where(w => w.NobetSayisi == nobetFrekans.Select(s => s.NobetSayisi).Max()
                                     //&& !eksikler.Contains(w.EczaneNobetGrupId)
                                     )
                            .OrderByDescending(o => o.NobetSayisi).ThenBy(c => r.NextDouble())
                            .Select(s => s.EczaneNobetGrupId)
                            .Take(fazlaNobetTutacakEczaneSayisi).ToList();
                    }
                }
            }

            var nobetGunKuralDetayHaftaIciToplam = new NobetGunKuralDetay
            {
                FazlaNobetTutacakEczaneler = fazlaNobetTutacakEczaneler,
                KuralSTD = kuralStd,
                GunKuralId = 0,
                OrtalamaNobetSayisi = ortalamaNobetSayisi,
                EnCokNobetTutulanNobetSayisi = enCokNobetTutulanNobetSayisi
            };

            return nobetGunKuralDetayHaftaIciToplam;
        }
    }
}


/*
 * #region Diğer Günlerin Hedefleri

                    //gunDegerler = data.TarihAraligi.Select(s => s.GunDegerId).Distinct().ToList();
                    //gunKurallar = data.NobetGrupGunKurallar
                    //                            .Where(s => s.NobetGrupId == nobetGrup.Id)
                    //                            .Select(s => s.NobetGunKuralId);

                    //var gunler = gunDegerler.Where(s => gunKurallar.Contains(s)).ToList();

                    //foreach (var gunDeger in gunler)
                    //{
                    //    GetEczaneGunHedef(eczaneNobetGrup, out double maxArz, out double minArz, gunDeger);

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
                    //        //    if (pazarYazilacakEczaneler.Contains(eczaneNobetGrup))
                    //        //    {
                    //        //        maxArz = hedef.Pazar + 1;
                    //        //    }

                    //        //}

                    //        model.AddConstraint(
                    //          Expression.Sum(data.EczaneNobetTarihAralik
                    //                           .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                    //                                       && e.GunDegerId == gunDeger
                    //                                       && e.NobetGrupId == nobetGrup.Id)
                    //                           .Select(m => x[m])) <= maxArz,
                    //                           $"her eczaneye bir ayda nobet grubunun {gunDeger} hedefi kadar nobet yazilmali, {eczaneNobetGrup}");
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
                    //                            .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                    //                                        && e.GunDegerId == gunDeger
                    //                                        && e.NobetGrupId == nobetGrup.Id
                    //                                        )
                    //                            .Select(m => x[m])) >= minArz,
                    //                            $"her eczaneye bir ayda nobet grubunun {gunDeger} hedefi kadar nobet yazilmali, {eczaneNobetGrup}");
                    //    }
                    //}
                    #endregion

 * 
            var nobetIstatistigiCumartesiler = data.EczaneNobetGrupGunKuralIstatistikler
                .Where(w => w.NobetGunKuralId == 7).ToList();
            var nobetIstatistigiPazarlar = data.EczaneNobetGrupGunKuralIstatistikler
                .Where(w => w.NobetGunKuralId == 1).ToList();
            var nobetIstatistigiBayramlar = data.EczaneNobetGrupGunKuralIstatistikler
                .Where(w => w.NobetGunKuralId > 7).ToList();
            var nobetIstatistigiHaftaIci = data.EczaneNobetGrupGunKuralIstatistikler
                .Where(w => w.NobetGunKuralId > 1 && w.NobetGunKuralId < 7).ToList();



                //+ x[i] * Convert.ToInt32(i.CumartesiGunuMu)  * (BigMCumartesiCevrim / ((cozulenAyIlkCumartesi.Tarih - p.SonNobetTarihiCumartesi).TotalDays))
            //+x[i] * Convert.ToInt32(i.PazarGunuMu)       * (BigMPazarCevrim    / ((cozulenAyIlkPazar.Tarih - p.SonNobetTarihiPazar).TotalDays))
            //+ x[i] * Convert.ToInt32(i.BayramMi)         * (BigMBayramCevrim   / ((cozulenAyIlkBayram.Tarih - p.SonNobetTarihiBayram).TotalDays))
            //+ x[i] * Convert.ToInt32(i.HaftaIciMi)       * (BigMHaftaIciCevrim / ((cozulenAyIlkHaftaIci.Tarih - p.SonNobetTarihiHaftaIci).TotalDays))

            //int BigMCuma = 20000;
            //int BigMCumartesi = 40000;
            //int BigMPazartesi = 500;
            //int BigMSali = 500;
            //int BigMCarsamba = 500;
            //int BigMPersembe = 500;
            //int BigMHaftaIciToplam = 80000;//200000;//15000;

                //var nobetIstatistigi = data.EczaneNobetGrupGunKuralIstatistikler
            //   .GroupBy(g => new
            //   {
            //       g.EczaneNobetGrupId,
            //       g.EczaneId,
            //       g.EczaneAdi,
            //       g.NobetGrupAdi,
            //       g.NobetGrupId,
            //       g.NobetGorevTipId,
            //       g.NobetAltGrupId
            //   })
            //   .Select(s => new EczaneNobetGrupGunKuralIstatistikYatay
            //   {
            //       EczaneNobetGrupId= s.Key.EczaneNobetGrupId,
            //       EczaneId = s.Key.EczaneId,
            //       EczaneAdi = s.Key.EczaneAdi,
            //       NobetGrupId = s.Key.NobetGrupId,
            //       NobetGrupAdi = s.Key.NobetGrupAdi,
            //       NobetGorevTipId = s.Key.NobetGorevTipId,
            //       NobetAltGrupId = s.Key.NobetAltGrupId,

            //       NobetSayisiCumartesi = s.Where(w => w.NobetGunKuralId == 7).Sum(f => f.NobetSayisi),
            //       SonNobetTarihiCumartesi = s.Where(w => w.NobetGunKuralId == 7).Sum(f => f.NobetSayisi) > 0
            //       ? s.Where(w => w.NobetGunKuralId == 7).Max(f => f.SonNobetTarihi)
            //       : new DateTime(2010, 1, 1),

            //       NobetSayisiPazar = s.Where(w => w.NobetGunKuralId == 1).Sum(f => f.NobetSayisi),
            //       SonNobetTarihiPazar = s.Where(w => w.NobetGunKuralId == 1).Sum(f => f.NobetSayisi) > 0
            //       ? s.Where(w => w.NobetGunKuralId == 1).Max(f => f.SonNobetTarihi)
            //       : new DateTime(2010, 1, 1),

            //       NobetSayisiBayram = s.Where(w => w.NobetGunKuralId > 7).Sum(f => f.NobetSayisi),
            //       SonNobetTarihiBayram = s.Where(w => w.NobetGunKuralId > 7).Sum(f => f.NobetSayisi) > 0
            //       ? s.Where(w => w.NobetGunKuralId > 7).Max(f => f.SonNobetTarihi)
            //       : new DateTime(2010, 1, 1),

            //       NobetSayisiHaftaIci = s.Where(w => w.NobetGunKuralId > 1 && w.NobetGunKuralId <= 7).Sum(f => f.NobetSayisi),
            //       SonNobetTarihiHaftaIci = s.Where(w => w.NobetGunKuralId > 1 && w.NobetGunKuralId <= 7).Sum(f => f.NobetSayisi) > 0
            //       ? s.Where(w => w.NobetGunKuralId > 1 && w.NobetGunKuralId <= 7).Max(f => f.SonNobetTarihi)
            //       : new DateTime(2010, 1, 1),
            //       //NobetSayisiPazartesi = s.Where(w => w.NobetGunKuralId == 2).Sum(f => f.NobetSayisi),
            //       //NobetSayisiSali = s.Where(w => w.NobetGunKuralId == 3).Sum(f => f.NobetSayisi),
            //       //NobetSayisiCarsamba = s.Where(w => w.NobetGunKuralId == 4).Sum(f => f.NobetSayisi),
            //       //NobetSayisiPersembe = s.Where(w => w.NobetGunKuralId == 5).Sum(f => f.NobetSayisi),
            //       //NobetSayisiCuma = s.Where(w => w.NobetGunKuralId == 6).Sum(f => f.NobetSayisi)
            //   }).ToList();
            */
