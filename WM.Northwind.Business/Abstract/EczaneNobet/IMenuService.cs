using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.Business.Abstract.EczaneNobet
{
    public interface IMenuService
    {
        Menu GetById(int menuId);
        List<Menu> GetList();
        //List<Menu> GetByRoleId(int roleId);
        void Insert(Menu menu);
        void Update(Menu menu);
        void Delete(int menuId);
    }
}
