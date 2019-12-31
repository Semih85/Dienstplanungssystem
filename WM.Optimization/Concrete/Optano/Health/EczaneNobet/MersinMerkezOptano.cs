using OPTANO.Modeling.Common;
using OPTANO.Modeling.Optimization;
using OPTANO.Modeling.Optimization.Configuration;
using OPTANO.Modeling.Optimization.Enums;
using OPTANO.Modeling.Optimization.Exporter;
using OPTANO.Modeling.Optimization.Solver;
using OPTANO.Modeling.Optimization.Solver.Cplex128;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.Optimization.EczaneNobet;
using WM.Optimization.Abstract.Health;
using WM.Optimization.Entities.KisitParametre;

namespace WM.Optimization.Concrete.Optano.Health.EczaneNobet
{
    public class MersinMerkezOptano : EczaneNobetKisit, IEczaneNobetMersinMerkezOptimizationV2
    {
        #region local değişkenler
        //Karar değişkeni model çalıştıktan sonra değer aldığından burada tanımlandı
        private VariableCollection<EczaneNobetTarihAralik> _x { get; set; }
        private VariableCollection<EczaneNobetAltGrupTarihAralik> y { get; set; }
        //private CplexSolverConfiguration _solverConfig;
        //private CplexSolver _solver;
        //private Model model;
        //private Configuration _configuration;

        #endregion

        private Model Model(MersinMerkezDataModelV2 data)
        {
            var model = new Model() { Name = "Mersin Eczane Nöbet" };

            #region veriler

            #region kısıtlar

            var pespeseHaftaIciAyniGunNobet = NobetUstGrupKisit(data.Kisitlar, "pespeseHaftaIciAyniGunNobet", data.NobetUstGrupId);
            var nobetBorcOdeme = NobetUstGrupKisit(data.Kisitlar, "nobetBorcOdeme", data.NobetUstGrupId);

            //pasifler
            //var performansNoktasi_2 = new PerformansTakip { PerformansNoktasi = "2: Kisitlar", Sure = stopwatch.Elapsed };
            //performansTakip.Add(performansNoktasi_2);
            #endregion

            #region tur çevrim katsayıları            

            //int haftaIciCevrim = 500;
            //int cumartesiCevrim = 900;
            //int bayramCevrim = 8000;
            //int pazarCevrim = 1000;

            //var knt = data.EczaneNobetGrupGunKuralIstatistikler
            //    .Where(w => w.EczaneNobetGrupId == 415).ToList();
            #endregion

            //özel tur takibi yapılacak günler
            var ozelTurTakibiYapilacakGunler =
                new List<int> {
                    1 // pazar
                };

            var herAyEnFazlaIlgiliKisit = new NobetUstGrupKisitDetay();
            var kumulatifEnFazlaIlgiliGunkural = new NobetUstGrupKisitDetay();

            var bayramlarTumu = data.TarihAraligi.Where(w => w.GunGrupId == 1).OrderBy(o => o.Tarih).ToList();
            var pazarGunleriTumu = data.TarihAraligi.Where(w => w.NobetGunKuralId == 1).OrderBy(o => o.Tarih).ToList();
            var haftaIciGunleriTumu = data.TarihAraligi.Where(w => w.GunGrupId == 3).OrderBy(o => o.Tarih).ToList();
            var cumartesiGunleriTumu = data.TarihAraligi.Where(w => w.NobetGunKuralId == 7).OrderBy(o => o.Tarih).ToList();

            #region Karar Değişkenleri
            _x = new VariableCollection<EczaneNobetTarihAralik>(
                    model,
                    data.EczaneNobetTarihAralik,
                    "x",
                    t => $"{t.EczaneNobetGrupId}_{t.TakvimId}, {t.NobetGrupAdi}, {t.EczaneAdi}, {t.Tarih.ToShortDateString()}",
                    h => 0,
                    h => 1,
                    a => VariableType.Binary);

            //y = new VariableCollection<EczaneNobetAltGrupTarihAralik>(
            //        model,
            //        data.EczaneNobetAltGrupTarihAralik,
            //        "y",
            //        null,
            //        h => 0,
            //        h => 1,
            //        a => VariableType.Binary);

            #endregion

            #endregion

            #region Amaç Fonksiyonu

            var amac1 = Expression.Sum(from i in data.EczaneNobetTarihAralik
                                       select (_x[i] * i.AmacFonksiyonKatsayi));

            //var amac1 = Expression.Sum(
            //                    (from i in data.EczaneNobetTarihAralik
            //                     from p in data.EczaneNobetGrupGunKuralIstatistikYatay
            //                     where i.EczaneNobetGrupId == p.EczaneNobetGrupId
            //                        && i.NobetGorevTipId == p.NobetGorevTipId
            //                     select (_x[i]
            //                            //ilk yazılan nöbet öncelikli olsun:
            //                            + _x[i] * Convert.ToInt32(i.BayramMi)
            //                                    * (bayramCevrim + bayramCevrim / Math.Sqrt((i.Tarih - p.SonNobetTarihiBayram).TotalDays))

            //                            + _x[i] * Convert.ToInt32(i.CumartesiGunuMu)
            //                                    * (cumartesiCevrim + cumartesiCevrim / Math.Sqrt((i.Tarih - p.SonNobetTarihiCumartesi).TotalDays * Math.Ceiling((double)i.Tarih.Day / 7)))

            //                            + _x[i] * Convert.ToInt32(i.PazarGunuMu)
            //                                    * (pazarCevrim + pazarCevrim / Math.Sqrt((i.Tarih - p.SonNobetTarihiPazar).TotalDays * Math.Ceiling((double)i.Tarih.Day / 7)))

            //                            + _x[i] * Convert.ToInt32(i.HaftaIciMi)
            //                                    * (haftaIciCevrim + haftaIciCevrim / Math.Sqrt((i.Tarih - p.SonNobetTarihiHaftaIci).TotalDays * i.Tarih.Day)
            //                                    * (pespeseHaftaIciAyniGunNobet.PasifMi == false
            //                                    ? (i.Tarih.DayOfWeek == p.SonNobetTarihiHaftaIci.DayOfWeek ? 1 : 0.2)
            //                                    : 1)//aynı gün peşpeşe gelmesin
            //                                    )
            //                            )));

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

            foreach (var nobetGrupGorevTip in data.NobetGrupGorevTipler)
            {
                #region kısıtlar grup bazlı

                var kisitlarGrupBazli = data.NobetGrupGorevTipKisitlar.Where(w => w.NobetGrupGorevTipId == nobetGrupGorevTip.Id).ToList();

                var kisitlarAktif = GetKisitlarNobetGrupBazli(data.Kisitlar, kisitlarGrupBazli);

                #endregion

                #region ön hazırlama

                var nobetGrupKurallar = data.NobetGrupKurallar.Where(w => w.NobetGrupId == nobetGrupGorevTip.NobetGrupId).ToList();

                var pespeseNobetSayisi = (int)GetNobetGunKural(nobetGrupKurallar, 1);
                var gunlukNobetciSayisi = (int)GetNobetGunKural(nobetGrupKurallar, 3);
                var pespeseNobetSayisiHaftaIci = (int)GetNobetGunKural(nobetGrupKurallar, 5);
                var pespeseNobetSayisiPazar = (int)GetNobetGunKural(nobetGrupKurallar, 6);
                var pespeseNobetSayisiCumartesi = (int)GetNobetGunKural(nobetGrupKurallar, 7);

                //var nobetGrupTalepler = data.NobetGrupTalepler.Where(w => w.NobetGrupId == nobetGrupGorevTip.NobetGrupId).ToList();

                #region tarihler

                var tarihler = data.TarihAraligi
                    .Where(w => w.NobetGrupId == nobetGrupGorevTip.NobetGrupId
                             && w.NobetGorevTipId == nobetGrupGorevTip.NobetGorevTipId).ToList();

                var nobetGrupTalepler = tarihler
                    .GroupBy(g => g.TalepEdilenNobetciSayisi)
                    .Select(s => new
                    {
                        TalepEdilenNobetciSayisi = s.Key,
                        GunSayisi = s.Count()
                    }).ToList();

                //TalepleriTakvimeIsle(nobetGrupTalepler, gunlukNobetciSayisi, tarihler);

                var pazarGunleri = tarihler.Where(w => w.GunGrupId == 1).OrderBy(o => o.Tarih).ToList();
                var cumaGunleri = tarihler.Where(w => w.NobetGunKuralId == 6).OrderBy(o => o.Tarih).ToList();
                var cumaVeCumartesiGunleri = tarihler.Where(w => w.NobetGunKuralId == 6 || w.NobetGunKuralId == 7).OrderBy(o => o.Tarih).ToList();
                var cumartesiVePazarGunleri = tarihler.Where(w => w.NobetGunKuralId == 1 || w.NobetGunKuralId == 7).OrderBy(o => o.Tarih).ToList();
                var bayramlar = tarihler.Where(w => w.GunGrupId == 2).OrderBy(o => o.Tarih).ToList();
                var haftaIciGunleri = tarihler.Where(w => w.GunGrupId == 3).OrderBy(o => o.Tarih).ToList();
                var cumartesiGunleri = tarihler.Where(w => w.GunGrupId == 4).OrderBy(o => o.Tarih).ToList();

                var gunSayisi = tarihler.Count();
                var haftaIciSayisi = haftaIciGunleri.Count();
                var pazarSayisi = pazarGunleri.Count();
                var cumartesiSayisi = cumartesiGunleri.Count();
                var bayramSayisi = bayramlar.Count();

                #endregion

                var nobetGunKurallar = tarihler.Select(s => s.NobetGunKuralId).Distinct().ToList();

                var r = new Random();

                var eczaneNobetGruplar = data.EczaneNobetGruplar
                    .Where(w => w.NobetGrupGorevTipId == nobetGrupGorevTip.Id)
                    .OrderBy(x => //x.EczaneAdi
                                  r.NextDouble()
                            ).ToList();

                var eczaneNobetGrupGunKuralIstatistikler = data.EczaneNobetGrupGunKuralIstatistikYatay
                    .Where(w => w.NobetGrupGorevTipId == nobetGrupGorevTip.Id).ToList();

                var eczaneNobetGrupGunKuralIstatistiklerSon3Ay = data.EczaneNobetGrupGunKuralIstatistikYataySon3Ay
                    .Where(w => w.NobetGrupGorevTipId == nobetGrupGorevTip.Id).ToList();

                var gruptakiEczaneSayisi = eczaneNobetGruplar.Select(s => s.EczaneId).Count();

                // karar değişkeni - nöbet grup bazlı filtrelenmiş
                var eczaneNobetTarihAralikGrupBazli = data.EczaneNobetTarihAralik
                           .Where(e => e.NobetGrupGorevTipId == nobetGrupGorevTip.Id).ToList();

                var nobetGrupGunKurallarAktifGunler = data.NobetGrupGunKurallar
                                                        .Where(s => s.NobetGrupId == nobetGrupGorevTip.NobetGrupId
                                                                 && nobetGunKurallar.Contains(s.NobetGunKuralId))
                                                        .Select(s => s.NobetGunKuralId).ToList();

                //var nobetGrupGorevTipler = data.NobetGrupGorevTipler.Where(w => w.NobetGrupId == nobetGrupGorevTip.NobetGrupId).ToList();

                #region peş peşe görev en az nöbet zamanı

                var farkliAyPespeseGorevAraligi = (gruptakiEczaneSayisi / gunlukNobetciSayisi * 1.2);
                //var altLimit = farkliAyPespeseGorevAraligi * 0.7666 - 10; //0.95
                var altLimit = (gruptakiEczaneSayisi / gunlukNobetciSayisi) * 0.5; //0.95

                //hafta içi                
                altLimit = GetArdisikBosGunSayisi(pespeseNobetSayisiHaftaIci, altLimit);

                var ustLimit = farkliAyPespeseGorevAraligi + farkliAyPespeseGorevAraligi * 0.6667; //77;
                var ustLimitKontrol = ustLimit * 0.95; //0.81 
                //pazar
                var pazarNobetiYazilabilecekIlkAy = (int)Math.Ceiling((double)gruptakiEczaneSayisi / 5) - 1;
                var cumartersiNobetiYazilabilecekIlkAy = (int)Math.Ceiling((double)gruptakiEczaneSayisi / 5) - 1;

                #endregion

                #region ortalama nöbet sayıları

                var talepEdilenNobetciSayisi = tarihler.Sum(s => s.TalepEdilenNobetciSayisi);
                var talepEdilenNobetciSayisiHaftaIci = haftaIciGunleri.Sum(s => s.TalepEdilenNobetciSayisi);
                var talepEdilenNobetciSayisiHaftaSonu = cumartesiVePazarGunleri.Sum(s => s.TalepEdilenNobetciSayisi);
                var talepEdilenNobetciSayisiCuma = cumaGunleri.Sum(s => s.TalepEdilenNobetciSayisi);
                var talepEdilenNobetciSayisiCumartesi = cumartesiGunleri.Sum(s => s.TalepEdilenNobetciSayisi);
                var talepEdilenNobetciSayisiPazar = pazarGunleri.Sum(s => s.TalepEdilenNobetciSayisi);

                var ortalamaNobetSayisi = OrtalamaNobetSayisi(talepEdilenNobetciSayisi, gruptakiEczaneSayisi);
                var ortamalaNobetSayisiHaftaIci = OrtalamaNobetSayisi(talepEdilenNobetciSayisiHaftaIci, gruptakiEczaneSayisi);
                var ortamalaNobetSayisiHaftaSonu = OrtalamaNobetSayisi(talepEdilenNobetciSayisiHaftaSonu, gruptakiEczaneSayisi);
                var ortamalaNobetSayisiPazar = OrtalamaNobetSayisi(talepEdilenNobetciSayisiPazar, gruptakiEczaneSayisi);
                var ortamalaNobetSayisiCumartesi = OrtalamaNobetSayisi(talepEdilenNobetciSayisiCumartesi, gruptakiEczaneSayisi);
                var ortalamaNobetSayisiBayram = OrtalamaNobetSayisi(bayramlar.Sum(s => s.TalepEdilenNobetciSayisi), gruptakiEczaneSayisi);

                #region kümülatif ortalamalar

                #region daha önce tutulan toplam nöbet sayıları

                var kumulatifToplamNobetSayisi = eczaneNobetGrupGunKuralIstatistikler.Sum(s => s.NobetSayisiToplam);
                var kumulatifToplamNobetSayisiHaftaIci = eczaneNobetGrupGunKuralIstatistikler.Sum(s => s.NobetSayisiHaftaIci);
                var kumulatifToplamNobetSayisiCuma = eczaneNobetGrupGunKuralIstatistikler.Sum(s => s.NobetSayisiCuma);
                var kumulatifToplamNobetSayisiCumartesi = eczaneNobetGrupGunKuralIstatistikler.Sum(s => s.NobetSayisiCumartesi);
                var kumulatifToplamNobetSayisiCumaVeCumartesi = kumulatifToplamNobetSayisiCuma + kumulatifToplamNobetSayisiCumartesi;
                var kumulatifToplamNobetSayisiPazar = eczaneNobetGrupGunKuralIstatistikler.Sum(s => s.NobetSayisiPazar);

                var kumulatifToplamNobetSayisiSon3Ay = eczaneNobetGrupGunKuralIstatistiklerSon3Ay.Sum(s => s.NobetSayisiToplam);
                var kumulatifToplamNobetSayisiHaftaIciSon3Ay = eczaneNobetGrupGunKuralIstatistiklerSon3Ay.Sum(s => s.NobetSayisiHaftaIci);
                var kumulatifToplamNobetSayisiCumartesiSon3Ay = eczaneNobetGrupGunKuralIstatistiklerSon3Ay.Sum(s => s.NobetSayisiCumartesi);
                var kumulatifToplamNobetSayisiPazarSon3Ay = eczaneNobetGrupGunKuralIstatistiklerSon3Ay.Sum(s => s.NobetSayisiPazar);

                #endregion

                var ortalamaNobetSayisiKumulatif = OrtalamaNobetSayisi(talepEdilenNobetciSayisi + kumulatifToplamNobetSayisi, gruptakiEczaneSayisi);
                var ortalamaNobetSayisiKumulatifHaftaIci = OrtalamaNobetSayisi(talepEdilenNobetciSayisiHaftaIci + kumulatifToplamNobetSayisiHaftaIci, gruptakiEczaneSayisi);
                var ortalamaNobetSayisiKumulatifCuma = OrtalamaNobetSayisi(talepEdilenNobetciSayisiCuma + kumulatifToplamNobetSayisiCuma, gruptakiEczaneSayisi);
                var ortalamaNobetSayisiKumulatifCumartesi = OrtalamaNobetSayisi(talepEdilenNobetciSayisiCumartesi
                                                                                + kumulatifToplamNobetSayisiCumartesi, gruptakiEczaneSayisi);

                var ortalamaNobetSayisiKumulatifCumaVeCumartesi = OrtalamaNobetSayisi(talepEdilenNobetciSayisiCuma
                                                                                      + talepEdilenNobetciSayisiCumartesi
                                                                                      + kumulatifToplamNobetSayisiCumaVeCumartesi, gruptakiEczaneSayisi);
                var ortalamaNobetSayisiKumulatifPazar = OrtalamaNobetSayisi(talepEdilenNobetciSayisiPazar + kumulatifToplamNobetSayisiPazar, gruptakiEczaneSayisi);

                var ortalamaNobetSayisiKumulatifSon3Ay = OrtalamaNobetSayisi(talepEdilenNobetciSayisi + kumulatifToplamNobetSayisiSon3Ay, gruptakiEczaneSayisi);
                var ortalamaNobetSayisiKumulatifHaftaIciSon3Ay = OrtalamaNobetSayisi(talepEdilenNobetciSayisiHaftaIci + kumulatifToplamNobetSayisiHaftaIciSon3Ay, gruptakiEczaneSayisi);
                var ortalamaNobetSayisiKumulatifCumartesiSon3Ay = OrtalamaNobetSayisi(talepEdilenNobetciSayisiCumartesi + kumulatifToplamNobetSayisiCumartesiSon3Ay, gruptakiEczaneSayisi);
                var ortalamaNobetSayisiKumulatifPazarSon3Ay = OrtalamaNobetSayisi(talepEdilenNobetciSayisiPazar + kumulatifToplamNobetSayisiPazarSon3Ay, gruptakiEczaneSayisi);

                #endregion

                #endregion

                var eczaneNobetSonuclarGrupBazliTumu = data.EczaneNobetSonuclar
                    .Where(w => w.NobetGrupGorevTipId == nobetGrupGorevTip.Id).ToList();

                var eczaneNobetSonuclarGrupBazli = eczaneNobetSonuclarGrupBazliTumu
                    .Where(w => w.Tarih >= data.NobetUstGrupBaslangicTarihi).ToList();

                var nobetGrupBayramNobetleri = eczaneNobetSonuclarGrupBazli
                    .Where(w => w.GunGrupId == 2
                             && w.Tarih >= data.NobetUstGrupBaslangicTarihi)
                    .Select(s => new { s.TakvimId, s.NobetGunKuralId }).ToList();

                #region hafta içi dağılım
                var haftaIciOrtamalaNobetSayisi2 = ortamalaNobetSayisiHaftaIci;

                if (NobetUstGrupKisit(kisitlarAktif, "k32").SagTarafDegeri > 0)
                    haftaIciOrtamalaNobetSayisi2 += (int)NobetUstGrupKisit(kisitlarAktif, "k32").SagTarafDegeri;

                var yazilabilecekHaftaIciNobetSayisi = haftaIciOrtamalaNobetSayisi2;// - eczaneNobetIstatistik.NobetSayisiHaftaIci; 
                if (data.CalismaSayisi >= 1)
                    yazilabilecekHaftaIciNobetSayisi++;

                #endregion

                var nobetGunKuralIstatistikler = data.TakvimNobetGrupGunDegerIstatistikler
                                      .Where(w => w.NobetGrupGorevTipId == nobetGrupGorevTip.Id
                                               //&& tarihler.Select(s => s.NobetGunKuralId).Distinct().Contains(w.NobetGunKuralId)
                                               ).ToList();

                var nobetGunKuralTarihler = OrtalamaNobetSayilariniHesapla(tarihler, gruptakiEczaneSayisi, eczaneNobetGrupGunKuralIstatistikler, nobetGunKuralIstatistikler);

                #region ilk ayda fazla yazılacak eczaneler

                var fazlaYazilacakEczaneSayisi = gunSayisi - gruptakiEczaneSayisi;
                var fazlaYazilacakEczaneler = new List<EczaneNobetGrupDetay>();

                //ilk ayda eczane sayısı gün sayısından azsa, eksik sayı kadar rasgele seçilen eczaneye 1 fazla nöbet yazılır. toplam hedeflerde
                if (data.Yil == 2018 && data.Ay == 1) //&& fazlaYazilacakEczaneSayisi > 0)
                {
                    fazlaYazilacakEczaneler = eczaneNobetGruplar.OrderBy(x => r.NextDouble()).Take(fazlaYazilacakEczaneSayisi).ToList();
                }
                #endregion

                #region Nöbet gün kurallar

                var nobetGunKuralDetaylar = new List<NobetGunKuralDetay>();

                //if (nobetGunKural)
                //{
                //    foreach (var gunKuralId in nobetGrupGunKurallar)
                //    {
                //        var cozulenAydakiNobetSayisi = tarihler.Where(w => w.NobetGunKuralId == gunKuralId).Count();

                //        nobetGunKuralDetaylar.Add(GetNobetGunKural(gunKuralId, data, nobetGrup.Id, gruptakiEczaneSayisi, cozulenAydakiNobetSayisi));
                //    }
                //}

                //var cozulenAydakiHaftaIciToplamNobetSayisi = haftaIciGunleri.Count();

                //nobetGunKuralDetaylar.Add(GetNobetGunKuralHaftaIciToplam(data, nobetGrup.Id, gruptakiNobetciSayisi, cozulenAydakiHaftaIciToplamNobetSayisi));
                #endregion

                #region talep

                var talebiKarsilaKisitParametreModel = new KpTalebiKarsila
                {
                    EczaneNobetTarihAralikTumu = eczaneNobetTarihAralikGrupBazli,
                    NobetGrupGorevTip = nobetGrupGorevTip,
                    Tarihler = tarihler,
                    Model = model,
                    NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k89"),
                    KararDegiskeni = _x
                };

                TalebiKarsila(talebiKarsilaKisitParametreModel);

                #endregion

                #endregion

                #region Eczane bazlı kısıtlar

                foreach (var eczaneNobetGrup in eczaneNobetGruplar)
                {
                    #region kontrol

                    var kontrol = true;

                    if (kontrol)
                    {
                        var kontrolEdilecekEczaneler = data.DebugYapilacakEczaneler.Select(s => s.EczaneNobetGrupId).ToArray();

                        if (kontrolEdilecekEczaneler.Contains(eczaneNobetGrup.Id))
                        {
                            var kontrolEdilenEczane = eczaneNobetGrup.EczaneAdi;
                        }
                    }

                    #endregion

                    #region eczane bazlı veriler

                    var eczaneKalibrasyon = data.Kalibrasyonlar
                        .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id).ToList();

                    // karar değişkeni - eczane bazlı filtrelenmiş
                    var eczaneNobetTarihAralikEczaneBazli = eczaneNobetTarihAralikGrupBazli
                       .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id).ToList();

                    var eczaneNobetSonuclar = eczaneNobetSonuclarGrupBazliTumu
                                    .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id)
                                    .OrderBy(o => o.Tarih).ToList();

                    var eczaneNobetIstatistik = eczaneNobetGrupGunKuralIstatistikler
                        .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id).SingleOrDefault() ?? new EczaneNobetGrupGunKuralIstatistikYatay();

                    var eczaneNobetIstatistikSon3Ay = eczaneNobetGrupGunKuralIstatistiklerSon3Ay
                        .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id).SingleOrDefault() ?? new EczaneNobetGrupGunKuralIstatistikYatay();

                    var yazilabilecekIlkPazarTarihi = eczaneNobetIstatistik.SonNobetTarihiPazar.AddMonths(pazarNobetiYazilabilecekIlkAy - 1);
                    var yazilabilecekIlkCumartesiTarihi = eczaneNobetIstatistik.SonNobetTarihiCumartesi.AddMonths(cumartersiNobetiYazilabilecekIlkAy - 1);
                    var yazilabilecekIlkHaftaIciTarihi = eczaneNobetIstatistik.SonNobetTarihiHaftaIci.AddDays(altLimit);
                    var nobetYazilabilecekIlkTarih = eczaneNobetIstatistik.SonNobetTarihi.AddDays(pespeseNobetSayisi);

                    var kpKumulatifToplam = new KpKumulatifToplam
                    {
                        Model = model,
                        KararDegiskeni = _x,
                        EczaneNobetGrup = eczaneNobetGrup,
                        EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazli
                    };

                    var KpTarihAraligindaEnAz1NobetYaz = new KpTarihAraligindaEnAz1NobetYaz
                    {
                        Model = model,
                        KararDegiskeni = _x,
                        EczaneNobetGrup = eczaneNobetGrup,
                        EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazli,
                        GruptakiNobetciSayisi = gruptakiEczaneSayisi,
                        //GunlukNobetciSayisi = gunlukNobetciSayisi,
                    };
                    #endregion

                    #region aktif kısıtlar

                    #region Peş peşe nöbet        

                    #region ay içinde

                    var kpHerAyPespeseGorev = new KpHerAyPespeseGorev
                    {
                        Model = model,
                        EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazli,
                        NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k1"),
                        Tarihler = tarihler,
                        PespeseNobetSayisi = pespeseNobetSayisi,
                        EczaneNobetGrup = eczaneNobetGrup,
                        KararDegiskeni = _x
                    };
                    HerAyPespeseGorev(kpHerAyPespeseGorev);

                    var kpHerAyPespeseGorevHaftaIci = new KpHerAyHaftaIciPespeseGorev
                    {
                        Model = model,
                        Tarihler = haftaIciGunleri,
                        OrtamalaNobetSayisi = ortamalaNobetSayisiHaftaIci,
                        EczaneNobetGrup = eczaneNobetGrup,
                        EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazli,
                        PespeseNobetSayisiAltLimit = altLimit,//gruptakiEczaneSayisi * 0.5, //altLimit, 0.6//14.02.2019
                        NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k27"),
                        KararDegiskeni = _x
                    };
                    HerAyHaftaIciPespeseGorev(kpHerAyPespeseGorevHaftaIci);

                    #endregion

                    #region farklı aylar peş peşe görev

                    var kpPesPeseGorevEnAz = new KpPesPeseGorevEnAz
                    {
                        Model = model,
                        KararDegiskeni = _x,
                        EczaneNobetGrup = eczaneNobetGrup,
                        EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazli
                    };

                    var pesPeseGorevEnAzFarkliAylar = (KpPesPeseGorevEnAz)kpPesPeseGorevEnAz.Clone();

                    pesPeseGorevEnAzFarkliAylar.NobetSayisi = eczaneNobetIstatistik.NobetSayisiToplam;
                    pesPeseGorevEnAzFarkliAylar.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k18");
                    pesPeseGorevEnAzFarkliAylar.Tarihler = tarihler;
                    pesPeseGorevEnAzFarkliAylar.NobetYazilabilecekIlkTarih = nobetYazilabilecekIlkTarih;
                    pesPeseGorevEnAzFarkliAylar.SonNobetTarihi = eczaneNobetIstatistik.SonNobetTarihi;

                    PesPeseGorevEnAz(pesPeseGorevEnAzFarkliAylar);

                    var pesPeseGorevEnAzPazar = (KpPesPeseGorevEnAz)kpPesPeseGorevEnAz.Clone();

                    pesPeseGorevEnAzPazar.NobetSayisi = eczaneNobetIstatistik.NobetSayisiPazar;
                    pesPeseGorevEnAzPazar.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k28");
                    pesPeseGorevEnAzPazar.Tarihler = pazarGunleri;
                    pesPeseGorevEnAzPazar.NobetYazilabilecekIlkTarih = yazilabilecekIlkPazarTarihi;
                    pesPeseGorevEnAzPazar.SonNobetTarihi = eczaneNobetIstatistik.SonNobetTarihiPazar;

                    PesPeseGorevEnAz(pesPeseGorevEnAzPazar);

                    var pesPeseGorevEnAzCumartesi = (KpPesPeseGorevEnAz)kpPesPeseGorevEnAz.Clone();

                    pesPeseGorevEnAzCumartesi.NobetSayisi = eczaneNobetIstatistik.NobetSayisiCumartesi;
                    pesPeseGorevEnAzCumartesi.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k37");
                    pesPeseGorevEnAzCumartesi.Tarihler = cumartesiGunleri;
                    pesPeseGorevEnAzCumartesi.NobetYazilabilecekIlkTarih = yazilabilecekIlkCumartesiTarihi;
                    pesPeseGorevEnAzCumartesi.SonNobetTarihi = eczaneNobetIstatistik.SonNobetTarihiCumartesi;

                    PesPeseGorevEnAz(pesPeseGorevEnAzCumartesi);

                    var pesPeseGorevEnAzHaftaIci = (KpPesPeseGorevEnAz)kpPesPeseGorevEnAz.Clone();

                    pesPeseGorevEnAzHaftaIci.NobetSayisi = eczaneNobetIstatistik.NobetSayisiHaftaIci;
                    pesPeseGorevEnAzHaftaIci.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k27");
                    pesPeseGorevEnAzHaftaIci.Tarihler = tarihler;
                    pesPeseGorevEnAzHaftaIci.NobetYazilabilecekIlkTarih = yazilabilecekIlkHaftaIciTarihi;
                    pesPeseGorevEnAzHaftaIci.SonNobetTarihi = eczaneNobetIstatistik.SonNobetTarihiHaftaIci;

                    PesPeseGorevEnAz(pesPeseGorevEnAzHaftaIci);

                    #endregion

                    #endregion

                    #region Tarih aralığı ortalama en fazla (gün grupları)

                    var kpTarihAraligiOrtalamaEnFazla = new KpTarihAraligiOrtalamaEnFazla
                    {
                        Model = model,
                        KararDegiskeni = _x,
                        EczaneNobetGrup = eczaneNobetGrup,
                        EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazli
                    };

                    var ortalamaEnFazlaTumTarihAraligi = (KpTarihAraligiOrtalamaEnFazla)kpTarihAraligiOrtalamaEnFazla.Clone();
                    ortalamaEnFazlaTumTarihAraligi.Tarihler = tarihler;
                    ortalamaEnFazlaTumTarihAraligi.GunSayisi = gunSayisi;
                    ortalamaEnFazlaTumTarihAraligi.OrtalamaNobetSayisi = ortalamaNobetSayisi;
                    ortalamaEnFazlaTumTarihAraligi.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k19");

                    TarihAraligiOrtalamaEnFazla(ortalamaEnFazlaTumTarihAraligi);

                    var ortalamaEnFazlaHaftaIci = (KpTarihAraligiOrtalamaEnFazla)kpTarihAraligiOrtalamaEnFazla.Clone();
                    ortalamaEnFazlaHaftaIci.Tarihler = haftaIciGunleri;
                    ortalamaEnFazlaHaftaIci.GunSayisi = haftaIciSayisi;
                    ortalamaEnFazlaHaftaIci.OrtalamaNobetSayisi = ortamalaNobetSayisiHaftaIci;
                    ortalamaEnFazlaHaftaIci.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k32");

                    TarihAraligiOrtalamaEnFazla(ortalamaEnFazlaHaftaIci);

                    var ortalamaEnFazlaHaftaSonu = (KpTarihAraligiOrtalamaEnFazla)kpTarihAraligiOrtalamaEnFazla.Clone();
                    ortalamaEnFazlaHaftaSonu.Tarihler = cumartesiVePazarGunleri;
                    ortalamaEnFazlaHaftaSonu.GunSayisi = cumartesiSayisi + pazarSayisi;
                    ortalamaEnFazlaHaftaSonu.OrtalamaNobetSayisi = ortamalaNobetSayisiHaftaSonu;
                    ortalamaEnFazlaHaftaSonu.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k73");

                    TarihAraligiOrtalamaEnFazla(ortalamaEnFazlaHaftaSonu);

                    var ortalamaEnFazlaBayram = (KpTarihAraligiOrtalamaEnFazla)kpTarihAraligiOrtalamaEnFazla.Clone();
                    ortalamaEnFazlaBayram.Tarihler = bayramlar;
                    ortalamaEnFazlaBayram.GunSayisi = bayramSayisi;
                    ortalamaEnFazlaBayram.OrtalamaNobetSayisi = ortalamaNobetSayisiBayram;
                    ortalamaEnFazlaBayram.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k14");

                    TarihAraligiOrtalamaEnFazla(ortalamaEnFazlaBayram);

                    #endregion

                    #region Tur takip kısıtı - nöbet ortalamaları

                    var eczaneToplamKalibrasyon = GetKalibrasyonDegeri(eczaneKalibrasyon);

                    #region aylık en fazla

                    var aylar = tarihler.Select(s => s.Ay).Distinct().ToList();

                    if (aylar.Count > 1)
                    {
                        foreach (var ay in aylar)
                        {
                            var ayTumGunler = tarihler.Where(w => w.Ay == ay).ToList();

                            var ortalamaNobetSayisiAylikTumu = OrtalamaNobetSayisi(ayTumGunler.Sum(s => s.TalepEdilenNobetciSayisi), gruptakiEczaneSayisi);
                            var ortalamaEnFazlaHerAy = (KpTarihAraligiOrtalamaEnFazla)kpTarihAraligiOrtalamaEnFazla.Clone();

                            ortalamaEnFazlaHerAy.Tarihler = ayTumGunler;
                            ortalamaEnFazlaHerAy.GunSayisi = ayTumGunler.Count;
                            ortalamaEnFazlaHerAy.OrtalamaNobetSayisi = ortalamaNobetSayisiAylikTumu;
                            ortalamaEnFazlaHerAy.GunKuralAdi = $"{ay}.ay en fazla";
                            ortalamaEnFazlaHerAy.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k65");

                            TarihAraligiOrtalamaEnFazla(ortalamaEnFazlaHerAy);

                            var ayHaftaIiciGunler = haftaIciGunleri.Where(w => w.Ay == ay).ToList();
                            var ortalamaNobetSayisiAylikHaftaIci = OrtalamaNobetSayisi(ayHaftaIiciGunler.Sum(s => s.TalepEdilenNobetciSayisi), gruptakiEczaneSayisi);

                            var ortalamaEnFazlaHerAyHaftaIici = (KpTarihAraligiOrtalamaEnFazla)kpTarihAraligiOrtalamaEnFazla.Clone();
                            ortalamaEnFazlaHerAyHaftaIici.Tarihler = haftaIciGunleri.Where(w => w.Ay == ay).ToList();
                            ortalamaEnFazlaHerAyHaftaIici.GunSayisi = ayHaftaIiciGunler.Count;
                            ortalamaEnFazlaHerAyHaftaIici.GunKuralAdi = $"{ay}.ay en fazla";
                            ortalamaEnFazlaHerAyHaftaIici.OrtalamaNobetSayisi = ortalamaNobetSayisiAylikHaftaIci;
                            ortalamaEnFazlaHerAyHaftaIici.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k66");

                            TarihAraligiOrtalamaEnFazla(ortalamaEnFazlaHerAyHaftaIici);
                        }
                    }

                    #endregion

                    #region hafta içi nöbet sayıları arasındaki farklar - max/min

                    var nobetGunKuralNobetSayilari = GetNobetGunKuralNobetSayilari(nobetGunKuralIstatistikler, eczaneNobetIstatistik);

                    //var haftaIciEnAzVeEnCokNobetSayisiArasindakiFark = nobetGunKuralNobetSayilari.Max(m => m.NobetSayisi) - nobetGunKuralNobetSayilari.Min(m => m.NobetSayisi);
                    var haftaIciEnCokNobetSayisi = 0;
                    var haftaIciEnAzNobetSayisi = 0;

                    if (nobetGunKuralIstatistikler.Count > 0)
                    {
                        var haftaiciNobetIstatistik = nobetGunKuralNobetSayilari
                            .Where(w => w.GunGrupId == 3).ToList();

                        haftaIciEnCokNobetSayisi = haftaiciNobetIstatistik.Max(m => m.NobetSayisi);
                        haftaIciEnAzNobetSayisi = haftaiciNobetIstatistik.Min(m => m.NobetSayisi);

                        //haftaIciEnCokNobetSayisi = nobetGunKuralNobetSayilari.Max(m => m.NobetSayisi);
                        //haftaIciEnAzNobetSayisi = nobetGunKuralNobetSayilari.Min(m => m.NobetSayisi);
                    }

                    #endregion

                    #region nöbet ortalamaları - gün kurallar

                    var kumulatifEnfazlaHaftaIciDagilimi = NobetUstGrupKisit(kisitlarAktif, "k44");

                    var kpOrtalamaEnFazlaKumulatif = new KpKumulatifToplam
                    {
                        Model = model,
                        KararDegiskeni = _x,
                        EczaneNobetGrup = eczaneNobetGrup,
                        EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazli
                    };

                    foreach (var nobetGunKural in nobetGunKuralIstatistikler)//aktifGunKurallar)
                    {
                        //if (nobetGunKural.NobetGunKuralKapanmaTarihi != null)
                        //    continue;

                        //if (kontrol && gunKural.NobetGunKuralAdi == "Cuma")
                        //{
                        //}

                        #region tarih aralığı en fazla

                        var tarihAralik = nobetGunKuralTarihler
                            .Where(w => w.NobetGunKuralId == nobetGunKural.NobetGunKuralId).SingleOrDefault() ?? new NobetGunKuralTarihAralik();

                        herAyEnFazlaIlgiliKisit = GetNobetGunKuralIlgiliKisitTarihAraligi(kisitlarAktif, nobetGunKural.NobetGunKuralId);

                        var tarihAraligiOrtalamaEnFazlaIlgiliKisit = (KpTarihAraligiOrtalamaEnFazla)kpTarihAraligiOrtalamaEnFazla.Clone();

                        tarihAraligiOrtalamaEnFazlaIlgiliKisit.Tarihler = tarihAralik.TakvimNobetGruplar;
                        tarihAraligiOrtalamaEnFazlaIlgiliKisit.GunSayisi = tarihAralik.GunSayisi;
                        tarihAraligiOrtalamaEnFazlaIlgiliKisit.OrtalamaNobetSayisi = tarihAralik.OrtalamaNobetSayisi;
                        tarihAraligiOrtalamaEnFazlaIlgiliKisit.NobetUstGrupKisit = herAyEnFazlaIlgiliKisit;
                        tarihAraligiOrtalamaEnFazlaIlgiliKisit.GunKuralAdi = herAyEnFazlaIlgiliKisit.KisitId == 42 ? nobetGunKural.NobetGunKuralAdi : "";

                        TarihAraligiOrtalamaEnFazla(tarihAraligiOrtalamaEnFazlaIlgiliKisit);

                        #endregion

                        #region kümülatif en fazla

                        var kumulatifOrtalamaGunKuralNobetSayisi = tarihAralik.KumulatifOrtalamaNobetSayisi;

                        var gunKuralNobetSayisi = GetToplamGunKuralNobetSayisi(eczaneNobetIstatistik, nobetGunKural.NobetGunKuralId);

                        var haftaIciEnCokVeGunKuralNobetleriArasindakiFark = haftaIciEnCokNobetSayisi - gunKuralNobetSayisi;
                        var haftaIciEnAzVeEnCokNobetSayisiArasindakiFark = haftaIciEnCokNobetSayisi - haftaIciEnAzNobetSayisi;

                        if (gunKuralNobetSayisi > kumulatifOrtalamaGunKuralNobetSayisi)
                            kumulatifOrtalamaGunKuralNobetSayisi = gunKuralNobetSayisi;

                        kumulatifEnFazlaIlgiliGunkural = GetNobetGunKuralIlgiliKisitKumulatif(kisitlarAktif, nobetGunKural.NobetGunKuralId);

                        var kumulatifToplamEnFazla = (KpKumulatifToplam)kpOrtalamaEnFazlaKumulatif.Clone();

                        if (KumulatifEnfazlaHafIciDagilimiArasindaFarkVarmi(
                            herAyEnFazlaIlgiliKisit,
                            kumulatifEnfazlaHaftaIciDagilimi,
                            nobetGunKural,
                            gunKuralNobetSayisi,
                            haftaIciEnCokVeGunKuralNobetleriArasindakiFark,
                            haftaIciEnAzVeEnCokNobetSayisiArasindakiFark)
                            )
                        {//hafta içi dağılım

                            kumulatifOrtalamaGunKuralNobetSayisi = 0;

                            kumulatifToplamEnFazla.GunKuralAdi = nobetGunKural.NobetGunKuralAdi;

                            kumulatifEnFazlaIlgiliGunkural = kumulatifEnfazlaHaftaIciDagilimi;

                            //kumulatifEnFazlaIlgiliGunkural.SagTarafDegeri = kumulatifOrtalamaGunKuralNobetSayisi;
                        }

                        kumulatifToplamEnFazla.Tarihler = tarihAralik.TakvimNobetGruplar;
                        kumulatifToplamEnFazla.KumulatifOrtalamaNobetSayisi = kumulatifOrtalamaGunKuralNobetSayisi;
                        kumulatifToplamEnFazla.ToplamNobetSayisi = gunKuralNobetSayisi;
                        kumulatifToplamEnFazla.NobetUstGrupKisit = kumulatifEnFazlaIlgiliGunkural;
                        //kumulatifToplamEnFazla.GunKuralAdi = nobetGunKural.NobetGunKuralAdi;

                        if (nobetGunKural.NobetGunKuralId == 1)
                            kumulatifToplamEnFazla.ToplamNobetSayisi += (int)eczaneToplamKalibrasyon.KalibrasyonToplamPazar;

                        KumulatifToplamEnFazla(kumulatifToplamEnFazla);

                        #endregion
                    }

                    #endregion

                    #endregion

                    #region Bayram kümülatif toplam en fazla ve farklı tür bayram

                    if (bayramSayisi > 0)
                    {
                        //var bayramNobetleri = eczaneNobetSonuclar
                        //        .Where(w => w.NobetGunKuralId > 7
                        //                 && w.Tarih >= data.NobetUstGrupBaslangicTarihi)
                        //        .Select(s => new { s.TakvimId, s.NobetGunKuralId }).ToList();

                        var bayramNobetleriAnahtarli = eczaneNobetSonuclar
                                .Where(w => w.NobetGunKuralId > 7).ToList();

                        var toplamBayramNobetSayisi = eczaneNobetIstatistik.NobetSayisiBayram;// bayramNobetleri.Count();

                        var bayramGunKuralIstatistikler = nobetGunKuralIstatistikler
                                                        .Where(w => w.NobetGunKuralId > 7).ToList();

                        var toplamBayramGrupToplamNobetSayisi = nobetGrupBayramNobetleri.Count();

                        var kumulatifBayramSayisi = bayramGunKuralIstatistikler.Sum(x => x.GunSayisi);

                        var yillikOrtalamaGunKuralSayisi = OrtalamaNobetSayisi(gunlukNobetciSayisi, gruptakiEczaneSayisi, kumulatifBayramSayisi);

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

                        if (NobetUstGrupKisit(kisitlarAktif, "k5").SagTarafDegeri > 0)
                            yillikOrtalamaGunKuralSayisi = NobetUstGrupKisit(kisitlarAktif, "k5").SagTarafDegeri;

                        if (data.CalismaSayisi >= 3)
                            yillikOrtalamaGunKuralSayisi++;

                        var sonBayram = bayramNobetleriAnahtarli.LastOrDefault();

                        if (sonBayram != null)
                        {
                            if (bayramlar.Select(s => s.NobetGunKuralId).Contains(sonBayram.NobetGunKuralId) && !NobetUstGrupKisit(kisitlarAktif, "k36").PasifMi)
                            {
                                var bayramPespeseFarkliTurKisit = new KpPespeseFarkliTurNobet
                                {
                                    Model = model,
                                    NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k36"),
                                    Tarihler = bayramlar,
                                    EczaneNobetGrup = eczaneNobetGrup,
                                    EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazli,
                                    SonNobet = sonBayram,
                                    KararDegiskeni = _x
                                };
                                PespeseFarkliTurNobetYaz(bayramPespeseFarkliTurKisit);
                            }
                            else
                            {
                                var kumulatifToplamEnFazlaBayram = new KpKumulatifToplam
                                {
                                    Model = model,
                                    Tarihler = bayramlar,
                                    EczaneNobetGrup = eczaneNobetGrup,
                                    EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazli,
                                    NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k5"),
                                    KumulatifOrtalamaNobetSayisi = yillikOrtalamaGunKuralSayisi,
                                    ToplamNobetSayisi = toplamBayramNobetSayisi,
                                    GunKuralAdi = "Bayram",
                                    KararDegiskeni = _x
                                };

                                KumulatifToplamEnFazla(kumulatifToplamEnFazlaBayram);
                            }
                        }
                    }
                    #endregion

                    #endregion

                    #region pasif kısıtlar        

                    #region kümülatif en fazla

                    #region Toplam max

                    var kpKumulatifToplamEnFazla = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnFazla.Tarihler = tarihler;
                    kpKumulatifToplamEnFazla.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k2");
                    kpKumulatifToplamEnFazla.ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiToplam;
                    kpKumulatifToplamEnFazla.KumulatifOrtalamaNobetSayisi = ortalamaNobetSayisiKumulatif;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnFazla);

                    #endregion

                    #region Toplam max

                    //var kpKumulatifToplamEnFazlaY1_UzakBolge = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    //kpKumulatifToplamEnFazlaY1_UzakBolge.Tarihler = tarihler;
                    //kpKumulatifToplamEnFazlaY1_UzakBolge.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k2");
                    //kpKumulatifToplamEnFazlaY1_UzakBolge.ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiToplam;
                    //kpKumulatifToplamEnFazlaY1_UzakBolge.KumulatifOrtalamaNobetSayisi = 8;

                    //KumulatifToplamEnFazla(kpKumulatifToplamEnFazlaY1_UzakBolge);

                    #endregion

                    #region Hafta içi toplam max hedefler

                    var kpKumulatifToplamEnFazlaHaftaIci = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnFazlaHaftaIci.Tarihler = haftaIciGunleri;
                    kpKumulatifToplamEnFazlaHaftaIci.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k16");
                    kpKumulatifToplamEnFazlaHaftaIci.ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiHaftaIci
                          + (int)eczaneToplamKalibrasyon.KalibrasyonToplamHaftaIci;
                    kpKumulatifToplamEnFazlaHaftaIci.KumulatifOrtalamaNobetSayisi = ortalamaNobetSayisiKumulatifHaftaIci;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnFazlaHaftaIci);

                    #endregion

                    #region son 3 ay

                    #region son 3 ay toplam 

                    var kpKumulatifToplamEnFazlaSon3Ay = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnFazlaSon3Ay.Tarihler = tarihler;
                    kpKumulatifToplamEnFazlaSon3Ay.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k68");
                    kpKumulatifToplamEnFazlaSon3Ay.ToplamNobetSayisi = eczaneNobetIstatistikSon3Ay.NobetSayisiToplam;
                    kpKumulatifToplamEnFazlaSon3Ay.KumulatifOrtalamaNobetSayisi = ortalamaNobetSayisiKumulatifSon3Ay;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnFazlaSon3Ay);

                    #endregion

                    #region son 3 ay hafta içi

                    var kpKumulatifToplamEnFazlaHaftaIciSon3Ay = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnFazlaHaftaIciSon3Ay.Tarihler = haftaIciGunleri;
                    kpKumulatifToplamEnFazlaHaftaIciSon3Ay.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k69");
                    kpKumulatifToplamEnFazlaHaftaIciSon3Ay.ToplamNobetSayisi = eczaneNobetIstatistikSon3Ay.NobetSayisiHaftaIci;
                    kpKumulatifToplamEnFazlaHaftaIciSon3Ay.KumulatifOrtalamaNobetSayisi = ortalamaNobetSayisiKumulatifHaftaIciSon3Ay;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnFazlaHaftaIciSon3Ay);

                    #endregion

                    #region son 3 ay cumartesi

                    var kpKumulatifToplamEnFazlaCumartesiSon3Ay = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnFazlaCumartesiSon3Ay.Tarihler = cumartesiGunleri;
                    kpKumulatifToplamEnFazlaCumartesiSon3Ay.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k70");
                    kpKumulatifToplamEnFazlaCumartesiSon3Ay.ToplamNobetSayisi = eczaneNobetIstatistikSon3Ay.NobetSayisiCumartesi;
                    kpKumulatifToplamEnFazlaCumartesiSon3Ay.KumulatifOrtalamaNobetSayisi = ortalamaNobetSayisiKumulatifCumartesiSon3Ay;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnFazlaCumartesiSon3Ay);

                    #endregion

                    #region son 3 ay pazar

                    var kpKumulatifToplamEnFazlaPazarSon3Ay = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnFazlaPazarSon3Ay.Tarihler = pazarGunleri;
                    kpKumulatifToplamEnFazlaPazarSon3Ay.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k71");
                    kpKumulatifToplamEnFazlaPazarSon3Ay.ToplamNobetSayisi = eczaneNobetIstatistikSon3Ay.NobetSayisiPazar;
                    kpKumulatifToplamEnFazlaPazarSon3Ay.KumulatifOrtalamaNobetSayisi = ortalamaNobetSayisiKumulatifPazarSon3Ay;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnFazlaPazarSon3Ay);

                    #endregion 

                    #endregion

                    #region Toplam hafta içi toplam kalibrasyonlu 

                    //var kpKumulatifToplamEnFazlaHaftaIciKalibrasyonlu = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    //kpKumulatifToplamEnFazlaHaftaIciKalibrasyonlu.Tarihler = haftaIciGunleri;
                    //kpKumulatifToplamEnFazlaHaftaIciKalibrasyonlu.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k54");//k16
                    //kpKumulatifToplamEnFazlaHaftaIciKalibrasyonlu.ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiHaftaIci
                    //    + (int)eczaneToplamKalibrasyon.KalibrasyonToplamHaftaIci;
                    //kpKumulatifToplamEnFazlaHaftaIciKalibrasyonlu.KumulatifOrtalamaNobetSayisi = ortalamaNobetSayisiKumulatifHaftaIci;

                    //KumulatifToplamEnFazla(kpKumulatifToplamEnFazlaHaftaIciKalibrasyonlu);

                    #endregion

                    #region Toplam pazar toplam kalibrasyonlu 

                    //var kpKumulatifToplamEnFazlaPazarToplamKalibrasyonlu = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    //kpKumulatifToplamEnFazlaPazarToplamKalibrasyonlu.Tarihler = pazarGunleri;
                    //kpKumulatifToplamEnFazlaPazarToplamKalibrasyonlu.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k55");//k57
                    //kpKumulatifToplamEnFazlaPazarToplamKalibrasyonlu.KumulatifOrtalamaNobetSayisi = ortalamaNobetSayisiKumulatifPazar;
                    //kpKumulatifToplamEnFazlaPazarToplamKalibrasyonlu.ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiPazar
                    //    + (int)eczaneToplamKalibrasyon.KalibrasyonToplamPazar;
                    //kpKumulatifToplamEnFazlaPazarToplamKalibrasyonlu.KumulatifOrtalamaNobetSayisi = ortalamaNobetSayisiKumulatifPazar;

                    //KumulatifToplamEnFazla(kpKumulatifToplamEnFazlaPazarToplamKalibrasyonlu);

                    #endregion

                    #region Toplam cuma ve cumartesi max hedefler

                    var kpKumulatifToplamEnFazlaCumaVeCumartesi = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnFazlaCumaVeCumartesi.Tarihler = cumaVeCumartesiGunleri;
                    kpKumulatifToplamEnFazlaCumaVeCumartesi.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k25");
                    kpKumulatifToplamEnFazlaCumaVeCumartesi.ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiCuma + eczaneNobetIstatistik.NobetSayisiCumartesi;
                    kpKumulatifToplamEnFazlaCumaVeCumartesi.KumulatifOrtalamaNobetSayisi = ortalamaNobetSayisiKumulatifCumaVeCumartesi;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnFazlaCumaVeCumartesi);

                    #endregion

                    #region Toplam cumartesi ve pazar max hedefler

                    var kpKumulatifToplamEnFazlaCumartesiVePazar = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnFazlaCumartesiVePazar.Tarihler = cumartesiVePazarGunleri;
                    kpKumulatifToplamEnFazlaCumartesiVePazar.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k50");
                    kpKumulatifToplamEnFazlaCumartesiVePazar.ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiPazar + eczaneNobetIstatistik.NobetSayisiCumartesi;
                    kpKumulatifToplamEnFazlaCumartesiVePazar.KumulatifOrtalamaNobetSayisi = ortalamaNobetSayisiKumulatifPazar;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnFazlaCumartesiVePazar);

                    #endregion

                    #endregion

                    #region en az

                    #region tarih aralığı en az

                    #region tarih aralığı en az 1 görev

                    var tarihAraligindaEnAz1NobetYazKisitTarihAraligi = (KpTarihAraligindaEnAz1NobetYaz)KpTarihAraligindaEnAz1NobetYaz.Clone();

                    tarihAraligindaEnAz1NobetYazKisitTarihAraligi.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k4");
                    tarihAraligindaEnAz1NobetYazKisitTarihAraligi.Tarihler = tarihler;

                    TarihAraligindaEnAz1NobetYaz(tarihAraligindaEnAz1NobetYazKisitTarihAraligi);

                    #endregion

                    #region tarih aralığı en az 1 hafta içi

                    var tarihAraligindaEnAz1NobetYazKisitHaftaIci = (KpTarihAraligindaEnAz1NobetYaz)KpTarihAraligindaEnAz1NobetYaz.Clone();

                    tarihAraligindaEnAz1NobetYazKisitHaftaIci.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k40");
                    tarihAraligindaEnAz1NobetYazKisitHaftaIci.Tarihler = haftaIciGunleri;

                    TarihAraligindaEnAz1NobetYaz(tarihAraligindaEnAz1NobetYazKisitHaftaIci);

                    #endregion

                    #endregion

                    #region kümülatif en az

                    kpKumulatifToplam.EnAzMi = true;

                    #region Kümülatif en az

                    var kpKumulatifToplamEnAz = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnAz.Tarihler = tarihler;
                    kpKumulatifToplamEnAz.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k3");
                    kpKumulatifToplamEnAz.ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiToplam;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnAz);

                    #endregion

                    #region Kümülatif cuma ve cumartesi en az

                    var kpKumulatifToplamEnAzCumaVeCumartesi = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnAzCumaVeCumartesi.Tarihler = cumaVeCumartesiGunleri;
                    kpKumulatifToplamEnAzCumaVeCumartesi.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k26");
                    kpKumulatifToplamEnAzCumaVeCumartesi.ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiCuma + eczaneNobetIstatistik.NobetSayisiCumartesi;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnAzCumaVeCumartesi);

                    #endregion

                    #region Kümülatif cumartesi ve pazar en az

                    var kpKumulatifToplamEnAzCumartesiVePazar = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnAzCumartesiVePazar.Tarihler = cumartesiVePazarGunleri;
                    kpKumulatifToplamEnAzCumartesiVePazar.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k62");
                    kpKumulatifToplamEnAzCumartesiVePazar.ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiCumartesi + eczaneNobetIstatistik.NobetSayisiPazar;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnAzCumartesiVePazar);

                    #endregion

                    #region Kümülatif cumartesi en az

                    var kpKumulatifToplamEnAzCumartesi = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnAzCumartesi.Tarihler = cumartesiGunleri;
                    kpKumulatifToplamEnAzCumartesi.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k30");
                    kpKumulatifToplamEnAzCumartesi.ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiCumartesi;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnAzCumartesi);

                    #endregion

                    #region Kümülatif pazar en az

                    var kpKumulatifToplamEnAzPazar = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnAzPazar.Tarihler = pazarGunleri;
                    kpKumulatifToplamEnAzPazar.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k56");
                    kpKumulatifToplamEnAzPazar.ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiPazar
                        + (int)eczaneToplamKalibrasyon.KalibrasyonToplamPazar;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnAzPazar);

                    #endregion

                    #region Kümülatif hafta içi en az

                    var kpKumulatifToplamEnAzHaftaIci = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnAzHaftaIci.Tarihler = haftaIciGunleri;
                    kpKumulatifToplamEnAzHaftaIci.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k17");
                    kpKumulatifToplamEnAzHaftaIci.ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiHaftaIci
                        + (int)eczaneToplamKalibrasyon.KalibrasyonToplamHaftaIci;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnAzHaftaIci);

                    #endregion

                    #region Kümülatif en az - son 3 ay

                    var kpKumulatifToplamEnAzSon3Ay = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnAzSon3Ay.Tarihler = tarihler;
                    kpKumulatifToplamEnAzSon3Ay.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k85");
                    kpKumulatifToplamEnAzSon3Ay.ToplamNobetSayisi = eczaneNobetIstatistikSon3Ay.NobetSayisiToplam;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnAzSon3Ay);

                    #endregion

                    #region Kümülatif hafta içi en az - son 3 ay

                    var kpKumulatifToplamEnAzHaftaIciSon3Ay = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnAzHaftaIciSon3Ay.Tarihler = haftaIciGunleri;
                    kpKumulatifToplamEnAzHaftaIciSon3Ay.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k72");
                    kpKumulatifToplamEnAzHaftaIciSon3Ay.ToplamNobetSayisi = eczaneNobetIstatistikSon3Ay.NobetSayisiHaftaIci;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnAzHaftaIciSon3Ay);

                    #endregion

                    #region Kümülatif bayram en az

                    var kpKumulatifToplamEnAzBayram = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnAzBayram.Tarihler = bayramlar;
                    kpKumulatifToplamEnAzBayram.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k6");
                    kpKumulatifToplamEnAzBayram.ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiBayram;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnAzBayram);

                    #endregion

                    #endregion

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

                    #endregion
                }
                #endregion

                #region istek ve mazeret

                var istegiKarsilaKisit = new KpIstegiKarsila
                {
                    Model = model,
                    EczaneNobetTarihAralik = data.EczaneNobetTarihAralik.Where(w => w.NobetGrupGorevTipId == nobetGrupGorevTip.Id).ToList(),
                    NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k12"),
                    EczaneNobetIstekler = data.EczaneNobetIstekler.Where(w => w.NobetGrupGorevTipId == nobetGrupGorevTip.Id).ToList(),
                    KararDegiskeni = _x
                };
                IstegiKarsila(istegiKarsilaKisit);

                var mazereteGorevYazmaKisit = new KpMazereteGorevYazma
                {
                    Model = model,
                    EczaneNobetTarihAralik = data.EczaneNobetTarihAralik.Where(w => w.NobetGrupGorevTipId == nobetGrupGorevTip.Id).ToList(),
                    NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k13"),
                    EczaneNobetMazeretler = data.EczaneNobetMazeretler.Where(w => w.NobetGrupGorevTipId == nobetGrupGorevTip.Id).ToList(),
                    KararDegiskeni = _x
                };
                MazereteGorevYazma(mazereteGorevYazmaKisit);

                #endregion

                #region Alt gruplarla eşit sayıda nöbet tutulsun - eski

                var ayniGunNobetTutmasiTakipEdilecekGruplar = new List<int>
                {
                    20,//Yenişehir-1,
                    22 //Yenişehir-3 (M.Ü. Hastanesi)
                };

                if (false
                    && !NobetUstGrupKisit(kisitlarAktif, "k29").PasifMi
                    && ayniGunNobetTutmasiTakipEdilecekGruplar.Contains(nobetGrupGorevTip.NobetGrupId) //nobetGruplar.Count() > 0
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

                    var altGruplarlaAyniGunNobetTutmaStd = NobetUstGrupKisit(kisitlarAktif, "k29").SagTarafDegeri;

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
                                     ).ToList();

                        var bakilanEczaneninToplamNobetSayisi = bakilanEczaneninSonuclari.Count();

                        var bolum = Math.Round((double)bakilanEczaneninToplamNobetSayisi / nobetAltGruplar.Count(), 0);

                        var altGrupIleTutulacakNobetSayisiUstLimiti = bolum + 1;

                        if (altGruplarlaAyniGunNobetTutmaStd > 0)
                            altGrupIleTutulacakNobetSayisiUstLimiti += altGruplarlaAyniGunNobetTutmaStd;

                        var yeniSehir2dekiEczaneninSonuclari = data.EczaneGrupNobetSonuclarTumu
                            .Where(w => altGrubuOlanNobetGruplar.Contains(w.NobetGrupId)
                                     && w.Tarih >= data.NobetUstGrupBaslangicTarihi).ToList();

                        var altGrupluEczaneler = data.EczaneNobetGrupAltGruplar;

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
                                                       kd = _x[m],
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
                                       kd = _x[m],
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
                                                       .Select(m => _x[m])) <= 1,
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

            #region Eş gruplar (aynı gün nöbet)

            var eczaneGruplar = data.EczaneGruplar.Where(w => w.EczaneGrupTanimTipId > 0).ToList();

            var esGrubaAyniGunNobetYazmaEczaneGruplar = new KpEsGrubaAyniGunNobetYazma
            {
                Model = model,
                EczaneNobetTarihAralik = data.EczaneNobetTarihAralik,
                EczaneNobetSonuclar = data.EczaneGrupNobetSonuclarTumu,
                NobetUstGrupKisit = NobetUstGrupKisit(data.Kisitlar, "k11"),//eczaneGrup,
                EczaneGruplar = eczaneGruplar,
                Tarihler = data.TarihAraligi,
                KararDegiskeni = _x
            };
            EsGruptakiEczanelereAyniGunNobetYazma(esGrubaAyniGunNobetYazmaEczaneGruplar);

            var esGrubaAyniGunNobetYazmaIkiliEczaneler = new KpEsGrubaAyniGunNobetYazma
            {
                Model = model,
                EczaneNobetTarihAralik = data.EczaneNobetTarihAralik.Where(w => w.HaftaIciMi).ToList(),
                EczaneNobetSonuclar = data.EczaneGrupNobetSonuclarTumu,
                NobetUstGrupKisit = NobetUstGrupKisit(data.Kisitlar, "k45"),//ikiliEczaneAyniGunNobet,
                EczaneGruplar = data.ArasindaAyniGun2NobetFarkiOlanIkiliEczaneler,
                Tarihler = haftaIciGunleriTumu,
                KararDegiskeni = _x
            };
            EsGruptakiEczanelereAyniGunNobetYazma(esGrubaAyniGunNobetYazmaIkiliEczaneler);

            var esGrubaAyniGunNobetYazmaOncekiAylar = new KpEsGrubaAyniGunNobetYazma
            {
                Model = model,
                EczaneNobetTarihAralik = data.EczaneNobetTarihAralik.Where(w => w.HaftaIciMi).ToList(),
                EczaneNobetSonuclar = data.EczaneGrupNobetSonuclarTumu,
                NobetUstGrupKisit = NobetUstGrupKisit(data.Kisitlar, "k41"),//oncekiAylarAyniGunNobet,
                EczaneGruplar = data.OncekiAylardaAyniGunNobetTutanEczaneler,
                Tarihler = haftaIciGunleriTumu, //data.TarihAraligi,
                KararDegiskeni = _x
            };
            EsGruptakiEczanelereAyniGunNobetYazma(esGrubaAyniGunNobetYazmaOncekiAylar);

            var sonrakiAylarAyniGunIstekGirilenler = new KpEsGrubaAyniGunNobetYazma
            {
                Model = model,
                EczaneNobetTarihAralik = data.EczaneNobetTarihAralik,
                EczaneNobetSonuclar = data.EczaneGrupNobetSonuclarTumu,
                NobetUstGrupKisit = NobetUstGrupKisit(data.Kisitlar, "k41"),//oncekiAylarAyniGunNobet,
                EczaneGruplar = data.SonrakiDonemAyniGunNobetIstekGirilenler,
                Tarihler = data.TarihAraligi,
                KararDegiskeni = _x
            };
            EsGruptakiEczanelereAyniGunNobetYazma(sonrakiAylarAyniGunIstekGirilenler);

            #endregion

            #region Alt gruplarla aynı gün nöbet dağılımı

            #region veriler

            #region yenişehir

            //var altGrupluTakipEdilecekNobetGrupGorevTiplerYenisehir = new List<NobetGrupGorevTipDetay>() {
            //    new NobetGrupGorevTipDetay { NobetGrupId = 20 }, //Yenişehir-1
            //    new NobetGrupGorevTipDetay { NobetGrupId = 21 }, //Yenişehir-2
            //    new NobetGrupGorevTipDetay { NobetGrupId = 22 }  //Yenişehir-3
            //};

            #endregion

            var kpEsGrubaAyniGunNobetYazma = new KpEsGrubaAyniGunNobetYazma
            {
                Model = model,
                KararDegiskeni = _x
            };

            var gunGruplar = data.TarihAraligi.Select(s => new { s.GunGrupId, s.GunGrupAdi }).Distinct().OrderBy(o => o.GunGrupId).ToList();

            #endregion

            #region toroslar

            var altGrupluTakipEdilecekNobetGrupGorevTiplerToroslar = new List<NobetGrupGorevTipDetay>() {
                new NobetGrupGorevTipDetay { NobetGrupId = 15 }, //Toroslar-1
                new NobetGrupGorevTipDetay { NobetGrupId = 16 }, //Toroslar-2
            };

            var eczaneNobetTarihAralikAtlGrupluToroslar = data.EczaneNobetTarihAralik
                .Where(w => altGrupluTakipEdilecekNobetGrupGorevTiplerToroslar.Select(s => s.NobetGrupId).Contains(w.NobetGrupId)).ToList();

            var eczaneNobetSonuclarAltGruplaAyniGunToroslar = data.EczaneGrupNobetSonuclarTumu
                .Where(w => altGrupluTakipEdilecekNobetGrupGorevTiplerToroslar.Select(s => s.NobetGrupId).Contains(w.NobetGrupId)).ToList();

            foreach (var gunGrup in gunGruplar)
            {
                kpEsGrubaAyniGunNobetYazma.Tarihler = data.TarihAraligi.Where(w => w.GunGrupId == gunGrup.GunGrupId).Distinct().ToList();

                var kpEsGrubaAyniGunNobetYazmaToroslar = (KpEsGrubaAyniGunNobetYazma)kpEsGrubaAyniGunNobetYazma.Clone();

                kpEsGrubaAyniGunNobetYazmaToroslar.EczaneNobetTarihAralik = eczaneNobetTarihAralikAtlGrupluToroslar;
                kpEsGrubaAyniGunNobetYazmaToroslar.NobetUstGrupKisit = NobetUstGrupKisit(data.Kisitlar, "k58");
                kpEsGrubaAyniGunNobetYazmaToroslar.EczaneNobetSonuclar = GetSonuclarByGunGrup(eczaneNobetSonuclarAltGruplaAyniGunToroslar, gunGrup.GunGrupId);
                kpEsGrubaAyniGunNobetYazmaToroslar.EczaneGruplar = GetEczaneGruplarByEczaneGrupTanimTipId(data.AltGruplarlaAyniGunNobetTutmayacakEczanelerToroslar, gunGrup.GunGrupId);

                EsGruptakiEczanelereAyniGunNobetYazma(kpEsGrubaAyniGunNobetYazmaToroslar);
            }

            #endregion

            #region yenişehir 1-2

            var altGruplaTakipEdileceklerYenisehir1_2 = new List<NobetGrupGorevTipDetay>() {
                new NobetGrupGorevTipDetay { NobetGrupId = 20 }, //Yenişehir-1
                new NobetGrupGorevTipDetay { NobetGrupId = 21 }, //Yenişehir-2
                //new NobetGrupGorevTipDetay { NobetGrupId = 22 }  //Yenişehir-3
            };

            var grpSayi_1_2 = data.NobetGrupGorevTipler.Select(s => s.NobetGrupId).Where(w => altGruplaTakipEdileceklerYenisehir1_2.Select(s => s.NobetGrupId).Contains(w)).Count();

            if (grpSayi_1_2 == 2)
            {
                var eczaneNobetTarihAralikAtlGrupluYenisehir1_2 = data.EczaneNobetTarihAralik
                .Where(w => altGruplaTakipEdileceklerYenisehir1_2.Select(s => s.NobetGrupId).Contains(w.NobetGrupId)).ToList();

                var eczaneNobetSonuclarAltGruplaAyniGunYenisehir1_2 = data.EczaneGrupNobetSonuclarTumu
                    .Where(w => altGruplaTakipEdileceklerYenisehir1_2.Select(s => s.NobetGrupId).Contains(w.NobetGrupId)).ToList();

                var y12 = data.AltGruplarlaAyniGunNobetTutmayacakEczanelerYenisehir1_2;
                    //.Where(w => w.NobetGrupId == 20 || w.NobetGrupId == 21)
                    //.ToList();

                foreach (var gunGrup in gunGruplar)
                {
                    kpEsGrubaAyniGunNobetYazma.Tarihler = data.TarihAraligi.Where(w => w.GunGrupId == gunGrup.GunGrupId).Distinct().ToList();

                    var kpEsGrubaAyniGunNobetYazmaYeniSehir = (KpEsGrubaAyniGunNobetYazma)kpEsGrubaAyniGunNobetYazma.Clone();

                    kpEsGrubaAyniGunNobetYazmaYeniSehir.EczaneNobetTarihAralik = eczaneNobetTarihAralikAtlGrupluYenisehir1_2;
                    kpEsGrubaAyniGunNobetYazmaYeniSehir.NobetUstGrupKisit = NobetUstGrupKisit(data.Kisitlar, "k29");
                    kpEsGrubaAyniGunNobetYazmaYeniSehir.EczaneNobetSonuclar = GetSonuclarByGunGrup(eczaneNobetSonuclarAltGruplaAyniGunYenisehir1_2, gunGrup.GunGrupId);
                    kpEsGrubaAyniGunNobetYazmaYeniSehir.EczaneGruplar = GetEczaneGruplarByEczaneGrupTanimTipId(y12, gunGrup.GunGrupId);

                    EsGruptakiEczanelereAyniGunNobetYazma(kpEsGrubaAyniGunNobetYazmaYeniSehir);
                }
            }

            #endregion

            #region yenişehir 3-2

            var altGruplaTakipEdileceklerYenisehir3_2 = new List<NobetGrupGorevTipDetay>() {
                //new NobetGrupGorevTipDetay { NobetGrupId = 20 }, //Yenişehir-1
                new NobetGrupGorevTipDetay { NobetGrupId = 21 }, //Yenişehir-2
                new NobetGrupGorevTipDetay { NobetGrupId = 22 }  //Yenişehir-3
            };

            var grpSayi_3_2 = data.NobetGrupGorevTipler.Select(s => s.NobetGrupId).Where(w => altGruplaTakipEdileceklerYenisehir3_2.Select(s => s.NobetGrupId).Contains(w)).Count();

            if (grpSayi_3_2 == 2)
            {
                var eczaneNobetTarihAralikAtlGrupluYenisehir3_2 = data.EczaneNobetTarihAralik
                    .Where(w => altGruplaTakipEdileceklerYenisehir3_2.Select(s => s.NobetGrupId).Contains(w.NobetGrupId)).ToList();

                var eczaneNobetSonuclarAltGruplaAyniGunYenisehir3_2 = data.EczaneGrupNobetSonuclarTumu
                    .Where(w => altGruplaTakipEdileceklerYenisehir3_2.Select(s => s.NobetGrupId).Contains(w.NobetGrupId)).ToList();

                var y32 = data.AltGruplarlaAyniGunNobetTutmayacakEczanelerYenisehir3_2;
                    //.Where(w => w.NobetGrupId == 21 || w.NobetGrupId == 22)
                    //.ToList();

                foreach (var gunGrup in gunGruplar)
                {
                    kpEsGrubaAyniGunNobetYazma.Tarihler = data.TarihAraligi.Where(w => w.GunGrupId == gunGrup.GunGrupId).Distinct().ToList();

                    var kpEsGrubaAyniGunNobetYazmaYeniSehir = (KpEsGrubaAyniGunNobetYazma)kpEsGrubaAyniGunNobetYazma.Clone();

                    kpEsGrubaAyniGunNobetYazmaYeniSehir.EczaneNobetTarihAralik = eczaneNobetTarihAralikAtlGrupluYenisehir3_2;
                    kpEsGrubaAyniGunNobetYazmaYeniSehir.NobetUstGrupKisit = NobetUstGrupKisit(data.Kisitlar, "k29");
                    kpEsGrubaAyniGunNobetYazmaYeniSehir.EczaneNobetSonuclar = GetSonuclarByGunGrup(eczaneNobetSonuclarAltGruplaAyniGunYenisehir3_2, gunGrup.GunGrupId);
                    kpEsGrubaAyniGunNobetYazmaYeniSehir.EczaneGruplar = GetEczaneGruplarByEczaneGrupTanimTipId(y32, gunGrup.GunGrupId);

                    EsGruptakiEczanelereAyniGunNobetYazma(kpEsGrubaAyniGunNobetYazmaYeniSehir);
                }
            }

            #endregion

            #endregion

            #region Tarih aralığı içinde aynı gün nöbet

            var ayIcindeSadece1KezAyniGunNobetKisit = new KpAyIcindeSadece1KezAyniGunNobet
            {
                Model = model,
                EczaneNobetTarihAralik = data.EczaneNobetTarihAralik,
                IkiliEczaneler = data.IkiliEczaneler,
                NobetUstGrupKisit = NobetUstGrupKisit(data.Kisitlar, "k10"), //ayIcindeAyniGunNobet,
                Tarihler = data.TarihAraligi,
                KararDegiskeni = _x
            };
            AyIcindeSadece1KezAyniGunNobetTutulsun(ayIcindeSadece1KezAyniGunNobetKisit);

            #endregion

            #endregion

            return model;
        }

        public EczaneNobetSonucModel Solve(MersinMerkezDataModelV2 data)
        {
            var results = new EczaneNobetSonucModel();
            var calismaSayisiEnFazla = data.CalismaSayisiLimit;

            var config = new Configuration
            {
                NameHandling = NameHandlingStyle.Manual,
                ComputeRemovedVariables = true
            };

            try
            {
                using (var scope = new ModelScope(config))
                {
                    var model = Model(data);

                    // Get a solver instance, change your solver
                    var solverConfig = new CplexSolverConfiguration();

                    if (data.TimeLimit > 0)
                    {
                        solverConfig.ComputeIIS = data.TimeLimit > 0 ? true : false;
                        solverConfig.TimeLimit = data.TimeLimit;
                    }
                    else
                    {
                        solverConfig.ComputeIIS = false;
                        //solverConfig.TimeLimit = data.TimeLimit;
                    }

                    var solver = new CplexSolver(solverConfig);

                    //solver.Abort();
                    //solver.Configuration.TimeLimit = 1;

                    // solve the model
                    var solution = solver.Solve(model);

                    //Assert.IsTrue(solution.ConflictingSet.ConstraintsLB.Contains(brokenConstraint1));
                    //Assert.IsTrue(solution.ConflictingSet.ConstraintsLB.Contains(brokenConstraint2));

                    var modelStatus = solution.ModelStatus;
                    var solutionStatus = solution.Status;
                    var modelName = solution.ModelName;

                    if (modelStatus != ModelStatus.Feasible)
                    {
                        //data.CalismaSayisi++;

                        if (data.CalismaSayisi == calismaSayisiEnFazla)
                        {
                            results.Celiskiler = CeliskileriEkle(solution);
                        }

                        throw new Exception($"Uygun çözüm bulunamadı!.");
                    }
                    //else
                    //{
                    //    var ayIcindeSadece1KezAyniGunNobetKisit = new KpAyIcindeSadece1KezAyniGunNobet
                    //    {
                    //        Model = model,
                    //        EczaneNobetTarihAralik = data.EczaneNobetTarihAralik,
                    //        IkiliEczaneler = data.IkiliEczaneler,
                    //        NobetUstGrupKisit = NobetUstGrupKisit(data.Kisitlar, "k10"),
                    //        Tarihler = data.TarihAraligi,
                    //        KararDegiskeni = _x
                    //    };
                    //    AyIcindeSadece1KezAyniGunNobetTutulsun(ayIcindeSadece1KezAyniGunNobetKisit);

                    //    //var kpAyIcindeSadece1KezAyniGunNobetDegiskenDonusumlu = new KpAyIcindeSadece1KezAyniGunNobetDegiskenDonusumlu
                    //    //{
                    //    //    Model = model,
                    //    //    EczaneNobetTarihAralik = data.EczaneNobetTarihAralik,
                    //    //    EczaneNobetTarihAralikIkiliEczaneler = data.EczaneNobetTarihAralikIkiliEczaneler,
                    //    //    IkiliEczaneler = data.IkiliEczaneler,
                    //    //    NobetUstGrupKisit = NobetUstGrupKisit(data.Kisitlar, "k10"),
                    //    //    Tarihler = data.TarihAraligi,
                    //    //    KararDegiskeni = _x,
                    //    //    KararDegiskeniIkiliEczaneler = _y
                    //    //};
                    //    //AyIcindeSadece1KezAyniGunNobetTutulsun(kpAyIcindeSadece1KezAyniGunNobetDegiskenDonusumlu);

                    //    if (!NobetUstGrupKisit(data.Kisitlar, "k10").PasifMi)
                    //    {
                    //        solution = solver.Solve(model);

                    //        modelStatus = solution.ModelStatus;

                    //        if (modelStatus != ModelStatus.Feasible)
                    //        {
                    //            //data.CalismaSayisi++;

                    //            if (data.CalismaSayisi == calismaSayisiEnFazla)
                    //            {
                    //                results.Celiskiler = CeliskileriEkle(solution);
                    //            }

                    //            throw new Exception($"Uygun çözüm bulunamadı! k10 aynı gün 2 kez nöbet.");
                    //        }
                    //    }                      
                    //}

                    // import the results back into the model 
                    model.VariableCollections.ForEach(vc => vc.SetVariableValues(solution.VariableValues));
                    var objective = solution.ObjectiveValues.Single();
                    var sure = solution.OverallWallTime;
                    var bestBound = solution.BestBound;
                    var bakilanDugumSayisi = solution.NumberOfExploredNodes;
                    var kisitSayisi = model.ConstraintsCount;

                    results.CozumSuresi = sure;
                    results.ObjectiveValue = objective.Value;
                    results.BakilanDugumSayisi = bakilanDugumSayisi;
                    results.KisitSayisi = kisitSayisi;
                    results.ResultModel = new List<EczaneNobetCozum>();
                    results.KararDegikeniSayisi = data.EczaneNobetTarihAralik.Count;
                    results.CalismaSayisi = data.CalismaSayisi;
                    results.NobetGrupSayisi = data.NobetGruplar.Count;
                    results.IncelenenEczaneSayisi = data.EczaneNobetGruplar.Count;

                    var sonuclar = data.EczaneNobetTarihAralik.Where(s => _x[s].Value.IsAlmost(1) == true).ToList();

                    var nobetGrupTarihler1 = data.EczaneNobetTarihAralik
                         .Select(s => new
                         {
                             s.NobetGrupId,
                             s.Tarih,
                             s.NobetGorevTipId,
                             Talep = s.TalepEdilenNobetciSayisi
                         }).Distinct().ToList();

                    var toplamArz = sonuclar.Count;
                    var toplamTalep = nobetGrupTarihler1.Sum(s => s.Talep);

                    if (toplamArz != toplamTalep)
                    {
                        throw new Exception("Talebi karşılanmayan günler var");
                    }

                    foreach (var r in sonuclar)
                    {
                        results.ResultModel.Add(new EczaneNobetCozum()
                        {
                            TakvimId = r.TakvimId,
                            EczaneNobetGrupId = r.EczaneNobetGrupId,
                            NobetGorevTipId = r.NobetGorevTipId
                        });
                    }
                }
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                data.CalismaSayisi++;

                if (data.CalismaSayisi <= calismaSayisiEnFazla)//10
                {
                    results = Solve(data);
                }
                else if (ex.Message.StartsWith("Uygun çözüm bulunamadı"))
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

                    var celiskiler = results.Celiskiler.Split('*');

                    mesaj = CeliskileriTabloyaAktar(data.BaslangicTarihi, data.BitisTarihi, data.CalismaSayisi, iterasyonMesaj, data.NobetGrupGorevTipler, celiskiler);

                    throw new Exception(mesaj);
                }
                else
                {
                    throw ex;
                }
            }
            return results;
        }

        public EczaneNobetSonucModel SolveMersin(MersinMerkezDataModelV2 data)
        {
            var results = new EczaneNobetSonucModel();
            var calismaSayisiEnFazla = data.CalismaSayisiLimit;

            var config = new Configuration
            {
                NameHandling = NameHandlingStyle.Manual,
                ComputeRemovedVariables = true
            };

            try
            {
                using (var scope = new ModelScope(config))
                {
                    var model = Model(data);

                    // Get a solver instance, change your solver
                    var solverConfig = new CplexSolverConfiguration()
                    {
                        ComputeIIS = true,
                        TimeLimit = data.TimeLimit
                    };

                    var solver = new CplexSolver(solverConfig);
                    // Get a solver instance, change your solver

                    //solver.Abort();
                    //solver.Configuration.TimeLimit = 1;

                    //var yol = @"C:\temp\testmodel.mps";
                    //var mpsStream = new StreamWriter(yol);
                    //var mpsExporter = new MPSExporter(mpsStream.BaseStream);
                    //mpsExporter.Write(model);
                    //mpsStream.Close();

                    //var lpStream = new StreamWriter(@"C:\temp\testmodel.lp");
                    //var lpExporter = new LPExporter(lpStream.BaseStream);
                    //lpExporter.Write(model);
                    //lpStream.Close();

                    // solve the model
                    var solution = solver.Solve(model);

                    //Assert.IsTrue(solution.ConflictingSet.ConstraintsLB.Contains(brokenConstraint1));
                    //Assert.IsTrue(solution.ConflictingSet.ConstraintsLB.Contains(brokenConstraint2));

                    var modelStatus = solution.ModelStatus;
                    var solutionStatus = solution.Status;
                    var modelName = solution.ModelName;

                    if (modelStatus != ModelStatus.Feasible)
                    {
                        if (data.CalismaSayisi == calismaSayisiEnFazla)
                        {
                            results.Celiskiler = CeliskileriEkle(solution);
                        }

                        throw new Exception($"Uygun çözüm bulunamadı!.");
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

                        results.CozumSuresi = sure;
                        results.ObjectiveValue = objective.Value;
                        results.BakilanDugumSayisi = bakilanDugumSayisi;
                        results.KisitSayisi = kisitSayisi;
                        results.ResultModel = new List<EczaneNobetCozum>();
                        results.KararDegikeniSayisi = data.EczaneNobetTarihAralik.Count;
                        results.CalismaSayisi = data.CalismaSayisi;
                        results.NobetGrupSayisi = data.NobetGruplar.Count;
                        results.IncelenenEczaneSayisi = data.EczaneNobetGruplar.Count;

                        var sonuclar = data.EczaneNobetTarihAralik.Where(s => _x[s].Value.IsAlmost(1) == true).ToList();

                        var nobetGorevTipId = 1;

                        var nobetGrupTarihler1 = data.EczaneNobetTarihAralik
                             .Where(w => w.NobetGorevTipId == nobetGorevTipId)
                             .Select(s => new
                             {
                                 s.NobetGrupId,
                                 s.Tarih,
                                 s.NobetGorevTipId,
                                 Talep = s.TalepEdilenNobetciSayisi
                                 //data.NobetGrupTalepler
                                 // .Where(w => w.NobetGrupGorevTipId == s.NobetGrupGorevTipId
                                 //         && w.TakvimId == s.TakvimId).SingleOrDefault() == null
                                 //? (int)data.NobetGrupKurallar
                                 //    .Where(k => k.NobetKuralId == 3
                                 //             && k.NobetGrupGorevTipId == s.NobetGrupGorevTipId)
                                 //    .Select(k => k.Deger).SingleOrDefault()
                                 //: data.NobetGrupTalepler
                                 // .Where(w => w.NobetGrupGorevTipId == s.NobetGrupGorevTipId
                                 //         && w.TakvimId == s.TakvimId).SingleOrDefault().NobetciSayisi
                             }).Distinct().ToList();

                        var toplamArz = sonuclar.Count;
                        var toplamTalep = nobetGrupTarihler1.Sum(s => s.Talep);

                        if (toplamArz != toplamTalep)
                        {
                            throw new Exception("Talebi karşılanmayan günler var");
                        }

                        foreach (var r in sonuclar //data.EczaneNobetTarihAralik.Where(s => _x[s].Value == 1)
                            )
                        {
                            results.ResultModel.Add(new EczaneNobetCozum()
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
                throw ex;
            }
            catch (Exception ex)
            {
                data.CalismaSayisi++;

                //if (ex.Message.Contains("Make sure"))
                //{

                //}
                if (data.CalismaSayisi <= calismaSayisiEnFazla)//10
                {
                    results = Solve(data);
                }
                else if (ex.Message.StartsWith("Uygun çözüm bulunamadı"))
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

                    //$"<strong><span class='text-danger'>Çözüm bulunamadı.</strong> "
                    //      + "<br/> "
                    //      +

                    var celiskiler = results.Celiskiler.Split('*');

                    mesaj = "Tabloya göre <a href=\"/EczaneNobet/NobetUstGrupKisit/KisitAyarla\" class=\"card-link\" target=\"_blank\">nöbet ayarlarında</a> bazı değişiklikler yaparak <strong>tekrar çözmelisiniz..</strong>"
                          + "<hr /> "
                          + $"<strong>Kontrol edilecek kurallar <span class='badge badge-warning'>{celiskiler[1]}</span></strong>"
                          + "<br /> "
                          + celiskiler[0];

                    mesaj += $"<strong>Nöbet seçenekleri:</strong>" +
                        $"<br />Nöbet Grupları: {cozulenNobetGruplar}" +
                        $"<br />Tarih Aralığı: <strong>{data.BaslangicTarihi.ToShortDateString()}-{data.BitisTarihi.ToShortDateString()}</strong>" +
                        $"<hr/> " +
                        $"<strong>Çalışma adımları <span class='badge badge-info'>{data.CalismaSayisi}</span></strong>" +
                        $"{iterasyonMesaj} "
                        ;

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
                        mesaj += "<br /> " + i + " " + calismaAdimlari[i];
                    }

                    throw new Exception(mesaj);
                }
                else
                {
                    throw ex;
                }
            }
            return results;
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

        public void ModeliKapat()
        {
            //_solver.Abort();

            var field = typeof(ModelScope).GetRuntimeProperties().Single(rp => rp.Name == "Current");
            ModelScope reflectedScope = (ModelScope)field.GetValue(null);
            if (reflectedScope != null)
            {
                // dispose registered ModelScope
                reflectedScope.Dispose();
            }
            // create a new ModelScope, it will register with ModelScope.Current itself
            //var scope = new ModelScope(scopeSettings);
        }

    }
}

