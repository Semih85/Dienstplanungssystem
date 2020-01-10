using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;

namespace WM.EczaneNobet.WebApi.Controllers
{
    public class EkranlarController : ApiController
    {
        static EkranTakipDetay Ekran(int cihazId)
        {
            return MyClass.EkranListe.SingleOrDefault(x => x.CihazId == cihazId);
        }

        [Route("ekranlar/{cihazId:int:min(1)}")]
        [HttpGet]
        public EkranTakipDetay Get(int cihazId)
        {
            return Ekran(cihazId);
        }

        [Route("cihaz-durum-guncelle/{cihazId:int:min(1)}/{cihazDurumId:int:min(0)}")]
        [HttpPost]
        public void CihazDurumGuncelle(int cihazId, int cihazDurumId)
        {
            Ekran(cihazId).CihazDurumId = cihazDurumId;
        }
    }

    static class MyClass
    {
        public static List<EkranTakipDetay> EkranListe { get; set; }

        static MyClass()
        {
            EkranListe = new List<EkranTakipDetay>
            {
                new EkranTakipDetay { CihazId = 1, CihazUrl = "https://nobetyaz.com/onee/954", CihazDurumId = 1 }
            };
        }
    }
}
