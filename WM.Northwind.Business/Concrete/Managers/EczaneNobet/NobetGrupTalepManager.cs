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
    public class NobetGrupTalepManager : INobetGrupTalepService
    {
        private INobetGrupTalepDal _nobetGrupTalepDal;

        public NobetGrupTalepManager(INobetGrupTalepDal nobetGrupTalepDal)
        {
            _nobetGrupTalepDal = nobetGrupTalepDal;
        }
        
        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Delete(int nobetGrupTalepId)
        {
            _nobetGrupTalepDal.Delete(new NobetGrupTalep { Id = nobetGrupTalepId });
        }

        public NobetGrupTalep GetById(int nobetGrupTalepId)
        {
            return _nobetGrupTalepDal.Get(x => x.Id == nobetGrupTalepId);
        }
        
        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupTalep> GetList()
        {
            return _nobetGrupTalepDal.GetList();
        }
        
        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Insert(NobetGrupTalep nobetGrupTalep)
        {
            _nobetGrupTalepDal.Insert(nobetGrupTalep);
        }
        
        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(NobetGrupTalep nobetGrupTalep)
        {
            _nobetGrupTalepDal.Update(nobetGrupTalep);
        }
        public NobetGrupTalepDetay GetDetayById(int nobetGrupTalepId)
        {
            return _nobetGrupTalepDal.GetDetay(x => x.Id == nobetGrupTalepId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupTalepDetay> GetDetaylar()
        {
            return _nobetGrupTalepDal.GetDetayList();
        }

        public List<NobetGrupTalepDetay> GetDetaylar(List<int> nobetGrupIdList)
        {
            return _nobetGrupTalepDal.GetDetayList(x => nobetGrupIdList.Contains(x.NobetGrupId));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupTalepDetay> GetDetaylar(int nobetUstGrupId)
        {
            return _nobetGrupTalepDal.GetDetayList(x => x.NobetUstGrupId == nobetUstGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupTalepDetay> GetDetaylarByNobetUstGrupIdList(List<int> nobetUstGrupId)
        {
            return _nobetGrupTalepDal.GetDetayList(x => nobetUstGrupId.Contains(x.NobetUstGrupId));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupTalepDetay> GetDetaylarByNobetGrupGorevTipId(int nobetGrupGorevTipId)
        {
            return _nobetGrupTalepDal.GetDetayList(x => x.NobetUstGrupId == nobetGrupGorevTipId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupTalepDetay> GetDetaylar(List<int> nobetGrupIdList, DateTime baslamaTarihi, DateTime bitisTarihi)
        {
            return _nobetGrupTalepDal.GetDetayList(x => nobetGrupIdList.Contains(x.NobetGrupId)
                                                    && (x.Tarih >= baslamaTarihi && x.Tarih <= bitisTarihi));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupTalepDetay> GetDetaylar(DateTime baslamaTarihi, DateTime bitisTarihi, List<int> nobetGrupGorevTipIdList)
        {
            return _nobetGrupTalepDal.GetDetayList(x => nobetGrupGorevTipIdList.Contains(x.NobetGrupGorevTipId)
                                                    && (x.Tarih >= baslamaTarihi && x.Tarih <= bitisTarihi));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupTalepDetay> GetDetaylar(DateTime baslamaTarihi, DateTime bitisTarihi, int nobetGrupGorevTipId)
        {
            return _nobetGrupTalepDal.GetDetayList(x => x.NobetGrupGorevTipId == nobetGrupGorevTipId
                                                    && (x.Tarih >= baslamaTarihi && x.Tarih <= bitisTarihi));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupTalepDetay> GetDetaylarSonrasi(DateTime baslamaTarihi, int nobetGrupGorevTipId)
        {
            return _nobetGrupTalepDal.GetDetayList(x => x.NobetGrupGorevTipId == nobetGrupGorevTipId
                                                     && x.Tarih >= baslamaTarihi);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupTalepDetay> GetDetaylarOncesi(DateTime baslamaTarihi, int nobetGrupGorevTipId)
        {
            return _nobetGrupTalepDal.GetDetayList(x => x.NobetGrupGorevTipId == nobetGrupGorevTipId
                                                     && x.Tarih <= baslamaTarihi);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupTalepDetay> GetDetaylar(DateTime? baslamaTarihi, DateTime? bitisTarihi, int nobetGrupGorevTipId)
        {
            return _nobetGrupTalepDal.GetDetayList(x => x.NobetGrupGorevTipId == nobetGrupGorevTipId
                                                    && (x.Tarih >= baslamaTarihi || baslamaTarihi == null)
                                                    && (x.Tarih <= bitisTarihi || bitisTarihi == null));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupTalepDetay> GetDetaylar(DateTime baslamaTarihi, List<int> nobetGrupGorevTipIdList)
        {
            return _nobetGrupTalepDal.GetDetayList(x => nobetGrupGorevTipIdList.Contains(x.NobetGrupGorevTipId)
                                                     && x.Tarih >= baslamaTarihi);
        }
    }
}
