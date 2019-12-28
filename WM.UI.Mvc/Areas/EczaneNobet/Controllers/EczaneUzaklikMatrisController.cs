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
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;
using WM.UI.Mvc.Areas.EczaneNobet.Models;
using WM.UI.Mvc.Models;
using WM.UI.Mvc.Services;

namespace WM.UI.Mvc.Areas.EczaneNobet.Controllers
{
    [HandleError]
    [Authorize]
    public class EczaneUzaklikMatrisController : Controller
    {
        #region ctor
        private IEczaneService _eczaneService;
        private IUserService _userService;
        private IEczaneUzaklikMatrisService _eczaneUzaklikMatrisService;
        private IEczaneNobetGrupService _eczaneNobetGrupService;
        private INobetGrupService _nobetGrupService;
        private INobetUstGrupService _nobetUstGrupService;
        private IEczaneNobetSonucService _eczaneNobetSonucService;
        private INobetUstGrupSessionService _nobetUstGrupSessionService;

        public EczaneUzaklikMatrisController(IEczaneService eczaneService,
                                IUserService userService,
                                IEczaneUzaklikMatrisService eczaneUzaklikMatrisService,
                                INobetUstGrupService nobetUstGrupService,
                                INobetGrupService nobetGrupService,
                                IEczaneNobetGrupService eczaneNobetGrupService,
                                IEczaneNobetSonucService eczaneNobetSonucService,
                                INobetUstGrupSessionService nobetUstGrupSessionService)
        {
            _eczaneService = eczaneService;
            _nobetUstGrupService = nobetUstGrupService;
            _nobetGrupService = nobetGrupService;
            _eczaneUzaklikMatrisService = eczaneUzaklikMatrisService;
            _eczaneNobetSonucService = eczaneNobetSonucService;
            _userService = userService;
            _eczaneNobetGrupService = eczaneNobetGrupService;
            _nobetUstGrupSessionService = nobetUstGrupSessionService;
        }
        #endregion
        private WMUIMvcContext db = new WMUIMvcContext();

        // GET: EczaneNobet/EczaneUzaklikMatris
        public ActionResult Index()
        {
            //var user = _userService.GetByUserName(User.Identity.Name);
            //var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user).Select(s => s.Id);
            var ustGrupSession = _nobetUstGrupSessionService.GetSession("nobetUstGrup");
            
            var nobetUstGrupId = ustGrupSession.Id;
            
            var eczaneGrupIdList = _nobetGrupService.GetListByNobetUstGrupId(nobetUstGrupId).Select(s => s.Id).ToList();

            var nobetciEczaneler = _eczaneNobetGrupService.GetAktifEczaneNobetGrupList(eczaneGrupIdList);
            //nobetciEczaneler = nobetciEczaneler.Take(10).ToList();

            var model = new EczaneUzaklikMatrisViewModel
            {
                Eczaneler = new List<Eczane>(),
                NobetUstGrupId = nobetUstGrupId,
                //Uzakliklar = new List<EczaneUzaklikMatrisDetay>()
            };

            foreach (var item in nobetciEczaneler)
            {
                var adres = _eczaneService.GetById(item.EczaneId).Adres;
                var enlem = _eczaneService.GetById(item.EczaneId).Enlem;
                var boylam = _eczaneService.GetById(item.EczaneId).Boylam;
                var telefonNo = _eczaneService.GetById(item.EczaneId).TelefonNo;
                var adresTarifi = _eczaneService.GetById(item.EczaneId).AdresTarifi;
                var adresTarifiKisa = _eczaneService.GetById(item.EczaneId).AdresTarifiKisa;

                model.Eczaneler.Add(new Eczane
                {
                    Id = item.EczaneId,
                    Adi = item.EczaneAdi,
                    Adres = adres,
                    Enlem = enlem,
                    Boylam = boylam,
                    TelefonNo = telefonNo,
                    AdresTarifi = adresTarifi,
                    AdresTarifiKisa = adresTarifiKisa
                });
            }

            SetUzakliklarKusUcusu(model.Eczaneler);

            var sonuclar = _eczaneUzaklikMatrisService.GetDetaylar(nobetUstGrupId);

            model.Uzakliklar = sonuclar;

            return View(model);
        }

        double DegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180;
        }

        void SetUzakliklarKusUcusu(List<Eczane> eczaneler)
        {
            double earthRadiusM = 6371000;

            var siraliEcaneler = eczaneler.OrderBy(s => s.Id).ToList();

            var eczaneUzaklikMatrisList = new List<EczaneUzaklikMatrisDetay>();

            foreach (var itemFrom in siraliEcaneler)
            {
                var kotrol = true;

                if (kotrol)
                {
                    if (itemFrom.Adi == "NEFES")
                    {
                    }
                }

                if (itemFrom.Enlem <= 1 || itemFrom.Boylam <= 1)
                    continue;

                var siraliEcaneler2 = siraliEcaneler.Where(s => s.Id > itemFrom.Id).ToList();

                foreach (var itemTo in siraliEcaneler2)
                {
                    if (itemTo.Enlem <= 1 || itemTo.Boylam <= 1)
                        continue;

                    double lat1 = itemFrom.Enlem;
                    double lon1 = itemFrom.Boylam;
                    double lat2 = itemTo.Enlem;
                    double lon2 = itemTo.Boylam;

                    double dLat = DegreesToRadians(lat2 - lat1);
                    double dLon = DegreesToRadians(lon2 - lon1);

                    lat1 = DegreesToRadians(lat1);
                    lat2 = DegreesToRadians(lat2);

                    double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                        Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(lat1) * Math.Cos(lat2);

                    double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

                    double distance = earthRadiusM * c;

                    eczaneUzaklikMatrisList.Add(new EczaneUzaklikMatrisDetay
                    {
                        EczaneIdFrom = itemFrom.Id,
                        EczaneIdTo = itemTo.Id,
                        Mesafe = Convert.ToInt32(distance)
                    });

                    var ustGrupSession = _nobetUstGrupSessionService.GetSession("nobetUstGrup");
                    var nobetUstGrupId = ustGrupSession.Id;
                }
            }

            try
            {
                _eczaneUzaklikMatrisService.CokluEkle(eczaneUzaklikMatrisList);
            }
            catch (Exception ex)
            {
                TempData["MessageDanger"] = "ERROR: " + ex.InnerException.InnerException.Message.ToString();
            }
        }

        // GET: EczaneNobet/EczaneUzaklikMatris/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EczaneUzaklikMatris eczaneUzaklikMatris = db.EczaneUzaklikMatris.Find(id);
            if (eczaneUzaklikMatris == null)
            {
                return HttpNotFound();
            }
            return View(eczaneUzaklikMatris);
        }

        // GET: EczaneNobet/EczaneUzaklikMatris/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EczaneNobet/EczaneUzaklikMatris/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EczaneIdFrom,EczaneIdTo,Mesafe")] EczaneUzaklikMatris eczaneUzaklikMatris)
        {
            if (ModelState.IsValid)
            {
                db.EczaneUzaklikMatris.Add(eczaneUzaklikMatris);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(eczaneUzaklikMatris);
        }

        // GET: EczaneNobet/EczaneUzaklikMatris/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EczaneUzaklikMatris eczaneUzaklikMatris = db.EczaneUzaklikMatris.Find(id);
            if (eczaneUzaklikMatris == null)
            {
                return HttpNotFound();
            }
            return View(eczaneUzaklikMatris);
        }

        // POST: EczaneNobet/EczaneUzaklikMatris/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EczaneIdFrom,EczaneIdTo,Mesafe")] EczaneUzaklikMatris eczaneUzaklikMatris)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eczaneUzaklikMatris).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(eczaneUzaklikMatris);
        }

        // GET: EczaneNobet/EczaneUzaklikMatris/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EczaneUzaklikMatris eczaneUzaklikMatris = db.EczaneUzaklikMatris.Find(id);
            if (eczaneUzaklikMatris == null)
            {
                return HttpNotFound();
            }
            return View(eczaneUzaklikMatris);
        }

        // POST: EczaneNobet/EczaneUzaklikMatris/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EczaneUzaklikMatris eczaneUzaklikMatris = db.EczaneUzaklikMatris.Find(id);
            db.EczaneUzaklikMatris.Remove(eczaneUzaklikMatris);
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
