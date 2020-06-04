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
using WM.Northwind.Business.Abstract.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.EczaneNobet.WebApi.Controllers
{
    public class EczaneMobilBildirimController : ApiController
    {
        private INobetUstGrupService _nobetUstGrupService;
        private IUserService _userService;
        private IUserEczaneService _userEczaneService;
        private IUserRoleService _userRoleService;
        private IEczaneMobilBildirimService _eczaneMobilBildirimService;
        private IMobilBildirimService _mobilBildirimService;
        Yetkilendirme _yetkilendirme;
        
        public EczaneMobilBildirimController(IUserService userService,
                                            IUserEczaneService userEczaneService,
                                            IEczaneMobilBildirimService eczaneMobilBildirimService,
                                            INobetUstGrupService nobetUstGrupService,
                                            IMobilBildirimService mobilBildirimService,
                                            IUserRoleService userRoleService)
        {
            _userService = userService;
            _userEczaneService = userEczaneService;
            _eczaneMobilBildirimService = eczaneMobilBildirimService;
            _nobetUstGrupService = nobetUstGrupService;
            _mobilBildirimService = mobilBildirimService;
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
                 userApi.CihazId,"0");
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


        [Route("bildirim-gordu")]
        //[Route("login/{eMail:maxlength(100)}/{password:maxlength(100)}")]
        [HttpPost]
        public HttpResponseMessage UpdateEczaneMobilBildirim([FromBody] MobilBildirimModelApi mobilBildirimModelApi)//([FromUri]string eMail, [FromUri]string password)
        {
             //string islem = mobilBildirimModelApi.EczacininYaptigiIslem;

           
            User user = _userService.GetById(mobilBildirimModelApi.UserId);
            int eczaneId = _userEczaneService.GetDetaylarByUserId(mobilBildirimModelApi.UserId)
                .Select(s => s.EczaneId).FirstOrDefault();

            int eczaneMobilBildirimId = _eczaneMobilBildirimService.GetDetaylar(mobilBildirimModelApi.MobilBildirimId, eczaneId)
                .Select(s => s.Id).FirstOrDefault();

            EczaneMobilBildirim eczaneMobilBildirim = _eczaneMobilBildirimService.GetById(eczaneMobilBildirimId);
            eczaneMobilBildirim.BildirimGormeTarihi = DateTime.Now;
            _eczaneMobilBildirimService.Update(eczaneMobilBildirim);

            return Request.CreateResponse(HttpStatusCode.OK);        
        }

        //[Route("bildirim-gordu-get")]
        ////[Route("login/{eMail:maxlength(100)}/{password:maxlength(100)}")]
        //[HttpGet]
        //public HttpResponseMessage GetUpdateEczaneMobilBildirim()//([FromUri]string eMail, [FromUri]string password)
        //{
        //    //string islem = mobilBildirimModelApi.EczacininYaptigiIslem;


        //    User user = _userService.GetById(48);
        //    int nobetUstGrupId = _nobetUstGrupService.GetListByUser(user).FirstOrDefault().Id;
        //    int eczaneId = _userEczaneService.GetDetaylarByUserId(48)
        //        .Select(s => s.EczaneId).FirstOrDefault();

        //    int eczaneMobilBildirimId = _eczaneMobilBildirimService.GetDetaylar(10, eczaneId)
        //        .Select(s => s.Id).FirstOrDefault();

        //    EczaneMobilBildirim eczaneMobilBildirim = _eczaneMobilBildirimService.GetById(eczaneMobilBildirimId);
        //    eczaneMobilBildirim.BildirimGormeTarihi = DateTime.Now;

        //    _eczaneMobilBildirimService.Update(eczaneMobilBildirim);

        //    return Request.CreateResponse(HttpStatusCode.OK);
        //}
    }
}
