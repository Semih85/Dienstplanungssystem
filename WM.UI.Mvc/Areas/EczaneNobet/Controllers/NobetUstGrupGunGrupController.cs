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

namespace WM.UI.Mvc.Areas.EczaneNobet.Controllers
{
    public class NobetUstGrupGunGrupController : Controller
    {
        private WMUIMvcContext db = new WMUIMvcContext();
        private INobetUstGrupGunGrupService _nobetUstGrupGunGrupService;
        private INobetUstGrupService _nobetUstGrupService;
        private IUserService _userService;

        public NobetUstGrupGunGrupController(INobetUstGrupGunGrupService nobetUstGrupGunGrupService,
            IUserService userService,
            INobetUstGrupService nobetUstGrupService)
        {
            _nobetUstGrupGunGrupService = nobetUstGrupGunGrupService;
            _userService = userService;
            _nobetUstGrupService = nobetUstGrupService;
        }

        // GET: EczaneNobet/NobetUstGrupGunGrup
        public ActionResult Index()
        {
            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGrup = _nobetUstGrupService.GetListByUser(user).FirstOrDefault();

            var nobetUstGrupGunGruplar = _nobetUstGrupGunGrupService.GetDetaylar(nobetUstGrup.Id);
            return View(nobetUstGrupGunGruplar);
        }

        // GET: EczaneNobet/NobetUstGrupGunGrup/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NobetUstGrupGunGrup nobetUstGrupGunGrup = db.NobetUstGrupGunGrups.Find(id);
            if (nobetUstGrupGunGrup == null)
            {
                return HttpNotFound();
            }
            return View(nobetUstGrupGunGrup);
        }

        // GET: EczaneNobet/NobetUstGrupGunGrup/Create
        public ActionResult Create()
        {
            ViewBag.GunGrupId = new SelectList(db.GunGrups, "Id", "Adi");
            ViewBag.NobetUstGrupId = new SelectList(db.NobetUstGrups, "Id", "Adi");
            return View();
        }

        // POST: EczaneNobet/NobetUstGrupGunGrup/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,GunGrupId,NobetUstGrupId,Aciklama")] NobetUstGrupGunGrup nobetUstGrupGunGrup)
        {
            if (ModelState.IsValid)
            {
                db.NobetUstGrupGunGrups.Add(nobetUstGrupGunGrup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GunGrupId = new SelectList(db.GunGrups, "Id", "Adi", nobetUstGrupGunGrup.GunGrupId);
            ViewBag.NobetUstGrupId = new SelectList(db.NobetUstGrups, "Id", "Adi", nobetUstGrupGunGrup.NobetUstGrupId);
            return View(nobetUstGrupGunGrup);
        }

        // GET: EczaneNobet/NobetUstGrupGunGrup/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NobetUstGrupGunGrup nobetUstGrupGunGrup = db.NobetUstGrupGunGrups.Find(id);
            if (nobetUstGrupGunGrup == null)
            {
                return HttpNotFound();
            }
            ViewBag.GunGrupId = new SelectList(db.GunGrups, "Id", "Adi", nobetUstGrupGunGrup.GunGrupId);
            ViewBag.NobetUstGrupId = new SelectList(db.NobetUstGrups, "Id", "Adi", nobetUstGrupGunGrup.NobetUstGrupId);
            return View(nobetUstGrupGunGrup);
        }

        // POST: EczaneNobet/NobetUstGrupGunGrup/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,GunGrupId,NobetUstGrupId,Aciklama")] NobetUstGrupGunGrup nobetUstGrupGunGrup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nobetUstGrupGunGrup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GunGrupId = new SelectList(db.GunGrups, "Id", "Adi", nobetUstGrupGunGrup.GunGrupId);
            ViewBag.NobetUstGrupId = new SelectList(db.NobetUstGrups, "Id", "Adi", nobetUstGrupGunGrup.NobetUstGrupId);
            return View(nobetUstGrupGunGrup);
        }

        // GET: EczaneNobet/NobetUstGrupGunGrup/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NobetUstGrupGunGrup nobetUstGrupGunGrup = db.NobetUstGrupGunGrups.Find(id);
            if (nobetUstGrupGunGrup == null)
            {
                return HttpNotFound();
            }
            return View(nobetUstGrupGunGrup);
        }

        // POST: EczaneNobet/NobetUstGrupGunGrup/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NobetUstGrupGunGrup nobetUstGrupGunGrup = db.NobetUstGrupGunGrups.Find(id);
            db.NobetUstGrupGunGrups.Remove(nobetUstGrupGunGrup);
            db.SaveChanges();
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
