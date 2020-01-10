using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WM.UI.Mvc.Areas.EczaneNobet.Controllers
{
    [HandleError]
    public class EkranController : Controller
    {
        // GET: EczaneNobet/Ekran
        public ActionResult Index()
        {
            return View();
        }
    }
}