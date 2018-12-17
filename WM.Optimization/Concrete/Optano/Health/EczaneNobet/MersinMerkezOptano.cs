﻿using OPTANO.Modeling.Common;
using OPTANO.Modeling.Optimization;
using OPTANO.Modeling.Optimization.Configuration;
using OPTANO.Modeling.Optimization.Enums;
using OPTANO.Modeling.Optimization.Solver;
using OPTANO.Modeling.Optimization.Solver.Cplex126;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.Optimization.EczaneNobet;
using WM.Optimization.Abstract.Health;
using WM.Optimization.Entities.KisitParametre;

namespace WM.Optimization.Concrete.Optano.Health.EczaneNobet
{
    public class MersinMerkezOptano : EczaneNobetKisit, IEczaneNobetMersinMerkezOptimizationV2
    {
        #region Değişkenler
        //Karar değişkeni model çalıştıktan sonra değer aldığından burada tanımlandı
        private VariableCollection<EczaneNobetTarihAralik> _x { get; set; }
        private VariableCollection<EczaneNobetAltGrupTarihAralik> y { get; set; }
        #endregion

        private Model Model(MersinMerkezDataModelV2 data)
        {
            var model = new Model() { Name = "Mersin Merkez Eczane Nöbet" };

            #region veriler

            #region kısıtlar

            var herAyPespeseGorev = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "herAyPespeseGorev", data.NobetUstGrupId);
            var farkliAyPespeseGorev = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "farkliAyPespeseGorev", data.NobetUstGrupId);
            var pazarPespeseGorevEnAz = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "pazarPespeseGorevEnAz", data.NobetUstGrupId);

            var herAyEnFazlaGorev = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "herAyEnFazlaGorev", data.NobetUstGrupId);
            var herAyEnFazlaHaftaIci = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "herAyEnFazlaHaftaIci", data.NobetUstGrupId);

            var haftaIciPespeseGorevEnAz = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "haftaIciPespeseGorevEnAz", data.NobetUstGrupId);
            var cumartesiPespeseGorevEnAz = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "cumartesiPespeseGorevEnAz", data.NobetUstGrupId);

            var gunKumulatifToplamEnFazla = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "gunKumulatifToplamEnFazla", data.NobetUstGrupId);
            var herAyEnFazla1HaftaIciGunler = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "herAyEnFazla1HaftaIciGunler", data.NobetUstGrupId);

            var herAyEnFazla1Pazar = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "herAyEnFazla1Pazar", data.NobetUstGrupId);
            var herAyEnFazla1Cumartesi = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "herAyEnFazla1Cumartesi", data.NobetUstGrupId);
            var herAyEnFazla1Cuma = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "herAyEnFazla1Cuma", data.NobetUstGrupId);

            var bayramToplamEnFazla = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "bayramToplamEnFazla", data.NobetUstGrupId);
            var bayramPespeseFarkliTur = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "bayramPespeseFarkliTur", data.NobetUstGrupId);
            var herAyEnFazla1Bayram = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "herAyEnFazla1Bayram", data.NobetUstGrupId);

            var eczaneGrup = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "eczaneGrup", data.NobetUstGrupId);
            var oncekiAylarAyniGunNobet = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "oncekiAylarAyniGunNobet", data.NobetUstGrupId);

            var istek = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "istek", data.NobetUstGrupId);
            var mazeret = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "mazeret", data.NobetUstGrupId);

            var pespeseHaftaIciAyniGunNobet = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "pespeseHaftaIciAyniGunNobet", data.NobetUstGrupId);
            var nobetBorcOdeme = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "nobetBorcOdeme", data.NobetUstGrupId);

            var ayIcindeAyniGunNobet = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "ayIcindeAyniGunNobet", data.NobetUstGrupId);
            var altGruplarlaAyniGunNobetTutma = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "altGruplarlaAyniGunNobetTutma", data.NobetUstGrupId);
            var ikiliEczaneAyniGunNobet = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "ikiliEczaneAyniGunNobet", data.NobetUstGrupId);

            //pasifler
            var herAyEnaz1Gorev = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "herAyEnaz1Gorev", data.NobetUstGrupId);

            var haftaninGunleriDagilimi = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "haftaninGunleriDagilimi", data.NobetUstGrupId);
            var herAyEnaz1HaftaIciGorev = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "herAyEnaz1HaftaIciGorev", data.NobetUstGrupId);

            var toplamMaxHedef = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "toplamMaxHedef", data.NobetUstGrupId);
            var haftaIciToplamMaxHedef = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "haftaIciToplamMaxHedef", data.NobetUstGrupId);
            var toplamCumaCumartesiMaxHedef = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "toplamCumaCumartesiMaxHedef", data.NobetUstGrupId);
            var toplamCumaMaxHedef = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "toplamCumaMaxHedef", data.NobetUstGrupId);
            var toplamCumartesiMaxHedef = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "toplamCumartesiMaxHedef", data.NobetUstGrupId);

            var toplamMinHedef = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "toplamMinHedef", data.NobetUstGrupId);
            var haftaIciToplamMinHedef = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "haftaIciToplamMinHedef", data.NobetUstGrupId);
            var toplamCumaCumartesiMinHedef = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "toplamCumaCumartesiMinHedef", data.NobetUstGrupId);
            var toplamCumartesiMinHedef = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "toplamCumartesiMinHedef", data.NobetUstGrupId);
            //var performansNoktasi_2 = new PerformansTakip { PerformansNoktasi = "2: Kisitlar", Sure = stopwatch.Elapsed };
            //performansTakip.Add(performansNoktasi_2);
            #endregion

            #region tur çevrim katsayıları            

            int haftaIciCevrim = 500;
            int cumartesiCevrim = 900;
            int bayramCevrim = 8000;
            int pazarCevrim = 1000;

            //var knt = data.EczaneNobetGrupGunKuralIstatistikler
            //    .Where(w => w.EczaneNobetGrupId == 415).ToList();
            #endregion

            //özel tur takibi yapılacak günler
            var ozelTurTakibiYapilacakGunler =
                new List<int> {
                    1 // pazar
                };

            var herAyEnFazlaIlgiliKisit = new NobetUstGrupKisitDetay();

            var bayramlarTumu = data.TarihAraligi.Where(w => w.NobetGunKuralId > 7).OrderBy(o => o.Tarih).ToList();
            var pazarGunleriTumu = data.TarihAraligi.Where(w => w.NobetGunKuralId == 1).OrderBy(o => o.Tarih).ToList();
            var haftaIciGunleriTumu = data.TarihAraligi.Where(w => (w.NobetGunKuralId >= 2 && w.NobetGunKuralId < 7)).OrderBy(o => o.Tarih).ToList();
            var cumartesiGunleriTumu = data.TarihAraligi.Where(w => (w.NobetGunKuralId == 7)).OrderBy(o => o.Tarih).ToList();

            #region Karar Değişkenleri
            _x = new VariableCollection<EczaneNobetTarihAralik>(
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

            #endregion 

            #endregion

            #region Amaç Fonksiyonu

            var amac1 = Expression.Sum(
                                (from i in data.EczaneNobetTarihAralik
                                 from p in data.EczaneNobetGrupGunKuralIstatistikYatay
                                 where i.EczaneNobetGrupId == p.EczaneNobetGrupId
                                    && i.NobetGorevTipId == p.NobetGorevTipId
                                 select (_x[i]
                                        //ilk yazılan nöbet öncelikli olsun:
                                        + _x[i] * Convert.ToInt32(i.BayramMi)
                                                * (bayramCevrim + bayramCevrim / Math.Sqrt((i.Tarih - p.SonNobetTarihiBayram).TotalDays))

                                        + _x[i] * Convert.ToInt32(i.CumartesiGunuMu)
                                                * (cumartesiCevrim + cumartesiCevrim / Math.Sqrt((i.Tarih - p.SonNobetTarihiCumartesi).TotalDays * Math.Ceiling((double)i.Tarih.Day / 7)))

                                        + _x[i] * Convert.ToInt32(i.PazarGunuMu)
                                                * (pazarCevrim + pazarCevrim / Math.Sqrt((i.Tarih - p.SonNobetTarihiPazar).TotalDays * Math.Ceiling((double)i.Tarih.Day / 7)))

                                        + _x[i] * Convert.ToInt32(i.HaftaIciMi)
                                                * (haftaIciCevrim + haftaIciCevrim / Math.Sqrt((i.Tarih - p.SonNobetTarihiHaftaIci).TotalDays * i.Tarih.Day)
                                                * (pespeseHaftaIciAyniGunNobet.PasifMi == false
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

            foreach (var nobetGrupGorevTip in data.NobetGrupGorevTipler)
            {
                #region ön hazırlama

                #region tarihler

                var tarihler = data.TarihAraligi
                    .Where(w => w.NobetGrupId == nobetGrupGorevTip.NobetGrupId
                             && w.NobetGorevTipId == nobetGrupGorevTip.NobetGorevTipId).ToList();
                var pazarGunleri = tarihler.Where(w => w.GunGrupId == 1).OrderBy(o => o.Tarih).ToList();
                var cumaGunleri = tarihler.Where(w => w.NobetGunKuralId == 6).OrderBy(o => o.Tarih).ToList();
                var cumaVeCumartesiGunleri = tarihler.Where(w => w.NobetGunKuralId == 6 || w.NobetGunKuralId == 7).OrderBy(o => o.Tarih).ToList();
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
                    .Where(w => w.NobetGrupId == nobetGrupGorevTip.NobetGrupId)
                    .OrderBy(x => //x.EczaneAdi
                                  r.NextDouble()
                            ).ToList();

                var eczaneNobetGrupGunKuralIstatistikler = data.EczaneNobetGrupGunKuralIstatistikYatay
                    .Where(w => w.NobetGrupId == nobetGrupGorevTip.NobetGrupId).ToList();

                var gruptakiEczaneSayisi = eczaneNobetGruplar.Select(s => s.EczaneId).Count();

                // karar değişkeni - nöbet grup bazlı filtrelenmiş
                var eczaneNobetTarihAralikGrupBazli = data.EczaneNobetTarihAralik
                           .Where(e => e.NobetGorevTipId == nobetGrupGorevTip.NobetGorevTipId
                                    && e.NobetGrupId == nobetGrupGorevTip.NobetGrupId).ToList();

                var nobetGrupGunKurallar = data.NobetGrupGunKurallar
                                                    .Where(s => s.NobetGrupId == nobetGrupGorevTip.NobetGrupId
                                                             && nobetGunKurallar.Contains(s.NobetGunKuralId))
                                                    .Select(s => s.NobetGunKuralId).ToList();

                //var nobetGrupGorevTipler = data.NobetGrupGorevTipler.Where(w => w.NobetGrupId == nobetGrupGorevTip.NobetGrupId).ToList();
                var nobetGrupKurallar = data.NobetGrupKurallar.Where(w => w.NobetGrupId == nobetGrupGorevTip.NobetGrupId).ToList();
                var nobetGrupTalepler = data.NobetGrupTalepler.Where(w => w.NobetGrupId == nobetGrupGorevTip.NobetGrupId).ToList();

                var pespeseNobetSayisi = (int)nobetGrupKurallar
                        .Where(s => s.NobetKuralId == 1)
                        .Select(s => s.Deger).SingleOrDefault();

                int gunlukNobetciSayisi = (int)nobetGrupKurallar
                        .Where(s => s.NobetKuralId == 3)
                        .Select(s => s.Deger).SingleOrDefault();

                #region peş peşe görev en az nöbet zamanı
                //hafta içi
                var farkliAyPespeseGorevAraligi = (gruptakiEczaneSayisi / gunlukNobetciSayisi * 1.2);
                var altLimit = farkliAyPespeseGorevAraligi * 0.7666; //0.95
                var ustLimit = farkliAyPespeseGorevAraligi + farkliAyPespeseGorevAraligi * 0.6667; //77;
                var ustLimitKontrol = ustLimit * 0.95; //0.81 
                //pazar
                var pazarNobetiYazilabilecekIlkAy = (int)Math.Ceiling((double)gruptakiEczaneSayisi / 5) - 1;
                var cumartersiNobetiYazilabilecekIlkAy = (int)Math.Ceiling((double)gruptakiEczaneSayisi / 5) - 1;
                #endregion

                var haftaIciOrtamalaNobetSayisi = OrtalamaNobetSayisi(gunlukNobetciSayisi, gruptakiEczaneSayisi, haftaIciSayisi);
                var cumartesiOrtamalaNobetSayisi = OrtalamaNobetSayisi(gunlukNobetciSayisi, gruptakiEczaneSayisi, cumartesiSayisi);
                var pazarOrtamalaNobetSayisi = OrtalamaNobetSayisi(gunlukNobetciSayisi, gruptakiEczaneSayisi, pazarSayisi);
                var ortalamaNobetSayisi = OrtalamaNobetSayisi(gunlukNobetciSayisi, gruptakiEczaneSayisi, gunSayisi);
                var bayramOrtalamaNobetSayisi = OrtalamaNobetSayisi(gunlukNobetciSayisi, gruptakiEczaneSayisi, bayramSayisi);

                var eczaneNobetSonuclarGrupBazliTumu = data.EczaneNobetSonuclar
                    .Where(w => w.NobetGrupId == nobetGrupGorevTip.NobetGrupId).ToList();

                var eczaneNobetSonuclarGrupBazli = eczaneNobetSonuclarGrupBazliTumu
                    .Where(w => w.Tarih >= data.NobetUstGrupBaslangicTarihi).ToList();


                var nobetGrupBayramNobetleri = eczaneNobetSonuclarGrupBazli
                    .Where(w => w.NobetGunKuralId > 7//bayramlar.Select(s => s.TakvimId).Contains(w.TakvimId) 
                             && w.Tarih >= data.NobetUstGrupBaslangicTarihi)
                    .Select(s => new { s.TakvimId, s.NobetGunKuralId }).ToList();

                #region hafta içi dağılım
                var haftaIciOrtamalaNobetSayisi2 = haftaIciOrtamalaNobetSayisi;

                if (herAyEnFazlaHaftaIci.SagTarafDegeri > 0)
                    haftaIciOrtamalaNobetSayisi2 += (int)herAyEnFazlaHaftaIci.SagTarafDegeri;

                var yazilabilecekHaftaIciNobetSayisi = haftaIciOrtamalaNobetSayisi2;// - eczaneNobetIstatistik.NobetSayisiHaftaIci; 
                if (data.CalismaSayisi >= 1)
                    yazilabilecekHaftaIciNobetSayisi++;

                #endregion

                var nobetGunKuralIstatistikler = data.TakvimNobetGrupGunDegerIstatistikler
                                      .Where(w => w.NobetGrupId == nobetGrupGorevTip.NobetGrupId
                                               && tarihler.Select(s => s.NobetGunKuralId).Distinct().Contains(w.NobetGunKuralId)).ToList();

                var nobetGunKuralTarihler = new List<NobetGunKuralTarihAralik>();

                foreach (var item in nobetGunKuralIstatistikler)
                {
                    var tarihler2 = tarihler.Where(w => w.NobetGunKuralId == item.NobetGunKuralId).ToList();
                    var gunKuralGunSayisi = tarihler2.Count;

                    nobetGunKuralTarihler.Add(new NobetGunKuralTarihAralik
                    {
                        NobetGunKuralId = item.NobetGunKuralId,
                        NobetGunKuralAdi = item.NobetGunKuralAdi,
                        TakvimNobetGruplar = tarihler2,
                        GunSayisi = gunKuralGunSayisi,
                        OrtalamaNobetSayisi = OrtalamaNobetSayisi(gunlukNobetciSayisi, gruptakiEczaneSayisi, gunKuralGunSayisi),
                        KumulatifGunSayisi = item.GunSayisi,
                        KumulatifOrtalamaNobetSayisi = OrtalamaNobetSayisi(gunlukNobetciSayisi, gruptakiEczaneSayisi, item.GunSayisi)
                    });
                }

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

                #region talebi karşıla

                var talebiKarsilaKisitParametreModel = new KpTalebiKarsila
                {
                    EczaneNobetTarihAralikTumu = eczaneNobetTarihAralikGrupBazli,
                    GunlukNobetciSayisi = gunlukNobetciSayisi,
                    NobetGrupTalepler = nobetGrupTalepler,
                    NobetGrupGorevTip = nobetGrupGorevTip,
                    Tarihler = tarihler,
                    Model = model,
                    KararDegiskeni = _x
                };

                TalebiKarsila(talebiKarsilaKisitParametreModel);

                #endregion

                #endregion

                #region Eczane bazlı kısıtlar

                foreach (var eczaneNobetGrup in eczaneNobetGruplar)
                {
                    #region kontrol

                    var kontrol = false;

                    if (kontrol)
                    {
                        var kontrolEdilecekEczaneler = new string[] { "DAMLA", "GÜNEY" };

                        if (kontrolEdilecekEczaneler.Contains(eczaneNobetGrup.EczaneAdi))
                        {
                        }
                    }
                    #endregion

                    #region eczane bazlı veriler

                    // karar değişkeni - eczane bazlı filtrelenmiş
                    var eczaneNobetTarihAralikEczaneBazli = eczaneNobetTarihAralikGrupBazli
                       .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id).ToList();

                    var eczaneNobetSonuclar = eczaneNobetSonuclarGrupBazliTumu
                                    .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id)
                                    .OrderBy(o => o.Tarih).ToList();

                    var eczaneNobetIstatistik = eczaneNobetGrupGunKuralIstatistikler
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
                        GunlukNobetciSayisi = gunlukNobetciSayisi,
                    };
                    #endregion    

                    #region aktif kısıtlar

                    #region Peş peşe nöbet        

                    var kpHerAyPespeseGorev = new KpHerAyPespeseGorev
                    {
                        Model = model,
                        EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazli,
                        NobetUstGrupKisit = herAyPespeseGorev,
                        Tarihler = tarihler,
                        PespeseNobetSayisi = pespeseNobetSayisi,
                        EczaneNobetGrup = eczaneNobetGrup,
                        KararDegiskeni = _x
                    };
                    HerAyPespeseGorev(kpHerAyPespeseGorev);

                    var kpHerAyHaftaIciPespeseGorev = new KpHerAyHaftaIciPespeseGorev
                    {
                        Model = model,
                        //Tarihler = tarihler,
                        HaftaIciGunleri = haftaIciGunleri,
                        HaftaIciOrtamalaNobetSayisi = haftaIciOrtamalaNobetSayisi,
                        EczaneNobetGrup = eczaneNobetGrup,
                        EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazli,
                        PespeseNobetSayisiAltLimit = gruptakiEczaneSayisi * 0.6, //altLimit,
                        NobetUstGrupKisit = haftaIciPespeseGorevEnAz,
                        KararDegiskeni = _x
                    };
                    HerAyHaftaIciPespeseGorev(kpHerAyHaftaIciPespeseGorev);

                    var pesPeseGorevEnAzFarkliAylar = new KpPesPeseGorevEnAz
                    {
                        Model = model,
                        NobetSayisi = eczaneNobetIstatistik.NobetSayisiToplam,
                        EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazli,
                        NobetUstGrupKisit = farkliAyPespeseGorev,
                        Tarihler = tarihler,
                        NobetYazilabilecekIlkTarih = nobetYazilabilecekIlkTarih,
                        EczaneNobetGrup = eczaneNobetGrup,
                        KararDegiskeni = _x
                    };
                    PesPeseGorevEnAz(pesPeseGorevEnAzFarkliAylar);

                    var pesPeseGorevEnAzPazar = new KpPesPeseGorevEnAz
                    {
                        Model = model,
                        NobetSayisi = eczaneNobetIstatistik.NobetSayisiPazar,
                        EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazli,
                        NobetUstGrupKisit = pazarPespeseGorevEnAz,
                        Tarihler = pazarGunleri,
                        NobetYazilabilecekIlkTarih = yazilabilecekIlkPazarTarihi,
                        EczaneNobetGrup = eczaneNobetGrup,
                        KararDegiskeni = _x
                    };
                    PesPeseGorevEnAz(pesPeseGorevEnAzPazar);

                    var pesPeseGorevEnAzHaftaIci = new KpPesPeseGorevEnAz
                    {
                        Model = model,
                        NobetSayisi = eczaneNobetIstatistik.NobetSayisiHaftaIci,
                        EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazli,
                        NobetUstGrupKisit = haftaIciPespeseGorevEnAz,
                        Tarihler = haftaIciGunleri,
                        NobetYazilabilecekIlkTarih = yazilabilecekIlkHaftaIciTarihi,
                        EczaneNobetGrup = eczaneNobetGrup,
                        KararDegiskeni = _x
                    };
                    PesPeseGorevEnAz(pesPeseGorevEnAzHaftaIci);

                    var pesPeseGorevEnAzCumartesi = new KpPesPeseGorevEnAz
                    {
                        Model = model,
                        NobetSayisi = eczaneNobetIstatistik.NobetSayisiCumartesi,
                        EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazli,
                        NobetUstGrupKisit = cumartesiPespeseGorevEnAz,
                        Tarihler = cumartesiGunleri,
                        NobetYazilabilecekIlkTarih = yazilabilecekIlkCumartesiTarihi,
                        EczaneNobetGrup = eczaneNobetGrup,
                        KararDegiskeni = _x
                    };
                    PesPeseGorevEnAz(pesPeseGorevEnAzCumartesi);

                    #endregion

                    #region Tarih aralığı ortalama en fazla                   

                    var tarihAraligiOrtalamaEnFazlaTumTarihAraligi = new KpTarihAraligiOrtalamaEnFazla
                    {
                        Model = model,
                        Tarihler = tarihler,
                        GunSayisi = gunSayisi,
                        OrtalamaNobetSayisi = ortalamaNobetSayisi,
                        EczaneNobetGrup = eczaneNobetGrup,
                        EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazli,
                        NobetUstGrupKisit = herAyEnFazlaGorev,
                        KararDegiskeni = _x
                    };
                    TarihAraligiOrtalamaEnFazla(tarihAraligiOrtalamaEnFazlaTumTarihAraligi);

                    var tarihAraligiOrtalamaEnFazlaHaftaIici = new KpTarihAraligiOrtalamaEnFazla
                    {
                        Model = model,
                        Tarihler = haftaIciGunleri,
                        GunSayisi = haftaIciSayisi,
                        OrtalamaNobetSayisi = haftaIciOrtamalaNobetSayisi,
                        EczaneNobetGrup = eczaneNobetGrup,
                        EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazli,
                        NobetUstGrupKisit = herAyEnFazlaHaftaIci,
                        KararDegiskeni = _x
                    };
                    TarihAraligiOrtalamaEnFazla(tarihAraligiOrtalamaEnFazlaHaftaIici);

                    var tarihAraligiOrtalamaEnFazlaBayram = new KpTarihAraligiOrtalamaEnFazla
                    {
                        Model = model,
                        Tarihler = bayramlar,
                        GunSayisi = bayramSayisi,
                        OrtalamaNobetSayisi = bayramOrtalamaNobetSayisi,
                        EczaneNobetGrup = eczaneNobetGrup,
                        EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazli,
                        NobetUstGrupKisit = herAyEnFazla1Bayram,
                        KararDegiskeni = _x
                    };
                    TarihAraligiOrtalamaEnFazla(tarihAraligiOrtalamaEnFazlaBayram);

                    #endregion

                    #region Kümülatif ve her ay en falza kısıtları

                    var nobetGunKuralNobetSayilari = new List<NobetGunKuralNobetSayisi>();

                    foreach (var gunKural in nobetGunKuralIstatistikler)
                    {
                        int toplamNobetSayisi = GetToplamGunKuralNobetSayisi(eczaneNobetIstatistik, gunKural.NobetGunKuralId);

                        nobetGunKuralNobetSayilari.Add(new NobetGunKuralNobetSayisi
                        {
                            NobetGunKuralAdi = gunKural.NobetGunKuralAdi,
                            NobetGunKuralId = gunKural.NobetGunKuralId,
                            NobetSayisi = toplamNobetSayisi
                        });
                    }

                    //var haftaIciEnAzVeEnCokNobetSayisiArasindakiFark = nobetGunKuralNobetSayilari.Max(m => m.NobetSayisi) - nobetGunKuralNobetSayilari.Min(m => m.NobetSayisi);
                    var haftaIciEnCokNobetSayisi = 0;
                    var haftaIciEnAzNobetSayisi = 0;

                    if (nobetGunKuralIstatistikler.Count > 0)
                    {
                        haftaIciEnCokNobetSayisi = nobetGunKuralNobetSayilari.Max(m => m.NobetSayisi);
                        haftaIciEnAzNobetSayisi = nobetGunKuralNobetSayilari.Min(m => m.NobetSayisi);
                    }
                    foreach (var gunKural in nobetGunKuralIstatistikler)
                    {//gun kural bazlı
                        if (kontrol && gunKural.NobetGunKuralAdi == "Cuma")
                        {
                        }

                        switch (gunKural.NobetGunKuralId)
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

                        var tarihAralik = nobetGunKuralTarihler.Where(w => w.NobetGunKuralId == gunKural.NobetGunKuralId).SingleOrDefault() ?? new NobetGunKuralTarihAralik();

                        //gün kural ortalama en fazla
                        var tarihAraligiOrtalamaEnFazlaIlgiliKisit = new KpTarihAraligiOrtalamaEnFazla
                        {
                            Model = model,
                            Tarihler = tarihAralik.TakvimNobetGruplar,
                            GunSayisi = tarihAralik.GunSayisi,
                            OrtalamaNobetSayisi = tarihAralik.OrtalamaNobetSayisi,
                            EczaneNobetGrup = eczaneNobetGrup,
                            EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazli,
                            NobetUstGrupKisit = herAyEnFazlaIlgiliKisit,
                            KararDegiskeni = _x
                        };
                        TarihAraligiOrtalamaEnFazla(tarihAraligiOrtalamaEnFazlaIlgiliKisit);
                        //TarihAraligiOrtalamaEnFazla(model, tarihAralik.TakvimNobetGruplar, tarihAralik.GunSayisi, tarihAralik.OrtalamaNobetSayisi, eczaneNobetGrup, eczaneNobetTarihAralikEczaneBazli, herAyEnFazlaIlgiliKisit, _x);

                        #region Kümülatif toplam en fazla - Tur takip kısıtı

                        var kumulatifOrtalamaGunKuralNobetSayisi = tarihAralik.KumulatifOrtalamaNobetSayisi;

                        int gunKuralNobetSayisi = GetToplamGunKuralNobetSayisi(eczaneNobetIstatistik, gunKural.NobetGunKuralId);

                        var haftaIciEnCokVeGunKuralNobetleriArasindakiFark = haftaIciEnCokNobetSayisi - gunKuralNobetSayisi;
                        var haftaIciEnAzVeEnCokNobetSayisiArasindakiFark = haftaIciEnCokNobetSayisi - haftaIciEnAzNobetSayisi;

                        if (gunKumulatifToplamEnFazla.SagTarafDegeri > 0 && !ozelTurTakibiYapilacakGunler.Contains(gunKural.NobetGunKuralId))
                            kumulatifOrtalamaGunKuralNobetSayisi = gunKumulatifToplamEnFazla.SagTarafDegeri;

                        if (gunKuralNobetSayisi > kumulatifOrtalamaGunKuralNobetSayisi)
                            kumulatifOrtalamaGunKuralNobetSayisi = gunKuralNobetSayisi;

                        //haftaninGunleriDagilimi.SagTarafDegeri = 3
                        if (haftaIciEnAzVeEnCokNobetSayisiArasindakiFark < haftaninGunleriDagilimi.SagTarafDegeri && !haftaninGunleriDagilimi.PasifMi)
                        {//hafta içi dağılım
                            if (data.CalismaSayisi == 1
                                //&& !nobetGrupGunKurallar.Contains(gunKural.NobetGunKuralId)
                                && !ozelTurTakibiYapilacakGunler.Contains(gunKural.NobetGunKuralId)
                                && haftaIciEnCokVeGunKuralNobetleriArasindakiFark >= 1
                                )
                                kumulatifOrtalamaGunKuralNobetSayisi++;

                            if (data.CalismaSayisi == 2
                                //&& !nobetGrupGunKurallar.Contains(gunKural.NobetGunKuralId)
                                && !ozelTurTakibiYapilacakGunler.Contains(gunKural.NobetGunKuralId)
                                && haftaIciEnCokVeGunKuralNobetleriArasindakiFark >= 0// haftaIciEnCokNobetSayisi
                                )
                                kumulatifOrtalamaGunKuralNobetSayisi++;

                            if (data.CalismaSayisi == 3
                                && !nobetGrupGunKurallar.Contains(gunKural.NobetGunKuralId)
                                //&& !ozelTurTakibiYapilacakGunler.Contains(gunKural.NobetGunKuralId)
                                //&& haftaIciEnAzVeEnCokNobetSayisiArasindakiFark <= haftaIciEnCokNobetSayisi
                                )
                                kumulatifOrtalamaGunKuralNobetSayisi++;
                        }

                        var kumulatifToplamEnFazla = new KpKumulatifToplam
                        {
                            Model = model,
                            Tarihler = tarihAralik.TakvimNobetGruplar,
                            EczaneNobetGrup = eczaneNobetGrup,
                            EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazli,
                            NobetUstGrupKisit = gunKumulatifToplamEnFazla,
                            KumulatifOrtalamaGunKuralSayisi = kumulatifOrtalamaGunKuralNobetSayisi,
                            ToplamNobetSayisi = gunKuralNobetSayisi,
                            KararDegiskeni = _x
                        };

                        KumulatifToplamEnFazla(kumulatifToplamEnFazla);
                        //KumulatifToplamEnFazla(model, tarihAralik.TakvimNobetGruplar, eczaneNobetGrup, eczaneNobetTarihAralikEczaneBazli, gunKumulatifToplamEnFazla, kumulatifOrtalamaGunKuralNobetSayisi, gunKuralNobetSayisi, _x);

                        #endregion
                    }

                    #endregion

                    #region Bayram kümülatif toplam en fazla ve farklı tür bayram

                    if (bayramSayisi > 0)
                    {
                        var bayramNobetleri = eczaneNobetSonuclar
                                .Where(w => w.NobetGunKuralId > 7
                                         && w.Tarih >= data.NobetUstGrupBaslangicTarihi)
                                .Select(s => new { s.TakvimId, s.NobetGunKuralId }).ToList();

                        var bayramNobetleriAnahtarli = eczaneNobetSonuclar
                                .Where(w => w.NobetGunKuralId > 7).ToList();
                        //.Select(s => new { s.TakvimId, s.NobetGunKuralId }).ToList();

                        var toplamBayramNobetSayisi = bayramNobetleri.Count();

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

                        if (bayramToplamEnFazla.SagTarafDegeri > 0)
                            yillikOrtalamaGunKuralSayisi = bayramToplamEnFazla.SagTarafDegeri;

                        if (data.CalismaSayisi >= 3)
                            yillikOrtalamaGunKuralSayisi++;

                        var sonBayram = bayramNobetleriAnahtarli.LastOrDefault();

                        if (sonBayram != null)
                        {
                            if (bayramlar.Select(s => s.NobetGunKuralId).Contains(sonBayram.NobetGunKuralId) && !bayramPespeseFarkliTur.PasifMi)
                            {
                                var bayramPespeseFarkliTurKisit = new KpBayramPespeseFarkliTur
                                {
                                    Model = model,
                                    NobetUstGrupKisit = bayramPespeseFarkliTur,
                                    Tarihler = bayramlar,
                                    EczaneNobetGrup = eczaneNobetGrup,
                                    EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazli,
                                    SonBayram = sonBayram,
                                    KararDegiskeni = _x
                                };
                                BayramPespeseFarkliTur(bayramPespeseFarkliTurKisit);

                                //BayramPespeseFarkliTur(model, bayramlar, eczaneNobetGrup, eczaneNobetTarihAralikEczaneBazli, sonBayram.NobetGunKuralId, _x);
                            }
                            else
                            {
                                var kumulatifToplamEnFazlaBayram = new KpKumulatifToplam
                                {
                                    Model = model,
                                    Tarihler = bayramlar,
                                    EczaneNobetGrup = eczaneNobetGrup,
                                    EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazli,
                                    NobetUstGrupKisit = bayramToplamEnFazla,
                                    KumulatifOrtalamaGunKuralSayisi = yillikOrtalamaGunKuralSayisi,
                                    ToplamNobetSayisi = toplamBayramNobetSayisi,
                                    KararDegiskeni = _x
                                };

                                KumulatifToplamEnFazla(kumulatifToplamEnFazlaBayram);
                                //KumulatifToplamEnFazla(model, bayramlar, eczaneNobetGrup, eczaneNobetTarihAralikEczaneBazli, bayramToplamEnFazla, yillikOrtalamaGunKuralSayisi, toplamBayramNobetSayisi, _x);
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
                    kpKumulatifToplamEnFazla.NobetUstGrupKisit = toplamMaxHedef;
                    kpKumulatifToplamEnFazla.ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiToplam;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnFazla);

                    #endregion 

                    #region Hafta içi toplam max hedefler

                    var kpKumulatifToplamEnFazlaHaftaIci = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnFazlaHaftaIci.Tarihler = haftaIciGunleri;
                    kpKumulatifToplamEnFazlaHaftaIci.NobetUstGrupKisit = haftaIciToplamMaxHedef;
                    kpKumulatifToplamEnFazlaHaftaIci.ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiHaftaIci;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnFazlaHaftaIci);

                    #endregion

                    #region Toplam cuma cumartesi max hedefler

                    var kpKumulatifToplamEnFazlaCumaVeCumartesi = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnFazlaCumaVeCumartesi.Tarihler = cumaVeCumartesiGunleri;
                    kpKumulatifToplamEnFazlaCumaVeCumartesi.NobetUstGrupKisit = toplamCumaCumartesiMaxHedef;
                    kpKumulatifToplamEnFazlaCumaVeCumartesi.ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiCuma + eczaneNobetIstatistik.NobetSayisiCumartesi;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnFazlaCumaVeCumartesi);

                    #endregion

                    #region Toplam Cuma Max Hedef

                    var kpKumulatifToplamEnFazlaCuma = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnFazlaCuma.Tarihler = cumaGunleri;
                    kpKumulatifToplamEnFazlaCuma.NobetUstGrupKisit = toplamCumaMaxHedef;
                    kpKumulatifToplamEnFazlaCuma.ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiCuma;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnFazlaCuma);

                    #endregion

                    #region Toplam Cumartesi Max Hedef

                    var kpKumulatifToplamEnFazlaCumartesi = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnFazlaCumartesi.Tarihler = cumartesiGunleri;
                    kpKumulatifToplamEnFazlaCumartesi.NobetUstGrupKisit = toplamCumartesiMaxHedef;
                    kpKumulatifToplamEnFazlaCumartesi.ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiCumartesi;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnFazlaCumartesi);

                    #endregion

                    #endregion

                    #region en az

                    #region tarih aralığı en az

                    #region tarih aralığı en az 1 görev

                    var tarihAraligindaEnAz1NobetYazKisitTarihAraligi = (KpTarihAraligindaEnAz1NobetYaz)KpTarihAraligindaEnAz1NobetYaz.Clone();

                    tarihAraligindaEnAz1NobetYazKisitTarihAraligi.NobetUstGrupKisit = herAyEnaz1Gorev;
                    tarihAraligindaEnAz1NobetYazKisitTarihAraligi.Tarihler = tarihler;

                    TarihAraligindaEnAz1NobetYaz(tarihAraligindaEnAz1NobetYazKisitTarihAraligi);

                    #endregion

                    #region tarih aralığı en az 1 hafta içi

                    var tarihAraligindaEnAz1NobetYazKisitHaftaIci = (KpTarihAraligindaEnAz1NobetYaz)KpTarihAraligindaEnAz1NobetYaz.Clone();

                    tarihAraligindaEnAz1NobetYazKisitHaftaIci.NobetUstGrupKisit = herAyEnaz1HaftaIciGorev;
                    tarihAraligindaEnAz1NobetYazKisitHaftaIci.Tarihler = haftaIciGunleri;

                    TarihAraligindaEnAz1NobetYaz(tarihAraligindaEnAz1NobetYazKisitHaftaIci);

                    #endregion

                    #endregion

                    #region kümülatif en az

                    kpKumulatifToplam.EnAzMi = true;

                    #region Toplam min

                    var kpKumulatifToplamEnAz = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnAz.Tarihler = tarihler;
                    kpKumulatifToplamEnAz.NobetUstGrupKisit = toplamMinHedef;
                    kpKumulatifToplamEnAz.ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiToplam;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnAz);

                    #endregion

                    #region Toplam cuma cumartesi min hedefler

                    var kpKumulatifToplamEnAzCumaVeCumartesi = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnAzCumaVeCumartesi.Tarihler = cumaVeCumartesiGunleri;
                    kpKumulatifToplamEnAzCumaVeCumartesi.NobetUstGrupKisit = toplamCumaCumartesiMinHedef;
                    kpKumulatifToplamEnAzCumaVeCumartesi.ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiCuma + eczaneNobetIstatistik.NobetSayisiCumartesi;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnAzCumaVeCumartesi);

                    #endregion

                    #region Toplam cumartesi min hedefler

                    var kpKumulatifToplamEnAzCumartesi = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnAzCumartesi.Tarihler = cumartesiGunleri;
                    kpKumulatifToplamEnAzCumartesi.NobetUstGrupKisit = toplamCumartesiMinHedef;
                    kpKumulatifToplamEnAzCumartesi.ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiCumartesi;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnAzCumartesi);

                    #endregion

                    #region Hafta içi toplam min hedefler

                    var kpKumulatifToplamEnAzHaftaIci = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnAzHaftaIci.Tarihler = haftaIciGunleri;
                    kpKumulatifToplamEnAzHaftaIci.NobetUstGrupKisit = haftaIciToplamMinHedef;
                    kpKumulatifToplamEnAzHaftaIci.ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiHaftaIci;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnAzHaftaIci);

                    #endregion

                    #region Bayram toplam en az

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

                    #endregion

                    #endregion

                    #region Farklı aylar peşpeşe görev yazılmasın (Pazar peşpeşe)

                    //var pazarPespeseGorev0 = data.NobetUstGrupKisitlar
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

                    var pazarPespeseGorevEnFazla = data.NobetUstGrupKisitlar
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

                #region Alt gruplarla eşit sayıda nöbet tutulsun - eski

                var ayniGunNobetTutmasiTakipEdilecekGruplar = new List<int>
                {
                    20,//Yenişehir-1,
                    22 //Yenişehir-3 (M.Ü. Hastanesi)
                };

                if (false
                    && !altGruplarlaAyniGunNobetTutma.PasifMi
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

                    var altGruplarlaAyniGunNobetTutmaStd = altGruplarlaAyniGunNobetTutma.SagTarafDegeri;

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
                NobetUstGrupKisit = eczaneGrup,
                EczaneGruplar = eczaneGruplar,
                Tarihler = data.TarihAraligi,
                KararDegiskeni = _x
            };
            EsGruptakiEczanelereAyniGunNobetYazma(esGrubaAyniGunNobetYazmaEczaneGruplar);

            var esGrubaAyniGunNobetYazmaIkiliEczaneler = new KpEsGrubaAyniGunNobetYazma
            {
                Model = model,
                EczaneNobetTarihAralik = data.EczaneNobetTarihAralik,
                EczaneNobetSonuclar = data.EczaneGrupNobetSonuclarTumu,
                NobetUstGrupKisit = ikiliEczaneAyniGunNobet,
                EczaneGruplar = data.ArasindaAyniGun2NobetFarkiOlanIkiliEczaneler,
                Tarihler = data.TarihAraligi,
                KararDegiskeni = _x
            };
            EsGruptakiEczanelereAyniGunNobetYazma(esGrubaAyniGunNobetYazmaIkiliEczaneler);

            var esGrubaAyniGunNobetYazmaOncekiAylar = new KpEsGrubaAyniGunNobetYazma
            {
                Model = model,
                EczaneNobetTarihAralik = data.EczaneNobetTarihAralik,
                EczaneNobetSonuclar = data.EczaneGrupNobetSonuclarTumu,
                NobetUstGrupKisit = oncekiAylarAyniGunNobet,
                EczaneGruplar = data.OncekiAylardaAyniGunNobetTutanEczaneler,
                Tarihler = data.TarihAraligi,
                KararDegiskeni = _x
            };
            EsGruptakiEczanelereAyniGunNobetYazma(esGrubaAyniGunNobetYazmaOncekiAylar);

            #endregion

            #region Alt gruplarla aynı gün nöbet dağılımı

            #region veriler

            #region yenişehir

            var altGrupluTakipEdilecekNobetGrupGorevTiplerYenisehir = new List<NobetGrupGorevTipDetay>() {
                new NobetGrupGorevTipDetay { NobetGrupId = 20 }, //Yenişehir-1
                new NobetGrupGorevTipDetay { NobetGrupId = 21 }, //Yenişehir-2
                new NobetGrupGorevTipDetay { NobetGrupId = 22 }  //Yenişehir-3
            };

            var eczaneNobetTarihAralikAtlGrupluYenisehir = data.EczaneNobetTarihAralik
                .Where(w => altGrupluTakipEdilecekNobetGrupGorevTiplerYenisehir.Select(s => s.NobetGrupId).Contains(w.NobetGrupId)).ToList();

            var eczaneNobetSonuclarAltGruplaAyniGunYenisehir = data.EczaneGrupNobetSonuclarTumu
                .Where(w => altGrupluTakipEdilecekNobetGrupGorevTiplerYenisehir.Select(s => s.NobetGrupId).Contains(w.NobetGrupId)).ToList();

            #endregion

            #region toroslar

            var altGrupluTakipEdilecekNobetGrupGorevTiplerToroslar = new List<NobetGrupGorevTipDetay>() {
                new NobetGrupGorevTipDetay { NobetGrupId = 15 }, //Toroslar-1
                new NobetGrupGorevTipDetay { NobetGrupId = 16 }, //Toroslar-2
            };

            var eczaneNobetTarihAralikAtlGrupluToroslar = data.EczaneNobetTarihAralik.Where(w => altGrupluTakipEdilecekNobetGrupGorevTiplerToroslar.Select(s => s.NobetGrupId).Contains(w.NobetGrupId)).ToList();

            var eczaneNobetSonuclarAltGruplaAyniGunToroslar = data.EczaneGrupNobetSonuclarTumu
                .Where(w => altGrupluTakipEdilecekNobetGrupGorevTiplerToroslar.Select(s => s.NobetGrupId).Contains(w.NobetGrupId)).ToList();

            #endregion

            var kpEsGrubaAyniGunNobetYazma = new KpEsGrubaAyniGunNobetYazma
            {
                Model = model,
                NobetUstGrupKisit = altGruplarlaAyniGunNobetTutma,
                KararDegiskeni = _x
            };

            var gunGruplar = data.TarihAraligi.Select(s => new { s.GunGrupId, s.GunGrupAdi }).Distinct().OrderBy(o => o.GunGrupId).ToList();

            #endregion

            foreach (var gunGrup in gunGruplar)
            {
                kpEsGrubaAyniGunNobetYazma.Tarihler = data.TarihAraligi.Where(w => w.GunGrupId == gunGrup.GunGrupId).ToList();

                #region yenişehir
                var kpEsGrubaAyniGunNobetYazmaYeniSehir = (KpEsGrubaAyniGunNobetYazma)kpEsGrubaAyniGunNobetYazma.Clone();

                kpEsGrubaAyniGunNobetYazmaYeniSehir.EczaneNobetTarihAralik = eczaneNobetTarihAralikAtlGrupluYenisehir;
                kpEsGrubaAyniGunNobetYazmaYeniSehir.EczaneNobetSonuclar = GetSonuclarByGunGrup(eczaneNobetSonuclarAltGruplaAyniGunYenisehir, gunGrup.GunGrupId);
                kpEsGrubaAyniGunNobetYazmaYeniSehir.EczaneGruplar = GetEczaneGruplarByEczaneGrupTanimTipId(data.AltGruplarlaAyniGunNobetTutmayacakEczanelerYenisehir, gunGrup.GunGrupId);

                EsGruptakiEczanelereAyniGunNobetYazma(kpEsGrubaAyniGunNobetYazmaYeniSehir);
                #endregion

                #region toroslar
                var kpEsGrubaAyniGunNobetYazmaToroslar = (KpEsGrubaAyniGunNobetYazma)kpEsGrubaAyniGunNobetYazma.Clone();

                kpEsGrubaAyniGunNobetYazmaToroslar.EczaneNobetTarihAralik = eczaneNobetTarihAralikAtlGrupluToroslar;
                kpEsGrubaAyniGunNobetYazmaToroslar.EczaneNobetSonuclar = GetSonuclarByGunGrup(eczaneNobetSonuclarAltGruplaAyniGunToroslar, gunGrup.GunGrupId);
                kpEsGrubaAyniGunNobetYazmaToroslar.EczaneGruplar = GetEczaneGruplarByEczaneGrupTanimTipId(data.AltGruplarlaAyniGunNobetTutmayacakEczanelerToroslar, gunGrup.GunGrupId);

                EsGruptakiEczanelereAyniGunNobetYazma(kpEsGrubaAyniGunNobetYazmaToroslar);
                #endregion
            }

            #endregion

            #region istek ve mazeret

            var istegiKarsilaKisit = new KpIstegiKarsila
            {
                Model = model,
                EczaneNobetTarihAralik = data.EczaneNobetTarihAralik,
                NobetUstGrupKisit = istek,
                EczaneNobetIstekler = data.EczaneNobetIstekler,
                KararDegiskeni = _x
            };
            IstegiKarsila(istegiKarsilaKisit);

            var mazereteGorevYazmaKisit = new KpMazereteGorevYazma
            {
                Model = model,
                EczaneNobetTarihAralik = data.EczaneNobetTarihAralik,
                NobetUstGrupKisit = mazeret,
                EczaneNobetMazeretler = data.EczaneNobetMazeretler,
                KararDegiskeni = _x
            };
            MazereteGorevYazma(mazereteGorevYazmaKisit);

            #endregion

            #region Tarih aralığı içinde aynı gün nöbet

            var ayIcindeSadece1KezAyniGunNobetKisit = new KpAyIcindeSadece1KezAyniGunNobet
            {
                Model = model,
                EczaneNobetTarihAralik = data.EczaneNobetTarihAralik,
                IkiliEczaneler = data.IkiliEczaneler,
                NobetUstGrupKisit = ayIcindeAyniGunNobet,
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
                    var stopwatch = new Stopwatch();
                    stopwatch.Start();

                    var model = Model(data);
                    // Get a solver instance, change your solver
                    var solver = new CplexSolver();

                    var sure_0 = stopwatch.Elapsed;
                    // solve the model
                    var solution = solver.Solve(model);
                    var sure_01 = stopwatch.Elapsed;

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
                        var sure = sure_01 - sure_0; //solution.OverallWallTime;
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

                        var sonuclar = data.EczaneNobetTarihAralik.Where(s => _x[s].Value == 1).ToList();
                        var sonuclar2 = data.EczaneNobetTarihAralik.Where(s => _x[s].Value != 1).ToList();

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
                        stopwatch.Stop();

                        var toplamSure = stopwatch.Elapsed;
                    }
                }
            }
            catch (Exception ex)
            {
                if (data.CalismaSayisi <= 3)
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

                    mesaj = $"<strong><span class='text-danger'>Uygun çözüm alanı olmadığından</span> mevcut kısıtlarla çözüm bulunamamıştır.</strong>" +
                        $"<br/><strong>Nöbet yazdırma seçenekleri:</strong>" +
                        $"<br/>Nöbet Grupları: {cozulenNobetGruplar}" +
                        $"<br/>Tarih Aralığı: <strong>{data.BaslangicTarihi.ToShortDateString()}-{data.BitisTarihi.ToShortDateString()}</strong>" +
                        $"<br/>Farklı deneme sayısı: <strong>{data.CalismaSayisi - 1}</strong>" +
                        $"{iterasyonMesaj} " +
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

    }
}

