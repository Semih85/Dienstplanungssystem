using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WM.UI.Mvc
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();

            var lang = System.Threading.Thread.CurrentThread.CurrentUICulture.Name;

            if (lang.Length > 2)
            {
                lang = lang.Substring(0, 2);
            }

            routes.MapRoute(
                name: "Login",
                url: "{language}/giris",
                defaults: new
                {
                    language = lang,
                    controller = "Account",
                    action = "Login"
                },
                namespaces: new string[] { "WM.UI.Mvc.Controllers" }
            );

            routes.MapRoute(
                name: "Contact",
                url: "{language}/iletisim",
                defaults: new
                {
                    language = lang,
                    controller = "Home",
                    action = "Contact"
                },
                namespaces: new string[] { "WM.UI.Mvc.Controllers" }
            );

            routes.MapRoute(
                name: "NobetYazDetay",
                url: "{language}/nobet-sistemi-detaylar",
                defaults: new
                {
                    language = lang,
                    controller = "Home",
                    action = "NobetYazDetay"
                },
                namespaces: new string[] { "WM.UI.Mvc.Controllers" }
            );

            routes.MapRoute(
                name: "EczaneNobetSistemi",
                url: "eczane-nobet-sistemi",
                defaults: new { controller = "Home", action = "EczaneNobetSistemi" },
                namespaces: new string[] { "WM.UI.Mvc.Controllers" }
            );

            routes.MapRoute(
                name: "EczaneNobetSistemiDetay",
                url: "eczane-nobet-sistemi-arayuzler",
                defaults: new { controller = "Home", action = "EczaneNobetSistemiDetay" },
                namespaces: new string[] { "WM.UI.Mvc.Controllers" }
            );

            routes.MapRoute(
                name: "DijitalTabela",
                url: "online-eczane-ekrani",
                defaults: new { controller = "Home", action = "DijitalTabela" },
                namespaces: new string[] { "WM.UI.Mvc.Controllers" }
            );

            routes.MapRoute(
                name: "Anasayfa",
                url: "{language}",
                defaults: new
                {
                    language = lang,
                    controller = "Home",
                    action = "Index"
                },
                namespaces: new string[] { "WM.UI.Mvc.Controllers" }
            );

            routes.MapRoute(
                name: "DefaultWithLang",
                url: "{language}/{controller}/{action}/{id}",
                defaults: new
                {
                    language = lang,
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional
                },
                namespaces: new string[] { "WM.UI.Mvc.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "WM.UI.Mvc.Controllers" }
            );
        }
    }
}
