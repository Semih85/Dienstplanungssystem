using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WM.Northwind.Business.Abstract.Authorization;
using WM.Northwind.Business.Abstract.EczaneNobet;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;
using WM.Northwind.Entities.Concrete.Optimization.EczaneNobet;
using WM.UI.Mvc.Areas.EczaneNobet.Models;

namespace WM.UI.Mvc.Areas.EczaneNobet.Controllers
{
    [Authorize]
    [HandleError]
    public class EczaneNobetSonucController : Controller
    {
        #region ctor
        private IEczaneNobetOrtakService _eczaneNobetOrtakService;
        private IEczaneNobetSonucService _eczaneNobetSonucService;
        private ITakvimService _takvimService;
        private IEczaneNobetGrupService _eczaneNobetGrupService;
        private IEczaneGrupService _eczaneGrupService;
        private INobetUstGrupService _nobetUstGrupService;
        private INobetGrupService _nobetGrupService;
        private INobetGrupGorevTipService _nobetGrupGorevTipService;
        private INobetGorevTipService _nobetGorevTipService;
        private IUserService _userService;
        private IEczaneService _eczaneService;
        private IEczaneNobetGrupAltGrupService _eczaneNobetGrupAltGrupService;
        private IAyniGunTutulanNobetService _ayniGunTutulanNobetService;
        private IKalibrasyonService _kalibrasyonService;
        private INobetUstGrupGunGrupService _nobetUstGrupGunGrupService;
        private IEczaneNobetSonucPlanlananService _eczaneNobetSonucPlanlananService;

        public EczaneNobetSonucController(ITakvimService takvimService,
                                          IEczaneNobetGrupService eczaneNobetGrupService,
                                          IEczaneNobetSonucService eczaneNobetSonucService,
                                          INobetGrupGorevTipService nobetGrupGorevTipService,
                                          INobetGorevTipService nobetGorevTipService,
                                          IEczaneGrupService eczaneGrupService,
                                          IUserService userService,
                                          INobetUstGrupService nobetUstGrupService,
                                          INobetGrupService nobetGrupService,
                                          IEczaneService eczaneService,
                                          IEczaneNobetOrtakService eczaneNobetOrtakService,
                                          IEczaneNobetGrupAltGrupService eczaneNobetGrupAltGrupService,
                                          IAyniGunTutulanNobetService ayniGunTutulanNobetService,
                                          IKalibrasyonService kalibrasyonService,
                                          INobetUstGrupGunGrupService nobetUstGrupGunGrupService,
                                          IEczaneNobetSonucPlanlananService eczaneNobetSonucPlanlananService
                                          )
        {
            _takvimService = takvimService;
            _eczaneNobetGrupService = eczaneNobetGrupService;
            _eczaneNobetSonucService = eczaneNobetSonucService;
            _nobetGrupGorevTipService = nobetGrupGorevTipService;
            _nobetGorevTipService = nobetGorevTipService;
            _eczaneGrupService = eczaneGrupService;
            _nobetUstGrupService = nobetUstGrupService;
            _nobetGrupService = nobetGrupService;
            _userService = userService;
            _eczaneGrupService = eczaneGrupService;
            _eczaneService = eczaneService;
            _eczaneNobetOrtakService = eczaneNobetOrtakService;
            _eczaneNobetGrupAltGrupService = eczaneNobetGrupAltGrupService;
            _ayniGunTutulanNobetService = ayniGunTutulanNobetService;
            _kalibrasyonService = kalibrasyonService;
            _nobetUstGrupGunGrupService = nobetUstGrupGunGrupService;
            _eczaneNobetSonucPlanlananService = eczaneNobetSonucPlanlananService;
        }
        #endregion

        // GET: EczaneNobet/EczaneNobetSonuc
        public ActionResult PivotSonuclar()
        {
            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);

            var nobetUstGrup = nobetUstGruplar.FirstOrDefault();
            var nobetGruplar = _nobetGrupService.GetDetaylarByNobetUstGrupIdList(nobetUstGruplar.Select(x => x.Id).ToList())
                .Select(s => new MyDrop { Id = s.Id, Value = s.Adi });

            //var sonuclar = _eczaneNobetSonucService.GetSonuclar2(nobetUstGrup.Id)
            //    .Where(w => w.Tarih >= nobetUstGrup.BaslangicTarihi).ToList();

            var yillar = new int[] { 2018, 2019 }; //sonuclar.Select(s => s.Yil).Distinct().OrderBy(o => o).ToList();

            var buYil = DateTime.Now.Year;
            var buAy = DateTime.Now.Month;

            var yilBaslangic = 2018;
            //sonuclar.Where(w => w.Yil == buYil)
            //.Select(s => s.Yil).Distinct()
            //.OrderBy(o => o).FirstOrDefault();

            var yilBitis = yillar.Where(s => s == buYil).SingleOrDefault();
            var nobetGrup = 0;

            var ayBaslangic = new DateTime(buYil, buAy, 1);

            if (TempData["CozulenYil"] != null)
            {
                yilBitis = (int)TempData["CozulenYil"];
                //sonuclar = sonuclar.Where(w => w.Yil == yilBitis).ToList();
            }
            else
            {
                //sonuclar = sonuclar.Where(w => w.Tarih >= ayBaslangic).ToList();
            }

            ViewBag.yilBaslangic = yilBaslangic;
            ViewBag.yilBitis = yilBitis;
            ViewBag.SonuclarGet = 1;
            ViewBag.nobetGrup = nobetGrup;

            //ViewBag.YilBaslangiclar = new SelectList(yillar, null, null, yilBaslangic);
            //ViewBag.YilBitisler = new SelectList(yillar, null, null, yilBitis);
            ViewBag.NobetGruplar = new SelectList(nobetGruplar, "Id", "Value");

            //ViewBag.ToplamUzunluk = sonuclar.Count;
            //_eczaneNobetOrtakService.EczaneNobetIstatistikGunFarkiHesapla(sonuclar);
            // _eczaneNobetOrtakService.EczaneNobetIstatistikGunFarkiFrekans(gunFarklari);

            var model = new EczaneNobetSonuclarViewModel
            {
                PivotSonuclar = new List<EczaneNobetSonucListe2>() { new EczaneNobetSonucListe2 { Id = 0 } },// sonuclar,
                GunFarklariTumSonuclar = new List<EczaneNobetIstatistikGunFarki>(),
                GunFarklariFrekanslar = new List<EczaneNobetIstatistikGunFarkiFrekans>(),
                AltGrupAyniGunNobetTutanEczaneler = new List<AyniGunNobetTutanEczane>(),
                AyniGunNobetTutanEczaneler = new List<AyniGunNobetTutanEczane>(),
                EczaneNobetAlacakVerecek = new List<EczaneNobetAlacakVerecek>(),
                EsGrubaAyniGunYazilanNobetler = new List<EsGrubaAyniGunYazilanNobetler>(),
                EczaneNobetSonuclar = new EczaneNobetSonucModel(),
                GunDagilimiMaxMin = new List<NobetGrupGunDagilim>(),
                NobetUstGrupId = nobetUstGrup.Id
            };
            return View(model);
        }

        //public JsonResult GetSonuclar()
        //{
        //    var user = _userService.GetByUserName(User.Identity.Name);
        //    var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);

        //    var nobetUstGrup = nobetUstGruplar.FirstOrDefault();
        //    var nobetGruplar = _nobetGrupService.GetList()
        //        .Where(w => nobetUstGruplar.Select(x => x.Id).Contains(w.NobetUstGrupId))
        //        .Select(s => new MyDrop { Id = s.Id, Value = s.Adi });

        //    var sonuclar = _eczaneNobetSonucService.GetSonuclar2(nobetUstGrup.Id)
        //        .Where(w => w.Tarih >= nobetUstGrup.BaslangicTarihi)
        //        .Select(s => new EczaneNobetSonucDagilimlar
        //        {
        //            Yıl_Ay = String.Format("{0:yy MM MMM.}", s.Tarih),
        //            Gün = String.Format("{0:dd}", s.Tarih),
        //            Eczane = s.EczaneAdi,
        //            GünTanım = s.GunTanim,
        //            GünGrup = s.GunGrup,
        //            NöbetGrubu = s.NobetGrupAdi,
        //            Tarih = String.Format("{0:yyyy MM dd, ddd}", s.Tarih),
        //            MazereteNöbet = s.Mazeret
        //        }).ToList();

        //    var model = new EczaneNobetSonucViewJsonModel
        //    {
        //        PivotSonuclar = sonuclar,
        //        GunFarklariTumSonuclar = new List<EczaneNobetIstatistikGunFarki>(),
        //        GunFarklariFrekanslar = new List<EczaneNobetIstatistikGunFarkiFrekans>(),
        //        AltGrupAyniGunNobetTutanEczaneler = new List<AyniGunNobetTutanEczane>(),
        //        EczaneNobetAlacakVerecek = new List<EczaneNobetAlacakVerecek>(),
        //        EsGrubaAyniGunYazilanNobetler = new List<EsGrubaAyniGunYazilanNobetler>()
        //    };

        //    var jsonResult = Json(model, JsonRequestBehavior.AllowGet);
        //    jsonResult.MaxJsonLength = int.MaxValue;

        //    return jsonResult;
        //}

        [HttpPost]
        [HandleError]
        [ValidateAntiForgeryToken]
        public ActionResult PivotSonuclar(int nobetGrup = 0, int yilBaslangic = 2018, int yilBitis = 2020)
        {
            return PivotCozum(nobetGrup, yilBaslangic, yilBitis);
        }

        public ActionResult PivotCozum(int nobetGrup = 0, int yilBaslangic = 2018, int yilBitis = 2020)
        {
            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            var nobetUstGrup = nobetUstGruplar.FirstOrDefault();

            var nobetGruplarTumu = _nobetGrupService.GetList()
                .Where(w => nobetUstGruplar.Select(x => x.Id).Contains(w.NobetUstGrupId))
                .Select(s => new MyDrop { Id = s.Id, Value = s.Adi });

            var nobetGruplar = nobetGruplarTumu
                .Where(w => w.Id == nobetGrup || nobetGrup == 0).ToList();

            var nobetGrupIdListe = nobetGruplar.Select(s => s.Id).ToList();

            //var pivotSekiller = _eczaneNobetOrtakService.GetPivotSekiller();

            var eczaneNobetSonuclarTumu = _eczaneNobetSonucService.GetSonuclar(nobetUstGrup.Id);

            var sonuclar = eczaneNobetSonuclarTumu
                .Where(w => (w.NobetGrupId == nobetGrup || nobetGrup == 0)
                          && w.Tarih >= nobetUstGrup.BaslangicTarihi)
                //anahtar listedeki sonuçları görmek için üstteki satırı kapat
                .ToList();

            var anahtarListeTumu = eczaneNobetSonuclarTumu
                .Where(w => w.Tarih < nobetUstGrup.BaslangicTarihi).ToList();

            var eczaneNobetGruplarTumu = _eczaneNobetGrupService.GetAktifEczaneNobetGrupList(nobetGrupIdListe);

            var enSonNobetler = _eczaneNobetOrtakService.GetEczaneNobetGrupGunKuralIstatistik(eczaneNobetGruplarTumu, eczaneNobetSonuclarTumu);
            //.Where(w => (w.SonNobetTarihi.Year >= yilBaslangic && w.SonNobetTarihi.Year <= yilBitis)).ToList();

            var eczaneNobetGrupGunKuralIstatistikYatayTumu = _eczaneNobetOrtakService.GetEczaneNobetGrupGunKuralIstatistikYatay(enSonNobetler);

            var eczaneNobetAlacakVerecek = EczaneNobetAlacakVerecekHesapla(nobetUstGrup, nobetGrupIdListe, anahtarListeTumu, eczaneNobetGruplarTumu, eczaneNobetGrupGunKuralIstatistikYatayTumu);

            var altGrupluEczaneler = _eczaneNobetGrupAltGrupService.GetDetaylar(nobetUstGrup.Id)
                .Select(s => s.NobetGrupId).Distinct().ToArray();

            var ayniGunNobetTutanEczaneler = new List<AyniGunNobetTutanEczane>();

            if (nobetGrup == 0)
                ayniGunNobetTutanEczaneler = _eczaneNobetOrtakService.GetAyniGunNobetTutanEczaneler(sonuclar);

            var yillar = sonuclar
                .Select(s => s.Yil).Distinct().OrderBy(o => o).ToList(); //sonuclarEski.Union(sonuclarYeni).Select(s => s.Yil).Distinct().OrderBy(o => o).ToList();

            ViewBag.yilBaslangic = yilBaslangic;
            ViewBag.yilBitis = yilBitis;
            ViewBag.nobetGrup = nobetGrup;
            ViewBag.SonuclarPost = 1;
            ViewBag.YilBaslangiclar = new SelectList(yillar, null, null, yilBaslangic);
            ViewBag.YilBitisler = new SelectList(yillar, null, null, yilBitis);
            ViewBag.NobetGruplar = new SelectList(nobetGruplarTumu, "Id", "Value", nobetGrup);

            ViewBag.ToplamUzunluk = sonuclar.Count;

            var gunFarklari = _eczaneNobetOrtakService.EczaneNobetIstatistikGunFarkiHesapla(sonuclar);

            var gunFarkiFrekanslar = _eczaneNobetOrtakService.EczaneNobetIstatistikGunFarkiFrekans(gunFarklari);

            var esGrubaAyniGunYazilanNobetler = new List<EsGrubaAyniGunYazilanNobetler>();

            if (nobetGrup == 0)
                esGrubaAyniGunYazilanNobetler = _eczaneNobetOrtakService.GetEsGrubaAyniGunYazilanNobetler(sonuclar);

            var model = new EczaneNobetSonuclarViewModel
            {
                PivotSonuclar = sonuclar,
                GunFarklariTumSonuclar = gunFarklari,
                GunFarklariFrekanslar = gunFarkiFrekanslar,
                EsGrubaAyniGunYazilanNobetler = esGrubaAyniGunYazilanNobetler,
                AltGrupAyniGunNobetTutanEczaneler = ayniGunNobetTutanEczaneler,//new List<AltGrupIleAyniGunNobetTutanEczane>()//altGrupAyniGunNobetTutanEczaneler
                EczaneNobetAlacakVerecek = eczaneNobetAlacakVerecek,
                EczaneNobetSonuclar = (EczaneNobetSonucModel)TempData["EczaneNobetSonuclar"] ?? new EczaneNobetSonucModel(),
            };
            return View("PivotSonuclar", model);
        }

        public JsonResult GetSonuclarTekli()
        {
            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            var nobetUstGrup = nobetUstGruplar.FirstOrDefault();

            var nobetGruplarTumu = _nobetGrupService.GetDetaylarByNobetUstGrupIdList(nobetUstGruplar.Select(x => x.Id).ToList())
                //.Where(w => .Contains(w.NobetUstGrupId))
                .Select(s => new MyDrop { Id = s.Id, Value = s.Adi }).ToList();

            var nobetGruplar = nobetGruplarTumu;

            var nobetGrupIdListe = nobetGruplar.Select(s => s.Id).ToList();

            var eczaneNobetSonuclarTumu = _eczaneNobetSonucService.GetSonuclar(nobetUstGrup.Id);

            var model = eczaneNobetSonuclarTumu
                .Where(w => w.Tarih >= nobetUstGrup.BaslangicTarihi)
                    //anahtar listedeki sonuçları görmek için üstteki satırı kapat
                    .Select(s => new EczaneNobetSonucDagilimlar
                    {
                        Yıl_Ay = s.Yıl_Ay,
                        Gun = s.GunIkiHane,
                        Eczane = s.EczaneAdi,
                        GunTanim = s.GunTanim,
                        GunGrup = s.GunGrup,
                        NobetGrubu = s.NobetGrubu,
                        Tarih = s.TarihAciklama,
                        MazereteNobet = s.Mazeret
                    }).ToList();

            var jsonResult = Json(model, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public JsonResult GetSonuclar()
        {
            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            var nobetUstGrup = nobetUstGruplar.FirstOrDefault();

            var nobetUstGrupDetay = _nobetUstGrupService.GetDetay(nobetUstGrup.Id);

            //var nobetGruplarTumu = _nobetGrupService.GetDetaylarByNobetUstGrupIdList(nobetUstGruplar.Select(x => x.Id).ToList())
            //    //.Where(w => .Contains(w.NobetUstGrupId))
            //    .Select(s => new MyDrop { Id = s.Id, Value = s.Adi }).ToList();

            var nobetGrupGorevTiplerTumu = _nobetGrupGorevTipService.GetDetaylarByNobetUstGrupIdList(nobetUstGruplar.Select(x => x.Id).ToList());

            var nobetGrupGorevTipIdList = nobetGrupGorevTiplerTumu
                .Select(s => s.Id)
                .ToList();
            //.Select(s => new MyDrop { Id = s.Id, Value = $"{s.NobetGrupAdi} {s.NobetGorevTipAdi}" }).ToList();

            //var nobetGruplar = nobetGruplarTumu;

            var nobetGrupIdListe = nobetGrupGorevTiplerTumu.Select(s => s.NobetGrupId).Distinct().ToList();

            var eczaneNobetSonuclarTumu = _eczaneNobetSonucService.GetSonuclar(nobetUstGrup.Id);
            var eczaneNobetSonuclarPlanlanan = _eczaneNobetSonucPlanlananService.GetSonuclar(nobetUstGrup.Id);

            var eczaneNobetSonuclarPlanlananSonrasi = eczaneNobetSonuclarPlanlanan
                .Where(w => w.Tarih >= w.NobetGrupGorevTipBaslamaTarihi).ToList();

            var sonuclar = new List<EczaneNobetSonucListe2>();

            //sonuclar = eczaneNobetSonuclarTumu
            //        //.Where(w => w.Tarih >= nobetUstGrup.BaslangicTarihi)
            //        //anahtar listedeki sonuçları görmek için üstteki satırı kapat
            //        .ToList();

            var eskiVeriGosterilsinMi = false;

            if (eskiVeriGosterilsinMi && nobetUstGrup.Id == 5)
            {
                sonuclar = eczaneNobetSonuclarTumu;
            }
            else
            {
                sonuclar = eczaneNobetSonuclarTumu
                    //.Where(w => w.Tarih < w.NobetGrupGorevTipBaslamaTarihi)//nobetUstGrup.BaslangicTarihi
                    .Where(w => w.Tarih >= w.NobetGrupGorevTipBaslamaTarihi)//nobetUstGrup.BaslangicTarihi
                                                                            //anahtar listedeki sonuçları görmek için üstteki satırı kapat
                    .ToList();
            }

            var eczaneNobetGruplarTumu = _eczaneNobetGrupService.GetAktifEczaneGrupListByNobetGrupGorevTipIdList(nobetGrupGorevTipIdList);

            //var planlananNobetlerBaslamaTarihindenSonra = eczaneNobetSonuclarPlanlanan.Where(w => w.Tarih >= nobetUstGrup.BaslangicTarihi).ToList();

            var enSonNobetler = _eczaneNobetOrtakService.GetEczaneNobetGrupGunKuralIstatistik(eczaneNobetGruplarTumu, eczaneNobetSonuclarTumu);
            //var enSonNobetlerPlanlanan = _eczaneNobetOrtakService.GetEczaneNobetGrupGunKuralIstatistik(eczaneNobetGruplarTumu, eczaneNobetSonuclarPlanlanan);
            //.Where(w => (w.SonNobetTarihi.Year >= yilBaslangic && w.SonNobetTarihi.Year <= yilBitis)).ToList();


            var eczaneNobetGrupGunKuralIstatistikYatayTumu = _eczaneNobetOrtakService.GetEczaneNobetGrupGunKuralIstatistikYatay(enSonNobetler);
            //var eczaneNobetGrupGunKuralIstatistikYatayTumuPlanlanan = _eczaneNobetOrtakService.GetEczaneNobetGrupGunKuralIstatistikYatay(enSonNobetlerPlanlanan);

            //var nobetUstGrupGunGruplar = _nobetUstGrupGunGrupService.GetDetaylar(nobetUstGrup.Id);

            var eczaneNobetAlacakVerecek = new List<EczaneNobetAlacakVerecek>();

            if (nobetUstGrup.Id == 2)
            {
                eczaneNobetAlacakVerecek = _takvimService.EczaneNobetAlacakVerecekHesaplaAntalya(nobetGrupGorevTiplerTumu, eczaneNobetSonuclarPlanlanan, eczaneNobetGruplarTumu, eczaneNobetGrupGunKuralIstatistikYatayTumu);
                //_eczaneNobetOrtakService.EczaneNobetAlacakVerecekHesapla(nobetUstGrupDetay, eczaneNobetGrupGunKuralIstatistikYatayTumu, eczaneNobetGrupGunKuralIstatistikYatayTumuPlanlanan, nobetUstGrupGunGruplar);
            }
            else
            {
                var anahtarListeTumu = eczaneNobetSonuclarTumu
                    .Where(w => w.Tarih < w.NobetGrupGorevTipBaslamaTarihi).ToList();
                //eczaneNobetAlacakVerecek = EczaneNobetAlacakVerecekHesapla(nobetUstGrup, nobetGrupIdListe, anahtarListeTumu, eczaneNobetGruplarTumu, eczaneNobetGrupGunKuralIstatistikYatayTumu);
                eczaneNobetAlacakVerecek = EczaneNobetAlacakVerecekHesapla(nobetGrupGorevTiplerTumu, anahtarListeTumu, eczaneNobetGruplarTumu, eczaneNobetGrupGunKuralIstatistikYatayTumu);
            }

            //var altGrupluEczaneler = _eczaneNobetGrupAltGrupService.GetDetaylar(nobetUstGrup.Id)
            //    .Select(s => s.NobetGrupId).Distinct().ToArray();

            var ayniGunNobetTutanAltGrupluEczaneler = _eczaneNobetOrtakService.GetAyniGunNobetTutanAltGrupluEczaneler(sonuclar);
            var ayniGunNobetTutanEczaneler = _eczaneNobetOrtakService.GetAyniGunNobetTutanEczaneler(sonuclar);

            ViewBag.ToplamUzunluk = sonuclar.Count;

            var gunFarklari = _eczaneNobetOrtakService.EczaneNobetIstatistikGunFarkiHesapla(sonuclar);
            var gunFarkiFrekanslar = _eczaneNobetOrtakService.EczaneNobetIstatistikGunFarkiFrekans(gunFarklari);
            var esGrubaAyniGunYazilanNobetler = _eczaneNobetOrtakService.GetEsGrubaAyniGunYazilanNobetler(sonuclar);

            var gunDagilimiMaxMin = enSonNobetler//eczaneNobetAlacakVerecek
                .GroupBy(g => new
                {
                    g.NobetGrupId,
                    g.NobetGrupAdi,
                    g.GunGrup
                })
                .Select(s => new NobetGrupGunDagilim
                {
                    NobetGrupId = s.Key.NobetGrupId,
                    NobetGrupAdi = s.Key.NobetGrupAdi,
                    GunGrup = s.Key.GunGrup,
                    //SonNobetTarihi = s.Key.SonNobetTarihi,
                    //SonNobetTarihiAciklama = s.Key.SonNobetTarihiAciklama,
                    NobetSayisiMax = s.Max(x => x.NobetSayisi),
                    NobetSayisiMin = s.Min(x => x.NobetSayisi),
                    //BorcluGunSayisiMax = s.Max(x => x.BorcluGunSayisi),
                    //BorcluGunSayisiMin = s.Min(x => x.BorcluGunSayisi)
                }).ToList();

            var model = new EczaneNobetSonucViewJsonModel
            {
                PivotSonuclar = sonuclar
                //.Where(w => w.EczaneNobetGrupBitisTarihi == null)
                    .Select(s => new EczaneNobetSonucDagilimlar
                    {
                        Yıl_Ay = s.Yıl_Ay,
                        Gun = s.GunIkiHane,
                        Eczane = s.EczaneAdi,
                        GunTanim = s.GunTanim,
                        GunGrup = s.GunGrup,
                        NobetGrubu = s.NobetGrubu,
                        GorevTipi = s.NobetGorevTipAdi,
                        Tarih = s.TarihAciklama,
                        Tarih2 = s.Tarih2,
                        MazereteNobet = s.Mazeret,
                        NobetTipi = s.NobetTipi,
                        EczaneSonucAdi = s.EczaneSonucAdi,
                        AyIkili = s.AyIkili,
                        Mevsim = s.Mevsim,
                        Yil = s.Yil,
                        Ay = s.Ay,
                        Hafta = s.Hafta,
                        NobetAltGrubu = s.NobetAltGrupAdi,
                        EczaneId = s.EczaneId,
                        EczaneNobetGrupId = s.EczaneNobetGrupId,
                        NobetGrupGorevTipId = s.NobetGrupGorevTipId,
                        NobetDurumAdi = s.NobetDurumAdi,
                        NobetDurumTipAdi = s.NobetDurumTipAdi,
                        KalibrasyonDeger = KalibrasyonDegeriToplam(nobetUstGrup.Id, s.EczaneNobetGrupId, s.GunGrupId, 7),
                        EczaneNobetGrupBitisTarihi = s.EczaneNobetGrupBitisTarihi
                    }).ToList(),
                KalibrasyonluToplamlar = KalibrasyonlaSonuclariBirlestir(nobetUstGrup.Id),
                GunFarklariTumSonuclar = gunFarklari,
                GunFarklariFrekanslar = gunFarkiFrekanslar,
                EsGrubaAyniGunYazilanNobetler = esGrubaAyniGunYazilanNobetler,
                AltGrupAyniGunNobetTutanEczaneler = ayniGunNobetTutanAltGrupluEczaneler,
                AyniGunNobetTutanEczaneler = ayniGunNobetTutanEczaneler,
                EczaneNobetAlacakVerecek = eczaneNobetAlacakVerecek,
                GunDagilimiMaxMin = gunDagilimiMaxMin,
                NobetUstGrupId = nobetUstGrup.Id,
                SonuclarPlanlananVeGercek = sonuclar.Union(eczaneNobetSonuclarPlanlananSonrasi)
            };

            double KalibrasyonDegeriToplam(int nobetUstGrupId, int eczaneNobetGrupId, int gunGrupId, int kalibrasyonTipId)
            {
                if (nobetUstGrupId == 5)
                {
                    var kalibrasyon = _kalibrasyonService.GetDetay(eczaneNobetGrupId, gunGrupId, kalibrasyonTipId);

                    return kalibrasyon == null ? 0 : kalibrasyon.Deger;
                }
                else
                {
                    return 0;
                }

            };

            List<KalibrasyonDetay> KalibrasyonlaSonuclariBirlestir(int nobetUstGrupId)
            {
                if (nobetUstGrupId == 5)
                {
                    var kalibrasyonlarTumu = _kalibrasyonService.GetDetaylar(nobetUstGrupId);

                    foreach (var kalibrasyon in kalibrasyonlarTumu)
                    {
                        kalibrasyon.Durum = "Eski";
                    }

                    var kalibrasyonlar = kalibrasyonlarTumu
                        .Where(w => w.GunGrupId != 3).ToList();

                    var kalibrasyonlarHaftaIci = kalibrasyonlarTumu
                        .Where(w => w.GunGrupId == 3 && w.KalibrasyonTipId == 7).ToList();

                    var sonuclarKalibrasyonUyumlu = sonuclar
                        .GroupBy(g => new
                        {
                            g.EczaneNobetGrupId,
                            g.EczaneAdi,
                            g.AyIkili,
                            g.GunGrupId,
                            g.GunGrup
                        })
                        .Select(s => new KalibrasyonDetay
                        {
                            EczaneNobetGrupId = s.Key.EczaneNobetGrupId,
                            EczaneAdi = s.Key.EczaneAdi,
                            KalibrasyonTipAdi = s.Key.AyIkili,
                            GunGrupId = s.Key.GunGrupId,
                            GunGrupAdi = s.Key.GunGrup,
                            Deger = s.Count(),
                            Durum = "N Yeni"
                        }).ToList();

                    var sonuclarKalibrasyonUyumluToplam = GetSonuclarAylik(sonuclar);

                    var kalibrasyonVeSonucbirlesim = (from k in kalibrasyonlar
                                                      .Where(w => w.KalibrasyonTipId < 7)
                                                      let uyumlu = sonuclarKalibrasyonUyumlu
                                                          .Where(w => k.EczaneNobetGrupId == w.EczaneNobetGrupId
                                                                   && k.KalibrasyonTipAdi == w.KalibrasyonTipAdi
                                                                   && k.GunGrupId == w.GunGrupId).SingleOrDefault()
                                                      select new KalibrasyonDetay
                                                      {
                                                          EczaneNobetGrupId = k.EczaneNobetGrupId,
                                                          EczaneAdi = k.EczaneAdi,
                                                          KalibrasyonTipAdi = k.KalibrasyonTipAdi,
                                                          KalibrasyonTipId = k.KalibrasyonTipId,
                                                          GunGrupId = k.GunGrupId,
                                                          GunGrupAdi = k.GunGrupAdi,
                                                          Deger = k.Deger + (uyumlu == null ? 0 : uyumlu.Deger),
                                                          NobetGrupId = k.NobetGrupId,
                                                          NobetGrupAdi = k.NobetGrupAdi,
                                                          NobetGorevTipAdi = k.NobetGorevTipAdi,
                                                          Durum = "N Yeni + Eski"
                                                      }).ToList();

                    var uyumlu1 = sonuclarKalibrasyonUyumlu
                                                        .Where(w => w.EczaneNobetGrupId == 1082
                                                                 && w.GunGrupId == 4).Sum(s => s.Deger);

                    var kalibrasyonVeSonucToplam = (from k in kalibrasyonlar
                                                      .Where(w => w.KalibrasyonTipId == 7)
                                                    let uyumlu = sonuclarKalibrasyonUyumlu
                                                        .Where(w => k.EczaneNobetGrupId == w.EczaneNobetGrupId
                                                                 && k.GunGrupId == w.GunGrupId).Sum(s => s.Deger)
                                                    select new KalibrasyonDetay
                                                    {
                                                        EczaneNobetGrupId = k.EczaneNobetGrupId,
                                                        EczaneAdi = k.EczaneAdi,
                                                        KalibrasyonTipAdi = k.KalibrasyonTipAdi,
                                                        KalibrasyonTipId = k.KalibrasyonTipId,
                                                        GunGrupId = k.GunGrupId,
                                                        GunGrupAdi = k.GunGrupAdi,
                                                        Deger = k.Deger + uyumlu,
                                                        NobetGrupId = k.NobetGrupId,
                                                        NobetGrupAdi = k.NobetGrupAdi,
                                                        NobetGorevTipAdi = k.NobetGorevTipAdi,
                                                        Durum = "Toplam"
                                                    }).ToList();

                    var kalibrasyonVeSonucToplamHaftaIci = (from k in kalibrasyonlarHaftaIci
                                                            let uyumlu = sonuclarKalibrasyonUyumlu
                                                                .Where(w => k.EczaneNobetGrupId == w.EczaneNobetGrupId
                                                                         && k.GunGrupId == w.GunGrupId).Sum(s => s.Deger)
                                                            select new KalibrasyonDetay
                                                            {
                                                                EczaneNobetGrupId = k.EczaneNobetGrupId,
                                                                EczaneAdi = k.EczaneAdi,
                                                                KalibrasyonTipAdi = k.KalibrasyonTipAdi,
                                                                KalibrasyonTipId = k.KalibrasyonTipId,
                                                                GunGrupId = k.GunGrupId,
                                                                GunGrupAdi = k.GunGrupAdi,
                                                                Deger = k.Deger + uyumlu,
                                                                NobetGrupId = k.NobetGrupId,
                                                                NobetGrupAdi = k.NobetGrupAdi,
                                                                NobetGorevTipAdi = k.NobetGorevTipAdi,
                                                                Durum = "Toplam"
                                                            }).ToList();

                    var kalibrasyonlarHepsi = kalibrasyonlar
                        .Union(sonuclarKalibrasyonUyumlu)
                        .Union(sonuclarKalibrasyonUyumluToplam)
                        .Union(kalibrasyonVeSonucbirlesim)
                        .Union(kalibrasyonVeSonucToplam)
                        .Union(kalibrasyonlarHaftaIci)
                        .Union(kalibrasyonVeSonucToplamHaftaIci)
                        .ToList();

                    return kalibrasyonlarHepsi;
                }
                else
                {
                    return new List<KalibrasyonDetay>();
                }
            }

            var jsonResult = Json(model, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        private static List<KalibrasyonDetay> GetSonuclarAylik(List<EczaneNobetSonucListe2> sonuclar)
        {
            return sonuclar
                    .GroupBy(g => new
                    {
                        g.EczaneNobetGrupId,
                        g.EczaneAdi,
                        g.GunGrupId,
                        g.GunGrup
                    })
                    .Select(s => new KalibrasyonDetay
                    {
                        EczaneNobetGrupId = s.Key.EczaneNobetGrupId,
                        EczaneAdi = s.Key.EczaneAdi,
                        KalibrasyonTipAdi = "Toplam",
                        GunGrupId = s.Key.GunGrupId,
                        GunGrupAdi = s.Key.GunGrup,
                        Deger = s.Count(),
                        Durum = "N Yeni"
                    }).ToList();
        }

        private List<EczaneNobetAlacakVerecek> EczaneNobetAlacakVerecekHesapla(NobetUstGrup nobetUstGrup,
            List<int> nobetGrupIdListe,
            List<EczaneNobetSonucListe2> anahtarListeTumu,
            List<EczaneNobetGrupDetay> eczaneNobetGruplarTumu,
            List<EczaneNobetGrupGunKuralIstatistikYatay> eczaneNobetGrupGunKuralIstatistikYatayTumu)
        {
            var anahtarListeTumEczanelerHepsi = new List<AnahtarListe>();

            var gunGruplar = anahtarListeTumu
                //.Where(w => w.GunGrup != "Bayram")
                .Select(s => s.GunGrup)
                .Distinct().ToList();

            foreach (var gunGrup in gunGruplar)
            {
                //if (gunGrup == "Cumartesi")
                //    continue;

                var anahtarListeGunGrup = anahtarListeTumu
                  .Where(w => w.GunGrup == gunGrup).ToList();

                var anahtarListeTumEczaneler = _takvimService.AnahtarListeyiBuGuneTasi(nobetGrupIdListe, 1, nobetUstGrup.BaslangicTarihi, eczaneNobetGruplarTumu, eczaneNobetGrupGunKuralIstatistikYatayTumu, anahtarListeGunGrup, gunGrup);

                anahtarListeTumEczanelerHepsi.AddRange(anahtarListeTumEczaneler);
            }

            var eczaneNobetAlacakVerecek = (from s in eczaneNobetGrupGunKuralIstatistikYatayTumu
                                            from b in anahtarListeTumEczanelerHepsi
                                            where s.EczaneNobetGrupId == b.EczaneNobetGrupId
                                            //&& b.GunGrup == gunGrup
                                            && (b.GunGrup == "Pazar"
                                                ? s.NobetSayisiPazar == b.NobetSayisi
                                                : b.GunGrup == "Arife"
                                                ? s.NobetSayisiArife == b.NobetSayisi
                                                : b.GunGrup == "Bayram"
                                                ? s.NobetSayisiBayram == b.NobetSayisi
                                                : b.GunGrup == "Cumartesi"
                                                ? s.NobetSayisiCumartesi == b.NobetSayisi
                                                : s.NobetSayisiHaftaIci == b.NobetSayisi
                                                )
                                            select new EczaneNobetAlacakVerecek
                                            {
                                                EczaneNobetGrupId = s.EczaneNobetGrupId,
                                                EczaneId = s.EczaneId,
                                                EczaneAdi = s.EczaneAdi,
                                                NobetGrupAdi = s.NobetGrupAdi,
                                                NobetGrupId = s.NobetGrupId,
                                                NobetSayisi = b.GunGrup == "Pazar"
                                                    ? s.NobetSayisiPazar
                                                    : b.GunGrup == "Arife"
                                                    ? s.NobetSayisiArife
                                                    : b.GunGrup == "Bayram"
                                                    ? s.NobetSayisiBayram
                                                    : b.GunGrup == "Cumartesi"
                                                    ? s.NobetSayisiCumartesi
                                                    : s.NobetSayisiHaftaIci,
                                                SonNobetTarihi = b.GunGrup == "Pazar"
                                                    ? s.SonNobetTarihiPazar
                                                    : b.GunGrup == "Arife"
                                                    ? s.SonNobetTarihiArife
                                                    : b.GunGrup == "Bayram"
                                                    ? s.SonNobetTarihiBayram
                                                    : b.GunGrup == "Cumartesi"
                                                    ? s.SonNobetTarihiCumartesi
                                                    : s.SonNobetTarihiHaftaIci,
                                                AnahtarTarih = b.Tarih,
                                                BorcluGunSayisi = b.GunGrup == "Pazar"
                                                 ? (int)(s.NobetSayisiPazar > 0
                                                        ? (s.SonNobetTarihiPazar - b.Tarih).TotalDays
                                                        : (s.SonNobetTarihiPazar - b.Tarih).TotalDays - (s.SonNobetTarihiPazar - b.NobetUstGrupBaslamaTarihi).TotalDays)
                                                 : b.GunGrup == "Arife"
                                                 ? (int)(s.NobetSayisiArife > 0
                                                        ? (s.SonNobetTarihiArife - b.Tarih).TotalDays
                                                        : (s.SonNobetTarihiArife - b.Tarih).TotalDays - (s.SonNobetTarihiArife - b.NobetUstGrupBaslamaTarihi).TotalDays)
                                                 : b.GunGrup == "Bayram"
                                                 ? (int)(s.NobetSayisiBayram > 0
                                                        ? (s.SonNobetTarihiBayram - b.Tarih).TotalDays
                                                        : (s.SonNobetTarihiBayram - b.Tarih).TotalDays - (s.SonNobetTarihiBayram - b.NobetUstGrupBaslamaTarihi).TotalDays)
                                                 : b.GunGrup == "Cumartesi"
                                                 ? (int)(s.NobetSayisiCumartesi > 0
                                                        ? (s.SonNobetTarihiCumartesi - b.Tarih).TotalDays
                                                        : (s.SonNobetTarihiCumartesi - b.Tarih).TotalDays - (s.SonNobetTarihiCumartesi - b.NobetUstGrupBaslamaTarihi).TotalDays)
                                                 : (int)(s.NobetSayisiHaftaIci > 0
                                                        ? (s.SonNobetTarihiHaftaIci - b.Tarih).TotalDays
                                                        : (s.SonNobetTarihiHaftaIci - b.Tarih).TotalDays - (s.SonNobetTarihiHaftaIci - b.NobetUstGrupBaslamaTarihi).TotalDays),
                                                GunGrup = b.GunGrup,
                                                //Nobets = b.NobetSayisi,
                                                AnahtarSıra = b.Id
                                            }).ToList();

            return eczaneNobetAlacakVerecek;
        }

        private List<EczaneNobetAlacakVerecek> EczaneNobetAlacakVerecekHesapla(
            List<NobetGrupGorevTipDetay> nobetGrupGorevTipler,
            List<EczaneNobetSonucListe2> anahtarListeTumu,
            List<EczaneNobetGrupDetay> eczaneNobetGruplarTumu,
            List<EczaneNobetGrupGunKuralIstatistikYatay> eczaneNobetGrupGunKuralIstatistikYatayTumu)
        {
            var anahtarListeTumEczanelerHepsi = new List<AnahtarListe>();

            var gunGruplar = anahtarListeTumu
                //.Where(w => w.GunGrup != "Bayram")
                .Select(s => s.GunGrup)
                .Distinct().ToList();

            foreach (var gunGrup in gunGruplar)
            {
                //if (gunGrup == "Cumartesi")
                //    continue;

                var anahtarListeGunGrup = anahtarListeTumu
                  .Where(w => w.GunGrup == gunGrup).ToList();

                var anahtarListeTumEczaneler = _takvimService.AnahtarListeyiBuGuneTasi(nobetGrupGorevTipler, eczaneNobetGruplarTumu, eczaneNobetGrupGunKuralIstatistikYatayTumu, anahtarListeGunGrup, gunGrup)
                    .OrderBy(o => o.Tarih).ToList();

                anahtarListeTumEczanelerHepsi.AddRange(anahtarListeTumEczaneler);
            }

            var eczaneNobetAlacakVerecekler = (from s in eczaneNobetGrupGunKuralIstatistikYatayTumu
                                               from b in anahtarListeTumEczanelerHepsi
                                               where s.EczaneNobetGrupId == b.EczaneNobetGrupId
                                               //&& b.GunGrup == gunGrup
                                               && (b.GunGrup == "Pazar"
                                                   ? s.NobetSayisiPazar == b.NobetSayisi
                                                   : b.GunGrup == "Arife"
                                                   ? s.NobetSayisiArife == b.NobetSayisi
                                                   : b.GunGrup == "Bayram"
                                                   ? s.NobetSayisiBayram == b.NobetSayisi
                                                   : b.GunGrup == "Cumartesi"
                                                   ? s.NobetSayisiCumartesi == b.NobetSayisi
                                                   : s.NobetSayisiHaftaIci == b.NobetSayisi
                                                   )
                                               //let anahtarListe = anahtarListeTumEczanelerHepsi.Where(w => w.EczaneNobetGrupId == s.EczaneNobetGrupId).ToList()
                                               select new EczaneNobetAlacakVerecek
                                               {
                                                   EczaneNobetGrupId = s.EczaneNobetGrupId,
                                                   EczaneId = s.EczaneId,
                                                   EczaneAdi = s.EczaneAdi,
                                                   NobetGrupAdi = s.NobetGrupAdi,
                                                   NobetGrupId = s.NobetGrupId,
                                                   NobetSayisi = b.GunGrup == "Pazar"
                                                       ? s.NobetSayisiPazar
                                                       : b.GunGrup == "Arife"
                                                       ? s.NobetSayisiArife
                                                       : b.GunGrup == "Bayram"
                                                       ? s.NobetSayisiBayram
                                                       : b.GunGrup == "Cumartesi"
                                                       ? s.NobetSayisiCumartesi
                                                       : s.NobetSayisiHaftaIci,
                                                   SonNobetTarihi = b.GunGrup == "Pazar"
                                                       ? s.SonNobetTarihiPazar
                                                       : b.GunGrup == "Arife"
                                                       ? s.SonNobetTarihiArife
                                                       : b.GunGrup == "Bayram"
                                                       ? s.SonNobetTarihiBayram
                                                       : b.GunGrup == "Cumartesi"
                                                       ? s.SonNobetTarihiCumartesi
                                                       : s.SonNobetTarihiHaftaIci,
                                                   AnahtarTarih = b.Tarih,
                                                   BorcluGunSayisi = b.GunGrup == "Pazar"
                                                    ? (int)(s.NobetSayisiPazar > 0
                                                           ? (s.SonNobetTarihiPazar - b.Tarih).TotalDays
                                                           : (s.SonNobetTarihiPazar - b.Tarih).TotalDays - (s.SonNobetTarihiPazar - b.NobetUstGrupBaslamaTarihi).TotalDays)
                                                    : b.GunGrup == "Arife"
                                                    ? (int)(s.NobetSayisiArife > 0
                                                           ? (s.SonNobetTarihiArife - b.Tarih).TotalDays
                                                           : (s.SonNobetTarihiArife - b.Tarih).TotalDays - (s.SonNobetTarihiArife - b.NobetUstGrupBaslamaTarihi).TotalDays)
                                                    : b.GunGrup == "Bayram"
                                                    ? (int)(s.NobetSayisiBayram > 0
                                                           ? (s.SonNobetTarihiBayram - b.Tarih).TotalDays
                                                           : (s.SonNobetTarihiBayram - b.Tarih).TotalDays - (s.SonNobetTarihiBayram - b.NobetUstGrupBaslamaTarihi).TotalDays)
                                                    : b.GunGrup == "Cumartesi"
                                                    ? (int)(s.NobetSayisiCumartesi > 0
                                                           ? (s.SonNobetTarihiCumartesi - b.Tarih).TotalDays
                                                           : (s.SonNobetTarihiCumartesi - b.Tarih).TotalDays - (s.SonNobetTarihiCumartesi - b.NobetUstGrupBaslamaTarihi).TotalDays)
                                                    : (int)(s.NobetSayisiHaftaIci > 0
                                                           ? (s.SonNobetTarihiHaftaIci - b.Tarih).TotalDays
                                                           : (s.SonNobetTarihiHaftaIci - b.Tarih).TotalDays - (s.SonNobetTarihiHaftaIci - b.NobetUstGrupBaslamaTarihi).TotalDays),
                                                   GunGrup = b.GunGrup,
                                                   //Nobets = b.NobetSayisi,
                                                   AnahtarSıra = b.Id
                                               }).ToList();

            var ecz = eczaneNobetAlacakVerecekler.Where(w => w.EczaneAdi == "YURTÖZ").ToList();

            return eczaneNobetAlacakVerecekler;
        }

        public ActionResult PivotGunFarklari()
        {
            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            var nobetUstGrup = nobetUstGruplar.FirstOrDefault();

            var nobetGruplar = _nobetGrupService.GetList()
                .Where(w => nobetUstGruplar.Select(x => x.Id).Contains(w.NobetUstGrupId))
                .Select(s => new MyDrop { Id = s.Id, Value = s.Adi });

            var sonuclar = _eczaneNobetSonucService.EczaneNobetIstatistikGunFarkiHesapla(nobetUstGrup.Id)
                .Where(w => DateTime.Parse(w.Nobet1) >= nobetUstGrup.BaslangicTarihi).ToList();

            var gunGruplar = sonuclar.Select(s => s.GunGrup).Distinct();
            var gunGrup = "";

            var sekil = 1;

            var gunFarkiFrekanslar = _eczaneNobetOrtakService.EczaneNobetIstatistikGunFarkiFrekans(sonuclar);

            var nobetGrup = 0;

            ViewBag.sekil = sekil;
            ViewBag.nobetGrup = nobetGrup;
            ViewBag.gunGrup = gunGrup;

            ViewBag.NobetGruplar = new SelectList(nobetGruplar, "Id", "Value");
            ViewBag.GunGruplar = new SelectList(gunGruplar, gunGrup);

            ViewBag.ToplamUzunluk = sonuclar.Count;

            var model = new PivotGunFarklariViewModel
            {
                GunFarklariTumSonuclar = sonuclar,
                GunFarklariFrekanslar = gunFarkiFrekanslar
            };

            return View(model);
        }

        [HttpPost]
        [HandleError]
        [ValidateAntiForgeryToken]
        public ActionResult PivotGunFarklari(int nobetGrup = 0, string gunGrup = "")
        {
            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            var nobetUstGrup = nobetUstGruplar.FirstOrDefault();

            var nobetGruplar = _nobetGrupService.GetList()
                .Where(w => nobetUstGruplar.Select(x => x.Id).Contains(w.NobetUstGrupId))
                .Select(s => new MyDrop { Id = s.Id, Value = s.Adi });

            var sonuclarTumu = _eczaneNobetSonucService.EczaneNobetIstatistikGunFarkiHesapla(nobetUstGrup.Id)
                .Where(w => DateTime.Parse(w.Nobet1) >= nobetUstGrup.BaslangicTarihi).ToList();

            var sonuclar = sonuclarTumu
                .Where(w => (w.NobetGrupId == nobetGrup || nobetGrup == 0)
                         && (w.GunGrup == gunGrup || gunGrup == "")).ToList();

            var gunGruplar = sonuclarTumu.Select(s => s.GunGrup).Distinct();

            var gunFarkiFrekanslar = _eczaneNobetOrtakService.EczaneNobetIstatistikGunFarkiFrekans(sonuclar);

            ViewBag.nobetGrup = nobetGrup;
            ViewBag.gunGrup = gunGrup;

            ViewBag.NobetGruplar = new SelectList(nobetGruplar, "Id", "Value");
            ViewBag.GunGruplar = new SelectList(gunGruplar, gunGrup);

            ViewBag.ToplamUzunluk = sonuclar.Count();

            var model = new PivotGunFarklariViewModel
            {
                GunFarklariTumSonuclar = sonuclar,
                GunFarklariFrekanslar = gunFarkiFrekanslar
            };

            return View(model);
        }

        public ActionResult GelecekDonemSil()
        {
            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            var nobetUstGrup = nobetUstGruplar.FirstOrDefault();
            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrup.Id);

            var nobetGorevTipler = nobetGrupGorevTipler.Select(s => s.NobetGorevTipId).Distinct().ToList();
            var nobetGrupGorevTipBaslamaSaatleri = nobetGrupGorevTipler.Select(s => s.BaslamaTarihi).Distinct().ToList();

            var nobetGruplar = new List<MyDrop>();

            if (nobetGrupGorevTipBaslamaSaatleri.Count > 1)
            {
                nobetGruplar = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrup.Id)
                .Select(s => new MyDrop
                {
                    Id = s.Id,
                    Value = $"{s.Id}, {s.NobetGrupAdi}, {s.NobetGorevTipAdi} ({s.BaslamaTarihi.ToShortDateString()})"
                }).ToList();
            }
            else if (nobetGorevTipler.Count > 1)
            {
                nobetGruplar = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrup.Id)
                .Select(s => new MyDrop
                {
                    Id = s.Id,
                    Value = $"{s.Id}, {s.NobetGrupAdi}, {s.NobetGorevTipAdi}"
                }).ToList();
            }
            else
            {
                nobetGruplar = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrup.Id)
                .Select(s => new MyDrop
                {
                    Id = s.Id,
                    Value = $"{s.Id}, {s.NobetGrupAdi}"
                }).ToList();
            }


            var gelecekTarih = DateTime.Today.AddMonths(1);
            int gelecekYil = gelecekTarih.Year;
            int gelecekAy = gelecekTarih.Month;

            var baslangicTarihi = new DateTime(gelecekYil, gelecekAy, 1);

            if (nobetUstGrup.BaslangicTarihi > baslangicTarihi)
            {
                baslangicTarihi = nobetUstGrup.BaslangicTarihi;
            }
            //var yillar = _takvimService.GetList()
            //    .Where(w => w.Tarih >= gelecekTarih)
            //    .Select(s => s.Tarih.Year).Distinct().ToList();

            //var aylar = _takvimService.GetAylarDdl();

            //ViewBag.Yil = new SelectList(yillar, gelecekYil);
            ViewBag.NobetGrupGorevTipId = new SelectList(nobetGruplar, "Id", "Value");
            //ViewBag.Ay = new SelectList(items: aylar, dataValueField: "Id", dataTextField: "Value", selectedValue: gelecekAy);
            ViewBag.grupSayisi = nobetGruplar.Count();

            var model = new GelecekDonemSilViewModel
            {
                Yil = gelecekYil,
                Ay = gelecekAy,
                NobetGrupId = 0,
                NobetGrupAdi = "",
                BaslangicTarihi = baslangicTarihi
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandleError]
        public ActionResult GelecekDonemSil(DateTime baslangicTarihi, DateTime? bitisTarihi, int[] nobetGrupGorevTipId)
        {
            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            var nobetUstGrup = nobetUstGruplar.FirstOrDefault();
            //var silinecekTarihBaslangic = new DateTime(yil, ay, 1);
            var gelecekTarih = DateTime.Today.AddMonths(1);
            int gelecekYil = gelecekTarih.Year;
            int gelecekAy = gelecekTarih.Month;

            //var buGun = DateTime.Today;
            var kriter = new DateTime(gelecekYil, gelecekAy, 1);

            var bitisTarihi2 = new DateTime();

            if (bitisTarihi == null)
            {
                bitisTarihi2 = DateTime.Today.AddYears(10);
            }
            else
            {
                bitisTarihi2 = (DateTime)bitisTarihi;
            }

            if (baslangicTarihi < nobetUstGrup.BaslangicTarihi)
            {//geçmiş silinemez
                ViewBag.BaslangicTarihiUyari = $"Silinecek tarih ({baslangicTarihi.ToShortDateString()}) üst grup başlama tarihinden ({nobetUstGrup.BaslangicTarihi.ToShortDateString()}) küçük olamaz.";

                return RedirectToAction("SilinenNobetlerPartialView", new { silinecekKayitSayisi = -1 });
            }

            if (baslangicTarihi < kriter)
            {//giçmiş silinemez
                ViewBag.BaslangicTarihiUyari = $"Silinecek tarih ({baslangicTarihi.ToShortDateString()}) tarihinden ({kriter.ToShortDateString()}) küçük olamaz.";

                return RedirectToAction("SilinenNobetlerPartialView", new { silinecekKayitSayisi = -1 });
            }

            var silinecekNobetlerTumu = _eczaneNobetSonucService.GetDetaylarByNobetGrupGorevTipIdList(baslangicTarihi, bitisTarihi2, nobetGrupGorevTipId);

            var silinecekNobetler = silinecekNobetlerTumu
                .Where(w => w.Tarih >= w.NobetGrupGorevTipBaslamaTarihi
                        && !w.YayimlandiMi) //yayımlanan nöbetler silinemez
                .Select(s => s.Id).ToArray();

            //var silinecekNobetlerPlanlanan = _eczaneNobetSonucPlanlananService.GetDetaylar(baslangicTarihi, nobetUstGrup.Id)
            //    .Where(w => w.NobetGrupId == nobetGrupId || nobetGrupId == 0)
            //    .Select(s => s.Id).ToArray();

            var silinecekKayitSayisi = silinecekNobetler.Count();// + silinecekNobetlerPlanlanan.Count();

            if (TempData["SilinenAy"] != null)
            {
                TempData["SilinenAy"] = $"{baslangicTarihi}-{bitisTarihi}";
            }

            if (silinecekKayitSayisi > 0)
            {
                try
                {
                    _eczaneNobetSonucService.CokluSil(silinecekNobetler);

                    if (nobetUstGrup.Id == 2)
                    {
                        //_eczaneNobetSonucPlanlananService.CokluSil(silinecekNobetlerPlanlanan);
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Silme işlemi başarısız...");
                }

                if (nobetUstGrup.Id == 1 || nobetUstGrup.Id == 3)
                {
                    _ayniGunTutulanNobetService.IkiliEczaneIstatistiginiSifirla(nobetUstGrup.Id);
                    var sonuclar = _eczaneNobetSonucService.GetSonuclarUstGrupBaslamaTarihindenSonra(nobetUstGrup.Id);
                    var ayniGunNobetTutanEczaneler = _eczaneNobetOrtakService.GetAyniGunNobetTutanEczaneler(sonuclar);
                    _ayniGunTutulanNobetService.AyniGunNobetTutanlariTabloyaEkle(ayniGunNobetTutanEczaneler);
                }
            }

            return RedirectToAction("SilinenNobetlerPartialView", new { silinecekKayitSayisi });
        }

        public ActionResult SilinenNobetlerPartialView(int? silinecekKayitSayisi)
        {
            ViewBag.SilinenKayitSayisi = silinecekKayitSayisi;
            ViewBag.SilinenKayitSayisiPost = silinecekKayitSayisi == 0 ? 1 : 0;

            return PartialView();
        }

        public ActionResult YayimlananNobetlerPartialView(int? silinecekKayitSayisi, bool yayimlandiMi)
        {
            ViewBag.YayimlanmaDurumu = yayimlandiMi;
            ViewBag.SilinenKayitSayisi = silinecekKayitSayisi;
            ViewBag.SilinenKayitSayisiPost = silinecekKayitSayisi == 0 ? 1 : 0;

            return PartialView();
        }


        public ActionResult NobetleriYayimla()
        {
            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            var nobetUstGrup = nobetUstGruplar.FirstOrDefault();
            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrup.Id);

            var nobetGorevTipler = nobetGrupGorevTipler.Select(s => s.NobetGorevTipId).Distinct().ToList();
            var nobetGrupGorevTipBaslamaSaatleri = nobetGrupGorevTipler.Select(s => s.BaslamaTarihi).Distinct().ToList();

            var nobetGruplar = new List<MyDrop>();

            if (nobetGrupGorevTipBaslamaSaatleri.Count > 1)
            {
                nobetGruplar = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrup.Id)
                .Select(s => new MyDrop
                {
                    Id = s.Id,
                    Value = $"{s.Id}, {s.NobetGrupAdi}, {s.NobetGorevTipAdi} ({s.BaslamaTarihi.ToShortDateString()})"
                }).ToList();
            }
            else if (nobetGorevTipler.Count > 1)
            {
                nobetGruplar = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrup.Id)
                .Select(s => new MyDrop
                {
                    Id = s.Id,
                    Value = $"{s.Id}, {s.NobetGrupAdi}, {s.NobetGorevTipAdi}"
                }).ToList();
            }
            else
            {
                nobetGruplar = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrup.Id)
                .Select(s => new MyDrop
                {
                    Id = s.Id,
                    Value = $"{s.Id}, {s.NobetGrupAdi}"
                }).ToList();
            }


            var gelecekTarih = DateTime.Today.AddMonths(1);
            int gelecekYil = gelecekTarih.Year;
            int gelecekAy = gelecekTarih.Month;

            var baslangicTarihi = new DateTime(gelecekYil, gelecekAy, 1);

            if (nobetUstGrup.BaslangicTarihi > baslangicTarihi)
            {
                baslangicTarihi = nobetUstGrup.BaslangicTarihi;
            }
            //var yillar = _takvimService.GetList()
            //    .Where(w => w.Tarih >= gelecekTarih)
            //    .Select(s => s.Tarih.Year).Distinct().ToList();

            //var aylar = _takvimService.GetAylarDdl();

            //ViewBag.Yil = new SelectList(yillar, gelecekYil);
            ViewBag.NobetGrupGorevTipId = new SelectList(nobetGruplar, "Id", "Value");
            //ViewBag.Ay = new SelectList(items: aylar, dataValueField: "Id", dataTextField: "Value", selectedValue: gelecekAy);
            ViewBag.grupSayisi = nobetGruplar.Count();

            var model = new NobetleriYayimlaViewModel
            {
                Yil = gelecekYil,
                Ay = gelecekAy,
                NobetGrupId = 0,
                NobetGrupAdi = "",
                BaslangicTarihi = baslangicTarihi
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandleError]
        public ActionResult NobetleriYayimla(DateTime baslangicTarihi, DateTime? bitisTarihi, int[] nobetGrupGorevTipId, bool yayimlandiMi)
        {
            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            var nobetUstGrup = nobetUstGruplar.FirstOrDefault();
            //var silinecekTarihBaslangic = new DateTime(yil, ay, 1);
            var gelecekTarih = DateTime.Today.AddMonths(1);
            int gelecekYil = gelecekTarih.Year;
            int gelecekAy = gelecekTarih.Month;

            //var buGun = DateTime.Today;
            var kriter = new DateTime(gelecekYil, gelecekAy, 1);

            var bitisTarihi2 = new DateTime();

            if (bitisTarihi == null)
            {
                bitisTarihi2 = DateTime.Today.AddYears(10);
            }
            else
            {
                bitisTarihi2 = (DateTime)bitisTarihi;
            }

            if (baslangicTarihi < nobetUstGrup.BaslangicTarihi)
            {//geçmiş silinemez
                ViewBag.BaslangicTarihiUyari = $"Yayımlanacak tarih ({baslangicTarihi.ToShortDateString()}) üst grup başlama tarihinden ({nobetUstGrup.BaslangicTarihi.ToShortDateString()}) küçük olamaz.";

                return RedirectToAction("YayimlananNobetlerPartialView", new { silinecekKayitSayisi = -1, yayimlandiMi });
            }

            if (baslangicTarihi < kriter)
            {//giçmiş silinemez
                ViewBag.BaslangicTarihiUyari = $"Yayımlanacak tarih ({baslangicTarihi.ToShortDateString()}) tarihinden ({kriter.ToShortDateString()}) küçük olamaz.";

                return RedirectToAction("YayimlananNobetlerPartialView", new { silinecekKayitSayisi = -1 , yayimlandiMi });
            }

            var yayimlanacakNobetlerTumu = _eczaneNobetSonucService.GetDetaylarByNobetGrupGorevTipIdList(baslangicTarihi, bitisTarihi2, nobetGrupGorevTipId);

            var yayimlanacakNobetler = yayimlanacakNobetlerTumu
                .Where(w => w.Tarih >= w.NobetGrupGorevTipBaslamaTarihi)
                .Select(s => s.Id).ToArray();

            //var silinecekNobetlerPlanlanan = _eczaneNobetSonucPlanlananService.GetDetaylar(baslangicTarihi, nobetUstGrup.Id)
            //    .Where(w => w.NobetGrupId == nobetGrupId || nobetGrupId == 0)
            //    .Select(s => s.Id).ToArray();

            var silinecekKayitSayisi = yayimlanacakNobetler.Count();// + silinecekNobetlerPlanlanan.Count();

            if (TempData["SilinenAy"] != null)
            {
                TempData["SilinenAy"] = $"{baslangicTarihi}-{bitisTarihi}";
            }

            if (silinecekKayitSayisi > 0)
            {
                try
                {
                    _eczaneNobetSonucService.CokluNobetYayimla(yayimlanacakNobetler, yayimlandiMi);
                }
                catch (Exception)
                {
                    throw new Exception("Yayımlama işlemi başarısız...");
                }
            }

            return RedirectToAction("YayimlananNobetlerPartialView", new { silinecekKayitSayisi , yayimlandiMi });
        }

        public ActionResult AylarDdlPartialView(int yil)
        {
            var user = _userService.GetByUserName(User.Identity.Name);
            var tarih = DateTime.Today.AddMonths(1);
            int buYil = tarih.Year;
            int buAy = tarih.Month;

            if (yil > buYil)
            {
                buAy = 1;
            }

            if (TempData["kesinlesenAy"] != null)
            {
                buAy = (int)TempData["kesinlesenAy"];
            }

            if (TempData["SilinenAy"] != null)
            {
                buAy = (int)TempData["SilinenAy"];
            }

            if (TempData["Ay"] != null)
            {
                buAy = (int)TempData["Ay"];
            }

            var adminUserIds = new int[3] { 5, 8, 9 };

            if (adminUserIds.Contains(user.Id))
            {
                tarih = new DateTime(2018, 1, 1);
            }

            var aylar = _takvimService.GetAylar(tarih, yil);

            ViewBag.Ay = buAy;
            ViewBag.ay = new SelectList(aylar, "Id", "Value", buAy);

            return PartialView();
        }

        //[ChildActionOnly]
        public PartialViewResult PivotPartialView()
        {
            var model = _eczaneNobetSonucService.GetSonuclar()
                .Select(s => new EczaneNobetSonucListe2
                {
                    Yil = s.Yil,
                    Ay = s.Ay,
                    EczaneAdi = s.EczaneAdi,
                    NobetGrupAdi = s.NobetGrupAdi,
                    NobetGunKuralId = s.NobetGunKuralId,
                    GunTanim = s.GunTanim,
                    Gun = s.Gun
                }).ToList();

            return PartialView("PivotPartialView", model);
        }

        public ActionResult GorselSonuclar()
        {
            var user = _userService.GetByUserName(User.Identity.Name);
            var yetkiliNobetUstGruplar = _nobetUstGrupService.GetListByUser(user).Select(r => r.Id).ToArray();
            var yetkiliEczaneler = _eczaneService.GetListByUser(user).Select(n => n.Id).ToList();

            var eczaneler = _eczaneNobetGrupService.GetDetaylarByEczaneIdList(yetkiliEczaneler)
                                .Select(s => new
                                {
                                    s.EczaneId,
                                    EczaneGrupAdi = $"{s.EczaneAdi} ({s.NobetGrupAdi})"
                                })
                                .Distinct().OrderBy(s => s.EczaneGrupAdi).ToList();

            var eczaneGruplar = _eczaneGrupService.GetDetaylar()
                .Where(w => yetkiliEczaneler.Contains(w.EczaneId)).ToList();

            var eczaneGrupNodes = _eczaneGrupService.GetNodes(yetkiliNobetUstGruplar)
                .Where(s => eczaneGruplar
                                //.Where(x => x.EczaneGrupTanimTipId == 1)
                                .Select(w => w.EczaneId).Contains(s.Id)).ToList();

            var eczaneGrupEdges = _eczaneGrupService.GetEdges(yetkiliNobetUstGruplar).ToList();

            var yillar = _takvimService.GetList().Select(s => s.Tarih.Year).Distinct().ToList();
            var aylar = _takvimService.GetAylar().ToList();

            var Frekanslar = new List<int> { 1, 2, 3, 4, 5 };

            ViewBag.eczaneId = new SelectList(eczaneler, "EczaneId", "EczaneGrupAdi");
            //ViewBag.yilBaslangic = new SelectList(yillar, "Yil");
            ViewBag.yilBitis = new SelectList(yillar, "Yil");
            //ViewBag.ayBaslangic = new SelectList(aylar, "Id", "Value");
            ViewBag.Aylar = new SelectList(aylar, "Id", "Value");
            var yil = DateTime.Today.Year;
            var ay = DateTime.Today.Month;
            var frekans = 1;
            ViewBag.ayBitis = ay;
            ViewBag.frekansDefault = frekans;
            ViewBag.Frekans = new SelectList(Frekanslar, null, null, frekans);

            int ayniGuneDenkGelenNobetSayisi = frekans;

            var eczaneNobetSonucNodes = _eczaneNobetSonucService.GetEczaneNobetSonucNodes(yillar.FirstOrDefault(), ay, eczaneler.Select(t => t.EczaneId).ToList())
                .OrderBy(o => o.Level).ToList();

            var eczaneNobetSonucEdges = _eczaneNobetSonucService.GetEczaneNobetSonucEdges(yil, ay, ayniGuneDenkGelenNobetSayisi, yetkiliNobetUstGruplar.FirstOrDefault());

            var model = new EczaneNobetGorselSonucViewModel()
            {
                EczaneNobetSonucNodes = eczaneNobetSonucNodes,
                EczaneNobetSonucEdges = eczaneNobetSonucEdges,
                EczaneGrupNodes = eczaneGrupNodes,
                EczaneGrupEdges = eczaneGrupEdges
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GorselSonuclar(int yilBitis, int ayBitis = 0, int eczaneId = 0, int frekans = 1)
        {
            var user = _userService.GetByUserName(User.Identity.Name);
            var yetkiliNobetUstGruplar = _nobetUstGrupService.GetListByUser(user).Select(r => r.Id).ToArray();
            var yetkiliEczaneler = _eczaneService.GetListByUser(user).Select(n => n.Id);

            var eczaneler = _eczaneNobetGrupService.GetDetaylar()
                                .Where(w => yetkiliEczaneler.Contains(w.EczaneId))
                                .Select(s => new
                                {
                                    s.EczaneId,
                                    EczaneGrupAdi = $"{s.EczaneAdi} ({s.NobetGrupAdi})"
                                })
                                .Distinct()
                                .OrderBy(s => s.EczaneGrupAdi).ToList();

            var eczaneGruplar = _eczaneGrupService.GetDetaylar()
                .Where(w => yetkiliEczaneler.Contains(w.EczaneId));

            var eczaneGrupNodes = _eczaneGrupService.GetNodes(yetkiliNobetUstGruplar)
                .Where(s => eczaneGruplar.Select(w => w.EczaneId).Contains(s.Id)).ToList();

            var eczaneGrupEdges = _eczaneGrupService.GetEdges(yetkiliNobetUstGruplar).ToList();

            var yillar = _takvimService.GetList().Select(s => s.Tarih.Year).Distinct().ToList();
            var aylar = _takvimService.GetAylar().ToList();
            var Frekanslar = new List<int> { 1, 2, 3, 4, 5 };
            //var y = yillar.Max();

            ViewBag.eczaneId = new SelectList(eczaneler, "EczaneId", "EczaneGrupAdi");
            //ViewBag.yilBaslangic = new SelectList(yillar, "Yil");
            ViewBag.yilBitis = new SelectList(yillar, "Yil");
            //ViewBag.ayBaslangic = new SelectList(aylar, "Id", "Value");
            ViewBag.Aylar = new SelectList(aylar, "Id", "Value");
            ViewBag.Frekans = new SelectList(Frekanslar, null, null, frekans);

            ViewBag.ayBitis = ayBitis;
            ViewBag.frekansDefault = frekans;

            int ayniGuneDenkGelenNobetSayisi = frekans;

            var eczaneNobetSonucNodes = _eczaneNobetSonucService.GetEczaneNobetSonucNodes(yilBitis, ayBitis, eczaneler.Select(t => t.EczaneId).ToList())
                .OrderBy(o => o.Level).ToList();

            var eczaneNobetSonucEdges = _eczaneNobetSonucService.GetEczaneNobetSonucEdges(yilBitis, ayBitis, ayniGuneDenkGelenNobetSayisi, yetkiliNobetUstGruplar.FirstOrDefault());

            if (eczaneId == 0 && ayBitis == 0)
            {
                ayBitis = 12;
                var model = new EczaneNobetGorselSonucViewModel()
                {
                    EczaneNobetSonucNodes = eczaneNobetSonucNodes,
                    EczaneNobetSonucEdges = eczaneNobetSonucEdges,
                    EczaneGrupNodes = eczaneGrupNodes,
                    EczaneGrupEdges = _eczaneGrupService.GetEdges()
                };
                return View(model);
            }
            else if (ayBitis == 0)
            {
                ayBitis = 12;
                var model = new EczaneNobetGorselSonucViewModel()
                {
                    EczaneNobetSonucNodes = eczaneNobetSonucNodes,
                    EczaneNobetSonucEdges = _eczaneNobetSonucService.GetEczaneNobetSonucEdges(yilBitis, ayBitis, ayniGuneDenkGelenNobetSayisi, yetkiliNobetUstGruplar.FirstOrDefault())
                          .Where(d => d.From == eczaneId || d.To == eczaneId).ToList(),
                    EczaneGrupNodes = eczaneGrupNodes,
                    EczaneGrupEdges = eczaneGrupEdges
                         .Where(x => x.From == eczaneId || x.To == eczaneId).ToList()
                };
                return View(model);
            }
            else if (eczaneId == 0)
            {
                var model = new EczaneNobetGorselSonucViewModel()
                {
                    EczaneNobetSonucNodes = eczaneNobetSonucNodes,
                    EczaneNobetSonucEdges = eczaneNobetSonucEdges,
                    EczaneGrupNodes = eczaneGrupNodes,
                    EczaneGrupEdges = eczaneGrupEdges

                };
                return View(model);
            }
            else
            {
                var model = new EczaneNobetGorselSonucViewModel()
                {
                    EczaneNobetSonucNodes = eczaneNobetSonucNodes,
                    EczaneNobetSonucEdges = _eczaneNobetSonucService.GetEczaneNobetSonucEdges(yilBitis, ayBitis, ayniGuneDenkGelenNobetSayisi, yetkiliNobetUstGruplar.FirstOrDefault())
                           .Where(d => d.From == eczaneId || d.To == eczaneId).ToList(),
                    EczaneGrupNodes = eczaneGrupNodes,
                    EczaneGrupEdges = eczaneGrupEdges
                           .Where(x => x.From == eczaneId || x.To == eczaneId).ToList()

                };
                return View(model);
            }
        }

        // GET: EczaneNobet/EczaneNobetSonuc/Details/5
        public ActionResult Details(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EczaneNobetSonuc eczaneNobetSonuc = _eczaneNobetSonucService.GetById(id);
            if (eczaneNobetSonuc == null)
            {
                return HttpNotFound();
            }
            return View(eczaneNobetSonuc);
        }

        // GET: EczaneNobet/EczaneNobetSonuc/Create
        public ActionResult Create()
        {
            var user = _userService.GetByUserName(User.Identity.Name);
            var eczaneler = _eczaneService.GetListByUser(user);

            var eczaneNobetGruplar = _eczaneNobetGrupService.GetDetaylar()
                .Where(w => eczaneler.Select(s => s.Id).Contains(w.EczaneId))
                .OrderBy(s => s.EczaneAdi).ThenBy(t => t.NobetGrupAdi)
                .Select(s => new MyDrop { Id = s.Id, Value = $"{s.EczaneAdi}, {s.NobetGrupGorevTipAdi}" });


            var tarihler = _takvimService.GetList().Select(s => new MyDrop { Id = s.Id, Value = $"{s.Tarih.ToLongDateString()}" });

            var nobetGorevTipler = _nobetGorevTipService.GetList()
                .Select(s => new MyDrop { Id = s.Id, Value = s.Adi })
                .OrderBy(s => s.Id);

            ViewBag.TakvimId = new SelectList(tarihler, "Id", "Value");
            ViewBag.EczaneNobetGrupId = new SelectList(eczaneNobetGruplar, "Id", "Value");
            ViewBag.NobetGorevTipId = new SelectList(nobetGorevTipler, "Id", "Value");
            return View();
        }

        public ActionResult CreateGercekNobetci(int takvimId)
        {
            var user = _userService.GetByUserName(User.Identity.Name);
            var eczaneler = _eczaneService.GetListByUser(user);

            var eczaneNobetGruplar = _eczaneNobetGrupService.GetDetaylar()
                .Where(w => eczaneler.Select(s => s.Id).Contains(w.EczaneId))
                .OrderBy(s => s.EczaneAdi).ThenBy(t => t.NobetGrupAdi)
                .Select(s => new MyDrop { Id = s.Id, Value = $"{s.EczaneAdi}, {s.NobetGrupGorevTipAdi}" });


            var tarihler = _takvimService.GetList().Select(s => new MyDrop { Id = s.Id, Value = $"{s.Tarih.ToLongDateString()}" });

            var nobetGorevTipler = _nobetGorevTipService.GetList()
                .Select(s => new MyDrop { Id = s.Id, Value = s.Adi })
                .OrderBy(s => s.Id);

            ViewBag.TakvimId = new SelectList(tarihler, "Id", "Value", takvimId);
            ViewBag.EczaneNobetGrupId = new SelectList(eczaneNobetGruplar, "Id", "Value");
            ViewBag.NobetGorevTipId = new SelectList(nobetGorevTipler, "Id", "Value");
            return View("Create");
        }

        // POST: EczaneNobet/EczaneNobetSonuc/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EczaneNobetGrupId,TakvimId,NobetGorevTipId")] EczaneNobetSonuc eczaneNobetSonuc)
        {
            if (ModelState.IsValid)
            {
                _eczaneNobetSonucService.Insert(eczaneNobetSonuc);
                return RedirectToAction("PivotSonuclar");
            }
            var user = _userService.GetByUserName(User.Identity.Name);
            var eczaneler = _eczaneService.GetListByUser(user);

            var eczaneNobetGruplar = _eczaneNobetGrupService.GetDetaylar()
                .Where(w => eczaneler.Select(s => s.Id).Contains(w.EczaneId))
                .OrderBy(s => s.EczaneAdi).ThenBy(t => t.NobetGrupAdi)
                .Select(s => new MyDrop { Id = s.Id, Value = $"{s.EczaneAdi}, {s.NobetGrupGorevTipAdi}" });

            var tarihler = _takvimService.GetList().Select(s => new MyDrop { Id = s.Id, Value = $"{s.Tarih.ToLongDateString()}" });
            var nobetGorevTipler = _nobetGorevTipService.GetList()
                .Select(s => new MyDrop { Id = s.Id, Value = s.Adi })
                .OrderBy(s => s.Id);

            ViewBag.EczaneNobetGrupId = new SelectList(eczaneNobetGruplar, "Id", "Value", eczaneNobetSonuc.EczaneNobetGrupId);
            ViewBag.TakvimId = new SelectList(tarihler, "Id", "Value", eczaneNobetSonuc.TakvimId);
            ViewBag.NobetGorevTipId = new SelectList(nobetGorevTipler, "Id", "Value", eczaneNobetSonuc.NobetGorevTipId);
            return View(eczaneNobetSonuc);
        }

        public ActionResult NobetDegistir()
        {
            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            var nobetUstGrupId = nobetUstGruplar.Select(x => x.Id).FirstOrDefault();
            var nobetGruplar = _nobetGrupService.GetList()
                .Where(w => nobetUstGruplar.Select(x => x.Id).Contains(w.NobetUstGrupId))
                .Select(s => new MyDrop { Id = s.Id, Value = s.Adi });

            //ViewBag.NobetGrupId = 0;

            ViewBag.NobetGrupId = new SelectList(nobetGruplar, "Id", "Value");

            return View();
        }

        //[HttpPost]
        //[ChildActionOnly]
        public ActionResult SonuclarPartial(DateTime? nobetTarihi, int nobetGrupId = 0)
        {
            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            var nobetUstGrupId = nobetUstGruplar.Select(x => x.Id).FirstOrDefault();
            var nobetGruplar = _nobetGrupService.GetList()
                .Where(w => nobetUstGruplar.Select(x => x.Id).Contains(w.NobetUstGrupId))
                .Select(s => new MyDrop { Id = s.Id, Value = s.Adi });

            var pivotSekiller = _eczaneNobetOrtakService.GetPivotSekiller();

            var sonuclar = _eczaneNobetSonucService.GetDetaylar(nobetUstGrupId)
                .Where(w => (w.NobetGrupId == nobetGrupId || nobetGrupId == 0)
                          && w.Tarih == nobetTarihi).ToList();

            ViewBag.nobetGrup = nobetGrupId;

            ViewBag.NobetGruplar = new SelectList(nobetGruplar, "Id", "Value", nobetGrupId);

            ViewBag.ToplamUzunluk = sonuclar.Count;

            return PartialView(sonuclar);
        }

        //[ChildActionOnly]
        //[HttpPost]
        [ValidateAntiForgeryToken]
        public PartialViewResult YeniNobetcininNobetleriPartial(int eczaneNobetGrupIdYeniNobetci)
        {
            var sonuclar = _eczaneNobetSonucService.GetDetaylarUstGrupBaslamaTarihindenSonraEczaneNobetGrupId(eczaneNobetGrupIdYeniNobetci)
                .OrderByDescending(o => o.Tarih)
                .Select(s => new MyDrop { Id = s.Id, Value = $"{s.Tarih.ToLongDateString()} ({s.NobetGorevTipAdi} {s.NobetGrupAdi})" }).ToList();

            ViewBag.YeniEczaneAdi = _eczaneNobetGrupService.GetDetayById(eczaneNobetGrupIdYeniNobetci).EczaneAdi;

            ViewBag.YeniNobetcininNobetSayisi = sonuclar.Count();

            ViewBag.EczaneNobetSonucIdYeniNobetci = new SelectList(sonuclar, "Id", "Value");

            return PartialView(sonuclar);
        }

        // GET: EczaneNobet/EczaneNobetSonuc/Edit/5
        public ActionResult Edit(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EczaneNobetSonucDetay2 eczaneNobetSonucDetay = _eczaneNobetSonucService.GetDetay2ById(id);
            EczaneNobetSonuc eczaneNobetSonuc = _eczaneNobetSonucService.GetById(id);
            if (eczaneNobetSonuc == null)
            {
                return HttpNotFound();
            }
            //var user = _userService.GetByUserName(User.Identity.Name);
            //var eczaneler = _eczaneService.GetListByUser(user);

            var eczaneNobetGruplar = _eczaneNobetGrupService.GetDetaylar()
                .Where(w => w.NobetGrupId == eczaneNobetSonucDetay.NobetGrupId
                //eczaneler.Select(s => s.Id).Contains(w.EczaneId)
                )
                .OrderBy(s => s.EczaneAdi).ThenBy(t => t.NobetGrupGorevTipAdi)
                .Select(s => new MyDrop { Id = s.Id, Value = $"{s.EczaneAdi}, {s.NobetGrupGorevTipAdi}" });

            var tarihler = _takvimService.GetList().Select(s => new MyDrop { Id = s.Id, Value = $"{s.Tarih.ToLongDateString()}" });
            var nobetGorevTipler = _nobetGorevTipService.GetList()
                .Select(s => new MyDrop { Id = s.Id, Value = s.Adi })
                .OrderBy(s => s.Id);

            ViewBag.EczaneNobetGrupId = new SelectList(eczaneNobetGruplar, "Id", "Value", eczaneNobetSonuc.EczaneNobetGrupId);
            ViewBag.TakvimId = new SelectList(tarihler, "Id", "Value", eczaneNobetSonuc.TakvimId);
            ViewBag.NobetGorevTipId = new SelectList(nobetGorevTipler, "Id", "Value");
            return View(eczaneNobetSonuc);
        }

        // POST: EczaneNobet/EczaneNobetSonuc/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EczaneNobetGrupId,TakvimId,NobetGorevTipId")] EczaneNobetSonuc eczaneNobetSonuc)
        {
            if (ModelState.IsValid)
            {
                _eczaneNobetSonucService.Update(eczaneNobetSonuc);
                return RedirectToAction("PivotSonuclar");
            }
            var eczaneNobetGruplar = _eczaneNobetGrupService.GetDetaylar().Select(s => new MyDrop { Id = s.Id, Value = $"{s.EczaneAdi}, {s.NobetGrupGorevTipAdi}" });
            var tarihler = _takvimService.GetList().Select(s => new MyDrop { Id = s.Id, Value = $"{s.Tarih.ToLongDateString()}" });
            var nobetGorevTipler = _nobetGorevTipService.GetList()
              .Select(s => new MyDrop { Id = s.Id, Value = s.Adi })
              .OrderBy(s => s.Id);


            ViewBag.EczaneNobetGrupId = new SelectList(eczaneNobetGruplar, "Id", "Value", eczaneNobetSonuc.EczaneNobetGrupId);
            ViewBag.TakvimId = new SelectList(tarihler, "Id", "Value", eczaneNobetSonuc.TakvimId);
            ViewBag.NobetGorevTipId = new SelectList(nobetGorevTipler, "Id", "Value", eczaneNobetSonuc.NobetGorevTipId);
            return View(eczaneNobetSonuc);
        }

        public ActionResult UpdateSonuclarInsertDegisim(int eczaneNobetSonucId)
        {
            if (eczaneNobetSonucId < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var eczaneNobetSonuc = _eczaneNobetSonucService.GetDetay2ById(eczaneNobetSonucId);

            if (eczaneNobetSonuc == null)
            {
                return HttpNotFound();
            }

            var eczaneNobetGruplar = _eczaneNobetGrupService.GetAktifEczaneNobetGrup(eczaneNobetSonuc.NobetGrupId)
                .OrderBy(s => s.EczaneAdi)
                .ThenBy(t => t.NobetGrupAdi)
                .Select(s => new MyDrop { Id = s.Id, Value = $"{s.EczaneAdi}, {s.NobetGrupGorevTipAdi}" });

            var tarihler = _takvimService.GetList().Select(s => new MyDrop { Id = s.Id, Value = $"{s.Tarih.ToLongDateString()}" });
            var nobetGorevTipler = _nobetGorevTipService.GetList()
                .Select(s => new MyDrop { Id = s.Id, Value = s.Adi })
                .OrderBy(s => s.Id);

            ViewBag.EczaneNobetGrupIdEski = new SelectList(eczaneNobetGruplar, "Id", "Value", eczaneNobetSonuc.EczaneNobetGrupId);
            ViewBag.EczaneNobetGrupId = new SelectList(eczaneNobetGruplar.Where(w => w.Id != eczaneNobetSonuc.EczaneNobetGrupId).ToList(), "Id", "Value");
            ViewBag.TakvimId = new SelectList(tarihler, "Id", "Value", eczaneNobetSonuc.TakvimId);
            ViewBag.NobetGorevTipId = new SelectList(nobetGorevTipler, "Id", "Value");

            var eczaneNobetDegitir = new EczaneNobetDegistir();

            return View(eczaneNobetDegitir);
        }

        [HttpPost]
        [HandleError]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateSonuclarInsertDegisim([Bind(Include = "Id,EczaneNobetGrupId,EczaneNobetSonucId,EczaneNobetSonucIdYeniNobetci,KarsilikliNobetDegistir,Aciklama")] EczaneNobetDegistir eczaneNobetDegistir)
        {
            //eski nöbetçi
            var user = _userService.GetByUserName(User.Identity.Name);

            #region ilk değişim

            var eczaneNobetSonucEski = _eczaneNobetSonucService.GetById(eczaneNobetDegistir.EczaneNobetSonucId);
            var eczaneNobetGrupIdEski = eczaneNobetSonucEski.EczaneNobetGrupId;

            var eskiNobetci = _eczaneNobetGrupService.GetDetayById(eczaneNobetGrupIdEski);

            //yeni nöbetçi
            var eczaneNobetGrupIdYeni = eczaneNobetDegistir.EczaneNobetGrupId;
            var yeniNobetci = _eczaneNobetGrupService.GetDetayById(eczaneNobetGrupIdYeni);

            //eczaneNobetSonuc.EczaneNobetGrupId = eczaneNobetGrupIdYeni;

            var eczaneNobetSonuc = new EczaneNobetSonuc
            {
                Id = eczaneNobetDegistir.EczaneNobetSonucId,
                EczaneNobetGrupId = eczaneNobetGrupIdYeni,
                NobetGorevTipId = eczaneNobetSonucEski.NobetGorevTipId,
                TakvimId = eczaneNobetSonucEski.TakvimId
            };

            //eski nöbetçiyi değişim tablosuna ekle
            var eczaneNobetDegisim = new EczaneNobetDegisim
            {
                Aciklama = eczaneNobetDegistir.Aciklama,
                EczaneNobetGrupId = eczaneNobetGrupIdEski,
                EczaneNobetSonucId = eczaneNobetDegistir.EczaneNobetSonucId,
                KayitTarihi = DateTime.Now,
                UserId = user.Id
            };
            #endregion

            var nobetDegisimTarihi = _takvimService.GetById(eczaneNobetSonuc.TakvimId);

            TempData["EskiNobetci"] = eskiNobetci.EczaneAdi;
            TempData["YeniNobetci"] = yeniNobetci.EczaneAdi;
            TempData["NobetDegisimTarihi"] = nobetDegisimTarihi.Tarih;

            if (ModelState.IsValid)
            {
                if (eczaneNobetDegistir.KarsilikliNobetDegistir && eczaneNobetDegistir.EczaneNobetSonucId > 0)
                {
                    #region 2. değişim

                    var eczaneNobetSonucYeni = _eczaneNobetSonucService.GetById(eczaneNobetDegistir.EczaneNobetSonucIdYeniNobetci);

                    var eczaneNobetSonuc2 = new EczaneNobetSonuc
                    {
                        Id = eczaneNobetDegistir.EczaneNobetSonucIdYeniNobetci,
                        EczaneNobetGrupId = eczaneNobetGrupIdEski,
                        NobetGorevTipId = eczaneNobetSonucYeni.NobetGorevTipId,
                        TakvimId = eczaneNobetSonucYeni.TakvimId
                    };

                    var eczaneNobetDegisim2 = new EczaneNobetDegisim
                    {
                        Aciklama = eczaneNobetDegistir.Aciklama,
                        EczaneNobetGrupId = eczaneNobetGrupIdYeni,
                        EczaneNobetSonucId = eczaneNobetDegistir.EczaneNobetSonucIdYeniNobetci,
                        KayitTarihi = DateTime.Now,
                        UserId = user.Id
                    };

                    var nobetDegisimler = new List<NobetDegisim>
                        {
                            new NobetDegisim { EczaneNobetSonuc = eczaneNobetSonuc, EczaneNobetDegisim = eczaneNobetDegisim },
                            new NobetDegisim { EczaneNobetSonuc = eczaneNobetSonuc2, EczaneNobetDegisim = eczaneNobetDegisim2 }
                        };
                    #endregion

                    try
                    {
                        _eczaneNobetSonucService.UpdateSonuclarInsertDegisim(nobetDegisimler);
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

                        // throw ex;
                    }
                    catch (Exception ex)
                    {
                        //return PartialView("ErrorPartial");
                        throw ex;
                    }

                    var nobetDegisimTarihi2 = _takvimService.GetById(eczaneNobetSonuc2.TakvimId);

                    TempData["EskiNobetci2"] = yeniNobetci.EczaneAdi;
                    TempData["YeniNobetci2"] = eskiNobetci.EczaneAdi;
                    TempData["NobetDegisimTarihi2"] = nobetDegisimTarihi2.Tarih;
                }
                else
                {
                    try
                    {
                        _eczaneNobetSonucService.UpdateSonuclarInsertDegisim(eczaneNobetSonuc, eczaneNobetDegisim);
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
                }

                return RedirectToAction("NobetDegistir");
            }

            var eczaneNobetGruplar = _eczaneNobetGrupService.GetDetaylar().Select(s => new MyDrop { Id = s.Id, Value = $"{s.EczaneAdi}, {s.NobetGrupGorevTipAdi}" });
            var tarihler = _takvimService.GetList().Select(s => new MyDrop { Id = s.Id, Value = $"{s.Tarih.ToLongDateString()}" });

            ViewBag.EczaneNobetGrupIdEski = new SelectList(eczaneNobetGruplar, "Id", "Value", eczaneNobetSonuc.EczaneNobetGrupId);
            ViewBag.EczaneNobetGrupId = new SelectList(eczaneNobetGruplar, "Id", "Value", eczaneNobetSonuc.EczaneNobetGrupId);
            ViewBag.TakvimId = new SelectList(tarihler, "Id", "Value", eczaneNobetSonuc.TakvimId);

            return View(eczaneNobetDegisim);
        }

        // GET: EczaneNobet/EczaneNobetSonuc/Delete/5
        public ActionResult Delete(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EczaneNobetSonuc eczaneNobetSonuc = _eczaneNobetSonucService.GetById(id);
            if (eczaneNobetSonuc == null)
            {
                return HttpNotFound();
            }
            return View(eczaneNobetSonuc);
        }

        // POST: EczaneNobet/EczaneNobetSonuc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EczaneNobetSonuc eczaneNobetSonuc = _eczaneNobetSonucService.GetById(id);
            _eczaneNobetSonucService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}

/*
[HttpPost]
[ValidateAntiForgeryToken]
[HandleError]
public ActionResult GelecekDonemSilE(int yil, int ay, bool buAyVeSonrasi, int nobetGrupId = 0)
{
    var user = _userService.GetByUserName(User.Identity.Name);
    var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
    var nobetUstGrup = nobetUstGruplar.FirstOrDefault();
    var silinecekTarihBaslangic = new DateTime(yil, ay, 1);
    var buGun = DateTime.Today;
    var kriter = new DateTime(buGun.Year, buGun.Month, 1);

    if (silinecekTarihBaslangic < kriter)
    {//giçmiş silinemez

        ViewBag.BaslangicTarihiUyari = $"Silinecek tarih ({silinecekTarihBaslangic.ToShortDateString()}) üst grup başlama tarihinden ({nobetUstGrup.BaslangicTarihi.ToShortDateString()}) küçük olamaz.";

        return RedirectToAction("SilinenNobetlerPartialView", new { silinecekKayitSayisi = -1 });
    }

    var nobetGruplar = _nobetGrupService.GetList()
        .Where(w => nobetUstGruplar.Select(x => x.Id).Contains(w.NobetUstGrupId))
        .Select(s => new MyDrop { Id = s.Id, Value = s.Adi });

    var silinecekNobetler = _eczaneNobetSonucService.GetDetaylar2ByYilAyNobetGrup(yil, ay, nobetGrupId, buAyVeSonrasi)
        .Where(w => w.NobetUstGrupId == nobetUstGrup.Id)
        .Select(s => s.Id).ToArray();

    var silinecekKayitSayisi = silinecekNobetler.Count();

    if (TempData["SilinenAy"] != null)
    {
        TempData["SilinenAy"] = ay;
    }

    if (silinecekKayitSayisi > 0)
    {
        try
        {
            _eczaneNobetSonucService.CokluSil(silinecekNobetler);
        }
        catch (Exception)
        {
            throw new Exception("Silme işlemi başarısız...");
        }
    }

    return RedirectToAction("SilinenNobetlerPartialView", new { silinecekKayitSayisi });

    //var gelecekTarih = DateTime.Today.AddMonths(1);
    //int gelecekYil = gelecekTarih.Year;
    //int gelecekAy = gelecekTarih.Month;

    //var yillar = _takvimService.GetList()
    //    .Where(w => w.Tarih >= gelecekTarih)
    //    .Select(s => s.Tarih.Year).Distinct().ToList();

    //var aylar = _takvimService.GetAylarDdl();

    //ViewBag.Yil = new SelectList(yillar, yil);
    //ViewBag.NobetGrupId = new SelectList(nobetGruplar, "Id", "Value", nobetGrupId);
    //ViewBag.Ay = new SelectList(items: aylar, dataValueField: "Id", dataTextField: "Value", selectedValue: ay);

    //var nobetGrubu = "";

    //if (nobetGrupId > 0)
    //{
    //    nobetGrubu = _nobetGrupService.GetById(nobetGrupId: nobetGrupId).Adi;
    //}

    ////var model = new GelecekDonemSilViewModel
    ////{
    ////    Yil = yil,
    ////    Ay = ay,
    ////    NobetGrupId = nobetGrupId,
    ////    NobetGrupAdi = nobetGrubu
    ////};

    //return View();
}
*/
