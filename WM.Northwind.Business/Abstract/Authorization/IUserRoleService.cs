using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.Authorization;

namespace WM.Northwind.Business.Abstract.Authorization
{
    public interface IUserRoleService
    {
        //UserRole GetByUserId(int userRoleId);
        //List<UserRole> GetByRoleId(int roleId);

        UserRole GetById(int userRoleId);
        List<UserRole> GetList();
        //List<UserRole> GetByNobetGrup(int nobetId);
        void Insert(UserRole userRole);
        void Update(UserRole userRole);
        void Delete(int userRoleId);

        List<UserRole> GetListByUserId(int userId);
        UserRoleDetay GetDetayById(int userRoleId);
        List<UserRoleDetay> GetDetaylar();

    }
}
