using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WM.UI.Mvc.Controllers
{
    public class ModelBindingController : Controller
    {
        // GET: ModelBinding
        //routedata
        public ActionResult Ders1()
        {
            var controller = RouteData.Values["controller"];
            var action = RouteData.Values["action"];
            var id = RouteData.Values["id"];

            var adi = Request.QueryString["adi"];
            var soyAdi = Request.QueryString["soyAdi"];


            return Content(string.Format("RouteData.Values: Controller: {0} - Action: {1}  Request.QueryStrings(adi, soyAdi): adi: {2}, soyAdi: {3}", controller, action, adi, soyAdi));
        }
    }
}