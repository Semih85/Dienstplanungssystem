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
    public class EczaneGrupTanimTipManager : IEczaneGrupTanimTipService
    {
        private IEczaneGrupTanimTipDal _eczaneGrupTanimTipDal;

        public EczaneGrupTanimTipManager(IEczaneGrupTanimTipDal eczaneGrupTanimTipDal)
        {
            _eczaneGrupTanimTipDal = eczaneGrupTanimTipDal;
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Delete(int eczaneGrupTanimTipId)
        {
            _eczaneGrupTanimTipDal.Delete(new EczaneGrupTanimTip { Id = eczaneGrupTanimTipId });
        }

        public EczaneGrupTanimTip GetById(int eczaneGrupTanimTipId)
        {
            return _eczaneGrupTanimTipDal.Get(x => x.Id == eczaneGrupTanimTipId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneGrupTanimTip> GetList()
        {
            return _eczaneGrupTanimTipDal.GetList();
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<EczaneGrupTanimTip> GetList(List<int> eczaneGrupTanimTipIdList)
        {
            return _eczaneGrupTanimTipDal.GetList(x => eczaneGrupTanimTipIdList.Contains(x.Id));
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Insert(EczaneGrupTanimTip eczaneGrupTanimTip)
        {
            _eczaneGrupTanimTipDal.Insert(eczaneGrupTanimTip);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(EczaneGrupTanimTip eczaneGrupTanimTip)
        {
            _eczaneGrupTanimTipDal.Update(eczaneGrupTanimTip);
        }

    }
}