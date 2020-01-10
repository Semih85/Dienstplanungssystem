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
using WM.Core.Aspects.PostSharp.TranstionAspects;
using WM.Core.Aspects.PostSharp.LogAspects;
using WM.Core.CrossCuttingConcerns.Logging.Log4Net.Logger;

namespace WM.Northwind.Business.Concrete.Managers.EczaneNobet
{
    public class NobetGrupKuralManager : INobetGrupKuralService
    {
        private INobetGrupKuralDal _nobetGrupKuralDal;

        public NobetGrupKuralManager(INobetGrupKuralDal nobetGrupKuralDal)
        {
            _nobetGrupKuralDal = nobetGrupKuralDal;
        }
        
        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Delete(int nobetGrupKuralId)
        {
            _nobetGrupKuralDal.Delete(new NobetGrupKural { Id = nobetGrupKuralId });
        }

        public NobetGrupKural GetById(int nobetGrupKuralId)
        {
            return _nobetGrupKuralDal.Get(x => x.Id == nobetGrupKuralId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupKural> GetList()
        {
            return _nobetGrupKuralDal.GetList();
        }

        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Insert(NobetGrupKural nobetGrupKural)
        {
            _nobetGrupKuralDal.Insert(nobetGrupKural);
        }

        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(NobetGrupKural nobetGrupKural)
        {
            _nobetGrupKuralDal.Update(nobetGrupKural);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public NobetGrupKuralDetay GetDetayById(int nobetGrupKuralId)
        {
            return _nobetGrupKuralDal.GetDetay(x => x.Id == nobetGrupKuralId);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public NobetGrupKuralDetay GetDetay(int nobetGrupGorevTipId, int nobetKuralId)
        {
            return _nobetGrupKuralDal.GetDetay(x => x.NobetGrupGorevTipId == nobetGrupGorevTipId && x.NobetKuralId == nobetKuralId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupKuralDetay> GetDetaylar()
        {
            return _nobetGrupKuralDal.GetDetayList();
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupKuralDetay> GetDetaylar(List<int> nobetGrupIdList)
        {
            return _nobetGrupKuralDal.GetDetayList(x => nobetGrupIdList.Contains(x.NobetGrupId));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupKuralDetay> GetDetaylar(int nobetGrupGorevTipId, int nobetKuralId, List<int> nobetUstGrupIdList)
        {
            return _nobetGrupKuralDal.GetDetayList(x => (x.NobetGrupGorevTipId == nobetGrupGorevTipId || nobetGrupGorevTipId == 0)
            && (x.NobetKuralId == nobetKuralId || nobetKuralId == 0)
            && nobetUstGrupIdList.Contains(x.NobetUstGrupId));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupKuralDetay> GetDetaylar(int nobetGrupGorevTipId, int nobetKuralId, int nobetUstGrupId)
        {
            return _nobetGrupKuralDal.GetDetayList(x => (x.NobetGrupGorevTipId == nobetGrupGorevTipId || nobetGrupGorevTipId == 0)
            && (x.NobetKuralId == nobetKuralId || nobetKuralId == 0)
            && nobetUstGrupId == x.NobetUstGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupKuralDetay> GetDetaylar(int nobetGrupGorevTipId, int nobetKuralId)
        {
            return _nobetGrupKuralDal.GetDetayList(x => (x.NobetGrupGorevTipId == nobetGrupGorevTipId || nobetGrupGorevTipId == 0)
            && x.NobetKuralId == nobetKuralId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupKuralDetay> GetDetaylar(List<int> nobetGrupGorevTipIdList, int nobetKuralId)
        {
            return _nobetGrupKuralDal.GetDetayList(x => nobetGrupGorevTipIdList.Contains(x.NobetGrupGorevTipId)
            && (x.NobetKuralId == nobetKuralId));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupKuralDetay> GetDetaylarByNobetGrupGorevTipIdList(List<int> nobetGrupGorevTipIdList)
        {
            return _nobetGrupKuralDal.GetDetayList(x => nobetGrupGorevTipIdList.Contains(x.NobetGrupGorevTipId));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupKuralDetay> GetDetaylarByNobetUstGrup(List<int> nobetUstGrupIdList)
        {
            return _nobetGrupKuralDal.GetDetayList(x => nobetUstGrupIdList.Contains(x.NobetUstGrupId));
        }

        [TransactionScopeAspect]
        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void CokluEkle(List<NobetGrupKural> nobetGrupKurallar)
        {
            _nobetGrupKuralDal.CokluEkle(nobetGrupKurallar);
        }

        [TransactionScopeAspect]
        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void CokluDegistir(List<NobetGrupKural> nobetGrupKurallar)
        {
            _nobetGrupKuralDal.CokluDegistir(nobetGrupKurallar);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupKuralDetay> GetDetaylarByNobetUstGrup(int nobetUstGrupId)
        {
            return _nobetGrupKuralDal.GetDetayList(x => x.NobetUstGrupId == nobetUstGrupId);
        }
    }
}
