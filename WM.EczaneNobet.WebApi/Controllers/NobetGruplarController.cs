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
    //[Route("api/[controller]")]
    public class NobetGruplarController : ApiController
    {
        private INobetGrupService _nobetGrupService;

        public NobetGruplarController(INobetGrupService nobetGrupService)
        {
            _nobetGrupService = nobetGrupService;
        }

        public List<NobetGrupDetay> Get()
        {
            return _nobetGrupService.GetDetaylar();
        }
    }
}
