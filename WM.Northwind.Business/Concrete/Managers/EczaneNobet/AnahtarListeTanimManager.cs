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
    public class AnahtarListeTanimManager : IAnahtarListeTanimService
    {
        private IAnahtarListeTanimDal _anahtarListeTanimDal;

        public AnahtarListeTanimManager(IAnahtarListeTanimDal anahtarListeTanimDal)
        {
            _anahtarListeTanimDal = anahtarListeTanimDal;
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Delete(int anahtarListeTanimId)
        {
            _anahtarListeTanimDal.Delete(new AnahtarListeTanim { Id = anahtarListeTanimId });
        }

        public AnahtarListeTanim GetById(int anahtarListeTanimId)
        {
            return _anahtarListeTanimDal.Get(x => x.Id == anahtarListeTanimId);
        }
         [CacheAspect(typeof(MemoryCacheManager))]
        public List<AnahtarListeTanim> GetList()
        {
            return _anahtarListeTanimDal.GetList();
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Insert(AnahtarListeTanim anahtarListeTanim)
        {
            _anahtarListeTanimDal.Insert(anahtarListeTanim);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(AnahtarListeTanim anahtarListeTanim)
        {
            _anahtarListeTanimDal.Update(anahtarListeTanim);
        }
                        

    } 
}