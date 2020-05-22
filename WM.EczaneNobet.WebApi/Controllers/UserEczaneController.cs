using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WM.EczaneNobet.WebApi.Models;
using WM.Northwind.Business.Abstract.Authorization;
using WM.Northwind.Business.Abstract.EczaneNobet;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.Authorization;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.EczaneNobet.WebApi.Controllers
{
    public class UserEczaneController : ApiController
    {
        #region ctor
        private IEczaneService _eczaneService;
        private IUserService _userService;
        private IUserEczaneService _userEczaneService;
        private IEczaneNobetGrupService _eczaneNobetGrupService;
        private INobetUstGrupService _nobetUstGrupService;

        public UserEczaneController(IEczaneService eczaneService,
                                        IUserService userService,
                                        IUserEczaneService userEczaneService,
                                        IEczaneNobetGrupService eczaneNobetGrupService,
                                        INobetUstGrupService nobetUstGrupService)
        {
            _eczaneService = eczaneService;
            _userService = userService;
            _userEczaneService = userEczaneService;
            _eczaneNobetGrupService = eczaneNobetGrupService;
            _nobetUstGrupService = nobetUstGrupService;
        }
        #endregion

        [Route("user-eczane/{userId:int:min(1)}")]
        [HttpGet]
        public List<UserEczaneDetay> Get(int userId)
        {
            return _userEczaneService.GetDetaylarByUserId(userId);
        }

        //eczanenin nöbetleri için
        [Route("eczane-nobet-grupId/{userId:int:min(1)}")]
        [HttpGet]
        public int GetEczaneNobetGrupId(int userId)
        {
            //User user = new User();
            //user = _userService.GetById(userId);
            int eczaneId = _userEczaneService.GetDetaylarByUserId(userId).Select(s => s.EczaneId).FirstOrDefault();
            return _eczaneNobetGrupService.GetDetayByEczaneId(eczaneId).Id;
        }

        [Route("eczane/{userId:int:min(1)}")]
        [HttpGet]
        public HttpResponseMessage GetEczane(int userId)
        {//virtual propertiesler web apide json serilizationda sorun çıkrıyor
            try
            {
                //User user = new User();
                //user = _userService.GetById(userId);
                EczaneApi eczaneApi = new EczaneApi();
                Eczane eczane = new Eczane();
                int eczaneId = _userEczaneService.GetDetaylarByUserId(userId).Select(s => s.EczaneId).FirstOrDefault();
                eczane = _eczaneService.GetById(eczaneId);
                eczaneApi.Id = eczane.Id;
                eczaneApi.Adi = eczane.Adi;
                eczaneApi.AcilisTarihi = eczane.AcilisTarihi;
                eczaneApi.KapanisTarihi = eczane.KapanisTarihi;
                eczaneApi.AdresTarifi = eczane.AdresTarifi;
                eczaneApi.AdresTarifiKisa = eczane.AdresTarifiKisa;
                eczaneApi.Enlem = eczane.Enlem;
                eczaneApi.Boylam = eczane.Boylam;
                return Request.CreateResponse(HttpStatusCode.OK, eczaneApi);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message + e.InnerException.StackTrace);
            }
        }
     
    }
}
