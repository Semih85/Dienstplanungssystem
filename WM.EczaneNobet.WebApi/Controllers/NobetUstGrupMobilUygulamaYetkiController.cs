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
    public class NobetUstGrupMobilUygulamaYetkiController : ApiController
    {
        #region ctor
        private IMobilUygulamaYetkiService _mobilUygulamaYetkiService;
        private INobetUstGrupMobilUygulamaYetkiService _nobetUstGrupMobilUygulamaYetkiService;
        private INobetUstGrupService _nobetUstGrupService;
        private IEczaneNobetGrupService _eczaneNobetGrupService;
        private IUserService _userService;
        private IEczaneService _eczaneService;
        private IUserEczaneService _userEczaneService;

        public NobetUstGrupMobilUygulamaYetkiController(IMobilUygulamaYetkiService mobilUygulamaYetkiService,
                                                INobetUstGrupMobilUygulamaYetkiService ustGrupMobilUygulamaYetkiService,
                                                IUserEczaneService userEczaneService,
                                                IEczaneService eczaneService,
                                                IEczaneNobetGrupService eczaneNobetGrupService,
                                                IUserService userService,
                                                INobetUstGrupService nobetUstGrupService)
        {

            _mobilUygulamaYetkiService = mobilUygulamaYetkiService;
            _nobetUstGrupService = nobetUstGrupService;
            _nobetUstGrupMobilUygulamaYetkiService = ustGrupMobilUygulamaYetkiService;
            _userEczaneService = userEczaneService;
            _eczaneNobetGrupService = eczaneNobetGrupService;
            _eczaneService = eczaneService;
            _userService = userService;
        }
        #endregion

        [Route("nobet-ust-grup-mobil-uygulama-yetki/{userId:int:min(1)}")]
        [HttpGet]
        public List<NobetUstGrupMobilUygulamaYetkiDetay> Get(int userId)
        {
            User User = _userService.GetById(userId);
            int eczaneId = _userEczaneService.GetListByUserId(userId).Select(s => s.EczaneId).FirstOrDefault();
            int eczaneNobetGrupId = _eczaneNobetGrupService.GetDetayByEczaneId(eczaneId).Id;
            NobetUstGrup nobetUstGrup = _eczaneService.GetByEczaneNobetGrupId(eczaneNobetGrupId);
            return _nobetUstGrupMobilUygulamaYetkiService.GetDetayListByNobetUstGrupId(nobetUstGrup.Id);
        }


    }
}
