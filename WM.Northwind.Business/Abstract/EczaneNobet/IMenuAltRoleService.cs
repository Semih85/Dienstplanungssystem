using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.Business.Abstract.EczaneNobet
{
    public interface IMenuAltRoleService
    {
        MenuAltRole GetById(int menuAltRoleId);
        List<MenuAltRole> GetList();
        List<MenuAltRole> GetByRoleId(int roleId);
        void Insert(MenuAltRole menuAltRole);
        void Update(MenuAltRole menuAltRole);
        void Delete(int menuAltRoleId);

        List<MenuAltRoleDetay> GetMenuAltRoleDetaylar();
    }
}
