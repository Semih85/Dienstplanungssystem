using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;

namespace WM.UI.Mvc.Services
{
    public class NobetUstGrupSessionService : INobetUstGrupSessionService
    {
        public NobetUstGrupDetay GetSession(string sessionAdi)
        {
            var nobetUstGrupToCheck = (NobetUstGrupDetay)HttpContext.Current.Session[sessionAdi];

            if (nobetUstGrupToCheck == null)
            {
                HttpContext.Current.Session[sessionAdi] = new NobetUstGrupDetay();

                nobetUstGrupToCheck = (NobetUstGrupDetay)HttpContext.Current.Session[sessionAdi];
            }

            return nobetUstGrupToCheck;
        }

        public void SetSession(NobetUstGrupDetay nobetUstGrupDetay, string sessionAdi)
        {
            HttpContext.Current.Session[sessionAdi] = nobetUstGrupDetay;
        }
    }
}