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
    public class EczaneNobetDegisimController : ApiController
    {
        #region ctor
        private IEczaneNobetDegisimService _eczaneNobetDegisimService;
        private IUserEczaneService _userEczaneService;
        private IEczaneService _eczaneService;
        private IUserService _userService;
        private INobetUstGrupService _nobetUstGrupService;
        private INobetGrupService _nobetGrupService;
        private ITakvimService _takvimService;
        private IEczaneNobetSonucService _eczaneNobetSonucService;
        private IEczaneNobetGrupService _eczaneNobetGrupService;
        private IUserRoleService _userRoleService;
        Yetkilendirme _yetkilendirme;

        public EczaneNobetDegisimController(IEczaneNobetDegisimService eczaneNobetDegisimService,
                                                IEczaneService eczaneService,
                                                IUserEczaneService userEczaneService,
                                                IUserService userService,
                                                INobetGrupService nobetGrupService,
                                                INobetUstGrupService nobetUstGrupService,
                                                ITakvimService takvimService,
                                                IEczaneNobetSonucService eczaneNobetSonucService,
                                                IEczaneNobetGrupService eczaneNobetGrupService,
                                IUserRoleService userRoleService)
        {
            _eczaneNobetDegisimService = eczaneNobetDegisimService;
            _eczaneService = eczaneService;
            _userEczaneService = userEczaneService;
            _userService = userService;
            _nobetUstGrupService = nobetUstGrupService;
            _nobetGrupService = nobetGrupService;
            _takvimService = takvimService;
            _eczaneNobetSonucService = eczaneNobetSonucService;
            _eczaneNobetGrupService = eczaneNobetGrupService;
            _userRoleService = userRoleService;
            _yetkilendirme = new Yetkilendirme(_userService, _userRoleService);

        }
        #endregion


        [Route("eczane-nobet-degisimler-hepsi/{userId:int:min(1)}")]
        [HttpGet]
        public List<EczaneNobetDegisimDetay> Get(int userId)
        {
            User user = _userService.GetById(userId);
            int eczaneId = _userEczaneService.GetListByUserId(userId).Select(s => s.EczaneId).FirstOrDefault();
            int eczaneNobetGrupId = _eczaneNobetGrupService.GetDetayByEczaneId(eczaneId).Id;
            NobetUstGrup nobetUstGrup = _eczaneService.GetByEczaneNobetGrupId(eczaneNobetGrupId);
            user = _userService.GetById(userId);
            EczaneNobetGrup eczaneNobetGrup = new EczaneNobetGrup();
            eczaneNobetGrup = _eczaneNobetGrupService.GetById(eczaneNobetGrupId);
            return _eczaneNobetDegisimService.GetDetaylar(nobetUstGrup.Id)
                .Where(w => w.NobetGrupId == eczaneNobetGrup.NobetGrupGorevTipId
                //&& w.NobetTarihi > DateTime.Now
                ).ToList();
        }



        [Route("eczane-nobet-degisimler-tarihli")]
        [HttpPost]
        public HttpResponseMessage GetNobetDegisim([FromBody]EczaneNobetDegisimApi eczaneNobetDegisimApi)
        {
            try
            {
                DateTime dt_tarihi = Convert.ToDateTime(eczaneNobetDegisimApi.Tarih);
                Takvim takvim = _takvimService.GetByTarih(dt_tarihi);
                User User = _userService.GetById(eczaneNobetDegisimApi.UserId);
                int eczaneId = _userEczaneService.GetListByUserId(User.Id).Select(s => s.EczaneId).FirstOrDefault();
                int eczaneNobetGrupId = _eczaneNobetGrupService.GetDetayByEczaneId(eczaneId).Id;
                NobetUstGrup nobetUstGrup = _eczaneService.GetByEczaneNobetGrupId(eczaneNobetGrupId);
                EczaneNobetGrup eczaneNobetGrup = new EczaneNobetGrup();
                eczaneNobetGrup = _eczaneNobetGrupService.GetById(eczaneNobetDegisimApi.EczaneNobetGrupId);
                List<EczaneNobetDegisimDetay> eczaneNobetDegisimDetayList = new List<EczaneNobetDegisimDetay>();
                eczaneNobetDegisimDetayList = _eczaneNobetDegisimService.GetDetaylar(nobetUstGrup.Id)
                 .Where(w => w.NobetTarihi == dt_tarihi
                    && w.NobetGrupId == eczaneNobetGrup.NobetGrupGorevTipId
                    && w.NobetTarihi > DateTime.Now
                    //&& w.Onay == false
                    )
                 .ToList();
                return Request.CreateResponse(HttpStatusCode.OK, eczaneNobetDegisimDetayList);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message + e.InnerException.StackTrace);
            }
        }


        //[Route("degisim-ekle/{eczaneNobetGrupId:int:min(1)}/{tarih:maxlength(200)}/{aciklama:maxlength(200)}/{mazeretId:int:min(1)}")]
        // üstteki şekilde olursa FromUri olacak aşağıda

        [Route("degisim-ekle")]
        [HttpPost]
        public HttpResponseMessage EczaneNobetDegisimTalebiEkle([FromBody]EczaneNobetDegisimApi eczaneNobetDegisimApi)
        {
            LoginItem loginUser;
            User user;
            _yetkilendirme.YetkiKontrolu(eczaneNobetDegisimApi, out loginUser, out user);
            string token = _yetkilendirme.GetToken2(loginUser);

            if (user != null)
            {
                if (token == eczaneNobetDegisimApi.Token)
                {
                    try
                    {
                        Takvim takvim = _takvimService.GetByTarih(Convert.ToDateTime(eczaneNobetDegisimApi.Tarih));
                        EczaneNobetDegisim eczaneNobetDegisim = new EczaneNobetDegisim();
                        int eczaneNobetSonucId = _eczaneNobetSonucService.GetDetay(takvim.Id, eczaneNobetDegisimApi.MyEczaneNobetGrupId).Id;
                        eczaneNobetDegisim.EczaneNobetSonucId = eczaneNobetSonucId;
                        eczaneNobetDegisim.EczaneNobetGrupId = eczaneNobetDegisimApi.MyEczaneNobetGrupId;
                        eczaneNobetDegisim.Aciklama = eczaneNobetDegisimApi.Aciklama;
                        eczaneNobetDegisim.KayitTarihi = DateTime.Now;
                        eczaneNobetDegisim.UserId = eczaneNobetDegisimApi.UserId;
                        _eczaneNobetDegisimService.Insert(eczaneNobetDegisim);
                        return Request.CreateResponse(HttpStatusCode.OK, eczaneNobetDegisim);
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


        [Route("degisim-cevap-ekle")]
        [HttpPost]
        public HttpResponseMessage EczaneNobetDegisimTalebineCevapEkle([FromBody]EczaneNobetDegisimApi eczaneNobetDegisimApi)
        {
            LoginItem loginUser;
            User user;
            _yetkilendirme.YetkiKontrolu(eczaneNobetDegisimApi, out loginUser, out user);
            string token = _yetkilendirme.GetToken2(loginUser);

            if (user != null)
            {
                if (token == eczaneNobetDegisimApi.Token)
                {
                    try
                    {
                        Takvim takvim = _takvimService.GetByTarih(Convert.ToDateTime(eczaneNobetDegisimApi.Tarih));
                        EczaneNobetDegisim eczaneNobetDegisim = new EczaneNobetDegisim();
                        int eczaneNobetSonucId = _eczaneNobetSonucService.GetDetay(takvim.Id, eczaneNobetDegisimApi.EczaneNobetGrupId).Id;
                        eczaneNobetDegisim.EczaneNobetSonucId = eczaneNobetSonucId;
                        eczaneNobetDegisim.EczaneNobetGrupId = eczaneNobetDegisimApi.MyEczaneNobetGrupId;
                        eczaneNobetDegisim.Aciklama = eczaneNobetDegisimApi.Aciklama;
                        eczaneNobetDegisim.KayitTarihi = DateTime.Now;
                        eczaneNobetDegisim.UserId = eczaneNobetDegisimApi.UserId;
                        _eczaneNobetDegisimService.Insert(eczaneNobetDegisim);
                        return Request.CreateResponse(HttpStatusCode.OK, eczaneNobetDegisim);
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

        //[Route("degisim-cevap-ekle-get")]
        //[HttpGet]
        //public HttpResponseMessage EczaneNobetDegisimTalebineCevapEkleGet()
        //{
        //    EczaneNobetDegisimApi eczaneNobetDegisimApi = new EczaneNobetDegisimApi();
        //    eczaneNobetDegisimApi.EczaneNobetGrupId = 147;
        //    eczaneNobetDegisimApi.MyEczaneNobetGrupId = 137;
        //    eczaneNobetDegisimApi.Aciklama = "";
        //    eczaneNobetDegisimApi.Tarih = DateTime.Now;
        //    eczaneNobetDegisimApi.UserId = 2;
        //    eczaneNobetDegisimApi.Token = "49AC3F84555FDB62B85F3718CAAF86609E6D09652BCC594EC562E7A513373F3E";
        //    eczaneNobetDegisimApi.Onay = false;
        //    LoginItem loginUser = new LoginItem();
        //    User user = new User();
        //    _yetkilendirme.YetkiKontrolu(eczaneNobetDegisimApi, out loginUser, out user);
        //    string token = _yetkilendirme.GetToken2(loginUser);

        //    if (user != null)
        //    {
        //        if (token == eczaneNobetDegisimApi.Token)
        //        {
        //            try
        //            {
        //                Takvim takvim = _takvimService.GetByTarih(Convert.ToDateTime(eczaneNobetDegisimApi.Tarih));
        //                EczaneNobetDegisim eczaneNobetDegisim = new EczaneNobetDegisim();
        //                int eczaneNobetSonucId = _eczaneNobetSonucService.GetDetay(takvim.Id, eczaneNobetDegisimApi.EczaneNobetGrupId).Id;
        //                eczaneNobetDegisim.EczaneNobetSonucId = eczaneNobetSonucId;
        //                eczaneNobetDegisim.EczaneNobetGrupId = eczaneNobetDegisimApi.MyEczaneNobetGrupId;
        //                eczaneNobetDegisim.Aciklama = eczaneNobetDegisimApi.Aciklama;
        //                eczaneNobetDegisim.KayitTarihi = DateTime.Now;
        //                eczaneNobetDegisim.UserId = eczaneNobetDegisimApi.UserId;
        //                eczaneNobetDegisim.Onay = eczaneNobetDegisimApi.Onay;
        //                _eczaneNobetDegisimService.Insert(eczaneNobetDegisim);
        //                return Request.CreateResponse(HttpStatusCode.OK, eczaneNobetDegisim);
        //            }
        //            catch (Exception e)
        //            {
        //                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message + e.InnerException.StackTrace);
        //            }
        //        }
        //        else
        //        {
        //            return Request.CreateResponse(HttpStatusCode.Conflict, "Token geçersiz.");
        //        }
        //    }
        //    else
        //    {
        //        return Request.CreateResponse(HttpStatusCode.Unauthorized, "Kullanıcı adı ve şifresi geçersiz.");
        //    }
        //    //else
        //    // return Request.CreateResponse(HttpStatusCode.Unauthorized);
        //}

        [Route("degisim-sil")]
        [HttpPost]
        public HttpResponseMessage EczaneNobetDegisimTalebiSil([FromBody]EczaneNobetDegisimApi eczaneNobetDegisimApi)
        {
            LoginItem loginUser;
            User user;
            _yetkilendirme.YetkiKontrolu(eczaneNobetDegisimApi, out loginUser, out user);
            string token = _yetkilendirme.GetToken2(loginUser);

            if (user != null)
            {
                if (token == eczaneNobetDegisimApi.Token)
                {
                    try
                    {
                        _eczaneNobetDegisimService.Delete(eczaneNobetDegisimApi.Id);
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
