using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
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
    public class MersinMerkezOptimizationManagerV1 : IMersinMerkezOptimizationService
    {
        #region ctor
        private IEczaneGrupService _eczaneGrupService;
        private IEczaneGrupTanimService _eczaneGrupTanimService;
        private IEczaneNobetMersinMerkezOptimization _eczaneNobetMersinMerkezOptimization;
        private IEczaneNobetGrupService _eczaneNobetGrupService;
        private IEczaneNobetIstekService _eczaneNobetIstekService;
        private IEczaneNobetMazeretService _eczaneNobetMazeretService;
        private IEczaneNobetOrtakService _eczaneNobetOrtakService;
        private IEczaneNobetSonucAktifService _eczaneNobetSonucAktifService;
        private IEczaneNobetSonucService _eczaneNobetSonucService;
        private INobetGrupGorevTipService _nobetGrupGorevTipService;
        private INobetGrupGunKuralService _nobetGrupGunKuralService;
        private INobetGrupKuralService _nobetGrupKuralService;
        private INobetGrupService _nobetGrupService;
        private INobetAltGrupService _nobetAltGrupService;
        private IEczaneNobetGrupAltGrupService _eczaneNobetGrupAltGrupService;
        private INobetGrupTalepService _nobetGrupTalepService;
        private INobetUstGrupKisitService _nobetUstGrupKisitService;
        private ITakvimService _takvimService;
        private IEczaneNobetMuafiyetService _eczaneNobetMuafiyetService;

        public MersinMerkezOptimizationManagerV1(
                    IEczaneGrupService eczaneGrupService,
                    IEczaneGrupTanimService eczaneGrupTanimService,
                    IEczaneNobetMersinMerkezOptimization eczaneNobetMersinMerkezOptimization,
                    IEczaneNobetGrupService eczaneNobetGrupService,
                    IEczaneNobetIstekService eczaneNobetIstekService,
                    IEczaneNobetMazeretService eczaneNobetMazeretService,
                    IEczaneNobetOrtakService eczaneNobetOrtakService,
                    IEczaneNobetSonucAktifService eczaneNobetSonucAktifService,
                    INobetGrupGorevTipService nobetGrupGorevTipService,
                    INobetGrupGunKuralService nobetGrupGunKuralService,
                    INobetGrupKuralService nobetGrupKuralService,
                    INobetGrupService nobetGrupService,
                    INobetAltGrupService nobetAltGrupService,
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
            _eczaneNobetMersinMerkezOptimization = eczaneNobetMersinMerkezOptimization;
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
            _nobetAltGrupService = nobetAltGrupService;
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
        //[TransactionScopeAspect]
        public void EczaneNobetCozAktifiGuncelle(MersinMerkezDataModel data)
        {
            var mevcutSonuclar = _eczaneNobetSonucAktifService.GetSonuclar2(data.Yil, data.Ay, data.NobetUstGrupId);
            var guncellenecekSonuclar = mevcutSonuclar
                .Where(x => data.NobetGruplar.Select(s => s.Id).Contains(x.NobetGrupId))
                .Select(s => s.Id).ToArray();

            var yeniSonuclar = _eczaneNobetMersinMerkezOptimization.Solve(data);

            if (yeniSonuclar.ResultModel.Count > 0)
            {
                var cozumSuresi = yeniSonuclar.CozumSuresi;
                var amacFonksiyonu = yeniSonuclar.ObjectiveValue;
            }

            //gelen datadaki nöbet grup id aktif sonuçlarda varsa o nöbet gruba ait önceki sonuçları sil
            //foreach (var yeniSonuc in guncellenecekSonuclar)
            //{
            //    _eczaneNobetSonucAktifService.Delete(yeniSonuc.Id);
            //}

            _eczaneNobetSonucAktifService.CokluSil(guncellenecekSonuclar);

            AktiftekiArtiklariSil(data.NobetUstGrupId);

            //yeni sonuçları ekle
            //foreach (var yeniSonuc in yeniSonuclar.ResultModel)
            //{            
            //    //_eczaneNobetSonucAktifService.Insert(yeniSonuc);
            //}

            _eczaneNobetSonucAktifService.CokluEkle(yeniSonuclar.ResultModel);

            #region aynı gün nöbet tutan eczaneler için
            /*
            var ayniGunNobetTutmasiTakipEdilecekGruplar = new List<int> { 20, 21, 22 };
            var nobetGruplar = data.NobetGruplar.Where(w => ayniGunNobetTutmasiTakipEdilecekGruplar.Contains(w.Id)).Select(s => s.Id).ToList();
            if (nobetGruplar.Count() > 0)
            {
                //çözülen grubun sonuçları
                mevcutSonuclar = _eczaneNobetSonucAktifService.GetSonuclar2(data.Yil, data.Ay, data.NobetUstGrupId);

                //aynı aydaki diğer grupların sonuçları
                var ayniAydakiDigerGruplarinSonuclari = _eczaneNobetSonucService.GetSonuclar2(data.Yil, data.Ay, data.NobetUstGrupId)
                    .Where(w => ayniGunNobetTutmasiTakipEdilecekGruplar.Contains(w.NobetGrupId)).ToList();
                var ayIcindekiTumSonuclar = mevcutSonuclar.Union(ayniAydakiDigerGruplarinSonuclari).ToList();
                var ayIcindeCozulenNobetGruplar = ayIcindekiTumSonuclar.Select(s => s.NobetGrupId).Distinct();
                var ayIcindeAyniGunNobet = _nobetUstGrupKisitService.GetKisitPasifMi("ayIcindeAyniGunNobet", data.NobetUstGrupId);

                if (ayIcindeAyniGunNobet && ayIcindeCozulenNobetGruplar.Count() > 1)
                {
                    var ayIcindeAyniGunNobetTutanEczaneler = new List<EczaneCiftGrup>();
                    var ayIcindekiTumSonuclar2 = new List<EczaneNobetSonucListe>();

                    foreach (var x in ayIcindekiTumSonuclar)
                    {
                        ayIcindekiTumSonuclar2.Add(
                        new EczaneNobetSonucListe
                        {
                            Ay = x.Ay,
                            Yil = x.Yil,
                            EczaneAdi = x.EczaneAdi,
                            EczaneId = x.EczaneId,
                            EczaneNobetGrupId = x.EczaneNobetGrupId,
                            Gun = x.Gun,
                            Tarih = x.Tarih,
                            HaftaninGunu = (int)x.Tarih.DayOfWeek + 1,
                            NobetGrupId = x.NobetGrupId,
                            NobetGrupAdi = x.NobetGrupAdi,
                            NobetUstGrupId = x.NobetUstGrupId,
                            NobetGorevTipAdi = x.NobetGorevTipAdi,
                            NobetGorevTipId = x.NobetGorevTipId,
                            TakvimId = x.TakvimId,
                            NobetGunKuralId = x.NobetGunKuralId
                        });
                    }

                    ayIcindeAyniGunNobetTutanEczaneler = _eczaneNobetOrtakService.GetCiftGrupluEczaneler(ayIcindekiTumSonuclar2, 2);
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
            }
            */
            #endregion
        }

        /// <summary>
        /// Eğer başka nöbet gruplarından kalan data varsa siler.
        /// --artık kayıtlar--
        /// </summary>
        public void AktiftekiArtiklariSil(int nobetUstGrupId)
        {
            var mevcutSonuclar = _eczaneNobetSonucAktifService.GetDetaylar2(nobetUstGrupId)
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
        public void Kesinlestir(int nobetUstGrupId)
        {
            var eczaneNobetSonucAktifler = _eczaneNobetSonucAktifService.GetDetaylar2(nobetUstGrupId);

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

        private MersinMerkezDataModel EczaneNobetDataModel(EczaneNobetDataModelParametre eczaneNobetDataModelParametre)
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
            #endregion

            int aydakiGunSayisi = DateTime.DaysInMonth(yilBitis, ayBitis);
            var baslangicTarihi = new DateTime(yilBitis, ayBitis, 1);
            var bitisTarihi = new DateTime(yilBitis, ayBitis, aydakiGunSayisi);

            var nobetGruplar = _nobetGrupService.GetList(nobetGrupIdListe).OrderBy(s => s.Id).ToList();
            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetList(nobetGorevTipId, nobetGrupIdListe);
            var eczaneNobetSonuclar = _eczaneNobetSonucService.GetEczaneGrupNobetSonuc(nobetUstGrupId);

            var eczaneNobetGruplarTumu = _eczaneNobetGrupService.GetList(nobetGrupIdListe, baslangicTarihi, bitisTarihi);

            var eczaneNobetMazeretNobettenDusenler = _eczaneNobetMazeretService.GetEczaneNobetMazeretSayilari(baslangicTarihi, bitisTarihi, nobetGrupIdListe);
            //var eczaneNobettenMuafOlanlar = _eczaneNobetMuafiyetService.GetDetaylar(baslangicTarihi, bitisTarihi, nobetGrupIdListe);

            var eczaneNobetGruplar = _eczaneNobetGrupService.GetDetaylar(nobetGrupIdListe, baslangicTarihi, bitisTarihi)
                .Where(w => !eczaneNobetMazeretNobettenDusenler.Select(s => s.EczaneNobetGrupId).Contains(w.Id)
                         //&& !eczaneNobettenMuafOlanlar.Select(s => s.EczaneId).Contains(w.EczaneId)
                         ).ToList();

            var eczaneNobetSonuclarCozulenGrup = eczaneNobetSonuclar
                .Where(w => !eczaneNobetMazeretNobettenDusenler.Select(s => s.EczaneNobetGrupId).Contains(w.EczaneNobetGrupId)
                         //&& !eczaneNobettenMuafOlanlar.Select(s => s.EczaneId).Contains(w.EczaneId)
                         && nobetGrupIdListe.Contains(w.NobetGrupId)).ToList();

            var enSonNobetDurumlari = eczaneNobetSonuclarCozulenGrup
               .GroupBy(g => new
               {
                   g.NobetGunKuralId,
                   g.EczaneNobetGrupId,
                   g.EczaneId,
                   g.EczaneAdi,
                   g.NobetGrupId,
                   g.NobetGrupAdi,
                   g.NobetGorevTipId
               })
               .Select(s => new EczaneNobetGrupGunKuralIstatistik
               {
                   NobetGunKuralId = s.Key.NobetGunKuralId,
                   EczaneNobetGrupId = s.Key.EczaneNobetGrupId,
                   EczaneId = s.Key.EczaneId,
                   EczaneAdi = s.Key.EczaneAdi,
                   NobetGrupId = s.Key.NobetGrupId,
                   NobetGrupAdi = s.Key.NobetGrupAdi,
                   NobetGorevTipId = s.Key.NobetGorevTipId,
                   IlkNobetTarihi = s.Min(c => c.Tarih),
                   SonNobetTarihi = s.Max(c => c.Tarih),
                   NobetSayisi = s.Count()
               }).ToList();

            var sonucuOlanGunler = enSonNobetDurumlari
                .Select(s => new { s.NobetGunKuralId, s.NobetGorevTipId })
                .Distinct()
                .OrderBy(o => o.NobetGorevTipId).ThenBy(t => t.NobetGunKuralId).ToList();

            var varsayilanBaslangicNobetTarihi = new DateTime(2012, 1, 1);

            foreach (var nobetGunKural in sonucuOlanGunler)
            {
                var nobetDurumlari = enSonNobetDurumlari
                    .Where(w => w.NobetGunKuralId == nobetGunKural.NobetGunKuralId)
                    .Select(s => s.EczaneNobetGrupId).ToList();

                var sonucuOlmayanlar = eczaneNobetGruplar
                    .Where(w => !nobetDurumlari.Contains(w.Id)).ToList();

                if (sonucuOlmayanlar.Count > 0)
                {
                    foreach (var eczaneNobetGrup in sonucuOlmayanlar)
                    {
                        enSonNobetDurumlari.Add(new EczaneNobetGrupGunKuralIstatistik
                        {
                            EczaneId = eczaneNobetGrup.EczaneId,
                            EczaneAdi = eczaneNobetGrup.EczaneAdi,
                            NobetGrupAdi = eczaneNobetGrup.NobetGrupAdi,
                            NobetAltGrupId = 0,
                            EczaneNobetGrupId = eczaneNobetGrup.Id,
                            IlkNobetTarihi = varsayilanBaslangicNobetTarihi, // eczaneNobetGrup.BaslangicTarihi, 
                            SonNobetTarihi = varsayilanBaslangicNobetTarihi, // eczaneNobetGrup.BaslangicTarihi, 
                            NobetGorevTipId = nobetGunKural.NobetGorevTipId,
                            NobetGunKuralId = nobetGunKural.NobetGunKuralId,
                            NobetGrupId = eczaneNobetGrup.NobetGrupId,
                            NobetSayisi = 1
                        });
                    }
                }
            }

            var grupluEczaneNobetSonuclar = eczaneNobetSonuclar
                .Where(w => w.Tarih.Year == yilBitis
                         && w.Tarih.Month == ayBitis).ToList();

            var eczaneGrupEdges = _eczaneGrupService.GetEdges()
                .Where(e => (nobetGrupIdListe.Contains(e.FromNobetGrupId)
                         || nobetGrupIdListe.Contains(e.ToNobetGrupId)))
                .Where(w => (grupluEczaneNobetSonuclar.Select(s => s.EczaneId).Distinct().Contains(w.From)
                         || grupluEczaneNobetSonuclar.Select(s => s.EczaneId).Distinct().Contains(w.To))
                )
                .ToList();

            //sonuclarda ilişkili eczaneler
            var eczaneGruplar = _eczaneGrupService.GetDetaylar(nobetUstGrupId)
                .Where(x => x.EczaneGrupTanimBitisTarihi == null
                         //&& x.EczaneGrupTanimTipId == 2 //coğrafi yakınlık hariç
                         && (eczaneGrupEdges.Select(s => s.From).Distinct().Contains(x.EczaneId)
                          || eczaneGrupEdges.Select(s => s.To).Distinct().Contains(x.EczaneId))
                         && nobetGrupIdListe.Contains(x.NobetGrupId)
                         )
                .Where(w => !eczaneNobetMazeretNobettenDusenler.Select(s => s.EczaneId).Contains(w.EczaneId)).ToList();

            //fazladan gelen tanımlar var iyileştirmekte fayda var
            var eczaneGrupTanimlar = _eczaneGrupTanimService.GetAktifTanimList(eczaneGruplar.Select(x => x.EczaneGrupTanimId).ToList());
            var eczaneGruplar2 = _eczaneGrupService.GetDetaylarByEczaneGrupTanimId(eczaneGrupTanimlar.Select(s => s.Id).ToList());

            var eczaneGrupNobetSonuclar = grupluEczaneNobetSonuclar
                .Where(w => eczaneGruplar2.Select(s => s.EczaneId).Contains(w.EczaneId)).ToList();

            var esGrupluEczaneler = _eczaneNobetGrupService.GetDetaylar(nobetUstGrupId)
                .Where(x => nobetGrupIdListe.Contains(x.NobetGrupId)
                        && x.BitisTarihi == null
                        && !eczaneNobetMazeretNobettenDusenler.Select(s => s.EczaneId).Contains(x.EczaneId))
                .Select(x => x.EczaneId).Distinct().ToList();

            //var eczaneNobetGruplar = _eczaneNobetGrupService.GetAktifEczaneGrupList(nobetGruplar.Select(w => w.Id).ToList())
            //    .Where(s => !eczaneNobetMazeretNobettenDusenler.Select(f => f.EczaneId).Contains(s.EczaneId)).ToList();

            //nöbet yazılacak tarih aralığı(örn. Ocak ayının tüm günleri)
            var tarihAralik = _takvimService.GetTakvimNobetGruplar(baslangicTarihi, bitisTarihi, nobetGrupIdListe, nobetGorevTipId);

            //var eczaneKumulatifHedefler = _takvimService
            //    .GetEczaneKumulatifHedeflerTumYillar(yilBaslangic, yilBitis, ayBaslangic, ayBitis, nobetGrupIdListe);

            var eczaneKumulatifHedefler = _takvimService.GetEczaneKumulatifHedefler(yilBaslangic, yilBitis, ayBaslangic, ayBitis, nobetGrupIdListe, nobetGorevTipId)
                .Where(w => eczaneNobetGruplar.Select(s => s.EczaneId).Contains(w.EczaneId)).ToList();

            var eczaneNobetTarihAralik = _takvimService.GetEczaneNobetTarihAralik(baslangicTarihi, bitisTarihi, nobetGorevTipId, nobetGrupIdListe)
                .Where(w => eczaneNobetGruplar.Select(s => s.EczaneId).Contains(w.EczaneId)).ToList();

            var takvimNobetGrupGunDegerIstatistikler = _takvimService.GetTakvimNobetGrupGunDegerIstatistikler(nobetUstGrupBaslangicTarihi, bitisTarihi, nobetGrupIdListe, nobetGorevTipId);

            #region aynı gün nöbet tutan eczaneler
            /*
            var ayniGunNobetTutmasiTakipEdilecekGruplar = new List<int> { 20, 21, 22 };

            //nöbet yazılacak tarih aralığı(örn. Ocak ayının tüm günleri)
            int cozumOncekiAyi = GetOncekiAy(periyot: 3, cozumAyi: ayBitis);
            var cozumOncekiIkiAyi = GetOncekiAy(periyot: 2, cozumAyi: ayBitis);
            var aktifSonuclar = _eczaneNobetSonucAktifService.GetSonuclarAylik(yilBitis, ayBitis, nobetUstGrupId);
            var ayniAydakiDigerGruplarinSonuclari = _eczaneNobetSonucService.GetSonuclarAylik(yilBitis, ayBitis, nobetUstGrupId)
                .Where(w => ayniGunNobetTutmasiTakipEdilecekGruplar.Contains(w.NobetGrupId)).ToList();

            var ayIcindekiTumSonuclar = aktifSonuclar.Union(ayniAydakiDigerGruplarinSonuclari).ToList();
            var ayIcindeCozulenNobetGruplar = ayIcindekiTumSonuclar.Select(s => s.NobetGrupId).Distinct().ToList();

            #region son iki ay

            var sonIkiAyBakilacakGruplar = new List<int>();

            foreach (var item in ayIcindeCozulenNobetGruplar)
            {
                sonIkiAyBakilacakGruplar.Add(item);
            }

            //şimdi çözülecek grup
            foreach (var item in nobetGrupIdListe)
            {
                sonIkiAyBakilacakGruplar.Add(item);
            }

            var sonIkiAyAyniGunNobetTutanEczaneler = new List<EczaneCiftGrup>();
            var gruplar = sonIkiAyBakilacakGruplar.Distinct().ToList();

            if (_nobetUstGrupKisitService.GetKisitPasifMi("sonIkiAydakiGrup", nobetUstGrupId) && sonIkiAyBakilacakGruplar.Count() > 1 && ayBitis > 1)
            {
                var sonIkiAydakiSonuclar = _eczaneNobetSonucService.GetSonuclar(yilBitis, cozumOncekiAyi, ayBitis - 1, nobetUstGrupId)
                 .Where(x => gruplar.Contains(x.NobetGrupId)
                        && ayniGunNobetTutmasiTakipEdilecekGruplar.Contains(x.NobetGrupId)).ToList();

                sonIkiAyAyniGunNobetTutanEczaneler = _eczaneNobetOrtakService.GetCiftGrupluEczaneler(sonIkiAydakiSonuclar, 1);
            }
            #endregion

            #region yillik kümülatif

            var yilIcindeAyniGunNobetTutanEczaneler = new List<EczaneCiftGrup>();

            if (_nobetUstGrupKisitService.GetKisitPasifMi("yildaEncokUcKezGrup", nobetUstGrupId) && sonIkiAyBakilacakGruplar.Count() > 1 && ayBitis > 2)
            {
                var yillikKumulatifSonuclar = _eczaneNobetSonucService.GetSonuclarYillikKumulatif(yilBitis, ayBitis - 1, nobetUstGrupId)
                    .Where(x => gruplar.Contains(x.NobetGrupId)
                            && ayniGunNobetTutmasiTakipEdilecekGruplar.Contains(x.NobetGrupId)).ToList();

                yilIcindeAyniGunNobetTutanEczaneler = _eczaneNobetOrtakService.GetCiftGrupluEczaneler(yillikKumulatifSonuclar, 3);

                yilIcindeAyniGunNobetTutanEczaneler = (from a in yilIcindeAyniGunNobetTutanEczaneler
                                                       from t in _eczaneNobetGrupAltGrupService.GetDetaylar()
                                                            .Where(w => w.EczaneId == a.EczaneId).DefaultIfEmpty()
                                                       select new EczaneCiftGrup
                                                       {
                                                           Id = a.Id,
                                                           NobetAltGrupId = (t?.EczaneId == a.EczaneId) ? t.NobetAltGrupId : 0,
                                                           EczaneId = a.EczaneId,
                                                           BirlikteNobetTutmaSayisi = a.BirlikteNobetTutmaSayisi
                                                       }).ToList();
            }
            #endregion

            var ayIcindeAyniGunNobetTutanEczaneler = new List<EczaneCiftGrup>();
            */
            #endregion

            var mersinMerkezDataModel = new MersinMerkezDataModel()
            {
                Yil = yilBitis,
                Ay = ayBitis,
                LowerBound = 0,
                UpperBound = 1,
                NobetUstGrupId = nobetUstGrupId,
                EczaneNobetTarihAralik = eczaneNobetTarihAralik, //karar değişkeni
                EczaneKumulatifHedefler = eczaneKumulatifHedefler,//.Where(w => w.EczaneId != 121).ToList(),
                TarihAraligi = tarihAralik,
                NobetGruplar = nobetGruplar,
                EczaneGrupTanimlar = eczaneGrupTanimlar,
                EczaneGruplar = eczaneGruplar2,
                EczaneNobetMazeretler = _eczaneNobetMazeretService.GetDetaylar(yilBitis, ayBitis, esGrupluEczaneler),
                EczaneNobetIstekler = _eczaneNobetIstekService.GetDetaylarByNobetGrupIdList(yilBitis, ayBitis, nobetGrupIdListe),
                NobetGrupGunKurallar = _nobetGrupGunKuralService.GetAktifList(nobetGrupIdListe),
                NobetGrupKurallar = _nobetGrupKuralService.GetDetaylar(nobetGrupIdListe),
                NobetGrupGorevTipler = _nobetGrupGorevTipService.GetList(nobetGorevTipId, nobetGrupIdListe),
                NobetGrupTalepler = _nobetGrupTalepService.GetDetaylar(nobetGrupIdListe, baslangicTarihi, bitisTarihi),
                EczaneNobetGruplar = eczaneNobetGruplar,
                NobetUstGrupKisitlar = _nobetUstGrupKisitService.GetDetaylar(nobetUstGrupId),
                EczaneGrupNobetSonuclar = eczaneGrupNobetSonuclar,
                EczaneGrupNobetSonuclarTumu = eczaneNobetSonuclar,
                EczaneNobetSonuclar = eczaneNobetSonuclarCozulenGrup,
                EczaneNobetGrupGunKuralIstatistikler = enSonNobetDurumlari,
                TakvimNobetGrupGunDegerIstatistikler = takvimNobetGrupGunDegerIstatistikler

                #region aynı gün nöbet tutma durumu
                //AyIcindeAyniGunNobetTutanEczaneler = ayIcindeAyniGunNobetTutanEczaneler, //bu alan EczaneNobetCozAktifiGuncelle içinde kullanılıyor.
                //YilIcindeAyniGunNobetTutanEczaneler = yilIcindeAyniGunNobetTutanEczaneler,
                //SonIkiAyAyniGunNobetTutanEczaneler = sonIkiAyAyniGunNobetTutanEczaneler,
                //EczaneNobetSonuclarSonIkiAy = _eczaneNobetSonucService.GetSonuclar(yilBitis, cozumOncekiIkiAyi, ayBitis, nobetUstGrupId),
                //EczaneNobetSonuclarOncekiAylar = _eczaneNobetSonucService.GetSonuclar(yilBitis, ayBaslangic, ayBitis, nobetUstGrupId) 
                #endregion
            };
            return mersinMerkezDataModel;
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
                    //.Where(w => w.NobetGrupId == eczaneNobetModelCoz.NobetGrupId || eczaneNobetModelCoz.NobetGrupId == 0).ToList();
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
                                .Select(s => s.NobetGrupId).OrderBy(x => r.NextDouble()).ToList();

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
                                    .Select(s => s.NobetGrupId).OrderBy(x => r.NextDouble()).ToList();

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
                                .Select(s => s.NobetGrupId).OrderBy(x => x).ToList();

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
                                .Select(s => s.NobetGrupId).OrderBy(x => x).ToList();

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
                        var aylar = _takvimService.GetAylar().Where(w => w.Id >= eczaneNobetModelCoz.AyBitis).ToArray();
                        foreach (var ay in aylar)
                        {
                            foreach (var item in tumNobetGruplar
                                .Where(x => eczaneNobetModelCoz.NobetGrupId.Contains(x.NobetGrupId))
                                //.Where(x => x.NobetGrupId == eczaneNobetModelCoz.NobetGrupId)
                                .Select(s => s.Id).Distinct().ToList())
                            {
                                var liste = tumNobetGruplar
                                    .Where(x => x.Id == item)
                                    .Select(s => s.NobetGrupId).OrderBy(x => x).ToArray();

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
                            var liste = tumNobetGruplar.Where(x => x.Id == item).Select(s => s.NobetGrupId).OrderBy(x => x).ToArray();
                            model.NobetGrupId = liste;
                            var data = EczaneNobetDataModel(model);
                            EczaneNobetCozAktifiGuncelle(data);
                        }
                    }
                }
                #endregion
            }
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
    }
}