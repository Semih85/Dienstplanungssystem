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
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;
using WM.UI.Mvc.Models;
using WM.UI.Mvc.Services;

namespace WM.UI.Mvc.Areas.EczaneNobet.Controllers
{
    [Authorize(Roles = "Admin,Oda,Üst Grup")]
    [HandleError]
    public class NobetGrupGorevTipController : Controller
    {
        private INobetGrupGorevTipService _nobetGrupGorevTipService;
        private INobetGorevTipService _nobetGorevTipService;
        private INobetGrupService _nobetGrupService;
        private IUserService _userService;
        private INobetUstGrupService _nobetUstGrupService;
        private INobetUstGrupSessionService _nobetUstGrupSessionService;

        public NobetGrupGorevTipController(INobetGrupGorevTipService nobetGrupGorevTipService,
            INobetGorevTipService nobetGorevTipService,
            INobetGrupService nobetGrupService,
            IUserService userService,
            INobetUstGrupService nobetUstGrupService,
            INobetUstGrupSessionService nobetUstGrupSessionService)
        {
            _nobetGrupGorevTipService = nobetGrupGorevTipService;
            _nobetGorevTipService = nobetGorevTipService;
            _nobetGrupService = nobetGrupService;
            _userService = userService;
            _nobetUstGrupService = nobetUstGrupService;
            _nobetUstGrupSessionService = nobetUstGrupSessionService;
        }

        // GET: EczaneNobet/NobetGrupGorevTip
        public ActionResult Index()
        {
            //var user = _userService.GetByUserName(User.Identity.Name);
            //var nobetGruplar = _nobetGrupService.GetListByUser(user);
            var ustGrupSession = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var model = _nobetGrupGorevTipService.GetDetaylar(ustGrupSession.Id);

            return View(model);
        }

        // GET: EczaneNobet/NobetGrupGorevTip/Details/5
        public ActionResult Details(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NobetGrupGorevTipDetay nobetGrupGorevTip = _nobetGrupGorevTipService.GetDetayById(id);
            if (nobetGrupGorevTip == null)
            {
                return HttpNotFound();
            }
            return View(nobetGrupGorevTip);
        }

        // GET: EczaneNobet/NobetGrupGorevTip/Create
        public ActionResult Create()
        {
            //var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            ViewBag.NobetGorevTipId = new SelectList(_nobetGorevTipService.GetList(), "Id", "Adi");

            ViewBag.NobetGrupId = new SelectList(_nobetGrupService.GetDetaylar(nobetUstGrup.Id), "Id", "Adi");

            return View();
        }

        // POST: EczaneNobet/NobetGrupGorevTip/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NobetGrupId,NobetGorevTipId,BaslamaTarihi,BitisTarihi")] NobetGrupGorevTip nobetGrupGorevTip)
        {
            if (ModelState.IsValid)
            {
                _nobetGrupGorevTipService.Insert(nobetGrupGorevTip);
                return RedirectToAction("Index");
            }
            //var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            ViewBag.NobetGorevTipId = new SelectList(_nobetGorevTipService.GetList(), "Id", "Adi", nobetGrupGorevTip.NobetGorevTipId);
            ViewBag.NobetGrupId = new SelectList(_nobetGrupService.GetDetaylar(nobetUstGrup.Id), "Id", "Adi", nobetGrupGorevTip.NobetGrupId);

            return View(nobetGrupGorevTip);
        }

        // GET: EczaneNobet/NobetGrupGorevTip/Edit/5
        public ActionResult Edit(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NobetGrupGorevTipDetay nobetGrupGorevTip = _nobetGrupGorevTipService.GetDetayById(id);
            if (nobetGrupGorevTip == null)
            {
                return HttpNotFound();
            }
            //var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            ViewBag.NobetGorevTipId = new SelectList(_nobetGorevTipService.GetList(), "Id", "Adi", nobetGrupGorevTip.NobetGorevTipId);

            ViewBag.NobetGrupId = new SelectList(_nobetGrupService.GetDetaylar(nobetUstGrup.Id), "Id", "Adi", nobetGrupGorevTip.NobetGrupId);

            return View(nobetGrupGorevTip);
        }

        // POST: EczaneNobet/NobetGrupGorevTip/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NobetGrupId,NobetGorevTipId,BaslamaTarihi,BitisTarihi")] NobetGrupGorevTip nobetGrupGorevTip)
        {
            if (ModelState.IsValid)
            {
                _nobetGrupGorevTipService.Update(nobetGrupGorevTip);
                return RedirectToAction("Index");
            }
            //var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            ViewBag.NobetGorevTipId = new SelectList(_nobetGorevTipService.GetList(), "Id", "Adi", nobetGrupGorevTip.NobetGorevTipId);
            ViewBag.NobetGrupId = new SelectList(_nobetGrupService.GetDetaylar(nobetUstGrup.Id), "Id", "Adi", nobetGrupGorevTip.NobetGrupId);

            return View(nobetGrupGorevTip);
        }

        // GET: EczaneNobet/NobetGrupGorevTip/Delete/5
        public ActionResult Delete(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NobetGrupGorevTipDetay nobetGrupGorevTip = _nobetGrupGorevTipService.GetDetayById(id);
            if (nobetGrupGorevTip == null)
            {
                return HttpNotFound();
            }
            return View(nobetGrupGorevTip);
        }

        // POST: EczaneNobet/NobetGrupGorevTip/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NobetGrupGorevTipDetay nobetGrupGorevTip = _nobetGrupGorevTipService.GetDetayById(id);
            _nobetGrupGorevTipService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
