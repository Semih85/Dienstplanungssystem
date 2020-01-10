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
    public class DepoController : Controller
    {
        private IDepoService _depoService;

        public DepoController(IDepoService depoService)
        {
            _depoService = depoService;
        }


        // GET: Optimization/Depoes
        public ActionResult Index()
        {
            var model = _depoService.GetList();

            return View(model);
        }

        public ActionResult DepoPartialViewList()
        {
            var model = _depoService.GetList();

            return PartialView(model);
        }

        // GET: Optimization/Depoes/Details/5
        public ActionResult Details(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Depo depo = _depoService.GetById(id);
            if (depo == null)
            {
                return HttpNotFound();
            }
            return View(depo);
        }

        // GET: Optimization/Depoes/Create
        public ActionResult Create() => View();

        // GET: Optimization/Depoes/Create
        public ActionResult CreateAjax() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAjax(Depo depo)
        {
            if (ModelState.IsValid)
            {
                _depoService.Insert(depo);
                return RedirectToAction("DepoPartialViewList");
            }

            return View(depo);
        }

        // POST: Optimization/Depoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Adi,Talep")] Depo depo)
        {
            if (ModelState.IsValid)
            {
                _depoService.Insert(depo);
                return RedirectToAction("Index");
            }

            return View(depo);
        }

        // GET: Optimization/Depoes/Edit/5
        public ActionResult Edit(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Depo depo = _depoService.GetById(id);
            if (depo == null)
            {
                return HttpNotFound();
            }
            return View(depo);
        }

        // POST: Optimization/Depoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Adi,Talep")] Depo depo)
        {
            if (ModelState.IsValid)
            {
                _depoService.Update(depo);
                return RedirectToAction("Index");
            }
            return View(depo);
        }

        // GET: Optimization/Depoes/Delete/5
        public ActionResult Delete2(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Depo depo = _depoService.GetById(id);
            if (depo == null)
            {
                return HttpNotFound();
            }
            return View(depo);
        }

        public ActionResult Delete(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Depo depo = _depoService.GetById(id);
            if (depo == null)
            {
                return HttpNotFound();
            }

            _depoService.Delete(id);

            var model = _depoService.GetList();
            return PartialView("DepoPartialViewList", model);
        }

        // POST: Optimization/Depoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _depoService.Delete(id);
            return RedirectToAction("Index");
        }


    }
}
