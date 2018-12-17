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
    [HandleError]
    [Authorize]
    public class EczaneNobetDegisimController : Controller
    {
        #region ctor
        private IEczaneNobetDegisimService _eczaneNobetDegisimService;
        private IUserNobetUstGrupService _userNobetUstGrupService;
        private IUserService _userService;
        private INobetUstGrupService _nobetUstGrupService;
        private IEczaneNobetSonucService _eczaneNobetSonucService;
        private IEczaneNobetGrupService _eczaneNobetGrupService;

        public EczaneNobetDegisimController(IEczaneNobetDegisimService eczaneNobetDegisimService,
            IUserService userService,
            INobetUstGrupService nobetUstGrupService,
            IEczaneNobetSonucService eczaneNobetSonucService,
            IEczaneNobetGrupService eczaneNobetGrupService,
            IUserNobetUstGrupService userNobetUstGrupService)
        {
            _eczaneNobetDegisimService = eczaneNobetDegisimService;
            _userService = userService;
            _nobetUstGrupService = nobetUstGrupService;
            _eczaneNobetSonucService = eczaneNobetSonucService;
            _eczaneNobetGrupService = eczaneNobetGrupService;
            _userNobetUstGrupService = userNobetUstGrupService;
        } 
        #endregion

        // GET: EczaneNobet/EczaneNobetDegisim
        public ActionResult Index()
        {
            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user).Select(s => s.Id);

            var eczaneNobetDegisimler = _eczaneNobetDegisimService.GetDetaylar()
                .Where(w => nobetUstGruplar.Contains(w.NobetUstGrupId));

            return View(eczaneNobetDegisimler);
        }

        // GET: EczaneNobet/EczaneNobetDegisim/Details/5
        public ActionResult Details(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EczaneNobetDegisimDetay eczaneNobetDegisim = _eczaneNobetDegisimService.GetDetayById(id);
            if (eczaneNobetDegisim == null)
            {
                return HttpNotFound();
            }
            return View(eczaneNobetDegisim);
        }

        // GET: EczaneNobet/EczaneNobetDegisim/Create
        public ActionResult Create()
        {
            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user).Select(s => s.Id);
            var nobetUstGrupId = nobetUstGruplar.FirstOrDefault();
            var sonuclar = _eczaneNobetGrupService.GetDetaylar(nobetUstGrupId);

            ViewBag.EczaneNobetGrupId = new SelectList(sonuclar, "Id", "Aciklama");
            ViewBag.EczaneNobetSonucId = new SelectList(sonuclar, "Id", "Id");
            ViewBag.UserId = new SelectList(_userNobetUstGrupService.GetDetaylar(nobetUstGrupId), "Id", "UserName");
            return View();
        }

        // POST: EczaneNobet/EczaneNobetDegisim/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EczaneNobetSonucId,EczaneNobetGrupId,UserId,KayitTarihi,Aciklama")] EczaneNobetDegisim eczaneNobetDegisim)
        {
            if (ModelState.IsValid)
            {
                _eczaneNobetDegisimService.Insert(eczaneNobetDegisim);
                return RedirectToAction("Index");
            }

            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user).Select(s => s.Id);
            var nobetUstGrupId = nobetUstGruplar.FirstOrDefault();
            var sonuclar = _eczaneNobetGrupService.GetDetaylar(nobetUstGrupId);

            ViewBag.EczaneNobetGrupId = new SelectList(sonuclar, "Id", "Aciklama", eczaneNobetDegisim.EczaneNobetGrupId);
            ViewBag.EczaneNobetSonucId = new SelectList(sonuclar, "Id", "Id", eczaneNobetDegisim.EczaneNobetSonucId);
            ViewBag.UserId = new SelectList(_userNobetUstGrupService.GetDetaylar(nobetUstGrupId), "Id", "UserName", eczaneNobetDegisim.UserId);
            return View(eczaneNobetDegisim);
        }

        // GET: EczaneNobet/EczaneNobetDegisim/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var eczaneNobetDegisim = _eczaneNobetDegisimService.GetDetayById(id);
            if (eczaneNobetDegisim == null)
            {
                return HttpNotFound();
            }

            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user).Select(s => s.Id);
            var nobetUstGrupId = nobetUstGruplar.FirstOrDefault();
            var sonuclar = _eczaneNobetGrupService.GetDetaylar(nobetUstGrupId);

            ViewBag.EczaneNobetGrupId = new SelectList(sonuclar, "Id", "Aciklama", eczaneNobetDegisim.EczaneNobetGrupId);
            ViewBag.EczaneNobetSonucId = new SelectList(sonuclar, "Id", "Id", eczaneNobetDegisim.EczaneNobetSonucId);
            ViewBag.UserId = new SelectList(_userNobetUstGrupService.GetDetaylar(nobetUstGrupId), "Id", "UserName", eczaneNobetDegisim.UserId);
            return View(eczaneNobetDegisim);
        }

        // POST: EczaneNobet/EczaneNobetDegisim/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EczaneNobetSonucId,EczaneNobetGrupId,UserId,KayitTarihi,Aciklama")] EczaneNobetDegisim eczaneNobetDegisim)
        {
            if (ModelState.IsValid)
            {
                _eczaneNobetDegisimService.Update(eczaneNobetDegisim);
                return RedirectToAction("Index");
            }

            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user).Select(s => s.Id);
            var nobetUstGrupId = nobetUstGruplar.FirstOrDefault();
            var sonuclar = _eczaneNobetGrupService.GetDetaylar(nobetUstGrupId);

            ViewBag.EczaneNobetGrupId = new SelectList(sonuclar, "Id", "Aciklama", eczaneNobetDegisim.EczaneNobetGrupId);
            ViewBag.EczaneNobetSonucId = new SelectList(sonuclar, "Id", "Id", eczaneNobetDegisim.EczaneNobetSonucId);
            ViewBag.UserId = new SelectList(_userNobetUstGrupService.GetDetaylar(nobetUstGrupId), "Id", "UserName", eczaneNobetDegisim.UserId);
            return View(eczaneNobetDegisim);
        }

        // GET: EczaneNobet/EczaneNobetDegisim/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EczaneNobetDegisimDetay eczaneNobetDegisim = _eczaneNobetDegisimService.GetDetayById(id);
            if (eczaneNobetDegisim == null)
            {
                return HttpNotFound();
            }
            return View(eczaneNobetDegisim);
        }

        // POST: EczaneNobet/EczaneNobetDegisim/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EczaneNobetDegisimDetay eczaneNobetDegisim = _eczaneNobetDegisimService.GetDetayById(id);
            _eczaneNobetDegisimService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
