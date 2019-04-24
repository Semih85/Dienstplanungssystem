﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
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
    public class EczaneNobetGrupController : Controller
    {
        #region ctor
        private IEczaneNobetGrupService _eczaneNobetGrupService;
        private IEczaneService _eczaneService;
        private INobetGrupGorevTipService _nobetGrupGorevTipService;
        private IUserService _userService;
        private INobetUstGrupService _nobetUstGrupService;
        private IAyniGunTutulanNobetService _ayniGunTutulanNobetService;
        private ITakvimService _takvimService;

        public EczaneNobetGrupController(IEczaneNobetGrupService eczaneNobetGrupService,
                                         IEczaneService eczaneService,
                                         INobetGrupGorevTipService nobetGrupGorevTipService,
                                         INobetUstGrupService nobetUstGrupService,
                                         IUserService userService,
                                         IAyniGunTutulanNobetService ayniGunTutulanNobetService,
                                         ITakvimService takvimService)
        {
            _eczaneNobetGrupService = eczaneNobetGrupService;
            _eczaneService = eczaneService;
            _nobetGrupGorevTipService = nobetGrupGorevTipService;
            _userService = userService;
            _nobetUstGrupService = nobetUstGrupService;
            _ayniGunTutulanNobetService = ayniGunTutulanNobetService;
            _takvimService = takvimService;
        }
        #endregion

        // GET: EczaneNobet/EczaneNobetGrup
        public ActionResult Index()
        {
            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetListByUser(user).Select(s => s.Id).ToList();

            var model = _eczaneNobetGrupService.GetDetaylarByNobetGrupGorevTipler(nobetGrupGorevTipler)
                .OrderBy(s => s.NobetGorevTipId)
                .ThenBy(s => s.NobetGrupAdi)
                .ThenBy(s => s.EczaneAdi).ToList();

            return View(model);
        }

        // GET: EczaneNobet/EczaneNobetGrup/Details/5
        public ActionResult Details(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EczaneNobetGrupDetay eczaneNobetGrup = _eczaneNobetGrupService.GetDetayById(id);
            if (eczaneNobetGrup == null)
            {
                return HttpNotFound();
            }
            return View(eczaneNobetGrup);
        }

        // GET: EczaneNobet/EczaneNobetGrup/Create
        [Authorize(Roles = "Admin,Oda,Üst Grup")]
        public ActionResult Create()
        {
            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGrup = _nobetUstGrupService.GetListByUser(user).FirstOrDefault();

            //var eczaneNobetGruplar = _eczaneNobetGrupService.GetDetaylar(nobetUstGrup.Id)
            //    //.Where(w => w.BitisTarihi == null || w.nobet)
            //    .Select(s => s.EczaneId).Distinct().ToList();

            var eczaneler = _eczaneService.GetDetaylar(nobetUstGrup.Id)
                .Where(s => s.KapanisTarihi == null
                        //&& !eczaneNobetGruplar.Contains(s.Id)
                        )
                .OrderBy(s => s.EczaneAdi).ToList();

            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrup.Id);

            var eczaneNobetGrupCoklu = new EczaneNobetGrupCoklu();

            if (TempData["EklenecekEczane"] != null)
            {
                eczaneNobetGrupCoklu = (EczaneNobetGrupCoklu)TempData["EklenecekEczane"];

                ViewBag.EczaneId = new SelectList(eczaneler, "Id", "EczaneAdi", eczaneNobetGrupCoklu.EczaneId.FirstOrDefault());
            }
            else
            {
                ViewBag.EczaneId = new SelectList(eczaneler, "Id", "EczaneAdi");
            }

            ViewBag.NobetGrupGorevTipId = new SelectList(nobetGrupGorevTipler, "Id", "NobetGrupGorevTipAdi");

            //var baslamaTarihi = new EczaneNobetGrup { BaslangicTarihi = DateTime.Now, Aciklama = "..... eczane bu gruba yeni katıldı." };
            return View(eczaneNobetGrupCoklu);
        }

        public ActionResult Search(string Keywords)
        {
            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetGruplar = _nobetGrupGorevTipService.GetListByUser(user);
            var model = _eczaneNobetGrupService.GetDetaylar()
                .Where(x => nobetGruplar.Select(s => s.Id).Contains(x.NobetGrupId)
                            && Regex.Split(Keywords, @"\s")
                            .Any(y => x.EczaneAdi.ToLower().Contains(y.ToLower())
                                   || x.NobetGrupAdi.ToLower().Contains(y.ToLower())
                            )
                            )
                .OrderBy(s => s.NobetGrupAdi).ThenBy(s => s.EczaneAdi);

            return View("Index", model);//result:model
        }

        // POST: EczaneNobet/EczaneNobetGrup/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Oda,Üst Grup")]
        public ActionResult Create([Bind(Include = "Id,EczaneId,NobetGrupGorevTipId,BaslangicTarihi,BitisTarihi,Aciklama,EnErkenTarihteNobetYazilsinMi")] EczaneNobetGrupCoklu eczaneNobetGrupCoklu)
        {
            var eczaneNobetGruplar = new List<EczaneNobetGrup>();

            foreach (var eczaneId in eczaneNobetGrupCoklu.EczaneId)
            {
                eczaneNobetGruplar.Add(new EczaneNobetGrup
                {
                    EczaneId = eczaneId,
                    NobetGrupGorevTipId = eczaneNobetGrupCoklu.NobetGrupGorevTipId,
                    BaslangicTarihi = eczaneNobetGrupCoklu.BaslangicTarihi,
                    BitisTarihi = eczaneNobetGrupCoklu.BitisTarihi,
                    Aciklama = eczaneNobetGrupCoklu.Aciklama
                });
            }

            var eklenecekEczaneSayisi = eczaneNobetGruplar.Count;

            if (ModelState.IsValid && eklenecekEczaneSayisi > 0)
            {
                var eklenenNobetGrupGorevTip = _nobetGrupGorevTipService.GetDetayById(eczaneNobetGrupCoklu.NobetGrupGorevTipId);

                TempData["EklenenEczane"] = $"{eklenenNobetGrupGorevTip.NobetGrupGorevTipAdi} nöbet grubuna {eczaneNobetGrupCoklu.EczaneId.Count()} adet eczane başarılı bir şekilde eklenmiştir.";

                _eczaneNobetGrupService.CokluEkle(eczaneNobetGruplar);

                var gruptakiEczaneler = _eczaneNobetGrupService.GetDetaylarByNobetGrupGorevTipler(eczaneNobetGrupCoklu.NobetGrupGorevTipId);
                var eczaneIdList = eczaneNobetGruplar.Select(s => s.EczaneId).ToList();
                var eczaneNobetGrupDetaylar = _eczaneNobetGrupService.GetDetaylar(eczaneIdList, eczaneNobetGrupCoklu.NobetGrupGorevTipId);

                if (eklenenNobetGrupGorevTip.NobetUstGrupId == 2)
                {//antalya'da planlanan nöbetleri yazmak için
                    if (eczaneNobetGrupDetaylar.Count > 0)
                    {//grupta eczaneler var. grup yeni değil. tekli olarak eklenen eczaneler için planlanan nöbetler yeniden yazılacak.
                        #region planlanan nöbetler - sıralı nöbet yazma (gün grubu bazında)

                        var baslangicTarihi = eczaneNobetGrupDetaylar.Min(s => s.BaslangicTarihi);

                        var planlananNobetlerinYazilacagiSonTarih = new DateTime(2020, 12, 31);

                        var planlananNobetlerinYazilacagiNobetGrubu = _nobetGrupGorevTipService.GetDetayById(eczaneNobetGrupCoklu.NobetGrupGorevTipId);

                        _takvimService.SiraliNobetYazGrupBazinda(planlananNobetlerinYazilacagiNobetGrubu, gruptakiEczaneler, baslangicTarihi, planlananNobetlerinYazilacagiSonTarih);

                        #endregion
                    }
                    else
                    {//gruba ilk kez eczane ekleniyor

                    }
                }

                var nobetGrupGorevTip = _nobetGrupGorevTipService.GetDetayById(eczaneNobetGrupCoklu.NobetGrupGorevTipId);
                var nobetUstGrupId = nobetGrupGorevTip.NobetUstGrupId;

                if (nobetUstGrupId == 1 || nobetUstGrupId == 3)
                {//anlanya ya da mersin için
                    var eklenenIkiliEczaneler = _ayniGunTutulanNobetService.IkiliEczaneleriOlustur(eczaneNobetGrupDetaylar);
                }

                ViewBag.EklenenEczaneSayisi = eklenecekEczaneSayisi;
                ViewBag.EklenenNobetGrupAdi = _nobetGrupGorevTipService.GetDetayById(eczaneNobetGrupCoklu.NobetGrupGorevTipId).NobetGrupAdi;
                return RedirectToAction("Index");
            }

            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGrup = _nobetUstGrupService.GetListByUser(user).FirstOrDefault();
            var nobetGruplar = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrup.Id);
            var eczaneler = _eczaneService.GetList(nobetUstGrup.Id).OrderBy(s => s.Adi).ToList();

            ViewBag.EczaneId = new SelectList(eczaneler, "Id", "Adi", eczaneNobetGrupCoklu.EczaneId);
            ViewBag.NobetGrupGorevTipId = new SelectList(nobetGruplar, "Id", "NobetGrupGorevTipAdi", eczaneNobetGrupCoklu.NobetGrupGorevTipId);
            return View(); //eczaneNobetGrup
        }

        // GET: EczaneNobet/EczaneNobetGrup/Edit/5
        [Authorize(Roles = "Admin,Oda,Üst Grup")]
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EczaneNobetGrup eczaneNobetGrup = _eczaneNobetGrupService.GetById(id);
            if (eczaneNobetGrup == null)
            {
                return HttpNotFound();
            }
            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGrup = _nobetUstGrupService.GetListByUser(user).FirstOrDefault();
            var nobetGruplar = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrup.Id);
            var eczaneler = _eczaneService.GetList(nobetUstGrup.Id).OrderBy(s => s.Adi).ToList();

            ViewBag.EczaneId = new SelectList(eczaneler, "Id", "Adi", eczaneNobetGrup.EczaneId);
            ViewBag.NobetGrupGorevTipId = new SelectList(nobetGruplar, "Id", "NobetGrupGorevTipAdi", eczaneNobetGrup.NobetGrupGorevTipId);
            return View(eczaneNobetGrup);
        }

        // POST: EczaneNobet/EczaneNobetGrup/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Oda,Üst Grup")]
        public ActionResult Edit([Bind(Include = "Id,EczaneId,NobetGrupGorevTipId,BaslangicTarihi,BitisTarihi,Aciklama,EnErkenTarihteNobetYazilsinMi")] EczaneNobetGrup eczaneNobetGrup)
        {
            if (ModelState.IsValid)
            {
                var degisecekEczaneNobetGrupOncekiHali = _eczaneNobetGrupService.GetDetayById(eczaneNobetGrup.Id);

                var eklenenNobetGrupGorevTip = _nobetGrupGorevTipService.GetDetayById(eczaneNobetGrup.NobetGrupGorevTipId);

                _eczaneNobetGrupService.Update(eczaneNobetGrup);

                var bitisTarihiDegistiMi = degisecekEczaneNobetGrupOncekiHali.BitisTarihi != eczaneNobetGrup.BitisTarihi;
                //var baslangicTarihiDegistiMi = degisecekEczaneNobetGrupOncekiHali.BaslangicTarihi != eczaneNobetGrup.BaslangicTarihi;

                if (eklenenNobetGrupGorevTip.NobetUstGrupId == 2
                    && (bitisTarihiDegistiMi 
                    //|| baslangicTarihiDegistiMi
                    )
                    )
                {//antalya'da planlanan nöbetleri yazmak için
                 //grupta eczaneler var. grup yeni değil. tekli olarak eklenen eczaneler için planlanan nöbetler yeniden yazılacak.
                    #region planlanan nöbetler - sıralı nöbet yazma (gün grubu bazında)

                    var gruptakiEczaneler = _eczaneNobetGrupService.GetDetaylarByNobetGrupGorevTipler(eczaneNobetGrup.NobetGrupGorevTipId);

                    var baslangicTarihi = eczaneNobetGrup.BaslangicTarihi;

                    //if (baslangicTarihiDegistiMi)
                    //{
                    //    baslangicTarihi = eczaneNobetGrup.BaslangicTarihi < degisecekEczaneNobetGrupOncekiHali.NobetGrupGorevTipBaslamaTarihi 
                    //        ? degisecekEczaneNobetGrupOncekiHali.NobetGrupGorevTipBaslamaTarihi
                    //        : eczaneNobetGrup.BaslangicTarihi;
                    //}

                    if (bitisTarihiDegistiMi 
                        && eczaneNobetGrup.BitisTarihi != null)
                    {
                        baslangicTarihi = (DateTime)eczaneNobetGrup.BitisTarihi;
                    }

                    var planlananNobetlerinYazilacagiSonTarih = new DateTime(2020, 12, 31);

                    var planlananNobetlerinYazilacagiNobetGrubu = _nobetGrupGorevTipService.GetDetayById(eczaneNobetGrup.NobetGrupGorevTipId);

                    _takvimService.SiraliNobetYazGrupBazinda(planlananNobetlerinYazilacagiNobetGrubu, gruptakiEczaneler, baslangicTarihi, planlananNobetlerinYazilacagiSonTarih);

                    #endregion
                }

                return RedirectToAction("Index");
            }
            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGrup = _nobetUstGrupService.GetListByUser(user).FirstOrDefault();
            var nobetGruplar = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrup.Id);
            var eczaneler = _eczaneService.GetList(nobetUstGrup.Id).OrderBy(s => s.Adi).ToList();

            ViewBag.EczaneId = new SelectList(eczaneler, "Id", "Adi", eczaneNobetGrup.EczaneId);
            ViewBag.NobetGrupGorevTipId = new SelectList(nobetGruplar, "Id", "NobetGrupGorevTipAdi", eczaneNobetGrup.NobetGrupGorevTipId);
            return View(eczaneNobetGrup);
        }

        // GET: EczaneNobet/EczaneNobetGrup/Delete/5
        [Authorize(Roles = "Admin,Oda")]
        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EczaneNobetGrupDetay eczaneNobetGrup = _eczaneNobetGrupService.GetDetayById(id);
            if (eczaneNobetGrup == null)
            {
                return HttpNotFound();
            }
            return View(eczaneNobetGrup);
        }

        // POST: EczaneNobet/EczaneNobetGrup/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EczaneNobetGrupDetay eczaneNobetGrup = _eczaneNobetGrupService.GetDetayById(id);
            _eczaneNobetGrupService.Delete(id);
            return RedirectToAction("Index");
        }

    }
}
