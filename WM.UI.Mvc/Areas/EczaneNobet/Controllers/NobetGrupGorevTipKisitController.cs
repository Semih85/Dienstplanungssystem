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
    [Authorize]
    [HandleError]
    public class NobetGrupGorevTipKisitController : Controller
    {
        private WMUIMvcContext db = new WMUIMvcContext();
        private INobetGrupGorevTipKisitService _nobetGrupGorevTipKisitService;
        private INobetUstGrupService _nobetUstGrupService;
        private IUserService _userService;

        public NobetGrupGorevTipKisitController(INobetGrupGorevTipKisitService nobetGrupGorevTipKisitService,
            INobetUstGrupService nobetUstGrupService,
            IUserService userService)
        {
            _nobetGrupGorevTipKisitService = nobetGrupGorevTipKisitService;
            _nobetUstGrupService = nobetUstGrupService;
            _userService = userService;
        }

        // GET: EczaneNobet/NobetGrupGorevTipKisit
        public ActionResult Index()
        {
            var user = _userService.GetByUserName(User.Identity.Name);

            var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            var nobetUstGrup = nobetUstGruplar.FirstOrDefault();

            var nobetGrupGorevTipKisitlar = _nobetGrupGorevTipKisitService.GetDetaylar(nobetUstGrup.Id);

            return View(nobetGrupGorevTipKisitlar);
        }

        // GET: EczaneNobet/NobetGrupGorevTipKisit/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NobetGrupGorevTipKisit nobetGrupGorevTipKisit = db.NobetGrupGorevTipKisits.Find(id);
            if (nobetGrupGorevTipKisit == null)
            {
                return HttpNotFound();
            }
            return View(nobetGrupGorevTipKisit);
        }

        // GET: EczaneNobet/NobetGrupGorevTipKisit/Create
        public ActionResult Create()
        {
            ViewBag.NobetGrupGorevTipId = new SelectList(db.NobetGrupGorevTips, "Id", "Id");
            ViewBag.NobetUstGrupKisitId = new SelectList(db.NobetUstGrupKisits, "Id", "Aciklama");
            return View();
        }

        // POST: EczaneNobet/NobetGrupGorevTipKisit/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NobetUstGrupKisitId,NobetGrupGorevTipId,PasifMi,SagTarafDegeri,VarsayilanPasifMi,SagTarafDegeriVarsayilan,Aciklama")] NobetGrupGorevTipKisit nobetGrupGorevTipKisit)
        {
            if (ModelState.IsValid)
            {
                db.NobetGrupGorevTipKisits.Add(nobetGrupGorevTipKisit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.NobetGrupGorevTipId = new SelectList(db.NobetGrupGorevTips, "Id", "Id", nobetGrupGorevTipKisit.NobetGrupGorevTipId);
            ViewBag.NobetUstGrupKisitId = new SelectList(db.NobetUstGrupKisits, "Id", "Aciklama", nobetGrupGorevTipKisit.NobetUstGrupKisitId);
            return View(nobetGrupGorevTipKisit);
        }

        // GET: EczaneNobet/NobetGrupGorevTipKisit/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NobetGrupGorevTipKisit nobetGrupGorevTipKisit = db.NobetGrupGorevTipKisits.Find(id);
            if (nobetGrupGorevTipKisit == null)
            {
                return HttpNotFound();
            }
            ViewBag.NobetGrupGorevTipId = new SelectList(db.NobetGrupGorevTips, "Id", "Id", nobetGrupGorevTipKisit.NobetGrupGorevTipId);
            ViewBag.NobetUstGrupKisitId = new SelectList(db.NobetUstGrupKisits, "Id", "Aciklama", nobetGrupGorevTipKisit.NobetUstGrupKisitId);
            return View(nobetGrupGorevTipKisit);
        }

        // POST: EczaneNobet/NobetGrupGorevTipKisit/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NobetUstGrupKisitId,NobetGrupGorevTipId,PasifMi,SagTarafDegeri,VarsayilanPasifMi,SagTarafDegeriVarsayilan,Aciklama")] NobetGrupGorevTipKisit nobetGrupGorevTipKisit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nobetGrupGorevTipKisit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.NobetGrupGorevTipId = new SelectList(db.NobetGrupGorevTips, "Id", "Id", nobetGrupGorevTipKisit.NobetGrupGorevTipId);
            ViewBag.NobetUstGrupKisitId = new SelectList(db.NobetUstGrupKisits, "Id", "Aciklama", nobetGrupGorevTipKisit.NobetUstGrupKisitId);
            return View(nobetGrupGorevTipKisit);
        }

        // GET: EczaneNobet/NobetGrupGorevTipKisit/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NobetGrupGorevTipKisit nobetGrupGorevTipKisit = db.NobetGrupGorevTipKisits.Find(id);
            if (nobetGrupGorevTipKisit == null)
            {
                return HttpNotFound();
            }
            return View(nobetGrupGorevTipKisit);
        }

        // POST: EczaneNobet/NobetGrupGorevTipKisit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NobetGrupGorevTipKisit nobetGrupGorevTipKisit = db.NobetGrupGorevTipKisits.Find(id);
            db.NobetGrupGorevTipKisits.Remove(nobetGrupGorevTipKisit);
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
