using System.Collections.Generic;
using System.Linq;
using WM.Core.DAL.EntityFramework;
using WM.Northwind.DataAccess.Abstract.Authorization;
using WM.Northwind.DataAccess.Concrete.EntityFramework.Contexts;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.Authorization;

namespace WM.Northwind.DataAccess.Concrete.EntityFramework.Authorization
{
    public class EfUserDal : EfEntityRepositoryBase<User, EczaneNobetContext>, IUserDal
    {
        public List<UserRoleItem> GetUserRoles(User user)
        {
            using (var context = new EczaneNobetContext())
            {
                var result = (from ur in context.UserRoles
                              where ur.UserId == user.Id
                                 && ur.BitisTarihi == null
                              select new UserRoleItem
                              {
                                  RoleId = ur.RoleId,
                                  RoleName = ur.Role.Name
                              })
                              .ToList();
                return result;
            }
        }

        public List<UserRoleItem> GetByUserName(string userName)
        {
            using (var context = new EczaneNobetContext())
            {
                var result = (from ur in context.UserRoles
                              where ur.User.UserName == userName
                                 && ur.BitisTarihi == null
                              select new UserRoleItem
                              {
                                  RoleId = ur.RoleId,
                                  RoleName = ur.Role.Name
                              })
                              .ToList();
                return result;
            }
        }

        
    }
}