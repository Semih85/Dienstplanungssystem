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
    public class UserEczaneOdaController : ApiController
    {
        #region ctor
        private IEczaneOdaService _eczaneOdaService;
        private IUserService _userService;
        private IUserEczaneOdaService _userEczaneOdaService;

        public UserEczaneOdaController(IEczaneOdaService eczaneOdaservice,
                                        IUserService userService,
                                        IUserEczaneOdaService userEczaneOdaService)
        {
            _eczaneOdaService = eczaneOdaservice;
            _userService = userService;
            _userEczaneOdaService = userEczaneOdaService;
        }
        #endregion

        [Route("user-eczane-odalar/{userId:int:min(1)}")]
        [HttpGet]
        public List<UserEczaneOdaDetay> GetEczaneOdaIdId(int userId)
        {
            List<UserEczaneOdaDetay> list = _userEczaneOdaService.GetDetayListByUserId(userId);
            return list;
        }

        [Route("user-eczane-oda-detay/{eczaneOdaId:int:min(1)}")]
        [HttpGet]
        public UserEczaneOdaDetay Get(int eczaneOdaId)
        {
            return _userEczaneOdaService.GetDetayById(eczaneOdaId);
        }


        [Route("web-sitesi/{userId:int:min(1)}")]
        [HttpGet]
        public string GetWebSitesi(int userId)
        {
           
            UserEczaneOdaDetay userEczaneOdaDetay = _userEczaneOdaService.GetDetayListByUserId(userId).FirstOrDefault();
            EczaneOda eczaneOda = _eczaneOdaService.GetById(userEczaneOdaDetay.EczaneOdaId);
            return eczaneOda.WebSitesi;
        }
    }
}
