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
    public class NobetDurumTipManager : INobetDurumTipService
    {
        private INobetDurumTipDal _nobetDurumTipDal;

        public NobetDurumTipManager(INobetDurumTipDal nobetDurumTipDal)
        {
            _nobetDurumTipDal = nobetDurumTipDal;
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Delete(int nobetDurumId)
        {
            _nobetDurumTipDal.Delete(new NobetDurumTip { Id = nobetDurumId });
        }

        public NobetDurumTip GetById(int nobetDurumId)
        {
            return _nobetDurumTipDal.Get(x => x.Id == nobetDurumId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<NobetDurumTip> GetList()
        {
            return _nobetDurumTipDal.GetList();
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Insert(NobetDurumTip nobetDurum)
        {
            _nobetDurumTipDal.Insert(nobetDurum);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(NobetDurumTip nobetDurum)
        {
            _nobetDurumTipDal.Update(nobetDurum);
        }

    }
}