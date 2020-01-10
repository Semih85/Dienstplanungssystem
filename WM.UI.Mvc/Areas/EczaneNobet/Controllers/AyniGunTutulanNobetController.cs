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
using WM.Northwind.Entities.Concrete.Enums;
using WM.UI.Mvc.Models;
using WM.UI.Mvc.Services;

namespace WM.UI.Mvc.Areas.EczaneNobet.Controllers
{
    [Authorize(Roles = "Admin,Oda,Üst Grup")]
    [HandleError]
    [Authorize]
    public class AyniGunTutulanNobetController : Controller
    {
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
            var ustGrupSession = _nobetUstGrupSessionService.GetSession("nobetUstGrup");
            var nobetUstGrupId = ustGrupSession.Id;

            //var ikiliEczaneler = new List<AyniGunTutulanNobetDetay>();

            //Bir üst grupta çok sayıda ikili olduğu için bu listeyi view'a direkt olarak göndermiyoruz. 
            var ikiliEczaneler = _ayniGunTutulanNobetService.GetDetaylar(nobetUstGrupId);

            var sayi = ikiliEczaneler.Count;

            //if (sayi == 0)
            //{
            //    ikiliEczaneler = ikiliEczaneler2;
            //}

            //.OrderBy(o => o.NobetGrupAdi1)
            //.ThenBy(t => t.EczaneAdi1)
            //.ThenBy(o => o.NobetGrupAdi2)
            //.ThenBy(t => t.EczaneAdi2).ToList();

            var ayniGunNobetSayilari = ikiliEczaneler
                .Select(s => new
                {
                    Id = s.AyniGunNobetSayisi,
                    Value = $"{s.AyniGunNobetSayisi} nöbet"
                })
                .Distinct()
                .OrderBy(o => o.Id).ToList();

            ViewBag.IkiliEczaneSayisi = sayi;
            ViewBag.AyniGunNobetSayisi = new SelectList(ayniGunNobetSayilari, "Id", "Value");

            return View();
        }

        public JsonResult AyniGunNobetTutanlarinListesi(int ayniGunNobetSayisi)
        {
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var ikiliEczaneler = _ayniGunTutulanNobetService.GetDetaylar(nobetUstGrup.Id, ayniGunNobetSayisi);

            var jsonResult = Json(ikiliEczaneler, JsonRequestBehavior.AllowGet);

            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public ActionResult IkiliEczaneleriOlustur()
        {
            //var user = _userService.GetByUserName(User.Identity.Name);
            //var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

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
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var ikiliEczanelerTumu = _ayniGunTutulanNobetService.GetDetaylar(nobetUstGrup.Id);

            var sonuclar = _eczaneNobetSonucService.GetSonuclarUstGrupBaslamaTarihindenSonra(nobetUstGrup.Id);

            var ayniGunNobetTutanEczaneler = _eczaneNobetOrtakService.GetAyniGunNobetTutanEczaneler(sonuclar);
            var ayniGunNobetSayisiGrouped = _eczaneNobetOrtakService.AyniGunTutulanNobetSayisiniHesapla(ayniGunNobetTutanEczaneler);

            _ayniGunTutulanNobetService.AyniGunNobetSayisiniGuncelle(ayniGunNobetSayisiGrouped, AyniGunNobetEklemeTuru.Eşitle);

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
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");
            var ikiliEczanelerTumu = _ayniGunTutulanNobetService.GetDetaylar(nobetUstGrup.Id);

            var ikiliEczanelerSifirdanBuyukler = _ayniGunTutulanNobetService.GetListSifirdanFarkli(nobetUstGrup.Id);
            _ayniGunTutulanNobetService.IkiliEczaneIstatistiginiSifirla(nobetUstGrup.Id);

            ViewBag.IkiliEczaneSayisi = ikiliEczanelerTumu.Count;
            ViewBag.SifirlananKayitSayisi = ikiliEczanelerSifirdanBuyukler.Count;

            var ikiliEczaneler = new List<AyniGunTutulanNobetDetay>();

            return View("Index", ikiliEczaneler);
        }

        // GET: EczaneNobet/AyniGunTutulanNobet/Details/5
        public ActionResult Details(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ayniGunTutulanNobet = _ayniGunTutulanNobetService.GetDetayById(id);
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
                _ayniGunTutulanNobetService.Insert(ayniGunTutulanNobet);
                return RedirectToAction("Index");
            }

            return View(ayniGunTutulanNobet);
        }

        // GET: EczaneNobet/AyniGunTutulanNobet/Edit/5
        public ActionResult Edit(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ayniGunTutulanNobet = _ayniGunTutulanNobetService.GetDetayById(id);
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
                _ayniGunTutulanNobetService.Update(ayniGunTutulanNobet);
                return RedirectToAction("Index");
            }
            return View(ayniGunTutulanNobet);
        }

        // GET: EczaneNobet/AyniGunTutulanNobet/Delete/5
        public ActionResult Delete(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ayniGunTutulanNobet = _ayniGunTutulanNobetService.GetDetayById(id);
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
            var ayniGunTutulanNobet = _ayniGunTutulanNobetService.GetDetayById(id);
            _ayniGunTutulanNobetService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
