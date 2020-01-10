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
    public class EczaneNobetSonucPlanlananController : Controller
    {
        #region ctor
        private WMUIMvcContext db = new WMUIMvcContext();
        private IEczaneNobetSonucPlanlananService _eczaneNobetSonucPlanlananService;
        private INobetUstGrupSessionService _nobetUstGrupSessionService;
        private INobetGrupGorevTipService _nobetGrupGorevTipService;
        private ITakvimService _takvimService;
        private IEczaneNobetGrupService _eczaneNobetGrupService;
        private INobetUstGrupGunGrupService _nobetUstGrupGunGrupService;

        public EczaneNobetSonucPlanlananController(IEczaneNobetSonucPlanlananService eczaneNobetSonucPlanlananService,
            INobetUstGrupSessionService nobetUstGrupSessionService,
            INobetGrupGorevTipService nobetGrupGorevTipService,
            ITakvimService takvimService,
            IEczaneNobetGrupService eczaneNobetGrupService,
            INobetUstGrupGunGrupService nobetUstGrupGunGrupService)
        {
            _eczaneNobetSonucPlanlananService = eczaneNobetSonucPlanlananService;
            _nobetUstGrupSessionService = nobetUstGrupSessionService;
            _nobetGrupGorevTipService = nobetGrupGorevTipService;
            _takvimService = takvimService;
            _eczaneNobetGrupService = eczaneNobetGrupService;
            _nobetUstGrupGunGrupService = nobetUstGrupGunGrupService;
        }
        #endregion

        // GET: EczaneNobet/EczaneNobetSonucPlanlanan
        public ActionResult Index()
        {
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetMyDrop(nobetUstGrup.Id);

            ViewBag.NobetGrupGorevTipId = new SelectList(items: nobetGrupGorevTipler, dataValueField: "Id", dataTextField: "Value");

            return View();
        }

        public ActionResult IndexNakilEczaneyeGore()
        {
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetMyDrop(nobetUstGrup.Id);

            var nobetUstGrupGunGruplar = _nobetUstGrupGunGrupService.GetDetaylar(nobetUstGrup.Id);

            var gunGruplar = nobetUstGrupGunGruplar.Select(s => new MyDrop { Id = s.GunGrupId, Value = s.GunGrupAdi }).Distinct().ToList();

            ViewBag.NobetGrupGorevTipId = new SelectList(items: nobetGrupGorevTipler, dataValueField: "Id", dataTextField: "Value");
            ViewBag.GunGrupId = new SelectList(items: gunGruplar, dataValueField: "Id", dataTextField: "Value");

            return View();
        }

        public ActionResult EczaneNobetSonucPlanlananPartialView()
        {

            return PartialView();
        }

        public JsonResult PlanlanaNobetleriYaz(DateTime? baslangicTarihi, DateTime bitisTarihi, int nobetGrupGorevTipId = 0)
        {
            //baslangicTarihi = new DateTime(2018, 6, 1);
            //baslangicTarihi = new DateTime(2019, 3, 13);
            //bitisTarihi = new DateTime(2020, 12, 31);

            //var eczaneNobetGruplarHepsi = _eczaneNobetGrupService.GetDetaylarByNobetGrupGorevTipler(nobetGrupGorevTipId);//, baslangicTarihi, bitisTarihi);

            //var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetGrupIdListe.ToList());

            //var nobetUstGrupId = nobetGrupGorevTipler.Select(s => s.NobetUstGrupId).FirstOrDefault();

            //_takvimService.SiraliNobetYaz(nobetGrupGorevTipler, eczaneNobetGruplarHepsi, baslangicTarihi, bitisTarihi, nobetUstGrupId);

            //var besinciBolge = nobetGrupGorevTipler.SingleOrDefault(x => x.Id == 8);

            //var nobetGrupGorevTip = _nobetGrupGorevTipService.GetDetayById(nobetGrupGorevTipId);

            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrup.Id)
                .Where(w => w.Id == nobetGrupGorevTipId || nobetGrupGorevTipId == 0).ToList();

            foreach (var nobetGrupGorevTip in nobetGrupGorevTipler)
            {
                var baslamaTarihi = nobetGrupGorevTip.BaslamaTarihi;

                var eczaneNobetGruplarHepsi = _eczaneNobetGrupService.GetDetaylarByNobetGrupGorevTipler(nobetGrupGorevTip.Id);

                if (baslangicTarihi != null)
                {
                    baslamaTarihi = (DateTime)baslangicTarihi;
                }

                _takvimService.SiraliNobetYazGrupBazinda(
                    nobetGrupGorevTip,
                    eczaneNobetGruplarHepsi,
                    baslamaTarihi,
                    bitisTarihi);
            }

            var jsonResult = Json(nobetGrupGorevTipler.Count(), JsonRequestBehavior.AllowGet);

            //jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public JsonResult PlanlanaNobetleriYazNakilEczaneyeGore(
            DateTime? baslangicTarihi,
            DateTime bitisTarihi,
            int gunGrupId = 0,
            int nobetGrupGorevTipId = 0)
        {
            //baslangicTarihi = new DateTime(2018, 6, 1);
            //baslangicTarihi = new DateTime(2019, 3, 13);
            //bitisTarihi = new DateTime(2020, 12, 31);
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var gunGruplar = _nobetUstGrupGunGrupService.GetDetaylar(nobetUstGrup.Id)
                .Where(w => w.GunGrupId == gunGrupId || gunGrupId == 0).ToList();

            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrup.Id)
                .Where(w => w.Id == nobetGrupGorevTipId || nobetGrupGorevTipId == 0).ToList();

            foreach (var nobetGrupGorevTip in nobetGrupGorevTipler)
            {
                var baslamaTarihi = nobetGrupGorevTip.BaslamaTarihi;

                if (baslangicTarihi != null)
                {
                    baslamaTarihi = (DateTime)baslangicTarihi;
                }

                var eczaneNobetGruplarHepsi = _eczaneNobetGrupService.GetDetaylarByNobetGrupGorevTipler(nobetGrupGorevTip.Id);

                foreach (var gunGrup in gunGruplar)
                {
                    _takvimService.SiraliNobetYazGunGrupBazinda(
                        nobetGrupGorevTip,
                        eczaneNobetGruplarHepsi,
                        baslamaTarihi,
                        bitisTarihi,
                        gunGrup.GunGrupId);
                }
            }

            var jsonResult = Json(nobetGrupGorevTipler.Count(), JsonRequestBehavior.AllowGet);

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
