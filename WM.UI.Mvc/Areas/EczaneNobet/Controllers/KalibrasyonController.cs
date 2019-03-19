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
    public class KalibrasyonController : Controller
    {
        private WMUIMvcContext db = new WMUIMvcContext();

        private IKalibrasyonService _kalibrasyonService;
        private IKalibrasyonTipService _kalibrasyonTipService;
        private IUserService _userService;
        private INobetUstGrupService _nobetUstGrupService;
        private IEczaneNobetGrupService _eczaneNobetGrupService;

        public KalibrasyonController(IKalibrasyonService kalibrasyonService,
            IKalibrasyonTipService kalibrasyonTipService,
            IUserService userService,
            INobetUstGrupService nobetUstGrupService,
            IEczaneNobetGrupService eczaneNobetGrupService)
        {
            _kalibrasyonService = kalibrasyonService;
            _kalibrasyonTipService = kalibrasyonTipService;
            _userService = userService;
            _nobetUstGrupService = nobetUstGrupService;
            _eczaneNobetGrupService = eczaneNobetGrupService;
        }

        // GET: EczaneNobet/Kalibrasyon
        public ActionResult Index()
        {
            var kalibrasyons = _kalibrasyonService.GetDetaylar();
            return View(kalibrasyons.ToList());
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
        public ActionResult Create()
        {
            ViewBag.EczaneNobetGrupId = new SelectList(db.EczaneNobetGrups, "Id", "Aciklama");
            ViewBag.KalibrasyonTipId = new SelectList(db.KalibrasyonTips, "Id", "Adi");
            ViewBag.NobetUstGrupGunGrupId = new SelectList(db.NobetUstGrupGunGrups, "Id", "Aciklama");
            return View();
        }

        // POST: EczaneNobet/Kalibrasyon/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EczaneNobetGrupId,KalibrasyonTipId,NobetUstGrupGunGrupId,Deger,Aciklama")] Kalibrasyon kalibrasyon)
        {
            if (ModelState.IsValid)
            {
                db.Kalibrasyons.Add(kalibrasyon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EczaneNobetGrupId = new SelectList(db.EczaneNobetGrups, "Id", "Aciklama", kalibrasyon.EczaneNobetGrupId);
            ViewBag.KalibrasyonTipId = new SelectList(db.KalibrasyonTips, "Id", "Adi", kalibrasyon.KalibrasyonTipId);
            ViewBag.NobetUstGrupGunGrupId = new SelectList(db.NobetUstGrupGunGrups, "Id", "Aciklama", kalibrasyon.NobetUstGrupGunGrupId);
            return View(kalibrasyon);
        }

        // GET: EczaneNobet/Kalibrasyon/Edit/5
        public ActionResult Edit(int id)
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
            ViewBag.EczaneNobetGrupId = new SelectList(db.EczaneNobetGrups, "Id", "Aciklama", kalibrasyon.EczaneNobetGrupId);
            ViewBag.KalibrasyonTipId = new SelectList(db.KalibrasyonTips, "Id", "Adi", kalibrasyon.KalibrasyonTipId);
            ViewBag.NobetUstGrupGunGrupId = new SelectList(db.NobetUstGrupGunGrups, "Id", "Aciklama", kalibrasyon.NobetUstGrupGunGrupId);
            return View(kalibrasyon);
        }

        // POST: EczaneNobet/Kalibrasyon/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EczaneNobetGrupId,KalibrasyonTipId,NobetUstGrupGunGrupId,Deger,Aciklama")] Kalibrasyon kalibrasyon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kalibrasyon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EczaneNobetGrupId = new SelectList(db.EczaneNobetGrups, "Id", "Aciklama", kalibrasyon.EczaneNobetGrupId);
            ViewBag.KalibrasyonTipId = new SelectList(db.KalibrasyonTips, "Id", "Adi", kalibrasyon.KalibrasyonTipId);
            ViewBag.NobetUstGrupGunGrupId = new SelectList(db.NobetUstGrupGunGrups, "Id", "Aciklama", kalibrasyon.NobetUstGrupGunGrupId);
            return View(kalibrasyon);
        }

        // GET: EczaneNobet/Kalibrasyon/Delete/5
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
        public ActionResult DeleteConfirmed(int id)
        {
            //var kalibrasyon = _kalibrasyonService.GetDetayById(id);
            _kalibrasyonService.Delete(id);
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
