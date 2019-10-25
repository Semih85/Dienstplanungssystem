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
    public class NobetKuralManager : INobetKuralService
    {
        private INobetKuralDal _nobetKuralDal;

        public NobetKuralManager(INobetKuralDal nobetKuralDal)
        {
            _nobetKuralDal = nobetKuralDal;
        }
        
        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Delete(int nobetKuralId)
        {
            _nobetKuralDal.Delete(new NobetKural { Id = nobetKuralId });
        }

        public NobetKural GetById(int nobetKuralId)
        {
            return _nobetKuralDal.Get(x => x.Id == nobetKuralId);
        }
         [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetKural> GetList()
        {
            return _nobetKuralDal.GetList();
        }
        
        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Insert(NobetKural nobetKural)
        {
            _nobetKuralDal.Insert(nobetKural);
        }
        
        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(NobetKural nobetKural)
        {
            _nobetKuralDal.Update(nobetKural);
        }
    } 
}