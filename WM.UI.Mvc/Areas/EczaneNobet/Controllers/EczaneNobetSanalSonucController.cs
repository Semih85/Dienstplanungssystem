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
    [Authorize]
    [HandleError]
    public class EczaneNobetSanalSonucController : Controller
    {
        private IEczaneNobetSanalSonucService _eczaneNobetSanalSonucService;
        private IEczaneNobetSonucService _eczaneNobetSonucService;
        private INobetUstGrupService _nobetUstGrupService;
        private IUserService _userService;
        //private INobetSanalSonucTipService _nobetSanalSonucTipService;
        private IEczaneNobetGrupService _eczaneNobetGrupService;
        private INobetUstGrupSessionService _nobetUstGrupSessionService;
        private ITakvimService _takvimService;
        private IEczaneService _eczaneService;

        public EczaneNobetSanalSonucController(
            IEczaneNobetSanalSonucService eczaneNobetSanalSonucService,
            IEczaneNobetSonucService eczaneNobetSonucService,
            INobetUstGrupService nobetUstGrupService,
            IUserService userService,
            //INobetSanalSonucTipService nobetSanalSonucTipService,
            IEczaneNobetGrupService eczaneNobetGrupService,
            INobetUstGrupSessionService nobetUstGrupSessionService,
            ITakvimService takvimService,
            IEczaneService eczaneService)
        {
            _eczaneNobetSanalSonucService = eczaneNobetSanalSonucService;
            _eczaneNobetSonucService = eczaneNobetSonucService;
            _nobetUstGrupService = nobetUstGrupService;
            _userService = userService;
            //_nobetSanalSonucTipService = nobetSanalSonucTipService;
            _eczaneNobetGrupService = eczaneNobetGrupService;
            _nobetUstGrupSessionService = nobetUstGrupSessionService;
            _takvimService = takvimService;
            _eczaneService = eczaneService;
        }
        // GET: EczaneNobet/EczaneNobetSanalSonuc
        public ActionResult Index()
        {
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var eczaneNobetSanalSonuclar = _eczaneNobetSanalSonucService.GetDetaylar(nobetUstGrup.Id);

            return View(eczaneNobetSanalSonuclar);
        }

        // GET: EczaneNobet/EczaneNobetSanalSonuc/Details/5
        public ActionResult Details(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var eczaneNobetSanalSonuc = _eczaneNobetSanalSonucService.GetDetayById(id);
            if (eczaneNobetSanalSonuc == null)
            {
                return HttpNotFound();
            }
            return View(eczaneNobetSanalSonuc);
        }

        // GET: EczaneNobet/EczaneNobetSanalSonuc/Create
        public ActionResult Create()
        {
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var eczaneler = _eczaneService.GetDetaylar(nobetUstGrup.Id);

            var eczaneNobetGruplar = _eczaneNobetGrupService.GetDetaylarByEczaneIdList(eczaneler.Select(s => s.Id).ToList())
                .OrderBy(s => s.EczaneAdi).ThenBy(t => t.NobetGrupAdi)
                .Select(s => new MyDrop { Id = s.Id, Value = $"{s.EczaneAdi}, {s.NobetGrupGorevTipAdi}" });

            ViewBag.EczaneNobetGrupId = new SelectList(eczaneNobetGruplar, "Id", "Value");

            return View();
        }

        // POST: EczaneNobet/EczaneNobetSanalSonuc/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EczaneNobetGrupId,NobetTarihi,Aciklama")] EczaneNobetSanalSonucEkle eczaneNobetSanalSonuc)
        {
            if (eczaneNobetSanalSonuc.NobetTarihi == null)
            {
                throw new ArgumentNullException("Nöbet tarihi boş bırakılamaz.");
            }

            if (ModelState.IsValid)
            {
                var user = _userService.GetByUserName(User.Identity.Name);
                var takvim = _takvimService.GetByTarih(eczaneNobetSanalSonuc.NobetTarihi);
                var eczane = _eczaneNobetGrupService.GetDetayById(eczaneNobetSanalSonuc.EczaneNobetGrupId);

                var sonucParametreler = new EczaneNobetSonuc
                {
                    TakvimId = takvim.Id,
                    NobetGorevTipId = eczane.NobetGorevTipId,
                    EczaneNobetGrupId = eczaneNobetSanalSonuc.EczaneNobetGrupId,
                    //YayimlandiMi = eczaneNobetSanalSonuc.YayimlandiMi
                };

                var sanalSonucParametreler = new EczaneNobetSanalSonuc
                {
                    //EczaneNobetSonucId = sonuc.Id,
                    KayitTarihi = DateTime.Now,
                    UserId = user.Id,
                    Aciklama = eczaneNobetSanalSonuc.Aciklama
                };

                _eczaneNobetSonucService.InsertSonuclarInsertSanalSonuclar(sonucParametreler, sanalSonucParametreler);

                return RedirectToAction("Index");
            }

            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var eczaneler = _eczaneService.GetDetaylar(nobetUstGrup.Id);

            var eczaneNobetGruplar = _eczaneNobetGrupService.GetDetaylarByEczaneIdList(eczaneler.Select(s => s.Id).ToList())
                .OrderBy(s => s.EczaneAdi).ThenBy(t => t.NobetGrupAdi)
                .Select(s => new MyDrop { Id = s.Id, Value = $"{s.EczaneAdi}, {s.NobetGrupGorevTipAdi}" });

            ViewBag.EczaneNobetGrupId = new SelectList(eczaneNobetGruplar, "Id", "Value", eczaneNobetSanalSonuc.EczaneNobetGrupId);

            return View(eczaneNobetSanalSonuc);
        }

        // GET: EczaneNobet/EczaneNobetSanalSonuc/Edit/5
        public ActionResult Edit(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var eczaneNobetSanalSonuc = _eczaneNobetSanalSonucService.GetDetayById(id);

            if (eczaneNobetSanalSonuc == null)
            {
                return HttpNotFound();
            }

            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var eczaneler = _eczaneService.GetDetaylar(nobetUstGrup.Id);

            var eczaneNobetGruplar = _eczaneNobetGrupService.GetDetaylarByEczaneIdList(eczaneler.Select(s => s.Id).ToList())
                .OrderBy(s => s.EczaneAdi).ThenBy(t => t.NobetGrupAdi)
                .Select(s => new MyDrop { Id = s.Id, Value = $"{s.EczaneAdi}, {s.NobetGrupGorevTipAdi}" });

            ViewBag.EczaneNobetGrupId = new SelectList(eczaneNobetGruplar, "Id", "Value", eczaneNobetSanalSonuc.EczaneNobetGrupId);

            return View(eczaneNobetSanalSonuc);
        }

        // POST: EczaneNobet/EczaneNobetSanalSonuc/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EczaneNobetSonucId,EczaneNobetGrupId,NobetTarihi,Aciklama")] EczaneNobetSanalSonucDuzenle eczaneNobetSanalSonuc)
        {
            var eczaneNobetSanalSonucDetay = _eczaneNobetSanalSonucService.GetDetayById(eczaneNobetSanalSonuc.EczaneNobetSonucId);

            if (ModelState.IsValid)
            {
                var user = _userService.GetByUserName(User.Identity.Name);
                var takvim = _takvimService.GetByTarih(eczaneNobetSanalSonuc.NobetTarihi);
                var eczaneNobetGrup = _eczaneNobetGrupService.GetDetayById(eczaneNobetSanalSonuc.EczaneNobetGrupId);

                var sonucParametreler = new EczaneNobetSonuc
                {
                    Id = eczaneNobetSanalSonuc.EczaneNobetSonucId,
                    TakvimId = takvim.Id,
                    NobetGorevTipId = eczaneNobetGrup.NobetGorevTipId,
                    EczaneNobetGrupId = eczaneNobetGrup.Id,
                    //YayimlandiMi = eczaneNobetSanalSonuc.YayimlandiMi
                };

                var sanalSonucParametreler = new EczaneNobetSanalSonuc
                {
                    EczaneNobetSonucId = eczaneNobetSanalSonuc.EczaneNobetSonucId,
                    KayitTarihi = DateTime.Now,
                    UserId = user.Id,
                    Aciklama = eczaneNobetSanalSonuc.Aciklama
                };

                _eczaneNobetSonucService.UpdateSonuclarUpdateSanalSonuclar(sonucParametreler, sanalSonucParametreler);

                return RedirectToAction("Index");
            }

            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var eczaneler = _eczaneService.GetDetaylar(nobetUstGrup.Id);

            var eczaneNobetGruplar = _eczaneNobetGrupService.GetDetaylarByEczaneIdList(eczaneler.Select(s => s.Id).ToList())
                .OrderBy(s => s.EczaneAdi).ThenBy(t => t.NobetGrupAdi)
                .Select(s => new MyDrop { Id = s.Id, Value = $"{s.EczaneAdi}, {s.NobetGrupGorevTipAdi}" });

            ViewBag.EczaneNobetGrupId = new SelectList(eczaneNobetGruplar, "Id", "Value", eczaneNobetSanalSonucDetay.EczaneNobetGrupId);

            return View(eczaneNobetSanalSonuc);
        }

        // GET: EczaneNobet/EczaneNobetSanalSonuc/Delete/5
        public ActionResult Delete(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var eczaneNobetSanalSonuc = _eczaneNobetSanalSonucService.GetDetayById(id);
            if (eczaneNobetSanalSonuc == null)
            {
                return HttpNotFound();
            }
            return View(eczaneNobetSanalSonuc);
        }

        // POST: EczaneNobet/EczaneNobetSanalSonuc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var eczaneNobetSanalSonuc = _eczaneNobetSanalSonucService.GetDetayById(id);
            //_eczaneNobetSanalSonucService.Delete(id);
            _eczaneNobetSonucService.SilSonuclarSilSanalSonuclar(id);
            return RedirectToAction("Index");
        }
    }
}
