using System;
using System.Collections.Generic;
using System.Linq;
//using System.Data.Linq;
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
    public class AlanyaOptimizationManager : IAlanyaOptimizationService
    {
        #region ctor
        private IEczaneGrupService _eczaneGrupService;
        private IEczaneGrupTanimService _eczaneGrupTanimService;
        private IEczaneNobetAlanyaOptimizationV2 _eczaneNobetAlanyaOptimization;
        private IEczaneNobetGrupService _eczaneNobetGrupService;
        private IEczaneNobetGrupAltGrupService _eczaneNobetGrupAltGrupService;
        private IEczaneNobetIstekService _eczaneNobetIstekService;
        private IEczaneNobetMazeretService _eczaneNobetMazeretService;
        private IEczaneNobetOrtakService _eczaneNobetOrtakService;
        private IEczaneNobetSonucAktifService _eczaneNobetSonucAktifService;
        private IEczaneNobetSonucService _eczaneNobetSonucService;
        private INobetGrupGorevTipService _nobetGrupGorevTipService;
        private INobetGrupGorevTipGunKuralService _nobetGrupGorevTipGunKuralService;
        private INobetGrupKuralService _nobetGrupKuralService;
        private INobetGrupService _nobetGrupService;
        private INobetGrupTalepService _nobetGrupTalepService;
        private INobetUstGrupKisitService _nobetUstGrupKisitService;
        private ITakvimService _takvimService;
        private IEczaneNobetMuafiyetService _eczaneNobetMuafiyetService;
        private IAyniGunTutulanNobetService _ayniGunTutulanNobetService;
        private INobetGrupGorevTipKisitService _nobetGrupGorevTipKisitService;
        private IKalibrasyonService _kalibrasyonService;
        private INobetUstGrupGunGrupService _nobetUstGrupGunGrupService;
        private IEczaneNobetKisit _eczaneNobetKisit;
        private IDebugEczaneService _debugEczaneService;

        public AlanyaOptimizationManager(
                    IEczaneGrupService eczaneGrupService,
                    IEczaneGrupTanimService eczaneGrupTanimService,
                    IEczaneNobetAlanyaOptimizationV2 eczaneNobetAlanyaOptimization,
                    IEczaneNobetGrupService eczaneNobetGrupService,
                    IEczaneNobetIstekService eczaneNobetIstekService,
                    IEczaneNobetMazeretService eczaneNobetMazeretService,
                    IEczaneNobetOrtakService eczaneNobetOrtakService,
                    IEczaneNobetSonucAktifService eczaneNobetSonucAktifService,
                    INobetGrupGorevTipService nobetGrupGorevTipService,
                    INobetGrupGorevTipGunKuralService nobetGrupGorevTipGunKuralService,
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
                    INobetUstGrupGunGrupService nobetUstGrupGunGrupService,
                    IEczaneNobetKisit eczaneNobetKisit,
                    IDebugEczaneService debugEczaneService
            )
        {
            _eczaneGrupService = eczaneGrupService;
            _eczaneGrupTanimService = eczaneGrupTanimService;
            _eczaneNobetAlanyaOptimization = eczaneNobetAlanyaOptimization;
            _eczaneNobetGrupService = eczaneNobetGrupService;
            _eczaneNobetIstekService = eczaneNobetIstekService;
            _eczaneNobetMazeretService = eczaneNobetMazeretService;
            _eczaneNobetOrtakService = eczaneNobetOrtakService;
            _eczaneNobetSonucAktifService = eczaneNobetSonucAktifService;
            _eczaneNobetSonucService = eczaneNobetSonucService;
            _nobetGrupGorevTipService = nobetGrupGorevTipService;
            _nobetGrupGorevTipGunKuralService = nobetGrupGorevTipGunKuralService;
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
            _nobetUstGrupGunGrupService = nobetUstGrupGunGrupService;
            _eczaneNobetKisit = eczaneNobetKisit;
            _debugEczaneService = debugEczaneService;
        }
        #endregion

        /// <summary>
        /// Her çözüm sonrasında aktif sonuçlardaki mevcut kayıtlar silinip yerine yeni sonuçlar eklenir.
        /// </summary>
        /// <param name="data"></param>
        //[TransactionScopeAspect]
        public EczaneNobetSonucModel EczaneNobetCozAktifiGuncelle(AlanyaDataModel data)
        {
            //var mevcutSonuclar = _eczaneNobetSonucAktifService.GetSonuclar2(data.NobetUstGrupId);
            var yeniSonuclar = _eczaneNobetAlanyaOptimization.Solve(data);

            //gelen datadaki nöbet grup id aktif sonuçlarda varsa o nöbet gruba ait önceki sonuçları sil
            AktiftekiArtiklariSil(data.NobetUstGrupId);

            //yeni sonuçları ekle
            _eczaneNobetSonucAktifService.CokluEkle(yeniSonuclar.ResultModel);

            return yeniSonuclar;
        }

        public EczaneNobetSonucModel EczaneNobetCozAktifiGuncelleIteratif(AlanyaDataModel data)
        {
            var mevcutSonuclar = _eczaneNobetSonucAktifService.GetSonuclar2(data.NobetUstGrupId);

            var guncellenecekSonuclar = mevcutSonuclar
                .Where(x => data.NobetGruplar.Select(s => s.Id).Contains(x.NobetGrupId))
                .Select(s => s.Id).ToArray();

            var yeniSonuclar = _eczaneNobetAlanyaOptimization.Solve(data);

            //gelen datadaki nöbet grup id aktif sonuçlarda varsa o nöbet gruba ait önceki sonuçları sil
            AktiftekiArtiklariSil(data.NobetUstGrupId);

            //yeni sonuçları ekle
            _eczaneNobetSonucAktifService.CokluEkle(yeniSonuclar.ResultModel);

            #region aynı ay içinde aynı gün nöbet tutan eczaneler için

            //çözülen grubun sonuçları
            mevcutSonuclar = _eczaneNobetSonucAktifService.GetSonuclar2(data.NobetUstGrupId);

            //aynı aydaki diğer grupların sonuçları
            var ayniAydakiDigerGruplarinSonuclari = _eczaneNobetSonucService.GetSonuclar(data.BaslangicTarihi, data.BitisTarihi, data.NobetUstGrupId);
            var ayIcindekiTumSonuclar = mevcutSonuclar.Union(ayniAydakiDigerGruplarinSonuclari).ToList();
            var ayIcindeCozulenNobetGruplar = ayIcindekiTumSonuclar.Select(s => s.NobetGrupId).Distinct();
            var ayIcindeAyniGunNobet = _nobetUstGrupKisitService.GetKisitPasifMi("ayIcindeAyniGunNobet", data.NobetUstGrupId);

            data.EczaneNobetSonuclarAyIci.AddRange(ayIcindekiTumSonuclar);
            //data.EczaneNobetSonuclarAyIci = ayIcindekiTumSonuclar;

            if (ayIcindeAyniGunNobet && ayIcindeCozulenNobetGruplar.Count() > 1)
            {//ayIcindeAyniGunNobet olayı eş grup kısıtına yüklenemez.
                //çünkü ay içinde sadece 2. kez birlikte olmalarının engellenmesi gerekiyor.
                var ayIcindeAyniGunNobetTutanEczaneler = new List<EczaneCiftGrup>();

                ayIcindeAyniGunNobetTutanEczaneler = _eczaneNobetOrtakService.GetCiftGrupluEczaneler(ayIcindekiTumSonuclar, 2);

                var indisId = data.AyIcindeAyniGunNobetTutanEczaneler.Select(s => s.Id).LastOrDefault();

                foreach (var item in ayIcindeAyniGunNobetTutanEczaneler)
                {
                    var eczane = _eczaneNobetGrupService.GetDetayByEczaneId(item.EczaneId);

                    data.AyIcindeAyniGunNobetTutanEczaneler
                        .Add(new EczaneCiftGrup
                        {
                            Id = indisId + item.Id,
                            EczaneId = item.EczaneId,
                            BirlikteNobetTutmaSayisi = item.BirlikteNobetTutmaSayisi,
                            EczaneAdi = eczane.EczaneAdi,
                            NobetGrupAdi = eczane.NobetGrupAdi,
                            NobetUstGrupAdi = eczane.NobetUstGrupAdi,
                            EczaneNobetGrupId = eczane.Id
                        });
                }

                //ay içinde grup olan eczane varsa bu sayı bitene iterasyon yapılıyor 
                if (ayIcindeAyniGunNobetTutanEczaneler.Count > 0)
                {
                    data.CozumItereasyon.IterasyonSayisi++;
                    EczaneNobetCozAktifiGuncelle(data);
                }
            }
            #endregion

            yeniSonuclar.IterasyonSayisi = data.CozumItereasyon.IterasyonSayisi;

            return yeniSonuclar;
        }

        //[TransactionScopeAspect]
        public EczaneNobetSonucModel EczaneNobetCozSonuclaraEkle(AlanyaDataModel data)
        {
            var yeniSonuclar = _eczaneNobetAlanyaOptimization.Solve(data);
            _eczaneNobetSonucService.CokluEkle(yeniSonuclar.ResultModel);

            //var sonuclar = _eczaneNobetSonucService.GetSonuclarUstGrupBaslamaTarihindenSonra(data.NobetUstGrupId);
            //var ayniGunNobetTutanEczaneler = _eczaneNobetOrtakService.GetAyniGunNobetTutanEczaneler(sonuclar);
            //_ayniGunTutulanNobetService.AyniGunNobetTutanlariTabloyaEkle(ayniGunNobetTutanEczaneler);

            return yeniSonuclar;

        }

        public EczaneNobetSonucModel EczaneNobetCozSonuclaraEkleYalin(AlanyaDataModel data)
        {
            var yeniSonuclar = _eczaneNobetAlanyaOptimization.Solve(data);
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

        private int GetOncekiAy(int periyot, int cozumAyi)
        {
            var oncekiAy = 1;

            switch (periyot)
            {
                case 2:
                    //arada 1 ay boşluk
                    oncekiAy = cozumAyi == 1 ? 1 :
                        cozumAyi - 1;
                    break;
                case 3:
                    //arada 2 ay boşluk
                    oncekiAy = cozumAyi == 1 ? 1 :
                        cozumAyi == 2 ? cozumAyi - 1 :
                        cozumAyi - 2;
                    break;
                case 4:
                    //arada 3 ay boşluk
                    oncekiAy = cozumAyi == 1 ? 1 :
                        cozumAyi == 2 ? cozumAyi - 1 :
                        cozumAyi == 3 ? cozumAyi - 2 :
                        cozumAyi - 3;
                    break;
                case 5:
                    //arada 4 ay boşluk
                    oncekiAy = cozumAyi == 1 ? 1 :
                        cozumAyi == 2 ? cozumAyi - 1 :
                        cozumAyi == 3 ? cozumAyi - 2 :
                        cozumAyi == 4 ? cozumAyi - 3 :
                        cozumAyi - 4;
                    break;
                case 6:
                    //arada 5 ay boşluk
                    oncekiAy = cozumAyi == 1 ? 1 :
                        cozumAyi == 2 ? cozumAyi - 1 :
                        cozumAyi == 3 ? cozumAyi - 2 :
                        cozumAyi == 4 ? cozumAyi - 3 :
                        cozumAyi == 5 ? cozumAyi - 4 :
                        cozumAyi - 5;
                    break;
                default:
                    break;
            }

            return oncekiAy;
        }

        private AlanyaDataModel EczaneNobetDataModel(EczaneNobetDataModelParametre eczaneNobetDataModelParametre)
        {
            #region parametreler
            var nobetUstGrupId = eczaneNobetDataModelParametre.NobetUstGrupId;
            var nobetGrupIdListe = eczaneNobetDataModelParametre.NobetGrupId.ToList();
            //var nobetGorevTipId = eczaneNobetDataModelParametre.NobetGorevTipId;
            var nobetUstGrupBaslangicTarihi = eczaneNobetDataModelParametre.NobetUstGrupBaslangicTarihi;
            var baslangicTarihi = eczaneNobetDataModelParametre.BaslangicTarihi;
            var bitisTarihi = eczaneNobetDataModelParametre.BitisTarihi;
            var nobetGrupGorevTipler = eczaneNobetDataModelParametre.NobetGrupGorevTipler;
            var nobetGorevTipId = nobetGrupGorevTipler.FirstOrDefault().NobetGorevTipId;

            #endregion

            if (baslangicTarihi < nobetUstGrupBaslangicTarihi)
                throw new Exception($"</strong>Nöbet başlangıç tarihi ({baslangicTarihi.ToShortDateString()}) üst grup başlama tarihinden ({nobetUstGrupBaslangicTarihi.ToShortDateString()}) küçük olamaz.");

            var debugYapilacakEczaneler = _debugEczaneService.GetDetaylarAktifOlanlar(nobetUstGrupId);

            var nobetGruplar = _nobetGrupService.GetDetaylar(nobetGrupIdListe).OrderBy(s => s.Id).ToList();

            var eczaneNobetSonuclar = _eczaneNobetSonucService.GetSonuclar(nobetUstGrupId); //nobetGrupIdListe

            var eczaneNobetMazeretNobettenDusenler = new List<EczaneNobetMazeretSayilari>();

            var mazeret = _nobetUstGrupKisitService.GetKisitPasifMi("mazeret", nobetUstGrupId);

            if (mazeret)
                eczaneNobetMazeretNobettenDusenler = _eczaneNobetMazeretService.GetEczaneNobetMazeretSayilari(baslangicTarihi, bitisTarihi, nobetGrupIdListe);

            var eczaneNobetGruplar = _eczaneNobetGrupService.GetDetaylar(nobetGrupIdListe, baslangicTarihi, bitisTarihi)
                .Where(w => !eczaneNobetMazeretNobettenDusenler.Select(s => s.EczaneNobetGrupId).Contains(w.Id)
                //&& w.EczaneAdi == "ÖZGÜR"
                ).ToList();

            var eczaneNobetSonuclarOncekiAylar = eczaneNobetSonuclar
                .Where(w => w.Tarih >= nobetUstGrupBaslangicTarihi
                         && eczaneNobetGruplar.Select(s => s.Id).Contains(w.EczaneNobetGrupId)).ToList();

            var eczaneNobetSonuclarCozulenGruplar = eczaneNobetSonuclar
                .Where(w => eczaneNobetGruplar.Select(s => s.Id).Contains(w.EczaneNobetGrupId)).ToList();

            var sonuclarKontrol = _eczaneNobetSonucService.GetSonuclar(baslangicTarihi, bitisTarihi, eczaneNobetSonuclarCozulenGruplar);

            if (sonuclarKontrol.Count > 0)
                throw new Exception("<strong>Kriterlere uygun daha önce yazılmış nöbetler bulunmaktadır. Lütfen kontrol ediniz!</strong>");

            var enSonNobetler = _eczaneNobetOrtakService.GetEczaneNobetGrupGunKuralIstatistik(eczaneNobetGruplar, eczaneNobetSonuclarCozulenGruplar);

            var eczaneNobetGrupGunKuralIstatistikYatay = _eczaneNobetOrtakService.GetEczaneNobetGrupGunKuralIstatistikYatay(enSonNobetler);

            var anahtarListe = eczaneNobetSonuclar
                 .Where(w => w.Tarih < nobetUstGrupBaslangicTarihi && nobetGrupIdListe.Contains(w.NobetGrupId)).ToList();

            var haftaIciAnahtarListeTumEczaneler = _takvimService.AnahtarListeyiBuGuneTasi(nobetGrupIdListe, nobetGorevTipId, nobetUstGrupBaslangicTarihi, eczaneNobetGruplar, eczaneNobetGrupGunKuralIstatistikYatay, anahtarListe, "Hafta İçi");

            var nobetBorcluEczanelerhaftaIci = (from s in eczaneNobetGrupGunKuralIstatistikYatay
                                                from b in haftaIciAnahtarListeTumEczaneler
                                                where s.EczaneNobetGrupId == b.EczaneNobetGrupId
                                                && s.NobetSayisiHaftaIci == b.NobetSayisi
                                                select new
                                                {
                                                    s.EczaneNobetGrupId,
                                                    s.EczaneId,
                                                    s.EczaneAdi,
                                                    s.NobetGrupAdi,
                                                    s.NobetGrupId,
                                                    s.NobetSayisiHaftaIci,
                                                    s.SonNobetTarihiHaftaIci,
                                                    AnahtarTarih = b.Tarih,
                                                    BorcluGunSayisi = s.NobetSayisiHaftaIci > 0
                                                            ? (s.SonNobetTarihiHaftaIci - b.Tarih).TotalDays
                                                            : (s.SonNobetTarihiHaftaIci - b.Tarih).TotalDays - (s.SonNobetTarihiHaftaIci - b.NobetUstGrupBaslamaTarihi).TotalDays,
                                                    GunGrup = "Hafta İçi"
                                                }).ToList();

            foreach (var eczane in nobetBorcluEczanelerhaftaIci)
            {
                eczaneNobetGrupGunKuralIstatistikYatay
                    .Where(w => w.EczaneNobetGrupId == eczane.EczaneNobetGrupId)
                    .FirstOrDefault().BorcluNobetSayisiHaftaIci = (int)eczane.BorcluGunSayisi;
            }

            var grupluEczaneNobetSonuclar = _eczaneNobetSonucService.GetSonuclar(baslangicTarihi, bitisTarihi, eczaneNobetSonuclar);

            //var grupluEczaneNobetSonuclar = eczaneNobetSonuclar
            //    .Where(w => (w.Tarih >= baslangicTarihi && w.Tarih <= bitisTarihi)).ToList();

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
            var tarihAralik = _takvimService.GetTakvimNobetGruplar(baslangicTarihi, bitisTarihi, nobetGrupIdListe, nobetGorevTipId);

            var eczaneKumulatifHedefler = new List<EczaneNobetIstatistik>();
            var eczaneNobetIstatistikler = new List<EczaneNobetIstatistik>();

            var nobetGrupGorevTipGunKurallar = _nobetGrupGorevTipGunKuralService.GetDetaylarAktifList(nobetGrupGorevTipler.Select(s => s.Id).ToList());

            var eczaneNobetTarihAralik1 = _takvimService.GetEczaneNobetTarihAralik(baslangicTarihi, bitisTarihi, nobetGorevTipId, nobetGrupIdListe)
                .Where(w => eczaneNobetGruplar.Select(s => s.EczaneId).Contains(w.EczaneId)).ToList();

            var eczaneNobetTarihAralik = _eczaneNobetOrtakService.AmacFonksiyonuKatsayisiBelirle(eczaneNobetTarihAralik1, eczaneNobetGrupGunKuralIstatistikYatay);

            var eczaneNobetTarihAralikIkiliEczaneler = _takvimService.GetIkiliEczaneNobetTarihAralik(baslangicTarihi, bitisTarihi, nobetGorevTipId, nobetGrupIdListe)
                .Where(w => eczaneNobetGruplar.Select(s => s.EczaneId).Contains(w.EczaneId1)
                         || eczaneNobetGruplar.Select(s => s.EczaneId).Contains(w.EczaneId2)
                ).ToList();

            var ikiliEczaneler = _ayniGunTutulanNobetService.GetDetaylar(nobetGrupIdListe);

            var eczaneNobetIstekler = _eczaneNobetIstekService.GetDetaylarByNobetGrupIdList(baslangicTarihi, bitisTarihi, nobetGrupIdListe)
                .Where(w => eczaneNobetGruplar.Select(s => s.EczaneId).Contains(w.EczaneId)).ToList();

            var eczaneNobetMazeretler = _eczaneNobetMazeretService.GetDetaylarByEczaneIdList(baslangicTarihi, bitisTarihi, eczaneNobetGruplar.Select(s => s.EczaneId).Distinct().ToList())
                  .Where(w => w.MazeretId != 3)
                  .ToList();

            //var takvimNobetGrupGunDegerIstatistikler = _takvimService.GetTakvimNobetGrupGunDegerIstatistikler(nobetUstGrupBaslangicTarihi, bitisTarihi, nobetGrupIdListe, nobetGorevTipId);
            var takvimNobetGrupGunDegerIstatistikler = _takvimService.GetTakvimNobetGrupGunDegerIstatistikler(nobetUstGrupBaslangicTarihi, bitisTarihi, nobetGrupGorevTipler);
            var takvimNobetGrupGunDegerIstatistiklerTarihAralik = _takvimService.GetTakvimNobetGrupGunDegerIstatistikler(baslangicTarihi, bitisTarihi, nobetGrupGorevTipler);

            var eczaneNobetGrupAltGruplar = _eczaneNobetGrupAltGrupService.GetDetaylar(nobetUstGrupId);

            #region aynı gün nöbet tutan eczaneler            

            //bu alan EczaneNobetCozAktifiGuncelle içinde kullanılıyor.
            var ayIcindeAyniGunNobetTutanEczaneler = new List<EczaneCiftGrup>();

            #region ikili eczaneler

            //ikiliEczaneAyniGunNobet
            var ikiliEczaneAyniGunNobet = _nobetUstGrupKisitService.GetDetay(45, nobetUstGrupId);
            var arasindaAyniGun2NobetFarkiOlanIkiliEczaneler = new List<EczaneGrupDetay>();

            if (!ikiliEczaneAyniGunNobet.PasifMi && nobetGrupIdListe.Count() > 1)
            {
                arasindaAyniGun2NobetFarkiOlanIkiliEczaneler = _ayniGunTutulanNobetService.GetArasinda2FarkOlanIkiliEczaneleri(
                    eczaneNobetGruplar,
                    nobetGrupGorevTipler.Select(s => s.Id).ToArray(),
                    (int)ikiliEczaneAyniGunNobet.SagTarafDegeri);

                var kontrol = true;

                if (kontrol)
                {
                    //839: bilge
                    var ss1 = arasindaAyniGun2NobetFarkiOlanIkiliEczaneler
                        .Where(w => w.EczaneGrupTanimId == 2839).ToList();

                    var ss2 = arasindaAyniGun2NobetFarkiOlanIkiliEczaneler
                        .Where(w => w.EczaneGrupTanimId == 3839).ToList();
                }
            }

            #endregion

            #region önceki aylar aynı gün nöbet tutanlar çözülen ayda aynı gün nöbetçi olmasın

            var oncekiAylardaAyniGunNobetTutanEczaneler = new List<EczaneGrupDetay>();

            var oncekiAylarAyniGunNobet = _nobetUstGrupKisitService.GetDetay(41, nobetUstGrupId);

            if (!oncekiAylarAyniGunNobet.PasifMi && nobetGrupIdListe.Count() > 1)
            {
                //indisId = eczaneGruplar2.Select(s => s.EczaneGrupTanimId).Max();
                oncekiAylardaAyniGunNobetTutanEczaneler = _eczaneNobetSonucService.OncekiAylardaAyniGunNobetTutanlar(baslangicTarihi,
                    eczaneNobetSonuclarOncekiAylar,
                    0,
                    (int)oncekiAylarAyniGunNobet.SagTarafDegeri);

                var oncekiAylarEczaneGrupTanimlar = oncekiAylardaAyniGunNobetTutanEczaneler.Select(s => new { s.EczaneGrupTanimId, s.EczaneGrupTanimAdi }).Distinct().ToList();

                if (oncekiAylarEczaneGrupTanimlar.Count > 0)
                {
                    var ikililerdenCikacakEczaneler = new List<AyniGunTutulanNobetDetay>();

                    foreach (var item in oncekiAylarEczaneGrupTanimlar)
                    {
                        var gruptakiEczaneler = oncekiAylardaAyniGunNobetTutanEczaneler
                            .Where(w => w.EczaneGrupTanimId == item.EczaneGrupTanimId)
                            .OrderBy(o => o.EczaneId).ToList();

                        foreach (var gruptakiEczane1 in gruptakiEczaneler)
                        {
                            var ikinciListe = gruptakiEczaneler.Where(w => w.EczaneId > gruptakiEczane1.EczaneId).ToList();

                            foreach (var gruptakiEczane2 in ikinciListe)
                            {
                                var eklenecekEczaneler = ikiliEczaneler
                                    .Where(w => (w.EczaneId1 == gruptakiEczane1.EczaneId || w.EczaneId1 == gruptakiEczane2.EczaneId)
                                             && (w.EczaneId2 == gruptakiEczane1.EczaneId || w.EczaneId2 == gruptakiEczane2.EczaneId)).ToList();

                                ikililerdenCikacakEczaneler.AddRange(eklenecekEczaneler);
                            }
                        }
                    }

                    ikiliEczaneler = ikiliEczaneler.Where(w => !ikililerdenCikacakEczaneler.Select(s => s.Id).Contains(w.Id)).ToList();
                }
            }

            #endregion

            var eczaneNobetSonuclarAltGruplaAyniGun = new List<EczaneNobetSonucListe2>();

            var nobetGrupKurallar = _nobetGrupKuralService.GetDetaylar(nobetGrupIdListe);

            #endregion

            var nobetUstGrupKisitlar = _nobetUstGrupKisitService.GetDetaylar(nobetUstGrupId);
            var grupBazliKisitlar = _nobetGrupGorevTipKisitService.GetDetaylar(nobetUstGrupId);

            //if (grupBazliKisitlar.Count > 0)
            //{
            //    foreach (var grupBazliKisit in grupBazliKisitlar)
            //    {
            //        nobetUstGrupKisitlar.SingleOrDefault(w => w.KisitId == grupBazliKisit.KisitId).PasifMi = grupBazliKisit.PasifMi;
            //        nobetUstGrupKisitlar.SingleOrDefault(w => w.KisitId == grupBazliKisit.KisitId).SagTarafDegeri = grupBazliKisit.SagTarafDegeri;
            //    }
            //}
            var gunGruplar = _nobetUstGrupGunGrupService.GetDetaylar(nobetUstGrupId);

            var alanyaDataModel = new AlanyaDataModel()
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
                EczaneNobetTarihAralikIkiliEczaneler = eczaneNobetTarihAralikIkiliEczaneler,
                EczaneKumulatifHedefler = eczaneKumulatifHedefler,//.Where(w => w.EczaneId != 121).ToList(),
                EczaneNobetIstatistikler = eczaneNobetIstatistikler,
                EczaneNobetMazeretler = eczaneNobetMazeretler,
                EczaneGrupTanimlar = eczaneGrupTanimlar,
                TarihAraligi = tarihAralik,
                NobetGruplar = nobetGruplar,
                EczaneGruplar = eczaneGruplar2,
                ArasindaAyniGun2NobetFarkiOlanIkiliEczaneler = arasindaAyniGun2NobetFarkiOlanIkiliEczaneler,
                OncekiAylardaAyniGunNobetTutanEczaneler = oncekiAylardaAyniGunNobetTutanEczaneler,
                EczaneNobetIstekler = eczaneNobetIstekler,
                NobetGrupGorevTipGunKurallar = nobetGrupGorevTipGunKurallar,
                NobetGrupKurallar = nobetGrupKurallar,
                NobetGrupGorevTipler = nobetGrupGorevTipler,
                NobetGrupTalepler = _nobetGrupTalepService.GetDetaylar(nobetGrupIdListe, baslangicTarihi, bitisTarihi),
                EczaneNobetGruplar = eczaneNobetGruplar,
                Kisitlar = nobetUstGrupKisitlar,
                EczaneGrupNobetSonuclar = eczaneGrupNobetSonuclar,
                EczaneNobetSonuclar = eczaneNobetSonuclarCozulenGruplar,
                EczaneGrupNobetSonuclarTumu = eczaneNobetSonuclar,
                EczaneNobetGrupGunKuralIstatistikler = enSonNobetler,
                TakvimNobetGrupGunDegerIstatistikler = takvimNobetGrupGunDegerIstatistikler,
                EczaneNobetGrupGunKuralIstatistikYatay = eczaneNobetGrupGunKuralIstatistikYatay,
                EczaneNobetGrupAltGruplar = eczaneNobetGrupAltGruplar,
                GunGruplar = gunGruplar,
                AyIcindeAyniGunNobetTutanEczaneler = ayIcindeAyniGunNobetTutanEczaneler,
                //YilIcindeAyniGunNobetTutanEczaneler = yilIcindeAyniGunNobetTutanEczaneler,
                //SonIkiAyAyniGunNobetTutanEczaneler = sonIkiAyAyniGunNobetTutanEczaneler,
                EczaneNobetSonuclarAyIci = new List<EczaneNobetSonucListe2>(), //ayIcindekiTumSonuclar,
                //EczaneNobetSonuclarSonIkiAy = sonIkiAydakiSonuclar,
                EczaneNobetSonuclarOncekiAylar = eczaneNobetSonuclarOncekiAylar,

                IkiliEczaneler = ikiliEczaneler,
                NobetGrupGorevTipKisitlar = grupBazliKisitlar,
                Kalibrasyonlar = _kalibrasyonService.GetKalibrasyonlarYatay(nobetUstGrupId),
                DebugYapilacakEczaneler = debugYapilacakEczaneler
            };

            #region kontrol

            //_eczaneNobetOrtakService.KurallariKontrolEtHaftaIciEnAzEnCok(nobetUstGrupId, eczaneNobetGrupGunKuralIstatistikYatay);
            _eczaneNobetOrtakService.KurallariKontrolEtMazeretIstek(nobetUstGrupId, eczaneNobetMazeretler, eczaneNobetIstekler);
            _eczaneNobetOrtakService.KurallariKontrolEtIstek(nobetUstGrupId, eczaneNobetIstekler, nobetGrupKurallar);

            var arzTalepKontrol = false;

            if (arzTalepKontrol)
            {
                foreach (var nobetGrupGorevTip in nobetGrupGorevTipler)
                {
                    var herAyEnFazlaIlgiliKisit = new NobetUstGrupKisitDetay();

                    var kisitlarGrupBazli = grupBazliKisitlar
                        .Where(w => w.NobetGrupGorevTipId == nobetGrupGorevTip.Id).ToList();

                    var kisitlarAktif = _eczaneNobetKisit.GetKisitlarNobetGrupBazli(nobetUstGrupKisitlar, kisitlarGrupBazli);

                    var eczaneNobetTarihAralikGrupBazli = takvimNobetGrupGunDegerIstatistiklerTarihAralik
                        .Where(w => w.NobetGrupGorevTipId == nobetGrupGorevTip.Id).ToList();

                    var taleplerGunGrupBazli = new List<TakvimNobetGrupGunGrupIstatistik>();

                    foreach (var gunGrup in gunGruplar)
                    {
                        var eczaneNobetTarihAralikGunGrupBazli = eczaneNobetTarihAralikGrupBazli
                            .Where(w => w.GunGrupId == gunGrup.GunGrupId).ToList();

                        var taleplerGunGrupBazli1 = eczaneNobetTarihAralikGunGrupBazli
                            .Where(w => w.GunGrupId == gunGrup.GunGrupId)
                            .GroupBy(s => new
                            {
                                s.NobetGrupGorevTipId,
                                s.GunGrupId,
                                s.GunGrupAdi,
                                s.NobetGorevTipAdi,
                                s.NobetGorevTipId,
                                s.NobetGrupAdi,
                                s.NobetGrupId
                            })
                            .Select(s => new TakvimNobetGrupGunGrupIstatistik
                            {
                                NobetGrupGorevTipId = s.Key.NobetGrupGorevTipId,
                                GunGrupId = s.Key.GunGrupId,
                                GunGrupAdi = s.Key.GunGrupAdi,
                                NobetGorevTipAdi = s.Key.NobetGorevTipAdi,
                                NobetGorevTipId = s.Key.NobetGorevTipId,
                                NobetGrupAdi = s.Key.NobetGrupAdi,
                                NobetGrupId = s.Key.NobetGrupId,
                                IstatistikBaslamaTarihi = s.Min(f => f.IstatistikBaslamaTarihi),
                                IstatistikBitisTarihi = s.Max(f => f.IstatistikBitisTarihi),
                                GunSayisi = s.Count(),
                                TalepEdilenNobetciSayisi = s.Sum(f => f.TalepEdilenNobetciSayisi)
                            }).ToList();

                        taleplerGunGrupBazli.AddRange(taleplerGunGrupBazli1);
                    }
                }
            }

            #endregion

            return alanyaDataModel;
        }

        //[TransactionScopeAspect]
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
                    ////var sonuclarTumu = _eczaneNobetSonucAktifService.GetSonuclar2(eczaneNobetModelCoz.NobetUstGrupId);
                    ////var ayniGunNobetTutanEczaneler = _eczaneNobetOrtakService.GetAyniGunNobetTutanEczaneler(sonuclarTumu);
                    ////var ayniGunNobetSayisiGrouped = _eczaneNobetOrtakService.AyniGunTutulanNobetSayisiniHesapla(ayniGunNobetTutanEczaneler);
                    ////_ayniGunTutulanNobetService.AyniGunNobetTutanlariTabloyaEkle(ayniGunNobetSayisiGrouped, azaltilsinMi: false);
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

/*
 #region ikili eczaneler -- eski 17_06_2019 10:06

            //bu alan EczaneNobetCozAktifiGuncelle içinde kullanılıyor.
            var ayIcindeAyniGunNobetTutanEczaneler = new List<EczaneCiftGrup>();

            var arasindaAyniGunNobetFarkiOlanIkiliEczaneler = _nobetUstGrupKisitService.GetDetay(45, nobetUstGrupId);
            var arasindaAyniGun2NobetFarkiOlanIkiliEczaneler = new List<EczaneGrupDetay>();

            var indisId = 0;

            if (!arasindaAyniGunNobetFarkiOlanIkiliEczaneler.PasifMi)
            {
                //indisId = eczaneGruplar2.Select(s => s.EczaneGrupTanimId).Max();
                var ayniGunNobetTutanEczaneler = _eczaneNobetOrtakService.GetAyniGunNobetTutanEczaneler(eczaneNobetSonuclarOncekiAylar);
                var ayniGunNobetSayisiGrouped = _eczaneNobetOrtakService.AyniGunTutulanNobetSayisiniHesapla(ayniGunNobetTutanEczaneler)
                    .Where(w => w.AyniGunNobetSayisi >= arasindaAyniGunNobetFarkiOlanIkiliEczaneler.SagTarafDegeri).ToList();
                
                //var cc = _ayniGunTutulanNobetService.ArasindaKritereGoreFarkOlanEczaneler(eczaneNobetGruplar, ayniGunNobetSayisiGrouped, (int)arasindaAyniGunNobetFarkiOlanIkiliEczaneler.SagTarafDegeri)
                //bu koda bakılacak. 

                foreach (var ayniGunNobet in ayniGunNobetSayisiGrouped)
                {
                    var bakilanEczane1 = _eczaneNobetGrupService.GetDetayById(ayniGunNobet.EczaneNobetGrupId1);

                    arasindaAyniGun2NobetFarkiOlanIkiliEczaneler.Add(new EczaneGrupDetay
                    {
                        EczaneGrupTanimId = Convert.ToInt32(ayniGunNobet.EczaneBirlesim),
                        EczaneId = bakilanEczane1.EczaneId,
                        ArdisikNobetSayisi = 0,
                        NobetUstGrupId = nobetUstGrupId,
                        EczaneGrupTanimAdi = $"{ayniGunNobet.EczaneAdi1}-{ayniGunNobet.EczaneAdi2}",
                        EczaneGrupTanimTipAdi = "Tüm eczanelerle aynı gün nöbet",
                        EczaneGrupTanimTipId = -10, //-2,
                        NobetGrupId = bakilanEczane1.NobetGrupId,
                        EczaneAdi = bakilanEczane1.EczaneAdi,
                        NobetGrupAdi = bakilanEczane1.NobetGrupAdi,
                        EczaneNobetGrupId = bakilanEczane1.Id,
                        AyniGunNobetTutabilecekEczaneSayisi = 1
                        //BirlikteNobetTutmaSayisi = item.BirlikteNobetTutmaSayisi
                    });

                    var bakilanEczane2 = _eczaneNobetGrupService.GetDetayById(ayniGunNobet.EczaneNobetGrupId2);

                    arasindaAyniGun2NobetFarkiOlanIkiliEczaneler.Add(new EczaneGrupDetay
                    {
                        EczaneGrupTanimId = Convert.ToInt32(ayniGunNobet.EczaneBirlesim),
                        EczaneId = bakilanEczane2.EczaneId,
                        ArdisikNobetSayisi = 0,
                        NobetUstGrupId = nobetUstGrupId,
                        EczaneGrupTanimAdi = $"{ayniGunNobet.EczaneAdi1}-{ayniGunNobet.EczaneAdi2}",
                        EczaneGrupTanimTipAdi = "Tüm eczanelerle aynı gün nöbet",
                        EczaneGrupTanimTipId = -10, //-2,
                        NobetGrupId = bakilanEczane2.NobetGrupId,
                        EczaneAdi = bakilanEczane2.EczaneAdi,
                        NobetGrupAdi = bakilanEczane2.NobetGrupAdi,
                        EczaneNobetGrupId = bakilanEczane2.Id,
                        AyniGunNobetTutabilecekEczaneSayisi = 1
                        //BirlikteNobetTutmaSayisi = item.BirlikteNobetTutmaSayisi
                    });
                }

                //arasindaAyniGun2NobetFarkiOlanIkiliEczaneler = _ayniGunTutulanNobetService.GetArasinda2FarkOlanIkiliEczaneleri(eczaneNobetGruplar, nobetUstGrupId, (int)arasindaAyniGunNobetFarkiOlanIkiliEczaneler.SagTarafDegeri);
            }

            #endregion
     */
