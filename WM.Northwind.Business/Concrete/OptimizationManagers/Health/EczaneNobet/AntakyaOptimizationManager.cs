using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using WM.Core.Aspects.PostSharp.AutorizationAspects;
using WM.Core.Aspects.PostSharp.LogAspects;
using WM.Core.CrossCuttingConcerns.Logging.Log4Net.Logger;
using WM.Northwind.Business.Abstract.EczaneNobet;
using WM.Northwind.Business.Abstract.Optimization.EczaneNobet;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.Enums;
using WM.Northwind.Entities.Concrete.Optimization.EczaneNobet;
using WM.Optimization.Abstract.Health;

namespace WM.Northwind.Business.Concrete.OptimizationManagers.Health.EczaneNobet
{
    public class AntakyaOptimizationManager : IAntakyaOptimizationService
    {
        #region ctor
        private IEczaneGrupService _eczaneGrupService;
        private IEczaneGrupTanimService _eczaneGrupTanimService;
        private IEczaneNobetAntakyaOptimization _eczaneNobetAntakyaOptimization;
        private IEczaneNobetGrupService _eczaneNobetGrupService;
        private IEczaneNobetGrupAltGrupService _eczaneNobetGrupAltGrupService;
        private IEczaneNobetIstekService _eczaneNobetIstekService;
        private IEczaneNobetMazeretService _eczaneNobetMazeretService;
        private IEczaneNobetOrtakService _eczaneNobetOrtakService;
        private IEczaneNobetSonucAktifService _eczaneNobetSonucAktifService;
        private IEczaneNobetSonucService _eczaneNobetSonucService;
        private IEczaneNobetSonucPlanlananService _eczaneNobetSonucPlanlananService;
        private INobetGrupGorevTipService _nobetGrupGorevTipService;
        private INobetGrupGunKuralService _nobetGrupGunKuralService;
        private INobetGrupKuralService _nobetGrupKuralService;
        private INobetGrupService _nobetGrupService;
        private INobetGrupTalepService _nobetGrupTalepService;
        private INobetUstGrupKisitService _nobetUstGrupKisitService;
        private ITakvimService _takvimService;
        private IEczaneNobetMuafiyetService _eczaneNobetMuafiyetService;
        private INobetUstGrupGunGrupService _nobetUstGrupGunGrupService;
        private INobetUstGrupService _nobetUstGrupService;
        private INobetGrupGorevTipKisitService _nobetGrupGorevTipKisitService;
        private INobetGrupGorevTipGunKuralService _nobetGrupGorevTipGunKuralService;
        private INobetGrupGorevTipTakvimOzelGunService _nobetGrupGorevTipTakvimOzelGunService;
        private IKalibrasyonService _kalibrasyonService;
        private IDebugEczaneService _debugEczaneService;
        private INobetAltGrupService _nobetAltGrupService;

        public AntakyaOptimizationManager(
                    IEczaneGrupService eczaneGrupService,
                    IEczaneGrupTanimService eczaneGrupTanimService,
                    IEczaneNobetAntakyaOptimization eczaneNobetAntakyaOptimization,
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
                    IEczaneNobetSonucPlanlananService eczaneNobetSonucPlanlananService,
                    INobetUstGrupGunGrupService nobetUstGrupGunGrupService,
                    INobetUstGrupService nobetUstGrupService,
                    INobetGrupGorevTipKisitService nobetGrupGorevTipKisitService,
                    INobetGrupGorevTipGunKuralService nobetGrupGorevTipGunKuralService,
                    INobetGrupGorevTipTakvimOzelGunService nobetGrupGorevTipTakvimOzelGunService,
                    IKalibrasyonService kalibrasyonService,
                    IDebugEczaneService debugEczaneService,
                    INobetAltGrupService nobetAltGrupService
            )
        {
            _eczaneGrupService = eczaneGrupService;
            _eczaneGrupTanimService = eczaneGrupTanimService;
            _eczaneNobetAntakyaOptimization = eczaneNobetAntakyaOptimization;
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
            _eczaneNobetSonucPlanlananService = eczaneNobetSonucPlanlananService;
            _nobetUstGrupGunGrupService = nobetUstGrupGunGrupService;
            _nobetUstGrupService = nobetUstGrupService;
            _nobetGrupGorevTipKisitService = nobetGrupGorevTipKisitService;
            _nobetGrupGorevTipGunKuralService = nobetGrupGorevTipGunKuralService;
            _nobetGrupGorevTipTakvimOzelGunService = nobetGrupGorevTipTakvimOzelGunService;
            _kalibrasyonService = kalibrasyonService;
            _debugEczaneService = debugEczaneService;
            _nobetAltGrupService = nobetAltGrupService;
        }
        #endregion

        /// <summary>
        /// Her çözüm sonrasında aktif sonuçlardaki mevcut kayıtlar silinip yerine yeni sonuçlar eklenir.
        /// </summary>
        /// <param name="data"></param>
        //[TransactionScopeAspect]
        public EczaneNobetSonucModel EczaneNobetCozAktifiGuncelle(AntakyaDataModel data)
        {
            var mevcutSonuclar = _eczaneNobetSonucAktifService.GetDetaylar2(data.NobetUstGrupId);
            var guncellenecekSonuclar = mevcutSonuclar
                .Where(x => data.NobetGruplar.Select(s => s.Id).Contains(x.NobetGrupId))
                .Select(s => s.Id).ToArray();

            var yeniSonuclar = _eczaneNobetAntakyaOptimization.Solve(data);

            //gelen datadaki nöbet grup id aktif sonuçlarda varsa o nöbet gruba ait önceki sonuçları sil
            _eczaneNobetSonucAktifService.CokluSil(guncellenecekSonuclar);

            AktiftekiArtiklariSil(data.NobetUstGrupId);

            //yeni sonuçları ekle
            _eczaneNobetSonucAktifService.CokluEkle(yeniSonuclar.ResultModel);

            return yeniSonuclar;
        }

        public EczaneNobetSonucModel EczaneNobetCozSonuclaraEkle(AntakyaDataModel data)
        {
            var yeniSonuclar = _eczaneNobetAntakyaOptimization.Solve(data);
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

        private AntakyaDataModel EczaneNobetDataModel(EczaneNobetDataModelParametre eczaneNobetDataModelParametre)
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            #region parametreler
            var nobetUstGrupId = eczaneNobetDataModelParametre.NobetUstGrupId;
            var nobetGrupIdListe = eczaneNobetDataModelParametre.NobetGrupId.ToList();
            var nobetUstGrupBaslangicTarihi = eczaneNobetDataModelParametre.NobetUstGrupBaslangicTarihi;
            var baslangicTarihi = eczaneNobetDataModelParametre.BaslangicTarihi;
            var bitisTarihi = eczaneNobetDataModelParametre.BitisTarihi;
            var nobetGrupGorevTipler = eczaneNobetDataModelParametre.NobetGrupGorevTipler;
            var nobetGorevTipId = nobetGrupGorevTipler.FirstOrDefault().NobetGorevTipId;
            #endregion

            var nobetUstGrup = _nobetUstGrupService.GetDetay(nobetUstGrupId);

            if (baslangicTarihi < nobetUstGrupBaslangicTarihi)
                throw new Exception($"Nöbet başlangıç tarihi <strong>({baslangicTarihi.ToShortDateString()})</strong> üst grup başlama tarihinden <strong>({nobetUstGrupBaslangicTarihi.ToShortDateString()})</strong> küçük olamaz.");

            var debugYapilacakEczaneler = _debugEczaneService.GetDetaylarAktifOlanlar(nobetUstGrupId);

            var nobetGruplar = _nobetGrupService.GetDetaylar(nobetGrupIdListe).OrderBy(s => s.Id).ToList();

            var eczaneNobetSonuclar = _eczaneNobetSonucService.GetSonuclar(nobetUstGrupId);

            //var nobetGrupGorevTipGunKurallar = _nobetGrupGorevTipGunKuralService.GetDetaylarByNobetGrupGorevTipIdList(nobetGrupGorevTipler.Select(s => s.Id).ToList());

            //var eczaneNobetSonucDetaylar = _eczaneNobetSonucService.GetDetaylarByNobetGrupGorevTipIdList(baslangicTarihi, bitisTarihi, nobetGrupGorevTipler.Select(s => s.Id).ToArray());

            //var nobetGrupGorevTipTakvimOzelGunler = _nobetGrupGorevTipTakvimOzelGunService.GetDetaylar(baslangicTarihi, bitisTarihi, nobetGrupGorevTipler);

            //var sonuclarTumu = _eczaneNobetOrtakService.EczaneNobetSonucBirlesim(nobetGrupGorevTipGunKurallar, eczaneNobetSonucDetaylar, nobetGrupGorevTipTakvimOzelGunler, EczaneNobetSonucTuru.Kesin);

            var eczaneNobetSonuclarPlanlanan = _eczaneNobetSonucPlanlananService.GetSonuclar(nobetUstGrupId);
            //var planlananNobetler = _eczaneNobetSonucPlanlananService.SiraliNobetYaz(nobetUstGrupId);

            var nobetUstGrupGunGruplar = _nobetUstGrupGunGrupService.GetDetaylar(nobetUstGrupId);

            var eczaneNobetMazeretNobettenDusenler = new List<EczaneNobetMazeretSayilari>();

            var mazeret = _nobetUstGrupKisitService.GetKisitPasifMi("mazeret", nobetUstGrupId);

            if (mazeret)
                eczaneNobetMazeretNobettenDusenler = _eczaneNobetMazeretService.GetEczaneNobetMazeretSayilari(baslangicTarihi, bitisTarihi, nobetGrupIdListe);

            var eczaneNobetGruplar = _eczaneNobetGrupService.GetDetaylar(nobetGrupIdListe, baslangicTarihi, bitisTarihi)
                .Where(w => !eczaneNobetMazeretNobettenDusenler.Select(s => s.EczaneNobetGrupId).Contains(w.Id)).ToList();
            var sure_eczaneNobetGruplar = stopwatch.Elapsed;

            #region planlanan nöbetler - sıralı nöbet yazma (gün grubu bazında)
            //baslangicTarihi = new DateTime(2018, 6, 1);
            //baslangicTarihi = new DateTime(2019, 3, 13);
            //bitisTarihi = new DateTime(2020, 12, 31);

            //var eczaneNobetGruplarHepsi = _eczaneNobetGrupService.GetDetaylar(nobetGrupIdListe);//, baslangicTarihi, bitisTarihi);

            //_takvimService.SiraliNobetYaz(nobetGrupGorevTipler, eczaneNobetGruplarHepsi, baslangicTarihi, bitisTarihi, nobetUstGrupId);
            //var besinciBolge = nobetGrupGorevTipler.SingleOrDefault(x => x.Id == 8);

            //_takvimService.SiraliNobetYazGrupBazinda(besinciBolge, eczaneNobetGruplarHepsi, baslangicTarihi, bitisTarihi);
            #endregion

            var eczaneNobetSonuclarCozulenGruplar = eczaneNobetSonuclar
                .Where(w => eczaneNobetGruplar.Select(s => s.Id).Contains(w.EczaneNobetGrupId)).ToList();

            var eczaneNobetSonuclarCozulenGruplarPlanlanan = eczaneNobetSonuclarPlanlanan
                .Where(w => eczaneNobetGruplar.Select(s => s.Id).Contains(w.EczaneNobetGrupId)).ToList();

            var eczaneNobetSonuclarCozulenGruplarPlanlananBaslangicSonrasi = eczaneNobetSonuclarCozulenGruplarPlanlanan
                .Where(w => w.Tarih >= nobetUstGrupBaslangicTarihi).ToList();

            var sure_eczaneNobetSonuclarCozulenGrup = stopwatch.Elapsed;

            var eczaneNobetSonuclarOncekiAylar = eczaneNobetSonuclarCozulenGruplar
                .Where(w => w.Tarih >= w.NobetGrupGorevTipBaslamaTarihi).ToList();

            var sonuclarKontrol = _eczaneNobetSonucService.GetSonuclar(baslangicTarihi, bitisTarihi, eczaneNobetSonuclarCozulenGruplar);

            if (sonuclarKontrol.Count > 0)
                throw new Exception("Kriterlere uygun <strong>daha önce yazılmış nöbetler</strong> bulunmaktadır. Lütfen kontrol ediniz!");

            //var anahtarListe = eczaneNobetSonuclarCozulenGruplar
            //    .Where(w => w.Tarih < nobetUstGrupBaslangicTarihi).ToList();

            var enSonNobetler = _eczaneNobetOrtakService.GetEczaneNobetGrupGunKuralIstatistik(eczaneNobetGruplar, eczaneNobetSonuclarCozulenGruplar);
            //var enSonNobetlerPlanlanan = _eczaneNobetOrtakService.GetEczaneNobetGrupGunKuralIstatistik(eczaneNobetGruplar, eczaneNobetSonuclarCozulenGruplarPlanlanan);
            var sure_enSonNobetler = stopwatch.Elapsed;

            var eczaneNobetGrupGunKuralIstatistikYatay = _eczaneNobetOrtakService.GetEczaneNobetGrupGunKuralIstatistikYatay(enSonNobetler);
            //var eczaneNobetGrupGunKuralIstatistikYatayPlanlanan = _eczaneNobetOrtakService.GetEczaneNobetGrupGunKuralIstatistikYatay(enSonNobetlerPlanlanan);

            //var eczaneNobetGrupGunKuralIstatistikYataySon = _eczaneNobetOrtakService.GetEczaneNobetGrupGunKuralIstatistikYatay(enSonNobetler, enSonNobetlerPlanlanan);

            //var sure_eczaneNobetGrupGunKuralIstatistikYatay = stopwatch.Elapsed;

            //var sure_anahtarListe1 = stopwatch.Elapsed;

            var nobetBorcAlacakVerecek = _takvimService.EczaneNobetAlacakVerecekHesaplaAntalya(nobetGrupGorevTipler, eczaneNobetSonuclarPlanlanan, eczaneNobetGruplar, eczaneNobetGrupGunKuralIstatistikYatay);

            var grupluEczaneNobetSonuclar = _eczaneNobetSonucService.GetSonuclar(baslangicTarihi, bitisTarihi, eczaneNobetSonuclar);

            var eczaneGrupEdges = _eczaneGrupService.GetEdges(nobetUstGrupId)
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

            //var sure_eczaneGruplar2 = stopwatch.Elapsed;

            var eczaneGrupNobetSonuclar = grupluEczaneNobetSonuclar
                .Where(w => eczaneGruplar2.Select(s => s.EczaneId).Contains(w.EczaneId)).ToList();

            //var sure_eczaneGrupNobetSonuclar = stopwatch.Elapsed;

            //nöbet yazılacak tarih aralığı(örn. Ocak ayının tüm günleri)
            var tarihAralik = _takvimService.GetTakvimNobetGruplar(baslangicTarihi, bitisTarihi, nobetGrupGorevTipler);
            var nobetGrupKurallar = _nobetGrupKuralService.GetDetaylar(nobetGrupIdListe);

            //var gunGrupIstatistik = _eczaneNobetOrtakService.GetEczaneNobetGrupGunGrupIstatistik(enSonNobetler)
            //    .Where(w => new int[] { 1, 3 }.Contains(w.GunGrupId)).ToList();

            //var GetNobetTutamayacaklariGunAraligi = _takvimService.GetNobetTutamayacaklariGunAraligi(gunGrupIstatistik, eczaneNobetGruplar, nobetGrupKurallar);

            //var sure_tarihAralik = stopwatch.Elapsed;

            var eczaneNobetTarihAralik1 = _takvimService.GetEczaneNobetTarihAralik(baslangicTarihi, bitisTarihi, nobetGrupGorevTipler)
                .Where(w => eczaneNobetGruplar.Select(s => s.EczaneId).Contains(w.EczaneId)).ToList();

            var eczaneNobetTarihAralik = _eczaneNobetOrtakService.AmacFonksiyonuKatsayisiBelirle(eczaneNobetTarihAralik1, eczaneNobetGrupGunKuralIstatistikYatay, null, nobetBorcAlacakVerecek);

            //var sure_eczaneNobetTarihAralik = stopwatch.Elapsed;

            var eczaneNobetIstekler = _eczaneNobetIstekService.GetDetaylarByNobetGrupIdList(baslangicTarihi, bitisTarihi, nobetGrupIdListe)
                .Where(w => eczaneNobetGruplar.Select(s => s.EczaneId).Contains(w.EczaneId)).ToList();

            var eczaneNobetMazeretler = _eczaneNobetMazeretService.GetDetaylarByEczaneIdList(baslangicTarihi, bitisTarihi, eczaneNobetGruplar.Select(s => s.EczaneId).Distinct().ToList())
                  .Where(w => w.MazeretId != 3)
                  .ToList();

            var takvimNobetGrupGunDegerIstatistikler = _takvimService.GetTakvimNobetGrupGunDegerIstatistikler(nobetUstGrupBaslangicTarihi, bitisTarihi, nobetGrupGorevTipler);

            //var sure_takvimNobetGrupGunDegerIstatistikler = stopwatch.Elapsed;
            
            #region önceki aylar aynı gün nöbet tutanlar çözülen ayda aynı gün nöbetçi olmasın

            var oncekiAylardaAyniGunNobetTutanEczaneler = new List<EczaneGrupDetay>();

            var oncekiAylarAyniGunNobet = _nobetUstGrupKisitService.GetDetay("oncekiAylarAyniGunNobet", nobetUstGrupId);

            if (!oncekiAylarAyniGunNobet.PasifMi)
                oncekiAylardaAyniGunNobetTutanEczaneler = _eczaneNobetSonucService.OncekiAylardaAyniGunNobetTutanlar(baslangicTarihi, eczaneNobetSonuclarOncekiAylar, 0, (int)oncekiAylarAyniGunNobet.SagTarafDegeri);

            #endregion

            var eczaneNobetGrupAltGruplar = _eczaneNobetGrupAltGrupService.GetDetaylar(nobetUstGrupId);

            //var sure_eczaneNobetGrupAltGruplar = stopwatch.Elapsed;

            var nobetUstGrupKisitlar = _nobetUstGrupKisitService.GetDetaylar(nobetUstGrupId);
            var grupBazliKisitlar = _nobetGrupGorevTipKisitService.GetDetaylar(nobetUstGrupId);
            var nobetAltGruplar = _nobetAltGrupService.GetDetaylar(nobetUstGrupId);

            var dataModel = new AntakyaDataModel()
            {
                Yil = eczaneNobetDataModelParametre.YilBitis,
                Ay = eczaneNobetDataModelParametre.AyBitis,
                TimeLimit = eczaneNobetDataModelParametre.TimeLimit,
                CalismaSayisiLimit = eczaneNobetDataModelParametre.CalismaSayisi,
                LowerBound = 0,
                UpperBound = 1,
                BaslangicTarihi = baslangicTarihi,
                BitisTarihi = bitisTarihi,
                NobetUstGrupBaslangicTarihi = nobetUstGrupBaslangicTarihi,
                NobetUstGrupId = nobetUstGrupId,
                EczaneNobetTarihAralik = eczaneNobetTarihAralik, //karar değişkeni
                EczaneNobetMazeretler = eczaneNobetMazeretler,
                TarihAraligi = tarihAralik,
                NobetGruplar = nobetGruplar,
                EczaneGruplar = eczaneGruplar2,
                //AltGruplarlaAyniGunNobetTutmayacakEczaneler = altGruplarlaAyniGunNobetTutmayacakEczaneler,
                EczaneNobetIstekler = eczaneNobetIstekler,
                NobetGrupGunKurallar = _nobetGrupGunKuralService.GetDetaylarAktifList(nobetGrupIdListe),
                NobetGrupKurallar = nobetGrupKurallar,
                NobetGrupGorevTipler = nobetGrupGorevTipler,
                NobetGrupTalepler = _nobetGrupTalepService.GetDetaylar(nobetGrupIdListe, baslangicTarihi, bitisTarihi),
                EczaneNobetGruplar = eczaneNobetGruplar,
                Kisitlar = nobetUstGrupKisitlar,
                OncekiAylardaAyniGunNobetTutanEczaneler = oncekiAylardaAyniGunNobetTutanEczaneler,
                EczaneGrupNobetSonuclar = eczaneGrupNobetSonuclar,
                //EczaneNobetSonuclarAltGruplarlaBirlikte = eczaneNobetSonuclarAltGruplaAyniGun,
                EczaneNobetSonuclar = eczaneNobetSonuclarCozulenGruplar,
                EczaneGrupNobetSonuclarTumu = eczaneNobetSonuclar,
                EczaneNobetGrupGunKuralIstatistikler = enSonNobetler,
                TakvimNobetGrupGunDegerIstatistikler = takvimNobetGrupGunDegerIstatistikler,
                EczaneNobetGrupGunKuralIstatistikYatay = eczaneNobetGrupGunKuralIstatistikYatay,
                EczaneNobetGrupAltGruplar = eczaneNobetGrupAltGruplar,
                NobetGrupGorevTipKisitlar = grupBazliKisitlar,
                Kalibrasyonlar = _kalibrasyonService.GetKalibrasyonlarYatay(nobetUstGrupId),
                NobetAltGruplar = nobetAltGruplar,
                DebugYapilacakEczaneler = debugYapilacakEczaneler
            };

            var sure_toplam = stopwatch.Elapsed;

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
                    NobetGrupGorevTipler = eczaneNobetModelCoz.NobetGrupGorevTipler,
                    NobetGorevTipId = eczaneNobetModelCoz.NobetGorevTipId,
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

        [LogAspect(typeof(DatabaseLogger))]
        [SecuredOperation(Roles = "Admin,Oda,Üst Grup")]
        public EczaneNobetSonucModel ModelCozE(EczaneNobetModelCoz eczaneNobetModelCoz)
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
                                      from e in nobetGruplarBagDurumu
                                      where g.NobetGrupId == e.NobetGrupId
                                      orderby e.Id, g.SiraId
                                      select new NobetGrupBagGrup
                                      {
                                          BagId = e.Id,
                                          NobetGrupId = e.NobetGrupId
                                      };

                #endregion

                var nobetGrupBaglar = tumNobetGruplar.Select(s => s.BagId).Distinct().ToList();

                #region Karar kuralları
                var model = new EczaneNobetDataModelParametre
                {
                    //AyBaslangic = eczaneNobetModelCoz.AyBaslangic,
                    AyBitis = eczaneNobetModelCoz.AyBitis,
                    NobetGorevTipId = eczaneNobetModelCoz.NobetGorevTipId,
                    NobetGrupId = eczaneNobetModelCoz.NobetGrupId,
                    NobetUstGrupId = eczaneNobetModelCoz.NobetUstGrupId,
                    //YilBaslangic = eczaneNobetModelCoz.YilBaslangic,
                    //YilBitis = eczaneNobetModelCoz.YilBitis,
                    NobetUstGrupBaslangicTarihi = eczaneNobetModelCoz.NobetUstGrupBaslangicTarihi,
                    //AyBitisBaslangicGunu = eczaneNobetModelCoz.AyBitisBaslangicGunu,
                    //AyBitisBitisGunu = eczaneNobetModelCoz.AyBitisBitisGunu,
                    BuAyVeSonrasi = eczaneNobetModelCoz.BuAyVeSonrasi,
                    BaslangicTarihi = eczaneNobetModelCoz.BaslangicTarihi,
                    BitisTarihi = eczaneNobetModelCoz.BitisTarihi
                };

                var aylar = _takvimService.GetAylar()
                    .Where(w => w.Id >= model.AyBitis)
                    .OrderBy(o => o.Id).ToList();

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
                                    model.AyBitis = ay.Id;
                                    model.NobetGrupId = new int[] { nobetGrupId };
                                    var data = EczaneNobetDataModel(model);
                                    return EczaneNobetCozSonuclaraEkle(data);
                                }
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
                                    model.AyBitis = ay.Id;
                                    model.NobetGrupId = new int[] { nobetGrupId };
                                    var data = EczaneNobetDataModel(model);
                                    return EczaneNobetCozSonuclaraEkle(data);
                                }
                            }
                        }
                    }
                }
                else if (eczaneNobetModelCoz.BuAyVeSonrasi && eczaneNobetModelCoz.CozumTercih == 0)
                {//seçilen ay ve sonrası, gruplar öncelikli
                    foreach (var ay in aylar)
                    {
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
                                    model.AyBitis = ay.Id;
                                    model.NobetGrupId = new int[] { nobetGrupId };
                                    var data = EczaneNobetDataModel(model);
                                    return EczaneNobetCozSonuclaraEkle(data);
                                }
                            }
                        }
                    }
                }
                else if (eczaneNobetModelCoz.BuAyVeSonrasi && eczaneNobetModelCoz.CozumTercih == 1)
                {//seçilen ay ve sonrası, gruplar önceliksiz
                    foreach (var ay in aylar)
                    {
                        foreach (var item in nobetGrupBaglar)
                        {
                            var nobetGrupIdListe = tumNobetGruplar
                                .Where(x => x.BagId == item)
                                .Select(s => s.NobetGrupId)
                                .ToArray();

                            model.AyBitis = ay.Id;
                            model.NobetGrupId = nobetGrupIdListe;
                            var data = EczaneNobetDataModel(model);
                            return EczaneNobetCozSonuclaraEkle(data);
                        }
                    }
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
                                sonuclar = EczaneNobetCozSonuclaraEkle(data);
                            }
                            return sonuclar;
                        }
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
                        return EczaneNobetCozAktifiGuncelle(data);
                    }
                }
                else
                {//diğer
                    throw new Exception("Nöbet yazdırma kriter seçimi hatalıdır. Lütfen kontrol ediniz.");
                }
                #endregion
            }
            return sonuclar;
        }
    }
}

#region başka gruplarla ilişkisi olmayan gruplar

//başka gruplarla ilişkisi olan gruplar
//var esGruplar = eczaneGruplar.Select(x => x.NobetGrupId).Distinct().OrderBy(x => x).ToList();

//var tekliNobetGruplar = _nobetGrupService.GetList(nobetGruplar)
//    .Where(x => !esGruplar.Contains(x.Id)).ToList();

//var tekliNobetGruplar = tekEczaneNobetGruplar
//    //.Where(d => nobetGruplar.Contains(d.NobetGrupId))
//    .Select(c => c.NobetGrupId).Distinct().ToList();
#endregion

#region tüm nöbet gruplar
//var tumNobetGruplar = new List<NobetBagGrup>();

//foreach (var esliNobetGrup in esliNobetGruplar.Distinct())
//{
//    tumNobetGruplar.Add(new NobetBagGrup
//    {
//        Id = esliNobetGrup.Id,
//        NobetGrupId = esliNobetGrup.NobetGrupId
//    });
//}

//var indis = tumNobetGruplar.Select(s => s.Id).LastOrDefault();
//foreach (var tekliNobetGrup in tekliNobetGruplar)
//{
//    indis++;
//    tumNobetGruplar.Add(new NobetBagGrup
//    {
//        Id = indis,
//        NobetGrupId = tekliNobetGrup.Id
//    });
//}

#endregion

#region kararlar
//else if (eczaneNobetModelCoz.NobetGrupId.Contains(0))// == 0)
//{//ay seçili, gruplar --tümü--
//    if (eczaneNobetModelCoz.BuAyVeSonrasi)
//    {//bu ay ve sonrası
//        aylar = aylar.Where(w => w.Id >= eczaneNobetModelCoz.AyBitis).ToList();

//        foreach (var ay in aylar)
//        {
//            //foreach (var item in nobetGrupTanimlar)
//            //{
//            var r = new Random();
//            //var ngler = new int[] { 4, 5 };
//            var liste = tumNobetGruplar
//                //.Where(x => ngler.Contains(x.NobetGrupId))
//                .Select(s => s.NobetGrupId)
//                //.OrderBy(x => r.NextDouble())
//                .ToArray();

//            //foreach (var item2 in liste)
//            //{
//            //var liste2 = new List<int>() { item2 };
//            model.AyBitis = ay.Id;
//            model.NobetGrupId = liste;
//            var data = EczaneNobetDataModel(model);
//            //EczaneNobetCozAktifiGuncelle(data);
//            //Kesinlestir(model.NobetUstGrupId);
//            EczaneNobetCozSonuclaraEkle(data);
//            //}
//            //}

//            //foreach (var item in nobetGrupTanimlar)
//            //{
//            //    var r = new Random();
//            //    var liste = tumNobetGruplar
//            //        .Where(x => x.Id == item)
//            //        .Select(s => s.NobetGrupId).OrderBy(x => r.NextDouble()).ToList();

//            //    foreach (var item2 in liste)
//            //    {
//            //        var liste2 = new List<int>() { item2 };
//            //        model.AyBitis = ay.Id;
//            //        model.NobetGrupId = liste2;
//            //        var data = EczaneNobetDataModel(model);
//            //        //EczaneNobetCozAktifiGuncelle(data);
//            //        //Kesinlestir(model.NobetUstGrupId);
//            //        EczaneNobetCozSonuclaraEkle(data);
//            //    }
//            //}
//        }
//    }
//    else
//    {
//        var r = new Random();
//        //var ngler = new int[] { 4, 5, 6, 7, 8, 9, 10, 11, 12 };
//        var liste = tumNobetGruplar
//            //.Where(x => ngler.Contains(x.NobetGrupId))
//            .Select(s => s.NobetGrupId)
//            //.OrderBy(x => r.NextDouble())
//            .ToArray();

//        //foreach (var item2 in liste)
//        //{
//        //var liste2 = new List<int>() { item2 };
//        model.NobetGrupId = liste;
//        var data = EczaneNobetDataModel(model);
//        //EczaneNobetCozAktifiGuncelle(data);
//        //Kesinlestir(model.NobetUstGrupId);
//        EczaneNobetCozSonuclaraEkle(data);
//        //}
//        //}
//        //foreach (var item in nobetGrupTanimlar)
//        //{
//        //    var liste = tumNobetGruplar
//        //        .Where(x => x.Id == item)
//        //        .Select(s => s.NobetGrupId).OrderBy(x => x).ToList();

//        //    foreach (var item2 in liste)
//        //    {
//        //        var liste2 = new List<int>() { item2 };
//        //        model.NobetGrupId = liste2;
//        //        var data = EczaneNobetDataModel(model);
//        //        //EczaneNobetCozAktifiGuncelle(data);
//        //        //Kesinlestir(model.NobetUstGrupId);
//        //        EczaneNobetCozSonuclaraEkle(data);
//        //    }
//        //}
//    }
//}
//else if (eczaneNobetModelCoz.AyBitis == 0)
//{//grup seçili, aylar --tümü--
//    foreach (var ay in aylar)
//    {
//        foreach (var item in tumNobetGruplar
//            .Where(x => eczaneNobetModelCoz.NobetGrupId.Contains(x.NobetGrupId))
//            //.Where(x => x.NobetGrupId == eczaneNobetModelCoz.NobetGrupId)
//            .Select(s => s.BagId).Distinct().ToList())
//        {
//            var liste = tumNobetGruplar
//                .Where(x => x.BagId == item)
//                .Select(s => s.NobetGrupId).OrderBy(x => x).ToList();

//            foreach (var item2 in liste)
//            {
//                var liste2 = new int[] { item2 };
//                model.AyBitis = ay.Id;
//                model.NobetGrupId = liste2;
//                var data = EczaneNobetDataModel(model);
//                //EczaneNobetCozAktifiGuncelle(data);
//                //Kesinlestir(model.NobetUstGrupId);
//                EczaneNobetCozSonuclaraEkle(data);
//            }
//        }
//    }
//}

/*
 if (eczaneNobetModelCoz.BuAyVeSonrasi)
                {//bu ay ve sonrası                        
                    foreach (var ay in aylar)
                    {
                        foreach (var item in tumNobetGruplar
                            .Where(x => eczaneNobetModelCoz.NobetGrupId.Contains(x.NobetGrupId))
                            //.Where(x => x.NobetGrupId == eczaneNobetModelCoz.NobetGrupId)
                            .Select(s => s.BagId).Distinct().ToList())
                        {
                            var liste = tumNobetGruplar
                                .Where(x => x.BagId == item)
                                .Select(s => s.NobetGrupId).OrderBy(x => x).ToArray();

                            model.AyBitis = ay.Id;
                            model.NobetGrupId = liste;
                            var data = EczaneNobetDataModel(model);
                            //EczaneNobetCozAktifiGuncelle(data);
                            //Kesinlestir(model.NobetUstGrupId);
                            EczaneNobetCozSonuclaraEkle(data);
                        }
                    }
                }
                else
                {
                    foreach (var item in tumNobetGruplar
                        .Where(x => eczaneNobetModelCoz.NobetGrupId.Contains(x.NobetGrupId))
                        .Select(s => s.BagId).Distinct().ToList())
                    {
                        var liste = tumNobetGruplar.Where(x => x.BagId == item).Select(s => s.NobetGrupId).OrderBy(x => x).ToArray();
                        model.NobetGrupId = liste;
                        var data = EczaneNobetDataModel(model);
                        EczaneNobetCozAktifiGuncelle(data);
                    }
                }*/
#endregion