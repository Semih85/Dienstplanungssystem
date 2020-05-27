using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;
using WM.EczaneNobet.WebApi.Models;
using WM.Northwind.Business.Abstract.Authorization;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.Authorization;
//using WM.UI.Mvc.Areas.EczaneNobet;
using WM.EczaneNobet.WebApi.MessageHandlers;
using WM.UI.Mvc.Areas.EczaneNobet;

namespace WM.EczaneNobet.WebApi.Controllers
{
    public class LoginController : ApiController
    {
        private IUserService _userService;
        private IUserRoleService _userRoleService;
        Yetkilendirme _yetkilendirme;
        
        public LoginController(IUserService userService,
                                IUserRoleService userRoleService)
        {
            _userService = userService;
            _userRoleService = userRoleService;
            _yetkilendirme = new Yetkilendirme(_userService, _userRoleService);

        }
        //[Route("token-kontrol/{userId:int:min(1)}")]
        //[HttpGet]
        //public string TokenKontrol(int userId)
        //{
        //    LoginItem loginUser;
        //    User user = _userService.GetById(userId);
        //    loginUser = new LoginItem { Email = user.Email, Password = _yetkilendirme.SHA256(user.Password), RememberMe = true };
        //    string token = _yetkilendirme.GetToken2(loginUser);
        //    return token;
        //}
        [Route("bildirim-test")]
        //[Route("login/{eMail:maxlength(100)}/{password:maxlength(100)}")]
        [HttpPost]
        public HttpResponseMessage PostBildirimTest([FromBody] UserApi userApi)//([FromUri]string eMail, [FromUri]string password)
        {
            PushNotification pushNotification = new PushNotification(userApi.Password,
                 userApi.Username,
                 userApi.CihazId);
            return Request.CreateResponse(HttpStatusCode.OK, userApi.CihazId);

        }

        //[Route("bildirim-test-get")]
        ////[Route("login/{eMail:maxlength(100)}/{password:maxlength(100)}")]
        //[HttpGet]
        //public HttpResponseMessage GetBildirimTest()//([FromUri]string eMail, [FromUri]string password)
        //{
        //    PushNotification pushNotification = new PushNotification("kan ihtiyacı",
        //         "Duyuru",
        //         "cxzrXvNdTCk:APA91bG51xqnymrAW_BuHSJGUTQOZbv-4Mn_LD7hQCHQrzn2j_uNFltw86l3XMpUXnURr7GktU-_bOGWAeuq-qvTXopG1codEEmcotNBsbfwBH3nP705hOziudxWHPhOp_lFytyMzBhw");
        //    return Request.CreateResponse(HttpStatusCode.OK);

        //}

        [Route("login")]
        //[Route("login/{eMail:maxlength(100)}/{password:maxlength(100)}")]
        [HttpPost]
        public HttpResponseMessage Login([FromBody] UserApi userApi)//([FromUri]string eMail, [FromUri]string password)
        {
            LoginItem loginUser;
            User user;
            _yetkilendirme.YetkiKontrolu(userApi, out loginUser, out user);

            if (user != null)
            {
                string token = _yetkilendirme.GetToken2(loginUser);
                List<UserRoleDetay> UserRoleDetayList = new List<UserRoleDetay>();
                UserRoleDetayList = _userRoleService.GetDetayListByUserId(user.Id).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, "UserId:" + UserRoleDetayList[0].UserId
                    + ",Token:" + token
                    + ",CihazId:" + UserRoleDetayList[0].CihazId
                    + ",Password:" + user.Password);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Kullanıcı adı ve şifresi geçersiz.");
            }
        }

        //[Route("login-get")]
        ////[Route("login/{eMail:maxlength(100)}/{password:maxlength(100)}")]
        //[HttpGet]
        //public HttpResponseMessage LoginGet()//([FromUri]string eMail, [FromUri]string password)
        //{
        //    UserApi userApi = new UserApi();
        //    userApi.Username = "atesates2012@gmail.com";
        //    userApi.Password = "0327ates";
        //    LoginItem loginUser;
        //    User user;
        //    _yetkilendirme.YetkiKontrolu(userApi, out loginUser, out user);

        //    if (user != null)
        //    {
        //        string token = _yetkilendirme.GetToken2(loginUser);
        //        List<UserRoleDetay> UserRoleDetayList = new List<UserRoleDetay>();
        //        UserRoleDetayList = _userRoleService.GetDetayListByUserId(user.Id).ToList();
        //        return Request.CreateResponse(HttpStatusCode.OK, "UserId:" + UserRoleDetayList[0].UserId
        //            + ",Token:" + token
        //            + ",CihazId:" + UserRoleDetayList[0].CihazId
        //            + ",Password:" + user.Password);
        //    }
        //    else
        //    {
        //        return Request.CreateResponse(HttpStatusCode.Unauthorized, "Kullanıcı adı ve şifresi geçersiz.");
        //    }
        //}

        [Route("parola-degistir")]
        //[Route("login/{eMail:maxlength(100)}/{password:maxlength(100)}")]
        [HttpPost]
        public HttpResponseMessage ParolaDegistir([FromBody] UserApi userApi)//([FromUri]string eMail, [FromUri]string password)
        {
            LoginItem loginUser;
            User user;
            _yetkilendirme.YetkiKontrolu(userApi, out loginUser, out user);

            if (user != null)
            {
                user.Password = _yetkilendirme.SHA256(userApi.NewPassword);
                _userService.Update(user);
                string token = _yetkilendirme.GetToken2(loginUser);
                List<UserRoleDetay> UserRoleDetayList = new List<UserRoleDetay>();
                UserRoleDetayList = _userRoleService.GetDetayListByUserId(user.Id).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, "UserId:" + UserRoleDetayList[0].UserId
                    + ",Token:" + token
                    + ",CihazId:" + UserRoleDetayList[0].CihazId
                    + ",Password:" + user.Password);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Kullanıcı adı ve şifresi geçersiz.");
            }
        }

        //[Route("parola-degistir-get")]
        ////[Route("login/{eMail:maxlength(100)}/{password:maxlength(100)}")]
        //[HttpGet]
        //public HttpResponseMessage GetParolaDegistir()//([FromUri]string eMail, [FromUri]string password)
        //{
        //    UserApi userApi = new UserApi
        //    {
        //        CihazId = "ci2ssXB9fUM:APA91bHGY05FVss6IsiggGnR54AhdlgWpZgzPjKbHxcKjbUHZk9hkwAL7MBFsHWw__2a-y5pYUzW4hnUp16J9sBxwjvuscueMTjBncvBWjPHHJtfElnZKt4IPZkHdXVQJ7Bnen5NnPR7",
        //        NewPassword = "0327ates",
        //        Password = "0327ates",
        //        Username = "atesates2012@gmail.com"
        //    };
        //    LoginItem loginUser;
        //    User user;
        //    _yetkilendirme.YetkiKontrolu(userApi, out loginUser, out user);


        //    if (user != null)
        //    {
        //        user.Password = _yetkilendirme.SHA256(userApi.NewPassword);
        //        _userService.Update(user);
        //        string token = _yetkilendirme.GetToken2(loginUser);
        //        List<UserRoleDetay> UserRoleDetayList = new List<UserRoleDetay>();
        //        UserRoleDetayList = _userRoleService.GetDetayListByUserId(user.Id).ToList();
        //        return Request.CreateResponse(HttpStatusCode.OK, "UserId:" + UserRoleDetayList[0].UserId
        //            + ",Token:" + token
        //            + ",CihazId:" + UserRoleDetayList[0].CihazId
        //            + ",Password:" + user.Password);
        //    }
        //    else
        //    {
        //        return Request.CreateResponse(HttpStatusCode.Unauthorized, "Kullanıcı adı ve şifresi geçersiz.");
        //    }
        //}

        [Route("cihazId")]
        //[Route("login/{eMail:maxlength(100)}/{password:maxlength(100)}")]
        [HttpPost]
        public HttpResponseMessage UpdateCihazId([FromBody] UserApi userApi)//([FromUri]string eMail, [FromUri]string password)
        {
            LoginItem loginUser;
            User user;
            _yetkilendirme.YetkiKontrolu(userApi, out loginUser, out user);

            if (user != null)
            {
                user.CihazId = userApi.CihazId;
                _userService.Update(user);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "CihazId güncellenemedi.");
            }
        }
        //[Route("cihazId-get")]
        ////[Route("login/{eMail:maxlength(100)}/{password:maxlength(100)}")]
        //[HttpGet]
        //public HttpResponseMessage GetUpdateCihazId()//([FromUri]string eMail, [FromUri]string password)
        //{
        //    LoginItem loginUser;
        //    User user;
        //    UserApi userApi = new UserApi();
        //    userApi.Username = "atesates2012@gmail.com";
        //    userApi.Password = "0327ates";
        //    _yetkilendirme.YetkiKontrolu(userApi, out loginUser, out user);

        //    if (user != null)
        //    {
        //        user.CihazId = userApi.CihazId;
        //        _userService.Update(user);
        //        return Request.CreateResponse(HttpStatusCode.OK);
        //    }
        //    else
        //    {
        //        return Request.CreateResponse(HttpStatusCode.Unauthorized, "CihazId güncellenemedi.");
        //    }
        //}

        [Route("deneme/{eMail:maxlength(100)}/{password:maxlength(100)}")]
        [HttpGet]
        public HttpResponseMessage Deneme([FromUri]string eMail, [FromUri]string password)
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("deneme2/{eMail:int:min(1)}/{password:int:min(1)}")]
        [HttpGet]
        public HttpResponseMessage Deneme2([FromUri]int eMail, [FromUri]int password)
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }

       

    }
}
