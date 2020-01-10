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
    public class TakvimController : Controller
    {        
        private ITakvimService _takvimService;
        private IBayramService _bayramService;

        public TakvimController(ITakvimService takvimService,
            IBayramService bayramService)
        {
            _takvimService = takvimService;
            _bayramService = bayramService;
        }
        // GET: EczaneNobet/Takvim
        public ActionResult Index()
        {
            var model = _takvimService.GetList();
            return View(model);
        }

        // GET: EczaneNobet/Takvim/Details/5
        public ActionResult Details(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Takvim takvim =_takvimService.GetById(id);
            if (takvim == null)
            {
                return HttpNotFound();
            }
            return View(takvim);
        }

        // GET: EczaneNobet/Takvim/Create
        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(_bayramService.GetList(), "TakvimId", "TakvimId");
            return View();
        }

        // POST: EczaneNobet/Takvim/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Tarih,Aciklama")] Takvim takvim)
        {
            if (ModelState.IsValid)
            {
                _takvimService.Insert(takvim);
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(_bayramService.GetList(), "TakvimId", "TakvimId", takvim.Id);
            return View(takvim);
        }

        // GET: EczaneNobet/Takvim/Edit/5
        public ActionResult Edit(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Takvim takvim =_takvimService.GetById(id);
            if (takvim == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(_bayramService.GetList(), "TakvimId", "TakvimId", takvim.Id);
            return View(takvim);
        }

        // POST: EczaneNobet/Takvim/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Tarih,Aciklama")] Takvim takvim)
        {
            if (ModelState.IsValid)
            {
                _takvimService.Update(takvim);
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(_bayramService.GetList(), "TakvimId", "TakvimId", takvim.Id);
            return View(takvim);
        }

        // GET: EczaneNobet/Takvim/Delete/5
        public ActionResult Delete(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Takvim takvim =_takvimService.GetById(id);
            if (takvim == null)
            {
                return HttpNotFound();
            }
            return View(takvim);
        }

        // POST: EczaneNobet/Takvim/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Takvim takvim =_takvimService.GetById(id);
            _takvimService.Delete(id);
            return RedirectToAction("Index");
        }
        
    }
}
