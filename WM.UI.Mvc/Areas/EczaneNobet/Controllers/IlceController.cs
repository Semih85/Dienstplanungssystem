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
    public class IlceController : Controller
    {
        private IIlceService _ilceService;
        private ISehirService _sehirService;

        public IlceController(IIlceService ilceService,
                              ISehirService sehirService)
        {
            _ilceService = ilceService;
            _sehirService = sehirService;
        }

        // GET: EczaneNobet/Ilce
        public ActionResult Index()
        {
            var model = _ilceService.GetListDetay();
            return View(model);
        }

        // GET: EczaneNobet/Ilce/Details/5
        public ActionResult Details(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IlceDetay ilce = _ilceService.GetDetayById(id);
            if (ilce == null)
            {
                return HttpNotFound();
            }
            return View(ilce);
        }

        // GET: EczaneNobet/Ilce/Create
        public ActionResult Create()
        {
            ViewBag.SehirId = new SelectList(_sehirService.GetList(), "Id", "Adi");
            return View();
        }

        // POST: EczaneNobet/Ilce/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Adi,SehirId")] Ilce ilce)
        {
            if (ModelState.IsValid)
            {
                _ilceService.Insert(ilce);
                return RedirectToAction("Index");
            }

            ViewBag.SehirId = new SelectList(_sehirService.GetList(), "Id", "Adi", ilce.SehirId);
            return View(ilce);
        }

        // GET: EczaneNobet/Ilce/Edit/5
        public ActionResult Edit(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IlceDetay ilce = _ilceService.GetDetayById(id);
            if (ilce == null)
            {
                return HttpNotFound();
            }
            ViewBag.SehirId = new SelectList(_sehirService.GetList(), "Id", "Adi", ilce.SehirId);
            return View(ilce);
        }

        // POST: EczaneNobet/Ilce/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Adi,SehirId")] Ilce ilce)
        {
            if (ModelState.IsValid)
            {
                _ilceService.Update(ilce);
                return RedirectToAction("Index");
            }
            ViewBag.SehirId = new SelectList(_sehirService.GetList(), "Id", "Adi", ilce.SehirId);
            return View(ilce);
        }

        // GET: EczaneNobet/Ilce/Delete/5
        public ActionResult Delete(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IlceDetay ilce = _ilceService.GetDetayById(id);
            if (ilce == null)
            {
                return HttpNotFound();
            }
            return View(ilce);
        }

        // POST: EczaneNobet/Ilce/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IlceDetay ilce = _ilceService.GetDetayById(id);
            _ilceService.Delete(id);
            return RedirectToAction("Index");
        }

        
    }
}
