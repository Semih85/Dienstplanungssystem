using System.ComponentModel.DataAnnotations;
using WM.Core.Entities;
using WM.Northwind.Entities.Concrete;

namespace WM.Northwind.Entities.ComplexTypes.EczaneNobet
{
    public class MenuAltDetay : MenuBase, IComplexType
    {
        public int Id { get; set; }
        public int MenuId { get; set; }
        public string MenuLinkText { get; set; }
        public bool MenuPasifMi { get; set; }


    }

}
