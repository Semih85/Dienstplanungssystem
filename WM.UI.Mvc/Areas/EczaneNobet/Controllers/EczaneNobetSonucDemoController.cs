using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WM.Northwind.Business.Abstract.Authorization;
using WM.Northwind.Business.Abstract.EczaneNobet;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;
using WM.UI.Mvc.Areas.EczaneNobet.Models;
using WM.UI.Mvc.Models;

namespace WM.UI.Mvc.Areas.EczaneNobet.Controllers
{
    [Authorize]
    [HandleError]
    public class EczaneNobetSonucDemoController : Controller
    {
        #region ctor
        private IEczaneNobetOrtakService _eczaneNobetOrtakService;
        private IEczaneNobetSonucDemoService _eczaneNobetSonucDemoService;
        private ITakvimService _takvimService;
        private IEczaneNobetGrupService _eczaneNobetGrupService;
        private IEczaneGrupService _eczaneGrupService;
        private IUserService _userService;
        private IEczaneService _eczaneService;
        private INobetUstGrupService _nobetUstGrupService;
        private INobetGrupService _nobetGrupService;
        private INobetSonucDemoTipService _nobetSonucDemoTipService;
        private INobetGunKuralService _nobetGunKuralService;

        public EczaneNobetSonucDemoController(IEczaneNobetSonucDemoService eczaneNobetSonucDemoService,
            ITakvimService takvimService,
            IEczaneNobetGrupService eczaneNobetGrupService,
            IEczaneGrupService eczaneGrupService,
            INobetUstGrupService nobetUstGrupService,
            INobetGrupService nobetGrupService,
            IUserService userService,
            IEczaneService eczaneService,
            INobetSonucDemoTipService nobetSonucDemoTipService,
            INobetGunKuralService nobetGunKuralService,
            IEczaneNobetOrtakService eczaneNobetOrtakService)
        {
            _eczaneNobetSonucDemoService = eczaneNobetSonucDemoService;
            _takvimService = takvimService;
            _eczaneNobetGrupService = eczaneNobetGrupService;
            _eczaneGrupService = eczaneGrupService;
            _userService = userService;
            _eczaneService = eczaneService;
            _nobetUstGrupService = nobetUstGrupService;
            _nobetGrupService = nobetGrupService;
            _nobetSonucDemoTipService = nobetSonucDemoTipService;
            _nobetGunKuralService = nobetGunKuralService;
            _eczaneNobetOrtakService = eczaneNobetOrtakService;
        }
        #endregion
        // GET: EczaneNobet/EczaneNobetSonucDemoe
        public ActionResult Index()
        {
            var model = _eczaneNobetSonucDemoService.GetDetaylar();
            return View(model);
        }

        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonResult()
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior,
                MaxJsonLength = 86753090//Int32.MaxValue
            };
        }

        public ActionResult DemoPivot()
        {
            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGrupId = _nobetUstGrupService.GetListByUser(user).Select(x => x.Id).FirstOrDefault();
            var nobetGunKurallar = _nobetGunKuralService.GetList();
            var nobetGruplar = _nobetGrupService.GetListByNobetUstGrupId(nobetUstGrupId)
                .Select(s => new MyDrop { Id = s.Id, Value = s.Adi });

            var sonuclarHepsi = _eczaneNobetSonucDemoService.GetSonuclar2(nobetUstGrupId);

            var yillar = sonuclarHepsi
                .Select(s => s.Yil).Distinct()
                .OrderBy(o => o).ToList();

            var versiyonlar = sonuclarHepsi
                    .Select(s => s.NobetSonucDemoTipId).Distinct().ToList();

            var demoTipler = _nobetSonucDemoTipService.GetList()
                .Where(w => versiyonlar.Contains(w.Id))
                .Select(s => new MyDrop { Id = s.Id, Value = s.Adi })
                .OrderBy(o => o.Id).ToList();

            var pivotSekiller = _eczaneNobetOrtakService.GetPivotSekiller();

            var versiyon = demoTipler.Select(s => s.Id).LastOrDefault();
            var sekil = 1;

            var yilBaslangic = sonuclarHepsi
                .Where(w => w.Yil == DateTime.Now.Year)
                .Select(s => s.Yil).Distinct()
                .OrderBy(o => o).FirstOrDefault();

            //var yilBaslangic = sonuclar.Select(s => s.Yil).Distinct().OrderBy(o => o).FirstOrDefault();
            var yilBitis = yillar.Where(s => s == DateTime.Now.Year).SingleOrDefault();
            var nobetGrup = 0;

            ViewBag.yilBaslangic = yilBaslangic;
            ViewBag.yilBitis = yilBitis;
            ViewBag.versiyon = versiyon;
            ViewBag.sekil = sekil;
            ViewBag.nobetGrup = nobetGrup;

            ViewBag.Versiyonlar = new SelectList(demoTipler, "Id", "Value", versiyon);
            ViewBag.YilBaslangiclar = new SelectList(yillar, null, null, yilBaslangic);
            ViewBag.YilBitisler = new SelectList(yillar, null, null, yilBitis);
            //ViewBag.PivotSekiller = new SelectList(pivotSekiller, "Id", "Value", sekil);
            ViewBag.NobetGruplar = new SelectList(nobetGruplar, "Id", "Value");

            var sonuclar = sonuclarHepsi.Where(s =>
                                        (s.Yil >= yilBaslangic && s.Yil <= yilBitis)
                                     //&& s.Ay == DateTime.Now.Month
                                     && s.NobetSonucDemoTipId == versiyon).ToList();

            ViewBag.ToplamUzunluk = sonuclar.Count();

            var gunFarklari = _eczaneNobetOrtakService.EczaneNobetIstatistikGunFarkiHesapla(sonuclar);

            var gunFarkiFrekanslar = _eczaneNobetOrtakService.EczaneNobetIstatistikGunFarkiFrekans(gunFarklari);

            var esGrubaAyniGunYazilanNobetler = _eczaneNobetOrtakService.GetEsGrubaAyniGunYazilanNobetler(sonuclar);

            var model = new EczaneNobetSonucViewModel
            {
                PivotSonuclar = sonuclar,
                GunFarklariTumSonuclar = gunFarklari,
                GunFarklariFrekanslar = gunFarkiFrekanslar,
                EsGrubaAyniGunYazilanNobetler = esGrubaAyniGunYazilanNobetler
            };

            return View(model);
        }

        [HttpPost]
        [HandleError]
        [ValidateAntiForgeryToken]
        public ActionResult DemoPivot(int nobetGrup = 0, int yilBaslangic = 2018, int yilBitis = 2020, int versiyon = 1)
        {
            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGrupId = _nobetUstGrupService.GetListByUser(user).Select(x => x.Id).FirstOrDefault();
            var eczaneler = _eczaneService.GetListByUser(user);
            var nobetGunKurallar = _nobetGunKuralService.GetList();

            var nobetGruplar = _nobetGrupService.GetListByNobetUstGrupId(nobetUstGrupId)
                .Select(s => new MyDrop { Id = s.Id, Value = s.Adi });

            var sonuclarHepsi = _eczaneNobetSonucDemoService.GetSonuclar2(nobetUstGrupId);

            var sonuclarFiltre = sonuclarHepsi
                .Where(s => s.NobetSonucDemoTipId == versiyon
                && (s.NobetGrupId == nobetGrup || nobetGrup == 0));

            var sonuclar = sonuclarFiltre.Where(s => (s.Yil >= yilBaslangic && s.Yil <= yilBitis)
                                     //&& eczaneler.Select(n => n.Id).Contains(s.EczaneId)
                                     ).ToList();
            var yillar = sonuclarHepsi
                .Select(s => s.Yil).Distinct()
                .OrderBy(o => o).ToList();

            var versiyonlar = sonuclarHepsi
                .Select(s => s.NobetSonucDemoTipId).Distinct().ToList();

            var demoTipler = _nobetSonucDemoTipService.GetList()
                .Where(w => versiyonlar.Contains(w.Id))
                .Select(s => new MyDrop { Id = s.Id, Value = s.Adi }).ToList();

            var pivotSekiller = _eczaneNobetOrtakService.GetPivotSekiller();

            ViewBag.yilBaslangic = yilBaslangic;
            ViewBag.yilBitis = yilBitis;
            ViewBag.versiyon = versiyon;
            ViewBag.nobetGrup = nobetGrup;

            ViewBag.Versiyonlar = new SelectList(demoTipler, "Id", "Value", versiyon);
            ViewBag.YilBaslangiclar = new SelectList(yillar, null, null, yilBaslangic);
            ViewBag.YilBitisler = new SelectList(yillar, null, null, yilBitis);
            //ViewBag.PivotSekiller = new SelectList(pivotSekiller, "Id", "Value", sekil);
            ViewBag.NobetGruplar = new SelectList(nobetGruplar, "Id", "Value", nobetGrup);
            ViewBag.ToplamUzunluk = sonuclar.Count;
            
            var gunFarklari = _eczaneNobetOrtakService.EczaneNobetIstatistikGunFarkiHesapla(sonuclar);

            var gunFarkiFrekanslar = _eczaneNobetOrtakService.EczaneNobetIstatistikGunFarkiFrekans(gunFarklari);
            var esGrubaAyniGunYazilanNobetler = _eczaneNobetOrtakService.GetEsGrubaAyniGunYazilanNobetler(sonuclar);

            var model = new EczaneNobetSonucViewModel
            {
                PivotSonuclar = sonuclar,
                GunFarklariTumSonuclar = gunFarklari,
                GunFarklariFrekanslar = gunFarkiFrekanslar,
                EsGrubaAyniGunYazilanNobetler = esGrubaAyniGunYazilanNobetler
            };

            return View(model);
        }

        public ActionResult PivotGunFarklari()
        {
            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            var nobetUstGrupId = nobetUstGruplar.Select(x => x.Id).FirstOrDefault();
            var nobetGruplar = _nobetGrupService.GetList()
                .Where(w => nobetUstGruplar.Select(x => x.Id).Contains(w.NobetUstGrupId))
                .Select(s => new MyDrop { Id = s.Id, Value = s.Adi });

            var sonuclarHepsi = _eczaneNobetSonucDemoService.EczaneNobetIstatistikGunFarkiHesapla(nobetUstGrupId);

            var versiyonlar = sonuclarHepsi
                .Select(s => s.NobetSonucDemoTipId).Distinct().ToList();

            var pivotSekiller = _eczaneNobetOrtakService.GetPivotSekillerGunFarki();

            var demoTipler = _nobetSonucDemoTipService.GetList()
                .Where(w => versiyonlar.Contains(w.Id))
                .Select(s => new MyDrop { Id = s.Id, Value = s.Adi })
                .OrderBy(o => o.Id).ToList();

            var versiyon = demoTipler.Select(s => s.Id).LastOrDefault();

            var sonuclar = _eczaneNobetSonucDemoService.EczaneNobetIstatistikGunFarkiHesapla(nobetUstGrupId, versiyon);

            var gunGruplar = sonuclar.Select(s => s.GunGrupAdi).Distinct();
            var gunGrup = "";

            var sekil = 1;

            //var gunFarkiFrekanslar = new List<EczaneNobetIstatistikGunFarkiFrekans>();

            //if (sekil < 3)
            //{
            var gunFarkiFrekanslar = _eczaneNobetOrtakService.EczaneNobetIstatistikGunFarkiFrekans(sonuclar);
            //}

            var nobetGrup = 0;

            ViewBag.sekil = sekil;
            ViewBag.nobetGrup = nobetGrup;
            ViewBag.gunGrup = gunGrup;

            ViewBag.Versiyonlar = new SelectList(demoTipler, "Id", "Value", versiyon);
            ViewBag.PivotSekiller = new SelectList(pivotSekiller, "Id", "Value", sekil);
            ViewBag.NobetGruplar = new SelectList(nobetGruplar, "Id", "Value");
            ViewBag.GunGruplar = new SelectList(gunGruplar, gunGrup);

            ViewBag.ToplamUzunluk = sonuclar.Count;

            var model = new PivotGunFarklariDemoViewModel
            {
                GunFarklariTumSonuclar = sonuclar,
                GunFarklariFrekanslar = gunFarkiFrekanslar
            };

            return View(model);
        }

        [HttpPost]
        [HandleError]
        [ValidateAntiForgeryToken]
        public ActionResult PivotGunFarklari(int nobetGrup = 0, string gunGrup = "", int versiyon = 1)
        {
            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            var nobetUstGrupId = nobetUstGruplar.Select(x => x.Id).FirstOrDefault();
            var nobetGruplar = _nobetGrupService.GetList()
                .Where(w => nobetUstGruplar.Select(x => x.Id).Contains(w.NobetUstGrupId))
                .Select(s => new MyDrop { Id = s.Id, Value = s.Adi });

            var sonuclarTumu = _eczaneNobetSonucDemoService.EczaneNobetIstatistikGunFarkiHesapla(nobetUstGrupId);

            var versiyonlar = sonuclarTumu
                .Select(s => s.NobetSonucDemoTipId).Distinct().ToList();

            var pivotSekiller = _eczaneNobetOrtakService.GetPivotSekillerGunFarki();

            var demoTipler = _nobetSonucDemoTipService.GetList()
                .Where(w => versiyonlar.Contains(w.Id))
                .Select(s => new MyDrop { Id = s.Id, Value = s.Adi })
                .OrderBy(o => o.Value).ToList();

            var sonuclarTumu2 = _eczaneNobetSonucDemoService.EczaneNobetIstatistikGunFarkiHesapla(nobetUstGrupId, versiyon);

            var sonuclar = sonuclarTumu2
                .Where(w => (w.NobetGrupId == nobetGrup || nobetGrup == 0)
                         && (w.GunGrupAdi == gunGrup || gunGrup == "")).ToList();

            var gunGruplar = sonuclarTumu2.Select(s => s.GunGrupAdi).Distinct();

            //var gunFarkiFrekanslar = new List<EczaneNobetIstatistikGunFarkiFrekans>();

            //if (sekil < 3)
            //{
            var gunFarkiFrekanslar = _eczaneNobetOrtakService.EczaneNobetIstatistikGunFarkiFrekans(sonuclar);
            //}

            //ViewBag.sekil = sekil;
            ViewBag.nobetGrup = nobetGrup;
            ViewBag.gunGrup = gunGrup;

            ViewBag.Versiyonlar = new SelectList(demoTipler, "Id", "Value", versiyon);
            //ViewBag.PivotSekiller = new SelectList(pivotSekiller, "Id", "Value", sekil);
            ViewBag.NobetGruplar = new SelectList(nobetGruplar, "Id", "Value");
            ViewBag.GunGruplar = new SelectList(gunGruplar, gunGrup);

            ViewBag.ToplamUzunluk = sonuclar.Count();

            var model = new PivotGunFarklariDemoViewModel
            {
                GunFarklariTumSonuclar = sonuclar,
                GunFarklariFrekanslar = gunFarkiFrekanslar
            };

            return View(model);
        }


        public ActionResult GorselSonuclar()
        {
            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGrupId = _nobetUstGrupService.GetListByUser(user).Select(r => r.Id).FirstOrDefault();
            var yetkiliEczaneler = _eczaneService.GetListByUser(user).Select(n => n.Id).ToList();
            var demoSonuclar = _eczaneNobetSonucDemoService.GetDetaylar(nobetUstGrupId);
            var nobetGunKurallar = _nobetGunKuralService.GetList();

            var eczaneler = _eczaneNobetGrupService.GetDetaylarByEczaneIdList(yetkiliEczaneler)
                                //.Where(w => yetkiliEczaneler.Contains(w.EczaneId))
                                .Select(s => new
                                {
                                    s.EczaneId,
                                    EczaneGrupAdi = $"{s.EczaneAdi}, {s.NobetGrupId}"
                                })
                                .Distinct().OrderBy(s => s.EczaneGrupAdi).ToList();

            var eczaneGruplar = _eczaneGrupService.GetDetaylar()
                .Where(w => yetkiliEczaneler.Contains(w.EczaneId)).ToList();

            var eczaneGrupNodes = _eczaneGrupService.GetNodes()
                .Where(s => eczaneGruplar.Select(w => w.EczaneId).Contains(s.Id)).ToList();

            var eczaneGrupEdges = _eczaneGrupService.GetEdges()
                .Where(s => nobetUstGrupId == s.NobetUstGrupId).ToList();

            var yillar = demoSonuclar.Select(s => s.Tarih.Year).Distinct().ToList();
            var versiyonlar = demoSonuclar.Select(s => s.NobetSonucDemoTipId).Distinct().ToList();

            var aylar = _takvimService.GetAylar().ToList();
            var Frekanslar = new List<int> { 1, 2, 3, 4, 5 };

            var demoTipler = _nobetSonucDemoTipService.GetList()
                .Where(w => versiyonlar.Contains(w.Id))
                .Select(s => new MyDrop { Id = s.Id, Value = s.Adi });

            ViewBag.eczaneId = new SelectList(eczaneler, "EczaneId", "EczaneGrupAdi");
            //ViewBag.yilBaslangic = new SelectList(yillar, "Yil");
            ViewBag.yilBitis = new SelectList(yillar, "Yil");
            //ViewBag.ayBaslangic = new SelectList(aylar, "Id", "Value");
            ViewBag.Aylar = new SelectList(aylar, "Id", "Value");
            var ay = 1;
            var frekans = 1;
            var versiyon = demoTipler.Select(s => s.Id).FirstOrDefault();

            ViewBag.ayBitis = ay;
            ViewBag.frekansDefault = frekans;
            ViewBag.versiyonDefault = versiyon;

            ViewBag.Frekans = new SelectList(Frekanslar, null, null, frekans);
            //ViewBag.Versiyon = new SelectList(versiyonlar, null, null, versiyon);
            ViewBag.Versiyonlar = new SelectList(demoTipler, "Id", "Value", versiyon);

            int ayniGuneDenkGelenNobetSayisi = frekans;

            //var eczaneNobetSonucNodes = _eczaneNobetSonucDemoService.GetEczaneNobetSonucDemoNodes(2018, ay, versiyon, yetkiliEczaneler);
            //var eczaneNobetSonucEdges = _eczaneNobetSonucDemoService.GetEczaneNobetSonucDemoEdges(2018, ay, ayniGuneDenkGelenNobetSayisi, versiyon, nobetUstGrupId);

            var model = new EczaneNobetGorselSonucViewModel()
            {
                EczaneNobetSonucNodes = new List<EczaneNobetSonucNode>(), //eczaneNobetSonucNodes,
                EczaneNobetSonucEdges = new List<EczaneNobetSonucEdge>(), //eczaneNobetSonucEdges,
                EczaneGrupNodes = eczaneGrupNodes,
                EczaneGrupEdges = eczaneGrupEdges
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GorselSonuclar(int yilBitis, int ayBitis = 0, int eczaneId = 0, int frekans = 1, int versiyon = 1)
        {
            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGrupId = _nobetUstGrupService.GetListByUser(user).Select(r => r.Id).FirstOrDefault();
            var yetkiliEczaneler = _eczaneService.GetListByUser(user).Select(n => n.Id).ToList();
            var demoSonuclar = _eczaneNobetSonucDemoService.GetDetaylar(nobetUstGrupId);
            var nobetGunKurallar = _nobetGunKuralService.GetList();

            var eczaneler = _eczaneNobetGrupService.GetDetaylarByEczaneIdList(yetkiliEczaneler)
                                //.Where(w => yetkiliEczaneler.Contains(w.EczaneId))
                                .Select(s => new
                                {
                                    s.EczaneId,
                                    EczaneGrupAdi = $"{s.EczaneAdi}, {s.NobetGrupId}"
                                })
                                .Distinct().OrderBy(s => s.EczaneGrupAdi).ToList();

            var eczaneGruplar = _eczaneGrupService.GetDetaylar()
                .Where(w => yetkiliEczaneler.Contains(w.EczaneId));

            var eczaneGrupNodes = _eczaneGrupService.GetNodes()
                .Where(s => eczaneGruplar.Select(w => w.EczaneId).Contains(s.Id)).ToList();

            var eczaneGrupEdges = _eczaneGrupService.GetEdges()
                .Where(s => nobetUstGrupId == s.NobetUstGrupId).ToList();

            var yillar = demoSonuclar.Select(s => s.Tarih.Year).Distinct().ToList();
            var versiyonlar = demoSonuclar.Select(s => s.NobetSonucDemoTipId).Distinct().ToList();

            var aylar = _takvimService.GetAylar().ToList();
            var Frekanslar = new List<int> { 1, 2, 3, 4, 5 };

            var demoTipler = _nobetSonucDemoTipService.GetList()
                .Where(w => versiyonlar.Contains(w.Id))
                .Select(s => new MyDrop { Id = s.Id, Value = s.Adi });

            ViewBag.eczaneId = new SelectList(eczaneler, "EczaneId", "EczaneGrupAdi");
            //ViewBag.yilBaslangic = new SelectList(yillar, "Yil");
            ViewBag.yilBitis = new SelectList(yillar, "Yil");
            //ViewBag.ayBaslangic = new SelectList(aylar, "Id", "Value");
            ViewBag.Aylar = new SelectList(aylar, "Id", "Value");
            ViewBag.Frekans = new SelectList(Frekanslar, null, null, frekans);
            ViewBag.Versiyonlar = new SelectList(demoTipler, "Id", "Value", versiyon);

            ViewBag.ayBitis = ayBitis;
            ViewBag.frekansDefault = frekans;
            ViewBag.versiyonDefault = versiyon;

            int ayniGuneDenkGelenNobetSayisi = frekans;

            //var eczaneNobetSonucNodes = _eczaneNobetSonucDemoService.GetEczaneNobetSonucDemoNodes(yilBitis, ayBitis, versiyon, yetkiliEczaneler);
            //var eczaneNobetSonucEdges = _eczaneNobetSonucDemoService.GetEczaneNobetSonucDemoEdges(yilBitis, ayBitis, ayniGuneDenkGelenNobetSayisi, versiyon, nobetUstGrupId);

            if (eczaneId == 0 && ayBitis == 0)
            {
                ayBitis = 12;
                var model = new EczaneNobetGorselSonucViewModel()
                {
                    EczaneNobetSonucNodes = new List<EczaneNobetSonucNode>(), //eczaneNobetSonucNodes,
                    EczaneNobetSonucEdges = new List<EczaneNobetSonucEdge>(), // eczaneNobetSonucEdges,
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
                    EczaneNobetSonucNodes = new List<EczaneNobetSonucNode>(), //eczaneNobetSonucNodes,
                    EczaneNobetSonucEdges = new List<EczaneNobetSonucEdge>(), 
                    //_eczaneNobetSonucDemoService.GetEczaneNobetSonucDemoEdges(yilBitis, ayBitis, ayniGuneDenkGelenNobetSayisi, versiyon, nobetUstGrupId).Where(d => d.From == eczaneId || d.To == eczaneId).ToList(),
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
                    EczaneNobetSonucNodes = new List<EczaneNobetSonucNode>(), //eczaneNobetSonucNodes,
                    EczaneNobetSonucEdges = new List<EczaneNobetSonucEdge>(), //eczaneNobetSonucEdges,
                    EczaneGrupNodes = eczaneGrupNodes,
                    EczaneGrupEdges = eczaneGrupEdges

                };
                return View(model);
            }
            else
            {
                var model = new EczaneNobetGorselSonucViewModel()
                {
                    EczaneNobetSonucNodes = new List<EczaneNobetSonucNode>(), //eczaneNobetSonucNodes,
                    EczaneNobetSonucEdges = new List<EczaneNobetSonucEdge>(),
                     //_eczaneNobetSonucDemoService.GetEczaneNobetSonucDemoEdges(yilBitis, ayBitis, ayniGuneDenkGelenNobetSayisi, versiyon, nobetUstGrupId).Where(d => d.From == eczaneId || d.To == eczaneId).ToList(),
                    EczaneGrupNodes = eczaneGrupNodes,
                    EczaneGrupEdges = eczaneGrupEdges
                           .Where(x => x.From == eczaneId || x.To == eczaneId).ToList()

                };
                return View(model);
            }
        }

        // GET: EczaneNobet/EczaneNobetSonucDemoe/Details/5
        public ActionResult Details(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EczaneNobetSonucDemoDetay2 eczaneNobetSonucDemo = _eczaneNobetSonucDemoService.GetDetayById(id);
            if (eczaneNobetSonucDemo == null)
            {
                return HttpNotFound();
            }
            return View(eczaneNobetSonucDemo);
        }

        // GET: EczaneNobet/EczaneNobetSonucDemoe/Create
        public ActionResult Create()
        {
            ViewBag.EczaneNobetGrupId = new SelectList(_eczaneNobetGrupService.GetList(), "Id", "Aciklama");
            ViewBag.TakvimId = new SelectList(_takvimService.GetList(), "Id", "Tarih");
            return View();
        }

        // POST: EczaneNobet/EczaneNobetSonucDemoe/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EczaneNobetGrupId,TakvimId,NobetGorevTipId")] EczaneNobetSonucDemo eczaneNobetSonucDemo)
        {
            if (ModelState.IsValid)
            {
                _eczaneNobetSonucDemoService.Insert(eczaneNobetSonucDemo);
                return RedirectToAction("Index");
            }

            ViewBag.EczaneNobetGrupId = new SelectList(_eczaneNobetGrupService.GetList(), "Id", "Aciklama", eczaneNobetSonucDemo.EczaneNobetGrupId);
            ViewBag.TakvimId = new SelectList(_takvimService.GetList(), "Id", "Tarih", eczaneNobetSonucDemo.TakvimId);
            return View(eczaneNobetSonucDemo);
        }

        // GET: EczaneNobet/EczaneNobetSonucDemoe/Edit/5
        public ActionResult Edit(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EczaneNobetSonucDemoDetay2 eczaneNobetSonucDemo = _eczaneNobetSonucDemoService.GetDetayById(id);
            if (eczaneNobetSonucDemo == null)
            {
                return HttpNotFound();
            }
            ViewBag.EczaneNobetGrupId = new SelectList(_eczaneNobetGrupService.GetList(), "Id", "Aciklama", eczaneNobetSonucDemo.EczaneNobetGrupId);
            ViewBag.TakvimId = new SelectList(_takvimService.GetList(), "Id", "Tarih", eczaneNobetSonucDemo.TakvimId);
            return View(eczaneNobetSonucDemo);
        }

        // POST: EczaneNobet/EczaneNobetSonucDemoe/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EczaneNobetGrupId,TakvimId,NobetGorevTipId")] EczaneNobetSonucDemo eczaneNobetSonucDemo)
        {
            if (ModelState.IsValid)
            {
                _eczaneNobetSonucDemoService.Update(eczaneNobetSonucDemo);
                return RedirectToAction("Index");
            }
            ViewBag.EczaneNobetGrupId = new SelectList(_eczaneNobetGrupService.GetList(), "Id", "Aciklama", eczaneNobetSonucDemo.EczaneNobetGrupId);
            ViewBag.TakvimId = new SelectList(_takvimService.GetList(), "Id", "Tarih", eczaneNobetSonucDemo.TakvimId);
            return View(eczaneNobetSonucDemo);
        }

        // GET: EczaneNobet/EczaneNobetSonucDemoe/Delete/5
        public ActionResult Delete(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EczaneNobetSonucDemoDetay2 eczaneNobetSonucDemo = _eczaneNobetSonucDemoService.GetDetayById(id);
            if (eczaneNobetSonucDemo == null)
            {
                return HttpNotFound();
            }
            return View(eczaneNobetSonucDemo);
        }

        // POST: EczaneNobet/EczaneNobetSonucDemoe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EczaneNobetSonucDemoDetay2 eczaneNobetSonucDemo = _eczaneNobetSonucDemoService.GetDetayById(id);
            _eczaneNobetSonucDemoService.Delete(id);
            return RedirectToAction("Index");
        }

    }
}
