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
using WM.Northwind.Entities.Concrete.EczaneNobet;
using WM.UI.Mvc.Models;

namespace WM.UI.Mvc.Areas.EczaneNobet.Controllers
{
    [Authorize]
    [HandleError]
    public class NobetDurumController : Controller
    {
        private INobetDurumService _nobetDurumService;
        private INobetAltGrupService _nobetAltGrupService;
        private INobetDurumTipService _nobetDurumTipService;
        private IUserService _userService;
        private INobetUstGrupService _nobetUstGrupService;

        public NobetDurumController(INobetDurumService nobetDurumService,
            INobetDurumTipService nobetDurumTipService,
            INobetAltGrupService nobetAltGrupService,
            IUserService userService,
            INobetUstGrupService nobetUstGrupService)
        {
            _nobetDurumService = nobetDurumService;
            _nobetDurumTipService = nobetDurumTipService;
            _nobetAltGrupService = nobetAltGrupService;
            _userService = userService;
            _nobetUstGrupService = nobetUstGrupService;
        }

        // GET: EczaneNobet/NobetDurum
        public ActionResult Index()
        {
            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            var nobetAltGruplar = _nobetAltGrupService.GetDetaylarByNobetUstGrup(nobetUstGruplar.Select(s => s.Id).ToList());

            ViewBag.NobetAltGrupId = new SelectList(nobetAltGruplar, "Id", "Adi");

            var nobetDurumlar = _nobetDurumService.GetDetaylar(nobetUstGruplar.Select(s => s.Id).ToList())
                .OrderBy(o => o.NobetAltGrupAdi1)
                .ThenBy(o => o.NobetAltGrupAdi2)
                .ThenBy(o => o.NobetAltGrupAdi3).ToList();

            return View(nobetDurumlar);
        }

        // GET: EczaneNobet/NobetDurum/Details/5
        public ActionResult Details(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var nobetDurum = _nobetDurumService.GetDetayById(id);
            if (nobetDurum == null)
            {
                return HttpNotFound();
            }
            return View(nobetDurum);
        }

        // GET: EczaneNobet/NobetDurum/Create
        public ActionResult Create()
        {
            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            var nobetAltGruplar = _nobetAltGrupService.GetDetaylarByNobetUstGrup(nobetUstGruplar.Select(s => s.Id).ToList());

            ViewBag.NobetAltGrupId1 = new SelectList(nobetAltGruplar, "Id", "Adi");
            ViewBag.NobetAltGrupId2 = new SelectList(nobetAltGruplar, "Id", "Adi");
            ViewBag.NobetAltGrupId3 = new SelectList(nobetAltGruplar, "Id", "Adi");

            ViewBag.NobetDurumTipId = new SelectList(_nobetDurumTipService.GetList(), "Id", "Adi");
            return View();
        }

        // POST: EczaneNobet/NobetDurum/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NobetAltGrupId1,NobetAltGrupId2,NobetAltGrupId3,NobetDurumTipId")] NobetDurum nobetDurum)
        {
            if (ModelState.IsValid)
            {
                _nobetDurumService.Insert(nobetDurum);
                return RedirectToAction("Index");
            }

            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            var nobetAltGruplar = _nobetAltGrupService.GetDetaylarByNobetUstGrup(nobetUstGruplar.Select(s => s.Id).ToList());

            ViewBag.NobetAltGrupId1 = new SelectList(nobetAltGruplar, "Id", "Adi", nobetDurum.NobetAltGrupId1);
            ViewBag.NobetAltGrupId2 = new SelectList(nobetAltGruplar, "Id", "Adi", nobetDurum.NobetAltGrupId2);
            ViewBag.NobetAltGrupId3 = new SelectList(nobetAltGruplar, "Id", "Adi", nobetDurum.NobetAltGrupId3);

            ViewBag.NobetDurumTipId = new SelectList(_nobetDurumTipService.GetList(), "Id", "Adi", nobetDurum.NobetDurumTipId);
            return View(nobetDurum);
        }

        // GET: EczaneNobet/NobetDurum/Edit/5
        public ActionResult Edit(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var nobetDurum = _nobetDurumService.GetById(id);
            if (nobetDurum == null)
            {
                return HttpNotFound();
            }
            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            var nobetAltGruplar = _nobetAltGrupService.GetDetaylarByNobetUstGrup(nobetUstGruplar.Select(s => s.Id).ToList());

            ViewBag.NobetAltGrupId1 = new SelectList(nobetAltGruplar, "Id", "Adi", nobetDurum.NobetAltGrupId1);
            ViewBag.NobetAltGrupId2 = new SelectList(nobetAltGruplar, "Id", "Adi", nobetDurum.NobetAltGrupId2);
            ViewBag.NobetAltGrupId3 = new SelectList(nobetAltGruplar, "Id", "Adi", nobetDurum.NobetAltGrupId3);
            ViewBag.NobetDurumTipId = new SelectList(_nobetDurumTipService.GetList(), "Id", "Adi", nobetDurum.NobetDurumTipId);
            return View(nobetDurum);
        }

        // POST: EczaneNobet/NobetDurum/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NobetAltGrupId1,NobetAltGrupId2,NobetAltGrupId3,NobetDurumTipId")] NobetDurum nobetDurum)
        {
            if (ModelState.IsValid)
            {
                _nobetDurumService.Update(nobetDurum);
                return RedirectToAction("Index");
            }
            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            var nobetAltGruplar = _nobetAltGrupService.GetDetaylarByNobetUstGrup(nobetUstGruplar.Select(s => s.Id).ToList());

            ViewBag.NobetAltGrupId1 = new SelectList(nobetAltGruplar, "Id", "Adi", nobetDurum.NobetAltGrupId1);
            ViewBag.NobetAltGrupId2 = new SelectList(nobetAltGruplar, "Id", "Adi", nobetDurum.NobetAltGrupId2);
            ViewBag.NobetAltGrupId3 = new SelectList(nobetAltGruplar, "Id", "Adi", nobetDurum.NobetAltGrupId3);
            ViewBag.NobetDurumTipId = new SelectList(_nobetDurumTipService.GetList(), "Id", "Adi", nobetDurum.NobetDurumTipId);
            return View(nobetDurum);
        }

        // GET: EczaneNobet/NobetDurum/Delete/5
        public ActionResult Delete(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var nobetDurum = _nobetDurumService.GetDetayById(id);
            if (nobetDurum == null)
            {
                return HttpNotFound();
            }
            return View(nobetDurum);
        }

        // POST: EczaneNobet/NobetDurum/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var nobetDurum = _nobetDurumService.GetDetayById(id);
            _nobetDurumService.Delete(id);
            return RedirectToAction("Index");
        }

    }
}
