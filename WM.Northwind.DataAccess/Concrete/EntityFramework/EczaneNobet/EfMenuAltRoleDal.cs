using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.DAL.EntityFramework;
using WM.Northwind.DataAccess.Abstract.EczaneNobet;
using WM.Northwind.DataAccess.Concrete.EntityFramework.Contexts;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.DataAccess.Concrete.EntityFramework.EczaneNobet
{
    public class EfMenuAltRoleDal : EfEntityRepositoryBase<MenuAltRole, EczaneNobetContext>, IMenuAltRoleDal
    {
        public List<MenuAltRoleDetay> GetMenuAltRoleDetaylar()
        {
            using (var ctx = new EczaneNobetContext())
            {
                return ctx.MenuAltRoles
                        .Select(s => new MenuAltRoleDetay
                        {
                            Id = s.Id,
                            MenuAltId = s.MenuAltId,
                            RoleId = s.RoleId,
                            MenuAltAdi = s.MenuAlt.LinkText,
                            RolAdi = s.Role.Name
                        }).ToList();
            }
        }
    }
}
