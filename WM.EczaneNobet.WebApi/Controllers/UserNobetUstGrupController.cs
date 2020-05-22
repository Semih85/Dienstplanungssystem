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
    public class UserNobetUstGrupController : ApiController
    {
        #region ctor
        private INobetUstGrupService _nobetUstGrupService;
        private IUserService _userService;
        private IUserNobetUstGrupService _userNobetUstGrupService;
        private IUserEczaneService _userEczaneService;
        private IEczaneNobetGrupService _eczaneNobetGrupService;
        private IEczaneService _eczaneService;



        public UserNobetUstGrupController(INobetUstGrupService nobetUstGrupservice,
                                            IUserService userService,
                                                IUserEczaneService userEczaneService,
                                                IEczaneNobetGrupService eczaneNobetGrupService,
                                                IEczaneService eczaneService,
                                            IUserNobetUstGrupService userNobetUstGrupService)
        {
            _nobetUstGrupService = nobetUstGrupservice;
            _userService = userService;
            _userNobetUstGrupService = userNobetUstGrupService;
            _userEczaneService = userEczaneService;
            _eczaneNobetGrupService = eczaneNobetGrupService;
            _eczaneService = eczaneService;
        }
        #endregion

        [Route("user-nobet-ust-grup-detaylar/{userId:int:min(1)}")]
        [HttpGet]
        public List<UserNobetUstGrupDetay> GetNobetUstGrupIdId(int userId)
        {
            return _userNobetUstGrupService.GetDetayListByUserId(userId);
        }

        [Route("user-nobet-ust-grup-detay/{nobetUstGrupId:int:min(1)}")]
        [HttpGet]
        public UserNobetUstGrupDetay Get(int nobetUstGrupId)
        {
            return _userNobetUstGrupService.GetDetayById(nobetUstGrupId);
        }

        [Route("nobetustgrupId/{userId:int:min(1)}")]
        [HttpGet]
        public int GetNobetUstGrup(int userId)
        {
            //User user = _userService.GetById(userId);
            //user = _userService.GetById(userId);
            int eczaneId = _userEczaneService.GetListByUserId(userId).Select(s => s.EczaneId).FirstOrDefault();
            int eczaneNobetGrupId = _eczaneNobetGrupService.GetDetayByEczaneId(eczaneId).Id;
            NobetUstGrup nobetUstGrup = _eczaneService.GetByEczaneNobetGrupId(eczaneNobetGrupId);
            return nobetUstGrup.Id;
        }


    }
}
