using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WM.UI.Mvc.Areas.EczaneNobet.Controllers
{
    [Authorize(Roles = "Admin,Oda,Üst Grup,Nöbet Komisyonu Üyesi")]
    public class NobetKomisyonuController : Controller
    {
        // GET: EczaneNobet/NobetKomisyonu
        public ActionResult Index()
        {
            return View();
        }
    }
}