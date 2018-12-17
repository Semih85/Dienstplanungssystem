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
    public class BayramTurManager : IBayramTurService
    {
        private IBayramTurDal _bayramTurDal;

        public BayramTurManager(IBayramTurDal bayramTurDal)
        {
            _bayramTurDal = bayramTurDal;
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Delete(int bayramTurId)
        {
            _bayramTurDal.Delete(new BayramTur { Id = bayramTurId });
        }

        public BayramTur GetById(int bayramTurId)
        {
            return _bayramTurDal.Get(x => x.Id == bayramTurId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<BayramTur> GetList()
        {
            return _bayramTurDal.GetList();
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Insert(BayramTur bayramTur)
        {
            _bayramTurDal.Insert(bayramTur);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(BayramTur bayramTur)
        {
            _bayramTurDal.Update(bayramTur);
        }


    }
}