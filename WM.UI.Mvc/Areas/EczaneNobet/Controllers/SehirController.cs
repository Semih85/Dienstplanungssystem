using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WM.Northwind.Business.Abstract.EczaneNobet;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;
using WM.UI.Mvc.Models;

namespace WM.UI.Mvc.Areas.EczaneNobet.Controllers
{
    [Authorize]
    public class SehirController : Controller
    {
        private ISehirService _sehirService;
        private IEczaneOdaService _eczaneOdaService;

        public SehirController(ISehirService sehirService,
                               IEczaneOdaService eczaneOdaService)
        {
            _sehirService = sehirService;
            _eczaneOdaService = eczaneOdaService;
        }
        // GET: EczaneNobet/Sehir
        public ActionResult Index()
        {
            var model = _sehirService.GetDetaylar();
            return View(model);
        }

        // GET: EczaneNobet/Sehir/Details/5
        public ActionResult Details(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SehirDetay sehir = _sehirService.GetDetayById(id);
            if (sehir == null)
            {
                return HttpNotFound();
            }
            return View(sehir);
        }

        // GET: EczaneNobet/Sehir/Create
        public ActionResult Create()
        {
            ViewBag.EczaneOdaId = new SelectList(_eczaneOdaService.GetList(), "Id", "Adi");
            return View();
        }

        // POST: EczaneNobet/Sehir/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Adi,EczaneOdaId")] Sehir sehir)
        {
            if (ModelState.IsValid)
            {
                _sehirService.Insert(sehir);
                return RedirectToAction("Index");
            }

            ViewBag.EczaneOdaId = new SelectList(_eczaneOdaService.GetList(), "Id", "Adi", sehir.EczaneOdaId);
            return View(sehir);
        }

        // GET: EczaneNobet/Sehir/Edit/5
        public ActionResult Edit(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SehirDetay sehir = _sehirService.GetDetayById(id);
            if (sehir == null)
            {
                return HttpNotFound();
            }
            ViewBag.EczaneOdaId = new SelectList(_eczaneOdaService.GetList(), "Id", "Adi", sehir.EczaneOdaId);
            return View(sehir);
        }

        // POST: EczaneNobet/Sehir/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Adi,EczaneOdaId")] Sehir sehir)
        {
            if (ModelState.IsValid)
            {
                _sehirService.Update(sehir);
                return RedirectToAction("Index");
            }
            ViewBag.EczaneOdaId = new SelectList(_eczaneOdaService.GetList(), "Id", "Adi", sehir.EczaneOdaId);
            return View(sehir);
        }

        // GET: EczaneNobet/Sehir/Delete/5
        public ActionResult Delete(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SehirDetay sehir = _sehirService.GetDetayById(id);
            if (sehir == null)
            {
                return HttpNotFound();
            }
            return View(sehir);
        }

        // POST: EczaneNobet/Sehir/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sehir sehir = _sehirService.GetById(id);
            _sehirService.Delete(id);
            return RedirectToAction("Index");
        }
        
    }
}
