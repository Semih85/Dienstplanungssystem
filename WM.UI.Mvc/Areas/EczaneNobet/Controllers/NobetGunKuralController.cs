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
    [Authorize]
    public class NobetGunKuralController : Controller
    {
        private INobetGunKuralService _nobetGunKuralService;

        public NobetGunKuralController(INobetGunKuralService nobetGunKuralService)
        {
            _nobetGunKuralService = nobetGunKuralService;
        }

        // GET: EczaneNobet/NobetGunKural
        public ActionResult Index()
        {
            var model = _nobetGunKuralService.GetList()
                .OrderBy(o => o.Id);
            return View(model);
        }

        // GET: EczaneNobet/NobetGunKural/Details/5
        public ActionResult Details(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NobetGunKural nobetGunKural = _nobetGunKuralService.GetById(id);
            if (nobetGunKural == null)
            {
                return HttpNotFound();
            }
            return View(nobetGunKural);
        }

        // GET: EczaneNobet/NobetGunKural/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EczaneNobet/NobetGunKural/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Adi,Aciklama")] NobetGunKural nobetGunKural)
        {
            if (ModelState.IsValid)
            {
                _nobetGunKuralService.Insert(nobetGunKural);
                return RedirectToAction("Index");
            }

            return View(nobetGunKural);
        }

        // GET: EczaneNobet/NobetGunKural/Edit/5
        public ActionResult Edit(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NobetGunKural nobetGunKural = _nobetGunKuralService.GetById(id);
            if (nobetGunKural == null)
            {
                return HttpNotFound();
            }
            return View(nobetGunKural);
        }

        // POST: EczaneNobet/NobetGunKural/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Adi,Aciklama")] NobetGunKural nobetGunKural)
        {
            if (ModelState.IsValid)
            {
                _nobetGunKuralService.Update(nobetGunKural);
                return RedirectToAction("Index");
            }
            return View(nobetGunKural);
        }

        // GET: EczaneNobet/NobetGunKural/Delete/5
        public ActionResult Delete(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NobetGunKural nobetGunKural = _nobetGunKuralService.GetById(id);
            if (nobetGunKural == null)
            {
                return HttpNotFound();
            }
            return View(nobetGunKural);
        }

        // POST: EczaneNobet/NobetGunKural/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NobetGunKural nobetGunKural = _nobetGunKuralService.GetById(id);
            _nobetGunKuralService.Delete(id);
            return RedirectToAction("Index");
        }

    }
}
