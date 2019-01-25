using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using WM.Northwind.Business.Abstract.Authorization;
using WM.Northwind.Business.Abstract.EczaneNobet;
using WM.Northwind.Business.Abstract.Optimization.EczaneNobet;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.Optimization.EczaneNobet;
using WM.UI.Mvc.Areas.EczaneNobet.Filters;
using WM.UI.Mvc.Areas.EczaneNobet.Models;

namespace WM.UI.Mvc.Areas.EczaneNobet.Controllers
{
    [Authorize]
    [HandleError]
    public class NobetYazController : Controller
    {
        #region ctor
        private IAlanyaOptimizationServiceV2 _alanyaOptimizationService;
        private IAntalyaMerkezOptimizationService _antalyaMerkezOptimizationService;
        private IMersinMerkezOptimizationServiceV2 _mersinMerkezOptimizationServiceV2;
        private IGiresunOptimizationService _giresunOptimizationService;
        private IOsmaniyeOptimizationService _osmaniyeOptimizationService;
        private IBartinOptimizationService _bartinOptimizationService;

        private IEczaneNobetGrupService _eczaneNobetGrupService;
        private INobetGrupGorevTipService _nobetGrupGorevTipService;
        private INobetGrupService _nobetGrupService;
        private IEczaneGrupService _eczaneGrupService;
        private INobetUstGrupService _nobetUstGrupService;
        private ITakvimService _takvimService;
        private IUserNobetUstGrupService _userNobetUstGrupService;
        private INobetUstGrupKisitService _nobetUstGrupKisitService;
        private IEczaneNobetSonucService _eczaneNobetSonucService;
        private IUserService _userService;

        public NobetYazController(IAlanyaOptimizationServiceV2 alanyaOptimizationService,
                                  IAntalyaMerkezOptimizationService antalyaMerkezOptimizationService,
                                  IMersinMerkezOptimizationServiceV2 mersinMerkezOptimizationServiceV2,
                                  IGiresunOptimizationService giresunOptimizationService,
                                  IOsmaniyeOptimizationService osmaniyeOptimizationService,
                                  IBartinOptimizationService bartinOptimizationService,

                                  IEczaneNobetGrupService eczaneNobetGrupService,
                                  INobetGrupGorevTipService nobetGrupGorevTipService,
                                  INobetGrupService nobetGrupService,
                                  IEczaneGrupService eczaneGrupService,
                                  INobetUstGrupService nobetUstGrupService,
                                  IUserNobetUstGrupService userNobetUstGrupService,
                                  IUserService userService,
                                  ITakvimService takvimService,
                                  INobetUstGrupKisitService nobetUstGrupKisitService,
                                  IEczaneNobetSonucService eczaneNobetSonucService
            )
        {
            _alanyaOptimizationService = alanyaOptimizationService;
            _antalyaMerkezOptimizationService = antalyaMerkezOptimizationService;
            _mersinMerkezOptimizationServiceV2 = mersinMerkezOptimizationServiceV2;
            _giresunOptimizationService = giresunOptimizationService;
            _osmaniyeOptimizationService = osmaniyeOptimizationService;
            _bartinOptimizationService = bartinOptimizationService;

            _eczaneNobetGrupService = eczaneNobetGrupService;
            _nobetGrupGorevTipService = nobetGrupGorevTipService;
            _nobetGrupService = nobetGrupService;
            _eczaneGrupService = eczaneGrupService;
            _nobetUstGrupService = nobetUstGrupService;
            _takvimService = takvimService;
            _userNobetUstGrupService = userNobetUstGrupService;
            _userService = userService;
            _nobetUstGrupKisitService = nobetUstGrupKisitService;
            _eczaneNobetSonucService = eczaneNobetSonucService;
        }
        #endregion

        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception.Message.StartsWith("Aşağıdaki"))
            {
                var model = new HandleErrorInfo(filterContext.Exception, "NobetYaz", "ModelCoz");

                filterContext.Result = new ViewResult
                {
                    ViewName = "ErrorModelCoz",
                    ViewData = new ViewDataDictionary<HandleErrorInfo>(model),
                };
                filterContext.ExceptionHandled = true;
            }
            else
            {
                base.OnException(filterContext);
            }
        }

        // GET: Index
        public ActionResult Index()
        {
            var user = _userService.GetByUserName(User.Identity.Name);
            var rolIdler = _userService.GetUserRoles(user).OrderBy(s => s.RoleId).Select(u => u.RoleId).ToArray();
            var rolId = rolIdler.FirstOrDefault();

            var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            var nobetUstGrup = nobetUstGruplar.FirstOrDefault();

            var gelecekTarih = DateTime.Now.AddMonths(1);
            var gelecekAy = gelecekTarih.Month;
            var yilCozulen = gelecekTarih.Year;
            int aydakiGunSayisi = DateTime.DaysInMonth(yilCozulen, gelecekAy);

            if (TempData["kesinlesenNobetGrupId"] != null)
            {
                ViewBag.KesinlesenGrup = TempData["kesinlesenNobetGrupId"];
                ViewBag.KesinlesenGrupSayisi = TempData["KesinlesenNobetGrupSayisi"];
                ViewBag.KesinlesenBaslangicTarihi = Convert.ToDateTime(TempData["KesinlesenBaslangicTarihi"]).ToShortDateString();
                ViewBag.KesinlesenBitisTarihi = Convert.ToDateTime(TempData["KesinlesenBitisTarihi"]).ToShortDateString();
            }

            var baslangicTarihi = DateTime.Today; //new DateTime(gelecekTarih.Year, gelecekTarih.Month, 1);
            var bitisTarihi = new DateTime(gelecekTarih.Year, gelecekTarih.Month, aydakiGunSayisi);

            ViewBag.NobetUstGrupId = new SelectList(items: nobetUstGruplar, dataValueField: "Id", dataTextField: "Adi", selectedValue: nobetUstGrup.Id);
            ViewBag.NobetUstGrupSayisi = nobetUstGruplar.Count;

            var sonNobetTarihi = _eczaneNobetSonucService.GetSonNobetTarihi(nobetUstGrup.Id);
            var model = new NobetYazViewModel
            {
                RolId = rolId,
                NobetUstGrupId = nobetUstGrup.Id,
                BaslangicTarihi = sonNobetTarihi < nobetUstGrup.BaslangicTarihi ? nobetUstGrup.BaslangicTarihi : baslangicTarihi,
                BitisTarihi = bitisTarihi,
                SonNobetTarihi = sonNobetTarihi < nobetUstGrup.BaslangicTarihi ? nobetUstGrup.BaslangicTarihi.AddDays(-1) : sonNobetTarihi
            };


            if (TempData["KesinlesenBaslangicTarihi"] != null)
            {
                model.BaslangicTarihi = (DateTime)TempData["KesinlesenBaslangicTarihi"];
            }

            if (TempData["KesinlesenBitisTarihi"] != null)
            {
                model.BitisTarihi = (DateTime)TempData["KesinlesenBitisTarihi"];
            }

            if (TempData["TaslaktakiBaslamaTarihi"] != null)
            {
                model.BaslangicTarihi = (DateTime)TempData["TaslaktakiBaslamaTarihi"];
            }

            if (TempData["TaslaktakiBitisTarihi"] != null)
            {
                model.BitisTarihi = (DateTime)TempData["TaslaktakiBitisTarihi"];
            }

            return View(model);
        }

        public ActionResult Anasayfa(int nobetGrupId, int yil, int ay)
        {
            TempData["NobetGrupId"] = nobetGrupId;
            TempData["Yil"] = yil;
            TempData["Ay"] = ay;

            return RedirectToAction("Index");
        }

        [HttpPost]
        //[HandleError(View= "ErrorModelCoz")]
        //[HandleException]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Oda,Üst Grup")]
        public ActionResult ModelCoz(NobetYazViewModel eczaneNobetViewModel)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var nobetUstGrup = _nobetUstGrupService.GetById(eczaneNobetViewModel.NobetUstGrupId);
            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylarByIdList(eczaneNobetViewModel.NobetGrupGorevTipId.ToList());
            var nobetGrupIdList = nobetGrupGorevTipler.Select(s => s.NobetGrupId).Distinct().ToArray();

            if (nobetGrupIdList == null)
                nobetGrupIdList = new int[1] { 0 };

            var eczaneNobetModelCoz = new EczaneNobetModelCoz
            {
                BuAyVeSonrasi = eczaneNobetViewModel.BuAyVeSonrasi,
                NobetGrupGorevTipler = nobetGrupGorevTipler,
                NobetGrupId = nobetGrupIdList, //eczaneNobetViewModel.NobetGrupId,
                NobetUstGrupId = eczaneNobetViewModel.NobetUstGrupId,
                RolId = eczaneNobetViewModel.RolId,
                AyBitis = eczaneNobetViewModel.BitisTarihi.Month,
                NobetUstGrupBaslangicTarihi = nobetUstGrup.BaslangicTarihi,
                CozumTercih = eczaneNobetViewModel.CozumTercih,
                SonrakiAylarRasgele = eczaneNobetViewModel.SonrakiAylarRasgele,
                BaslangicTarihi = eczaneNobetViewModel.BaslangicTarihi,
                BitisTarihi = eczaneNobetViewModel.BitisTarihi
            };

            var sonucModel = new EczaneNobetSonucModel();

            switch (nobetUstGrup.Id)
            {
                case 1:
                    sonucModel = _alanyaOptimizationService.ModelCoz(eczaneNobetModelCoz);
                    break;
                case 2:
                    sonucModel = _antalyaMerkezOptimizationService.ModelCoz(eczaneNobetModelCoz);
                    break;
                case 3:
                    sonucModel = _mersinMerkezOptimizationServiceV2.ModelCoz(eczaneNobetModelCoz);
                    break;
                case 4:
                    sonucModel = _giresunOptimizationService.ModelCoz(eczaneNobetModelCoz);
                    break;
                case 5:
                    sonucModel = _osmaniyeOptimizationService.ModelCoz(eczaneNobetModelCoz);
                    break;
                case 6:
                    sonucModel = _bartinOptimizationService.ModelCoz(eczaneNobetModelCoz);
                    break;
                default:
                    return RedirectToAction("Index");
            }

            stopwatch.Stop();
            sonucModel.ToplamSure = stopwatch.Elapsed;
            TempData["EczaneNobetSonuclar"] = sonucModel;

            if (eczaneNobetModelCoz.BuAyVeSonrasi)
            {
                return RedirectToAction("PivotCozum", "EczaneNobetSonuc",
                    new { area = "EczaneNobet", nobetGrup = eczaneNobetModelCoz.NobetGrupId, yilBaslangic = eczaneNobetModelCoz.BaslangicTarihi.Year, yilBitis = eczaneNobetModelCoz.BitisTarihi.Year });
            }
            else if (eczaneNobetModelCoz.CozumTercih == 0 && eczaneNobetModelCoz.NobetGrupId.Count() > 1)
            {
                return RedirectToAction("PivotCozum", "EczaneNobetSonuc",
                    new { area = "EczaneNobet", nobetGrup = eczaneNobetModelCoz.NobetGrupId, yilBaslangic = eczaneNobetModelCoz.BaslangicTarihi.Year, yilBitis = eczaneNobetModelCoz.BitisTarihi.Year });
            }

            var routeValues = new PivotSonuclarParams
            {
                Area = "EczaneNobet",
                BaslangicTarihi = eczaneNobetModelCoz.BaslangicTarihi,
                BitisTarihi = eczaneNobetModelCoz.BitisTarihi
            };

            int sayac = 0;
            foreach (var item in nobetGrupIdList)
            {
                if (sayac == 0)
                    routeValues.NobetGrupIdList = item.ToString();
                else
                    routeValues.NobetGrupIdList = routeValues.NobetGrupIdList + "," + item.ToString();
                sayac++;
            }

            return RedirectToAction("PivotSonuclar", "EczaneNobetSonucAktif", routeValues);
        }

        [ChildActionOnly]
        public PartialViewResult EczaneNobetDataPartialView()
        {
            var categories = new List<KeyValuePair<int, string>>()
            {
                //new KeyValuePair<int, string>(1,"Eczane Odası"),
                //new KeyValuePair<int, string>(2,"Nöbet Üst Grupları"),
                //new KeyValuePair<int, string>(3,"Nöbet Grupları"),
                //new KeyValuePair<int, string>(4,"Eczaneler"),
                new KeyValuePair<int, string>(5,"Eczane Mazeret Girişi"),
                //new KeyValuePair<int, string>(6,"Bayramlar"),
                //new KeyValuePair<int, string>(7,"Eczane (Eş/Sınır) Grubu Tanımlama"),
                new KeyValuePair<int, string>(8,"Eczane Nöbet Grup"),
                //new KeyValuePair<int, string>(9,"Mazeret Türü Tanımlama"),
                //new KeyValuePair<int, string>(10,"Mazeret Tanımlama"),
                new KeyValuePair<int, string>(11,"Eczane Grup"),
                //new KeyValuePair<int, string>(12,"Takvim"),
                //new KeyValuePair<int, string>(13,"Taslak Sonuçlar"),
                new KeyValuePair<int, string>(14,"Sonuçlar"),
                new KeyValuePair<int, string>(15,"Sonuçlar Görsel")
            };

            var currCtgId = Convert.ToInt32(Request.QueryString["categoryId"]);

            var model = new EczaneNobetPartialViewModel
            {
                Categories = categories,
                CurrentCategory = currCtgId
            };

            return PartialView(model);
        }

        //[ChildActionOnly]
        public ActionResult NobetGruplarDDLPartialView(int nobetUstGrupId)
        {
            //int[] nobetGrupId = new int[] { 0 };

            //if (TempData["NobetGrupId"] != null)
            //{
            //    nobetGrupId = (int[])TempData["NobetGrupId"];
            //}
            var nobetGruplar = _eczaneNobetGrupService.NobetGruplarDDL(nobetUstGrupId);

            #region başka gruplarla ilişkisi olan gruplar
            var eczaneGruplar = _eczaneGrupService.GetDetaylarAktifGruplar(nobetUstGrupId);

            //Birbiri ile ilişkili grupların gruplanması
            var esliNobetGruplar = _eczaneGrupService.EsGrupluEczanelerinGruplariniBelirleTumu(eczaneGruplar, nobetGruplar.Select(s => s.NobetGrupId).ToList());

            var nobetGorevTipler = nobetGruplar.Select(s => s.NobeGorevTipId).Distinct().ToList();

            #endregion

            var nobetGruplarDDL = new List<MyDrop>();

            if (esliNobetGruplar.Select(s => s.Id).Distinct().Count() > 1)
            {
                if (nobetGorevTipler.Count > 1)
                {
                    nobetGruplarDDL = (from g in nobetGruplar
                                       from e in esliNobetGruplar
                                       where g.NobetGrupId == e.NobetGrupId
                                       orderby e.Id, g.NobetGrupId
                                       select new MyDrop
                                       {
                                           Id = g.NobetGrupGorevTipId,
                                           Value = $"{g.NobetGrupGorevTipId} {g.NobetGrupAdi}, {g.NobeGorevTipAdi} ({g.EczaneSayisi}), G:{e.Id}"
                                       }).ToList();
                }
                else
                {
                    nobetGruplarDDL = (from g in nobetGruplar
                                       from e in esliNobetGruplar
                                       where g.NobetGrupId == e.NobetGrupId
                                       orderby e.Id, g.NobetGrupId
                                       select new MyDrop
                                       {
                                           Id = g.NobetGrupGorevTipId,
                                           Value = $"{g.NobetGrupGorevTipId} {g.NobetGrupAdi} ({g.EczaneSayisi}), G:{e.Id}"
                                       }).ToList();
                }
            }
            else
            {
                if (nobetGorevTipler.Count > 1)
                {
                    nobetGruplarDDL = (from g in nobetGruplar
                                       select new MyDrop
                                       {
                                           Id = g.NobetGrupGorevTipId,
                                           Value = $"{g.NobetGrupGorevTipId} {g.NobetGrupAdi}, {g.NobeGorevTipAdi} ({g.EczaneSayisi})"
                                       })
                                   .OrderBy(o => o.Id).ToList();
                }
                else
                {
                    nobetGruplarDDL = (from g in nobetGruplar
                                       select new MyDrop
                                       {
                                           Id = g.NobetGrupGorevTipId,
                                           Value = $"{g.NobetGrupGorevTipId} {g.NobetGrupAdi} ({g.EczaneSayisi})"
                                       })
                                   .OrderBy(o => o.Id).ToList();
                }
            }

            ViewBag.NobetGrupGorevTipId = new SelectList(nobetGruplarDDL, "Id", "Value");
            ViewBag.grupSayisi = nobetGruplarDDL.Count();

            TempData["NobetGrupAdi"] = nobetGruplar.Select(s => GetNobetGrupGorevTipAdlari(s.NobetUstGrupId, s.NobetGrupGorevTipId, s.NobetGrupGorevTipAdi, s.NobetGrupAdi)).ToArray();
            TempData["GruptakiEczaneSayisi"] = nobetGruplar.Select(s => s.EczaneSayisi).ToArray();

            return PartialView();
        }

        string GetNobetGrupGorevTipAdlari(int nobetUstGrupId, int nobetGrupGorevTipId, string nobetGrupGorevTipAdi, string nobetGrupAdi)
        {
            var sonuc = "";

            if (nobetUstGrupId == 4)
            {
                sonuc = nobetGrupGorevTipAdi;
            }
            else
            {
                if (nobetGrupGorevTipId == 17)
                {
                    sonuc = nobetGrupAdi.Substring(0, 10);
                }
                else if (nobetGrupAdi.Length > 11)
                {
                    sonuc = nobetGrupAdi.Substring(0, 11);
                }
                else
                {
                    sonuc = nobetGrupAdi;
                }
                //sonuc = nobetGrupAdi;
            }
            return sonuc;
        }
        public ActionResult NobetGrupGorevTiplerDDLPartialView(int nobetUstGrupId)
        {
            var nobetGrupGorevTipler = _eczaneNobetGrupService.NobetGruplarDDL(nobetUstGrupId);
            ViewBag.NobetGrupId = new SelectList(nobetGrupGorevTipler, "Id", "Value");

            return PartialView();
        }
    }
}


//if (eczaneNobetModelCoz.BuAyVeSonrasi)
//{
//    return RedirectToAction("PivotCozum", "EczaneNobetSonuc",
//        new { area = "EczaneNobet", nobetGrup = eczaneNobetModelCoz.NobetGrupId, yilBaslangic = eczaneNobetModelCoz.YilBaslangic, yilBitis = eczaneNobetModelCoz.YilBitis });
//}
//else if (eczaneNobetModelCoz.CozumTercih == 0 && eczaneNobetModelCoz.NobetGrupId.Count() > 1)
//{
//    return RedirectToAction("PivotCozum", "EczaneNobetSonuc",
//        new { area = "EczaneNobet", nobetGrup = eczaneNobetModelCoz.NobetGrupId, yilBaslangic = eczaneNobetModelCoz.YilBaslangic, yilBitis = eczaneNobetModelCoz.YilBitis });
//}

//if (eczaneNobetViewModel.BaslangicTarihi < nobetUstGrup.BaslangicTarihi)
//{//nöbet yazılacak tarihin ilk günü üst grubun başlama tarihinden küçük olamaz.
//    var user = _userService.GetByUserName(User.Identity.Name);
//    var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);

//    //var yillar = _takvimService.GetList()
//    //    .Where(w => w.Tarih.Year >= eczaneNobetViewModel.Yil)
//    //                .Select(s => s.Tarih.Year).Distinct().ToList();

//    //var aylar = _takvimService.GetAylarDdl();

//    ViewBag.NobetUstGrupId = new SelectList(items: nobetUstGruplar, dataValueField: "Id", dataTextField: "Adi", selectedValue: eczaneNobetViewModel.NobetUstGrupId);
//    //ViewBag.Yil = new SelectList(items: yillar, selectedValue: eczaneNobetViewModel.Yil);
//    //ViewBag.Ay = new SelectList(items: aylar, dataValueField: "Id", dataTextField: "Value", selectedValue: eczaneNobetViewModel.Ay);

//    ViewBag.NobetUstGrupSayisi = nobetUstGruplar.Count;

//    TempData["NobetGrupId"] = eczaneNobetViewModel.NobetGrupId;

//    var model = new NobetYazViewModel
//    {
//        //Yil = yilCozulen,
//        //Ay = gelecekAy,
//        RolId = eczaneNobetViewModel.RolId,
//        NobetUstGrupId = eczaneNobetViewModel.NobetUstGrupId,
//        //DegisenKisitSayisi = _nobetUstGrupKisitService.GetDegisenKisitlar(eczaneNobetViewModel.NobetUstGrupId),
//        //BaslangicGunu = 1,//new DateTime(yilCozulen, gelecekAy.Month, 1),
//        //BitisGunu = aydakiGunSayisi//new DateTime(yilCozulen, gelecekAy.Month, aydakiGunSayisi),
//        BaslangicTarihi = eczaneNobetViewModel.BaslangicTarihi,
//        BitisTarihi = eczaneNobetViewModel.BitisTarihi
//    };

//    ViewBag.BaslangicTarihiUyari = $"İlk nöbet tarihi ({eczaneNobetViewModel.BaslangicTarihi.ToShortDateString()}) üst grup başlama tarihinden ({nobetUstGrup.BaslangicTarihi.ToShortDateString()}) küçük olamaz.";

//    return View("Index", model);
//}

//var sonuclar = _eczaneNobetSonucService.GetDetaylar2(eczaneNobetViewModel.BaslangicTarihi, eczaneNobetViewModel.BitisTarihi, eczaneNobetModelCoz.NobetGrupId);

//var sure_sonuclar = stopwatch.Elapsed;

//if (sonuclar.Count > 0)
//{
//    var user = _userService.GetByUserName(User.Identity.Name);
//    var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
//    ViewBag.NobetUstGrupId = new SelectList(items: nobetUstGruplar, dataValueField: "Id", dataTextField: "Adi", selectedValue: eczaneNobetViewModel.NobetUstGrupId);
//    ViewBag.NobetUstGrupSayisi = nobetUstGruplar.Count;
//    TempData["NobetGrupId"] = eczaneNobetViewModel.NobetGrupId;

//    var model = new NobetYazViewModel
//    {
//        //Yil = yilCozulen,
//        //Ay = gelecekAy,
//        RolId = eczaneNobetViewModel.RolId,
//        NobetUstGrupId = eczaneNobetViewModel.NobetUstGrupId,
//        //DegisenKisitSayisi = _nobetUstGrupKisitService.GetDegisenKisitlar(eczaneNobetViewModel.NobetUstGrupId),
//        //BaslangicGunu = 1,//new DateTime(yilCozulen, gelecekAy.Month, 1),
//        //BitisGunu = aydakiGunSayisi//new DateTime(yilCozulen, gelecekAy.Month, aydakiGunSayisi),
//        BaslangicTarihi = eczaneNobetViewModel.BaslangicTarihi,
//        BitisTarihi = eczaneNobetViewModel.BitisTarihi
//    };

//    ViewBag.KayitYokUyari = 1;

//    return View("Index", model);
//}