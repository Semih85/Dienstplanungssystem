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
            if (language != null)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
            }

            HttpCookie cookie = new HttpCookie("language")
            {
                Value = language
            };

            Response.Cookies.Add(cookie);

            //return RedirectToAction("Index", "Home");
            return View(viewName);
        }
    }
}