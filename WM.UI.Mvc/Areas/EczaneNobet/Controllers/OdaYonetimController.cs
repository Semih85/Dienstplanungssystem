using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WM.UI.Mvc.Areas.EczaneNobet.Controllers
{
    public class OdaYonetimController : Controller
    {
        // GET: EczaneNobet/OdaYonetim
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}