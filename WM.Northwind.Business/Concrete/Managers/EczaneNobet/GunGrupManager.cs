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

namespace WM.Northwind.Business.Concrete.Managers.EczaneNobet
{
    public class GunGrupManager : IGunGrupService
    {
        private IGunGrupDal _gunGrupDal;

        public GunGrupManager(IGunGrupDal gunGrupDal)
        {
            _gunGrupDal = gunGrupDal;
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Delete(int gunGrupId)
        {
            _gunGrupDal.Delete(new GunGrup { Id = gunGrupId });
        }

        public GunGrup GetById(int gunGrupId)
        {
            return _gunGrupDal.Get(x => x.Id == gunGrupId);
        }
        [CacheAspect(typeof(MemoryCacheManager))]
        public List<GunGrup> GetList()
        {
            return _gunGrupDal.GetList();
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Insert(GunGrup gunGrup)
        {
            _gunGrupDal.Insert(gunGrup);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(GunGrup gunGrup)
        {
            _gunGrupDal.Update(gunGrup);
        }


    }
}