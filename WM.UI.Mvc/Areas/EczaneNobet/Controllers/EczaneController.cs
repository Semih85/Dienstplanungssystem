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
using WM.UI.Mvc.Services;

namespace WM.UI.Mvc.Areas.EczaneNobet.Controllers
{
    [HandleError]
    [Authorize]
    public class EczaneController : Controller
    {
        private IEczaneService _eczaneService;
        private IUserService _userService;
        private IUserRoleService _userRoleService;
        private IUserEczaneService _userEczaneService;
        private IEczaneNobetGrupService _eczaneNobetGrupService;
        private IEczaneNobetGrupAltGrupService _eczaneNobetGrupAltGrup;
        private INobetUstGrupService _nobetUstGrupService;
        private INobetUstGrupSessionService _nobetUstGrupSessionService;
        private IEczaneUzaklikMatrisService _eczaneUzaklikMatrisService;
        private IEczaneNobetOrtakService _eczaneNobetOrtakService;

        public EczaneController(IEczaneService eczaneService,
                                IUserService userService,
                                IUserRoleService userRoleService,
                                IUserEczaneService userEczaneService,
                                IEczaneNobetGrupService eczaneNobetGrupService,
                                INobetUstGrupService nobetUstGrupService,
                                INobetUstGrupSessionService nobetUstGrupSessionService,
                                IEczaneNobetGrupAltGrupService eczaneNobetGrupAltGrup,
                                IEczaneUzaklikMatrisService eczaneUzaklikMatrisService,
                                IEczaneNobetOrtakService eczaneNobetOrtakService)
        {
            _eczaneService = eczaneService;
            _userService = userService;
            _userRoleService = userRoleService;
            _eczaneNobetGrupService = eczaneNobetGrupService;
            _nobetUstGrupService = nobetUstGrupService;
            _nobetUstGrupSessionService = nobetUstGrupSessionService;
            _userEczaneService = userEczaneService;
            _eczaneNobetGrupAltGrup = eczaneNobetGrupAltGrup;
            _eczaneUzaklikMatrisService = eczaneUzaklikMatrisService;
            _eczaneNobetOrtakService = eczaneNobetOrtakService;
        }

        // GET: EczaneNobet/Eczane
        [Authorize(Roles = "Admin,Oda,Üst Grup,Eczane")]
        public ActionResult Index()
        {
            var user = _userService.GetByUserName(User.Identity.Name);
            var ustGrupSession = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var eczaneler = _eczaneService.GetDetaylar(ustGrupSession.Id)
                .OrderBy(o => o.AcilisTarihi).ToList();

            //var rolIdler = _userService.GetUserRoles(user).OrderBy(s => s.RoleId).Select(u => u.RoleId).ToArray();
            var rolIdler = _userRoleService.GetDetayListByUserId(user.Id).Select(s => s.RoleId).ToList();

            var rolId = rolIdler.FirstOrDefault();

            var model = eczaneler;

            if (rolIdler.Count() == 1 && rolId == 4)
            {//sadece eczane yetkisi varsa
                var userEczaneler = _userEczaneService.GetDetaylarByUserId(user.Id);

                model = eczaneler.Where(w => userEczaneler.Select(s => s.EczaneId).Contains(w.Id)).ToList();
            }

            return View(model);
        }

        // GET: EczaneNobet/Eczane/Details/5
        public ActionResult Details(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var eczane = _eczaneService.GetDetayById(id);
            if (eczane == null)
            {
                return HttpNotFound();
            }
            return View(eczane);
        }

        // GET: EczaneNobet/Eczane/Create
        [Authorize(Roles = "Admin,Oda,Üst Grup")]
        public ActionResult Create()
        {
            //var eczane = new Eczane { AcilisTarihi = DateTime.Today, Enlem = 0, Boylam = 0 };
            var ustGrupSession = _nobetUstGrupSessionService.GetSession("nobetUstGrup");
            //var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGruplar = _nobetUstGrupService.GetDetaylar(ustGrupSession.Id);
            ViewBag.NobetUstGrupId = new SelectList(nobetUstGruplar.Select(s => new { s.Id, s.Adi }), "Id", "Adi");

            return View();
        }

        // POST: EczaneNobet/Eczane/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Oda,Üst Grup")]
        public ActionResult Create([Bind(Include = "Id,Adi,AcilisTarihi,KapanisTarihi,Enlem,Boylam,Adres,TelefonNo,MailAdresi,WebSitesi,NobetUstGrupId")] Eczane eczane)
        {
            if (ModelState.IsValid)
            {
                _eczaneService.Insert(eczane);

                var eklenenEczane = _eczaneService.GetEczane(eczane.Adi, eczane.AcilisTarihi, eczane.NobetUstGrupId);

                TempData["EklenenEczane"] = eczane.Adi;

                var eczaneler = new int[1]
                {
                    eczane.Id
                };

                var eczaneNobetGrupCoklu = new EczaneNobetGrupCoklu
                {
                    EczaneId = eczaneler,
                    BaslangicTarihi = eczane.AcilisTarihi,
                };

                TempData["EklenecekEczane"] = eczaneNobetGrupCoklu;

                var eczanelerArasiUzakliklar = _eczaneUzaklikMatrisService.GetDetaylar(eczane.NobetUstGrupId);

                if (eczanelerArasiUzakliklar.Count > 0)
                {
                    EczanelerArasiMesafeleriEkle(eklenenEczane);
                }

                return RedirectToAction("Create", "EczaneNobetGrup");
                //return RedirectToAction("Index");
            }
            var ustGrupSession = _nobetUstGrupSessionService.GetSession("nobetUstGrup");
            var nobetUstGruplar = _nobetUstGrupService.GetDetaylar(ustGrupSession.Id);
            ViewBag.NobetUstGrupId = new SelectList(nobetUstGruplar, "Id", "Adi", eczane.NobetUstGrupId);

            return View(eczane);
        }

        // GET: EczaneNobet/Eczane/Edit/5
        [Authorize(Roles = "Admin,Oda,Üst Grup,Eczane")]
        public ActionResult Edit(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Eczane eczane = _eczaneService.GetById(id);
            if (eczane == null)
            {
                return HttpNotFound();
            }
            var ustGrupSession = _nobetUstGrupSessionService.GetSession("nobetUstGrup");
            var nobetUstGruplar = _nobetUstGrupService.GetDetaylar(ustGrupSession.Id);
            ViewBag.NobetUstGrupId = new SelectList(nobetUstGruplar.Select(s => new { s.Id, s.Adi }), "Id", "Adi");
            return View(eczane);
        }

        // POST: EczaneNobet/Eczane/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Oda,Üst Grup,Eczane")]
        public ActionResult Edit([Bind(Include = "Id,Adi,AcilisTarihi,KapanisTarihi,Enlem,Boylam,Adres,TelefonNo,MailAdresi,WebSitesi, NobetUstGrupId")] Eczane eczane)
        {
            if (ModelState.IsValid)
            {
                var eczaneMevcut = _eczaneService.GetById(eczane.Id);

                if (eczane.KapanisTarihi != null)
                {
                    var gruplardakiEczaneler = _eczaneNobetGrupService.GetGruptaAktifOlanEczanelerByEczaneId(eczane.Id);

                    foreach (var gruplardakiEczane in gruplardakiEczaneler)
                    {
                        KapananEczanelerinNobetGruplariniKapat(eczane, gruplardakiEczane);
                    }
                }

                _eczaneService.Update(eczane);

                EczanelerArasiMesafeleriGuncelle(eczane, eczaneMevcut);

                return RedirectToAction("Index");
            }
            var ustGrupSession = _nobetUstGrupSessionService.GetSession("nobetUstGrup");
            var nobetUstGruplar = _nobetUstGrupService.GetDetaylar(ustGrupSession.Id);
            ViewBag.NobetUstGrupId = new SelectList(nobetUstGruplar.Select(s => new { s.Id, s.Adi }), "Id", "Adi");
            return View(eczane);
        }

        private void EczanelerArasiMesafeleriGuncelle(Eczane eczaneYeni, Eczane eczaneEski)
        {
            if (eczaneEski.Enlem != eczaneYeni.Enlem
             || eczaneEski.Boylam != eczaneYeni.Boylam)
            {
                var eczanelerArasiUzakliklar = _eczaneUzaklikMatrisService.GetDetaylarByEczaneId(eczaneYeni.Id);

                foreach (var eczanelerArasiUzaklik in eczanelerArasiUzakliklar)
                {
                    var eczaneFrom = _eczaneService.GetById(eczanelerArasiUzaklik.EczaneIdFrom);
                    var eczaneTo = _eczaneService.GetById(eczanelerArasiUzaklik.EczaneIdTo);

                    var mesafe = _eczaneNobetOrtakService.EczanelerArasiMesafeHesaplaKusUcusu(eczaneFrom, eczaneTo);

                    var eczaneUzaklikMatris = new EczaneUzaklikMatris()
                    {
                        Id = eczanelerArasiUzaklik.Id,
                        EczaneIdFrom = eczaneFrom.Id,
                        EczaneIdTo = eczaneTo.Id,
                        Mesafe = mesafe.Mesafe
                    };

                    _eczaneUzaklikMatrisService.Update(eczaneUzaklikMatris);
                }
            }
        }

        private void EczanelerArasiMesafeleriEkle(Eczane eczane)
        {
            var eczaneler = _eczaneService.GetList(eczane.NobetUstGrupId);

            var eczaneUzaklikMatrisList = _eczaneNobetOrtakService.SetUzakliklarKusUcusuEczaneBazli(eczaneler, eczane);

            _eczaneUzaklikMatrisService.CokluEkle(eczaneUzaklikMatrisList);
        }

        private void KapananEczanelerinNobetGruplariniKapat(Eczane eczane, EczaneNobetGrup gruplardakiEczane)
        {
            var altGruplardakiEczaneler = _eczaneNobetGrupAltGrup.GetListAltGruptaAcikEczanelerByEczaneNobetGrupId(gruplardakiEczane.Id);

            KapananEczaneninAltGruplariniKapat(eczane, altGruplardakiEczaneler);

            var eczaneNobetGrup = new EczaneNobetGrup
            {
                Id = gruplardakiEczane.Id,
                EczaneId = gruplardakiEczane.EczaneId,
                NobetGrupGorevTipId = gruplardakiEczane.NobetGrupGorevTipId,
                Aciklama = gruplardakiEczane.Aciklama + " (Eczane Kapandı.)",
                BaslangicTarihi = gruplardakiEczane.BaslangicTarihi,
                BitisTarihi = eczane.KapanisTarihi
            };

            _eczaneNobetGrupService.Update(eczaneNobetGrup);
        }

        private void KapananEczaneninAltGruplariniKapat(Eczane eczane, List<EczaneNobetGrupAltGrup> altGruplardakiEczaneler)
        {
            foreach (var altGruplardakiEczane in altGruplardakiEczaneler)
            {
                var eczaneNobetAltGrup = new EczaneNobetGrupAltGrup
                {
                    Aciklama = altGruplardakiEczane.Aciklama + " (Eczane Kapandı.)",
                    BitisTarihi = eczane.KapanisTarihi,
                    Id = altGruplardakiEczane.Id,
                    BaslangicTarihi = altGruplardakiEczane.BaslangicTarihi,
                    EczaneNobetGrupId = altGruplardakiEczane.EczaneNobetGrupId,
                    NobetAltGrupId = altGruplardakiEczane.NobetAltGrupId
                };

                //altGruplardakiEczane.BitisTarihi = eczane.KapanisTarihi;
                //altGruplardakiEczane.Aciklama += " Eczane Kapandı";

                _eczaneNobetGrupAltGrup.Update(eczaneNobetAltGrup);
            }
        }

        // GET: EczaneNobet/Eczane/Delete/5
        [Authorize(Roles = "Admin,Oda,Üst Grup")]
        public ActionResult Delete(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Eczane eczane = _eczaneService.GetById(id);
            if (eczane == null)
            {
                return HttpNotFound();
            }
            return View(eczane);
        }

        // POST: EczaneNobet/Eczane/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Oda,Üst Grup")]
        public ActionResult DeleteConfirmed(int id)
        {
            Eczane eczane = _eczaneService.GetById(id);
            _eczaneService.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult EczanelerDdlPartialView(int nobetGrupId = 0)
        {
            var ustGrupSession = _nobetUstGrupSessionService.GetSession("nobetUstGrup");
            var eczaneler = _eczaneService.GetDetaylar(ustGrupSession.Id).Select(s => s.Id).ToList();

            var eczaneNobetGruplar = _eczaneNobetGrupService.GetDetaylarByEczaneIdList(eczaneler)
                .Where(w => w.NobetGrupId == nobetGrupId || nobetGrupId == 0)
                .Select(s => new MyDrop { Id = s.EczaneId, Value = $"{s.EczaneAdi}, {s.NobetGrupAdi}" }).Distinct()
                .OrderBy(o => o.Value).ToList();

            ViewBag.EczaneId = new SelectList(items: eczaneNobetGruplar, dataValueField: "Id", dataTextField: "Value");

            return PartialView(eczaneNobetGruplar);
        }

        public ActionResult EczanelerSingleDdlPartialView(int nobetGrupId = 0)
        {
            var ustGrupSession = _nobetUstGrupSessionService.GetSession("nobetUstGrup");
            var eczaneler = _eczaneService.GetDetaylar(ustGrupSession.Id).Select(s => s.Id).ToList();

            var eczaneNobetGruplar = _eczaneNobetGrupService.GetDetaylarByEczaneIdList(eczaneler)
                .Where(w => w.NobetGrupId == nobetGrupId || nobetGrupId == 0)
                .Select(s => new MyDrop { Id = s.EczaneId, Value = $"{s.EczaneAdi}, {s.NobetGrupAdi}" }).Distinct()
                .OrderBy(o => o.Value).ToList();

            ViewBag.EczaneId = new SelectList(items: eczaneNobetGruplar, dataValueField: "Id", dataTextField: "Value");

            return PartialView(eczaneNobetGruplar);
        }
    }
}
