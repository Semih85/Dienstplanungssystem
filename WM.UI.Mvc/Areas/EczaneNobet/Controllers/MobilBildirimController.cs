using System.Net;
using System.Web.Mvc;
using WM.Northwind.Business.Abstract.Authorization;
using WM.Northwind.Business.Abstract.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;
using WM.Northwind.Entities.Concrete.Authorization;
using WM.UI.Mvc.Areas.EczaneNobet.Models;
using WM.UI.Mvc.Services;
using System.Linq;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using System;

namespace WM.UI.Mvc.Areas.EczaneNobet.Controllers
{
    [Authorize]
    [HandleError]
    public class MobilBildirimController : Controller
    {
        #region ctor
        private IMobilBildirimService _mobilBildirimService;
        private IEczaneMobilBildirimService _eczaneMobilBildirimService;
        private INobetUstGrupService _nobetUstGrupService;
        private IUserService _userService;
        private IUserEczaneService _userEczaneService;
        private IUserNobetUstGrupService _userNobetUstGrupService;
        private INobetUstGrupSessionService _nobetUstGrupSessionService;

        public MobilBildirimController(IMobilBildirimService mobilBildirimService,
            IEczaneMobilBildirimService eczaneMobilBildirimService,
                                 INobetUstGrupService nobetUstGrupService,
                                 IUserService userService,
                                            IUserEczaneService userEczaneService,
                                            IUserNobetUstGrupService userNobetUstGrupService,
                                            INobetUstGrupSessionService nobetUstGrupSessionService)
        {
            _mobilBildirimService = mobilBildirimService;
            _eczaneMobilBildirimService = eczaneMobilBildirimService;
            _nobetUstGrupService = nobetUstGrupService;
            _userService = userService;
            _userEczaneService = userEczaneService;
            _userNobetUstGrupService = userNobetUstGrupService;
            _nobetUstGrupSessionService = nobetUstGrupSessionService;
        }
        #endregion
        // GET: EczaneNobet/MobilBildirim
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var model = _mobilBildirimService.GetDetaylar(nobetUstGrup.Id);

            return View(model);
        }

        // GET: EczaneNobet/MobilBildirim/Details/5
        [Authorize(Roles = "Admin,Oda,Üst Grup")]

        public ActionResult Details(int id)
        {
            return RedirectToAction("Index", "EczaneMobilBildirim", new { mobilBildirimId = id });
        }

        // GET: EczaneNobet/MobilBildirim/Create
        [Authorize(Roles = "Admin,Oda,Üst Grup")]

        public ActionResult Create()
        {
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            //var userNobetUstGrupDetaylar = _userNobetUstGrupService.GetDetaylar(nobetUstGrup.Id)
            // .Where(w => w.CihazId != null);// mobil uygulamayı yüklemeyene yani cihazId si null olan listeye hiç gelmesin.
            var userEczaneDetaylar = _userEczaneService.GetDetaylar(nobetUstGrup.Id).Where(w=>w.CihazId != null).ToList();

            //ViewBag.UserId = new SelectList(userNobetUstGrupDetaylar.Select(s => new { s.UserId, s.KullaniciAdi }), "UserId", "KullaniciAdi");
            ViewBag.EczaneId = new SelectList(userEczaneDetaylar.Select(s => new { s.EczaneId, s.EczaneAdi }), "EczaneId", "EczaneAdi");

            return View();
        }

        // POST: EczaneNobet/MobilBildirim/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Oda,Üst Grup")]
        public ActionResult Create([Bind(Include = "Baslik,Metin,EczaneId,Aciklama")] BildirimModel bildirimModel)
        {
            MobilBildirim mobilBildirim = new MobilBildirim();
            mobilBildirim.Baslik = bildirimModel.Baslik;
            mobilBildirim.Metin = bildirimModel.Metin;
            mobilBildirim.Aciklama = bildirimModel.Aciklama;
            mobilBildirim.GonderimTarihi = DateTime.Now;
            mobilBildirim.NobetUstGrupId = _nobetUstGrupSessionService.GetSession("nobetUstGrup").Id;
            _mobilBildirimService.Insert(mobilBildirim);

            int mobilBildirimId = _mobilBildirimService.GetDetaylarByNobetUstGrupGonderimTarihi(mobilBildirim.NobetUstGrupId, mobilBildirim.GonderimTarihi)
                .Select(s => s.Id).FirstOrDefault();

            if (bildirimModel.EczaneId != null)
            {
                foreach (var item in bildirimModel.EczaneId)
                {
                    if (item != null)
                    {
                        UserEczane userEczane = _userEczaneService.GetListByEczaneId(item).FirstOrDefault();
                        User User = _userService.GetById(userEczane.UserId);
                        if (User.CihazId != null)
                        {
                            PushNotification pushNotification = new PushNotification(bildirimModel.Metin, bildirimModel.Baslik, User.CihazId, mobilBildirimId.ToString());
                            EczaneMobilBildirim eczaneMobilBildirim = new EczaneMobilBildirim();

                            eczaneMobilBildirim.EczaneId = item;
                            eczaneMobilBildirim.BildirimGormeTarihi = null;
                            eczaneMobilBildirim.MobilBildirimId = mobilBildirimId;
                            _eczaneMobilBildirimService.Insert(eczaneMobilBildirim);

                        }
                    }
                }
                TempData["BildirimGonderilenKullanici"] = "Secilen eczanelere bildirim gönderilmiştir.";
            }
            else
            {
                TempData["BildirimGonderilenKullanici"] = "Eczane seçiniz.";
            }
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            //var userNobetUstGrupDetaylar = _userNobetUstGrupService.GetDetaylar(nobetUstGrup.Id)
            // .Where(w => w.CihazId != null);// mobil uygulamayı yüklemeyene yani cihazId si null olan listeye hiç gelmesin.

            var userEczaneDetaylar = _userEczaneService.GetDetaylar(nobetUstGrup.Id);

            //ViewBag.UserId = new SelectList(userNobetUstGrupDetaylar.Select(s => new { s.UserId, s.KullaniciAdi }), "UserId", "KullaniciAdi");
            ViewBag.EczaneId = new SelectList(userEczaneDetaylar.Select(s => new { s.EczaneId, s.EczaneAdi }), "EczaneId", "EczaneAdi");

            return View(bildirimModel);
        }

        // GET: EczaneNobet/MobilBildirim/Create
        [Authorize(Roles = "Admin,Oda,Üst Grup")]
        public ActionResult Gonder(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MobilBildirim mobilBildirim = _mobilBildirimService.GetById(id);
            if (mobilBildirim == null)
            {
                return HttpNotFound();
            }
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            BildirimModel bildirimModel = new BildirimModel();
            bildirimModel.Aciklama = mobilBildirim.Aciklama;
            bildirimModel.Baslik = mobilBildirim.Baslik;
            bildirimModel.Metin = mobilBildirim.Metin;
            bildirimModel.GonderimTarihi = mobilBildirim.GonderimTarihi;
            //var userNobetUstGrupDetaylar = _userNobetUstGrupService.GetDetaylar(nobetUstGrup.Id)
            // .Where(w => w.CihazId != null);// mobil uygulamayı yüklemeyene yani cihazId si null olan listeye hiç gelmesin.
            var userEczaneDetaylar = _userEczaneService.GetDetaylar(nobetUstGrup.Id).Where(w => w.CihazId != null).ToList();

            //ViewBag.UserId = new SelectList(userNobetUstGrupDetaylar.Select(s => new { s.UserId, s.KullaniciAdi }), "UserId", "KullaniciAdi");
            ViewBag.EczaneId = new SelectList(userEczaneDetaylar.Select(s => new { s.EczaneId, s.EczaneAdi }), "EczaneId", "EczaneAdi");

            return View("Create",bildirimModel);
        }
     
        // POST: EczaneNobet/MazeretTur/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Gonder([Bind(Include = "Baslik,Metin,EczaneId,Aciklama,GonderimTarihi")] BildirimModel bildirimModel)
        {//yeni gönderim zamanlı yeni mobil bildirim insert ve ardından eczaneMobilBildirimler insert
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            MobilBildirim mobilBildirim = new MobilBildirim();
            mobilBildirim.Baslik = bildirimModel.Baslik;
            mobilBildirim.Metin = bildirimModel.Metin;
            mobilBildirim.Aciklama = bildirimModel.Aciklama;
            mobilBildirim.GonderimTarihi = DateTime.Now;
            mobilBildirim.NobetUstGrupId = _nobetUstGrupSessionService.GetSession("nobetUstGrup").Id;
            _mobilBildirimService.Insert(mobilBildirim);

            int mobilBildirimId = _mobilBildirimService.GetDetaylarByNobetUstGrupGonderimTarihi(mobilBildirim.NobetUstGrupId, mobilBildirim.GonderimTarihi)
                .Select(s => s.Id).FirstOrDefault();

            if (bildirimModel.EczaneId != null)
            {
                foreach (var item in bildirimModel.EczaneId)
                {
                    if (item != null)
                    {
                        UserEczane userEczane = _userEczaneService.GetListByEczaneId(item).FirstOrDefault();
                        User User = _userService.GetById(userEczane.UserId);
                        if (User.CihazId != null)
                        {
                            PushNotification pushNotification = new PushNotification(bildirimModel.Metin, bildirimModel.Baslik, User.CihazId, mobilBildirimId.ToString());
                            EczaneMobilBildirim eczaneMobilBildirim = new EczaneMobilBildirim();

                            eczaneMobilBildirim.EczaneId = item;
                            eczaneMobilBildirim.BildirimGormeTarihi = null;
                            eczaneMobilBildirim.MobilBildirimId = mobilBildirimId;
                            _eczaneMobilBildirimService.Insert(eczaneMobilBildirim);

                        }
                    }
                }
                TempData["BildirimGonderilenKullanici"] = "Secilen eczanelere bildirim gönderilmiştir.";

            }
            else
            {
                TempData["BildirimGonderilenKullanici"] = "Eczane seçiniz.";
            }

            //var userNobetUstGrupDetaylar = _userNobetUstGrupService.GetDetaylar(nobetUstGrup.Id)
            // .Where(w => w.CihazId != null);// mobil uygulamayı yüklemeyene yani cihazId si null olan listeye hiç gelmesin.

            var userEczaneDetaylar = _userEczaneService.GetDetaylar(nobetUstGrup.Id);

            //ViewBag.UserId = new SelectList(userNobetUstGrupDetaylar.Select(s => new { s.UserId, s.KullaniciAdi }), "UserId", "KullaniciAdi");
            ViewBag.EczaneId = new SelectList(userEczaneDetaylar.Select(s => new { s.EczaneId, s.EczaneAdi }), "EczaneId", "EczaneAdi");

            return View("Create", bildirimModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GonderEskiBildirimZamaniIleAyniBildirimi([Bind(Include = "Baslik,Metin,EczaneId,Aciklama,GonderimTarihi")] BildirimModel bildirimModel)
        {
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");
            int mobilBildirimId = _mobilBildirimService.GetDetaylarByNobetUstGrupGonderimTarihi(nobetUstGrup.Id, bildirimModel.GonderimTarihi)
               .Select(s => s.Id).FirstOrDefault();
            if (bildirimModel.EczaneId != null)
            {
                foreach (var item in bildirimModel.EczaneId)
                {
                    if (item != null)
                    {
                        UserEczane userEczane = _userEczaneService.GetListByEczaneId(item).FirstOrDefault();
                        User User = _userService.GetById(userEczane.UserId);
                        if (User.CihazId != null)
                        {
                            PushNotification pushNotification = new PushNotification(bildirimModel.Metin, bildirimModel.Baslik, User.CihazId, mobilBildirimId.ToString());
                            EczaneMobilBildirim eczaneMobilBildirim = new EczaneMobilBildirim();

                            eczaneMobilBildirim.EczaneId = item;
                            eczaneMobilBildirim.BildirimGormeTarihi = null;
                            eczaneMobilBildirim.MobilBildirimId = mobilBildirimId;
                            _eczaneMobilBildirimService.Insert(eczaneMobilBildirim);

                        }
                    }
                }
                TempData["BildirimGonderilenKullanici"] = "Secilen eczanelere bildirim gönderilmiştir.";

            }
            else
            {
                TempData["BildirimGonderilenKullanici"] = "Eczane seçiniz.";
            }

            //var userNobetUstGrupDetaylar = _userNobetUstGrupService.GetDetaylar(nobetUstGrup.Id)
            // .Where(w => w.CihazId != null);// mobil uygulamayı yüklemeyene yani cihazId si null olan listeye hiç gelmesin.

            var userEczaneDetaylar = _userEczaneService.GetDetaylar(nobetUstGrup.Id);

            //ViewBag.UserId = new SelectList(userNobetUstGrupDetaylar.Select(s => new { s.UserId, s.KullaniciAdi }), "UserId", "KullaniciAdi");
            ViewBag.EczaneId = new SelectList(userEczaneDetaylar.Select(s => new { s.EczaneId, s.EczaneAdi }), "EczaneId", "EczaneAdi");

            return View("Create", bildirimModel);
        }


    }
}
