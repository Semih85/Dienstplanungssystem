using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Business.Abstract;
using WM.Northwind.Business.Abstract.EczaneNobet;
using WM.Northwind.DataAccess.Abstract.EczaneNobet;
using WM.Core.Aspects.PostSharp.CacheAspects;
using WM.Core.CrossCuttingConcerns.Caching.Microsoft;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;
using WM.Northwind.Entities.Concrete.Optimization.EczaneNobet;
using WM.Optimization.Abstract.Samples;
using WM.Core.Aspects.PostSharp.LogAspects;
using WM.Core.CrossCuttingConcerns.Logging.Log4Net.Logger;

namespace WM.Northwind.Business.Concrete.Managers.EczaneNobet
{
    public class NobetGrupGorevTipKisitManager : INobetGrupGorevTipKisitService
    {
        private INobetGrupGorevTipKisitDal _nobetGrupGorevTipKisitDal;

        public NobetGrupGorevTipKisitManager(INobetGrupGorevTipKisitDal nobetGrupGorevTipKisitDal)
        {
            _nobetGrupGorevTipKisitDal = nobetGrupGorevTipKisitDal;
        }

        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Delete(int nobetGrupGorevTipKisitId)
        {
            _nobetGrupGorevTipKisitDal.Delete(new NobetGrupGorevTipKisit { Id = nobetGrupGorevTipKisitId });
        }

        public NobetGrupGorevTipKisit GetById(int nobetGrupGorevTipKisitId)
        {
            return _nobetGrupGorevTipKisitDal.Get(x => x.Id == nobetGrupGorevTipKisitId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupGorevTipKisit> GetList()
        {
            return _nobetGrupGorevTipKisitDal.GetList();
        }

        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Insert(NobetGrupGorevTipKisit nobetGrupGorevTipKisit)
        {
            _nobetGrupGorevTipKisitDal.Insert(nobetGrupGorevTipKisit);
        }

        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(NobetGrupGorevTipKisit nobetGrupGorevTipKisit)
        {
            _nobetGrupGorevTipKisitDal.Update(nobetGrupGorevTipKisit);
        }
        public NobetGrupGorevTipKisitDetay GetDetayById(int nobetGrupGorevTipKisitId)
        {
            return _nobetGrupGorevTipKisitDal.GetDetay(x => x.Id == nobetGrupGorevTipKisitId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupGorevTipKisitDetay> GetDetaylar()
        {
            return _nobetGrupGorevTipKisitDal.GetDetayList();
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupGorevTipKisitDetay> GetDetaylar(int nobetUstGrupId)
        {
            return _nobetGrupGorevTipKisitDal.GetDetayList(x => x.NobetUstGrupId == nobetUstGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public NobetGrupGorevTipKisitDetay GetDetay(int kisitId, int nobetGrupGorevTipId)
        {
            return _nobetGrupGorevTipKisitDal.GetDetay(x => x.KisitId == kisitId && x.NobetGrupGorevTipId == nobetGrupGorevTipId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupGorevTipKisitDetay> GetDetaylarByNobetGrupGorevTipId(int nobetGrupGorevTipId)
        {
            return _nobetGrupGorevTipKisitDal.GetDetayList(x => x.NobetGrupGorevTipId == nobetGrupGorevTipId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupGorevTipKisitDetay> GetDetaylar(int kisitId, List<int> nobetGrupGorevTipIdList)
        {
            return _nobetGrupGorevTipKisitDal.GetDetayList(x => x.KisitId == kisitId && nobetGrupGorevTipIdList.Contains(x.NobetGrupGorevTipId));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupGorevTipKisitDetay> GetDetaylarByKisitId(int kisitId)
        {
            return _nobetGrupGorevTipKisitDal.GetDetayList(x => x.KisitId == kisitId);
        }

    }
}