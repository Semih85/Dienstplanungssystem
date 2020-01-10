using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WM.Northwind.Business.Abstract.EczaneNobet;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;
using WM.Northwind.Business.Abstract.Authorization;
using WM.UI.Mvc.HtmlHelpers;
using WM.UI.Mvc.Areas.EczaneNobet.Models;
using System.Text.RegularExpressions;
using System.Data.Entity.Infrastructure;
using WM.UI.Mvc.Services;

namespace WM.UI.Mvc.Areas.EczaneNobet.Controllers
{
    [Authorize(Roles = "Admin,Oda,Üst Grup")]
    [HandleError]
    public class EczaneGrupController : Controller
    {
        #region ctor
        private IEczaneGrupService _eczaneGrupService;
        private IEczaneService _eczaneService;
        private IEczaneNobetGrupService _eczaneNobetGrupService;
        private IEczaneGrupTanimService _eczaneGrupTanimService;
        private IUserService _userService;
        private IGridMvcHelper gridMvcHelper;
        private INobetUstGrupService _nobetUstGrupService;
        private INobetGrupService _nobetGrupService;
        private INobetGorevTipService _nobetGorevTipService;
        private INobetGrupGorevTipService _nobetGrupGorevTipService;
        private INobetUstGrupSessionService _nobetUstGrupSessionService;

        public EczaneGrupController(IEczaneGrupService eczaneGrupService,
                                    IEczaneService eczaneService,
                                    IEczaneNobetGrupService eczaneNobetGrupService,
                                    IEczaneGrupTanimService eczaneGrupTanimService,
                                    INobetUstGrupService nobetUstGrupService,
                                    IUserService userService,
                                    INobetGrupService nobetGrupService,
                                    INobetGorevTipService nobetGorevTipService,
                                    INobetGrupGorevTipService nobetGrupGorevTipService,
                                    INobetUstGrupSessionService nobetUstGrupSessionService)
        {
            _eczaneGrupService = eczaneGrupService;
            _eczaneService = eczaneService;
            _nobetUstGrupService = nobetUstGrupService;
            _eczaneGrupTanimService = eczaneGrupTanimService;
            _eczaneNobetGrupService = eczaneNobetGrupService;
            _userService = userService;
            _nobetGrupService = nobetGrupService;
            _nobetGorevTipService = nobetGorevTipService;
            _nobetGrupGorevTipService = nobetGrupGorevTipService;
            _nobetUstGrupSessionService = nobetUstGrupSessionService;
            this.gridMvcHelper = new GridMvcHelper();
        }
        #endregion

        // GET: EczaneNobet/EczaneGrup
        public ActionResult Index(int id = 0)
        {
            //if (id == 0)
            //    return RedirectToAction("Index", "EczaneGrupTanim");
            //int Id = Convert.ToInt32(id);

            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            var nobetUstGrupId = nobetUstGruplar.Select(s => s.Id).FirstOrDefault();

            var nobetGruplar = _nobetGrupService.GetListByNobetUstGrupId(nobetUstGrupId);
            var eczaneGrupTanimlar = _eczaneGrupTanimService.GetDetaylar(nobetUstGrupId)
                .Select(s => new MyDrop { Id = s.Id, Value = s.EczaneGrupTanimAdi })
                .OrderBy(o => o.Value);

            var grupId = 0;

            if (TempData["arananNobetGrupId"] != null)
            {
                grupId = (int)TempData["arananNobetGrupId"];
            }

            ViewBag.NobetGrupId = new SelectList(items: nobetGruplar, dataValueField: "Id", dataTextField: "Adi", selectedValue: grupId);
            ViewBag.EczaneGrupTanimId = new SelectList(items: eczaneGrupTanimlar, dataValueField: "Id", dataTextField: "Value", selectedValue: id);
            ViewBag.grupTanimId = id;
            ViewBag.grupId = grupId;

            return View();
        }

        public ActionResult EczaneGrupPartialView(int id = 0)
        {
            //if (id == 0)
            //    return RedirectToAction("Index", "EczaneGrupTanim");
            //int Id = Convert.ToInt32(id);

            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            var nobetUstGrupId = nobetUstGruplar.Select(s => s.Id).FirstOrDefault();
            //var eczaneGruplar = _eczaneGrupService.GetDetaylar(nobetUstGrupId);
            var eczaneGrupDetaylar = _eczaneGrupService.GetDetaylar(nobetUstGrupId)
                .Where(w => w.EczaneGrupTanimId == id || id == 0)
                .OrderBy(o => o.EczaneAdi).ToList();
            ViewBag.eczaneGrupTanimId = id;

            return PartialView(eczaneGrupDetaylar);
        }

        public ActionResult GorselAnaliz(int id = 0)
        {
            var user = _userService.GetByUserName(User.Identity.Name);

            var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            var nobetUstGrupId = nobetUstGruplar.Select(s => s.Id).FirstOrDefault();

            ViewBag.NobetUstGrupId = new SelectList(items: nobetUstGruplar, dataValueField: "Id", dataTextField: "Adi", selectedValue: nobetUstGrupId);
            ViewBag.NobetUstGrupSayisi = nobetUstGruplar.Count;

            var eczaneGruplar = _eczaneGrupService.GetDetaylar(nobetUstGrupId);

            var esliEczaneSayilari = eczaneGruplar
                       .Select(s => new { s.NobetGrupId, s.NobetGrupAdi, s.EczaneAdi, s.EczaneId, s.NobetUstGrupId })
                       .Distinct()
                       .GroupBy(g => g.NobetGrupId)
                       .Select(s => new { NobetGrupId = s.Key, EsliEczaneSayisi = s.Count() }).ToList();

            var nobetGruplarGrafik = _eczaneNobetGrupService.NobetGruplarDDL(nobetUstGrupId);

            var grafik = esliEczaneSayilari
                .SelectMany(s => nobetGruplarGrafik
                                    .Where(w => w.NobetGrupId == s.NobetGrupId), (s, w) => new EczaneGrupIstatistik
                                    {
                                        NobetGrupId = s.NobetGrupId,
                                        NobetGrupAdi = w.NobetGrupAdi,
                                        GruptakiEczaneSayisi = w.EczaneSayisi,
                                        EsliEczaneSayisi = s.EsliEczaneSayisi,
                                        EsliEczaneYuzdesi = 100 * s.EsliEczaneSayisi / w.EczaneSayisi
                                    }).OrderBy(o => o.NobetGrupId).ToList();

            var yetkiliNobetUstGruplar = nobetUstGruplar.Select(r => r.Id).ToArray();

            //var eczaneGruplar = _eczaneGrupService.GetDetaylar()
            //    .Where(w => yetkiliEczaneler.Contains(w.EczaneId)).ToList();

            var eczaneGrupNodes = _eczaneGrupService.GetNodes(yetkiliNobetUstGruplar)
                .Where(s => eczaneGruplar
                                //.Where(x => x.EczaneGrupTanimTipId == 1)
                                .Select(w => w.EczaneId).Contains(s.Id)).ToList();

            var eczaneGrupEdges = _eczaneGrupService.GetEdges(yetkiliNobetUstGruplar);

            ViewBag.NobetGrupAdi = grafik.Select(s => s.NobetGrupAdi).ToArray();
            ViewBag.EsliEczaneSayisi = grafik.Select(s => s.EsliEczaneSayisi).ToArray();
            ViewBag.GruptakiEczaneSayisi = grafik.Select(s => s.GruptakiEczaneSayisi).ToArray();
            ViewBag.EsliEczaneYuzdesi = grafik.Select(s => s.EsliEczaneYuzdesi).ToArray();

            var model = new EczaneGrupGorselAnalizViewModel()
            {
                EczaneGrupNodes = eczaneGrupNodes,
                EczaneGrupEdges = eczaneGrupEdges
            };

            return View(model);
        }

        public ActionResult Search(int nobetGrupId = 0, int eczaneGrupTanimId = 0, string keywords = null)
        {
            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            var nobetUstGrupId = nobetUstGruplar.Select(s => s.Id).FirstOrDefault();

            var eczaneGrupDetaylar = _eczaneGrupService.GetDetaylar(nobetUstGrupId)
                .Where(w => (w.NobetGrupId == nobetGrupId || nobetGrupId == 0)
                         && (w.EczaneGrupTanimId == eczaneGrupTanimId || eczaneGrupTanimId == 0))
                .Where(p => Regex.Split(keywords, @"\s")
                    .Any(x => p.EczaneAdi.ToLower().Contains(x.ToLower())
                           || p.NobetGrupAdi.ToLower().Contains(x.ToLower())
                           || p.EczaneGrupTanimAdi.ToLower().Contains(x.ToLower())
                           || p.EczaneGrupTanimTipAdi.ToLower().Contains(x.ToLower())
                           )
                           )
                .OrderBy(o => o.EczaneAdi).ToList();

            TempData["arananEczaneGrupTanimId"] = eczaneGrupTanimId;
            TempData["arananNobetGrupId"] = nobetGrupId;

            return PartialView("EczaneGrupPartialView", eczaneGrupDetaylar);
        }

        public ActionResult PasifYap(int Id, int eczaneGrupTanimId)
        {
            var eczaneGrupDetay = _eczaneGrupService.GetDetayById(Id);
            var eczaneGrup = _eczaneGrupService.GetById(Id);

            if (eczaneGrup.PasifMi)
                eczaneGrup.PasifMi = false;
            else
                eczaneGrup.PasifMi = true;

            _eczaneGrupService.Update(eczaneGrup);

            if (TempData["arananEczaneGrupTanimId"] != null)
            {
                eczaneGrupTanimId = (int)TempData["arananEczaneGrupTanimId"];
            }

            ViewBag.eczaneGrupTanimId = eczaneGrupTanimId;

            return RedirectToAction("Index", new { id = eczaneGrupTanimId });//result:model
        }
        //[ChildActionOnly]
        public ActionResult EczaneGrupPartial()
        {
            var user = _userService.GetByUserName(User.Identity.Name);
            var eczaneler = _eczaneService.GetListByUser(user).Select(s => s.Id);

            var items = this._eczaneGrupService.GetDetaylar()
                .Where(s => eczaneler.Contains(s.EczaneId))
                .AsQueryable().OrderBy(f => f.Id);

            var grid = this.gridMvcHelper.GetAjaxGrid(items);
            return PartialView(grid);
        }

        //public ActionResult EczaneGrupPartial(int eczaneGrupTanimId)
        //{
        //    var user = _userService.GetByUserName(User.Identity.Name);
        //    var eczaneler = _eczaneService.GetListByUser(user).Select(s => s.Id);

        //    var items = this._eczaneGrupService.GetDetaylar()
        //        .Where(s => eczaneler.Contains(s.EczaneId)
        //                 && s.EczaneGrupTanimId == eczaneGrupTanimId)
        //        .AsQueryable().OrderBy(f => f.Id);

        //    var grid = this.gridMvcHelper.GetAjaxGrid(items);
        //    return PartialView(grid);
        //}

        [HttpGet]
        public ActionResult EczaneGrupPartialPager(int? page)
        {
            var user = _userService.GetByUserName(User.Identity.Name);
            var eczaneler = _eczaneService.GetListByUser(user).Select(s => s.Id);

            var items = this._eczaneGrupService.GetDetaylar()
                .Where(s => eczaneler.Contains(s.EczaneId))
                .AsQueryable().OrderBy(f => f.Id);

            var grid = this.gridMvcHelper.GetAjaxGrid(items, page);
            var strPath = "~/Areas/EczaneNobet/Views/EczaneGrup/EczaneGrupPartial.cshtml";
            object jsonData = this.gridMvcHelper.GetGridJsonData(grid, strPath, this);
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        // GET: EczaneNobet/EczaneGrup/Details/5
        public ActionResult Details(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EczaneGrupDetay GetEczaneGrupDetaylar = _eczaneGrupService.GetDetayById(id).SingleOrDefault();
            if (GetEczaneGrupDetaylar == null)
            {
                return HttpNotFound();
            }
            return View(GetEczaneGrupDetaylar);
        }

        // GET: EczaneNobet/EczaneGrup/Create
        public ActionResult Create()
        {
            //var user = _userService.GetByUserName(User.Identity.Name);
            //var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            var ustGrupSession = _nobetUstGrupSessionService.GetSession("nobetUstGrup");
            var nobetUstGrupId = ustGrupSession.Id;

            //var eczaneNobetGruplar = _eczaneNobetGrupService.GetDetaylar(ustGrupSession.Id)
            //    .Where(w => w.EczaneKapanmaTarihi == null).ToList();

            var eczaneler = _eczaneService.GetDetaylar(nobetUstGrupId)
                .Where(w => w.KapanisTarihi == null)
                .OrderBy(f => f.EczaneAdi)
                .ToList();

            var eczaneGrupTanimlar = _eczaneGrupTanimService.GetDetaylar(nobetUstGrupId);
            //var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrupId);

            //{ s.NobetGrupAdi}
            ViewBag.EczaneId = new SelectList(eczaneler, "Id", "EczaneAdi");
            ViewBag.EczaneGrupTanimId = new SelectList(eczaneGrupTanimlar.Select(s => new { s.Id, EczaneGrupTanimAdi = $"{s.Adi} ({s.NobetGorevTipAdi})" }), "Id", "EczaneGrupTanimAdi");
            //ViewBag.NobetGorevTipId = new SelectList(_nobetGorevTipService.GetList(nobetGrupGorevTipler.Select(s => s.NobetGorevTipId).ToList()), "Id", "Adi");

            return View();
        }

        public ActionResult Create2(int eczaneGrupTanimId)
        {
            //var user = _userService.GetByUserName(User.Identity.Name);
            //var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            //var nobetUstGrupId = nobetUstGruplar.Select(s => s.Id).ToList();

            var ustGrupSession = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var gruptakiEczaneler = _eczaneGrupService.GetDetaylarByEczaneGrupTanimId(eczaneGrupTanimId);

            //var eczaneNobetGruplar = _eczaneNobetGrupService.GetDetaylar(ustGrupSession.Id)
            //                .Where(w => w.EczaneKapanmaTarihi == null
            //                        && !gruptakiEczaneler.Select(s => s.EczaneId).Contains(w.Id)).ToList();

            var eczaneler = _eczaneService.GetDetaylar(ustGrupSession.Id)
                .Where(w => w.KapanisTarihi == null
                        && !gruptakiEczaneler.Select(s => s.EczaneId).Contains(w.Id))
                .OrderBy(f => f.EczaneAdi)
                .ToList();

            var eczaneGrupTanimlar = _eczaneGrupTanimService.GetDetaylarAktifTanimList(eczaneGrupTanimId);

            ViewBag.EczaneId = new SelectList(eczaneler, "Id", "EczaneAdi");
            ViewBag.EczaneGrupTanimId = new SelectList(eczaneGrupTanimlar.Select(s => new { s.Id, EczaneGrupTanimAdi = $"{s.Adi} ({s.NobetGorevTipAdi})" }), "Id", "EczaneGrupTanimAdi", eczaneGrupTanimId);

            return View("Create");
        }

        public ActionResult CreateAjax()
        {
            //var user = _userService.GetByUserName(User.Identity.Name);
            var ustGrupSession = _nobetUstGrupSessionService.GetSession("nobetUstGrup");
            var eczaneler = _eczaneService.GetDetaylar(ustGrupSession.Id).Select(s => s.Id).ToList();

            var items = this._eczaneNobetGrupService.GetDetaylarByEczaneIdList(eczaneler)
                .Where(w => w.BitisTarihi == null)
                .OrderBy(f => f.EczaneAdi);

            var eczaneGrupTanimlar = _eczaneGrupTanimService.GetDetaylar(ustGrupSession.Id);

            ViewBag.EczaneId = new SelectList(items.Select(s => new { s.EczaneId, EczaneNobetGrupAdi = $"{s.EczaneAdi} {s.NobetGrupAdi}" }), "EczaneId", "EczaneNobetGrupAdi");
            ViewBag.EczaneGrupTanimId = new SelectList(eczaneGrupTanimlar, "Id", "Adi");
            return View();
        }

        // POST: EczaneNobet/EczaneGrup/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EczaneGrupTanimId,EczaneId,PasifMi")] EczaneGrupCoklu eczaneGrupCoklu)
        {
            if (ModelState.IsValid)
            {
                var eczaneGruplar = new List<EczaneGrup>();

                foreach (var eczaneId in eczaneGrupCoklu.EczaneId)
                {
                    eczaneGruplar.Add(new EczaneGrup
                    {
                        EczaneId = eczaneId,
                        EczaneGrupTanimId = eczaneGrupCoklu.EczaneGrupTanimId
                    });
                }

                var eklenecekEczaneSayisi = eczaneGruplar.Count;

                if (ModelState.IsValid && eklenecekEczaneSayisi > 0)
                {                    
                    try
                    {
                        _eczaneGrupService.CokluEkle(eczaneGruplar);
                    }
                    catch (DbUpdateException ex)
                    {
                        var hata = ex.InnerException.ToString();

                        string[] dublicateHata = { "Cannot insert dublicate row in object", "with unique index" };

                        var dublicateRowHatasiMi = dublicateHata.Any(h => hata.Contains(h));

                        if (dublicateRowHatasiMi)
                        {
                            throw new Exception("<strong>Mükerrer kayıt eklenemez...</strong>");
                            //return PartialView("ErrorDublicateRowPartial");
                        }

                        throw ex;
                    }
                    catch (Exception ex)
                    {
                        //return PartialView("ErrorPartial");
                        throw ex;
                    }

                    ViewBag.EklenenEczaneSayisi = eklenecekEczaneSayisi;
                    ViewBag.EklenenGrupAdi = _eczaneGrupTanimService.GetById(eczaneGrupCoklu.EczaneGrupTanimId).Adi;

                    return RedirectToAction("Index","EczaneGrupTanim");
                }
                return RedirectToAction("Index");

                //_eczaneGrupService.Insert(eczaneGrup);
                ////ViewBag.EklenenEczane = _eczaneService.GetById(eczaneGrup.EczaneId).Adi;
                //return RedirectToAction("Index");
            }
            
            var ustGrupSession = _nobetUstGrupSessionService.GetSession("nobetUstGrup");
            var nobetUstGrupId = ustGrupSession.Id;
            //var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrupId);

            ViewBag.EczaneId = new SelectList(_eczaneService.GetDetaylar(nobetUstGrupId), "Id", "EczaneAdi");
            ViewBag.EczaneGrupTanimId = new SelectList(_eczaneGrupTanimService.GetDetaylar(nobetUstGrupId).Select(s => new { s.Id, EczaneGrupTanimAdi = $"{s.Adi} ({s.NobetGorevTipAdi})" }), "Id", "EczaneGrupTanimAdi", eczaneGrupCoklu.EczaneGrupTanimId);
            //ViewBag.NobetGorevTipId = new SelectList(_nobetGorevTipService.GetList(nobetGrupGorevTipler.Select(s => s.NobetGorevTipId).ToList()), "Id", "Adi");
            return View(eczaneGrupCoklu);
        }

        // GET: EczaneNobet/EczaneGrup/Edit/5
        public ActionResult Edit(int id)
        {
            if (id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EczaneGrup eczaneGrup = _eczaneGrupService.GetById(id);
            if (eczaneGrup == null)
            {
                return HttpNotFound();
            }
            //var user = _userService.GetByUserName(User.Identity.Name);
            //var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);

            var ustGrupSession = _nobetUstGrupSessionService.GetSession("nobetUstGrup");
            var nobetUstGrupId = ustGrupSession.Id;
            //var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrupId);

            var eczaneler = _eczaneService.GetDetaylar(nobetUstGrupId)
                .Where(w => w.KapanisTarihi == null)
                .OrderBy(f => f.EczaneAdi)
                .ToList();

            var eczaneGrupTanimlar = _eczaneGrupTanimService.GetDetaylar(nobetUstGrupId);

            //{ s.NobetGrupAdi}
            ViewBag.EczaneId = new SelectList(eczaneler.Select(s => new { s.Id, EczaneNobetGrupAdi = $"{s.EczaneAdi}" }), "Id", "EczaneNobetGrupAdi", eczaneGrup.EczaneId);
            ViewBag.EczaneGrupTanimId = new SelectList(eczaneGrupTanimlar.Select(s => new { s.Id, EczaneGrupTanimAdi = $"{s.Adi} ({s.NobetGorevTipAdi})" }), "Id", "EczaneGrupTanimAdi", eczaneGrup.EczaneGrupTanimId);
            //ViewBag.NobetGorevTipId = new SelectList(_nobetGorevTipService.GetList(nobetGrupGorevTipler.Select(s => s.NobetGorevTipId).ToList()), "Id", "Adi");
            return View(eczaneGrup);
        }

        // POST: EczaneNobet/EczaneGrup/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EczaneGrupTanimId,EczaneId,PasifMi")] EczaneGrup eczaneGrup)
        {
            if (ModelState.IsValid)
            {
                _eczaneGrupService.Update(eczaneGrup);
                return RedirectToAction("Index", "EczaneGrupTanim");
                //return RedirectToAction("Index");
            }
            var ustGrupSession = _nobetUstGrupSessionService.GetSession("nobetUstGrup");
            var nobetUstGrupId = ustGrupSession.Id;
            //var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrupId);

            ViewBag.EczaneId = new SelectList(_eczaneService.GetList(nobetUstGrupId), "Id", "EczaneAdi", eczaneGrup.EczaneId);
            ViewBag.EczaneGrupTanimId = new SelectList(_eczaneGrupTanimService.GetDetaylar(nobetUstGrupId).Select(s => new { s.Id, EczaneGrupTanimAdi = $"{s.Adi} ({s.NobetGorevTipAdi})" }), "Id", "EczaneGrupTanimAdi", eczaneGrup.EczaneGrupTanimId);
            //ViewBag.NobetGorevTipId = new SelectList(_nobetGorevTipService.GetList(nobetGrupGorevTipler.Select(s => s.NobetGorevTipId).ToList()), "Id", "Adi");
            return View(eczaneGrup);
        }

        // GET: EczaneNobet/EczaneGrup/Delete/5
        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EczaneGrupDetay GetEczaneGrupDetaylar = _eczaneGrupService.GetDetayById(id).SingleOrDefault();
            if (GetEczaneGrupDetaylar == null)
            {
                return HttpNotFound();
            }
            return View(GetEczaneGrupDetaylar);
        }

        [HttpPost]
        public ActionResult SecilenleriSil(List<int> eczaneGrupIdList)
        {
            foreach (var eczaneGrupId in eczaneGrupIdList)
            {
                _eczaneGrupService.Delete(eczaneGrupId);
            }
            return RedirectToAction("Index");
        }

        // POST: EczaneNobet/EczaneGrup/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EczaneGrup eczaneGrup = _eczaneGrupService.GetById(id);
            _eczaneGrupService.Delete(id);
            return RedirectToAction("Index");
        }

        public void AjaxSil(int id, int eczaneGrupTanimId)
        {
            EczaneGrup eczaneGrup = _eczaneGrupService.GetById(id);
            _eczaneGrupService.Delete(id);

            //var sonuclar = _eczaneGrupService.GetDetaylarByEczaneGrupTanimId(new List<int> { eczaneGrupTanimId });

            //return PartialView("EczaneGrupDetayPartial", sonuclar);
        }
    }
}
