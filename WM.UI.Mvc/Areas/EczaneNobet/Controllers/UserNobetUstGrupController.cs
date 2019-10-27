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
using WM.Northwind.Entities.Concrete.EczaneNobet;
using WM.UI.Mvc.Models;
using WM.UI.Mvc.Services;

namespace WM.UI.Mvc.Areas.EczaneNobet.Controllers
{
    [Authorize(Roles = "Admin,Oda,Üst Grup")]
    [HandleError]
    public class UserNobetUstGrupController : Controller
    {
        private IUserNobetUstGrupService _userNobetUstGrupService;
        private INobetUstGrupService _nobetUstGrupService;
        private IUserService _userService;
        private INobetUstGrupSessionService _nobetUstGrupSessionService;

        public UserNobetUstGrupController(IUserNobetUstGrupService userNobetUstGrupService,
                                          INobetUstGrupService nobetUstGrupService,
                                          IUserService userService,
                                          INobetUstGrupSessionService nobetUstGrupSessionService)
        {
            _userNobetUstGrupService = userNobetUstGrupService;
            _nobetUstGrupService = nobetUstGrupService;
            _userService = userService;
            _nobetUstGrupSessionService = nobetUstGrupSessionService;
        }

        // GET: EczaneNobet/UserNobetUstGrup
        public ActionResult Index()
        {
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var model = _userNobetUstGrupService.GetDetaylar(nobetUstGrup.Id);

            return View(model);
        }

        // GET: EczaneNobet/UserNobetUstGrup/Details/5
        public ActionResult Details(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userNobetUstGrup = _userNobetUstGrupService.GetDetayById(id);
            if (userNobetUstGrup == null)
            {
                return HttpNotFound();
            }
            return View(userNobetUstGrup);
        }

        // GET: EczaneNobet/UserNobetUstGrup/Create
        public ActionResult Create()
        {
            ViewBag.NobetUstGrupId = new SelectList(_nobetUstGrupService.GetList().Select(s => new { s.Id, s.Adi }), "Id", "Adi");
            ViewBag.UserId = new SelectList(_userService.GetList().Select(s => new { s.Id, s.UserName }), "Id", "UserName");
            return View();
        }

        // POST: EczaneNobet/UserNobetUstGrup/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NobetUstGrupId,UserId,BaslamaTarihi,BitisTarihi")] UserNobetUstGrup userNobetUstGrup)
        {
            if (ModelState.IsValid)
            {
                _userNobetUstGrupService.Insert(userNobetUstGrup);
                return RedirectToAction("Index");
            }

            ViewBag.NobetUstGrupId = new SelectList(_nobetUstGrupService.GetList().Select(s => new { s.Id, s.Adi }), "Id", "Adi", userNobetUstGrup.NobetUstGrupId);
            ViewBag.UserId = new SelectList(_userService.GetList().Select(s => new { s.Id, s.UserName }), "Id", "UserName", userNobetUstGrup.UserId);
            return View(userNobetUstGrup);
        }

        // GET: EczaneNobet/UserNobetUstGrup/Edit/5
        public ActionResult Edit(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userNobetUstGrup = _userNobetUstGrupService.GetDetayById(id);
            if (userNobetUstGrup == null)
            {
                return HttpNotFound();
            }
            ViewBag.NobetUstGrupId = new SelectList(_nobetUstGrupService.GetList().Select(s => new { s.Id, s.Adi }), "Id", "Adi", userNobetUstGrup.NobetUstGrupId);
            ViewBag.UserId = new SelectList(_userService.GetList().Select(s => new { s.Id, s.UserName }), "Id", "UserName", userNobetUstGrup.UserId);
            return View(userNobetUstGrup);
        }

        // POST: EczaneNobet/UserNobetUstGrup/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NobetUstGrupId,UserId,BaslamaTarihi,BitisTarihi")] UserNobetUstGrup userNobetUstGrup)
        {
            if (ModelState.IsValid)
            {
                _userNobetUstGrupService.Update(userNobetUstGrup);
                return RedirectToAction("Index");
            }
            ViewBag.NobetUstGrupId = new SelectList(_nobetUstGrupService.GetList().Select(s => new { s.Id, s.Adi }), "Id", "Adi", userNobetUstGrup.NobetUstGrupId);
            ViewBag.UserId = new SelectList(_userService.GetList().Select(s => new { s.Id, s.UserName }), "Id", "UserName", userNobetUstGrup.UserId);
            return View(userNobetUstGrup);
        }

        // GET: EczaneNobet/UserNobetUstGrup/Delete/5
        public ActionResult Delete(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userNobetUstGrup = _userNobetUstGrupService.GetDetayById(id);
            if (userNobetUstGrup == null)
            {
                return HttpNotFound();
            }
            return View(userNobetUstGrup);
        }

        // POST: EczaneNobet/UserNobetUstGrup/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var userNobetUstGrup = _userNobetUstGrupService.GetDetayById(id);
            _userNobetUstGrupService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
