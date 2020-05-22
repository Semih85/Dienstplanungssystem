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
    public class EczaneNobetDegisimArzManager : IEczaneNobetDegisimArzService
    {
        private IEczaneNobetDegisimArzDal _eczaneNobetDegisimArzDal;

        public EczaneNobetDegisimArzManager(IEczaneNobetDegisimArzDal eczaneNobetDegisimArzDal)
        {
            _eczaneNobetDegisimArzDal = eczaneNobetDegisimArzDal;
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Delete(int eczaneNobetDegisimArzId)
        {
            _eczaneNobetDegisimArzDal.Delete(new EczaneNobetDegisimArz { Id = eczaneNobetDegisimArzId });
        }

        public EczaneNobetDegisimArz GetById(int eczaneNobetDegisimArzId)
        {
            return _eczaneNobetDegisimArzDal.Get(x => x.Id == eczaneNobetDegisimArzId);
        }
         [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetDegisimArz> GetList()
        {
            return _eczaneNobetDegisimArzDal.GetList();
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Insert(EczaneNobetDegisimArz eczaneNobetDegisimArz)
        {
            _eczaneNobetDegisimArzDal.Insert(eczaneNobetDegisimArz);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(EczaneNobetDegisimArz eczaneNobetDegisimArz)
        {
            _eczaneNobetDegisimArzDal.Update(eczaneNobetDegisimArz);
        }
        public EczaneNobetDegisimArzDetay GetDetayById(int eczaneNobetDegisimArzId)
        {
            return _eczaneNobetDegisimArzDal.GetDetay(x => x.Id == eczaneNobetDegisimArzId);
        }
            
        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetDegisimArzDetay> GetDetaylar()
        {
            return _eczaneNobetDegisimArzDal.GetDetayList();
        }
        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneNobetDegisimArzDetay> GetDetaylar(int nobetUstGrupId)
        {
            return _eczaneNobetDegisimArzDal.GetDetayList(x => x.NobetUstGrupId == nobetUstGrupId);
        }
        [CacheAspect(typeof(MemoryCacheManager))]

        public List<EczaneNobetDegisimArzDetay> GetDetaylarByEczaneSonucId(int eczaneNobetSonucId)
        {
            return _eczaneNobetDegisimArzDal.GetDetayList(x => x.EczaneNobetSonucId == eczaneNobetSonucId);

        }
    } 
}