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
    public class EczaneNobetFeragatController : Controller
    {
        #region ctor
        private IEczaneNobetFeragatService _eczaneNobetFeragatService;
        private IEczaneNobetSonucService _eczaneNobetSonucService;
        private INobetUstGrupService _nobetUstGrupService;
        private IUserService _userService;
        private INobetFeragatTipService _nobetFeragatTipService;

        public EczaneNobetFeragatController(IEczaneNobetFeragatService eczaneNobetFeragatService,
            IEczaneNobetSonucService eczaneNobetSonucService,
            INobetUstGrupService nobetUstGrupService,
            IUserService userService,
            INobetFeragatTipService nobetFeragatTipService)
        {
            _eczaneNobetFeragatService = eczaneNobetFeragatService;
            _eczaneNobetSonucService = eczaneNobetSonucService;
            _nobetUstGrupService = nobetUstGrupService;
            _userService = userService;
            _nobetFeragatTipService = nobetFeragatTipService;
        } 
        #endregion

        // GET: EczaneNobet/EczaneNobetFeragat
        public ActionResult Index()
        {
            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user).Select(s => s.Id);
            var model = _eczaneNobetFeragatService.GetDetaylar()
                .Where(s => nobetUstGruplar.Contains(s.NobetUstGrupId));
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
            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user).Select(s => s.Id);
            var eczaneNobetSonuclar = _eczaneNobetSonucService.GetSonuclar()
                    .Where(s => nobetUstGruplar.Contains(s.NobetUstGrupId))
                    .OrderBy(o => o.TakvimId);

            var nobetFeragatTipler = _nobetFeragatTipService.GetList();

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
                _eczaneNobetFeragatService.Insert(eczaneNobetFeragat);
                return RedirectToAction("Index");
            }
            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user).Select(s => s.Id);
            var eczaneNobetSonuclar = _eczaneNobetSonucService.GetSonuclar()
                    .Where(s => nobetUstGruplar.Contains(s.NobetUstGrupId))
                    .OrderBy(o => o.TakvimId);
            var nobetFeragatTipler = _nobetFeragatTipService.GetList();

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
            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user).Select(s => s.Id);
            var eczaneNobetSonuclar = _eczaneNobetSonucService.GetSonuclar()
                    .Where(s => nobetUstGruplar.Contains(s.NobetUstGrupId))
                    .OrderBy(o => o.TakvimId);
            var nobetFeragatTipler = _nobetFeragatTipService.GetList();

            ViewBag.NobetFeragatTipId = new SelectList(nobetFeragatTipler.Select(s => new MyDrop { Id = s.Id, Value = s.Adi }), "Id", "Value");
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
                _eczaneNobetFeragatService.Update(eczaneNobetFeragat);
                return RedirectToAction("Index");
            }
            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user).Select(s => s.Id);
            var eczaneNobetSonuclar = _eczaneNobetSonucService.GetSonuclar()
                    .Where(s => nobetUstGruplar.Contains(s.NobetUstGrupId))
                    .OrderBy(o => o.TakvimId);
            var nobetFeragatTipler = _nobetFeragatTipService.GetList();

            ViewBag.NobetFeragatTipId = new SelectList(nobetFeragatTipler.Select(s => new MyDrop { Id = s.Id, Value = s.Adi }), "Id", "Value");
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
