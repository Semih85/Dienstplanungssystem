﻿using OPTANO.Modeling.Common;
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
using WM.Optimization.Entities.KisitParametre;

namespace WM.Optimization.Concrete.Optano.Health.EczaneNobet
{
    public class AlanyaOptano : EczaneNobetKisit, IEczaneNobetAlanyaOptimizationV2
    {
        //Karar değişkeni model çalıştıktan sonra değer aldığından burada tanımlandı
        private VariableCollection<EczaneNobetTarihAralik> _x { get; set; }
        private VariableCollection<EczaneNobetTarihAralikIkili> _y { get; set; }

        private Model Model(AlanyaDataModel data)
        {
            var model = new Model() { Name = "Alanya Eczane Nöbet" };

            #region Veriler

            #region kısıtlar

            var bayramPespeseFarkliTur = NobetUstGrupKisit(data.Kisitlar, "bayramPespeseFarkliTur", data.NobetUstGrupId);

            var eczaneGrup = NobetUstGrupKisit(data.Kisitlar, "eczaneGrup", data.NobetUstGrupId);

            var pespeseHaftaIciAyniGunNobet = NobetUstGrupKisit(data.Kisitlar, "pespeseHaftaIciAyniGunNobet", data.NobetUstGrupId);
            var nobetBorcOdeme = NobetUstGrupKisit(data.Kisitlar, "nobetBorcOdeme", data.NobetUstGrupId);

            //var yildaEncokUcKezGrup = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "yildaEncokUcKezGrup", data.NobetUstGrupId);
            //var sonIkiAydakiGrup = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "sonIkiAydakiGrup", data.NobetUstGrupId);
            var ayIcindeAyniGunNobet = NobetUstGrupKisit(data.Kisitlar, "ayIcindeAyniGunNobet", data.NobetUstGrupId);
            var oncekiAylarAyniGunNobet = NobetUstGrupKisit(data.Kisitlar, "oncekiAylarAyniGunNobet", data.NobetUstGrupId);
            var ikiliEczaneAyniGunNobet = NobetUstGrupKisit(data.Kisitlar, "ikiliEczaneAyniGunNobet", data.NobetUstGrupId);

            //var altGruplarlaAyniGunNobetTutma = NobetUstGrupKisit(data.NobetUstGrupKisitlar, "altGruplarlaAyniGunNobetTutma", data.NobetUstGrupId);

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

            var amac = new Objective(Expression
                .Sum(from i in data.EczaneNobetTarihAralik
                     select (_x[i] * i.AmacFonksiyonKatsayi)),
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

                var nobetGrupKurallar = data.NobetGrupKurallar.Where(w => w.NobetGrupGorevTipId == nobetGrupGorevTip.Id).ToList();

                var pespeseNobetSayisi = (int)GetNobetGunKural(nobetGrupKurallar, 1);
                var gunlukNobetciSayisi = (int)GetNobetGunKural(nobetGrupKurallar, 3);
                var pespeseNobetSayisiHaftaIci = (int)GetNobetGunKural(nobetGrupKurallar, 5);
                var pespeseNobetSayisiPazar = (int)GetNobetGunKural(nobetGrupKurallar, 6);

                #region tarihler

                var tarihler = data.TarihAraligi
                    .Where(w => w.NobetGrupId == nobetGrupGorevTip.NobetGrupId
                             && w.NobetGorevTipId == nobetGrupGorevTip.NobetGorevTipId).ToList();

                //TalepleriTakvimeIsle(nobetGrupTalepler, gunlukNobetciSayisi, tarihler);
                var nobetGrupTalepler = tarihler
                    .GroupBy(g => g.TalepEdilenNobetciSayisi)
                    .Select(s => new
                    {
                        TalepEdilenNobetciSayisi = s.Key,
                        GunSayisi = s.Count()
                    }).ToList();

                var pazarGunleri = tarihler.Where(w => w.GunGrupId == 1).OrderBy(o => o.Tarih).ToList();
                var cumaGunleri = tarihler.Where(w => w.NobetGunKuralId == 6).OrderBy(o => o.Tarih).ToList();
                var cumartesiGunleri = tarihler.Where(w => w.NobetGunKuralId == 7).OrderBy(o => o.Tarih).ToList();
                var bayramlar = tarihler.Where(w => w.GunGrupId == 2).OrderBy(o => o.Tarih).ToList();
                var haftaIciGunleri = tarihler.Where(w => w.GunGrupId == 3).OrderBy(o => o.Tarih).ToList();

                var cumaVeCumartesiGunleri = tarihler.Where(w => w.NobetGunKuralId == 6 || w.NobetGunKuralId == 7).OrderBy(o => o.Tarih).ToList();
                var cumartesiVePazarGunleri = tarihler.Where(w => w.NobetGunKuralId == 1 || w.NobetGunKuralId == 7).OrderBy(o => o.Tarih).ToList();

                var gunSayisi = tarihler.Count();
                var pazarSayisi = pazarGunleri.Count();
                var bayramSayisi = bayramlar.Count();
                var haftaIciSayisi = haftaIciGunleri.Count();
                var cumartesiSayisi = cumartesiGunleri.Count();

                #endregion

                var nobetGunKurallar = tarihler.Select(s => s.NobetGunKuralId).Distinct().ToList();

                var r = new Random();

                var eczaneNobetGruplar = data.EczaneNobetGruplar
                    .Where(w => w.NobetGrupGorevTipId == nobetGrupGorevTip.Id)
                    .OrderBy(x => r.NextDouble())
                    .ToList();

                var eczaneNobetGrupGunKuralIstatistikler = data.EczaneNobetGrupGunKuralIstatistikYatay
                    .Where(w => w.NobetGrupGorevTipId == nobetGrupGorevTip.Id).ToList();

                var gruptakiEczaneSayisi = eczaneNobetGruplar.Count;

                // karar değişkeni - nöbet grup bazlı filtrelenmiş
                var eczaneNobetTarihAralikGrupBazli = data.EczaneNobetTarihAralik
                           .Where(e => e.NobetGrupGorevTipId == nobetGrupGorevTip.Id).ToList();

                //var eczaneNobetTarihAralikIkiliEczanelerGrupBazli = data.EczaneNobetTarihAralikIkiliEczaneler
                //     .Where(e => e.NobetGorevTipId == nobetGrupGorevTip.NobetGorevTipId
                //     && (e.NobetGrupId1 == nobetGrupGorevTip.NobetGrupId
                //      || e.NobetGrupId2 == nobetGrupGorevTip.NobetGrupId)).ToList();

                var nobetGrupGunKurallarAktifGunler = data.NobetGrupGunKurallar
                                                    .Where(s => s.NobetGrupId == nobetGrupGorevTip.NobetGrupId
                                                             && nobetGunKurallar.Contains(s.NobetGunKuralId))
                                                    .Select(s => s.NobetGunKuralId)
                                                    .ToList();

                //var nobetGrupGunGruplar = nobetGrupGunKurallar.Select(s=>s.)

                //var nobetGrupGorevTipler = data.NobetGrupGorevTipler.Where(w => w.NobetGrupId == nobetGrupGorevTip.NobetGrupId).ToList();

                #region peş peşe görev en az nöbet zamanı

                var farkliAyPespeseGorevAraligi = (gruptakiEczaneSayisi / gunlukNobetciSayisi * 1.2);
                //var altLimit = farkliAyPespeseGorevAraligi * 0.7666; //0.95
                var altLimit = (gruptakiEczaneSayisi / gunlukNobetciSayisi) * 0.6; //0.95

                //hafta içi                
                altLimit = GetArdisikBosGunSayisi(pespeseNobetSayisiHaftaIci, altLimit);

                var ustLimit = farkliAyPespeseGorevAraligi + farkliAyPespeseGorevAraligi * 0.6667; //77;
                var ustLimitKontrol = ustLimit * 0.95; //0.81 

                var pazarNobetiYazilabilecekIlkAy = (Math.Ceiling((double)gruptakiEczaneSayisi / 5) - 2) * 30;

                pazarNobetiYazilabilecekIlkAy = GetArdisikBosGunSayisi(pespeseNobetSayisiPazar, pazarNobetiYazilabilecekIlkAy);

                #endregion

                #region her ay ortalama nöbet sayıları

                var talepEdilenNobetciSayisi = tarihler.Sum(s => s.TalepEdilenNobetciSayisi);
                var talepEdilenNobetciSayisiHaftaIci = haftaIciGunleri.Sum(s => s.TalepEdilenNobetciSayisi);
                var talepEdilenNobetciSayisiCuma = cumaGunleri.Sum(s => s.TalepEdilenNobetciSayisi);
                var talepEdilenNobetciSayisiCumartesi = cumartesiGunleri.Sum(s => s.TalepEdilenNobetciSayisi);
                var talepEdilenNobetciSayisiPazar = pazarGunleri.Sum(s => s.TalepEdilenNobetciSayisi);

                var ortalamaNobetSayisi = OrtalamaNobetSayisi(talepEdilenNobetciSayisi, gruptakiEczaneSayisi);
                var ortamalaNobetSayisiHaftaIci = OrtalamaNobetSayisi(talepEdilenNobetciSayisiHaftaIci, gruptakiEczaneSayisi);
                var bayramOrtalamaNobetSayisi = OrtalamaNobetSayisi(bayramlar.Sum(s => s.TalepEdilenNobetciSayisi), gruptakiEczaneSayisi);

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

                var eczaneNobetSonuclarGrupBazliTumu = data.EczaneNobetSonuclar
                    .Where(w => w.NobetGrupGorevTipId == nobetGrupGorevTip.Id).ToList();

                var eczaneNobetSonuclarGrupBazli = eczaneNobetSonuclarGrupBazliTumu
                    .Where(w => w.Tarih >= data.NobetUstGrupBaslangicTarihi).ToList();

                var nobetGrupBayramNobetleri = eczaneNobetSonuclarGrupBazli
                    .Where(w => w.GunGrupId == 2
                             //&& w.Tarih >= data.NobetUstGrupBaslangicTarihi
                             )
                    .Select(s => new { s.TakvimId, s.NobetGunKuralId }).ToList();

                #region hafta içi dağılım

                var haftaIciOrtamalaNobetSayisi2 = ortamalaNobetSayisiHaftaIci;

                if (NobetUstGrupKisit(kisitlarAktif, "k32").SagTarafDegeri > 0)
                    haftaIciOrtamalaNobetSayisi2 += (int)NobetUstGrupKisit(kisitlarAktif, "k32").SagTarafDegeri;

                var yazilabilecekHaftaIciNobetSayisi = haftaIciOrtamalaNobetSayisi2;// - eczaneNobetIstatistik.NobetSayisiHaftaIci; 

                #endregion

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
                {
                    #region kontrol

                    var kontrol = false;

                    var kontrolEdilecekEczaneler = new string[] { "NUR", "FARUK", "HİDAYET" };

                    if (kontrol && kontrolEdilecekEczaneler.Contains(eczaneNobetGrup.EczaneAdi))
                    {
                    }

                    #endregion

                    #region eczane bazlı veriler

                    var eczaneKalibrasyon = data.Kalibrasyonlar
                        .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id).ToList();

                    // karar değişkeni - eczane bazlı filtrelenmiş
                    var eczaneNobetTarihAralikEczaneBazli = eczaneNobetTarihAralikGrupBazli
                               .Where(e => e.EczaneNobetGrupId == eczaneNobetGrup.Id).ToList();

                    //var eczaneNobetTarihAralikIkiliEczanelerEczaneBazli = data.EczaneNobetTarihAralikIkiliEczaneler
                    //           .Where(e => (e.EczaneNobetGrupId1 == eczaneNobetGrup.Id
                    //                     || e.EczaneNobetGrupId2 == eczaneNobetGrup.Id)).ToList();

                    //eczaneNobetSonuclarCozulenGrup - tüm sonuçlar
                    var eczaneNobetSonuclar = data.EczaneNobetSonuclar
                                    .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id).ToList();

                    var eczaneNobetIstatistik = eczaneNobetGrupGunKuralIstatistikler
                        .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id).SingleOrDefault() ?? new EczaneNobetGrupGunKuralIstatistikYatay();

                    if (eczaneNobetIstatistik.NobetSayisiPazar == 0)
                    {//0 olduğunda gerekmedikçe kullanmıyor zaten.
                        pazarNobetiYazilabilecekIlkAy *= 0.5;
                    }

                    var yazilabilecekIlkPazarTarihi = eczaneNobetIstatistik.SonNobetTarihiPazar.AddDays(pazarNobetiYazilabilecekIlkAy);
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
                        PespeseNobetSayisiAltLimit = altLimit,// gruptakiEczaneSayisi * 0.6, //altLimit,
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
                    pesPeseGorevEnAzFarkliAylar.EczaneNobetIstekler = data.EczaneNobetIstekler
                        .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id)
                        .ToList();//istisna: bayram nöbetleri manuel yazıldığı için eklendi

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
                    ortalamaEnFazlaBayram.OrtalamaNobetSayisi = bayramOrtalamaNobetSayisi;
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

                        herAyEnFazlaIlgiliKisit = GetNobetGunKuralIlgiliKisitTarihAraligi(kisitlarAktif, gunKural.NobetGunKuralId);

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
                        var bayramNobetleriAnahtarli = eczaneNobetSonuclar
                                .Where(w => w.GunGrupId == 2).ToList();

                        var toplamBayramNobetSayisi = eczaneNobetIstatistik.NobetSayisiBayram;

                        var bayramGunKuralIstatistikler = nobetGunKuralIstatistikler
                            .Where(w => w.GunGrupId == 2).ToList();

                        //var toplamBayramGrupToplamNobetSayisi = nobetGrupBayramNobetleri.Count();

                        var kumulatifBayramSayisi = bayramGunKuralIstatistikler.Sum(x => x.GunSayisi);

                        var yillikOrtalamaGunKuralSayisi = OrtalamaNobetSayisi(gunlukNobetciSayisi, gruptakiEczaneSayisi, kumulatifBayramSayisi);

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
                            if (bayramlar.Select(s => s.NobetGunKuralId).Contains(sonBayram.NobetGunKuralId) && !bayramPespeseFarkliTur.PasifMi)
                            {
                                var bayramPespeseFarkliTurKisit = new KpPespeseFarkliTurNobet
                                {
                                    Model = model,
                                    NobetUstGrupKisit = bayramPespeseFarkliTur,
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

                    var eczaneToplamKalibrasyon = GetKalibrasyonDegeri(eczaneKalibrasyon);

                    #region Toplam hafta içi toplam kalibrasyonlu 

                    var kpKumulatifToplamEnFazlaHaftaIciKalibrasyonlu = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnFazlaHaftaIciKalibrasyonlu.Tarihler = haftaIciGunleri;
                    kpKumulatifToplamEnFazlaHaftaIciKalibrasyonlu.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k54");//k16
                    kpKumulatifToplamEnFazlaHaftaIciKalibrasyonlu.ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiHaftaIci
                        + (int)eczaneToplamKalibrasyon.KalibrasyonToplamHaftaIci;
                    kpKumulatifToplamEnFazlaHaftaIciKalibrasyonlu.KumulatifOrtalamaGunKuralSayisi = ortalamaNobetSayisiKumulatifHaftaIci;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnFazlaHaftaIciKalibrasyonlu);

                    #endregion

                    #region Toplam pazar toplam kalibrasyonlu 

                    var kpKumulatifToplamEnFazlaPazarToplamKalibrasyonlu = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnFazlaPazarToplamKalibrasyonlu.Tarihler = pazarGunleri;
                    kpKumulatifToplamEnFazlaPazarToplamKalibrasyonlu.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k55");//k57
                    kpKumulatifToplamEnFazlaPazarToplamKalibrasyonlu.ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiPazar
                        + (int)eczaneToplamKalibrasyon.KalibrasyonToplamPazar;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnFazlaPazarToplamKalibrasyonlu);

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

                    #region Kümülatif bayram en az

                    var kpKumulatifToplamEnAzBayram = (KpKumulatifToplam)kpKumulatifToplam.Clone();

                    kpKumulatifToplamEnAzBayram.Tarihler = bayramlar;
                    kpKumulatifToplamEnAzBayram.NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k6");
                    kpKumulatifToplamEnAzBayram.ToplamNobetSayisi = eczaneNobetIstatistik.NobetSayisiBayram;

                    KumulatifToplamEnFazla(kpKumulatifToplamEnAzBayram);

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
            }

            #region Eş grup aynı gün nöbet

            var esGrubaAyniGunNobetYazma = new KpEsGrubaAyniGunNobetYazma
            {
                Model = model,
                EczaneNobetTarihAralik = data.EczaneNobetTarihAralik.Where(w => !w.BayramMi).ToList(),
                EczaneNobetSonuclar = data.EczaneGrupNobetSonuclarTumu,
                Tarihler = data.TarihAraligi,
                KararDegiskeni = _x
            };

            var esGrubaAyniGunNobetYazmaEczaneGruplar = (KpEsGrubaAyniGunNobetYazma)esGrubaAyniGunNobetYazma.Clone();
            esGrubaAyniGunNobetYazmaEczaneGruplar.NobetUstGrupKisit = NobetUstGrupKisit(data.Kisitlar, "k11");
            esGrubaAyniGunNobetYazmaEczaneGruplar.EczaneGruplar = data.EczaneGruplar;

            EsGruptakiEczanelereAyniGunNobetYazma(esGrubaAyniGunNobetYazmaEczaneGruplar);

            var esGrubaAyniGunNobetYazmaOncekiAylar = (KpEsGrubaAyniGunNobetYazma)esGrubaAyniGunNobetYazma.Clone();
            esGrubaAyniGunNobetYazmaOncekiAylar.EczaneNobetTarihAralik = data.EczaneNobetTarihAralik.Where(w => w.HaftaIciMi).ToList();
            esGrubaAyniGunNobetYazmaOncekiAylar.Tarihler = data.TarihAraligi.Where(w => w.GunGrupId == 3).ToList();
            esGrubaAyniGunNobetYazmaOncekiAylar.NobetUstGrupKisit = oncekiAylarAyniGunNobet;
            esGrubaAyniGunNobetYazmaOncekiAylar.EczaneGruplar = data.OncekiAylardaAyniGunNobetTutanEczaneler;

            EsGruptakiEczanelereAyniGunNobetYazma(esGrubaAyniGunNobetYazmaOncekiAylar);

            var esGrubaAyniGunNobetYazmaIkiliEczaneler = (KpEsGrubaAyniGunNobetYazma)esGrubaAyniGunNobetYazma.Clone();
            esGrubaAyniGunNobetYazmaIkiliEczaneler.EczaneNobetTarihAralik = data.EczaneNobetTarihAralik.Where(w => w.HaftaIciMi).ToList();
            esGrubaAyniGunNobetYazmaIkiliEczaneler.Tarihler = data.TarihAraligi.Where(w => w.GunGrupId == 3).ToList();
            esGrubaAyniGunNobetYazmaIkiliEczaneler.NobetUstGrupKisit = ikiliEczaneAyniGunNobet;
            esGrubaAyniGunNobetYazmaIkiliEczaneler.EczaneGruplar = data.ArasindaAyniGun2NobetFarkiOlanIkiliEczaneler;

            EsGruptakiEczanelereAyniGunNobetYazma(esGrubaAyniGunNobetYazmaIkiliEczaneler);

            #endregion

            #region Tarih aralığı içinde aynı gün nöbet

            //bu bölüm aşağıda. eğer çözüm varsa devreye giriyor.
            //var ayIcindeSadece1KezAyniGunNobetKisit = new KpAyIcindeSadece1KezAyniGunNobet
            //{
            //    Model = model,
            //    EczaneNobetTarihAralik = data.EczaneNobetTarihAralik,
            //    IkiliEczaneler = data.IkiliEczaneler,
            //    NobetUstGrupKisit = NobetUstGrupKisit(data.Kisitlar, "k10"),
            //    Tarihler = data.TarihAraligi,
            //    KararDegiskeni = _x
            //};
            //AyIcindeSadece1KezAyniGunNobetTutulsun(ayIcindeSadece1KezAyniGunNobetKisit);

            //var kpAyIcindeSadece1KezAyniGunNobetDegiskenDonusumlu = new KpAyIcindeSadece1KezAyniGunNobetDegiskenDonusumlu
            //{
            //    Model = model,
            //    EczaneNobetTarihAralik = data.EczaneNobetTarihAralik,
            //    EczaneNobetTarihAralikIkiliEczaneler = data.EczaneNobetTarihAralikIkiliEczaneler,
            //    IkiliEczaneler = data.IkiliEczaneler,
            //    NobetUstGrupKisit = ayIcindeAyniGunNobet,
            //    Tarihler = data.TarihAraligi,
            //    KararDegiskeni = _x,
            //    KararDegiskeniIkiliEczaneler = _y
            //};
            //AyIcindeSadece1KezAyniGunNobetTutulsun(kpAyIcindeSadece1KezAyniGunNobetDegiskenDonusumlu);

            #endregion

            #region Yılda en fazla aynı gün 3'ten fazla nöbet tutulmasın

            //if (data.CalismaSayisi == 5) sonIkiAydakiGrup = false;

            //if (!yildaEncokUcKezGrup.PasifMi)
            //{
            //    foreach (var g in data.YilIcindeAyniGunNobetTutanEczaneler.Select(s => s.Id).Distinct().ToList())
            //    {
            //        var ikiliEczaneler = data.YilIcindeAyniGunNobetTutanEczaneler
            //                                        .Where(w => w.Id == g
            //                                        && data.EczaneNobetGruplar.Select(s => s.EczaneId).Contains(w.EczaneId))
            //                                        .Select(s => s.EczaneId).ToList();

            //        var ikiliEczaneler2 = data.YilIcindeAyniGunNobetTutanEczaneler
            //                                        .Where(w => w.Id == g
            //                                        && !data.EczaneNobetGruplar.Select(s => s.EczaneId).Contains(w.EczaneId))
            //                                        .Select(s => s.EczaneId).ToList();

            //        var nobetTutulanGunler = data.EczaneNobetSonuclarOncekiAylar
            //            .Where(w => ikiliEczaneler2.Contains(w.EczaneId)
            //                     && w.Tarih.Month == data.Ay)
            //            .Select(s => s.TakvimId).ToList();

            //        if (nobetTutulanGunler.Count > 0 && ikiliEczaneler.Count > 0)
            //        {
            //            foreach (var tarih in data.TarihAraligi.Where(w => nobetTutulanGunler.Contains(w.TakvimId)))
            //            {
            //                model.AddConstraint(
            //                      Expression.Sum(data.EczaneNobetTarihAralik
            //                                       .Where(e => ikiliEczaneler.Contains(e.EczaneId)
            //                                                && e.TakvimId == tarih.TakvimId)
            //                                       .Select(m => _x[m])) == 0,
            //                                       $"yilda diger gruptaki eczaneler ile en fazla bir kez nobet tutulsun, {g}");
            //            }
            //        }

            //    }

            //}
            #endregion

            #endregion

            //var kisitSayisi = model.ConstraintsCount;

            return model;
        }

        public EczaneNobetSonucModel Solve(AlanyaDataModel data)
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

                        if (data.CalismaSayisi == calismaSayisiEnFazla)
                        {
                            results.Celiskiler = CeliskileriEkle(solution);
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
                            //    IkiliEczaneler = data.IkiliEczaneler,
                            //    NobetUstGrupKisit = NobetUstGrupKisit(data.Kisitlar, "k10"),
                            //    Tarihler = data.TarihAraligi,
                            //    //EczaneGruplar = data.EczaneGruplar,
                            //    KararDegiskeni = _x
                            //};
                            //AyIcindeSadece1KezAyniGunNobetTutulsun(ayIcindeSadece1KezAyniGunNobetKisit);

                            var kpAyIcindeSadece1KezAyniGunNobetDegiskenDonusumlu = new KpAyIcindeSadece1KezAyniGunNobetDegiskenDonusumlu
                            {
                                Model = model,
                                EczaneNobetTarihAralik = data.EczaneNobetTarihAralik,
                                EczaneNobetTarihAralikIkiliEczaneler = data.EczaneNobetTarihAralikIkiliEczaneler,
                                IkiliEczaneler = data.IkiliEczaneler,
                                NobetUstGrupKisit = NobetUstGrupKisit(data.Kisitlar, "k10"),
                                Tarihler = data.TarihAraligi,
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

                        sonuclar = data.EczaneNobetTarihAralik.Where(s => _x[s].Value.IsAlmost(1) == true).ToList();//BUNU SAKIN KAPATMA.. 

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

                    var celiskiler = new string[1000];

                    if (results.Celiskiler != null)
                    {
                        celiskiler = results.Celiskiler.Split('*');
                    }

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


/*
 #region Kümülatif ve her ay en falza kısıtları

                    var nobetGunKuralNobetSayilari = new List<NobetGunKuralNobetSayisi>();

                    foreach (var gunKural in nobetGunKuralIstatistikler)
                    {
                        int toplamNobetSayisi = GetToplamGunKuralNobetSayisi(eczaneNobetIstatistik, gunKural.NobetGunKuralId);

                        nobetGunKuralNobetSayilari.Add(new NobetGunKuralNobetSayisi
                        {
                            NobetGunKuralAdi = gunKural.NobetGunKuralAdi,
                            NobetGunKuralId = gunKural.NobetGunKuralId,
                            NobetSayisi = toplamNobetSayisi,
                            GunGrupId = gunKural.GunGrupId,
                            GunGrupAdi = gunKural.GunGrupAdi
                        });
                    }

                    var haftaIciEnCokNobetSayisi = 0;
                    var haftaIciEnAzNobetSayisi = 0;

                    if (nobetGunKuralIstatistikler.Count > 0)
                    {
                        haftaIciEnCokNobetSayisi = nobetGunKuralNobetSayilari.Max(m => m.NobetSayisi);
                        haftaIciEnAzNobetSayisi = nobetGunKuralNobetSayilari.Min(m => m.NobetSayisi);
                    }

                    foreach (var gunKural in nobetGunKuralIstatistikler)
                    {
                        if (kontrol && gunKural.NobetGunKuralAdi == "Pazartesi")
                        {
                        }

                        herAyEnFazlaIlgiliKisit = GetNobetGunKuralIlgiliKisit(kisitlarAktif, gunKural.NobetGunKuralId);

                        var tarihAralik = nobetGunKuralTarihler.Where(w => w.NobetGunKuralId == gunKural.NobetGunKuralId).SingleOrDefault() ?? _nobetGunKuralTarihAralik;

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

                        #region Kümülatif toplam en fazla - Tur takip kısıtı

                        var kumulatifOrtalamaGunKuralNobetSayisi = tarihAralik.KumulatifOrtalamaNobetSayisi;

                        int gunKuralNobetSayisi = GetToplamGunKuralNobetSayisi(eczaneNobetIstatistik, gunKural.NobetGunKuralId);

                        var haftaIciEnCokVeGunKuralNobetleriArasindakiFark = haftaIciEnCokNobetSayisi - gunKuralNobetSayisi;
                        var haftaIciEnAzVeEnCokNobetSayisiArasindakiFark = haftaIciEnCokNobetSayisi - haftaIciEnAzNobetSayisi;

                        if (NobetUstGrupKisit(kisitlarAktif, "k34").SagTarafDegeri > 0 && !ozelTurTakibiYapilacakGunler.Contains(gunKural.NobetGunKuralId))
                            kumulatifOrtalamaGunKuralNobetSayisi = NobetUstGrupKisit(kisitlarAktif, "k34").SagTarafDegeri;

                        if (gunKuralNobetSayisi > kumulatifOrtalamaGunKuralNobetSayisi)
                            kumulatifOrtalamaGunKuralNobetSayisi = gunKuralNobetSayisi;

                        if (haftaIciEnAzVeEnCokNobetSayisiArasindakiFark < NobetUstGrupKisit(kisitlarAktif, "k44").SagTarafDegeri && !NobetUstGrupKisit(kisitlarAktif, "k44").PasifMi)
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
                            NobetUstGrupKisit = NobetUstGrupKisit(kisitlarAktif, "k34"),
                            KumulatifOrtalamaGunKuralSayisi = kumulatifOrtalamaGunKuralNobetSayisi,
                            ToplamNobetSayisi = gunKuralNobetSayisi,
                            GunKuralAdi = gunKural.NobetGunKuralAdi,
                            KararDegiskeni = _x
                        };
                        KumulatifToplamEnFazla(kumulatifToplamEnFazla);
                        #endregion

                        #region hafta içi dağılım
                        //if (data.CalismaSayisi >= 1)
                        //    yazilabilecekHaftaIciNobetSayisi++;

                        //var haftaIciToplam = eczaneNobetIstatistik.NobetSayisiHaftaIci + yazilabilecekHaftaIciNobetSayisi;
                        //var haftaIciOrtalama = Math.Ceiling((double)haftaIciToplam / 6);

                        //if (toplamNobetSayisi >= haftaIciOrtalama)
                        //    haftaIciOrtalama = toplamNobetSayisi;

                        //if (data.CalismaSayisi == 2)
                        //    haftaIciOrtalama++;

                        //HaftaIciGunleri(model, haftaIciGunleri, eczaneNobetGrup, eczaneNobetTarihAralikEczaneBazli, haftaIciOrtalama, toplamNobetSayisi, NobetUstGrupKisit(kisitlarAktif, "k44"), _x); 
                        #endregion
                    }
                    #endregion
     */
