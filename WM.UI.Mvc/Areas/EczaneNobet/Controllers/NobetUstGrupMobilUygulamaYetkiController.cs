using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WM.Northwind.Business.Abstract.Authorization;
using WM.Northwind.Business.Abstract.EczaneNobet;
using WM.Northwind.Entities.Concrete.Authorization;
using WM.Northwind.Entities.Concrete.EczaneNobet;
using WM.UI.Mvc.Models;
using WM.UI.Mvc.Services;

namespace WM.UI.Mvc.Areas.EczaneNobet.Controllers
{
    [Authorize]
    [HandleError]
    public class NobetUstGrupMobilUygulamaYetkiController : Controller
    {
        private INobetUstGrupMobilUygulamaYetkiService _nobetUstGrupMobilUygulamaYetkiService;
        private INobetUstGrupService _nobetUstGrupService;
        private INobetUstGrupSessionService _nobetUstGrupSessionService;
        private IMobilUygulamaYetkiService _mobilUygulamaYetkiService;

        public NobetUstGrupMobilUygulamaYetkiController(INobetUstGrupMobilUygulamaYetkiService nobetUstGrupMobilUygulamaYetkiService,
                                  INobetUstGrupService nobetUstGrupService,
                                  IMobilUygulamaYetkiService mobilUygulamaYetkiService,
                                    INobetUstGrupSessionService nobetUstGrupSessionService)
        {
            _nobetUstGrupMobilUygulamaYetkiService = nobetUstGrupMobilUygulamaYetkiService;
            _nobetUstGrupService = nobetUstGrupService;
            _nobetUstGrupSessionService = nobetUstGrupSessionService;
            _mobilUygulamaYetkiService = mobilUygulamaYetkiService;
        }

        // GET: EczaneNobet/NobetUstGrupMobilUygulamaYetki
        public ActionResult Index()
        {
            var model = _nobetUstGrupMobilUygulamaYetkiService.GetDetaylar();
            return View(model);
        }



        // GET: EczaneNobet/NobetUstGrupMobilUygulamaYetki/Create
        public ActionResult Create()
        {
         
            ViewBag.MobilUygulamaYetkiId = new SelectList(_mobilUygulamaYetkiService.GetList().Select(s => new { s.Id, s.Adi }), "Id", "Adi");
           
            return View();
        }

        // POST: EczaneNobet/NobetUstGrupMobilUygulamaYetki/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MobilUygulamaYetkiId")] NobetUstGrupMobilUygulamaYetki nobetUstGrupMobilUygulamaYetki)
        {
            if (ModelState.IsValid)
            {
                var ustGrupSession = _nobetUstGrupSessionService.GetSession("nobetUstGrup");
                nobetUstGrupMobilUygulamaYetki.NobetUstGrupId = _nobetUstGrupService.GetById(ustGrupSession.Id).Id;
                _nobetUstGrupMobilUygulamaYetkiService.Insert(nobetUstGrupMobilUygulamaYetki);
                return RedirectToAction("Index");
            }

            ViewBag.MobilUygulamaYetkiId = new SelectList(_mobilUygulamaYetkiService.GetList().Select(s => new { s.Id, s.Adi }), "Id", "Adi", nobetUstGrupMobilUygulamaYetki.MobilUygulamaYetkiId);
            return View(nobetUstGrupMobilUygulamaYetki);
        }

      

   

        // GET: EczaneNobet/NobetUstGrupMobilUygulamaYetki/Delete/5
        public ActionResult Delete(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var nobetUstGrupMobilUygulamaYetki = _nobetUstGrupMobilUygulamaYetkiService.GetDetayById(id);
            if (nobetUstGrupMobilUygulamaYetki == null)
            {
                return HttpNotFound();
            }
            return View(nobetUstGrupMobilUygulamaYetki);
        }

        // POST: EczaneNobet/NobetUstGrupMobilUygulamaYetki/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var nobetUstGrupMobilUygulamaYetki = _nobetUstGrupMobilUygulamaYetkiService.GetDetayById(id);
            _nobetUstGrupMobilUygulamaYetkiService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
