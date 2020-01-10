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
    public class EczaneController : Controller
    {
        private IEczaneService _eczaneService;
        private IUserService _userService;
        private IUserEczaneService _userEczaneService;
        private IEczaneNobetGrupService _eczaneNobetGrupService;
        private INobetUstGrupService _nobetUstGrupService;
        private INobetUstGrupSessionService _nobetUstGrupSessionService;

        public EczaneController(IEczaneService eczaneService,
                                IUserService userService,
                                IUserEczaneService userEczaneService,
                                IEczaneNobetGrupService eczaneNobetGrupService,
                                INobetUstGrupService nobetUstGrupService,
                                INobetUstGrupSessionService nobetUstGrupSessionService)
        {
            _eczaneService = eczaneService;
            _userService = userService;
            _eczaneNobetGrupService = eczaneNobetGrupService;
            _nobetUstGrupService = nobetUstGrupService;
            _nobetUstGrupSessionService = nobetUstGrupSessionService;
            _userEczaneService = userEczaneService;
        }

        // GET: EczaneNobet/Eczane 
        [Authorize(Roles = "Admin,Oda,Üst Grup,Eczane")]
        public ActionResult Index()
        {
            var user = _userService.GetByUserName(User.Identity.Name);
            var ustGrupSession = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var eczaneler = _eczaneService.GetDetaylar(ustGrupSession.Id)
                .OrderBy(o => o.AcilisTarihi).ToList();

            var rolIdler = _userService.GetUserRoles(user).OrderBy(s => s.RoleId).Select(u => u.RoleId).ToArray();
            var rolId = rolIdler.FirstOrDefault();

            var model = eczaneler;

            if (rolIdler.Count() == 1 && rolId == 4)
            {
                var userEczaneler = _userEczaneService.GetDetaylarByUserId(user.Id);

                model = eczaneler.Where(w => userEczaneler.Select(s => s.EczaneId).Contains(w.Id)).ToList();
            }

            return View(model);
        }

        // GET: EczaneNobet/Eczane/Details/5
        public ActionResult Details(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var eczane = _eczaneService.GetDetayById(id);
            if (eczane == null)
            {
                return HttpNotFound();
            }
            return View(eczane);
        }

        // GET: EczaneNobet/Eczane/Create
        [Authorize(Roles = "Admin,Oda,Üst Grup")]
        public ActionResult Create()
        {
            //var eczane = new Eczane { AcilisTarihi = DateTime.Today, Enlem = 0, Boylam = 0 };
            var ustGrupSession = _nobetUstGrupSessionService.GetSession("nobetUstGrup");
            //var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGruplar = _nobetUstGrupService.GetDetaylar(ustGrupSession.Id);
            ViewBag.NobetUstGrupId = new SelectList(nobetUstGruplar.Select(s => new { s.Id, s.Adi }), "Id", "Adi");

            return View();
        }

        // POST: EczaneNobet/Eczane/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Oda,Üst Grup")]
        public ActionResult Create([Bind(Include = "Id,Adi,AcilisTarihi,KapanisTarihi,Enlem,Boylam,Adres,TelefonNo,MailAdresi,WebSitesi,NobetUstGrupId")] Eczane eczane)
        {
            if (ModelState.IsValid)
            {
                _eczaneService.Insert(eczane);
                TempData["EklenenEczane"] = eczane.Adi;

                var eczaneler = new int[1]
                {
                    eczane.Id
                };

                var eczaneNobetGrupCoklu = new EczaneNobetGrupCoklu
                {
                    EczaneId = eczaneler,
                    BaslangicTarihi = eczane.AcilisTarihi,
                };

                TempData["EklenecekEczane"] = eczaneNobetGrupCoklu;

                return RedirectToAction("Create", "EczaneNobetGrup");
                //return RedirectToAction("Index");
            }
            var ustGrupSession = _nobetUstGrupSessionService.GetSession("nobetUstGrup");
            var nobetUstGruplar = _nobetUstGrupService.GetDetaylar(ustGrupSession.Id);
            ViewBag.NobetUstGrupId = new SelectList(nobetUstGruplar, "Id", "Adi", eczane.NobetUstGrupId);

            return View(eczane);
        }

        // GET: EczaneNobet/Eczane/Edit/5
        [Authorize(Roles = "Admin,Oda,Üst Grup")]
        public ActionResult Edit(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Eczane eczane = _eczaneService.GetById(id);
            if (eczane == null)
            {
                return HttpNotFound();
            }
            var ustGrupSession = _nobetUstGrupSessionService.GetSession("nobetUstGrup");
            var nobetUstGruplar = _nobetUstGrupService.GetDetaylar(ustGrupSession.Id);
            ViewBag.NobetUstGrupId = new SelectList(nobetUstGruplar.Select(s => new { s.Id, s.Adi }), "Id", "Adi");
            return View(eczane);
        }

        // POST: EczaneNobet/Eczane/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Oda,Üst Grup")]
        public ActionResult Edit([Bind(Include = "Id,Adi,AcilisTarihi,KapanisTarihi,Enlem,Boylam,Adres,TelefonNo,MailAdresi,WebSitesi, NobetUstGrupId")] Eczane eczane)
        {
            if (ModelState.IsValid)
            {
                _eczaneService.Update(eczane);
                return RedirectToAction("Index");
            }
            var ustGrupSession = _nobetUstGrupSessionService.GetSession("nobetUstGrup");
            var nobetUstGruplar = _nobetUstGrupService.GetDetaylar(ustGrupSession.Id);
            ViewBag.NobetUstGrupId = new SelectList(nobetUstGruplar.Select(s => new { s.Id, s.Adi }), "Id", "Adi");
            return View(eczane);
        }

        // GET: EczaneNobet/Eczane/Delete/5
        [Authorize(Roles = "Admin,Oda,Üst Grup")]
        public ActionResult Delete(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Eczane eczane = _eczaneService.GetById(id);
            if (eczane == null)
            {
                return HttpNotFound();
            }
            return View(eczane);
        }

        // POST: EczaneNobet/Eczane/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Oda,Üst Grup")]
        public ActionResult DeleteConfirmed(int id)
        {
            Eczane eczane = _eczaneService.GetById(id);
            _eczaneService.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult EczanelerDdlPartialView(int nobetGrupId = 0)
        {
            var ustGrupSession = _nobetUstGrupSessionService.GetSession("nobetUstGrup");
            var eczaneler = _eczaneService.GetDetaylar(ustGrupSession.Id).Select(s => s.Id).ToList();

            var eczaneNobetGruplar = _eczaneNobetGrupService.GetDetaylarByEczaneIdList(eczaneler)
                .Where(w => w.NobetGrupId == nobetGrupId || nobetGrupId == 0)
                .Select(s => new MyDrop { Id = s.EczaneId, Value = $"{s.EczaneAdi}, {s.NobetGrupAdi}" }).Distinct()
                .OrderBy(o => o.Value).ToList();

            ViewBag.EczaneId = new SelectList(items: eczaneNobetGruplar, dataValueField: "Id", dataTextField: "Value");

            return PartialView(eczaneNobetGruplar);
        }

        public ActionResult EczanelerSingleDdlPartialView(int nobetGrupId = 0)
        {
            var ustGrupSession = _nobetUstGrupSessionService.GetSession("nobetUstGrup");
            var eczaneler = _eczaneService.GetDetaylar(ustGrupSession.Id).Select(s => s.Id).ToList();

            var eczaneNobetGruplar = _eczaneNobetGrupService.GetDetaylarByEczaneIdList(eczaneler)
                .Where(w => w.NobetGrupId == nobetGrupId || nobetGrupId == 0)
                .Select(s => new MyDrop { Id = s.EczaneId, Value = $"{s.EczaneAdi}, {s.NobetGrupAdi}" }).Distinct()
                .OrderBy(o => o.Value).ToList();

            ViewBag.EczaneId = new SelectList(items: eczaneNobetGruplar, dataValueField: "Id", dataTextField: "Value");

            return PartialView(eczaneNobetGruplar);
        }
    }
}
