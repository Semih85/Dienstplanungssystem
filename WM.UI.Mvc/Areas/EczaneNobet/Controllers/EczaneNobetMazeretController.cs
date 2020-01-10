using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WM.Northwind.Business.Abstract.Authorization;
using WM.Northwind.Business.Abstract.EczaneNobet;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.Authorization;
using WM.Northwind.Entities.Concrete.EczaneNobet;
using WM.UI.Mvc.Areas.EczaneNobet.Filters;
using WM.UI.Mvc.Areas.EczaneNobet.Models;
using WM.UI.Mvc.Models;
using WM.UI.Mvc.Services;

namespace WM.UI.Mvc.Areas.EczaneNobet.Controllers
{
    [Authorize]
    [HandleError]
    public class EczaneNobetMazeretController : Controller
    {
        #region ctor
        private IEczaneNobetMazeretService _eczaneNobetMazeretService;
        private IEczaneNobetIstekService _eczaneNobetIstekService;
        private IEczaneNobetGrupService _eczaneNobetGrupService;
        private IEczaneService _eczaneService;
        private IMazeretService _mazeretService;
        private ITakvimService _takvimService;
        private IUserService _userService;
        private INobetGrupService _nobetGrupService;
        private INobetGrupGorevTipService _nobetGrupGorevTipService;
        private IBayramService _bayramService;
        private INobetUstGrupService _nobetUstGrupService;
        private INobetGrupGorevTipTakvimOzelGunService _nobetGrupGorevTipTakvimOzelGunService;
        private INobetUstGrupSessionService _nobetUstGrupSessionService;

        public EczaneNobetMazeretController(IEczaneNobetMazeretService eczaneNobetMazeretService,
                                            IEczaneNobetIstekService eczaneNobetIstekService,
                                            IEczaneNobetGrupService eczaneNobetGrupService,
                                            IEczaneService eczaneService,
                                            IMazeretService mazeretService,
                                            ITakvimService takvimService,
                                            IUserService userService,
                                            INobetGrupService nobetGrupService,
                                            INobetGrupGorevTipService nobetGrupGorevTipService,
                                            IBayramService bayramService,
                                            INobetUstGrupService nobetUstGrupService,
                                            INobetGrupGorevTipTakvimOzelGunService nobetGrupGorevTipTakvimOzelGunService,
                                            INobetUstGrupSessionService nobetUstGrupSessionService)
        {
            _eczaneNobetMazeretService = eczaneNobetMazeretService;
            _eczaneNobetIstekService = eczaneNobetIstekService;
            _eczaneNobetGrupService = eczaneNobetGrupService;
            _eczaneService = eczaneService;
            _mazeretService = mazeretService;
            _takvimService = takvimService;
            _userService = userService;
            _nobetGrupService = nobetGrupService;
            _bayramService = bayramService;
            _nobetUstGrupService = nobetUstGrupService;
            _nobetGrupGorevTipTakvimOzelGunService = nobetGrupGorevTipTakvimOzelGunService;
            _nobetUstGrupSessionService = nobetUstGrupSessionService;
            _nobetGrupGorevTipService = nobetGrupGorevTipService;
        }
        #endregion

        // GET: EczaneNobet/EczaneNobetMazeret
        [Authorize(Roles = "Admin,Oda,Üst Grup")]
        public ActionResult Index()
        {
            //var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");
            var nobetGruplar = _nobetGrupGorevTipService.GetMyDrop(nobetUstGrup.Id);

            ViewBag.NobetGrupGorevTipId = new SelectList(items: nobetGruplar, dataValueField: "Id", dataTextField: "Value");

            var eczaneNobetMezaretIstekTipler = new List<MyDrop>
            {
                new MyDrop{Id = 1,Value = "Mazeret"},
                new MyDrop{Id = 2,Value = "İstek"}
            };

            ViewBag.EczaneNobetMazeretIstekTipId = new SelectList(eczaneNobetMezaretIstekTipler, "Id", "Value", 0);
            // ViewBag.EczaneNobetMazeretIstekTipId = new SelectList(items: eczaneNobetMezaretIstekTipler, dataValueField: "Id", dataTextField: "Adi", selectedValue: 0);
            //ViewData["Sonuclar"] = 1;

            ViewBag.NobetUstGrupId = nobetUstGrup.Id;
            return View();
        }
        public ActionResult EczaneNobetMazeretPartialView(int nobetGrupId = 0, int eczaneId = 0, DateTime? MazeretTarihi = null)
        {
            //var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var nobetGruplar = _nobetGrupService.GetDetaylar(nobetUstGrup.Id)
                .Where(w => w.Id == nobetGrupId || nobetGrupId == 0).ToList();

            var eczaneNobetMazeretler = _eczaneNobetMazeretService.GetDetaylar(nobetUstGrup.Id)
                .Where(w => nobetGruplar.Select(n => n.Id).Contains(w.NobetGrupId)
                   && (w.EczaneId == eczaneId || eczaneId == 0)
                   && (w.Tarih == MazeretTarihi || MazeretTarihi == null))
                .OrderByDescending(o => o.Tarih).ThenBy(f => f.EczaneAdi);

            return PartialView(eczaneNobetMazeretler);
        }
        public List<EczaneNobetMazeretIstekDetay> GetModel(string unSelectedEczaneMazeretIstekIDs)
        {
            var eczaneNobetMazeretlerVeIstekler = new List<EczaneNobetMazeretIstekDetay>();
            List<EczaneNobetMazeretDetay> myEczaneNobetMazeretDetay = new List<EczaneNobetMazeretDetay>();
            List<EczaneNobetIstekDetay> myEczaneNobetIstekDetay = new List<EczaneNobetIstekDetay>();
            var liste = unSelectedEczaneMazeretIstekIDs.Split(',');

            if (unSelectedEczaneMazeretIstekIDs != "")
            {
                foreach (var item in liste)
                {
                    var id = item.Substring(0, item.IndexOf(';'));
                    var ind = item.IndexOf(';');
                    var tur = item.Substring(item.IndexOf(';') + 1, item.Length - item.IndexOf(';') - 1);
                    if (tur == "1")
                    {
                        eczaneNobetMazeretlerVeIstekler
                                          .Add(ConvertEczaneNobetIMazeretDetayToEczaneNobetMazeretIstekDetay
                                          (_eczaneNobetMazeretService.GetDetayById((Convert.ToInt32(id)))));
                    }
                    else if (tur == "2")
                    {
                        eczaneNobetMazeretlerVeIstekler
                                          .Add(ConvertEczaneNobetIstekDetayToEczaneNobetMazeretIstekDetay
                                          (_eczaneNobetIstekService.GetDetayById((Convert.ToInt32(id)))));
                    }
                }


            }
            return eczaneNobetMazeretlerVeIstekler;
        }
        public ActionResult EczaneNobetMazeretPartialView3(string unSelectedEczaneMazeretIstekIDs)
        {
            var eczaneNobetMazeretlerVeIstekler = new List<EczaneNobetMazeretIstekDetay>();
            eczaneNobetMazeretlerVeIstekler = GetModel(unSelectedEczaneMazeretIstekIDs);
            return View("Index", "EczaneNobetMazeretIstek");
        }
        public EczaneNobetMazeretIstekDetay ConvertEczaneNobetIstekDetayToEczaneNobetMazeretIstekDetay(EczaneNobetIstekDetay model)
        {
            var myEczaneNobetMazeretIstekDetay = new EczaneNobetMazeretIstekDetay
            {
                Aciklama = model.Aciklama,
                Ay = model.Ay,
                EczaneAdi = model.EczaneAdi,
                EczaneId = model.EczaneId,
                EczaneNobetGrupId = model.EczaneNobetGrupId,
                Gun = model.Gun,
                Id = model.Id,
                MazeretIstekAdi = model.IstekAdi,
                MazeretIstekId = 2,
                MazeretIstekTurId = model.IstekTurId,
                MazeretIstekTuru = model.IstekTuru,
                NobetGrupAdi = model.NobetGrupAdi,
                NobetGrupId = model.NobetGrupId,
                TakvimId = model.TakvimId,
                Tarih = model.Tarih,
                Yil = model.Yil,
                NobetGorevTipAdi = model.NobetGorevTipAdi
            };

            return myEczaneNobetMazeretIstekDetay;
        }
        public EczaneNobetMazeretIstekDetay ConvertEczaneNobetIMazeretDetayToEczaneNobetMazeretIstekDetay(EczaneNobetMazeretDetay model)
        {
            EczaneNobetMazeretIstekDetay myEczaneNobetMazeretIstekDetay = new EczaneNobetMazeretIstekDetay
            {
                Aciklama = model.Aciklama,
                Ay = model.Ay,
                EczaneAdi = model.EczaneAdi,
                EczaneId = model.EczaneId,
                EczaneNobetGrupId = model.EczaneNobetGrupId,
                Gun = model.Gun,
                Id = model.Id,
                MazeretIstekAdi = model.MazeretAdi,
                MazeretIstekId = 1,
                MazeretIstekTurId = model.MazeretTurId,
                MazeretIstekTuru = model.MazeretTuru,
                NobetGrupAdi = model.NobetGrupAdi,
                NobetGrupId = model.NobetGrupId,
                TakvimId = model.TakvimId,
                Tarih = model.Tarih,
                Yil = model.Yil,
                NobetGorevTipAdi = model.NobetGorevTipAdi
            };

            return myEczaneNobetMazeretIstekDetay;
        }
        public ActionResult EczaneNobetMazeretPartialView2(
            int[] eczaneId = null,
            DateTime? baslangicTarihi = null,
            DateTime? bitisTarihi = null,
            int? EczaneNobetMazeretIstekTipId = 0)
        {
            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            var nobetUstGrup = nobetUstGruplar.FirstOrDefault();

            var eczaneNobetMazeretler = eczaneId == null
                ? _eczaneNobetMazeretService.GetDetaylar(nobetUstGrup.Id)
                   //.Where(w => eczaneId.Contains(w.EczaneId))
                   .OrderByDescending(o => o.Tarih)
                   .ThenBy(f => f.EczaneAdi)
                   .ToList()
                : _eczaneNobetMazeretService.GetDetaylar(nobetUstGrup.Id)
                   .Where(w => eczaneId.Contains(w.EczaneId))
                   .OrderByDescending(o => o.Tarih)
                   .ThenBy(f => f.EczaneAdi)
                   .ToList();

            var eczaneNobetIstekler = eczaneId == null
                ? _eczaneNobetIstekService.GetDetaylar(nobetUstGrup.Id)
                  //.Where(w => eczaneId.Contains(w.EczaneId))
                  .OrderByDescending(o => o.Tarih)
                  .ThenBy(f => f.EczaneAdi)
                  .ToList()
                : _eczaneNobetIstekService.GetDetaylar(nobetUstGrup.Id)
                  .Where(w => eczaneId.Contains(w.EczaneId))
                  .OrderByDescending(o => o.Tarih)
                  .ThenBy(f => f.EczaneAdi)
                  .ToList();

            var eczaneNobetMazeretlerVeIstekler = new List<EczaneNobetMazeretIstekDetay>();

            if (EczaneNobetMazeretIstekTipId == 0)
            {//istek ve mazeret
                foreach (var item in eczaneNobetIstekler)
                {
                    eczaneNobetMazeretlerVeIstekler.Add(ConvertEczaneNobetIstekDetayToEczaneNobetMazeretIstekDetay(item));
                }
                foreach (var item in eczaneNobetMazeretler)
                {
                    eczaneNobetMazeretlerVeIstekler.Add(ConvertEczaneNobetIMazeretDetayToEczaneNobetMazeretIstekDetay(item));
                }
            }
            else if (EczaneNobetMazeretIstekTipId == 1)
            {//mazeret
                foreach (var item in eczaneNobetMazeretler)
                {
                    eczaneNobetMazeretlerVeIstekler.Add(ConvertEczaneNobetIMazeretDetayToEczaneNobetMazeretIstekDetay(item));
                }
            }
            else if (EczaneNobetMazeretIstekTipId == 2)
            {//istek
                foreach (var item in eczaneNobetIstekler)
                {
                    eczaneNobetMazeretlerVeIstekler.Add(ConvertEczaneNobetIstekDetayToEczaneNobetMazeretIstekDetay(item));
                }
            }

            var eczaneNobetMezaretIstekTipler = new List<MyDrop>();

            MyDrop Item1 = new MyDrop
            {
                Id = 1,
                Value = "Mazeret"
            };
            eczaneNobetMezaretIstekTipler.Add(Item1);

            MyDrop Item2 = new MyDrop
            {
                Id = 2,
                Value = "Istek"
            };
            eczaneNobetMezaretIstekTipler.Add(Item2);

            ViewBag.EczaneNobetMazeretIstekTipId = new SelectList(eczaneNobetMezaretIstekTipler, "Id", "Value", EczaneNobetMazeretIstekTipId);

            if (baslangicTarihi != null && bitisTarihi != null)
            {
                eczaneNobetMazeretlerVeIstekler = eczaneNobetMazeretlerVeIstekler.Where(w => w.Tarih >= baslangicTarihi && w.Tarih <= bitisTarihi).ToList();
            }
            else if (baslangicTarihi != null)
            {
                eczaneNobetMazeretlerVeIstekler = eczaneNobetMazeretlerVeIstekler.Where(w => w.Tarih >= baslangicTarihi).ToList();
            }
            else if (bitisTarihi != null)
            {
                eczaneNobetMazeretlerVeIstekler = eczaneNobetMazeretlerVeIstekler.Where(w => w.Tarih <= bitisTarihi).ToList();
            }

            //ViewData["Sonuclar"] = eczaneNobetMazeretlerVeIstekler.Count();
            //ViewData["Sonuclar"] = 2;

            return PartialView("EczaneNobetMazeretPartialView", eczaneNobetMazeretlerVeIstekler);
        }
        public JsonResult GetMazeretlerTumu()
        {
            //var user = _userService.GetByUserName(User.Identity.Name);
            //var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            //var nobetUstGrup = nobetUstGruplar.FirstOrDefault();
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var eczaneNobetMazeretler = _eczaneNobetMazeretService.GetDetaylar(nobetUstGrup.Id)
                   .OrderByDescending(o => o.Tarih)
                   .ThenBy(f => f.EczaneAdi)
                   .ToList();

            var eczaneNobetIstekler = _eczaneNobetIstekService.GetDetaylar(nobetUstGrup.Id)
                  .OrderByDescending(o => o.Tarih)
                  .ThenBy(f => f.EczaneAdi)
                  .ToList();

            var eczaneNobetMazeretlerVeIstekler = new List<EczaneNobetMazeretIstekDetay>();

            foreach (var item in eczaneNobetIstekler)
            {
                eczaneNobetMazeretlerVeIstekler.Add(ConvertEczaneNobetIstekDetayToEczaneNobetMazeretIstekDetay(item));
            }

            foreach (var item in eczaneNobetMazeretler)
            {
                eczaneNobetMazeretlerVeIstekler.Add(ConvertEczaneNobetIMazeretDetayToEczaneNobetMazeretIstekDetay(item));
            }

            var jsonResult = Json(eczaneNobetMazeretlerVeIstekler, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }
        public JsonResult GetMazeretler(int[] nobetGrupGorevTipId,
            DateTime? baslangicTarihi = null,
            DateTime? bitisTarihi = null,
            int? eczaneNobetMazeretIstekTipId = 0)
        {
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var eczaneNobetMazeretler = new List<EczaneNobetMazeretDetay>();
            var eczaneNobetIstekler = new List<EczaneNobetIstekDetay>();
            var eczaneNobetMazeretlerVeIstekler = new List<EczaneNobetMazeretIstekDetay>();

            if (nobetGrupGorevTipId == null)
            {
                throw new Exception("Lütfen nöbet grubunu seçiniz...");
            }

            if (eczaneNobetMazeretIstekTipId == 0)
            {
                eczaneNobetMazeretler = _eczaneNobetMazeretService.GetDetaylar(baslangicTarihi, bitisTarihi, nobetGrupGorevTipId);
                eczaneNobetIstekler = _eczaneNobetIstekService.GetDetaylar(baslangicTarihi, bitisTarihi, nobetGrupGorevTipId);
            }
            else if (eczaneNobetMazeretIstekTipId == 1)
            {
                eczaneNobetMazeretler = _eczaneNobetMazeretService.GetDetaylar(baslangicTarihi, bitisTarihi, nobetGrupGorevTipId);
            }
            else if (eczaneNobetMazeretIstekTipId == 2)
            {
                eczaneNobetIstekler = _eczaneNobetIstekService.GetDetaylar(baslangicTarihi, bitisTarihi, nobetGrupGorevTipId);
            }

            foreach (var item in eczaneNobetIstekler)
            {
                eczaneNobetMazeretlerVeIstekler.Add(ConvertEczaneNobetIstekDetayToEczaneNobetMazeretIstekDetay(item));
            }

            foreach (var item in eczaneNobetMazeretler)
            {
                eczaneNobetMazeretlerVeIstekler.Add(ConvertEczaneNobetIMazeretDetayToEczaneNobetMazeretIstekDetay(item));
            }

            var sonuclar = eczaneNobetMazeretlerVeIstekler
                .Where(w => w.MazeretIstekId == eczaneNobetMazeretIstekTipId
                        || eczaneNobetMazeretIstekTipId == 0)
                .OrderByDescending(o => o.Tarih)
                .ThenBy(f => f.EczaneAdi).ToList();

            var jsonResult = Json(sonuclar, JsonRequestBehavior.AllowGet);

            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        // GET: EczaneNobet/EczaneNobetMazeret/Details/5
        public ActionResult Details(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EczaneNobetMazeret eczaneNobetMazeret = _eczaneNobetMazeretService.GetById(id);
            if (eczaneNobetMazeret == null)
            {
                return HttpNotFound();
            }
            return View(eczaneNobetMazeret);
        }

        // GET: EczaneNobet/EczaneNobetMazeret/Create
        [Authorize(Roles = "Admin,Oda,Üst Grup")]
        public ActionResult Create()
        {
            //var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            //var eczaneler = _eczaneService.GetListByUser(user).Select(s => s.Id).ToList();
            var eczaneNobetGruplar = _eczaneNobetGrupService.GetDetaylar(nobetUstGrup.Id);

            ViewBag.EczaneNobetGrupId = new SelectList(_eczaneNobetGrupService.GetMyDrop(eczaneNobetGruplar),
            //.Select(s => new MyDrop { Id = s.Id, Value = $"{s.EczaneAdi}, {s.NobetGrupGorevTipAdi}" })
            //.OrderBy(s => s.Value), 
            "Id", "Value");

            ViewBag.MazeretId = new SelectList(_mazeretService.GetList().Where(w => w.Id != 3), "Id", "Adi");
            ViewBag.HaftaninGunu = new SelectList(_takvimService.GetHaftaninGunleri(), "Id", "Value");
            ViewBag.SecilenHaftaninGunuSayisi = 0;

            return View();
        }

        // POST: EczaneNobet/EczaneNobetMazeret/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EczaneNobetGrupId,MazeretId,BaslangicTarihi,BitisTarihi,HaftaninGunu,Aciklama")] EczaneNobetMazeretCoklu eczaneNobetMazeretCoklu)
        {
            //var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            //var haftaninGunleri = Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>().ToList();
            if (eczaneNobetMazeretCoklu.HaftaninGunu == null)
            {
                eczaneNobetMazeretCoklu.HaftaninGunu = new int[1] { 0 };
            }

            var haftaninGunu = eczaneNobetMazeretCoklu.HaftaninGunu;

            var eczaneNobetGrup = _eczaneNobetGrupService.GetDetaylar(eczaneNobetMazeretCoklu.EczaneNobetGrupId);

            var bayramlar = _nobetGrupGorevTipTakvimOzelGunService.GetDetaylar(eczaneNobetMazeretCoklu.BaslangicTarihi, eczaneNobetMazeretCoklu.BitisTarihi, eczaneNobetGrup.Select(s => s.NobetGrupId).ToList(), 1)
                .Where(w => eczaneNobetMazeretCoklu.HaftaninGunu.Contains(w.NobetGunKuralId)).ToList();

            var tarihAraligi = _takvimService.GetDetaylar(eczaneNobetMazeretCoklu.BaslangicTarihi, eczaneNobetMazeretCoklu.BitisTarihi);

            if (eczaneNobetMazeretCoklu.HaftaninGunu.Count() > 0)
            {
                tarihAraligi = tarihAraligi.Where(w => eczaneNobetMazeretCoklu.HaftaninGunu.Contains(w.HaftaninGunu)
                                                    || bayramlar.Select(s => s.TakvimId).Contains(w.TakvimId)).ToList();
            }

            var eczaneler = _eczaneService.GetDetaylar(nobetUstGrup.Id).Select(s => s.Id).ToList();

            var baslangicTarihi = _takvimService.GetByTarih(eczaneNobetMazeretCoklu.BaslangicTarihi);
            var bitisTarihi = _takvimService.GetByTarih(eczaneNobetMazeretCoklu.BitisTarihi);

            //seçilen tarih aralığı takvimde olmalıdır.
            if (baslangicTarihi == null || bitisTarihi == null)
            {
                var minYil = _takvimService.GetList().Min(x => x.Tarih.Year);
                var maxYil = _takvimService.GetList().Max(x => x.Tarih.Year);
                ViewBag.minYil = minYil;
                ViewBag.maxYil = maxYil;

                ViewBag.Mesaj = $"Başlangıç-Bitiş tarih aralığı enaz {minYil} ila ençok {maxYil} arasında olmalıdır...";
                ViewBag.EczaneNobetGrupId = new SelectList(_eczaneNobetGrupService.GetDetaylarByEczaneIdList(eczaneler)
                .Select(s => new MyDrop { Id = s.Id, Value = $"({s.EczaneAdi}, {s.NobetGrupGorevTipAdi}" }).OrderBy(s => s.Value), "Id", "Value", eczaneNobetMazeretCoklu.EczaneNobetGrupId);
                ViewBag.MazeretId = new SelectList(_mazeretService.GetList().Where(w => w.Id != 3), "Id", "Adi", eczaneNobetMazeretCoklu.MazeretId);
                ViewBag.HaftaninGunu = new SelectList(_takvimService.GetHaftaninGunleri(), "Id", "Value", eczaneNobetMazeretCoklu.HaftaninGunu);

                return View(eczaneNobetMazeretCoklu);
            }

            //Başlangıç tarihi Bitiş tarihinden büyük olamaz.
            if (baslangicTarihi.Id > bitisTarihi.Id)
            {
                ViewBag.Mesaj2 = $"Başlangıç tarihi ({baslangicTarihi.Tarih}) Bitiş tarihinden ({bitisTarihi.Tarih}) büyük olamaz...";
                ViewBag.EczaneNobetGrupId = new SelectList(_eczaneNobetGrupService.GetDetaylarByEczaneIdList(eczaneler)
                .Select(s => new MyDrop { Id = s.Id, Value = $"{s.EczaneAdi}, {s.NobetGrupGorevTipAdi}" }).OrderBy(s => s.Value), "Id", "Value", eczaneNobetMazeretCoklu.EczaneNobetGrupId);
                ViewBag.MazeretId = new SelectList(_mazeretService.GetList().Where(w => w.Id != 3), "Id", "Adi", eczaneNobetMazeretCoklu.MazeretId);
                ViewBag.HaftaninGunu = new SelectList(_takvimService.GetHaftaninGunleri(), "Id", "Value", eczaneNobetMazeretCoklu.HaftaninGunu);

                return View(eczaneNobetMazeretCoklu);
            }

            var eczaneNobetMazeretler = new List<EczaneNobetMazeret>();

            foreach (var eczaneNobetGrupId in eczaneNobetMazeretCoklu.EczaneNobetGrupId)
            {
                foreach (var item in tarihAraligi)
                {
                    eczaneNobetMazeretler.Add(new EczaneNobetMazeret
                    {
                        MazeretId = eczaneNobetMazeretCoklu.MazeretId,
                        EczaneNobetGrupId = eczaneNobetGrupId,// eczaneNobetMazeretCoklu.EczaneNobetGrupId,
                        TakvimId = item.TakvimId,
                        Aciklama = eczaneNobetMazeretCoklu.Aciklama,
                    });
                }
            }

            var eklenecekMazeretSayisi = eczaneNobetMazeretler.Count;

            if (ModelState.IsValid && eklenecekMazeretSayisi > 0)
            {
                try
                {
                    _eczaneNobetMazeretService.CokluEkle(eczaneNobetMazeretler);
                }
                catch (DbUpdateException ex)
                {
                    var hata = ex.InnerException.ToString();

                    string[] dublicateHata = { "Cannot insert dublicate row in object", "with unique index" };

                    var dublicateRowHatasiMi = dublicateHata.Any(h => hata.Contains(h));

                    if (dublicateRowHatasiMi)
                    {
                        throw new Exception("<strong>Bir eczaneye aynı gün için iki mazeret kaydı eklenemez...</strong>");
                    }

                    throw ex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                TempData["EklenenMazeretSayisi"] = eklenecekMazeretSayisi;
                ViewBag.EczaneNobetGrupId = new SelectList(_eczaneNobetGrupService.GetDetaylarByEczaneIdList(eczaneler)
                .Select(s => new MyDrop { Id = s.Id, Value = $"{s.EczaneAdi}, {s.NobetGrupGorevTipAdi}" }).OrderBy(s => s.Value), "Id", "Value", eczaneNobetMazeretCoklu.EczaneNobetGrupId);
                ViewBag.MazeretId = new SelectList(_mazeretService.GetList().Where(w => w.Id != 3), "Id", "Adi", eczaneNobetMazeretCoklu.MazeretId);
                ViewBag.HaftaninGunu = new SelectList(_takvimService.GetHaftaninGunleri(), "Id", "Value", eczaneNobetMazeretCoklu.HaftaninGunu);
                ViewBag.SecilenHaftaninGunuSayisi = eczaneNobetMazeretCoklu.HaftaninGunu.Count();
                //return RedirectToAction("Index");
                return View(eczaneNobetMazeretCoklu);
            }
            else
            {
                ViewBag.EczaneNobetGrupId = new SelectList(_eczaneNobetGrupService.GetDetaylarByEczaneIdList(eczaneler)
                                        .Select(s => new MyDrop
                                        {
                                            Id = s.Id,
                                            Value = $"{s.EczaneAdi}, {s.NobetGrupGorevTipAdi}"
                                        }).OrderBy(s => s.Value), "Id", "Value");

                ViewBag.MazeretId = new SelectList(_mazeretService.GetList().Where(w => w.Id != 3), "Id", "Adi", eczaneNobetMazeretCoklu.MazeretId);
                ViewBag.HaftaninGunu = new SelectList(_takvimService.GetHaftaninGunleri(), "Id", "Value");
                ViewBag.SecilenHaftaninGunuSayisi = eczaneNobetMazeretCoklu.HaftaninGunu.Count();

                //bayram ve hafta günleri kontrol
                if (bayramlar.Count == 0)
                {
                    if (eczaneNobetMazeretCoklu.HaftaninGunu.Where(w => w == 8 && w == 9).Count() > 0)
                    {
                        ViewBag.MesajBayram = $"Girilen tarih aralığına uygun bayram bulunmamaktadır.";
                    }
                    else if (eczaneNobetMazeretCoklu.HaftaninGunu.Where(w => w == 8).Count() > 0)
                    {
                        ViewBag.MesajBayram = $"Girilen tarih aralığına uygun dini bayram bulunmamaktadır.";
                    }
                    else if (eczaneNobetMazeretCoklu.HaftaninGunu.Where(w => w == 9).Count() > 0)
                    {
                        ViewBag.MesajBayram = $"Girilen tarih aralığına uygun milli bayram bulunmamaktadır.";
                    }
                    else if (eczaneNobetMazeretCoklu.HaftaninGunu.Where(w => w <= 7).Count() > 0)
                    {
                        ViewBag.MesajBayram = $"Girilen tarih aralığına uygun hafta günü bulunmamaktadır.";
                    }
                }
                else
                {
                    if (eczaneNobetMazeretCoklu.HaftaninGunu.Count() == 1)
                    {
                        ViewBag.MesajBayram = $"Seçilen hafta gününe uygun tarih aralığı bulunmamaktadır.";
                    }
                    else
                    {
                        ViewBag.MesajBayram = $"Seçilen hafta günlerine uygun tarih aralığı bulunmamaktadır.";
                    }
                }
            }

            return View(eczaneNobetMazeretCoklu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[HandleException]
        public ActionResult CreatePartial([Bind(Include = "Id,EczaneNobetGrupId,MazeretId,BaslangicTarihi,BitisTarihi,HaftaninGunu,Aciklama")] EczaneNobetMazeretCoklu eczaneNobetMazeretCoklu)
        {
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            //var haftaninGunleri = Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>().ToList();
            if (eczaneNobetMazeretCoklu.HaftaninGunu == null)
            {
                eczaneNobetMazeretCoklu.HaftaninGunu = new int[1] { 0 };
            }

            var haftaninGunu = eczaneNobetMazeretCoklu.HaftaninGunu;

            var eczaneNobetGrup = _eczaneNobetGrupService.GetDetaylar(eczaneNobetMazeretCoklu.EczaneNobetGrupId);

            var bayramlar = _nobetGrupGorevTipTakvimOzelGunService.GetDetaylar(eczaneNobetMazeretCoklu.BaslangicTarihi, eczaneNobetMazeretCoklu.BitisTarihi, eczaneNobetGrup.Select(s => s.NobetGrupId).ToList(), 1)
                .Where(w => eczaneNobetMazeretCoklu.HaftaninGunu.Contains(w.NobetGunKuralId)).ToList();

            var tarihAraligi = _takvimService.GetDetaylar(eczaneNobetMazeretCoklu.BaslangicTarihi, eczaneNobetMazeretCoklu.BitisTarihi);

            if (eczaneNobetMazeretCoklu.HaftaninGunu.Count() > 0)
            {
                tarihAraligi = tarihAraligi.Where(w => eczaneNobetMazeretCoklu.HaftaninGunu.Contains(w.HaftaninGunu)
                                                    || bayramlar.Select(s => s.TakvimId).Contains(w.TakvimId)).ToList();
            }

            var eczaneler = _eczaneService.GetDetaylar(nobetUstGrup.Id).Select(s => s.Id).ToList();

            var baslangicTarihi = _takvimService.GetByTarih(eczaneNobetMazeretCoklu.BaslangicTarihi);
            var bitisTarihi = _takvimService.GetByTarih(eczaneNobetMazeretCoklu.BitisTarihi);

            //seçilen tarih aralığı takvimde olmalıdır.
            if (baslangicTarihi == null || bitisTarihi == null)
            {
                var minYil = _takvimService.GetList().Min(x => x.Tarih.Year);
                var maxYil = _takvimService.GetList().Max(x => x.Tarih.Year);
                ViewBag.minYil = minYil;
                ViewBag.maxYil = maxYil;

                ViewBag.Mesaj = $"Başlangıç-Bitiş tarih aralığı enaz {minYil} ila ençok {maxYil} arasında olmalıdır...";

                return PartialView();
            }

            //Başlangıç tarihi Bitiş tarihinden büyük olamaz.
            if (baslangicTarihi.Id > bitisTarihi.Id)
            {
                ViewBag.Mesaj2 = $"Başlangıç tarihi ({baslangicTarihi.Tarih}) Bitiş tarihinden ({bitisTarihi.Tarih}) büyük olamaz...";

                return PartialView();
            }

            var eczaneNobetMazeretler = new List<EczaneNobetMazeret>();

            foreach (var eczaneNobetGrupId in eczaneNobetMazeretCoklu.EczaneNobetGrupId)
            {
                foreach (var item in tarihAraligi)
                {
                    eczaneNobetMazeretler.Add(new EczaneNobetMazeret
                    {
                        MazeretId = eczaneNobetMazeretCoklu.MazeretId,
                        EczaneNobetGrupId = eczaneNobetGrupId,// eczaneNobetMazeretCoklu.EczaneNobetGrupId,
                        TakvimId = item.TakvimId,
                        Aciklama = eczaneNobetMazeretCoklu.Aciklama,
                    });
                }
            }

            var eklenecekMazeretSayisi = eczaneNobetMazeretler.Count;
            var eklenenEczaneler = new List<EczaneNobetMazeretDetay>();

            if (ModelState.IsValid && eklenecekMazeretSayisi > 0)
            {
                try
                {
                    _eczaneNobetMazeretService.CokluEkle(eczaneNobetMazeretler);

                    foreach (var item in eczaneNobetMazeretler)
                    {
                        var eczane = _eczaneNobetGrupService.GetDetayById(item.EczaneNobetGrupId);

                        eklenenEczaneler.Add(new EczaneNobetMazeretDetay
                        {
                            NobetGorevTipAdi = eczane.NobetGorevTipAdi,
                            EczaneAdi = eczane.EczaneAdi,
                            Tarih = _takvimService.GetById(item.TakvimId).Tarih,
                            Aciklama = item.Aciklama
                        });
                    }
                }
                catch (DbUpdateException ex)
                {
                    var hata = ex.InnerException.ToString();

                    string[] dublicateHata = { "Cannot insert dublicate row in object", "with unique index" };

                    var dublicateRowHatasiMi = dublicateHata.Any(h => hata.Contains(h));

                    if (dublicateRowHatasiMi)
                    {
                        //throw new Exception("<strong>Bir eczaneye aynı gün için iki mazeret kaydı eklenemez...</strong>");
                        return PartialView("ErrorDublicateRowPartial");
                    }

                    //throw ex;
                }
                catch (Exception)
                {
                    return PartialView("ErrorPartial");
                    //throw ex;
                }

                TempData["EklenenMazeretSayisi"] = eklenenEczaneler.Count;

                TempData["EklenenMazeretler"] = eklenenEczaneler;

                ViewBag.SecilenHaftaninGunuSayisi = eczaneNobetMazeretCoklu.HaftaninGunu.Count();
                //return RedirectToAction("Index");
                return PartialView();
            }
            else
            {
                //bayram ve hafta günleri kontrol
                if (bayramlar.Count == 0)
                {
                    if (eczaneNobetMazeretCoklu.HaftaninGunu.Where(w => w == 8 && w == 9).Count() > 0)
                    {
                        ViewBag.MesajBayram = $"Girilen tarih aralığına uygun bayram bulunmamaktadır.";
                    }
                    else if (eczaneNobetMazeretCoklu.HaftaninGunu.Where(w => w == 8).Count() > 0)
                    {
                        ViewBag.MesajBayram = $"Girilen tarih aralığına uygun dini bayram bulunmamaktadır.";
                    }
                    else if (eczaneNobetMazeretCoklu.HaftaninGunu.Where(w => w == 9).Count() > 0)
                    {
                        ViewBag.MesajBayram = $"Girilen tarih aralığına uygun milli bayram bulunmamaktadır.";
                    }
                    else if (eczaneNobetMazeretCoklu.HaftaninGunu.Where(w => w <= 7).Count() > 0)
                    {
                        ViewBag.MesajBayram = $"Girilen tarih aralığına uygun hafta günü bulunmamaktadır.";
                    }
                }
                else
                {
                    if (eczaneNobetMazeretCoklu.HaftaninGunu.Count() == 1)
                    {
                        ViewBag.MesajBayram = $"Seçilen hafta gününe uygun tarih aralığı bulunmamaktadır.";
                    }
                    else
                    {
                        ViewBag.MesajBayram = $"Seçilen hafta günlerine uygun tarih aralığı bulunmamaktadır.";
                    }
                }
            }

            return PartialView();
        }

        //protected override void OnException(ExceptionContext filterContext)
        //{

        //    filterContext.ExceptionHandled = true;

        //    //if (filterContext.ExceptionHandled)
        //    //{
        //    //    return;
        //    //}

        //    filterContext.Result = new PartialViewResult
        //    {//D:\Projects\WorkingModels\WM.UI.Mvc\Areas\EczaneNobet\Views\Shared\ErrorPartial.cshtml
        //        ViewName = "~/Areas/EczaneNobet/Views/Shared/ErrorPartial.cshtml"
        //    };
        //}
        // GET: EczaneNobet/EczaneNobetMazeret/Edit/5
        [Authorize(Roles = "Admin,Oda,Üst Grup")]
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EczaneNobetMazeret eczaneNobetMazeret = _eczaneNobetMazeretService.GetById(id);
            if (eczaneNobetMazeret == null)
            {
                return HttpNotFound();
            }
            var user = _userService.GetByUserName(User.Identity.Name);

            var yil = DateTime.Now.AddMonths(1).Year;
            var ay = DateTime.Now.AddMonths(1).Month;

            var tarihler = _takvimService.GetList()
                .Where(w => w.Tarih.Year == yil
                         //&& w.Tarih.Month == ay
                         )
                .Select(s => new MyDrop { Id = s.Id, Value = s.Tarih.ToLongDateString() });

            var eczaneler = _eczaneService.GetListByUser(user).Select(s => s.Id).ToList();
            ViewBag.EczaneNobetGrupId = new SelectList(_eczaneNobetGrupService.GetDetaylarByEczaneIdList(eczaneler)
                .Select(s => new MyDrop { Id = s.Id, Value = $"{s.EczaneAdi} ({s.NobetGrupAdi})" }).OrderBy(s => s.Value), "Id", "Value", eczaneNobetMazeret.EczaneNobetGrupId);
            ViewBag.MazeretId = new SelectList(_mazeretService.GetList(), "Id", "Adi", eczaneNobetMazeret.MazeretId);
            ViewBag.TakvimId = new SelectList(tarihler, "Id", "Value", eczaneNobetMazeret.TakvimId);
            return View(eczaneNobetMazeret);
        }

        // POST: EczaneNobet/EczaneNobetMazeret/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Oda,Üst Grup")]
        public ActionResult Edit([Bind(Include = "Id,EczaneNobetGrupId,MazeretId,TakvimId,Aciklama")] EczaneNobetMazeret eczaneNobetMazeret)
        {
            if (ModelState.IsValid)
            {
                _eczaneNobetMazeretService.Update(eczaneNobetMazeret);
                return RedirectToAction("Index");
            }
            var user = _userService.GetByUserName(User.Identity.Name);
            var eczaneler = _eczaneService.GetListByUser(user).Select(s => s.Id).ToList();
            ViewBag.EczaneNobetGrupId = new SelectList(_eczaneNobetGrupService.GetDetaylarByEczaneIdList(eczaneler)
                .Select(s => new MyDrop { Id = s.Id, Value = $"{s.EczaneAdi} ({s.NobetGrupAdi})" }).OrderBy(s => s.Value), "Id", "Value", eczaneNobetMazeret.EczaneNobetGrupId);
            ViewBag.MazeretId = new SelectList(_mazeretService.GetList(), "Id", "Adi", eczaneNobetMazeret.MazeretId);
            ViewBag.TakvimId = new SelectList(_takvimService.GetList(), "Id", "Tarih", eczaneNobetMazeret.TakvimId);
            return View(eczaneNobetMazeret);
        }
        
        //[HttpPost]
        public void SecilenleriSil(string selectedEczaneMazeretIstekIDs, string unSelectedEczaneMazeretIstekIDs)
        {
            var cor = "Seçim Yapmadınız!";

            if (selectedEczaneMazeretIstekIDs == "")
            {
                throw new Exception(cor);
                //return Json(cor, JsonRequestBehavior.AllowGet);
            }
            //seçilenleri sil
            var eczaneNobetMazeret = new EczaneNobetMazeret();
            var eczaneNobetIstek = new EczaneNobetIstek();
            var silinecekKayitlar = selectedEczaneMazeretIstekIDs.Split(',');

            foreach (string item in silinecekKayitlar)
            {
                var id = item.Substring(0, item.IndexOf(';'));
                var ind = item.IndexOf(';');
                var tur = item.Substring(item.IndexOf(';') + 1, item.Length - item.IndexOf(';') - 1);
                if (tur == "1")
                {
                    //eczaneNobetMazeret = _eczaneNobetMazeretService.GetById(Convert.ToInt32(id));
                    _eczaneNobetMazeretService.Delete(Convert.ToInt32(id));
                }
                else if (tur == "2")
                {
                    //eczaneNobetIstek = _eczaneNobetIstekService.GetById(Convert.ToInt32(id));
                    _eczaneNobetIstekService.Delete(Convert.ToInt32(id));
                }
            }
            //TempData["silinenMazeretSayisi"] = liste.Length;
            //seçilmeyenleri döndür
            //var eczaneNobetMazeretlerVeIstekler = new List<EczaneNobetMazeretIstekDetay>();
            //eczaneNobetMazeretlerVeIstekler = GetModel(unSelectedEczaneMazeretIstekIDs);

            //return PartialView("EczaneNobetMazeretPartialView", eczaneNobetMazeretlerVeIstekler);
            //  return Json(unSelectedEczaneMazeretIstekIDs, JsonRequestBehavior.AllowGet);          
        }
        
        // GET: EczaneNobet/EczaneNobetMazeret/Delete/5
        [Authorize(Roles = "Admin,Oda,Üst Grup")]
        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var eczaneNobetMazeret = _eczaneNobetMazeretService.GetDetayById(id);
            if (eczaneNobetMazeret == null)
            {
                return HttpNotFound();
            }
            return View(eczaneNobetMazeret);
        }

        // POST: EczaneNobet/EczaneNobetMazeret/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Oda,Üst Grup")]
        public ActionResult DeleteConfirmed(int id)
        {
            EczaneNobetMazeret eczaneNobetMazeret = _eczaneNobetMazeretService.GetById(id);
            _eczaneNobetMazeretService.Delete(id);
            return RedirectToAction("Index");
        }

    }
}
