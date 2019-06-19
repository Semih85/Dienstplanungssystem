using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WM.Northwind.Business.Abstract.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;
using WM.UI.Mvc.Models;
using WM.UI.Mvc.Services;

namespace WM.UI.Mvc.Areas.EczaneNobet.Controllers
{
    public class EczaneNobetSonucPlanlananController : Controller
    {
        private WMUIMvcContext db = new WMUIMvcContext();
        private IEczaneNobetSonucPlanlananService _eczaneNobetSonucPlanlananService;
        private INobetUstGrupSessionService _nobetUstGrupSessionService;
        private INobetGrupGorevTipService _nobetGrupGorevTipService;
        private ITakvimService _takvimService;
        private IEczaneNobetGrupService _eczaneNobetGrupService;

        public EczaneNobetSonucPlanlananController(IEczaneNobetSonucPlanlananService eczaneNobetSonucPlanlananService,
            INobetUstGrupSessionService nobetUstGrupSessionService,
            INobetGrupGorevTipService nobetGrupGorevTipService,
            ITakvimService takvimService,
            IEczaneNobetGrupService eczaneNobetGrupService)
        {
            _eczaneNobetSonucPlanlananService = eczaneNobetSonucPlanlananService;
            _nobetUstGrupSessionService = nobetUstGrupSessionService;
            _nobetGrupGorevTipService = nobetGrupGorevTipService;
            _takvimService = takvimService;
            _eczaneNobetGrupService = eczaneNobetGrupService;
        }

        // GET: EczaneNobet/EczaneNobetSonucPlanlanan
        public ActionResult Index()
        {
            var nobetUstGrup = _nobetUstGrupSessionService.GetNobetUstGrup();

            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetMyDrop(nobetUstGrup.Id);

            ViewBag.NobetGrupGorevTipId = new SelectList(items: nobetGrupGorevTipler, dataValueField: "Id", dataTextField: "Value");
            //var eczaneNobetSonucPlanlananlar = _eczaneNobetSonucPlanlananService.GetDetaylar(nobetUstGrup.Id);

            return View();
        }

        public ActionResult EczaneNobetSonucPlanlananPartialView()
        {

            return PartialView();
        }

        public JsonResult PlanlanaNobetleriYaz(DateTime baslangicTarihi, DateTime bitisTarihi, int nobetGrupGorevTipId)
        {
            //baslangicTarihi = new DateTime(2018, 6, 1);
            //baslangicTarihi = new DateTime(2019, 3, 13);
            //bitisTarihi = new DateTime(2020, 12, 31);

            var eczaneNobetGruplarHepsi = _eczaneNobetGrupService.GetDetaylarByNobetGrupGorevTipler(nobetGrupGorevTipId);//, baslangicTarihi, bitisTarihi);

            //var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetGrupIdListe.ToList());

            //var nobetUstGrupId = nobetGrupGorevTipler.Select(s => s.NobetUstGrupId).FirstOrDefault();

            //_takvimService.SiraliNobetYaz(nobetGrupGorevTipler, eczaneNobetGruplarHepsi, baslangicTarihi, bitisTarihi, nobetUstGrupId);

            //var besinciBolge = nobetGrupGorevTipler.SingleOrDefault(x => x.Id == 8);

            var nobetGrupGorevTip = _nobetGrupGorevTipService.GetDetayById(nobetGrupGorevTipId);

            _takvimService.SiraliNobetYazGrupBazinda(nobetGrupGorevTip, eczaneNobetGruplarHepsi, baslangicTarihi, bitisTarihi);

            var jsonResult = Json(nobetGrupGorevTip, JsonRequestBehavior.AllowGet);

            //jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        // GET: EczaneNobet/EczaneNobetSonucPlanlanan/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EczaneNobetSonucPlanlanan eczaneNobetSonucPlanlanan = db.EczaneNobetSonucPlanlanans.Find(id);
            if (eczaneNobetSonucPlanlanan == null)
            {
                return HttpNotFound();
            }
            return View(eczaneNobetSonucPlanlanan);
        }

        // GET: EczaneNobet/EczaneNobetSonucPlanlanan/Create
        public ActionResult Create()
        {
            ViewBag.EczaneNobetGrupId = new SelectList(db.EczaneNobetGrups, "Id", "Aciklama");
            ViewBag.NobetGorevTipId = new SelectList(db.NobetGorevTips, "Id", "Adi");
            ViewBag.TakvimId = new SelectList(db.Takvims, "Id", "Aciklama");
            return View();
        }

        // POST: EczaneNobet/EczaneNobetSonucPlanlanan/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EczaneNobetGrupId,TakvimId,NobetGorevTipId")] EczaneNobetSonucPlanlanan eczaneNobetSonucPlanlanan)
        {
            if (ModelState.IsValid)
            {
                db.EczaneNobetSonucPlanlanans.Add(eczaneNobetSonucPlanlanan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EczaneNobetGrupId = new SelectList(db.EczaneNobetGrups, "Id", "Aciklama", eczaneNobetSonucPlanlanan.EczaneNobetGrupId);
            ViewBag.NobetGorevTipId = new SelectList(db.NobetGorevTips, "Id", "Adi", eczaneNobetSonucPlanlanan.NobetGorevTipId);
            ViewBag.TakvimId = new SelectList(db.Takvims, "Id", "Aciklama", eczaneNobetSonucPlanlanan.TakvimId);
            return View(eczaneNobetSonucPlanlanan);
        }

        // GET: EczaneNobet/EczaneNobetSonucPlanlanan/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EczaneNobetSonucPlanlanan eczaneNobetSonucPlanlanan = db.EczaneNobetSonucPlanlanans.Find(id);
            if (eczaneNobetSonucPlanlanan == null)
            {
                return HttpNotFound();
            }
            ViewBag.EczaneNobetGrupId = new SelectList(db.EczaneNobetGrups, "Id", "Aciklama", eczaneNobetSonucPlanlanan.EczaneNobetGrupId);
            ViewBag.NobetGorevTipId = new SelectList(db.NobetGorevTips, "Id", "Adi", eczaneNobetSonucPlanlanan.NobetGorevTipId);
            ViewBag.TakvimId = new SelectList(db.Takvims, "Id", "Aciklama", eczaneNobetSonucPlanlanan.TakvimId);
            return View(eczaneNobetSonucPlanlanan);
        }

        // POST: EczaneNobet/EczaneNobetSonucPlanlanan/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EczaneNobetGrupId,TakvimId,NobetGorevTipId")] EczaneNobetSonucPlanlanan eczaneNobetSonucPlanlanan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eczaneNobetSonucPlanlanan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EczaneNobetGrupId = new SelectList(db.EczaneNobetGrups, "Id", "Aciklama", eczaneNobetSonucPlanlanan.EczaneNobetGrupId);
            ViewBag.NobetGorevTipId = new SelectList(db.NobetGorevTips, "Id", "Adi", eczaneNobetSonucPlanlanan.NobetGorevTipId);
            ViewBag.TakvimId = new SelectList(db.Takvims, "Id", "Aciklama", eczaneNobetSonucPlanlanan.TakvimId);
            return View(eczaneNobetSonucPlanlanan);
        }

        // GET: EczaneNobet/EczaneNobetSonucPlanlanan/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EczaneNobetSonucPlanlanan eczaneNobetSonucPlanlanan = db.EczaneNobetSonucPlanlanans.Find(id);
            if (eczaneNobetSonucPlanlanan == null)
            {
                return HttpNotFound();
            }
            return View(eczaneNobetSonucPlanlanan);
        }

        // POST: EczaneNobet/EczaneNobetSonucPlanlanan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EczaneNobetSonucPlanlanan eczaneNobetSonucPlanlanan = db.EczaneNobetSonucPlanlanans.Find(id);
            db.EczaneNobetSonucPlanlanans.Remove(eczaneNobetSonucPlanlanan);
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
