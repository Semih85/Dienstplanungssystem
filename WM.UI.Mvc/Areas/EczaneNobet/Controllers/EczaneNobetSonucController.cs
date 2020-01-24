using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WM.Northwind.Business.Abstract.Authorization;
using WM.Northwind.Business.Abstract.EczaneNobet;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;
using WM.Northwind.Entities.Concrete.Enums;
using WM.Northwind.Entities.Concrete.Optimization.EczaneNobet;
using WM.UI.Mvc.Areas.EczaneNobet.Models;
using WM.UI.Mvc.Services;

namespace WM.UI.Mvc.Areas.EczaneNobet.Controllers
{
    [Authorize]
    [HandleError]
    public class EczaneNobetSonucController : Controller
    {
        #region ctor
        private IEczaneNobetOrtakService _eczaneNobetOrtakService;
        private IEczaneNobetSonucService _eczaneNobetSonucService;
        private IEczaneNobetSonucAktifService _eczaneNobetSonucAktifService;
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
        private INobetUstGrupSessionService _nobetUstGrupSessionService;
        private IRaporService _raporService;
        private IRaporNobetUstGrupService _raporNobetUstGrupService;
        private IRaporRolService _raporRolService;
        private INobetGrupGorevTipGunKuralService _nobetGrupGorevTipGunKuralService;
        private IEczaneNobetMazeretService _eczaneNobetMazeretService;
        private IEczaneNobetIstekService _eczaneNobetIstekService;
        private INobetGrupGorevTipTakvimOzelGunService _nobetGrupGorevTipTakvimOzelGunService;
        private INobetDurumService _nobetDurumService;
        private INobetGrupKuralService _nobetGrupKuralService;
        private IEczaneUzaklikMatrisService _eczaneUzaklikMatrisService;
        private INobetUstGrupKisitService _nobetUstGrupKisitService;

        public EczaneNobetSonucController(ITakvimService takvimService,
                                          IEczaneNobetGrupService eczaneNobetGrupService,
                                          IEczaneNobetSonucService eczaneNobetSonucService,
                                          IEczaneNobetSonucAktifService eczaneNobetSonucAktifService,
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
                                          IEczaneNobetSonucPlanlananService eczaneNobetSonucPlanlananService,
                                          INobetUstGrupSessionService nobetUstGrupSessionService,
                                          IRaporService raporService,
                                          IRaporNobetUstGrupService raporNobetUstGrupService,
                                          IRaporRolService raporRolService,
                                          INobetGrupGorevTipGunKuralService nobetGrupGorevTipGunKuralService,
                                          IEczaneNobetMazeretService eczaneNobetMazeretService,
                                          IEczaneNobetIstekService eczaneNobetIstekService,
                                          INobetGrupGorevTipTakvimOzelGunService nobetGrupGorevTipTakvimOzelGunService,
                                          INobetDurumService nobetDurumService,
                                          INobetGrupKuralService nobetGrupKuralService,
                                          IEczaneUzaklikMatrisService eczaneUzaklikMatrisService,
                                          INobetUstGrupKisitService nobetUstGrupKisitService
                                          )
        {
            _takvimService = takvimService;
            _eczaneNobetGrupService = eczaneNobetGrupService;
            _eczaneNobetSonucService = eczaneNobetSonucService;
            _eczaneNobetSonucAktifService = eczaneNobetSonucAktifService;
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
            _nobetUstGrupSessionService = nobetUstGrupSessionService;
            _raporService = raporService;
            _raporNobetUstGrupService = raporNobetUstGrupService;
            _raporRolService = raporRolService;
            _nobetGrupGorevTipGunKuralService = nobetGrupGorevTipGunKuralService;
            _eczaneNobetMazeretService = eczaneNobetMazeretService;
            _eczaneNobetIstekService = eczaneNobetIstekService;
            _nobetGrupGorevTipTakvimOzelGunService = nobetGrupGorevTipTakvimOzelGunService;
            _nobetDurumService = nobetDurumService;
            _nobetGrupKuralService = nobetGrupKuralService;
            _eczaneUzaklikMatrisService = eczaneUzaklikMatrisService;
            _nobetUstGrupKisitService = nobetUstGrupKisitService;
        }
        #endregion

        // GET: EczaneNobet/EczaneNobetSonuc
        public ActionResult PivotSonuclar()
        {
            var user = _userService.GetByUserName(User.Identity.Name);

            var rolIdler = _userService.GetUserRoles(user)
                .OrderBy(s => s.RoleId)
                .Select(u => u.RoleId).ToArray();

            var rolId = rolIdler.FirstOrDefault();

            var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);

            var nobetUstGrup = nobetUstGruplar.FirstOrDefault();

            var ustGrupSession = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var nobetUstGrupId = nobetUstGrup.Id;

            if (ustGrupSession.Id != 0)
            {
                nobetUstGrupId = ustGrupSession.Id;
            }

            var nobetGruplar = new List<MyDrop>();

            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrupId);

            var nobetGorevTipler = nobetGrupGorevTipler.Select(s => s.NobetGorevTipId).Distinct().ToList();
            //var nobetGrupGorevTipBaslamaSaatleri = nobetGrupGorevTipler.Select(s => s.BaslamaTarihi).Distinct().ToList();

            if (nobetGorevTipler.Count > 1 && nobetUstGrupId == 4)
            {
                nobetGruplar = nobetGrupGorevTipler
                .Select(s => new MyDrop
                {
                    Id = s.Id,
                    Value = $"{s.Id}, {s.NobetGrupAdi}, {s.NobetGorevTipAdi}"
                }).ToList();
            }
            else
            {
                nobetGruplar = nobetGrupGorevTipler
                .Select(s => new MyDrop
                {
                    Id = s.Id,
                    Value = $"{s.Id}, {s.NobetGrupAdi}"
                }).ToList();
            }

            var yillar = new int[] { 2018, 2019 }; //sonuclar.Select(s => s.Yil).Distinct().OrderBy(o => o).ToList();

            var buYil = DateTime.Now.Year;
            var buAy = DateTime.Now.Month;

            var yilBaslangic = 2018;

            var yilBitis = yillar.Where(s => s == buYil).SingleOrDefault();

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

            var gunGruplar = _nobetUstGrupGunGrupService.GetDetaylar(nobetUstGrupId);

            ViewBag.nobetGrupGorevTipId = new SelectList(nobetGruplar, "Id", "Value");
            ViewBag.GunGrupId = new SelectList(gunGruplar, "GunGrupId", "GunGrupAdi");

            ViewBag.NobetGrupGorevTipSayisi = nobetGrupGorevTipler.Count;

            if (nobetGrupGorevTipler.Count == 1)
            {
                ViewBag.nobetGrupGorevTipId = new SelectList(nobetGruplar, "Id", "Value", nobetGruplar.Select(s => s.Id).FirstOrDefault());
            }

            var raporNobetUstGruplar = _raporNobetUstGrupService.GetDetaylar(nobetUstGrupId);
            var raporRoller = _raporRolService.GetDetaylar(rolId);

            var raporlar = (from r1 in raporNobetUstGruplar
                            from r2 in raporRoller
                            where r1.RaporId == r2.RaporId
                            orderby r1.RaporKategoriId, r1.RaporSiraId
                            select new RaporDetay
                            {
                                Id = r1.RaporId,
                                Adi = $"R{(r1.RaporId < 10 ? string.Format("0{0}", r1.RaporId) : r1.RaporId.ToString())}. {r1.RaporAdi}",
                                RaporKategoriAdi = r1.RaporKategoriAdi,
                                RaporKategoriId = r1.RaporKategoriId
                            }).ToList();

            //var rplar = _raporService.GetDetaylar()
            //    .Select(s => new RaporDetay
            //    {
            //        Id = s.Id,
            //        Adi = $"{s.Id}. {s.Adi}",
            //        RaporKategoriAdi = s.RaporKategoriAdi,
            //        RaporKategoriId = s.RaporKategoriId
            //    }).ToList();

            ViewBag.RaporId = new SelectList(raporlar, "Id", "Adi", "RaporKategoriAdi", 1);
            ViewBag.NobetUstGrupId = nobetUstGrupId;

            var sonNobetTarihi = _eczaneNobetSonucService.GetSonNobetTarihi(nobetUstGrupId);

            var model = new EczaneNobetSonuclarViewModel
            {
                PivotSonuclar = new List<EczaneNobetSonucListe2>() { new EczaneNobetSonucListe2 { Id = 0 } },// sonuclar,
                GunFarklariTumSonuclar = new List<EczaneNobetIstatistikGunFarki>(),
                GunFarklariFrekanslar = new List<EczaneNobetIstatistikGunFarkiFrekans>(),
                AltGrupAyniGunNobetTutanEczaneler = new List<AyniGunTutulanNobetDetay>(),
                AyniGunNobetTutanEczaneler = new List<AyniGunTutulanNobetDetay>(),
                EczaneNobetAlacakVerecek = new List<EczaneNobetAlacakVerecek>(),
                EsGrubaAyniGunYazilanNobetler = new List<EsGrubaAyniGunYazilanNobetler>(),
                EczaneNobetSonuclar = new EczaneNobetSonucModel(),
                GunDagilimiMaxMin = new List<NobetGrupGunDagilim>(),
                NobetUstGrupId = nobetUstGrupId,
                Raporlar = raporlar,
                SonNobetTarihi = sonNobetTarihi,
                KapaliEczaneler = false
                //BaslangicTarihi = nobetUstGrup.BaslangicTarihi
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

            var ayniGunNobetTutanEczaneler = new List<AyniGunTutulanNobetDetay>();

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
                        NobetGunKuralAdi = s.NobetGunKuralAdi,
                        GunGrupAdi = s.GunGrupAdi,
                        NobetGrubu = s.NobetGrubu,
                        Tarih = s.TarihAciklama,
                        MazereteNobet = s.Mazeret
                    }).ToList();

            var jsonResult = Json(model, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        public JsonResult GetSonuclar(int[] nobetGrupGorevTipId, DateTime? baslangicTarihi, DateTime? bitisTarihi, int raporId, bool kapaliEczaneler, bool sanalNobetler, int gunGrupId = 0)
        {
            //var eczaneNobetSonuclarTumu = _eczaneNobetSonucService.GetSonuclar(nobetGrupGorevTipId, baslangicTarihi, bitisTarihi);
            var nobetGrupGorevTipIdList = nobetGrupGorevTipId.ToList();

            var nobetGrupGorevTiplerTumu = _nobetGrupGorevTipService.GetDetaylarByIdList(nobetGrupGorevTipIdList);

            var nobetUstGrupId = nobetGrupGorevTiplerTumu.Select(s => s.NobetUstGrupId).FirstOrDefault();

            var nobetUstGrup = _nobetUstGrupService.GetDetay(nobetUstGrupId);

            var sonuclar = new List<EczaneNobetSonucListe2>();

            var nobetGrupGorevTipGunKurallar = _nobetGrupGorevTipGunKuralService.GetDetaylarByNobetGrupGorevTipIdList(nobetGrupGorevTipId.ToList());

            var eczaneNobetSonucDetaylar = _eczaneNobetSonucService.GetDetaylarByNobetGrupGorevTipIdList(baslangicTarihi, bitisTarihi, nobetGrupGorevTipId, kapaliEczaneler, sanalNobetler);

            var nobetGrupGorevTipTakvimOzelGunler = _nobetGrupGorevTipTakvimOzelGunService.GetDetaylar(baslangicTarihi, bitisTarihi, nobetGrupGorevTipId);

            if (raporId == 26)
            {
                var mazeretler = _eczaneNobetMazeretService.GetDetaylar(baslangicTarihi, bitisTarihi, nobetGrupGorevTipId);

                var istekler = _eczaneNobetIstekService.GetDetaylar(baslangicTarihi, bitisTarihi, nobetGrupGorevTipId);

                var sonuclarMazeretli = _eczaneNobetOrtakService
                    .EczaneNobetSonucBirlesim(nobetGrupGorevTipGunKurallar,
                    eczaneNobetSonucDetaylar,
                    nobetGrupGorevTipTakvimOzelGunler,
                    mazeretler,
                    istekler,
                    EczaneNobetSonucTuru.Kesin)
                    .Where(w => w.Tarih >= w.NobetGrupGorevTipBaslamaTarihi).ToList();

                _eczaneNobetOrtakService.VirgulleAyrilanNobetGruplariniAyir(nobetUstGrupId, sonuclarMazeretli);

                var sonuclarMazeretliJson = GetSonuclar(sonuclarMazeretli, raporId);

                return ConvertToJson(sonuclarMazeretliJson);
            }

            var sonuclarTumu = _eczaneNobetOrtakService.EczaneNobetSonucBirlesim(
                nobetGrupGorevTipGunKurallar,
                eczaneNobetSonucDetaylar,
                nobetGrupGorevTipTakvimOzelGunler,
                EczaneNobetSonucTuru.Kesin)
                .Where(w => w.GunGrupId == gunGrupId || gunGrupId == 0).ToList();

            if (nobetUstGrup.BaslamaTarihindenOncekiSonuclarGosterilsinMi)
            {
                if (nobetUstGrup.Id == 5)
                {
                    sonuclar = sonuclarTumu
                        .Where(w => ((w.Tarih >= w.NobetGrupGorevTipBaslamaTarihi && w.GunGrupId != 2)
                                    || w.GunGrupId == 2)).ToList();
                }
                else
                {
                    sonuclar = sonuclarTumu;
                }
            }
            else
            {
                sonuclar = sonuclarTumu
                    //.Where(w => w.Tarih < w.NobetGrupGorevTipBaslamaTarihi)//nobetUstGrup.BaslangicTarihi
                    .Where(w => w.Tarih >= w.NobetGrupGorevTipBaslamaTarihi)//nobetUstGrup.BaslangicTarihi
                                                                            //anahtar listedeki sonuçları görmek için üstteki satırı kapat
                    .ToList();
            }

            if (raporId == 7)
            {
                var eczaneNobetGruplarTumu = _eczaneNobetGrupService.GetAktifEczaneGrupListByNobetGrupGorevTipIdList(nobetGrupGorevTipIdList);

                var enSonNobetler = _eczaneNobetOrtakService.GetEczaneNobetGrupGunKuralIstatistik(eczaneNobetGruplarTumu, sonuclarTumu);

                //var eczaneNobetGrupGunKuralIstatistikYatayTumu = _eczaneNobetOrtakService.GetEczaneNobetGrupGunKuralIstatistikYatay(enSonNobetler);

                var gunGrupBazliNobetSayilari = enSonNobetler//eczaneNobetAlacakVerecek
                .GroupBy(g => new
                {
                    g.NobetGrupId,
                    g.NobetGrupAdi,
                    g.GunGrupAdi,
                    g.NobetGrupGorevTipId,
                    g.NobetGorevTipId,
                    g.EczaneNobetGrupId
                })
                .Select(s => new NobetGrupGunDagilim
                {
                    NobetGrupId = s.Key.NobetGrupId,
                    NobetGrupAdi = s.Key.NobetGrupAdi,
                    NobetGorevTipId = s.Key.NobetGorevTipId,
                    GunGrupAdi = s.Key.GunGrupAdi,
                    NobetGrupGorevTipId = s.Key.NobetGorevTipId,
                    EczaneNobetGrupId = s.Key.EczaneNobetGrupId,
                    //SonNobetTarihi = s.Key.SonNobetTarihi,
                    //SonNobetTarihiAciklama = s.Key.SonNobetTarihiAciklama,
                    NobetSayisi = s.Sum(x => x.NobetSayisi),
                    //NobetSayisiMax = s.Count(x => x.NobetSayisi),
                    //NobetSayisiMin = s.Min(x => x.NobetSayisi),
                    //BorcluGunSayisiMax = s.Max(x => x.BorcluGunSayisi),
                    //BorcluGunSayisiMin = s.Min(x => x.BorcluGunSayisi)
                }).ToList();

                var gunDagilimiMaxMin = gunGrupBazliNobetSayilari//eczaneNobetAlacakVerecek
                .GroupBy(g => new
                {
                    g.NobetGrupId,
                    g.NobetGrupAdi,
                    g.GunGrupAdi,
                    g.NobetGrupGorevTipId,
                    g.NobetGorevTipId
                })
                .Select(s => new NobetGrupGunDagilim
                {
                    NobetGrupId = s.Key.NobetGrupId,
                    NobetGrupAdi = s.Key.NobetGrupAdi,
                    NobetGorevTipId = s.Key.NobetGorevTipId,
                    NobetGrupGorevTipId = s.Key.NobetGorevTipId,
                    GunGrupAdi = s.Key.GunGrupAdi,
                    //SonNobetTarihi = s.Key.SonNobetTarihi,
                    //SonNobetTarihiAciklama = s.Key.SonNobetTarihiAciklama,
                    //NobetSayisi = s.Sum(x => x.NobetSayisi),
                    NobetSayisiMax = s.Max(x => x.NobetSayisi),
                    NobetSayisiMin = s.Min(x => x.NobetSayisi),
                    //BorcluGunSayisiMax = s.Max(x => x.BorcluGunSayisi),
                    //BorcluGunSayisiMin = s.Min(x => x.BorcluGunSayisi)
                }).ToList();

                return ConvertToJson(gunDagilimiMaxMin);
            }
            else if (raporId >= 8 && raporId <= 13)
            {
                var gunFarklari = _eczaneNobetOrtakService.EczaneNobetIstatistikGunFarkiHesapla(sonuclar);

                var gunFarkiFrekanslar = _eczaneNobetOrtakService.EczaneNobetIstatistikGunFarkiFrekans(gunFarklari);

                var gunFarklariHepsi = new GunFarklariHepsi
                {
                    GunFarklariTumSonuclar = gunFarklari,
                    GunFarklariFrekanslar = gunFarkiFrekanslar,
                };
                return ConvertToJson(gunFarklariHepsi);
            }
            else if (raporId >= 18 && raporId <= 20)
            {
                var ayniGunNobetTutanAltGrupluEczaneler = _eczaneNobetOrtakService.GetAyniGunNobetTutanAltGrupluEczaneler(sonuclar);

                return ConvertToJson(ayniGunNobetTutanAltGrupluEczaneler);
            }
            else if (raporId == 24)
            {//kalibrasyonlu nöbet
                var kalibrasyonluToplamlar = KalibrasyonlaSonuclariBirlestir(nobetUstGrupId, sonuclar, gunGrupId);

                return ConvertToJson(kalibrasyonluToplamlar);
            }
            else if (raporId == 25)
            {//nöbet durum
                var nobetDurumlar = _nobetDurumService.GetDetaylar(nobetUstGrup.Id);
                //.Where(w => w.NobetDurumTipId != 4).ToList();

                sonuclar = _eczaneNobetSonucService.GetSonuclar(sonuclar, nobetDurumlar);

                var modelNobetDurumlar = GetSonuclar(sonuclar, raporId);

                return ConvertToJson(modelNobetDurumlar);
            }

            _eczaneNobetOrtakService.VirgulleAyrilanNobetGruplariniAyir(nobetUstGrupId, sonuclar);

            ViewBag.ToplamUzunluk = sonuclar.Count;

            var model = GetSonuclar(sonuclar, raporId);

            return ConvertToJson(model);
        }

        private JsonResult ConvertToJson(object sonuclar)
        {
            var jsonResult = Json(sonuclar, JsonRequestBehavior.AllowGet);

            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        private List<EczaneNobetSonucDagilimlar> GetSonuclar(List<EczaneNobetSonucListe2> sonuclar, int raporId)
        {
            if (raporId == 24)
            {
                return sonuclar.Select(s => new EczaneNobetSonucDagilimlar
                {
                    Yıl_Ay = s.Yıl_Ay,
                    Gun = s.GunIkiHane,
                    Eczane = s.EczaneAdi,
                    NobetGunKuralAdi = s.NobetGunKuralAdi,
                    GunGrupAdi = s.GunGrupAdi,
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
                    NobetGrupAdiGunluk = s.NobetGrupAdiGunluk,
                    EczaneId = s.EczaneId,
                    EczaneNobetGrupId = s.EczaneNobetGrupId,
                    NobetGrupGorevTipId = s.NobetGrupGorevTipId,
                    NobetDurumAdi = s.NobetDurumAdi,
                    NobetDurumTipAdi = s.NobetDurumTipAdi,
                    KalibrasyonDeger = KalibrasyonDegeriToplam(s.EczaneNobetGrupId, s.GunGrupId, 7, s.NobetUstGrupId),
                    EczaneNobetGrupBaslamaTarihi = s.EczaneNobetGrupBaslamaTarihi,
                    EczaneNobetGrupBitisTarihi = s.EczaneNobetGrupBitisTarihi,
                    AgirlikDegeri = s.AgirlikDegeri,
                    NobetOzelGunAdi = s.NobetOzelGunAdi,
                    NobetOzelGunKategoriAdi = s.NobetOzelGunKategoriAdi
                }).ToList();
            }
            else
            {
                return sonuclar.Select(s => new EczaneNobetSonucDagilimlar
                {
                    Yıl_Ay = s.Yıl_Ay,
                    Gun = s.GunIkiHane,
                    Eczane = s.EczaneAdi,
                    NobetGunKuralAdi = s.NobetGunKuralAdi,
                    GunGrupAdi = s.GunGrupAdi,
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
                    NobetGrupAdiGunluk = s.NobetGrupAdiGunluk,
                    EczaneId = s.EczaneId,
                    EczaneNobetGrupId = s.EczaneNobetGrupId,
                    NobetGrupGorevTipId = s.NobetGrupGorevTipId,
                    NobetDurumAdi = s.NobetDurumAdi,
                    NobetDurumTipAdi = s.NobetDurumTipAdi,
                    EczaneNobetGrupBaslamaTarihi = s.EczaneNobetGrupBaslamaTarihi,
                    EczaneNobetGrupBitisTarihi = s.EczaneNobetGrupBitisTarihi,
                    AgirlikDegeri = s.AgirlikDegeri,
                    NobetOzelGunAdi = s.NobetOzelGunAdi,
                    NobetOzelGunKategoriAdi = s.NobetOzelGunKategoriAdi
                }).ToList();
            }

        }

        public JsonResult GetEczaneNobetAlacakVerecekler(int[] nobetGrupGorevTipId, DateTime? baslangicTarihi, DateTime? bitisTarihi, int raporId, bool kapaliEczaneler, bool sanalNobetler, int gunGrupId = 0)
        {
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var nobetGrupGorevTipIdList = nobetGrupGorevTipId.ToList();

            var nobetGrupGorevTiplerTumu = _nobetGrupGorevTipService.GetDetaylar(nobetGrupGorevTipIdList);

            var nobetGrupGorevTipGunKurallar = _nobetGrupGorevTipGunKuralService.GetDetaylarByNobetGrupGorevTipIdList(nobetGrupGorevTipIdList);

            var eczaneNobetSonucDetaylar = _eczaneNobetSonucService.GetDetaylarByNobetGrupGorevTipIdList(baslangicTarihi, bitisTarihi, nobetGrupGorevTipId, kapaliEczaneler, sanalNobetler);

            var nobetGrupGorevTipTakvimOzelGunler = _nobetGrupGorevTipTakvimOzelGunService.GetDetaylar(baslangicTarihi, bitisTarihi, nobetGrupGorevTipId);

            var sonuclarTumu = _eczaneNobetOrtakService.EczaneNobetSonucBirlesim(nobetGrupGorevTipGunKurallar, eczaneNobetSonucDetaylar, nobetGrupGorevTipTakvimOzelGunler, EczaneNobetSonucTuru.Kesin)
                .Where(w => w.GunGrupId == gunGrupId || gunGrupId == 0).ToList();

            var eczaneNobetSonuclarPlanlanan = _eczaneNobetSonucPlanlananService.GetSonuclar(baslangicTarihi, bitisTarihi, nobetGrupGorevTipId, kapaliEczaneler)
                .Where(w => w.GunGrupId == gunGrupId || gunGrupId == 0).ToList();

            var eczaneNobetGruplarTumu = _eczaneNobetGrupService.GetDetaylarByNobetGrupGorevTipler(nobetGrupGorevTipIdList, kapaliEczaneler);//GetAktifEczaneGrupListByNobetGrupGorevTipIdList

            var eczaneNobetSonuclarPlanlananSonrasi = eczaneNobetSonuclarPlanlanan;
            //.Where(w => w.Tarih >= w.NobetGrupGorevTipBaslamaTarihi).ToList();

            NobetleriSirala(eczaneNobetSonuclarPlanlananSonrasi, eczaneNobetGruplarTumu, 0);

            var sonuclar = new List<EczaneNobetSonucListe2>();

            if (nobetUstGrup.BaslamaTarihindenOncekiSonuclarGosterilsinMi)
            {
                sonuclar = sonuclarTumu;
            }
            else
            {
                sonuclar = sonuclarTumu
                    //.Where(w => w.Tarih < w.NobetGrupGorevTipBaslamaTarihi)//nobetUstGrup.BaslangicTarihi
                    .Where(w => w.Tarih >= w.NobetGrupGorevTipBaslamaTarihi)//nobetUstGrup.BaslangicTarihi
                                                                            //anahtar listedeki sonuçları görmek için üstteki satırı kapat
                    .ToList();
            }

            NobetleriSirala(sonuclar, eczaneNobetGruplarTumu, 1);

            var enSonNobetler = _eczaneNobetOrtakService.GetEczaneNobetGrupGunKuralIstatistik(eczaneNobetGruplarTumu, sonuclarTumu);

            var eczaneNobetGrupGunKuralIstatistikYatayTumu = _eczaneNobetOrtakService.GetEczaneNobetGrupGunKuralIstatistikYatay(enSonNobetler);

            var eczaneNobetAlacakVerecek = new List<EczaneNobetAlacakVerecek>();

            if (raporId >= 14 && raporId <= 16)
            {
                if (nobetUstGrup.Id == 2)
                {
                    eczaneNobetAlacakVerecek = _takvimService.EczaneNobetAlacakVerecekHesaplaAntalya(nobetGrupGorevTiplerTumu, eczaneNobetSonuclarPlanlanan, eczaneNobetGruplarTumu, eczaneNobetGrupGunKuralIstatistikYatayTumu);
                }
                else
                {
                    var anahtarListeTumu = sonuclarTumu
                        .Where(w => w.Tarih < w.NobetGrupGorevTipBaslamaTarihi).ToList();
                    //eczaneNobetAlacakVerecek = EczaneNobetAlacakVerecekHesapla(nobetUstGrup, nobetGrupIdListe, anahtarListeTumu, eczaneNobetGruplarTumu, eczaneNobetGrupGunKuralIstatistikYatayTumu);
                    eczaneNobetAlacakVerecek = EczaneNobetAlacakVerecekHesapla(nobetGrupGorevTiplerTumu, anahtarListeTumu, eczaneNobetGruplarTumu, eczaneNobetGrupGunKuralIstatistikYatayTumu);
                }

                var eczaneNobetAlacakVerecekKontrol = eczaneNobetAlacakVerecek
                    .Where(w => w.EczaneAdi == "KUMBUL"
                             //|| w.EczaneAdi == "MÜĞREN"
                             )
                    .ToList();

                return ConvertToJson(eczaneNobetAlacakVerecek);
            }

            if (raporId == 17)
            {
                var plVsGerc = sonuclar.Union(eczaneNobetSonuclarPlanlananSonrasi);
                //.Where(w => w.NobetSayisi > 0).ToList();

                return ConvertToJson(plVsGerc);
            }

            //var model = new EczaneNobetSonucViewJsonModel
            //{
            //    EczaneNobetAlacakVerecek = eczaneNobetAlacakVerecek,
            //    SonuclarPlanlananVeGercek = sonuclar.Union(eczaneNobetSonuclarPlanlananSonrasi)
            //};

            return ConvertToJson(eczaneNobetAlacakVerecek);
        }

        private void NobetleriSirala(List<EczaneNobetSonucListe2> sonuclar, List<EczaneNobetGrupDetay> eczaneNobetGruplarTumu, int indis)
        {
            var gunGruplar = sonuclar.Select(s => new { s.GunGrupId, s.GunGrupAdi }).Distinct().ToArray();

            foreach (var gunGrup in gunGruplar)
            {
                foreach (var eczaneNobetGrup in eczaneNobetGruplarTumu)
                {
                    var sonuclarEczaneBazliSitali = sonuclar
                        .Where(w => w.EczaneNobetGrupId == eczaneNobetGrup.Id
                                 && w.GunGrupId == gunGrup.GunGrupId)
                        .OrderBy(o => o.GunGrupId)
                        .ThenBy(o => o.Tarih)
                        .ToList();

                    var sonucIndis = indis;

                    foreach (var sonuc in sonuclarEczaneBazliSitali)
                    {
                        sonuc.NobetSayisi = sonucIndis;

                        sonucIndis++;
                    }
                }
            }
        }

        public JsonResult GetAyniGunNobetler(int[] nobetGrupGorevTipId, DateTime? baslangicTarihi, DateTime? bitisTarihi, int raporId, bool kapaliEczaneler, int gunGrupId = 0)
        {
            var nobetGrupIdListe = nobetGrupGorevTipId.ToList();

            var nobetGrupGorevTipGunKurallar = _nobetGrupGorevTipGunKuralService.GetDetaylarByNobetGrupGorevTipIdList(nobetGrupIdListe);

            var eczaneNobetSonucDetaylar = _eczaneNobetSonucService.GetDetaylarByNobetGrupGorevTipIdList(baslangicTarihi, bitisTarihi, nobetGrupGorevTipId, kapaliEczaneler)
                .Where(w => w.Tarih >= w.NobetGrupGorevTipBaslamaTarihi).ToList();

            var nobetGrupGorevTipTakvimOzelGunler = _nobetGrupGorevTipTakvimOzelGunService.GetDetaylar(baslangicTarihi, bitisTarihi, nobetGrupGorevTipId);

            var sonuclarTumu = _eczaneNobetOrtakService.EczaneNobetSonucBirlesim(
                nobetGrupGorevTipGunKurallar,
                eczaneNobetSonucDetaylar,
                nobetGrupGorevTipTakvimOzelGunler,
                EczaneNobetSonucTuru.Kesin)
                .Where(w => w.GunGrupId == gunGrupId || gunGrupId == 0).ToList();

            if (raporId == 21
                || raporId == 22
                || raporId == 32
                //|| (raporId >= 18 && raporId <= 22)
                )
            {
                var ayniGunNobetTutanEczaneler = _ayniGunTutulanNobetService.GetDetaylar(nobetGrupGorevTipId);

                //var altGruplarlaTakipEdilecekNobetGrupGorevTipler = new List<AltGruplarlaTakipEdilecekNobetGrup>
                //{
                //    new AltGruplarlaTakipEdilecekNobetGrup{ GrupAdi = "Yenişehir 1-2", NobetGrupGorevTipId = 20 },
                //    new AltGruplarlaTakipEdilecekNobetGrup{ GrupAdi = "Yenişehir 1-2", NobetGrupGorevTipId = 21 },

                //    new AltGruplarlaTakipEdilecekNobetGrup{ GrupAdi = "Yenişehir 3-2", NobetGrupGorevTipId = 22 },
                //    new AltGruplarlaTakipEdilecekNobetGrup{ GrupAdi = "Yenişehir 3-2", NobetGrupGorevTipId = 21 },

                //    new AltGruplarlaTakipEdilecekNobetGrup{ GrupAdi = "Antalya 10-11", NobetGrupGorevTipId = 13 },
                //    new AltGruplarlaTakipEdilecekNobetGrup{ GrupAdi = "Antalya 10-11", NobetGrupGorevTipId = 14 },
                //};

                //var altGruplarlaTakipEdilecekNobetGrupGorevTipList = altGruplarlaTakipEdilecekNobetGrupGorevTipler
                //    .Where(w => nobetGrupGorevTipId.Contains(w.NobetGrupGorevTipId)).ToArray();

                //foreach (var grup in altGruplarlaTakipEdilecekNobetGrupGorevTipList)
                //{
                //    var l1 = ayniGunNobetTutanEczaneler
                //        .Where(w => w.NobetGrupGorevTipId1 == grup.NobetGrupGorevTipId 
                //                 || w.NobetGrupGorevTipId2 == grup.NobetGrupGorevTipId)
                //        //.Select(s => s.Grup)
                //        .ToArray();

                //    foreach (var l in l1)
                //    {
                //        l.Grup = grup.GrupAdi;
                //    }

                //    //for (int i = 0; i < l1.Count(); i++)
                //    //{
                //    //    ayniGunNobetTutanEczaneler[i].Grup = grup.GrupAdi;
                //    //}
                //}

                //var ayniGunNobetSayisiGrouped = _eczaneNobetOrtakService.AyniGunTutulanNobetSayisiniHesapla(ayniGunNobetTutanEczaneler);

                //var ayniGunNobetSayisi1denFazlaGrouped = ayniGunNobetSayisiGrouped;
                //.Where(w => w.AyniGunNobetSayisi > 1)
                //.ToList();

                //ayniGunNobetTutanEczaneler = ayniGunNobetTutanEczaneler
                //    .Where(w => ayniGunNobetSayisi1denFazlaGrouped.Select(s => s.EczaneBirlesim).Contains(w.EczaneBirlesim)).ToList();

                //var model = new AyniGunNobetDagilimModel
                //{
                //    AyniGunNobetTutanEczaneler = ayniGunNobetTutanEczaneler,//new List<AyniGunTutulanNobetDetay>(),
                //    //AyniGunNobetTutanEczanelerOzet = ayniGunNobetSayisiGrouped
                //};

                return ConvertToJson(ayniGunNobetTutanEczaneler);
            }

            if (raporId == 33)
            {
                var ayniGunNobetTutanEczaneler = _eczaneNobetOrtakService.GetAyniGunNobetTutanEczaneler(sonuclarTumu);

                return ConvertToJson(ayniGunNobetTutanEczaneler);
            }

            //ViewBag.ToplamUzunluk = sonuclar.Count;

            if (raporId == 23)
            {
                var esGrubaAyniGunYazilanNobetler = _eczaneNobetOrtakService.GetEsGrubaAyniGunYazilanNobetler(sonuclarTumu);
                return ConvertToJson(esGrubaAyniGunYazilanNobetler);
            }

            return ConvertToJson(new List<EczaneNobetSonucListe2>());
        }

        double KalibrasyonDegeriToplam(int eczaneNobetGrupId, int gunGrupId, int kalibrasyonTipId, int nobetUstGrupId)
        {
            //if (nobetUstGrupId == 5)
            //{
            var kalibrasyon = _kalibrasyonService.GetDetay(eczaneNobetGrupId, gunGrupId, kalibrasyonTipId);

            return kalibrasyon == null ? 0 : kalibrasyon.Deger;
            //}
            //else
            //{
            //    return 0;
            //}

        }

        List<KalibrasyonDetay> KalibrasyonlaSonuclariBirlestir(int nobetUstGrupId, List<EczaneNobetSonucListe2> sonuclar, int gunGrupId = 0)
        {
            //if (nobetUstGrupId == 5)
            //{
            var kalibrasyonlarTumu = _kalibrasyonService.GetDetaylar(nobetUstGrupId);
            //.Where(w => w.EczaneNobetGrupId != 1031).ToList();

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
                    g.GunGrupAdi
                })
                .Select(s => new KalibrasyonDetay
                {
                    EczaneNobetGrupId = s.Key.EczaneNobetGrupId,
                    EczaneAdi = s.Key.EczaneAdi,
                    KalibrasyonTipAdi = s.Key.AyIkili,
                    GunGrupId = s.Key.GunGrupId,
                    GunGrupAdi = s.Key.GunGrupAdi,
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

            return kalibrasyonlarHepsi.Where(w => w.GunGrupId == gunGrupId || gunGrupId == 0).ToList();
            //}
            //else
            //{
            //    return new List<KalibrasyonDetay>();
            //}
        }

        private static List<KalibrasyonDetay> GetSonuclarAylik(List<EczaneNobetSonucListe2> sonuclar)
        {
            return sonuclar
                    .GroupBy(g => new
                    {
                        g.EczaneNobetGrupId,
                        g.EczaneAdi,
                        g.GunGrupId,
                        g.GunGrupAdi
                    })
                    .Select(s => new KalibrasyonDetay
                    {
                        EczaneNobetGrupId = s.Key.EczaneNobetGrupId,
                        EczaneAdi = s.Key.EczaneAdi,
                        KalibrasyonTipAdi = "Toplam",
                        GunGrupId = s.Key.GunGrupId,
                        GunGrupAdi = s.Key.GunGrupAdi,
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
                .Select(s => s.GunGrupAdi)
                .Distinct().ToList();

            foreach (var gunGrup in gunGruplar)
            {
                //if (gunGrup == "Cumartesi")
                //    continue;

                var anahtarListeGunGrup = anahtarListeTumu
                  .Where(w => w.GunGrupAdi == gunGrup).ToList();

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
                                                GunGrupAdi = b.GunGrup,
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
                .Select(s => s.GunGrupAdi)
                .Distinct().ToList();

            foreach (var gunGrup in gunGruplar)
            {
                //if (gunGrup == "Cumartesi")
                //    continue;

                var anahtarListeGunGrup = anahtarListeTumu
                  .Where(w => w.GunGrupAdi == gunGrup).ToList();

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
                                                   GunGrupAdi = b.GunGrup,
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

            var gunGruplar = sonuclar.Select(s => s.GunGrupAdi).Distinct();
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
                         && (w.GunGrupAdi == gunGrup || gunGrup == "")).ToList();

            var gunGruplar = sonuclarTumu.Select(s => s.GunGrupAdi).Distinct();

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
            //var user = _userService.GetByUserName(User.Identity.Name);
            //var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            //var nobetUstGrup = nobetUstGruplar.FirstOrDefault();
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrup.Id);

            var nobetGorevTipler = nobetGrupGorevTipler.Select(s => s.NobetGorevTipId).Distinct().ToList();
            var nobetGrupGorevTipBaslamaSaatleri = nobetGrupGorevTipler.Select(s => s.BaslamaTarihi).Distinct().ToList();

            //var nobetGruplar = new List<MyDrop>();

            var nobetGruplar = _eczaneNobetSonucService.GetNobetGrupSonYayimNobetTarihleri(nobetUstGrup.Id);

            //if (nobetGrupGorevTipBaslamaSaatleri.Count > 1)
            //{
            //    nobetGruplar = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrup.Id)
            //    .Select(s => new MyDrop
            //    {
            //        Id = s.Id,
            //        Value = $"{s.Id}, {s.NobetGrupAdi}, {s.NobetGorevTipAdi} ({s.BaslamaTarihi.ToShortDateString()})"
            //    }).ToList();
            //}
            //else if (nobetGorevTipler.Count > 1)
            //{
            //    nobetGruplar = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrup.Id)
            //    .Select(s => new MyDrop
            //    {
            //        Id = s.Id,
            //        Value = $"{s.Id}, {s.NobetGrupAdi}, {s.NobetGorevTipAdi}"
            //    }).ToList();
            //}
            //else
            //{
            //    nobetGruplar = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrup.Id)
            //    .Select(s => new MyDrop
            //    {
            //        Id = s.Id,
            //        Value = $"{s.Id}, {s.NobetGrupAdi}"
            //    }).ToList();
            //}

            var gelecekTarih = DateTime.Today.AddMonths(1);
            int gelecekYil = gelecekTarih.Year;
            int gelecekAy = gelecekTarih.Month;

            var baslangicTarihi = new DateTime(gelecekYil, gelecekAy, 1);

            if (nobetUstGrup.BaslangicTarihi > baslangicTarihi)
            {
                baslangicTarihi = nobetUstGrup.BaslangicTarihi;
            }

            ViewBag.NobetGrupGorevTipId = new SelectList(nobetGruplar, "Id", "Value");
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
            //var user = _userService.GetByUserName(User.Identity.Name);
            //var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            //var nobetUstGrup = nobetUstGruplar.FirstOrDefault();
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

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

            var silinecekNobetlerTumu = _eczaneNobetSonucService.GetSonuclar(nobetGrupGorevTipId, baslangicTarihi, bitisTarihi2);

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

            var silinecekNobetAktifSonuclar = _eczaneNobetSonucAktifService.GetDetaylar2(nobetUstGrup.Id).Select(s => s.Id).ToArray();

            try
            {
                _eczaneNobetSonucAktifService.CokluSil(silinecekNobetAktifSonuclar);
            }
            catch (Exception e)
            {
                //TempData["LastError"] = e;

                throw new Exception("Aktif sonuçlar silinemedi!", e.InnerException);
            }

            if (silinecekKayitSayisi > 0)
            {
                try
                {
                    //var sw = new Stopwatch();
                    //sw.Start();

                    _eczaneNobetSonucService.CokluSil(silinecekNobetler);
                    //_eczaneNobetSonucService.Delete(silinecekNobetler);

                    //var r = sw.Elapsed;

                    if (nobetUstGrup.Id == 2)
                    {
                        //_eczaneNobetSonucPlanlananService.CokluSil(silinecekNobetlerPlanlanan);
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Nöbetler silinemedi!", e.InnerException);
                }

                if (nobetUstGrup.Id == 1    //alanya
                    || nobetUstGrup.Id == 3 //mersin
                    || nobetUstGrup.Id == 9 //çorum
                    )
                {
                    //_ayniGunTutulanNobetService.IkiliEczaneIstatistiginiSifirla(nobetUstGrup.Id);
                    //var sonuclar = _eczaneNobetSonucService.GetSonuclarUstGrupBaslamaTarihindenSonra(nobetUstGrup.Id);
                    //var sonuclar = _eczaneNobetSonucService.GetSonuclar(nobetGrupGorevTipId, baslangicTarihi, bitisTarihi);
                    var ayniGunNobetTutanEczaneler = _eczaneNobetOrtakService.GetAyniGunNobetTutanEczaneler(silinecekNobetlerTumu);
                    var ayniGunNobetSayisiGrouped = _eczaneNobetOrtakService.AyniGunTutulanNobetSayisiniHesapla(ayniGunNobetTutanEczaneler);

                    _ayniGunTutulanNobetService.AyniGunNobetSayisiniGuncelle(ayniGunNobetSayisiGrouped, AyniGunNobetEklemeTuru.Azalt);
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
            //var user = _userService.GetByUserName(User.Identity.Name);
            //var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            //var nobetUstGrup = nobetUstGruplar.FirstOrDefault();
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrup.Id);

            var nobetGorevTipler = nobetGrupGorevTipler.Select(s => s.NobetGorevTipId).Distinct().ToList();
            var nobetGrupGorevTipBaslamaSaatleri = nobetGrupGorevTipler.Select(s => s.BaslamaTarihi).Distinct().ToList();

            var nobetGruplar = _eczaneNobetSonucService.GetNobetGrupSonYayimNobetTarihleri(nobetUstGrup.Id);

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
                BaslangicTarihi = baslangicTarihi,
                YayimlandiMi = true
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandleError]
        public ActionResult NobetleriYayimla(DateTime baslangicTarihi, DateTime? bitisTarihi, int[] nobetGrupGorevTipId, bool yayimlandiMi)
        {
            //var user = _userService.GetByUserName(User.Identity.Name);
            //var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            //var nobetUstGrup = nobetUstGruplar.FirstOrDefault();
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

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

                return RedirectToAction("YayimlananNobetlerPartialView", new { silinecekKayitSayisi = -1, yayimlandiMi });
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

            return RedirectToAction("YayimlananNobetlerPartialView", new { silinecekKayitSayisi, yayimlandiMi });
        }

        //public ActionResult AlternatifNobetciOnerileri()
        //{

        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[HandleError]
        public ActionResult AlternatifNobetciOnerileri(int eczaneNobetSonucId)
        {
            var eczaneNobetSonuc = _eczaneNobetSonucService.GetDetay2ById(eczaneNobetSonucId);

            var eczanelerArasiMesafeyiKoru = _nobetUstGrupKisitService.GetDetay("eczanelerArasiMesafeyiKoru", eczaneNobetSonuc.NobetUstGrupId);

            ViewBag.MesafeKriteri = (int)eczanelerArasiMesafeyiKoru.SagTarafDegeri;

            return View(eczaneNobetSonuc);
        }

        public ActionResult AlternatifNobetcilerPartial(int eczaneNobetSonucId, int mesafeKriter = 0)
        {
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");
            var nobetUstGrupId = nobetUstGrup.Id;
            //var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetMyDrop(nobetUstGrupId);

            var feragatEdenNobetciEczane = _eczaneNobetSonucService.GetDetay2ById(eczaneNobetSonucId);

            var nobetGrupGorevTipId = feragatEdenNobetciEczane.NobetGrupGorevTipId;
            var nobetTarihi = feragatEdenNobetciEczane.Tarih;

            var pespeseGunAraligi = (int)_nobetGrupKuralService.GetDetay(nobetGrupGorevTipId, 1).Deger;

            var baslangicTarihi = nobetTarihi.AddDays(-pespeseGunAraligi);
            var bitisTarihi = nobetTarihi.AddDays(pespeseGunAraligi);

            var nobetGrupGorevTipGunKurallar = _nobetGrupGorevTipGunKuralService.GetDetaylarByNobetGrupGorevTipId(nobetGrupGorevTipId);

            var nobetGrupGorevTipTakvimOzelGunler = _nobetGrupGorevTipTakvimOzelGunService.GetDetaylarNobetGrupGorevTipBaslamaTarihindenSonra(nobetGrupGorevTipId);

            var mazeretler = _eczaneNobetMazeretService.GetDetaylarByNobetGrupGorevTipId(nobetTarihi, nobetTarihi, nobetGrupGorevTipId);

            var istekler = _eczaneNobetIstekService.GetDetaylar(nobetTarihi, nobetTarihi, nobetGrupGorevTipId);

            var eczanelerArasiMesafeler = _eczaneUzaklikMatrisService.GetDetaylarByEczaneId(feragatEdenNobetciEczane.EczaneId, mesafeKriter);

            var yerineNobetTutabilecekEczanelerinSonuclariTumu = _eczaneNobetSonucService.GetDetaylar(nobetUstGrupId)
                .Where(w => w.Tarih >= w.NobetGrupGorevTipBaslamaTarihi)
                .ToList();

            var yerineNobetTutamayacakEczaneler = yerineNobetTutabilecekEczanelerinSonuclariTumu
                .Where(w => w.NobetGrupGorevTipId == nobetGrupGorevTipId
                        && (w.Tarih >= baslangicTarihi && w.Tarih <= bitisTarihi))
                .Select(s => s.EczaneId).Distinct().ToList();

            var yerineNobetTutabilecekEczanelerinSonuclarIlgili = yerineNobetTutabilecekEczanelerinSonuclariTumu
                .Where(w => w.NobetGrupGorevTipId == nobetGrupGorevTipId
                        && !yerineNobetTutamayacakEczaneler.Contains(w.EczaneId)).ToList();

            var sonuclar = _eczaneNobetOrtakService
                .EczaneNobetSonucBirlesim(nobetGrupGorevTipGunKurallar,
                yerineNobetTutabilecekEczanelerinSonuclariTumu,
                nobetGrupGorevTipTakvimOzelGunler,
                EczaneNobetSonucTuru.Kesin)
                //.Select(s => s.EczaneId)
                .ToList();

            var nobetTarihindekiDigerEczaneler = sonuclar
                .Where(w => w.Tarih == nobetTarihi
                         && w.Id != eczaneNobetSonucId)
                .ToList();

            var mesafeler = _eczaneUzaklikMatrisService.GetDetaylar(nobetUstGrupId);

            var eczaneMesafeler = mesafeler.Where(w => w.Mesafe <= mesafeKriter).ToList();

            var mesafeKontrolEczaneler = new List<EczaneGrupDetay>();

            foreach (var nobetTarihindekiDigerEczane in nobetTarihindekiDigerEczaneler)
            {
                var eczaneMesafelerDigerEczaneler = eczaneMesafeler
                    .Where(w => w.EczaneIdFrom == nobetTarihindekiDigerEczane.EczaneId
                             || w.EczaneIdTo == nobetTarihindekiDigerEczane.EczaneId).ToList();

                var mesafeKontrolEczaneler2 = _eczaneUzaklikMatrisService.GetMesafeKriterineGoreKontrolEdilecekEczaneGruplar(mesafeKriter, eczaneMesafelerDigerEczaneler);

                mesafeKontrolEczaneler.AddRange(mesafeKontrolEczaneler2);
            }

            var nobetTarihindekiDigerEczanelerinEsGruplari = _eczaneGrupService.GetDetaylarEczaneninEsOlduguEczaneler(nobetTarihindekiDigerEczaneler.Select(s => s.EczaneNobetGrupId).ToList());

            nobetTarihindekiDigerEczanelerinEsGruplari.AddRange(mesafeKontrolEczaneler);

            var eklenecekEczanelerinEsGrupTanimlari = nobetTarihindekiDigerEczanelerinEsGruplari.Select(s => new { s.EczaneGrupTanimId, s.EczaneGrupTanimAdi }).Distinct().ToList();

            var eczaneNobetGruplar = _eczaneNobetGrupService.GetDetaylarByNobetGrupGorevTipler(nobetGrupGorevTipId)
                .Where(w => w.BitisTarihi == null).ToList();

            var feragatEdenNobetciEczaneDetay = sonuclar
                .Where(w => w.Id == eczaneNobetSonucId).SingleOrDefault();

            var sonuclarIlgili = sonuclar
                .Where(w => w.NobetGrupGorevTipId == nobetGrupGorevTipId).ToList();

            var alternatifOlasiEczaneler = new List<AlternatifOlasiNobetciEczaneler>();

            if (mesafeKriter > 0)
            {
                sonuclarIlgili = sonuclar.Where(w => eczanelerArasiMesafeler.Select(s => s.EczaneIdTo).Contains(w.EczaneId)).ToList();

                var uygunEczaneler = eczanelerArasiMesafeler
                    .Where(w => !yerineNobetTutamayacakEczaneler.Contains(w.EczaneIdTo))
                    .OrderBy(o => o.Mesafe)
                    .ToList();

                foreach (var uygunEczane in uygunEczaneler)
                {
                    var mazereti = mazeretler.SingleOrDefault(x => x.EczaneId == uygunEczane.EczaneIdTo);

                    var alternatifEczanelerdenAyniEsGruptaOlanlar = new List<EczaneGrupDetay>();

                    foreach (var eklenecekEczanelerinEsGrupTanim in eklenecekEczanelerinEsGrupTanimlari)
                    {
                        var ayniGruptaOlanEklenmekIstenenEczaneler = nobetTarihindekiDigerEczanelerinEsGruplari
                            .Where(w => (w.EczaneId == uygunEczane.EczaneIdTo
                                      || w.EczaneId == uygunEczane.EczaneIdFrom)
                                      && w.EczaneGrupTanimId == eklenecekEczanelerinEsGrupTanim.EczaneGrupTanimId).ToList();

                        if (ayniGruptaOlanEklenmekIstenenEczaneler.Count > 1)
                        {
                            alternatifEczanelerdenAyniEsGruptaOlanlar.AddRange(ayniGruptaOlanEklenmekIstenenEczaneler);

                            continue;
                        }
                    }

                    if (alternatifEczanelerdenAyniEsGruptaOlanlar.Count > 0)
                        continue;

                    var yerineNobetTutabilecekEczanelerinSonuclariBakilanEczane = sonuclarIlgili
                        .Where(w => w.EczaneId == uygunEczane.EczaneIdTo).ToList();

                    var oncekiNobetler = yerineNobetTutabilecekEczanelerinSonuclariBakilanEczane.Where(w => w.Tarih < nobetTarihi).ToList();

                    var sonrakiNobetler = yerineNobetTutabilecekEczanelerinSonuclariBakilanEczane.Where(w => w.Tarih > nobetTarihi).ToList();

                    alternatifOlasiEczaneler.Add(new AlternatifOlasiNobetciEczaneler
                    {
                        Tarih = nobetTarihi,
                        EczaneAdi = uygunEczane.EczaneAdiTo,
                        Mesafe = uygunEczane.Mesafe,
                        Mazereti = mazereti != null ? mazereti.MazeretAdi : "yok",
                        OncekiNobetTarihi = oncekiNobetler.Count > 0
                                ? oncekiNobetler.OrderByDescending(o => o.Tarih).FirstOrDefault().Tarih
                                : DateTime.Today, //yerineNobetTutabilecekEczanelerinSonuclariBakilanEczane.OrderByDescending(o => o.Tarih).FirstOrDefault().Tarih,
                        SonrakiNobetTarihi = sonrakiNobetler.Count > 0
                                ? sonrakiNobetler.OrderBy(o => o.Tarih).FirstOrDefault().Tarih
                                : DateTime.Today, //yerineNobetTutabilecekEczanelerinSonuclariBakilanEczane.OrderBy(o => o.Tarih).FirstOrDefault().Tarih,
                        NobetSayisiToplam = yerineNobetTutabilecekEczanelerinSonuclariBakilanEczane.Count,
                        NobetSayisiGunGrup = yerineNobetTutabilecekEczanelerinSonuclariBakilanEczane.Where(w => w.GunGrupId == feragatEdenNobetciEczaneDetay.GunGrupId).Count()
                    });
                }
            }
            else if (mesafeKriter == 0)
            {
                var uygunEczaneler = eczaneNobetGruplar
                    .Where(w => !yerineNobetTutamayacakEczaneler.Contains(w.EczaneId))
                    .ToList();

                foreach (var uygunEczane in uygunEczaneler)
                {
                    var mazereti = mazeretler.SingleOrDefault(x => x.EczaneId == uygunEczane.EczaneId);

                    var alternatifEczanelerdenAyniEsGruptaOlanlar = new List<EczaneGrupDetay>();

                    foreach (var eklenecekEczanelerinEsGrupTanim in eklenecekEczanelerinEsGrupTanimlari)
                    {
                        var ayniGruptaOlanEklenmekIstenenEczaneler = nobetTarihindekiDigerEczanelerinEsGruplari
                            .Where(w => w.EczaneId == uygunEczane.EczaneId
                                     && w.EczaneGrupTanimId == eklenecekEczanelerinEsGrupTanim.EczaneGrupTanimId).SingleOrDefault();

                        if (ayniGruptaOlanEklenmekIstenenEczaneler != null)
                        {
                            alternatifEczanelerdenAyniEsGruptaOlanlar.Add(ayniGruptaOlanEklenmekIstenenEczaneler);

                            continue;
                        }
                    }

                    if (alternatifEczanelerdenAyniEsGruptaOlanlar.Count > 0)
                        continue;

                    var yerineNobetTutabilecekEczanelerinSonuclariBakilanEczane = sonuclarIlgili
                        .Where(w => w.EczaneId == uygunEczane.EczaneId).ToList();

                    var oncekiNobetler = yerineNobetTutabilecekEczanelerinSonuclariBakilanEczane.Where(w => w.Tarih < nobetTarihi).ToList();

                    var sonrakiNobetler = yerineNobetTutabilecekEczanelerinSonuclariBakilanEczane.Where(w => w.Tarih > nobetTarihi).ToList();

                    var olasiNobetciEczane = new AlternatifOlasiNobetciEczaneler
                    {
                        Tarih = nobetTarihi,
                        EczaneAdi = uygunEczane.EczaneAdi,
                        Mesafe = 0,
                        Mazereti = mazereti != null ? mazereti.MazeretAdi : "yok",
                        OncekiNobetTarihi = oncekiNobetler.Count > 0
                                ? oncekiNobetler.OrderByDescending(o => o.Tarih).FirstOrDefault().Tarih
                                : DateTime.Today,// yerineNobetTutabilecekEczanelerinSonuclariBakilanEczane.OrderByDescending(o => o.Tarih).FirstOrDefault().Tarih,
                        SonrakiNobetTarihi = sonrakiNobetler.Count > 0
                                ? sonrakiNobetler.OrderBy(o => o.Tarih).FirstOrDefault().Tarih
                                : DateTime.Today,// yerineNobetTutabilecekEczanelerinSonuclariBakilanEczane.OrderBy(o => o.Tarih).FirstOrDefault().Tarih,
                        NobetSayisiToplam = yerineNobetTutabilecekEczanelerinSonuclariBakilanEczane.Count,
                        NobetSayisiGunGrup = yerineNobetTutabilecekEczanelerinSonuclariBakilanEczane.Where(w => w.GunGrupId == feragatEdenNobetciEczaneDetay.GunGrupId).Count()
                    };

                    alternatifOlasiEczaneler.Add(olasiNobetciEczane);
                }
            }
            else
            {
                throw new Exception("Mesafe kriteri negatif olamaz!!");
            }

            var uzunluk = alternatifOlasiEczaneler.Count;

            ViewBag.nobetGrup = nobetGrupGorevTipId;
            ViewBag.GunGrupAdi = feragatEdenNobetciEczaneDetay.GunGrupAdi;

            if (uzunluk > 0)
            {
                ViewBag.NobetSayisiToplamEnAz = alternatifOlasiEczaneler.Min(x => x.NobetSayisiToplam);
                ViewBag.NobetSayisiToplamEnCok = alternatifOlasiEczaneler.Max(x => x.NobetSayisiToplam);

                ViewBag.NobetSayisiGunGrupEnAz = alternatifOlasiEczaneler.Min(x => x.NobetSayisiGunGrup);
                ViewBag.NobetSayisiGunGrupEnCok = alternatifOlasiEczaneler.Max(x => x.NobetSayisiGunGrup);
            }

            //ViewBag.NobetGruplar = new SelectList(nobetGrupGorevTipler, "Id", "Value", nobetGrupGorevTipId);
            ViewBag.MesafeKriter = mesafeKriter;
            ViewBag.ToplamUzunluk = uzunluk;

            return PartialView("AlternatifNobetcilerPartial", alternatifOlasiEczaneler);
        }

        public ActionResult NobetciEczaneMesafeler()
        {
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");
            var nobetUstGrupId = nobetUstGrup.Id;

            var eczanelerArasiMesafeyiKoru = _nobetUstGrupKisitService.GetDetay("eczanelerArasiMesafeyiKoru", nobetUstGrupId);

            ViewBag.MesafeKriteri = (int)eczanelerArasiMesafeyiKoru.SagTarafDegeri;

            return View();
        }

        public ActionResult NobetciEczaneMesafelerPartial(DateTime nobetTarihi, int mesafeKriter = 0)
        {
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");
            var nobetUstGrupId = nobetUstGrup.Id;
            //var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetMyDrop(nobetUstGrupId);

            //var nobetGrupGorevTipId = feragatEdenNobetciEczane.NobetGrupGorevTipId;
            if (mesafeKriter <= 0)
            {
                throw new Exception("Mesafe kriteri negatif olamaz!!");
            }

            var gunlukSonuclar = _eczaneNobetSonucService.GetDetaylarGunluk(nobetTarihi, nobetUstGrupId)
                       .Where(w => w.Tarih >= w.NobetGrupGorevTipBaslamaTarihi)
                .ToList();

            var eczanelerArasiMesafeler = new List<NobetciEczaneMesafe>();

            var sonucSayi = gunlukSonuclar.Count;

            var mesafeler = _eczaneUzaklikMatrisService.GetDetaylar(nobetUstGrupId);
            var eczaneNobetGruplar = _eczaneNobetGrupService.GetDetaylar(nobetUstGrupId);

            for (int i = 0; i < sonucSayi - 1; i++)
            {
                for (int j = i + 1; j < sonucSayi; j++)
                {
                    var s = _eczaneUzaklikMatrisService.GetDetay(gunlukSonuclar[i].EczaneId, gunlukSonuclar[j].EczaneId, mesafeler);
                    var eczaneFrom = eczaneNobetGruplar.SingleOrDefault(x => x.EczaneId == gunlukSonuclar[i].EczaneId) ?? new EczaneNobetGrupDetay();
                    var eczaneTo = eczaneNobetGruplar.SingleOrDefault(x => x.EczaneId == gunlukSonuclar[j].EczaneId) ?? new EczaneNobetGrupDetay();

                    var m = new NobetciEczaneMesafe
                    {
                        EczaneAdiFrom = s.EczaneAdiFrom,
                        EczaneAdiTo = s.EczaneAdiTo,
                        EczaneIdFrom = s.EczaneIdFrom,
                        EczaneIdTo = s.EczaneIdTo,
                        Mesafe = s.Mesafe,
                        NobetUstGrupId = s.NobetUstGrupId,
                        NobetTarihi = nobetTarihi,
                        EczaneNobetGrupIdFrom = eczaneFrom.Id,
                        EczaneNobetGrupIdTo = eczaneTo.Id,
                        NobetGrupGorevTipIdTo = eczaneTo.NobetGrupGorevTipId,
                        NobetGrupGorevTipAdiTo = eczaneTo.NobetGrupGorevTipAdi,
                        NobetGrupGorevTipIdFrom = eczaneFrom.NobetGrupGorevTipId,
                        NobetGrupGorevTipAdiFrom = eczaneFrom.NobetGrupGorevTipAdi
                    };

                    eczanelerArasiMesafeler.Add(m);
                }
            }

            var eczaneMesafeler = eczanelerArasiMesafeler.Where(w => w.Mesafe <= mesafeKriter).ToList();

            var uzunluk = eczanelerArasiMesafeler.Count;
            var mesafeKriterineUygunOlmayanEczaneSayisi = eczaneMesafeler.Count;

            var eczanelerArasiMesafeyiKoru = _nobetUstGrupKisitService.GetDetay("eczanelerArasiMesafeyiKoru", nobetUstGrupId);

            ViewBag.MesafeKriteri = (int)eczanelerArasiMesafeyiKoru.SagTarafDegeri;

            return PartialView("NobetciEczaneMesafelerPartial", eczanelerArasiMesafeler.OrderBy(o => o.Mesafe).ToList());
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
                    NobetGunKuralAdi = s.NobetGunKuralAdi,
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
            //var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var eczaneler = _eczaneService.GetDetaylar(nobetUstGrup.Id);
            //.GetListByUser(user);

            var eczaneNobetGruplar = _eczaneNobetGrupService.GetDetaylarByEczaneIdList(eczaneler.Select(s => s.Id).ToList())
                .OrderBy(s => s.EczaneAdi).ThenBy(t => t.NobetGrupAdi)
                .Select(s => new MyDrop { Id = s.Id, Value = $"{s.EczaneAdi}, {s.NobetGrupGorevTipAdi}" });

            //var tarihler = _takvimService.GetList()
            //    .Select(s => new MyDrop { Id = s.Id, Value = $"{s.Tarih.ToLongDateString()}" });

            //var nobetGorevTipler = _nobetGorevTipService.GetList()
            //    .Select(s => new MyDrop { Id = s.Id, Value = s.Adi })
            //    .OrderBy(s => s.Id);

            //ViewBag.TakvimId = new SelectList(tarihler, "Id", "Value");
            ViewBag.EczaneNobetGrupId = new SelectList(eczaneNobetGruplar, "Id", "Value");
            //ViewBag.NobetGorevTipId = new SelectList(nobetGorevTipler, "Id", "Value");

            return View();
        }

        public ActionResult CreateGercekNobetci(int takvimId)
        {
            //var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var eczaneler = _eczaneService.GetDetaylar(nobetUstGrup.Id);
            //.GetListByUser(user);

            var eczaneNobetGruplar = _eczaneNobetGrupService.GetDetaylarByEczaneIdList(eczaneler.Select(s => s.Id).ToList())
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
        public ActionResult Create([Bind(Include = "Id,EczaneNobetGrupId,NobetTarihi,YayimlandiMi")] EczaneNobetSonucEkle eczaneNobetSonuc)
        {
            if (eczaneNobetSonuc.NobetTarihi == null)
            {
                throw new ArgumentNullException("Nöbet tarihi boş bırakılamaz.");
            }

            var takvim = _takvimService.GetByTarih(eczaneNobetSonuc.NobetTarihi);
            var eczane = _eczaneNobetGrupService.GetDetayById(eczaneNobetSonuc.EczaneNobetGrupId);

            var sonucParametreler = new EczaneNobetSonuc
            {
                TakvimId = takvim.Id,
                NobetGorevTipId = eczane.NobetGorevTipId,
                EczaneNobetGrupId = eczaneNobetSonuc.EczaneNobetGrupId,
                YayimlandiMi = eczaneNobetSonuc.YayimlandiMi
            };

            if (ModelState.IsValid)
            {
                _eczaneNobetSonucService.Insert(sonucParametreler);

                return RedirectToAction("PivotSonuclar");
            }

            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var eczaneler = _eczaneService.GetDetaylar(nobetUstGrup.Id);

            var eczaneNobetGruplar = _eczaneNobetGrupService.GetDetaylarByEczaneIdList(eczaneler.Select(s => s.Id).ToList())
                .OrderBy(s => s.EczaneAdi).ThenBy(t => t.NobetGrupAdi)
                .Select(s => new MyDrop { Id = s.Id, Value = $"{s.EczaneAdi}, {s.NobetGrupGorevTipAdi}" });

            //var tarihler = _takvimService.GetList()
            //    .Select(s => new MyDrop { Id = s.Id, Value = $"{s.Tarih.ToLongDateString()}" });

            //var nobetGorevTipler = _nobetGorevTipService.GetList()
            //    .Select(s => new MyDrop { Id = s.Id, Value = s.Adi })
            //    .OrderBy(s => s.Id);

            ViewBag.EczaneNobetGrupId = new SelectList(eczaneNobetGruplar, "Id", "Value", eczaneNobetSonuc.EczaneNobetGrupId);
            //ViewBag.TakvimId = new SelectList(tarihler, "Id", "Value", eczaneNobetSonuc.TakvimId);
            //ViewBag.NobetGorevTipId = new SelectList(nobetGorevTipler, "Id", "Value", eczaneNobetSonuc.NobetGorevTipId);

            return View(eczaneNobetSonuc);
        }

        public ActionResult NobetDegistir()
        {
            //var user = _userService.GetByUserName(User.Identity.Name);
            //var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            //var nobetUstGrupId = nobetUstGruplar.Select(x => x.Id).FirstOrDefault();
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetMyDrop(nobetUstGrup.Id);

            //ViewBag.NobetGrupId = 0;

            ViewBag.NobetGrupGorevTipId = new SelectList(nobetGrupGorevTipler, "Id", "Value");

            return View();
        }

        //[HttpPost]
        //[ChildActionOnly]
        public ActionResult SonuclarPartial(DateTime? nobetTarihi, int nobetGrupGorevTipId = 0)
        {
            //var user = _userService.GetByUserName(User.Identity.Name);
            //var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");
            var nobetUstGrupId = nobetUstGrup.Id;
            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetMyDrop(nobetUstGrupId);

            var pivotSekiller = _eczaneNobetOrtakService.GetPivotSekiller();

            var sonuclar = _eczaneNobetSonucService.GetDetaylar(nobetUstGrupId)
                .Where(w => (w.NobetGrupGorevTipId == nobetGrupGorevTipId || nobetGrupGorevTipId == 0)
                          && w.Tarih == nobetTarihi)
                .OrderBy(o => o.NobetGrupGorevTipId)
                .ThenBy(o => o.EczaneAdi).ToList();

            ViewBag.nobetGrup = nobetGrupGorevTipId;

            ViewBag.NobetGruplar = new SelectList(nobetGrupGorevTipler, "Id", "Value", nobetGrupGorevTipId);

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

            var tarihler = _takvimService.GetList()
                .Select(s => new MyDrop { Id = s.Id, Value = $"{s.Tarih.ToLongDateString()}" });

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

        private class AltGruplarlaTakipEdilecekNobetGrup
        {
            public string GrupAdi { get; internal set; }
            public int NobetGrupGorevTipId { get; internal set; }
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
