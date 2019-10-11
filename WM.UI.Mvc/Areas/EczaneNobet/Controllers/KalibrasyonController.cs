using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WM.Northwind.Business.Abstract.Authorization;
using WM.Northwind.Business.Abstract.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;
using WM.UI.Mvc.Models;
using WM.UI.Mvc.Services;

namespace WM.UI.Mvc.Areas.EczaneNobet.Controllers
{
    [Authorize]
    [HandleError]
    public class KalibrasyonController : Controller
    {
        #region ctor

        private IKalibrasyonService _kalibrasyonService;
        private IKalibrasyonTipService _kalibrasyonTipService;
        private IUserService _userService;
        private INobetUstGrupService _nobetUstGrupService;
        private IEczaneNobetGrupService _eczaneNobetGrupService;
        private INobetGrupGorevTipService _nobetGrupGorevTipService;
        private INobetUstGrupGunGrupService _nobetUstGrupGunGrupService;
        private INobetUstGrupSessionService _nobetUstGrupSessionService;

        public KalibrasyonController(IKalibrasyonService kalibrasyonService,
            IKalibrasyonTipService kalibrasyonTipService,
            IUserService userService,
            INobetUstGrupService nobetUstGrupService,
            IEczaneNobetGrupService eczaneNobetGrupService,
            INobetGrupGorevTipService nobetGrupGorevTipService,
            INobetUstGrupGunGrupService nobetUstGrupGunGrupService,
            INobetUstGrupSessionService nobetUstGrupSessionService)
        {
            _kalibrasyonService = kalibrasyonService;
            _kalibrasyonTipService = kalibrasyonTipService;
            _userService = userService;
            _nobetUstGrupService = nobetUstGrupService;
            _eczaneNobetGrupService = eczaneNobetGrupService;
            _nobetGrupGorevTipService = nobetGrupGorevTipService;
            _nobetUstGrupGunGrupService = nobetUstGrupGunGrupService;
            _nobetUstGrupSessionService = nobetUstGrupSessionService;
        }

        #endregion

        // GET: EczaneNobet/Kalibrasyon
        [Authorize(Roles = "Admin,Oda,Üst Grup")]
        public ActionResult Index()
        {
            //var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var kalibrasyonlar = _kalibrasyonService.GetDetaylar(nobetUstGrup.Id);

            return View(kalibrasyonlar);
        }

        // GET: EczaneNobet/Kalibrasyon/Details/5
        public ActionResult Details(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var kalibrasyon = _kalibrasyonService.GetDetayById(id);
            if (kalibrasyon == null)
            {
                return HttpNotFound();
            }
            return View(kalibrasyon);
        }

        // GET: EczaneNobet/Kalibrasyon/Create
        [Authorize(Roles = "Admin,Oda,Üst Grup")]
        public ActionResult Create()
        {
            //var user = _userService.GetByUserName(User.Identity.Name);
            //var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user).Select(s => s.Id).ToList();
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var eczaneNobetGrupList = _eczaneNobetGrupService.GetDetaylar(nobetUstGrup.Id);
            var eczaneNobetGruplar = _eczaneNobetGrupService.GetMyDrop(eczaneNobetGrupList);
            
            var kalibrasyonTipList = _kalibrasyonTipService.GetDetaylar(nobetUstGrup.Id);
            var kalibrasyonTipler = _kalibrasyonTipService.GetMyDrop(kalibrasyonTipList);

            var nobetUstGrupGunGruplar = _nobetUstGrupGunGrupService.GetDetaylar(nobetUstGrup.Id);

            var nobetUstGrupGunGrupMydrop = _nobetUstGrupGunGrupService.GetMyDrop(nobetUstGrupGunGruplar);

            ViewBag.EczaneNobetGrupId = new SelectList(eczaneNobetGruplar, "Id", "Value");
            ViewBag.KalibrasyonTipId = new SelectList(kalibrasyonTipler, "Id", "Value");
            ViewBag.NobetUstGrupGunGrupId = new SelectList(nobetUstGrupGunGrupMydrop, "Id", "Value");

            return View();
        }

        // POST: EczaneNobet/Kalibrasyon/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Oda,Üst Grup")]
        public ActionResult Create([Bind(Include = "Id,EczaneNobetGrupId,KalibrasyonTipId,NobetUstGrupGunGrupId,Deger,Aciklama")] Kalibrasyon kalibrasyon)
        {
            if (ModelState.IsValid)
            {
                _kalibrasyonService.Insert(kalibrasyon);
                return RedirectToAction("Index");
            }

            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var eczaneNobetGrupList = _eczaneNobetGrupService.GetDetaylar(nobetUstGrup.Id);
            var eczaneNobetGruplar = _eczaneNobetGrupService.GetMyDrop(eczaneNobetGrupList);

            var kalibrasyonTipList = _kalibrasyonTipService.GetDetaylar(nobetUstGrup.Id);
            var kalibrasyonTipler = _kalibrasyonTipService.GetMyDrop(kalibrasyonTipList);

            var nobetUstGrupGunGruplar = _nobetUstGrupGunGrupService.GetDetaylar(nobetUstGrup.Id);

            var nobetUstGrupGunGrupMydrop = _nobetUstGrupGunGrupService.GetMyDrop(nobetUstGrupGunGruplar);

            ViewBag.EczaneNobetGrupId = new SelectList(eczaneNobetGruplar, "Id", "Value", kalibrasyon.EczaneNobetGrupId);
            ViewBag.KalibrasyonTipId = new SelectList(kalibrasyonTipler, "Id", "Value", kalibrasyon.KalibrasyonTipId);
            ViewBag.NobetUstGrupGunGrupId = new SelectList(nobetUstGrupGunGrupMydrop, "Id", "Value", kalibrasyon.NobetUstGrupGunGrupId);
            return View(kalibrasyon);
        }

        // GET: EczaneNobet/Kalibrasyon/Edit/5
        [Authorize(Roles = "Admin,Oda,Üst Grup")]
        public ActionResult Edit(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var kalibrasyon = _kalibrasyonService.GetById(id);
            if (kalibrasyon == null)
            {
                return HttpNotFound();
            }
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var eczaneNobetGrupList = _eczaneNobetGrupService.GetDetaylar(nobetUstGrup.Id);
            var eczaneNobetGruplar = _eczaneNobetGrupService.GetMyDrop(eczaneNobetGrupList);

            var kalibrasyonTipList = _kalibrasyonTipService.GetDetaylar(nobetUstGrup.Id);
            var kalibrasyonTipler = _kalibrasyonTipService.GetMyDrop(kalibrasyonTipList);

            var nobetUstGrupGunGruplar = _nobetUstGrupGunGrupService.GetDetaylar(nobetUstGrup.Id);

            var nobetUstGrupGunGrupMydrop = _nobetUstGrupGunGrupService.GetMyDrop(nobetUstGrupGunGruplar);

            ViewBag.EczaneNobetGrupId = new SelectList(eczaneNobetGruplar, "Id", "Value", kalibrasyon.EczaneNobetGrupId);
            ViewBag.KalibrasyonTipId = new SelectList(kalibrasyonTipler, "Id", "Value", kalibrasyon.KalibrasyonTipId);
            ViewBag.NobetUstGrupGunGrupId = new SelectList(nobetUstGrupGunGrupMydrop, "Id", "Value", kalibrasyon.NobetUstGrupGunGrupId);
            return View(kalibrasyon);
        }

        // POST: EczaneNobet/Kalibrasyon/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Oda,Üst Grup")]
        public ActionResult Edit([Bind(Include = "Id,EczaneNobetGrupId,KalibrasyonTipId,NobetUstGrupGunGrupId,Deger,Aciklama")] Kalibrasyon kalibrasyon)
        {
            if (ModelState.IsValid)
            {
                _kalibrasyonService.Update(kalibrasyon);
                return RedirectToAction("Index");
            }
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var eczaneNobetGrupList = _eczaneNobetGrupService.GetDetaylar(nobetUstGrup.Id);
            var eczaneNobetGruplar = _eczaneNobetGrupService.GetMyDrop(eczaneNobetGrupList);

            var kalibrasyonTipList = _kalibrasyonTipService.GetDetaylar(nobetUstGrup.Id);
            var kalibrasyonTipler = _kalibrasyonTipService.GetMyDrop(kalibrasyonTipList);

            var nobetUstGrupGunGruplar = _nobetUstGrupGunGrupService.GetDetaylar(nobetUstGrup.Id);

            var nobetUstGrupGunGrupMydrop = _nobetUstGrupGunGrupService.GetMyDrop(nobetUstGrupGunGruplar);

            ViewBag.EczaneNobetGrupId = new SelectList(eczaneNobetGruplar, "Id", "Value", kalibrasyon.EczaneNobetGrupId);
            ViewBag.KalibrasyonTipId = new SelectList(kalibrasyonTipler, "Id", "Value", kalibrasyon.KalibrasyonTipId);
            ViewBag.NobetUstGrupGunGrupId = new SelectList(nobetUstGrupGunGrupMydrop, "Id", "Value", kalibrasyon.NobetUstGrupGunGrupId);
            return View(kalibrasyon);
        }

        // GET: EczaneNobet/Kalibrasyon/Delete/5
        [Authorize(Roles = "Admin,Oda,Üst Grup")]
        public ActionResult Delete(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var kalibrasyon = _kalibrasyonService.GetDetayById(id);
            if (kalibrasyon == null)
            {
                return HttpNotFound();
            }
            return View(kalibrasyon);
        }

        // POST: EczaneNobet/Kalibrasyon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Oda,Üst Grup")]
        public ActionResult DeleteConfirmed(int id)
        {
            //var kalibrasyon = _kalibrasyonService.GetDetayById(id);
            _kalibrasyonService.Delete(id);
            return RedirectToAction("Index");
        }

    }
}
