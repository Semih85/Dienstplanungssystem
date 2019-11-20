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
    [Authorize(Roles = "Admin,Oda,Üst Grup")]
    [HandleError]
    public class AyniGunNobetTakipGrupAltGrupController : Controller
    {
        private IAyniGunNobetTakipGrupAltGrupService _ayniGunNobetTakipGrupAltGrupService;
        private IUserService _userService;
        private INobetUstGrupService _nobetUstGrupService;
        private INobetGrupGorevTipService _nobetGrupGorevTipService;
        private INobetUstGrupSessionService _nobetUstGrupSessionService;
        private INobetAltGrupService _nobetAltGrup;

        public AyniGunNobetTakipGrupAltGrupController(
            IAyniGunNobetTakipGrupAltGrupService ayniGunNobetTakipGrupAltGrupService,
            IUserService userService,
            INobetUstGrupService nobetUstGrupService,
            INobetGrupGorevTipService nobetGrupGorevTipService,
            INobetAltGrupService nobetAltGrup,
            INobetUstGrupSessionService nobetUstGrupSessionService)
        {
            _ayniGunNobetTakipGrupAltGrupService = ayniGunNobetTakipGrupAltGrupService;
            _userService = userService;
            _nobetUstGrupService = nobetUstGrupService;
            _nobetUstGrupSessionService = nobetUstGrupSessionService;
            _nobetGrupGorevTipService = nobetGrupGorevTipService;
            _nobetAltGrup = nobetAltGrup;
        }

        // GET: EczaneNobet/AyniGunNobetTakipGrupAltGrup
        public ActionResult Index()
        {
            //var user = _userService.GetByUserName(User.Identity.Name);
            //var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var ayniGunNobetTakipGrupAltGruplar = _ayniGunNobetTakipGrupAltGrupService.GetDetaylar(nobetUstGrup.Id);

            return View(ayniGunNobetTakipGrupAltGruplar);
        }

        // GET: EczaneNobet/AyniGunNobetTakipGrupAltGrup/Details/5
        public ActionResult Details(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ayniGunNobetTakipGrupAltGrup = _ayniGunNobetTakipGrupAltGrupService.GetDetayById(id);
            if (ayniGunNobetTakipGrupAltGrup == null)
            {
                return HttpNotFound();
            }
            return View(ayniGunNobetTakipGrupAltGrup);
        }

        // GET: EczaneNobet/AyniGunNobetTakipGrupAltGrup/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrup.Id);

            var nobetAltGruplar = _nobetAltGrup.GetDetaylar(nobetUstGrup.Id);

            ViewBag.NobetAltGrupId = new SelectList(nobetAltGruplar, "Id", "NobetAltGrupTanim");
            ViewBag.NobetGrupGorevTipId = new SelectList(nobetGrupGorevTipler, "Id", "NobetGrupGorevTipAdi");

            return View();
        }

        // POST: EczaneNobet/AyniGunNobetTakipGrupAltGrup/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NobetGrupGorevTipId,NobetAltGrupId,BaslamaTarihi,BitisTarihi")] AyniGunNobetTakipGrupAltGrup ayniGunNobetTakipGrupAltGrup)
        {
            if (ModelState.IsValid)
            {
                _ayniGunNobetTakipGrupAltGrupService.Insert(ayniGunNobetTakipGrupAltGrup);
                return RedirectToAction("Index");
            }

            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrup.Id);

            var nobetAltGruplar = _nobetAltGrup.GetDetaylar(nobetUstGrup.Id);

            ViewBag.NobetAltGrupId = new SelectList(nobetAltGruplar, "Id", "NobetAltGrupTanim");
            ViewBag.NobetGrupGorevTipId = new SelectList(nobetGrupGorevTipler, "Id", "NobetGrupGorevTipAdi");

            return View(ayniGunNobetTakipGrupAltGrup);
        }

        // GET: EczaneNobet/AyniGunNobetTakipGrupAltGrup/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ayniGunNobetTakipGrupAltGrup = _ayniGunNobetTakipGrupAltGrupService.GetDetayById(id);
            if (ayniGunNobetTakipGrupAltGrup == null)
            {
                return HttpNotFound();
            }
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrup.Id);

            var nobetAltGruplar = _nobetAltGrup.GetDetaylar(nobetUstGrup.Id);

            ViewBag.NobetAltGrupId = new SelectList(nobetAltGruplar, "Id", "NobetAltGrupTanim", ayniGunNobetTakipGrupAltGrup.NobetAltGrupId);
            ViewBag.NobetGrupGorevTipId = new SelectList(nobetGrupGorevTipler, "Id", "NobetGrupGorevTipAdi", ayniGunNobetTakipGrupAltGrup.NobetGrupGorevTipId);

            return View(ayniGunNobetTakipGrupAltGrup);
        }

        // POST: EczaneNobet/AyniGunNobetTakipGrupAltGrup/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NobetGrupGorevTipId,NobetAltGrupId,BaslamaTarihi,BitisTarihi")] AyniGunNobetTakipGrupAltGrup ayniGunNobetTakipGrupAltGrup)
        {
            if (ModelState.IsValid)
            {
                _ayniGunNobetTakipGrupAltGrupService.Update(ayniGunNobetTakipGrupAltGrup);
                return RedirectToAction("Index");
            }
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrup.Id);

            var nobetAltGruplar = _nobetAltGrup.GetDetaylar(nobetUstGrup.Id);

            ViewBag.NobetAltGrupId = new SelectList(nobetAltGruplar, "Id", "NobetAltGrupTanim", ayniGunNobetTakipGrupAltGrup.NobetAltGrupId);
            ViewBag.NobetGrupGorevTipId = new SelectList(nobetGrupGorevTipler, "Id", "NobetGrupGorevTipAdi", ayniGunNobetTakipGrupAltGrup.NobetGrupGorevTipId);

            return View(ayniGunNobetTakipGrupAltGrup);
        }

        // GET: EczaneNobet/AyniGunNobetTakipGrupAltGrup/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ayniGunNobetTakipGrupAltGrup = _ayniGunNobetTakipGrupAltGrupService.GetDetayById(id);
            if (ayniGunNobetTakipGrupAltGrup == null)
            {
                return HttpNotFound();
            }
            return View(ayniGunNobetTakipGrupAltGrup);
        }

        // POST: EczaneNobet/AyniGunNobetTakipGrupAltGrup/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var ayniGunNobetTakipGrupAltGrup = _ayniGunNobetTakipGrupAltGrupService.GetDetayById(id);
            _ayniGunNobetTakipGrupAltGrupService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
