using System.Collections.Generic;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.UI.Mvc.Areas.EczaneNobet.Models
{
    public class MenuPartialViewModel
    {
        public List<Menu> Menuler { get; internal set; }
        public List<MenuAlt> MenuAltlar { get; internal set; }
        public List<MenuAlt> MenuAltlarTumu { get; internal set; }
    }
}