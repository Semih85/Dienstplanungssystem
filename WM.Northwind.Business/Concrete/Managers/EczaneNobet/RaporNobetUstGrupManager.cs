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
    public class RaporNobetUstGrupManager : IRaporNobetUstGrupService
    {
        private IRaporNobetUstGrupDal _raporNobetUstGrupDal;

        public RaporNobetUstGrupManager(IRaporNobetUstGrupDal raporNobetUstGrupDal)
        {
            _raporNobetUstGrupDal = raporNobetUstGrupDal;
        }

        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Delete(int raporNobetUstGrupId)
        {
            _raporNobetUstGrupDal.Delete(new RaporNobetUstGrup { Id = raporNobetUstGrupId });
        }

        public RaporNobetUstGrup GetById(int raporNobetUstGrupId)
        {
            return _raporNobetUstGrupDal.Get(x => x.Id == raporNobetUstGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<RaporNobetUstGrup> GetList()
        {
            return _raporNobetUstGrupDal.GetList();
        }

        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Insert(RaporNobetUstGrup raporNobetUstGrup)
        {
            _raporNobetUstGrupDal.Insert(raporNobetUstGrup);
        }

        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(RaporNobetUstGrup raporNobetUstGrup)
        {
            _raporNobetUstGrupDal.Update(raporNobetUstGrup);
        }
        public RaporNobetUstGrupDetay GetDetayById(int raporNobetUstGrupId)
        {
            return _raporNobetUstGrupDal.GetDetay(x => x.Id == raporNobetUstGrupId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<RaporNobetUstGrupDetay> GetDetaylar()
        {
            return _raporNobetUstGrupDal.GetDetayList();
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<RaporNobetUstGrupDetay> GetDetaylar(int nobetUstGrupId)
        {
            return _raporNobetUstGrupDal.GetDetayList(x => x.NobetUstGrupId == nobetUstGrupId);
        }

    }
}