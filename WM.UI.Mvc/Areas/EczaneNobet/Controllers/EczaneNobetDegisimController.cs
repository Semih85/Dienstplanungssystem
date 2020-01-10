using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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
    [HandleError]
    [Authorize(Roles = "Admin,Oda,Üst Grup")]
    public class EczaneNobetDegisimController : Controller
    {
        #region ctor
        private IEczaneNobetDegisimService _eczaneNobetDegisimService;
        private IUserNobetUstGrupService _userNobetUstGrupService;
        private IUserService _userService;
        private INobetUstGrupService _nobetUstGrupService;
        private IEczaneNobetSonucService _eczaneNobetSonucService;
        private IEczaneNobetGrupService _eczaneNobetGrupService;
        private INobetUstGrupSessionService _nobetUstGrupSessionService;

        public EczaneNobetDegisimController(IEczaneNobetDegisimService eczaneNobetDegisimService,
            IUserService userService,
            INobetUstGrupService nobetUstGrupService,
            IEczaneNobetSonucService eczaneNobetSonucService,
            IEczaneNobetGrupService eczaneNobetGrupService,
            IUserNobetUstGrupService userNobetUstGrupService,
            INobetUstGrupSessionService nobetUstGrupSessionService)
        {
            _eczaneNobetDegisimService = eczaneNobetDegisimService;
            _userService = userService;
            _nobetUstGrupService = nobetUstGrupService;
            _eczaneNobetSonucService = eczaneNobetSonucService;
            _eczaneNobetGrupService = eczaneNobetGrupService;
            _userNobetUstGrupService = userNobetUstGrupService;
            _nobetUstGrupSessionService = nobetUstGrupSessionService;
        }
        #endregion

        // GET: EczaneNobet/EczaneNobetDegisim
        public ActionResult Index()
        {
            //var user = _userService.GetByUserName(User.Identity.Name);
            //var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user).Select(s => s.Id);
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var eczaneNobetDegisimler = _eczaneNobetDegisimService.GetDetaylar(nobetUstGrup.Id)
                .OrderByDescending(o => o.KayitTarihi).ToList();
            //.Where(w => nobetUstGruplar.Contains(w.NobetUstGrupId));

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
            //var user = _userService.GetByUserName(User.Identity.Name);
            //var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user).Select(s => s.Id);
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");
            var nobetUstGrupId = nobetUstGrup.Id;
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
                try
                {
                    _eczaneNobetDegisimService.Insert(eczaneNobetDegisim);
                }
                catch (DbUpdateException ex)
                {
                    var hata = ex.InnerException.ToString();

                    string[] dublicateHata = { "Cannot insert dublicate row in object", "with unique index" };

                    var dublicateRowHatasiMi = dublicateHata.Any(h => hata.Contains(h));

                    if (dublicateRowHatasiMi)
                    {
                        //throw new Exception("<strong>Bir eczaneye aynı gün için iki istek kaydı eklenemez...</strong>");
                        return PartialView("ErrorDublicateRowPartial");
                    }

                    // throw ex;
                }
                catch (Exception)
                {
                    return PartialView("ErrorPartial");
                    //throw ex;
                }

                return RedirectToAction("Index");
            }

            //var user = _userService.GetByUserName(User.Identity.Name);
            //var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user).Select(s => s.Id);
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");
            var nobetUstGrupId = nobetUstGrup.Id;
            var sonuclar = _eczaneNobetGrupService.GetDetaylar(nobetUstGrupId);

            ViewBag.EczaneNobetGrupId = new SelectList(sonuclar, "Id", "Aciklama", eczaneNobetDegisim.EczaneNobetGrupId);
            ViewBag.EczaneNobetSonucId = new SelectList(sonuclar, "Id", "Id", eczaneNobetDegisim.EczaneNobetSonucId);
            ViewBag.UserId = new SelectList(_userNobetUstGrupService.GetDetaylar(nobetUstGrupId), "Id", "UserName", eczaneNobetDegisim.UserId);
            return View(eczaneNobetDegisim);
        }

        // GET: EczaneNobet/EczaneNobetDegisim/Edit/5
        [Authorize(Roles = "Admin,Oda,Üst Grup")]
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

            //var user = _userService.GetByUserName(User.Identity.Name);
            //var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user).Select(s => s.Id);
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");
            var nobetUstGrupId = nobetUstGrup.Id;
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

            //var user = _userService.GetByUserName(User.Identity.Name);
            //var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user).Select(s => s.Id);
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");
            var nobetUstGrupId = nobetUstGrup.Id;
            var sonuclar = _eczaneNobetGrupService.GetDetaylar(nobetUstGrupId);

            ViewBag.EczaneNobetGrupId = new SelectList(sonuclar, "Id", "Aciklama", eczaneNobetDegisim.EczaneNobetGrupId);
            ViewBag.EczaneNobetSonucId = new SelectList(sonuclar, "Id", "Id", eczaneNobetDegisim.EczaneNobetSonucId);
            ViewBag.UserId = new SelectList(_userNobetUstGrupService.GetDetaylar(nobetUstGrupId), "Id", "UserName", eczaneNobetDegisim.UserId);
            return View(eczaneNobetDegisim);
        }

        // GET: EczaneNobet/EczaneNobetDegisim/Delete/5
        [Authorize(Roles = "Admin,Oda,Üst Grup")]
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
