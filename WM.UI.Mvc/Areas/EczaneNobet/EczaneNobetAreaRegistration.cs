using System.Web.Mvc;

namespace WM.UI.Mvc.Areas.EczaneNobet
{
    public class EczaneNobetAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "EczaneNobet";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {

            context.MapRoute(
                "Create",
                "EczaneNobet/{controller}/Ekle",
                new { action = "Create" }
            );

            context.MapRoute(
                "Edit",
                "EczaneNobet/{controller}/Duzenle/{id}",
                new { action = "Edit", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "Delete",
                "EczaneNobet/{controller}/Sil/{id}",
                new { action = "Delete", id = UrlParameter.Optional }
            );

            context.MapRoute(
                "Details",
                "EczaneNobet/{controller}/Detaylar/{id}",
                new { action = "Details", id = UrlParameter.Optional }
            );

            #region MyRegion
            /*
                context.MapRoute(
                    "NobetGrup",
                    "NobetGrup",
                    new { controller = "NobetGrup", action = "Index" }
                );

                context.MapRoute(
                    "EczaneNobetGrup",
                    "EczaneNobetGrup",
                    new { controller = "EczaneNobetGrup", action = "Index" }
                );

                context.MapRoute(
                    "EczaneGrupTanim",
                    "EczaneGrupTanim",
                    new { controller = "EczaneGrupTanim", action = "Index" }
                );

                context.MapRoute(
                    "EczaneGrup",
                    "EczaneGrup",
                    new { controller = "EczaneGrup", action = "Index" }
                );
                context.MapRoute(
                    "NobetGrupKural",
                    "NobetGrupKural",
                    new { controller = "NobetGrupKural", action = "Index" }
                );

                context.MapRoute(
                    "EczaneNobetMazeret",
                    "EczaneNobetMazeret",
                    new { controller = "EczaneNobetMazeret", action = "Index" }
                );

                context.MapRoute(
                    "EczaneNobetIstek",
                    "EczaneNobetIstek",
                    new { controller = "EczaneNobetIstek", action = "Index" }
                );

                context.MapRoute(
                    "Eczane",
                    "Eczane",
                    new { controller = "Eczane", action = "Index" }
                );

                context.MapRoute(
                    "PivotSonuclar",
                    "PivotSonuclar",
                    new { controller = "EczaneNobetSonuc", action = "PivotSonuclar" }
                );

                context.MapRoute(
                    "GorselSonuclar",
                    "GorselSonuclar",
                    new { controller= "EczaneNobetSonuc", action = "GorselSonuclar"}
                );

                context.MapRoute(
                    "DemoPivotSonuclar",
                    "DemoPivotSonuclar",
                    new { controller = "EczaneNobetSonucDemo", action = "DemoPivot" }
                );

                context.MapRoute(
                    "DemoGorselSonuclar",
                    "DemoGorselSonuclar",
                    new { controller = "EczaneNobetSonucDemo", action = "GorselSonuclar" }
                );

                context.MapRoute(
                    "EczaneNobet",
                    "EczaneNobet",
                    new { controller = "EczaneNobet", action = "Index" }
                );
                */ 
            #endregion

            context.MapRoute(
                "EczaneNobet_default",
                "EczaneNobet/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}