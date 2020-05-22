using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WM.Northwind.Business.Abstract.Authorization;
using WM.Northwind.Business.Abstract.EczaneNobet;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.Authorization;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.EczaneNobet.WebApi.Controllers
{
    public class UserRoleController : ApiController
    {
        #region ctor
        private IRoleService _rolesService;
        private IUserService _userService;
        private IUserRoleService _userRoleService;

        public UserRoleController(IRoleService rolesService,
                                    IUserService userService,
                                    IUserRoleService userRoleService)
        {
            _rolesService = rolesService;
            _userService = userService;
            _userRoleService = userRoleService;
        }
        #endregion

        [Route("user-role/{userId:int:min(1)}")]
        [HttpGet]
        public List<UserRoleDetay> Get(int userId)
        {
            return _userRoleService.GetDetayListByUserId(userId);
        }


    }
}
