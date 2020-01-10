using System.Web.Mvc;

namespace WM.UI.Mvc.Areas.Northwind
{
    public class NorthwindAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Northwind";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Northwind_default",
                "Northwind/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}