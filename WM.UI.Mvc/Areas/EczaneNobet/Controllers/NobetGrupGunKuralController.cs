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
    public class NobetGrupGunKuralController : Controller
    {
        private INobetGrupGunKuralService _nobetGrupGunKuralService;
        private INobetGunKuralService _nobetGunKuralService;
        private INobetGrupService _nobetGrupService;
        private IUserService _userService;

        public NobetGrupGunKuralController(INobetGrupGunKuralService nobetGrupGunKuralService,
            INobetGunKuralService nobetGunKuralService,
            INobetGrupService nobetGrupService,
            IUserService userService)
        {
            _nobetGrupGunKuralService = nobetGrupGunKuralService;
            _nobetGunKuralService = nobetGunKuralService;
            _nobetGrupService = nobetGrupService;
            _userService = userService;
        }

        // GET: EczaneNobet/NobetGrupGunKural
        public ActionResult Index()
        {
            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetGruplar = _nobetGrupService.GetListByUser(user);
            var nobetGunKurallar = _nobetGunKuralService.GetList()
                .Select(s => new MyDrop { Id = s.Id, Value = s.Adi })
                .OrderBy(o => o.Id);

            ViewBag.NobetGrupId = new SelectList(items: nobetGruplar, dataValueField: "Id", dataTextField: "Adi");
            ViewBag.NobetGunKuralId = new SelectList(items: nobetGunKurallar, dataValueField: "Id", dataTextField: "Value");

            return View();
        }

        public ActionResult NobetGrupGunKuralPartial(int nobetGrupId = 0, int nobetGunKuralId = 0)
        {
            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetGruplar = _nobetGrupService.GetListByUser(user)
                .Where(w => w.Id == nobetGrupId || nobetGrupId == 0).ToList();

            var nobetGrupGunKurallar = GetNobetGrupKurallar(nobetGunKuralId, nobetGruplar);

            return PartialView(nobetGrupGunKurallar);
        }

        public ActionResult AktifPasifYap(int nobetGrupId = 0, int nobetGunKuralId = 0, bool pasifMi = false)
        {
            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetGruplar = _nobetGrupService.GetListByUser(user)
                .Where(w => w.Id == nobetGrupId || nobetGrupId == 0).ToList();

            var nobetGrupGunKurallar = GetNobetGrupKurallar(nobetGunKuralId, nobetGruplar);

            _nobetGrupGunKuralService.CokluAktifPasifYap(nobetGrupGunKurallar, pasifMi);

            nobetGrupGunKurallar = GetNobetGrupKurallar(nobetGunKuralId, nobetGruplar);

            return PartialView("NobetGrupGunKuralPartial", nobetGrupGunKurallar);
        }

        private List<NobetGrupGunKuralDetay> GetNobetGrupKurallar(int nobetGunKuralId, List<NobetGrup> nobetGruplar)
        {
            var nobetGrupGunKurallar = _nobetGrupGunKuralService.GetDetaylar()
                .Where(w => nobetGruplar.Select(a => a.Id).Contains(w.NobetGrupId)
                         && (w.NobetGunKuralId == nobetGunKuralId || nobetGunKuralId == 0)
                         //&& w.NobetGunKuralId <= 7
                         )
                .OrderBy(s => s.NobetGrupId).ThenBy(e => e.NobetGunKuralId).ToList();
            return nobetGrupGunKurallar;
        }

        // GET: EczaneNobet/NobetGrupGunKural/Details/5
        public ActionResult Details(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NobetGrupGunKuralDetay nobetGrupGunKural = _nobetGrupGunKuralService.GetDetayById(id);
            if (nobetGrupGunKural == null)
            {
                return HttpNotFound();
            }
            return View(nobetGrupGunKural);
        }

        // GET: EczaneNobet/NobetGrupGunKural/Create
        public ActionResult Create()
        {
            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetGruplar = _nobetGrupService.GetListByUser(user);

            ViewBag.NobetGrupId = new SelectList(nobetGruplar.Select(s => new { s.Id, s.Adi }), "Id", "Adi");
            ViewBag.NobetGunKuralId = new SelectList(_nobetGunKuralService.GetList().OrderBy(s => s.Adi), "Id", "Adi");
            return View();
        }

        // POST: EczaneNobet/NobetGrupGunKural/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NobetGrupId,NobetGunKuralId,BaslangicTarihi,BitisTarihi")] NobetGrupGunKural nobetGrupGunKural)
        {
            if (ModelState.IsValid)
            {
                _nobetGrupGunKuralService.Insert(nobetGrupGunKural);
                return RedirectToAction("Index");
            }

            ViewBag.NobetGrupId = new SelectList(_nobetGrupService.GetList().Select(s => new { s.Id, s.Adi }), "Id", "Adi", nobetGrupGunKural.NobetGrupId);
            ViewBag.NobetGunKuralId = new SelectList(_nobetGunKuralService.GetList().OrderBy(s => s.Adi), "Id", "Adi", nobetGrupGunKural.NobetGunKuralId);
            return View(nobetGrupGunKural);
        }

        // GET: EczaneNobet/NobetGrupGunKural/Edit/5
        public ActionResult Edit(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NobetGrupGunKuralDetay nobetGrupGunKural = _nobetGrupGunKuralService.GetDetayById(id);
            if (nobetGrupGunKural == null)
            {
                return HttpNotFound();
            }
            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetGruplar = _nobetGrupService.GetListByUser(user);
            ViewBag.NobetGrupId = new SelectList(nobetGruplar.Select(s => new { s.Id, s.Adi }), "Id", "Adi", nobetGrupGunKural.NobetGrupId);
            ViewBag.NobetGunKuralId = new SelectList(_nobetGunKuralService.GetList().OrderBy(s => s.Adi).OrderBy(s => s.Adi), "Id", "Adi", nobetGrupGunKural.NobetGunKuralId);
            return View(nobetGrupGunKural);
        }

        // POST: EczaneNobet/NobetGrupGunKural/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NobetGrupId,NobetGunKuralId,BaslangicTarihi,BitisTarihi")] NobetGrupGunKural nobetGrupGunKural)
        {
            if (ModelState.IsValid)
            {
                _nobetGrupGunKuralService.Update(nobetGrupGunKural);
                return RedirectToAction("Index");
            }
            ViewBag.NobetGrupId = new SelectList(_nobetGrupService.GetList().Select(s => new { s.Id, s.Adi }), "Id", "Adi", nobetGrupGunKural.NobetGrupId);
            ViewBag.NobetGunKuralId = new SelectList(_nobetGunKuralService.GetList().OrderBy(s => s.Adi), "Id", "Adi", nobetGrupGunKural.NobetGunKuralId);
            return View(nobetGrupGunKural);
        }

        // GET: EczaneNobet/NobetGrupGunKural/Delete/5
        public ActionResult Delete(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NobetGrupGunKuralDetay nobetGrupGunKural = _nobetGrupGunKuralService.GetDetayById(id);
            if (nobetGrupGunKural == null)
            {
                return HttpNotFound();
            }
            return View(nobetGrupGunKural);
        }

        // POST: EczaneNobet/NobetGrupGunKural/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NobetGrupGunKuralDetay nobetGrupGunKural = _nobetGrupGunKuralService.GetDetayById(id);
            _nobetGrupGunKuralService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
