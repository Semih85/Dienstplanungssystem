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
using WM.Northwind.Business.Abstract.Optimization.EczaneNobet;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;
using WM.Northwind.Entities.Concrete.Optimization.EczaneNobet;
using WM.UI.Mvc.Areas.EczaneNobet.Models;
using WM.UI.Mvc.Models;
using WM.UI.Mvc.Services;

namespace WM.UI.Mvc.Areas.EczaneNobet.Controllers
{
    [Authorize]
    [HandleError]
    public class EczaneNobetSonucAktifController : Controller
    {
        #region ctor
        private IEczaneNobetOrtakService _eczaneNobetOrtakService;
        private IEczaneNobetSonucService _eczaneNobetSonucService;
        private IEczaneNobetSonucAktifService _eczaneNobetSonucAktifService;
        private IEczaneNobetOptimizationService _eczaneNobetOptimizationService;
        private IEczaneNobetGrupService _eczaneNobetGrupService;
        private INobetGrupService _nobetGrupService;
        private IEczaneService _eczaneService;
        private ITakvimService _takvimService;
        private IUserService _userService;
        private INobetUstGrupService _nobetUstGrupService;
        private INobetUstGrupSessionService _nobetUstGrupSessionService;

        public EczaneNobetSonucAktifController(IEczaneNobetSonucAktifService eczaneNobetSonucAktifService,
                                               IEczaneNobetGrupService eczaneNobetGrupService,
                                               ITakvimService takvimService,
                                               IEczaneNobetOptimizationService eczaneNobetOptimizationService,
                                               IUserService userService,
                                               INobetUstGrupService nobetUstGrupService,
            IEczaneService eczaneService,
            INobetGrupService nobetGrupService,
            IEczaneNobetOrtakService eczaneNobetOrtakService,
            IEczaneNobetSonucService eczaneNobetSonucService,
            INobetUstGrupSessionService nobetUstGrupSessionService)
        {
            _eczaneNobetSonucAktifService = eczaneNobetSonucAktifService;
            _eczaneNobetOptimizationService = eczaneNobetOptimizationService;
            _eczaneNobetGrupService = eczaneNobetGrupService;
            _takvimService = takvimService;
            _userService = userService;
            _nobetUstGrupService = nobetUstGrupService;
            _eczaneService = eczaneService;
            _nobetGrupService = nobetGrupService;
            _eczaneNobetOrtakService = eczaneNobetOrtakService;
            _eczaneNobetSonucService = eczaneNobetSonucService;
            _nobetUstGrupSessionService = nobetUstGrupSessionService;
        }
        #endregion

        // GET: EczaneNobet/EczaneNobetSonucAktif
        public ActionResult Index(int nobetGrupId = 0, int yil = 2018, int ay = 0)
        {
            //var user = _userService.GetByUserName(User.Identity.Name);
            //var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            //var nobetUstGrup = nobetUstGruplar.FirstOrDefault();
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var aktifSonuclar = _eczaneNobetSonucAktifService.GetDetaylar2(nobetUstGrup.Id)
                .Where(w => (w.NobetGrupId == nobetGrupId || nobetGrupId == 0)
                         && (w.Tarih.Month == ay || ay == 0))
                .OrderBy(x => x.TakvimId).ToList();

            TempData["NobetGrupId"] = nobetGrupId;
            TempData["Yil"] = yil;
            TempData["Ay"] = ay;

            return View(aktifSonuclar);
        }

        public ActionResult Kesinlestir(string nobetGrupId, DateTime baslangicTarihi, DateTime bitisTarihi)
        {
            var arr = ParseStringToIntArray(nobetGrupId);

            _eczaneNobetOptimizationService.Kesinlestir(arr.ToArray(), baslangicTarihi, bitisTarihi);

            TempData["KesinlesenNobetGrupId"] = nobetGrupId;
            TempData["KesinlesenNobetGrupSayisi"] = arr.Count();
            TempData["KesinlesenBaslangicTarihi"] = baslangicTarihi;
            TempData["KesinlesenBitisTarihi"] = bitisTarihi;

            TempData["NobetGrupId"] = nobetGrupId;

            return RedirectToAction("Index", "NobetYaz", new { area = "EczaneNobet" });
            //return RedirectToAction("PivotSonuclar", "EczaneNobetSonuc", new { area = "EczaneNobet" });
            //return RedirectToAction("Index");
        }

        private List<int> ParseStringToIntArray(string parameter)
        {
            var array = new List<int>();
            if (parameter != null)
            {
                var parca = parameter.Split(',');

                foreach (var item in parca)
                {
                    array.Add(Convert.ToInt32(item));
                }
            }

            return array;
        }

        // GET: EczaneNobet/EczaneNobetSonuc  int[] nobetGrupIdList = null, int yil = 2018, int ay = 1
        public ActionResult PivotSonuclar(PivotSonuclarParams pivotSonuclarParams)
        {
            var parametreler = pivotSonuclarParams;

            if (parametreler == null)
            {
                parametreler = new PivotSonuclarParams();
            }

            //pivotSonuclarParams.NobetGrupIdList.ToArray();

            var arr = ParseStringToIntArray(pivotSonuclarParams.NobetGrupIdList);

            //var user = _userService.GetByUserName(User.Identity.Name);
            //var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            //var nobetUstGrup = nobetUstGruplar.FirstOrDefault();
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");
            var nobetUstGrupId = nobetUstGrup.Id;

            var nobetGruplar = _nobetGrupService.GetDetaylar(nobetUstGrupId)
                .Select(s => new MyDrop { Id = s.Id, Value = s.Adi });

            var baslamaTarihi = pivotSonuclarParams.BaslangicTarihi;
            var bitisTarihi = pivotSonuclarParams.BitisTarihi;

            var sonuclarEski = _eczaneNobetSonucService.GetSonuclar(nobetUstGrupId)
                .Where(w => w.Tarih >= nobetUstGrup.BaslangicTarihi);

            var sonuclarAktif = _eczaneNobetSonucAktifService.GetSonuclar2(nobetUstGrupId)
                .Where(w => w.Tarih >= baslamaTarihi && w.Tarih <= bitisTarihi).ToList();

            var tumSonuclar = sonuclarEski.Union(sonuclarAktif).ToList();

            ViewBag.nobetGrup = arr.FirstOrDefault();

            var beklenenGunFarklari = new List<NobetGrupBeklenenGunFarki>();

            //ViewBag.NobetGruptakiEczaneSayisi

            var nobetGrupEczaneSayilari = arr.Count == 0
                ? _eczaneNobetGrupService.NobetGruplarDDL(nobetUstGrupId)
                    //.Where(w => arr.Contains(w.NobetGrupId))
                    .Select(s => new { s.NobetGrupAdi, s.EczaneSayisi })
                : _eczaneNobetGrupService.NobetGruplarDDL(nobetUstGrupId)
                    .Where(w => arr.Contains(w.NobetGrupId))
                    .Select(s => new { s.NobetGrupAdi, s.EczaneSayisi });

            foreach (var item in nobetGrupEczaneSayilari)
            {
                beklenenGunFarklari.Add(new NobetGrupBeklenenGunFarki
                {
                    NobetGrupAdi = item.NobetGrupAdi,
                    Haftaİci = $"{Math.Round(item.EczaneSayisi * 1.2 * 0.766, 0)}-{(Math.Ceiling(item.EczaneSayisi * 1.34))}",
                    Pazar = $"{(int)Math.Ceiling(((double)item.EczaneSayisi / 5) - 1) * 30}-{((int)Math.Ceiling((double)item.EczaneSayisi / 4) + 1) * 30}",
                    EczaneSayisi = item.EczaneSayisi
                });
            };

            TempData["NobetGrupId"] = pivotSonuclarParams.NobetGrupIdList;

            //ViewBag.YilBaslangiclar = new SelectList(yillar, null, null, yilBaslangic);
            //ViewBag.YilBitisler = new SelectList(yillar, null, null, pivotSonuclarParams.Yil);
            //if (pivotSonuclarParams.NobetGrupIdList != null)
            //{
            //    ViewBag.NobetGruplar = new SelectList(nobetGruplar, "Id", "Value", pivotSonuclarParams.NobetGrupIdList.FirstOrDefault());
            //}
            //else
            //{
            //    ViewBag.NobetGruplar = new SelectList(nobetGruplar, "Id", "Value");
            //}        

            ViewBag.ToplamUzunluk = tumSonuclar.Count();

            var gunFarklari = _eczaneNobetOrtakService.EczaneNobetIstatistikGunFarkiHesapla(tumSonuclar)
                .Where(w => (w.Nobet2Tarih >= baslamaTarihi && w.Nobet2Tarih <= bitisTarihi)).ToList();

            var gunFarkiFrekanslar = _eczaneNobetOrtakService.EczaneNobetIstatistikGunFarkiFrekans(gunFarklari);

            ViewBag.ToplamUzunluk2 = gunFarklari.Count;

            //TempData["EczaneNobetSonuclar"] = sonucModel;

            var model = new EczaneNobetSonucAktifPivotSonuclarViewModel
            {
                TumSonuclar = tumSonuclar.Where(w => arr.Contains(w.NobetGrupId)).ToList(),
                GunFarklari = gunFarklari,
                GunFarklariFrekanslar = gunFarkiFrekanslar,
                NobetGrupBeklenenGunFarklari = beklenenGunFarklari,
                EczaneNobetSonuclar = (EczaneNobetSonucModel)TempData["EczaneNobetSonuclar"] ?? new EczaneNobetSonucModel(),
                BaslamaTarihi = baslamaTarihi,
                BitisTarihi = bitisTarihi
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PivotSonuclar(int nobetGrup = 0, int yilBaslangic = 2018, int yilBitis = 2020, int ay = 0)
        {
            //var user = _userService.GetByUserName(User.Identity.Name);
            //var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            //var nobetUstGrup = nobetUstGruplar.FirstOrDefault();
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");
            var nobetUstGrupId = nobetUstGrup.Id;

            var nobetGruplar = _nobetGrupService.GetDetaylar(nobetUstGrupId)
                .Select(s => new MyDrop { Id = s.Id, Value = s.Adi });

            var sonuclarEski = _eczaneNobetSonucService.GetSonuclar(nobetUstGrupId)
                .Where(w => w.Tarih >= nobetUstGrup.BaslangicTarihi);

            var sonuclarAktif = _eczaneNobetSonucAktifService.GetSonuclar2(nobetUstGrupId);
            var sonuclar = sonuclarEski.Union(sonuclarAktif)
                .Where(w => w.NobetGrupId == nobetGrup || nobetGrup == 0).ToList();

            var tumSonuclar = sonuclar
                .Where(w => (w.Yil >= yilBaslangic && w.Yil <= yilBitis)).ToList();

            var yillar = sonuclar
                .Select(s => s.Yil).Distinct().OrderBy(o => o).ToList();

            ViewBag.yilBaslangic = yilBaslangic;
            ViewBag.yilBitis = yilBitis;
            ViewBag.nobetGrup = nobetGrup;


            //ViewBag.NobetGruptakiEczaneSayisi = _eczaneNobetGrupService.NobetGruplarDDL(nobetUstGrupId).Where(w => w.NobetGrupId == nobetGrup).Select(s => s.EczaneSayisi).SingleOrDefault();
            var beklenenGunFarklari = new List<NobetGrupBeklenenGunFarki>();

            //ViewBag.NobetGruptakiEczaneSayisi
            var nobetGrupEczaneSayilari = _eczaneNobetGrupService.NobetGruplarDDL(nobetUstGrupId)
               .Where(w => w.NobetGrupId == nobetGrup).Select(s => new { s.NobetGrupAdi, s.EczaneSayisi });

            foreach (var item in nobetGrupEczaneSayilari)
            {
                beklenenGunFarklari.Add(new NobetGrupBeklenenGunFarki
                {
                    NobetGrupAdi = item.NobetGrupAdi,
                    Haftaİci = $"{Math.Round(item.EczaneSayisi * 1.2 * 0.766, 0)}-{(Math.Ceiling(item.EczaneSayisi * 1.34))}",
                    Pazar = $"{(int)Math.Ceiling(((double)item.EczaneSayisi / 5) - 1) * 30}-{((int)Math.Ceiling((double)item.EczaneSayisi / 4) + 1) * 30}",
                    EczaneSayisi = item.EczaneSayisi
                });
            };

            TempData["NobetGrupId"] = nobetGrup;
            TempData["Yil"] = yilBitis;
            TempData["Ay"] = ay;

            ViewBag.YilBaslangiclar = new SelectList(yillar, null, null, yilBaslangic);
            ViewBag.YilBitisler = new SelectList(yillar, null, null, yilBitis);
            ViewBag.NobetGruplar = new SelectList(nobetGruplar, "Id", "Value", nobetGrup);

            ViewBag.ToplamUzunluk = tumSonuclar.Count;

            //var pivotSekiller2 = _eczaneNobetOrtakService.GetPivotSekillerGunFarki();
            var gunFarklari = _eczaneNobetOrtakService.EczaneNobetIstatistikGunFarkiHesapla(sonuclar)
                .Where(w => w.Nobet2Yil == yilBitis && w.Nobet2Ay == ay).ToList();

            var gunFarkiFrekanslar = _eczaneNobetOrtakService.EczaneNobetIstatistikGunFarkiFrekans(gunFarklari);
            ViewBag.ToplamUzunluk2 = gunFarklari.Count;

            var model = new EczaneNobetSonucAktifPivotSonuclarViewModel
            {
                TumSonuclar = tumSonuclar,
                GunFarklari = gunFarklari,
                GunFarklariFrekanslar = gunFarkiFrekanslar,
                NobetGrupBeklenenGunFarklari = beklenenGunFarklari
            };

            return View(model);
        }

        // GET: EczaneNobet/EczaneNobetSonucAktif/Details/5
        public ActionResult Details(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EczaneNobetSonucAktif eczaneNobetSonucAktif = _eczaneNobetSonucAktifService.GetById(id);
            if (eczaneNobetSonucAktif == null)
            {
                return HttpNotFound();
            }
            return View(eczaneNobetSonucAktif);
        }
        // GET: EczaneNobet/EczaneNobetSonucAktif/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EczaneNobetSonucAktif eczaneNobetSonucAktif = _eczaneNobetSonucAktifService.GetById(id);
            if (eczaneNobetSonucAktif == null)
            {
                return HttpNotFound();
            }
            ViewBag.EczaneNobetGrupId = new SelectList(_eczaneNobetGrupService.GetList(), "Id", "EczaneId", eczaneNobetSonucAktif.EczaneNobetGrupId);
            ViewBag.TakvimId = new SelectList(_takvimService.GetList(), "Id", "Tarih", eczaneNobetSonucAktif.TakvimId);
            return View(eczaneNobetSonucAktif);
        }

        // POST: EczaneNobet/EczaneNobetSonucAktif/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EczaneNobetGrupId,TakvimId")] EczaneNobetSonucAktif eczaneNobetSonucAktif)
        {
            if (ModelState.IsValid)
            {
                _eczaneNobetSonucAktifService.Update(eczaneNobetSonucAktif);
                return RedirectToAction("Index");
            }
            ViewBag.EczaneNobetGrupId = new SelectList(_eczaneNobetGrupService.GetList(), "Id", "EczaneId", eczaneNobetSonucAktif.EczaneNobetGrupId);
            ViewBag.TakvimId = new SelectList(_takvimService.GetList(), "Id", "Tarih", eczaneNobetSonucAktif.TakvimId);
            return View(eczaneNobetSonucAktif);
        }


    }
}
