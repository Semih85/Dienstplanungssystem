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
    public class NobetSonucDemoTipController : Controller
    {
        private INobetSonucDemoTipService _nobetSonucDemoTipService;

        public NobetSonucDemoTipController(INobetSonucDemoTipService nobetSonucDemoTipService)
        {
            _nobetSonucDemoTipService = nobetSonucDemoTipService;
        }

        // GET: EczaneNobet/NobetSonucDemoTip
        public ActionResult Index()
        {
            var model = _nobetSonucDemoTipService.GetList();
            return View(model);
        }

        // GET: EczaneNobet/NobetSonucDemoTip/Details/5
        public ActionResult Details(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NobetSonucDemoTip nobetSonucDemoTip = _nobetSonucDemoTipService.GetById(id);
            if (nobetSonucDemoTip == null)
            {
                return HttpNotFound();
            }
            return View(nobetSonucDemoTip);
        }

        // GET: EczaneNobet/NobetSonucDemoTip/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EczaneNobet/NobetSonucDemoTip/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Adi,Aciklama")] NobetSonucDemoTip nobetSonucDemoTip)
        {
            if (ModelState.IsValid)
            {
                _nobetSonucDemoTipService.Insert(nobetSonucDemoTip);
                return RedirectToAction("Index");
            }

            return View(nobetSonucDemoTip);
        }

        // GET: EczaneNobet/NobetSonucDemoTip/Edit/5
        public ActionResult Edit(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NobetSonucDemoTip nobetSonucDemoTip = _nobetSonucDemoTipService.GetById(id);
            if (nobetSonucDemoTip == null)
            {
                return HttpNotFound();
            }
            return View(nobetSonucDemoTip);
        }

        // POST: EczaneNobet/NobetSonucDemoTip/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Adi,Aciklama")] NobetSonucDemoTip nobetSonucDemoTip)
        {
            if (ModelState.IsValid)
            {
                _nobetSonucDemoTipService.Update(nobetSonucDemoTip);
                return RedirectToAction("Index");
            }
            return View(nobetSonucDemoTip);
        }

        // GET: EczaneNobet/NobetSonucDemoTip/Delete/5
        public ActionResult Delete(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NobetSonucDemoTip nobetSonucDemoTip = _nobetSonucDemoTipService.GetById(id);
            if (nobetSonucDemoTip == null)
            {
                return HttpNotFound();
            }
            return View(nobetSonucDemoTip);
        }

        // POST: EczaneNobet/NobetSonucDemoTip/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NobetSonucDemoTip nobetSonucDemoTip = _nobetSonucDemoTipService.GetById(id);
            _nobetSonucDemoTipService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
