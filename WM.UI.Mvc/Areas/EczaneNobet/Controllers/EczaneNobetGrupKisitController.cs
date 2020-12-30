using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WM.Northwind.Business.Abstract.EczaneNobet;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;
using WM.UI.Mvc.Models;
using WM.UI.Mvc.Services;

namespace WM.UI.Mvc.Areas.EczaneNobet.Controllers
{
    public class EczaneNobetGrupKisitController : Controller
    {
        private WMUIMvcContext db = new WMUIMvcContext();

        private IEczaneNobetGrupKisitService _eczaneNobetGrupKisitService;
        private IEczaneNobetGrupService _eczaneNobetGrupService;
        private IKisitService _kisitService;
        private INobetUstGrupKisitService _nobetUstGrupKisitService;
        private INobetUstGrupSessionService _nobetUstGrupSessionService;

        public EczaneNobetGrupKisitController(IEczaneNobetGrupKisitService eczaneNobetGrupKisitService,
            IEczaneNobetGrupService eczaneNobetGrupService,
            IKisitService kisitService,
            INobetUstGrupKisitService nobetUstGrupKisitService,
            INobetUstGrupSessionService nobetUstGrupSessionService)
        {
            _eczaneNobetGrupKisitService = eczaneNobetGrupKisitService;
            _eczaneNobetGrupService = eczaneNobetGrupService;
            _kisitService = kisitService;
            _nobetUstGrupKisitService = nobetUstGrupKisitService;
            _nobetUstGrupSessionService = nobetUstGrupSessionService;
        }

        // GET: EczaneNobet/EczaneNobetGrupKisit
        public ActionResult Index()
        {
            var ustGrupSession = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var eczaneNobetGrupKisits = _eczaneNobetGrupKisitService.GetDetaylar(ustGrupSession.Id);
            return View(eczaneNobetGrupKisits);
        }

        // GET: EczaneNobet/EczaneNobetGrupKisit/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EczaneNobetGrupKisit eczaneNobetGrupKisit = db.EczaneNobetGrupKisits.Find(id);
            if (eczaneNobetGrupKisit == null)
            {
                return HttpNotFound();
            }
            return View(eczaneNobetGrupKisit);
        }

        // GET: EczaneNobet/EczaneNobetGrupKisit/Create
        public ActionResult Create()
        {
            var ustGrupSession = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            ViewBag.EczaneNobetGrupId = new SelectList(_eczaneNobetGrupService.GetDetaylar(ustGrupSession.Id), "Id", "EczaneGorevTipAdi");
            ViewBag.NobetUstGrupKisitId = new SelectList(_nobetUstGrupKisitService.GetDetaylar(ustGrupSession.Id), "Id", "KisitAdiUzun");
            return View();
        }

        // POST: EczaneNobet/EczaneNobetGrupKisit/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NobetUstGrupKisitId,EczaneNobetGrupId,PasifMi,SagTarafDegeri,VarsayilanPasifMi,SagTarafDegeriVarsayilan,Aciklama")] EczaneNobetGrupKisit eczaneNobetGrupKisit)
        {
            if (ModelState.IsValid)
            {
                _eczaneNobetGrupKisitService.Insert(eczaneNobetGrupKisit);
                return RedirectToAction("Index");
            }

            var ustGrupSession = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            ViewBag.EczaneNobetGrupId = new SelectList(_eczaneNobetGrupService.GetDetaylar(ustGrupSession.Id), "Id", "EczaneGorevTipAdi", eczaneNobetGrupKisit.EczaneNobetGrupId);
            ViewBag.NobetUstGrupKisitId = new SelectList(_nobetUstGrupKisitService.GetDetaylar(ustGrupSession.Id), "Id", "KisitAdiUzun", eczaneNobetGrupKisit.NobetUstGrupKisitId);
            return View(eczaneNobetGrupKisit);
        }

        // GET: EczaneNobet/EczaneNobetGrupKisit/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EczaneNobetGrupKisitDetay eczaneNobetGrupKisit = _eczaneNobetGrupKisitService.GetDetayById(id);
            if (eczaneNobetGrupKisit == null)
            {
                return HttpNotFound();
            }
            var ustGrupSession = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            ViewBag.EczaneNobetGrupId = new SelectList(_eczaneNobetGrupService.GetDetaylar(ustGrupSession.Id), "Id", "EczaneGorevTipAdi", eczaneNobetGrupKisit.EczaneNobetGrupId);
            ViewBag.NobetUstGrupKisitId = new SelectList(_nobetUstGrupKisitService.GetDetaylar(ustGrupSession.Id), "Id", "KisitAdiUzun", eczaneNobetGrupKisit.NobetUstGrupKisitId);
            return View(eczaneNobetGrupKisit);
        }

        // POST: EczaneNobet/EczaneNobetGrupKisit/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NobetUstGrupKisitId,EczaneNobetGrupId,PasifMi,SagTarafDegeri,VarsayilanPasifMi,SagTarafDegeriVarsayilan,Aciklama")] EczaneNobetGrupKisit eczaneNobetGrupKisit)
        {
            if (ModelState.IsValid)
            {
                _eczaneNobetGrupKisitService.Update(eczaneNobetGrupKisit);

                return RedirectToAction("Index");
            }
            var ustGrupSession = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            ViewBag.EczaneNobetGrupId = new SelectList(_eczaneNobetGrupService.GetDetaylar(ustGrupSession.Id), "Id", "EczaneGorevTipAdi", eczaneNobetGrupKisit.EczaneNobetGrupId);
            ViewBag.NobetUstGrupKisitId = new SelectList(_nobetUstGrupKisitService.GetDetaylar(ustGrupSession.Id), "Id", "KisitAdiUzun", eczaneNobetGrupKisit.NobetUstGrupKisitId);
            return View(eczaneNobetGrupKisit);
        }

        // GET: EczaneNobet/EczaneNobetGrupKisit/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EczaneNobetGrupKisitDetay eczaneNobetGrupKisit = _eczaneNobetGrupKisitService.GetDetayById(id);
            if (eczaneNobetGrupKisit == null)
            {
                return HttpNotFound();
            }
            return View(eczaneNobetGrupKisit);
        }

        // POST: EczaneNobet/EczaneNobetGrupKisit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EczaneNobetGrupKisitDetay eczaneNobetGrupKisit = _eczaneNobetGrupKisitService.GetDetayById(id);
            _eczaneNobetGrupKisitService.Delete(id);
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
