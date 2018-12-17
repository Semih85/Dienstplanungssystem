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
    public class MenuAltRoleManager : IMenuAltRoleService
    {
        private IMenuAltRoleDal _menuAltRoleDal;
        
        public MenuAltRoleManager(IMenuAltRoleDal menuAltRoleDal)
        {
            _menuAltRoleDal = menuAltRoleDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [PerformansCounterAspect(2)]
        //[SecuredOperation(Roles="Admin,Editor,Student")]
        public List<MenuAltRole> GetList()
        {
            //Thread.Sleep(3000);    //performanscounterı test için yazıldı
            return _menuAltRoleDal.GetList();
        }

        public MenuAltRole GetById(int menuAltRoleId)
        {
            return _menuAltRoleDal.Get(x => x.Id == menuAltRoleId);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public void Insert(MenuAltRole menuAltRole)
        {
            _menuAltRoleDal.Insert(menuAltRole);
        }
        
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public void Update(MenuAltRole menuAltRole)
        {
            _menuAltRoleDal.Update(menuAltRole);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [LogAspect(typeof(DatabaseLogger))]
        public void Delete(int menuAltRoleId)
        {
            _menuAltRoleDal.Delete(new MenuAltRole { Id = menuAltRoleId });
        }

        public List<MenuAltRole> GetByRoleId(int roleId)
        {
            return _menuAltRoleDal.GetList(x => x.RoleId == roleId);
        }

        public List<MenuAltRoleDetay> GetMenuAltRoleDetaylar()
        {
            return _menuAltRoleDal.GetMenuAltRoleDetaylar();
        }
    }
}
