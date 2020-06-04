using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WM.Northwind.Business.Abstract.Authorization;
using WM.Northwind.Business.Abstract.EczaneNobet;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;
using WM.UI.Mvc.Models;

namespace WM.UI.Mvc.Areas.EczaneNobet.Controllers
{
    [Authorize]
    [HandleError]
    public class EczaneMobilBildirimController : Controller
    {
        private IEczaneMobilBildirimService _eczaneMobilBildirimService;
        private IMobilBildirimService _mobilBildirimService;
        private IEczaneService _eczaneService;

        public EczaneMobilBildirimController(IEczaneMobilBildirimService eczaneMobilBildirimService,
                                     IMobilBildirimService mobilBildirimService,
                                     IEczaneService czaneService)
        {
            _eczaneMobilBildirimService = eczaneMobilBildirimService;
            _mobilBildirimService = mobilBildirimService;
            _eczaneService = czaneService;
        }
        // GET: EczaneNobet/MenuAltRole
        public ActionResult Index(int? mobilBildirimId)
        {
            int intMobilBildirimId = Convert.ToInt32(mobilBildirimId);
            var model = _eczaneMobilBildirimService.GetDetaylar(intMobilBildirimId);
            //var menuAltRoles = db.MenuAltRoles.Include(m => m.MenuAlt).Include(m => m.Role);
            return View(model);
        }

      

      
    }
}
