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

namespace WM.UI.Mvc.Areas.EczaneNobet.Controllers
{
    [Authorize]
    [HandleError]
    public class UserEczaneOdaController : Controller
    {
        private IUserEczaneOdaService _userEczaneOdaService;
        private IEczaneOdaService _eczaneOdaService;
        private IUserService _userService;

        public UserEczaneOdaController(IUserEczaneOdaService userEczaneOdaService,
                                       IEczaneOdaService eczaneOdaService,
                                       IUserService userService)
        {
            _userEczaneOdaService = userEczaneOdaService;
            _eczaneOdaService = eczaneOdaService;
            _userService = userService;
        }

        // GET: EczaneNobet/UserEczaneOda
        public ActionResult Index()
        {
            var userEczaneOdalar = _userEczaneOdaService.GetDetaylar();
            return View(userEczaneOdalar);
        }

        // GET: EczaneNobet/UserEczaneOda/Details/5
        public ActionResult Details(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserEczaneOdaDetay userEczaneOda = _userEczaneOdaService.GetDetayById(id);
            if (userEczaneOda == null)
            {
                return HttpNotFound();
            }
            return View(userEczaneOda);
        }

        // GET: EczaneNobet/UserEczaneOda/Create
        public ActionResult Create()
        {
            ViewBag.EczaneOdaId = new SelectList(_eczaneOdaService.GetList().Select(s => new { s.Id, s.Adi }), "Id", "Adi");
            ViewBag.UserId = new SelectList(_userService.GetList().Select(s => new { s.Id, s.UserName }), "Id", "UserName");
            return View();
        }

        // POST: EczaneNobet/UserEczaneOda/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EczaneOdaId,UserId,BaslamaTarihi,BitisTarihi")] UserEczaneOda userEczaneOda)
        {
            if (ModelState.IsValid)
            {
                _userEczaneOdaService.Insert(userEczaneOda);
                return RedirectToAction("Index");
            }

            ViewBag.EczaneOdaId = new SelectList(_eczaneOdaService.GetList().Select(s => new { s.Id, s.Adi }), "Id", "Adi", userEczaneOda.EczaneOdaId);
            ViewBag.UserId = new SelectList(_userService.GetList().Select(s => new { s.Id, s.UserName }), "Id", "UserName", userEczaneOda.UserId);
            return View(userEczaneOda);
        }

        // GET: EczaneNobet/UserEczaneOda/Edit/5
        public ActionResult Edit(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserEczaneOdaDetay userEczaneOda = _userEczaneOdaService.GetDetayById(id);
            if (userEczaneOda == null)
            {
                return HttpNotFound();
            }
            ViewBag.EczaneOdaId = new SelectList(_eczaneOdaService.GetList().Select(s => new { s.Id, s.Adi }), "Id", "Adi", userEczaneOda.EczaneOdaId);
            ViewBag.UserId = new SelectList(_userService.GetList().Select(s => new { s.Id, s.UserName }), "Id", "UserName", userEczaneOda.UserId);
            return View(userEczaneOda);
        }

        // POST: EczaneNobet/UserEczaneOda/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EczaneOdaId,UserId,BaslamaTarihi,BitisTarihi")] UserEczaneOda userEczaneOda)
        {
            if (ModelState.IsValid)
            {
                _userEczaneOdaService.Update(userEczaneOda);
                return RedirectToAction("Index");
            }
            ViewBag.EczaneOdaId = new SelectList(_eczaneOdaService.GetList().Select(s => new { s.Id, s.Adi }), "Id", "Adi", userEczaneOda.EczaneOdaId);
            ViewBag.UserId = new SelectList(_userService.GetList().Select(s => new { s.Id, s.UserName }), "Id", "UserName", userEczaneOda.UserId);
            return View(userEczaneOda);
        }

        // GET: EczaneNobet/UserEczaneOda/Delete/5
        public ActionResult Delete(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserEczaneOdaDetay userEczaneOda = _userEczaneOdaService.GetDetayById(id);
            if (userEczaneOda == null)
            {
                return HttpNotFound();
            }
            return View(userEczaneOda);
        }

        // POST: EczaneNobet/UserEczaneOda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserEczaneOdaDetay userEczaneOda = _userEczaneOdaService.GetDetayById(id);
            _userEczaneOdaService.Delete(id);
            return RedirectToAction("Index");
        }


    }
}
