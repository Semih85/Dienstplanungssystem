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
    public class NobetFeragatTipManager : INobetFeragatTipService
    {
        private INobetFeragatTipDal _nobetFeragatTipDal;

        public NobetFeragatTipManager(INobetFeragatTipDal nobetFeragatTipDal)
        {
            _nobetFeragatTipDal = nobetFeragatTipDal;
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Delete(int nobetFeragatTipId)
        {
            _nobetFeragatTipDal.Delete(new NobetFeragatTip { Id = nobetFeragatTipId });
        }

        public NobetFeragatTip GetById(int nobetFeragatTipId)
        {
            return _nobetFeragatTipDal.Get(x => x.Id == nobetFeragatTipId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetFeragatTip> GetList()
        {
            return _nobetFeragatTipDal.GetList();
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Insert(NobetFeragatTip nobetFeragatTip)
        {
            _nobetFeragatTipDal.Insert(nobetFeragatTip);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(NobetFeragatTip nobetFeragatTip)
        {
            _nobetFeragatTipDal.Update(nobetFeragatTip);
        }
                        
    }
}