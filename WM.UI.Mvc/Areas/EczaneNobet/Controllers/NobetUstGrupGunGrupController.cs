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
using WM.UI.Mvc.Services;

namespace WM.UI.Mvc.Areas.EczaneNobet.Controllers
{
    [Authorize]
    [HandleError]
    public class NobetUstGrupGunGrupController : Controller
    {        
        private INobetUstGrupGunGrupService _nobetUstGrupGunGrupService;
        private IGunGrupService _gunGrupService;
        private INobetUstGrupService _nobetUstGrupService;
        private IUserService _userService;
        private INobetUstGrupSessionService _nobetUstGrupSessionService;

        public NobetUstGrupGunGrupController(INobetUstGrupGunGrupService nobetUstGrupGunGrupService,
            IUserService userService,
            IGunGrupService gunGrupService,
            INobetUstGrupService nobetUstGrupService,
            INobetUstGrupSessionService nobetUstGrupSessionService)
        {
            _nobetUstGrupGunGrupService = nobetUstGrupGunGrupService;
            _gunGrupService = gunGrupService;
            _userService = userService;
            _nobetUstGrupService = nobetUstGrupService;
            _nobetUstGrupSessionService = nobetUstGrupSessionService;
        }

        // GET: EczaneNobet/NobetUstGrupGunGrup
        public ActionResult Index()
        {
            //var user = _userService.GetByUserName(User.Identity.Name);
            //var nobetUstGrup = _nobetUstGrupService.GetListByUser(user).FirstOrDefault();
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var nobetUstGrupGunGruplar = _nobetUstGrupGunGrupService.GetDetaylar(nobetUstGrup.Id);
            return View(nobetUstGrupGunGruplar);
        }

        // GET: EczaneNobet/NobetUstGrupGunGrup/Details/5
        public ActionResult Details(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var nobetUstGrupGunGrup = _nobetUstGrupGunGrupService.GetDetayById(id);

            if (nobetUstGrupGunGrup == null)
            {
                return HttpNotFound();
            }
            return View(nobetUstGrupGunGrup);
        }

        // GET: EczaneNobet/NobetUstGrupGunGrup/Create
        public ActionResult Create()
        {
            //var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");
            var nobetUstGruplar = _nobetUstGrupService.GetDetaylar(nobetUstGrup.Id);

            ViewBag.GunGrupId = new SelectList(_gunGrupService.GetList(), "Id", "Adi");
            ViewBag.NobetUstGrupId = new SelectList(nobetUstGruplar, "Id", "Adi");
            return View();
        }

        // POST: EczaneNobet/NobetUstGrupGunGrup/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,GunGrupId,NobetUstGrupId,Aciklama,AmacFonksiyonuKatsayisi")] NobetUstGrupGunGrup nobetUstGrupGunGrup)
        {
            if (ModelState.IsValid)
            {
                _nobetUstGrupGunGrupService.Insert(nobetUstGrupGunGrup);
                return RedirectToAction("Index");
            }

            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");
            var nobetUstGruplar = _nobetUstGrupService.GetDetaylar(nobetUstGrup.Id);

            ViewBag.GunGrupId = new SelectList(_gunGrupService.GetList(), "Id", "Adi", nobetUstGrupGunGrup.GunGrupId);
            ViewBag.NobetUstGrupId = new SelectList(nobetUstGruplar, "Id", "Adi", nobetUstGrupGunGrup.NobetUstGrupId);
            
            return View(nobetUstGrupGunGrup);
        }

        // GET: EczaneNobet/NobetUstGrupGunGrup/Edit/5
        public ActionResult Edit(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var nobetUstGrupGunGrup = _nobetUstGrupGunGrupService.GetDetayById(id);

            if (nobetUstGrupGunGrup == null)
            {
                return HttpNotFound();
            }

            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");
            var nobetUstGruplar = _nobetUstGrupService.GetDetaylar(nobetUstGrup.Id);

            ViewBag.GunGrupId = new SelectList(_gunGrupService.GetList(), "Id", "Adi", nobetUstGrupGunGrup.GunGrupId);
            ViewBag.NobetUstGrupId = new SelectList(nobetUstGruplar, "Id", "Adi", nobetUstGrupGunGrup.NobetUstGrupId);
            return View(nobetUstGrupGunGrup);
        }

        // POST: EczaneNobet/NobetUstGrupGunGrup/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,GunGrupId,NobetUstGrupId,Aciklama,AmacFonksiyonuKatsayisi")] NobetUstGrupGunGrup nobetUstGrupGunGrup)
        {
            if (ModelState.IsValid)
            {
                _nobetUstGrupGunGrupService.Update(nobetUstGrupGunGrup);
                return RedirectToAction("Index");
            }
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");
            var nobetUstGruplar = _nobetUstGrupService.GetDetaylar(nobetUstGrup.Id);

            ViewBag.GunGrupId = new SelectList(_gunGrupService.GetList(), "Id", "Adi", nobetUstGrupGunGrup.GunGrupId);
            ViewBag.NobetUstGrupId = new SelectList(nobetUstGruplar, "Id", "Adi", nobetUstGrupGunGrup.NobetUstGrupId);
            return View(nobetUstGrupGunGrup);
        }

        // GET: EczaneNobet/NobetUstGrupGunGrup/Delete/5
        public ActionResult Delete(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var nobetUstGrupGunGrup = _nobetUstGrupGunGrupService.GetDetayById(id);

            if (nobetUstGrupGunGrup == null)
            {
                return HttpNotFound();
            }
            return View(nobetUstGrupGunGrup);
        }

        // POST: EczaneNobet/NobetUstGrupGunGrup/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var nobetUstGrupGunGrup = _nobetUstGrupGunGrupService.GetDetayById(id);
            _nobetUstGrupGunGrupService.Delete(id);
            return RedirectToAction("Index");
        }

    }
}
