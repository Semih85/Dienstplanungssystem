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
    [Authorize(Roles = "Admin,Oda,Üst Grup")]
    [HandleError]
    public class IstekController : Controller
    {
        private IIstekService _istekService;
        private IIstekTurService _istekTurService;

        public IstekController(IIstekService istekService,
                               IIstekTurService istekTurService)
        {
            _istekService = istekService;
            _istekTurService = istekTurService;
        }
        // GET: EczaneNobet/Istek
        public ActionResult Index()
        {
            var model = _istekService.GetList();
            return View(model);
        }

        // GET: EczaneNobet/Istek/Details/5
        public ActionResult Details(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Istek istek = _istekService.GetById(id);
            if (istek == null)
            {
                return HttpNotFound();
            }
            return View(istek);
        }

        // GET: EczaneNobet/Istek/Create
        public ActionResult Create()
        {
            ViewBag.IstekTurId = new SelectList(_istekTurService.GetList().Select(s => new { s.Id, s.Adi }), "Id", "Adi");
            return View();
        }

        // POST: EczaneNobet/Istek/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Adi,IstekTurId")] Istek istek)
        {
            if (ModelState.IsValid)
            {
                _istekService.Insert(istek);
                return RedirectToAction("Index");
            }

            ViewBag.IstekTurId = new SelectList(_istekTurService.GetList().Select(s => new { s.Id, s.Adi }), "Id", "Adi", istek.IstekTurId);
            return View(istek);
        }

        // GET: EczaneNobet/Istek/Edit/5
        public ActionResult Edit(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Istek istek = _istekService.GetById(id);
            if (istek == null)
            {
                return HttpNotFound();
            }
            ViewBag.IstekTurId = new SelectList(_istekTurService.GetList().Select(s => new { s.Id, s.Adi }), "Id", "Adi", istek.IstekTurId);
            return View(istek);
        }

        // POST: EczaneNobet/Istek/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Adi,IstekTurId")] Istek istek)
        {
            if (ModelState.IsValid)
            {
                _istekService.Update(istek);
                return RedirectToAction("Index");
            }
            ViewBag.IstekTurId = new SelectList(_istekTurService.GetList().Select(s => new { s.Id, s.Adi }), "Id", "Adi", istek.IstekTurId);
            return View(istek);
        }

        // GET: EczaneNobet/Istek/Delete/5
        public ActionResult Delete(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Istek istek = _istekService.GetById(id);
            if (istek == null)
            {
                return HttpNotFound();
            }
            return View(istek);
        }

        // POST: EczaneNobet/Istek/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Istek istek = _istekService.GetById(id);
            _istekService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
