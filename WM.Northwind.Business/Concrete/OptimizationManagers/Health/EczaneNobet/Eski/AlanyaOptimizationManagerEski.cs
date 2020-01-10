using System;
using System.Collections.Generic;
using System.Linq;
using WM.Core.Aspects.PostSharp.AutorizationAspects;
using WM.Core.Aspects.PostSharp.LogAspects;
using WM.Core.CrossCuttingConcerns.Logging.Log4Net.Logger;
using WM.Northwind.Business.Abstract.EczaneNobet;
using WM.Northwind.Business.Abstract.Optimization.EczaneNobet;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;
using WM.Northwind.Entities.Concrete.Optimization.EczaneNobet;
using WM.Optimization.Abstract.Health;

namespace WM.Northwind.Business.Concrete.OptimizationManagers.Health.EczaneNobet.Eski
{
    public class AlanyaOptimizationManagerEski : IAlanyaOptimizationServiceEski
    {
        #region ctor
        //private List<EczaneCiftGrup> UcAylikCiftGrupluEczanelerKumulatif;
        private IEczaneGrupService _eczaneGrupService;
        private IEczaneGrupTanimService _eczaneGrupTanimService;
        private IEczaneNobetAlanyaOptimization _eczaneNobetAlanyaOptimization;
        private IEczaneNobetGrupService _eczaneNobetGrupService;
        private IEczaneNobetIstekService _eczaneNobetIstekService;
        private IEczaneNobetMazeretService _eczaneNobetMazeretService;
        private IEczaneNobetOrtakService _eczaneNobetOrtakService;
        private IEczaneNobetSonucAktifService _eczaneNobetSonucAktifService;
        private IEczaneNobetSonucService _eczaneNobetSonucService;
        private IEczaneNobetTekGrupOptimization _eczaneNobetTekGrupOptimization;
        private INobetGrupGorevTipService _nobetGrupGorevTipService;
        private INobetGrupGunKuralService _nobetGrupGunKuralService;
        private INobetGrupKuralService _nobetGrupKuralService;
        private INobetGrupService _nobetGrupService;
        private INobetGrupTalepService _nobetGrupTalepService;
        private INobetUstGrupKisitService _nobetUstGrupKisitService;
        private ITakvimService _takvimService;
        private List<int> sonUcAydaPazarGunuNobetTutanEczaneler;

        public AlanyaOptimizationManagerEski(
            IEczaneGrupService eczaneGrupService,
            IEczaneGrupTanimService eczaneGrupTanimService,
            IEczaneNobetAlanyaOptimization eczaneNobetAlanyaOptimization,
            IEczaneNobetGrupService eczaneNobetGrupService,
            IEczaneNobetIstekService eczaneNobetIstekService,
            IEczaneNobetMazeretService eczaneNobetMazeretService,
            IEczaneNobetOrtakService eczaneNobetOrtakService,
            IEczaneNobetSonucAktifService eczaneNobetSonucAktifService,
            IEczaneNobetTekGrupOptimization eczaneNobetTekGrupOptimization,
            INobetGrupGorevTipService nobetGrupGorevTipService,
            INobetGrupGunKuralService nobetGrupGunKuralService,
            INobetGrupKuralService nobetGrupKuralService,
            INobetGrupService nobetGrupService,
            INobetGrupTalepService nobetGrupTalepService,
            INobetUstGrupKisitService nobetUstGrupKisitService,
            ITakvimService takvimService,
            IEczaneNobetSonucService eczaneNobetSonucService
            )
        {
            _takvimService = takvimService;
            _nobetGrupService = nobetGrupService;
            _eczaneNobetSonucService = eczaneNobetSonucService;
            _eczaneNobetOrtakService = eczaneNobetOrtakService;
            _eczaneNobetMazeretService = eczaneNobetMazeretService;
            _eczaneNobetAlanyaOptimization = eczaneNobetAlanyaOptimization;
            _eczaneNobetTekGrupOptimization = eczaneNobetTekGrupOptimization;
            _eczaneNobetGrupService = eczaneNobetGrupService;
            _eczaneNobetSonucAktifService = eczaneNobetSonucAktifService;
            _eczaneGrupService = eczaneGrupService;
            _eczaneGrupTanimService = eczaneGrupTanimService;
            _eczaneNobetIstekService = eczaneNobetIstekService;
            _nobetGrupKuralService = nobetGrupKuralService;
            _nobetGrupGunKuralService = nobetGrupGunKuralService;
            _nobetGrupGorevTipService = nobetGrupGorevTipService;
            _nobetGrupTalepService = nobetGrupTalepService;
            _nobetUstGrupKisitService = nobetUstGrupKisitService;
            //UcAylikCiftGrupluEczanelerKumulatif = new List<EczaneCiftGrup>();
            sonUcAydaPazarGunuNobetTutanEczaneler = new List<int>();
        }
        #endregion

        /// <summary>
        /// Nöbetlerin tekrar tekrar yazılabilmesi için her çözüm sonrasında 
        /// aktif sonuçlardaki mevcut kayıtlar silinip yerine yeni sonuçlar eklenir.
        /// Eczane Nöbet Çok Grup Data Model için
        /// </summary>
        /// <param name="data"></param>
        public void EczaneNobetCozAktifiGuncelle(AlanyaDataModelEski data)
        {
            var aktifSonuclar = _eczaneNobetSonucAktifService.GetSonuclar2(data.NobetUstGrupId);
            var guncellenecekSonuclar = aktifSonuclar.Where(x => data.NobetGruplar.Select(s => s.Id).Contains(x.NobetGrupId));
            var yeniSonuclar = _eczaneNobetAlanyaOptimization.Solve(data);

            #region çözüm süresi
            var timeSpan = new TimeSpan();
            if (yeniSonuclar.ResultModel.Count > 0)
            {
                timeSpan = yeniSonuclar.CozumSuresi;
                var amacFonksiyonu = yeniSonuclar.ObjectiveValue;
            }
            #endregion

            //gelen datadaki nöbet grup id aktif sonuçlarda varsa o nöbet gruba ait önceki sonuçları sil
            AktiftekiArtiklariSil(data.NobetUstGrupId);

            //yeni sonuçları ekle           
            _eczaneNobetSonucAktifService.CokluEkle(yeniSonuclar.ResultModel);

            #region aynı gün nöbet tutan eczaneler için
            //çözülen grubun sonuçları
            aktifSonuclar = _eczaneNobetSonucAktifService.GetSonuclar2(data.NobetUstGrupId);

            //aynı aydaki diğer grupların sonuçları
            var ayniAydakiDigerGruplarinSonuclari = _eczaneNobetSonucService.GetSonuclar(data.BaslangicTarihi, data.BitisTarihi, data.NobetUstGrupId);
            var ayIcindekiTumSonuclar = aktifSonuclar.Union(ayniAydakiDigerGruplarinSonuclari).ToList();
            var ayIcindeCozulenNobetGruplar = ayIcindekiTumSonuclar.Select(s => s.NobetGrupId).Distinct();
            var ayIcindeAyniGunNobet = _nobetUstGrupKisitService.GetKisitPasifMi("ayIcindeAyniGunNobet", data.NobetUstGrupId);

            if (ayIcindeAyniGunNobet && ayIcindeCozulenNobetGruplar.Count() > 1)
            {
                var ayIcindeAyniGunNobetTutanEczaneler = new List<EczaneCiftGrup>();

                ayIcindeAyniGunNobetTutanEczaneler = _eczaneNobetOrtakService.GetCiftGrupluEczaneler(ayIcindekiTumSonuclar, 2);
                var indisId = data.AyIcindeAyniGunNobetTutanEczaneler.Select(s => s.Id).LastOrDefault();

                foreach (var item in ayIcindeAyniGunNobetTutanEczaneler)
                {
                    data.AyIcindeAyniGunNobetTutanEczaneler
                        .Add(new EczaneCiftGrup
                        {
                            Id = indisId + item.Id,
                            EczaneId = item.EczaneId,
                            BirlikteNobetTutmaSayisi = item.BirlikteNobetTutmaSayisi
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

        /// <summary>
        /// eğer başka nöbet gruplarından kalan data varsa siler.
        /// --artık kayıtlar--
        /// </summary>
        public void AktiftekiArtiklariSil(int nobetUstGrupId)
        {
            var mevcutSonuclar = _eczaneNobetSonucAktifService.GetSonuclar2(nobetUstGrupId)
                      .Select(s => s.Id).ToArray();

            //foreach (var mevcutSonuc in mevcutSonuclar)
            //{
            //    _eczaneNobetSonucAktifService.Delete(mevcutSonuc.Id);
            //}

            _eczaneNobetSonucAktifService.CokluSil(mevcutSonuclar);
        }

        /// <summary>
        /// Eczane nöbet sonuc aktifteki tüm kayıtları alıp eczane nöbet sonuçlar tablosuna ekler.
        /// </summary>
        [LogAspect(typeof(DatabaseLogger))]
        public void Kesinlestir(int nobetUstGrupId)
        {
            var eczaneNobetSonucAktifler = _eczaneNobetSonucAktifService.GetSonuclar2(nobetUstGrupId);

            foreach (var aktifSonuc in eczaneNobetSonucAktifler)
            {
                var insertEntity = new EczaneNobetSonuc
                {
                    EczaneNobetGrupId = aktifSonuc.EczaneNobetGrupId,
                    TakvimId = aktifSonuc.TakvimId,
                    NobetGorevTipId = aktifSonuc.NobetGorevTipId
                };
                _eczaneNobetSonucService.Insert(insertEntity);
            }
        }

        private AlanyaDataModelEski EczaneNobetDataModel(EczaneNobetDataModelParametre eczaneNobetDataModelParametre)
        {
            #region parametreler
            var nobetUstGrupId = eczaneNobetDataModelParametre.NobetUstGrupId;
            var yilBaslangic = eczaneNobetDataModelParametre.YilBaslangic;
            var yilBitis = eczaneNobetDataModelParametre.YilBitis;
            var ayBaslangic = eczaneNobetDataModelParametre.AyBaslangic;
            var ayBitis = eczaneNobetDataModelParametre.AyBitis;
            var nobetGrupIdListe = eczaneNobetDataModelParametre.NobetGrupId.ToList();
            var nobetGorevTipId = eczaneNobetDataModelParametre.NobetGorevTipId;
            #endregion

            var baslangicTarihi = new DateTime(yilBitis, ayBitis, 1);
            var bitisTarihi = baslangicTarihi.AddMonths(1);
            var nobetGruplar = _nobetGrupService.GetList(nobetGrupIdListe).OrderBy(s => s.Id).ToList();
            var eczaneNobetMazeretNobettenDusenler = _eczaneNobetMazeretService.GetEczaneNobetMazeretSayilari(baslangicTarihi, bitisTarihi, nobetGrupIdListe);
            var eczaneNobetSonuclarAylik = _eczaneNobetSonucService.GetSonuclar(baslangicTarihi, bitisTarihi, nobetUstGrupId);

            var eczaneGrupEdges = _eczaneGrupService.GetEdges(nobetUstGrupId)
                .Where(e => (nobetGrupIdListe.Contains(e.FromNobetGrupId) || nobetGrupIdListe.Contains(e.ToNobetGrupId)))
                .Where(w => (eczaneNobetSonuclarAylik.Select(s => s.EczaneId).Distinct().Contains(w.From) || eczaneNobetSonuclarAylik.Select(s => s.EczaneId).Distinct().Contains(w.To)))
                .ToList();

            //sonuclarda ilişkili eczaneler
            var eczaneGruplar = _eczaneGrupService.GetDetaylar(nobetUstGrupId)
                .Where(x => x.EczaneGrupTanimBitisTarihi == null
                         //&& x.EczaneGrupTanimTipId == 2 //coğrafi yakınlık hariç
                         && nobetGrupIdListe.Contains(x.NobetGrupId)
                         && (eczaneGrupEdges.Select(s => s.From).Distinct().Contains(x.EczaneId) || eczaneGrupEdges.Select(s => s.To).Distinct().Contains(x.EczaneId))
                         ).ToList();

            //fazladan gelen tanımlar var. burayı iyileştirmekte fayda var
            var eczaneGrupTanimlar = _eczaneGrupTanimService.GetAktifTanimList(eczaneGruplar.Select(x => x.EczaneGrupTanimId).ToList());
            var eczaneGruplar2 = _eczaneGrupService.GetDetaylarByEczaneGrupTanimId(eczaneGrupTanimlar.Select(s => s.Id).ToList());

            var eczaneGrupNobetSonuclar = eczaneNobetSonuclarAylik
                .Where(w => eczaneGruplar2.Select(s => s.EczaneId).Contains(w.EczaneId)).ToList();

            var esGrupluEczaneler = _eczaneNobetGrupService.GetAktifEczaneGrupList(nobetGrupIdListe)
                .Where(x => !eczaneNobetMazeretNobettenDusenler.Select(s => s.EczaneId).Contains(x.EczaneId))
                .Select(x => x.EczaneId).Distinct().ToList();

            var eczaneNobetGruplar = _eczaneNobetGrupService.GetAktifEczaneGrupList(nobetGruplar.Select(w => w.Id).ToList())
                .Where(s => !eczaneNobetMazeretNobettenDusenler.Select(f => f.EczaneId).Contains(s.EczaneId)).ToList();

            #region aynı gün nöbet tutan eczaneler

            //nöbet yazılacak tarih aralığı(örn. Ocak ayının tüm günleri)
            //int cozumOncekiAyi = GetOncekiAy(periyot: 3, cozumAyi: ayBitis);
            //var cozumOncekiIkiAyi = GetOncekiAy(periyot: 2, cozumAyi: ayBitis);
            //var aktifSonuclar = _eczaneNobetSonucAktifService.GetSonuclarAylik(yilBitis, ayBitis, nobetUstGrupId);
            //var ayniAydakiDigerGruplarinSonuclari = _eczaneNobetSonucService.GetSonuclarAylik(yilBitis, ayBitis, nobetUstGrupId);
            //var ayIcindekiTumSonuclar = aktifSonuclar.Union(ayniAydakiDigerGruplarinSonuclari).ToList();
            //var ayIcindeCozulenNobetGruplar = ayIcindekiTumSonuclar.Select(s => s.NobetGrupId).Distinct().ToList();

            #region son iki ay

            //var sonIkiAyBakilacakGruplar = new List<int>();

            //foreach (var item in ayIcindeCozulenNobetGruplar)
            //{
            //    sonIkiAyBakilacakGruplar.Add(item);
            //}

            ////şimdi çözülecek grup
            //foreach (var item in nobetGrupIdListe)
            //{
            //    sonIkiAyBakilacakGruplar.Add(item);
            //}

            //var sonIkiAyAyniGunNobetTutanEczaneler = new List<EczaneCiftGrup>();
            //var gruplar = sonIkiAyBakilacakGruplar.Distinct().ToList();

            //if (_nobetUstGrupKisitService.GetKisitPasifMi("sonIkiAydakiGrup", nobetUstGrupId) && sonIkiAyBakilacakGruplar.Count() > 1 && ayBitis > 1)
            //{
            //    var sonIkiAydakiSonuclar = _eczaneNobetSonucService.GetSonuclar(yilBitis, cozumOncekiAyi, ayBitis - 1, nobetUstGrupId)
            //     .Where(x => gruplar.Contains(x.NobetGrupId)).ToList();

            //    sonIkiAyAyniGunNobetTutanEczaneler = _eczaneNobetOrtakService.GetCiftGrupluEczaneler(sonIkiAydakiSonuclar, 1);
            //}
            #endregion

            #region yillik kümülatif

            //var yilIcindeAyniGunNobetTutanEczaneler = new List<EczaneCiftGrup>();

            //if (_nobetUstGrupKisitService.GetKisitPasifMi("yildaEncokUcKezGrup", nobetUstGrupId) && sonIkiAyBakilacakGruplar.Count() > 1 && ayBitis > 2)
            //{
            //    var yillikKumulatifSonuclar = _eczaneNobetSonucService.GetSonuclarYillikKumulatif(yilBitis, ayBitis - 1, nobetUstGrupId)
            //        .Where(x => gruplar.Contains(x.NobetGrupId)).ToList();

            //    yilIcindeAyniGunNobetTutanEczaneler = _eczaneNobetOrtakService.GetCiftGrupluEczaneler(yillikKumulatifSonuclar, 3);
            //}
            #endregion

            //bu alan EczaneNobetCozAktifiGuncelle içinde kullanılıyor.
            var ayIcindeAyniGunNobetTutanEczaneler = new List<EczaneCiftGrup>();

            #endregion

            var alanyaDataModel = new AlanyaDataModelEski()
            {
                Yil = yilBitis,
                Ay = ayBitis,
                LowerBound = 0,
                UpperBound = 1,
                NobetUstGrupId = nobetUstGrupId,
                NobetGruplar = nobetGruplar,
                EczaneNobetTarihAralik = _takvimService.GetEczaneNobetTarihAralik(baslangicTarihi, bitisTarihi, nobetGorevTipId, nobetGrupIdListe), //karar değişkeni
                EczaneKumulatifHedefler = _takvimService.GetEczaneKumulatifHedefler(yilBaslangic, yilBitis, ayBaslangic, ayBitis, nobetGrupIdListe, nobetGorevTipId),
                EczaneNobetMazeretListe = _eczaneNobetMazeretService.GetDetaylar(yilBitis, ayBitis, esGrupluEczaneler),
                EczaneGrupTanimlar = eczaneGrupTanimlar,
                TarihAraligi = _takvimService.GetTakvimNobetGruplar(baslangicTarihi, bitisTarihi, nobetGrupIdListe, nobetGorevTipId),
                EczaneGruplar = eczaneGruplar2,
                EczaneNobetIstekListe = _eczaneNobetIstekService.GetDetaylarByNobetGrupIdList(yilBitis, ayBitis, nobetGrupIdListe),
                NobetGrupGunKurallar = _nobetGrupGunKuralService.GetAktifList(nobetGrupIdListe),
                NobetGrupKurallar = _nobetGrupKuralService.GetDetaylar(nobetGrupIdListe),
                NobetGrupGorevTipler = _nobetGrupGorevTipService.GetList(nobetGorevTipId, nobetGrupIdListe),
                NobetGrupTalepler = _nobetGrupTalepService.GetDetaylar(nobetGrupIdListe, baslangicTarihi, bitisTarihi),
                EczaneNobetGruplar = eczaneNobetGruplar,
                NobetUstGrupKisitlar = _nobetUstGrupKisitService.GetDetaylar(nobetUstGrupId),
                EczaneGrupNobetSonuclar = eczaneGrupNobetSonuclar,
                EczaneNobetSonuclar = _eczaneNobetSonucService.GetSonuclar(nobetUstGrupId),

                AyIcindeAyniGunNobetTutanEczaneler = ayIcindeAyniGunNobetTutanEczaneler,
                YilIcindeAyniGunNobetTutanEczaneler = new List<EczaneCiftGrup>(), //yilIcindeAyniGunNobetTutanEczaneler,
                SonIkiAyAyniGunNobetTutanEczaneler = new List<EczaneCiftGrup>(), //sonIkiAyAyniGunNobetTutanEczaneler,
                EczaneNobetSonuclarSonIkiAy = new List<EczaneNobetSonucListe>(), //_eczaneNobetSonucService.GetSonuclar(yilBitis, cozumOncekiIkiAyi, ayBitis, nobetUstGrupId),
                EczaneNobetSonuclarOncekiAylar = new List<EczaneNobetSonucListe>() //_eczaneNobetSonucService.GetSonuclar(yilBitis, ayBaslangic, ayBitis, nobetUstGrupId)
            };
            return alanyaDataModel;
        }

        [LogAspect(typeof(DatabaseLogger))]
        [SecuredOperation(Roles = "Admin,Oda,Üst Grup")]
        public void ModelCoz(EczaneNobetModelCoz eczaneNobetModelCoz)
        {
            var nobetGruplar = _nobetGrupService.GetListByNobetUstGrupId(eczaneNobetModelCoz.NobetUstGrupId);

            if (nobetGruplar.Count > 0)
            {
                #region başka gruplarla ilişkisi olan gruplar
                var eczaneGrupDetaylar = _eczaneGrupService.GetDetaylar()
                    .Where(d => d.EczaneGrupTanimBitisTarihi == null
                     && nobetGruplar.Select(s => s.Id).Contains(d.NobetGrupId)).ToList();

                //başka gruplarla ilişkisi olan gruplar
                var esGruplar = eczaneGrupDetaylar.Select(x => x.NobetGrupId).Distinct().OrderBy(x => x).ToList();

                //Birbiri ile ilişkili grupların gruplanması
                var esliNobetGruplar = _eczaneGrupService.EsGrupluEczanelerinGruplariniBelirle(eczaneGrupDetaylar, esGruplar)
                    .Where(w => eczaneNobetModelCoz.NobetGrupId.Contains(w.NobetGrupId)).ToList();
                //.Where(w => w.NobetGrupId == eczaneNobetModelCoz.NobetGrupId || eczaneNobetModelCoz.NobetGrupId.Contains(0)).ToList();
                #endregion

                #region başka gruplarla ilişkisi olmayan gruplar
                var tekEczaneNobetGruplar = _eczaneNobetGrupService.GetDetaylar(eczaneNobetModelCoz.NobetUstGrupId)
                    .Where(x => !esGruplar.Contains(x.NobetGrupId)).ToList();

                var tekliNobetGruplar = tekEczaneNobetGruplar
                    .Where(d => nobetGruplar.Select(s => s.Id).Contains(d.NobetGrupId))
                    .Select(c => c.NobetGrupId).Distinct().ToList();
                #endregion

                #region tüm nöbet gruplar
                var tumNobetGruplar = new List<NobetBagGrup>();

                foreach (var esliNobetGrup in esliNobetGruplar.Distinct())
                {
                    tumNobetGruplar.Add(new NobetBagGrup
                    {
                        Id = esliNobetGrup.Id,
                        NobetGrupId = esliNobetGrup.NobetGrupId
                    });
                }

                var indis = tumNobetGruplar.Select(s => s.Id).LastOrDefault();
                foreach (var tekliNobetGrup in tekliNobetGruplar.Distinct())
                {
                    indis++;
                    tumNobetGruplar.Add(new NobetBagGrup
                    {
                        Id = indis,
                        NobetGrupId = tekliNobetGrup
                    });
                }

                var nobetGrupTanimlar = tumNobetGruplar.Select(s => s.Id).Distinct().ToList();
                #endregion

                #region Karar kuralları
                var model = new EczaneNobetDataModelParametre
                {
                    //AyBaslangic = eczaneNobetModelCoz.AyBaslangic,
                    AyBitis = eczaneNobetModelCoz.AyBitis,
                    NobetGorevTipId = eczaneNobetModelCoz.NobetGorevTipId,
                    NobetGrupId = eczaneNobetModelCoz.NobetGrupId,
                    NobetUstGrupId = eczaneNobetModelCoz.NobetUstGrupId,
                    //YilBaslangic = eczaneNobetModelCoz.YilBaslangic,
                    //YilBitis = eczaneNobetModelCoz.YilBitis
                };

                if (eczaneNobetModelCoz.NobetGrupId.Contains(0) && eczaneNobetModelCoz.AyBitis == 0)
                {//ay ve gruplar --tümü--
                    foreach (var ay in _takvimService.GetAylar())
                    {
                        foreach (var item in nobetGrupTanimlar)
                        {
                            var r = new Random();
                            var liste = tumNobetGruplar
                                .Where(x => x.Id == item)
                                .Select(s => s.NobetGrupId)
                                .OrderByDescending(x => r.NextDouble()).ToList();

                            foreach (var item2 in liste)
                            {
                                var liste2 = new int[] { item2 };
                                model.AyBitis = ay.Id;
                                model.NobetGrupId = liste2;
                                var data = EczaneNobetDataModel(model);
                                EczaneNobetCozAktifiGuncelle(data);
                                Kesinlestir(model.NobetUstGrupId);
                            }
                        }
                    }
                }
                else if (eczaneNobetModelCoz.NobetGrupId.Contains(0))
                {//ay seçili, gruplar --tümü--
                    if (eczaneNobetModelCoz.BuAyVeSonrasi)
                    {
                        var aylar = _takvimService.GetAylar()
                            .Where(w => w.Id >= eczaneNobetModelCoz.AyBitis).ToList();

                        foreach (var ay in aylar)
                        {
                            foreach (var item in nobetGrupTanimlar)
                            {
                                var r = new Random();
                                var liste = tumNobetGruplar
                                    .Where(x => x.Id == item)
                                    .Select(s => s.NobetGrupId)
                                    .OrderByDescending(x => r.NextDouble()).ToList();

                                foreach (var item2 in liste)
                                {
                                    var liste2 = new int[] { item2 };
                                    model.AyBitis = ay.Id;
                                    model.NobetGrupId = liste2;
                                    var data = EczaneNobetDataModel(model);
                                    EczaneNobetCozAktifiGuncelle(data);
                                    Kesinlestir(model.NobetUstGrupId);
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (var item in nobetGrupTanimlar)
                        {
                            var liste = tumNobetGruplar
                                .Where(x => x.Id == item)
                                .Select(s => s.NobetGrupId)
                                .OrderByDescending(x => x).ToList();

                            foreach (var item2 in liste)
                            {
                                var liste2 = new int[] { item2 };
                                model.NobetGrupId = liste2;
                                var data = EczaneNobetDataModel(model);
                                EczaneNobetCozAktifiGuncelle(data);
                                Kesinlestir(model.NobetUstGrupId);
                            }
                        }
                    }
                }
                else if (eczaneNobetModelCoz.AyBitis == 0)
                {//grup seçili, aylar --tümü--
                    foreach (var ay in _takvimService.GetAylar())
                    {
                        foreach (var item in tumNobetGruplar
                            .Where(x => eczaneNobetModelCoz.NobetGrupId.Contains(x.NobetGrupId))
                            //.Where(x => x.NobetGrupId == eczaneNobetModelCoz.NobetGrupId)
                            .Select(s => s.Id).Distinct().ToList())
                        {
                            var liste = tumNobetGruplar
                                .Where(x => x.Id == item)
                                .Select(s => s.NobetGrupId)
                                .OrderByDescending(x => x).ToList();

                            foreach (var item2 in liste)
                            {
                                var liste2 = new int[] { item2 };
                                model.AyBitis = ay.Id;
                                model.NobetGrupId = liste2;
                                var data = EczaneNobetDataModel(model);
                                EczaneNobetCozAktifiGuncelle(data);
                                Kesinlestir(model.NobetUstGrupId);
                            }
                        }
                    }
                }
                else
                {//seçili grup ve ay
                    if (eczaneNobetModelCoz.BuAyVeSonrasi)
                    {
                        var aylar = _takvimService.GetAylar()
                            .Where(w => w.Id >= eczaneNobetModelCoz.AyBitis).ToList();

                        foreach (var ay in aylar)
                        {
                            foreach (var item in tumNobetGruplar
                                .Where(x => eczaneNobetModelCoz.NobetGrupId.Contains(x.NobetGrupId))
                                //.Where(x => x.NobetGrupId == eczaneNobetModelCoz.NobetGrupId)
                                .Select(s => s.Id).Distinct().ToList())
                            {
                                var liste = tumNobetGruplar
                                    .Where(x => x.Id == item)
                                    .Select(s => s.NobetGrupId)
                                    .OrderByDescending(x => x).ToArray();

                                model.AyBitis = ay.Id;
                                model.NobetGrupId = liste;
                                var data = EczaneNobetDataModel(model);
                                EczaneNobetCozAktifiGuncelle(data);
                                Kesinlestir(model.NobetUstGrupId);
                            }
                        }
                    }
                    else
                    {
                        foreach (var item in tumNobetGruplar
                            .Where(x => eczaneNobetModelCoz.NobetGrupId.Contains(x.NobetGrupId))
                            //.Where(x => x.NobetGrupId == eczaneNobetModelCoz.NobetGrupId)
                            .Select(s => s.Id).Distinct().ToList())
                        {
                            var liste = tumNobetGruplar
                                .Where(x => x.Id == item)
                                .Select(s => s.NobetGrupId)
                                .OrderByDescending(x => x).ToArray();

                            model.NobetGrupId = liste;
                            var data = EczaneNobetDataModel(model);
                            EczaneNobetCozAktifiGuncelle(data);
                        }
                    }
                    #endregion
                }
            }
        }
    }
}