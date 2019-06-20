using System.Linq;
using System.Collections.Generic;
using WM.Northwind.Business.Abstract.EczaneNobet;
using WM.Northwind.DataAccess.Abstract.EczaneNobet;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;
using WM.Northwind.Entities.Concrete.Optimization.EczaneNobet;
using WM.Optimization.Abstract.Samples;
using System.Globalization;
using WM.Core.Aspects.PostSharp.LogAspects;
using WM.Core.CrossCuttingConcerns.Logging.Log4Net.Logger;
using WM.Core.Aspects.PostSharp.CacheAspects;
using WM.Core.CrossCuttingConcerns.Caching.Microsoft;
using System;
using WM.Core.Aspects.PostSharp.ExceptionAspects;
using WM.Northwind.Entities.Concrete.Authorization;
using WM.Northwind.Business.Abstract.Optimization;
using WM.Northwind.Business.Abstract.Optimization.EczaneNobet;
using System.Data.Entity.SqlServer;
using WM.Northwind.Entities.Concrete.Enums;

namespace WM.Northwind.Business.Concrete.Managers.EczaneNobet
{
    public class EczaneNobetSonucDemoManager : IEczaneNobetSonucDemoService
    {
        #region ctor
        private IEczaneNobetSonucDemoDal _eczaneNobetSonucDemoDal;
        private IEczaneNobetOrtakService _eczaneNobetOrtakService;
        private ITakvimService _takvimService;
        private IBayramService _bayramService;
        private INobetGrupGorevTipService _nobetGrupGorevTipService;
        private IEczaneNobetMazeretService _eczaneNobetMazeretService;

        public EczaneNobetSonucDemoManager(IEczaneNobetSonucDemoDal eczaneNobetSonucDemoDal,
                                           IEczaneNobetOrtakService eczaneNobetOrtakService,
                                           INobetGrupService nobetGrupService,
                                           IEczaneService eczaneService,
                                           ITakvimService takvimService,
                                           IBayramService bayramService,
                                           INobetGrupGorevTipService nobetGrupGorevTipService,
                                           IEczaneNobetMazeretService eczaneNobetMazeretService
            )
        {
            _eczaneNobetSonucDemoDal = eczaneNobetSonucDemoDal;
            _eczaneNobetOrtakService = eczaneNobetOrtakService;
            _takvimService = takvimService;
            _bayramService = bayramService;
            _nobetGrupGorevTipService = nobetGrupGorevTipService;
            _eczaneNobetMazeretService = eczaneNobetMazeretService;
        }
        #endregion

        #region Listele, Ekle, sil, güncelle

        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Delete(int Id)
        {
            _eczaneNobetSonucDemoDal.Delete(new EczaneNobetSonucDemo { Id = Id });
        }

        public EczaneNobetSonucDemo GetById(int Id)
        {
            return _eczaneNobetSonucDemoDal.Get(x => x.Id == Id);
        }

        //[CacheAspect(typeof(MemoryCacheManager))]
        //public List<EczaneNobetIstatistik> GetEczaneNobetIstatistik()
        //{
        //    var nobetIstatistik = (from t in GetDetaylar()
        //                           .Select(s =>
        //                           new
        //                           {
        //                               Yil = s.Tarih.Year,
        //                               Ay = s.Tarih.Month,
        //                               s.EczaneNobetGrupId,
        //                               s.EczaneId,
        //                               s.NobetGrupId,
        //                               s.NobetGunKuralId
        //                           })
        //                           group t by new
        //                           {
        //                               t.Yil,
        //                               t.Ay,
        //                               t.EczaneNobetGrupId,
        //                               t.EczaneId,
        //                               t.NobetGrupId
        //                           } into grouped
        //                           select new EczaneNobetIstatistik
        //                           {
        //                               EczaneNobetGrupId = grouped.Key.EczaneNobetGrupId,
        //                               EczaneId = grouped.Key.EczaneId,
        //                               NobetGrupId = grouped.Key.NobetGrupId,

        //                               Pazar = grouped.Count(c => c.NobetGunKuralId == 1),
        //                               Pazartesi = grouped.Count(c => c.NobetGunKuralId == 2),
        //                               Sali = grouped.Count(c => c.NobetGunKuralId == 3),
        //                               Carsamba = grouped.Count(c => c.NobetGunKuralId == 4),
        //                               Persembe = grouped.Count(c => c.NobetGunKuralId == 5),
        //                               Cuma = grouped.Count(c => c.NobetGunKuralId == 6),
        //                               Cumartesi = grouped.Count(c => c.NobetGunKuralId == 7),
        //                               DiniBayram = grouped.Count(c => c.NobetGunKuralId == 8),
        //                               MilliBayram = grouped.Count(c => c.NobetGunKuralId == 9),
        //                               ToplamBayram = grouped.Count(c => c.NobetGunKuralId > 7),
        //                               Toplam = grouped.Count()
        //                           }).ToList();

        //    return nobetIstatistik;
        //}

        [CacheAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public List<EczaneNobetSonucDemo> GetList()
        {
            return _eczaneNobetSonucDemoDal.GetList();
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public List<EczaneNobetSonucDemoDetay2> GetDetaylar2(int nobetUstGrupId)
        {
            return _eczaneNobetSonucDemoDal.GetDetayList(w => w.NobetUstGrupId == nobetUstGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public List<EczaneNobetSonucDemoDetay2> GetDetaylar2(int nobetUstGrupId, int demoSonucVersiyon)
        {
            return _eczaneNobetSonucDemoDal.GetDetayList(w => w.NobetUstGrupId == nobetUstGrupId 
                                                           && w.NobetSonucDemoTipId == demoSonucVersiyon);
        }

        //[CacheAspect(typeof(MemoryCacheManager))]
        //[LogAspect(typeof(DatabaseLogger))]
        //public List<EczaneNobetSonucListe> GetSonuclar()
        //{
        //    return (from t in GetDetaylar()
        //            select new EczaneNobetSonucListe
        //            {
        //                Ay = t.Tarih.Month,
        //                BayramTurId = t.BayramTurId,
        //                EczaneAdi = t.EczaneAdi,
        //                EczaneId = t.EczaneId,
        //                EczaneNobetGrupId = t.EczaneNobetGrupId,
        //                Gun = t.Tarih.Day,
        //                GunGrup = t.GunGrup,
        //                GunTanim = t.GunTanim,
        //                Id = t.Id,
        //                NobetGorevTipAdi = t.NobetGorevTipAdi,
        //                NobetGorevTipId = t.NobetGorevTipId,
        //                NobetGrupAdi = t.NobetGrupAdi,
        //                NobetGrupId = t.NobetGrupId,
        //                NobetGunKuralId = t.NobetGunKuralId,
        //                NobetUstGrupId = t.NobetUstGrupId,
        //                TakvimId = t.TakvimId,
        //                Tarih = t.Tarih,
        //                Yil = t.Tarih.Year
        //            }).ToList();
        //}

        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Insert(EczaneNobetSonucDemo sonuc)
        {
            _eczaneNobetSonucDemoDal.Insert(sonuc);
        }

        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(EczaneNobetSonucDemo sonuc)
        {
            _eczaneNobetSonucDemoDal.Update(sonuc);
        }

        public EczaneNobetSonucDemoDetay2 GetDetayById(int eczaneNobetSonucId)
        {
            return _eczaneNobetSonucDemoDal.GetDetay(x => x.Id == eczaneNobetSonucId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetSonucDemoDetay2> GetDetaylar()
        {
            return _eczaneNobetSonucDemoDal.GetDetayList();
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetSonucDemoDetay2> GetDetaylar(int nobetUstGrupId)
        {
            return _eczaneNobetSonucDemoDal.GetDetayList(x=> x.NobetUstGrupId == nobetUstGrupId);
        }
        #endregion

        #region Çift eczaneler nodes - edges

        //public List<EczaneNobetSonucNode> GetEczaneNobetSonucDemoNodes()
        //{
        //    var nodes = GetDetaylar()
        //        .Select(s => new { s.EczaneId, s.EczaneAdi, s.NobetGrupId, s.NobetSonucDemoTipId }).Distinct()
        //               .Select(h => new EczaneNobetSonucNode
        //               {
        //                   Id = h.EczaneId,
        //                   Label = h.EczaneAdi,
        //                   Value = 5,
        //                   Level = h.NobetGrupId - 1,
        //                   Group = h.NobetGrupId - 1,
        //                   NobetSonucDemoTipId = h.NobetSonucDemoTipId
        //               })
        //               .ToList();
        //    return nodes;
        //}

        //public List<EczaneNobetSonucNode> GetEczaneNobetSonucDemoNodes(int yil, int ay, int demoTipId, List<int> eczaneIdList)
        //{
        //    var nodes = _eczaneNobetSonucDemoDal.GetDetayList(x => x.Yil == yil && x.Ay <= ay && x.NobetSonucDemoTipId == demoTipId && eczaneIdList.Contains(x.EczaneId))
        //        .Select(s => new
        //        {
        //            s.EczaneId,
        //            s.EczaneAdi,
        //            s.NobetGrupId,
        //            s.NobetSonucDemoTipId
        //        }).Distinct()
        //               .Select(h => new EczaneNobetSonucNode
        //               {
        //                   Id = h.EczaneId,
        //                   Label = h.EczaneAdi,
        //                   Value = 5,
        //                   Level = h.NobetGrupId - 1,
        //                   Group = h.NobetGrupId - 1,
        //                   NobetSonucDemoTipId = h.NobetSonucDemoTipId
        //               })
        //               .ToList();
        //    return nodes;
        //}

        //public List<EczaneNobetSonucEdge> GetEczaneNobetSonucDemoEdges(int yil, int ay, int ayniGuneDenkGelenNobetSayisi, int demoSonucVersiyon, int nobetUstGrupId)
        //{
        //    var nobetSonuclar = GetSonuclarYillikKumulatif(yil, ay, demoSonucVersiyon, nobetUstGrupId);
        //    //{ from: 1, to: 5, value: 1,  label: 1,  title: 'FA->DE: 1 adet' },
        //    var edges = _eczaneNobetOrtakService.GrupSayisinaGoreAnalizeGonder(nobetSonuclar, ayniGuneDenkGelenNobetSayisi)
        //                    .Select(h => new EczaneNobetSonucEdge
        //                    {
        //                        NobetUstGrupId = nobetUstGrupId,
        //                        From = h.G1EczaneId,
        //                        To = h.G2EczaneId,
        //                        Value = h.AyniGunNobetTutmaSayisi,
        //                        Label = h.AyniGunNobetTutmaSayisi,
        //                        Title = "From eczane: " + h.G1EczaneId + " To eczane: " + h.G2EczaneId + ": " + h.AyniGunNobetTutmaSayisi + " adet birlikte nöbet"
        //                    })
        //                    .ToList();
        //    return edges;
        //}

        #endregion
        //public List<EczaneNobetSonucListe> GetSonuclarYillikKumulatif(int yil, int ay, int demoSonucVersiyon, int nobetUstGrupId)
        //{
        //    return _eczaneNobetSonucDemoDal.GetDetayList(x => x.Yil == yil && x.Ay <= ay && x.NobetSonucDemoTipId == demoSonucVersiyon && x.NobetUstGrupId == nobetUstGrupId)
        //        .Select(s => new EczaneNobetSonucListe
        //        {
        //            Ay = s.Ay,
        //            BayramTurId = s.BayramTurId,
        //            EczaneAdi = s.EczaneAdi,
        //            EczaneId = s.EczaneId,
        //            EczaneNobetGrupId = s.EczaneNobetGrupId,
        //            Gun = s.Gun,
        //            GunGrup = s.GunGrup,
        //            GunTanim = s.GunTanim,
        //            HaftaninGunu = s.HaftaninGunu,
        //            Id = s.Id,
        //            NobetGorevTipAdi = s.NobetGorevTipAdi,
        //            NobetGorevTipId = s.NobetGorevTipId,
        //            NobetGrupAdi = s.NobetGrupAdi,
        //            NobetGrupId = s.NobetGrupId,
        //            NobetGunKuralId = s.NobetGunKuralId,
        //            NobetUstGrupId = s.NobetUstGrupId,
        //            TakvimId = s.TakvimId,
        //            Tarih = s.Tarih,
        //            Yil = s.Yil
        //        }).ToList();
        //}

        //public List<EczaneNobetSonucDemoDetay> GetDetaylar(int nobetUstGrupId)
        //{
        //    return _eczaneNobetSonucDemoDal.GetDetayList(x => x.NobetUstGrupId == nobetUstGrupId);
        //}

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetSonucListe2> GetSonuclar2(int nobetUstGrupId)
        {
            var sonuclar = GetDetaylar2(nobetUstGrupId)
                .Select(e => new
                {
                    e.NobetGorevTipId,
                    e.TakvimId,
                    //e.NobetSonucDemoTipId,
                    e.EczaneNobetGrupId,
                    e.EczaneAdi,
                    e.NobetGrupAdi,
                    e.NobetGrupId,
                    e.Tarih,
                    e.NobetUstGrupId,
                    e.EczaneId
                });

            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar()
                .Select(v => new
                {
                    v.NobetGorevTipAdi,
                    v.NobetGorevTipId,
                    v.NobetGrupId,
                    v.Id
                });

            var liste2 = (from s in sonuclar
                          join b in nobetGrupGorevTipler
                              on new { s.NobetGrupId, s.NobetGorevTipId }
                              equals new { b.NobetGrupId, b.NobetGorevTipId }
                          select new
                          {
                              s.NobetGorevTipId,
                              s.TakvimId,
                              //s.NobetSonucDemoTipId,
                              s.EczaneNobetGrupId,
                              s.EczaneAdi,
                              s.NobetGrupAdi,
                              s.NobetGrupId,
                              NobetGrupGorevTipId = b.Id,
                              s.Tarih,
                              s.NobetUstGrupId,
                              s.EczaneId
                          }).ToList();

            var culture = new CultureInfo("tr-TR");

            var mazeretler = _eczaneNobetMazeretService.GetDetaylar(nobetUstGrupId);

            var bayramlar = _bayramService.GetDetaylar();

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
                             EczaneId = s.EczaneId,
                             Tarih = s.Tarih,
                             EczaneNobetGrupId = s.EczaneNobetGrupId,
                             NobetUstGrupId = s.NobetUstGrupId,
                             TakvimId = s.TakvimId,
                             EczaneAdi = s.EczaneAdi,
                             NobetGrupAdi = $"{s.NobetGrupId} {s.NobetGrupAdi}",
                             NobetGrupId = s.NobetGrupId,
                             //NobetSonucDemoTipId = s.NobetSonucDemoTipId,
                             NobetGunKuralId = (b?.TakvimId == s.TakvimId && b?.NobetGrupGorevTipId == s.NobetGrupGorevTipId) ? b.NobetGunKuralId : (int)s.Tarih.DayOfWeek,
                             NobetGunKuralAdi = (b?.TakvimId == s.TakvimId && b?.NobetGrupGorevTipId == s.NobetGrupGorevTipId) ? b.NobetGunKuralAdi : culture.DateTimeFormat.GetDayName(s.Tarih.DayOfWeek),
                             //GunTanim = b?.NobetGunKuralAdi ?? culture.DateTimeFormat.GetDayName(s.Tarih.DayOfWeek),                             
                             //GunTanim = $"{p.NobetGunKuralId}",//.{s.GunTanim}",
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
                             //GunGrup = (b?.TakvimId == s.TakvimId && b?.NobetGrupGorevTipId == s.NobetGrupGorevTipId) ? "Bayram" : s.Tarih.DayOfWeek == 0 ? "Pazar" : "Hafta İçi",
                             Gun = s.Tarih.Day,
                             MazeretId = (m?.TakvimId == s.TakvimId && m?.EczaneNobetGrupId == s.EczaneNobetGrupId) ? m.MazeretId : 0,
                             Mazeret = (m?.TakvimId == s.TakvimId && m?.EczaneNobetGrupId == s.EczaneNobetGrupId) ? m.MazeretAdi : null,
                             MazeretTuru = (m?.TakvimId == s.TakvimId && m?.EczaneNobetGrupId == s.EczaneNobetGrupId) ? m.MazeretTuru : null,
                             SonucTuru = EczaneNobetSonucTuru.Demo
                         }).ToList();

            return liste;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetSonucListe2> GetSonuclar2(int nobetUstGrupId, int demoSonucVersiyon)
        {
            var sonuclar = GetDetaylar2(nobetUstGrupId, demoSonucVersiyon)
                .Select(e => new
                {
                    e.NobetGorevTipId,
                    e.TakvimId,
                    //e.NobetSonucDemoTipId,
                    e.EczaneNobetGrupId,
                    e.EczaneAdi,
                    e.NobetGrupAdi,
                    e.NobetGrupId,
                    e.Tarih,
                    e.NobetUstGrupId,
                    e.EczaneId
                });

            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar()
                .Select(v => new
                {
                    v.NobetGorevTipAdi,
                    v.NobetGorevTipId,
                    v.NobetGrupId,
                    v.Id
                });

            var liste2 = (from s in sonuclar
                          join b in nobetGrupGorevTipler
                              on new { s.NobetGrupId, s.NobetGorevTipId }
                              equals new { b.NobetGrupId, b.NobetGorevTipId }
                          select new
                          {
                              s.NobetGorevTipId,
                              s.TakvimId,
                              //s.NobetSonucDemoTipId,
                              s.EczaneNobetGrupId,
                              s.EczaneAdi,
                              s.NobetGrupAdi,
                              s.NobetGrupId,
                              NobetGrupGorevTipId = b.Id,
                              s.Tarih,
                              s.NobetUstGrupId,
                              s.EczaneId
                          }).ToList();

            var culture = new CultureInfo("tr-TR");

            var mazeretler = _eczaneNobetMazeretService.GetDetaylar(nobetUstGrupId);

            var bayramlar = _bayramService.GetDetaylar();

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
                             EczaneId = s.EczaneId,
                             Tarih = s.Tarih,
                             EczaneNobetGrupId = s.EczaneNobetGrupId,
                             NobetUstGrupId = s.NobetUstGrupId,
                             TakvimId = s.TakvimId,
                             EczaneAdi = s.EczaneAdi,
                             NobetGrupAdi = $"{s.NobetGrupId} {s.NobetGrupAdi}",
                             NobetGrupId = s.NobetGrupId,
                             //NobetSonucDemoTipId = s.NobetSonucDemoTipId,
                             NobetGunKuralId = (b?.TakvimId == s.TakvimId && b?.NobetGrupGorevTipId == s.NobetGrupGorevTipId) ? b.NobetGunKuralId : (int)s.Tarih.DayOfWeek,
                             NobetGunKuralAdi = (b?.TakvimId == s.TakvimId && b?.NobetGrupGorevTipId == s.NobetGrupGorevTipId) ? b.NobetGunKuralAdi : culture.DateTimeFormat.GetDayName(s.Tarih.DayOfWeek),
                             //GunTanim = b?.NobetGunKuralAdi ?? culture.DateTimeFormat.GetDayName(s.Tarih.DayOfWeek),                             
                             //GunTanim = $"{p.NobetGunKuralId}",//.{s.GunTanim}",
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
                             //GunGrup = (b?.TakvimId == s.TakvimId && b?.NobetGrupGorevTipId == s.NobetGrupGorevTipId) ? "Bayram" : s.Tarih.DayOfWeek == 0 ? "Pazar" : "Hafta İçi",
                             Gun = s.Tarih.Day,
                             MazeretId = (m?.TakvimId == s.TakvimId && m?.EczaneNobetGrupId == s.EczaneNobetGrupId) ? m.MazeretId : 0,
                             Mazeret = (m?.TakvimId == s.TakvimId && m?.EczaneNobetGrupId == s.EczaneNobetGrupId) ? m.MazeretAdi : null,
                             MazeretTuru = (m?.TakvimId == s.TakvimId && m?.EczaneNobetGrupId == s.EczaneNobetGrupId) ? m.MazeretTuru : null
                         }).ToList();

            return liste;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetIstatistikGunFarki> EczaneNobetIstatistikGunFarkiHesapla(int nobetUstGrupId)
        {
            var sonuclar = GetSonuclar2(nobetUstGrupId);
            var gunDegerler = sonuclar.Select(s => s.NobetGunKuralId).Distinct().OrderBy(o => o).ToList();
            var gunFarki = _eczaneNobetOrtakService.EczaneNobetIstatistikGunFarkiHesapla(sonuclar);

            return gunFarki;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetIstatistikGunFarki> EczaneNobetIstatistikGunFarkiHesapla(int nobetUstGrupId, int demoSonucVersiyon)
        {
            var sonuclar = GetSonuclar2(nobetUstGrupId, demoSonucVersiyon);
            var gunDegerler = sonuclar.Select(s => s.NobetGunKuralId).Distinct().OrderBy(o => o).ToList();
            var gunFarki = _eczaneNobetOrtakService.EczaneNobetIstatistikGunFarkiHesapla(sonuclar);

            return gunFarki;
        }        
    }
}

