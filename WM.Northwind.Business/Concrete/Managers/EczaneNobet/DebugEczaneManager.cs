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
//using WM.Northwind.Entities.Concrete.Optimization.EczaneNobet;
//using WM.Optimization.Abstract.Samples;

namespace WM.Northwind.Business.Concrete.Managers.EczaneNobet
{
    public class DebugEczaneManager : IDebugEczaneService
    {
        private IDebugEczaneDal _debugEczaneDal;

        public DebugEczaneManager(IDebugEczaneDal debugEczaneDal)
        {
            _debugEczaneDal = debugEczaneDal;
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Delete(int debugEczaneId)
        {
            _debugEczaneDal.Delete(new DebugEczane { Id = debugEczaneId });
        }

        public DebugEczane GetById(int debugEczaneId)
        {
            return _debugEczaneDal.Get(x => x.Id == debugEczaneId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<DebugEczane> GetList()
        {
            return _debugEczaneDal.GetList();
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Insert(DebugEczane debugEczane)
        {
            _debugEczaneDal.Insert(debugEczane);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(DebugEczane debugEczane)
        {
            _debugEczaneDal.Update(debugEczane);
        }

        public DebugEczaneDetay GetDetayById(int debugEczaneId)
        {
            return _debugEczaneDal.GetDetay(x => x.Id == debugEczaneId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<DebugEczaneDetay> GetDetaylar()
        {
            return _debugEczaneDal.GetDetayList();
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<DebugEczaneDetay> GetDetaylar(int nobetUstGrupId)
        {
            return _debugEczaneDal.GetDetayList(x => x.NobetUstGrupId == nobetUstGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<DebugEczaneDetay> GetDetaylarAktifOlanlar(int nobetUstGrupId)
        {
            return _debugEczaneDal.GetDetayList(x => x.NobetUstGrupId == nobetUstGrupId && x.AktifMi);
        }
    }
}