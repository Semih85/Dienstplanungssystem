using System;
using System.Collections.Generic;
using System.Linq;
using WM.Core.Aspects.PostSharp.AutorizationAspects;
using WM.Core.Aspects.PostSharp.LogAspects;
using WM.Core.CrossCuttingConcerns.Logging.Log4Net.Logger;
using WM.Northwind.Business.Abstract.EczaneNobet;
using WM.Northwind.Business.Abstract.Optimization.EczaneNobet;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.Optimization.EczaneNobet;
using WM.Optimization.Abstract.Health;

namespace WM.Northwind.Business.Concrete.OptimizationManagers.Health.EczaneNobet
{
    public class DiyarbakirOptimizationManager : IDiyarbakirOptimizationService
    {
        #region ctor
        private IEczaneGrupService _eczaneGrupService;
        private IEczaneGrupTanimService _eczaneGrupTanimService;
        private IEczaneNobetDiyarbakirOptimization _eczaneNobetDiyarbakirOptimization;
        private IEczaneNobetGrupService _eczaneNobetGrupService;
        private IEczaneNobetGrupAltGrupService _eczaneNobetGrupAltGrupService;
        private IEczaneNobetIstekService _eczaneNobetIstekService;
        private IEczaneNobetMazeretService _eczaneNobetMazeretService;
        private IEczaneNobetOrtakService _eczaneNobetOrtakService;
        private IEczaneNobetSonucAktifService _eczaneNobetSonucAktifService;
        private IEczaneNobetSonucService _eczaneNobetSonucService;
        private INobetGrupGorevTipService _nobetGrupGorevTipService;
        private INobetGrupGorevTipGunKuralService _nobetGrupGorevTipGunKuralService;
        private INobetGrupGunKuralService _nobetGrupGunKuralService;
        private INobetGrupKuralService _nobetGrupKuralService;
        private INobetGrupService _nobetGrupService;
        private INobetGrupTalepService _nobetGrupTalepService;
        private INobetUstGrupKisitService _nobetUstGrupKisitService;
        private ITakvimService _takvimService;
        private IEczaneNobetMuafiyetService _eczaneNobetMuafiyetService;
        private IKalibrasyonService _kalibrasyonService;
        private IAyniGunTutulanNobetService _ayniGunTutulanNobetService;
        private INobetDurumService _nobetDurumService;
        private IEczaneUzaklikMatrisService _eczaneUzaklikMatrisService;
        private IEczaneService _eczaneService;
        private INobetGrupGorevTipKisitService _nobetGrupGorevTipKisitService;
        private IDebugEczaneService _debugEczaneService;
        private INobetAltGrupService _nobetAltGrupService;

        public DiyarbakirOptimizationManager(
                    IEczaneGrupService eczaneGrupService,
                    IEczaneGrupTanimService eczaneGrupTanimService,
                    IEczaneNobetDiyarbakirOptimization eczaneNobetDiyarbakirOptimization,
                    IEczaneNobetGrupService eczaneNobetGrupService,
                    IEczaneNobetIstekService eczaneNobetIstekService,
                    IEczaneNobetMazeretService eczaneNobetMazeretService,
                    IEczaneNobetOrtakService eczaneNobetOrtakService,
                    IEczaneNobetSonucAktifService eczaneNobetSonucAktifService,
                    INobetGrupGorevTipService nobetGrupGorevTipService,
                    INobetGrupGorevTipGunKuralService nobetGrupGorevTipGunKuralService,
                    INobetGrupGunKuralService nobetGrupGunKuralService,
                    INobetGrupKuralService nobetGrupKuralService,
                    INobetGrupService nobetGrupService,
                    INobetGrupTalepService nobetGrupTalepService,
                    INobetUstGrupKisitService nobetUstGrupKisitService,
                    ITakvimService takvimService,
                    IEczaneNobetSonucService eczaneNobetSonucService,
                    IEczaneNobetMuafiyetService eczaneNobetMuafiyetService,
                    IEczaneNobetGrupAltGrupService eczaneNobetGrupAltGrupService,
                    IKalibrasyonService kalibrasyonService,
                    IAyniGunTutulanNobetService ayniGunTutulanNobetService,
                    INobetDurumService nobetDurumService,
                    IEczaneUzaklikMatrisService eczaneUzaklikMatrisService,
                    IEczaneService eczaneService,
                    INobetGrupGorevTipKisitService nobetGrupGorevTipKisitService,
                    IDebugEczaneService debugEczaneService,
                    INobetAltGrupService nobetAltGrupService
            )
        {
            _eczaneGrupService = eczaneGrupService;
            _eczaneGrupTanimService = eczaneGrupTanimService;
            _eczaneNobetDiyarbakirOptimization = eczaneNobetDiyarbakirOptimization;
            _eczaneNobetGrupService = eczaneNobetGrupService;
            _eczaneNobetIstekService = eczaneNobetIstekService;
            _eczaneNobetMazeretService = eczaneNobetMazeretService;
            _eczaneNobetOrtakService = eczaneNobetOrtakService;
            _eczaneNobetSonucAktifService = eczaneNobetSonucAktifService;
            _eczaneNobetSonucService = eczaneNobetSonucService;
            _nobetGrupGorevTipService = nobetGrupGorevTipService;
            _nobetGrupGorevTipGunKuralService = nobetGrupGorevTipGunKuralService;
            _nobetGrupGunKuralService = nobetGrupGunKuralService;
            _nobetGrupKuralService = nobetGrupKuralService;
            _nobetGrupService = nobetGrupService;
            _nobetGrupTalepService = nobetGrupTalepService;
            _nobetUstGrupKisitService = nobetUstGrupKisitService;
            _takvimService = takvimService;
            _eczaneNobetMuafiyetService = eczaneNobetMuafiyetService;
            _eczaneNobetGrupAltGrupService = eczaneNobetGrupAltGrupService;
            _kalibrasyonService = kalibrasyonService;
            _ayniGunTutulanNobetService = ayniGunTutulanNobetService;
            _nobetDurumService = nobetDurumService;
            _eczaneUzaklikMatrisService = eczaneUzaklikMatrisService;
            _eczaneService = eczaneService;
            _nobetGrupGorevTipKisitService = nobetGrupGorevTipKisitService;
            _debugEczaneService = debugEczaneService;
            _nobetAltGrupService = nobetAltGrupService;
        }
        #endregion

        /// <summary>
        /// Her çözüm sonrasında aktif sonuçlardaki mevcut kayıtlar silinip yerine yeni sonuçlar eklenir.
        /// </summary>
        /// <param name="data"></param>
        //[TransactionScopeAspect]
        public EczaneNobetSonucModel EczaneNobetCozAktifiGuncelle(DiyarbakirDataModel data)
        {
            var mevcutSonuclar = _eczaneNobetSonucAktifService.GetDetaylar2(data.NobetUstGrupId);
            var guncellenecekSonuclar = mevcutSonuclar
                .Where(x => data.NobetGruplar.Select(s => s.Id).Contains(x.NobetGrupId))
                .Select(s => s.Id).ToArray();

            var yeniSonuclar = _eczaneNobetDiyarbakirOptimization.Solve(data);

            //gelen datadaki nöbet grup id aktif sonuçlarda varsa o nöbet gruba ait önceki sonuçları sil
            _eczaneNobetSonucAktifService.CokluSil(guncellenecekSonuclar);
            AktiftekiArtiklariSil(data.NobetUstGrupId);

            //yeni sonuçları ekle
            _eczaneNobetSonucAktifService.CokluEkle(yeniSonuclar.ResultModel);

            return yeniSonuclar;
        }

        public EczaneNobetSonucModel EczaneNobetCozSonuclaraEkle(DiyarbakirDataModel data)
        {
            var yeniSonuclar = _eczaneNobetDiyarbakirOptimization.Solve(data);
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

        private DiyarbakirDataModel EczaneNobetDataModel(EczaneNobetDataModelParametre eczaneNobetDataModelParametre)
        {
            #region parametreler
            var nobetUstGrupId = eczaneNobetDataModelParametre.NobetUstGrupId;
            var nobetGrupIdListe = eczaneNobetDataModelParametre.NobetGrupId.ToList();
            //var nobetGorevTipId = eczaneNobetDataModelParametre.NobetGorevTipId;
            var nobetUstGrupBaslangicTarihi = eczaneNobetDataModelParametre.NobetUstGrupBaslangicTarihi;
            var baslangicTarihi = eczaneNobetDataModelParametre.BaslangicTarihi;
            var bitisTarihi = eczaneNobetDataModelParametre.BitisTarihi;
            var nobetGrupGorevTipler = eczaneNobetDataModelParametre.NobetGrupGorevTipler;
            var nobetGorevTipler = eczaneNobetDataModelParametre.NobetGrupGorevTipler.Select(s => s.NobetGorevTipId).Distinct().ToList();
            #endregion

            if (baslangicTarihi < nobetUstGrupBaslangicTarihi)
                throw new Exception($"Nöbet başlangıç tarihi <strong>({baslangicTarihi.ToShortDateString()})</strong> üst grup başlama tarihinden <strong>({nobetUstGrupBaslangicTarihi.ToShortDateString()})</strong> küçük olamaz.");

            var debugYapilacakEczaneler = _debugEczaneService.GetDetaylarAktifOlanlar(nobetUstGrupId);

            var nobetGruplar = _nobetGrupService.GetDetaylar(nobetGrupIdListe).OrderBy(s => s.Id).ToList();
            //var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetGrupIdListe); //nobetGorevTipId,
            var eczaneNobetSonuclar = _eczaneNobetSonucService.GetSonuclar(nobetUstGrupId);

            var eczaneNobetMazeretNobettenDusenler = new List<EczaneNobetMazeretSayilari>();

            var mazeret = _nobetUstGrupKisitService.GetKisitPasifMi("mazeret", nobetUstGrupId);

            if (mazeret)
                eczaneNobetMazeretNobettenDusenler = _eczaneNobetMazeretService.GetEczaneNobetMazeretSayilari(baslangicTarihi, bitisTarihi, nobetGrupIdListe);

            var eczaneNobetGruplarTumu = _eczaneNobetGrupService.GetDetaylar(nobetGrupIdListe, baslangicTarihi, bitisTarihi)
                .Where(w => !eczaneNobetMazeretNobettenDusenler.Select(s => s.EczaneNobetGrupId).Contains(w.Id)
                //&& w.EczaneAdi == "ÖZGÜR"
                ).ToList();

            var nobetGorevTipId = 1;
            if (!nobetGorevTipler.Contains(nobetGorevTipId))
                nobetGorevTipId = 0;

            var eczaneNobetGruplarGorevTip1 = eczaneNobetGruplarTumu
                .Where(w => w.NobetGorevTipId == nobetGorevTipId).ToList();

            //nobetGorevTipId = 2;
            //if (!nobetGorevTipler.Contains(nobetGorevTipId))
            //    nobetGorevTipId = 0;

            //var eczaneNobetGruplarGorevTip2 = eczaneNobetGruplarTumu
            //    .Where(w => w.NobetGorevTipId == nobetGorevTipId).ToList();

            var eczaneNobetSonuclarOncekiAylar = eczaneNobetSonuclar
                .Where(w => w.Tarih >= nobetUstGrupBaslangicTarihi
                         && eczaneNobetGruplarTumu.Select(s => s.Id).Contains(w.EczaneNobetGrupId) //görevtip: tümü
                                                                                                   //&& eczaneNobetGruplarGorevTip1.Select(s => s.Id).Contains(w.EczaneNobetGrupId) //görevtip: 1
                         ).ToList();

            var eczaneNobetSonuclarCozulenGruplar = eczaneNobetSonuclar
                .Where(w => eczaneNobetGruplarTumu.Select(s => s.Id).Contains(w.EczaneNobetGrupId)).ToList();

            var son3Ay = baslangicTarihi.AddMonths(-2);

            var eczaneNobetSonuclarSon3Ay = eczaneNobetSonuclarCozulenGruplar
                .Where(w => w.Tarih >= son3Ay).ToList();

            var sonuclarKontrol = _eczaneNobetSonucService.GetSonuclar(baslangicTarihi, bitisTarihi, eczaneNobetSonuclarCozulenGruplar);

            if (sonuclarKontrol.Count > 0)
                throw new Exception("Kriterlere uygun <strong>daha önce yazılmış nöbetler</strong> bulunmaktadır. Lütfen kontrol ediniz!");

            var enSonNobetler = _eczaneNobetOrtakService.GetEczaneNobetGrupGunKuralIstatistik(eczaneNobetGruplarTumu, eczaneNobetSonuclarCozulenGruplar);

            var enSonNobetlerSon3Ay = _eczaneNobetOrtakService.GetEczaneNobetGrupGunKuralIstatistik(eczaneNobetGruplarTumu, eczaneNobetSonuclarSon3Ay);

            var eczaneNobetGrupGunKuralIstatistikYatay = _eczaneNobetOrtakService.GetEczaneNobetGrupGunKuralIstatistikYatay(enSonNobetler);

            var eczaneNobetGrupGunKuralIstatistikYataySon3Ay = _eczaneNobetOrtakService.GetEczaneNobetGrupGunKuralIstatistikYatay(enSonNobetlerSon3Ay);

            //var bak = eczaneNobetGrupGunKuralIstatistikYatay.Where(w => w.NobetGorevTipId == 2).ToList();

            var anahtarListe = eczaneNobetSonuclar
                 .Where(w => w.Tarih < nobetUstGrupBaslangicTarihi && nobetGrupIdListe.Contains(w.NobetGrupId)).ToList();

            //var haftaIciAnahtarListeTumEczaneler = _takvimService.AnahtarListeyiBuGuneTasi(nobetGrupIdListe, 1, nobetUstGrupBaslangicTarihi, eczaneNobetGruplarTumu, eczaneNobetGrupGunKuralIstatistikYatay, anahtarListe, "Hafta İçi");
            var haftaIciAnahtarListeTumEczaneler = _takvimService.AnahtarListeyiBuGuneTasi(nobetGrupIdListe, nobetGorevTipId, nobetUstGrupBaslangicTarihi, eczaneNobetGruplarTumu, eczaneNobetGrupGunKuralIstatistikYatay, anahtarListe, "Hafta İçi");

            var nobetBorcluEczanelerhaftaIci = (from s in eczaneNobetGrupGunKuralIstatistikYatay
                                                from b in haftaIciAnahtarListeTumEczaneler
                                                where s.EczaneNobetGrupId == b.EczaneNobetGrupId
                                                && s.NobetSayisiHaftaIci == b.NobetSayisi
                                                select new EczaneNobetAlacakVerecek
                                                {
                                                    EczaneNobetGrupId = s.EczaneNobetGrupId,
                                                    EczaneId = s.EczaneId,
                                                    EczaneAdi = s.EczaneAdi,
                                                    NobetGrupAdi = s.NobetGrupAdi,
                                                    NobetGrupId = s.NobetGrupId,
                                                    NobetSayisi = s.NobetSayisiHaftaIci,
                                                    SonNobetTarihi = s.SonNobetTarihiHaftaIci,
                                                    AnahtarTarih = b.Tarih,
                                                    BorcluGunSayisi = (int)(s.NobetSayisiHaftaIci > 0
                                                            ? (s.SonNobetTarihiHaftaIci - b.Tarih).TotalDays
                                                            : (s.SonNobetTarihiHaftaIci - b.Tarih).TotalDays - (s.SonNobetTarihiHaftaIci - b.NobetUstGrupBaslamaTarihi).TotalDays),
                                                    GunGrupAdi = "Hafta İçi"
                                                }).ToList();

            foreach (var eczane in nobetBorcluEczanelerhaftaIci)
            {
                eczaneNobetGrupGunKuralIstatistikYatay
                    .Where(w => w.EczaneNobetGrupId == eczane.EczaneNobetGrupId)
                    .FirstOrDefault().BorcluNobetSayisiHaftaIci = (int)eczane.BorcluGunSayisi;
            }

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

            var eczaneGrupNobetSonuclar = grupluEczaneNobetSonuclar
                .Where(w => eczaneGruplar2.Select(s => s.EczaneId).Contains(w.EczaneId)).ToList();

            //nöbet yazılacak tarih aralığı(örn. Ocak ayının tüm günleri)
            var tarihAralik = _takvimService.GetTakvimNobetGruplar(baslangicTarihi, bitisTarihi, nobetGrupGorevTipler);

            var eczaneKumulatifHedefler = new List<EczaneNobetIstatistik>();
            var eczaneNobetIstatistikler = new List<EczaneNobetIstatistik>();

            //2.görev tipi için sadece cumartesi olduğu için tüm günler tüm görev tiplerini almaya gerek yok.
            //var eczaneNobetTarihAralik = _takvimService.GetEczaneNobetTarihAralik(baslangicTarihi, bitisTarihi, nobetGrupGorevTipler)
            //    .Where(w => eczaneNobetGruplar.Select(s => s.EczaneId).Contains(w.EczaneId)).ToList();
            var nobetGrupGorevTipGunKurallar = _nobetGrupGorevTipGunKuralService.GetDetaylarAktifList(nobetGrupGorevTipler.Select(s => s.Id).ToList());

            nobetGorevTipId = 1;
            var nobetGrupGorevTip1 = nobetGrupGorevTipler.Where(w => w.NobetGorevTipId == nobetGorevTipId).ToList();

            var eczaneNobetTarihAralik1 = _takvimService.GetEczaneNobetTarihAralik(baslangicTarihi, bitisTarihi, nobetGrupGorevTip1)
                .Where(w => eczaneNobetGruplarGorevTip1.Select(s => s.Id).Contains(w.EczaneNobetGrupId)).ToList();

            //nobetGorevTipId = 2;
            //var nobetGrupGorevTip2 = nobetGrupGorevTipler.Where(w => w.NobetGorevTipId == nobetGorevTipId).ToList();
            //var noberGunKurallar = nobetGrupGorevTipGunKurallar.Where(w => nobetGrupGorevTip2.Select(s => s.Id).Contains(w.NobetGrupGorevTipId)).Select(s => s.NobetGunKuralId).ToList();

            //var eczaneNobetTarihAralik2 = _takvimService.GetEczaneNobetTarihAralik(baslangicTarihi, bitisTarihi, nobetGrupGorevTip2, noberGunKurallar)
            //    .Where(w => eczaneNobetGruplarGorevTip2.Select(s => s.Id).Contains(w.EczaneNobetGrupId)).ToList();

            var eczaneNobetTarihAralik = _eczaneNobetOrtakService.AmacFonksiyonuKatsayisiBelirle(eczaneNobetTarihAralik1, eczaneNobetGrupGunKuralIstatistikYatay);

            //.Union(eczaneNobetTarihAralik2).ToList();

            var eczaneNobetIstekler = _eczaneNobetIstekService.GetDetaylarByNobetGrupIdList(baslangicTarihi, bitisTarihi, nobetGrupIdListe)
                .Where(w => eczaneNobetGruplarTumu.Select(s => s.EczaneId).Contains(w.EczaneId)).ToList();

            var eczaneNobetMazeretler = _eczaneNobetMazeretService.GetDetaylarByEczaneIdList(baslangicTarihi, bitisTarihi, eczaneNobetGruplarTumu.Select(s => s.EczaneId).Distinct().ToList())
                  .Where(w => w.MazeretId != 3)
                  .ToList();

            var takvimNobetGrupGunDegerIstatistikler = _takvimService.GetTakvimNobetGrupGunDegerIstatistikler(nobetUstGrupBaslangicTarihi, bitisTarihi, nobetGrupGorevTipler);

            var eczaneNobetGrupAltGruplar = _eczaneNobetGrupAltGrupService.GetDetaylar(nobetUstGrupId);

            #region ikili eczaneler 

            var ayIcindeAyniGunNobetTutanEczaneler = new List<EczaneCiftGrup>();

            var arasindaAyniGunNobetFarkiOlanIkiliEczaneler = _nobetUstGrupKisitService.GetDetay("ikiliEczaneAyniGunNobet", nobetUstGrupId);
            var arasindaAyniGun2NobetFarkiOlanIkiliEczaneler = new List<EczaneGrupDetay>();

            if (!arasindaAyniGunNobetFarkiOlanIkiliEczaneler.PasifMi)
            {
                //indisId = eczaneGruplar2.Select(s => s.EczaneGrupTanimId).Max();
                arasindaAyniGun2NobetFarkiOlanIkiliEczaneler = _ayniGunTutulanNobetService.GetArasinda2FarkOlanIkiliEczaneleri(eczaneNobetGruplarTumu, nobetUstGrupId, (int)arasindaAyniGunNobetFarkiOlanIkiliEczaneler.SagTarafDegeri);
            }

            #endregion

            #region önceki aylar aynı gün nöbet tutanlar çözülen ayda aynı gün nöbetçi olmasın

            var oncekiAylardaAyniGunNobetTutanEczaneler = new List<EczaneGrupDetay>();

            var oncekiAylarAyniGunNobet = _nobetUstGrupKisitService.GetDetay("oncekiAylarAyniGunNobet", nobetUstGrupId);

            if (!oncekiAylarAyniGunNobet.PasifMi)
                oncekiAylardaAyniGunNobetTutanEczaneler = _eczaneNobetSonucService.OncekiAylardaAyniGunNobetTutanlar(baslangicTarihi, eczaneNobetSonuclarOncekiAylar, 0, (int)oncekiAylarAyniGunNobet.SagTarafDegeri);

            #endregion

            #region sonraki aydaki istekler

            var sonrakiAy = bitisTarihi.AddDays(1);
            var bitisTarihiSonrakiAy = bitisTarihi.AddMonths((int)oncekiAylarAyniGunNobet.SagTarafDegeri + 2);

            var eczaneNobetIsteklerSonrakiDonem = _eczaneNobetIstekService.GetDetaylarByNobetGrupIdList(sonrakiAy, bitisTarihiSonrakiAy, nobetGrupIdListe)
                .Where(w => eczaneNobetGruplarGorevTip1.Select(s => s.EczaneId).Contains(w.EczaneId)).ToList();

            var sonrakiDonemAyniGunNobetIstekGirilenler = _eczaneNobetIstekService.SonrakiAylardaAyniGunIstekGirilenEczaneler(eczaneNobetIsteklerSonrakiDonem);

            #endregion

            var nobetGrupGorevTipKisitlar = _nobetGrupGorevTipKisitService.GetDetaylar(nobetUstGrupId);

            #region mesafeler

            #region mesafe kontrol

            var eczanelerArasiMesafeyiKoru = _nobetUstGrupKisitService.GetDetay("eczanelerArasiMesafeyiKoru", nobetUstGrupId);

            var mesafeKriter = (int)eczanelerArasiMesafeyiKoru.SagTarafDegeri;

            var mesafeler = _eczaneUzaklikMatrisService.GetDetaylar(nobetUstGrupId);

            var eczaneMesafeler = mesafeler.Where(w => w.Mesafe <= mesafeKriter).ToList();

            var mesafeKontrolEczaneler = _eczaneUzaklikMatrisService.GetMesafeKriterineGoreKontrolEdilecekEczaneGruplar(mesafeKriter, eczaneMesafeler);

            var mesafeKontrolEczanelerGrupBazli = new List<EczaneGrupDetay>();

            foreach (var nobetGrupGorevTip in nobetGrupGorevTipler)
            {
                var eczanelerArasiMesafeyiKoruGrupBazli = nobetGrupGorevTipKisitlar
                    .FirstOrDefault(w => w.KisitId == 59 
                                       && w.NobetGrupGorevTipId == nobetGrupGorevTip.Id) ?? new NobetGrupGorevTipKisitDetay();

                var mesafeKriterGrupBazli = (int)eczanelerArasiMesafeyiKoruGrupBazli.SagTarafDegeri;

                var eczaneMesafelerGrupBazli = mesafeler.Where(w => w.Mesafe <= mesafeKriterGrupBazli).ToList();

                var mesafeKontrolEczanelerGruplu = _eczaneUzaklikMatrisService.GetMesafeKriterineGoreKontrolEdilecekEczaneGruplar(mesafeKriter, eczaneMesafelerGrupBazli)
                    .Where(w=> w.NobetGrupGorevTipIdFrom == nobetGrupGorevTip.Id).ToList();

                mesafeKontrolEczanelerGrupBazli.AddRange(mesafeKontrolEczanelerGruplu);
            }

            #endregion

            #region ikili eczaneler

            //mesafe kriterinden büyük olanlar (birbirleri ile aynı gün nöbet tutabilirler ama 1 ayda 2 kez üst üste tutamazlar.)

            var ikiliEczaneler = _ayniGunTutulanNobetService.GetDetaylar(nobetGrupIdListe);
            var kritereUygunSayilar = mesafeler.Where(w => w.Mesafe > mesafeKriter).ToList();
            var ikiliEczanelerMesafe = _eczaneNobetOrtakService.MesafelerListesiniOlustur(eczaneNobetMazeretNobettenDusenler, eczaneNobetGruplarGorevTip1, kritereUygunSayilar);

            var eczaneNobetTarihAralikIkiliEczaneler = (from m in ikiliEczanelerMesafe
                                                        from t in tarihAralik
                                                        select new EczaneNobetTarihAralikIkili
                                                        {
                                                            EczaneNobetGrupId1 = m.EczaneNobetGrupId1,
                                                            EczaneNobetGrupId2 = m.EczaneNobetGrupId2,
                                                            AyniGunTutulanNobetId = m.Id,
                                                            TakvimId = t.TakvimId,
                                                            Tarih = t.Tarih,
                                                            EczaneAdi1 = m.EczaneAdi1,
                                                            EczaneAdi2 = m.EczaneAdi2,
                                                            EczaneId1 = m.EczaneId1,
                                                            EczaneId2 = m.EczaneId2,
                                                            NobetGrupAdi1 = m.NobetGrupAdi1,
                                                            NobetGrupAdi2 = m.NobetGrupAdi2,
                                                            NobetGrupId1 = m.NobetGrupId1,
                                                            NobetGrupId2 = m.NobetGrupId2
                                                        }).ToList();

            //        _takvimService.GetIkiliEczaneNobetTarihAralik(baslangicTarihi, bitisTarihi, nobetGorevTipId, nobetGrupIdListe)
            //.Where(w => eczaneNobetGruplarTumu.Select(s => s.EczaneId).Contains(w.EczaneId1)
            //         || eczaneNobetGruplarTumu.Select(s => s.EczaneId).Contains(w.EczaneId2)
            //).ToList(); 

            #endregion

            #endregion

            var nobetGrupKurallar = _nobetGrupKuralService.GetDetaylar(nobetGrupIdListe);

            var nobetAltGruplar = _nobetAltGrupService.GetDetaylar(nobetUstGrupId);

            var dataModel = new DiyarbakirDataModel()
            {
                Yil = eczaneNobetDataModelParametre.YilBaslangic,
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
                EczaneNobetTarihAralikIkiliEczaneler = eczaneNobetTarihAralikIkiliEczaneler,
                EczaneKumulatifHedefler = eczaneKumulatifHedefler,//.Where(w => w.EczaneId != 121).ToList(),
                EczaneNobetIstatistikler = eczaneNobetIstatistikler,
                EczaneNobetMazeretler = eczaneNobetMazeretler,
                EczaneGrupTanimlar = eczaneGrupTanimlar,
                TarihAraligi = tarihAralik,
                NobetGruplar = nobetGruplar,
                EczaneGruplar = eczaneGruplar2,
                ArasindaAyniGun2NobetFarkiOlanIkiliEczaneler = arasindaAyniGun2NobetFarkiOlanIkiliEczaneler,
                AyniGunNobetTutanEsGruplar = new List<EczaneGrupDetay>(), //ayniGunNoetTutanEczaneGruplar,
                OncekiAylardaAyniGunNobetTutanEczaneler = oncekiAylardaAyniGunNobetTutanEczaneler,
                EczaneNobetIstekler = eczaneNobetIstekler,
                NobetGrupGunKurallar = _nobetGrupGunKuralService.GetAktifList(nobetGrupIdListe),
                NobetGrupGorevTipGunKurallar = _nobetGrupGorevTipGunKuralService.GetDetaylarAktifList(nobetGrupGorevTipler.Select(s => s.Id).ToList()),
                NobetGrupKurallar = nobetGrupKurallar,
                NobetGrupGorevTipler = nobetGrupGorevTipler,
                NobetGrupTalepler = _nobetGrupTalepService.GetDetaylar(nobetGrupIdListe, baslangicTarihi, bitisTarihi),
                EczaneNobetGruplar = eczaneNobetGruplarTumu,
                Kisitlar = _nobetUstGrupKisitService.GetDetaylar(nobetUstGrupId),
                NobetGrupGorevTipKisitlar = nobetGrupGorevTipKisitlar,

                EczaneGrupNobetSonuclar = eczaneGrupNobetSonuclar,
                EczaneNobetSonuclar = eczaneNobetSonuclarCozulenGruplar,
                EczaneGrupNobetSonuclarTumu = eczaneNobetSonuclar,
                EczaneNobetGrupGunKuralIstatistikler = enSonNobetler,
                TakvimNobetGrupGunDegerIstatistikler = takvimNobetGrupGunDegerIstatistikler,
                EczaneNobetGrupGunKuralIstatistikYatay = eczaneNobetGrupGunKuralIstatistikYatay,
                EczaneNobetGrupGunKuralIstatistikYataySon3Ay = eczaneNobetGrupGunKuralIstatistikYataySon3Ay,
                EczaneNobetGrupAltGruplar = eczaneNobetGrupAltGruplar,
                Kalibrasyonlar = _kalibrasyonService.GetKalibrasyonlarYatay(nobetUstGrupId),
                IkiliEczanelerMesafe = ikiliEczanelerMesafe,//ikiliEczaneler,3
                IkiliEczaneler = ikiliEczaneler,
                NobetDurumlar = _nobetDurumService.GetDetaylar(nobetUstGrupId),
                MesafeKontrolEczaneler = mesafeKontrolEczaneler,
                MesafeKontrolEczanelerGrupBazli = mesafeKontrolEczanelerGrupBazli,                
                SonrakiDonemAyniGunNobetIstekGirilenler = sonrakiDonemAyniGunNobetIstekGirilenler,
                EczaneNobetIsteklerSonrakiDonem = eczaneNobetIsteklerSonrakiDonem,
                DebugYapilacakEczaneler = debugYapilacakEczaneler,
                NobetAltGruplar = nobetAltGruplar
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
                    //var ayniGunNobetTutanEczaneler = _eczaneNobetOrtakService.GetAyniGunNobetTutanEczaneler(sonuclarTumu);
                    //_ayniGunTutulanNobetService.AyniGunNobetTutanlariTabloyaEkle(ayniGunNobetTutanEczaneler);
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
        public EczaneNobetSonucModel ModelCozEski(EczaneNobetModelCoz eczaneNobetModelCoz)
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
    }
}

