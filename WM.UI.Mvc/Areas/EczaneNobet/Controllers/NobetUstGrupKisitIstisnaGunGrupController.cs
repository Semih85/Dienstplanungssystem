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
    [Authorize]
    [HandleError]
    public class NobetUstGrupKisitIstisnaGunGrupController : Controller
    {
        private INobetUstGrupKisitIstisnaGunGrupService _nobetUstGrupKisitIstisnaGunGrupService;
        private INobetUstGrupKisitService _nobetUstGrupKisitService;
        private INobetUstGrupGunGrupService _nobetUstGrupGunGrupService;
        private INobetUstGrupSessionService _nobetUstGrupSessionService;

        public NobetUstGrupKisitIstisnaGunGrupController(INobetUstGrupKisitIstisnaGunGrupService nobetUstGrupKisitIstisnaGunGrupService,
                                     INobetUstGrupKisitService nobetUstGrupKisitService,
                                     INobetUstGrupGunGrupService nobetUstGrupGunGrupService,
                                            INobetUstGrupSessionService nobetUstGrupSessionService)
        {
            _nobetUstGrupKisitIstisnaGunGrupService = nobetUstGrupKisitIstisnaGunGrupService;
            _nobetUstGrupKisitService = nobetUstGrupKisitService;
            _nobetUstGrupSessionService = nobetUstGrupSessionService;
            _nobetUstGrupGunGrupService = nobetUstGrupGunGrupService;
        }
        // GET: EczaneNobet/NobetUstGrupKisitIstisnaGunGrup
        public ActionResult Index()
        {
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var model = _nobetUstGrupKisitIstisnaGunGrupService.GetDetaylar(nobetUstGrup.Id);

            //var menuAltRoles = db.MenuAltRoles.Include(m => m.MenuAlt).Include(m => m.Role);
            return View(model);
        }

        // GET: EczaneNobet/NobetUstGrupKisitIstisnaGunGrup/Details/5
        public ActionResult Details(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var nobetUstGrupKisitIstisnaGunGrup = _nobetUstGrupKisitIstisnaGunGrupService.GetById(id);
            var menuAltRoleDetay = _nobetUstGrupKisitIstisnaGunGrupService.GetDetaylar()
                .Where(s => s.Id == nobetUstGrupKisitIstisnaGunGrup.Id).SingleOrDefault();

            if (nobetUstGrupKisitIstisnaGunGrup == null)
            {
                return HttpNotFound();
            }
            return View(menuAltRoleDetay);
        }

        // GET: EczaneNobet/NobetUstGrupKisitIstisnaGunGrup/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var nobetUstGrupKisitlar = _nobetUstGrupKisitService.GetDetaylar(nobetUstGrup.Id);

            var nobetUstGrupGunGruplar = _nobetUstGrupGunGrupService.GetDetaylar(nobetUstGrup.Id);

            var nobetUstGrupKisitDrop = nobetUstGrupKisitlar
                .Select(s => new MyDrop { Id = s.Id, Value = $"{s.KisitTanimKisa}" })
                .OrderBy(w => w.Value)
                .ToList();

            var nobetUstGrupGunGrupDrop = nobetUstGrupGunGruplar
                .Select(s => new MyDrop { Id = s.Id, Value = $"{s.GunGrupAdi}" });

            ViewBag.NobetUstGrupKisitId = new SelectList(nobetUstGrupKisitDrop, "Id", "Value");
            ViewBag.NobetUstGrupGunGrupId = new SelectList(nobetUstGrupGunGrupDrop, "Id", "Value");

            return View();
        }

        // POST: EczaneNobet/NobetUstGrupKisitIstisnaGunGrup/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "Id,NobetUstGrupKisitId,NobetUstGrupGunGrupId,BaslangicTarihi,BitisTarihi,Aciklama")] NobetUstGrupKisitIstisnaGunGrup nobetUstGrupKisitIstisnaGunGrup)
        {
            if (ModelState.IsValid)
            {
                _nobetUstGrupKisitIstisnaGunGrupService.Insert(nobetUstGrupKisitIstisnaGunGrup);
                return RedirectToAction("Index");
            }

            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var nobetUstGrupKisitlar = _nobetUstGrupKisitService.GetDetaylar(nobetUstGrup.Id);

            var nobetUstGrupGunGruplar = _nobetUstGrupGunGrupService.GetDetaylar(nobetUstGrup.Id);

            var nobetUstGrupKisitDrop = nobetUstGrupKisitlar
                .Select(s => new MyDrop { Id = s.Id, Value = $"{s.KisitTanimKisa}" })
                .OrderBy(w => w.Value)
                .ToList();

            var nobetUstGrupGunGrupDrop = nobetUstGrupGunGruplar
                .Select(s => new MyDrop { Id = s.Id, Value = $"{s.GunGrupAdi}" });

            ViewBag.NobetUstGrupKisitId = new SelectList(nobetUstGrupKisitDrop, "Id", "Value");
            ViewBag.NobetUstGrupGunGrupId = new SelectList(nobetUstGrupGunGrupDrop, "Id", "Value");

            return View(nobetUstGrupKisitIstisnaGunGrup);
        }

        // GET: EczaneNobet/NobetUstGrupKisitIstisnaGunGrup/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var nobetUstGrupKisitIstisnaGunGrup = _nobetUstGrupKisitIstisnaGunGrupService.GetById(id);
            if (nobetUstGrupKisitIstisnaGunGrup == null)
            {
                return HttpNotFound();
            }
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var nobetUstGrupKisitlar = _nobetUstGrupKisitService.GetDetaylar(nobetUstGrup.Id);

            var nobetUstGrupGunGruplar = _nobetUstGrupGunGrupService.GetDetaylar(nobetUstGrup.Id);

            var nobetUstGrupKisitDrop = nobetUstGrupKisitlar
                .Select(s => new MyDrop { Id = s.Id, Value = $"{s.KisitTanimKisa}" })
                .OrderBy(w => w.Value)
                .ToList();

            var nobetUstGrupGunGrupDrop = nobetUstGrupGunGruplar
                .Select(s => new MyDrop { Id = s.Id, Value = $"{s.GunGrupAdi}" });

            ViewBag.NobetUstGrupKisitId = new SelectList(nobetUstGrupKisitDrop, "Id", "Value");
            ViewBag.NobetUstGrupGunGrupId = new SelectList(nobetUstGrupGunGrupDrop, "Id", "Value");

            return View(nobetUstGrupKisitIstisnaGunGrup);
        }

        // POST: EczaneNobet/NobetUstGrupKisitIstisnaGunGrup/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "Id,NobetUstGrupKisitId,NobetUstGrupGunGrupId,BaslangicTarihi,BitisTarihi,Aciklama")] NobetUstGrupKisitIstisnaGunGrup nobetUstGrupKisitIstisnaGunGrup)
        {
            if (ModelState.IsValid)
            {
                _nobetUstGrupKisitIstisnaGunGrupService.Update(nobetUstGrupKisitIstisnaGunGrup);
                return RedirectToAction("Index");
            }
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var nobetUstGrupKisitlar = _nobetUstGrupKisitService.GetDetaylar(nobetUstGrup.Id);

            var nobetUstGrupGunGruplar = _nobetUstGrupGunGrupService.GetDetaylar(nobetUstGrup.Id);

            var nobetUstGrupKisitDrop = nobetUstGrupKisitlar
                .Select(s => new MyDrop { Id = s.Id, Value = $"{s.KisitTanimKisa}" })
                .OrderBy(w => w.Value)
                .ToList();

            var nobetUstGrupGunGrupDrop = nobetUstGrupGunGruplar
                .Select(s => new MyDrop { Id = s.Id, Value = $"{s.GunGrupAdi}" });

            ViewBag.NobetUstGrupKisitId = new SelectList(nobetUstGrupKisitDrop, "Id", "Value");
            ViewBag.NobetUstGrupGunGrupId = new SelectList(nobetUstGrupGunGrupDrop, "Id", "Value");

            return View(nobetUstGrupKisitIstisnaGunGrup);
        }

        // GET: EczaneNobet/NobetUstGrupKisitIstisnaGunGrup/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var nobetUstGrupKisitIstisnaGunGrup = _nobetUstGrupKisitIstisnaGunGrupService.GetById(id);
            var menuAltRoleDetay = _nobetUstGrupKisitIstisnaGunGrupService.GetDetaylar().Where(s => s.Id == nobetUstGrupKisitIstisnaGunGrup.Id).SingleOrDefault();
            if (nobetUstGrupKisitIstisnaGunGrup == null)
            {
                return HttpNotFound();
            }
            return View(menuAltRoleDetay);
        }

        // POST: EczaneNobet/NobetUstGrupKisitIstisnaGunGrup/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            var nobetUstGrupKisitIstisnaGunGrup = _nobetUstGrupKisitIstisnaGunGrupService.GetById(id);

            _nobetUstGrupKisitIstisnaGunGrupService.Delete(id);

            return RedirectToAction("Index");
        }       
    }
}
