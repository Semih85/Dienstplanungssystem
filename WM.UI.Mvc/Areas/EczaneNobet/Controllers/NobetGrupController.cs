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
using WM.UI.Mvc.Areas.EczaneNobet.Models;
using WM.UI.Mvc.Models;

namespace WM.UI.Mvc.Areas.EczaneNobet.Controllers
{
    [HandleError]
    [Authorize]
    public class NobetGrupController : Controller
    {
        #region ctor
        private IEczaneOdaService _eczaneOdaService;
        private INobetUstGrupService _nobetUstGrupService;
        private INobetGrupService _nobetGrupService;
        private IUserService _userService;
        private INobetAltGrupService _nobetAltGrupService;

        public NobetGrupController(IEczaneOdaService eczaneOdaService,
                                   INobetUstGrupService nobetUstGrupService,
                                   INobetGrupService nobetGrupService,
                                   IUserService userService,
                                   INobetAltGrupService nobetAltGrupService)
        {
            _eczaneOdaService = eczaneOdaService;
            _nobetUstGrupService = nobetUstGrupService;
            _nobetGrupService = nobetGrupService;
            _userService = userService;
            _nobetAltGrupService = nobetAltGrupService;
        } 
        #endregion

        // GET: EczaneNobet/NobetGrup
        public ActionResult Index()
        {
            var user = _userService.GetByUserName(User.Identity.Name);
            var rolIdler = _userService.GetUserRoles(user).OrderBy(s => s.RoleId).Select(u => u.RoleId).ToArray();
            var rolId = rolIdler.FirstOrDefault();

            var eczaneOdalar = _eczaneOdaService.GetListByUser(user);
            var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            var nobetUstGrup = _nobetUstGrupService.GetListByUser(user).FirstOrDefault();
            var yetkiliNobetGruplar = _nobetGrupService.GetListByUser(user);

            var nobetGruplar = _nobetGrupService.GetDetaylar(yetkiliNobetGruplar.Select(w => w.Id).ToList());

            var nobetGruptakiAltGrupSayilari = _nobetAltGrupService.GetNobetGruptakiAltGrupSayisi(nobetUstGrup.Id);

            ViewBag.EczaneOdaId = new SelectList(eczaneOdalar, "Id", "Adi");
            ViewBag.NobetUstGrupId = new SelectList(nobetUstGruplar, "Id", "Adi");
            //ViewBag.NobetGrupId = new SelectList(nobetGruplar, "Id", "Adi");

            var model = new NobetGrupViewModel()
            {
                EczaneOdalar = eczaneOdalar,
                NobetUstGruplar = nobetUstGruplar,
                NobetGruplar = nobetGruplar,
                NobetGruptakiAltGruplar = nobetGruptakiAltGrupSayilari
            };

            return View(model);
        }

        // GET: EczaneNobet/NobetGrup/Details/5
        public ActionResult Details(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NobetGrupDetay nobetGrup = _nobetGrupService.GetDetayById(id);
            if (nobetGrup == null)
            {
                return HttpNotFound();
            }
            return View(nobetGrup);
        }

        // GET: EczaneNobet/NobetGrup/Create
        public ActionResult Create()
        {
            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);

            ViewBag.NobetUstGrupId = new SelectList(nobetUstGruplar, "Id", "Adi");
            return View();
        }

        // POST: EczaneNobet/NobetGrup/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Adi,NobetUstGrupId,BaslamaTarihi,BitisTarihi")] NobetGrup nobetGrup)
        {
            if (ModelState.IsValid)
            {
                _nobetGrupService.Insert(nobetGrup);
                return RedirectToAction("Index");
            }

            ViewBag.NobetUstGrupId = new SelectList(_nobetUstGrupService.GetList(), "Id", "Adi", nobetGrup.NobetUstGrupId);
            return View(nobetGrup);
        }

        // GET: EczaneNobet/NobetGrup/Edit/5
        public ActionResult Edit(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NobetGrupDetay nobetGrup = _nobetGrupService.GetDetayById(id);
            if (nobetGrup == null)
            {
                return HttpNotFound();
            }
            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);

            ViewBag.NobetUstGrupId = new SelectList(nobetUstGruplar, "Id", "Adi", nobetGrup.NobetUstGrupId);
            return View(nobetGrup);
        }

        // POST: EczaneNobet/NobetGrup/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Adi,NobetUstGrupId,BaslamaTarihi,BitisTarihi")] NobetGrup nobetGrup)
        {
            if (ModelState.IsValid)
            {
                _nobetGrupService.Update(nobetGrup);
                return RedirectToAction("Index");
            }
            ViewBag.NobetUstGrupId = new SelectList(_nobetUstGrupService.GetList(), "Id", "Adi", nobetGrup.NobetUstGrupId);
            return View(nobetGrup);
        }

        // GET: EczaneNobet/NobetGrup/Delete/5
        public ActionResult Delete(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NobetGrupDetay nobetGrup = _nobetGrupService.GetDetayById(id);
            if (nobetGrup == null)
            {
                return HttpNotFound();
            }
            return View(nobetGrup);
        }

        // POST: EczaneNobet/NobetGrup/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NobetGrupDetay nobetGrup = _nobetGrupService.GetDetayById(id);
            _nobetGrupService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
