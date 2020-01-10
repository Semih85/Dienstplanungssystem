using OPTANO.Modeling.Common;
using OPTANO.Modeling.Optimization;
using OPTANO.Modeling.Optimization.Configuration;
using OPTANO.Modeling.Optimization.Enums;
using OPTANO.Modeling.Optimization.Solver;
using OPTANO.Modeling.Optimization.Solver.Cplex128;
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
    public class CorumOptano : EczaneNobetKisit, IEczaneNobetCorumOptimization
    {
        //Karar değişkeni model çalıştıktan sonra değer aldığından burada tanımlandı
        private VariableCollection<EczaneNobetTarihAralik> _x { get; set; }
        private VariableCollection<EczaneNobetTarihAralikIkili> _y { get; set; }

        private Model Model(CorumDataModel data)
        {
            var model = new Model() { Name = "Corum Eczane Nöbet" };

            #region Veriler

            #region tur çevrim katsayıları

            //int bayramCevrimDini = 8000;
            //int bayramCevrimMilli = 8000;
            //int yilbasiCevrim = 7000;
            ////int arifeCevrim = 5000;
            //int cumartesiCevrim = 900;
            //int pazarCevrim = 1000;
            //int haftaIciCevrim = 500;
            #endregion

            #region özel tur takibi yapılacak günler

            var ozelTurTakibiYapilacakGunler =
                new List<int> {
                    1, // pazar
                    7  // cumartesi
                };

            #endregion

            var herAyEnFazlaIlgiliKisit = new NobetUstGrupKisitDetay();
            var kumulatifEnFazlaIlgiliGunkural = new NobetUstGrupKisitDetay();

            #endregion

            #region Karar Değişkenleri

            _x = new VariableCollection<EczaneNobetTarihAralik>(
                    model,
                    data.EczaneNobetTarihAralik,
                    "_x",
                    t => $"{t.EczaneNobetGrupId}_{t.TakvimId}, {t.EczaneAdi}, {t.Tarih.ToShortDateString()}",
                    h => data.LowerBound,
                    h => data.UpperBound,
                    a => VariableType.Binary);

            _y = new VariableCollection<EczaneNobetTarihAralikIkili>(
                    model,
                    data.EczaneNobetTarihAralikIkiliEczaneler,
                    "_y",
                    t => $"{t.EczaneNobetGrupId1},{t.EczaneNobetGrupId2},{t.TakvimId}, {t.EczaneAdi1}, {t.EczaneAdi2}, {t.Tarih.ToShortDateString()}",
                    h => data.LowerBound,
                    h => data.UpperBound,
                    a => VariableType.Binary);

            #endregion

            #region Amaç Fonksiyonu

            //ilk yazılan nöbet öncelikli olsun...
            var amac = new Objective(Expression
                .Sum(from i in data.EczaneNobetTarihAralik
                     select (_x[i] * i.AmacFonksiyonKatsayi)),
                     "Sum of all item-values: ",
                     ObjectiveSense.Minimize);

            model.AddObjective(amac);

            #endregion

            #region Kısıtlar

            var kisitlarAktif = data.NobetUstGrupKisitlar;

            foreach (var nobetGrupGorevTip in data.NobetGrupGorevTipler)
            {
                #region ön hazırlama

                var nobetGrupKurallar = data.NobetGrupKurallar.Where(w => w.NobetGrupGorevTipId == nobetGrupGorevTip.Id).ToList();

                var pespeseNobetSayisi = (int)GetNobetGunKural(nobetGrupKurallar, 1);
                var gunlukNobetciSayisi = (int)GetNobetGunKural(nobetGrupKurallar, 3);
                var pespeseNobetSayisiHaftaIci = (int)GetNobetGunKural(nobetGrupKurallar, 5);
                var pespeseNobetSayisiPazar = (int)GetNobetGunKural(nobetGrupKurallar, 6);
                var pespeseNobetSayisiCumartesi = (int)GetNobetGunKural(nobetGrupKurallar, 7);

                #region tarihler

                var tarihler = data.TarihAraligi
                    .Where(w => w.NobetGrupGorevTipId == nobetGrupGorevTip.Id)
                             .OrderBy(o => o.Tarih).ToList();

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
                    .OrderBy(x => r.NextDouble())
                    .ToList();

                var gruptakiEczaneSayisi = eczaneNobetGruplar.Count;

                var eczaneNobetSonuclarGorevTipBazli = data.EczaneNobetSonuclar
                    .Where(w => w.NobetGorevTipId == nobetGrupGorevTip.NobetGorevTipId).ToList();

                var eczaneNobetGrupGunKuralIstatistikler = data.EczaneNobetGrupGunKuralIstatistikYatay
                    .Where(w => w.NobetGrupGorevTipId == nobetGrupGorevTip.Id).ToList();

                var eczaneNobetGrupGunKuralIstatistiklerSon3Ay = data.EczaneNobetGrupGunKuralIstatistikYataySon3Ay
                    .Where(w => w.NobetGrupGorevTipId == nobetGrupGorevTip.Id).ToList();

                var nobetGrupGunKurallarAktifGunler = data.NobetGrupGorevTipGunKurallar
                                                        .Where(s => s.NobetGrupGorevTipId == nobetGrupGorevTip.Id
                                                                 && nobetGunKurallar.Contains(s.NobetGunKuralId))
                                                        .Select(s => s.NobetGunKuralId).ToList();

                //var nobetGrupGorevTipler = data.NobetGrupGorevTipler.Where(w => w.NobetGrupId == nobetGrupGorevTip.NobetGrupId).ToList();

                // karar değişkeni - nöbet grup bazlı filtrelenmiş
                var eczaneNobetTarihAralikGrupBazli = data.EczaneNobetTarihAralik
                           .Where(e => e.NobetGrupGorevTipId == nobetGrupGorevTip.Id).ToList();

                #region haftaIciPespeseGorevEnAz

                var farkliAyPespeseGorevAraligi = (gruptakiEczaneSayisi / gunlukNobetciSayisi * 1.2);

                var altLimit = (gruptakiEczaneSayisi / gunlukNobetciSayisi) * 0.6; //0.95

                //hafta içi                
                altLimit = GetArdisikBosGunSayisi(pespeseNobetSayisiHaftaIci, altLimit);

                var ustLimit = farkliAyPespeseGorevAraligi + farkliAyPespeseGorevAraligi * 0.6667; //77;
                var ustLimitKontrol = ustLimit * 0.95; //0.81 

                //var pazarNobetiYazilabilecekIlkAy = (int)Math.Ceiling((double)gruptakiEczaneSayisi / (5 * gunlukNobetciSayisi)) - 1;
                var pazarNobetiYazilabilecekIlkAy = (Math.Ceiling((double)gruptakiEczaneSayisi / (5 * gunlukNobetciSayisi)) - 3) * 30;

                pazarNobetiYazilabilecekIlkAy = GetArdisikBosGunSayisi(pespeseNobetSayisiPazar, pazarNobetiYazilabilecekIlkAy);

                var cumartersiNobetiYazilabilecekIlkAy = (int)Math.Ceiling((double)gruptakiEczaneSayisi / (5 * gunlukNobetciSayisi)) - 1;
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
                var OrtalamaNobetSayisiBayram = OrtalamaNobetSayisi(bayramlar.Sum(s => s.TalepEdilenNobetciSayisi), gruptakiEczaneSayisi);

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

                var ortalamaNobetSayisiKumulatifHaftaIciKalibrasyonlu = OrtalamaNobetSayisi(talepEdilenNobetciSayisiHaftaIci
                    + kumulatifToplamNobetSayisiHaftaIci
                    + (int)data.Kalibrasyonlar
                    .Where(w => w.KalibrasyonTipId == 12
                                && eczaneNobetGruplar.Select(s => s.Id).Contains(w.EczaneNobetGrupId))
                    .Sum(s => s.KalibrasyonToplamHaftaIci), gruptakiEczaneSayisi);

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

                var ortalamaNobetSayisiKumulatifPazarKalibrasyonlu = OrtalamaNobetSayisi(talepEdilenNobetciSayisiPazar
                    + kumulatifToplamNobetSayisiPazar
                    + (int)data.Kalibrasyonlar
                    .Where(w => w.KalibrasyonTipId == 12
                             && eczaneNobetGruplar.Select(s => s.Id).Contains(w.EczaneNobetGrupId))
                    .Sum(s => s.KalibrasyonToplamPazar)
                    , gruptakiEczaneSayisi);

                #endregion

                #endregion

                var nobetGrupBayramNobetleri = data.EczaneNobetSonuclar
                    .Where(w => w.NobetGrupGorevTipId == nobetGrupGorevTip.Id
                             && w.GunGrupId == 2
                             && w.Tarih >= data.NobetUstGrupBaslangicTarihi)
                    .Select(s => new { s.TakvimId, s.NobetGunKuralId }).ToList();

                var nobetGunKuralIstatistikler = data.TakvimNobetGrupGunDegerIstatistikler
                                      .Where(w => w.NobetGrupGorevTipId == nobetGrupGorevTip.Id
                                               //&& tarihler.Select(s => s.NobetGunKuralId).Distinct().Contains(w.NobetGunKuralId)
                                               )
                                      .OrderBy(o => o.NobetGunKuralId).ToList();

                var nobetGunKuralTarihler = OrtalamaNobetSayilariniHesapla(tarihler, gruptakiEczaneSayisi, eczaneNobetGrupGunKuralIstatistikler, nobetGunKuralIstatistikler);

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

                foreach (var eczaneNobetGrup in eczaneNobetGruplar)
                {//eczane bazlı kısıtlar

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

                    var eczaneNobetTarihAralikEczaneBazliTumGrorevTipler = data.EczaneNobetTarihAralik
                        .Where(e => e.EczaneId == eczaneNobetGrup.EczaneId
                                //&& e.CumartesiGunuMu == true
                                ).ToList();

                    var eczaneNobetSonuclar = eczaneNobetSonuclarGorevTipBazli
                        .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id).ToList();

                    var eczaneNobetIstatistik = eczaneNobetGrupGunKuralIstatistikler
                        .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id).SingleOrDefault() ?? new EczaneNobetGrupGunKuralIstatistikYatay();

                    var eczaneNobetIstatistikSon3Ay = eczaneNobetGrupGunKuralIstatistiklerSon3Ay
                        .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id).SingleOrDefault() ?? new EczaneNobetGrupGunKuralIstatistikYatay();

                    var eczaneNobetIstatistikTumGorevTipleri = data.EczaneNobetGrupGunKuralIstatistikYatay
                        .GroupBy(g => new
                        {
                            g.EczaneNobetGrupId,
                            g.NobetGrupId,
                            g.NobetGrupAdi,
                            g.EczaneAdi,
                            g.EczaneId
                        })
                        .Select(s => new EczaneNobetGrupGunKuralIstatistikYatay
                        {
                            NobetGrupId = s.Key.NobetGrupId,
                            EczaneId = s.Key.EczaneId,
                            EczaneAdi = s.Key.EczaneAdi,
                            NobetGrupAdi = s.Key.NobetGrupAdi,
                            EczaneNobetGrupId = s.Key.EczaneNobetGrupId,
                            SonNobetTarihi = s.Max(m => m.SonNobetTarihi),
                            NobetSayisiToplam = s.Sum(c => c.NobetSayisiToplam)
                        })
                        .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id).SingleOrDefault() ?? new EczaneNobetGrupGunKuralIstatistikYatay();

                    //var yazilabilecekIlkPazarTarihi = eczaneNobetIstatistik.SonNobetTarihiPazar.AddMonths(pazarNobetiYazilabilecekIlkAy - 1);
                    var yazilabilecekIlkPazarTarihi = eczaneNobetIstatistik.SonNobetTarihiPazar.AddDays(pazarNobetiYazilabilecekIlkAy - 1);
                    var yazilabilecekIlkCumartesiTarihi = eczaneNobetIstatistik.SonNobetTarihiCumartesi.AddMonths(cumartersiNobetiYazilabilecekIlkAy - 1);

                    var yazilabilecekIlkHaftaIciTarihi = eczaneNobetIstatistik.SonNobetTarihiHaftaIci.AddDays(altLimit);
                    var nobetYazilabilecekIlkTarih = eczaneNobetIstatistik.SonNobetTarihi.AddDays(pespeseNobetSayisi);
                    var nobetYazilabilecekIlkTarihTumGorevTipleri = eczaneNobetIstatistikTumGorevTipleri.SonNobetTarihi.AddDays(pespeseNobetSayisi);

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
                        GruptakiNobetciSayisi = gruptakiEczaneSayisi
                    };
                    //var nobetTutamazGunler = eczaneNobetTutamazGunler.Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id).ToList();

                    //var nobetYazilabilirGunler = nobetGrupGunKurallar
                    //    .Where(w => !nobetTutamazGunler.Select(s => s.NobetGunKuralId).Contains(w)
                    //             && !ozelTurTakibiYapilacakGunler.Contains(w)).ToList();
                    #endregion

                    #region aktif kısıtlar

                    #region Peş peşe nöbet

                    #region ay içinde

                    var kpHerAyPespeseGorev = new KpHerAyPespeseGorev
                    {
                        Model = model,
                        EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazliTumGrorevTipler,
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
                        PespeseNobetSayisiAltLimit = gruptakiEczaneSayisi * 0.3,//0,4
                        NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k27"),
                        GunKuralAdi = "h.içi",
                        KararDegiskeni = _x
                    };
                    HerAyHaftaIciPespeseGorev(kpHerAyPespeseGorevHaftaIci);

                    var kpHerAyPespeseGorevPazar = new KpHerAyHaftaIciPespeseGorev
                    {
                        Model = model,
                        Tarihler = pazarGunleri,
                        OrtamalaNobetSayisi = ortamalaNobetSayisiPazar,
                        EczaneNobetGrup = eczaneNobetGrup,
                        EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazli,
                        PespeseNobetSayisiAltLimit = gruptakiEczaneSayisi * 0.8,
                        NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k27"),
                        GunKuralAdi = "pazar",
                        KararDegiskeni = _x
                    };
                    HerAyHaftaIciPespeseGorev(kpHerAyPespeseGorevPazar);

                    var kpHerAyPespeseGorevCumartesi = new KpHerAyHaftaIciPespeseGorev
                    {
                        Model = model,
                        Tarihler = cumartesiGunleri,
                        OrtamalaNobetSayisi = ortamalaNobetSayisiCumartesi,
                        EczaneNobetGrup = eczaneNobetGrup,
                        EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazli,
                        PespeseNobetSayisiAltLimit = gruptakiEczaneSayisi * 0.5, //0,5
                        NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k27"),
                        GunKuralAdi = "cumartesi",
                        KararDegiskeni = _x
                    };
                    HerAyHaftaIciPespeseGorev(kpHerAyPespeseGorevCumartesi);

                    #endregion

                    #region farklı aylar peş peşe görev

                    var kpPesPeseGorevEnAz = new KpPesPeseGorevEnAz
                    {
                        Model = model,
                        KararDegiskeni = _x,
                        EczaneNobetGrup = eczaneNobetGrup,
                        EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazli
                    };

                    #region peşpeşe herhangi 2 nöbet

                    //if (//eczaneNobetGrup.EczaneAdi != "SERKAN"
                    //    //|| 
                    //    eczaneNobetGrup.EczaneAdi != "DAMLA GÜMÜŞAĞA")
                    //{
                    var pesPeseGorevEnAzFarkliAylar = (KpPesPeseGorevEnAz)kpPesPeseGorevEnAz.Clone();

                    pesPeseGorevEnAzFarkliAylar.NobetSayisi = eczaneNobetIstatistik.NobetSayisiToplam;
                    pesPeseGorevEnAzFarkliAylar.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k18");
                    pesPeseGorevEnAzFarkliAylar.Tarihler = tarihler;
                    pesPeseGorevEnAzFarkliAylar.NobetYazilabilecekIlkTarih = nobetYazilabilecekIlkTarih;
                    pesPeseGorevEnAzFarkliAylar.SonNobetTarihi = eczaneNobetIstatistik.SonNobetTarihi;

                    PesPeseGorevEnAz(pesPeseGorevEnAzFarkliAylar);
                    //}

                    #endregion

                    #region peşpeşe pazar

                    var pesPeseGorevEnAzPazar = (KpPesPeseGorevEnAz)kpPesPeseGorevEnAz.Clone();

                    pesPeseGorevEnAzPazar.NobetSayisi = eczaneNobetIstatistik.NobetSayisiPazar;
                    pesPeseGorevEnAzPazar.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k28");
                    pesPeseGorevEnAzPazar.Tarihler = pazarGunleri;
                    pesPeseGorevEnAzPazar.NobetYazilabilecekIlkTarih = yazilabilecekIlkPazarTarihi;
                    pesPeseGorevEnAzPazar.SonNobetTarihi = eczaneNobetIstatistik.SonNobetTarihiPazar;

                    //istisna -- Ekim 2019 sonuna kadar.
                    PesPeseGorevEnAz(pesPeseGorevEnAzPazar);

                    #endregion

                    #region Peşpeşe cumartesi

                    var pesPeseGorevEnAzCumartesi = (KpPesPeseGorevEnAz)kpPesPeseGorevEnAz.Clone();

                    pesPeseGorevEnAzCumartesi.NobetSayisi = eczaneNobetIstatistik.NobetSayisiCumartesi;
                    pesPeseGorevEnAzCumartesi.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k37");
                    pesPeseGorevEnAzCumartesi.Tarihler = cumartesiGunleri;
                    pesPeseGorevEnAzCumartesi.NobetYazilabilecekIlkTarih = yazilabilecekIlkCumartesiTarihi;
                    pesPeseGorevEnAzCumartesi.SonNobetTarihi = eczaneNobetIstatistik.SonNobetTarihiCumartesi;

                    PesPeseGorevEnAz(pesPeseGorevEnAzCumartesi);

                    #endregion

                    #region peşpeşe hafta içi

                    var pesPeseGorevEnAzHaftaIci = (KpPesPeseGorevEnAz)kpPesPeseGorevEnAz.Clone();

                    pesPeseGorevEnAzHaftaIci.NobetSayisi = eczaneNobetIstatistik.NobetSayisiHaftaIci;
                    pesPeseGorevEnAzHaftaIci.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k27");
                    pesPeseGorevEnAzHaftaIci.Tarihler = haftaIciGunleri;
                    pesPeseGorevEnAzHaftaIci.NobetYazilabilecekIlkTarih = yazilabilecekIlkHaftaIciTarihi;
                    pesPeseGorevEnAzHaftaIci.SonNobetTarihi = eczaneNobetIstatistik.SonNobetTarihiHaftaIci;

                    PesPeseGorevEnAz(pesPeseGorevEnAzHaftaIci);

                    #endregion

                    #endregion

                    #endregion

                    #region Tarih aralığı ortalama en fazla

                    var kpTarihAraligiOrtalamaEnFazla = new KpTarihAraligiOrtalamaEnFazla
                    {
                        Model = model,
                        KararDegiskeni = _x,
                        EczaneNobetGrup = eczaneNobetGrup,
                        EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazli
                    };

                    #region seçilen tarih aralık

                    var ortalamaEnFazlaTumTarihAraligi = (KpTarihAraligiOrtalamaEnFazla)kpTarihAraligiOrtalamaEnFazla.Clone();
                    ortalamaEnFazlaTumTarihAraligi.Tarihler = tarihler;
                    ortalamaEnFazlaTumTarihAraligi.GunSayisi = gunSayisi;
                    ortalamaEnFazlaTumTarihAraligi.OrtalamaNobetSayisi = ortalamaNobetSayisi;
                    ortalamaEnFazlaTumTarihAraligi.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k19");

                    TarihAraligiOrtalamaEnFazla(ortalamaEnFazlaTumTarihAraligi);

                    var ortalamaEnFazlaHaftaIici = (KpTarihAraligiOrtalamaEnFazla)kpTarihAraligiOrtalamaEnFazla.Clone();
                    ortalamaEnFazlaHaftaIici.Tarihler = haftaIciGunleri;
                    ortalamaEnFazlaHaftaIici.GunSayisi = haftaIciSayisi;
                    ortalamaEnFazlaHaftaIici.OrtalamaNobetSayisi = ortamalaNobetSayisiHaftaIci;
                    ortalamaEnFazlaHaftaIici.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k32");

                    TarihAraligiOrtalamaEnFazla(ortalamaEnFazlaHaftaIici);

                    var ortalamaEnFazlaHaftaSonu = (KpTarihAraligiOrtalamaEnFazla)kpTarihAraligiOrtalamaEnFazla.Clone();
                    ortalamaEnFazlaHaftaSonu.Tarihler = cumartesiVePazarGunleri;
                    ortalamaEnFazlaHaftaSonu.GunSayisi = cumartesiSayisi + pazarSayisi;
                    ortalamaEnFazlaHaftaSonu.OrtalamaNobetSayisi = ortamalaNobetSayisiHaftaSonu;
                    ortalamaEnFazlaHaftaSonu.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k73");

                    TarihAraligiOrtalamaEnFazla(ortalamaEnFazlaHaftaSonu);

                    var ortalamaEnFazlaBayram = (KpTarihAraligiOrtalamaEnFazla)kpTarihAraligiOrtalamaEnFazla.Clone();
                    ortalamaEnFazlaBayram.Tarihler = bayramlar;
                    ortalamaEnFazlaBayram.GunSayisi = bayramSayisi;
                    ortalamaEnFazlaBayram.OrtalamaNobetSayisi = OrtalamaNobetSayisiBayram;
                    ortalamaEnFazlaBayram.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k14");

                    TarihAraligiOrtalamaEnFazla(ortalamaEnFazlaBayram);

                    #endregion

                    #endregion

                    #region Tur takip kısıtı - nöbet ortalamaları

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

                        //if (nobetGunKural.GunGrupId == 1)
                        //{//istisna
                        //    continue;
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

                        KumulatifToplamEnFazla(kumulatifToplamEnFazla);

                        #endregion
                    }

                    #endregion

                    #endregion

                    #region Bayram kümülatif toplam en fazla ve farklı tür bayram

                    if (bayramlar.Count() > 0)
                    {
                        var bayramNobetleriAnahtarli = eczaneNobetSonuclar
                                .Where(w => w.GunGrupId == 2).ToList();

                        var toplamBayramNobetSayisi = eczaneNobetIstatistik.NobetSayisiBayram;

                        if (!NobetUstGrupKisit(kisitlarAktif, "k5").PasifMi)
                        {
                            var bayramGunKuralIstatistikler = nobetGunKuralIstatistikler
                                                            .Where(w => w.GunGrupId == 2).ToList();

                            //var toplamBayramGrupToplamNobetSayisi = nobetGrupBayramNobetleri.Count();

                            var kumulatifBayramSayisi = bayramGunKuralIstatistikler.Sum(x => x.GunSayisi);

                            var yillikOrtalamaGunKuralSayisi = OrtalamaNobetSayisi(gunlukNobetciSayisi, gruptakiEczaneSayisi, kumulatifBayramSayisi);
                            //Math.Ceiling((double)kumulatifBayramSayisi / gruptakiEczaneSayisi);

                            if (kumulatifBayramSayisi == gruptakiEczaneSayisi)
                            {
                                //yillikOrtalamaGunKuralSayisi++;
                            }

                            //if (toplamBayramGrupToplamNobetSayisi == gruptakiEczaneSayisi)
                            //{
                            //    //yillikOrtalamaGunKuralSayisi++;
                            //}

                            if (yillikOrtalamaGunKuralSayisi < 1) yillikOrtalamaGunKuralSayisi = 0;

                            if (toplamBayramNobetSayisi > yillikOrtalamaGunKuralSayisi)
                                yillikOrtalamaGunKuralSayisi = toplamBayramNobetSayisi;

                            if (NobetUstGrupKisit(kisitlarAktif, "k5").SagTarafDegeri > 0)
                                yillikOrtalamaGunKuralSayisi = NobetUstGrupKisit(kisitlarAktif, "k5").SagTarafDegeri;

                            var sonBayram = bayramNobetleriAnahtarli.LastOrDefault();

                            if (sonBayram != null)
                            {
                                var sonBayramTuru = bayramNobetleriAnahtarli.LastOrDefault();

                                if (bayramlar.Select(s => s.NobetGunKuralId).Contains(sonBayramTuru.NobetGunKuralId) && !NobetUstGrupKisit(kisitlarAktif, "k36").PasifMi)
                                {
                                    var bayramPespeseFarkliTurKisit = new KpPespeseFarkliTurNobet
                                    {
                                        Model = model,
                                        NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k36"),
                                        Tarihler = bayramlar,
                                        EczaneNobetGrup = eczaneNobetGrup,
                                        EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazli,
                                        SonNobet = sonBayramTuru,
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
                    }
                    #endregion

                    #endregion

                    #region pasif kısıtlar

                    var eczaneToplamKalibrasyon = GetKalibrasyonDegeri(eczaneKalibrasyon);

                    var eczaneKalibrasyonToplami = eczaneToplamKalibrasyon.KalibrasyonToplamHaftaIci
                         + eczaneToplamKalibrasyon.KalibrasyonToplamCumartesi
                         + eczaneToplamKalibrasyon.KalibrasyonToplamPazar;

                    #region kümülatif en fazla

                    #region Toplam max

                    var kpKumulatifToplamEnFazla = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnFazla.Tarihler = tarihler;
                    kpKumulatifToplamEnFazla.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k2");
                    kpKumulatifToplamEnFazla.ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiToplam;
                    //+ (int)eczaneKalibrasyonToplami;

                    kpKumulatifToplamEnFazla.KumulatifOrtalamaNobetSayisi = ortalamaNobetSayisiKumulatif;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnFazla);

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

                    #region Hafta içi toplam max hedefler

                    var kpKumulatifToplamEnFazlaHaftaIci = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnFazlaHaftaIci.Tarihler = haftaIciGunleri;
                    kpKumulatifToplamEnFazlaHaftaIci.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k16");
                    kpKumulatifToplamEnFazlaHaftaIci.ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiHaftaIci;
                    kpKumulatifToplamEnFazlaHaftaIci.KumulatifOrtalamaNobetSayisi = ortalamaNobetSayisiKumulatifHaftaIci;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnFazlaHaftaIci);

                    #endregion

                    #region Toplam hafta içi toplam kalibrasyonlu 

                    var kpKumulatifToplamEnFazlaHaftaIciKalibrasyonlu = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnFazlaHaftaIciKalibrasyonlu.Tarihler = haftaIciGunleri;
                    kpKumulatifToplamEnFazlaHaftaIciKalibrasyonlu.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k54");//k16
                    kpKumulatifToplamEnFazlaHaftaIciKalibrasyonlu.ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiHaftaIci
                        + (int)eczaneToplamKalibrasyon.KalibrasyonToplamHaftaIci;
                    kpKumulatifToplamEnFazlaHaftaIciKalibrasyonlu.KumulatifOrtalamaNobetSayisi = ortalamaNobetSayisiKumulatifHaftaIciKalibrasyonlu;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnFazlaHaftaIciKalibrasyonlu);

                    #endregion

                    #region Toplam pazar toplam kalibrasyonlu 

                    var kpKumulatifToplamEnFazlaPazarToplamKalibrasyonlu = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnFazlaPazarToplamKalibrasyonlu.Tarihler = pazarGunleri;
                    kpKumulatifToplamEnFazlaPazarToplamKalibrasyonlu.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k55");//k57
                    kpKumulatifToplamEnFazlaPazarToplamKalibrasyonlu.ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiPazar
                        + (int)eczaneToplamKalibrasyon.KalibrasyonToplamPazar;
                    kpKumulatifToplamEnFazlaPazarToplamKalibrasyonlu.KumulatifOrtalamaNobetSayisi = ortalamaNobetSayisiKumulatifPazarKalibrasyonlu;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnFazlaPazarToplamKalibrasyonlu);

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

                    var istisnalarKumulatifToplamEnAz = new string[] {
                        //"GAZİ",
                        //"OYA",
                        //"PAPATYA",
                        //"GÖKMEN",
                    };

                    if (!istisnalarKumulatifToplamEnAz.Contains(eczaneNobetGrup.EczaneAdi))
                    {
                        kpKumulatifToplamEnAz.Tarihler = tarihler;
                        kpKumulatifToplamEnAz.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k3");
                        kpKumulatifToplamEnAz.ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiToplam
                               + (int)eczaneKalibrasyonToplami;

                        KumulatifToplamEnFazla(kpKumulatifToplamEnAz);
                    }

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

                    var istisnalarKumulatifToplamEnAzCumartesi = new string[] {
                        //"SÖNMEZ",
                        //"SEVDA"
                    };

                    if (!istisnalarKumulatifToplamEnAzCumartesi.Contains(eczaneNobetGrup.EczaneAdi))
                    {
                        kpKumulatifToplamEnAzCumartesi.Tarihler = cumartesiGunleri;
                        kpKumulatifToplamEnAzCumartesi.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k30");
                        kpKumulatifToplamEnAzCumartesi.ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiCumartesi;

                        KumulatifToplamEnFazla(kpKumulatifToplamEnAzCumartesi);
                    }

                    #endregion

                    #region Kümülatif pazar en az

                    var kpKumulatifToplamEnAzPazar = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnAzPazar.Tarihler = pazarGunleri;
                    kpKumulatifToplamEnAzPazar.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k56");
                    kpKumulatifToplamEnAzPazar.ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiPazar;

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

                    #region son 3 ay

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

                    #endregion
                }

                #region aynı gün nöbet

                var tarihAraligi = data.TarihAraligi
                    .Where(w => w.NobetGorevTipId == nobetGrupGorevTip.NobetGorevTipId).ToList();

                var tarihAraligiIstisna = tarihAraligi
                    .Where(w => w.GunGrupId != 2
                    ).ToList();//istisna -- ekim 2019 sonrası "w.GunGrupId != 2" olarak düeltildi. Öncesinde "w.GunGrupId > 2" idi. bu istisna da mayıs 2020'de son bulacak.

                #region eczane gruplar

                var esGrubaAyniGunNobetYazma = new KpEsGrubaAyniGunNobetYazma
                {
                    Model = model,
                    EczaneNobetTarihAralik = eczaneNobetTarihAralikGrupBazli,
                    EczaneNobetSonuclar = eczaneNobetSonuclarGorevTipBazli,
                    Tarihler = tarihAraligi,
                    KararDegiskeni = _x
                };

                var esGrubaAyniGunNobetYazmaEczaneGruplar = (KpEsGrubaAyniGunNobetYazma)esGrubaAyniGunNobetYazma.Clone();
                esGrubaAyniGunNobetYazmaEczaneGruplar.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k11");
                esGrubaAyniGunNobetYazmaEczaneGruplar.EczaneGruplar = data.EczaneGruplar;

                EsGruptakiEczanelereAyniGunNobetYazma(esGrubaAyniGunNobetYazmaEczaneGruplar);

                #endregion

                #region önceki aylar

                var esGrubaAyniGunNobetYazmaOncekiAylar = (KpEsGrubaAyniGunNobetYazma)esGrubaAyniGunNobetYazma.Clone();
                esGrubaAyniGunNobetYazmaOncekiAylar.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k41");
                esGrubaAyniGunNobetYazmaOncekiAylar.EczaneGruplar = data.OncekiAylardaAyniGunNobetTutanEczaneler;
                esGrubaAyniGunNobetYazmaOncekiAylar.Tarihler = tarihAraligiIstisna;//.Where(w => w.GunGrupId != 3).ToList();

                EsGruptakiEczanelereAyniGunNobetYazma(esGrubaAyniGunNobetYazmaOncekiAylar);

                #endregion

                #region ikili eczaneler

                var esGrubaAyniGunNobetYazmaIkiliEczaneler = (KpEsGrubaAyniGunNobetYazma)esGrubaAyniGunNobetYazma.Clone();

                esGrubaAyniGunNobetYazmaIkiliEczaneler.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k45");
                esGrubaAyniGunNobetYazmaIkiliEczaneler.EczaneGruplar = data.ArasindaAyniGun2NobetFarkiOlanIkiliEczaneler;
                esGrubaAyniGunNobetYazmaIkiliEczaneler.Tarihler = tarihAraligiIstisna;

                EsGruptakiEczanelereAyniGunNobetYazma(esGrubaAyniGunNobetYazmaIkiliEczaneler);

                #endregion

                #region mesafe

                var esGrubaAyniGunNobetYazmaMesafeler = (KpEsGrubaAyniGunNobetYazma)esGrubaAyniGunNobetYazma.Clone();
                esGrubaAyniGunNobetYazmaMesafeler.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k59");
                esGrubaAyniGunNobetYazmaMesafeler.EczaneGruplar = data.MesafeKontrolEczaneler;
                esGrubaAyniGunNobetYazmaMesafeler.Tarihler = tarihAraligiIstisna;

                EsGruptakiEczanelereAyniGunNobetYazma(esGrubaAyniGunNobetYazmaMesafeler);

                #endregion

                #region sonraki aylarda istek girilenler

                var sonrakiAylarAyniGunIstekGirilenler = (KpEsGrubaAyniGunNobetYazma)esGrubaAyniGunNobetYazma.Clone();
                sonrakiAylarAyniGunIstekGirilenler.EczaneNobetSonuclar = data.EczaneGrupNobetSonuclarTumu;
                sonrakiAylarAyniGunIstekGirilenler.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k41");
                sonrakiAylarAyniGunIstekGirilenler.EczaneGruplar = data.SonrakiDonemAyniGunNobetIstekGirilenler;
                sonrakiAylarAyniGunIstekGirilenler.Tarihler = data.TarihAraligi;

                EsGruptakiEczanelereAyniGunNobetYazma(sonrakiAylarAyniGunIstekGirilenler);

                #endregion

                #endregion

                #region istek ve mazeret

                var istegiKarsilaKisit = new KpIstegiKarsila
                {
                    Model = model,
                    EczaneNobetTarihAralik = eczaneNobetTarihAralikGrupBazli,
                    NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k12"),
                    EczaneNobetIstekler = data.EczaneNobetIstekler
                    .Where(w => w.NobetGrupGorevTipId == nobetGrupGorevTip.Id
                    //&& !(w.EczaneAdi == "VOLKAN" && w.Tarih.DayOfWeek == DayOfWeek.Saturday)
                    ).ToList(),
                    KararDegiskeni = _x
                };

                #region manuel istek

                //var manuelIstekEczane = data.EczaneNobetGruplar
                //    .SingleOrDefault(w => w.EczaneAdi == "YAŞAM");

                //var manuelIstekTarih = data.TarihAraligi.SingleOrDefault(w => w.Tarih == new DateTime(2019, 5, 4));

                //istegiKarsilaKisit.EczaneNobetIstekler.Add(new EczaneNobetIstekDetay
                //{
                //    EczaneNobetGrupId = manuelIstekEczane.Id,
                //    EczaneAdi = manuelIstekEczane.EczaneAdi,
                //    TakvimId = manuelIstekTarih.TakvimId,
                //    Tarih = manuelIstekTarih.Tarih
                //}); 

                #endregion

                IstegiKarsila(istegiKarsilaKisit);

                var pazarNobetIstegiOlanlar = data.EczaneNobetIsteklerSonrakiDonem
                    .Where(w => w.Tarih < new DateTime(2019, 9, 1))
                    .Union(data.EczaneNobetIstekler).ToList();
                //.Where(w => w.Tarih.DayOfWeek == DayOfWeek.Sunday).Distinct().ToList();

                var eczaneNobetMazeretlerPazar = new List<EczaneNobetMazeretDetay>();

                foreach (var cumartesi in cumartesiGunleri)
                {
                    foreach (var item in pazarNobetIstegiOlanlar)
                    {
                        eczaneNobetMazeretlerPazar.Add(new EczaneNobetMazeretDetay
                        {
                            TakvimId = cumartesi.TakvimId,
                            Tarih = cumartesi.Tarih,
                            EczaneAdi = item.EczaneAdi,
                            EczaneNobetGrupId = item.EczaneNobetGrupId,
                            Aciklama = item.Aciklama,
                            NobetGrupAdi = item.NobetGrupAdi,
                            NobetGrupId = item.NobetGrupId
                        });
                    }
                }

                var mazereteGorevYazmaKisitPazar = new KpMazereteGorevYazma
                {
                    Model = model,
                    EczaneNobetTarihAralik = eczaneNobetTarihAralikGrupBazli,
                    NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k13"),
                    EczaneNobetMazeretler = eczaneNobetMazeretlerPazar,
                    KararDegiskeni = _x
                };
                //MazereteGorevYazma(mazereteGorevYazmaKisitPazar);

                var mazereteGorevYazmaKisit = new KpMazereteGorevYazma
                {
                    Model = model,
                    EczaneNobetTarihAralik = eczaneNobetTarihAralikGrupBazli,
                    NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k13"),
                    EczaneNobetMazeretler = data.EczaneNobetMazeretler.Where(w => w.NobetGrupGorevTipId == nobetGrupGorevTip.Id).ToList(),
                    KararDegiskeni = _x
                };
                MazereteGorevYazma(mazereteGorevYazmaKisit);

                #endregion

                #region Tarih aralığı içinde aynı gün nöbet

                //var ayIcindeSadece1KezAyniGunNobetKisit = new KpAyIcindeSadece1KezAyniGunNobet
                //{
                //    Model = model,
                //    EczaneNobetTarihAralik = data.EczaneNobetTarihAralik,
                //    IkiliEczaneler = data.IkiliEczaneler,
                //    NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k10"),
                //    Tarihler = data.TarihAraligi.Where(w => w.TalepEdilenNobetciSayisi > 1 && w.GunGrupId != 1).ToList(),
                //    KararDegiskeni = _x
                //};
                //AyIcindeSadece1KezAyniGunNobetTutulsun(ayIcindeSadece1KezAyniGunNobetKisit);

                #endregion
            }

            #endregion

            return model;
        }

        public EczaneNobetSonucModel Solve(CorumDataModel data)
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
                    var modelTime = new Stopwatch();
                    modelTime.Start();

                    // Get a solver instance, change your solver
                    var solverConfig = new CplexSolverConfiguration()
                    {
                        ComputeIIS = true,
                        TimeLimit = data.TimeLimit
                    };

                    var solver = new CplexSolver(solverConfig);

                    //solver.Abort();
                    //solver.Configuration.TimeLimit = 1;

                    // solve the model
                    var solution = solver.Solve(model);

                    modelTime.Stop();
                    //Assert.IsTrue(solution.ConflictingSet.ConstraintsLB.Contains(brokenConstraint1));
                    //Assert.IsTrue(solution.ConflictingSet.ConstraintsLB.Contains(brokenConstraint2));

                    var modelStatus = solution.ModelStatus;
                    var solutionStatus = solution.Status;
                    var modelName = solution.ModelName;

                    if (modelTime.Elapsed.TotalSeconds > data.TimeLimit)
                    {
                        results.Celiskiler = $"<h6 class= 'text-danger'> Belirlenen süre ({data.TimeLimit} saniye) içinde çözüm bulunamadı. </h6>";
                    }

                    if (modelStatus != ModelStatus.Feasible)
                    {
                        //if (double.IsNaN(solution.Gap) || solution.Gap > solverConfig.MIPGap)
                        //{
                        //    throw new Exception($"<h6 class= 'text-danger'> Belirlenen süre ({data.TimeLimit} saniye) içinde çözüm bulunamadı. </h6>");
                        //    //TimeLimitReachedException();
                        //}

                        if (data.CalismaSayisi == calismaSayisiEnFazla)
                        {
                            results.Celiskiler += CeliskileriEkle(solution);
                        }

                        throw new Exception($"Uygun çözüm bulunamadı!.");
                    }
                    else
                    {
                        var sonuclar = data.EczaneNobetTarihAralik.Where(s => _x[s].Value.IsAlmost(1) == true).ToList();

                        var ayniGunNobetTutanEczaneler = GetAyniGunNobetTutanEczaneler(sonuclar);

                        var ayniGunNobetSayisi1denFazlaGrouped = (from s in ayniGunNobetTutanEczaneler
                                                                  group s by new
                                                                  {
                                                                      //s.EczaneBirlesim,
                                                                      s.EczaneAdi1,
                                                                      s.EczaneAdi2,
                                                                      s.EczaneId1,
                                                                      s.EczaneId2
                                                                      //s.EczaneNobetGrupId1,
                                                                      //s.EczaneNobetGrupId2,
                                                                      //s.Grup,
                                                                      //s.AltGrupAdi
                                                                  } into grouped
                                                                  //where grouped.Count() > 1
                                                                  select new AyniGunTutulanNobetDetay
                                                                  {
                                                                      //Grup = grouped.Key.Grup,
                                                                      EczaneAdi1 = grouped.Key.EczaneAdi1,
                                                                      EczaneAdi2 = grouped.Key.EczaneAdi2,
                                                                      EczaneId1 = grouped.Key.EczaneId1,
                                                                      EczaneId2 = grouped.Key.EczaneId2,
                                                                      //EczaneNobetGrupId1 = grouped.Key.EczaneNobetGrupId1,
                                                                      //EczaneNobetGrupId2 = grouped.Key.EczaneNobetGrupId2,
                                                                      //AltGrupAdi = grouped.Key.AltGrupAdi,
                                                                      AyniGunNobetSayisi = grouped.Count(),
                                                                      //Tarih = grouped.Key.Tarih,
                                                                      //TakvimId = s.TakvimId,
                                                                      //GunGrup = grouped.Key.GunGrup
                                                                  })
                                          .Where(w => w.AyniGunNobetSayisi > 1)
                                          .ToList();

                        if (ayniGunNobetSayisi1denFazlaGrouped.Count > 0)
                        {
                            //var ayIcindeSadece1KezAyniGunNobetKisit = new KpAyIcindeSadece1KezAyniGunNobet
                            //{
                            //    Model = model,
                            //    EczaneNobetTarihAralik = data.EczaneNobetTarihAralik,
                            //    IkiliEczaneler = data.IkiliEczanelerMesafe,
                            //    NobetUstGrupKisit = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "k10"),
                            //    Tarihler = data.TarihAraligi
                            //    .Where(w => w.TalepEdilenNobetciSayisi > 1
                            //             && w.GunGrupId != 1
                            //    ).ToList(),
                            //    KararDegiskeni = _x
                            //};
                            //AyIcindeSadece1KezAyniGunNobetTutulsun(ayIcindeSadece1KezAyniGunNobetKisit);

                            var kpAyIcindeSadece1KezAyniGunNobetDegiskenDonusumlu = new KpAyIcindeSadece1KezAyniGunNobetDegiskenDonusumlu
                            {
                                Model = model,
                                EczaneNobetTarihAralik = data.EczaneNobetTarihAralik,//.Where(w => w.GunGrupId != 2).ToList(),
                                EczaneNobetTarihAralikIkiliEczaneler = data.EczaneNobetTarihAralikIkiliEczaneler,
                                IkiliEczaneler = data.IkiliEczanelerMesafe,
                                NobetUstGrupKisit = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "k10"),
                                Tarihler = data.TarihAraligi,//.Where(w => w.GunGrupId != 2).ToList(),
                                KararDegiskeni = _x,
                                KararDegiskeniIkiliEczaneler = _y
                            };
                            AyIcindeSadece1KezAyniGunNobetTutulsun(kpAyIcindeSadece1KezAyniGunNobetDegiskenDonusumlu);

                            solution = solver.Solve(model);

                            modelStatus = solution.ModelStatus;

                            if (modelStatus != ModelStatus.Feasible)
                            {
                                //data.CalismaSayisi++;

                                results.Celiskiler = $"<h6 class= 'text-danger'>k10 tarih aralığında aynı gün 2 kez nöbet eczaneler bulunmaktadır.</h6>*1";
                                results.Celiskiler += CeliskileriEkle(solution);

                                throw new Exception($"Uygun çözüm bulunamadı!");
                            }
                        }

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

                        sonuclar = data.EczaneNobetTarihAralik.Where(s => _x[s].Value.IsAlmost(1) == true).ToList();

                        //var nobetGorevTipId = 1;

                        var nobetGrupTarihler1 = data.EczaneNobetTarihAralik
                             //.Where(w => w.NobetGorevTipId == nobetGorevTipId)
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


    }

}
