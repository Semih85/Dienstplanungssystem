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
    public class RaporRolManager : IRaporRolService
    {
        private IRaporRolDal _raporRolDal;

        public RaporRolManager(IRaporRolDal raporRolDal)
        {
            _raporRolDal = raporRolDal;
        }

        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Delete(int raporRolId)
        {
            _raporRolDal.Delete(new RaporRol { Id = raporRolId });
        }

        public RaporRol GetById(int raporRolId)
        {
            return _raporRolDal.Get(x => x.Id == raporRolId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<RaporRol> GetList()
        {
            return _raporRolDal.GetList();
        }

        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Insert(RaporRol raporRol)
        {
            _raporRolDal.Insert(raporRol);
        }

        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(RaporRol raporRol)
        {
            _raporRolDal.Update(raporRol);
        }
        public RaporRolDetay GetDetayById(int raporRolId)
        {
            return _raporRolDal.GetDetay(x => x.Id == raporRolId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<RaporRolDetay> GetDetaylar()
        {
            return _raporRolDal.GetDetayList();
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<RaporRolDetay> GetDetaylar(int rolId)
        {
            return _raporRolDal.GetDetayList(x => x.RoleId == rolId);
        }

    }
}