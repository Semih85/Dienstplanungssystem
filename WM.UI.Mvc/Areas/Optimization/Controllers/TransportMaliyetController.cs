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
using WM.Northwind.Entities.ComplexTypes;
using WM.Northwind.Entities.ComplexTypes.Transport;
using WM.Northwind.Entities.Concrete;
using WM.Northwind.Entities.Concrete.Optimization.Transport;
using WM.UI.Mvc.Areas.Optimization.Models;
using WM.UI.Mvc.Models;

namespace WM.UI.Mvc.Areas.Optimization.Controllers
{
    public class TransportMaliyetController : Controller
    {
        private ITransportMaliyetService _transportMaliyetService;
        private IDepoService _depoService;
        private IFabrikaService _fabrikaService;

        public TransportMaliyetController(ITransportMaliyetService transportMaliyetService, 
            IDepoService depoService, 
            IFabrikaService fabrikaService)
        {
            _transportMaliyetService = transportMaliyetService;
            _depoService = depoService;
            _fabrikaService = fabrikaService;
        }

        // GET: Optimization/TransportMaliyet
        public ActionResult Index()
        {
            //var transportMaliyets = db.TransportMaliyets.Include(t => t.Depo).Include(t => t.Fabrika);

            var model = new TransportMaliyetIndexModel()
            {
               MaliyetDetail = _transportMaliyetService.GetMaliyetDetails(null)
            };

            return View(model);
        }

        // GET: Optimization/TransportMaliyet/Details/5
        public ActionResult Details(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MaliyetDetail transportMaliyet = _transportMaliyetService.GetMaliyetDetailsById(id);
            if (transportMaliyet == null)
            {
                return HttpNotFound();
            }
            return View(transportMaliyet);
        }

        // GET: Optimization/TransportMaliyet/Create
        public ActionResult Create()
        {
            ViewBag.DepoId = new SelectList(_depoService.GetList(), "Id", "Adi");
            ViewBag.FabrikaId = new SelectList(_fabrikaService.GetList(), "Id", "Adi");

            return View();
        }

        // POST: Optimization/TransportMaliyet/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FabrikaId,DepoId,Deger")] TransportMaliyet transportMaliyet)
        {
            if (ModelState.IsValid)
            {
                _transportMaliyetService.Insert(transportMaliyet);
                return RedirectToAction("Index");
            }

            ViewBag.DepoId = new SelectList(_depoService.GetList(), "Id", "Adi", transportMaliyet.DepoId);
            ViewBag.FabrikaId = new SelectList(_fabrikaService.GetList(), "Id", "Adi", transportMaliyet.FabrikaId);

            return View(transportMaliyet);
        }

        // GET: Optimization/TransportMaliyet/Edit/5
        public ActionResult Edit(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransportMaliyet transportMaliyet = _transportMaliyetService.GetById(id);
            if (transportMaliyet == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepoId = new SelectList(_depoService.GetList(), "Id", "Adi", transportMaliyet.DepoId);
            ViewBag.FabrikaId = new SelectList(_fabrikaService.GetList(), "Id", "Adi", transportMaliyet.FabrikaId);
            return View(transportMaliyet);
        }

        // POST: Optimization/TransportMaliyet/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FabrikaId,DepoId,Deger")] TransportMaliyet transportMaliyet)
        {
            if (ModelState.IsValid)
            {
                _transportMaliyetService.Update(transportMaliyet);
                return RedirectToAction("Index");
            }
            ViewBag.DepoId = new SelectList(_depoService.GetList(), "Id", "Adi", transportMaliyet.DepoId);
            ViewBag.FabrikaId = new SelectList(_fabrikaService.GetList(), "Id", "Adi", transportMaliyet.FabrikaId);

            return View(transportMaliyet);
        }

        // GET: Optimization/TransportMaliyet/Delete/5
        public ActionResult Delete(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MaliyetDetail transportMaliyet = _transportMaliyetService.GetMaliyetDetailsById(id);
            if (transportMaliyet == null)
            {
                return HttpNotFound();
            }
            return View(transportMaliyet);
        }

        // POST: Optimization/TransportMaliyet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _transportMaliyetService.Delete(id);
            return RedirectToAction("Index");
        }

    }
}
