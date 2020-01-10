using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Text.RegularExpressions;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WM.Northwind.Business.Abstract.Authorization;
using WM.Northwind.Business.Abstract.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;
using WM.UI.Mvc.Models;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.UI.Mvc.Services;

namespace WM.UI.Mvc.Areas.EczaneNobet.Controllers
{
    [Authorize(Roles = "Admin,Oda,Üst Grup")]
    [HandleError]
    public class NobetGrupKuralController : Controller
    {
        #region ctor
        private INobetGrupKuralService _nobetGrupKuralService;
        private IEczaneNobetGrupService _eczaneNobetGrupService;
        private INobetGrupGorevTipService _nobetGrupGorevTipService;
        private INobetUstGrupService _nobetUstGrupService;
        private INobetKuralService _nobetKuralService;
        private IUserService _userService;
        private INobetUstGrupSessionService _nobetUstGrupSessionService;

        public NobetGrupKuralController(INobetGrupKuralService nobetGrupKuralService,
                                        INobetGrupGorevTipService nobetGrupGorevTipService,
                                        IEczaneNobetGrupService eczaneNobetGrupService,
                                        INobetUstGrupService nobetUstGrupService,
                                        INobetKuralService nobetKuralService,
                                        IUserService userService,
                                        INobetUstGrupSessionService nobetUstGrupSessionService)
        {
            _nobetGrupKuralService = nobetGrupKuralService;
            _nobetUstGrupService = nobetUstGrupService;
            _nobetGrupGorevTipService = nobetGrupGorevTipService;
            _nobetKuralService = nobetKuralService;
            _eczaneNobetGrupService = eczaneNobetGrupService;
            _userService = userService;
            _nobetUstGrupSessionService = nobetUstGrupSessionService;
        }
        #endregion

        // GET: EczaneNobet/NobetGrupKural
        public ActionResult Index()
        {
            //var user = _userService.GetByUserName(User.Identity.Name);

            //var nobetUstGrupIdlar = _nobetUstGrupService.GetListByUser(user)
            //    .Select(s => s.Id).ToList();

            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var nobetGrupKuralIdlar = _nobetGrupKuralService.GetDetaylarByNobetUstGrup(nobetUstGrup.Id)
                .Select(s => s.NobetKuralId).Distinct().ToList();

            var nobetKurallar = _nobetKuralService.GetList()
                .Where(w => nobetGrupKuralIdlar.Contains(w.Id)).ToList();

            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrup.Id)
                .Select(s => new MyDrop { Id = s.Id, Value = $"{s.NobetGrupAdi}, {s.NobetGorevTipAdi}" }).ToList();

            ViewBag.NobetKuralId = new SelectList(nobetKurallar, "Id", "Adi");
            ViewBag.NobetGrupGorevTipId = new SelectList(nobetGrupGorevTipler, "Id", "Value");

            return View();
        }

        public ActionResult SearchWithNobetGrubKural(int? nobetGrupGorevTipId = 0, int? nobetKuralId = 0)
        {
            //var user = _userService.GetByUserName(User.Identity.Name);

            //var nobetUstGrupIdlar = _nobetUstGrupService.GetListByUser(user)
            //    .Select(s => s.Id).ToList();

            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var model = _nobetGrupKuralService.GetDetaylar((int)nobetGrupGorevTipId, (int)nobetKuralId, nobetUstGrup.Id)
                .OrderBy(o => o.NobetKuralAdi)
                .ThenBy(o => o.NobetGrupId).ToList();

            return PartialView("NobetGrupKuralPartialView", model);
        }

        [HttpPost]
        public ActionResult SecilenleriSil(string silinecekNobetGrupKurallar, string silinMEyecekNobetGrupKurallar)
        {            
            var cor = "Seçim Yapmadınız!";

            if (silinecekNobetGrupKurallar == "")
            {
                return Json(cor, JsonRequestBehavior.AllowGet);
            }

            var model = new List<NobetGrupKuralDetay>();

            var liste = silinecekNobetGrupKurallar.Split(',');

            foreach (string item in liste)
            {
                _nobetGrupKuralService.Delete(Convert.ToInt32(item));
            }

            var liste2 = silinMEyecekNobetGrupKurallar.Split(',');

            foreach (string item in liste2)
            {
                NobetGrupKuralDetay nobetGrupKuralDetay = _nobetGrupKuralService.GetDetayById(Convert.ToInt32(item));
                model.Add(nobetGrupKuralDetay);
            }

            TempData["SilinenNobetGrupKuralSayisi"] = liste.Length;

            return PartialView("NobetGrupKuralPartialView", model);
        }

        // GET: EczaneNobet/NobetGrupKural/Details/5
        public ActionResult Details(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var nobetGrupKural = _nobetGrupKuralService.GetDetayById(id);
            if (nobetGrupKural == null)
            {
                return HttpNotFound();
            }
            return View(nobetGrupKural);
        }

        // GET: EczaneNobet/NobetGrupKural/Create
        public ActionResult Create()
        {
            //var user = _userService.GetByUserName(User.Identity.Name);

            //var nobetUstGrupIdlar = _nobetUstGrupService.GetListByUser(user)
            //  .Select(s => s.Id).ToList();

            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrup.Id)
                .Select(s => new MyDrop { Id = s.Id, Value = $"{s.NobetGrupAdi}, {s.NobetGorevTipAdi}" }).ToList();

            ViewBag.NobetGrupGorevTipId = new SelectList(nobetGrupGorevTipler, "Id", "Value");
            ViewBag.NobetKuralId = new SelectList(_nobetKuralService.GetList().OrderBy(s => s.Adi).Select(s => new { s.Id, s.Adi }), "Id", "Adi");
            return View();
        }


        [HttpPost]
        public ActionResult Index(string degisecekNobetGrupKurallar)
        {
            List<int> nobetUstGruplar = new List<int>();
            var cor = "Seçim Yapmadınız!";

            if (degisecekNobetGrupKurallar == "")
            {
                return Json(cor, JsonRequestBehavior.AllowGet);
            }

            NobetGrupKuralCoklu model = new NobetGrupKuralCoklu();
            NobetGrupKuralDetay nobetGrupKuralDetay = new NobetGrupKuralDetay();
            var liste = degisecekNobetGrupKurallar.Split(',');

            int i = 0;

            foreach (string item in liste)
            {
                nobetGrupKuralDetay = _nobetGrupKuralService.GetDetayById(Convert.ToInt32(item));
                if (i == 0)
                    model.Id = nobetGrupKuralDetay.Id.ToString();
                else
                    model.Id = model.Id + "," + nobetGrupKuralDetay.Id.ToString();
                i++;
            }

            model.NobetKuralId = nobetGrupKuralDetay.NobetKuralId;
            model.Deger = Convert.ToInt32(nobetGrupKuralDetay.Deger);


            var user = _userService.GetByUserName(User.Identity.Name);

            //var nobetGrup = new NobetGrup();
            var nobetGruplar = new List<NobetGrupGorevTipDetay>();
            var liste2 = model.Id.Split(',');

            foreach (var item in liste2)
            {
                int nobetGrupid = _nobetGrupKuralService.GetById(Convert.ToInt32(item)).NobetGrupGorevTipId;
                var nobetGrup = _nobetGrupGorevTipService.GetDetayById(nobetGrupid);
                nobetGruplar.Add(nobetGrup);
            }
            //var nobetGruplar = _nobetGrupService.GetListByUser(user);
            model.BaslangicTarihi = nobetGrupKuralDetay.BaslangicTarihi;
            model.BitisTarihi = Convert.ToDateTime(nobetGrupKuralDetay.BitisTarihi);
            model.Deger = Convert.ToInt32(nobetGrupKuralDetay.Deger);
            model.NobetKuralId = nobetGrupKuralDetay.NobetKuralId;

            var nobetGrupGorevTipler = nobetGruplar
                .Select(s => new MyDrop { Id = s.Id, Value = $"{s.NobetGrupAdi}, {s.NobetGorevTipAdi}" }).ToList();

            ViewBag.NobetGrupGorevTipId = new SelectList(nobetGrupGorevTipler, "Id", "Value");
            ViewBag.NobetKuralId = new SelectList(_nobetKuralService.GetList().OrderBy(s => s.Adi).Select(s => new { s.Id, s.Adi }), "Id", "Adi");

            //TempData["DegisenNobetGrupKuralSayisi"] = liste.Length;

            return View("EditCoklu", model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCoklu([Bind(Include = "Id,NobetGrupGorevTipId,NobetKuralId,BaslangicTarihi,BitisTarihi,Deger")] NobetGrupKuralCoklu nobetGrupKuralCoklu)
        {
            var user = _userService.GetByUserName(User.Identity.Name);

            if (ModelState.IsValid)
            {
                var nobetGrupKural = new NobetGrupKural();
                var liste2 = nobetGrupKuralCoklu.Id.Split(',');

                int i = 0;
                foreach (var id in liste2)
                {
                    nobetGrupKural.Id = Convert.ToInt32(id);
                    nobetGrupKural.BitisTarihi = nobetGrupKuralCoklu.BitisTarihi;
                    nobetGrupKural.BaslangicTarihi = nobetGrupKuralCoklu.BaslangicTarihi;
                    nobetGrupKural.Deger = nobetGrupKuralCoklu.Deger;
                    nobetGrupKural.NobetKuralId = nobetGrupKuralCoklu.NobetKuralId;
                    nobetGrupKural.NobetGrupGorevTipId = nobetGrupKuralCoklu.NobetGrupGorevTipId[i];
                    _nobetGrupKuralService.Update(nobetGrupKural);
                    i++;
                }

                var ekleneceknobetGrupKuralSayisi = i;

                if (ModelState.IsValid && ekleneceknobetGrupKuralSayisi > 0)
                {
                    TempData["DuzenlenenNobetGrupKuralSayisi"] = ekleneceknobetGrupKuralSayisi;
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }

            var nobetUstGrupIdlar = _nobetUstGrupService.GetListByUser(user)
              .Select(s => s.Id).ToList();

            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylarByNobetUstGrupIdList(nobetUstGrupIdlar)
                .Select(s => new MyDrop { Id = s.Id, Value = $"{s.NobetGrupAdi}, {s.NobetGorevTipAdi}" }).ToList();

            ViewBag.NobetGrupGorevTipId = new SelectList(nobetGrupGorevTipler, "Id", "Value");
            ViewBag.NobetKuralId = new SelectList(_nobetKuralService.GetList().OrderBy(s => s.Adi).Select(s => new { s.Id, s.Adi }), "Id", "Adi");
            return View(nobetGrupKuralCoklu);
        }
        // POST: EczaneNobet/NobetGrupKural/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NobetGrupGorevTipId,NobetKuralId,BaslangicTarihi,BitisTarihi,Deger")] NobetGrupKuralCoklu nobetGrupKuralCoklu)
        {
            var user = _userService.GetByUserName(User.Identity.Name);

            if (ModelState.IsValid)
            {
                var nobetGrupKurallar = new List<NobetGrupKural>();

                foreach (var NobetGrupGorevTipId in nobetGrupKuralCoklu.NobetGrupGorevTipId)
                {
                    nobetGrupKurallar.Add(new NobetGrupKural
                    {
                        NobetGrupGorevTipId = NobetGrupGorevTipId,
                        NobetKuralId = nobetGrupKuralCoklu.NobetKuralId,
                        BaslangicTarihi = nobetGrupKuralCoklu.BaslangicTarihi,
                        BitisTarihi = nobetGrupKuralCoklu.BitisTarihi,
                        Deger = nobetGrupKuralCoklu.Deger

                    });
                }

                var ekleneceknobetGrupKuralSayisi = nobetGrupKurallar.Count;

                if (ModelState.IsValid && ekleneceknobetGrupKuralSayisi > 0)
                {
                    _nobetGrupKuralService.CokluEkle(nobetGrupKurallar);
                    TempData["EklenenNobetGrupKuralSayisi"] = ekleneceknobetGrupKuralSayisi;
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");

            }

            var nobetUstGrupIdlar = _nobetUstGrupService.GetListByUser(user)
              .Select(s => s.Id).ToList();

            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylarByNobetUstGrupIdList(nobetUstGrupIdlar)
                .Select(s => new MyDrop { Id = s.Id, Value = $"{s.NobetGrupAdi}, {s.NobetGorevTipAdi}" }).ToList();

            ViewBag.NobetGrupGorevTipId = new SelectList(nobetGrupGorevTipler, "Id", "Value");
            ViewBag.NobetKuralId = new SelectList(_nobetKuralService.GetList().OrderBy(s => s.Adi).Select(s => new { s.Id, s.Adi }), "Id", "Adi", nobetGrupKuralCoklu.NobetKuralId);
            return View(nobetGrupKuralCoklu);
        }

        // GET: EczaneNobet/NobetGrupKural/Edit/5
        public ActionResult Edit(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var nobetGrupKural = _nobetGrupKuralService.GetDetayById(id);
            if (nobetGrupKural == null)
            {
                return HttpNotFound();
            }

            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrup.Id)
                .Select(s => new MyDrop { Id = s.Id, Value = $"{s.NobetGrupAdi}, {s.NobetGorevTipAdi}" }).ToList();

            ViewBag.NobetGrupGorevTipId = new SelectList(nobetGrupGorevTipler, "Id", "Value", nobetGrupKural.NobetGrupGorevTipId);
            ViewBag.NobetKuralId = new SelectList(_nobetKuralService.GetList().OrderBy(s => s.Adi).Select(s => new { s.Id, s.Adi }), "Id", "Adi", nobetGrupKural.NobetKuralId);
            return View(nobetGrupKural);
        }

        // POST: EczaneNobet/NobetGrupKural/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NobetGrupGorevTipId,NobetKuralId,BaslangicTarihi,BitisTarihi,Deger")] NobetGrupKural nobetGrupKural)
        {
            if (ModelState.IsValid)
            {
                _nobetGrupKuralService.Update(nobetGrupKural);
                return RedirectToAction("Index");
            }

            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrup.Id)
                .Select(s => new MyDrop { Id = s.Id, Value = $"{s.NobetGrupAdi}, {s.NobetGorevTipAdi}" }).ToList();

            ViewBag.NobetGrupGorevTipId = new SelectList(nobetGrupGorevTipler, "Id", "Value", nobetGrupKural.NobetGrupGorevTipId);
            ViewBag.NobetKuralId = new SelectList(_nobetKuralService.GetList().OrderBy(s => s.Adi).Select(s => new { s.Id, s.Adi }), "Id", "Adi", nobetGrupKural.NobetKuralId);
            return View(nobetGrupKural);
        }

        // GET: EczaneNobet/NobetGrupKural/Delete/5
        public ActionResult Delete(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var nobetGrupKural = _nobetGrupKuralService.GetDetayById(id);
            if (nobetGrupKural == null)
            {
                return HttpNotFound();
            }
            return View(nobetGrupKural);
        }

        // POST: EczaneNobet/NobetGrupKural/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var nobetGrupKural = _nobetGrupKuralService.GetDetayById(id);
            _nobetGrupKuralService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
