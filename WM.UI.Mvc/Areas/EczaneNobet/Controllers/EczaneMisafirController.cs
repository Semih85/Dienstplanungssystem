using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WM.UI.Mvc.Areas.EczaneNobet.Controllers
{
    public class EczaneMisafirController : Controller
    {
        // GET: EczaneNobet/EczaneMisafir
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}