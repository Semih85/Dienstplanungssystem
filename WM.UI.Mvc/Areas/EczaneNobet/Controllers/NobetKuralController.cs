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
    [Authorize(Roles ="Admin")]
    public class NobetKuralController : Controller
    {
        private INobetKuralService _nobetKuralService;

        public NobetKuralController(INobetKuralService nobetKuralService)
        {
            _nobetKuralService = nobetKuralService;
        }
        // GET: EczaneNobet/NobetKural
        public ActionResult Index()
        {
            var model = _nobetKuralService.GetList();
            return View(model);
        }

        // GET: EczaneNobet/NobetKural/Details/5
        public ActionResult Details(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NobetKural nobetKural = _nobetKuralService.GetById(id);
            if (nobetKural == null)
            {
                return HttpNotFound();
            }
            return View(nobetKural);
        }

        // GET: EczaneNobet/NobetKural/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EczaneNobet/NobetKural/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Adi,Aciklama")] NobetKural nobetKural)
        {
            if (ModelState.IsValid)
            {
                _nobetKuralService.Insert(nobetKural);
                return RedirectToAction("Index");
            }

            return View(nobetKural);
        }

        // GET: EczaneNobet/NobetKural/Edit/5
        public ActionResult Edit(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NobetKural nobetKural = _nobetKuralService.GetById(id);
            if (nobetKural == null)
            {
                return HttpNotFound();
            }
            return View(nobetKural);
        }

        // POST: EczaneNobet/NobetKural/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Adi,Aciklama")] NobetKural nobetKural)
        {
            if (ModelState.IsValid)
            {
                _nobetKuralService.Update(nobetKural);
                return RedirectToAction("Index");
            }
            return View(nobetKural);
        }

        // GET: EczaneNobet/NobetKural/Delete/5
        public ActionResult Delete(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NobetKural nobetKural = _nobetKuralService.GetById(id);
            if (nobetKural == null)
            {
                return HttpNotFound();
            }
            return View(nobetKural);
        }

        // POST: EczaneNobet/NobetKural/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NobetKural nobetKural = _nobetKuralService.GetById(id);
            _nobetKuralService.Delete(id);
            return RedirectToAction("Index");
        }

    }
}
