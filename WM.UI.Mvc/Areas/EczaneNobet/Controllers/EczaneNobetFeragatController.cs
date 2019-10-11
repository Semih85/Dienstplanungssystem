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
    public class EczaneNobetFeragatController : Controller
    {
        #region ctor
        private IEczaneNobetFeragatService _eczaneNobetFeragatService;
        private IEczaneNobetSonucService _eczaneNobetSonucService;
        private INobetUstGrupService _nobetUstGrupService;
        private IUserService _userService;
        private INobetFeragatTipService _nobetFeragatTipService;
        private IEczaneNobetGrupService _eczaneNobetGrupService;
        private INobetUstGrupSessionService _nobetUstGrupSessionService;

        public EczaneNobetFeragatController(IEczaneNobetFeragatService eczaneNobetFeragatService,
            IEczaneNobetSonucService eczaneNobetSonucService,
            INobetUstGrupService nobetUstGrupService,
            IUserService userService,
            INobetFeragatTipService nobetFeragatTipService,
            IEczaneNobetGrupService eczaneNobetGrupService,
            INobetUstGrupSessionService nobetUstGrupSessionService)
        {
            _eczaneNobetFeragatService = eczaneNobetFeragatService;
            _eczaneNobetSonucService = eczaneNobetSonucService;
            _nobetUstGrupService = nobetUstGrupService;
            _userService = userService;
            _nobetFeragatTipService = nobetFeragatTipService;
            _eczaneNobetGrupService = eczaneNobetGrupService;
            _nobetUstGrupSessionService = nobetUstGrupSessionService;
        } 
        #endregion

        // GET: EczaneNobet/EczaneNobetFeragat
        public ActionResult Index()
        {
            //var user = _userService.GetByUserName(User.Identity.Name);
            //var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user).Select(s => s.Id);
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var model = _eczaneNobetFeragatService.GetDetaylar(nobetUstGrup.Id);

            return View(model);
        }

        // GET: EczaneNobet/EczaneNobetFeragat/Details/5
        public ActionResult Details(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EczaneNobetFeragatDetay eczaneNobetFeragat = _eczaneNobetFeragatService.GetDetayById(id);
            if (eczaneNobetFeragat == null)
            {
                return HttpNotFound();
            }
            return View(eczaneNobetFeragat);
        }

        // GET: EczaneNobet/EczaneNobetFeragat/Create
        public ActionResult Create()
        {
            //var user = _userService.GetByUserName(User.Identity.Name);
            //var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user).Select(s => s.Id);
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var eczaneNobetSonuclar = _eczaneNobetSonucService.GetSonuclar(nobetUstGrup.Id)
                    .OrderBy(o => o.TakvimId);

            var nobetFeragatTipler = _nobetFeragatTipService.GetList();

            //var eczaneNobetGruplar = _eczaneNobetGrupService.GetDetaylar(nobetUstGruplar.FirstOrDefault())
            //    .OrderBy(s => s.EczaneAdi)
            //    .ThenBy(t => t.NobetGrupAdi)
            //    .Select(s => new MyDrop { Id = s.Id, Value = $"{s.EczaneAdi} ({s.NobetGrupAdi}, {s.NobetGorevTipAdi})" });

            //ViewBag.EczaneNobetGrupId = new SelectList(eczaneNobetGruplar, "Id", "Value");
            ViewBag.NobetFeragatTipId = new SelectList(nobetFeragatTipler.Select(s => new MyDrop { Id = s.Id, Value = s.Adi }), "Id", "Value");
            ViewBag.EczaneNobetSonucId = new SelectList(eczaneNobetSonuclar.Select(s => new MyDrop { Id = s.Id, Value = $"{s.EczaneAdi}, {s.NobetGrupAdi}, {s.Tarih.ToLongDateString()}" }), "Id", "Value");
            return View();
        }

        // POST: EczaneNobet/EczaneNobetFeragat/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EczaneNobetSonucId,Aciklama,NobetFeragatTipId")] EczaneNobetFeragat eczaneNobetFeragat)
        {
            if (ModelState.IsValid)
            {
                var feragatEdilecekSonuc = _eczaneNobetSonucService.GetById(eczaneNobetFeragat.EczaneNobetSonucId);

                eczaneNobetFeragat.EczaneNobetGrupId = feragatEdilecekSonuc.EczaneNobetGrupId;

                _eczaneNobetFeragatService.Insert(eczaneNobetFeragat);
                return RedirectToAction("Index");
            }
            //var user = _userService.GetByUserName(User.Identity.Name);
            //var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user).Select(s => s.Id);
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var eczaneNobetSonuclar = _eczaneNobetSonucService.GetSonuclar(nobetUstGrup.Id)
                    .OrderBy(o => o.TakvimId);

            var nobetFeragatTipler = _nobetFeragatTipService.GetList();

            //var eczaneNobetGruplar = _eczaneNobetGrupService.GetDetaylar(nobetUstGruplar.FirstOrDefault())
            //    .OrderBy(s => s.EczaneAdi)
            //    .ThenBy(t => t.NobetGrupAdi)
            //    .Select(s => new MyDrop { Id = s.Id, Value = $"{s.EczaneAdi} ({s.NobetGrupAdi}, {s.NobetGorevTipAdi})" });

            //ViewBag.EczaneNobetGrupId = new SelectList(eczaneNobetGruplar, "Id", "Value");
            ViewBag.NobetFeragatTipId = new SelectList(nobetFeragatTipler.Select(s => new MyDrop { Id = s.Id, Value = s.Adi }), "Id", "Value");
            ViewBag.EczaneNobetSonucId = new SelectList(eczaneNobetSonuclar.Select(s => new MyDrop { Id = s.Id, Value = $"{s.EczaneAdi}, {s.NobetGrupAdi}, {s.Tarih.ToLongDateString()}" }), "Id", "Value", eczaneNobetFeragat.EczaneNobetSonucId);
            return View(eczaneNobetFeragat);
        }

        // GET: EczaneNobet/EczaneNobetFeragat/Edit/5
        public ActionResult Edit(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EczaneNobetFeragatDetay eczaneNobetFeragat = _eczaneNobetFeragatService.GetDetayById(id);
            if (eczaneNobetFeragat == null)
            {
                return HttpNotFound();
            }
            //var user = _userService.GetByUserName(User.Identity.Name);
            //var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user).Select(s => s.Id);
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var eczaneNobetSonuclar = _eczaneNobetSonucService.GetSonuclar(nobetUstGrup.Id)
                    .OrderBy(o => o.TakvimId);

            var nobetFeragatTipler = _nobetFeragatTipService.GetList();

            var eczaneNobetGruplar = _eczaneNobetGrupService.GetDetaylar(nobetUstGrup.Id)
                .OrderBy(s => s.EczaneAdi)
                .ThenBy(t => t.NobetGrupAdi)
                .Select(s => new MyDrop { Id = s.Id, Value = $"{s.EczaneAdi} ({s.NobetGrupAdi}, {s.NobetGorevTipAdi})" });

            ViewBag.EczaneNobetGrupId = new SelectList(eczaneNobetGruplar, "Id", "Value", eczaneNobetFeragat.EczaneNobetGrupId);
            ViewBag.NobetFeragatTipId = new SelectList(nobetFeragatTipler.Select(s => new MyDrop { Id = s.Id, Value = s.Adi }), "Id", "Value", eczaneNobetFeragat.NobetFeragatTipId);
            ViewBag.EczaneNobetSonucId = new SelectList(eczaneNobetSonuclar.Select(s => new MyDrop { Id = s.Id, Value = $"{s.EczaneAdi}, {s.NobetGrupAdi}, {s.Tarih.ToLongDateString()}" }), "Id", "Value", eczaneNobetFeragat.EczaneNobetSonucId);
            return View(eczaneNobetFeragat);
        }

        // POST: EczaneNobet/EczaneNobetFeragat/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EczaneNobetSonucId,Aciklama,NobetFeragatTipId")] EczaneNobetFeragat eczaneNobetFeragat)
        {
            if (ModelState.IsValid)
            {
                var feragatEdilecekSonuc = _eczaneNobetSonucService.GetById(eczaneNobetFeragat.EczaneNobetSonucId);

                eczaneNobetFeragat.EczaneNobetGrupId = feragatEdilecekSonuc.EczaneNobetGrupId;

                _eczaneNobetFeragatService.Update(eczaneNobetFeragat);

                return RedirectToAction("Index");
            }

            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var eczaneNobetSonuclar = _eczaneNobetSonucService.GetSonuclar(nobetUstGrup.Id)
                    .OrderBy(o => o.TakvimId);

            var nobetFeragatTipler = _nobetFeragatTipService.GetList();

            var eczaneNobetGruplar = _eczaneNobetGrupService.GetDetaylar(nobetUstGrup.Id)
                .OrderBy(s => s.EczaneAdi)
                .ThenBy(t => t.NobetGrupAdi)
                .Select(s => new MyDrop { Id = s.Id, Value = $"{s.EczaneAdi} ({s.NobetGrupAdi}, {s.NobetGorevTipAdi})" });

            ViewBag.EczaneNobetGrupId = new SelectList(eczaneNobetGruplar, "Id", "Value", eczaneNobetFeragat.EczaneNobetGrupId);
            ViewBag.NobetFeragatTipId = new SelectList(nobetFeragatTipler.Select(s => new MyDrop { Id = s.Id, Value = s.Adi }), "Id", "Value", eczaneNobetFeragat.NobetFeragatTipId);
            ViewBag.EczaneNobetSonucId = new SelectList(eczaneNobetSonuclar.Select(s => new MyDrop { Id = s.Id, Value = $"{s.EczaneAdi}, {s.NobetGrupAdi}, {s.Tarih.ToLongDateString()}" }), "Id", "Value", eczaneNobetFeragat.EczaneNobetSonucId);

            return View(eczaneNobetFeragat);
        }

        // GET: EczaneNobet/EczaneNobetFeragat/Delete/5
        public ActionResult Delete(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EczaneNobetFeragatDetay eczaneNobetFeragat = _eczaneNobetFeragatService.GetDetayById(id);
            if (eczaneNobetFeragat == null)
            {
                return HttpNotFound();
            }
            return View(eczaneNobetFeragat);
        }

        // POST: EczaneNobet/EczaneNobetFeragat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EczaneNobetFeragatDetay eczaneNobetFeragat = _eczaneNobetFeragatService.GetDetayById(id);
            _eczaneNobetFeragatService.Delete(id);
            return RedirectToAction("Index");
        }

    }
}
