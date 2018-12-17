﻿using System;
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
    public class NobetGrupGorevTipTakvimOzelGunController : Controller
    {
        private INobetGrupGorevTipTakvimOzelGunService _nobetGrupGorevTipTakvimOzelGunService;
        private INobetGrupGorevTipService _nobetGrupGorevTipService;
        private INobetGrupGorevTipGunKuralService _nobetGrupGorevTipGunKuralService;
        private INobetUstGrupService _nobetUstGrupService;
        private ITakvimService _takvimService;
        private INobetOzelGunService _nobetOzelGunService;
        private IUserService _userService;
        private INobetGunKuralService _nobetGunKuralService;

        public NobetGrupGorevTipTakvimOzelGunController(INobetGrupGorevTipTakvimOzelGunService nobetGrupGorevTipTakvimOzelGunService,
            INobetGrupGorevTipService nobetGrupGorevTipService,
            ITakvimService takvimService,
            INobetOzelGunService nobetOzelGunService,
            IUserService userService,
            INobetUstGrupService nobetUstGrupService,
            INobetGunKuralService nobetGunKuralService,
            INobetGrupGorevTipGunKuralService nobetGrupGorevTipGunKuralService)
        {
            _nobetGrupGorevTipTakvimOzelGunService = nobetGrupGorevTipTakvimOzelGunService;
            _nobetGrupGorevTipService = nobetGrupGorevTipService;
            _takvimService = takvimService;
            _nobetOzelGunService = nobetOzelGunService;
            _userService = userService;
            _nobetUstGrupService = nobetUstGrupService;
            _nobetGunKuralService = nobetGunKuralService;
            _nobetGrupGorevTipGunKuralService = nobetGrupGorevTipGunKuralService;
        }
        // GET: EczaneNobet/NobetGrupGorevTipTakvimOzelGun
        public ActionResult Index()
        {
            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user).Select(s => s.Id).ToList();

            var nobetGrupGorevTipTakvimOzelGunler = _nobetGrupGorevTipTakvimOzelGunService.GetDetaylar(nobetUstGruplar);

            return View(nobetGrupGorevTipTakvimOzelGunler);
        }

        // GET: EczaneNobet/NobetGrupGorevTipTakvimOzelGun/Details/5
        public ActionResult Details(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var nobetGrupGorevTipTakvimOzelGun = _nobetGrupGorevTipTakvimOzelGunService.GetDetayById(id);
            if (nobetGrupGorevTipTakvimOzelGun == null)
            {
                return HttpNotFound();
            }
            return View(nobetGrupGorevTipTakvimOzelGun);
        }

        // GET: EczaneNobet/NobetGrupGorevTipTakvimOzelGun/Create
        public ActionResult Create()
        {
            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user).Select(s => s.Id).ToList();

            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylarByNobetUstGrupIdList(nobetUstGruplar)
                .Select(s => new MyDrop { Id = s.Id, Value = $"{s.NobetGrupAdi}, {s.NobetGorevTipAdi}" });

            var nobetGrupGorevTipGunKurallarTumu = _nobetGrupGorevTipGunKuralService.GetDetaylar(nobetUstGruplar);

            var nobetGrupGorevTipGunKurallar = nobetGrupGorevTipGunKurallarTumu
                .Where(w => w.NobetGunKuralId > 7)
                .Select(s => new MyDrop { Id = s.Id, Value = $"{s.NobetGrupAdi}, {s.NobetGorevTipAdi}, {s.NobetGunKuralAdi}" });

            //normalde bayram olan bir günün farklı bir gün olarak gösterilebilmesi için
            //.Where(w => w.Id <= 7)
            var nobetGrunKurallar = nobetGrupGorevTipGunKurallarTumu
                .Select(s => new { s.NobetGunKuralId, s.NobetGunKuralAdi })
                .Distinct()
                .OrderBy(o => o.NobetGunKuralId)
                .ToList();

            ViewBag.NobetGrupGorevTipGunKuralId = new SelectList(nobetGrupGorevTipGunKurallar, "Id", "Value");
            ViewBag.NobetOzelGunId = new SelectList(_nobetOzelGunService.GetList(), "Id", "Adi");
            ViewBag.NobetGunKuralId = new SelectList(nobetGrunKurallar, "NobetGunKuralId", "NobetGunKuralAdi");
            //ViewBag.TakvimId = new SelectList(_takvimService.GetList(), "Id", "Tarih");

            return View();
        }

        // POST: EczaneNobet/NobetGrupGorevTipTakvimOzelGun/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Tarih,NobetGunKuralId,NobetGrupGorevTipGunKuralId,FarkliGunGosterilsinMi,NobetOzelGunId")] TakvimOzelGunCoklu takvimOzelGunCoklu)
        {
            if (ModelState.IsValid)
            {
                var nobetGrupGorevTipTakvimOzelGunler = new List<NobetGrupGorevTipTakvimOzelGun>();

                foreach (var nobetGrupGorevTipGunKuralId in takvimOzelGunCoklu.NobetGrupGorevTipGunKuralId)
                {
                    var nobetGrupGorevTipGunKuralId1 = _nobetGrupGorevTipGunKuralService.GetDetayById(nobetGrupGorevTipGunKuralId);

                    nobetGrupGorevTipTakvimOzelGunler.Add(new NobetGrupGorevTipTakvimOzelGun
                    {
                        TakvimId = _takvimService.GetByTarih(takvimOzelGunCoklu.Tarih).Id,
                        NobetGunKuralId = takvimOzelGunCoklu.FarkliGunGosterilsinMi == false
                        ? nobetGrupGorevTipGunKuralId1.NobetGunKuralId
                        : takvimOzelGunCoklu.NobetGunKuralId,
                        NobetGrupGorevTipGunKuralId = nobetGrupGorevTipGunKuralId,
                        NobetOzelGunId = takvimOzelGunCoklu.NobetOzelGunId,
                        FarkliGunGosterilsinMi = takvimOzelGunCoklu.FarkliGunGosterilsinMi
                    });
                }

                var eklenecekbayramSayisi = nobetGrupGorevTipTakvimOzelGunler.Count;

                if (ModelState.IsValid && eklenecekbayramSayisi > 0)
                {
                    _nobetGrupGorevTipTakvimOzelGunService.CokluEkle(nobetGrupGorevTipTakvimOzelGunler);
                    TempData["EklenenBayramSayisi"] = eklenecekbayramSayisi;
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }

            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user).Select(s => s.Id).ToList();

            var nobetGrupGorevTipGunKurallarTumu = _nobetGrupGorevTipGunKuralService.GetDetaylar(nobetUstGruplar);

            var nobetGrupGorevTipGunKurallar = nobetGrupGorevTipGunKurallarTumu
                .Where(w => w.NobetGunKuralId > 7)
                .Select(s => new MyDrop { Id = s.Id, Value = $"{s.NobetGrupAdi}, {s.NobetGorevTipAdi}, {s.NobetGunKuralAdi}" });

            //normalde bayram olan bir günün farklı bir gün olarak gösterilebilmesi için
            //.Where(w => w.Id <= 7)
            var nobetGrunKurallar = nobetGrupGorevTipGunKurallarTumu
                .Select(s => new { s.NobetGunKuralId, s.NobetGunKuralAdi })
                .Distinct()
                .OrderBy(o => o.NobetGunKuralId)
                .ToList();

            ViewBag.NobetGrupGorevTipGunKuralId = new SelectList(nobetGrupGorevTipGunKurallar, "Id", "Value");
            ViewBag.NobetOzelGunId = new SelectList(_nobetOzelGunService.GetList(), "Id", "Adi");
            ViewBag.NobetGunKuralId = new SelectList(nobetGrunKurallar, "NobetGunKuralId", "NobetGunKuralAdi");
            //ViewBag.TakvimId = new SelectList(_takvimService.GetList(), "Id", "Tarih");

            return View(takvimOzelGunCoklu);
        }

        // GET: EczaneNobet/NobetGrupGorevTipTakvimOzelGun/Edit/5
        public ActionResult Edit(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var nobetGrupGorevTipTakvimOzelGun = _nobetGrupGorevTipTakvimOzelGunService.GetById(id);
            if (nobetGrupGorevTipTakvimOzelGun == null)
            {
                return HttpNotFound();
            }
            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user).Select(s => s.Id).ToList();

            var nobetGrupGorevTipGunKurallarTumu = _nobetGrupGorevTipGunKuralService.GetDetaylar(nobetUstGruplar);

            var nobetGrupGorevTipGunKurallar = nobetGrupGorevTipGunKurallarTumu
                .Where(w => w.NobetGunKuralId > 7)
                .Select(s => new MyDrop { Id = s.Id, Value = $"{s.NobetGrupAdi}, {s.NobetGorevTipAdi}, {s.NobetGunKuralAdi}" });

            //normalde bayram olan bir günün farklı bir gün olarak gösterilebilmesi için
            //.Where(w => w.Id <= 7)
            var nobetGrunKurallar = nobetGrupGorevTipGunKurallarTumu
                .Select(s => new { s.NobetGunKuralId, s.NobetGunKuralAdi })
                .Distinct() 
                .OrderBy(o => o.NobetGunKuralId)
                .ToList();

            ViewBag.NobetGrupGorevTipGunKuralId = new SelectList(nobetGrupGorevTipGunKurallar, "Id", "Value", nobetGrupGorevTipTakvimOzelGun.NobetGrupGorevTipGunKuralId);
            ViewBag.NobetOzelGunId = new SelectList(_nobetOzelGunService.GetList(), "Id", "Adi", nobetGrupGorevTipTakvimOzelGun.NobetOzelGunId);
            ViewBag.NobetGunKuralId = new SelectList(nobetGrunKurallar, "NobetGunKuralId", "NobetGunKuralAdi", nobetGrupGorevTipTakvimOzelGun.NobetGunKuralId);
            ViewBag.TakvimId = new SelectList(_takvimService.GetList().Select(s => new MyDrop { Id = s.Id, Value = s.Tarih.ToLongDateString() }), "Id", "Value", nobetGrupGorevTipTakvimOzelGun.TakvimId);

            return View(nobetGrupGorevTipTakvimOzelGun);
        }

        // POST: EczaneNobet/NobetGrupGorevTipTakvimOzelGun/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TakvimId,NobetGunKuralId,NobetGrupGorevTipGunKuralId,FarkliGunGosterilsinMi,NobetOzelGunId")] NobetGrupGorevTipTakvimOzelGun nobetGrupGorevTipTakvimOzelGun)
        {
            if (ModelState.IsValid)
            {
                var nobetGrupGorevTipGunKuralId = _nobetGrupGorevTipTakvimOzelGunService.GetDetayById(nobetGrupGorevTipTakvimOzelGun.Id);

                nobetGrupGorevTipTakvimOzelGun.NobetGunKuralId = nobetGrupGorevTipTakvimOzelGun.FarkliGunGosterilsinMi == false
                  ? nobetGrupGorevTipGunKuralId.NobetGunKuralId
                  : nobetGrupGorevTipTakvimOzelGun.NobetGunKuralId;

                _nobetGrupGorevTipTakvimOzelGunService.Update(nobetGrupGorevTipTakvimOzelGun);
                return RedirectToAction("Index");
            }
            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user).Select(s => s.Id).ToList();

            var nobetGrupGorevTipGunKurallarTumu = _nobetGrupGorevTipGunKuralService.GetDetaylar(nobetUstGruplar);

            var nobetGrupGorevTipGunKurallar = nobetGrupGorevTipGunKurallarTumu
                .Where(w => w.NobetGunKuralId > 7)
                .Select(s => new MyDrop { Id = s.Id, Value = $"{s.NobetGrupAdi}, {s.NobetGorevTipAdi}, {s.NobetGunKuralAdi}" });

            //normalde bayram olan bir günün farklı bir gün olarak gösterilebilmesi için
            //.Where(w => w.Id <= 7)
            var nobetGrunKurallar = nobetGrupGorevTipGunKurallarTumu
                .Select(s => new { s.NobetGunKuralId, s.NobetGunKuralAdi })
                .Distinct()
                .OrderBy(o => o.NobetGunKuralId)
                .ToList();

            ViewBag.NobetGrupGorevTipGunKuralId = new SelectList(nobetGrupGorevTipGunKurallar, "Id", "Value", nobetGrupGorevTipTakvimOzelGun.NobetGrupGorevTipGunKuralId);
            ViewBag.NobetOzelGunId = new SelectList(_nobetOzelGunService.GetList(), "Id", "Adi", nobetGrupGorevTipTakvimOzelGun.NobetOzelGunId);
            ViewBag.NobetGunKuralId = new SelectList(nobetGrunKurallar, "NobetGunKuralId", "NobetGunKuralAdi", nobetGrupGorevTipTakvimOzelGun.NobetGunKuralId);
            ViewBag.TakvimId = new SelectList(_takvimService.GetList().Select(s => new MyDrop { Id = s.Id, Value = s.Tarih.ToLongDateString() }), "Id", "Value", nobetGrupGorevTipTakvimOzelGun.TakvimId);

            return View(nobetGrupGorevTipTakvimOzelGun);
        }

        // GET: EczaneNobet/NobetGrupGorevTipTakvimOzelGun/Delete/5
        public ActionResult Delete(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var nobetGrupGorevTipTakvimOzelGun = _nobetGrupGorevTipTakvimOzelGunService.GetDetayById(id);
            if (nobetGrupGorevTipTakvimOzelGun == null)
            {
                return HttpNotFound();
            }
            return View(nobetGrupGorevTipTakvimOzelGun);
        }

        // POST: EczaneNobet/NobetGrupGorevTipTakvimOzelGun/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var nobetGrupGorevTipTakvimOzelGun = _nobetGrupGorevTipTakvimOzelGunService.GetDetayById(id);
            _nobetGrupGorevTipTakvimOzelGunService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
