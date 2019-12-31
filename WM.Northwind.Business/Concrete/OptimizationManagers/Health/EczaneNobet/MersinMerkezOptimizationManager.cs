using System;
using System.Collections.Generic;
using System.Linq;
using WM.Core.Aspects.PostSharp.AutorizationAspects;
using WM.Core.Aspects.PostSharp.ExceptionAspects;
using WM.Core.Aspects.PostSharp.LogAspects;
using WM.Core.Aspects.PostSharp.TranstionAspects;
using WM.Core.CrossCuttingConcerns.Logging.Log4Net.Logger;
using WM.Northwind.Business.Abstract.EczaneNobet;
using WM.Northwind.Business.Abstract.Optimization.EczaneNobet;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;
using WM.Northwind.Entities.Concrete.Optimization.EczaneNobet;
using WM.Optimization.Abstract.Health;

namespace WM.Northwind.Business.Concrete.OptimizationManagers.Health.EczaneNobet
{
    public class MersinMerkezOptimizationManager : IMersinMerkezOptimizationServiceV2
    {
        #region ctor
        private IEczaneGrupService _eczaneGrupService;
        private IEczaneGrupTanimService _eczaneGrupTanimService;
        private IEczaneNobetMersinMerkezOptimizationV2 _eczaneNobetMersinMerkezOptimizationV2;
        private IEczaneNobetGrupService _eczaneNobetGrupService;
        private IEczaneNobetGrupAltGrupService _eczaneNobetGrupAltGrupService;
        private IEczaneNobetIstekService _eczaneNobetIstekService;
        private IEczaneNobetMazeretService _eczaneNobetMazeretService;
        private IEczaneNobetOrtakService _eczaneNobetOrtakService;
        private IEczaneNobetSonucAktifService _eczaneNobetSonucAktifService;
        private IEczaneNobetSonucService _eczaneNobetSonucService;
        private INobetGrupGorevTipService _nobetGrupGorevTipService;
        private INobetGrupGunKuralService _nobetGrupGunKuralService;
        private INobetGrupKuralService _nobetGrupKuralService;
        private INobetGrupService _nobetGrupService;
        private INobetGrupTalepService _nobetGrupTalepService;
        private INobetUstGrupKisitService _nobetUstGrupKisitService;
        private ITakvimService _takvimService;
        private IEczaneNobetMuafiyetService _eczaneNobetMuafiyetService;
        private IAyniGunTutulanNobetService _ayniGunTutulanNobetService;
        private INobetGrupGorevTipKisitService _nobetGrupGorevTipKisitService;
        private IKalibrasyonService _kalibrasyonService;
        private IDebugEczaneService _debugEczaneService;

        public MersinMerkezOptimizationManager(
                    IEczaneGrupService eczaneGrupService,
                    IEczaneGrupTanimService eczaneGrupTanimService,
                    IEczaneNobetMersinMerkezOptimizationV2 eczaneNobetMersinMerkezOptimizationV2,
                    IEczaneNobetGrupService eczaneNobetGrupService,
                    IEczaneNobetIstekService eczaneNobetIstekService,
                    IEczaneNobetMazeretService eczaneNobetMazeretService,
                    IEczaneNobetOrtakService eczaneNobetOrtakService,
                    IEczaneNobetSonucAktifService eczaneNobetSonucAktifService,
                    INobetGrupGorevTipService nobetGrupGorevTipService,
                    INobetGrupGunKuralService nobetGrupGunKuralService,
                    INobetGrupKuralService nobetGrupKuralService,
                    INobetGrupService nobetGrupService,
                    INobetGrupTalepService nobetGrupTalepService,
                    INobetUstGrupKisitService nobetUstGrupKisitService,
                    ITakvimService takvimService,
                    IEczaneNobetSonucService eczaneNobetSonucService,
                    IEczaneNobetMuafiyetService eczaneNobetMuafiyetService,
                    IEczaneNobetGrupAltGrupService eczaneNobetGrupAltGrupService,
                    IAyniGunTutulanNobetService ayniGunTutulanNobetService,
                    INobetGrupGorevTipKisitService nobetGrupGorevTipKisitService,
                    IKalibrasyonService kalibrasyonService,
                    IDebugEczaneService debugEczaneService
            )
        {
            _eczaneGrupService = eczaneGrupService;
            _eczaneGrupTanimService = eczaneGrupTanimService;
            _eczaneNobetMersinMerkezOptimizationV2 = eczaneNobetMersinMerkezOptimizationV2;
            _eczaneNobetGrupService = eczaneNobetGrupService;
            _eczaneNobetIstekService = eczaneNobetIstekService;
            _eczaneNobetMazeretService = eczaneNobetMazeretService;
            _eczaneNobetOrtakService = eczaneNobetOrtakService;
            _eczaneNobetSonucAktifService = eczaneNobetSonucAktifService;
            _eczaneNobetSonucService = eczaneNobetSonucService;
            _nobetGrupGorevTipService = nobetGrupGorevTipService;
            _nobetGrupGunKuralService = nobetGrupGunKuralService;
            _nobetGrupKuralService = nobetGrupKuralService;
            _nobetGrupService = nobetGrupService;
            _nobetGrupTalepService = nobetGrupTalepService;
            _nobetUstGrupKisitService = nobetUstGrupKisitService;
            _takvimService = takvimService;
            _eczaneNobetMuafiyetService = eczaneNobetMuafiyetService;
            _eczaneNobetGrupAltGrupService = eczaneNobetGrupAltGrupService;
            _ayniGunTutulanNobetService = ayniGunTutulanNobetService;
            _nobetGrupGorevTipKisitService = nobetGrupGorevTipKisitService;
            _kalibrasyonService = kalibrasyonService;
            _debugEczaneService = debugEczaneService;
        }
        #endregion

        /// <summary>
        /// Her çözüm sonrasında aktif sonuçlardaki mevcut kayıtlar silinip yerine yeni sonuçlar eklenir.
        /// </summary>
        /// <param name="data"></param>
        public EczaneNobetSonucModel EczaneNobetCozAktifiGuncelle(MersinMerkezDataModelV2 data)
        {
            var yeniSonuclar = _eczaneNobetMersinMerkezOptimizationV2.Solve(data);
            AktiftekiArtiklariSil(data.NobetUstGrupId);
            //yeni sonuçları ekle
            _eczaneNobetSonucAktifService.CokluEkle(yeniSonuclar.ResultModel);

            return yeniSonuclar;
        }

        public EczaneNobetSonucModel EczaneNobetCozSonuclaraEkle(MersinMerkezDataModelV2 data)
        {
            var yeniSonuclar = _eczaneNobetMersinMerkezOptimizationV2.Solve(data);
            _eczaneNobetSonucService.CokluEkle(yeniSonuclar.ResultModel);
            return yeniSonuclar;
        }

        /// <summary>
        /// Eğer başka nöbet gruplarından kalan data varsa siler.
        /// --artık kayıtlar--
        /// </summary>
        public void AktiftekiArtiklariSil(int nobetUstGrupId)
        {
            var mevcutSonuclar = _eczaneNobetSonucAktifService.GetDetaylar2(nobetUstGrupId)
                .Select(s => s.Id).ToArray();

            _eczaneNobetSonucAktifService.CokluSil(mevcutSonuclar);
        }

        /// <summary>
        /// Eczane nöbet sonuc aktifteki tüm kayıtları alıp eczane nöbet sonuçlar tablosuna ekler.
        /// </summary>
        public void Kesinlestir(int nobetUstGrupId)
        {
            var eczaneNobetSonucAktifler = _eczaneNobetSonucAktifService.GetCozumler(nobetUstGrupId);
            _eczaneNobetSonucService.CokluEkle(eczaneNobetSonucAktifler);
        }

        private MersinMerkezDataModelV2 EczaneNobetDataModel(EczaneNobetDataModelParametre eczaneNobetDataModelParametre)
        {
            #region parametreler
            var nobetUstGrupId = eczaneNobetDataModelParametre.NobetUstGrupId;
            var yilBaslangic = eczaneNobetDataModelParametre.YilBaslangic;
            var yilBitis = eczaneNobetDataModelParametre.YilBitis;
            var ayBaslangic = eczaneNobetDataModelParametre.AyBaslangic;
            var ayBitis = eczaneNobetDataModelParametre.AyBitis;
            var nobetGrupIdListe = eczaneNobetDataModelParametre.NobetGrupId.ToList();
            //var nobetGorevTipId = eczaneNobetDataModelParametre.NobetGorevTipId;
            var nobetUstGrupBaslangicTarihi = eczaneNobetDataModelParametre.NobetUstGrupBaslangicTarihi;
            var ayBitisBaslangicGunu = eczaneNobetDataModelParametre.AyBitisBaslangicGunu;
            var ayBitisBitisGunu = eczaneNobetDataModelParametre.AyBitisBitisGunu;

            var baslangicTarihi = eczaneNobetDataModelParametre.BaslangicTarihi;
            var bitisTarihi = eczaneNobetDataModelParametre.BitisTarihi;
            var nobetGrupGorevTipler = eczaneNobetDataModelParametre.NobetGrupGorevTipler;
            var nobetGorevTipId = nobetGrupGorevTipler.FirstOrDefault().NobetGorevTipId;

            #endregion

            if (baslangicTarihi < nobetUstGrupBaslangicTarihi)
                throw new Exception($"Nöbet başlangıç tarihi <strong>({baslangicTarihi.ToShortDateString()})</strong> üst grup başlama tarihinden <strong>({nobetUstGrupBaslangicTarihi.ToShortDateString()})</strong> küçük olamaz.");

            var debugYapilacakEczaneler = _debugEczaneService.GetDetaylarAktifOlanlar(nobetUstGrupId);

            var nobetGruplar = _nobetGrupService.GetDetaylar(nobetGrupIdListe).OrderBy(s => s.Id).ToList();
            //var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetGorevTipId, nobetGrupIdListe);
            var eczaneNobetSonuclar = _eczaneNobetSonucService.GetSonuclar(nobetUstGrupId); //nobetGrupIdListe
            //.Where(w => !(w.EczaneNobetGrupId == 301 && w.TakvimId == 88)).ToList();
            //.Where(w => w.Tarih.Year > 2017).ToList();

            var eczaneNobetMazeretNobettenDusenler = new List<EczaneNobetMazeretSayilari>();

            var mazeret = _nobetUstGrupKisitService.GetKisitPasifMi("mazeret", nobetUstGrupId);

            if (mazeret)
                eczaneNobetMazeretNobettenDusenler = _eczaneNobetMazeretService.GetEczaneNobetMazeretSayilari(baslangicTarihi, bitisTarihi, nobetGrupIdListe);

            var eczaneNobetGruplar = _eczaneNobetGrupService.GetDetaylar(nobetGrupIdListe, baslangicTarihi, bitisTarihi)
                .Where(w => !eczaneNobetMazeretNobettenDusenler.Select(s => s.EczaneNobetGrupId).Contains(w.Id)).ToList();

            var eczaneNobetSonuclarCozulenGruplar = eczaneNobetSonuclar
                .Where(w => eczaneNobetGruplar.Select(s => s.Id).Contains(w.EczaneNobetGrupId)).ToList();

            var eczaneNobetSonuclarBaslamaTarihindenSonrasi = eczaneNobetSonuclarCozulenGruplar
                .Where(w => w.Tarih >= nobetUstGrupBaslangicTarihi).ToList();

            var son3Ay = baslangicTarihi.AddMonths(-3);

            var eczaneNobetSonuclarSon3Ay = eczaneNobetSonuclarCozulenGruplar
                .Where(w => w.Tarih >= son3Ay).ToList();

            var sonuclarKontrol = _eczaneNobetSonucService.GetSonuclar(baslangicTarihi, bitisTarihi, eczaneNobetSonuclarCozulenGruplar);

            if (sonuclarKontrol.Count > 0)
                throw new Exception("Kriterlere uygun <strong>daha önce yazılmış nöbetler</strong> bulunmaktadır. Lütfen kontrol ediniz!");

            var enSonNobetler = _eczaneNobetOrtakService.GetEczaneNobetGrupGunKuralIstatistik(eczaneNobetGruplar, eczaneNobetSonuclarCozulenGruplar);

            var enSonNobetlerSon3Ay = _eczaneNobetOrtakService.GetEczaneNobetGrupGunKuralIstatistik(eczaneNobetGruplar, eczaneNobetSonuclarSon3Ay);

            var eczaneNobetGrupGunKuralIstatistikYatay = _eczaneNobetOrtakService.GetEczaneNobetGrupGunKuralIstatistikYatay(enSonNobetler);

            var eczaneNobetGrupGunKuralIstatistikYataySon3Ay = _eczaneNobetOrtakService.GetEczaneNobetGrupGunKuralIstatistikYatay(enSonNobetlerSon3Ay);

            var grupluEczaneNobetSonuclar = _eczaneNobetSonucService.GetSonuclar(baslangicTarihi, bitisTarihi, eczaneNobetSonuclar);

            var eczaneGrupEdges = _eczaneGrupService.GetEdges()
                .Where(e => (nobetGrupIdListe.Contains(e.FromNobetGrupId)
                          || nobetGrupIdListe.Contains(e.ToNobetGrupId)))
                .Where(w => (grupluEczaneNobetSonuclar.Select(s => s.EczaneId).Distinct().Contains(w.From)
                          || grupluEczaneNobetSonuclar.Select(s => s.EczaneId).Distinct().Contains(w.To))
                      ).ToList();

            //sonuçlarda ilişkili eczaneler
            var eczaneGruplar = _eczaneGrupService.GetDetaylarAktifGruplar(nobetUstGrupId)
                .Where(x => (eczaneGrupEdges.Select(s => s.From).Distinct().Contains(x.EczaneId) || eczaneGrupEdges.Select(s => s.To).Distinct().Contains(x.EczaneId))
                          || nobetGrupIdListe.Contains(x.NobetGrupId)
                      ).ToList();

            //fazladan gelen tanımlar var iyileştirmekte fayda var
            var eczaneGrupTanimlar = _eczaneGrupTanimService.GetDetaylarAktifTanimList(eczaneGruplar.Select(x => x.EczaneGrupTanimId).ToList());
            var eczaneGruplar2 = _eczaneGrupService.GetDetaylarByEczaneGrupTanimId(eczaneGrupTanimlar.Select(s => s.Id).ToList());

            var eczaneGrupNobetSonuclar = grupluEczaneNobetSonuclar
                .Where(w => eczaneGruplar2.Select(s => s.EczaneId).Contains(w.EczaneId)).ToList();

            //nöbet yazılacak tarih aralığı(örn. Ocak ayının tüm günleri)
            var tarihAralik = _takvimService.GetTakvimNobetGruplar(baslangicTarihi, bitisTarihi, nobetGrupIdListe, nobetGorevTipId);

            //var tarihAralikAltGrupluGruplar = _takvimService.GetTakvimNobetGrupAltGruplar(baslangicTarihi, bitisTarihi, nobetGrupIdListe, nobetGorevTipId);

            var eczaneKumulatifHedefler = new List<EczaneNobetIstatistik>();
            var eczaneNobetIstatistikler = new List<EczaneNobetIstatistik>();

            var eczaneNobetTarihAralik1 = _takvimService.GetEczaneNobetTarihAralik(baslangicTarihi, bitisTarihi, nobetGorevTipId, nobetGrupIdListe)
                .Where(w => eczaneNobetGruplar.Select(s => s.EczaneId).Contains(w.EczaneId)).ToList();

            var eczaneNobetTarihAralik = _eczaneNobetOrtakService.AmacFonksiyonuKatsayisiBelirle(eczaneNobetTarihAralik1, eczaneNobetGrupGunKuralIstatistikYatay);

            var takipEdilecekEczaneler = new List<int> { 664 };

            var ikiliEczaneler = _ayniGunTutulanNobetService.GetDetaylar(nobetGrupIdListe)
                                .Where(w => //(eczaneNobetGruplar.Select(s => s.EczaneId).Contains(w.EczaneId1)
                                            //|| eczaneNobetGruplar.Select(s => s.EczaneId).Contains(w.EczaneId2))
                                            //  (takipEdilecekEczaneler.Contains(w.EczaneNobetGrupId1)
                                            //|| takipEdilecekEczaneler.Contains(w.EczaneNobetGrupId2))
                            (
                               (w.EczaneAdi1 == "ÖYKÜ" && w.EczaneAdi2 == "ALTUNBAŞ")
                            || (w.EczaneAdi1 == "ÖYKÜ" && w.EczaneAdi2 == "GÜLÇİN")
                            || (w.EczaneAdi1 == "TATLIDİL" && w.EczaneAdi2 == "ANIT")
                            || (w.EczaneAdi1 == "DİCLE" && w.EczaneAdi2 == "AYSEL")
                            || (w.EczaneAdi1 == "POLEN" && w.EczaneAdi2 == "SİTELER")
                            || (w.EczaneAdi1 == "RÜYA" && w.EczaneAdi2 == "AYFER")
                            )

                            //( 
                            //   (w.NobetGrupId1 == 15 && w.NobetGrupId2 == 16) //toroslar 1-2

                            //|| (w.NobetGrupId1 == 17 && w.NobetGrupId2 == 18) //Akdeniz 1-2
                            //|| (w.NobetGrupId1 == 17 && w.NobetGrupId2 == 19) //Akdeniz 1-3
                            //|| (w.NobetGrupId1 == 18 && w.NobetGrupId2 == 19) //Akdeniz 2-3

                            //|| (w.NobetGrupId1 == 20 && w.NobetGrupId2 == 21) //Yenişehir 1-2
                            //|| (w.NobetGrupId1 == 20 && w.NobetGrupId2 == 22) //Yenişehir 1-3
                            //|| (w.NobetGrupId1 == 21 && w.NobetGrupId2 == 22) //Yenişehir 2-3

                            //|| (w.NobetGrupId1 == 23 && w.NobetGrupId2 == 24) //Mezitli 1-2
                            //)
                            ).ToList();

            //2. karar değişkeni. her eczane ve ilgili altgrup
            var eczaneNobetAltGrupTarihAralik = _takvimService.GetEczaneNobetAltGrupTarihAralik(baslangicTarihi, bitisTarihi, nobetGorevTipId, nobetGrupIdListe)
                .Where(w => eczaneNobetGruplar.Select(s => s.EczaneId).Contains(w.EczaneId)).ToList();

            var eczaneNobetIstekler = _eczaneNobetIstekService.GetDetaylarByNobetGrupIdList(baslangicTarihi, bitisTarihi.AddDays(10), nobetGrupIdListe)
                .Where(w => eczaneNobetGruplar.Select(s => s.EczaneId).Contains(w.EczaneId)).ToList();

            var eczaneNobetMazeretler = _eczaneNobetMazeretService.GetDetaylarByEczaneIdList(baslangicTarihi, bitisTarihi, eczaneNobetGruplar.Select(s => s.EczaneId).Distinct().ToList())
                .Where(w => w.MazeretId != 3)
                .ToList();

            var takvimNobetGrupGunDegerIstatistikler = _takvimService.GetTakvimNobetGrupGunDegerIstatistikler(nobetUstGrupBaslangicTarihi, bitisTarihi, nobetGrupGorevTipler);

            var eczaneNobetGrupAltGruplarYenisehir2 = _eczaneNobetGrupAltGrupService.GetDetaylarByNobetGrupId(21);
            var eczaneNobetGrupAltGruplarToroslar1 = _eczaneNobetGrupAltGrupService.GetDetaylarByNobetGrupId(15);

            var altGruplarlaAyniGunNobetTutma = _nobetUstGrupKisitService.GetDetay("altGruplarlaAyniGunNobetTutma", nobetUstGrupId);
            var altGruplarlaAyniGunNobetTutmaToroslar = _nobetUstGrupKisitService.GetDetay("altGruplarlaAyniGunNobetTutmaToroslar", nobetUstGrupId);

            var eczaneNobetSonuclarAltGruplaAyniGun = new List<EczaneNobetSonucListe2>();
            var eczaneNobetSonuclarAltGruplaAyniGunToroslar = new List<EczaneNobetSonucListe2>();

            var indisId = 0;

            var altGruplarlaAyniGunNobetTutmayacakEczanelerYeniSehir1_2 = new List<EczaneGrupDetay>();
            var altGruplarlaAyniGunNobetTutmayacakEczanelerYeniSehir3_2 = new List<EczaneGrupDetay>();
            var altGruplarlaAyniGunNobetTutmayacakEczanelerToroslar = new List<EczaneGrupDetay>();

            if (!altGruplarlaAyniGunNobetTutma.PasifMi)
            {
                //indisId = eczaneGruplar2.Select(s => s.EczaneGrupTanimId).Max();

                #region yenişehir

                var altGrupluTakipEdilecekNobetGrupIdList =
                     new List<int> {
                                           20, //Yenişehir-1
                                           21, //Yenişehir-2 - alt grubu olan
                                           22  //Yenişehir-3
                     };

                var eczaneNobetGruplarAltGruplaAyniGun = _eczaneNobetGrupService.GetDetaylar(altGrupluTakipEdilecekNobetGrupIdList, baslangicTarihi, bitisTarihi)
                        .Where(w => !eczaneNobetMazeretNobettenDusenler.Select(s => s.EczaneNobetGrupId).Contains(w.Id)).ToList();

                eczaneNobetSonuclarAltGruplaAyniGun = eczaneNobetSonuclar
                        .Where(w => altGrupluTakipEdilecekNobetGrupIdList.Contains(w.NobetGrupId)).ToList();

                var ayniGunNobetTutmasiTakipEdilecekGruplar_1 = new List<int>
                    {
                        20,//Yenişehir-1,
                        //22 //Yenişehir-3 (M.Ü. Hastanesi)
                    };

                var ayniGunNobetTutmasiTakipEdilecekGruplar_3 = new List<int>
                    {
                        //20,//Yenişehir-1,
                        22 //Yenişehir-3 (M.Ü. Hastanesi)
                    };

                var altGrubuOlanNobetGruplar = new List<int>
                    {
                        21//Yenişehir-2
                    };

                altGruplarlaAyniGunNobetTutmayacakEczanelerYeniSehir1_2 = _eczaneNobetOrtakService.AltGruplarlaSiraliNobetListesiniOlusturMersin(
                    eczaneNobetSonuclarAltGruplaAyniGun,
                    eczaneNobetGruplarAltGruplaAyniGun,
                    eczaneNobetGrupAltGruplarYenisehir2,
                    altGruplarlaAyniGunNobetTutma,
                    nobetUstGrupBaslangicTarihi,
                    indisId,
                    ayniGunNobetTutmasiTakipEdilecekGruplar_1,
                    altGrubuOlanNobetGruplar,
                    0,
                    (int)altGruplarlaAyniGunNobetTutma.SagTarafDegeri);

                altGruplarlaAyniGunNobetTutmayacakEczanelerYeniSehir3_2 = _eczaneNobetOrtakService.AltGruplarlaSiraliNobetListesiniOlusturMersin(
                    eczaneNobetSonuclarAltGruplaAyniGun,
                    eczaneNobetGruplarAltGruplaAyniGun,
                    eczaneNobetGrupAltGruplarYenisehir2,
                    altGruplarlaAyniGunNobetTutma,
                    nobetUstGrupBaslangicTarihi,
                    indisId,
                    ayniGunNobetTutmasiTakipEdilecekGruplar_3,
                    altGrubuOlanNobetGruplar,
                    0,
                    (int)altGruplarlaAyniGunNobetTutma.SagTarafDegeri);
                #endregion                
            }

            if (!altGruplarlaAyniGunNobetTutmaToroslar.PasifMi)
            {
                #region toroslar

                var altGrupluTakipEdilecekNobetGrupIdListToroslar =
                 new List<int> {
                                           15, //Toroslar-1 - alt grubu olan
                                           16  //Toroslar-2 
                 };

                var eczaneNobetGruplarAltGruplaAyniGunToroslar = _eczaneNobetGrupService.GetDetaylar(altGrupluTakipEdilecekNobetGrupIdListToroslar, baslangicTarihi, bitisTarihi)
                        .Where(w => !eczaneNobetMazeretNobettenDusenler.Select(s => s.EczaneNobetGrupId).Contains(w.Id)).ToList();

                eczaneNobetSonuclarAltGruplaAyniGunToroslar = eczaneNobetSonuclar
                        .Where(w => altGrupluTakipEdilecekNobetGrupIdListToroslar.Contains(w.NobetGrupId)).ToList();

                var ayniGunNobetTutmasiTakipEdilecekGruplarToroslar = new List<int>
                    {
                        16,//Toroslar-1,
                    };

                var altGrubuOlanNobetGruplarToroslar = new List<int>
                    {
                        15//Toroslar-2
                    };

                altGruplarlaAyniGunNobetTutmayacakEczanelerToroslar = _eczaneNobetOrtakService
                    .AltGruplarlaSiraliNobetListesiniOlusturMersin(eczaneNobetSonuclarAltGruplaAyniGunToroslar, eczaneNobetGruplarAltGruplaAyniGunToroslar, eczaneNobetGrupAltGruplarToroslar1,
                    altGruplarlaAyniGunNobetTutma, nobetUstGrupBaslangicTarihi, 1,
                    ayniGunNobetTutmasiTakipEdilecekGruplarToroslar, altGrubuOlanNobetGruplarToroslar, 0, (int)altGruplarlaAyniGunNobetTutmaToroslar.SagTarafDegeri);

                #endregion
            }

            #region ikili eczaneler

            var ikiliEczaneAyniGunNobet = _nobetUstGrupKisitService.GetDetay("ikiliEczaneAyniGunNobet", nobetUstGrupId);

            var arasindaAyniGun2NobetFarkiOlanIkiliEczaneler = new List<EczaneGrupDetay>();

            if (!ikiliEczaneAyniGunNobet.PasifMi)
            {
                //indisId = eczaneGruplar2.Select(s => s.EczaneGrupTanimId).Max();
                arasindaAyniGun2NobetFarkiOlanIkiliEczaneler = _ayniGunTutulanNobetService.GetArasinda2FarkOlanIkiliEczaneleri(
                    eczaneNobetGruplar,
                    nobetGrupGorevTipler.Select(s => s.Id).ToArray(),
                    (int)ikiliEczaneAyniGunNobet.SagTarafDegeri);
            }

            #endregion

            #region önceki aylar aynı gün nöbet tutanlar çözülen ayda aynı gün nöbetçi olmasın

            var oncekiAylardaAyniGunNobetTutanEczaneler = new List<EczaneGrupDetay>();

            var oncekiAylarAyniGunNobet = _nobetUstGrupKisitService.GetDetay("oncekiAylarAyniGunNobet", nobetUstGrupId);

            if (!oncekiAylarAyniGunNobet.PasifMi && (int)oncekiAylarAyniGunNobet.SagTarafDegeri > 0)
            {
                indisId = eczaneGruplar2.Select(s => s.EczaneGrupTanimId).Max();
                oncekiAylardaAyniGunNobetTutanEczaneler = _eczaneNobetSonucService.OncekiAylardaAyniGunNobetTutanlar(
                    baslangicTarihi,
                    eczaneNobetSonuclarBaslamaTarihindenSonrasi,
                    indisId,
                    (int)oncekiAylarAyniGunNobet.SagTarafDegeri);
            }

            #endregion

            #region sonraki aydaki istekler
            var sonrakiAy = bitisTarihi.AddDays(1);
            var bitisTarihiSonrakiAy = bitisTarihi.AddMonths((int)oncekiAylarAyniGunNobet.SagTarafDegeri + 2);

            var eczaneNobetIsteklerSonrakiDonem = _eczaneNobetIstekService.GetDetaylarByNobetGrupIdList(sonrakiAy, bitisTarihiSonrakiAy, nobetGrupIdListe)
                .Where(w => eczaneNobetGruplar.Select(s => s.EczaneId).Contains(w.EczaneId)).ToList();

            var sonrakiDonemAyniGunNobetIstekGirilenler = _eczaneNobetIstekService.SonrakiAylardaAyniGunIstekGirilenEczaneler(eczaneNobetIsteklerSonrakiDonem);
            #endregion

            var nobetGrupKurallar = _nobetGrupKuralService.GetDetaylar(nobetGrupIdListe);

            var nobetUstGrupKisitlar = _nobetUstGrupKisitService.GetDetaylar(nobetUstGrupId);
            var grupBazliKisitlar = _nobetGrupGorevTipKisitService.GetDetaylar(nobetUstGrupId);

            var dataModel = new MersinMerkezDataModelV2()
            {
                Yil = yilBitis,
                Ay = ayBitis,
                LowerBound = 0,
                UpperBound = 1,
                TimeLimit = eczaneNobetDataModelParametre.TimeLimit,
                CalismaSayisiLimit = eczaneNobetDataModelParametre.CalismaSayisi,
                BaslangicTarihi = baslangicTarihi,
                BitisTarihi = bitisTarihi,
                NobetUstGrupBaslangicTarihi = nobetUstGrupBaslangicTarihi,
                NobetUstGrupId = nobetUstGrupId,
                EczaneNobetTarihAralik = eczaneNobetTarihAralik, //karar değişkeni
                EczaneNobetTarihAralikIkiliEczaneler = new List<EczaneNobetTarihAralikIkili>(), //eczaneNobetTarihAralikIkiliEczaneler,
                EczaneKumulatifHedefler = eczaneKumulatifHedefler,//.Where(w => w.EczaneId != 121).ToList(),
                EczaneNobetIstatistikler = eczaneNobetIstatistikler,
                EczaneNobetMazeretler = eczaneNobetMazeretler,
                EczaneGrupTanimlar = eczaneGrupTanimlar,
                TarihAraligi = tarihAralik,
                NobetGruplar = nobetGruplar,
                EczaneGruplar = eczaneGruplar2,
                ArasindaAyniGun2NobetFarkiOlanIkiliEczaneler = arasindaAyniGun2NobetFarkiOlanIkiliEczaneler,
                OncekiAylardaAyniGunNobetTutanEczaneler = oncekiAylardaAyniGunNobetTutanEczaneler,
                AltGruplarlaAyniGunNobetTutmayacakEczanelerYenisehir1_2 = altGruplarlaAyniGunNobetTutmayacakEczanelerYeniSehir1_2,
                AltGruplarlaAyniGunNobetTutmayacakEczanelerYenisehir3_2 = altGruplarlaAyniGunNobetTutmayacakEczanelerYeniSehir3_2,
                AltGruplarlaAyniGunNobetTutmayacakEczanelerToroslar = altGruplarlaAyniGunNobetTutmayacakEczanelerToroslar,

                EczaneNobetIstekler = eczaneNobetIstekler,
                NobetGrupGunKurallar = _nobetGrupGunKuralService.GetDetaylarAktifList(nobetGrupIdListe),
                NobetGrupKurallar = nobetGrupKurallar,
                NobetGrupGorevTipler = nobetGrupGorevTipler,
                NobetGrupTalepler = _nobetGrupTalepService.GetDetaylar(nobetGrupIdListe, baslangicTarihi, bitisTarihi),
                EczaneNobetGruplar = eczaneNobetGruplar,
                Kisitlar = nobetUstGrupKisitlar,
                EczaneGrupNobetSonuclar = eczaneGrupNobetSonuclar,
                //EczaneNobetSonuclarAltGruplarlaBirlikte = eczaneNobetSonuclarAltGruplaAyniGun,
                EczaneNobetSonuclar = eczaneNobetSonuclarCozulenGruplar,
                EczaneGrupNobetSonuclarTumu = eczaneNobetSonuclar,
                EczaneNobetGrupGunKuralIstatistikler = enSonNobetler,
                TakvimNobetGrupGunDegerIstatistikler = takvimNobetGrupGunDegerIstatistikler,
                EczaneNobetGrupGunKuralIstatistikYatay = eczaneNobetGrupGunKuralIstatistikYatay,
                EczaneNobetGrupGunKuralIstatistikYataySon3Ay = eczaneNobetGrupGunKuralIstatistikYataySon3Ay,
                EczaneNobetGrupAltGruplar = new List<EczaneNobetGrupAltGrupDetay>(), //eczaneNobetGrupAltGruplar,
                EczaneNobetAltGrupTarihAralik = eczaneNobetAltGrupTarihAralik,

                IkiliEczaneler = ikiliEczaneler,
                SonrakiDonemAyniGunNobetIstekGirilenler = sonrakiDonemAyniGunNobetIstekGirilenler,
                NobetGrupGorevTipKisitlar = grupBazliKisitlar,
                Kalibrasyonlar = _kalibrasyonService.GetKalibrasyonlarYatay(nobetUstGrupId),
                DebugYapilacakEczaneler = debugYapilacakEczaneler
            };

            //_eczaneNobetOrtakService.KurallariKontrolEtHaftaIciEnAzEnCok(nobetUstGrupId, eczaneNobetGrupGunKuralIstatistikYatay);
            _eczaneNobetOrtakService.KurallariKontrolEtMazeretIstek(nobetUstGrupId, eczaneNobetMazeretler, eczaneNobetIstekler);
            _eczaneNobetOrtakService.KurallariKontrolEtIstek(nobetUstGrupId, eczaneNobetIstekler, nobetGrupKurallar);

            return dataModel;
        }

        [LogAspect(typeof(DatabaseLogger))]
        [SecuredOperation(Roles = "Admin,Oda,Üst Grup")]
        public EczaneNobetSonucModel ModelCoz(EczaneNobetModelCoz eczaneNobetModelCoz)
        {
            //var nobetGruplarTumu = _nobetGrupService.GetListByNobetUstGrupId(eczaneNobetModelCoz.NobetUstGrupId);
            var nobetGruplar = eczaneNobetModelCoz.NobetGrupId.ToList();
            var nobetGruplarSirali = new List<NobetGruplarCozumSirali>();
            var sonuclar = new EczaneNobetSonucModel();

            var indis = 0;
            foreach (var nobetGrupId in nobetGruplar)
            {
                nobetGruplarSirali.Add(new NobetGruplarCozumSirali { NobetGrupId = nobetGrupId, SiraId = indis });
                indis++;
            }

            if (nobetGruplar.Count > 0)
            {
                #region başka gruplarla ilişkisi olan gruplar
                var eczaneGruplar = _eczaneGrupService.GetDetaylarAktifGruplar(eczaneNobetModelCoz.NobetUstGrupId)
                    .Where(d => nobetGruplar.Contains(d.NobetGrupId)).ToList();

                //Birbiri ile ilişkili grupların gruplanması
                var nobetGruplarBagDurumu = _eczaneGrupService.EsGrupluEczanelerinGruplariniBelirleTumu(eczaneGruplar, nobetGruplar);

                var tumNobetGruplar = from g in nobetGruplarSirali
                                      let e = nobetGruplarBagDurumu.SingleOrDefault(x => x.NobetGrupId == g.NobetGrupId) ?? new NobetBagGrup()
                                      orderby e.Id, g.SiraId
                                      select new NobetGrupBagGrup
                                      {
                                          BagId = e.Id,
                                          NobetGrupId = g.NobetGrupId
                                      };

                #endregion

                var nobetGrupBaglar = tumNobetGruplar.Select(s => s.BagId).Distinct().ToList();

                #region Karar kuralları
                var model = new EczaneNobetDataModelParametre
                {
                    AyBitis = eczaneNobetModelCoz.AyBitis,
                    NobetGorevTipId = eczaneNobetModelCoz.NobetGorevTipId,
                    NobetGrupGorevTipler = eczaneNobetModelCoz.NobetGrupGorevTipler,
                    NobetGrupId = eczaneNobetModelCoz.NobetGrupId,
                    NobetUstGrupId = eczaneNobetModelCoz.NobetUstGrupId,
                    NobetUstGrupBaslangicTarihi = eczaneNobetModelCoz.NobetUstGrupBaslangicTarihi,
                    BuAyVeSonrasi = eczaneNobetModelCoz.BuAyVeSonrasi,
                    BaslangicTarihi = eczaneNobetModelCoz.BaslangicTarihi,
                    BitisTarihi = eczaneNobetModelCoz.BitisTarihi,
                    TimeLimit = eczaneNobetModelCoz.TimeLimit,
                    CalismaSayisi = eczaneNobetModelCoz.CalismaSayisi
                };

                var aylar = _takvimService.GetAylar()
                    .Where(w => w.Id >= model.AyBitis)
                    .OrderBy(o => o.Id).ToList();

                var ayIndis = 0;

                if (eczaneNobetModelCoz.BuAyVeSonrasi && eczaneNobetModelCoz.CozumTercih == 0 && eczaneNobetModelCoz.SonrakiAylarRasgele)
                {//seçilen ay ve sonrası, gruplar öncelikli - sonraki aylarda gruplar rasgele sıralı
                    foreach (var ay in aylar)
                    {
                        if (eczaneNobetModelCoz.AyBitis == ay.Id)
                        {//ilk ay arayüzdeki sıra
                            foreach (var item in nobetGrupBaglar)
                            {
                                var nobetGrupIdListe = tumNobetGruplar
                                    .Where(x => x.BagId == item)
                                    .Select(s => s.NobetGrupId);

                                foreach (var nobetGrupId in nobetGrupIdListe)
                                {
                                    var tarihBas = model.BaslangicTarihi;
                                    var tarihBit = model.BitisTarihi;
                                    var aydakiGunSayisi = DateTime.DaysInMonth(tarihBit.Year, ay.Id);
                                    var baslangicGunu = 1;
                                    if (ayIndis == 1)
                                        baslangicGunu = tarihBas.Day;

                                    model.BaslangicTarihi = new DateTime(tarihBas.Year, ay.Id, baslangicGunu);
                                    model.BitisTarihi = new DateTime(tarihBit.Year, ay.Id, aydakiGunSayisi);
                                    model.NobetGrupId = new int[] { nobetGrupId };
                                    var data = EczaneNobetDataModel(model);
                                    //return EczaneNobetCozSonuclaraEkle(data);
                                    var sonuc = EczaneNobetCozSonuclaraEkle(data);

                                    sonuclar.ObjectiveValue += sonuc.ObjectiveValue;
                                    sonuclar.KararDegikeniSayisi += sonuc.KararDegikeniSayisi;
                                    sonuclar.KisitSayisi += sonuc.KisitSayisi;

                                    sonuclar.CozumSuresi += sonuc.CozumSuresi;

                                    sonuclar.CalismaSayisi += sonuc.CalismaSayisi;
                                    sonuclar.IterasyonSayisi += sonuc.IterasyonSayisi;
                                }

                                var sonuclarTumu = _eczaneNobetSonucService.GetSonuclarUstGrupBaslamaTarihindenSonra(eczaneNobetModelCoz.NobetUstGrupId);
                                var ayniGunNobetTutanEczaneler = _eczaneNobetOrtakService.GetAyniGunNobetTutanEczaneler(sonuclarTumu);
                                _ayniGunTutulanNobetService.AyniGunNobetTutanlariTabloyaEkle(ayniGunNobetTutanEczaneler);
                            }
                        }
                        else
                        {
                            foreach (var item in nobetGrupBaglar)
                            {
                                var r = new Random();
                                var nobetGrupIdListe = tumNobetGruplar
                                    .Where(x => x.BagId == item)
                                    .Select(s => s.NobetGrupId)
                                    .OrderBy(x => r.NextDouble());

                                foreach (var nobetGrupId in nobetGrupIdListe)
                                {
                                    var tarihBas = model.BaslangicTarihi;
                                    var tarihBit = model.BitisTarihi;
                                    var aydakiGunSayisi = DateTime.DaysInMonth(tarihBit.Year, ay.Id);
                                    var baslangicGunu = 1;
                                    if (ayIndis == 1)
                                        baslangicGunu = tarihBas.Day;

                                    model.BaslangicTarihi = new DateTime(tarihBas.Year, ay.Id, baslangicGunu);
                                    model.BitisTarihi = new DateTime(tarihBit.Year, ay.Id, aydakiGunSayisi);
                                    model.NobetGrupId = new int[] { nobetGrupId };
                                    var data = EczaneNobetDataModel(model);
                                    var sonuc = EczaneNobetCozSonuclaraEkle(data);

                                    sonuclar.ObjectiveValue += sonuc.ObjectiveValue;
                                    sonuclar.KararDegikeniSayisi += sonuc.KararDegikeniSayisi;
                                    sonuclar.KisitSayisi += sonuc.KisitSayisi;

                                    sonuclar.CozumSuresi += sonuc.CozumSuresi;

                                    sonuclar.CalismaSayisi += sonuc.CalismaSayisi;
                                    sonuclar.IterasyonSayisi += sonuc.IterasyonSayisi;
                                }
                            }
                            var sonuclarTumu = _eczaneNobetSonucService.GetSonuclarUstGrupBaslamaTarihindenSonra(eczaneNobetModelCoz.NobetUstGrupId);
                            var ayniGunNobetTutanEczaneler = _eczaneNobetOrtakService.GetAyniGunNobetTutanEczaneler(sonuclarTumu);
                            _ayniGunTutulanNobetService.AyniGunNobetTutanlariTabloyaEkle(ayniGunNobetTutanEczaneler);
                        }
                    }
                    return sonuclar;
                }
                else if (eczaneNobetModelCoz.BuAyVeSonrasi && eczaneNobetModelCoz.CozumTercih == 0)
                {//seçilen ay ve sonrası, gruplar öncelikli
                    foreach (var ay in aylar)
                    {
                        ayIndis++;
                        if (tumNobetGruplar.Count() == 1)
                        {
                            model.AyBitis = ay.Id;
                            var data = EczaneNobetDataModel(model);
                            return EczaneNobetCozSonuclaraEkle(data);
                        }
                        else
                        {
                            foreach (var item in nobetGrupBaglar)
                            {
                                var nobetGrupIdListe = tumNobetGruplar
                                    .Where(x => x.BagId == item)
                                    .Select(s => s.NobetGrupId);

                                foreach (var nobetGrupId in nobetGrupIdListe)
                                {
                                    var tarihBas = model.BaslangicTarihi;
                                    var tarihBit = model.BitisTarihi;
                                    var aydakiGunSayisi = DateTime.DaysInMonth(tarihBit.Year, ay.Id);
                                    var baslangicGunu = 1;
                                    if (ayIndis == 1)
                                        baslangicGunu = tarihBas.Day;

                                    model.BaslangicTarihi = new DateTime(tarihBas.Year, ay.Id, baslangicGunu);
                                    model.BitisTarihi = new DateTime(tarihBit.Year, ay.Id, aydakiGunSayisi);
                                    model.NobetGrupId = new int[] { nobetGrupId };
                                    var data = EczaneNobetDataModel(model);
                                    var sonuc = EczaneNobetCozSonuclaraEkle(data);

                                    sonuclar.ObjectiveValue += sonuc.ObjectiveValue;
                                    sonuclar.KararDegikeniSayisi += sonuc.KararDegikeniSayisi;
                                    sonuclar.KisitSayisi += sonuc.KisitSayisi;

                                    sonuclar.CozumSuresi += sonuc.CozumSuresi;

                                    sonuclar.CalismaSayisi += sonuc.CalismaSayisi;
                                    sonuclar.IterasyonSayisi += sonuc.IterasyonSayisi;
                                }
                            }
                        }
                        var sonuclarTumu = _eczaneNobetSonucService.GetSonuclarUstGrupBaslamaTarihindenSonra(eczaneNobetModelCoz.NobetUstGrupId);
                        var ayniGunNobetTutanEczaneler = _eczaneNobetOrtakService.GetAyniGunNobetTutanEczaneler(sonuclarTumu);
                        _ayniGunTutulanNobetService.AyniGunNobetTutanlariTabloyaEkle(ayniGunNobetTutanEczaneler);
                    }
                    return sonuclar;
                }
                else if (eczaneNobetModelCoz.BuAyVeSonrasi && eczaneNobetModelCoz.CozumTercih == 1)
                {//seçilen ay ve sonrası, gruplar önceliksiz
                    foreach (var ay in aylar)
                    {
                        ayIndis++;
                        foreach (var item in nobetGrupBaglar)
                        {
                            var nobetGrupIdListe = tumNobetGruplar
                                .Where(x => x.BagId == item)
                                .Select(s => s.NobetGrupId)
                                .ToArray();

                            var tarihBas = model.BaslangicTarihi;
                            var tarihBit = model.BitisTarihi;
                            var aydakiGunSayisi = DateTime.DaysInMonth(tarihBit.Year, ay.Id);
                            var baslangicGunu = 1;
                            if (ayIndis == 1)
                                baslangicGunu = tarihBas.Day;

                            model.BaslangicTarihi = new DateTime(tarihBas.Year, ay.Id, baslangicGunu);
                            model.BitisTarihi = new DateTime(tarihBit.Year, ay.Id, aydakiGunSayisi);
                            model.NobetGrupId = nobetGrupIdListe;
                            var data = EczaneNobetDataModel(model);
                            var sonuc = EczaneNobetCozSonuclaraEkle(data);

                            sonuclar.ObjectiveValue += sonuc.ObjectiveValue;
                            sonuclar.KararDegikeniSayisi += sonuc.KararDegikeniSayisi;
                            sonuclar.KisitSayisi += sonuc.KisitSayisi;

                            sonuclar.CozumSuresi += sonuc.CozumSuresi;

                            sonuclar.CalismaSayisi += sonuc.CalismaSayisi;
                            sonuclar.IterasyonSayisi += sonuc.IterasyonSayisi;
                        }
                    }
                    var sonuclarTumu = _eczaneNobetSonucService.GetSonuclarUstGrupBaslamaTarihindenSonra(eczaneNobetModelCoz.NobetUstGrupId);
                    var ayniGunNobetTutanEczaneler = _eczaneNobetOrtakService.GetAyniGunNobetTutanEczaneler(sonuclarTumu);
                    _ayniGunTutulanNobetService.AyniGunNobetTutanlariTabloyaEkle(ayniGunNobetTutanEczaneler);
                    return sonuclar;
                }
                else if (eczaneNobetModelCoz.CozumTercih == 0)
                {//ay, gruplar öncelikli
                    if (tumNobetGruplar.Count() == 1)
                    {
                        var data = EczaneNobetDataModel(model);
                        return EczaneNobetCozAktifiGuncelle(data);
                    }
                    else
                    {
                        foreach (var item in nobetGrupBaglar)
                        {
                            var nobetGrupIdListe = tumNobetGruplar
                                .Where(x => x.BagId == item)
                                .Select(s => s.NobetGrupId)
                                .ToArray();

                            foreach (var nobetGrupId in nobetGrupIdListe)
                            {
                                model.NobetGrupId = new int[] { nobetGrupId };
                                var data = EczaneNobetDataModel(model);
                                var sonuc = EczaneNobetCozSonuclaraEkle(data);

                                sonuclar.ObjectiveValue += sonuc.ObjectiveValue;
                                sonuclar.KararDegikeniSayisi += sonuc.KararDegikeniSayisi;
                                sonuclar.KisitSayisi += sonuc.KisitSayisi;

                                sonuclar.CozumSuresi += sonuc.CozumSuresi;

                                sonuclar.CalismaSayisi += sonuc.CalismaSayisi;
                                sonuclar.IterasyonSayisi += sonuc.IterasyonSayisi;
                            }
                        }
                        var sonuclarTumu = _eczaneNobetSonucService.GetSonuclarUstGrupBaslamaTarihindenSonra(eczaneNobetModelCoz.NobetUstGrupId);
                        var ayniGunNobetTutanEczaneler = _eczaneNobetOrtakService.GetAyniGunNobetTutanEczaneler(sonuclarTumu);
                        _ayniGunTutulanNobetService.AyniGunNobetTutanlariTabloyaEkle(ayniGunNobetTutanEczaneler);
                        return sonuclar;
                    }
                }
                else if (eczaneNobetModelCoz.CozumTercih == 1)
                {//ay, gruplar önceliksiz
                    foreach (var item in nobetGrupBaglar)
                    {
                        var nobetGrupIdListe = tumNobetGruplar
                            .Where(x => x.BagId == item)
                            .Select(s => s.NobetGrupId)
                            .ToArray();

                        model.NobetGrupId = nobetGrupIdListe;
                        var data = EczaneNobetDataModel(model);
                        //return EczaneNobetCozAktifiGuncelle(data);
                        sonuclar = EczaneNobetCozAktifiGuncelle(data);
                    }
                    //var sonuclarTumu = _eczaneNobetSonucService.GetSonuclarUstGrupBaslamaTarihindenSonra(eczaneNobetModelCoz.NobetUstGrupId);
                    //var sonuclarTumu = _eczaneNobetSonucAktifService.GetSonuclar2(eczaneNobetModelCoz.NobetUstGrupId);
                    //var ayniGunNobetTutanEczaneler = _eczaneNobetOrtakService.GetAyniGunNobetTutanEczaneler(sonuclarTumu);
                    //var ayniGunNobetSayisiGrouped = _eczaneNobetOrtakService.AyniGunTutulanNobetSayisiniHesapla(ayniGunNobetTutanEczaneler);
                    //_ayniGunTutulanNobetService.AyniGunNobetSayisiniGuncelle(ayniGunNobetSayisiGrouped, azaltilsinMi: false);
                    return sonuclar;
                }
                else
                {//diğer
                    throw new Exception("Nöbet yazdırma kriter seçimi hatalıdır. Lütfen kontrol ediniz.");
                }
                #endregion
            }
            return sonuclar;
        }

        public void ModeliKapat()
        {
            _eczaneNobetMersinMerkezOptimizationV2.ModeliKapat();
        }

    }
}
