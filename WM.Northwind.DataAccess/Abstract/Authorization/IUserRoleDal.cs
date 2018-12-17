using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.DAL;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.Authorization;

namespace WM.Northwind.DataAccess.Abstract.Authorization
{
    public interface IUserRoleDal : IEntityRepository<UserRole>, IEntityDetayRepository<UserRoleDetay>
    {   
    }
}
