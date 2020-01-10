using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;

namespace WM.UI.Mvc.Services
{
    public class NobetUstGrupKisitSessionService : INobetUstGrupKisitSessionService
    {
        public NobetUstGrupKisitDetay GetSession(string sessionAdi)
        {
            var nobetUstGrupToCheck = (NobetUstGrupKisitDetay)HttpContext.Current.Session[sessionAdi];

            if (nobetUstGrupToCheck == null)
            {
                HttpContext.Current.Session[sessionAdi] = new NobetUstGrupKisitDetay();

                nobetUstGrupToCheck = (NobetUstGrupKisitDetay)HttpContext.Current.Session[sessionAdi];
            }

            return nobetUstGrupToCheck;
        }
        public void SetSession(NobetUstGrupKisitDetay nobetUstGrupDetay, string sessionAdi)
        {
            HttpContext.Current.Session[sessionAdi] = nobetUstGrupDetay;
        }

        public List<NobetUstGrupKisitDetayDegisimTakip> GetSessionList(string sessionAdi, int nobetUstGrupId)
        {
            var nobetUstGrupToCheck = (List<NobetUstGrupKisitDetayDegisimTakip>)HttpContext.Current.Session[sessionAdi];

            if (nobetUstGrupToCheck == null)
            {
                HttpContext.Current.Session[sessionAdi] = new List<NobetUstGrupKisitDetayDegisimTakip>();

                nobetUstGrupToCheck = (List<NobetUstGrupKisitDetayDegisimTakip>)HttpContext.Current.Session[sessionAdi];
            }

            var sonNobetUstGrupToCheck = nobetUstGrupToCheck.Where(w => w.NobetUstGrupId == nobetUstGrupId).ToList();

            return sonNobetUstGrupToCheck;
        }
        public void AddSessionList(
            NobetUstGrupKisitDetay nobetUstGrupDetayOnce,
            NobetUstGrupKisitDetay nobetUstGrupDetaySonra,
            string sessionAdi,
            List<NobetUstGrupKisitDetayDegisimTakip> nobetUstGrupKisitlar)
        {
            var sira = nobetUstGrupKisitlar.LastOrDefault() != null ? nobetUstGrupKisitlar.LastOrDefault().SiraNumarasi : 0;

            nobetUstGrupKisitlar.Add(new NobetUstGrupKisitDetayDegisimTakip(sira)
            {
                NobetUstGrupKisitDetayOnce = nobetUstGrupDetayOnce,
                NobetUstGrupKisitDetaySonra = nobetUstGrupDetaySonra,
                DegisimTarihi = DateTime.Now,
                NobetUstGrupId = nobetUstGrupDetaySonra.NobetUstGrupId
            });

            HttpContext.Current.Session[sessionAdi] = nobetUstGrupKisitlar;
        }
    }
}