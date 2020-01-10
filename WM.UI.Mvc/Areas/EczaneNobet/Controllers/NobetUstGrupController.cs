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
    //[Authorize(Roles = "Admin,Oda,Üst Grup")]
    [Authorize]
    public class NobetUstGrupController : Controller
    {
        private INobetUstGrupService _nobetUstGrupService;
        private IEczaneOdaService _eczaneOdaService;
        private IUserService _userService;
        private INobetUstGrupSessionService _nobetUstGrupSessionService;

        public NobetUstGrupController(INobetUstGrupService nobetUstGrupService,
                                      IEczaneOdaService eczaneOdaService,
                                      IUserService userService,
                                      INobetUstGrupSessionService nobetUstGrupSessionService)
        {
            _nobetUstGrupService = nobetUstGrupService;
            _eczaneOdaService = eczaneOdaService;
            _userService = userService;
            _nobetUstGrupSessionService = nobetUstGrupSessionService;
        }

        // GET: EczaneNobet/NobetUstGrup
        [Authorize]
        public ActionResult Index()
        {
            var user = _userService.GetByUserName(User.Identity.Name);
            var rolIdler = _userService.GetUserRoles(user).OrderBy(s => s.RoleId).Select(u => u.RoleId).ToArray();
            var rolId = rolIdler.FirstOrDefault();
            ViewBag.rolId = rolId;
            //yetkili olduğu odalar
            //var eczaneOdalar = _eczaneOdaService.GetListByUser(user);
            //yetkili olduğu nöbet üst gruplar

            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var model = _nobetUstGrupService.GetDetaylar(nobetUstGrup.Id);

            return View(model);
        }

        // GET: EczaneNobet/NobetUstGrup/Details/5
        public ActionResult Details(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NobetUstGrupDetay nobetUstGrup = _nobetUstGrupService.GetDetay(id);
            if (nobetUstGrup == null)
            {
                return HttpNotFound();
            }
            return View(nobetUstGrup);
        }

        // GET: EczaneNobet/NobetUstGrup/Create
        [Authorize(Roles = "Admin,Oda,Üst Grup")]
        public ActionResult Create()
        {
            ViewBag.EczaneOdaId = new SelectList(_eczaneOdaService.GetList(), "Id", "Adi");
            return View();
        }

        // POST: EczaneNobet/NobetUstGrup/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Adi,EczaneOdaId,BaslangicTarihi,BitisTarihi,Enlem,Boylam,TimeLimit,Aciklama,OneedeGosterilecekEnUzakMesafe,BaslamaTarihindenOncekiSonuclarGosterilsinMi")] NobetUstGrup nobetUstGrup)
        {
            if (ModelState.IsValid)
            {
                _nobetUstGrupService.Insert(nobetUstGrup);
                return RedirectToAction("Index");
            }

            ViewBag.EczaneOdaId = new SelectList(_eczaneOdaService.GetList(), "Id", "Adi", nobetUstGrup.EczaneOdaId);
            return View(nobetUstGrup);
        }

        // GET: EczaneNobet/NobetUstGrup/Edit/5
        public ActionResult Edit(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NobetUstGrupDetay nobetUstGrup = _nobetUstGrupService.GetDetay(id);
            if (nobetUstGrup == null)
            {
                return HttpNotFound();
            }
            ViewBag.EczaneOdaId = new SelectList(_eczaneOdaService.GetList(), "Id", "Adi", nobetUstGrup.EczaneOdaId);
            return View(nobetUstGrup);
        }

        // POST: EczaneNobet/NobetUstGrup/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Adi,EczaneOdaId,BaslangicTarihi,BitisTarihi,Enlem,Boylam,TimeLimit,Aciklama,OneedeGosterilecekEnUzakMesafe,BaslamaTarihindenOncekiSonuclarGosterilsinMi")] NobetUstGrup nobetUstGrup)
        {
            if (ModelState.IsValid)
            {
                _nobetUstGrupService.Update(nobetUstGrup);
                return RedirectToAction("Index");
            }
            ViewBag.EczaneOdaId = new SelectList(_eczaneOdaService.GetList(), "Id", "Adi", nobetUstGrup.EczaneOdaId);
            return View(nobetUstGrup);
        }

        // GET: EczaneNobet/NobetUstGrup/Delete/5
        public ActionResult Delete(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NobetUstGrupDetay nobetUstGrup = _nobetUstGrupService.GetDetay(id);
            if (nobetUstGrup == null)
            {
                return HttpNotFound();
            }
            return View(nobetUstGrup);
        }

        // POST: EczaneNobet/NobetUstGrup/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NobetUstGrupDetay nobetUstGrup = _nobetUstGrupService.GetDetay(id);
            _nobetUstGrupService.Delete(id);
            return RedirectToAction("Index");
        }

        public PartialViewResult NobetUstGrupSecPartialView()
        {
            var user = _userService.GetByUserName(User.Identity.Name);

            var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);

            var nobetUstGrupId = nobetUstGruplar.FirstOrDefault().Id;

            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var nobetUstGrupDetay = _nobetUstGrupService.GetDetay(nobetUstGrupId);

            if (nobetUstGrup.Id != 0)
            {
                nobetUstGrupDetay = nobetUstGrup;
            }

            ViewBag.NobetUstGrupId = new SelectList(nobetUstGruplar, "Id", "Adi", nobetUstGrupDetay.Id);

            ViewBag.NobetUstGrupSayisi = nobetUstGruplar.Count;

            _nobetUstGrupSessionService.SetSession(nobetUstGrupDetay, "nobetUstGrup");

            return PartialView();
        }

        public ActionResult NobetUstGrupSec(int nobetUstGrupId, string returnUrl)
        {
            var nobetUstGrupDetay = _nobetUstGrupService.GetDetay(nobetUstGrupId);

            _nobetUstGrupSessionService.SetSession(nobetUstGrupDetay, "nobetUstGrup");

            return (ActionResult)Redirect(returnUrl);
        }
    }
}
