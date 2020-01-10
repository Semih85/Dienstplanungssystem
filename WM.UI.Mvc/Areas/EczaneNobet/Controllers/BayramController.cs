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

namespace WM.UI.Mvc.Areas.EczaneNobet.Controllers
{
    [Authorize]
    public class BayramController : Controller
    {
        #region ctor
        private IUserService _userService;
        private IBayramService _bayramService;
        private IBayramTurService _bayramTurService;
        private INobetGrupService _nobetGrupService;
        private INobetGrupGorevTipService _nobetGrupGorevTipService;
        private ITakvimService _takvimService;
        private INobetGunKuralService _nobetGunKuralService;

        public BayramController(IBayramService bayramService,
            IUserService userService,
            INobetGrupService nobetGrupService,
            IBayramTurService bayramTurService,
            INobetGrupGorevTipService nobetGrupGorevTipService,
            ITakvimService takvimService,
            INobetGunKuralService nobetGunKuralService)
        {
            _bayramService = bayramService;
            _userService = userService;
            _nobetGrupService = nobetGrupService;
            _bayramTurService = bayramTurService;
            _nobetGrupGorevTipService = nobetGrupGorevTipService;
            _takvimService = takvimService;
            _nobetGunKuralService = nobetGunKuralService;
        }
        #endregion

        // GET: EczaneNobet/Bayram
        public ActionResult Index()
        {
            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetGruplar = _nobetGrupService.GetListByUser(user);

            var bayramTurler = _bayramTurService.GetList();

            ViewBag.BayramTurId = new SelectList(bayramTurler, "Id", "Adi");
            ViewBag.NobetGrupId = new SelectList(nobetGruplar, "Id", "Adi");

            return View();
        }
        public ActionResult SearchWithBayram(int? NobetGrupId = 0, int? BayramTurId = 0)
        {
            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetGruplar = _nobetGrupService.GetListByUser(user).ToList();
            var nobetGrupIdlar = nobetGruplar.Select(s => s.Id).ToList();

            if (NobetGrupId != 0)
            {
                nobetGrupIdlar = _nobetGrupService.GetListByUser(user)
                .Where(w => w.Id == NobetGrupId)
                .Select(s => s.Id).ToList();
            }

            var bayramTurIdlar = _bayramTurService.GetList()
                .Select(s => s.Id).ToList();

            if (BayramTurId != 0)
            {
                bayramTurIdlar = _bayramTurService.GetList()
                .Where(w => w.Id == BayramTurId)
                .Select(s => s.Id).ToList();
            }

            var bayramTurler = _bayramTurService.GetList().ToList();

            var nobetGrupGorevTipIdler = _nobetGrupGorevTipService.GetList()
                .Where(w => nobetGrupIdlar.Contains(w.NobetGrupId))
                .Select(s => s.Id).ToList();

            var bayramlar = _bayramService.GetDetaylar()
                .Where(w => nobetGrupGorevTipIdler.Contains(w.NobetGrupGorevTipId)
                && bayramTurIdlar.Contains(w.BayramTurId))
                .OrderBy(o => o.Tarih).ToList();

            ViewBag.BayramTurId = new SelectList(bayramTurler, "Id", "Adi");
            ViewBag.NobetGrupId = new SelectList(nobetGruplar, "Id", "Adi");

            return PartialView("BayramPartialView", bayramlar);
        }

        [HttpPost]
        public ActionResult SecilenleriSil(string silinecekBayramlar, string silinMEyecekBayramlar)
        {
            //List<int> nobetUstGruplar = new List<int>();
            var mesaj = "Seçim Yapmadınız!";

            if (silinecekBayramlar == "")
                return Json(mesaj, JsonRequestBehavior.AllowGet);

            //List<BayramDetay> model = new List<BayramDetay>();
            var liste = silinecekBayramlar.Split(',');
            var ids = Array.ConvertAll(liste, s => int.Parse(s));

            if (ids.Count() > 0)
                _bayramService.CokluSil(ids);

            //foreach (string item in liste)
            //{
            //   var bayramId = _bayramService.GetDetaylar()
            //    .Where(w => w.Id == Convert.ToInt32(item)).Select(s => s.Id)
            //    .FirstOrDefault(); 
            //    _bayramService.Delete(bayramId);
            //}

            //var liste2 = silinMEyecekBayramlar.Split(',');
            //foreach (string item in liste2)
            //{
            //    BayramDetay bayramDetay = _bayramService.GetDetayById(Convert.ToInt32(item));
            //    model.Add(bayramDetay);
            //}

            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetGruplar = _nobetGrupService.GetListByUser(user).ToList();
            var nobetGrupIdlar = nobetGruplar.Select(s => s.Id).ToList();

            //var bayramTurler = _bayramTurService.GetList().ToList();

            var nobetGrupGorevTipIdler = _nobetGrupGorevTipService.GetList()
                .Where(w => nobetGrupIdlar.Contains(w.NobetGrupId))
                .Select(s => s.Id).ToList();

            var bayramlar = _bayramService.GetDetaylar()
                .Where(w => nobetGrupGorevTipIdler.Contains(w.NobetGrupGorevTipId)
                //&& bayramTurIdlar.Contains(w.BayramTurId)
               ).OrderBy(o => o.Tarih).ToList();


            TempData["SilinenBayramSayisi"] = ids.Length;

            return PartialView("BayramPartialView", bayramlar);
        }
        // GET: EczaneNobet/Bayram/Details/5
        public ActionResult Details(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var bayram = _bayramService.GetDetayById(id);
            if (bayram == null)
            {
                return HttpNotFound();
            }
            return View(bayram);
        }

        // GET: EczaneNobet/Bayram/Create
        public ActionResult Create()
        {
            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetGruplar = _nobetGrupService.GetListByUser(user);

            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar()
                .Where(w => nobetGruplar.Select(s => s.Id).Contains(w.NobetGrupId))
                .Select(s => new MyDrop { Id = s.Id, Value = $"{s.NobetGrupAdi}, {s.NobetGorevTipAdi}" });

            ViewBag.BayramTurId = new SelectList(_bayramTurService.GetList(), "Id", "Adi");
            ViewBag.NobetGrupGorevTipId = new SelectList(nobetGrupGorevTipler, "Id", "Value");
            ViewBag.NobetGunKuralId = new SelectList(_nobetGunKuralService.GetList().Where(w => w.Id > 7), "Id", "Adi");
            ViewBag.TakvimId = new SelectList(_takvimService.GetList(), "Id", "Tarih");

            return View();
        }

        // POST: EczaneNobet/Bayram/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Tarih,NobetGrupGorevTipId,NobetGunKuralId,BayramTurId")] BayramCoklu bayramCoklu)
        {
            if (ModelState.IsValid)
            {
                var bayramlar = new List<Bayram>();

                foreach (var nobetGrupGorevTipId in bayramCoklu.NobetGrupGorevTipId)
                {
                    bayramlar.Add(new Bayram
                    {
                        TakvimId = _takvimService.GetByTarih(bayramCoklu.Tarih).Id,
                        NobetGunKuralId = bayramCoklu.NobetGunKuralId,
                        NobetGrupGorevTipId = nobetGrupGorevTipId,
                        BayramTurId = bayramCoklu.BayramTurId
                    });
                }

                var eklenecekbayramSayisi = bayramlar.Count;

                if (ModelState.IsValid && eklenecekbayramSayisi > 0)
                {
                    _bayramService.CokluEkle(bayramlar);
                    TempData["EklenenBayramSayisi"] = eklenecekbayramSayisi;
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }

            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetGruplar = _nobetGrupService.GetListByUser(user);

            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar()
                .Where(w => nobetGruplar.Select(s => s.Id).Contains(w.NobetGrupId))
                .Select(s => new MyDrop { Id = s.Id, Value = $"{s.NobetGrupAdi}, {s.NobetGorevTipAdi}" });

            ViewBag.BayramTurId = new SelectList(_bayramTurService.GetList(), "Id", "Adi", bayramCoklu.BayramTurId);
            ViewBag.NobetGrupGorevTipId = new SelectList(nobetGrupGorevTipler, "Id", "Value");
            ViewBag.NobetGunKuralId = new SelectList(_nobetGunKuralService.GetList().Where(w => w.Id > 7), "Id", "Adi", bayramCoklu.NobetGunKuralId);
            ViewBag.TakvimId = new SelectList(_takvimService.GetList(), "Id", "Tarih", bayramCoklu.Tarih);
            return View(bayramCoklu);
        }

        // GET: EczaneNobet/Bayram/Edit/5
        public ActionResult Edit(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var bayram = _bayramService.GetDetayById(id);
            if (bayram == null)
            {
                return HttpNotFound();
            }
            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetGruplar = _nobetGrupService.GetListByUser(user);

            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar()
                .Where(w => nobetGruplar.Select(s => s.Id).Contains(w.NobetGrupId))
                .Select(s => new MyDrop { Id = s.Id, Value = $"{s.NobetGrupAdi}, {s.NobetGorevTipAdi}" });

            ViewBag.BayramTurId = new SelectList(_bayramTurService.GetList(), "Id", "Adi", bayram.BayramTurId);
            ViewBag.NobetGrupGorevTipId = new SelectList(nobetGrupGorevTipler, "Id", "Value", bayram.NobetGrupGorevTipId);
            ViewBag.NobetGunKuralId = new SelectList(_nobetGunKuralService.GetList().Where(w => w.Id > 7), "Id", "Adi", bayram.NobetGunKuralId);
            ViewBag.TakvimId = new SelectList(_takvimService.GetList(), "Id", "Tarih", bayram.TakvimId);
            return View(bayram);
        }

        // POST: EczaneNobet/Bayram/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TakvimId,NobetGrupGorevTipId,NobetGunKuralId,BayramTurId")] Bayram bayram)
        {
            if (ModelState.IsValid)
            {
                _bayramService.Update(bayram);
                return RedirectToAction("Index");
            }
            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetGruplar = _nobetGrupService.GetListByUser(user);

            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar()
                .Where(w => nobetGruplar.Select(s => s.Id).Contains(w.NobetGrupId))
                .Select(s => new MyDrop { Id = s.Id, Value = $"{s.NobetGrupAdi}, {s.NobetGorevTipAdi}" });

            ViewBag.BayramTurId = new SelectList(_bayramTurService.GetList(), "Id", "Adi", bayram.BayramTurId);
            ViewBag.NobetGrupGorevTipId = new SelectList(nobetGrupGorevTipler, "Id", "Value", bayram.NobetGrupGorevTipId);
            ViewBag.NobetGunKuralId = new SelectList(_nobetGunKuralService.GetList().Where(w => w.Id > 7), "Id", "Adi", bayram.NobetGunKuralId);
            ViewBag.TakvimId = new SelectList(_takvimService.GetList(), "Id", "Tarih", bayram.TakvimId);
            return View(bayram);
        }

        // GET: EczaneNobet/Bayram/Delete/5
        public ActionResult Delete(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var bayram = _bayramService.GetDetayById(id);
            if (bayram == null)
            {
                return HttpNotFound();
            }
            return View(bayram);
        }

        // POST: EczaneNobet/Bayram/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var bayram = _bayramService.GetDetayById(id);
            _bayramService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
