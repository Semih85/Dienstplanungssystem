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
    [Authorize]
    [HandleError]
    public class NobetGrupGorevTipGunKuralController : Controller
    {
        #region ctor
        private INobetGrupGorevTipGunKuralService _nobetGrupGorevTipGunKuralService;
        private INobetGrupGorevTipService _nobetGrupGorevTipService;
        private INobetGunKuralService _nobetGunKuralService;
        private INobetUstGrupService _nobetUstGrupService;
        private INobetUstGrupGunGrupService _nobetUstGrupGunGrupService;
        private IUserService _userService;
        private INobetUstGrupSessionService _nobetUstGrupSessionService;

        public NobetGrupGorevTipGunKuralController(INobetGrupGorevTipGunKuralService nobetGrupGorevTipGunKuralService,
            INobetUstGrupService nobetUstGrupService,
            IUserService userService,
            INobetGunKuralService nobetGunKuralService,
            INobetGrupGorevTipService nobetGrupGorevTipService,
            INobetUstGrupGunGrupService nobetUstGrupGunGrupService,
            INobetUstGrupSessionService nobetUstGrupSessionService)
        {
            _nobetGrupGorevTipGunKuralService = nobetGrupGorevTipGunKuralService;
            _nobetGrupGorevTipService = nobetGrupGorevTipService;
            _nobetUstGrupService = nobetUstGrupService;
            _userService = userService;
            _nobetGunKuralService = nobetGunKuralService;
            _nobetUstGrupGunGrupService = nobetUstGrupGunGrupService;
            _nobetUstGrupSessionService = nobetUstGrupSessionService;
        }
        #endregion
        // GET: EczaneNobet/NobetGrupGorevTipGunKural
        public ActionResult Index()
        {
            //throw new Exception();

            //var user = _userService.GetByUserName(User.Identity.Name);

            //var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            //var nobetUstGrup = nobetUstGruplar.Select(s => s.Id).ToList();

            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");
            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrup.Id);
            var nobetGrupGorevTipGunKurallarTumu = _nobetGrupGorevTipGunKuralService.GetDetaylar(nobetUstGrup.Id);

            //var nobetGrupGorevTipGunKurallar = _nobetGrupGorevTipGunKuralService.GetDetaylar(nobetUstGrup);

            var nobetGrunKurallar = nobetGrupGorevTipGunKurallarTumu
                 .Select(s => new { s.NobetGunKuralId, s.NobetGunKuralAdi })
                 .Distinct()
                 .OrderBy(o => o.NobetGunKuralId)
                 .ToList();

            ViewBag.NobetGunKuralId = new SelectList(nobetGrunKurallar, "NobetGunKuralId", "NobetGunKuralAdi");
            ViewBag.NobetGrupGorevTipId = new SelectList(nobetGrupGorevTipler, "Id", "NobetGrupGorevTipAdi");

            return View();
        }

        public ActionResult NobetGrupGorevTipGunKuralPartial(int nobetGrupGorevTipId = 0, int nobetGunKuralId = 0)
        {
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrup.Id)
                .Where(w => w.Id == nobetGrupGorevTipId || nobetGrupGorevTipId == 0).ToList();

            var nobetGrupGunKurallar = GetNobetGrupKurallar(nobetGunKuralId, nobetGrupGorevTipler);

            return PartialView(nobetGrupGunKurallar);
        }

        public ActionResult AktifPasifYap(int nobetGrupGorevTipId = 0, int nobetGunKuralId = 0, bool pasifMi = false)
        {
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrup.Id)
                .Where(w => w.Id == nobetGrupGorevTipId || nobetGrupGorevTipId == 0).ToList();

            var nobetGrupGunKurallar = GetNobetGrupKurallar(nobetGunKuralId, nobetGrupGorevTipler);

            _nobetGrupGorevTipGunKuralService.CokluAktifPasifYap(nobetGrupGunKurallar, pasifMi);

            nobetGrupGunKurallar = GetNobetGrupKurallar(nobetGunKuralId, nobetGrupGorevTipler);

            return PartialView("NobetGrupGorevTipGunKuralPartial", nobetGrupGunKurallar);
        }

        private List<NobetGrupGorevTipGunKuralDetay> GetNobetGrupKurallar(int nobetGunKuralId, List<NobetGrupGorevTipDetay> nobetGrupGorevTipler)
        {
            var nobetGrupGunKurallar = _nobetGrupGorevTipGunKuralService.GetDetaylarByNobetGrupGorevTipIdList(nobetGrupGorevTipler.Select(a => a.Id).ToList())
                .Where(w => (w.NobetGunKuralId == nobetGunKuralId || nobetGunKuralId == 0)
                         //&& w.NobetGunKuralId <= 7
                         )
                .OrderBy(s => s.NobetGrupGorevTipId)
                .ThenBy(e => e.NobetGunKuralId).ToList();

            return nobetGrupGunKurallar;
        }

        // GET: EczaneNobet/NobetGrupGorevTipGunKural/Details/5
        public ActionResult Details(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var nobetGrupGorevTipGunKural = _nobetGrupGorevTipGunKuralService.GetDetayById(id);
            if (nobetGrupGorevTipGunKural == null)
            {
                return HttpNotFound();
            }
            return View(nobetGrupGorevTipGunKural);
        }

        // GET: EczaneNobet/NobetGrupGorevTipGunKural/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");
            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrup.Id);
            var nobetUstGrupGunGruplar = _nobetUstGrupGunGrupService.GetDetaylar(nobetUstGrup.Id);

            ViewBag.NobetGrupGorevTipId = new SelectList(nobetGrupGorevTipler, "Id", "NobetGrupGorevTipAdi");
            ViewBag.NobetGunKuralId = new SelectList(_nobetGunKuralService.GetList(), "Id", "Adi");
            ViewBag.NobetUstGrupGunGrupId = new SelectList(nobetUstGrupGunGruplar, "Id", "GunGrupAdi");

            return View();
        }

        // POST: EczaneNobet/NobetGrupGorevTipGunKural/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "Id,NobetGrupGorevTipId,NobetGunKuralId,NobetUstGrupGunGrupId,BaslangicTarihi,BitisTarihi,NobetciSayisi")] NobetGrupGorevTipGunKural nobetGrupGorevTipGunKural)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _nobetGrupGorevTipGunKuralService.Insert(nobetGrupGorevTipGunKural);
                }
                catch (DbUpdateException ex)
                {
                    var hata = ex.InnerException.ToString();

                    string[] dublicateHata = { "Cannot insert dublicate row in object", "with unique index" };

                    var dublicateRowHatasiMi = dublicateHata.Any(h => hata.Contains(h));

                    if (dublicateRowHatasiMi)
                    {
                        throw new Exception("<strong>Bir Nöbet Grubu için iki kural eklenemez...</strong>");
                    }

                    throw ex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return RedirectToAction("Index");
            }

            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");
            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrup.Id);
            var nobetUstGrupGunGruplar = _nobetUstGrupGunGrupService.GetDetaylar(nobetUstGrup.Id);

            ViewBag.NobetGrupGorevTipId = new SelectList(nobetGrupGorevTipler, "Id", "NobetGrupGorevTipAdi", nobetGrupGorevTipGunKural.NobetGrupGorevTipId);
            ViewBag.NobetGunKuralId = new SelectList(_nobetGunKuralService.GetList(), "Id", "Adi", nobetGrupGorevTipGunKural.NobetGunKuralId);
            ViewBag.NobetUstGrupGunGrupId = new SelectList(nobetUstGrupGunGruplar, "Id", "GunGrupAdi", nobetGrupGorevTipGunKural.NobetUstGrupGunGrupId);
            return View(nobetGrupGorevTipGunKural);
        }

        // GET: EczaneNobet/NobetGrupGorevTipGunKural/Edit/5
        [Authorize(Roles = "Admin,Oda,Üst Grup")]
        public ActionResult Edit(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NobetGrupGorevTipGunKural nobetGrupGorevTipGunKural = _nobetGrupGorevTipGunKuralService.GetById(id);
            if (nobetGrupGorevTipGunKural == null)
            {
                return HttpNotFound();
            }
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");
            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrup.Id);
            var nobetUstGrupGunGruplar = _nobetUstGrupGunGrupService.GetDetaylar(nobetUstGrup.Id);

            ViewBag.NobetGrupGorevTipId = new SelectList(nobetGrupGorevTipler, "Id", "NobetGrupGorevTipAdi", nobetGrupGorevTipGunKural.NobetGrupGorevTipId);
            ViewBag.NobetGunKuralId = new SelectList(_nobetGunKuralService.GetList(), "Id", "Adi", nobetGrupGorevTipGunKural.NobetGunKuralId);
            ViewBag.NobetUstGrupGunGrupId = new SelectList(nobetUstGrupGunGruplar, "Id", "GunGrupAdi", nobetGrupGorevTipGunKural.NobetUstGrupGunGrupId);
            return View(nobetGrupGorevTipGunKural);
        }

        // POST: EczaneNobet/NobetGrupGorevTipGunKural/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Oda,Üst Grup")]
        public ActionResult Edit([Bind(Include = "Id,NobetGrupGorevTipId,NobetGunKuralId,NobetUstGrupGunGrupId,BaslangicTarihi,BitisTarihi,NobetciSayisi")] NobetGrupGorevTipGunKural nobetGrupGorevTipGunKural)
        {
            if (ModelState.IsValid)
            {
                _nobetGrupGorevTipGunKuralService.Update(nobetGrupGorevTipGunKural);
                return RedirectToAction("Index");
            }

            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");
            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrup.Id);
            var nobetUstGrupGunGruplar = _nobetUstGrupGunGrupService.GetDetaylar(nobetUstGrup.Id);

            ViewBag.NobetGrupGorevTipId = new SelectList(nobetGrupGorevTipler, "Id", "NobetGrupGorevTipAdi", nobetGrupGorevTipGunKural.NobetGrupGorevTipId);
            ViewBag.NobetGunKuralId = new SelectList(_nobetGunKuralService.GetList(), "Id", "Adi", nobetGrupGorevTipGunKural.NobetGunKuralId);
            ViewBag.NobetUstGrupGunGrupId = new SelectList(nobetUstGrupGunGruplar, "Id", "GunGrupAdi", nobetGrupGorevTipGunKural.NobetUstGrupGunGrupId);
            return View(nobetGrupGorevTipGunKural);
        }

        // GET: EczaneNobet/NobetGrupGorevTipGunKural/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var nobetGrupGorevTipGunKural = _nobetGrupGorevTipGunKuralService.GetDetayById(id);
            if (nobetGrupGorevTipGunKural == null)
            {
                return HttpNotFound();
            }
            return View(nobetGrupGorevTipGunKural);
        }

        // POST: EczaneNobet/NobetGrupGorevTipGunKural/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            var nobetGrupGorevTipGunKural = _nobetGrupGorevTipGunKuralService.GetDetayById(id);
            _nobetGrupGorevTipGunKuralService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
