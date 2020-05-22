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
    public class EczaneNobetDegisimArzController : ApiController
    {
        #region ctor
        private IEczaneNobetDegisimArzService _eczaneNobetDegisimArzService;
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

        public EczaneNobetDegisimArzController(IEczaneNobetDegisimArzService EczaneNobetDegisimArzService,
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
            _eczaneNobetDegisimArzService = EczaneNobetDegisimArzService;
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


        [Route("eczane-nobet-degisim-arzlar-hepsi/{userId:int:min(1)}")]
        [HttpGet]
        public List<EczaneNobetDegisimArzDetay> Get(int userId)
        {
            User user = _userService.GetById(userId);
            int eczaneId = _userEczaneService.GetListByUserId(userId).Select(s => s.EczaneId).FirstOrDefault();
            int eczaneNobetGrupId = _eczaneNobetGrupService.GetDetayByEczaneId(eczaneId).Id;
            NobetUstGrup nobetUstGrup = _eczaneService.GetByEczaneNobetGrupId(eczaneNobetGrupId);

            EczaneNobetGrup eczaneNobetGrup = new EczaneNobetGrup();
            eczaneNobetGrup = _eczaneNobetGrupService.GetById(eczaneNobetGrupId);
            return _eczaneNobetDegisimArzService.GetDetaylar(nobetUstGrup.Id)
                .Where(w => w.NobetGrupId == eczaneNobetGrup.NobetGrupGorevTipId
                //&& w.NobetTarihi > DateTime.Now
                ).ToList();
        }


        [Route("eczane-nobet-degisim-arzlar-tarihli")]
        [HttpPost]
        public HttpResponseMessage PostNobetDegisimArz([FromBody]EczaneNobetDegisimArzApi eczaneNobetDegisimArzApi)
        {
            try
            {
                DateTime dt_tarihi = Convert.ToDateTime(eczaneNobetDegisimArzApi.Tarih);
                Takvim takvim = _takvimService.GetByTarih(dt_tarihi);
                User User = _userService.GetById(eczaneNobetDegisimArzApi.UserId);

                EczaneNobetGrup eczaneNobetGrup = new EczaneNobetGrup();
                eczaneNobetGrup = _eczaneNobetGrupService.GetById(eczaneNobetDegisimArzApi.EczaneNobetGrupId);
                NobetUstGrup nobetUstGrup = _eczaneService.GetByEczaneNobetGrupId(eczaneNobetDegisimArzApi.EczaneNobetGrupId);

                List<EczaneNobetDegisimArzDetay> eczaneNobetDegisimArzDetayList = new List<EczaneNobetDegisimArzDetay>();
                eczaneNobetDegisimArzDetayList = _eczaneNobetDegisimArzService.GetDetaylar(nobetUstGrup.Id)
                 .Where(w => w.NobetTarihi == dt_tarihi
                    && w.NobetGrupId == eczaneNobetGrup.NobetGrupGorevTipId
                    && w.NobetTarihi > DateTime.Now
                    //&& w.Onay == false
                    )
                 .ToList();
                return Request.CreateResponse(HttpStatusCode.OK, eczaneNobetDegisimArzDetayList);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message + e.InnerException.StackTrace);
            }
        }
       


        //[Route("degisim-ekle/{eczaneNobetGrupId:int:min(1)}/{tarih:maxlength(200)}/{aciklama:maxlength(200)}/{mazeretId:int:min(1)}")]
        // üstteki şekilde olursa FromUri olacak aşağıda

        [Route("degisim-arz-ekle")]
        [HttpPost]
        public HttpResponseMessage EczaneNobetDegisimarziEkle([FromBody]EczaneNobetDegisimArzApi eczaneNobetDegisimArzApi)
        {
            LoginItem loginUser;
            User user;
            _yetkilendirme.YetkiKontrolu(eczaneNobetDegisimArzApi, out loginUser, out user);
            string token = _yetkilendirme.GetToken2(loginUser);

            if (user != null)
            {
                if (token == eczaneNobetDegisimArzApi.Token)
                {
                    try
                    {
                        Takvim takvim = _takvimService.GetByTarih(Convert.ToDateTime(eczaneNobetDegisimArzApi.Tarih));
                        EczaneNobetDegisimArz eczaneNobetDegisimArz = new EczaneNobetDegisimArz();
                        int eczaneNobetSonucId = _eczaneNobetSonucService.GetDetay(takvim.Id, eczaneNobetDegisimArzApi.EczaneNobetGrupId).Id;

                        eczaneNobetDegisimArz.EczaneNobetSonucId = eczaneNobetSonucId;
                        eczaneNobetDegisimArz.EczaneNobetGrupId = eczaneNobetDegisimArzApi.EczaneNobetGrupId;
                        eczaneNobetDegisimArz.Aciklama = eczaneNobetDegisimArzApi.Aciklama;
                        eczaneNobetDegisimArz.KayitTarihi = DateTime.Now;
                        eczaneNobetDegisimArz.UserId = eczaneNobetDegisimArzApi.UserId;
                        _eczaneNobetDegisimArzService.Insert(eczaneNobetDegisimArz);
                        return Request.CreateResponse(HttpStatusCode.OK, eczaneNobetDegisimArz);
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


      
        [Route("degisim-arz-sil")]
        [HttpPost]
        public HttpResponseMessage EczaneNobetDegisimTArziSil([FromBody]EczaneNobetDegisimArzApi eczaneNobetDegisimArzApi)
        {
            LoginItem loginUser;
            User user;
            _yetkilendirme.YetkiKontrolu(eczaneNobetDegisimArzApi, out loginUser, out user);
            string token = _yetkilendirme.GetToken2(loginUser);

            if (user != null)
            {
                if (token == eczaneNobetDegisimArzApi.Token)
                {
                    try
                    {
                        _eczaneNobetDegisimArzService.Delete(eczaneNobetDegisimArzApi.Id);
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
