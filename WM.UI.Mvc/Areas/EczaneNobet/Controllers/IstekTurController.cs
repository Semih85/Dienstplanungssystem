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
    public class IstekTurController : Controller
    {
        private IIstekTurService _istekTurService;

        public IstekTurController(IIstekTurService istekTurService)
        {
            _istekTurService = istekTurService;
        }
        // GET: EczaneNobet/IstekTur
        public ActionResult Index()
        {
            var model = _istekTurService.GetList();
            return View(model);
        }

        // GET: EczaneNobet/IstekTur/Details/5
        public ActionResult Details(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IstekTur istekTur = _istekTurService.GetById(id);
            if (istekTur == null)
            {
                return HttpNotFound();
            }
            return View(istekTur);
        }

        // GET: EczaneNobet/IstekTur/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EczaneNobet/IstekTur/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Adi,Aciklama")] IstekTur istekTur)
        {
            if (ModelState.IsValid)
            {
                _istekTurService.Insert(istekTur);
                return RedirectToAction("Index");
            }

            return View(istekTur);
        }

        // GET: EczaneNobet/IstekTur/Edit/5
        public ActionResult Edit(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IstekTur istekTur = _istekTurService.GetById(id);
            if (istekTur == null)
            {
                return HttpNotFound();
            }
            return View(istekTur);
        }

        // POST: EczaneNobet/IstekTur/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Adi,Aciklama")] IstekTur istekTur)
        {
            if (ModelState.IsValid)
            {
                _istekTurService.Update(istekTur);
                return RedirectToAction("Index");
            }
            return View(istekTur);
        }

        // GET: EczaneNobet/IstekTur/Delete/5
        public ActionResult Delete(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IstekTur istekTur = _istekTurService.GetById(id);
            if (istekTur == null)
            {
                return HttpNotFound();
            }
            return View(istekTur);
        }

        // POST: EczaneNobet/IstekTur/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IstekTur istekTur = _istekTurService.GetById(id);
            _istekTurService.Delete(id);
            return RedirectToAction("Index");
        }

        
    }
}
