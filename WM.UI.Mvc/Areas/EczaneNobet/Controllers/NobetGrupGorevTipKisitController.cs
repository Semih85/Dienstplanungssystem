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
    [HandleError]
    public class NobetGrupGorevTipKisitController : Controller
    {
        private INobetGrupGorevTipKisitService _nobetGrupGorevTipKisitService;
        private INobetUstGrupService _nobetUstGrupService;
        private INobetUstGrupKisitService _nobetUstGrupKisitService;
        private INobetGrupGorevTipService _nobetGrupGorevTipService;
        private IUserService _userService;

        public NobetGrupGorevTipKisitController(INobetGrupGorevTipKisitService nobetGrupGorevTipKisitService,
            INobetUstGrupService nobetUstGrupService,
            IUserService userService,
            INobetUstGrupKisitService nobetUstGrupKisitService,
            INobetGrupGorevTipService nobetGrupGorevTipService)
        {
            _nobetGrupGorevTipKisitService = nobetGrupGorevTipKisitService;
            _nobetUstGrupService = nobetUstGrupService;
            _userService = userService;
            _nobetUstGrupKisitService = nobetUstGrupKisitService;
            _nobetGrupGorevTipService = nobetGrupGorevTipService;
        }

        // GET: EczaneNobet/NobetGrupGorevTipKisit
        public ActionResult Index()
        {
            var user = _userService.GetByUserName(User.Identity.Name);

            var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            var nobetUstGrup = nobetUstGruplar.FirstOrDefault();

            var nobetGrupGorevTipKisitlar = _nobetGrupGorevTipKisitService.GetDetaylar(nobetUstGrup.Id);

            return View(nobetGrupGorevTipKisitlar);
        }

        // GET: EczaneNobet/NobetGrupGorevTipKisit/Details/5
        public ActionResult Details(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var nobetGrupGorevTipKisit = _nobetGrupGorevTipKisitService.GetDetayById(id);
            if (nobetGrupGorevTipKisit == null)
            {
                return HttpNotFound();
            }
            return View(nobetGrupGorevTipKisit);
        }

        // GET: EczaneNobet/NobetGrupGorevTipKisit/Create
        public ActionResult Create()
        {
            var user = _userService.GetByUserName(User.Identity.Name);

            var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            var nobetUstGrup = nobetUstGruplar.FirstOrDefault();

            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrup.Id);
            var nobetUstGrupKisitlar = _nobetUstGrupKisitService.GetDetaylar(nobetUstGrup.Id);

            ViewBag.NobetGrupGorevTipId = new SelectList(nobetGrupGorevTipler, "Id", "NobetGrupGorevTipAdi");
            ViewBag.NobetUstGrupKisitId = new SelectList(nobetUstGrupKisitlar, "Id", "KisitAdiUzun");
            return View();
        }

        public ActionResult Create2(int kisitId)
        {
            var user = _userService.GetByUserName(User.Identity.Name);

            var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            var nobetUstGrup = nobetUstGruplar.FirstOrDefault();

            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrup.Id);
            var nobetUstGrupKisitlar = _nobetUstGrupKisitService.GetDetaylar(nobetUstGrup.Id)
                .Where(w => w.KisitId == kisitId).ToList();

            var nobetUstGrupKisit = nobetUstGrupKisitlar.SingleOrDefault(x => x.KisitId == kisitId);

            var nobetGrupGorevTipKisit = new NobetGrupGorevTipKisitDetay
            {
                SagTarafDegeri = nobetUstGrupKisit.SagTarafDegeri,
                PasifMi = nobetUstGrupKisit.PasifMi
            };

            ViewBag.NobetGrupGorevTipId = new SelectList(nobetGrupGorevTipler, "Id", "NobetGrupGorevTipAdi");
            ViewBag.NobetUstGrupKisitId = new SelectList(nobetUstGrupKisitlar, "Id", "KisitAdiUzun", nobetUstGrupKisit.Id);

            return View("Create", nobetGrupGorevTipKisit);
        }

        // POST: EczaneNobet/NobetGrupGorevTipKisit/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NobetUstGrupKisitId,NobetGrupGorevTipId,PasifMi,SagTarafDegeri,VarsayilanPasifMi,SagTarafDegeriVarsayilan,Aciklama")] NobetGrupGorevTipKisit nobetGrupGorevTipKisit)
        {
            if (ModelState.IsValid)
            {
                _nobetGrupGorevTipKisitService.Insert(nobetGrupGorevTipKisit);
                return RedirectToAction("Index");
            }
            var user = _userService.GetByUserName(User.Identity.Name);

            var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            var nobetUstGrup = nobetUstGruplar.FirstOrDefault();

            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrup.Id);
            var nobetUstGrupKisitlar = _nobetUstGrupKisitService.GetDetaylar(nobetUstGrup.Id);

            ViewBag.NobetGrupGorevTipId = new SelectList(nobetGrupGorevTipler, "Id", "NobetGrupGorevTipAdi", nobetGrupGorevTipKisit.NobetGrupGorevTipId);
            ViewBag.NobetUstGrupKisitId = new SelectList(nobetUstGrupKisitlar, "Id", "KisitAdiUzun", nobetGrupGorevTipKisit.NobetUstGrupKisitId);
            return View(nobetGrupGorevTipKisit);
        }

        // GET: EczaneNobet/NobetGrupGorevTipKisit/Edit/5
        public ActionResult Edit(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var nobetGrupGorevTipKisit = _nobetGrupGorevTipKisitService.GetDetayById(id);
            if (nobetGrupGorevTipKisit == null)
            {
                return HttpNotFound();
            }
            var user = _userService.GetByUserName(User.Identity.Name);

            var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            var nobetUstGrup = nobetUstGruplar.FirstOrDefault();

            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrup.Id);
            var nobetUstGrupKisitlar = _nobetUstGrupKisitService.GetDetaylar(nobetUstGrup.Id);

            ViewBag.NobetGrupGorevTipId = new SelectList(nobetGrupGorevTipler, "Id", "NobetGrupGorevTipAdi", nobetGrupGorevTipKisit.NobetGrupGorevTipId);
            ViewBag.NobetUstGrupKisitId = new SelectList(nobetUstGrupKisitlar, "Id", "KisitAdiUzun", nobetGrupGorevTipKisit.NobetUstGrupKisitId);
            return View(nobetGrupGorevTipKisit);
        }

        // POST: EczaneNobet/NobetGrupGorevTipKisit/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NobetUstGrupKisitId,NobetGrupGorevTipId,PasifMi,SagTarafDegeri,VarsayilanPasifMi,SagTarafDegeriVarsayilan,Aciklama")] NobetGrupGorevTipKisit nobetGrupGorevTipKisit)
        {
            if (ModelState.IsValid)
            {
                _nobetGrupGorevTipKisitService.Update(nobetGrupGorevTipKisit);
                return RedirectToAction("Index");
            }
            var user = _userService.GetByUserName(User.Identity.Name);

            var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            var nobetUstGrup = nobetUstGruplar.FirstOrDefault();

            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrup.Id);
            var nobetUstGrupKisitlar = _nobetUstGrupKisitService.GetDetaylar(nobetUstGrup.Id);

            ViewBag.NobetGrupGorevTipId = new SelectList(nobetGrupGorevTipler, "Id", "NobetGrupGorevTipAdi", nobetGrupGorevTipKisit.NobetGrupGorevTipId);
            ViewBag.NobetUstGrupKisitId = new SelectList(nobetUstGrupKisitlar, "Id", "KisitAdiUzun", nobetGrupGorevTipKisit.NobetUstGrupKisitId);
            return View(nobetGrupGorevTipKisit);
        }

        // GET: EczaneNobet/NobetGrupGorevTipKisit/Delete/5
        public ActionResult Delete(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var nobetGrupGorevTipKisit = _nobetGrupGorevTipKisitService.GetDetayById(id);
            if (nobetGrupGorevTipKisit == null)
            {
                return HttpNotFound();
            }
            return View(nobetGrupGorevTipKisit);
        }

        // POST: EczaneNobet/NobetGrupGorevTipKisit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _nobetGrupGorevTipKisitService.Delete(id);
            return RedirectToAction("Index");
        }


    }
}
