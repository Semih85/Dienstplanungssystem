using System;
using System.Collections.Generic;
using System.Linq;
using WM.Core.Aspects.PostSharp.AutorizationAspects;
using WM.Core.Aspects.PostSharp.CacheAspects;
using WM.Core.Aspects.PostSharp.LogAspects;
using WM.Core.Aspects.PostSharp.TranstionAspects;
using WM.Core.Aspects.PostSharp.ValidationAspects;
using WM.Core.CrossCuttingConcerns.Caching.Microsoft;
using WM.Core.CrossCuttingConcerns.Logging.Log4Net.Logger;
using WM.Northwind.Business.Abstract;
using WM.Northwind.Business.Abstract.Authorization;
using WM.Northwind.Business.Abstract.EczaneNobet;
using WM.Northwind.Business.ValidationRules.FluentValidation;
using WM.Northwind.DataAccess.Abstract.EczaneNobet;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.Authorization;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.Business.Concrete.Managers.EczaneNobet
{
    //[SecuredOperation(Roles= "Admin,Oda,Üst Grup,Eczane")]
    public class EczaneNobetMazeretManager : IEczaneNobetMazeretService
    {
        private IEczaneNobetMazeretDal _eczaneNobetMazeretDal;
        private IUserService _userService;
        private IEczaneService _eczaneService;

        public EczaneNobetMazeretManager(IEczaneNobetMazeretDal eczaneNobetMazeretDal,
                                         IUserService userService,
                                         IEczaneService eczaneService)
        {
            _eczaneNobetMazeretDal = eczaneNobetMazeretDal;
            _userService = userService;
            _eczaneService = eczaneService;
        }

        [TransactionScopeAspect]
        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [FluentValidationAspect(typeof(EczaneNobetMazeretValidator))]
        public void CokluEkle(List<EczaneNobetMazeret> eczaneNobetMazeretler)
        {
            _eczaneNobetMazeretDal.CokluEkle(eczaneNobetMazeretler);
        }

        public EczaneNobetMazeretDetay GetDetayById(int eczaneNobetMazeretId)
        {
            return _eczaneNobetMazeretDal.GetDetay(x => x.Id == eczaneNobetMazeretId);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public void Delete(int Id)
        {
            _eczaneNobetMazeretDal.Delete(new EczaneNobetMazeret { Id = Id });
        }

        public EczaneNobetMazeret GetById(int eczaneNobetMazeretId)
        {
            return _eczaneNobetMazeretDal.Get(x => x.Id == eczaneNobetMazeretId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetMazeretDetay> GetDetaylar()
        {
            return _eczaneNobetMazeretDal.GetDetayList();
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetMazeretDetay> GetDetaylar(int yil, int ay, int nobetGrupId)
        {
            return _eczaneNobetMazeretDal.GetDetayList(x => x.Yil == yil && x.Ay == ay && x.NobetGrupId == nobetGrupId);
        }

        //[CacheAspect(typeof(MemoryCacheManager))]
        //public List<EczaneNobetMazeretDetay> GetDetaylar(int yil, int ayBaslangic, int ayBitis, int nobetGrupId)
        //{
        //    return _eczaneNobetMazeretDal.GetDetayList(yil, ayBaslangic, ayBitis, nobetGrupId);
        //}

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetMazeretDetay> GetDetaylar(int yil, int ay)
        {
            return _eczaneNobetMazeretDal.GetDetayList(x => x.Yil == yil && x.Ay == ay);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetMazeretDetay> GetDetaylar(int yil, int ay, List<int> ecznaneIdList)
        {
            return _eczaneNobetMazeretDal.GetDetayList(x => x.Yil == yil && x.Ay == ay && ecznaneIdList.Contains(x.EczaneId));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetMazeretDetay> GetDetaylar(DateTime baslangicTarihi, DateTime bitisTarihi, List<int> nobetGrupIdList)
        {
            return _eczaneNobetMazeretDal.GetDetayList(c => (c.Tarih >= baslangicTarihi && c.Tarih <= bitisTarihi)
                                                        && nobetGrupIdList.Contains(c.NobetGrupId));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetMazeretDetay> GetDetaylar(DateTime? baslangicTarihi, DateTime? bitisTarihi, int[] nobetGrupGorevTipIdList)
        {
            return _eczaneNobetMazeretDal.GetDetayList(x => (x.Tarih >= baslangicTarihi || baslangicTarihi == null)
                                                         && (x.Tarih <= bitisTarihi || bitisTarihi == null)
                                                        && nobetGrupGorevTipIdList.Contains(x.NobetGrupGorevTipId));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetMazeretDetay> GetDetaylarByNobetGrupGorevTipId(DateTime? baslangicTarihi, DateTime? bitisTarihi, int nobetGrupGorevTipId)
        {
            return _eczaneNobetMazeretDal.GetDetayList(x => (x.Tarih >= baslangicTarihi || baslangicTarihi == null)
                                                         && (x.Tarih <= bitisTarihi || bitisTarihi == null)
                                                         && nobetGrupGorevTipId == x.NobetGrupGorevTipId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetMazeretDetay> GetDetaylar(DateTime? baslangicTarihi, DateTime? bitisTarihi, int[] nobetGrupGorevTipIdList, int nobetUstGrupId)
        {
            return _eczaneNobetMazeretDal.GetDetayList(x => (x.Tarih >= baslangicTarihi || baslangicTarihi == null)
                                                         && (x.Tarih <= bitisTarihi || bitisTarihi == null)
                                                         && nobetGrupGorevTipIdList.Contains(x.NobetGrupGorevTipId)
                                                         && x.NobetUstGrupId == nobetUstGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetMazeretDetay> GetDetaylarByEczaneIdList(DateTime baslangicTarihi, DateTime bitisTarihi, List<int> ecznaneIdList)
        {
            return _eczaneNobetMazeretDal.GetDetayList(c => (c.Tarih >= baslangicTarihi && c.Tarih <= bitisTarihi)
                                                        && ecznaneIdList.Contains(c.EczaneId));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetMazeretDetay> GetDetaylarNobetGrupId(DateTime baslangicTarihi, DateTime bitisTarihi, int nobetGrupId)
        {
            return _eczaneNobetMazeretDal.GetDetayList(c => (c.Tarih >= baslangicTarihi && c.Tarih <= bitisTarihi)
                                                        && c.NobetGrupId == nobetGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetMazeretDetay> GetDetaylar(DateTime baslangicTarihi, DateTime bitisTarihi, int nobetUstGrupId)
        {
            return _eczaneNobetMazeretDal.GetDetayList(c => (c.Tarih >= baslangicTarihi && c.Tarih <= bitisTarihi)
                                                        && c.NobetUstGrupId == nobetUstGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetMazeretDetay> GetDetaylar(int nobetUstGrupId)
        {
            return _eczaneNobetMazeretDal.GetDetayList(x => x.NobetUstGrupId == nobetUstGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetMazeretSayilari> GetEczaneNobetMazeretSayilari(DateTime baslangicTarihi, DateTime bitisTarihi, List<int> nobetGrupIdList)
        {
            var liste = GetDetaylar(baslangicTarihi, bitisTarihi, nobetGrupIdList)
                .GroupBy(g => new { g.EczaneId, g.EczaneAdi, g.NobetGrupAdi, g.EczaneNobetGrupId })
                .Select(s => new EczaneNobetMazeretSayilari
                {
                    EczaneNobetGrupId = s.Key.EczaneNobetGrupId,
                    NobetGrupAdi = s.Key.NobetGrupAdi,
                    EczaneId = s.Key.EczaneId,
                    EczaneAdi = s.Key.EczaneAdi,
                    MazeretSayisi = s.Count()
                })
                .Where(w => w.MazeretSayisi > (bitisTarihi - baslangicTarihi).TotalDays
                //(bitisTarihi.DayOfYear - baslangicTarihi.DayOfYear)
                ).ToList();

            return liste;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetMazeretSayilari> GetEczaneNobetMazeretSayilari(DateTime baslangicTarihi, DateTime bitisTarihi, int nobetGrupId)
        {
            return GetDetaylar(baslangicTarihi, bitisTarihi, nobetGrupId)
                    .GroupBy(g => g.EczaneId)
                    .Select(s => new EczaneNobetMazeretSayilari
                    {
                        EczaneId = s.Key,
                        MazeretSayisi = s.Count()
                    })
                    .Where(w => w.MazeretSayisi > (bitisTarihi.DayOfYear - baslangicTarihi.DayOfYear)).ToList();
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetMazeret> GetList()
        {
            return _eczaneNobetMazeretDal.GetList();
        }

        public List<EczaneNobetMazeretDetay> GetListByUser(User user)
        {
            var rolIdler = _userService.GetUserRoles(user).OrderBy(s => s.RoleId).Select(u => u.RoleId).ToArray();
            var rolId = rolIdler.FirstOrDefault();
            var eczaneler = _eczaneService.GetListByUser(user);
            var eczaneNobetMazeretler = GetDetaylar().Where(x => eczaneler.Select(s => s.Id).Contains(x.EczaneId)).ToList();

            return eczaneNobetMazeretler;
        }

        [FluentValidationAspect(typeof(EczaneNobetMazeretValidator))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public void Insert(EczaneNobetMazeret sonuc)
        {
            _eczaneNobetMazeretDal.Insert(sonuc);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public void Update(EczaneNobetMazeret sonuc)
        {
            _eczaneNobetMazeretDal.Update(sonuc);
        }

    }
}
