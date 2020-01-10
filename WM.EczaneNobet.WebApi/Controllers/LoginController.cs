using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WM.Northwind.Business.Abstract.Authorization;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;

namespace WM.EczaneNobet.WebApi.Controllers
{
    public class LoginController : ApiController
    {
        private IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        public HttpResponseMessage Login([FromUri]string eMail, [FromUri]string password)
        {
            var loginUser = new LoginItem { Email = eMail, Password = password, RememberMe = true };

            var user = _userService.GetByEMailAndPassword(loginUser);

            if (user != null)
            {
                var jsonString = JsonConvert.SerializeObject(loginUser);

                var token = FTH.Extension.Encrypter.Encrypt(jsonString, loginUser.Password);

                return Request.CreateResponse(HttpStatusCode.OK, token);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Kullanıcı adı ve şifresi geçersiz.");
            }
        }
    }
}
