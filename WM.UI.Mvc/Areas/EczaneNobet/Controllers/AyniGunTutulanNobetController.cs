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
using WM.UI.Mvc.Models;
using WM.UI.Mvc.Services;

namespace WM.UI.Mvc.Areas.EczaneNobet.Controllers
{
    [HandleError]
    [Authorize]
    public class AyniGunTutulanNobetController : Controller
    {
        private WMUIMvcContext db = new WMUIMvcContext();
        private IAyniGunTutulanNobetService _ayniGunTutulanNobetService;
        private IUserService _userService;
        private INobetUstGrupService _nobetUstGrupService;
        private IEczaneNobetOrtakService _eczaneNobetOrtakService;
        private IEczaneNobetSonucService _eczaneNobetSonucService;
        private INobetUstGrupSessionService _nobetUstGrupSessionService;

        public AyniGunTutulanNobetController(IAyniGunTutulanNobetService ayniGunTutulanNobetService,
            IUserService userService,
            INobetUstGrupService nobetUstGrupService,
            IEczaneNobetOrtakService eczaneNobetOrtakService,
            IEczaneNobetSonucService eczaneNobetSonucService,
            INobetUstGrupSessionService nobetUstGrupSessionService)
        {
            _ayniGunTutulanNobetService = ayniGunTutulanNobetService;
            _userService = userService;
            _nobetUstGrupService = nobetUstGrupService;
            _eczaneNobetOrtakService = eczaneNobetOrtakService;
            _eczaneNobetSonucService = eczaneNobetSonucService;
            _nobetUstGrupSessionService = nobetUstGrupSessionService;
        }

        // GET: EczaneNobet/AyniGunTutulanNobet
        [Authorize(Roles = "Admin,Oda,Üst Grup")]
        public ActionResult Index()
        {
            var ustGrupSession = _nobetUstGrupSessionService.GetNobetUstGrup();
            var nobetUstGrupId = ustGrupSession.Id;

            var ikiliEczaneler = new List<AyniGunTutulanNobetDetay>();

            //Bir üst grupta çok sayıda ikili olduğu için bu listeyi view'a direkt olarak göndermiyoruz. 
            var ikiliEczaneler2 = _ayniGunTutulanNobetService.GetDetaylar(nobetUstGrupId);

            var sayi = ikiliEczaneler2.Count;

            if (sayi == 0)
            {
                ikiliEczaneler = ikiliEczaneler2;
            }

            //.OrderBy(o => o.NobetGrupAdi1)
            //.ThenBy(t => t.EczaneAdi1)
            //.ThenBy(o => o.NobetGrupAdi2)
            //.ThenBy(t => t.EczaneAdi2).ToList();

            ViewBag.IkiliEczaneSayisi = sayi;

            return View(ikiliEczaneler);
        }

        public ActionResult IkiliEczaneleriOlustur()
        {
            //var user = _userService.GetByUserName(User.Identity.Name);
            //var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            var nobetUstGrup = _nobetUstGrupSessionService.GetNobetUstGrup();

            var ikiliEczaneler = _ayniGunTutulanNobetService.IkiliEczaneleriOlustur(nobetUstGrup.Id);

            //var ikiliEczaneler = _ayniGunTutulanNobetService.GetDetaylar(nobetUstGrupId);
            //.OrderBy(o => o.NobetGrupAdi1)
            //.ThenBy(t => t.EczaneAdi1)
            //.ThenBy(o => o.NobetGrupAdi2)
            //.ThenBy(t => t.EczaneAdi2).ToList();
            ViewBag.IkiliEczaneSayisi = ikiliEczaneler.Count;

            var ikiliEczaneler1 = new List<AyniGunTutulanNobetDetay>();

            return View("Index", ikiliEczaneler1);
        }

        //yoksa ekler varsa günceller
        public ActionResult AyniGunNobetTutanlariTabloyaEkle()
        {
            var nobetUstGrup = _nobetUstGrupSessionService.GetNobetUstGrup();

            var ikiliEczanelerTumu = _ayniGunTutulanNobetService.GetDetaylar(nobetUstGrup.Id);

            var sonuclar = _eczaneNobetSonucService.GetSonuclarUstGrupBaslamaTarihindenSonra(nobetUstGrup.Id);

            var ayniGunNobetTutanEczaneler = _eczaneNobetOrtakService.GetAyniGunNobetTutanEczaneler(sonuclar);
            var ayniGunNobetSayisiGrouped = _eczaneNobetOrtakService.AyniGunTutulanNobetSayisiniHesapla(ayniGunNobetTutanEczaneler);

            _ayniGunTutulanNobetService.AyniGunNobetTutanlariTabloyaEkle(ayniGunNobetSayisiGrouped);

            ViewBag.IkiliEczaneSayisi = ikiliEczanelerTumu.Count;
            ViewBag.AyniGunNobetTutanEczaneler = ayniGunNobetTutanEczaneler.Count;

            var ikiliEczaneler = new List<AyniGunTutulanNobetDetay>();

            return View("Index", ikiliEczaneler);
        }

        //istatistiği sıfırlar
        public ActionResult IkiliEczaneIstatistiginiSifirla()
        {
            //var user = _userService.GetByUserName(User.Identity.Name);
            //var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            //var nobetUstGrup = nobetUstGruplar.FirstOrDefault();
            var nobetUstGrup = _nobetUstGrupSessionService.GetNobetUstGrup();
            var ikiliEczanelerTumu = _ayniGunTutulanNobetService.GetDetaylar(nobetUstGrup.Id);

            var ikiliEczanelerSifirdanBuyukler = _ayniGunTutulanNobetService.GetListSifirdanBuyukler(nobetUstGrup.Id);
            _ayniGunTutulanNobetService.IkiliEczaneIstatistiginiSifirla(nobetUstGrup.Id);

            ViewBag.IkiliEczaneSayisi = ikiliEczanelerTumu.Count;
            ViewBag.SifirlananKayitSayisi = ikiliEczanelerSifirdanBuyukler.Count;

            var ikiliEczaneler = new List<AyniGunTutulanNobetDetay>();

            return View("Index", ikiliEczaneler);
        }

        // GET: EczaneNobet/AyniGunTutulanNobet/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AyniGunTutulanNobet ayniGunTutulanNobet = db.AyniGunTutulanNobets.Find(id);
            if (ayniGunTutulanNobet == null)
            {
                return HttpNotFound();
            }
            return View(ayniGunTutulanNobet);
        }

        // GET: EczaneNobet/AyniGunTutulanNobet/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EczaneNobet/AyniGunTutulanNobet/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EczaneNobetGrupId1,EczaneNobetGrupId2,EnSonAyniGunNobetTakvimId,AyniGunNobetSayisi,AyniGunNobetTutamayacaklariGunSayisi")] AyniGunTutulanNobet ayniGunTutulanNobet)
        {
            if (ModelState.IsValid)
            {
                db.AyniGunTutulanNobets.Add(ayniGunTutulanNobet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ayniGunTutulanNobet);
        }

        // GET: EczaneNobet/AyniGunTutulanNobet/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AyniGunTutulanNobet ayniGunTutulanNobet = db.AyniGunTutulanNobets.Find(id);
            if (ayniGunTutulanNobet == null)
            {
                return HttpNotFound();
            }
            return View(ayniGunTutulanNobet);
        }

        // POST: EczaneNobet/AyniGunTutulanNobet/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EczaneNobetGrupId1,EczaneNobetGrupId2,EnSonAyniGunNobetTakvimId,AyniGunNobetSayisi,AyniGunNobetTutamayacaklariGunSayisi")] AyniGunTutulanNobet ayniGunTutulanNobet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ayniGunTutulanNobet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ayniGunTutulanNobet);
        }

        // GET: EczaneNobet/AyniGunTutulanNobet/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AyniGunTutulanNobet ayniGunTutulanNobet = db.AyniGunTutulanNobets.Find(id);
            if (ayniGunTutulanNobet == null)
            {
                return HttpNotFound();
            }
            return View(ayniGunTutulanNobet);
        }

        // POST: EczaneNobet/AyniGunTutulanNobet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AyniGunTutulanNobet ayniGunTutulanNobet = db.AyniGunTutulanNobets.Find(id);
            db.AyniGunTutulanNobets.Remove(ayniGunTutulanNobet);
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
