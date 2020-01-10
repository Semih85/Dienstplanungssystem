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
    public class NobetGunKuralManager : INobetGunKuralService
    {
        private INobetGunKuralDal _nobetGunKuralDal;

        public NobetGunKuralManager(INobetGunKuralDal nobetGunKuralDal)
        {
            _nobetGunKuralDal = nobetGunKuralDal;
        }
        
        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Delete(int nobetGunKuralId)
        {
            _nobetGunKuralDal.Delete(new NobetGunKural { Id = nobetGunKuralId });
        }

        public NobetGunKural GetById(int nobetGunKuralId)
        {
            return _nobetGunKuralDal.Get(x => x.Id == nobetGunKuralId);
        }
         [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGunKural> GetList()
        {
            return _nobetGunKuralDal.GetList();
        }
        
        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Insert(NobetGunKural nobetGunKural)
        {
            _nobetGunKuralDal.Insert(nobetGunKural);
        }
        
        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(NobetGunKural nobetGunKural)
        {
            _nobetGunKuralDal.Update(nobetGunKural);
        }
      
    }
}
