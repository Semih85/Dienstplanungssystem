using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WM.Northwind.Business.Abstract;
using WM.Northwind.Business.Abstract.Transport;
using WM.Northwind.Entities.Concrete;
using WM.Northwind.Entities.Concrete.Transport;
using WM.UI.Mvc.Models;

namespace WM.UI.Mvc.Areas.Optimization.Controllers
{
    public class FabrikaController : Controller
    {

        private IFabrikaService _fabrikaService;

        public FabrikaController(IFabrikaService fabrikaService)
        {
            _fabrikaService = fabrikaService;
        }

        // GET: Optimization/Fabrika
        public ActionResult Index()
        {
            var model = _fabrikaService.GetList();

            return View(model);
        }

        // GET: Optimization/Fabrika/Details/5
        public ActionResult Details(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fabrika fabrika = _fabrikaService.GetById(id);
            if (fabrika == null)
            {
                return HttpNotFound();
            }
            return View(fabrika);
        }

        // GET: Optimization/Fabrika/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Optimization/Fabrika/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Adi,Kapasite")] Fabrika fabrika)
        {
            if (ModelState.IsValid)
            {
                _fabrikaService.Insert(fabrika);
                return RedirectToAction("Index");
            }

            return View(fabrika);
        }

        // GET: Optimization/Fabrika/Edit/5
        public ActionResult Edit(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fabrika fabrika = _fabrikaService.GetById(id);
            if (fabrika == null)
            {
                return HttpNotFound();
            }
            return View(fabrika);
        }

        // POST: Optimization/Fabrika/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Adi,Kapasite")] Fabrika fabrika)
        {
            if (ModelState.IsValid)
            {
                _fabrikaService.Update(fabrika);
                return RedirectToAction("Index");
            }
            return View(fabrika);
        }

        // GET: Optimization/Fabrika/Delete/5
        public ActionResult Delete(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fabrika fabrika = _fabrikaService.GetById(id);
            if (fabrika == null)
            {
                return HttpNotFound();
            }
            return View(fabrika);
        }

        // POST: Optimization/Fabrika/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Fabrika fabrika = _fabrikaService.GetById(id);
            _fabrikaService.Delete(id);
            return RedirectToAction("Index");
        }

    }
}
