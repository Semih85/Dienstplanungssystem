using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WM.Core.DAL.EntityFramework;
using WM.Northwind.DataAccess.Abstract.EczaneNobet;
using WM.Northwind.DataAccess.Concrete.EntityFramework.Contexts;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.DataAccess.Concrete.EntityFramework.EczaneNobet
{
    public class EfMenuRoleDal : EfEntityRepositoryBase<MenuRole, EczaneNobetContext>, IMenuRoleDal
    {
        public List<MenuRoleDetay> GetMenuRoleDetaylar()
        {
            using (var ctx = new EczaneNobetContext())
            {
                return ctx.MenuRoles
                        .Select(s=> new MenuRoleDetay
                        {
                            Id = s.Id,
                            MenuId = s.MenuId,
                            RoleId = s.RoleId,
                            MenuAdi = s.Menu.LinkText,
                            RolAdi = s.Role.Name
                        }).ToList();
            }
        }
    }
}
