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
    [Authorize]
    [HandleError]
    public class NobetUstGrupKisitController : Controller
    {
        #region ctor
        private INobetUstGrupKisitService _nobetUstGrupKisitService;
        private IKisitService _kisitService;
        private INobetUstGrupService _nobetUstGrupService;
        private IUserService _userService;
        private INobetUstGrupSessionService _nobetUstGrupSessionService;
        private INobetUstGrupKisitSessionService _nobetUstGrupKisitSessionService;
        private INobetGrupGorevTipService _nobetGrupGorevTipService;

        public NobetUstGrupKisitController(INobetUstGrupKisitService nobetUstGrupKisitService,
            IKisitService kisitService,
            INobetUstGrupService nobetUstGrupService,
            IUserService userService,
            INobetUstGrupSessionService nobetUstGrupSessionService,
            INobetUstGrupKisitSessionService nobetUstGrupKisitSessionService,
            INobetGrupGorevTipService nobetGrupGorevTipService)
        {
            _nobetUstGrupKisitService = nobetUstGrupKisitService;
            _kisitService = kisitService;
            _nobetUstGrupService = nobetUstGrupService;
            _userService = userService;
            _nobetUstGrupSessionService = nobetUstGrupSessionService;
            _nobetGrupGorevTipService = nobetGrupGorevTipService;
            _nobetUstGrupKisitSessionService = nobetUstGrupKisitSessionService;
        }
        #endregion

        // GET: EczaneNobet/NobetUstGrupKisit
        [Authorize(Roles = "Admin,Oda,Üst Grup")]
        public ActionResult Index()
        {
            //var user = _userService.GetByUserName(User.Identity.Name);
            //var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user).Select(s => s.Id);
            //var rolIdler = _userService.GetUserRoles(user).OrderBy(s => s.RoleId).Select(u => u.RoleId).ToArray();
            //var rolId = rolIdler.FirstOrDefault();
            //ViewBag.RolId = rolId;
            var nobetUstGrupDetay = _nobetUstGrupSessionService.GetSession("nobetUstGrup");
            var nobetUstGrupKisitSession = _nobetUstGrupKisitSessionService.GetSessionList("nobetUstGrupKisitSession", nobetUstGrupDetay.Id);

            var model = _nobetUstGrupKisitService.GetDetaylar(nobetUstGrupDetay.Id)
                //.OrderByDescending(o => o.PasifMi)
                //.ThenBy(o => o.NobetUstGrupId)
                .OrderBy(r => r.KisitAdi);

            return View(model);
        }

        [Authorize(Roles = "Admin,Oda,Üst Grup")]
        public ActionResult KisitAyarla()
        {
            //var user = _userService.GetByUserName(User.Identity.Name);
            //var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            //var rolIdler = _userService.GetUserRoles(user).OrderBy(s => s.RoleId).Select(u => u.RoleId).ToArray();
            ////var rolId = rolIdler.FirstOrDefault();
            ////ViewBag.RolId = rolId;
            //var nobetUstGrup = nobetUstGruplar.FirstOrDefault();
            var nobetUstGrupDetay = _nobetUstGrupSessionService.GetSession("nobetUstGrup");
            //_nobetUstGrupService.GetDetay(nobetUstGrup.Id);
            var nobetUstGrupKisitSession = _nobetUstGrupKisitSessionService.GetSessionList("nobetUstGrupKisitSession", nobetUstGrupDetay.Id);

            var kisitlar = _nobetUstGrupKisitService.GetDetaylar(nobetUstGrupDetay.Id)
                //.Where(w => nobetUstGruplar.Contains(w.NobetUstGrupId))
                .OrderBy(o => o.KisitAdiGosterilen).ToList();

            var nobetGrupGorevTipSayisi = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrupDetay.Id).Count();

            TempData["NobetGrupGorevTipSayisi"] = nobetGrupGorevTipSayisi;

            var model = new KisitAyarlaViewModel
            {
                Kisitlar = kisitlar
            };

            return View(model);
        }

        [ChildActionOnly]
        public PartialViewResult KuralKategorilerPartialView(string tabId, string kisitTuru, IEnumerable<NobetUstGrupKisitDetay> nobetUstGrupKisitDetaylar)
        {
            var model = new KuralKategorilerPartialViewModel()
            {
                TabId = tabId,
                KisitTuru = kisitTuru,
                NobetUstGrupKisitDetaylar = nobetUstGrupKisitDetaylar
            };

            return PartialView(model);
        }

        public JsonResult EditAjax(int id, bool pasifMi, double sagTarafDegeri)
        {
            var kisitOnce = _nobetUstGrupKisitService.GetDetayById(id);
            var kisitOrj = new NobetUstGrupKisit
            {
                Id = kisitOnce.Id,
                //Aciklama = kisitOnce,
                KisitId = kisitOnce.KisitId,
                NobetUstGrupId = kisitOnce.NobetUstGrupId,
                PasifMi = kisitOnce.PasifMi,
                SagTarafDegeri = kisitOnce.SagTarafDegeri,
                SagTarafDegeriVarsayilan = kisitOnce.SagTarafDegeriVarsayilan,
                VarsayilanPasifMi = kisitOnce.VarsayilanPasifMi
            };

            //TempData["KisitDuzenleSonuc"] = $"Kısıt: {kisit.KisitId} ({kisit.KisitKategoriAdi} / {kisit.KisitAdiGosterilen})";

            //TempData["KisitDuzenleSonuc0"] = kisit.KisitId;
            //TempData["KisitDuzenleSonuc1"] = kisit.KisitKategoriAdi;
            //TempData["KisitDuzenleSonuc2"] = kisit.KisitAdiGosterilen;

            kisitOrj.PasifMi = !pasifMi;
            kisitOrj.SagTarafDegeri = sagTarafDegeri;

            try
            {
                var nobetUstGrupKisitSession = _nobetUstGrupKisitSessionService.GetSessionList("nobetUstGrupKisitSession", kisitOnce.NobetUstGrupId);

                _nobetUstGrupKisitService.Update(kisitOrj);

                var kisitSonra = _nobetUstGrupKisitService.GetDetayById(kisitOrj.Id);

                _nobetUstGrupKisitSessionService.AddSessionList(kisitOnce, kisitSonra, "nobetUstGrupKisitSession", nobetUstGrupKisitSession);

                nobetUstGrupKisitSession = _nobetUstGrupKisitSessionService.GetSessionList("nobetUstGrupKisitSession", kisitOnce.NobetUstGrupId);
            }
            catch (Exception e)
            {
                throw e;
            }

            var nobetUstGrupKisitlar = _nobetUstGrupKisitService.GetVarsayilandanFarkliOlanlar(kisitOrj.NobetUstGrupId);

            var varsayilandanFarkliMi = _nobetUstGrupKisitService.GetVarsayilandanFarkliMi(id);

            var sonuc = "Kural başarı ile güncellendi.";

            var kisit = _nobetUstGrupKisitService.GetDetayById(id);

            var guncellenenDurumlar = new GuncellenenNobetUstGrupKuralJsonModel
            {
                Mesaj = sonuc,
                VarsayilandanFarkliMi = varsayilandanFarkliMi,
                DegisenKisitSayisi = nobetUstGrupKisitlar.Count,
                GrupBazliKisitSayisi = kisit.NobetGrupGorevtipKisitSayisi,
                SagTarafDegeri = kisit.SagTarafDegeri,
                SagTarafDegeriVarsayilan = kisit.SagTarafDegeriVarsayilan,
                PasifMi = kisit.PasifMi,
                PasifMiVarsayilan = kisit.VarsayilanPasifMi
            };

            return ConvertToJson(guncellenenDurumlar);
        }

        //[ChildActionOnly]
        public PartialViewResult NobetUstGrupKisitDegisimPartialView()
        {
            var nobetUstGrupDetay = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var sonuc = _nobetUstGrupKisitSessionService.GetSessionList("nobetUstGrupKisitSession", nobetUstGrupDetay.Id);

            return PartialView(sonuc);
        }
        private JsonResult ConvertToJson(object sonuclar)
        {
            var jsonResult = Json(sonuclar, JsonRequestBehavior.AllowGet);

            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public ActionResult KisitAyarla2()
        {
            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user).Select(s => s.Id);
            var rolIdler = _userService.GetUserRoles(user).OrderBy(s => s.RoleId).Select(u => u.RoleId).ToArray();
            var rolId = rolIdler.FirstOrDefault();
            ViewBag.RolId = rolId;

            var kisitlar = _nobetUstGrupKisitService.GetDetaylar()
                .Where(w => nobetUstGruplar.Contains(w.NobetUstGrupId))
                .OrderBy(o => o.KisitAdi).ToList();

            //.ThenBy(o => o.NobetUstGrupId)
            //.ThenBy(r => r.KisitAdi);

            //var model = new KisitAyarlaViewModel
            //{
            //    Kisitlar = kisitlar
            //};

            return View(kisitlar);
        }

        public int GetDegisenKisitlar(int nobetUstGrupId)
        {
            return _nobetUstGrupKisitService.GetDegisenKisitlar(nobetUstGrupId);
        }

        [HttpPost]
        public ActionResult KisitlariGuncelle(List<NobetUstGrupKisitDetay> PasifMi)
        {
            return RedirectToAction("KisitAyarla2");
        }

        [Authorize(Roles = "Admin,Oda,Üst Grup")]
        [HandleError]
        public ActionResult VarsayilanKisitlar()
        {
            //var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGrupDetay = _nobetUstGrupSessionService.GetSession("nobetUstGrup");
            var nobetUstGrupId = nobetUstGrupDetay.Id;
            //_nobetUstGrupService.GetListByUser(user)
            //.Select(s => s.Id)
            //.FirstOrDefault();

            var nobetUstGrupKisitlar = _nobetUstGrupKisitService.GetVarsayilandanFarkliOlanlar(nobetUstGrupId);

            foreach (var nobetUstGrupKisit in nobetUstGrupKisitlar)
            {
                var kisit = _nobetUstGrupKisitService.GetById(nobetUstGrupKisit.Id);
                kisit.PasifMi = nobetUstGrupKisit.VarsayilanPasifMi;
                kisit.SagTarafDegeri = nobetUstGrupKisit.SagTarafDegeriVarsayilan;
                _nobetUstGrupKisitService.Update(kisit);
            }

            TempData["VarsayilanKistlarSonuc"] = true;

            //return View("Index", liste);
            return RedirectToAction("KisitAyarla");//, liste);
        }

        // GET: EczaneNobet/NobetUstGrupKisit/Details/5
        public ActionResult Details(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NobetUstGrupKisitDetay nobetUstGrupKisit = _nobetUstGrupKisitService.GetDetayById(id);
            if (nobetUstGrupKisit == null)
            {
                return HttpNotFound();
            }
            return View(nobetUstGrupKisit);
        }

        // GET: EczaneNobet/NobetUstGrupKisit/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var nobetUstGrupDetay = _nobetUstGrupSessionService.GetSession("nobetUstGrup");
            var nobetUstGruplar = _nobetUstGrupService.GetDetaylar(nobetUstGrupDetay.Id).Select(s => new { s.Id, s.Adi });

            ViewBag.KisitId = new SelectList(_kisitService.GetDetaylar(), "Id", "KisitAdi");
            ViewBag.NobetUstGrupId = new SelectList(nobetUstGruplar, "Id", "Adi");

            var nobetUstGrupKisit = new NobetUstGrupKisitCoklu
            {
                SagTarafDegeri = 0
            };

            return View(nobetUstGrupKisit);
        }

        // POST: EczaneNobet/NobetUstGrupKisit/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "Id,NobetUstGrupId,KisitId,PasifMi,VarsayilanPasifMi,SagTarafDegeri, SagTarafDegeriVarsayilan")] NobetUstGrupKisitCoklu nobetUstGrupKisitCoklu)
        {
            if (ModelState.IsValid)
            {
                var nobetUstGrupKisitlar = new List<NobetUstGrupKisit>();

                foreach (var kisitId in nobetUstGrupKisitCoklu.KisitId)
                {
                    nobetUstGrupKisitlar.Add(new NobetUstGrupKisit
                    {
                        KisitId = kisitId,
                        NobetUstGrupId = nobetUstGrupKisitCoklu.NobetUstGrupId,
                        SagTarafDegeri = nobetUstGrupKisitCoklu.SagTarafDegeri,
                        PasifMi = nobetUstGrupKisitCoklu.PasifMi,
                        Aciklama = nobetUstGrupKisitCoklu.Aciklama,
                        SagTarafDegeriVarsayilan = nobetUstGrupKisitCoklu.SagTarafDegeriVarsayilan,
                        VarsayilanPasifMi = nobetUstGrupKisitCoklu.VarsayilanPasifMi
                    });
                }

                _nobetUstGrupKisitService.CokluEkle(nobetUstGrupKisitlar);

                return RedirectToAction("Index");
            }

            //var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGrupDetay = _nobetUstGrupSessionService.GetSession("nobetUstGrup");
            var nobetUstGruplar = _nobetUstGrupService.GetDetaylar(nobetUstGrupDetay.Id).Select(s => new { s.Id, s.Adi });

            ViewBag.KisitId = new SelectList(_kisitService.GetDetaylar(), "Id", "KisitAdi", nobetUstGrupKisitCoklu.KisitId);
            ViewBag.NobetUstGrupId = new SelectList(nobetUstGruplar, "Id", "Adi", nobetUstGrupKisitCoklu.NobetUstGrupId);
            return View(nobetUstGrupKisitCoklu);
        }

        // GET: EczaneNobet/NobetUstGrupKisit/Edit/5
        [Authorize(Roles = "Admin,Oda,Üst Grup")]
        [HandleError]
        public ActionResult Edit(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NobetUstGrupKisitDetay nobetUstGrupKisit = _nobetUstGrupKisitService.GetDetayById(id);
            if (nobetUstGrupKisit == null)
            {
                return HttpNotFound();
            }
            var user = _userService.GetByUserName(User.Identity.Name);
            var rolIdler = _userService.GetUserRoles(user).OrderBy(s => s.RoleId).Select(u => u.RoleId).ToArray();
            var rolId = rolIdler.FirstOrDefault();
            var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user).Select(s => new { s.Id, s.Adi });
            ViewBag.RolId = rolId;
            ViewBag.KisitId = new SelectList(_kisitService.GetDetaylar(), "Id", "KisitAdi", nobetUstGrupKisit.KisitId);
            ViewBag.NobetUstGrupId = new SelectList(nobetUstGruplar, "Id", "Adi", nobetUstGrupKisit.NobetUstGrupId);
            return View(nobetUstGrupKisit);
        }

        // POST: EczaneNobet/NobetUstGrupKisit/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandleError]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "Id,NobetUstGrupId,KisitId,PasifMi,VarsayilanPasifMi,SagTarafDegeri, SagTarafDegeriVarsayilan")] NobetUstGrupKisit nobetUstGrupKisit)
        {
            var kisitEski = _nobetUstGrupKisitService.GetDetayById(nobetUstGrupKisit.Id);

            if (ModelState.IsValid)
            {
                //var user = _userService.GetByUserName(User.Identity.Name);
                //var rolIdler = _userService.GetUserRoles(user).OrderBy(s => s.RoleId).Select(u => u.RoleId).ToArray();
                //var rolId = rolIdler.FirstOrDefault();

                ViewBag.Sonuc1 = $"{kisitEski.KisitId} {kisitEski.KisitKategoriAdi}";
                ViewBag.Sonuc2 = kisitEski.KisitAdiGosterilen;

                var nobetUstGrupKisitSession = _nobetUstGrupKisitSessionService.GetSessionList("nobetUstGrupKisitSession", kisitEski.NobetUstGrupId);

                _nobetUstGrupKisitService.Update(nobetUstGrupKisit);

                var kisitYeni = _nobetUstGrupKisitService.GetDetayById(nobetUstGrupKisit.Id);

                _nobetUstGrupKisitSessionService.AddSessionList(kisitEski, kisitYeni, "nobetUstGrupKisitSession", nobetUstGrupKisitSession);

                nobetUstGrupKisitSession = _nobetUstGrupKisitSessionService.GetSessionList("nobetUstGrupKisitSession", kisitEski.NobetUstGrupId);

                return RedirectToAction("Index");
                //return RedirectToAction("KisitAyarla");
            }

            ViewBag.KisitId = new SelectList(_kisitService.GetDetaylar(), "Id", "KisitAdi", nobetUstGrupKisit.KisitId);
            ViewBag.NobetUstGrupId = new SelectList(_nobetUstGrupService.GetDetaylar(kisitEski.NobetUstGrupId).Select(s => new { s.Id, s.Adi }), "Id", "Adi", nobetUstGrupKisit.NobetUstGrupId);

            return View(nobetUstGrupKisit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandleError]
        [Authorize(Roles = "Admin,Oda,Üst Grup")]
        public ActionResult Edit2([Bind(Include = "Id,PasifMi,SagTarafDegeri")] NobetUstGrupKisit nobetUstGrupKisit)
        {
            if (ModelState.IsValid)
            {
                //var user = _userService.GetByUserName(User.Identity.Name);
                //var rolIdler = _userService.GetUserRoles(user).OrderBy(s => s.RoleId).Select(u => u.RoleId).ToArray();
                //var rolId = rolIdler.FirstOrDefault();
                var kisitEski = _nobetUstGrupKisitService.GetDetayById(nobetUstGrupKisit.Id);
                var kisitOrj = _nobetUstGrupKisitService.GetById(nobetUstGrupKisit.Id);

                //TempData["KisitDuzenleSonuc"] = $"Kısıt: {kisit.KisitId} ({kisit.KisitKategoriAdi} / {kisit.KisitAdiGosterilen})";

                TempData["KisitDuzenleSonuc0"] = kisitEski.KisitId < 10 ? $"0{kisitEski.KisitId}" : $"{kisitEski.KisitId}";
                TempData["KisitDuzenleSonuc1"] = kisitEski.KisitKategoriAdi;
                TempData["KisitDuzenleSonuc2"] = kisitEski.KisitAdiGosterilen;

                kisitOrj.PasifMi = !nobetUstGrupKisit.PasifMi;
                kisitOrj.SagTarafDegeri = nobetUstGrupKisit.SagTarafDegeri;

                var nobetUstGrupKisitSession = _nobetUstGrupKisitSessionService.GetSessionList("nobetUstGrupKisitSession", kisitEski.NobetUstGrupId);

                _nobetUstGrupKisitService.Update(kisitOrj);

                var kisitYeni = _nobetUstGrupKisitService.GetDetayById(nobetUstGrupKisit.Id);

                _nobetUstGrupKisitSessionService.AddSessionList(kisitEski, kisitYeni, "nobetUstGrupKisitSession", nobetUstGrupKisitSession);

                nobetUstGrupKisitSession = _nobetUstGrupKisitSessionService.GetSessionList("nobetUstGrupKisitSession", kisitEski.NobetUstGrupId);
            }
            return RedirectToAction("KisitAyarla");
            //return View(nobetUstGrupKisit);
        }

        // GET: EczaneNobet/NobetUstGrupKisit/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NobetUstGrupKisitDetay nobetUstGrupKisit = _nobetUstGrupKisitService.GetDetayById(id);
            if (nobetUstGrupKisit == null)
            {
                return HttpNotFound();
            }
            return View(nobetUstGrupKisit);
        }

        // POST: EczaneNobet/NobetUstGrupKisit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            NobetUstGrupKisitDetay nobetUstGrupKisit = _nobetUstGrupKisitService.GetDetayById(id);
            _nobetUstGrupKisitService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
