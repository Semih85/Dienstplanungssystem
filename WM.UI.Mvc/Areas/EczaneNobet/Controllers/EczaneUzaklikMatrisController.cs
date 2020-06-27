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
using WM.UI.Mvc.Services;

namespace WM.UI.Mvc.Areas.EczaneNobet.Controllers
{
    [HandleError]
    [Authorize]
    public class EczaneUzaklikMatrisController : Controller
    {
        #region ctor
        private IEczaneService _eczaneService;
        private IUserService _userService;
        private IEczaneUzaklikMatrisService _eczaneUzaklikMatrisService;
        private IEczaneNobetGrupService _eczaneNobetGrupService;
        private INobetGrupService _nobetGrupService;
        private INobetUstGrupService _nobetUstGrupService;
        private IEczaneNobetSonucService _eczaneNobetSonucService;
        private INobetUstGrupSessionService _nobetUstGrupSessionService;
        private IEczaneNobetOrtakService _eczaneNobetOrtakService;

        public EczaneUzaklikMatrisController(IEczaneService eczaneService,
                                IUserService userService,
                                IEczaneUzaklikMatrisService eczaneUzaklikMatrisService,
                                INobetUstGrupService nobetUstGrupService,
                                INobetGrupService nobetGrupService,
                                IEczaneNobetGrupService eczaneNobetGrupService,
                                IEczaneNobetSonucService eczaneNobetSonucService,
                                INobetUstGrupSessionService nobetUstGrupSessionService,
                                IEczaneNobetOrtakService eczaneNobetOrtakService)
        {
            _eczaneService = eczaneService;
            _nobetUstGrupService = nobetUstGrupService;
            _nobetGrupService = nobetGrupService;
            _eczaneUzaklikMatrisService = eczaneUzaklikMatrisService;
            _eczaneNobetSonucService = eczaneNobetSonucService;
            _userService = userService;
            _eczaneNobetGrupService = eczaneNobetGrupService;
            _nobetUstGrupSessionService = nobetUstGrupSessionService;
            _eczaneNobetOrtakService = eczaneNobetOrtakService;
        }
        #endregion

        // GET: EczaneNobet/EczaneUzaklikMatris
        public ActionResult Index()
        {
            //var user = _userService.GetByUserName(User.Identity.Name);
            //var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user).Select(s => s.Id);
            var ustGrupSession = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var nobetUstGrupId = ustGrupSession.Id;

            //var eczaneGrupIdList = _nobetGrupService.GetListByNobetUstGrupId(nobetUstGrupId).Select(s => s.Id).ToList();

            //var eczaneler = _eczaneService.GetDetaylar(nobetUstGrupId); //_eczaneNobetGrupService.GetAktifEczaneNobetGrupList(eczaneGrupIdList);

            var eczaneler = _eczaneService.GetList(nobetUstGrupId)
                .Where(w => w.KapanisTarihi == null)
                .OrderBy(s => s.Adi)
                .ToList();

            //var eczaneListesi = _eczaneNobetOrtak.EczaneDetayiEczaneListesineDonustur(eczaneler);

            //var eczaneUzaklikMatrisList = _eczaneNobetOrtak.SetUzakliklarKusUcusu(eczaneListesi);

            //_eczaneUzaklikMatrisService.CokluEkle(eczaneUzaklikMatrisList);

            //var sonuclar = _eczaneUzaklikMatrisService.GetDetaylar(nobetUstGrupId);

            ViewBag.EczaneId = new SelectList(eczaneler, "Id", "Adi");

            var model = new EczaneUzaklikMatrisViewModel
            {
                Eczaneler = new List<Eczane>(),
                NobetUstGrupId = nobetUstGrupId,
                Uzakliklar = new List<EczaneUzaklikMatrisDetay>(),// sonuclar,
            };

            return View(model);
        }

        public ActionResult UzaklikMatrisileriniYönet()
        {
            //var ustGrupSession = _nobetUstGrupSessionService.GetSession("nobetUstGrup");
            //
            //var nobetUstGrupId = ustGrupSession.Id;

            return View();
        }

        public ActionResult UzaklikMatrisiniOlusturPartialView(int eczaneId)
        {
            var sonuclar = _eczaneUzaklikMatrisService.GetDetaylarByEczaneId(eczaneId);

            return PartialView(sonuclar);
        }

        public void UzaklikMatrisiniOlustur()
        {
            var ustGrupSession = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var nobetUstGrupId = ustGrupSession.Id;

            var eczaneler = _eczaneService.GetDetaylar(nobetUstGrupId);

            var eczaneListesi = _eczaneNobetOrtakService.EczaneDetayiEczaneListesineDonustur(eczaneler);

            var eczaneUzaklikMatrisList = _eczaneNobetOrtakService.SetUzakliklarKusUcusu(eczaneListesi);

            _eczaneUzaklikMatrisService.CokluEkle(eczaneUzaklikMatrisList);
        }

        public void UzaklikMatrisiniSil()
        {
            var ustGrupSession = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var nobetUstGrupId = ustGrupSession.Id;

            var sonuclar = _eczaneUzaklikMatrisService.GetDetaylar(nobetUstGrupId).Select(s => s.Id).ToArray();

            _eczaneUzaklikMatrisService.CokluSil(sonuclar);
        }

        // GET: EczaneNobet/EczaneUzaklikMatris/Details/5
        public ActionResult Details(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EczaneUzaklikMatris eczaneUzaklikMatris = _eczaneUzaklikMatrisService.GetById(id);
            if (eczaneUzaklikMatris == null)
            {
                return HttpNotFound();
            }
            return View(eczaneUzaklikMatris);
        }

        // GET: EczaneNobet/EczaneUzaklikMatris/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EczaneNobet/EczaneUzaklikMatris/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EczaneIdFrom,EczaneIdTo,Mesafe")] EczaneUzaklikMatris eczaneUzaklikMatris)
        {
            if (ModelState.IsValid)
            {
                _eczaneUzaklikMatrisService.Insert(eczaneUzaklikMatris);
                return RedirectToAction("Index");
            }

            return View(eczaneUzaklikMatris);
        }

        // GET: EczaneNobet/EczaneUzaklikMatris/Edit/5
        public ActionResult Edit(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EczaneUzaklikMatrisDetay eczaneUzaklikMatris = _eczaneUzaklikMatrisService.GetDetayById(id);
            if (eczaneUzaklikMatris == null)
            {
                return HttpNotFound();
            }
            return View(eczaneUzaklikMatris);
        }

        // POST: EczaneNobet/EczaneUzaklikMatris/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EczaneIdFrom,EczaneIdTo,Mesafe")] EczaneUzaklikMatris eczaneUzaklikMatris)
        {
            if (ModelState.IsValid)
            {
                _eczaneUzaklikMatrisService.Update(eczaneUzaklikMatris);
                return RedirectToAction("Index");
            }
            return View(eczaneUzaklikMatris);
        }

        // GET: EczaneNobet/EczaneUzaklikMatris/Delete/5
        public ActionResult Delete(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EczaneUzaklikMatris eczaneUzaklikMatris = _eczaneUzaklikMatrisService.GetById(id);
            _eczaneUzaklikMatrisService.Delete(id);
            if (eczaneUzaklikMatris == null)
            {
                return HttpNotFound();
            }
            return View(eczaneUzaklikMatris);
        }

        // POST: EczaneNobet/EczaneUzaklikMatris/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EczaneUzaklikMatris eczaneUzaklikMatris = _eczaneUzaklikMatrisService.GetById(id);
            _eczaneUzaklikMatrisService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
