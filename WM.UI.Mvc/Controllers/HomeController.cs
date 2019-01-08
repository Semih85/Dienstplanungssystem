using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WM.Northwind.Business.Abstract;
using WM.UI.Mvc.Models;


namespace WM.UI.Mvc.Controllers
{
    //[Route("{action=Index}")]
    //[Authorize]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult HelperMethodDemo()
        {
            return View();
        }


        public ActionResult ViewDemo()
        {
            return View();
        }

        [HandleError]
        public ActionResult About()
        {
            var i = 0;
            var d = 200 / i;

            //throw new Exception("hata oldu");
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult DijitalTabela()
        {
            return View();
        }

        public ActionResult NobetYazDetay()
        {
            return View();
        }

        public ActionResult EczaneNobetSistemi()
        {
            return View();
        }

        public ActionResult EczaneNobetSistemiDetay()
        {
            return View();
        }

    }
}