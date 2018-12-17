using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WM.Northwind.Business.Abstract.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;
using WM.UI.Mvc.Models;

namespace WM.UI.Mvc.Areas.EczaneNobet.Controllers
{
    public class NobetOzelGunController : Controller
    {
        private INobetOzelGunService _nobetOzelGunService;

        public NobetOzelGunController(INobetOzelGunService nobetOzelGunService)
        {
            _nobetOzelGunService = nobetOzelGunService;
        }

        // GET: EczaneNobet/NobetOzelGun
        public ActionResult Index()
        {
            return View(_nobetOzelGunService.GetList());
        }

        // GET: EczaneNobet/NobetOzelGun/Details/5
        public ActionResult Details(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NobetOzelGun nobetOzelGun = _nobetOzelGunService.GetById(id);
            if (nobetOzelGun == null)
            {
                return HttpNotFound();
            }
            return View(nobetOzelGun);
        }

        // GET: EczaneNobet/NobetOzelGun/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EczaneNobet/NobetOzelGun/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Adi")] NobetOzelGun nobetOzelGun)
        {
            if (ModelState.IsValid)
            {
                _nobetOzelGunService.Insert(nobetOzelGun);
                return RedirectToAction("Index");
            }

            return View(nobetOzelGun);
        }

        // GET: EczaneNobet/NobetOzelGun/Edit/5
        public ActionResult Edit(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NobetOzelGun nobetOzelGun = _nobetOzelGunService.GetById(id);
            if (nobetOzelGun == null)
            {
                return HttpNotFound();
            }
            return View(nobetOzelGun);
        }

        // POST: EczaneNobet/NobetOzelGun/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Adi")] NobetOzelGun nobetOzelGun)
        {
            if (ModelState.IsValid)
            {
                _nobetOzelGunService.Update(nobetOzelGun);
                return RedirectToAction("Index");
            }
            return View(nobetOzelGun);
        }

        // GET: EczaneNobet/NobetOzelGun/Delete/5
        public ActionResult Delete(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NobetOzelGun nobetOzelGun = _nobetOzelGunService.GetById(id);
            if (nobetOzelGun == null)
            {
                return HttpNotFound();
            }
            return View(nobetOzelGun);
        }

        // POST: EczaneNobet/NobetOzelGun/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NobetOzelGun nobetOzelGun = _nobetOzelGunService.GetById(id);
            _nobetOzelGunService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
