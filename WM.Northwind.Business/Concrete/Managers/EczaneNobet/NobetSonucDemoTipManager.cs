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
    public class NobetSonucDemoTipManager : INobetSonucDemoTipService
    {
        private INobetSonucDemoTipDal _nobetSonucDemoTipDal;

        public NobetSonucDemoTipManager(INobetSonucDemoTipDal nobetSonucDemoTipDal)
        {
            _nobetSonucDemoTipDal = nobetSonucDemoTipDal;
        }
        
        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Delete(int nobetSonucDemoTipId)
        {
            _nobetSonucDemoTipDal.Delete(new NobetSonucDemoTip { Id = nobetSonucDemoTipId });
        }

        public NobetSonucDemoTip GetById(int nobetSonucDemoTipId)
        {
            return _nobetSonucDemoTipDal.Get(x => x.Id == nobetSonucDemoTipId);
        }
        
        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetSonucDemoTip> GetList()
        {
            return _nobetSonucDemoTipDal.GetList();
        }
        
        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Insert(NobetSonucDemoTip nobetSonucDemoTip)
        {
            _nobetSonucDemoTipDal.Insert(nobetSonucDemoTip);
        }
        
        [LogAspect(typeof(DatabaseLogger))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(NobetSonucDemoTip nobetSonucDemoTip)
        {
            _nobetSonucDemoTipDal.Update(nobetSonucDemoTip);
        }
        
    } 
}