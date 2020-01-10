using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.Authorization;

namespace WM.Northwind.Business.Abstract.Authorization
{
    public interface IRoleService
    {
        Role GetById(int roleId);
        List<Role> GetList();
        void Insert(Role role);
        void Update(Role role);
        void Delete(int roleId);

    }
}
