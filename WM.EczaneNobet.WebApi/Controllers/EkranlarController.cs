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
        [Route("ekranlar/{cihazId:int:min(1)}")]
        [HttpGet]
        public EkranTakipDetay Get(int cihazId) 
        {
            var cihazListesi = new List<EkranTakipDetay>
            {
                new EkranTakipDetay { CihazId = 1, CihazUrl = "https://nobetyaz.com/onee/954", TasarimDegisimTarihi = new DateTime(2019, 2, 10, 1, 1, 1), Durum = true }
            };

            return cihazListesi.SingleOrDefault(x => x.CihazId == cihazId);
        }
    }
}
