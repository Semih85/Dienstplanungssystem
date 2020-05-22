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
    public class EczaneNobetDegisimTalepController : ApiController
    {
        #region ctor
        private IEczaneNobetDegisimTalepService _eczaneNobetDegisimTalepService;
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

        public EczaneNobetDegisimTalepController(IEczaneNobetDegisimTalepService eczaneNobetDegisimTalepService,
            IEczaneNobetDegisimArzService eczaneNobetDegisimArzService,
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
            _eczaneNobetDegisimTalepService = eczaneNobetDegisimTalepService;
            _eczaneNobetDegisimArzService = eczaneNobetDegisimArzService;
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


        [Route("eczane-nobet-degisim-talepler-hepsi/{userId:int:min(1)}")]
        [HttpGet]
        public List<EczaneNobetDegisimTalepDetay> Get(int userId)
        {
            
            User user = _userService.GetById(userId);

            user = _userService.GetById(userId);
            int eczaneId = _userEczaneService.GetListByUserId(user.Id).Select(s => s.EczaneId).FirstOrDefault();
            int eczaneNobetGrupId = _eczaneNobetGrupService.GetDetayByEczaneId(eczaneId).Id;
            NobetUstGrup nobetUstGrup = _eczaneService.GetByEczaneNobetGrupId(eczaneNobetGrupId);

            EczaneNobetGrup eczaneNobetGrup = new EczaneNobetGrup();
            eczaneNobetGrup = _eczaneNobetGrupService.GetById(eczaneNobetGrupId);
            List < EczaneNobetDegisimTalepDetay > list = _eczaneNobetDegisimTalepService.GetDetaylar(nobetUstGrup.Id)
                .Where(w => w.NobetGrupId == eczaneNobetGrup.NobetGrupGorevTipId
                //&& w.NobetTarihi > DateTime.Now
                ).ToList();
            return list;
        }

        [Route("eczane-nobet-degisim-talepler-tarihli")]
        [HttpPost]
        public HttpResponseMessage GetNobetDegisimTalep([FromBody]EczaneNobetDegisimTalepApi eczaneNobetDegisimTalepApi)
        {
            try
            {
                DateTime dt_tarihi = Convert.ToDateTime(eczaneNobetDegisimTalepApi.Tarih);
                Takvim takvim = _takvimService.GetByTarih(dt_tarihi);
                User User = _userService.GetById(eczaneNobetDegisimTalepApi.UserId);
                int eczaneId = _userEczaneService.GetListByUserId(eczaneNobetDegisimTalepApi.UserId).Select(s => s.EczaneId).FirstOrDefault();
                int eczaneNobetGrupId = _eczaneNobetGrupService.GetDetayByEczaneId(eczaneId).Id;
                NobetUstGrup nobetUstGrup = _eczaneService.GetByEczaneNobetGrupId(eczaneNobetGrupId);

                EczaneNobetGrup eczaneNobetGrup = new EczaneNobetGrup();
                eczaneNobetGrup = _eczaneNobetGrupService.GetById(eczaneNobetDegisimTalepApi.EczaneNobetGrupId);
                List<EczaneNobetDegisimTalepDetay> eczaneNobetDegisimTalepDetayList = new List<EczaneNobetDegisimTalepDetay>();
                eczaneNobetDegisimTalepDetayList = _eczaneNobetDegisimTalepService.GetDetaylar(nobetUstGrup.Id)
                 .Where(w => w.NobetTarihi == dt_tarihi
                    && w.NobetGrupId == eczaneNobetGrup.NobetGrupGorevTipId
                    && w.NobetTarihi > DateTime.Now
                    //&& w.Onay == false
                    )
                 .ToList();
                return Request.CreateResponse(HttpStatusCode.OK, eczaneNobetDegisimTalepDetayList);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message + e.InnerException.StackTrace);
            }
        }


        //[Route("degisim-ekle/{eczaneNobetGrupId:int:min(1)}/{tarih:maxlength(200)}/{aciklama:maxlength(200)}/{mazeretId:int:min(1)}")]
        // üstteki şekilde olursa FromUri olacak aşağıda

        [Route("degisim-talep-ekle")]
        [HttpPost]
        public HttpResponseMessage EczaneNobetDegisimTalepTalebiEkle([FromBody]EczaneNobetDegisimTalepApi eczaneNobetDegisimTalepApi)
        {
            LoginItem loginUser;
            User user;
            _yetkilendirme.YetkiKontrolu(eczaneNobetDegisimTalepApi, out loginUser, out user);
            string token = _yetkilendirme.GetToken2(loginUser);

            if (user != null)
            {
                if (token == eczaneNobetDegisimTalepApi.Token)
                {
                    try
                    {
                        Takvim takvim = _takvimService.GetByTarih(Convert.ToDateTime(eczaneNobetDegisimTalepApi.Tarih));
                        EczaneNobetDegisimTalep eczaneNobetDegisimTalep = new EczaneNobetDegisimTalep();
                        List<EczaneNobetDegisimArzDetay> eczaneNobetDegisimArzDetaylar = new List<EczaneNobetDegisimArzDetay>();
                        int eczaneNobetSonucId = _eczaneNobetSonucService.GetDetay(takvim.Id, eczaneNobetDegisimTalepApi.EczaneNobetGrupId).Id;
                        // eczaneNobetDegisimTalep.EczaneNobetSonucId = eczaneNobetSonucId;
                        eczaneNobetDegisimArzDetaylar = _eczaneNobetDegisimArzService.GetDetaylarByEczaneSonucId(eczaneNobetSonucId);

                        eczaneNobetDegisimTalep.EczaneNobetDegisimArzId = eczaneNobetDegisimArzDetaylar.FirstOrDefault().Id;
                        eczaneNobetDegisimTalep.EczaneNobetGrupId = eczaneNobetDegisimTalepApi.MyEczaneNobetGrupId;
                        eczaneNobetDegisimTalep.Aciklama = eczaneNobetDegisimTalepApi.Aciklama;
                        eczaneNobetDegisimTalep.KayitTarihi = DateTime.Now;
                        eczaneNobetDegisimTalep.UserId = eczaneNobetDegisimTalepApi.UserId;
                        _eczaneNobetDegisimTalepService.Insert(eczaneNobetDegisimTalep);
                        return Request.CreateResponse(HttpStatusCode.OK, eczaneNobetDegisimTalep);
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


      
        [Route("degisim-talep-sil")]
        [HttpPost]
        public HttpResponseMessage EczaneNobetDegisimTalepTalebiSil([FromBody]EczaneNobetDegisimTalepApi eczaneNobetDegisimTalepApi)
        {
            LoginItem loginUser;
            User user;
            _yetkilendirme.YetkiKontrolu(eczaneNobetDegisimTalepApi, out loginUser, out user);
            string token = _yetkilendirme.GetToken2(loginUser);

            if (user != null)
            {
                if (token == eczaneNobetDegisimTalepApi.Token)
                {
                    try
                    {
                        _eczaneNobetDegisimTalepService.Delete(eczaneNobetDegisimTalepApi.Id);
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
