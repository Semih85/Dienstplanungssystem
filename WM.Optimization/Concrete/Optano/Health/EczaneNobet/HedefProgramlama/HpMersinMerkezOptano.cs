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

namespace WM.Optimization.Concrete.Optano.Health.EczaneNobet.HedefProgramlama
{
    public class HpMersinMerkezOptano : IEczaneNobetMersinMerkezOptimization
    {
        #region Değişkenler
        //Karar değişkeni model çalıştıktan sonra değer aldığından burada tanımlandı
        private VariableCollection<EczaneNobetTarihAralik> _x { get; set; }
        private VariableCollection<EczaneNobetGrupDetay> _hPazartesiPS { get; set; }
        private VariableCollection<EczaneNobetGrupDetay> _hPazartesiNS { get; set; }
        private VariableCollection<EczaneNobetGrupDetay> _hSaliPS { get; set; }
        private VariableCollection<EczaneNobetGrupDetay> _hSaliNS { get; set; }
        private VariableCollection<EczaneNobetGrupDetay> _hCarsambaPS { get; set; }
        private VariableCollection<EczaneNobetGrupDetay> _hCarsambaNS { get; set; }
        private VariableCollection<EczaneNobetGrupDetay> _hPersembePS { get; set; }
        private VariableCollection<EczaneNobetGrupDetay> _hPersembeNS { get; set; }
        private VariableCollection<EczaneNobetGrupDetay> _hCumaPS { get; set; }
        private VariableCollection<EczaneNobetGrupDetay> _hCumaNS { get; set; }
        private VariableCollection<EczaneNobetGrupDetay> _hCumartesiPS { get; set; }
        private VariableCollection<EczaneNobetGrupDetay> _hCumartesiNS { get; set; }
        private VariableCollection<EczaneNobetGrupDetay> _hHiciPS { get; set; }
        private VariableCollection<EczaneNobetGrupDetay> _hHiciNS { get; set; }
        #endregion

        private Model Model(MersinMerkezDataModel data)
        {
            var model = new Model() { Name = "Mersin Merkez Eczane Nöbet" };

            #region Veriler
            var bayramlar = data.TarihAraligi.Where(w => w.NobetGunKuralId > 7).OrderBy(o => o.Tarih).ToList();
            var cumartesiGunleri = data.TarihAraligi.Where(w => w.NobetGunKuralId == 7).OrderBy(o => o.Tarih).ToList();
            var pazarGunleri = data.TarihAraligi.Where(w => w.NobetGunKuralId == 1).OrderBy(o => o.Tarih).ToList();
            var haftaIciGunleri = data.TarihAraligi.Where(w => w.NobetGunKuralId >= 2 && w.NobetGunKuralId <= 7).OrderBy(o => o.Tarih).ToList();
            var nobetGunKurallar = data.TarihAraligi.Select(s => s.NobetGunKuralId).Distinct().ToList();

            //int BigMCuma = 20000;
            //int BigMCumartesi = 40000;
            //int BigMPazartesi = 500;
            //int BigMSali = 500;
            //int BigMCarsamba = 500;
            //int BigMPersembe = 500;

            //int BigMHaftaIciToplam = 80000;//200000;//15000;

            //int cumartesiCevrim = 30000;
            int bayramCevrim = 8000; // 8000;
            int pazarCevrim = 1000; // 1000;
            int haftaIciCevrim = 500;

            var cozulenAyIlkCumartesi = cumartesiGunleri.FirstOrDefault();
            var cozulenAyIlkPazar = pazarGunleri.FirstOrDefault();
            var cozulenAyIlkBayram = bayramlar.Count > 0 ? bayramlar.FirstOrDefault() : data.TarihAraligi.FirstOrDefault();
            var cozulenAyIlkHaftaIci = haftaIciGunleri.FirstOrDefault();
            /*
            var nobetIstatistigiCumartesiler = data.EczaneNobetGrupGunKuralIstatistikler
                .Where(w => w.NobetGunKuralId == 7).ToList();
            var nobetIstatistigiPazarlar = data.EczaneNobetGrupGunKuralIstatistikler
                .Where(w => w.NobetGunKuralId == 1).ToList();
            var nobetIstatistigiBayramlar = data.EczaneNobetGrupGunKuralIstatistikler
                .Where(w => w.NobetGunKuralId > 7).ToList();
            var nobetIstatistigiHaftaIci = data.EczaneNobetGrupGunKuralIstatistikler
                .Where(w => w.NobetGunKuralId > 1 && w.NobetGunKuralId < 7).ToList();
            */

            var nobetIstatistigi = data.EczaneNobetGrupGunKuralIstatistikler
               .GroupBy(g => new
               {
                   g.EczaneNobetGrupId,
                   g.EczaneId,
                   g.EczaneAdi,
                   g.NobetGrupAdi,
                   g.NobetGrupId,
                   g.NobetGorevTipId,
                   g.NobetAltGrupId
               })
               .Select(s => new
               {
                   s.Key.EczaneNobetGrupId,
                   s.Key.EczaneId,
                   s.Key.EczaneAdi,
                   s.Key.NobetGrupId,
                   s.Key.NobetGrupAdi,
                   s.Key.NobetGorevTipId,
                   s.Key.NobetAltGrupId,

                   NobetSayisiCumartesi = s.Where(w => w.NobetGunKuralId == 7).Sum(f => f.NobetSayisi),
                   SonNobetTarihiCumartesi = s.Where(w => w.NobetGunKuralId == 7).Sum(f => f.NobetSayisi) > 0
                   ? s.Where(w => w.NobetGunKuralId == 7).Max(f => f.SonNobetTarihi)
                   : new DateTime(2010, 1, 1),

                   NobetSayisiPazar = s.Where(w => w.NobetGunKuralId == 1).Sum(f => f.NobetSayisi),
                   SonNobetTarihiPazar = s.Where(w => w.NobetGunKuralId == 1).Sum(f => f.NobetSayisi) > 0
                   ? s.Where(w => w.NobetGunKuralId == 1).Max(f => f.SonNobetTarihi)
                   : new DateTime(2010, 1, 1),

                   NobetSayisiBayram = s.Where(w => w.NobetGunKuralId > 7).Sum(f => f.NobetSayisi),
                   SonNobetTarihiBayram = s.Where(w => w.NobetGunKuralId > 7).Sum(f => f.NobetSayisi) > 0
                   ? s.Where(w => w.NobetGunKuralId > 7).Max(f => f.SonNobetTarihi)
                   : new DateTime(2010, 1, 1),

                   NobetSayisiHaftaIci = s.Where(w => w.NobetGunKuralId > 1 && w.NobetGunKuralId <= 7).Sum(f => f.NobetSayisi),
                   SonNobetTarihiHaftaIci = s.Where(w => w.NobetGunKuralId > 1 && w.NobetGunKuralId <= 7).Sum(f => f.NobetSayisi) > 0
                   ? s.Where(w => w.NobetGunKuralId > 1 && w.NobetGunKuralId <= 7).Max(f => f.SonNobetTarihi)
                   : new DateTime(2010, 1, 1),
                   //NobetSayisiPazartesi = s.Where(w => w.NobetGunKuralId == 2).Sum(f => f.NobetSayisi),
                   //NobetSayisiSali = s.Where(w => w.NobetGunKuralId == 3).Sum(f => f.NobetSayisi),
                   //NobetSayisiCarsamba = s.Where(w => w.NobetGunKuralId == 4).Sum(f => f.NobetSayisi),
                   //NobetSayisiPersembe = s.Where(w => w.NobetGunKuralId == 5).Sum(f => f.NobetSayisi),
                   //NobetSayisiCuma = s.Where(w => w.NobetGunKuralId == 6).Sum(f => f.NobetSayisi)
               }).ToList();
            #endregion

            #region Karar Değişkenleri
            _x = new VariableCollection<EczaneNobetTarihAralik>(
                    model,
                    data.EczaneNobetTarihAralik,
                    "_x",
                    null,
                    h => data.LowerBound,
                    h => data.UpperBound,
                    a => VariableType.Binary);

            _hPazartesiPS = new VariableCollection<EczaneNobetGrupDetay>(
                                model,
                                data.EczaneNobetGruplar,
                                "_hPazartesiPS",
                                null,
                                h => data.LowerBound,
                                h => 10,
                                a => VariableType.Continuous);

            _hPazartesiNS = new VariableCollection<EczaneNobetGrupDetay>(
                                model,
                                data.EczaneNobetGruplar,
                                "_hPazartesiNS",
                                null,
                                h => data.LowerBound,
                                h => 10,
                                a => VariableType.Continuous);

            _hSaliPS = new VariableCollection<EczaneNobetGrupDetay>(
                                model,
                                data.EczaneNobetGruplar,
                                "_hSaliPS",
                                null,
                                h => data.LowerBound,
                                h => 10,
                                a => VariableType.Continuous);

            _hSaliNS = new VariableCollection<EczaneNobetGrupDetay>(
                                model,
                                data.EczaneNobetGruplar,
                                "_hSaliNS",
                                null,
                                h => data.LowerBound,
                                h => 10,
                                a => VariableType.Continuous);

            _hCarsambaPS = new VariableCollection<EczaneNobetGrupDetay>(
                    model,
                    data.EczaneNobetGruplar,
                    "_hCarsambaPS",
                    null,
                    h => data.LowerBound,
                    h => 10,
                    a => VariableType.Continuous);

            _hCarsambaNS = new VariableCollection<EczaneNobetGrupDetay>(
                                model,
                                data.EczaneNobetGruplar,
                                "_hCarsambaNS",
                                null,
                                h => data.LowerBound,
                                h => 10,
                                a => VariableType.Continuous);

            _hPersembePS = new VariableCollection<EczaneNobetGrupDetay>(
                    model,
                    data.EczaneNobetGruplar,
                    "_hPersembePS",
                    null,
                    h => data.LowerBound,
                    h => 10,
                    a => VariableType.Continuous);

            _hPersembeNS = new VariableCollection<EczaneNobetGrupDetay>(
                                model,
                                data.EczaneNobetGruplar,
                                "_hPersembeNS",
                                null,
                                h => data.LowerBound,
                                h => 10,
                                a => VariableType.Continuous);

            _hCumaPS = new VariableCollection<EczaneNobetGrupDetay>(
                    model,
                    data.EczaneNobetGruplar,
                    "_hCumaPS",
                    null,
                    h => data.LowerBound,
                    h => 10,
                    a => VariableType.Continuous);

            _hCumaNS = new VariableCollection<EczaneNobetGrupDetay>(
                                model,
                                data.EczaneNobetGruplar,
                                "_hCumaNS",
                                null,
                                h => data.LowerBound,
                                h => 10,
                                a => VariableType.Continuous);

            _hCumartesiPS = new VariableCollection<EczaneNobetGrupDetay>(
                model,
                data.EczaneNobetGruplar,
                "_hCumartesiPS",
                null,
                h => data.LowerBound,
                h => 10,
                a => VariableType.Continuous);

            _hCumartesiNS = new VariableCollection<EczaneNobetGrupDetay>(
                                model,
                                data.EczaneNobetGruplar,
                                "_hCumartesiNS",
                                null,
                                h => data.LowerBound,
                                h => 10,
                                a => VariableType.Continuous);

            _hHiciPS = new VariableCollection<EczaneNobetGrupDetay>(
                                model,
                                data.EczaneNobetGruplar,
                                "_hHiciPS",
                                null,
                                h => data.LowerBound,
                                h => 10,
                                a => VariableType.Continuous);

            _hHiciNS = new VariableCollection<EczaneNobetGrupDetay>(
                                model,
                                data.EczaneNobetGruplar,
                                "_hHiciNS",
                                null,
                                h => data.LowerBound,
                                h => 10,
                                a => VariableType.Continuous);

            #endregion

            #region Amaç Fonksiyonu

            #region eski
            //        var amac = new Objective(Expression.Sum(
            //(from i in data.EczaneNobetTarihAralik
            // from j in data.EczaneNobetGruplar
            // from p in nobetIstatistigi
            // where i.EczaneNobetGrupId == j.Id

            //    && i.EczaneNobetGrupId == p.EczaneNobetGrupId
            //    && i.NobetGorevTipId == p.NobetGorevTipId
            // select (_x[i]
            //        //ilk yazılan nöbet öncelikli olsun:
            //        + _x[i] * Convert.ToInt32(i.CumartesiGunuMu) * (BigMCumartesiCevrim / ((cozulenAyIlkCumartesi.Tarih - p.SonNobetTarihiCumartesi).TotalDays))
            //        + _x[i] * Convert.ToInt32(i.PazarGunuMu) * (BigMPazarCevrim / ((cozulenAyIlkPazar.Tarih - p.SonNobetTarihiPazar).TotalDays))
            //        + _x[i] * Convert.ToInt32(i.BayramMi) * (BigMBayramCevrim / ((cozulenAyIlkBayram.Tarih - p.SonNobetTarihiBayram).TotalDays))
            //        + _x[i] * Convert.ToInt32(i.HaftaIciMi) * (BigMHaftaIciCevrim / ((cozulenAyIlkHaftaIci.Tarih - p.SonNobetTarihiHaftaIci).TotalDays))
            //        //pozitif sapma değişkenleri
            //        + _hPazartesiPS[j] * BigMPazartesi
            //        + _hSaliPS[j] * BigMSali
            //        + _hCarsambaPS[j] * BigMCarsamba
            //        + _hPersembePS[j] * BigMPersembe
            //        + _hCumaPS[j] * BigMCuma
            //        + _hCumartesiPS[j] * BigMCumartesi
            //        + _hHiciPS[j] * BigMHaftaIciToplam
            //        //negatif sapma değişkenleri
            //        + _hPazartesiNS[j] * BigMPazartesi
            //        + _hSaliNS[j] * BigMSali
            //        + _hCarsambaNS[j] * BigMCarsamba
            //        + _hPersembeNS[j] * BigMPersembe
            //        + _hCumaNS[j] * BigMCuma
            //        + _hCumartesiNS[j] * BigMCumartesi
            //        + _hHiciNS[j] * BigMHaftaIciToplam
            //        ))),
            //        "Sum of all item-values: ",
            //        ObjectiveSense.Minimize); 
            #endregion
            var pespeseHaftaIciAyniGunNobet = data.NobetUstGrupKisitlar
                              .Where(s => s.KisitAdi == "pespeseHaftaIciAyniGunNobet"
                                       && s.NobetUstGrupId == data.NobetUstGrupId)
                              .Select(w => w.PasifMi == false).SingleOrDefault();

            var amac = new Objective(Expression.Sum(
                (from i in data.EczaneNobetTarihAralik
                 from p in nobetIstatistigi
                 where i.EczaneNobetGrupId == p.EczaneNobetGrupId
                    && i.NobetGorevTipId == p.NobetGorevTipId
                 select (_x[i]
                        //ilk yazılan nöbet öncelikli olsun:
                        + _x[i] * Convert.ToInt32(i.BayramMi)
                                * (bayramCevrim + bayramCevrim / Math.Sqrt((i.Tarih - p.SonNobetTarihiBayram).TotalDays))

                        + _x[i] * Convert.ToInt32(i.PazarGunuMu)
                                * (pazarCevrim + pazarCevrim / Math.Sqrt((i.Tarih - p.SonNobetTarihiPazar).TotalDays * Math.Ceiling((double)i.Tarih.Day / 7)))

                        + _x[i] * Convert.ToInt32(i.HaftaIciMi)
                                * (haftaIciCevrim + haftaIciCevrim / Math.Sqrt((i.Tarih - p.SonNobetTarihiHaftaIci).TotalDays * i.Tarih.Day)
                                * (pespeseHaftaIciAyniGunNobet == true
                                ? (i.Tarih.DayOfWeek == p.SonNobetTarihiHaftaIci.DayOfWeek ? 1 : 0.2)
                                : 1)//aynı gün peşpeşe gelmesin
                                )

                 #region hedef değişkenler
                        ////pozitif sapma değişkenleri
                        //+ _hPazartesiPS[j] * BigMPazartesi
                        //+ _hSaliPS[j] * BigMSali
                        //+ _hCarsambaPS[j] * BigMCarsamba
                        //+ _hPersembePS[j] * BigMPersembe
                        //+ _hCumaPS[j] * BigMCuma
                        //+ _hCumartesiPS[j] * BigMCumartesi
                        //+ _hHiciPS[j] * BigMHaftaIciToplam
                        ////+ _hHiciPespesePS[j] * BigMHaftaIciToplam
                        ////negatif sapma değişkenleri
                        //+ _hPazartesiNS[j] * BigMPazartesi
                        //+ _hSaliNS[j] * BigMSali
                        //+ _hCarsambaNS[j] * BigMCarsamba
                        //+ _hPersembeNS[j] * BigMPersembe
                        //+ _hCumaNS[j] * BigMCuma
                        //+ _hCumartesiNS[j] * BigMCumartesi
                        //+ _hHiciNS[j] * BigMHaftaIciToplam
                        ////+ _hHiciPespeseNS[j] * BigMHaftaIciToplam 
                 #endregion
                        ))),
                        "Sum of all item-values: ",
                        ObjectiveSense.Minimize);

            model.AddObjective(amac);
            #endregion

            #region Kısıtlar

            foreach (var nobetGrup in data.NobetGruplar)
            {
                #region ön hazırlama

                var r = new Random();
                var eczaneNobetGruplar = data.EczaneNobetGruplar
                    .Where(w => w.NobetGrupId == nobetGrup.Id)
                    .OrderBy(x => r.NextDouble()).ToList();

                var nobetGrupGunKurallar = data.NobetGrupGunKurallar
                                    .Where(s => s.NobetGrupId == nobetGrup.Id
                                             && nobetGunKurallar.Contains(s.NobetGunKuralId))
                                    .Select(s => s.NobetGunKuralId).ToList();

                #region ilk ayda fazla yazılacak eczaneler

                var gruptakiNobetciSayisi = eczaneNobetGruplar.Select(s => s.EczaneId).Count();
                var gunSayisi = data.TarihAraligi.Count();

                var fazlaYazilacakEczaneSayisi = gunSayisi - gruptakiNobetciSayisi;
                var fazlaYazilacakEczaneler = new List<EczaneNobetGrupDetay>();

                //ilk ayda eczane sayısı gün sayısından azsa, eksik sayı kadar rasgele seçilen eczaneye 1 fazla nöbet yazılır. toplam hedeflerde
                if (data.Yil == 2018 && data.Ay == 1) //&& fazlaYazilacakEczaneSayisi > 0)
                {
                    fazlaYazilacakEczaneler = eczaneNobetGruplar.OrderBy(x => r.NextDouble()).Take(fazlaYazilacakEczaneSayisi).ToList();
                }
                #endregion

                #region Nöbet gün kurallar

                var nobetGunKural = data.NobetUstGrupKisitlar
                                                        .Where(s => s.KisitAdi == "nobetGunKural"
                                                                 && s.NobetUstGrupId == data.NobetUstGrupId)
                                                        .Select(w => w.PasifMi == false).SingleOrDefault();

                var nobetGunKuralDetaylar = new List<NobetGunKuralDetay>();

                if (nobetGunKural)
                {
                    //var nobetGunKurallar = data.TarihAraligi.Select(s => s.NobetGunKuralId).Distinct().ToList();

                    //var nobetGrupGunKurallar = data.NobetGrupGunKurallar
                    //                                .Where(s => s.NobetGrupId == nobetGrup.Id
                    //                                         && nobetGunKurallar.Contains(s.NobetGunKuralId))
                    //                                .Select(s => s.NobetGunKuralId);

                    foreach (var gunKuralId in nobetGrupGunKurallar)
                    {
                        var cozulenAydakiNobetSayisi = data.TarihAraligi.Where(w => w.NobetGunKuralId == gunKuralId).Count();

                        nobetGunKuralDetaylar.Add(GetNobetGunKural(gunKuralId, data, nobetGrup.Id, gruptakiNobetciSayisi, cozulenAydakiNobetSayisi));
                    }
                }

                var cozulenAydakiHaftaIciToplamNobetSayisi = data.TarihAraligi.Where(w => w.NobetGunKuralId >= 2 && w.NobetGunKuralId <= 7).Count();

                nobetGunKuralDetaylar.Add(GetNobetGunKuralHaftaIciToplam(data, nobetGrup.Id, gruptakiNobetciSayisi, cozulenAydakiHaftaIciToplamNobetSayisi));
                #endregion

                #endregion

                #region Talep Kısıtları

                foreach (var nobetGrupGorevTip in data.NobetGrupGorevTipler.Where(w => w.NobetGrupId == nobetGrup.Id))
                {
                    //var istekler = data.EczaneNobetIstekListe
                    //    .Where(w => (w.Tarih.Month == 4 && w.Tarih.Year == 2018 && w.Tarih < new DateTime(2018, 4, 16))
                    //              && w.NobetGrupId == nobetGrup.Id)
                    //    .Select(s => s.TakvimId).ToList();

                    foreach (var d in data.TarihAraligi//.Where(w => istekler.Contains(w.TakvimId))
                            )
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

                    //foreach (var d in data.TarihAraligi.Where(w => w.Tarih > new DateTime(2018, 4, 15)))
                    //{
                    //    int gunlukNobetciSayisi = (int)data.NobetGrupKurallar
                    //            .Where(s => s.NobetGrupId == nobetGrupGorevTip.NobetGrupId
                    //                     && s.NobetKuralId == 3)
                    //            .Select(s => s.Deger).SingleOrDefault();

                    //    var talepFarkli = data.NobetGrupTalepler
                    //                        .Where(s => s.NobetGrupGorevTipId == nobetGrupGorevTip.Id
                    //                                 && s.TakvimId == d.TakvimId)
                    //                        .Select(s => s.NobetciSayisi).SingleOrDefault();

                    //    if (talepFarkli > 0)
                    //        gunlukNobetciSayisi = talepFarkli;

                    //    model.AddConstraint(
                    //               Expression.Sum(data.EczaneNobetTarihAralik
                    //                                .Where(k => k.TakvimId == d.TakvimId
                    //                                         && k.NobetGrupId == nobetGrupGorevTip.NobetGrupId
                    //                                         && k.NobetGorevTipId == nobetGrupGorevTip.NobetGorevTipId
                    //                                )
                    //                                .Select(m => _x[m])) == gunlukNobetciSayisi,
                    //                                $"her gune talep kadar eczane atanmali: {gunlukNobetciSayisi}");
                    //}

                    //foreach (var d in data.TarihAraligi.Where(w => !istekler.Contains(w.TakvimId) && w.Tarih < new DateTime(2018, 4, 16)))
                    //{
                    //    model.AddConstraint(
                    //               Expression.Sum(data.EczaneNobetTarihAralik
                    //                                .Where(k => k.TakvimId == d.TakvimId
                    //                                         && k.NobetGrupId == nobetGrupGorevTip.NobetGrupId
                    //                                         && k.NobetGorevTipId == nobetGrupGorevTip.NobetGorevTipId
                    //                                )
                    //                                .Select(m => _x[m])) == 0,
                    //                                $"her gune talep kadar eczane atanmali: {0}");
                    //}
                }

                #endregion

                #region Eczane bazlı kısıtlar

                foreach (var eczaneNobetGrup in eczaneNobetGruplar)
                {
                    var kontrolEdilecekEczaneler = new string[] { "CERRAHOĞLU" };

                    if (kontrolEdilecekEczaneler.Contains(eczaneNobetGrup.EczaneAdi))
                    {
                    }
                    ;

                    #region Ay içinde peşpeşe görev yazılmasın

                    var herAyPespeseGorev = data.NobetUstGrupKisitlar
                                .Where(s => s.KisitAdi == "herAyPespeseGorev"
                                         && s.NobetUstGrupId == data.NobetUstGrupId)
                                .Select(w => w.PasifMi == false).SingleOrDefault();

                    if (herAyPespeseGorev)
                    {
                        int pespeseNobetSayisi = (int)data.NobetGrupKurallar
                                .Where(s => s.NobetGrupId == nobetGrup.Id
                                         && s.NobetKuralId == 1)
                                .Select(s => s.Deger).SingleOrDefault();

                        foreach (var g in data.TarihAraligi.Take(data.TarihAraligi.Count() - pespeseNobetSayisi))
                        {
                            //model.AddConstraint(
                            //  Expression.Sum(data.EczaneNobetTarihAralik
                            //                   .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                            //                               && (e.Gun >= g.Gun && e.Gun <= g.Gun + pespeseNobetSayisi)
                            //                               && e.NobetGrupId == nobetGrup.Id
                            //                               )
                            //                   .Select(m => _x[m])) <= 1,
                            //                   $"eczanelere_pespese_nobet_yazilmasin, {eczaneNobetGrup.Id}");
                        }

                    }
                    #endregion

                    #region Farklı aylar peşpeşe görev yazılmasın

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
                        else if (data.CalismaSayisi == 8)
                        {
                            farkliAyPespeseGorevAraligi = 8;
                        }
                        else if (data.CalismaSayisi == 9)
                        {
                            farkliAyPespeseGorevAraligi = 5;
                        }

                        if (eczaneNobetGrup.Id == 516)
                        {

                        }

                        //var pazarVeBayramlar = new List<int> { 1, 8, 9 };
                        var farkliAyPespeseGorevAraligifStd = data.NobetUstGrupKisitlar
                                                            .Where(s => s.KisitAdi == "farkliAyPespeseGorev"
                                                                     && s.NobetUstGrupId == data.NobetUstGrupId)
                                                            .Select(w => w.SagTarafDegeri).SingleOrDefault();

                        if (farkliAyPespeseGorevAraligifStd > 0)
                        {
                            farkliAyPespeseGorevAraligi = farkliAyPespeseGorevAraligifStd;
                        }

                        var enSonNobetTarihi = data.EczaneNobetSonuclar
                            .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id
                                    //&& !pazarVeBayramlar.Contains(w.NobetGunKuralId)
                                    )
                            .Select(s => s.Tarih).OrderByDescending(o => o).FirstOrDefault();

                        var yazilabilecekIlkTarih = enSonNobetTarihi.AddDays(farkliAyPespeseGorevAraligi);

                        if (enSonNobetTarihi != null)
                        {
                            foreach (var g in data.TarihAraligi.Where(w => w.Tarih <= yazilabilecekIlkTarih))
                            {
                                model.AddConstraint(
                                  Expression.Sum(data.EczaneNobetTarihAralik
                                                   .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                               && e.TakvimId == g.TakvimId
                                                               && e.NobetGorevTipId == 1
                                                               //&& e.NobetGrupId == nobetGrup.Id
                                                               )
                                                   .Select(m => _x[m])) == 0,
                                                   $"eczanelere_farkli_aylarda_pespese_nobet_yazilmasin, {eczaneNobetGrup}");
                            }
                        }
                    }
                    #endregion

                    #region Pazar çevrim (farklı aylar peşpeşe görev yazılmasın)

                    var pazarPespeseGorev = data.NobetUstGrupKisitlar
                                            .Where(s => s.KisitAdi == "pazarPespeseGorev"
                                                     && s.NobetUstGrupId == data.NobetUstGrupId)
                                            .Select(w => w.PasifMi == false).SingleOrDefault();

                    if (pazarPespeseGorev)
                    {
                        var enSonPazarTuttuguGun = data.EczaneNobetSonuclar
                            .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id
                                     && w.NobetGunKuralId == 1)
                            .Select(s => s.TakvimId).OrderByDescending(o => o).FirstOrDefault();

                        var kontrolListesi = new int[] { 154 };
                        if (kontrolListesi.Contains(eczaneNobetGrup.Id))
                        {
                        }

                        if (enSonPazarTuttuguGun > 0) //enSonPazarTuttuguNobetAyi
                        {
                            var enSonPazarTuttuguTarih = data.EczaneNobetSonuclar
                                .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id
                                         && w.NobetGunKuralId == 1)
                                .Select(s => new { s.Tarih, s.TakvimId }).OrderByDescending(o => o.Tarih).FirstOrDefault();

                            //pazar nöbetinin yazılabileceği uygun çevrim aralığının belirlenmesi:

                            //küçük sayı
                            var pazarNobetiYazilabilecekIlkAy = (int)Math.Ceiling((double)gruptakiNobetciSayisi / 5);
                            //büyük sayı
                            var pazarNobetiYazilabilecekSonAy = (int)Math.Ceiling((double)gruptakiNobetciSayisi / 4);

                            //yazılabilecek aralığının sağlanması için
                            //küçük sayıyı azalttık
                            var yazilabilecekIlkTarih = enSonPazarTuttuguTarih.Tarih.AddMonths(pazarNobetiYazilabilecekIlkAy - 2);
                            //büyük sayıyı artırdık
                            var yazilabilecekSonTarih = enSonPazarTuttuguTarih.Tarih.AddMonths(pazarNobetiYazilabilecekSonAy + 2);

                            if (data.CalismaSayisi > 5)
                            {//buna rağmen çözüm aralığı yoksa +-1 ile aralığı biraz daha açtık
                                //yazilabilecekSonTarih = yazilabilecekIlkTarih.AddMonths(-1);
                                //yazilabilecekSonTarih = yazilabilecekSonTarih.AddMonths(1);
                            }

                            var tarihlerGruplu = pazarGunleri
                                .GroupBy(g => new { g.Yil, g.Ay })
                                .Select(s => new
                                {
                                    s.Key.Yil,
                                    s.Key.Ay,
                                    IlkTarih = s.Min(m => m.Tarih),
                                    SonTarih = s.Max(m => m.Tarih)
                                }).ToList();

                            var nobetYazilabilecegiPazarGunleri = (from t1 in pazarGunleri
                                                                   from t2 in tarihlerGruplu
                                                                   where t1.Yil == t2.Yil
                                                                   && t1.Yil == t2.Yil
                                                                   && !(t2.IlkTarih >= yazilabilecekIlkTarih && t2.SonTarih <= yazilabilecekSonTarih)
                                                                   select new { t1.TakvimId, t1.Tarih, t2.IlkTarih, t2.SonTarih }).ToList();

                            //nöbet yazılabileceği tarih aralığının (pazar günleri) dışında nöbet yazılmasın
                            foreach (var g in nobetYazilabilecegiPazarGunleri)
                            {
                                model.AddConstraint(
                                  Expression.Sum(data.EczaneNobetTarihAralik
                                                   .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                               && e.TakvimId == g.TakvimId
                                                               && e.NobetGrupId == nobetGrup.Id
                                                               )
                                                   .Select(m => _x[m])) == 0,
                                                   $"eczanelere_farkli_aylarda_pazar_gunleri_pespese_nobet_yazilmasin, {eczaneNobetGrup.Id}");
                            }

                            //periyot bitmeden bir ay önce hala pazar nöbeti yazılmamışsa yazılsın.
                            var periyottakiNobetSayisi = data.EczaneNobetSonuclar
                                       .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                && w.NobetGorevTipId == 1//eczaneNobetGrup.NobetGorevTipId
                                                                         //&& (w.Tarih >= yazilabilecekIlkTarih.AddMonths(2) && w.Tarih <= yazilabilecekSonTarih)
                                                && (w.Tarih >= yazilabilecekSonTarih.AddMonths(-1))
                                                && w.NobetGunKuralId == 1)
                                       .Select(s => s.TakvimId).Count();

                            if (cozulenAyIlkPazar.Tarih >= yazilabilecekSonTarih.AddMonths(-1) && periyottakiNobetSayisi == 0)
                            {
                                model.AddConstraint(
                                  Expression.Sum(data.EczaneNobetTarihAralik
                                                   .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                               && e.NobetGorevTipId == 1//eczaneNobetGrup.NobetGorevTipId
                                                               && pazarGunleri.Select(s => s.TakvimId).Contains(e.TakvimId)
                                                               )
                                                   .Select(m => _x[m])) == 1,
                                                   $"eczanelere_farkli_aylarda_pazar_gunu_pespese_nobet_yazilmasin, {eczaneNobetGrup}");
                            }
                        }
                    }
                    #endregion

                    #region Pazar çevrim (peşpeşe görev yazılmasın en az)

                    var pazarPespeseGorevEnAz = data.NobetUstGrupKisitlar
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

                            var pazarNobetiYazilabilecekIlkAy = (int)Math.Ceiling((double)gruptakiNobetciSayisi / 5);

                            var yazilabilecekIlkTarih = enSonPazarTuttuguTarih.Tarih.AddMonths(pazarNobetiYazilabilecekIlkAy - 1);

                            if (data.CalismaSayisi > 5)
                            {//buna rağmen çözüm aralığı yoksa +-1 ile aralığı biraz daha açtık
                                yazilabilecekIlkTarih = yazilabilecekIlkTarih.AddMonths(-1);
                            }

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
                                                               && e.NobetGrupId == nobetGrup.Id
                                                               )
                                                   .Select(m => _x[m])) == 0,
                                                   $"eczanelere_farkli_aylarda_pazar_gunu_pespese_nobet_yazilmasin_enaz, {eczaneNobetGrup.Id}");
                            }
                        }
                    }
                    #endregion

                    #region pazar çevrim -- sonra bakılacak. (31.03.2018)
                    var pazarCevrimi = false;

                    if (pazarCevrimi)
                    {
                        var cozulenAydakiNobetSayisi = pazarGunleri.Count;
                        var oncekiAylardakiToplamNobetSayisi = nobetIstatistigi.Select(s => s.NobetSayisiPazar).Sum();
                        var toplamNobetSayisi = oncekiAylardakiToplamNobetSayisi + cozulenAydakiNobetSayisi;

                        var cozulenAydakiIlkPazar = pazarGunleri.Select(s => s.Tarih).FirstOrDefault();
                        var cozulenAydakiSonPazar = pazarGunleri.Select(s => s.Tarih).LastOrDefault();
                        var cevrimdekiGunSayisi = gruptakiNobetciSayisi - 1;
                        var cevrimdekiIlkNobetTarihi = cozulenAydakiIlkPazar;
                        var cevrimdekiSonNobetTarihi = cozulenAydakiSonPazar;
                        var cevrimBitisTarihi = cevrimdekiIlkNobetTarihi.AddDays(cevrimdekiGunSayisi * 7);

                        if (oncekiAylardakiToplamNobetSayisi > 0)
                        {
                            cevrimdekiIlkNobetTarihi = nobetIstatistigi
                                .Where(w => w.NobetSayisiPazar > 0)
                                .Select(s => s.SonNobetTarihiPazar).Min();

                            cevrimdekiSonNobetTarihi = nobetIstatistigi
                                .Where(w => w.NobetSayisiPazar > 0)
                                .Select(s => s.SonNobetTarihiPazar).Max();

                            cevrimBitisTarihi = cevrimdekiIlkNobetTarihi.AddDays(cevrimdekiGunSayisi * 7);

                            if (cevrimdekiSonNobetTarihi > cevrimBitisTarihi)
                            {//tur bittikten sonraki ilk nöbet tarihi, yeni turun ilk tarihi olarak güncelle
                                cevrimdekiIlkNobetTarihi = nobetIstatistigi
                                    .Where(w => w.SonNobetTarihiPazar > cevrimBitisTarihi)
                                    .Select(s => s.SonNobetTarihiPazar).Min();
                            }
                        }

                        var nobetDurumu = nobetIstatistigi
                                .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id).SingleOrDefault();

                        //eczanelerin nöbet tutacakları günler
                        var tarihler = pazarGunleri
                              .Where(w => w.Tarih >= cevrimdekiIlkNobetTarihi && w.Tarih <= cevrimBitisTarihi)
                              .Select(s => s.TakvimId).ToList();

                        model.AddConstraint(
                                Expression.Sum(from i in data.EczaneNobetTarihAralik
                                               from j in data.EczaneNobetGruplar
                                               where i.EczaneNobetGrupId == j.Id
                                                     && i.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                     && tarihler.Contains(i.TakvimId)
                                                     && i.NobetGorevTipId == 1//hedef.NobetGorevTipId
                                                                              //&& i.GunDegerId == 1
                                               select (_x[i])) + nobetDurumu.NobetSayisiPazar <= 1,
                                                $"her eczaneye bir ayda nöbet grubunun cumartesi {1} hedefi kadar nöbet yazılmalı, {eczaneNobetGrup.Id}");

                    }
                    #endregion

                    #region Farklı aylar peşpeşe görev yazılmasın (hafta içi peşpeşe)

                    var haftaIciPespeseGorevEnAz = data.NobetUstGrupKisitlar
                                            .Where(s => s.KisitAdi == "haftaIciPespeseGorevEnAz"
                                                     && s.NobetUstGrupId == data.NobetUstGrupId)
                                            .Select(w => w.PasifMi == false).SingleOrDefault();

                    var ortamalaHaftaIci = Math.Ceiling((double)haftaIciGunleri.Count / gruptakiNobetciSayisi);

                    if (haftaIciPespeseGorevEnAz)
                    {
                        var pazarVeBayramlar = new List<int> { 1, 8, 9 };

                        var enSonNobetTarihi = data.EczaneNobetSonuclar
                            .Where(w => w.EczaneId == eczaneNobetGrup.EczaneId
                                     && !pazarVeBayramlar.Contains(w.NobetGunKuralId))
                            .Select(s => s.Tarih).OrderByDescending(o => o).FirstOrDefault();

                        var farkliAyPespeseGorevAraligi = (gruptakiNobetciSayisi * 1.2);
                        var altLimit = farkliAyPespeseGorevAraligi * 0.7666;
                        var ustLimit = farkliAyPespeseGorevAraligi + farkliAyPespeseGorevAraligi * 0.6667;
                        var ustLimitKontrol = ustLimit * 0.95; //0.81

                        var yazilabilecekIlkTarih = enSonNobetTarihi.AddDays(altLimit);
                        var yazilabilecekKontrolTarihi = enSonNobetTarihi.AddDays(ustLimitKontrol);
                        var yazilabilecekSonTarih = enSonNobetTarihi.AddDays(ustLimit);

                        if (enSonNobetTarihi != null)
                        {
                            var haftaIciGunler = haftaIciGunleri
                                .Where(w => w.Tarih <= yazilabilecekIlkTarih)
                                .Select(s => s.TakvimId).ToList();

                            if (eczaneNobetGrup.Id == 333)
                            {

                            }
                            if (haftaIciGunler.Count() > 0)
                            {
                                model.AddConstraint(
                                 Expression.Sum(from i in data.EczaneNobetTarihAralik
                                                from j in data.EczaneNobetGruplar
                                                where i.EczaneNobetGrupId == j.Id
                                                      && i.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                      && i.NobetGorevTipId == 1//hedef.NobetGorevTipId
                                                      && haftaIciGunler.Contains(i.TakvimId)
                                                select (_x[i] //+ _hHiciPespeseNS[j] - _hHiciPespesePS[j]
                                                                )) == 0,
                                                 $"her eczaneye bir ayda nöbet grubunun {1} hedefi kadar toplam hafta içi nobeti yazılmalı, {eczaneNobetGrup.Id}");
                            }
                        }

                        #region belirlenen tarih aralığında yazmadığında nöbet yazılacak

                        var haftaIciPespeseGorevUstLimitKontrol = data.NobetUstGrupKisitlar
                                            .Where(s => s.KisitAdi == "haftaIciPespeseGorevUstLimitKontrol"
                                                     && s.NobetUstGrupId == data.NobetUstGrupId)
                                            .Select(w => w.PasifMi == false).SingleOrDefault();

                        if (haftaIciPespeseGorevUstLimitKontrol)
                        {
                            var haftaIciGunler = haftaIciGunleri
                                .Where(w => (//w.Tarih >= yazilabilecekKontrolTarihi && 
                                             w.Tarih <= yazilabilecekSonTarih))
                                .Select(s => s.TakvimId).ToList();

                            var kontrolPeriyodundakitHaftaIciNobetSayisi = data.EczaneNobetSonuclar
                                           .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                    && w.NobetGorevTipId == 1//eczaneNobetGrup.NobetGorevTipId
                                                    && (w.Tarih >= yazilabilecekIlkTarih && w.Tarih <= yazilabilecekKontrolTarihi)
                                                    && !pazarVeBayramlar.Contains(w.NobetGunKuralId))
                                           .Select(s => s.TakvimId).Count();

                            if (haftaIciGunler.Count() > 3)
                            {
                                if (eczaneNobetGrup.Id == 102)
                                {
                                }

                                if (kontrolPeriyodundakitHaftaIciNobetSayisi == 0)
                                {
                                    model.AddConstraint(
                                      Expression.Sum(data.EczaneNobetTarihAralik
                                                       .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                                   && e.NobetGorevTipId == 1//eczaneNobetGrup.NobetGorevTipId
                                                                   && haftaIciGunler.Contains(e.TakvimId)
                                                                   )
                                                       .Select(m => _x[m])) == 1,
                                                       $"eczanelere_farkli_aylarda_pespese_nobet_yazilmasin, {eczaneNobetGrup}");
                                }
                            }
                        }

                        #endregion

                        #region üst limit

                        var haftaIciPespeseGorevUstLimit = data.NobetUstGrupKisitlar
                                            .Where(s => s.KisitAdi == "haftaIciPespeseGorevUstLimit"
                                                     && s.NobetUstGrupId == data.NobetUstGrupId)
                                            .Select(w => w.PasifMi == false).SingleOrDefault();

                        if (haftaIciPespeseGorevUstLimit)
                        {
                            var haftaIciGunler = haftaIciGunleri
                                .Where(w => (//w.Tarih >= yazilabilecekKontrolTarihi && 
                                             w.Tarih >= yazilabilecekSonTarih))
                                .Select(s => s.TakvimId).ToList();

                            var kontrolPeriyodundakitHaftaIciNobetSayisi = data.EczaneNobetSonuclar
                                           .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                    && w.NobetGorevTipId == 1//eczaneNobetGrup.NobetGorevTipId
                                                    && w.Tarih >= yazilabilecekSonTarih
                                                    && !pazarVeBayramlar.Contains(w.NobetGunKuralId))
                                           .Select(s => s.TakvimId).Count();

                            if (eczaneNobetGrup.Id == 102)
                            {
                            }

                            model.AddConstraint(
                              Expression.Sum(data.EczaneNobetTarihAralik
                                               .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                           && e.NobetGorevTipId == 1//eczaneNobetGrup.NobetGorevTipId
                                                           && haftaIciGunler.Contains(e.TakvimId)
                                                           )
                                               .Select(m => _x[m])) == 0,
                                               $"eczanelere_farkli_aylarda_pespese_nobet_yazilmasin, {eczaneNobetGrup}");
                        }

                        #endregion

                        #region Ay içinde peşpeşe görev yazılmasın

                        //var pespeseGorevHaftaIci = data.NobetUstGrupKisitlar
                        //            .Where(s => s.KisitAdi == "pespeseGorevHaftaIci"
                        //                     && s.NobetUstGrupId == data.NobetUstGrupId)
                        //            .Select(w => w.PasifMi == false).SingleOrDefault();

                        //pespeseGorevHaftaIci = true;

                        if (//pespeseGorevHaftaIci && 
                            ortamalaHaftaIci > 1)
                        {
                            var pespeseNobetSayisi = (int)altLimit;

                            //foreach (var g in data.TarihAraligi.Where(w => w.NobetGunKuralId >= 2 && w.NobetGunKuralId <= 7).Take(data.TarihAraligi.Count() - pespeseNobetSayisi))

                            foreach (var g in data.TarihAraligi.Take(data.TarihAraligi.Count() - pespeseNobetSayisi))
                            {
                                //model.AddConstraint(
                                //  Expression.Sum(data.EczaneNobetTarihAralik
                                //                   .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                                //                               && (e.Gun >= g.Gun && e.Gun <= g.Gun + pespeseNobetSayisi)
                                //                               && (e.GunDegerId >= 2 && e.GunDegerId <= 7)
                                //                               && e.NobetGrupId == nobetGrup.Id
                                //                               )
                                //                   .Select(m => _x[m])) <= 1,
                                //                   $"eczanelere_pespese_nobet_yazilmasin, {eczaneNobetGrup}");
                            }

                        }
                        #endregion
                    }
                    #endregion

                    #region Gün kümülatif toplam Max Hedef

                    var gunKumulatifToplamEnFazla = data.NobetUstGrupKisitlar
                                                .Where(s => s.KisitAdi == "gunKumulatifToplamEnFazla"
                                                         && s.NobetUstGrupId == data.NobetUstGrupId)
                                                .Select(w => w.PasifMi == false).SingleOrDefault();

                    if (gunKumulatifToplamEnFazla)
                    {
                        var gunKumulatifToplamMaxHedefSTD = data.NobetUstGrupKisitlar
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

                            var toplamNobetSayisi = data.EczaneNobetSonuclar
                                                        .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                                 && w.NobetGunKuralId == gunKural.NobetGunKuralId
                                                                 && w.Tarih >= data.NobetUstGrupBaslangicTarihi)
                                                        .Select(s => s.TakvimId).Count();

                            if (!nobetGrupGunKurallar.Contains(gunKural.NobetGunKuralId))
                            {
                                kumulatifOrtalamaGunKuralSayisi++;
                            }

                            if (gunKumulatifToplamMaxHedefSTD > 0)
                            {
                                kumulatifOrtalamaGunKuralSayisi = gunKumulatifToplamMaxHedefSTD;
                            }

                            if (toplamNobetSayisi > kumulatifOrtalamaGunKuralSayisi)
                            {
                                //continue;
                                kumulatifOrtalamaGunKuralSayisi = toplamNobetSayisi;
                            }

                            //>8
                            if (data.CalismaSayisi == 2)
                                kumulatifOrtalamaGunKuralSayisi++;

                            var tarihler = data.TarihAraligi.Where(w => w.NobetGunKuralId == gunKural.NobetGunKuralId);

                            //foreach (var g in data.TarihAraligi.Where(w => w.NobetGunKuralId == gunKural.NobetGunKuralId))
                            //{
                            model.AddConstraint(
                              Expression.Sum(data.EczaneNobetTarihAralik
                                               .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                           //&& e.TakvimId == g.TakvimId
                                                           && tarihler.Select(s => s.TakvimId).Contains(e.TakvimId)
                                                           )
                                               .Select(m => _x[m])) + toplamNobetSayisi <= kumulatifOrtalamaGunKuralSayisi,
                                               $"eczanelere_herbir_hafta_icinde_encok_yilda_ortalama_kadar_gorev_yaz, {eczaneNobetGrup}");
                            //}
                        }
                        #endregion

                        #region Pazar

                        if (nobetGrupGunKurallar.Contains(1))
                        {
                            var kumulatifPazarSayisi = data.TakvimNobetGrupGunDegerIstatistikler
                                                                .Where(w => w.NobetGrupId == eczaneNobetGrup.NobetGrupId
                                                                         && w.NobetGunKuralId == 1).SingleOrDefault();


                            var kumulatifOrtalamaPazarSayisi = Math.Ceiling((double)kumulatifPazarSayisi.GunSayisi / gruptakiNobetciSayisi);

                            var toplamNobetSayisi = data.EczaneNobetSonuclar
                                                        .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                                 && w.NobetGunKuralId == 1
                                                                 && w.Tarih >= data.NobetUstGrupBaslangicTarihi)
                                                        .Select(s => s.TakvimId).Count();

                            //if (pazarKumulatifToplamMaxHedefSTD > 0)
                            //{
                            //    kumulatifOrtalamaPazarSayisi = pazarKumulatifToplamMaxHedefSTD;
                            //}

                            if (toplamNobetSayisi > kumulatifOrtalamaPazarSayisi)
                            {
                                kumulatifOrtalamaPazarSayisi = toplamNobetSayisi;
                            }

                            //foreach (var g in pazarGunleri)
                            //{
                            model.AddConstraint(
                              Expression.Sum(data.EczaneNobetTarihAralik
                                               .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                           && pazarGunleri.Select(s => s.TakvimId).Contains(e.TakvimId))
                                               .Select(m => _x[m])) + toplamNobetSayisi <= kumulatifOrtalamaPazarSayisi,
                                               $"eczanelere_herbir_hafta_icinde_encok_yilda_ortalama_kadar_gorev_yaz, {eczaneNobetGrup}");
                            //}
                        }
                        #endregion
                    }
                    #endregion                    

                    #region Toplam Cuma Max Hedef

                    var toplamCumaMaxHedef = data.NobetUstGrupKisitlar
                                                .Where(s => s.KisitAdi == "toplamCumaMaxHedef"
                                                         && s.NobetUstGrupId == data.NobetUstGrupId)
                                                .Select(w => w.PasifMi == false).SingleOrDefault();

                    if (toplamCumaMaxHedef)
                    {
                        var toplamCumaMaxHedefSTD = data.NobetUstGrupKisitlar
                                                        .Where(s => s.KisitAdi == "toplamCumaMaxHedef"
                                                                 && s.NobetUstGrupId == data.NobetUstGrupId)
                                                        .Select(w => w.SagTarafDegeri).SingleOrDefault();

                        if (data.CalismaSayisi == 8) toplamCumaMaxHedefSTD = toplamCumaMaxHedefSTD++;

                        var toplamCumaSayisi = data.EczaneNobetSonuclar
                            .Where(w => w.EczaneId == eczaneNobetGrup.EczaneId
                                     && w.NobetGunKuralId == 6)
                            .Select(s => s.TakvimId).Count();

                        foreach (var g in data.TarihAraligi.Where(w => w.NobetGunKuralId == 6))
                        {
                            model.AddConstraint(
                              Expression.Sum(data.EczaneNobetTarihAralik
                                               .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                           && e.TakvimId == g.TakvimId
                                                           && e.NobetGrupId == nobetGrup.Id
                                                           )
                                               .Select(m => _x[m])) + toplamCumaSayisi <= toplamCumaMaxHedefSTD,
                                               $"eczanelere_cuma_gunu_encok_yilda_3_gorev_yaz, {eczaneNobetGrup.Id}");
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
                        var toplamCumartesiMaxHedefStd = data.NobetUstGrupKisitlar
                                                            .Where(s => s.KisitAdi == "toplamCumartesiMaxHedef"
                                                                     && s.NobetUstGrupId == data.NobetUstGrupId)
                                                            .Select(w => w.SagTarafDegeri).SingleOrDefault();

                        //if (data.CalismaSayisi == 8) toplamCumartesiMaxHedefSTD = toplamCumartesiMaxHedefSTD++;

                        var toplamCumartesiSayisi = data.EczaneNobetSonuclar
                            .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id
                                     && w.NobetGunKuralId == 7)
                            .Select(s => s.TakvimId).Count();

                        var cumartesiler = data.TarihAraligi.Where(w => w.NobetGunKuralId == 7).Select(s => s.TakvimId).ToList();
                        //foreach (var g in data.TarihAraligi.Where(w => w.NobetGunKuralId == 7))
                        //{
                        model.AddConstraint(
                          Expression.Sum(data.EczaneNobetTarihAralik
                                           .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                       && cumartesiler.Contains(e.TakvimId))
                                           .Select(m => _x[m])) + toplamCumartesiSayisi
                                                                       <= toplamCumartesiMaxHedefStd,
                                                                       $"eczanelere_cumartesi_gunu_encok_yilda_3_gorev_yaz, {eczaneNobetGrup.Id}");
                        //}
                    }
                    #endregion

                    #region Toplam Cumartesi Min Hedef

                    var toplamCumartesiMinHedef = data.NobetUstGrupKisitlar
                                                    .Where(s => s.KisitAdi == "toplamCumartesiMinHedef"
                                                             && s.NobetUstGrupId == data.NobetUstGrupId)
                                                    .Select(w => w.PasifMi == false).SingleOrDefault();

                    if (toplamCumartesiMinHedef)
                    {
                        var toplamCumartesiMinHedefStd = data.NobetUstGrupKisitlar
                                                            .Where(s => s.KisitAdi == "toplamCumartesiMinHedef"
                                                                     && s.NobetUstGrupId == data.NobetUstGrupId)
                                                            .Select(w => w.SagTarafDegeri).SingleOrDefault();

                        //if (data.CalismaSayisi == 8) toplamCumartesiMaxHedefSTD = toplamCumartesiMaxHedefSTD++;

                        var toplamCumartesiSayisi = data.EczaneNobetSonuclar
                            .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id
                                     && w.NobetGunKuralId == 7)
                            .Select(s => s.TakvimId).Count();

                        var cumartesiler = data.TarihAraligi.Where(w => w.NobetGunKuralId == 7).Select(s => s.TakvimId).ToList();
                        //foreach (var g in data.TarihAraligi.Where(w => w.NobetGunKuralId == 7))
                        //{
                        model.AddConstraint(
                          Expression.Sum(data.EczaneNobetTarihAralik
                                           .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                       && cumartesiler.Contains(e.TakvimId)
                                                       //&& e.NobetGrupId == nobetGrup.Id
                                                       )
                                           .Select(m => _x[m])) + toplamCumartesiSayisi >= toplamCumartesiMinHedefStd,
                                           $"eczanelere_cumartesi_gunu_encok_yilda_3_gorev_yaz, {eczaneNobetGrup.Id}");
                        //}
                    }
                    #endregion

                    #region Toplam min

                    var toplamMinHedef = data.NobetUstGrupKisitlar
                                                        .Where(s => s.KisitAdi == "toplamMinHedef"
                                                                 && s.NobetUstGrupId == data.NobetUstGrupId)
                                                        .Select(w => w.PasifMi == false).SingleOrDefault();
                    if (toplamMinHedef)
                    {
                        var hedef = data.EczaneKumulatifHedefler.Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id).SingleOrDefault();
                        var minToplam = hedef.Toplam - 1;

                        if (data.CalismaSayisi == 3 || data.CalismaSayisi == 4)
                        {
                            minToplam = hedef.Toplam - 1 > 1 ? 1 : 0;
                        }
                        var toplamNobetSayisi = data.EczaneNobetSonuclar
                           .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id)
                           .Select(s => s.TakvimId).Count();

                        var toplamMinHedefStd = data.NobetUstGrupKisitlar
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
                                            .Select(m => _x[m])) + toplamNobetSayisi >= minToplam,
                                            $"her_eczaneye_ayda_en_az_nobet_grubunun_hedefinden_1_eksik_nobet_yazilmali, {hedef}");
                    }
                    #endregion

                    #region Toplam max

                    var toplamMaxHedef = data.NobetUstGrupKisitlar
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
                           .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id)
                           .Select(s => s.TakvimId).Count();

                        var toplamMaxHedefStd = data.NobetUstGrupKisitlar
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
                                          .Select(m => _x[m])) + toplamNobetSayisi <= maxToplam,
                                          $"her_eczaneye_ayda_en_cok_nobet_grubunun_hedefi_kadar_nobet_yazilmali, {hedef}");
                    }
                    #endregion

                    #region Her ay en fazla görev

                    var herAyEnFazlaGorev = data.NobetUstGrupKisitlar
                                    .Where(s => s.KisitAdi == "herAyEnFazlaGorev"
                                             && s.NobetUstGrupId == data.NobetUstGrupId)
                                    .Select(w => w.PasifMi == false).SingleOrDefault();

                    if (herAyEnFazlaGorev)
                    {
                        var herAyEnFazlaGorevSayisi = Math.Ceiling((double)data.TarihAraligi.Count / gruptakiNobetciSayisi);

                        var herAyEnFazlaGorevStd = data.NobetUstGrupKisitlar
                                                 .Where(s => s.KisitAdi == "herAyEnFazlaGorev"
                                                          && s.NobetUstGrupId == data.NobetUstGrupId)
                                                 .Select(w => w.SagTarafDegeri).SingleOrDefault();

                        if (herAyEnFazlaGorevStd > 0)
                        {
                            herAyEnFazlaGorevSayisi += (int)herAyEnFazlaGorevStd;
                        }

                        //if (gruptakiNobetciSayisi >= data.TarihAraligi.Count)
                        //{
                        model.AddConstraint(
                          Expression.Sum(data.EczaneNobetTarihAralik
                                            .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                     && e.NobetGorevTipId == 1//hedef.NobetGorevTipId
                                                     )
                                            .Select(m => _x[m])) <= herAyEnFazlaGorevSayisi,
                                            $"her_eczaneye_bir_ayda_en_fazla_bir_nobet_yazilmali, {eczaneNobetGrup.Id}");
                        //}
                    }

                    #endregion

                    #region Her ay en az 1 görev

                    var herAyEnaz1Gorev = data.NobetUstGrupKisitlar
                                    .Where(s => s.KisitAdi == "herAyEnaz1Gorev"
                                             && s.NobetUstGrupId == data.NobetUstGrupId)
                                    .Select(w => w.PasifMi == false).SingleOrDefault();

                    if (herAyEnaz1Gorev && gruptakiNobetciSayisi < data.TarihAraligi.Count)
                    {
                        var herAyEnaz1GorevSTD = data.NobetUstGrupKisitlar
                                                .Where(s => s.KisitAdi == "herAyEnaz1Gorev"
                                                         && s.NobetUstGrupId == data.NobetUstGrupId)
                                                .Select(w => w.SagTarafDegeri).SingleOrDefault();

                        model.AddConstraint(
                              Expression.Sum(data.EczaneNobetTarihAralik
                                                .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                         && e.NobetGorevTipId == 1//hedef.NobetGorevTipId
                                                         )
                                                .Select(m => _x[m])) >= herAyEnaz1GorevSTD,
                                                $"her eczaneye bir ayda en az nobet grubunun hedefinden bir eksik nobet yazilmali, {eczaneNobetGrup.Id}");
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
                                                .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                         && e.NobetGorevTipId == 1//hedef.NobetGorevTipId
                                                         && e.NobetGunKuralId == 1)
                                                .Select(m => _x[m])) <= 1,
                                                $"her eczaneye bir ayda en az nobet grubunun hedefinden bir eksik nobet yazilmali, {eczaneNobetGrup.Id}");
                    }
                    #endregion

                    #region Her ay en fazla hafta içi (ortalama kadar)

                    var herAyEnFazlaHaftaIci = data.NobetUstGrupKisitlar
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
                                                .Select(m => _x[m])) <= ortamalaHaftaIci,
                                                $"her eczaneye bir ayda en az nobet grubunun hedefinden bir eksik nobet yazilmali, {eczaneNobetGrup}");
                    }
                    #endregion

                    #region Nöbet grup gün kurallar
                    //nobetGunKural = false;

                    if (nobetGunKural)
                    {
                        foreach (var item in nobetGunKuralDetaylar.Where(w => w.GunKuralId > 0))
                        {
                            var gunKuralId = item.GunKuralId;

                            var gunKuralNobetSayisi = data.EczaneNobetSonuclar
                                .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id
                                         && w.NobetGorevTipId == 1//hedef.NobetGorevTipId
                                         && w.NobetGunKuralId == gunKuralId)
                                .Select(s => s.TakvimId).Count();

                            var Std = item.KuralSTD;

                            if (!item.FazlaNobetTutacakEczaneler.Contains(eczaneNobetGrup.Id) && item.FazlaNobetTutacakEczaneler.Count > 0)
                            {
                                Std = item.KuralSTD - 1;
                            }

                            //eczanelerin nöbet tutacakları günler
                            var tarihler = data.TarihAraligi.Where(w => w.NobetGunKuralId == gunKuralId).Select(s => s.TakvimId).ToList();

                            if (gunKuralNobetSayisi <= Std)
                            {
                                var kontrol = true;

                                //if (data.CalismaSayisi >= 2) kontrol = false;

                                if (gunKuralId == 2 && kontrol)
                                {//pazartesi
                                    if (data.CalismaSayisi > 2) Std++;
                                    model.AddConstraint(
                                            Expression.Sum(from i in data.EczaneNobetTarihAralik
                                                           from j in data.EczaneNobetGruplar
                                                           where i.EczaneNobetGrupId == j.Id
                                                                 && i.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                                 && tarihler.Contains(i.TakvimId)
                                                                 && i.NobetGorevTipId == 1//hedef.NobetGorevTipId
                                                                 && i.NobetGunKuralId == 2
                                                           select (_x[i] + _hPazartesiNS[j] - _hPazartesiPS[j])) + gunKuralNobetSayisi
                                                            == Std,
                                                            $"her eczaneye bir ayda nöbet grubunun pazartesi {gunKuralId} hedefi kadar nöbet yazılmalı, {Std}");
                                }
                                else if (gunKuralId == 3 && kontrol)
                                {//salı
                                    if (data.CalismaSayisi > 2) Std++;
                                    model.AddConstraint(
                                            Expression.Sum(from i in data.EczaneNobetTarihAralik
                                                           from j in data.EczaneNobetGruplar
                                                           where i.EczaneNobetGrupId == j.Id
                                                                 && i.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                                 && tarihler.Contains(i.TakvimId)
                                                                 && i.NobetGorevTipId == 1//hedef.NobetGorevTipId
                                                                 && i.NobetGunKuralId == 3
                                                           select (_x[i] + _hSaliNS[j] - _hSaliPS[j])) + gunKuralNobetSayisi
                                                           == Std,
                                                            $"her eczaneye bir ayda nöbet grubunun sali {gunKuralId} hedefi kadar nöbet yazılmalı, {Std}");
                                }
                                else if (gunKuralId == 4 && kontrol)
                                {//çarşamba
                                    if (data.CalismaSayisi > 2) Std++;
                                    model.AddConstraint(
                                            Expression.Sum(from i in data.EczaneNobetTarihAralik
                                                           from j in data.EczaneNobetGruplar
                                                           where i.EczaneNobetGrupId == j.Id
                                                                 && i.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                                 && tarihler.Contains(i.TakvimId)
                                                                 && i.NobetGorevTipId == 1//hedef.NobetGorevTipId
                                                                 && i.NobetGunKuralId == 4
                                                           select (_x[i] + _hCarsambaNS[j] - _hCarsambaPS[j])) + gunKuralNobetSayisi
                                                           == Std,
                                                            $"her eczaneye bir ayda nöbet grubunun carsamba {gunKuralId} hedefi kadar nöbet yazılmalı, {Std}");
                                }
                                else if (gunKuralId == 5 && kontrol)
                                {//perşembe
                                    if (data.CalismaSayisi > 2) Std++;
                                    model.AddConstraint(
                                            Expression.Sum(from i in data.EczaneNobetTarihAralik
                                                           from j in data.EczaneNobetGruplar
                                                           where i.EczaneNobetGrupId == j.Id
                                                                 && i.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                                 && tarihler.Contains(i.TakvimId)
                                                                 && i.NobetGorevTipId == 1//hedef.NobetGorevTipId
                                                                 && i.NobetGunKuralId == 5
                                                           select (_x[i] + _hPersembeNS[j] - _hPersembePS[j])) + gunKuralNobetSayisi
                                                           == Std,
                                                            $"her eczaneye bir ayda nöbet grubunun persembe {gunKuralId} hedefi kadar nöbet yazılmalı, {Std}");
                                }
                                else if (gunKuralId == 6)
                                {//cuma
                                    model.AddConstraint(
                                            Expression.Sum(from i in data.EczaneNobetTarihAralik
                                                           from j in data.EczaneNobetGruplar
                                                           where i.EczaneNobetGrupId == j.Id
                                                                 && i.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                                 && tarihler.Contains(i.TakvimId)
                                                                 && i.NobetGorevTipId == 1//hedef.NobetGorevTipId
                                                                 && i.NobetGunKuralId == 6
                                                           select (_x[i] + _hCumaNS[j] - _hCumaPS[j])) + gunKuralNobetSayisi == Std,
                                                            $"her eczaneye bir ayda nöbet grubunun cuma {gunKuralId} hedefi kadar nöbet yazılmalı, {Std}");
                                }
                                else if (gunKuralId == 7)
                                {//cumartesi
                                    model.AddConstraint(
                                            Expression.Sum(from i in data.EczaneNobetTarihAralik
                                                           from j in data.EczaneNobetGruplar
                                                           where i.EczaneNobetGrupId == j.Id
                                                                 && i.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                                 && tarihler.Contains(i.TakvimId)
                                                                 && i.NobetGorevTipId == 1//hedef.NobetGorevTipId
                                                                 && i.NobetGunKuralId == 7
                                                           select (_x[i] + _hCumartesiNS[j] - _hCumartesiPS[j])) + gunKuralNobetSayisi == Std,
                                                            $"her eczaneye bir ayda nöbet grubunun cumartesi {gunKuralId} hedefi kadar nöbet yazılmalı, {Std}");
                                }
                                else if (gunKuralId == 1)
                                {//pazar
                                    model.AddConstraint(
                                            Expression.Sum(from i in data.EczaneNobetTarihAralik
                                                           from j in data.EczaneNobetGruplar
                                                           where i.EczaneNobetGrupId == j.Id
                                                                 && i.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                                 && tarihler.Contains(i.TakvimId)
                                                                 && i.NobetGorevTipId == 1//hedef.NobetGorevTipId
                                                                 && i.NobetGunKuralId == 1
                                                           select (_x[i])) + gunKuralNobetSayisi <= Std,
                                                            $"her eczaneye bir ayda nöbet grubunun cumartesi {gunKuralId} hedefi kadar nöbet yazılmalı, {Std}");
                                }
                                else if (gunKuralId == 8)
                                {//dini bayram
                                    model.AddConstraint(
                                            Expression.Sum(from i in data.EczaneNobetTarihAralik
                                                           from j in data.EczaneNobetGruplar
                                                           where i.EczaneNobetGrupId == j.Id
                                                                 && i.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                                 && tarihler.Contains(i.TakvimId)
                                                                 && i.NobetGorevTipId == 1//hedef.NobetGorevTipId
                                                                 && i.NobetGunKuralId == 8
                                                           select (_x[i])) + gunKuralNobetSayisi <= Std,
                                                            $"her eczaneye bir ayda nöbet grubunun cumartesi {gunKuralId} hedefi kadar nöbet yazılmalı, {Std}");
                                }
                                else if (gunKuralId == 9)
                                {//milli bayram
                                    model.AddConstraint(
                                            Expression.Sum(from i in data.EczaneNobetTarihAralik
                                                           from j in data.EczaneNobetGruplar
                                                           where i.EczaneNobetGrupId == j.Id
                                                                 && i.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                                 && tarihler.Contains(i.TakvimId)
                                                                 && i.NobetGorevTipId == 1//hedef.NobetGorevTipId
                                                                 && i.NobetGunKuralId == 9
                                                           select (_x[i])) + gunKuralNobetSayisi <= Std,
                                                            $"her eczaneye bir ayda nöbet grubunun cumartesi {gunKuralId} hedefi kadar nöbet yazılmalı, {Std}");
                                }
                                //else
                                //{
                                //    model.AddConstraint(
                                //        Expression.Sum(data.EczaneNobetTarihAralik
                                //                        .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                                //                                    && tarihler.Contains(e.TakvimId)
                                //                                    && e.NobetGorevTipId == 1//hedef.NobetGorevTipId
                                //                                    )
                                //                        .Select(m => _x[m]))// + gunKuralNobetSayisi 
                                //                                            <= Std,
                                //                        $"eczanelere_gun_kurallarini_periyodik_yaz, {item}-{hedef}");
                                //}
                            }
                            else
                            {
                                model.AddConstraint(
                                   Expression.Sum(data.EczaneNobetTarihAralik
                                                    .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                                && tarihler.Contains(e.TakvimId)
                                                                && e.NobetGorevTipId == 1//hedef.NobetGorevTipId
                                                                )
                                                    .Select(m => _x[m])) == 0,
                                                    $"eczanelere_gun_kurallarini_periyodik_yaz, {item}-{eczaneNobetGrup.Id}");
                            }
                        }
                    }
                    #endregion

                    #region Hafta içi toplam max hedefler

                    var haftaIciToplamMaxHedef = data.NobetUstGrupKisitlar
                                                .Where(s => s.KisitAdi == "haftaIciToplamMaxHedef"
                                                         && s.NobetUstGrupId == data.NobetUstGrupId)
                                                .Select(w => w.PasifMi == false).SingleOrDefault();
                    if (haftaIciToplamMaxHedef)
                    {
                        var haftaIciDurum = nobetGunKuralDetaylar.Where(w => w.GunKuralId == 0).FirstOrDefault();

                        var haftaIciToplamNobetSayisi = data.EczaneNobetSonuclar
                             .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id
                                      && w.NobetGorevTipId == 1//hedef.NobetGorevTipId
                                      && (w.NobetGunKuralId >= 2 && w.NobetGunKuralId <= 6)
                                      )
                             .Select(s => s.TakvimId).Count();

                        var Std = haftaIciDurum.KuralSTD;

                        var kontrolListesi = new int[] { 88 };
                        if (kontrolListesi.Contains(eczaneNobetGrup.Id))
                        {
                        }

                        if (!haftaIciDurum.FazlaNobetTutacakEczaneler.Contains(eczaneNobetGrup.Id) && haftaIciDurum.FazlaNobetTutacakEczaneler.Count > 0)
                        {
                            Std = haftaIciDurum.KuralSTD - 1;
                        }

                        //eczanelerin nöbet tutacakları günler
                        var tarihler = data.TarihAraligi
                            .Where(w => w.NobetGunKuralId >= 2 && w.NobetGunKuralId <= 6)
                            .Select(s => s.TakvimId).ToList();

                        var haftaIciToplamMaxHedefStd = data.NobetUstGrupKisitlar
                                                            .Where(s => s.KisitAdi == "haftaIciToplamMaxHedef"
                                                                     && s.NobetUstGrupId == data.NobetUstGrupId)
                                                            .Select(w => w.SagTarafDegeri).SingleOrDefault();

                        if (haftaIciToplamMaxHedefStd > 0) Std = haftaIciToplamMaxHedefStd;

                        if (haftaIciToplamNobetSayisi <= Std) //&& !haftaIciDurum.FazlaNobetTutacakEczaneler.Contains(eczaneNobetGrup.Id)
                        {
                            //if (data.CalismaSayisi < 4)
                            //{
                            model.AddConstraint(
                              Expression.Sum(data.EczaneNobetTarihAralik
                                            .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                    && e.NobetGorevTipId == 1//hedef.NobetGorevTipId
                                                    && tarihler.Contains(e.TakvimId)
                                                    )
                                            .Select(m => _x[m])) + haftaIciToplamNobetSayisi <= Std,
                                            $"her eczaneye bir ayda nobet grubunun hedefi kadar toplam hafta içi nobeti yazilmali, {eczaneNobetGrup.Id}");
                            //}
                            //else
                            //{
                            //    model.AddConstraint(
                            //     Expression.Sum(from i in data.EczaneNobetTarihAralik
                            //                    from j in data.EczaneNobetGruplar
                            //                    where i.EczaneNobetGrupId == j.Id
                            //                          && i.EczaneNobetGrupId == eczaneNobetGrup.Id
                            //                          && i.NobetGorevTipId == 1//hedef.NobetGorevTipId
                            //                          && tarihler.Contains(i.TakvimId)
                            //                    select (_x[i] + _hHiciNS[j] - _hHiciPS[j])) + haftaIciToplamNobetSayisi == Std,
                            //                     $"her eczaneye bir ayda nöbet grubunun {1} hedefi kadar toplam hafta içi nobeti yazılmalı, {haftaIciToplam}");
                            //}
                        }
                        else
                        {
                            model.AddConstraint(
                                 Expression.Sum(from i in data.EczaneNobetTarihAralik
                                                where i.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                      && i.NobetGorevTipId == 1//hedef.NobetGorevTipId
                                                      && tarihler.Contains(i.TakvimId)
                                                select (_x[i])) == 0,
                                                 $"her eczaneye bir ayda nöbet grubunun {1} hedefi kadar toplam hafta içi nobeti yazılmalı, {eczaneNobetGrup.Id}");
                        }
                    }
                    #endregion                    

                    #region Bayram toplam hedefleri

                    if (bayramlar.Count() > 0)
                    {
                        var bayramToplamEnFazla = data.NobetUstGrupKisitlar
                                                    .Where(s => s.KisitAdi == "bayramToplamEnFazla"
                                                             && s.NobetUstGrupId == data.NobetUstGrupId)
                                                    .Select(w => w.PasifMi == false).SingleOrDefault();

                        var bayramNobetleri = data.EczaneNobetSonuclar
                                .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id
                                         && w.NobetGunKuralId > 7
                                         && w.Tarih >= data.NobetUstGrupBaslangicTarihi
                                         )
                                .Select(s => new { s.TakvimId, s.NobetGunKuralId }).ToList();

                        var bayramNobetleriAnahtarli = data.EczaneNobetSonuclar
                                .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id
                                         && w.NobetGunKuralId > 7
                                         )
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
                                //bbbb
                                //yillikOrtalamaGunKuralSayisi++;
                            }

                            if (yillikOrtalamaGunKuralSayisi < 1) yillikOrtalamaGunKuralSayisi = 0;

                            var bayramToplamMaxHedefSTD = (int)data.NobetUstGrupKisitlar
                                                        .Where(s => s.KisitAdi == "bayramToplamEnFazla"
                                                                 && s.NobetUstGrupId == data.NobetUstGrupId)
                                                        .Select(w => w.SagTarafDegeri).SingleOrDefault();

                            if (toplamBayramNobetSayisi > yillikOrtalamaGunKuralSayisi)
                            {
                                //continue;
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

                                var bayramPespeseFarkliTur = data.NobetUstGrupKisitlar
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
                                                        .Select(m => _x[m])) == 0,
                                                        $"her eczaneye bir ayda nobet grubunun hedefi kadar toplam bayram nobeti yazilmali, {eczaneNobetGrup}");
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
                                                        .Select(m => _x[m])) + toplamBayramNobetSayisi <= yillikOrtalamaGunKuralSayisi,
                                                        $"her eczaneye bir ayda nobet grubunun hedefi kadar toplam bayram nobeti yazilmali, {eczaneNobetGrup}");
                                    }
                                }

                            }

                            //model.AddConstraint(
                            //  Expression.Sum(data.EczaneNobetTarihAralik
                            //                .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                            //                         && e.GunDegerId > 7)
                            //                .Select(m => _x[m])) + toplamBayramNobetSayisi <= toplamBayramMax,
                            //                $"her eczaneye bir ayda nobet grubunun hedefi kadar toplam bayram nobeti yazilmali, {eczaneNobetGrup}");
                        }

                        var bayramToplamMinHedef = data.NobetUstGrupKisitlar
                                                    .Where(s => s.KisitAdi == "bayramToplamMinHedef"
                                                             && s.NobetUstGrupId == data.NobetUstGrupId)
                                                    .Select(w => w.PasifMi == false).SingleOrDefault();
                        if (bayramToplamMinHedef)
                        {
                            var hedef = data.EczaneKumulatifHedefler.Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id).SingleOrDefault();
                            var toplamBayramMin = hedef.ToplamBayram - 1;
                            if (toplamBayramMin < 1) toplamBayramMin = 0;

                            var bayramToplamMinHedefSTD = (int)data.NobetUstGrupKisitlar
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
                                            .Select(m => _x[m])) + toplamBayramNobetSayisi >= toplamBayramMin,
                                            $"her eczaneye bir ayda nobet grubunun hedefi kadar toplam bayram nobeti yazilmali, {eczaneNobetGrup}");
                        }
                    }
                    #endregion

                    #region Hafta içi toplam min hedefler

                    var haftaIciToplamMinHedef = data.NobetUstGrupKisitlar
                                        .Where(s => s.KisitAdi == "haftaIciToplamMinHedef"
                                                 && s.NobetUstGrupId == data.NobetUstGrupId)
                                        .Select(w => w.PasifMi == false).SingleOrDefault();

                    //if (data.CalismaSayisi == 3 || data.CalismaSayisi == 4)
                    //{
                    //    haftaIciToplamMinHedef = false;
                    //}

                    if (haftaIciToplamMinHedef)
                    {
                        var hedef = data.EczaneKumulatifHedefler.Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id).SingleOrDefault();
                        var haftaIciMinToplam = hedef.ToplamHaftaIci - 1;

                        var haftaIciToplamNobetSayisi = data.EczaneNobetSonuclar
                             .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id
                                      && w.NobetGorevTipId == 1//hedef.NobetGorevTipId
                                      && (w.NobetGunKuralId >= 2 && w.NobetGunKuralId <= 6)
                                      )
                             .Select(s => s.TakvimId).Count();

                        var haftaIciToplamMinHedefStd = (int)data.NobetUstGrupKisitlar
                              .Where(s => s.KisitAdi == "haftaIciToplamMinHedef"
                                       && s.NobetUstGrupId == data.NobetUstGrupId)
                              .Select(w => w.SagTarafDegeri).SingleOrDefault();

                        if (haftaIciToplamMinHedefStd > 0) haftaIciMinToplam = haftaIciToplamMinHedefStd;

                        model.AddConstraint(
                          Expression.Sum(data.EczaneNobetTarihAralik
                                        .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                && (e.NobetGunKuralId >= 2 && e.NobetGunKuralId <= 6))
                                        .Select(m => _x[m])) + haftaIciToplamNobetSayisi >= haftaIciMinToplam,
                                        $"her eczaneye bir ayda nobet grubunun hedefi kadar toplam hafta içi nobeti yazilmali, {hedef}");
                    }
                    #endregion

                    #region Toplam cuma cumartesi max hedefler

                    var toplamCumaCumartesiMaxHedef = data.NobetUstGrupKisitlar
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
                                        .Select(m => _x[m])) <= cumaCumartesiToplam,
                                        $"her eczaneye bir ayda nobet grubunun hedefi kadar toplam hafta içi nobeti yazilmali, {hedef}");
                    }
                    #endregion

                    #region Toplam cuma cumartesi min hedefler

                    var toplamCumaCumartesiMinHedef = data.NobetUstGrupKisitlar
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
                                        .Select(m => _x[m])) >= cumaCumartesiToplam,
                                        $"her eczaneye bir ayda nobet grubunun hedefi kadar toplam hafta içi nobeti yazilmali, {hedef}");
                    }
                    #endregion                    
                }
                #endregion

                #region Eczane grup kısıtı

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

                        if (g.Id == 167)
                        {

                        }

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

                var istek = data.NobetUstGrupKisitlar
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

                var mazeret = data.NobetUstGrupKisitlar
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

                var bayram = data.NobetUstGrupKisitlar
                                .Where(s => s.KisitAdi == "herAyEnFazla1Bayram"
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
                        foreach (var f in eczaneNobetGruplar)
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

            #region Alt gruplarla eşit sayıda nöbet tutulsun

            var altGruplarlaAyniGunNobetTutma = data.NobetUstGrupKisitlar
                                                        .Where(s => s.KisitAdi == "altGruplarlaAyniGunNobetTutma"
                                                                    && s.NobetUstGrupId == data.NobetUstGrupId)
                                                        .Select(w => w.PasifMi == false).SingleOrDefault();

            var ayniGunNobetTutmasiTakipEdilecekGruplar = new List<int>
            {
                20, 22
                //21,
            };

            var nobetGruplar = data.NobetGruplar.Where(w => ayniGunNobetTutmasiTakipEdilecekGruplar.Contains(w.Id)).Select(s => s.Id).ToList();

            if (altGruplarlaAyniGunNobetTutma && nobetGruplar.Count() > 0)
            {
                var yeniSehir1Ve3tekiEczaneler = data.EczaneNobetGruplar
                    .Where(w => w.NobetGrupId == 20 || w.NobetGrupId == 22)
                    .Select(s => s.EczaneId).ToList();

                var altGruplarlaAyniGunNobetTutmaStd = data.NobetUstGrupKisitlar
                                                                   .Where(s => s.KisitAdi == "altGruplarlaAyniGunNobetTutma"
                                                                            && s.NobetUstGrupId == data.NobetUstGrupId)
                                                                   .Select(w => w.SagTarafDegeri).SingleOrDefault();

                foreach (var eczaneId in yeniSehir1Ve3tekiEczaneler)
                {
                    var nobetAltGruplar = data.EczaneGrupNobetSonuclarTumu
                        .Where(w => w.NobetGrupId == 21)
                        .Select(s => s.NobetAltGrupId).Distinct().OrderBy(o => o).ToList();

                    foreach (var altGrup in nobetAltGruplar)
                    {
                        var bakilanEczaneninSonuclari = data.EczaneNobetSonuclar
                            .Where(w => w.EczaneId == eczaneId)
                            .Select(s => s.TakvimId);

                        var yeniSehir2dekiEczaneninSonuclari = data.EczaneGrupNobetSonuclarTumu
                            .Where(w => w.NobetGrupId == 21
                                     && w.NobetAltGrupId == altGrup)
                            .Select(s => s.TakvimId);

                        var ayniGunTutulanNobetler = (from g1 in bakilanEczaneninSonuclari
                                                      from g2 in yeniSehir2dekiEczaneninSonuclari
                                                      where g1 == g2
                                                      select new
                                                      {
                                                          TakvimId = g1
                                                      }).ToList();

                        var birlikteNobetSayisi = ayniGunTutulanNobetler.Count();

                        model.AddConstraint(
                              Expression.Sum(data.EczaneNobetTarihAralik
                                               .Where(e => e.EczaneId == eczaneId)
                                               .Select(m => _x[m])) + birlikteNobetSayisi <= altGruplarlaAyniGunNobetTutmaStd,
                                               $"ay icinde diger gruptaki eczaneler ile en fazla bir kez nobet tutulsun, {eczaneId}");
                    }
                }
            }
            #endregion

            #endregion

            return model;
        }

        public EczaneNobetSonucModel Solve(MersinMerkezDataModel data)
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
                            mesaj = $"{data.NobetGruplar.Select(s => s.Adi).FirstOrDefault()} için {data.Yil} {data.Ay}.ayda bazı kısıtlar gevşetilerek {data.CalismaSayisi} farklı şekilde çözüm denendi.{iterasyonMesaj} Seçili kısıtlarla bu grup için uygun çözüm alanı bulunmamaktadır. Lütfen Nöbet Kural>Nöbet Üst Grup Kısıt sayfasındaki kısıtları kontrol edip tekrar deneyiniz..";
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

        private NobetGunKuralDetay GetNobetGunKural(int gunKuralId, MersinMerkezDataModel data, int nobetGrupId, int gruptakiNobetciSayisi, int cozulenAydakiNobetSayisi)
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
                    EnSonNobetTuttuguAy = s.Max(c => c.Tarih.Month),
                    EnSonNobetTuttuguTarih = s.Max(c => c.Tarih),
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
                        var nobetYazilanTarih = new DateTime(data.Yil, data.Ay, 1);

                        fazlaNobetTutacakEczaneler = nobetTutanEczaneler
                        .Where(w => w.NobetSayisi == kuralStd - 1
                                 && w.EnSonNobetTuttuguTarih < nobetYazilanTarih.AddMonths(-pazarNobetiYazilabilecekIlkAy)
                                 //&& w.EnSonNobetTuttuguAy < (data.Ay - pazarNobetiYazilabilecekIlkAy)
                                 )
                        .OrderBy(o => o.EnSonNobetTuttuguAy)
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
                        var pazarNobetiYazilabilecekIlkAy = (int)Math.Ceiling((double)gruptakiNobetciSayisi / 5) - 1;
                        var nobetYazilanTarih = new DateTime(data.Yil, data.Ay, 1);

                        fazlaNobetTutacakEczaneler = nobetTutanEczaneler
                        .Where(w => w.NobetSayisi == kuralStd - 1
                                 && w.EnSonNobetTuttuguTarih < nobetYazilanTarih.AddMonths(-pazarNobetiYazilabilecekIlkAy)
                                 //&& w.EnSonNobetTuttuguAy < (data.Ay - pazarNobetiYazilabilecekIlkAy)
                                 )
                        .OrderBy(o => o.EnSonNobetTuttuguAy)
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

        private NobetGunKuralDetay GetNobetGunKuralHaftaIciToplam(MersinMerkezDataModel data, int nobetGrupId, int gruptakiNobetciSayisi, int cozulenAydakiNobetSayisi)
        {
            var r = new Random();

            var eczaneler = data.EczaneKumulatifHedefler
                  .Where(w => w.NobetGrupId == nobetGrupId)
                  .OrderBy(x => r.NextDouble()).ToList();

            //eczanelerin nöbet sayıları
            var nobetTutanEczaneler = data.EczaneNobetSonuclar
                .Where(w => (w.NobetGunKuralId >= 2 && w.NobetGunKuralId <= 6)
                         && w.NobetGrupId == nobetGrupId
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
                        .Select(f => f.NobetSayisi).FirstOrDefault(),
                    EnSonNobetTuttuguAy = nobetTutanEczaneler
                        .Where(w => w.EczaneNobetGrupId == s.EczaneNobetGrupId)
                        .Select(f => f.EnSonNobetTuttuguAy).FirstOrDefault()
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
                //not: burası normalde açık. Toroslar-1 (Şehir Hast.) ve Yenişehir-2'yi iyileştirmek için kapatılarak denendi.
                kuralStd++;
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
                var fazlaNobetTutacakEczaneSayisi = cozulenAydakiNobetSayisi - eksikler.Count();// nobetiEksikEczaneler.Count();

                if (fazlaNobetTutacakEczaneSayisi > 0)// || eksigiOlanlar.Count() > 0)
                {
                    fazlaNobetTutacakEczaneler = tumEczaneler
                        .Where(w => w.NobetSayisi == nobetFrekans.Select(s => s.NobetSayisi).Max()
                                 //&& !eksikler.Contains(w.EczaneNobetGrupId)
                                 )
                        .OrderByDescending(o => o.NobetSayisi).ThenBy(c => r.NextDouble())
                        .Select(s => s.EczaneNobetGrupId)
                        .Take(fazlaNobetTutacakEczaneSayisi).ToList();
                }

                //if (cozulenAydakiEksikNobetiOlanSayisi > 0)
                //{
                //    fazlaNobetTutacakEczaneler = tumEczaneler
                //    .Where(w => w.NobetSayisi == kuralStd - 1)
                //    .OrderBy(o => r.NextDouble())
                //    .Select(s => s.EczaneNobetGrupId).Take(cozulenAydakiEksikNobetiOlanSayisi).ToList();
                //}
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

