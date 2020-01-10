using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WM.Northwind.Business.Abstract.EczaneNobet;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;

namespace WM.EczaneNobet.WebApi.Controllers
{
    public class EczaneNobetSonuclarController : ApiController
    {
        private IEczaneNobetSonucService _eczaneNobetSonucService;

        public EczaneNobetSonuclarController(IEczaneNobetSonucService eczaneNobetSonucService)
        {
            _eczaneNobetSonucService = eczaneNobetSonucService;
        }

        public List<EczaneNobetSonucListe2> Get()
        {
            return _eczaneNobetSonucService.GetSonuclar(3);
        }
    }
}
