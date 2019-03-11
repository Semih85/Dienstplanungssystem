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
    public class NobetGorevTipController : Controller
    {
        private INobetGorevTipService _nobetGorevTipService;

        public NobetGorevTipController(INobetGorevTipService nobetGorevTipService)
        {
            _nobetGorevTipService = nobetGorevTipService;
        }
        // GET: EczaneNobet/NobetGorevTip
        public ActionResult Index()
        {
            var model = _nobetGorevTipService.GetList()
                .OrderBy(o=>o.Id);

            return View(model);
        }

        // GET: EczaneNobet/NobetGorevTip/Details/5
        public ActionResult Details(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NobetGorevTip nobetGorevTip = _nobetGorevTipService.GetById(id);
            if (nobetGorevTip == null)
            {
                return HttpNotFound();
            }
            return View(nobetGorevTip);
        }

        // GET: EczaneNobet/NobetGorevTip/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EczaneNobet/NobetGorevTip/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Adi,EczaneninAcikOlduguSaatAraligi,NobetSaatAraligi")] NobetGorevTip nobetGorevTip)
        {
            if (ModelState.IsValid)
            {
                _nobetGorevTipService.Insert(nobetGorevTip);
                return RedirectToAction("Index");
            }

            return View(nobetGorevTip);
        }

        // GET: EczaneNobet/NobetGorevTip/Edit/5
        public ActionResult Edit(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NobetGorevTip nobetGorevTip = _nobetGorevTipService.GetById(id);

            if (nobetGorevTip == null)
            {
                return HttpNotFound();
            }
            return View(nobetGorevTip);
        }

        // POST: EczaneNobet/NobetGorevTip/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Adi,EczaneninAcikOlduguSaatAraligi,NobetSaatAraligi")] NobetGorevTip nobetGorevTip)
        {
            if (ModelState.IsValid)
            {
                _nobetGorevTipService.Update(nobetGorevTip);
                return RedirectToAction("Index");
            }
            return View(nobetGorevTip);
        }

        // GET: EczaneNobet/NobetGorevTip/Delete/5
        public ActionResult Delete(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NobetGorevTip nobetGorevTip = _nobetGorevTipService.GetById(id);

            if (nobetGorevTip == null)
            {
                return HttpNotFound();
            }
            return View(nobetGorevTip);
        }

        // POST: EczaneNobet/NobetGorevTip/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NobetGorevTip nobetGorevTip = _nobetGorevTipService.GetById(id);

            _nobetGorevTipService.Delete(id);
            return RedirectToAction("Index");
        }

       
    }
}
