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
using WM.Northwind.Entities.Concrete.Authorization;

namespace WM.Northwind.Business.Concrete.Managers.EczaneNobet
{
    public class NobetGorevTipManager : INobetGorevTipService
    {
        private INobetGorevTipDal _nobetGorevTipDal;

        public NobetGorevTipManager(INobetGorevTipDal nobetGorevTipDal)
        {
            _nobetGorevTipDal = nobetGorevTipDal;
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Delete(int nobetGorevTipId)
        {
            _nobetGorevTipDal.Delete(new NobetGorevTip { Id = nobetGorevTipId });
        }

        public NobetGorevTip GetById(int nobetGorevTipId)
        {
            return _nobetGorevTipDal.Get(x => x.Id == nobetGorevTipId);
        }
        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGorevTip> GetList()
        {
            return _nobetGorevTipDal.GetList();
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetGorevTip> GetList(List<int> nobetGorevTipIdList)
        {
            return _nobetGorevTipDal.GetList(x => nobetGorevTipIdList.Contains(x.Id));
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Insert(NobetGorevTip nobetGorevTip)
        {
            _nobetGorevTipDal.Insert(nobetGorevTip);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(NobetGorevTip nobetGorevTip)
        {
            _nobetGorevTipDal.Update(nobetGorevTip);
        }
    }
}
