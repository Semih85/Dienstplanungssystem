using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;

namespace WM.UI.Mvc.Services
{
    public class NobetUstGrupSessionService : INobetUstGrupSessionService
    {
        public NobetUstGrupDetay GetNobetUstGrup()
        {
            var nobetUstGrupToCheck = (NobetUstGrupDetay)HttpContext.Current.Session["nobetUstGrup"];

            if (nobetUstGrupToCheck == null)
            {
                HttpContext.Current.Session["nobetUstGrup"] = new NobetUstGrupDetay();

                nobetUstGrupToCheck = (NobetUstGrupDetay)HttpContext.Current.Session["nobetUstGrup"];
            }

            return nobetUstGrupToCheck;
        }

        public void SetNobetUstGrup(NobetUstGrupDetay nobetUstGrupDetay)
        {
            HttpContext.Current.Session["nobetUstGrup"] = nobetUstGrupDetay;
        }
    }
}