using System;
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
using WM.UI.Mvc.Areas.EczaneNobet.Models;
using WM.UI.Mvc.Models;
using WM.UI.Mvc.Services;

namespace WM.UI.Mvc.Areas.EczaneNobet.Controllers
{
    [Authorize]
    [HandleError]
    public class EczaneGrupTanimController : Controller
    {
        #region ctor
        private IEczaneGrupTanimService _eczaneGrupTanimService;
        private IEczaneGrupService _eczaneGrupService;
        private IEczaneService _eczaneService;
        private IEczaneGrupTanimTipService _eczaneGrupTanimTipService;
        private INobetUstGrupService _nobetUstGrupService;
        private IUserService _userService;
        private INobetGorevTipService _nobetGorevTipService;
        private INobetGrupGorevTipService _nobetGrupGorevTipService;
        private INobetUstGrupSessionService _nobetUstGrupSessionService;

        public EczaneGrupTanimController(IEczaneGrupTanimService eczaneGrupTanimService,
                                         IUserService userService,
                                         IEczaneGrupService eczaneGrupService,
                                         IEczaneService eczaneService,
                                         INobetUstGrupService nobetUstGrupService,
                                         IEczaneGrupTanimTipService eczaneGrupTanimTipService,
                                         INobetGorevTipService nobetGorevTipService,
                                         INobetGrupGorevTipService nobetGrupGorevTipService,
                                         INobetUstGrupSessionService nobetUstGrupSessionService)
        {
            _eczaneGrupTanimService = eczaneGrupTanimService;
            _eczaneGrupService = eczaneGrupService;
            _eczaneService = eczaneService;
            _userService = userService;
            _nobetUstGrupService = nobetUstGrupService;
            _eczaneGrupTanimTipService = eczaneGrupTanimTipService;
            _nobetGorevTipService = nobetGorevTipService;
            _nobetGrupGorevTipService = nobetGrupGorevTipService;
            _nobetUstGrupSessionService = nobetUstGrupSessionService;
        }
        #endregion
        // GET: EczaneNobet/EczaneGrupTanim
        public ActionResult Index()
        {
            //var user = _userService.GetByUserName(User.Identity.Name);
            //var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user).Select(s => s.Id).ToList();
            var ustGrupSession = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var eczaneGrupTanimlar = _eczaneGrupTanimService.GetDetaylar(ustGrupSession.Id)
                .OrderBy(o => o.NobetGorevTipAdi)
                .ThenBy(o => o.EczaneGrupTanimAdi).ToList();

            var eczaneGruptanimTipIdListe = eczaneGrupTanimlar.Select(s => s.EczaneGrupTanimTipId).Distinct().ToList();

            var eczaneGruptanimTipler = _eczaneGrupTanimTipService.GetList(eczaneGruptanimTipIdListe);

            ViewBag.EczaneGruptanimTipId = new SelectList(eczaneGruptanimTipler, "Id", "Adi");
            //var model = _nobetUstGrupService.GetNobetUstGrupDetaylar().Where(s => nobetUstGruplar.Contains(s.Id));
            return View(eczaneGrupTanimlar);
        }

        [HttpPost]
        // GET: EczaneNobet/EczaneGrupTanim/Details/5
        public ActionResult Details(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EczaneGrupTanimDetay eczaneGrupTanim = _eczaneGrupTanimService.GetDetayById(id);
            if (eczaneGrupTanim == null)
            {
                return HttpNotFound();
            }
            return View(eczaneGrupTanim);
        }

        public List<EczaneGrupTanimDetaylarViewModel> SearchMethod(string Keywords, int? EczaneGruptanimTipId = 0)
        {
            var eczaneGrupTanimDetaylar = new List<EczaneGrupTanimDetaylarViewModel>();

            if (Keywords == null)
                Keywords = "";
            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user).Select(s => s.Id);

            var ustGrupSession = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var eczaneGrupTanimlarTumu = _eczaneGrupTanimService.GetDetaylar(ustGrupSession.Id);
            //.Where(s => nobetUstGruplar.Contains(s.NobetUstGrupId));

            var eczaneGrupTanimlar = eczaneGrupTanimlarTumu//.Where(s => nobetUstGruplar.Contains(s.NobetUstGrupId))
                    .Where(p => Regex.Split(Keywords, @"\s")
                    .Any(x => p.Adi.ToLower().Contains(x.ToLower()) || p.Adi.ToLower().Contains(x.ToLower()))).ToList();

            if (EczaneGruptanimTipId != 0)
            {
                eczaneGrupTanimlar = eczaneGrupTanimlarTumu//.Where(s => nobetUstGruplar.Contains(s.NobetUstGrupId))
                  .Where(p => p.EczaneGrupTanimTipId == EczaneGruptanimTipId && Regex.Split(Keywords, @"\s")
                  .Any(x => p.Adi.ToLower().Contains(x.ToLower()) || p.Adi.ToLower().Contains(x.ToLower()))).ToList();
            }

            var eczaneGruptanimTipIdListe = eczaneGrupTanimlarTumu.Select(s => s.EczaneGrupTanimTipId).Distinct().ToList();

            var eczaneGruptanimTipler = _eczaneGrupTanimTipService.GetList()
                .Where(w => eczaneGruptanimTipIdListe.Contains(w.Id));

            ViewBag.EczaneGruptanimTipId = new SelectList(eczaneGruptanimTipler, "Id", "Adi");

            /////////////////////////////////

            var eczaneGrupDetaylar = _eczaneGrupService.GetDetaylar(ustGrupSession.Id);

            //var fff = eczaneGrupDetaylar.Where(w => w.EczaneGrupTanimAdi == "3-4(1)");

            foreach (var item in eczaneGrupTanimlar)
            {
                if (item.Adi == "3-4(1)")
                {

                }

                var eczaneGrupDetays = eczaneGrupDetaylar
                    .Where(w => w.EczaneGrupTanimId == item.Id)
                    .OrderBy(o => o.NobetGrupId)
                    .ThenBy(t => t.EczaneAdi)
                    .ToList();

                eczaneGrupTanimDetaylar.Add(new EczaneGrupTanimDetaylarViewModel
                {
                    EczaneGrupTanimDetay = item,
                    EczaneGrupDetaylar = eczaneGrupDetays,
                    Keyword = Keywords,
                    EczaneGruptanimTipId = EczaneGruptanimTipId
                });
            }

            return eczaneGrupTanimDetaylar.OrderBy(o => o.EczaneGrupTanimDetay.NobetGorevTipAdi).ThenBy(o => o.EczaneGrupTanimDetay.Adi).ToList();
        }

        public ActionResult SearchWithEczaneAdi(string Keywords, int? EczaneGruptanimTipId = 0)
        {
            var eczaneGrupTanimDetaylar = SearchMethod(Keywords, EczaneGruptanimTipId);

            return PartialView("EczaneGrupTanimPartialView", eczaneGrupTanimDetaylar);
        }

        public ActionResult PasifYapEczaneGrupTanimTipAdi(int EczaneGruptanimTipId = 0)
        {
            //var user = _userService.GetByUserName(User.Identity.Name);
            //var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user).Select(s => s.Id);
            var ustGrupSession = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var eczaneGrupTanimlarTumu = _eczaneGrupTanimService.GetDetaylar(ustGrupSession.Id);
            //.Where(s => nobetUstGruplar.Contains(s.NobetUstGrupId));

            var eczaneGruptanimTipIdListe = eczaneGrupTanimlarTumu.Select(s => s.EczaneGrupTanimTipId).Distinct().ToList();

            var eczaneGruptanimTipler = _eczaneGrupTanimTipService.GetList()
                .Where(w => eczaneGruptanimTipIdListe.Contains(w.Id)).ToList();

            ViewBag.EczaneGruptanimTipId = new SelectList(eczaneGruptanimTipler, "Id", "Adi", EczaneGruptanimTipId);

            var eczaneGrupTanimlar = eczaneGrupTanimlarTumu
                .Where(s => (s.EczaneGrupTanimTipId == EczaneGruptanimTipId || EczaneGruptanimTipId == 0)).ToList();

            foreach (var item in eczaneGrupTanimlar)
            {
                var eczaneGrupTanim = new EczaneGrupTanim();
                eczaneGrupTanim = _eczaneGrupTanimService.GetById(item.Id);
                eczaneGrupTanim.PasifMi = true;
                _eczaneGrupTanimService.Update(eczaneGrupTanim);
            }

            var model = eczaneGrupTanimlarTumu //_eczaneGrupTanimService.GetDetaylar()
                                               //.Where(s => nobetUstGruplar.Contains(s.NobetUstGrupId))
                .Where(w => (w.EczaneGrupTanimTipId == EczaneGruptanimTipId || EczaneGruptanimTipId == 0));

            return View("Index", model);//result:model
        }

        public ActionResult AktifYapEczaneGrupTanimTipAdi(int? inp)
        {
            if (inp == null)
            {
                inp = 0;
            }

            var EczaneGruptanimTipId = inp;
            //var user = _userService.GetByUserName(User.Identity.Name);
            //var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user).Select(s => s.Id);
            var ustGrupSession = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var eczaneGrupTanimlarTumu = _eczaneGrupTanimService.GetDetaylar(ustGrupSession.Id);
            //.Where(s => nobetUstGruplar.Contains(s.NobetUstGrupId));

            var eczaneGruptanimTipIdListe = eczaneGrupTanimlarTumu.Select(s => s.EczaneGrupTanimTipId).Distinct().ToList();

            var eczaneGruptanimTipler = _eczaneGrupTanimTipService.GetList()
                .Where(w => eczaneGruptanimTipIdListe.Contains(w.Id));

            ViewBag.EczaneGruptanimTipId = new SelectList(eczaneGruptanimTipler, "Id", "Adi", EczaneGruptanimTipId);

            var eczaneGrupTanimlar = _eczaneGrupTanimService.GetList()
                .Where(w => w.EczaneGrupTanimTipId == EczaneGruptanimTipId || EczaneGruptanimTipId == 0);

            foreach (var item in eczaneGrupTanimlar)
            {
                var eczaneGrupTanim = new EczaneGrupTanim();
                eczaneGrupTanim = _eczaneGrupTanimService.GetById(item.Id);
                eczaneGrupTanim.PasifMi = false;
                _eczaneGrupTanimService.Update(eczaneGrupTanim);
            }

            var model = eczaneGrupTanimlarTumu //_eczaneGrupTanimService.GetDetaylar()
                                               //.Where(s => nobetUstGruplar.Contains(s.NobetUstGrupId))
                .Where(w => w.EczaneGrupTanimTipId == EczaneGruptanimTipId || EczaneGruptanimTipId == 0);

            return View("Index", model);//result:model
        }

        public ActionResult PasifYap(int Id)
        {
            //var user = _userService.GetByUserName(User.Identity.Name);
            //var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user).Select(s => s.Id);

            var ustGrupSession = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var eczaneGrupTanimlarTumu = _eczaneGrupTanimService.GetDetaylar(ustGrupSession.Id);
            //.Where(s => nobetUstGruplar.Contains(s.NobetUstGrupId));

            var eczaneGruptanimTipIdListe = eczaneGrupTanimlarTumu
                .Select(s => s.EczaneGrupTanimTipId).Distinct().ToList();

            var eczaneGruptanimTipler = _eczaneGrupTanimTipService.GetList()
                .Where(w => eczaneGruptanimTipIdListe.Contains(w.Id));

            var eczaneGrupTanimDetay = _eczaneGrupTanimService.GetDetayById(Id);
            var eczaneGrupTanim = new EczaneGrupTanim();

            eczaneGrupTanim = _eczaneGrupTanimService.GetById(Id);

            if (eczaneGrupTanim.PasifMi)
                eczaneGrupTanim.PasifMi = false;
            else
                eczaneGrupTanim.PasifMi = true;

            _eczaneGrupTanimService.Update(eczaneGrupTanim);

            ViewBag.EczaneGruptanimTipId = new SelectList(eczaneGruptanimTipler, "Id", "Adi");

            var model = eczaneGrupTanimlarTumu;
            //_eczaneGrupTanimService.GetDetaylar()
            //    .Where(s => nobetUstGruplar.Contains(s.NobetUstGrupId));

            //return View("Index", model);
            return PartialView("EczaneGrupTanimPartialView", model);
        }

        // GET: EczaneNobet/EczaneGrupTanim/Create
        public ActionResult Create()
        {
            //var user = _userService.GetByUserName(User.Identity.Name);
            //var nobetUstGrupId = nobetUstGruplar.Select(s => s.Id).FirstOrDefault();

            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var nobetUstGruplar = _nobetUstGrupService.GetDetaylar(nobetUstGrup.Id);

            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrup.Id);

            ViewBag.NobetUstGrupId = new SelectList(nobetUstGruplar.Select(s => new { s.Id, s.Adi }), "Id", "Adi");
            ViewBag.EczaneGrupTanimTipId = new SelectList(_eczaneGrupTanimTipService.GetList().Select(s => new { s.Id, s.Adi }), "Id", "Adi");
            ViewBag.NobetGorevTipId = new SelectList(_nobetGorevTipService.GetList(nobetGrupGorevTipler.Select(s => s.NobetGorevTipId).ToList()), "Id", "Adi");
            return View();
        }

        // POST: EczaneNobet/EczaneGrupTanim/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Adi,ArdisikNobetSayisi,Aciklama,BaslangicTarihi,BitisTarihi,NobetUstGrupId,NobetGorevTipId,AyniGunNobetTutabilecekEczaneSayisi,EczaneGrupTanimTipId,PasifMi")] EczaneGrupTanim eczaneGrupTanim)
        {
            if (ModelState.IsValid)
            {
                _eczaneGrupTanimService.Insert(eczaneGrupTanim);
                return RedirectToAction("Index");
            }
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var nobetUstGruplar = _nobetUstGrupService.GetDetaylar(nobetUstGrup.Id);

            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrup.Id);

            ViewBag.NobetUstGrupId = new SelectList(nobetUstGruplar, "Id", "Adi", eczaneGrupTanim.NobetUstGrupId);
            ViewBag.EczaneGrupTanimTipId = new SelectList(_eczaneGrupTanimTipService.GetList().Select(s => new { s.Id, s.Adi }), "Id", "Adi", eczaneGrupTanim.EczaneGrupTanimTipId);
            ViewBag.NobetGorevTipId = new SelectList(_nobetGorevTipService.GetList(nobetGrupGorevTipler.Select(s => s.NobetGorevTipId).ToList()), "Id", "Adi", eczaneGrupTanim.NobetGorevTipId);
            return View(eczaneGrupTanim);
        }

        // GET: EczaneNobet/EczaneGrupTanim/Edit/5
        public ActionResult Edit(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EczaneGrupTanim eczaneGrupTanim = _eczaneGrupTanimService.GetById(id);
            if (eczaneGrupTanim == null)
            {
                return HttpNotFound();
            }
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var nobetUstGruplar = _nobetUstGrupService.GetDetaylar(nobetUstGrup.Id);

            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrup.Id);

            ViewBag.NobetUstGrupId = new SelectList(nobetUstGruplar, "Id", "Adi", eczaneGrupTanim.NobetUstGrupId);
            ViewBag.EczaneGrupTanimTipId = new SelectList(_eczaneGrupTanimTipService.GetList().Select(s => new { s.Id, s.Adi }), "Id", "Adi", eczaneGrupTanim.EczaneGrupTanimTipId);
            ViewBag.NobetGorevTipId = new SelectList(_nobetGorevTipService.GetList(nobetGrupGorevTipler.Select(s => s.NobetGorevTipId).ToList()), "Id", "Adi", eczaneGrupTanim.NobetGorevTipId);

            return View(eczaneGrupTanim);
        }

        // POST: EczaneNobet/EczaneGrupTanim/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Adi,ArdisikNobetSayisi,Aciklama,BaslangicTarihi,BitisTarihi,NobetUstGrupId,NobetGorevTipId,AyniGunNobetTutabilecekEczaneSayisi,EczaneGrupTanimTipId,PasifMi")] EczaneGrupTanim eczaneGrupTanim)
        {
            if (ModelState.IsValid)
            {
                _eczaneGrupTanimService.Update(eczaneGrupTanim);
                return RedirectToAction("Index");
            }
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var nobetUstGruplar = _nobetUstGrupService.GetDetaylar(nobetUstGrup.Id);

            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrup.Id);

            ViewBag.NobetUstGrupId = new SelectList(nobetUstGruplar.Select(s => new { s.Id, s.Adi }), "Id", "Adi", eczaneGrupTanim.NobetUstGrupId);
            ViewBag.EczaneGrupTanimTipId = new SelectList(_eczaneGrupTanimTipService.GetList().Select(s => new { s.Id, s.Adi }), "Id", "Adi", eczaneGrupTanim.EczaneGrupTanimTipId);
            ViewBag.NobetGorevTipId = new SelectList(_nobetGorevTipService.GetList(nobetGrupGorevTipler.Select(s => s.NobetGorevTipId).ToList()), "Id", "Adi", eczaneGrupTanim.NobetGorevTipId);
            return View(eczaneGrupTanim);
        }

        // GET: EczaneNobet/EczaneGrupTanim/Delete/5
        public ActionResult Delete(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EczaneGrupTanimDetay eczaneGrupTanim = _eczaneGrupTanimService.GetDetayById(id);
            if (eczaneGrupTanim == null)
            {
                return HttpNotFound();
            }
            return View(eczaneGrupTanim);
        }

        [HttpPost]
        public ActionResult SecilenleriAktifYap(string aktifYapilacakEczaneGrupTanimlar, string Keywords, string ExpandedForAktif, int? EczaneGruptanimTipIdForAktif = 0)
        {
            var nobetUstGruplar = new List<int>();
            var uyariMesaji = "Seçim Yapmadınız!";

            if (aktifYapilacakEczaneGrupTanimlar == null)
            {
                return Json(uyariMesaji, JsonRequestBehavior.AllowGet);
            }

            //search yapılmış listeyi tekrar döndermek için:
            var eczaneGrupTanimDetaylar = new List<EczaneGrupTanimDetaylarViewModel>();
            eczaneGrupTanimDetaylar = SearchMethod(Keywords, EczaneGruptanimTipIdForAktif);

            Int32 basamak = aktifYapilacakEczaneGrupTanimlar.IndexOf(';');
            Int32 toplam = aktifYapilacakEczaneGrupTanimlar.Length;

            var eczaneGrupTanimlar = aktifYapilacakEczaneGrupTanimlar.Substring(0, basamak);

            var eczaneGruplar = aktifYapilacakEczaneGrupTanimlar.Substring(basamak + 1, toplam - basamak - 1);

            var liste = eczaneGrupTanimlar.Split(',');
            var durumuDegisenEczaneGrupTanimListesi = "";
            //eczaneGrupTanim lari update 
            if (liste[0].Length > 0)
            {
                foreach (string item in liste)
                {
                    var eczaneGrupTanimOrj = _eczaneGrupTanimService.GetById(Convert.ToInt32(item));

                    var eczaneGrupTanim = new EczaneGrupTanim
                    {
                        Adi = eczaneGrupTanimOrj.Adi,
                        Aciklama = eczaneGrupTanimOrj.Aciklama,
                        ArdisikNobetSayisi = eczaneGrupTanimOrj.ArdisikNobetSayisi,
                        AyniGunNobetTutabilecekEczaneSayisi = eczaneGrupTanimOrj.AyniGunNobetTutabilecekEczaneSayisi,
                        BaslangicTarihi = eczaneGrupTanimOrj.BaslangicTarihi,
                        BitisTarihi = eczaneGrupTanimOrj.BitisTarihi,
                        EczaneGrupTanimTipId = eczaneGrupTanimOrj.EczaneGrupTanimTipId,
                        Id = eczaneGrupTanimOrj.Id,
                        NobetGorevTipId = eczaneGrupTanimOrj.NobetGorevTipId,
                        NobetUstGrupId = eczaneGrupTanimOrj.NobetUstGrupId,
                        PasifMi = false
                    };

                    _eczaneGrupTanimService.Update(eczaneGrupTanim);

                    nobetUstGruplar.Add(Convert.ToInt32(item));

                    if (liste.Count() > 1)
                        durumuDegisenEczaneGrupTanimListesi += ", " + eczaneGrupTanim.Adi;
                    else
                        durumuDegisenEczaneGrupTanimListesi += eczaneGrupTanim.Adi;

                    foreach (var eczaneGrupTanimDetays in eczaneGrupTanimDetaylar)
                    {
                        if (eczaneGrupTanimDetays.EczaneGrupTanimDetay.Id == Convert.ToInt32(item))
                        {
                            eczaneGrupTanimDetays.EczaneGrupTanimDetay.Checked = true;
                            eczaneGrupTanimDetays.EczaneGrupTanimDetay.PasifMi = false;
                        }
                    }
                }
            }

            var liste2 = eczaneGruplar.Split(',');
            var durumuDegisenEczaneListesi = "";
            //eczaneGrupları update

            if (liste2[0].Length > 0)
            {
                foreach (string item in liste2)
                {
                    var eczaneGrupOrj = _eczaneGrupService.GetById(Convert.ToInt32(item));

                    var eczaneGrup = new EczaneGrup
                    {
                        EczaneGrupTanimId = eczaneGrupOrj.EczaneGrupTanimId,
                        EczaneId = eczaneGrupOrj.EczaneId,
                        Id = eczaneGrupOrj.Id,
                        BirlikteNobetYazilsinMi = eczaneGrupOrj.BirlikteNobetYazilsinMi,
                        PasifMi = false
                    };

                    _eczaneGrupService.Update(eczaneGrup);

                    if (liste2.Count() > 1)
                        durumuDegisenEczaneListesi += ", " + _eczaneService.GetById(eczaneGrup.EczaneId).Adi;
                    else
                        durumuDegisenEczaneListesi += _eczaneService.GetById(eczaneGrup.EczaneId).Adi;
                    foreach (var eczaneGrupTanimDetays in eczaneGrupTanimDetaylar)
                    {
                        foreach (var myEczaneGrupDetay in eczaneGrupTanimDetays.EczaneGrupDetaylar)
                        {
                            if (Convert.ToInt32(item) == Convert.ToInt32(myEczaneGrupDetay.Id))
                            {
                                myEczaneGrupDetay.Checked = true;
                                myEczaneGrupDetay.PasifMi = false;
                            }
                        }
                    }
                }
            }

            TempData["DurumuDegisenGrupTanimSayisi"] = durumuDegisenEczaneGrupTanimListesi + " " + durumuDegisenEczaneListesi + " durumları aktif yapılmıştır.";

            ViewBag.NobetUstGrupId = new SelectList(nobetUstGruplar, "Id", "Adi");
            ViewBag.EczaneGrupTanimTipId = new SelectList(_eczaneGrupTanimTipService.GetList().Select(s => new { s.Id, s.Adi }), "Id", "Adi");

            var liste3 = ExpandedForAktif.Split(',');
            if (liste3[0].Length > 0)
            {
                foreach (string item in liste3)
                {
                    foreach (var eczaneGrupTanimDetays in eczaneGrupTanimDetaylar)
                    {
                        if (eczaneGrupTanimDetays.EczaneGrupTanimDetay.Id == Convert.ToInt32(item))
                        {
                            eczaneGrupTanimDetays.EczaneGrupTanimDetay.Expanded = true;
                        }
                    }
                }
            }

            return PartialView("EczaneGrupTanimPartialView", eczaneGrupTanimDetaylar);
        }

        [HttpPost]
        public ActionResult SecilenleriPasifYap(string pasifYapilacakEczaneGrupTanimlar, string Keywords, string ExpandedForPasif, int? EczaneGruptanimTipIdForPasif = 0)
        {
            var nobetUstGruplar = new List<int>();
            var uyariMesaji = "Seçim Yapmadınız!";

            if (pasifYapilacakEczaneGrupTanimlar == null)
            {
                return Json(uyariMesaji, JsonRequestBehavior.AllowGet);
            }

            //search yapılmış listeyi tekrar döndermek için:
            List<EczaneGrupTanimDetaylarViewModel> eczaneGrupTanimDetaylar = new List<EczaneGrupTanimDetaylarViewModel>();
            eczaneGrupTanimDetaylar = SearchMethod(Keywords, EczaneGruptanimTipIdForPasif);

            Int32 basamak = pasifYapilacakEczaneGrupTanimlar.IndexOf(';');
            Int32 toplam = pasifYapilacakEczaneGrupTanimlar.Length;

            var eczaneGrupTanimlar = pasifYapilacakEczaneGrupTanimlar.Substring(0, basamak);

            var eczaneGruplar = pasifYapilacakEczaneGrupTanimlar.Substring(basamak + 1, toplam - basamak - 1);

            var liste = eczaneGrupTanimlar.Split(',');
            var durumuDegisenEczaneGrupTanimListesi = "";
            //eczaneGrupTanim lari update 
            if (liste[0].Length > 0)
            {
                foreach (string item in liste)
                {
                    var eczaneGrupTanimOrj = _eczaneGrupTanimService.GetById(Convert.ToInt32(item));

                    var eczaneGrupTanim = new EczaneGrupTanim
                    {
                        Adi = eczaneGrupTanimOrj.Adi,
                        Aciklama = eczaneGrupTanimOrj.Aciklama,
                        ArdisikNobetSayisi = eczaneGrupTanimOrj.ArdisikNobetSayisi,
                        AyniGunNobetTutabilecekEczaneSayisi = eczaneGrupTanimOrj.AyniGunNobetTutabilecekEczaneSayisi,
                        BaslangicTarihi = eczaneGrupTanimOrj.BaslangicTarihi,
                        BitisTarihi = eczaneGrupTanimOrj.BitisTarihi,
                        EczaneGrupTanimTipId = eczaneGrupTanimOrj.EczaneGrupTanimTipId,
                        Id = eczaneGrupTanimOrj.Id,
                        NobetGorevTipId = eczaneGrupTanimOrj.NobetGorevTipId,
                        NobetUstGrupId = eczaneGrupTanimOrj.NobetUstGrupId,
                        PasifMi = true
                    };

                    _eczaneGrupTanimService.Update(eczaneGrupTanim);
                    nobetUstGruplar.Add(Convert.ToInt32(item));
                    if (liste.Count() > 1)
                        durumuDegisenEczaneGrupTanimListesi += ", " + eczaneGrupTanim.Adi;
                    else
                        durumuDegisenEczaneGrupTanimListesi += eczaneGrupTanim.Adi;
                    foreach (var eczaneGrupTanimDetays in eczaneGrupTanimDetaylar)
                    {
                        if (eczaneGrupTanimDetays.EczaneGrupTanimDetay.Id == Convert.ToInt32(item))
                        {
                            eczaneGrupTanimDetays.EczaneGrupTanimDetay.Checked = true;
                            eczaneGrupTanimDetays.EczaneGrupTanimDetay.PasifMi = true;
                        }
                    }
                }
            }

            var liste2 = eczaneGruplar.Split(',');
            var durumuDegisenEczaneListesi = "";
            //eczaneGrupları update
            if (liste2[0].Length > 0)
            {
                foreach (string item in liste2)
                {
                    var eczaneGrupOrj = _eczaneGrupService.GetById(Convert.ToInt32(item));

                    var eczaneGrup = new EczaneGrup
                    {
                        EczaneGrupTanimId = eczaneGrupOrj.EczaneGrupTanimId,
                        EczaneId = eczaneGrupOrj.EczaneId,
                        Id = eczaneGrupOrj.Id,
                        BirlikteNobetYazilsinMi = eczaneGrupOrj.BirlikteNobetYazilsinMi,
                        PasifMi = true
                    };

                    _eczaneGrupService.Update(eczaneGrup);

                    if (liste2.Count() > 1)
                        durumuDegisenEczaneListesi += ", " + _eczaneService.GetById(eczaneGrup.EczaneId).Adi;
                    else
                        durumuDegisenEczaneListesi += _eczaneService.GetById(eczaneGrup.EczaneId).Adi;

                    foreach (var eczaneGrupTanimDetays in eczaneGrupTanimDetaylar)
                    {
                        foreach (var myEczaneGrupDetay in eczaneGrupTanimDetays.EczaneGrupDetaylar)
                        {
                            if (Convert.ToInt32(item) == Convert.ToInt32(myEczaneGrupDetay.Id))
                            {
                                myEczaneGrupDetay.Checked = true;
                                myEczaneGrupDetay.PasifMi = true;
                            }
                        }
                    }
                }
            }

            TempData["DurumuDegisenGrupTanimSayisi"] = durumuDegisenEczaneGrupTanimListesi + " " + durumuDegisenEczaneListesi + " durumları pasif yapılmıştır.";

            ViewBag.NobetUstGrupId = new SelectList(nobetUstGruplar, "Id", "Adi");
            ViewBag.EczaneGrupTanimTipId = new SelectList(_eczaneGrupTanimTipService.GetList().Select(s => new { s.Id, s.Adi }), "Id", "Adi");

            var liste3 = ExpandedForPasif.Split(',');
            if (liste3[0].Length > 0)
            {
                foreach (string item in liste3)
                {
                    foreach (var eczaneGrupTanimDetays in eczaneGrupTanimDetaylar)
                    {
                        if (eczaneGrupTanimDetays.EczaneGrupTanimDetay.Id == Convert.ToInt32(item))
                        {
                            eczaneGrupTanimDetays.EczaneGrupTanimDetay.Expanded = true;
                        }
                    }
                }
            }

            return PartialView("EczaneGrupTanimPartialView", eczaneGrupTanimDetaylar);
        }

        //[AcceptVerbs(HttpVerbs.Get)]
        //[ChildActionOnly]​

        // POST: EczaneNobet/EczaneGrupTanim/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EczaneGrupTanimDetay eczaneGrupTanim = _eczaneGrupTanimService.GetDetayById(id);
            _eczaneGrupTanimService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
