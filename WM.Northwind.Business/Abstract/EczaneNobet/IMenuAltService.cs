using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.Business.Abstract.EczaneNobet
{
    public interface IMenuAltService
    {
        MenuAlt GetById(int menuAltId);
        List<MenuAlt> GetList();
        List<MenuAlt> GetByMenuId(int menuAltId);
        void Insert(MenuAlt menuAlt);
        void Update(MenuAlt menuAlt);
        void Delete(int menuAltId);

        List<MenuAltDetay> GetDetaylar();
        List<MenuAltDetay> GetDetaylar(int menuId);
        MenuAlt GetDetayById(int menuAltId);
    }
}
