using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using WM.Core.Aspects.PostSharp.TranstionAspects;
using WM.Northwind.Business.Abstract.EczaneNobet;
using WM.Northwind.DataAccess.Abstract.EczaneNobet;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;
using WM.Northwind.Entities.Concrete.Enums;

namespace WM.Northwind.Business.Concrete.Managers.EczaneNobet
{
    public class EczaneNobetSonucAktifManager : IEczaneNobetSonucAktifService
    {
        #region ctor
        private IEczaneNobetSonucAktifDal _eczaneNobetSonucAktifDal;
        private IEczaneNobetGrupService _eczaneNobetGrupService;
        private IBayramService _bayramService;
        private INobetGrupGorevTipService _nobetGrupGorevTipService;
        private IEczaneNobetMazeretService _eczaneNobetMazeretService;
        private INobetGrupGorevTipTakvimOzelGunService _nobetGrupGorevTipTakvimOzelGunService;
        private INobetGrupGorevTipGunKuralService _nobetGrupGorevTipGunKuralService;
        private IEczaneNobetOrtakService _eczaneNobetOrtakService;

        public EczaneNobetSonucAktifManager(IEczaneNobetSonucAktifDal eczaneNobetSonucAktifDal,
            IEczaneNobetGrupService eczaneNobetGrupService,
            IBayramService bayramService,
            INobetGrupGorevTipService nobetGrupGorevTipService,
            IEczaneNobetMazeretService eczaneNobetMazeretService,
            INobetGrupGorevTipTakvimOzelGunService nobetGrupGorevTipTakvimOzelGunService,
            INobetGrupGorevTipGunKuralService nobetGrupGorevTipGunKuralService,
            IEczaneNobetOrtakService eczaneNobetOrtakService)
        {
            _eczaneNobetSonucAktifDal = eczaneNobetSonucAktifDal;
            _eczaneNobetGrupService = eczaneNobetGrupService;
            _bayramService = bayramService;
            _nobetGrupGorevTipService = nobetGrupGorevTipService;
            _eczaneNobetMazeretService = eczaneNobetMazeretService;
            _nobetGrupGorevTipTakvimOzelGunService = nobetGrupGorevTipTakvimOzelGunService;
            _nobetGrupGorevTipGunKuralService = nobetGrupGorevTipGunKuralService;
            _eczaneNobetOrtakService = eczaneNobetOrtakService;
        }
        #endregion

        public void Delete(int Id)
        {
            _eczaneNobetSonucAktifDal.Delete(new EczaneNobetSonucAktif { Id = Id });
        }

        public EczaneNobetSonucAktif GetById(int Id)
        {
            return _eczaneNobetSonucAktifDal.Get(x => x.Id == Id);
        }

        public List<EczaneNobetSonucAktif> GetList()
        {
            return _eczaneNobetSonucAktifDal.GetList();
        }

        public void Insert(EczaneNobetSonucAktif sonuc)
        {
            _eczaneNobetSonucAktifDal.Insert(sonuc);
        }

        public void Update(EczaneNobetSonucAktif sonuc)
        {
            _eczaneNobetSonucAktifDal.Update(sonuc);
        }

        public List<EczaneNobetIstatistik> GetEczaneNobetIstatistik(Expression<Func<EczaneNobetIstatistik, bool>> filter = null)
        {
            var eczaneNobetSonuclar = GetDetaylarForIstatistik();

            var eczaneNobetSonuclardaOlmayanEczaneler = _eczaneNobetGrupService.GetDetaylar()
                .Where(w => !eczaneNobetSonuclar.Select(s => s.EczaneNobetGrupId).Contains(w.Id))
                .Select(c => new
                {
                    c.Id,
                    c.EczaneId,
                    c.NobetGrupId
                });

            //sonuç tablosunda olmayanların listeye eklenmesi için
            foreach (var t in eczaneNobetSonuclardaOlmayanEczaneler)
            {
                eczaneNobetSonuclar.Add(new EczaneNobetSonucGunKural()
                {
                    EczaneNobetGrupId = t.Id,
                    EczaneId = t.EczaneId,
                    NobetGrupId = t.NobetGrupId,
                    NobetGorevTipId = 1,
                    NobetGunKuralId = 0
                });
            }

            var nobetIstatistik = eczaneNobetSonuclar
                                   .GroupBy(t => new
                                   {
                                       t.EczaneNobetGrupId,
                                       t.EczaneId,
                                       t.NobetGrupId,
                                       t.NobetGorevTipId
                                   })
                                   .Select(grouped => new EczaneNobetIstatistik
                                   {
                                       EczaneNobetGrupId = grouped.Key.EczaneNobetGrupId,
                                       EczaneId = grouped.Key.EczaneId,
                                       NobetGrupId = grouped.Key.NobetGrupId,
                                       NobetGorevTipId = grouped.Key.NobetGorevTipId,

                                       Pazar = grouped.Count(c => c.NobetGunKuralId == 1),
                                       Pazartesi = grouped.Count(c => c.NobetGunKuralId == 2),
                                       Sali = grouped.Count(c => c.NobetGunKuralId == 3),
                                       Carsamba = grouped.Count(c => c.NobetGunKuralId == 4),
                                       Persembe = grouped.Count(c => c.NobetGunKuralId == 5),
                                       Cuma = grouped.Count(c => c.NobetGunKuralId == 6),
                                       Cumartesi = grouped.Count(c => c.NobetGunKuralId == 7),
                                       DiniBayram = grouped.Count(c => c.NobetGunKuralId == 8),
                                       MilliBayram = grouped.Count(c => c.NobetGunKuralId == 9),
                                       ToplamBayram = grouped.Count(c => c.NobetGunKuralId > 7),
                                       ToplamHaftaIci = grouped.Count(c => (c.NobetGunKuralId >= 2 && c.NobetGunKuralId <= 7)),
                                       ToplamCumaCumartesi = grouped.Count(c => (c.NobetGunKuralId >= 6 && c.NobetGunKuralId <= 7)),
                                       Toplam = grouped.Count(c => c.NobetGunKuralId > 0)
                                   }).AsQueryable();

            return filter == null
               ? nobetIstatistik.ToList()
               : nobetIstatistik.Where(filter).ToList();
        }

        public List<EczaneNobetIstatistik> GetEczaneNobetIstatistik(List<int> nobetGrupIdList)
        {
            return GetEczaneNobetIstatistik(x => nobetGrupIdList.Contains(x.NobetGrupId));
        }

        public List<EczaneNobetSonucGunKural> GetDetaylarForIstatistik()
        {
            return GetDetaylar2()
                .Select(t => new EczaneNobetSonucGunKural
                {
                    EczaneNobetGrupId = t.EczaneNobetGrupId,
                    EczaneId = t.EczaneId,
                    NobetGrupId = t.NobetGrupId,
                    NobetGorevTipId = t.NobetGorevTipId,
                    //NobetGunKuralId = t.NobetGunKuralId
                }).ToList();
        }

        public List<EczaneNobetSonucDetay2> GetDetaylar2(int nobetUstGrupId)
        {
            return _eczaneNobetSonucAktifDal.GetDetayList(x => x.NobetUstGrupId == nobetUstGrupId);
        }

        public List<EczaneNobetSonucDetay2> GetDetaylar2()
        {
            return _eczaneNobetSonucAktifDal.GetDetayList();
        }

        public List<EczaneNobetSonucDetay2> GetDetaylar2(int nobetGrupId, int yil, int ay)
        {
            return _eczaneNobetSonucAktifDal.GetDetayList(x => x.Tarih.Year == yil && x.Tarih.Month == ay && x.NobetGrupId == nobetGrupId);
        }

        public List<EczaneNobetSonucDetay2> GetDetaylar2ByNobetUstGrup(int yil, int ay, int nobetUstGrupId)
        {
            return _eczaneNobetSonucAktifDal.GetDetayList(x => x.Tarih.Year == yil && x.Tarih.Month == ay && x.NobetUstGrupId == nobetUstGrupId);
        }

        public List<EczaneNobetSonucDetay2> GetDetaylar2(DateTime baslangicTarihi, DateTime bitisTarihi, int nobetUstGrupId)
        {
            return _eczaneNobetSonucAktifDal.GetDetayList(x => (x.Tarih >= baslangicTarihi && x.Tarih <= bitisTarihi) && x.NobetUstGrupId == nobetUstGrupId);
        }

        public List<EczaneNobetSonucListe2> GetSonuclar2(int yil, int ay, int nobetUstGrupId)
        {
            var sonuclar = GetDetaylar2ByNobetUstGrup(yil, ay, nobetUstGrupId);

            return GetSonuclar2(sonuclar, nobetUstGrupId);
        }

        public List<EczaneNobetSonucListe2> GetSonuclar2(int nobetUstGrupId)
        {
            var sonuclar = GetDetaylar2(nobetUstGrupId);

            return GetSonuclar3(sonuclar, nobetUstGrupId);//GetSonuclar2
        }

        public List<EczaneNobetSonucListe2> GetSonuclar2(DateTime baslangicTarihi, DateTime bitisTarihi, int nobetUstGrupId)
        {
            var sonuclar = GetDetaylar2(baslangicTarihi, bitisTarihi, nobetUstGrupId);

            return GetSonuclar3(sonuclar, nobetUstGrupId);//GetSonuclar2
        }

        public List<EczaneNobetSonucListe2> GetSonuclar2(List<EczaneNobetSonucDetay2> eczaneNobetSonucDetaylar, int nobetUstGrupId)
        {
            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrupId);
            var bayramlar = _bayramService.GetDetaylar(nobetUstGrupId);
            var mazeretler = _eczaneNobetMazeretService.GetDetaylar(nobetUstGrupId);

            var liste2 = (from s in eczaneNobetSonucDetaylar
                          join b in nobetGrupGorevTipler
                              on new { s.NobetGrupId, s.NobetGorevTipId }
                              equals new { b.NobetGrupId, b.NobetGorevTipId }
                          select new
                          {
                              s.NobetGorevTipId,
                              s.TakvimId,
                              s.EczaneNobetGrupId,
                              s.EczaneAdi,
                              s.NobetGrupAdi,
                              s.NobetGrupId,
                              NobetGrupGorevTipId = b.Id,
                              s.Tarih,
                              s.NobetUstGrupId,
                              s.EczaneId,
                              b.NobetGorevTipAdi
                          }).ToList();

            var culture = new CultureInfo("tr-TR");

            var liste = (from s in liste2
                         from b in bayramlar
                                       .Where(w => w.TakvimId == s.TakvimId
                                                && w.NobetGrupGorevTipId == s.NobetGrupGorevTipId).DefaultIfEmpty()
                         from m in mazeretler
                                         .Where(w => w.TakvimId == s.TakvimId
                                                  && w.EczaneNobetGrupId == s.EczaneNobetGrupId).DefaultIfEmpty()
                         select new EczaneNobetSonucListe2
                         {
                             Yil = s.Tarih.Year,
                             Ay = s.Tarih.Month,
                             EczaneNobetGrupId = s.EczaneNobetGrupId,
                             EczaneId = s.EczaneId,
                             EczaneAdi = s.EczaneAdi,
                             NobetGrupAdi = s.NobetGrupAdi,
                             NobetGrupId = s.NobetGrupId,
                             NobetUstGrupId = s.NobetUstGrupId,
                             NobetGunKuralId = (b?.TakvimId == s.TakvimId && b?.NobetGrupGorevTipId == s.NobetGrupGorevTipId)
                             ? b.NobetGunKuralId
                             : (int)s.Tarih.DayOfWeek + 1,
                             NobetGunKuralAdi = (b?.TakvimId == s.TakvimId && b?.NobetGrupGorevTipId == s.NobetGrupGorevTipId)
                             ? b.NobetGunKuralAdi
                             : culture.DateTimeFormat.GetDayName(s.Tarih.DayOfWeek),
                             GunGrupAdi = s.NobetUstGrupId == 3
                             ? ((b?.TakvimId == s.TakvimId && b?.NobetGrupGorevTipId == s.NobetGrupGorevTipId)
                                ? "Bayram"
                                : (s.Tarih.DayOfWeek == 0 || (int)s.Tarih.DayOfWeek == 6)
                                ? culture.DateTimeFormat.GetDayName(s.Tarih.DayOfWeek)
                                : "Hafta İçi")
                             : ((b?.TakvimId == s.TakvimId && b?.NobetGrupGorevTipId == s.NobetGrupGorevTipId)
                                ? "Bayram"
                                : s.Tarih.DayOfWeek == 0
                                ? "Pazar"
                                : "Hafta İçi"),
                             Gun = s.Tarih.Day,
                             Tarih = s.Tarih,
                             TakvimId = s.TakvimId,
                             MazeretId = (m?.TakvimId == s.TakvimId && m?.EczaneNobetGrupId == s.EczaneNobetGrupId) ? m.MazeretId : 0,
                             Mazeret = (m?.TakvimId == s.TakvimId && m?.EczaneNobetGrupId == s.EczaneNobetGrupId) ? m.MazeretAdi : null,
                             MazeretTuru = (m?.TakvimId == s.TakvimId && m?.EczaneNobetGrupId == s.EczaneNobetGrupId) ? m.MazeretTuru : null,
                             NobetGorevTipAdi = s.NobetGorevTipAdi,
                             NobetGorevTipId = s.NobetGorevTipId,
                             SonucTuru = EczaneNobetSonucTuru.Taslak
                         }).ToList();
            return liste;
        }

        public List<EczaneNobetSonucListe2> GetSonuclar3(List<EczaneNobetSonucDetay2> eczaneNobetSonucDetaylar, int nobetUstGrupId)
        {
            //var sw = new Stopwatch();
            //sw.Start();
            var nobetGrupGorevTipGunKurallar = _nobetGrupGorevTipGunKuralService.GetDetaylar(nobetUstGrupId);
            //var s1 = sw.Elapsed;
            //sw.Restart();
            var nobetGrupGorevTipTakvimOzelGunler = _nobetGrupGorevTipTakvimOzelGunService.GetDetaylar(nobetUstGrupId);
            //var s2 = sw.Elapsed;
            //sw.Restart();
            //var mazeretler = _eczaneNobetMazeretService.GetDetaylar(nobetUstGrupId);
            //var s3 = sw.Elapsed;
            //sw.Restart();
            //var istekler = _eczaneNobetIstekService.GetDetaylar(nobetUstGrupId);
            //var s4 = sw.Elapsed;
            //sw.Restart();
            var sonuclar = _eczaneNobetOrtakService.EczaneNobetSonucBirlesim(nobetGrupGorevTipGunKurallar, eczaneNobetSonucDetaylar, nobetGrupGorevTipTakvimOzelGunler, EczaneNobetSonucTuru.Taslak);
            //var s5 = sw.Elapsed;
            //sw.Stop();

            return sonuclar;

            //var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrupId);
            ////var bayramlar = _bayramService.GetDetaylar(nobetUstGrupId);
            //var mazeretler = _eczaneNobetMazeretService.GetDetaylar(nobetUstGrupId);
            //var nobetGrupGorevTipGunKurallar = _nobetGrupGorevTipGunKuralService.GetDetaylar(nobetUstGrupId);
            //var nobetGrupGorevTipTakvimOzelGunler = _nobetGrupGorevTipTakvimOzelGunService.GetDetaylar(nobetUstGrupId);

            //var liste2 = (from s in eczaneNobetSonucDetaylar
            //              join b in nobetGrupGorevTipler
            //                  on new { s.NobetGrupId, s.NobetGorevTipId }
            //                  equals new { b.NobetGrupId, b.NobetGorevTipId }
            //              select new
            //              {
            //                  s.NobetGorevTipId,
            //                  s.TakvimId,
            //                  s.EczaneNobetGrupId,
            //                  s.EczaneAdi,
            //                  s.NobetGrupAdi,
            //                  s.NobetGrupId,
            //                  s.NobetGrupGorevTipId,
            //                  s.Tarih,
            //                  s.NobetUstGrupId,
            //                  s.EczaneId,
            //                  b.NobetGorevTipAdi,
            //              }).ToList();

            //var culture = new CultureInfo("tr-TR");

            //var liste = (from s in liste2
            //             from b in nobetGrupGorevTipTakvimOzelGunler
            //                           .Where(w => w.TakvimId == s.TakvimId
            //                                    && w.NobetGrupGorevTipId == s.NobetGrupGorevTipId).DefaultIfEmpty()
            //             from m in mazeretler
            //                             .Where(w => w.TakvimId == s.TakvimId
            //                                      && w.EczaneNobetGrupId == s.EczaneNobetGrupId).DefaultIfEmpty()
            //             select new EczaneNobetSonucListe2
            //             {
            //                 Yil = s.Tarih.Year,
            //                 Ay = s.Tarih.Month,
            //                 EczaneNobetGrupId = s.EczaneNobetGrupId,
            //                 EczaneId = s.EczaneId,
            //                 EczaneAdi = s.EczaneAdi,
            //                 NobetGrupAdi = s.NobetGrupAdi,
            //                 NobetGrupId = s.NobetGrupId,
            //                 NobetUstGrupId = s.NobetUstGrupId,
            //                 NobetGunKuralId = (b?.TakvimId == s.TakvimId && b?.NobetGrupGorevTipId == s.NobetGrupGorevTipId)
            //                             ? b.NobetGunKuralId
            //                             : (int)s.Tarih.DayOfWeek + 1,
            //                 GunTanim = (b?.TakvimId == s.TakvimId && b?.NobetGrupGorevTipId == s.NobetGrupGorevTipId)
            //                 ? b.NobetGunKuralAdi
            //                 : culture.DateTimeFormat.GetDayName(s.Tarih.DayOfWeek),
            //                 GunGrupAdi = (b?.TakvimId == s.TakvimId && b?.NobetGrupGorevTipId == s.NobetGrupGorevTipId)
            //                             ? b.GunGrupAdi
            //                             : nobetGrupGorevTipGunKurallar.SingleOrDefault(w => w.NobetGrupGorevTipId == s.NobetGrupGorevTipId
            //                                && w.NobetGunKuralId == (int)s.Tarih.DayOfWeek + 1).GunGrupAdi,
            //                 Gun = s.Tarih.Day,
            //                 Tarih = s.Tarih,
            //                 TakvimId = s.TakvimId,
            //                 MazeretId = (m?.TakvimId == s.TakvimId && m?.EczaneNobetGrupId == s.EczaneNobetGrupId) ? m.MazeretId : 0,
            //                 Mazeret = (m?.TakvimId == s.TakvimId && m?.EczaneNobetGrupId == s.EczaneNobetGrupId) ? m.MazeretAdi : null,
            //                 MazeretTuru = (m?.TakvimId == s.TakvimId && m?.EczaneNobetGrupId == s.EczaneNobetGrupId) ? m.MazeretTuru : null,
            //                 NobetGorevTipAdi = s.NobetGorevTipAdi,
            //                 NobetGorevTipId = s.NobetGorevTipId,
            //                 SonucTuru = EczaneNobetSonucTuru.Taslak,
            //                 NobetGrupGorevTipId = s.NobetGrupGorevTipId
            //             }).ToList();
            //return liste;
        }

        [TransactionScopeAspect]
        public void CokluSil(int[] ids)
        {
            _eczaneNobetSonucAktifDal.CokluSil(ids);
        }

        [TransactionScopeAspect]
        public void CokluEkle(List<EczaneNobetCozum> eczaneNobetCozumler)
        {
            _eczaneNobetSonucAktifDal.CokluEkle(eczaneNobetCozumler);
        }

        public List<EczaneNobetCozum> GetCozumler(int nobetUstGrupId)
        {
            return _eczaneNobetSonucAktifDal.GetDetayList(x => x.NobetUstGrupId == nobetUstGrupId)
                .Select(s =>
                new EczaneNobetCozum
                {
                    EczaneNobetGrupId = s.EczaneNobetGrupId,
                    NobetGorevTipId = s.NobetGorevTipId,
                    TakvimId = s.TakvimId
                }).ToList();
        }

        public List<EczaneNobetCozum> GetCozumler(int nobetGrupId, int yil, int ay)
        {
            return _eczaneNobetSonucAktifDal.GetDetayList(x => x.NobetGrupId == nobetGrupId && x.Tarih.Year == yil && x.Tarih.Month == ay)
                .Select(s =>
                new EczaneNobetCozum
                {
                    EczaneNobetGrupId = s.EczaneNobetGrupId,
                    NobetGorevTipId = s.NobetGorevTipId,
                    TakvimId = s.TakvimId
                }).ToList();
        }

        public List<EczaneNobetCozum> GetCozumler(int[] nobetGrupIdList, int yil, int ay)
        {
            return _eczaneNobetSonucAktifDal.GetDetayList(x => nobetGrupIdList.Contains(x.NobetGrupId) && x.Tarih.Year == yil && x.Tarih.Month == ay)
                .Select(s =>
                new EczaneNobetCozum
                {
                    EczaneNobetGrupId = s.EczaneNobetGrupId,
                    NobetGorevTipId = s.NobetGorevTipId,
                    TakvimId = s.TakvimId
                }).ToList();
        }

        public List<EczaneNobetCozum> GetCozumler(int[] nobetGrupIdList, DateTime baslangicTarihi, DateTime bitisTarihi)
        {
            return _eczaneNobetSonucAktifDal.GetDetayList(x => nobetGrupIdList.Contains(x.NobetGrupId)
            && (x.Tarih >= baslangicTarihi && x.Tarih <= bitisTarihi)
            )
                .Select(s =>
                new EczaneNobetCozum
                {
                    EczaneNobetGrupId = s.EczaneNobetGrupId,
                    NobetGorevTipId = s.NobetGorevTipId,
                    TakvimId = s.TakvimId
                }).ToList();
        }

    }
}


/*
 *         public List<EczaneNobetSonucListe> GetSonuclar(int nobetUstGrupId)
        {
            return GetSonuclar(x => x.NobetUstGrupId == nobetUstGrupId);
        }

        public List<EczaneNobetSonucListe> GetSonuclarByNobetGrupId(int nobetGrupId)
        {
            return GetSonuclar(x => x.NobetGrupId == nobetGrupId || nobetGrupId == 0);
        }

        public List<EczaneNobetSonucListe> GetSonuclar(int yil, int ay)
        {
            return GetSonuclar(x => x.Yil == yil && x.Ay == ay);
        }

        public List<EczaneNobetSonucListe> GetSonuclar(int nobetGrupId, int yil, int ay)
        {
            return GetSonuclar(x => x.Yil == yil && x.Ay == ay && x.NobetGrupId == nobetGrupId);
        }

        public List<EczaneNobetSonucListe> GetSonuclarYillikKumulatif(int yil, int ay, int nobetUstGrupId)
        {
            return GetSonuclar(x => x.Yil == yil && x.Ay <= ay && x.NobetUstGrupId == nobetUstGrupId);
        }

        public List<EczaneNobetSonucListe> GetSonuclarAylik(int yil, int ay, int nobetUstGrupId)
        {
            return GetSonuclar(x => x.Yil == yil && x.Ay == ay && x.NobetUstGrupId == nobetUstGrupId);
        }
 */
