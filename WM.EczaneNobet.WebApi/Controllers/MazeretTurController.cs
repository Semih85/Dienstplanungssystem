using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WM.EczaneNobet.WebApi.Models;
using WM.Northwind.Business.Abstract.Authorization;
using WM.Northwind.Business.Abstract.EczaneNobet;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.Authorization;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.EczaneNobet.WebApi.Controllers
{
    public class MazeretTurController : ApiController
    {
        #region ctor
        private IMazeretTurService _mazeretTurService;

        public MazeretTurController(IMazeretTurService mazeretTurService)
        {
            _mazeretTurService = mazeretTurService;
        }
        #endregion

        [Route("mazeret-turler")]
        [HttpGet]
        public List<MazeretTurApi> Get()
        {
            List<MazeretTur> list = _mazeretTurService.GetList();
            List<MazeretTurApi> listApi = new List<MazeretTurApi>();
            foreach (var item in list)
            {
                MazeretTurApi itemApi = new MazeretTurApi();
                itemApi.Id = item.Id;
                itemApi.Adi = item.Adi;
                listApi.Add(itemApi);
            }
            return listApi;
        }


    }
}
