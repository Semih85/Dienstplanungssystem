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
    public class EczaneNobetMazeretlerController : ApiController
    {
        private IEczaneNobetMazeretService _eczaneNobetMazeretService;

        public EczaneNobetMazeretlerController(IEczaneNobetMazeretService eczaneNobetMazeretService)
        {
            _eczaneNobetMazeretService = eczaneNobetMazeretService;
        }

        public List<EczaneNobetMazeretDetay> Get()
        {
            return _eczaneNobetMazeretService.GetDetaylar();
        }
    }
}
