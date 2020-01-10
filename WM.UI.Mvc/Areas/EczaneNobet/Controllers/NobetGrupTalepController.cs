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
    [HandleError]
    [Authorize(Roles = "Admin,Oda,Üst Grup")]
    public class NobetGrupTalepController : Controller
    {
        private INobetGrupTalepService _nobetGrupTalepService;
        private INobetGrupGorevTipService _nobetGrupGorevTipService;
        private ITakvimService _takvimService;
        private IUserService _userService;
        private INobetGrupService _nobetGrupService;
        private INobetUstGrupService _nobetUstGrupService;
        private INobetUstGrupSessionService _nobetUstGrupSessionService;

        public NobetGrupTalepController(INobetGrupTalepService nobetGrupTalepService,
            INobetGrupGorevTipService nobetGrupGorevTipService,
            ITakvimService takvimService,
            IUserService userService,
            INobetGrupService nobetGrupService,
            INobetUstGrupService nobetUstGrupService,
            INobetUstGrupSessionService nobetUstGrupSessionService)
        {
            _nobetGrupTalepService = nobetGrupTalepService;
            _nobetGrupGorevTipService = nobetGrupGorevTipService;
            _takvimService = takvimService;
            _userService = userService;
            _nobetGrupService = nobetGrupService;
            _nobetUstGrupService = nobetUstGrupService;
            _nobetUstGrupSessionService = nobetUstGrupSessionService;
        }
        // GET: EczaneNobet/NobetGrupTalep
        public ActionResult Index()
        {
            //var user = _userService.GetByUserName(User.Identity.Name);
            //var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            var ustGrupSession = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var nobetGrupTalepler = _nobetGrupTalepService.GetDetaylar(ustGrupSession.Id);

            return View(nobetGrupTalepler);
        }

        // GET: EczaneNobet/NobetGrupTalep/Details/5
        public ActionResult Details(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NobetGrupTalepDetay nobetGrupTalep = _nobetGrupTalepService.GetDetayById(id);
            if (nobetGrupTalep == null)
            {
                return HttpNotFound();
            }
            return View(nobetGrupTalep);
        }

        // GET: EczaneNobet/NobetGrupTalep/Create
        public ActionResult Create()
        {
            //user
            //var user = _userService.GetByUserName(User.Identity.Name);
            //var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            //var nobetUstGrup = _nobetUstGrupService.GetListByUser(user).FirstOrDefault();
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var tarih = DateTime.Today;
            //var yil = DateTime.Now.AddMonths(1).Year;
            //var ay = DateTime.Now.AddMonths(1).Month;

            var tarihler = _takvimService.GetList()
                            .Where(w => w.Tarih >= tarih)
                            .Select(s => new MyDrop { Id = s.Id, Value = s.Tarih.ToLongDateString() });

            ViewBag.NobetGrupGorevTipId = new SelectList(_nobetGrupGorevTipService.GetDetaylar(nobetUstGrup.Id)
                .Select(s => new { s.Id, Adi = $"{s.NobetGrupAdi}, {s.NobetGorevTipAdi}" }), "Id", "Adi");
            ViewBag.TakvimId = new SelectList(tarihler, "Id", "Value");

            return View();
        }

        // POST: EczaneNobet/NobetGrupTalep/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TakvimId,NobetGrupGorevTipId,NobetciSayisi")] NobetGrupTalep nobetGrupTalep)
        {
            if (ModelState.IsValid)
            {
                _nobetGrupTalepService.Insert(nobetGrupTalep);
                return RedirectToAction("Index");
            }

            //var user = _userService.GetByUserName(User.Identity.Name);
            //var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            //var nobetUstGrup = _nobetUstGrupService.GetListByUser(user).FirstOrDefault();
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            ViewBag.NobetGrupGorevTipId = new SelectList(_nobetGrupGorevTipService.GetDetaylar(nobetUstGrup.Id)
                .Select(s => new { s.Id, Adi = $"{s.NobetGrupAdi}, {s.NobetGorevTipAdi}" }), "Id", "Adi", nobetGrupTalep.NobetGrupGorevTipId);
            ViewBag.TakvimId = new SelectList(_takvimService.GetList(), "Id", "Tarih", nobetGrupTalep.TakvimId);
            return View(nobetGrupTalep);
        }

        // GET: EczaneNobet/NobetGrupTalep/Edit/5
        public ActionResult Edit(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NobetGrupTalepDetay nobetGrupTalep = _nobetGrupTalepService.GetDetayById(id);
            if (nobetGrupTalep == null)
            {
                return HttpNotFound();
            }
            var tarih = DateTime.Today;
            //var yil = DateTime.Now.AddMonths(1).Year;
            //var ay = DateTime.Now.AddMonths(1).Month;

            var tarihler = _takvimService.GetList()
                            .Where(w => w.Tarih >= tarih)
                            .Select(s => new MyDrop { Id = s.Id, Value = s.Tarih.ToLongDateString() });

            //var user = _userService.GetByUserName(User.Identity.Name);
            //var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            //var nobetUstGrup = _nobetUstGrupService.GetListByUser(user).FirstOrDefault();
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            ViewBag.NobetGrupGorevTipId = new SelectList(_nobetGrupGorevTipService.GetDetaylar(nobetUstGrup.Id)
                .Select(s => new { s.Id, Adi = $"{s.NobetGrupAdi}, {s.NobetGorevTipAdi}" }), "Id", "Adi", nobetGrupTalep.NobetGrupGorevTipId);
            ViewBag.TakvimId = new SelectList(tarihler, "Id", "Value", nobetGrupTalep.TakvimId);
            return View(nobetGrupTalep);
        }

        // POST: EczaneNobet/NobetGrupTalep/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TakvimId,NobetGrupGorevTipId,NobetciSayisi")] NobetGrupTalep nobetGrupTalep)
        {
            if (ModelState.IsValid)
            {
                _nobetGrupTalepService.Update(nobetGrupTalep);
                return RedirectToAction("Index");
            }
            //var user = _userService.GetByUserName(User.Identity.Name);
            //var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            //var nobetUstGrup = _nobetUstGrupService.GetListByUser(user).FirstOrDefault();
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            ViewBag.NobetGrupGorevTipId = new SelectList(_nobetGrupGorevTipService.GetDetaylar(nobetUstGrup.Id)
                .Select(s => new { s.Id, Adi = $"{s.NobetGrupAdi}, {s.NobetGorevTipAdi}" }), "Id", "Adi", nobetGrupTalep.NobetGrupGorevTipId);
            ViewBag.TakvimId = new SelectList(_takvimService.GetList(), "Id", "Tarih", nobetGrupTalep.TakvimId);
            return View(nobetGrupTalep);
        }

        // GET: EczaneNobet/NobetGrupTalep/Delete/5
        public ActionResult Delete(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NobetGrupTalepDetay nobetGrupTalep = _nobetGrupTalepService.GetDetayById(id);
            if (nobetGrupTalep == null)
            {
                return HttpNotFound();
            }
            return View(nobetGrupTalep);
        }

        // POST: EczaneNobet/NobetGrupTalep/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NobetGrupTalepDetay nobetGrupTalep = _nobetGrupTalepService.GetDetayById(id);
            _nobetGrupTalepService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
