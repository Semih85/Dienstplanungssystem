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
    public class GiresunOptimizationManager : IGiresunOptimizationService
    {
        #region ctor
        private IEczaneGrupService _eczaneGrupService;
        private IEczaneGrupTanimService _eczaneGrupTanimService;
        private IEczaneNobetGiresunOptimization _eczaneNobetGiresunOptimization;
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
        private INobetGrupGorevTipKisitService _nobetGrupGorevTipKisitService;
        private IAyniGunTutulanNobetService _ayniGunTutulanNobetService;
        private IDebugEczaneService _debugEczaneService;

        public GiresunOptimizationManager(
                    IEczaneGrupService eczaneGrupService,
                    IEczaneGrupTanimService eczaneGrupTanimService,
                    IEczaneNobetGiresunOptimization eczaneNobetGiresunOptimization,
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
                    INobetGrupGorevTipKisitService nobetGrupGorevTipKisitService,
                    IAyniGunTutulanNobetService ayniGunTutulanNobetService,
                    IDebugEczaneService debugEczaneService
            )
        {
            _eczaneGrupService = eczaneGrupService;
            _eczaneGrupTanimService = eczaneGrupTanimService;
            _eczaneNobetGiresunOptimization = eczaneNobetGiresunOptimization;
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
            _nobetGrupGorevTipKisitService = nobetGrupGorevTipKisitService;
            _ayniGunTutulanNobetService = ayniGunTutulanNobetService;
            _debugEczaneService = debugEczaneService;
        }
        #endregion

        /// <summary>
        /// Her çözüm sonrasında aktif sonuçlardaki mevcut kayıtlar silinip yerine yeni sonuçlar eklenir.
        /// </summary>
        /// <param name="data"></param>
        //[TransactionScopeAspect]
        public EczaneNobetSonucModel EczaneNobetCozAktifiGuncelle(GiresunDataModel data)
        {
            var mevcutSonuclar = _eczaneNobetSonucAktifService.GetDetaylar2(data.NobetUstGrupId);
            var guncellenecekSonuclar = mevcutSonuclar
                .Where(x => data.NobetGruplar.Select(s => s.Id).Contains(x.NobetGrupId))
                .Select(s => s.Id).ToArray();

            var yeniSonuclar = _eczaneNobetGiresunOptimization.Solve(data);

            //gelen datadaki nöbet grup id aktif sonuçlarda varsa o nöbet gruba ait önceki sonuçları sil
            _eczaneNobetSonucAktifService.CokluSil(guncellenecekSonuclar);
            AktiftekiArtiklariSil(data.NobetUstGrupId);

            //yeni sonuçları ekle
            _eczaneNobetSonucAktifService.CokluEkle(yeniSonuclar.ResultModel);

            return yeniSonuclar;
        }

        public EczaneNobetSonucModel EczaneNobetCozSonuclaraEkle(GiresunDataModel data)
        {
            var yeniSonuclar = _eczaneNobetGiresunOptimization.Solve(data);
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

        private GiresunDataModel EczaneNobetDataModel(EczaneNobetDataModelParametre eczaneNobetDataModelParametre)
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

            nobetGorevTipId = 2;
            if (!nobetGorevTipler.Contains(nobetGorevTipId))
                nobetGorevTipId = 0;

            var eczaneNobetGruplarGorevTip2 = eczaneNobetGruplarTumu
                .Where(w => w.NobetGorevTipId == nobetGorevTipId).ToList();

            var eczaneNobetSonuclarOncekiAylar = eczaneNobetSonuclar
                .Where(w => w.Tarih >= nobetUstGrupBaslangicTarihi
                         && eczaneNobetGruplarTumu.Select(s => s.Id).Contains(w.EczaneNobetGrupId) //görevtip: tümü
                                                                                                   //&& eczaneNobetGruplarGorevTip1.Select(s => s.Id).Contains(w.EczaneNobetGrupId) //görevtip: 1
                         ).ToList();

            var eczaneNobetSonuclarCozulenGruplar = eczaneNobetSonuclar
                .Where(w => eczaneNobetGruplarTumu.Select(s => s.Id).Contains(w.EczaneNobetGrupId)).ToList();

            var son3Ay = new DateTime(2020, 1, 1);
                //baslangicTarihi.AddMonths(-2);

            var eczaneNobetSonuclarSon3Ay = eczaneNobetSonuclarCozulenGruplar
                .Where(w => w.Tarih >= son3Ay).ToList();

            var sonuclarKontrol = _eczaneNobetSonucService.GetSonuclar(baslangicTarihi, bitisTarihi, eczaneNobetSonuclarCozulenGruplar);

            if (sonuclarKontrol.Count > 0)
                throw new Exception("Kriterlere uygun <strong>daha önce yazılmış nöbetler</strong> bulunmaktadır. Lütfen kontrol ediniz!");

            var tarihAralik = _takvimService.GetTakvimNobetGruplar(baslangicTarihi, bitisTarihi, nobetGrupGorevTipler);
            var enSonNobetler = _eczaneNobetOrtakService.GetEczaneNobetGrupGunKuralIstatistik(eczaneNobetGruplarTumu, eczaneNobetSonuclarCozulenGruplar);
            var enSonNobetlerSon3Ay = _eczaneNobetOrtakService.GetEczaneNobetGrupGunKuralIstatistik(eczaneNobetGruplarTumu, eczaneNobetSonuclarSon3Ay);

            var eczaneNobetGrupGunKuralIstatistikYatay = _eczaneNobetOrtakService.GetEczaneNobetGrupGunKuralIstatistikYatay(enSonNobetler);
            var eczaneBazliGunKuralIstatistikYatay = _eczaneNobetOrtakService.GetEczaneBazliGunKuralIstatistikYatay(enSonNobetler);
            var eczaneNobetGrupGunKuralIstatistikYataySon3Ay = _eczaneNobetOrtakService.GetEczaneNobetGrupGunKuralIstatistikYatay(enSonNobetlerSon3Ay);

            //var bak = eczaneNobetGrupGunKuralIstatistikYatay.Where(w => w.NobetGorevTipId == 2).ToList();

            var anahtarListe = eczaneNobetSonuclar
                 .Where(w => w.Tarih < nobetUstGrupBaslangicTarihi && nobetGrupIdListe.Contains(w.NobetGrupId)).ToList();

            var borcTakipEdilecekGunGruplar = new int[]
            {
                //1,
                //3,
                4
            };

            var gunGruplar = tarihAralik
                .Where(w => borcTakipEdilecekGunGruplar.Contains(w.GunGrupId))
                .Select(s => new { s.GunGrupId, s.GunGrupAdi })
                .Distinct()
                .OrderBy(o => o.GunGrupId).ToList();

            foreach (var gunGrup in gunGruplar)
            {
                var haftaIciAnahtarListeTumEczaneler = _takvimService.AnahtarListeyiBuGuneTasi(nobetGrupGorevTipler, eczaneNobetGruplarTumu, eczaneNobetGrupGunKuralIstatistikYatay, anahtarListe, gunGrup.GunGrupAdi);

                var nobetBorcEczaneler = (from s in eczaneNobetGrupGunKuralIstatistikYatay
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
                                              NobetSayisi = gunGrup.GunGrupId == 1
                                                  ? s.NobetSayisiPazar
                                                  : gunGrup.GunGrupId == 4
                                                  ? s.NobetSayisiCumartesi
                                                  : s.NobetSayisiHaftaIci,
                                              SonNobetTarihi = gunGrup.GunGrupId == 1
                                                  ? s.SonNobetTarihiPazar
                                                  : gunGrup.GunGrupId == 4
                                                  ? s.SonNobetTarihiCumartesi
                                                  : s.SonNobetTarihiHaftaIci,
                                              AnahtarTarih = b.Tarih,
                                              BorcluGunSayisi = gunGrup.GunGrupId == 1
                                                  ? ((int)(s.NobetSayisiPazar > 0
                                                      ? (s.SonNobetTarihiPazar - b.Tarih).TotalDays
                                                      : (s.SonNobetTarihiPazar - b.Tarih).TotalDays - (s.SonNobetTarihiPazar - b.NobetUstGrupBaslamaTarihi).TotalDays))
                                                  : gunGrup.GunGrupId == 4
                                                  ? ((int)(s.NobetSayisiCumartesi > 0
                                                      ? (s.SonNobetTarihiCumartesi - b.Tarih).TotalDays
                                                      : (s.SonNobetTarihiCumartesi - b.Tarih).TotalDays - (s.SonNobetTarihiCumartesi - b.NobetUstGrupBaslamaTarihi).TotalDays))
                                                  : ((int)(s.NobetSayisiHaftaIci > 0
                                                      ? (s.SonNobetTarihiHaftaIci - b.Tarih).TotalDays
                                                      : (s.SonNobetTarihiHaftaIci - b.Tarih).TotalDays - (s.SonNobetTarihiHaftaIci - b.NobetUstGrupBaslamaTarihi).TotalDays)),
                                              GunGrupAdi = gunGrup.GunGrupAdi,
                                              GunGrupId = gunGrup.GunGrupId
                                          }).ToList();

                foreach (var eczane in nobetBorcEczaneler)
                {
                    if (gunGrup.GunGrupId == 1)
                    {
                        //eczaneNobetGrupGunKuralIstatistikYatay
                        //.Where(w => w.EczaneNobetGrupId == eczane.EczaneNobetGrupId)
                        //.FirstOrDefault().bo = (int)eczane.BorcluGunSayisi;
                    }
                    else if (gunGrup.GunGrupId == 3)
                    {
                        eczaneNobetGrupGunKuralIstatistikYatay
                        .Where(w => w.EczaneNobetGrupId == eczane.EczaneNobetGrupId)
                        .FirstOrDefault().BorcluNobetSayisiHaftaIci = (int)eczane.BorcluGunSayisi;
                    }
                    else if (gunGrup.GunGrupId == 4)
                    {

                    }
                }
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

            nobetGorevTipId = 2;
            var nobetGrupGorevTip2 = nobetGrupGorevTipler.Where(w => w.NobetGorevTipId == nobetGorevTipId).ToList();
            var noberGunKurallar = nobetGrupGorevTipGunKurallar.Where(w => nobetGrupGorevTip2.Select(s => s.Id).Contains(w.NobetGrupGorevTipId)).Select(s => s.NobetGunKuralId).ToList();

            var eczaneNobetTarihAralik2 = _takvimService.GetEczaneNobetTarihAralik(baslangicTarihi, bitisTarihi, nobetGrupGorevTip2, noberGunKurallar)
                .Where(w => eczaneNobetGruplarGorevTip2.Select(s => s.Id).Contains(w.EczaneNobetGrupId)).ToList();

            var eczaneNobetTarihAralikTumu = eczaneNobetTarihAralik1.Union(eczaneNobetTarihAralik2).ToList();

            //var kd = eczaneNobetTarihAralikTumu.Where(w => w.EczaneAdi == "AHSEN").ToList();

            var eczaneNobetTarihAralik = _eczaneNobetOrtakService.AmacFonksiyonuKatsayisiBelirle(eczaneNobetTarihAralikTumu, eczaneNobetGrupGunKuralIstatistikYatay);

            var ikiliEczaneler = _ayniGunTutulanNobetService.GetDetaylar(nobetGrupIdListe);
                //.Where(w => w.EczaneId1 != w.EczaneId2).ToList();
                //.Select(s => new
                //{
                //    s.EczaneAdi1,
                //    s.EczaneAdi2,
                //    s.EczaneId1,
                //    s.EczaneId2
                //})
                //.Distinct()
                //.ToList();
            //.Where(w => w.NobetGorevTipId1 == 1 && w.NobetGorevTipId2 == 1).ToList();

            //var ikiliEczaneler2 = new List<AyniGunTutulanNobetDetay>();

            //foreach (var ikiliEczane in ikiliEczaneler)
            //{
            //    ikiliEczaneler2.Add(new AyniGunTutulanNobetDetay
            //    {                    
            //        EczaneId1 = ikiliEczane.EczaneId1,
            //        EczaneId2 = ikiliEczane.EczaneId2,
            //        EczaneAdi1 = ikiliEczane.EczaneAdi1,
            //        EczaneAdi2 = ikiliEczane.EczaneAdi2
            //    });
            //}

            var eczaneNobetIstekler = _eczaneNobetIstekService.GetDetaylarByNobetGrupIdList(baslangicTarihi, bitisTarihi, nobetGrupIdListe)
                .Where(w => eczaneNobetGruplarTumu.Select(s => s.EczaneId).Contains(w.EczaneId)).ToList();

            var eczaneNobetMazeretler = _eczaneNobetMazeretService.GetDetaylarByEczaneIdList(baslangicTarihi, bitisTarihi, eczaneNobetGruplarTumu.Select(s => s.EczaneId).Distinct().ToList())
                  .Where(w => w.MazeretId != 3)
                  .ToList();

            var takvimNobetGrupGunDegerIstatistikler = _takvimService.GetTakvimNobetGrupGunDegerIstatistikler(nobetUstGrupBaslangicTarihi, bitisTarihi, nobetGrupGorevTipler);

            var eczaneNobetGrupAltGruplar = _eczaneNobetGrupAltGrupService.GetDetaylar(nobetUstGrupId);

            #region alt Gruplarla Aynı Gün Nöbet Tutma

            var altGruplarlaAyniGunNobetTutma = _nobetUstGrupKisitService.GetDetay("altGruplarlaAyniGunNobetTutma", nobetUstGrupId);

            var eczaneNobetSonuclarAltGruplaAyniGun = new List<EczaneNobetSonucListe2>();
            var altGruplarlaAyniGunNobetTutmayacakEczanelerSehirDisi = new List<EczaneGrupDetay>();
            var altGruplarlaAyniGunNobetTutmayacakEczanelerSehirIci1 = new List<EczaneGrupDetay>();
            var altGruplarlaAyniGunNobetTutmayacakEczanelerSehirIci2 = new List<EczaneGrupDetay>();

            if (!altGruplarlaAyniGunNobetTutma.PasifMi)
            {
                eczaneNobetSonuclarAltGruplaAyniGun = eczaneNobetSonuclar
                        .Where(w => eczaneNobetGruplarGorevTip1.Select(s => s.NobetGorevTipId).Contains(w.NobetGorevTipId)).ToList();

                #region 13 - 11,12

                var ayniGunNobetTutmasiTakipEdilecekAltGruplar = new List<int>
                {
                    13 //Şehir dışı (doğu + batı)
                };

                var altGrubuOlanNobetGruplar = new List<int>
                {
                    11,//Şehir içi (merkez-1)	
                    12 //Şehir içi (merkez-2)	
                };

                altGruplarlaAyniGunNobetTutmayacakEczanelerSehirDisi = _eczaneNobetOrtakService.AltGruplarlaSiraliNobetListesiniOlusturGiresun(eczaneNobetSonuclarAltGruplaAyniGun,
                    eczaneNobetGruplarGorevTip1,
                    eczaneNobetGrupAltGruplar,
                    altGruplarlaAyniGunNobetTutma,
                    nobetUstGrupBaslangicTarihi,
                    0,
                    ayniGunNobetTutmasiTakipEdilecekAltGruplar,
                    altGrubuOlanNobetGruplar);

                #endregion

                #region 11 - 12,13

                ayniGunNobetTutmasiTakipEdilecekAltGruplar = new List<int>
                {
                    11//Şehir içi (merkez-1)
                };

                altGrubuOlanNobetGruplar = new List<int>
                {
                    12,//Şehir içi (merkez-2)	
                    13 //Şehir dışı (doğu + batı)
                };

                altGruplarlaAyniGunNobetTutmayacakEczanelerSehirIci1 = _eczaneNobetOrtakService.AltGruplarlaSiraliNobetListesiniOlusturGiresun(eczaneNobetSonuclarAltGruplaAyniGun,
                    eczaneNobetGruplarGorevTip1,
                    eczaneNobetGrupAltGruplar,
                    altGruplarlaAyniGunNobetTutma,
                    nobetUstGrupBaslangicTarihi,
                    0,
                    ayniGunNobetTutmasiTakipEdilecekAltGruplar,
                    altGrubuOlanNobetGruplar);

                #endregion

                #region 12 - 11,13

                ayniGunNobetTutmasiTakipEdilecekAltGruplar = new List<int>
                {
                    12//Şehir içi (merkez-2)
                };

                altGrubuOlanNobetGruplar = new List<int>
                {
                    11,//Şehir içi (merkez-1)	
                    13 //Şehir dışı (doğu + batı)
                };

                altGruplarlaAyniGunNobetTutmayacakEczanelerSehirIci2 = _eczaneNobetOrtakService.AltGruplarlaSiraliNobetListesiniOlusturGiresun(eczaneNobetSonuclarAltGruplaAyniGun,
                    eczaneNobetGruplarGorevTip1,
                    eczaneNobetGrupAltGruplar,
                    altGruplarlaAyniGunNobetTutma,
                    nobetUstGrupBaslangicTarihi,
                    0,
                    ayniGunNobetTutmasiTakipEdilecekAltGruplar,
                    altGrubuOlanNobetGruplar);

                #endregion
            }

            #endregion

            #region önceki aylar aynı gün nöbet tutanlar çözülen ayda aynı gün nöbetçi olmasın

            var oncekiAylardaAyniGunNobetTutanEczaneler = new List<EczaneGrupDetay>();

            var oncekiAylarAyniGunNobet = _nobetUstGrupKisitService.GetDetay("oncekiAylarAyniGunNobet", nobetUstGrupId);

            if (!oncekiAylarAyniGunNobet.PasifMi)
                oncekiAylardaAyniGunNobetTutanEczaneler = _eczaneNobetSonucService.OncekiAylardaAyniGunNobetTutanlar(baslangicTarihi, eczaneNobetSonuclarOncekiAylar, 0, (int)oncekiAylarAyniGunNobet.SagTarafDegeri);

            #endregion

            var nobetGrupKurallar = _nobetGrupKuralService.GetDetaylar(nobetGrupIdListe);

            var nobetUstGrupKisitlar = _nobetUstGrupKisitService.GetDetaylar(nobetUstGrupId);
            var grupBazliKisitlar = _nobetGrupGorevTipKisitService.GetDetaylar(nobetUstGrupId);

            var giresunDataModel = new GiresunDataModel()
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
                EczaneKumulatifHedefler = eczaneKumulatifHedefler,//.Where(w => w.EczaneId != 121).ToList(),
                EczaneNobetIstatistikler = eczaneNobetIstatistikler,
                EczaneNobetMazeretler = eczaneNobetMazeretler,
                EczaneGrupTanimlar = eczaneGrupTanimlar,
                TarihAraligi = tarihAralik,
                NobetGruplar = nobetGruplar,
                EczaneGruplar = eczaneGruplar2,
                //AyniGunNobetTutanEsGruplar = ayniGunNoetTutanEczaneGruplar,
                AltGruplarlaAyniGunNobetTutmayacakEczanelerSehirDisi = altGruplarlaAyniGunNobetTutmayacakEczanelerSehirDisi,
                AltGruplarlaAyniGunNobetTutmayacakEczanelerSehirIci1 = altGruplarlaAyniGunNobetTutmayacakEczanelerSehirIci1,
                AltGruplarlaAyniGunNobetTutmayacakEczanelerSehirIci2 = altGruplarlaAyniGunNobetTutmayacakEczanelerSehirIci2,
                OncekiAylardaAyniGunNobetTutanEczaneler = oncekiAylardaAyniGunNobetTutanEczaneler,
                EczaneNobetIstekler = eczaneNobetIstekler,
                NobetGrupGunKurallar = _nobetGrupGunKuralService.GetAktifList(nobetGrupIdListe),
                NobetGrupGorevTipGunKurallar = _nobetGrupGorevTipGunKuralService.GetDetaylarAktifList(nobetGrupGorevTipler.Select(s => s.Id).ToList()),
                NobetGrupKurallar = nobetGrupKurallar,
                NobetGrupGorevTipler = nobetGrupGorevTipler,
                NobetGrupTalepler = _nobetGrupTalepService.GetDetaylar(nobetGrupIdListe, baslangicTarihi, bitisTarihi),
                EczaneNobetGruplar = eczaneNobetGruplarTumu,
                Kisitlar = nobetUstGrupKisitlar,
                EczaneGrupNobetSonuclar = eczaneGrupNobetSonuclar,
                EczaneNobetSonuclar = eczaneNobetSonuclarCozulenGruplar,
                EczaneGrupNobetSonuclarTumu = eczaneNobetSonuclar,
                EczaneNobetSonuclarAltGruplarlaBirlikte = eczaneNobetSonuclarAltGruplaAyniGun,
                EczaneNobetGrupGunKuralIstatistikler = enSonNobetler,
                TakvimNobetGrupGunDegerIstatistikler = takvimNobetGrupGunDegerIstatistikler,
                EczaneNobetGrupGunKuralIstatistikYatay = eczaneNobetGrupGunKuralIstatistikYatay,
                EczaneNobetGrupGunKuralIstatistikYataySon3Ay = eczaneNobetGrupGunKuralIstatistikYataySon3Ay,
                EczaneBazliGunKuralIstatistikYatay = eczaneBazliGunKuralIstatistikYatay,
                EczaneNobetGrupAltGruplar = eczaneNobetGrupAltGruplar,
                NobetGrupGorevTipKisitlar = grupBazliKisitlar,
                IkiliEczaneler = ikiliEczaneler,
                DebugYapilacakEczaneler = debugYapilacakEczaneler
            };

            //_eczaneNobetOrtakService.KurallariKontrolEtHaftaIciEnAzEnCok(nobetUstGrupId, eczaneNobetGrupGunKuralIstatistikYatay);
            _eczaneNobetOrtakService.KurallariKontrolEtMazeretIstek(nobetUstGrupId, eczaneNobetMazeretler, eczaneNobetIstekler);
            _eczaneNobetOrtakService.KurallariKontrolEtIstek(nobetUstGrupId, eczaneNobetIstekler, nobetGrupKurallar);

            return giresunDataModel;
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

