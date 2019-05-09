using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.Entities.Concrete.Authorization
{
    public class Role : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<UserRole> UserRoles{ get; set; }
        public virtual List<MenuRole> MenuRoles{ get; set; }
        public virtual List<MenuAltRole> MenuAltRoles{ get; set; }
        public virtual List<RaporRol>  RaporRoller { get; set; }
    }
}
