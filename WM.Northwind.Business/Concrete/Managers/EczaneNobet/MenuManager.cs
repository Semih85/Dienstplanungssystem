using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Business.Abstract;
using WM.Northwind.Business.Abstract.EczaneNobet;
using WM.Northwind.Business.ValidationRules.FluentValidation;
using WM.Northwind.DataAccess.Abstract.EczaneNobet;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;
using WM.Northwind.Entities.Concrete.Optimization.EczaneNobet;
using WM.Core.Aspects.PostSharp.ValidationAspects;
using WM.Core.CrossCuttingConcerns.Caching.Microsoft;
using WM.Core.Aspects.PostSharp.CacheAspects;
using WM.Core.Aspects.PostSharp.LogAspects;
using WM.Core.CrossCuttingConcerns.Logging.Log4Net.Logger;
using WM.Core.Aspects.PostSharp.PerformansCounterAspects;
using System.Threading;
using WM.Core.Aspects.PostSharp.AutorizationAspects;

namespace WM.Northwind.Business.Concrete.Managers.EczaneNobet
{
    //[SecuredOperation(Roles = "Admin")]
    public class MenuManager : IMenuService
    {
        private IMenuDal _menuDal;
        
        public MenuManager(IMenuDal menuDal)
        {
            _menuDal = menuDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [PerformansCounterAspect(2)]
        //[SecuredOperation(Roles="Admin,Editor,Student")]
        public List<Menu> GetList()
        {
            //Thread.Sleep(3000);    //performanscounterı test için yazıldı
            return _menuDal.GetList();
        }

        public Menu GetById(int menuId)
        {
            return _menuDal.Get(x => x.Id == menuId);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public void Insert(Menu menu)
        {
            _menuDal.Insert(menu);
        }
        
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public void Update(Menu menu)
        {
            _menuDal.Update(menu);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public void Delete(int menuId)
        {
            _menuDal.Delete(new Menu { Id = menuId });
        }
    }
}
