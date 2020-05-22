using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WM.Northwind.Business.Abstract.Authorization;
using WM.Northwind.Business.Abstract.EczaneNobet;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.Authorization;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.EczaneNobet.WebApi.Controllers
{
    public class MazeretController : ApiController
    {
        #region ctor
        private IMazeretService _mazeretService;

        public MazeretController(IMazeretService mazeretService)
        {
            _mazeretService = mazeretService;
        }
        #endregion

        [Route("mazeretler")]
        [HttpGet]
        public List<Mazeret> Get()
        {
            return _mazeretService.GetList();
        }


    }
}
