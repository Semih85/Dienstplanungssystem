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
using WM.Northwind.Entities.Concrete.Authorization;
using WM.Core.Aspects.PostSharp.LogAspects;
using WM.Core.CrossCuttingConcerns.Logging.Log4Net.Logger;

namespace WM.Northwind.Business.Concrete.Managers.EczaneNobet
{
    public class NobetGrupGorevTipManager : INobetGrupGorevTipService
    {
        private INobetGrupGorevTipDal _nobetGrupGorevTipDal;
        private INobetGrupService _nobetGrupService;

        public NobetGrupGorevTipManager(INobetGrupGorevTipDal nobetGrupGorevTipDal,
            INobetGrupService nobetGrupService)
        {
            _nobetGrupGorevTipDal = nobetGrupGorevTipDal;
            _nobetGrupService = nobetGrupService;
        }
        
        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Delete(int nobetGrupGorevTipId)
        {
            _nobetGrupGorevTipDal.Delete(new NobetGrupGorevTip { Id = nobetGrupGorevTipId });
        }

        public NobetGrupGorevTip GetById(int nobetGrupGorevTipId)
        {
            return _nobetGrupGorevTipDal.Get(x => x.Id == nobetGrupGorevTipId);
        }
        
        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupGorevTip> GetList()
        {
            return _nobetGrupGorevTipDal.GetList();
        }
        
        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Insert(NobetGrupGorevTip nobetGrupGorevTip)
        {
            _nobetGrupGorevTipDal.Insert(nobetGrupGorevTip);
        }
        
        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(NobetGrupGorevTip nobetGrupGorevTip)
        {
            _nobetGrupGorevTipDal.Update(nobetGrupGorevTip);
        }
        public NobetGrupGorevTipDetay GetDetayById(int nobetGrupGorevTipId)
        {
            return _nobetGrupGorevTipDal.GetDetay(x => x.Id == nobetGrupGorevTipId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupGorevTipDetay> GetDetaylar()
        {
            return _nobetGrupGorevTipDal.GetDetayList();
        }

        public List<NobetGrupGorevTipDetay> GetDetaylarByNobetGorevTipId(int nobetGorevTipId)
        {
            return _nobetGrupGorevTipDal.GetDetayList(x => x.NobetGorevTipId == nobetGorevTipId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupGorevTipDetay> GetDetaylar(int nobetUstGrupId)
        {
            return _nobetGrupGorevTipDal.GetDetayList(x => x.NobetUstGrupId == nobetUstGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<MyDrop> GetMyDrop(int nobetUstGrupId)
        {
            var nobetGrupGorevTipler = _nobetGrupGorevTipDal.GetDetayList(x => x.NobetUstGrupId == nobetUstGrupId);

            if (nobetGrupGorevTipler.Select(s => s.NobetGorevTipId).Distinct().Count() > 1)
            {
                return nobetGrupGorevTipler.Select(s => new MyDrop { Id = s.Id, Value = $"{s.Id}, {s.NobetGrupAdi}, {s.NobetGorevTipAdi}" }).ToList();
            }
            else
            {
                return nobetGrupGorevTipler.Select(s => new MyDrop { Id = s.Id, Value = $"{s.Id}, {s.NobetGrupAdi}" }).ToList();
            }
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupGorevTipDetay> GetDetaylarByNobetGrupGorevTipId(int nobetGrupGorevTipId)
        {
            return _nobetGrupGorevTipDal.GetDetayList(x => x.Id == nobetGrupGorevTipId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupGorevTip> GetList(int nobetGorevTipId)
        {
            return _nobetGrupGorevTipDal.GetList(x => x.NobetGorevTipId == nobetGorevTipId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupGorevTip> GetList(int nobetGorevTipId, int nobetGrupId)
        {
            return _nobetGrupGorevTipDal.GetList(x => x.NobetGorevTipId == nobetGorevTipId && x.NobetGrupId == nobetGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupGorevTip> GetList(int nobetGorevTipId, List<int> nobetGrupIdList)
        {
            return _nobetGrupGorevTipDal.GetList(x => x.NobetGorevTipId == nobetGorevTipId && nobetGrupIdList.Contains(x.NobetGrupId));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupGorevTip> GetList(List<int> nobetGrupIdList)
        {
            return _nobetGrupGorevTipDal.GetList(x => nobetGrupIdList.Contains(x.NobetGrupId));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupGorevTipDetay> GetDetaylar(int nobetGorevTipId, List<int> nobetGrupIdList)
        {
            return _nobetGrupGorevTipDal.GetDetayList(x => x.NobetGorevTipId == nobetGorevTipId && nobetGrupIdList.Contains(x.NobetGrupId));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupGorevTipDetay> GetDetaylar(List<int> nobetGrupIdList)
        {
            return _nobetGrupGorevTipDal.GetDetayList(x => nobetGrupIdList.Contains(x.NobetGrupId));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupGorevTipDetay> GetDetaylarByIdList(List<int> idList)
        {
            return _nobetGrupGorevTipDal.GetDetayList(x => idList.Contains(x.Id));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupGorevTipDetay> GetDetaylarByNobetUstGrupIdList(List<int> nobetUstGrupIdList)
        {
            return _nobetGrupGorevTipDal.GetDetayList(x => nobetUstGrupIdList.Contains(x.NobetUstGrupId));
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGrupGorevTip> GetListByUser(User user)
        {
            //yetkili olduğu nöbet üst gruplar
            var nobetGruplar = _nobetGrupService.GetListByUser(user).Select(e => e.Id).ToList();
            //yetkili olduğu nöbet gruplar
            var yetkiliNobetGrupGorevTipler = GetList(nobetGruplar);

            return yetkiliNobetGrupGorevTipler;
        }
    }
}
