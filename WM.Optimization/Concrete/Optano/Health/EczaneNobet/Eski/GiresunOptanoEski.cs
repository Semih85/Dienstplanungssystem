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

namespace WM.Optimization.Concrete.Optano.Health.EczaneNobet.Eski
{
    public class GiresunOptanoEski : EczaneNobetKisit, IEczaneNobetGiresunOptimization
    {
        //Karar değişkeni model çalıştıktan sonra değer aldığından burada tanımlandı
        private VariableCollection<EczaneNobetTarihAralik> _x { get; set; }

        private Model Model(GiresunDataModel data)
        {
            var model = new Model() { Name = "Giresun Eczane Nöbet" };

            #region Veriler

            #region kısıtlar

            var herAyPespeseGorev = NobetUstGrupKisit(data.Kisitlar, "herAyPespeseGorev", data.NobetUstGrupId);
            var farkliAyPespeseGorev = NobetUstGrupKisit(data.Kisitlar, "farkliAyPespeseGorev", data.NobetUstGrupId);
            var pazarPespeseGorevEnAz = NobetUstGrupKisit(data.Kisitlar, "pazarPespeseGorevEnAz", data.NobetUstGrupId);

            var herAyEnFazlaGorev = NobetUstGrupKisit(data.Kisitlar, "herAyEnFazlaGorev", data.NobetUstGrupId);
            var herAyEnFazlaHaftaIci = NobetUstGrupKisit(data.Kisitlar, "herAyEnFazlaHaftaIci", data.NobetUstGrupId);

            var haftaIciPespeseGorevEnAz = NobetUstGrupKisit(data.Kisitlar, "haftaIciPespeseGorevEnAz", data.NobetUstGrupId);

            var gunKumulatifToplamEnFazla = NobetUstGrupKisit(data.Kisitlar, "gunKumulatifToplamEnFazla", data.NobetUstGrupId);
            var herAyEnFazla1HaftaIciGunler = NobetUstGrupKisit(data.Kisitlar, "herAyEnFazla1HaftaIciGunler", data.NobetUstGrupId);

            var herAyEnFazla1Pazar = NobetUstGrupKisit(data.Kisitlar, "herAyEnFazla1Pazar", data.NobetUstGrupId);
            var herAyEnFazla1Cumartesi = NobetUstGrupKisit(data.Kisitlar, "herAyEnFazla1Cumartesi", data.NobetUstGrupId);
            var herAyEnFazla1Cuma = NobetUstGrupKisit(data.Kisitlar, "herAyEnFazla1Cuma", data.NobetUstGrupId);

            var bayramToplamEnFazla = NobetUstGrupKisit(data.Kisitlar, "bayramToplamEnFazla", data.NobetUstGrupId);
            var bayramPespeseFarkliTur = NobetUstGrupKisit(data.Kisitlar, "bayramPespeseFarkliTur", data.NobetUstGrupId);
            var herAyEnFazla1Bayram = NobetUstGrupKisit(data.Kisitlar, "herAyEnFazla1Bayram", data.NobetUstGrupId);

            var eczaneGrup = NobetUstGrupKisit(data.Kisitlar, "eczaneGrup", data.NobetUstGrupId);
            var istek = NobetUstGrupKisit(data.Kisitlar, "istek", data.NobetUstGrupId);
            var mazeret = NobetUstGrupKisit(data.Kisitlar, "mazeret", data.NobetUstGrupId);

            var pespeseHaftaIciAyniGunNobet = NobetUstGrupKisit(data.Kisitlar, "pespeseHaftaIciAyniGunNobet", data.NobetUstGrupId);
            var nobetBorcOdeme = NobetUstGrupKisit(data.Kisitlar, "nobetBorcOdeme", data.NobetUstGrupId);

            var altGruplarlaAyniGunNobetTutma = NobetUstGrupKisit(data.Kisitlar, "altGruplarlaAyniGunNobetTutma", data.NobetUstGrupId);

            //pasifler
            var herAyEnaz1Gorev = NobetUstGrupKisit(data.Kisitlar, "herAyEnaz1Gorev", data.NobetUstGrupId);

            #endregion

            #region tur çevrim katsayıları

            int bayramCevrim = 8000;
            int arifeCevrim = 5000;
            int pazarCevrim = 1000;
            int haftaIciCevrim = 500;
            #endregion

            //özel tur takibi yapılacak günler
            var ozelTurTakibiYapilacakGunler =
                new List<int> {
                    1 // pazar
                };

            var herAyEnFazlaIlgiliKisit = new NobetUstGrupKisitDetay();

            var herAyEnFazlaTakipEdilecekGunler = new List<int>
                    {
                        1, //pazar
                        7, //cts
                        6  //cuma
                    };

            var herAyEnFazlaTakipEdilecekHaftaIciGunler = new List<int>
                    {
                        5, //perşembe
                        4, //çarşamba
                        3, //salı
                        2  //pazartesi
                    };

            if (!herAyEnFazla1HaftaIciGunler.PasifMi)
                herAyEnFazlaTakipEdilecekGunler.AddRange(herAyEnFazlaTakipEdilecekHaftaIciGunler);

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

            #region hedef değişkenler
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

            var amac = new Objective(Expression.Sum(
                (from i in data.EczaneNobetTarihAralik
                 from p in data.EczaneNobetGrupGunKuralIstatistikYatay
                 where i.EczaneNobetGrupId == p.EczaneNobetGrupId
                    && i.NobetGorevTipId == p.NobetGorevTipId
                 select (_x[i]
                        //ilk yazılan nöbet öncelikli olsun:
                        + _x[i] * Convert.ToInt32(i.BayramMi)
                                * (bayramCevrim + bayramCevrim / Math.Sqrt((i.Tarih - p.SonNobetTarihiBayram).TotalDays))

                        + _x[i] * Convert.ToInt32(i.ArifeMi)
                                * (arifeCevrim + arifeCevrim / Math.Sqrt((i.Tarih - p.SonNobetTarihiArife).TotalDays))

                        + _x[i] * Convert.ToInt32(i.PazarGunuMu)
                                * (pazarCevrim + pazarCevrim / Math.Sqrt((i.Tarih - p.SonNobetTarihiPazar).TotalDays * Math.Ceiling((double)i.Tarih.Day / 7)))

                        + _x[i] * Convert.ToInt32(i.HaftaIciMi)
                                //* (haftaIciCevrim + haftaIciCevrim / Math.Sqrt((i.Tarih - p.SonNobetTarihiHaftaIci).TotalDays * i.Tarih.Day)
                                * (haftaIciCevrim + haftaIciCevrim / Math.Sqrt(
                                    //(  i.EczaneAdi == "borçlu eczane" //BADE
                                    //? (i.Tarih - p.SonNobetTarihiHaftaIci).TotalDays + 15 
                                    //: (i.Tarih - p.SonNobetTarihiHaftaIci).TotalDays)
                                    nobetBorcOdeme.PasifMi == false
                                    ? (p.BorcluNobetSayisiHaftaIci >= 0 //-5
                                        ?
                 #region Manuel borç düzeltme
                                              //(i.EczaneAdi == "SERPİL"
                                              //    ? (i.Tarih - p.SonNobetTarihiHaftaIci).TotalDays + 7
                                              // : i.EczaneAdi == "ELİFSU"
                                              //    ? (i.Tarih - p.SonNobetTarihiHaftaIci).TotalDays + p.BorcluNobetSayisiHaftaIci
                                              // : i.EczaneAdi == "KÖYÜM"
                                              //    ? (i.Tarih - p.SonNobetTarihiHaftaIci).TotalDays + 8
                                              // : i.EczaneAdi == "DOLUNAY"
                                              //    ? (i.Tarih - p.SonNobetTarihiHaftaIci).TotalDays + p.BorcluNobetSayisiHaftaIci
                                              // : i.EczaneAdi == "SUN"
                                              //    ? (i.Tarih - p.SonNobetTarihiHaftaIci).TotalDays + p.BorcluNobetSayisiHaftaIci
                                              // : i.EczaneAdi == "TATLICAN"
                                              //    ? (i.Tarih - p.SonNobetTarihiHaftaIci).TotalDays + p.BorcluNobetSayisiHaftaIci
                                              // : i.EczaneAdi == "TEZCAN"
                                              //    ? (i.Tarih - p.SonNobetTarihiHaftaIci).TotalDays + p.BorcluNobetSayisiHaftaIci
                                              // : i.EczaneAdi == "YEŞİLIRMAK"
                                              //    ? (i.Tarih - p.SonNobetTarihiHaftaIci).TotalDays + p.BorcluNobetSayisiHaftaIci
                                              //    : (i.Tarih - p.SonNobetTarihiHaftaIci).TotalDays + p.BorcluNobetSayisiHaftaIci
                                              //  ) 
                 #endregion
                                              (i.Tarih - p.SonNobetTarihiHaftaIci).TotalDays + p.BorcluNobetSayisiHaftaIci
                                        : ((i.Tarih - p.SonNobetTarihiHaftaIci).TotalDays + p.BorcluNobetSayisiHaftaIci < 1
                                            ? (i.Tarih - p.SonNobetTarihiHaftaIci).TotalDays
                                            : (i.Tarih - p.SonNobetTarihiHaftaIci).TotalDays + p.BorcluNobetSayisiHaftaIci
                                          //(i.Tarih - p.SonNobetTarihiHaftaIci).TotalDays
                                          )
                                      ) * i.Tarih.Day
                                    : (i.Tarih - p.SonNobetTarihiHaftaIci).TotalDays * i.Tarih.Day)
                                * (pespeseHaftaIciAyniGunNobet.PasifMi == false
                                ? (i.Tarih.DayOfWeek == p.SonNobetTarihiHaftaIci.DayOfWeek ? 1 : 0.2)
                                : 1)//aynı gün peşpeşe gelmesin
                                )
                        ))),
                        "Sum of all item-values: ",
                        ObjectiveSense.Minimize);
            model.AddObjective(amac);
            #endregion

            #region Kısıtlar

            foreach (var nobetGrupGorevTip in data.NobetGrupGorevTipler)
            {
                #region ön hazırlama

                #region tarihler
                var tarihler = data.TarihAraligi.Where(w => w.NobetGrupId == nobetGrupGorevTip.NobetGrupId).ToList();
                var bayramlar = tarihler.Where(w => w.NobetGunKuralId > 7).OrderBy(o => o.Tarih).ToList();
                var cumartesiGunleri = tarihler.Where(w => w.NobetGunKuralId == 7).OrderBy(o => o.Tarih).ToList();
                var pazarGunleri = tarihler.Where(w => w.NobetGunKuralId == 1).OrderBy(o => o.Tarih).ToList();
                var haftaIciGunleri = tarihler.Where(w => (w.NobetGunKuralId >= 2 && w.NobetGunKuralId <= 7)).OrderBy(o => o.Tarih).ToList();

                var gunSayisi = tarihler.Count();
                var haftaIciSayisi = haftaIciGunleri.Count();
                var pazarSayisi = pazarGunleri.Count();
                var cumartesiSayisi = cumartesiGunleri.Count();
                var bayramSayisi = bayramlar.Count();
                #endregion

                var nobetGunKurallar = tarihler.Select(s => s.NobetGunKuralId).Distinct().ToList();

                //var eczaneNobetIstekler = data.EczaneNobetIstekler.Where(x => x.NobetGrupId == nobetGrupGorevTip.NobetGrupId).ToList();
                //var eczaneNobetMazeretler = data.EczaneNobetMazeretler.Where(x => x.NobetGrupId == nobetGrupGorevTip.NobetGrupId).ToList();

                var r = new Random();

                var eczaneNobetGruplar = data.EczaneNobetGruplar
                    .Where(w => w.NobetGrupId == nobetGrupGorevTip.NobetGrupId)
                    .OrderBy(x => r.NextDouble())
                    .ToList();

                var eczaneNobetGrupGunKuralIstatistikler = data.EczaneNobetGrupGunKuralIstatistikYatay
                    .Where(w => w.NobetGrupId == nobetGrupGorevTip.NobetGrupId).ToList();

                var gruptakiEczaneSayisi = eczaneNobetGruplar.Count;

                var nobetGrupGunKurallar = data.NobetGrupGunKurallar
                                                    .Where(s => s.NobetGrupId == nobetGrupGorevTip.NobetGrupId
                                                             && nobetGunKurallar.Contains(s.NobetGunKuralId))
                                                    .Select(s => s.NobetGunKuralId).ToList();

                //var nobetGrupGorevTipler = data.NobetGrupGorevTipler.Where(w => w.NobetGrupId == nobetGrupGorevTip.NobetGrupId).ToList();

                var nobetGrupKurallar = data.NobetGrupKurallar.Where(w => w.NobetGrupId == nobetGrupGorevTip.NobetGrupId).ToList();

                var pespeseNobetSayisi = (int)nobetGrupKurallar
                        .Where(s => s.NobetKuralId == 1)
                        .Select(s => s.Deger).SingleOrDefault();

                var herAyPesPeseNobetTarihleri = tarihler.Take(tarihler.Count - pespeseNobetSayisi).ToList();

                int gunlukNobetciSayisi = (int)nobetGrupKurallar
                        .Where(s => s.NobetKuralId == 3)
                        .Select(s => s.Deger).SingleOrDefault();

                // karar değişkeni - nöbet grup bazlı filtrelenmiş
                var eczaneNobetTarihAralikGrupBazli = data.EczaneNobetTarihAralik
                           .Where(e => e.NobetGorevTipId == nobetGrupGorevTip.NobetGorevTipId
                           && e.NobetGrupId == nobetGrupGorevTip.NobetGrupId).ToList();

                var nobetGrupTalepler = data.NobetGrupTalepler.Where(w => w.NobetGrupId == nobetGrupGorevTip.NobetGrupId).ToList();

                #region haftaIciPespeseGorevEnAz

                var farkliAyPespeseGorevAraligi = (gruptakiEczaneSayisi / gunlukNobetciSayisi * 1.2);
                var altLimit = farkliAyPespeseGorevAraligi * 0.7666; //0.95
                var ustLimit = farkliAyPespeseGorevAraligi + farkliAyPespeseGorevAraligi * 0.6667; //77;
                var ustLimitKontrol = ustLimit * 0.95; //0.81 

                var pazarNobetiYazilabilecekIlkAy = (int)Math.Ceiling((double)gruptakiEczaneSayisi / 5) - 1;
                #endregion

                var haftaIciOrtamalaNobetSayisi = OrtalamaNobetSayisi(gunlukNobetciSayisi, gruptakiEczaneSayisi, haftaIciSayisi);
                var ortalamaNobetSayisi = OrtalamaNobetSayisi(gunlukNobetciSayisi, gruptakiEczaneSayisi, gunSayisi);
                var bayramOrtalamaNobetSayisi = OrtalamaNobetSayisi(gunlukNobetciSayisi, gruptakiEczaneSayisi, bayramSayisi);

                var nobetGrupBayramNobetleri = data.EczaneNobetSonuclar
                    .Where(w => w.NobetGrupId == nobetGrupGorevTip.NobetGrupId
                             && w.NobetGunKuralId > 7//bayramlar.Select(s => s.TakvimId).Contains(w.TakvimId) 
                             && w.Tarih >= data.NobetUstGrupBaslangicTarihi)
                    .Select(s => new { s.TakvimId, s.NobetGunKuralId }).ToList();

                var nobetGunKuralIstatistikler = data.TakvimNobetGrupGunDegerIstatistikler
                                      .Where(w => w.NobetGrupId == nobetGrupGorevTip.NobetGrupId
                                               && tarihler.Select(s => s.NobetGunKuralId).Distinct().Contains(w.NobetGunKuralId)).ToList();

                var nobetGunKuralTarihler = new List<NobetGunKuralTarihAralik>();

                foreach (var item in nobetGunKuralIstatistikler)
                {
                    var tarihler2 = tarihler.Where(w => w.NobetGunKuralId == item.NobetGunKuralId).ToList();
                    var gunSayisi2 = tarihler2.Count;

                    nobetGunKuralTarihler.Add(new NobetGunKuralTarihAralik
                    {
                        NobetGunKuralId = item.NobetGunKuralId,
                        NobetGunKuralAdi = item.NobetGunKuralAdi,
                        TakvimNobetGruplar = tarihler2,
                        GunSayisi = gunSayisi2,
                        OrtalamaNobetSayisi = OrtalamaNobetSayisi(gunlukNobetciSayisi, gruptakiEczaneSayisi, gunSayisi2)
                    });
                }

                #region ilk ayda fazla yazılacak eczaneler

                var fazlaYazilacakEczaneSayisi = gunSayisi - gruptakiEczaneSayisi;
                var fazlaYazilacakEczaneler = new List<EczaneNobetGrupDetay>();

                //ilk ayda eczane sayısı gün sayısından azsa, eksik sayı kadar rasgele seçilen eczaneye 1 fazla nöbet yazılır.toplam hedeflerde
                if (data.Yil == 2018 && data.Ay == 1) //&& fazlaYazilacakEczaneSayisi > 0)
                {
                    fazlaYazilacakEczaneler = eczaneNobetGruplar.OrderBy(x => r.NextDouble()).Take(fazlaYazilacakEczaneSayisi).ToList();
                }

                #endregion             

                #endregion

                //TalebiKarsila(model, eczaneNobetTarihAralikGrupBazli, gunlukNobetciSayisi, nobetGrupTalepler, nobetGrupGorevTip, tarihler, _x);
                //EczaneGrup(model, data.EczaneNobetTarihAralik, data.EczaneGrupNobetSonuclar, eczaneGrup, data.EczaneGrupTanimlar, data.EczaneGruplar, nobetGrupGorevTip, tarihler, _x);
                //IstegiKarsila(model, data.EczaneNobetTarihAralik, istek, eczaneNobetIstekler, _x);
                //MazereteGorevYazma(model, data.EczaneNobetTarihAralik, mazeret, eczaneNobetMazeretler, _x);

                foreach (var eczaneNobetGrup in eczaneNobetGruplar)
                {//eczane bazlı kısıtlar

                    #region kontrol

                    var kontrol = true;

                    if (kontrol)
                    {
                        var kontrolEdilecekEczaneler = new string[] { "CEMRE" };

                        if (kontrolEdilecekEczaneler.Contains(eczaneNobetGrup.EczaneAdi))
                        {
                        }
                    }
                    #endregion

                    #region eczane bazlı veriler

                    // karar değişkeni - eczane bazlı filtrelenmiş
                    var eczaneNobetTarihAralikEczaneBazli = data.EczaneNobetTarihAralik
                               .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                                        && e.NobetGorevTipId == nobetGrupGorevTip.NobetGorevTipId).ToList();

                    var eczaneNobetSonuclar = data.EczaneNobetSonuclar
                                    .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id).ToList();

                    var eczaneNobetIstatistik = eczaneNobetGrupGunKuralIstatistikler
                        .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id).SingleOrDefault() ?? new EczaneNobetGrupGunKuralIstatistikYatay();

                    var yazilabilecekIlkPazarTarihi = eczaneNobetIstatistik.SonNobetTarihiPazar.AddMonths(pazarNobetiYazilabilecekIlkAy - 1);
                    var yazilabilecekIlkHaftaIciTarihi = eczaneNobetIstatistik.SonNobetTarihiHaftaIci.AddDays(altLimit);
                    var nobetYazilabilecekIlkTarih = eczaneNobetIstatistik.SonNobetTarihi.AddDays(pespeseNobetSayisi);

                    #endregion

                    #region aktif kısıtlar

                    //HerAyPespeseGorev(model, eczaneNobetTarihAralikEczaneBazli, herAyPespeseGorev, herAyPesPeseNobetTarihleri, pespeseNobetSayisi, eczaneNobetGrup, _x);
                    //HerAyHaftaIciPespeseGorev(model, tarihler, haftaIciGunleri, haftaIciOrtamalaNobetSayisi, eczaneNobetGrup, eczaneNobetTarihAralikEczaneBazli, altLimit, _x, !haftaIciPespeseGorevEnAz.PasifMi);

                    //PesPeseGorevEnAz(model, eczaneNobetIstatistik.NobetSayisiToplam, eczaneNobetTarihAralikEczaneBazli, farkliAyPespeseGorev, tarihler, nobetYazilabilecekIlkTarih, eczaneNobetGrup, _x);
                    //PesPeseGorevEnAz(model, eczaneNobetIstatistik.NobetSayisiPazar, eczaneNobetTarihAralikEczaneBazli, pazarPespeseGorevEnAz, pazarGunleri, yazilabilecekIlkPazarTarihi, eczaneNobetGrup, _x);
                    //PesPeseGorevEnAz(model, eczaneNobetIstatistik.NobetSayisiHaftaIci, eczaneNobetTarihAralikEczaneBazli, haftaIciPespeseGorevEnAz, haftaIciGunleri, yazilabilecekIlkHaftaIciTarihi, eczaneNobetGrup, _x);

                    //FarkliAyPespeseGorev(model, eczaneNobetSonuclar, eczaneNobetTarihAralikEczaneBazli, farkliAyPespeseGorev, tarihler, pespeseNobetSayisi, eczaneNobetGrup, _x);
                    //PazarPespeseGorevEnAz(model, eczaneNobetSonuclar, eczaneNobetTarihAralikEczaneBazli, pazarPespeseGorevEnAz, pazarGunleri, gruptakiEczaneSayisi, eczaneNobetGrup, _x);
                    //HaftaIciPespeseGorevEnAz(model, eczaneNobetSonuclar, eczaneNobetTarihAralikEczaneBazli, haftaIciPespeseGorevEnAz, haftaIciGunleri, eczaneNobetGrup, altLimit, _x);

                    #region Tarih aralığı ortalama en fazla 

                    //HerAyEnFazlaGorev(model, herAyEnFazlaGorevSayisi, eczaneNobetGrup, eczaneNobetTarihAralikEczaneBazli, herAyEnFazlaGorev, _x);
                    //HerAyEnFazlaHaftaIci(model, haftaIciGunleri, haftaIciOrtamalaNobetSayisi, eczaneNobetGrup, eczaneNobetTarihAralikEczaneBazli, herAyEnFazlaHaftaIci, _x);
                    //HerAyEnFazla1Bayram(model, bayramlar, eczaneNobetGrup, eczaneNobetTarihAralikEczaneBazli, _x, !herAyEnFazla1Bayram.PasifMi); //
                    //TarihAraligiOrtalamaEnFazla(model, tarihler, gunSayisi, ortalamaNobetSayisi, eczaneNobetGrup, eczaneNobetTarihAralikEczaneBazli, herAyEnFazlaGorev, _x);
                    //TarihAraligiOrtalamaEnFazla(model, haftaIciGunleri, haftaIciSayisi, haftaIciOrtamalaNobetSayisi, eczaneNobetGrup, eczaneNobetTarihAralikEczaneBazli, herAyEnFazlaHaftaIci, _x);
                    //TarihAraligiOrtalamaEnFazla(model, bayramlar, bayramSayisi, bayramOrtalamaNobetSayisi, eczaneNobetGrup, eczaneNobetTarihAralikEczaneBazli, herAyEnFazla1Bayram, _x);

                    if (!herAyEnFazla1HaftaIciGunler.PasifMi)
                        herAyEnFazlaTakipEdilecekGunler.AddRange(herAyEnFazlaTakipEdilecekHaftaIciGunler);

                    foreach (var nobetGunKuralId in herAyEnFazlaTakipEdilecekGunler)
                    {
                        if (kontrol && herAyEnFazlaTakipEdilecekHaftaIciGunler.Contains(nobetGunKuralId))
                        {
                        }

                        //if ((nobetGunKuralId == 1 && !herAyEnFazla1Pazar.PasifMi)
                        // || (nobetGunKuralId == 7 && !herAyEnFazla1Cumartesi.PasifMi)
                        // || (nobetGunKuralId == 6 && !herAyEnFazla1Cuma.PasifMi)
                        // || (herAyEnFazlaTakipEdilecekHaftaIciGunler.Contains(nobetGunKuralId) && !herAyEnFazla1HaftaIciGunler.PasifMi)
                        // )
                        //{
                        switch (nobetGunKuralId)
                        {
                            case 1:
                                herAyEnFazlaIlgiliKisit = herAyEnFazla1Pazar;
                                break;
                            case 7:
                                herAyEnFazlaIlgiliKisit = herAyEnFazla1Cumartesi;
                                break;
                            case 6:
                                herAyEnFazlaIlgiliKisit = herAyEnFazla1Cuma;
                                break;
                            default:
                                herAyEnFazlaIlgiliKisit = herAyEnFazla1HaftaIciGunler;
                                break;
                        }

                        //var gunKuralTarihler = tarihler.Where(e => e.NobetGunKuralId == nobetGunKuralId).ToList();
                        //var gunKuralSayisi = gunKuralTarihler.Count;
                        //var gunKuralOrtalamaNobetSayisi = OrtalamaNobetSayisi(gunlukNobetciSayisi, gruptakiEczaneSayisi, gunKuralSayisi);
                        //TarihAraligiOrtalamaEnFazla(model, gunKuralTarihler, gunKuralSayisi, gunKuralOrtalamaNobetSayisi, eczaneNobetGrup, eczaneNobetTarihAralikEczaneBazli, herAyEnFazlaIlgiliKisit, _x);
                        var tarihAralik = nobetGunKuralTarihler.Where(w => w.NobetGunKuralId == nobetGunKuralId).SingleOrDefault() ?? new NobetGunKuralTarihAralik();
                        //TarihAraligiOrtalamaEnFazla(model, tarihAralik.TakvimNobetGruplar, tarihAralik.GunSayisi, tarihAralik.OrtalamaNobetSayisi, eczaneNobetGrup, eczaneNobetTarihAralikEczaneBazli, herAyEnFazlaIlgiliKisit, _x);
                        //}
                    }

                    #endregion

                    #region Kümülatif toplam en fazla - Tur takip kısıtı

                    if (!gunKumulatifToplamEnFazla.PasifMi)
                    {
                        //var kumulatifGunSayisi = nobetGunKuralIstatistikler.Sum(s => s.GunSayisi);
                        //var kumulatifOrtalamaGunSayisi = Math.Ceiling(((double)kumulatifGunSayisi * gunlukNobetciSayisi) / gruptakiEczaneSayisi);

                        //KumulatifToplamEnFazla(model, tarihler, eczaneNobetGrup, eczaneNobetTarihAralikEczaneBazli, gunKumulatifToplamEnFazla, kumulatifOrtalamaGunSayisi, eczaneNobetIstatistik.NobetSayisiToplam, _x);

                        foreach (var gunKural in nobetGunKuralIstatistikler)
                        {
                            var kontrol2 = false;

                            if (kontrol2 && gunKural.NobetGunKuralAdi == "Pazartesi")
                            {//kontrol
                            }

                            var kumulatifOrtalamaGunKuralSayisi = Math.Ceiling(((double)gunKural.GunSayisi * gunlukNobetciSayisi) / gruptakiEczaneSayisi);

                            int toplamNobetSayisi = GetToplamGunKuralNobetSayisi(eczaneNobetIstatistik, gunKural.NobetGunKuralId);

                            //var toplamNobetSayisi = eczaneNobetSonuclar
                            //                            .Where(w => w.NobetGunKuralId == gunKural.NobetGunKuralId
                            //                                     && w.Tarih >= data.NobetUstGrupBaslangicTarihi)
                            //                            .Select(s => s.TakvimId).Count();

                            if (data.CalismaSayisi >= 2 && !nobetGrupGunKurallar.Contains(gunKural.NobetGunKuralId))
                                kumulatifOrtalamaGunKuralSayisi++; //06.10.2018'de 2.denemede ortalamayı zaten 1 artırdığı için kapattım

                            if (gunKumulatifToplamEnFazla.SagTarafDegeri > 0 && !ozelTurTakibiYapilacakGunler.Contains(gunKural.NobetGunKuralId))
                                kumulatifOrtalamaGunKuralSayisi = gunKumulatifToplamEnFazla.SagTarafDegeri;

                            if (toplamNobetSayisi > kumulatifOrtalamaGunKuralSayisi)
                                kumulatifOrtalamaGunKuralSayisi = toplamNobetSayisi;

                            if (data.CalismaSayisi >= 1 && !ozelTurTakibiYapilacakGunler.Contains(gunKural.NobetGunKuralId))
                                kumulatifOrtalamaGunKuralSayisi++;

                            //var tarihler2 = tarihler.Where(w => w.NobetGunKuralId == gunKural.NobetGunKuralId).ToList();
                            //KumulatifToplamEnFazla(model, tarihler2, eczaneNobetGrup, eczaneNobetTarihAralikEczaneBazli, gunKumulatifToplamEnFazla, kumulatifOrtalamaGunKuralSayisi, toplamNobetSayisi, _x);
                            var tarihAralik = nobetGunKuralTarihler.Where(w => w.NobetGunKuralId == gunKural.NobetGunKuralId).SingleOrDefault() ?? new NobetGunKuralTarihAralik();
                            //KumulatifToplamEnFazla(model, tarihAralik.TakvimNobetGruplar, eczaneNobetGrup, eczaneNobetTarihAralikEczaneBazli, gunKumulatifToplamEnFazla, kumulatifOrtalamaGunKuralSayisi, toplamNobetSayisi, _x);
                        }
                    }
                    #endregion

                    #region Bayram kümülatif toplam en fazla ve farklı tür bayram

                    if (bayramlar.Count() > 0)
                    {
                        var bayramNobetleri = eczaneNobetSonuclar
                                .Where(w => w.NobetGunKuralId > 7
                                         && w.Tarih >= data.NobetUstGrupBaslangicTarihi)
                                .Select(s => new { s.TakvimId, s.NobetGunKuralId }).ToList();

                        var bayramNobetleriAnahtarli = eczaneNobetSonuclar
                                .Where(w => w.NobetGunKuralId > 7)
                                .Select(s => new { s.TakvimId, s.NobetGunKuralId }).ToList();

                        var toplamBayramNobetSayisi = bayramNobetleri.Count();

                        if (!bayramToplamEnFazla.PasifMi)
                        {
                            var bayramGunKuralIstatistikler = nobetGunKuralIstatistikler
                                                            .Where(w => w.NobetGunKuralId > 7).ToList();

                            var toplamBayramGrupToplamNobetSayisi = nobetGrupBayramNobetleri.Count();

                            var kumulatifBayramSayisi = bayramGunKuralIstatistikler.Sum(x => x.GunSayisi);

                            var yillikOrtalamaGunKuralSayisi = Math.Ceiling((double)kumulatifBayramSayisi / gruptakiEczaneSayisi);

                            if (kumulatifBayramSayisi == gruptakiEczaneSayisi)
                            {
                                //yillikOrtalamaGunKuralSayisi++;
                            }

                            if (toplamBayramGrupToplamNobetSayisi == gruptakiEczaneSayisi)
                            {
                                //yillikOrtalamaGunKuralSayisi++;
                            }

                            if (yillikOrtalamaGunKuralSayisi < 1) yillikOrtalamaGunKuralSayisi = 0;

                            if (toplamBayramNobetSayisi > yillikOrtalamaGunKuralSayisi)
                                yillikOrtalamaGunKuralSayisi = toplamBayramNobetSayisi;

                            if (bayramToplamEnFazla.SagTarafDegeri > 0)
                                yillikOrtalamaGunKuralSayisi = bayramToplamEnFazla.SagTarafDegeri;

                            var sonBayram = bayramNobetleriAnahtarli.LastOrDefault();

                            if (sonBayram != null)
                            {
                                var sonBayramTuru = bayramNobetleriAnahtarli.LastOrDefault().NobetGunKuralId;

                                if (bayramlar.Select(s => s.NobetGunKuralId).Contains(sonBayramTuru) && !bayramPespeseFarkliTur.PasifMi)
                                {
                                    //PespeseFarkliTurNobetYaz(model, bayramlar, eczaneNobetGrup, eczaneNobetTarihAralikEczaneBazli, sonBayramTuru, _x);
                                }
                                else
                                {
                                    //KumulatifToplamEnFazla(model, bayramlar, eczaneNobetGrup, eczaneNobetTarihAralikEczaneBazli, bayramToplamEnFazla, yillikOrtalamaGunKuralSayisi, toplamBayramNobetSayisi, _x);
                                    //BayramToplamEnFazla(model, bayramlar, eczaneNobetGrup, eczaneNobetTarihAralikEczaneBazli, toplamBayramNobetSayisi, yillikOrtalamaGunKuralSayisi, _x);
                                }
                            }

                            //model.AddConstraint(
                            //  Expression.Sum(data.EczaneNobetTarihAralik
                            //                .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                            //                         && e.GunDegerId > 7)
                            //                .Select(m => _x[m])) + toplamBayramNobetSayisi <= toplamBayramMax,
                            //                $"her eczaneye bir ayda nobet grubunun hedefi kadar toplam bayram nobeti yazilmali, {eczaneNobetGrup}");
                        }
                    }
                    #endregion

                    #endregion

                    #region pasif kısıtlar

                    #region Her ay en az 1 görev

                    //HerAyEnAz1Gorev(model, herAyEnaz1Gorev, tarihler, gruptakiEczaneSayisi, eczaneNobetGrup, eczaneNobetTarihAralikEczaneBazli, _x);
                    //TarihAraligindaEnAz1NobetYaz(model, herAyEnaz1Gorev, tarihler, gruptakiEczaneSayisi, eczaneNobetGrup, eczaneNobetTarihAralikEczaneBazli, _x);

                    #region eski
                    //var herAyEnaz1Gorev = data.Kisitlar
                    //                .Where(s => s.KisitAdi == "herAyEnaz1Gorev"
                    //                         && s.NobetUstGrupId == data.NobetUstGrupId)
                    //                .Select(w => w.PasifMi == false).SingleOrDefault();

                    //if (herAyEnaz1Gorev && gruptakiEczaneSayisi < tarihler.Count)
                    //{
                    //    var herAyEnaz1GorevSTD = data.Kisitlar
                    //                            .Where(s => s.KisitAdi == "herAyEnaz1Gorev"
                    //                                     && s.NobetUstGrupId == data.NobetUstGrupId)
                    //                            .Select(w => w.SagTarafDegeri).SingleOrDefault();

                    //    model.AddConstraint(
                    //          Expression.Sum(data.EczaneNobetTarihAralik
                    //                            .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                    //                                     && e.NobetGorevTipId == 1//eczaneNobetGrup.NobetGorevTipId
                    //                                     )
                    //                            .Select(m => _x[m])) >= herAyEnaz1GorevSTD,
                    //                            $"her eczaneye bir ayda en az nobet grubunun hedefinden bir eksik nobet yazilmali, {eczaneNobetGrup}");
                    //} 
                    #endregion

                    #endregion

                    #region Bayram toplam en az

                    if (bayramlar.Count() > 0)
                    {
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
                                            .Select(m => _x[m])) + toplamBayramNobetSayisi >= toplamBayramMin,
                                            $"her eczaneye bir ayda nobet grubunun hedefi kadar toplam bayram nobeti yazilmali, {eczaneNobetGrup}");
                        }
                    }
                    #endregion

                    #region Hafta içi toplam max hedefler

                    //var haftaIciToplamMaxHedef = data.Kisitlar
                    //                            .Where(s => s.KisitAdi == "haftaIciToplamMaxHedef"
                    //                                     && s.NobetUstGrupId == data.NobetUstGrupId)
                    //                            .Select(w => w.PasifMi == false).SingleOrDefault();

                    //var haftaIciToplamNobetSayisi = data.EczaneNobetSonuclar
                    //     .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id
                    //              && w.NobetGorevTipId == 1//eczaneNobetGrup.NobetGorevTipId
                    //              && (w.NobetGunKuralId >= 2 && w.NobetGunKuralId <= 7)
                    //              )
                    //     .Select(s => s.TakvimId).Count();

                    //if (haftaIciToplamMaxHedef)
                    //{
                    //    var haftaIciDurum = nobetGunKuralDetaylar.Where(w => w.GunKuralId == 0).FirstOrDefault();

                    //    var Std = haftaIciDurum.KuralSTD;

                    //    //kontrol edilecek eczane nöbet grup id'ler
                    //    var kontrolListesi = new int[] { 135, 146 };
                    //    if (kontrolListesi.Contains(eczaneNobetGrup.Id))
                    //    {
                    //    }

                    //    if (!haftaIciDurum.FazlaNobetTutacakEczaneler.Contains(eczaneNobetGrup.Id) && haftaIciDurum.FazlaNobetTutacakEczaneler.Count > 0)
                    //    {
                    //        Std = haftaIciDurum.KuralSTD - 1;
                    //    }

                    //    var haftaIciToplamMaxHedefStd = data.Kisitlar
                    //        .Where(s => s.KisitAdi == "haftaIciToplamMaxHedef"
                    //                 && s.NobetUstGrupId == data.NobetUstGrupId)
                    //        .Select(w => w.SagTarafDegeri).SingleOrDefault();

                    //    if (haftaIciToplamMaxHedefStd > 0) Std = haftaIciToplamMaxHedefStd;

                    //    if (haftaIciToplamNobetSayisi <= Std) //&& !haftaIciDurum.FazlaNobetTutacakEczaneler.Contains(eczaneNobetGrup.Id)
                    //    {
                    //        //if (data.CalismaSayisi < 7)
                    //        //{
                    //        model.AddConstraint(
                    //             Expression.Sum(data.EczaneNobetTarihAralik
                    //                       .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                    //                               && e.NobetGorevTipId == 1//eczaneNobetGrup.NobetGorevTipId
                    //                               && haftaIciGunleri.Select(s => s.TakvimId).Contains(e.TakvimId)
                    //                               )
                    //                       .Select(m => _x[m])) + haftaIciToplamNobetSayisi <= Std,
                    //                       $"her eczaneye bir ayda nobet grubunun hedefi kadar toplam hafta içi nobeti yazilmali, {eczaneNobetGrup}");
                    //        //}
                    //        //else
                    //        //{
                    //        //    model.AddConstraint(
                    //        //     Expression.Sum(from i in data.EczaneNobetTarihAralik
                    //        //                    from j in data.EczaneNobetGruplar
                    //        //                    where i.EczaneNobetGrupId == j.Id
                    //        //                          && i.EczaneNobetGrupId == hedef.EczaneNobetGrupId
                    //        //                          && i.NobetGorevTipId == hedef.NobetGorevTipId
                    //        //                          && tarihlerHaftaIci.Select(s => s.TakvimId).Contains(i.TakvimId)
                    //        //                    select (_x[i] + _hHiciNS[j] - _hHiciPS[j])) + haftaIciToplamNobetSayisi == Std,
                    //        //                     $"her eczaneye bir ayda nöbet grubunun {1} hedefi kadar toplam hafta içi nobeti yazılmalı, {haftaIciToplam}");
                    //        //}
                    //    }
                    //    else
                    //    {
                    //        model.AddConstraint(
                    //             Expression.Sum(from i in data.EczaneNobetTarihAralik
                    //                            where i.EczaneNobetGrupId == eczaneNobetGrup.Id
                    //                                  && i.NobetGorevTipId == 1//eczaneNobetGrup.NobetGorevTipId
                    //                                  && haftaIciGunleri.Select(s => s.TakvimId).Contains(i.TakvimId)
                    //                            select (_x[i])) == 0,
                    //                             $"her eczaneye bir ayda nöbet grubunun {1} hedefi kadar toplam hafta içi nobeti yazılmalı, {eczaneNobetGrup.Id}");
                    //    }
                    //}
                    #endregion

                    #region Hafta içi toplam min hedefler

                    //var haftaIciToplamMinHedef = data.Kisitlar
                    //                    .Where(s => s.KisitAdi == "haftaIciToplamMinHedef"
                    //                             && s.NobetUstGrupId == data.NobetUstGrupId)
                    //                    .Select(w => w.PasifMi == false).SingleOrDefault();

                    //if (haftaIciToplamMinHedef)
                    //{
                    //    var hedef = data.EczaneKumulatifHedefler.Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id).SingleOrDefault();
                    //    var haftaIciMinToplam = hedef.ToplamHaftaIci - 1;

                    //    var haftaIciToplamMinHedefStd = (int)data.Kisitlar
                    //          .Where(s => s.KisitAdi == "haftaIciToplamMinHedef"
                    //                   && s.NobetUstGrupId == data.NobetUstGrupId)
                    //          .Select(w => w.SagTarafDegeri).SingleOrDefault();

                    //    if (haftaIciToplamMinHedefStd > 0) haftaIciMinToplam = haftaIciToplamMinHedefStd;

                    //    model.AddConstraint(
                    //      Expression.Sum(data.EczaneNobetTarihAralik
                    //                    .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                    //                            && haftaIciGunleri.Select(s => s.TakvimId).Contains(e.TakvimId)
                    //                            )
                    //                    .Select(m => _x[m])) + haftaIciToplamNobetSayisi >= haftaIciMinToplam,
                    //                    $"her eczaneye bir ayda nobet grubunun hedefi kadar toplam hafta içi nobeti yazilmali, {hedef}");
                    //}
                    #endregion

                    #region Farklı aylar peşpeşe görev yazılmasın (Pazar peşpeşe)

                    //var pazarPespeseGorev0 = data.Kisitlar
                    //                        .Where(s => s.KisitAdi == "pazarPespeseGorev0"
                    //                                 && s.NobetUstGrupId == data.NobetUstGrupId)
                    //                        .Select(w => w.PasifMi == false).SingleOrDefault();

                    //if (pazarPespeseGorev0)
                    //{
                    //    var enSonPazarTuttuguNobetAyi = data.EczaneNobetSonuclar
                    //        .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id
                    //                 && w.NobetGunKuralId == 1)
                    //        .Select(s => s.Tarih.Month).OrderByDescending(o => o).FirstOrDefault();

                    //    var pazarNobetiYazilabilecekIlkAy = Math.Ceiling((double)gruptakiEczaneSayisi / 5) - 1;
                    //    var pazarNobetiYazilabilecekSonAy = Math.Ceiling((double)gruptakiEczaneSayisi / 4) + 1;//+2

                    //    var yazilabilecekIlkTarih = enSonPazarTuttuguNobetAyi + pazarNobetiYazilabilecekIlkAy;
                    //    var yazilabilecekSonTarih = enSonPazarTuttuguNobetAyi + pazarNobetiYazilabilecekSonAy;

                    //    //if (data.CalismaSayisi == 5) yazilabilecekSonTarih = yazilabilecekSonTarih++;

                    //    var kontrolListesi = new int[] { 412, 326 };
                    //    if (kontrolListesi.Contains(eczaneNobetGrup.Id))
                    //    {
                    //    }

                    //    if (enSonPazarTuttuguNobetAyi > 0)
                    //    {
                    //        foreach (var g in tarihler.Where(w => !(w.Ay >= yazilabilecekIlkTarih && w.Ay <= yazilabilecekSonTarih)
                    //                                                        && w.NobetGunKuralId == 1))
                    //        {
                    //            model.AddConstraint(
                    //              Expression.Sum(data.EczaneNobetTarihAralik
                    //                               .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                    //                                           && e.TakvimId == g.TakvimId
                    //                                           && e.NobetGrupId == nobetGrupGorevTip.NobetGrupId
                    //                                           )
                    //                               .Select(m => _x[m])) == 0,
                    //                               $"eczanelere_farkli_aylarda_pazar_gunleri_pespese_nobet_yazilmasin, {eczaneNobetGrup.Id}");
                    //        }

                    //        var periyottakiNobetSayisi = data.EczaneNobetSonuclar
                    //                   .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id
                    //                            && w.NobetGorevTipId == 1//eczaneNobetGrup.NobetGorevTipId
                    //                            && (w.Tarih.Month >= yazilabilecekIlkTarih + 1 && w.Tarih.Month <= yazilabilecekSonTarih)
                    //                            && w.NobetGunKuralId == 1)
                    //                   .Select(s => s.TakvimId).Count();

                    //        //periyodu başladıktan 1 ay sonra hala pazar nöbeti yazılmamışsa yazılsın.
                    //        if (enSonPazarTuttuguNobetAyi < data.Ay - yazilabilecekIlkTarih //- 1
                    //            && periyottakiNobetSayisi == 0
                    //            )
                    //        {
                    //            model.AddConstraint(
                    //              Expression.Sum(data.EczaneNobetTarihAralik
                    //                               .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                    //                                           && e.NobetGorevTipId == 1//eczaneNobetGrup.NobetGorevTipId
                    //                                           && pazarGunleri.Select(s => s.TakvimId).Contains(e.TakvimId)
                    //                                           )
                    //                               .Select(m => _x[m])) == 1,
                    //                               $"eczanelere_farkli_aylarda_pazar_gunu_pespese_nobet_yazilmasin, {eczaneNobetGrup}");
                    //        }
                    //    }
                    //}
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

                            //var pazarNobetiYazilabilecekIlkAy = (int)Math.Ceiling((double)gruptakiEczaneSayisi / 5) - 1;
                            var pazarNobetiYazilabilecekSonAy = (int)Math.Ceiling((double)gruptakiEczaneSayisi / 4) + 1;

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
                                                               && e.NobetGrupId == nobetGrupGorevTip.NobetGrupId
                                                               )
                                                   .Select(m => _x[m])) == 0,
                                                   $"eczanelere_farkli_aylarda_pazar_gunu_pespese_nobet_yazilmasin, {eczaneNobetGrup.Id}");
                            }

                            //var tarihler = tarihler
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
                            //                       .Select(m => _x[m])) == 1,
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
                                        .Select(m => _x[m])) <= cumaCumartesiToplam,
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
                                        .Select(m => _x[m])) >= cumaCumartesiToplam,
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
                                               .Select(m => _x[m])) + toplamCumaSayisi <= toplamCumaMaxHedefSTD,
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

                        foreach (var g in tarihler.Where(w => w.NobetGunKuralId == 7))
                        {
                            model.AddConstraint(
                              Expression.Sum(data.EczaneNobetTarihAralik
                                               .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id
                                                           && e.TakvimId == g.TakvimId)
                                               .Select(m => _x[m])) + toplamCumartesiSayisi <= toplamCumartesiMaxHedefSTD,
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
                                          .Select(m => _x[m])) + toplamNobetSayisi <= maxToplam,
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
                                            .Select(m => _x[m])) + toplamNobetSayisi >= minToplam,
                                            $"her_eczaneye_ayda_en_az_nobet_grubunun_hedefinden_1_eksik_nobet_yazilmali, {hedef}");
                    }
                    #endregion

                    #endregion
                }
            }

            //EczaneGrupCokluCozum(model, data.EczaneNobetTarihAralik, eczaneGrup, data.EczaneGrupTanimlar, data.EczaneGruplar, data.NobetGrupGorevTipler, data.TarihAraligi, _x);
            //EsGruptakiEczanelereAyniGunNobetYazma(model, data.EczaneNobetTarihAralik, data.EczaneGrupNobetSonuclar, eczaneGrup, data.EczaneGruplar, //data.NobetGrupGorevTipler, 
            //    data.TarihAraligi, _x);
            //IstegiKarsila(model, data.EczaneNobetTarihAralik, istek, data.EczaneNobetIstekler, _x);
            //MazereteGorevYazma(model, data.EczaneNobetTarihAralik, mazeret, data.EczaneNobetMazeretler, _x);

            #endregion

            return model;
        }

        public EczaneNobetSonucModel Solve(GiresunDataModel data)
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

                    //var confilicts = solution.ConflictingSet;
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
                            CalismaSayisi = data.CalismaSayisi,
                            NobetGrupSayisi = data.NobetGruplar.Count,
                            IncelenenEczaneSayisi = data.EczaneNobetGruplar.Count
                        };

                        var sonuclar = data.EczaneNobetTarihAralik.Where(s => _x[s].Value == 1).ToList();
                        var sonuclar2 = data.EczaneNobetTarihAralik.Where(s => _x[s].Value != 1).ToList();

                        var nobetGrupTarihler = data.EczaneNobetTarihAralik.Select(s => new { s.NobetGrupId, s.Tarih, s.NobetGorevTipId }).Distinct().ToList();

                        //if (sonuclar.Count != nobetGrupTarihler.Count)
                        //{
                        //    throw new Exception("Talebi karşılanmayan günler var");
                        //}

                        foreach (var r in sonuclar //data.EczaneNobetTarihAralik.Where(s => _x[s].Value == 1)
                            )
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
            catch (NullReferenceException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                if (data.CalismaSayisi <= 2)//10
                {
                    Results = Solve(data);
                }
                else
                {//çözüm yok
                    var mesaj = ex.Message;

                    var iterasyonMesaj = "";
                    if (data.CozumItereasyon.IterasyonSayisi > 0)
                    {
                        iterasyonMesaj = $"<br/>Aynı gün nöbet: Çözüm esnasında ay içinde aynı gün nöbet tutan eczaneler;" +
                            $"<br/>İterasyon sayısı: <strong>{data.CozumItereasyon.IterasyonSayisi}</strong>."
                            //+ $"<br/>Elenen eczane sayısı: <strong>{data.AyIcindeAyniGunNobetTutanEczaneler.Count}</strong>."
                            ;
                    }

                    string cozulenNobetGruplar = null;

                    var ilkGrup = data.NobetGruplar.Select(s => s.Adi).FirstOrDefault();

                    foreach (var i in data.NobetGruplar.Select(s => s.Adi))
                    {
                        if (i == ilkGrup)
                        {
                            cozulenNobetGruplar += i;
                        }
                        else
                        {
                            cozulenNobetGruplar += $", {i}";
                        }
                    }

                    mesaj = $"Nöbet yazdırma seçenekleri: " +
                        $"<br/>Nöbet Grupları: {cozulenNobetGruplar}" +
                        $"<br/>Tarih Aralığı: <strong>{data.BaslangicTarihi.ToShortDateString()}-{data.BitisTarihi.ToShortDateString()}</strong>" +
                        $"<br/>Farklı deneme sayısı: <strong>{data.CalismaSayisi - 1}</strong>" +
                        $"{iterasyonMesaj} " +
                        $"<br/>Sonuç: <strong><span class='text-danger'>Uygun çözüm alanı olmadığından</span> mevcut kısıtlarla çözüm bulunamamıştır.</strong> " +
                        $"<br/>Lütfen <a href=\"/EczaneNobet/NobetUstGrupKisit/KisitAyarla\" class=\"card-link\" target=\"_blank\">Nöbet Ayarlarını</a> kontrol edip tekrar deneyiniz..";

                    var calismaAdimlari = new string[10]
                        {
                            "Çözüm bulunamadı.",
                            //"Tekrar çözüm denendi.",
                            "kumulatifOrtalamaGunKuralSayisi 1 artırıldı.",
                            "haftaIciOrtalama (satır ortalaması) 1 artırıldı.",
                            "haftaIciOrtalama (satır ortalaması) 2 artırıldı.",
                            "Ayda en fazla 1 gorev kaldırıldı!",
                            "Farklı ay peşpeşe görev gevşetildi!",
                            "Ayda en fazla 1 gorev kaldırıldı ve Farklı Ay Peşpeşe Görev gevşetildi!",
                            "Cuma ve cumartesi en fazla 3 olmadı 4 olarak gevşetildi!",
                            "Farklı ay peşpeşe görev sayısı en çok 5 olarak gevşetildi!",
                            "default"
                        };

                    for (int i = 0; i < data.CalismaSayisi; i++)
                    {
                        mesaj += "<br/> " + i + " " + calismaAdimlari[i];
                    }

                    throw new Exception(mesaj);
                }
            }
            return Results;
        }

    }
}
