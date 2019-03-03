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

namespace WM.Northwind.Business.Concrete.OptimizationManagers.Health.EczaneNobet.Eski
{
    public class MersinMerkezOptimizationManagerV2 : IMersinMerkezOptimizationServiceV2
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

        public MersinMerkezOptimizationManagerV2(
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
                    IEczaneNobetGrupAltGrupService eczaneNobetGrupAltGrupService
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
        }
        #endregion

        /// <summary>
        /// Her çözüm sonrasında aktif sonuçlardaki mevcut kayıtlar silinip yerine yeni sonuçlar eklenir.
        /// </summary>
        /// <param name="data"></param>
        public EczaneNobetSonucModel EczaneNobetCozAktifiGuncelle(MersinMerkezDataModelV2 data)
        {
            var mevcutSonuclar = _eczaneNobetSonucAktifService.GetDetaylar2(data.NobetUstGrupId);
            var guncellenecekSonuclar = mevcutSonuclar
                .Where(x => data.NobetGruplar.Select(s => s.Id).Contains(x.NobetGrupId))
                .Select(s => s.Id).ToArray();

            var yeniSonuclar = _eczaneNobetMersinMerkezOptimizationV2.Solve(data);
            _eczaneNobetSonucAktifService.CokluSil(guncellenecekSonuclar);

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
            var nobetGorevTipId = eczaneNobetDataModelParametre.NobetGorevTipId;
            var nobetUstGrupBaslangicTarihi = eczaneNobetDataModelParametre.NobetUstGrupBaslangicTarihi;
            var ayBitisBaslangicGunu = eczaneNobetDataModelParametre.AyBitisBaslangicGunu;
            var ayBitisBitisGunu = eczaneNobetDataModelParametre.AyBitisBitisGunu;

            var baslangicTarihi = eczaneNobetDataModelParametre.BaslangicTarihi;
            var bitisTarihi = eczaneNobetDataModelParametre.BitisTarihi;
            #endregion

            #region başlangıç bitiş tarihleri
            //int aydakiGunSayisi = DateTime.DaysInMonth(yilBitis, ayBitis);
            //var baslangicTarihi = new DateTime(yilBitis, ayBitis, 1);
            //var bitisTarihi = new DateTime(yilBitis, ayBitis, aydakiGunSayisi);

            //if (!eczaneNobetDataModelParametre.BuAyVeSonrasi)
            //{
            //    if (ayBitisBaslangicGunu > 1)
            //    {
            //        baslangicTarihi = new DateTime(yilBitis, ayBitis, ayBitisBaslangicGunu);
            //    }

            //    if (ayBitisBitisGunu < aydakiGunSayisi)
            //    {
            //        bitisTarihi = new DateTime(yilBitis, ayBitis, ayBitisBitisGunu);
            //    }
            //}
            //özel tarih aralığı
            //baslangicTarihi = new DateTime(yilBitis, 4, 16);
            //bitisTarihi = new DateTime(yilBitis, 7, 8);
            #endregion

            var nobetGruplar = _nobetGrupService.GetDetaylar(nobetGrupIdListe).OrderBy(s => s.Id).ToList();
            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetGorevTipId, nobetGrupIdListe);            
            var eczaneNobetSonuclar = _eczaneNobetSonucService.GetSonuclar(nobetUstGrupId); //nobetGrupIdListe
            //.Where(w => !(w.EczaneNobetGrupId == 301 && w.TakvimId == 88)).ToList();
            //.Where(w => w.Tarih.Year > 2017).ToList();

            var eczaneNobetMazeretNobettenDusenler = _eczaneNobetMazeretService.GetEczaneNobetMazeretSayilari(baslangicTarihi, bitisTarihi, nobetGrupIdListe);

            var eczaneNobetGruplar = _eczaneNobetGrupService.GetDetaylar(nobetGrupIdListe, baslangicTarihi, bitisTarihi)
                .Where(w => !eczaneNobetMazeretNobettenDusenler.Select(s => s.EczaneNobetGrupId).Contains(w.Id)).ToList();

            var eczaneNobetSonuclarCozulenGrup = eczaneNobetSonuclar
                .Where(w => !eczaneNobetMazeretNobettenDusenler.Select(s => s.EczaneNobetGrupId).Contains(w.EczaneNobetGrupId)
                         && nobetGrupIdListe.Contains(w.NobetGrupId)).ToList();

            var enSonNobetler = _eczaneNobetOrtakService.GetEczaneNobetGrupGunKuralIstatistik(eczaneNobetGruplar, eczaneNobetSonuclarCozulenGrup);

            var eczaneNobetGrupGunKuralIstatistikYatay = _eczaneNobetOrtakService.GetEczaneNobetGrupGunKuralIstatistikYatay(enSonNobetler);

            var grupluEczaneNobetSonuclar = eczaneNobetSonuclar
                .Where(w => (w.Tarih >= baslangicTarihi && w.Tarih <= bitisTarihi)).ToList();

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
            //_takvimService.GetEczaneKumulatifHedefler(yilBaslangic, yilBitis, ayBaslangic, ayBitis, nobetGrupIdListe, nobetGorevTipId)
            //.Where(w => eczaneNobetGruplar.Select(s => s.EczaneId).Contains(w.EczaneId)).ToList();

            var eczaneNobetIstatistikler = new List<EczaneNobetIstatistik>();
            //_eczaneNobetSonucService.GetEczaneNobetIstatistik2(nobetGrupIdListe)
            //.Where(w => eczaneNobetGruplar.Select(s => s.EczaneId).Contains(w.EczaneId)).ToList();

            var eczaneNobetTarihAralik = _takvimService.GetEczaneNobetTarihAralik(baslangicTarihi, bitisTarihi, nobetGorevTipId, nobetGrupIdListe)
                .Where(w => eczaneNobetGruplar.Select(s => s.EczaneId).Contains(w.EczaneId)).ToList();

            //2. karar değişkeni. her eczane ve ilgili altgrup
            var eczaneNobetAltGrupTarihAralik = _takvimService.GetEczaneNobetAltGrupTarihAralik(baslangicTarihi, bitisTarihi, nobetGorevTipId, nobetGrupIdListe)
                .Where(w => eczaneNobetGruplar.Select(s => s.EczaneId).Contains(w.EczaneId)).ToList();

            var eczaneNobetIstekler = _eczaneNobetIstekService.GetDetaylarByNobetGrupIdList(baslangicTarihi, bitisTarihi.AddDays(10), nobetGrupIdListe)
                .Where(w => eczaneNobetGruplar.Select(s => s.EczaneId).Contains(w.EczaneId)).ToList();

            var eczaneNobetMazeretler = _eczaneNobetMazeretService.GetDetaylarByEczaneIdList(baslangicTarihi, bitisTarihi, eczaneNobetGruplar.Select(s => s.EczaneId).Distinct().ToList())
                .Where(w => w.MazeretId != 3)
                .ToList();

            var takvimNobetGrupGunDegerIstatistikler = _takvimService.GetTakvimNobetGrupGunDegerIstatistikler(nobetUstGrupBaslangicTarihi, bitisTarihi, nobetGrupIdListe, nobetGorevTipId);

            var eczaneNobetGrupAltGruplar = _eczaneNobetGrupAltGrupService.GetDetaylar(nobetUstGrupId);

            var mersinMerkezDataModel = new MersinMerkezDataModelV2()
            {
                Yil = yilBitis,
                Ay = ayBitis,
                LowerBound = 0,
                UpperBound = 1,
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
                EczaneNobetIstekler = eczaneNobetIstekler,
                NobetGrupGunKurallar = _nobetGrupGunKuralService.GetDetaylarAktifList(nobetGrupIdListe),
                NobetGrupKurallar = _nobetGrupKuralService.GetDetaylar(nobetGrupIdListe),
                NobetGrupGorevTipler = nobetGrupGorevTipler,
                NobetGrupTalepler = _nobetGrupTalepService.GetDetaylar(nobetGrupIdListe, baslangicTarihi, bitisTarihi),
                EczaneNobetGruplar = eczaneNobetGruplar,
                Kisitlar = _nobetUstGrupKisitService.GetDetaylar(nobetUstGrupId),
                EczaneGrupNobetSonuclar = eczaneGrupNobetSonuclar,
                EczaneNobetSonuclar = eczaneNobetSonuclarCozulenGrup,
                EczaneGrupNobetSonuclarTumu = eczaneNobetSonuclar,
                EczaneNobetGrupGunKuralIstatistikler = enSonNobetler,
                TakvimNobetGrupGunDegerIstatistikler = takvimNobetGrupGunDegerIstatistikler,
                EczaneNobetGrupGunKuralIstatistikYatay = eczaneNobetGrupGunKuralIstatistikYatay,
                EczaneNobetGrupAltGruplar = eczaneNobetGrupAltGruplar,
                EczaneNobetAltGrupTarihAralik = eczaneNobetAltGrupTarihAralik
            };

            return mersinMerkezDataModel;
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

        public void ModeliKapat()
        {
            _eczaneNobetMersinMerkezOptimizationV2.ModeliKapat();
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