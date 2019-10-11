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
    [Authorize(Roles = "Admin,Oda,Üst Grup")]
    [HandleError]
    public class NobetAltGrupController : Controller
    {
        private INobetAltGrupService _nobetAltGrupService;
        private INobetGrupGorevTipService _nobetGrupGorevTipService;
        private IUserService _userService;
        private IEczaneNobetGrupAltGrupService _eczaneNobetGrupAltGrupService;
        private INobetUstGrupService _nobetUstGrupService;
        private INobetUstGrupSessionService _nobetUstGrupSessionService;

        public NobetAltGrupController(INobetAltGrupService nobetAltGrupService,
            INobetGrupGorevTipService nobetGrupGorevTipService,
            IUserService userService,
            IEczaneNobetGrupAltGrupService eczaneNobetGrupAltGrupService,
            INobetUstGrupService nobetUstGrupService,
            INobetUstGrupSessionService nobetUstGrupSessionService)
        {
            _nobetAltGrupService = nobetAltGrupService;
            _nobetGrupGorevTipService = nobetGrupGorevTipService;
            _userService = userService;
            _eczaneNobetGrupAltGrupService = eczaneNobetGrupAltGrupService;
            _nobetUstGrupService = nobetUstGrupService;
            _nobetUstGrupSessionService = nobetUstGrupSessionService;
        }
        // GET: EczaneNobet/NobetAltGrup
        public ActionResult Index()
        {
            //var user = _userService.GetByUserName(User.Identity.Name);
            //var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            //var nobetUstGrup = _nobetUstGrupService.GetListByUser(user).FirstOrDefault();
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            //var nobetGruplar = _nobetGrupService.GetListByUser(user).Select(s => s.Id);
            var nobetAltGruplar = _nobetAltGrupService.GetDetaylar(nobetUstGrup.Id);
                //.Where(w => nobetGruplar.Contains(w.NobetGrupId));

            var nobetGruptakiAltGrupSayilari = _eczaneNobetGrupAltGrupService.GetNobetAltGruptakiEczaneSayisi(nobetUstGrup.Id);

            var model = new NobetAltGrupViewModel
            {
                NobetAltGruplar = nobetAltGruplar,
                NobetAltGruptakiEczaneSayilari = nobetGruptakiAltGrupSayilari
            };

            return View(model);
        }

        // GET: EczaneNobet/NobetAltGrup/Details/5
        public ActionResult Details(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NobetAltGrup nobetAltGrup = _nobetAltGrupService.GetById(id);
            if (nobetAltGrup == null)
            {
                return HttpNotFound();
            }
            return View(nobetAltGrup);
        }

        // GET: EczaneNobet/NobetAltGrup/Create
        public ActionResult Create()
        {
            //var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrup.Id).Select(s => s.Id).ToList();

            ViewBag.NobetGrupGorevTipId = new SelectList(_nobetGrupGorevTipService.GetDetaylarByIdList(nobetGrupGorevTipler).Select(s => new MyDrop { Id = s.Id, Value = $"{s.NobetGrupAdi}, {s.NobetGorevTipAdi}" }), "Id", "Value");
            return View();
        }

        // POST: EczaneNobet/NobetAltGrup/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Adi,NobetGrupGorevTipId,BaslamaTarihi,BitisTarihi")] NobetAltGrup nobetAltGrup)
        {
            if (ModelState.IsValid)
            {
                _nobetAltGrupService.Insert(nobetAltGrup);
                return RedirectToAction("Index");
            }

            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrup.Id).Select(s => s.Id).ToList();

            ViewBag.NobetGrupGorevTipId = new SelectList(_nobetGrupGorevTipService.GetDetaylarByIdList(nobetGrupGorevTipler).Select(s => new MyDrop { Id = s.Id, Value = $"{s.NobetGrupAdi}, {s.NobetGorevTipAdi}" }), "Id", "Value", nobetAltGrup.NobetGrupGorevTipId);
            return View(nobetAltGrup);
        }

        // GET: EczaneNobet/NobetAltGrup/Edit/5
        public ActionResult Edit(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NobetAltGrup nobetAltGrup = _nobetAltGrupService.GetById(id);
            if (nobetAltGrup == null)
            {
                return HttpNotFound();
            }
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrup.Id).Select(s => s.Id).ToList();

            ViewBag.NobetGrupGorevTipId = new SelectList(_nobetGrupGorevTipService.GetDetaylarByIdList(nobetGrupGorevTipler).Select(s => new MyDrop { Id = s.Id, Value = $"{s.NobetGrupAdi}, {s.NobetGorevTipAdi}" }), "Id", "Value", nobetAltGrup.NobetGrupGorevTipId);

            return View(nobetAltGrup);
        }

        // POST: EczaneNobet/NobetAltGrup/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Adi,NobetGrupGorevTipId,BaslamaTarihi,BitisTarihi")] NobetAltGrup nobetAltGrup)
        {
            if (ModelState.IsValid)
            {
                _nobetAltGrupService.Update(nobetAltGrup);
                return RedirectToAction("Index");
            }
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrup.Id).Select(s => s.Id).ToList();

            ViewBag.NobetGrupGorevTipId = new SelectList(_nobetGrupGorevTipService.GetDetaylarByIdList(nobetGrupGorevTipler).Select(s => new MyDrop { Id = s.Id, Value = $"{s.NobetGrupAdi}, {s.NobetGorevTipAdi}" }), "Id", "Value", nobetAltGrup.NobetGrupGorevTipId);

            return View(nobetAltGrup);
        }

        // GET: EczaneNobet/NobetAltGrup/Delete/5
        public ActionResult Delete(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NobetAltGrup nobetAltGrup = _nobetAltGrupService.GetById(id);
            if (nobetAltGrup == null)
            {
                return HttpNotFound();
            }
            return View(nobetAltGrup);
        }

        // POST: EczaneNobet/NobetAltGrup/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NobetAltGrup nobetAltGrup = _nobetAltGrupService.GetById(id);
            _nobetAltGrupService.Delete(id);
            return RedirectToAction("Index");
        }

    }
}
