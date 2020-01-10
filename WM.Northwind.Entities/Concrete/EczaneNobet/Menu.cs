using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;
using WM.Northwind.Entities.Concrete.Authorization;

namespace WM.Northwind.Entities.Concrete.EczaneNobet
{
    public class Menu : MenuBase, IEntity
    {
        public int Id { get; set; }

        public virtual List<MenuAlt> MenuAltlar { get; set; }
    }
}
