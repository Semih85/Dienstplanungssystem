﻿using OPTANO.Modeling.Common;
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
    public class AntalyaMerkezOptano : EczaneNobetKisit, IEczaneNobetAntalyaMerkezOptimization
    {
        //Karar değişkeni model çalıştıktan sonra değer aldığından burada tanımlandı
        private VariableCollection<EczaneNobetTarihAralik> _x { get; set; }

        private Model Model(AntalyaMerkezDataModel data)
        {
            var model = new Model() { Name = "Antalya Merkez Eczane Nöbet" };

            #region Veriler            

            #region kısıtlar

            var eczaneGrup = NobetUstGrupKisit(data.Kisitlar, "eczaneGrup", data.NobetUstGrupId);

            var pespeseHaftaIciAyniGunNobet = NobetUstGrupKisit(data.Kisitlar, "pespeseHaftaIciAyniGunNobet", data.NobetUstGrupId);
            var nobetBorcOdeme = NobetUstGrupKisit(data.Kisitlar, "nobetBorcOdeme", data.NobetUstGrupId);

            var altGruplarlaAyniGunNobetTutma = NobetUstGrupKisit(data.Kisitlar, "altGruplarlaAyniGunNobetTutma", data.NobetUstGrupId);

            //pasifler
            
            #endregion

            //özel tur takibi yapılacak günler
            var ozelTurTakibiYapilacakGunler =
                new List<int> {
                    1 // pazar
                };

            var herAyEnFazlaIlgiliKisit = new NobetUstGrupKisitDetay();

            #endregion            

            #region Karar Değişkenleri
            _x = new VariableCollection<EczaneNobetTarihAralik>(
                    model,
                    data.EczaneNobetTarihAralik,
                    "_x",
                    t => $"{t.EczaneNobetGrupId}_{t.TakvimId}, {t.NobetGrupAdi}, {t.EczaneAdi}, {t.Tarih.ToShortDateString()}",
                    item => 0,//data.LowerBound,
                    item => 1,//data.UpperBound,
                    item => VariableType.Binary);

            #endregion

            #region Amaç Fonksiyonu

            var amac = new Objective(Expression
                .Sum(from i in data.EczaneNobetTarihAralik
                     select (_x[i] * i.AmacFonksiyonKatsayi)),
                     "Sum of all item-values: ",
                     ObjectiveSense.Minimize);

            model.AddObjective(amac);

            //var sure_kd_amacFonksiyonu = stopwatch.Elapsed;
            #endregion

            #region Kısıtlar

            foreach (var nobetGrupGorevTip in data.NobetGrupGorevTipler)
            {
                #region kısıtlar grup bazlı

                var kisitlarGrupBazli = data.NobetGrupGorevTipKisitlar.Where(w => w.NobetGrupGorevTipId == nobetGrupGorevTip.Id).ToList();

                var kisitlarAktif = GetKisitlarNobetGrupBazli(data.Kisitlar, kisitlarGrupBazli);

                #endregion

                #region ön hazırlama       

                #region tarihler

                var nobetGrupKurallar = data.NobetGrupKurallar.Where(w => w.NobetGrupId == nobetGrupGorevTip.NobetGrupId).ToList();
                
                var pespeseNobetSayisi = (int)GetNobetGunKural(nobetGrupKurallar, 1);
                var gunlukNobetciSayisi = (int)GetNobetGunKural(nobetGrupKurallar, 3);
                var pespeseNobetSayisiHaftaIci = (int)GetNobetGunKural(nobetGrupKurallar, 5);
                var pespeseNobetSayisiPazar = (int)GetNobetGunKural(nobetGrupKurallar, 6);

                //var nobetGrupTalepler = data.NobetGrupTalepler.Where(w => w.NobetGrupId == nobetGrupGorevTip.NobetGrupId).ToList();

                var tarihler = data.TarihAraligi
                    .Where(w => w.NobetGrupGorevTipId == nobetGrupGorevTip.Id).ToList();

                var nobetGrupTalepler = tarihler
                    .GroupBy(g => g.TalepEdilenNobetciSayisi)
                    .Select(s => new
                    {
                        TalepEdilenNobetciSayisi = s.Key,
                        GunSayisi = s.Count()
                    }).ToList();

                //TalepleriTakvimeIsle(nobetGrupTalepler, gunlukNobetciSayisi, tarihler);

                var pazarGunleri = tarihler.Where(w => w.NobetGunKuralId == 1).OrderBy(o => o.Tarih).ToList();
                var cumaGunleri = tarihler.Where(w => w.NobetGunKuralId == 6).OrderBy(o => o.Tarih).ToList();
                var cumaVeCumartesiGunleri = tarihler.Where(w => w.NobetGunKuralId == 6 || w.NobetGunKuralId == 7).OrderBy(o => o.Tarih).ToList();
                var cumartesiVePazarGunleri = tarihler.Where(w => w.NobetGunKuralId == 1 || w.NobetGunKuralId == 7).OrderBy(o => o.Tarih).ToList();
                var bayramlar = tarihler.Where(w => w.GunGrupId == 2).OrderBy(o => o.Tarih).ToList();
                var haftaIciGunleri = tarihler.Where(w => w.GunGrupId == 3).OrderBy(o => o.Tarih).ToList();
                var cumartesiGunleri = tarihler.Where(w => w.NobetGunKuralId == 7).OrderBy(o => o.Tarih).ToList();

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
                    .OrderBy(x => r.NextDouble())
                    .ToList();

                var eczaneNobetGrupGunKuralIstatistikler = data.EczaneNobetGrupGunKuralIstatistikYatay
                    .Where(w => w.NobetGrupId == nobetGrupGorevTip.NobetGrupId).ToList();

                var gruptakiEczaneSayisi = eczaneNobetGruplar.Count;

                // karar değişkeni - nöbet grup bazlı filtrelenmiş
                var eczaneNobetTarihAralikGrupBazli = data.EczaneNobetTarihAralik
                           .Where(e => e.NobetGrupGorevTipId == nobetGrupGorevTip.Id).ToList();

                var nobetGrupGunKurallarAktifGunler = data.NobetGrupGunKurallar
                                                    .Where(s => s.NobetGrupId == nobetGrupGorevTip.NobetGrupId
                                                             && nobetGunKurallar.Contains(s.NobetGunKuralId))
                                                    .Select(s => s.NobetGunKuralId).ToList();

                //var nobetGrupGorevTipler = data.NobetGrupGorevTipler.Where(w => w.NobetGrupId == nobetGrupGorevTip.NobetGrupId).ToList();

                #region peş peşe görev en az nöbet zamanı
                //hafta içi
                var farkliAyPespeseGorevAraligi = (gruptakiEczaneSayisi / gunlukNobetciSayisi * 1.2);
                //var altLimit = farkliAyPespeseGorevAraligi * 0.7666; //0.95
                var altLimit = (gruptakiEczaneSayisi / gunlukNobetciSayisi) * 0.6; //0.95

                //hafta içi                
                altLimit = GetArdisikBosGunSayisi(pespeseNobetSayisiHaftaIci, altLimit);

                var ustLimit = farkliAyPespeseGorevAraligi + farkliAyPespeseGorevAraligi * 0.6667; //77;
                var ustLimitKontrol = ustLimit * 0.95; //0.81 

                //pazar
                var pazarNobetiYazilabilecekIlkAy = (int)Math.Ceiling((double)gruptakiEczaneSayisi / 5) - 1;

                #endregion

                #region ortalama nöbet sayıları

                var talepEdilenNobetciSayisi = tarihler.Sum(s => s.TalepEdilenNobetciSayisi);
                var talepEdilenNobetciSayisiHaftaIci = haftaIciGunleri.Sum(s => s.TalepEdilenNobetciSayisi);
                var talepEdilenNobetciSayisiCuma = cumaGunleri.Sum(s => s.TalepEdilenNobetciSayisi);
                var talepEdilenNobetciSayisiCumartesi = cumartesiGunleri.Sum(s => s.TalepEdilenNobetciSayisi);
                var talepEdilenNobetciSayisiPazar = pazarGunleri.Sum(s => s.TalepEdilenNobetciSayisi);

                var ortalamaNobetSayisi = OrtalamaNobetSayisi(talepEdilenNobetciSayisi, gruptakiEczaneSayisi);
                var ortamalaNobetSayisiHaftaIci = OrtalamaNobetSayisi(talepEdilenNobetciSayisiHaftaIci, gruptakiEczaneSayisi);
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

                #endregion

                #endregion

                var eczaneNobetSonuclarGrupBazliTumu = data.EczaneNobetSonuclar
                    .Where(w => w.NobetGrupId == nobetGrupGorevTip.NobetGrupId).ToList();

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

                var nobetGunKuralTarihler = new List<NobetGunKuralTarihAralik>();

                foreach (var item in nobetGunKuralIstatistikler)
                {
                    var tarihler2 = tarihler.Where(w => w.NobetGunKuralId == item.NobetGunKuralId).ToList();
                    var gunKuralGunSayisi = tarihler2.Count;

                    nobetGunKuralTarihler.Add(new NobetGunKuralTarihAralik
                    {
                        GunGrupId = item.GunGrupId,
                        GunGrupAdi = item.GunGrupAdi,
                        NobetGunKuralId = item.NobetGunKuralId,
                        NobetGunKuralAdi = item.NobetGunKuralAdi,
                        TakvimNobetGruplar = tarihler2,
                        GunSayisi = gunKuralGunSayisi,
                        OrtalamaNobetSayisi = OrtalamaNobetSayisi(tarihler2.Sum(s => s.TalepEdilenNobetciSayisi), gruptakiEczaneSayisi),
                        KumulatifGunSayisi = item.GunSayisi,
                        KumulatifOrtalamaNobetSayisi = OrtalamaNobetSayisi(item.TalepEdilenNobetciSayisi, gruptakiEczaneSayisi)
                    });
                }
                #endregion

                #region talep

                var talebiKarsilaKisitParametreModel = new KpTalebiKarsila
                {
                    EczaneNobetTarihAralikTumu = eczaneNobetTarihAralikGrupBazli,
                    NobetGrupGorevTip = nobetGrupGorevTip,
                    Tarihler = tarihler,
                    Model = model,
                    KararDegiskeni = _x
                };

                TalebiKarsila(talebiKarsilaKisitParametreModel);

                #endregion

                foreach (var eczaneNobetGrup in eczaneNobetGruplar)
                {//eczane bazlı kısıtlar

                    #region kontrol

                    var kontrol = false;

                    if (kontrol)
                    {
                        var kontrolEdilecekEczaneler = new string[] { "AYDIN" };

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
                        GruptakiNobetciSayisi = gruptakiEczaneSayisi
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
                        PespeseNobetSayisiAltLimit = altLimit, //gruptakiEczaneSayisi * 0.6, //altLimit,
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

                    var pesPeseGorevEnAzHaftaIci = (KpPesPeseGorevEnAz)kpPesPeseGorevEnAz.Clone();

                    pesPeseGorevEnAzHaftaIci.NobetSayisi = eczaneNobetIstatistik.NobetSayisiHaftaIci;
                    pesPeseGorevEnAzHaftaIci.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k27");
                    pesPeseGorevEnAzHaftaIci.Tarihler = haftaIciGunleri;
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

                    var ortalamaEnFazlaHaftaIici = (KpTarihAraligiOrtalamaEnFazla)kpTarihAraligiOrtalamaEnFazla.Clone();
                    ortalamaEnFazlaHaftaIici.Tarihler = haftaIciGunleri;
                    ortalamaEnFazlaHaftaIici.GunSayisi = haftaIciSayisi;
                    ortalamaEnFazlaHaftaIici.OrtalamaNobetSayisi = ortamalaNobetSayisiHaftaIci;
                    ortalamaEnFazlaHaftaIici.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k32");

                    TarihAraligiOrtalamaEnFazla(ortalamaEnFazlaHaftaIici);

                    var ortalamaEnFazlaBayram = (KpTarihAraligiOrtalamaEnFazla)kpTarihAraligiOrtalamaEnFazla.Clone();
                    ortalamaEnFazlaBayram.Tarihler = bayramlar;
                    ortalamaEnFazlaBayram.GunSayisi = bayramSayisi;
                    ortalamaEnFazlaBayram.OrtalamaNobetSayisi = ortalamaNobetSayisiBayram;
                    ortalamaEnFazlaBayram.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k14");

                    TarihAraligiOrtalamaEnFazla(ortalamaEnFazlaBayram);

                    #endregion

                    #region Kümülatif ve her ay en falza kısıtları

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

                    var nobetGunKuralNobetSayilari = GetNobetGunKuralNobetSayilari(nobetGunKuralIstatistikler, eczaneNobetIstatistik);

                    #region tarih aralığı en fazla (gün kuralları)

                    var aktifGunKurallar = nobetGunKuralIstatistikler.Where(w => w.NobetGunKuralKapanmaTarihi == null).ToList();

                    foreach (var gunKural in aktifGunKurallar)
                    {//gun kural bazlı

                        //if (gunKural.NobetGunKuralKapanmaTarihi != null)
                        //    continue;

                        //if (kontrol && gunKural.NobetGunKuralAdi == "Cuma")
                        //{
                        //}

                        herAyEnFazlaIlgiliKisit = GetNobetGunKuralIlgiliKisit(kisitlarAktif, gunKural.NobetGunKuralId);

                        var tarihAralik = nobetGunKuralTarihler
                            .Where(w => w.NobetGunKuralId == gunKural.NobetGunKuralId).SingleOrDefault() ?? new NobetGunKuralTarihAralik();

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
                            GunKuralAdi = gunKural.NobetGunKuralAdi,
                            KararDegiskeni = _x
                        };

                        TarihAraligiOrtalamaEnFazla(tarihAraligiOrtalamaEnFazlaIlgiliKisit);
                    }

                    #endregion

                    #region kümülatif toplam en fazla - Tur takip kısıtı

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

                    var gunKuralKumulatifToplamEnFazla = NobetUstGrupKisit(kisitlarAktif, "k34");

                    var kumulatifEnfazlaHaftaIciDagilimi = NobetUstGrupKisit(kisitlarAktif, "k44");

                    if (!gunKuralKumulatifToplamEnFazla.PasifMi)
                    {
                        foreach (var gunKural in aktifGunKurallar)
                        {//gun kural bazlı

                            //if (gunKural.NobetGunKuralKapanmaTarihi != null)
                            //    continue;

                            //if (kontrol && gunKural.NobetGunKuralAdi == "Cuma")
                            //{
                            //}

                            var tarihAralik = nobetGunKuralTarihler
                                .Where(w => w.NobetGunKuralId == gunKural.NobetGunKuralId).SingleOrDefault() ?? new NobetGunKuralTarihAralik();

                            var kumulatifOrtalamaGunKuralNobetSayisi = tarihAralik.KumulatifOrtalamaNobetSayisi;

                            int gunKuralNobetSayisi = GetToplamGunKuralNobetSayisi(eczaneNobetIstatistik, gunKural.NobetGunKuralId);

                            var haftaIciEnCokVeGunKuralNobetleriArasindakiFark = haftaIciEnCokNobetSayisi - gunKuralNobetSayisi;
                            var haftaIciEnAzVeEnCokNobetSayisiArasindakiFark = haftaIciEnCokNobetSayisi - haftaIciEnAzNobetSayisi;

                            if (gunKuralKumulatifToplamEnFazla.SagTarafDegeri > 0 && !ozelTurTakibiYapilacakGunler.Contains(gunKural.NobetGunKuralId))
                                kumulatifOrtalamaGunKuralNobetSayisi = gunKuralKumulatifToplamEnFazla.SagTarafDegeri;

                            if (gunKuralNobetSayisi > kumulatifOrtalamaGunKuralNobetSayisi)
                                kumulatifOrtalamaGunKuralNobetSayisi = gunKuralNobetSayisi;

                            if (haftaIciEnAzVeEnCokNobetSayisiArasindakiFark <= kumulatifEnfazlaHaftaIciDagilimi.SagTarafDegeri && !kumulatifEnfazlaHaftaIciDagilimi.PasifMi)
                            {//hafta içi dağılım
                                if (!nobetGrupGunKurallarAktifGunler.Contains(gunKural.NobetGunKuralId)
                                     //&& !ozelTurTakibiYapilacakGunler.Contains(gunKural.NobetGunKuralId)
                                     && haftaIciEnCokVeGunKuralNobetleriArasindakiFark >= 1
                                     )
                                    kumulatifOrtalamaGunKuralNobetSayisi++;

                                //if (data.CalismaSayisi == 1
                                //    //&& !nobetGrupGunKurallar.Contains(gunKural.NobetGunKuralId)
                                //    && !ozelTurTakibiYapilacakGunler.Contains(gunKural.NobetGunKuralId)
                                //    && haftaIciEnCokVeGunKuralNobetleriArasindakiFark >= 1
                                //    )
                                //    kumulatifOrtalamaGunKuralNobetSayisi++;

                                //if (data.CalismaSayisi == 2
                                //    //&& !nobetGrupGunKurallar.Contains(gunKural.NobetGunKuralId)
                                //    && !ozelTurTakibiYapilacakGunler.Contains(gunKural.NobetGunKuralId)
                                //    && haftaIciEnCokVeGunKuralNobetleriArasindakiFark >= 0// haftaIciEnCokNobetSayisi
                                //    )
                                //    kumulatifOrtalamaGunKuralNobetSayisi++;

                                //if (data.CalismaSayisi == 3
                                //    && !nobetGrupGunKurallar.Contains(gunKural.NobetGunKuralId)
                                //    //&& !ozelTurTakibiYapilacakGunler.Contains(gunKural.NobetGunKuralId)
                                //    //&& haftaIciEnAzVeEnCokNobetSayisiArasindakiFark <= haftaIciEnCokNobetSayisi
                                //    )
                                //    kumulatifOrtalamaGunKuralNobetSayisi++;
                            }

                            var kumulatifToplamEnFazla = new KpKumulatifToplam
                            {
                                Model = model,
                                Tarihler = tarihAralik.TakvimNobetGruplar,
                                EczaneNobetGrup = eczaneNobetGrup,
                                EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazli,
                                NobetUstGrupKisit = gunKuralKumulatifToplamEnFazla,
                                KumulatifOrtalamaGunKuralSayisi = kumulatifOrtalamaGunKuralNobetSayisi,
                                ToplamNobetSayisi = gunKuralNobetSayisi,
                                GunKuralAdi = $"k34 {gunKural.NobetGunKuralAdi}",
                                KararDegiskeni = _x
                            };

                            KumulatifToplamEnFazla(kumulatifToplamEnFazla);
                        }
                    }

                    if (!kumulatifEnfazlaHaftaIciDagilimi.PasifMi)
                    {
                        foreach (var gunKural in nobetGunKuralIstatistikler)
                        {//gun kural bazlı

                            //if (kontrol && gunKural.NobetGunKuralAdi == "Cuma")
                            //{
                            //}

                            var tarihAralik = nobetGunKuralTarihler
                                .Where(w => w.NobetGunKuralId == gunKural.NobetGunKuralId).SingleOrDefault() ?? new NobetGunKuralTarihAralik();

                            var kumulatifOrtalamaGunKuralNobetSayisi = tarihAralik.KumulatifOrtalamaNobetSayisi;

                            int gunKuralNobetSayisi = GetToplamGunKuralNobetSayisi(eczaneNobetIstatistik, gunKural.NobetGunKuralId);

                            var haftaIciEnCokVeGunKuralNobetleriArasindakiFark = haftaIciEnCokNobetSayisi - gunKuralNobetSayisi;
                            var haftaIciEnAzVeEnCokNobetSayisiArasindakiFark = haftaIciEnCokNobetSayisi - haftaIciEnAzNobetSayisi;

                            if (gunKuralKumulatifToplamEnFazla.SagTarafDegeri > 0 && !ozelTurTakibiYapilacakGunler.Contains(gunKural.NobetGunKuralId))
                                kumulatifOrtalamaGunKuralNobetSayisi = gunKuralKumulatifToplamEnFazla.SagTarafDegeri;

                            if (gunKuralNobetSayisi > kumulatifOrtalamaGunKuralNobetSayisi)
                                kumulatifOrtalamaGunKuralNobetSayisi = gunKuralNobetSayisi;

                            //haftaninGunleriDagilimi.SagTarafDegeri = 3

                            if (haftaIciEnAzVeEnCokNobetSayisiArasindakiFark < kumulatifEnfazlaHaftaIciDagilimi.SagTarafDegeri && !kumulatifEnfazlaHaftaIciDagilimi.PasifMi)
                            {//hafta içi dağılım
                                if (!nobetGrupGunKurallarAktifGunler.Contains(gunKural.NobetGunKuralId)
                                     //&& !ozelTurTakibiYapilacakGunler.Contains(gunKural.NobetGunKuralId)
                                     && haftaIciEnCokVeGunKuralNobetleriArasindakiFark >= 1
                                     )
                                    kumulatifOrtalamaGunKuralNobetSayisi++;

                                //if (data.CalismaSayisi == 1
                                //    //&& !nobetGrupGunKurallar.Contains(gunKural.NobetGunKuralId)
                                //    && !ozelTurTakibiYapilacakGunler.Contains(gunKural.NobetGunKuralId)
                                //    && haftaIciEnCokVeGunKuralNobetleriArasindakiFark >= 1
                                //    )
                                //    kumulatifOrtalamaGunKuralNobetSayisi++;

                                //if (data.CalismaSayisi == 2
                                //    //&& !nobetGrupGunKurallar.Contains(gunKural.NobetGunKuralId)
                                //    && !ozelTurTakibiYapilacakGunler.Contains(gunKural.NobetGunKuralId)
                                //    && haftaIciEnCokVeGunKuralNobetleriArasindakiFark >= 0// haftaIciEnCokNobetSayisi
                                //    )
                                //    kumulatifOrtalamaGunKuralNobetSayisi++;

                                //if (data.CalismaSayisi == 3
                                //    && !nobetGrupGunKurallar.Contains(gunKural.NobetGunKuralId)
                                //    //&& !ozelTurTakibiYapilacakGunler.Contains(gunKural.NobetGunKuralId)
                                //    //&& haftaIciEnAzVeEnCokNobetSayisiArasindakiFark <= haftaIciEnCokNobetSayisi
                                //    )
                                //    kumulatifOrtalamaGunKuralNobetSayisi++;

                                var kumulatifToplamEnFazla = new KpKumulatifToplam
                                {
                                    Model = model,
                                    Tarihler = tarihAralik.TakvimNobetGruplar,
                                    EczaneNobetGrup = eczaneNobetGrup,
                                    EczaneNobetTarihAralik = eczaneNobetTarihAralikEczaneBazli,
                                    NobetUstGrupKisit = gunKuralKumulatifToplamEnFazla,
                                    KumulatifOrtalamaGunKuralSayisi = kumulatifOrtalamaGunKuralNobetSayisi,
                                    ToplamNobetSayisi = gunKuralNobetSayisi,
                                    GunKuralAdi = $"k44 {gunKural.NobetGunKuralAdi}",
                                    KararDegiskeni = _x
                                };

                                KumulatifToplamEnFazla(kumulatifToplamEnFazla);
                            }
                        }
                    }

                    #endregion

                    #endregion

                    #region Bayram kümülatif toplam en fazla ve farklı tür bayram

                    if (bayramSayisi > 0)
                    {
                        //var bayramNobetleri = nobetGunKuralNobetSayilari
                        //    .Where(w => w.GunGrupId == 2).ToList();

                        var bayramNobetleriAnahtarli = eczaneNobetSonuclar
                                .Where(w => w.GunGrupId == 2).ToList();

                        var toplamBayramNobetSayisi = eczaneNobetIstatistik.NobetSayisiBayram;// bayramNobetleri.Sum(s => s.NobetSayisi);

                        var bayramGunKuralIstatistikler = nobetGunKuralIstatistikler
                                                        .Where(w => w.GunGrupId == 2).ToList();

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
                                    KumulatifOrtalamaGunKuralSayisi = yillikOrtalamaGunKuralSayisi,
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
                    kpKumulatifToplamEnFazla.KumulatifOrtalamaGunKuralSayisi = ortalamaNobetSayisiKumulatif;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnFazla);

                    #endregion 

                    #region Hafta içi toplam max hedefler

                    var kpKumulatifToplamEnFazlaHaftaIci = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnFazlaHaftaIci.Tarihler = haftaIciGunleri;
                    kpKumulatifToplamEnFazlaHaftaIci.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k16");
                    kpKumulatifToplamEnFazlaHaftaIci.ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiHaftaIci;
                    kpKumulatifToplamEnFazlaHaftaIci.KumulatifOrtalamaGunKuralSayisi = ortalamaNobetSayisiKumulatifHaftaIci;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnFazlaHaftaIci);

                    #endregion

                    #region Toplam cuma cumartesi max hedefler

                    var kpKumulatifToplamEnFazlaCumaVeCumartesi = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnFazlaCumaVeCumartesi.Tarihler = cumaVeCumartesiGunleri;
                    kpKumulatifToplamEnFazlaCumaVeCumartesi.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k25");
                    kpKumulatifToplamEnFazlaCumaVeCumartesi.ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiCuma + eczaneNobetIstatistik.NobetSayisiCumartesi;
                    kpKumulatifToplamEnFazlaCumaVeCumartesi.KumulatifOrtalamaGunKuralSayisi = ortalamaNobetSayisiKumulatifCumaVeCumartesi;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnFazlaCumaVeCumartesi);

                    #endregion

                    #region Toplam cumartesi ve pazar max hedefler

                    var kpKumulatifToplamEnFazlaCumartesiVePazar = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnFazlaCumartesiVePazar.Tarihler = cumartesiVePazarGunleri;
                    kpKumulatifToplamEnFazlaCumartesiVePazar.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k50");
                    kpKumulatifToplamEnFazlaCumartesiVePazar.ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiPazar + eczaneNobetIstatistik.NobetSayisiCumartesi;
                    kpKumulatifToplamEnFazlaCumartesiVePazar.KumulatifOrtalamaGunKuralSayisi = ortalamaNobetSayisiKumulatifPazar;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnFazlaCumartesiVePazar);

                    #endregion

                    #region Toplam Cuma Max Hedef

                    var kpKumulatifToplamEnFazlaCuma = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnFazlaCuma.Tarihler = cumaGunleri;
                    kpKumulatifToplamEnFazlaCuma.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k20");
                    kpKumulatifToplamEnFazlaCuma.ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiCuma;
                    kpKumulatifToplamEnFazlaCuma.KumulatifOrtalamaGunKuralSayisi = ortalamaNobetSayisiKumulatifCuma;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnFazlaCuma);

                    #endregion

                    #region Toplam Cumartesi Max Hedef

                    var kpKumulatifToplamEnFazlaCumartesi = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnFazlaCumartesi.Tarihler = cumartesiGunleri;
                    kpKumulatifToplamEnFazlaCumartesi.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k21");
                    kpKumulatifToplamEnFazlaCumartesi.ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiCumartesi;
                    kpKumulatifToplamEnFazlaCumartesi.KumulatifOrtalamaGunKuralSayisi = ortalamaNobetSayisiKumulatifCumartesi;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnFazlaCumartesi);

                    #endregion

                    #region Toplam Pazar Max Hedef

                    var kpKumulatifToplamEnFazlaPazar = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnFazlaPazar.Tarihler = pazarGunleri;
                    kpKumulatifToplamEnFazlaPazar.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k57");
                    kpKumulatifToplamEnFazlaPazar.ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiPazar;
                    kpKumulatifToplamEnFazlaPazar.KumulatifOrtalamaGunKuralSayisi = ortalamaNobetSayisiKumulatifPazar;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnFazlaPazar);

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
                    kpKumulatifToplamEnAzPazar.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k6");
                    kpKumulatifToplamEnAzPazar.ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiPazar;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnAzPazar);

                    #endregion

                    #region Kümülatif hafta içi en az

                    var kpKumulatifToplamEnAzHaftaIci = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnAzHaftaIci.Tarihler = haftaIciGunleri;
                    kpKumulatifToplamEnAzHaftaIci.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k17");
                    kpKumulatifToplamEnAzHaftaIci.ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiHaftaIci;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnAzHaftaIci);

                    #endregion

                    #region Kümülatif bayram en az

                    var kpKumulatifToplamEnAzBayram = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnAzBayram.Tarihler = bayramlar;
                    kpKumulatifToplamEnAzBayram.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k56");
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

                #region istek ve mazeretler

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
            }

            #region Eş gruplar (aynı gün nöbet)

            var kpEsGrubaAyniGunNobetYazma = new KpEsGrubaAyniGunNobetYazma
            {
                Model = model,
                KararDegiskeni = _x
            };

            var esGrubaAyniGunNobetYazmaEczaneGruplar = (KpEsGrubaAyniGunNobetYazma)kpEsGrubaAyniGunNobetYazma.Clone();

            esGrubaAyniGunNobetYazmaEczaneGruplar.EczaneNobetTarihAralik = data.EczaneNobetTarihAralik;
            esGrubaAyniGunNobetYazmaEczaneGruplar.EczaneNobetSonuclar = data.EczaneGrupNobetSonuclarTumu;
            esGrubaAyniGunNobetYazmaEczaneGruplar.NobetUstGrupKisit = eczaneGrup;
            esGrubaAyniGunNobetYazmaEczaneGruplar.EczaneGruplar = data.EczaneGruplar;
            esGrubaAyniGunNobetYazmaEczaneGruplar.Tarihler = data.TarihAraligi;

            EsGruptakiEczanelereAyniGunNobetYazma(esGrubaAyniGunNobetYazmaEczaneGruplar);

            #endregion

            #region Alt gruplar (aynı gün nöbet)

            var altGrupluTakipEdilecekNobetGrupIdList =
                new List<int> {
                            13, //antalya-10
                            14  //antalya-11
                };
            //altGruplarlaAyniGunNobetTutmayacakEczaneNobetTarihAralik
            var eczaneNobetTarihAralikAtlGruplu = data.EczaneNobetTarihAralik
                .Where(w => altGrupluTakipEdilecekNobetGrupIdList.Contains(w.NobetGrupId)).ToList();

            kpEsGrubaAyniGunNobetYazma.EczaneNobetTarihAralik = eczaneNobetTarihAralikAtlGruplu;
            kpEsGrubaAyniGunNobetYazma.NobetUstGrupKisit = altGruplarlaAyniGunNobetTutma;

            var gunGruplar = data.TarihAraligi
                .Select(s => new { s.GunGrupId, s.GunGrupAdi })
                .Distinct()
                .OrderBy(o => o.GunGrupId).ToList();

            foreach (var gunGrup in gunGruplar)
            {
                kpEsGrubaAyniGunNobetYazma.Tarihler = data.TarihAraligi
                    .Where(w => altGrupluTakipEdilecekNobetGrupIdList.Contains(w.NobetGrupId)
                             && w.GunGrupId == gunGrup.GunGrupId).ToList();

                var kpEsGrubaAyniGunNobetYazmaGunGrup = (KpEsGrubaAyniGunNobetYazma)kpEsGrubaAyniGunNobetYazma.Clone();

                kpEsGrubaAyniGunNobetYazmaGunGrup.EczaneNobetSonuclar = GetSonuclarByGunGrup(data.EczaneNobetSonuclarAltGruplarlaBirlikte, gunGrup.GunGrupId);
                kpEsGrubaAyniGunNobetYazmaGunGrup.EczaneGruplar = GetEczaneGruplarByEczaneGrupTanimTipId(data.AltGruplarlaAyniGunNobetTutmayacakEczaneler, gunGrup.GunGrupId);

                EsGruptakiEczanelereAyniGunNobetYazma(kpEsGrubaAyniGunNobetYazmaGunGrup);
            }

            #endregion           

            #endregion

            return model;
        }

        public EczaneNobetSonucModel Solve(AntalyaMerkezDataModel data)
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

                        if (data.CalismaSayisi == calismaSayisiEnFazla //+ 1
                            )
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

                    mesaj = "Aşağıdaki açıklamalara göre <a href=\"/EczaneNobet/NobetUstGrupKisit/KisitAyarla\" class=\"card-link\" target=\"_blank\">nöbet ayarlarında</a> bazı değişiklikler yaparak <strong>tekrar çözmelisiniz..</strong>"
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
    }
}
