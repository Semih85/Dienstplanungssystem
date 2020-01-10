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
    public class MenuRoleManager : IMenuRoleService
    {
        private IMenuRoleDal _menuRoleDal;

        public MenuRoleManager(IMenuRoleDal menuRoleDal)
        {
            _menuRoleDal = menuRoleDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [PerformansCounterAspect(2)]
        //[SecuredOperation(Roles="Admin,Editor,Student")]
        public List<MenuRole> GetList()
        {
            //Thread.Sleep(3000);    //performanscounterı test için yazıldı
            return _menuRoleDal.GetList();
        }

        public MenuRole GetById(int menuRoleId)
        {
            return _menuRoleDal.Get(x => x.Id == menuRoleId);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public void Insert(MenuRole menuRole)
        {
            _menuRoleDal.Insert(menuRole);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public void Update(MenuRole menuRole)
        {
            _menuRoleDal.Update(menuRole);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public void Delete(int menuRoleId)
        {
            _menuRoleDal.Delete(new MenuRole { Id = menuRoleId });
        }

        public List<MenuRole> GetByRoleId(int roleId)
        {
            return _menuRoleDal.GetList(x => x.RoleId == roleId);
        }

        public List<MenuRoleDetay> GetMenuRoleDetaylar()
        {
            return _menuRoleDal.GetMenuRoleDetaylar();
        }
        
        
    }
}
