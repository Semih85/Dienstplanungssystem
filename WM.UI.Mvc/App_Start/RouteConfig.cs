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

            routes.MapRoute(
                name: "Login",
                url: "giris",
                defaults: new { controller = "Account", action = "Login" },
                namespaces: new string[] { "WM.UI.Mvc.Controllers" }
            );

            routes.MapRoute(
                name: "Contact",
                url: "iletisim",
                defaults: new { controller = "Home", action = "Contact" },
                namespaces: new string[] { "WM.UI.Mvc.Controllers" }
            );

            routes.MapRoute(
                name: "NobetYazDetay",
                url: "nobet-sistemi-detaylar",
                defaults: new { controller = "Home", action = "NobetYazDetay" },
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
                url: "anaSayfa",
                defaults: new { controller = "Home", action = "Index" },
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
