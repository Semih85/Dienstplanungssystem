using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WM.EczaneNobet.WebApi.MessageHandlers;
using WM.EczaneNobet.WebApi.Models;
using WM.Northwind.Business.Abstract.Authorization;
using WM.Northwind.Business.Abstract.EczaneNobet;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.Authorization;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.EczaneNobet.WebApi.Controllers
{
    public class EczaneNobetMazeretlerController : ApiController
    {
        #region ctor
        private IEczaneNobetMazeretService _eczaneNobetMazeretService;
        private IUserService _userService;
        private ITakvimService _takvimService;
        private IMazeretService _mazeretService;
        private IUserRoleService _userRoleService;
        Yetkilendirme _yetkilendirme;

        public EczaneNobetMazeretlerController(IEczaneNobetMazeretService eczaneNobetMazeretService,
                                                IUserService userService,
                                                ITakvimService takvimService,
                                                IMazeretService mazeretService,
                                IUserRoleService userRoleService)
        {
            _eczaneNobetMazeretService = eczaneNobetMazeretService;
            _userService = userService;
            _takvimService = takvimService;
            _mazeretService = mazeretService;
            _userRoleService = userRoleService;
            _yetkilendirme = new Yetkilendirme(_userService, _userRoleService);

        }
        #endregion

        [Route("eczane-nobet-mazeretler/{eczaneNobetGrupId:int:min(1)}")]
        [HttpGet]
        public List<EczaneNobetMazeretDetay> Get(int eczaneNobetGrupId)
        {
            DateTime? dt = Convert.ToDateTime("2018-01-01");
            DateTime? dt2 = Convert.ToDateTime("2021-01-01");
            return _eczaneNobetMazeretService.GetDetaylarByEczaneNobetId(DateTime.Now.AddYears(-1), DateTime.Now.AddYears(1), eczaneNobetGrupId)
                //.OrderByDescending(o => o.Tarih)
                .ToList();
        }

        [Route("eczane-nobet-mazeretler-tarihli")]
        [HttpPost]
        public HttpResponseMessage GetMazeretlerTarihli([FromBody]EczaneNobetMazeretApi eczaneNobetMazeretApi)
        {
            try
            {
                DateTime? dt_baslangicTarihi = Convert.ToDateTime(eczaneNobetMazeretApi.BaslangicTarihi);
                DateTime? dt_bitisTarihi = Convert.ToDateTime(eczaneNobetMazeretApi.BitisTarihi);
                List<EczaneNobetMazeretDetay> eczaneNobetMazeretDetayList = new List<EczaneNobetMazeretDetay>();
                eczaneNobetMazeretDetayList = _eczaneNobetMazeretService.GetDetaylarByEczaneNobetId(dt_baslangicTarihi, dt_bitisTarihi, eczaneNobetMazeretApi.EczaneNobetGrupId)
                    //.OrderByDescending(o => o.Tarih)
                    .ToList();
                return Request.CreateResponse(HttpStatusCode.OK, eczaneNobetMazeretDetayList);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message + e.InnerException.StackTrace);
            }
        }

        //[Route("mazeret-ekle/{eczaneNobetGrupId:int:min(1)}/{tarih:maxlength(200)}/{aciklama:maxlength(200)}/{mazeretId:int:min(1)}")]
        // üstteki şekilde olursa FromUri olacak aşağıda
        [Route("mazeret-ekle")]
        [HttpPost]
        public HttpResponseMessage MazeretEkle([FromBody]EczaneNobetMazeretApi eczaneNobetMazeretApi)
        {
            LoginItem loginUser;
            User user;
            _yetkilendirme.YetkiKontrolu(eczaneNobetMazeretApi, out loginUser, out user);
            string token = _yetkilendirme.GetToken2(loginUser);

            if (user != null)
            {
                if (token == eczaneNobetMazeretApi.Token)
                {
                    try
                    {
                        Takvim takvim = _takvimService.GetByTarih(Convert.ToDateTime(eczaneNobetMazeretApi.Tarih));
                        EczaneNobetMazeret eczaneNobetMazeret = new EczaneNobetMazeret();
                        eczaneNobetMazeret.TakvimId = takvim.Id;
                        eczaneNobetMazeret.EczaneNobetGrupId = eczaneNobetMazeretApi.EczaneNobetGrupId;
                        eczaneNobetMazeret.Aciklama = eczaneNobetMazeretApi.Aciklama;
                        eczaneNobetMazeret.MazeretId = Convert.ToInt32(eczaneNobetMazeretApi.MazeretId);
                        _eczaneNobetMazeretService.Insert(eczaneNobetMazeret);
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                    catch (Exception e)
                    {
                        return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message + e.InnerException.StackTrace);
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Conflict, "Token geçersiz.");
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Kullanıcı adı ve şifresi geçersiz.");
            }
            //else
            // return Request.CreateResponse(HttpStatusCode.Unauthorized);
        }

        [Route("mazeret-ekle-coklu")]
        [HttpPost]
        public HttpResponseMessage MazeretEkleCoklu([FromBody]EczaneNobetMazeretCokluApi eczaneNobetMazeretCokluApi)
        {
            LoginItem loginUser;
            User user;
            _yetkilendirme.YetkiKontrolu(eczaneNobetMazeretCokluApi, out loginUser, out user);
            string token = _yetkilendirme.GetToken2(loginUser);

            if (user != null)
            {
                if (token == eczaneNobetMazeretCokluApi.Token)
                {
                    try
                    {
                        foreach (var item in eczaneNobetMazeretCokluApi.Tarihler)
                        {
                            Takvim takvim = _takvimService.GetByTarih(Convert.ToDateTime(item));
                            EczaneNobetMazeret eczaneNobetMazeret = new EczaneNobetMazeret();
                            eczaneNobetMazeret.TakvimId = takvim.Id;
                            eczaneNobetMazeret.EczaneNobetGrupId = eczaneNobetMazeretCokluApi.EczaneNobetGrupId;
                            eczaneNobetMazeret.Aciklama = eczaneNobetMazeretCokluApi.Aciklama;
                            eczaneNobetMazeret.MazeretId = Convert.ToInt32(eczaneNobetMazeretCokluApi.MazeretId);
                            _eczaneNobetMazeretService.Insert(eczaneNobetMazeret);
                        }

                        return Request.CreateResponse(HttpStatusCode.OK);
                    }

                    catch (Exception e)
                    {
                        return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message + e.InnerException.StackTrace);
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Conflict, "Token geçersiz.");
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Kullanıcı adı ve şifresi geçersiz.");
            }
            //else
            // return Request.CreateResponse(HttpStatusCode.Unauthorized);
        }


        [Route("mazeret-guncelle")]
        [HttpPost]
        public HttpResponseMessage mazeretGuncelle([FromBody]EczaneNobetMazeretApi eczaneNobetMazeretApi)
        {
            LoginItem loginUser;
            User user;
            _yetkilendirme.YetkiKontrolu(eczaneNobetMazeretApi, out loginUser, out user);
            string token = _yetkilendirme.GetToken2(loginUser);

            if (user != null)
            {
                if (token == eczaneNobetMazeretApi.Token)
                {
                    try
                    {
                        Takvim takvim = _takvimService.GetByTarih(Convert.ToDateTime(eczaneNobetMazeretApi.Tarih));
                        EczaneNobetMazeret eczaneNobetMazeret = _eczaneNobetMazeretService.GetById(eczaneNobetMazeretApi.Id);
                        eczaneNobetMazeret.TakvimId = takvim.Id;
                        eczaneNobetMazeret.EczaneNobetGrupId = eczaneNobetMazeretApi.EczaneNobetGrupId;
                        eczaneNobetMazeret.Aciklama = eczaneNobetMazeretApi.Aciklama;
                        eczaneNobetMazeret.MazeretId = Convert.ToInt32(eczaneNobetMazeretApi.MazeretId);
                        _eczaneNobetMazeretService.Update(eczaneNobetMazeret);
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                    catch (Exception e)
                    {
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, e.Message + e.InnerException.StackTrace);
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Conflict, "Token geçersiz.");
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Kullanıcı adı ve şifresi geçersiz.");
            }
        }


        [Route("mazeret-sil")]
        [HttpPost]
        public HttpResponseMessage mazeretSil([FromBody]EczaneNobetMazeretApi eczaneNobetMazeretApi)
        {
            LoginItem loginUser;
            User user;
            _yetkilendirme.YetkiKontrolu(eczaneNobetMazeretApi, out loginUser, out user);
            string token = _yetkilendirme.GetToken2(loginUser);

            if (user != null)
            {
                if (token == eczaneNobetMazeretApi.Token)
                {
                    try
                    {
                        _eczaneNobetMazeretService.Delete(eczaneNobetMazeretApi.Id);
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                    catch (Exception e)
                    {
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, e.Message + e.InnerException.StackTrace);
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.Conflict, "Token geçersiz.");
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Kullanıcı adı ve şifresi geçersiz.");
            }
        }


    }
}
