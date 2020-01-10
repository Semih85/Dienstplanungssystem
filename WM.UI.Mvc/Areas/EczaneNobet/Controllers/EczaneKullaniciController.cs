using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WM.UI.Mvc.Areas.EczaneNobet.Controllers
{
    public class EczaneKullaniciController : Controller
    {
        // GET: EczaneNobet/EczaneKullanici
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}