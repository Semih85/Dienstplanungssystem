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
    public class BayramTurController : Controller
    {
        private IBayramTurService _bayramTurService;

        public BayramTurController(IBayramTurService bayramTurService)
        {
            _bayramTurService = bayramTurService;
        }
        // GET: EczaneNobet/BayramTur
        public ActionResult Index()
        {
            return View(_bayramTurService.GetList().OrderBy(o => o.Id).ToList());
        }

        // GET: EczaneNobet/BayramTur/Details/5
        public ActionResult Details(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BayramTur bayramTur = _bayramTurService.GetById(id);
            if (bayramTur == null)
            {
                return HttpNotFound();
            }
            return View(bayramTur);
        }

        // GET: EczaneNobet/BayramTur/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EczaneNobet/BayramTur/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Adi")] BayramTur bayramTur)
        {
            if (ModelState.IsValid)
            {
                _bayramTurService.Insert(bayramTur);
                return RedirectToAction("Index");
            }

            return View(bayramTur);
        }

        // GET: EczaneNobet/BayramTur/Edit/5
        public ActionResult Edit(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BayramTur bayramTur = _bayramTurService.GetById(id);
            if (bayramTur == null)
            {
                return HttpNotFound();
            }
            return View(bayramTur);
        }

        // POST: EczaneNobet/BayramTur/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Adi")] BayramTur bayramTur)
        {
            if (ModelState.IsValid)
            {
                _bayramTurService.Update(bayramTur);
                return RedirectToAction("Index");
            }
            return View(bayramTur);
        }

        // GET: EczaneNobet/BayramTur/Delete/5
        public ActionResult Delete(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BayramTur bayramTur = _bayramTurService.GetById(id);
            if (bayramTur == null)
            {
                return HttpNotFound();
            }
            return View(bayramTur);
        }

        // POST: EczaneNobet/BayramTur/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BayramTur bayramTur = _bayramTurService.GetById(id);
            _bayramTurService.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
