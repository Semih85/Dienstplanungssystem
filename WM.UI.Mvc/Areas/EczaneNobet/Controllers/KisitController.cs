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
    [HandleError]
    public class KisitController : Controller
    {
        private IKisitService _kisitService;
        private IKisitKategoriService _kisitKategoriService;

        public KisitController(IKisitService kisitService,
            IKisitKategoriService kisitKategoriService)
        {
            _kisitService = kisitService;
            _kisitKategoriService = kisitKategoriService;
        }
        // GET: EczaneNobet/Kisit
        public ActionResult Index()
        {
            var model = _kisitService.GetDetaylar();

            ViewBag.ToplamVeri = model.Count;
            return View(model);
        }

        // GET: EczaneNobet/Kisit/Details/5
        public ActionResult Details(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kisit kisit = _kisitService.GetById(id);
            if (kisit == null)
            {
                return HttpNotFound();
            }
            return View(kisit);
        }

        // GET: EczaneNobet/Kisit/Create
        public ActionResult Create()
        {
            var kisitKategoriler = _kisitKategoriService.GetList();
            ViewBag.KisitKategoriId = new SelectList(kisitKategoriler, "Id", "Adi");
            return View();
        }

        // POST: EczaneNobet/Kisit/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Adi,AdiGosterilen,Aciklama,OlusturmaTarihi,KisitKategoriId")] Kisit kisit)
        {
            if (ModelState.IsValid)
            {
                _kisitService.Insert(kisit);
                return RedirectToAction("Index");
            }
            var kisitKategoriler = _kisitKategoriService.GetList();
            ViewBag.KisitKategoriId = new SelectList(kisitKategoriler, "Id", "Adi", kisit.KisitKategoriId);
            return View(kisit);
        }

        // GET: EczaneNobet/Kisit/Edit/5
        public ActionResult Edit(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kisit kisit = _kisitService.GetById(id);
            if (kisit == null)
            {
                return HttpNotFound();
            }
            var kisitKategoriler = _kisitKategoriService.GetList();
            ViewBag.KisitKategoriId = new SelectList(kisitKategoriler, "Id", "Adi", kisit.KisitKategoriId);
            return View(kisit);
        }

        // POST: EczaneNobet/Kisit/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Adi,AdiGosterilen,Aciklama,OlusturmaTarihi,KisitKategoriId")] Kisit kisit)
        {
            if (ModelState.IsValid)
            {
                _kisitService.Update(kisit);
                return RedirectToAction("Index");
            }
            var kisitKategoriler = _kisitKategoriService.GetList();
            ViewBag.KisitKategoriId = new SelectList(kisitKategoriler, "Id", "Adi", kisit.KisitKategoriId);
            return View(kisit);
        }

        // GET: EczaneNobet/Kisit/Delete/5
        public ActionResult Delete(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kisit kisit = _kisitService.GetById(id);
            if (kisit == null)
            {
                return HttpNotFound();
            }
            return View(kisit);
        }

        // POST: EczaneNobet/Kisit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kisit kisit = _kisitService.GetById(id);
            _kisitService.Delete(id);
            return RedirectToAction("Index");
        }

    }
}
