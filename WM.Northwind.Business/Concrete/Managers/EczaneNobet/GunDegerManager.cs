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
    public class GunDegerManager : IGunDegerService
    {
        private IGunDegerDal _gunDegerDal;

        public GunDegerManager(IGunDegerDal gunDegerDal)
        {
            _gunDegerDal = gunDegerDal;
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Delete(int gunDegerId)
        {
            _gunDegerDal.Delete(new GunDeger { Id = gunDegerId });
        }

        public GunDeger GetById(int gunDegerId)
        {
            return _gunDegerDal.Get(x => x.Id == gunDegerId);
        }
         [CacheAspect(typeof(MemoryCacheManager))]
        public List<GunDeger> GetList()
        {
            return _gunDegerDal.GetList();
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Insert(GunDeger gunDeger)
        {
            _gunDegerDal.Insert(gunDeger);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(GunDeger gunDeger)
        {
            _gunDegerDal.Update(gunDeger);
        }
        //public GunDegerDetay GetDetayById(int gunDegerId)
        //{
        //    return _gunDegerDal.GetDetay(x => x.Id == gunDegerId);
        //}

        //[CacheAspect(typeof(MemoryCacheManager))]
        //public List<GunDegerDetay> GetDetaylar()
        //{
        //    return _gunDegerDal.GetDetayList();
        //}
    }
}
