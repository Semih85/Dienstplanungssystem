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
    public class AyniGunNobetTakipGrupAltGrupController : Controller
    {
        private WMUIMvcContext db = new WMUIMvcContext();
        private IAyniGunNobetTakipGrupAltGrupService _ayniGunNobetTakipGrupAltGrupService;
        private IUserService _userService;
        private INobetUstGrupService _nobetUstGrupService;
        private INobetUstGrupSessionService _nobetUstGrupSessionService;

        public AyniGunNobetTakipGrupAltGrupController(
            IAyniGunNobetTakipGrupAltGrupService ayniGunNobetTakipGrupAltGrupService,
            IUserService userService,
            INobetUstGrupService nobetUstGrupService,
            INobetUstGrupSessionService nobetUstGrupSessionService)
        {
            _ayniGunNobetTakipGrupAltGrupService = ayniGunNobetTakipGrupAltGrupService;
            _userService = userService;
            _nobetUstGrupService = nobetUstGrupService;
            _nobetUstGrupSessionService = nobetUstGrupSessionService;
        }

        // GET: EczaneNobet/AyniGunNobetTakipGrupAltGrup
        public ActionResult Index()
        {
            //var user = _userService.GetByUserName(User.Identity.Name);
            //var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            var nobetUstGrup = _nobetUstGrupSessionService.GetNobetUstGrup();

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
        public ActionResult Create()
        {
            ViewBag.NobetAltGrupId = new SelectList(db.NobetAltGrups, "Id", "Adi");
            ViewBag.NobetGrupGorevTipId = new SelectList(db.NobetGrupGorevTips, "Id", "Id");
            return View();
        }

        // POST: EczaneNobet/AyniGunNobetTakipGrupAltGrup/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NobetGrupGorevTipId,NobetAltGrupId,BaslamaTarihi,BitisTarihi")] AyniGunNobetTakipGrupAltGrup ayniGunNobetTakipGrupAltGrup)
        {
            if (ModelState.IsValid)
            {
                _ayniGunNobetTakipGrupAltGrupService.Insert(ayniGunNobetTakipGrupAltGrup);
                return RedirectToAction("Index");
            }

            ViewBag.NobetAltGrupId = new SelectList(db.NobetAltGrups, "Id", "Adi", ayniGunNobetTakipGrupAltGrup.NobetAltGrupId);
            ViewBag.NobetGrupGorevTipId = new SelectList(db.NobetGrupGorevTips, "Id", "Id", ayniGunNobetTakipGrupAltGrup.NobetGrupGorevTipId);

            return View(ayniGunNobetTakipGrupAltGrup);
        }

        // GET: EczaneNobet/AyniGunNobetTakipGrupAltGrup/Edit/5
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
            ViewBag.NobetAltGrupId = new SelectList(db.NobetAltGrups, "Id", "Adi", ayniGunNobetTakipGrupAltGrup.NobetAltGrupId);
            ViewBag.NobetGrupGorevTipId = new SelectList(db.NobetGrupGorevTips, "Id", "Id", ayniGunNobetTakipGrupAltGrup.NobetGrupGorevTipId);
            return View(ayniGunNobetTakipGrupAltGrup);
        }

        // POST: EczaneNobet/AyniGunNobetTakipGrupAltGrup/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NobetGrupGorevTipId,NobetAltGrupId,BaslamaTarihi,BitisTarihi")] AyniGunNobetTakipGrupAltGrup ayniGunNobetTakipGrupAltGrup)
        {
            if (ModelState.IsValid)
            {
                _ayniGunNobetTakipGrupAltGrupService.Update(ayniGunNobetTakipGrupAltGrup);
                return RedirectToAction("Index");
            }
            ViewBag.NobetAltGrupId = new SelectList(db.NobetAltGrups, "Id", "Adi", ayniGunNobetTakipGrupAltGrup.NobetAltGrupId);
            ViewBag.NobetGrupGorevTipId = new SelectList(db.NobetGrupGorevTips, "Id", "Id", ayniGunNobetTakipGrupAltGrup.NobetGrupGorevTipId);
            return View(ayniGunNobetTakipGrupAltGrup);
        }

        // GET: EczaneNobet/AyniGunNobetTakipGrupAltGrup/Delete/5
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var ayniGunNobetTakipGrupAltGrup = _ayniGunNobetTakipGrupAltGrupService.GetDetayById(id);
            _ayniGunNobetTakipGrupAltGrupService.Delete(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
