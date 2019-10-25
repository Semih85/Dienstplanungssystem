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
    public class NobetOzelGunManager : INobetOzelGunService
    {
        private INobetOzelGunDal _nobetOzelGunDal;

        public NobetOzelGunManager(INobetOzelGunDal nobetOzelGunDal)
        {
            _nobetOzelGunDal = nobetOzelGunDal;
        }
        
        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Delete(int nobetOzelGunId)
        {
            _nobetOzelGunDal.Delete(new NobetOzelGun { Id = nobetOzelGunId });
        }

        public NobetOzelGun GetById(int nobetOzelGunId)
        {
            return _nobetOzelGunDal.Get(x => x.Id == nobetOzelGunId);
        }
        
        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetOzelGun> GetList()
        {
            return _nobetOzelGunDal.GetList();
        }
        
        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Insert(NobetOzelGun nobetOzelGun)
        {
            _nobetOzelGunDal.Insert(nobetOzelGun);
        }
        
        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(NobetOzelGun nobetOzelGun)
        {
            _nobetOzelGunDal.Update(nobetOzelGun);
        }

    }
}