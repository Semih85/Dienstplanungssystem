using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WM.Northwind.Entities.Concrete.EczaneNobet;
using WM.UI.Mvc.Models;
using WM.Northwind.Business.Abstract.Authorization;
using WM.Northwind.Business.Abstract.EczaneNobet;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.UI.Mvc.Services;

namespace WM.UI.Mvc.Areas.EczaneNobet.Controllers
{
    [HandleError]
    [Authorize]
    public class DebugEczaneController : Controller
    {
        #region ctor
        private IEczaneService _eczaneService;
        private IDebugEczaneService _debugEczaneService;
        private IUserService _userService;
        private IUserEczaneService _userEczaneService;
        private IEczaneNobetGrupService _eczaneNobetGrupService;
        private INobetUstGrupService _nobetUstGrupService;
        private INobetUstGrupSessionService _nobetUstGrupSessionService;

        public DebugEczaneController(IEczaneService eczaneService,
                                IDebugEczaneService debugEczaneService,
                                IUserService userService,
                                IUserEczaneService userEczaneService,
                                IEczaneNobetGrupService eczaneNobetGrupService,
                                INobetUstGrupService nobetUstGrupService,
                                INobetUstGrupSessionService nobetUstGrupSessionService)
        {
            _eczaneService = eczaneService;
            _debugEczaneService = debugEczaneService;
            _userService = userService;
            _eczaneNobetGrupService = eczaneNobetGrupService;
            _nobetUstGrupService = nobetUstGrupService;
            _nobetUstGrupSessionService = nobetUstGrupSessionService;
            _userEczaneService = userEczaneService;
        }
        #endregion
        // GET: EczaneNobet/Eczane 
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var user = _userService.GetByUserName(User.Identity.Name);

            var debugEczaneler = _debugEczaneService.GetDetaylar()
                .OrderBy(o => o.Id).ToList();

            var model = debugEczaneler;

            return View(model);
        }

        // GET: EczaneNobet/Eczane/Details/5
        public ActionResult Details(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var debugEczane = _debugEczaneService.GetDetayById(id);
            if (debugEczane == null)
            {
                return HttpNotFound();
            }
            return View(debugEczane);
        }

        // GET: EczaneNobet/Eczane/Create
        [Authorize(Roles = "Admin,Oda,Üst Grup")]
        public ActionResult Create()
        {
            var ustGrupSession = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var eczaneNobetgrupIdler = _eczaneNobetGrupService.GetDetaylar(ustGrupSession.Id).OrderBy(s => s.EczaneAdi);

            ViewBag.EczaneNobetGrupId = new SelectList(eczaneNobetgrupIdler.Select(s => new { s.Id, s.EczaneNobetGrupAdi }), "Id", "EczaneNobetGrupAdi");
            return View();
        }

        // POST: EczaneNobet/Eczane/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "Id,EczaneNobetGrupId,AktifMi")] DebugEczane debugEczane)
        {
            if (ModelState.IsValid)
            {
                _debugEczaneService.Insert(debugEczane);

                var eczaneler = new int[1]
                {
                    debugEczane.Id
                };

                return RedirectToAction("Index");
            }

            return View(debugEczane);
        }

        // GET: EczaneNobet/Eczane/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DebugEczane debugEczane = _debugEczaneService.GetById(id);
            if (debugEczane == null)
            {
                return HttpNotFound();
            }
            var eczaneNobetGrubu = _eczaneNobetGrupService.GetDetayById(debugEczane.EczaneNobetGrupId);

            ViewBag.EczaneNobetGrupId = eczaneNobetGrubu;

            return View(debugEczane);
        }

        // POST: EczaneNobet/Eczane/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "Id,EczaneNobetGrupId,AktifMi")] DebugEczane debugEczane)
        {
            if (ModelState.IsValid)
            {
                _debugEczaneService.Update(debugEczane);
                return RedirectToAction("Index");
            }

            return View(debugEczane);
        }

        // GET: EczaneNobet/Eczane/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DebugEczane debugEczane = _debugEczaneService.GetById(id);
            if (debugEczane == null)
            {
                return HttpNotFound();
            }
            return View(debugEczane);
        }

        // POST: EczaneNobet/Eczane/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            DebugEczane debugEczane = _debugEczaneService.GetById(id);
            _debugEczaneService.Delete(id);
            return RedirectToAction("Index");
        }

    }
}

