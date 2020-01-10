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
    public class GorevTipManager : IGorevTipService
    {
        private IGorevTipDal _gorevTipDal;

        public GorevTipManager(IGorevTipDal gorevTipDal)
        {
            _gorevTipDal = gorevTipDal;
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Delete(int gorevTipId)
        {
            _gorevTipDal.Delete(new GorevTip { Id = gorevTipId });
        }

        public GorevTip GetById(int gorevTipId)
        {
            return _gorevTipDal.Get(x => x.Id == gorevTipId);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<GorevTip> GetList()
        {
            return _gorevTipDal.GetList();
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Insert(GorevTip gorevTip)
        {
            _gorevTipDal.Insert(gorevTip);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public void Update(GorevTip gorevTip)
        {
            _gorevTipDal.Update(gorevTip);
        }

    }
}