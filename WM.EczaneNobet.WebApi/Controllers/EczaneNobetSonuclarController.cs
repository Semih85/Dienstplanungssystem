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
    public class EczaneNobetSonuclarController : ApiController
    {
        #region ctor
        private IEczaneNobetSonucService _eczaneNobetSonucService;
        private IUserService _userService;
        private IUserEczaneService _userEczaneService;
        private INobetUstGrupService _nobetUstGrupService;
        private INobetGrupService _nobetGrupService;
        private IEczaneNobetGrupService _eczaneNobetGrupService;
        private IEczaneService _eczaneService;
        private IEczaneNobetOrtakService _eczaneNobetOrtakService;
        private IUserRoleService _userRoleService;
        Yetkilendirme _yetkilendirme;


        public EczaneNobetSonuclarController(IEczaneNobetSonucService eczaneNobetSonucService,
                                                IUserService userService,
                                                IUserEczaneService userEczaneService,
                                                INobetGrupService nobetGrupService,
                                                INobetUstGrupService nobetUstGrupService,
                                                IEczaneNobetGrupService eczaneNobetGrupService,
                                                IEczaneService eczaneService,
                                                IEczaneNobetOrtakService eczaneNobetOrtakService,
                                IUserRoleService userRoleService
            )
        {
            _userService = userService;
            _eczaneNobetSonucService = eczaneNobetSonucService;
            _nobetGrupService = nobetGrupService;
            _userEczaneService = userEczaneService;
            _nobetUstGrupService = nobetUstGrupService;
            _eczaneNobetGrupService = eczaneNobetGrupService;
            _eczaneService = eczaneService;
            _eczaneNobetOrtakService = eczaneNobetOrtakService;
            _userRoleService = userRoleService;
            _yetkilendirme = new Yetkilendirme(_userService, _userRoleService);

        }
        #endregion
        public List<EczaneNobetSonucListe2> Get()
        {
            return _eczaneNobetSonucService.GetSonuclar(3);
        }


        //istatistikler için
        [Route("eczane-nobet-sonuclar/{userId:int:min(1)}")]
        [HttpGet]
        public List<EczaneGrupNobetSonuc> Get(int userId)
        {
            //User user = _userService.GetById(userId);
            //user = _userService.GetById(userId);
            int eczaneId = _userEczaneService.GetListByUserId(userId).Select(s => s.EczaneId).FirstOrDefault();
            int eczaneNobetGrupId = _eczaneNobetGrupService.GetDetayByEczaneId(eczaneId).Id;
            int nobetGrupGorevTipId = _eczaneNobetGrupService.GetDetayByEczaneId(eczaneId).NobetGrupGorevTipId;
            NobetGrup nobetGrup = _nobetGrupService.GetById(nobetGrupGorevTipId);
            NobetUstGrup nobetUstGrup = _eczaneService.GetByEczaneNobetGrupId(eczaneNobetGrupId);
            return _eczaneNobetSonucService.GetEczaneGrupNobetSonuc(nobetGrup.BaslamaTarihi, DateTime.Now.AddMonths(12), nobetUstGrup.Id)
                //.OrderByDescending(o => o.Tarih)
                .ToList();
        }

        [Route("ping/{userId:int:min(1)}")]
        [HttpGet]
        public int Ping(int userId)
        {
            return userId;
        }
        //eczanenin nöbetleri için
        [Route("eczane-nobet-sonuclarim/{userId:int:min(1)}")]
        [HttpGet]
        public List<EczaneGrupNobetSonuc> GetNobetlerim(int userId)
        {
            User user = new User();
            user = _userService.GetById(userId);
            int eczaneId = _userEczaneService.GetListByUserId(userId).Select(s => s.EczaneId).FirstOrDefault();
            int eczaneNobetGrupId = _eczaneNobetGrupService.GetDetayByEczaneId(eczaneId).Id;
            NobetUstGrup nobetUstGrup = _eczaneService.GetByEczaneNobetGrupId(eczaneNobetGrupId);
            int nobetUstGrupId = _nobetUstGrupService.GetListByUser(user).FirstOrDefault().Id;
            int nobetGrupGorevTipId = _eczaneNobetGrupService.GetDetayByEczaneId(eczaneId).NobetGrupGorevTipId;
            NobetGrup nobetGrup = _nobetGrupService.GetById(nobetGrupGorevTipId);
            return _eczaneNobetSonucService.GetEczaneGrupNobetSonuc(nobetGrup.BaslamaTarihi, DateTime.Now.AddMonths(12), nobetUstGrupId)
                .Where(w => w.EczaneNobetGrupId == eczaneNobetGrupId)
                //.OrderByDescending(o => o.Tarih)
                .ToList();
        }
        //eczanenin nöbetleri için
        [Route("eczane-nobet-sonuclarim-mobil/{userId:int:min(1)}")]
        [HttpGet]
        public List<EczaneNobetSonucMobilUygulama> GetNobetlerimMobil(int userId)
        {
            //User user = new User();
            //user = _userService.GetById(userId);
            int eczaneId = _userEczaneService.GetListByUserId(userId).Select(s => s.EczaneId).FirstOrDefault();
            int eczaneNobetGrupId = _eczaneNobetGrupService.GetDetayByEczaneId(eczaneId).Id;
            NobetUstGrup nobetUstGrup = _eczaneService.GetByEczaneNobetGrupId(eczaneNobetGrupId);
            int nobetGrupGorevTipId = _eczaneNobetGrupService.GetDetayByEczaneId(eczaneId).NobetGrupGorevTipId;
            NobetGrup nobetGrup = _nobetGrupService.GetById(nobetGrupGorevTipId);
            return _eczaneNobetSonucService.GetSonuclarMobilUygulama(nobetGrup.BaslamaTarihi, DateTime.Now.AddMonths(12), nobetUstGrup.Id)
                .Where(w => w.EczaneNobetGrupId == eczaneNobetGrupId
                && w.YayimlandiMi == true)
                //.OrderByDescending(o => o.Tarih)
                .ToList();
        }

        //istatistikler için
        [Route("eczane-nobet-sonuclar-mobil/{userId:int:min(1)}")]
        [HttpGet]
        public List<EczaneNobetSonucMobilUygulama> GetMobil(int userId)
        {
            //User user = new User();
            //user = _userService.GetById(userId);
            int eczaneId = _userEczaneService.GetListByUserId(userId).Select(s => s.EczaneId).FirstOrDefault();
            int eczaneNobetGrupId = _eczaneNobetGrupService.GetDetayByEczaneId(eczaneId).Id;

            NobetUstGrup nobetUstGrup = _eczaneService.GetByEczaneNobetGrupId(eczaneNobetGrupId);
            int nobetGrupGorevTipId = _eczaneNobetGrupService.GetDetayByEczaneId(eczaneId).NobetGrupGorevTipId;
            NobetGrup nobetGrup = _nobetGrupService.GetById(nobetGrupGorevTipId);
            return _eczaneNobetSonucService.GetSonuclarMobilUygulama(nobetGrup.BaslamaTarihi,
                DateTime.Now.AddMonths(12), nobetUstGrup.Id)
                 .Where(w => w.YayimlandiMi == true)
                //.OrderByDescending(o=>o.Tarih)
                .ToList();
        }

        [Route("eczane-nobet-grup-id/{eczaneNobetSonucId:int:min(1)}")]
        [HttpGet]
        public int GetMobilEczaneNobetGrupId(int eczaneNobetSonucId)
        {

            return _eczaneNobetSonucService.GetById(eczaneNobetSonucId).EczaneNobetGrupId;
        }

        [Route("eczane-nobet-nobetlerim-tarihli-mobil/{userId:int:min(1)}")]
        [HttpPost]
        public List<EczaneGrupNobetSonuc> PostNobetlerTarihliMobil(int userId)
        {
            //User user = new User();
            //user = _userService.GetById(eczaneNobetSonucApi.UserId);
            int eczaneId = _userEczaneService.GetListByUserId(userId).Select(s => s.EczaneId).FirstOrDefault();
            int eczaneNobetGrupId = _eczaneNobetGrupService.GetDetayByEczaneId(eczaneId).Id;
            NobetUstGrup nobetUstGrup = _eczaneService.GetByEczaneNobetGrupId(eczaneNobetGrupId);
            int nobetGrupGorevTipId = _eczaneNobetGrupService.GetDetayByEczaneId(eczaneId).NobetGrupGorevTipId;
            NobetGrup nobetGrup = _nobetGrupService.GetById(nobetGrupGorevTipId);
            return _eczaneNobetSonucService.GetEczaneGrupNobetSonuc(nobetGrup.BaslamaTarihi, DateTime.Now.AddMonths(12), nobetUstGrup.Id)
                .Where(w => w.EczaneNobetGrupId == eczaneNobetGrupId)
                //----********---- yayinlsndiMi burada önceden kontrol edliyor!!!!!

                //&& w.YayimlandiMi == true)
                //.OrderByDescending(o => o.Tarih)
                .ToList();
        }



        [Route("eczane-nobet-nobetler-tarihli-istatistiklerim/{userId:int:min(1)}")]
        [HttpGet]
        public List<EczaneGrupNobetSonuc> GetNobetlerimIstatistiklerim(int userId)
        {
            //User user = new User();
            //user = _userService.GetById(userId);

            int eczaneId = _userEczaneService.GetListByUserId(userId).Select(s => s.EczaneId).FirstOrDefault();
            int eczaneNobetGrupId = _eczaneNobetGrupService.GetDetayByEczaneId(eczaneId).Id;
            NobetUstGrup nobetUstGrup = _eczaneService.GetByEczaneNobetGrupId(eczaneNobetGrupId);
            List<EczaneGrupNobetSonuc> list = new List<EczaneGrupNobetSonuc>();
            int nobetGrupId = _eczaneNobetGrupService.GetDetayByEczaneId(eczaneId).NobetGrupId;
            NobetGrup nobetGrup = _nobetGrupService.GetById(nobetGrupId);
            list = _eczaneNobetSonucService.GetEczaneGrupNobetSonuc(nobetGrup.BaslamaTarihi, DateTime.Now.AddMonths(12), nobetUstGrup.Id)
                .Where(w => w.EczaneNobetGrupId == eczaneNobetGrupId)
                //----********---- yayinlsndiMi burada önceden kontrol edliyor!!!!!
                // && w.YayimlandiMi == true)
                //.OrderByDescending(o => o.Tarih)
                .ToList();
            return list;
        }

        [Route("eczane-nobet-sonuclarim-ustgruplu/{userId:int:min(1)}")]
        [HttpGet]
        public List<EczaneGrupNobetSonuc> GetNobetlerimGunuTumUstGrup(int userId)
        {
            //User user = new User();
            //user = _userService.GetById(userId);
            int eczaneId = _userEczaneService.GetListByUserId(userId).Select(s => s.EczaneId).FirstOrDefault();
            int eczaneNobetGrupId = _eczaneNobetGrupService.GetDetayByEczaneId(eczaneId).Id;
            NobetUstGrup nobetUstGrup = _eczaneService.GetByEczaneNobetGrupId(eczaneNobetGrupId);
            List<DateTime> tarihListe = _eczaneNobetSonucService.GetEczaneGrupNobetSonuc(nobetUstGrup.Id)
                .Where(w => w.EczaneNobetGrupId == eczaneNobetGrupId).Select(s => s.Tarih).ToList();
            int nobetGrupGorevTipId = _eczaneNobetGrupService.GetDetayByEczaneId(eczaneId).NobetGrupGorevTipId;
            NobetGrup nobetGrup = _nobetGrupService.GetById(nobetGrupGorevTipId);
            List<EczaneGrupNobetSonuc> returnList = _eczaneNobetSonucService.GetEczaneGrupNobetSonuc(nobetGrup.BaslamaTarihi, DateTime.Now.AddMonths(12), nobetUstGrup.Id)
               .Where(w => tarihListe.Contains(w.Tarih))
                // .OrderByDescending(o => o.Tarih)
                .ToList();
            return returnList;
        }

        [Route("eczane-nobet-sonuclarim-ustgruplu-mobil/{userId:int:min(1)}")]
        [HttpGet]
        public List<EczaneNobetSonucMobilUygulama> GetNobetlerimGunuTumUstGrupMobil(int userId)
        {
            //User user = new User();
            //user = _userService.GetById(userId);
            int eczaneId = _userEczaneService.GetListByUserId(userId).Select(s => s.EczaneId).FirstOrDefault();
            int eczaneNobetGrupId = _eczaneNobetGrupService.GetDetayByEczaneId(eczaneId).Id;
            NobetUstGrup nobetUstGrup = _eczaneService.GetByEczaneNobetGrupId(eczaneNobetGrupId);
            int nobetGrupGorevTipId = _eczaneNobetGrupService.GetDetayByEczaneId(eczaneId).NobetGrupGorevTipId;
            NobetGrup nobetGrup = _nobetGrupService.GetById(nobetGrupGorevTipId);
            List<DateTime> tarihListe = new List<DateTime>();
            List<EczaneNobetSonucMobilUygulama> returnList = new List<EczaneNobetSonucMobilUygulama>();
            try
            {
                tarihListe = _eczaneNobetSonucService.GetSonuclarMobilUygulama(nobetGrup.BaslamaTarihi, DateTime.Now.AddMonths(12), nobetUstGrup.Id)
                    .Where(w => w.EczaneNobetGrupId == eczaneNobetGrupId).Select(s => s.Tarih).ToList();
            }
            catch { }
            try
            {
                if (tarihListe != null)
                    returnList = _eczaneNobetSonucService.GetSonuclarMobilUygulama(nobetGrup.BaslamaTarihi, DateTime.Now.AddMonths(12), nobetUstGrup.Id)
                        .Where(w => tarihListe.Contains(w.Tarih)
                        && w.YayimlandiMi == true)
                        // .OrderByDescending(o => o.Tarih)
                        .ToList();
                else
                    returnList = _eczaneNobetSonucService.GetSonuclarMobilUygulama(nobetGrup.BaslamaTarihi, DateTime.Now.AddMonths(12), nobetUstGrup.Id)
                            .Where(w => w.Tarih == DateTime.Now
                        && w.YayimlandiMi == true)
                        // .OrderByDescending(o => o.Tarih)
                        .ToList();
            }
            catch { }
            return returnList;
        }

        [Route("eczane-nobet-nobetler-tarihli")]
        [HttpPost]
        public HttpResponseMessage GetNobetlerTarihli([FromBody]EczaneNobetSonucApi eczaneNobetSonucApi)
        {
            try
            {
                //User user = new User();
                //user = _userService.GetById(eczaneNobetSonucApi.UserId);
                int eczaneId = _userEczaneService.GetListByUserId(eczaneNobetSonucApi.UserId).Select(s => s.EczaneId).FirstOrDefault();
                int eczaneNobetGrupId = _eczaneNobetGrupService.GetDetayByEczaneId(eczaneId).Id;
                NobetUstGrup nobetUstGrup = _eczaneService.GetByEczaneNobetGrupId(eczaneNobetGrupId);
                DateTime dt_baslangicTarihi = Convert.ToDateTime(eczaneNobetSonucApi.BaslangicTarihi.ToShortDateString());
                DateTime dt_bitisTarihi = Convert.ToDateTime(eczaneNobetSonucApi.BitisTarihi.ToShortDateString());
                List<EczaneGrupNobetSonuc> eczaneNobetSonucMobilUygulama = new List<EczaneGrupNobetSonuc>();
                eczaneNobetSonucMobilUygulama = _eczaneNobetSonucService.GetEczaneGrupNobetSonuc(dt_baslangicTarihi, dt_bitisTarihi, nobetUstGrup.Id)
                    //.Where(w => w.YayimlandiMi == true)//.OrderBy(o => o.NobetGrupId)
                    .ToList();
                return Request.CreateResponse(HttpStatusCode.OK, eczaneNobetSonucMobilUygulama);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message + e.InnerException.StackTrace);
            }
        }

        [Route("eczane-nobet-nobetler-tarihli-mobil")]
        [HttpPost]
        public HttpResponseMessage PostNobetlerTarihliMobil([FromBody]EczaneNobetSonucApi eczaneNobetSonucApi)
        {
            try
            {
                //User user = new User();
                //user = _userService.GetById(eczaneNobetSonucApi.UserId);
                int eczaneId = _userEczaneService.GetListByUserId(eczaneNobetSonucApi.UserId).Select(s => s.EczaneId).FirstOrDefault();
                int eczaneNobetGrupId = _eczaneNobetGrupService.GetDetayByEczaneId(eczaneId).Id;
                NobetUstGrup nobetUstGrup = _eczaneService.GetByEczaneNobetGrupId(eczaneNobetGrupId);
                DateTime dt_baslangicTarihi = Convert.ToDateTime(eczaneNobetSonucApi.BaslangicTarihi.ToShortDateString());
                DateTime dt_bitisTarihi = Convert.ToDateTime(eczaneNobetSonucApi.BitisTarihi.ToShortDateString());
                List<EczaneNobetSonucMobilUygulama> eczaneNobetSonucMobilUygulama = new List<EczaneNobetSonucMobilUygulama>();
                eczaneNobetSonucMobilUygulama = _eczaneNobetSonucService.GetSonuclarMobilUygulama(dt_baslangicTarihi, dt_bitisTarihi, nobetUstGrup.Id)
                    .Where(w => w.YayimlandiMi == true)//.OrderBy(o => o.NobetGrupId)
                    .ToList();
                return Request.CreateResponse(HttpStatusCode.OK, eczaneNobetSonucMobilUygulama);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message + e.InnerException.StackTrace);
            }
        }

    }
}
