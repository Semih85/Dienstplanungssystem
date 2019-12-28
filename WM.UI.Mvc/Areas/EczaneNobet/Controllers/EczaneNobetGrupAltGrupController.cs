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
    [Authorize(Roles = "Admin,Oda,Üst Grup")]
    [HandleError]
    public class EczaneNobetGrupAltGrupController : Controller
    {
        #region ctor
        private IEczaneNobetGrupAltGrupService _eczaneNobetGrupAltGrupService;
        private IEczaneNobetGrupService _eczaneNobetGrupService;
        private INobetAltGrupService _nobetAltGrupService;
        private IUserService _userService;
        private IEczaneService _eczaneService;
        private INobetGrupService _nobetGrupService;
        private INobetGrupGorevTipService _nobetGrupGorevTipService;
        private INobetUstGrupService _nobetUstGrupService;
        private INobetUstGrupSessionService _nobetUstGrupSessionService;

        public EczaneNobetGrupAltGrupController(IEczaneNobetGrupAltGrupService eczaneNobetGrupAltGrupService,
            IEczaneNobetGrupService eczaneNobetGrupService,
            INobetAltGrupService nobetAltGrupService,
            IUserService userService,
            IEczaneService eczaneService,
            INobetGrupService nobetGrupService,
            INobetGrupGorevTipService nobetGrupGorevTipService,
            INobetUstGrupService nobetUstGrupService,
            INobetUstGrupSessionService nobetUstGrupSessionService)
        {
            _eczaneNobetGrupAltGrupService = eczaneNobetGrupAltGrupService;
            _eczaneNobetGrupService = eczaneNobetGrupService;
            _nobetAltGrupService = nobetAltGrupService;
            _userService = userService;
            _eczaneService = eczaneService;
            _nobetGrupService = nobetGrupService;
            _nobetGrupGorevTipService = nobetGrupGorevTipService;
            _nobetUstGrupService = nobetUstGrupService;
            _nobetUstGrupSessionService = nobetUstGrupSessionService;
        }
        #endregion

        // GET: EczaneNobet/EczaneNobetGrupAltGrup
        public ActionResult Index()
        {
            //var user = _userService.GetByUserName(User.Identity.Name);
            //var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user).Select(s => s.Id).ToList();
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var eczaneNobetGrupAltGruplar = _eczaneNobetGrupAltGrupService.GetDetaylar(nobetUstGrup.Id)
                .OrderBy(o => o.NobetGorevTipAdi)
                .ThenBy(o => o.NobetGrupAdi)
                .ThenBy(o => o.NobetAltGrupAdi)
                .ThenBy(o => o.EczaneAdi)
                .ToList();

            var nobetAltGruplar = _nobetAltGrupService.GetDetaylar(nobetUstGrup.Id);

            ViewBag.NobetAltGrupId = new SelectList(nobetAltGruplar, "Id", "Adi");

            return View(eczaneNobetGrupAltGruplar);
        }

        public ActionResult EczaneNobetGrupAltGrupPartial(int nobetGrupId)
        {
            //var user = _userService.GetByUserName(User.Identity.Name);
            //var nobetGruplar = _nobetGrupService.GetListByUser(user).Select(s => s.Id).ToList();
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var eczaneNobetGrupAltGruplar = _eczaneNobetGrupAltGrupService.GetDetaylarByNobetGrupId(nobetGrupId)
                .OrderBy(o => o.NobetAltGrupAdi)
                .ThenBy(o => o.EczaneAdi)
                .ToList();
            //s.Where(w => nobetGruplar.Contains(w.NobetGrupId));

            return PartialView(eczaneNobetGrupAltGruplar);
        }

        public ActionResult GetEczaneNobetGrupAltGruplar(int? nobetAltGrupId)
        {
            nobetAltGrupId = nobetAltGrupId == null ? 0 : nobetAltGrupId;
            //var user = _userService.GetByUserName(User.Identity.Name);
            //var nobetGruplar = _nobetGrupService.GetListByUser(user).Select(s => s.Id).ToList();

            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var eczaneNobetGrupAltGruplar = _eczaneNobetGrupAltGrupService.GetDetaylar(nobetUstGrup.Id, nobetAltGrupId)
                .OrderBy(o => o.NobetAltGrupAdi)
                .ThenBy(o => o.EczaneAdi)
                .ToList();
            //s.Where(w => nobetGruplar.Contains(w.NobetGrupId));

            return PartialView("EczaneNobetGrupAltGrupPartial", eczaneNobetGrupAltGruplar);
        }

        // GET: EczaneNobet/EczaneNobetGrupAltGrup/Details/5
        public ActionResult Details(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var eczaneNobetGrupAltGrup = _eczaneNobetGrupAltGrupService.GetDetayById(id);
            if (eczaneNobetGrupAltGrup == null)
            {
                return HttpNotFound();
            }
            return View(eczaneNobetGrupAltGrup);
        }

        // GET: EczaneNobet/EczaneNobetGrupAltGrup/Create
        public ActionResult Create()
        {
            //var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrup.Id)
                .Select(s => s.Id).ToList();

            var eczaneNobetGrupList = _eczaneNobetGrupService.GetDetaylarByNobetGrupGorevTipler(nobetGrupGorevTipler)
                .Where(w => w.BitisTarihi == null).ToList();

            var eczaneNobetGruplar = _eczaneNobetGrupService.GetMyDrop(eczaneNobetGrupList);

            var nobetAltGruplar = _nobetAltGrupService.GetDetaylar(nobetGrupGorevTipler);

            ViewBag.EczaneNobetGrupId = new SelectList(eczaneNobetGruplar, "Id", "Value");
            ViewBag.NobetAltGrupId = new SelectList(nobetAltGruplar.Select(s => new MyDrop { Id = s.Id, Value = $"{s.Adi}, {s.NobetGorevTipAdi}" }), "Id", "Value");

            return View();
        }

        // POST: EczaneNobet/EczaneNobetGrupAltGrup/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EczaneNobetGrupId,NobetAltGrupId,BaslangicTarihi,BitisTarihi,Aciklama")] EczaneNobetGrupAltGrupCoklu eczaneNobetGrupAltGrupCoklu)
        {
            if (ModelState.IsValid)
            {
                var eczaneNobetGrupAltGruplar = new List<EczaneNobetGrupAltGrup>();

                foreach (var eczaneNobetGrupId in eczaneNobetGrupAltGrupCoklu.EczaneNobetGrupId)
                {
                    eczaneNobetGrupAltGruplar.Add(new EczaneNobetGrupAltGrup
                    {
                        EczaneNobetGrupId = eczaneNobetGrupId,
                        NobetAltGrupId = eczaneNobetGrupAltGrupCoklu.NobetAltGrupId,
                        BaslangicTarihi = eczaneNobetGrupAltGrupCoklu.BaslangicTarihi,
                        BitisTarihi = eczaneNobetGrupAltGrupCoklu.BitisTarihi,
                        Aciklama = eczaneNobetGrupAltGrupCoklu.Aciklama
                    });
                }

                var eklenecekEczaneSayisi = eczaneNobetGrupAltGruplar.Count;

                if (ModelState.IsValid && eklenecekEczaneSayisi > 0)
                {
                    _eczaneNobetGrupAltGrupService.CokluEkle(eczaneNobetGrupAltGruplar);
                    ViewBag.EklenenEczaneSayisi = eklenecekEczaneSayisi;
                    ViewBag.EklenenGrupAdi = _nobetAltGrupService.GetDetayById(eczaneNobetGrupAltGrupCoklu.NobetAltGrupId).Adi;
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
                //_eczaneNobetGrupAltGrupService.Insert(eczaneNobetGrupAltGrup);
                ////return RedirectToAction("Index");
                //return RedirectToAction("EczaneNobetGrupAltGrupPartial");
            }

            //var user = _userService.GetByUserName(User.Identity.Name);

            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrup.Id)
                .Select(s => s.Id).ToList();

            var eczaneNobetGrupList = _eczaneNobetGrupService.GetDetaylarByNobetGrupGorevTipler(nobetGrupGorevTipler)
                .Where(w => w.BitisTarihi == null).ToList();
            
            var eczaneNobetGruplar = _eczaneNobetGrupService.GetMyDrop(eczaneNobetGrupList);

            var nobetAltGruplar = _nobetAltGrupService.GetDetaylar(nobetGrupGorevTipler);

            ViewBag.EczaneNobetGrupId = new SelectList(eczaneNobetGruplar, "Id", "Value", eczaneNobetGrupAltGrupCoklu.EczaneNobetGrupId);
            ViewBag.NobetAltGrupId = new SelectList(nobetAltGruplar.Select(s => new MyDrop { Id = s.Id, Value = $"{s.Adi}, {s.NobetGorevTipAdi}" }), "Id", "Value", eczaneNobetGrupAltGrupCoklu.NobetAltGrupId);

            return View(eczaneNobetGrupAltGrupCoklu);
        }

        public ActionResult CreateAjax(int nobetGrupId)
        {
            var eczaneNobetGruplar = _eczaneNobetGrupService.GetAktifEczaneNobetGrup(nobetGrupId)
                .OrderBy(s => s.EczaneAdi)
                .ThenBy(t => t.NobetGrupAdi)
                .Select(s => new MyDrop
                {
                    Id = s.Id,
                    Value = $"{s.EczaneAdi} ({s.NobetGrupAdi}, {s.NobetGorevTipAdi})"
                });

            var nobetAltGruplar = _nobetAltGrupService.GetDetaylarByNobetGrupId(nobetGrupId);

            ViewBag.NobetGrupId = nobetGrupId;
            ViewBag.EczaneNobetGrupId = new SelectList(eczaneNobetGruplar, "Id", "Value");
            ViewBag.NobetAltGrupId = new SelectList(nobetAltGruplar.Select(s => new MyDrop { Id = s.Id, Value = $"{s.Adi}, {s.NobetGorevTipAdi}" }), "Id", "Value");
            return View();
        }

        // GET: EczaneNobet/EczaneNobetGrupAltGrup/Edit/5
        public ActionResult Edit(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var eczaneNobetGrupAltGrup = _eczaneNobetGrupAltGrupService.GetDetayById(id);
            if (eczaneNobetGrupAltGrup == null)
            {
                return HttpNotFound();
            }
            //var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrup.Id)
                .Select(s => s.Id).ToList();

            var eczaneNobetGrupList = _eczaneNobetGrupService.GetDetaylarByNobetGrupGorevTipler(nobetGrupGorevTipler)
                .Where(w => w.BitisTarihi == null).ToList();

            var eczaneNobetGruplar = _eczaneNobetGrupService.GetMyDrop(eczaneNobetGrupList);

            var nobetAltGruplar = _nobetAltGrupService.GetDetaylar(nobetGrupGorevTipler);

            ViewBag.EczaneNobetGrupId = new SelectList(eczaneNobetGruplar, "Id", "Value", eczaneNobetGrupAltGrup.EczaneNobetGrupId);
            ViewBag.NobetAltGrupId = new SelectList(nobetAltGruplar.Select(s => new MyDrop { Id = s.Id, Value = $"{s.Adi}, {s.NobetGorevTipAdi}" }), "Id", "Value", eczaneNobetGrupAltGrup.NobetAltGrupId);
            return View(eczaneNobetGrupAltGrup);
        }

        // POST: EczaneNobet/EczaneNobetGrupAltGrup/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EczaneNobetGrupId,NobetAltGrupId,BaslangicTarihi,BitisTarihi,Aciklama")] EczaneNobetGrupAltGrup eczaneNobetGrupAltGrup)
        {
            if (ModelState.IsValid)
            {
                _eczaneNobetGrupAltGrupService.Update(eczaneNobetGrupAltGrup);
                return RedirectToAction("Index");
            }
            //var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrup.Id)
                .Select(s => s.Id).ToList();

            var eczaneNobetGrupList = _eczaneNobetGrupService.GetDetaylarByNobetGrupGorevTipler(nobetGrupGorevTipler)
                .Where(w => w.BitisTarihi == null).ToList();

            var eczaneNobetGruplar = _eczaneNobetGrupService.GetMyDrop(eczaneNobetGrupList);

            var nobetAltGruplar = _nobetAltGrupService.GetDetaylar(nobetGrupGorevTipler);

            ViewBag.EczaneNobetGrupId = new SelectList(eczaneNobetGruplar, "Id", "Value", eczaneNobetGrupAltGrup.EczaneNobetGrupId);
            ViewBag.NobetAltGrupId = new SelectList(nobetAltGruplar.Select(s => new MyDrop { Id = s.Id, Value = $"{s.Adi}, {s.NobetGorevTipAdi}" }), "Id", "Value", eczaneNobetGrupAltGrup.NobetAltGrupId);
            return View(eczaneNobetGrupAltGrup);
        }

        // GET: EczaneNobet/EczaneNobetGrupAltGrup/Delete/5
        public ActionResult Delete(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var eczaneNobetGrupAltGrup = _eczaneNobetGrupAltGrupService.GetDetayById(id);
            if (eczaneNobetGrupAltGrup == null)
            {
                return HttpNotFound();
            }
            return View(eczaneNobetGrupAltGrup);
        }

        // POST: EczaneNobet/EczaneNobetGrupAltGrup/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //var eczaneNobetGrupAltGrup = _eczaneNobetGrupAltGrupService.GetDetayById(id);
            _eczaneNobetGrupAltGrupService.Delete(id);
            return RedirectToAction("Index");
        }

    }
}
