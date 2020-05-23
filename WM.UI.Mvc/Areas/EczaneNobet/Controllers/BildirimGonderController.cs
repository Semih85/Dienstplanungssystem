using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WM.Northwind.Business.Abstract.Authorization;
using WM.Northwind.Business.Abstract.EczaneNobet;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.Authorization;
using WM.Northwind.Entities.Concrete.EczaneNobet;
using WM.UI.Mvc.Areas.EczaneNobet.Filters;
using WM.UI.Mvc.Areas.EczaneNobet.Models;
using WM.UI.Mvc.Models;
using WM.UI.Mvc.Services;

namespace WM.UI.Mvc.Areas.EczaneNobet.Controllers
{
    [Authorize]
    [HandleError]
    public class BildirimGonderController : Controller
    {
        #region ctor
        private IUserService _userService;
        private IUserEczaneService _userEczaneService;
        private IUserNobetUstGrupService _userNobetUstGrupService;
        private INobetUstGrupSessionService _nobetUstGrupSessionService;

        public BildirimGonderController(IUserService userService,
                                            IUserEczaneService userEczaneService,
                                            IUserNobetUstGrupService userNobetUstGrupService,
                                            INobetUstGrupSessionService nobetUstGrupSessionService)
        {

            _userService = userService;
            _userEczaneService = userEczaneService;
            _userNobetUstGrupService = userNobetUstGrupService;
            _nobetUstGrupSessionService = nobetUstGrupSessionService;
        }
        #endregion

        // GET: EczaneNobet/EczaneNobetMazeret
        [Authorize(Roles = "Admin,Oda,Üst Grup")]
        public ActionResult Index()
        {

            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var _userNobetUstGrupDetaylar = _userNobetUstGrupService.GetDetaylar(nobetUstGrup.Id);

            ViewBag.UserId = new SelectList(_userNobetUstGrupDetaylar.Select(s => new { s.UserId, s.KullaniciAdi }), "UserId", "KullaniciAdi");

            return View();
        }


        // POST: EczaneNobet/EczaneNobetMazeret/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "Baslik,Metin,UserId")] BildirimModel bildirimModel)
        {
            if (bildirimModel.UserId != null)
            {
                foreach (var item in bildirimModel.UserId)
                {
                    if (item != null)
                    {
                        User User = _userService.GetById(item);
                        PushNotification pushNotification = new PushNotification(bildirimModel.Metin,
                      bildirimModel.Baslik,
                      User.CihazId);
                    }
                }
                TempData["BildirimGonderilenKullanici"] = "Secilen kullanıcılara bildirim gönderilmiştir.";

            }
            else
            {
                TempData["BildirimGonderilenKullanici"] = "Kullanıcı seçiniz.";

            }
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var _userNobetUstGrupDetaylar = _userNobetUstGrupService.GetDetaylar(nobetUstGrup.Id);

            ViewBag.UserId = new SelectList(_userNobetUstGrupDetaylar.Select(s => new { s.UserId, s.KullaniciAdi }), "UserId", "KullaniciAdi");


            return View();
        }
    }
}
