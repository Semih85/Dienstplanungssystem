using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace WM.UI.Mvc.Controllers
{
    public class LanguageController : Controller
    {
        public ActionResult Change(string language, string viewName)
        {
            var path = viewName;
            var eskiDil = viewName.Length > 1 ? viewName.Substring(1, 2) : "";
            var dilHaricYol = "";

            if (eskiDil == "tr"
                || eskiDil == "de"
                || eskiDil == "en")
            {
                var pathUzunluk = viewName.Length;
                dilHaricYol = viewName.Substring(3, pathUzunluk - 3);
                //var dil = eskiDil;
            }

            if (language != null)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);

                path = dilHaricYol;
            }

            HttpCookie cookie = new HttpCookie("language")
            {
                Value = language
            };

            Response.Cookies.Add(cookie);

            //return RedirectToAction("Index", "Home");
            path = "/" + language + dilHaricYol;

            return (ActionResult)Redirect(path);
            //return View(viewName);
        }
    }
}