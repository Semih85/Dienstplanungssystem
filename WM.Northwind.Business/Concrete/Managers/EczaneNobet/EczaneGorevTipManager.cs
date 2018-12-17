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
    public class EczaneGorevTipManager : IEczaneGorevTipService
    {
        private IEczaneGorevTipDal _eczaneGorevTipDal;

        public EczaneGorevTipManager(IEczaneGorevTipDal eczaneGorevTipDal)
        {
            _eczaneGorevTipDal = eczaneGorevTipDal;
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Delete(int eczaneGorevTipId)
        {
            _eczaneGorevTipDal.Delete(new EczaneGorevTip { Id = eczaneGorevTipId });
        }

        public EczaneGorevTip GetById(int eczaneGorevTipId)
        {
            return _eczaneGorevTipDal.Get(x => x.Id == eczaneGorevTipId);
        }
         [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneGorevTip> GetList()
        {
            return _eczaneGorevTipDal.GetList();
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Insert(EczaneGorevTip eczaneGorevTip)
        {
            _eczaneGorevTipDal.Insert(eczaneGorevTip);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(EczaneGorevTip eczaneGorevTip)
        {
            _eczaneGorevTipDal.Update(eczaneGorevTip);
        }
        public EczaneGorevTipDetay GetDetayById(int eczaneGorevTipId)
        {
            return _eczaneGorevTipDal.GetDetay(x => x.Id == eczaneGorevTipId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneGorevTipDetay> GetDetaylar()
        {
            return _eczaneGorevTipDal.GetDetayList();
        }
    }
}
