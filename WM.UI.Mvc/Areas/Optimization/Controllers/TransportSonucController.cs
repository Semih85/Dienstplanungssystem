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
    public class TransportSonucController : Controller
    {
        ITransportSonucService _tss;
        IDepoService _ds;
        IFabrikaService _fs;

        public TransportSonucController(ITransportSonucService transportSonucService,
            IDepoService depoService,
            IFabrikaService fabrikaService)
        {
            _tss = transportSonucService;
            _ds = depoService;
            _fs = fabrikaService;
        }

        // GET: Optimization/TransportSonuc
        public ActionResult Index()
        {
            //var transportSonucs = db.TransportSonucs.Include(t => t.Depo).Include(t => t.Fabrika);

            var model = new TransportSonucIndexModel()
            {
                TransportSonucDetails = _tss.GetSonucDetails(null),
                TransportNodes = _tss.GetTransportSonucNodes(),
                TransportEdges = _tss.GetTransportSonucEdges()
            };

            return View(model);
        }

        // GET: Optimization/TransportSonuc
        public ActionResult List()
        {
            //var transportSonucs = db.TransportSonucs.Include(t => t.Depo).Include(t => t.Fabrika);

            var model = new TransportSonucIndexModel()
            {
                TransportSonucDetails = _tss.GetSonucDetails(null)
            };

            return View(model);
        }

        // GET: Optimization/TransportSonuc/Details/5
        public ActionResult Details(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransportSonucDetail transportSonuc = _tss.GetSonucDetailsById(id);
            if (transportSonuc == null)
            {
                return HttpNotFound();
            }
            return View(transportSonuc);
        }

        // GET: Optimization/TransportSonuc/Create
        public ActionResult Create()
        {
            ViewBag.DepoId = new SelectList(_ds.GetList(), "Id", "Adi");
            ViewBag.FabrikaId = new SelectList(_fs.GetList(), "Id", "Adi");
            return View();
        }

        // POST: Optimization/TransportSonuc/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DepoId,FabrikaId,Sonuc")] TransportSonuc transportSonuc)
        {
            if (ModelState.IsValid)
            {
                _tss.Insert(transportSonuc);
                return RedirectToAction("Index");
            }

            ViewBag.DepoId = new SelectList(_ds.GetList(), "Id", "Adi", transportSonuc.DepoId);
            ViewBag.FabrikaId = new SelectList(_fs.GetList(), "Id", "Adi", transportSonuc.FabrikaId);
            return View(transportSonuc);
        }

        // GET: Optimization/TransportSonuc/Edit/5
        public ActionResult Edit(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransportSonuc transportSonuc = _tss.GetById(id);
            //TransportSonucDetail transportSonuc = _tss.GetSonucDetailsById(id);
            if (transportSonuc == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepoId = new SelectList(_ds.GetList(), "Id", "Adi", transportSonuc.DepoId);
            ViewBag.FabrikaId = new SelectList(_fs.GetList(), "Id", "Adi", transportSonuc.FabrikaId);
            return View(transportSonuc);
        }

        // POST: Optimization/TransportSonuc/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DepoId,FabrikaId,Sonuc")] TransportSonuc transportSonuc)
        {
            if (ModelState.IsValid)
            {
                _tss.Update(transportSonuc);
                return RedirectToAction("Index");
            }
            ViewBag.DepoId = new SelectList(_ds.GetList(), "Id", "Adi", transportSonuc.DepoId);
            ViewBag.FabrikaId = new SelectList(_fs.GetList(), "Id", "Adi", transportSonuc.FabrikaId);
            return View(transportSonuc);
        }

        // GET: Optimization/TransportSonuc/Delete/5
        public ActionResult Delete(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransportSonucDetail transportSonuc = _tss.GetSonucDetailsById(id);
            if (transportSonuc == null)
            {
                return HttpNotFound();
            }
            return View(transportSonuc);
        }

        // POST: Optimization/TransportSonuc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _tss.GetById(id);
            return RedirectToAction("Index");
        }

    }
}
