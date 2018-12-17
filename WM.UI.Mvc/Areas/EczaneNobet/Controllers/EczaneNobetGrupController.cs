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
using WM.UI.Mvc.Models;

namespace WM.UI.Mvc.Areas.EczaneNobet.Controllers
{
    [HandleError]
    [Authorize]
    public class EczaneNobetGrupController : Controller
    {
        #region ctor
        private IEczaneNobetGrupService _eczaneNobetGrupService;
        private IEczaneService _eczaneService;
        private INobetGrupGorevTipService _nobetGrupGorevTipService;
        private IUserService _userService;
        private INobetUstGrupService _nobetUstGrupService;
        private IAyniGunTutulanNobetService _ayniGunTutulanNobetService;

        public EczaneNobetGrupController(IEczaneNobetGrupService eczaneNobetGrupService,
                                         IEczaneService eczaneService,
                                         INobetGrupGorevTipService nobetGrupGorevTipService,
                                         INobetUstGrupService nobetUstGrupService,
                                         IUserService userService,
                                         IAyniGunTutulanNobetService ayniGunTutulanNobetService)
        {
            _eczaneNobetGrupService = eczaneNobetGrupService;
            _eczaneService = eczaneService;
            _nobetGrupGorevTipService = nobetGrupGorevTipService;
            _userService = userService;
            _nobetUstGrupService = nobetUstGrupService;
            _ayniGunTutulanNobetService = ayniGunTutulanNobetService;
        }
        #endregion

        // GET: EczaneNobet/EczaneNobetGrup
        public ActionResult Index()
        {
            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetListByUser(user).Select(s => s.Id).ToList();

            var model = _eczaneNobetGrupService.GetDetaylarByNobetGrupGorevTipler(nobetGrupGorevTipler)
                .OrderBy(s => s.NobetGorevTipId)
                .ThenBy(s => s.NobetGrupAdi)
                .ThenBy(s => s.EczaneAdi).ToList();

            return View(model);
        }

        // GET: EczaneNobet/EczaneNobetGrup/Details/5
        public ActionResult Details(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EczaneNobetGrupDetay eczaneNobetGrup = _eczaneNobetGrupService.GetDetayById(id);
            if (eczaneNobetGrup == null)
            {
                return HttpNotFound();
            }
            return View(eczaneNobetGrup);
        }

        // GET: EczaneNobet/EczaneNobetGrup/Create
        [Authorize(Roles = "Admin,Oda,Üst Grup")]
        public ActionResult Create()
        {
            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGrup = _nobetUstGrupService.GetListByUser(user).FirstOrDefault();

            //var eczaneNobetGruplar = _eczaneNobetGrupService.GetDetaylar(nobetUstGrup.Id)
            //    //.Where(w => w.BitisTarihi == null || w.nobet)
            //    .Select(s => s.EczaneId).Distinct().ToList();

            var eczaneler = _eczaneService.GetDetaylar(nobetUstGrup.Id)
                .Where(s => s.KapanisTarihi == null
                        //&& !eczaneNobetGruplar.Contains(s.Id)
                        )
                .OrderBy(s => s.EczaneAdi).ToList();

            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrup.Id);

            var eczaneNobetGrupCoklu = new EczaneNobetGrupCoklu();

            if (TempData["EklenecekEczane"] != null)
            {
                eczaneNobetGrupCoklu = (EczaneNobetGrupCoklu)TempData["EklenecekEczane"];

                ViewBag.EczaneId = new SelectList(eczaneler, "Id", "EczaneAdi", eczaneNobetGrupCoklu.EczaneId.FirstOrDefault());
            }
            else
            {
                ViewBag.EczaneId = new SelectList(eczaneler, "Id", "EczaneAdi");
            }

            ViewBag.NobetGrupGorevTipId = new SelectList(nobetGrupGorevTipler, "Id", "NobetGrupGorevTipAdi");

            //var baslamaTarihi = new EczaneNobetGrup { BaslangicTarihi = DateTime.Now, Aciklama = "..... eczane bu gruba yeni katıldı." };
            return View(eczaneNobetGrupCoklu);
        }

        public ActionResult Search(string Keywords)
        {
            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetGruplar = _nobetGrupGorevTipService.GetListByUser(user);
            var model = _eczaneNobetGrupService.GetDetaylar()
                .Where(x => nobetGruplar.Select(s => s.Id).Contains(x.NobetGrupId)
                            && Regex.Split(Keywords, @"\s")
                            .Any(y => x.EczaneAdi.ToLower().Contains(y.ToLower())
                                   || x.NobetGrupAdi.ToLower().Contains(y.ToLower())
                            )
                            )
                .OrderBy(s => s.NobetGrupAdi).ThenBy(s => s.EczaneAdi);

            return View("Index", model);//result:model
        }

        // POST: EczaneNobet/EczaneNobetGrup/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Oda,Üst Grup")]
        public ActionResult Create([Bind(Include = "Id,EczaneId,NobetGrupGorevTipId,BaslangicTarihi,BitisTarihi,Aciklama")] EczaneNobetGrupCoklu eczaneNobetGrupCoklu)
        {
            var eczaneNobetGruplar = new List<EczaneNobetGrup>();

            foreach (var eczaneId in eczaneNobetGrupCoklu.EczaneId)
            {
                eczaneNobetGruplar.Add(new EczaneNobetGrup
                {
                    EczaneId = eczaneId,
                    NobetGrupGorevTipId = eczaneNobetGrupCoklu.NobetGrupGorevTipId,
                    BaslangicTarihi = eczaneNobetGrupCoklu.BaslangicTarihi,
                    BitisTarihi = eczaneNobetGrupCoklu.BitisTarihi,
                    Aciklama = eczaneNobetGrupCoklu.Aciklama
                });
            }

            var eklenecekEczaneSayisi = eczaneNobetGruplar.Count;

            if (ModelState.IsValid && eklenecekEczaneSayisi > 0)
            {
                TempData["EklenenEczane"] = $"{_nobetGrupGorevTipService.GetDetayById(eczaneNobetGrupCoklu.NobetGrupGorevTipId).NobetGrupGorevTipAdi} nöbet grubuna {eczaneNobetGrupCoklu.EczaneId.Count()} adet eczane başarılı bir şekilde eklenmiştir.";

                _eczaneNobetGrupService.CokluEkle(eczaneNobetGruplar);

                var nobetGrupGorevTip = _nobetGrupGorevTipService.GetDetayById(eczaneNobetGrupCoklu.NobetGrupGorevTipId);
                var nobetUstGrupId = nobetGrupGorevTip.NobetUstGrupId;

                if (nobetUstGrupId == 1 || nobetUstGrupId == 3)
                {//anlanya ya da mersin için
                    var eczaneIdList = eczaneNobetGruplar.Select(s => s.EczaneId).ToList();
                    var eczaneNobetGrupDetaylar = _eczaneNobetGrupService.GetDetaylar(eczaneIdList, eczaneNobetGrupCoklu.NobetGrupGorevTipId);
                    var eklenenIkiliEczaneler = _ayniGunTutulanNobetService.IkiliEczaneleriOlustur(eczaneNobetGrupDetaylar);
                }

                ViewBag.EklenenEczaneSayisi = eklenecekEczaneSayisi;
                ViewBag.EklenenNobetGrupAdi = _nobetGrupGorevTipService.GetDetayById(eczaneNobetGrupCoklu.NobetGrupGorevTipId).NobetGrupAdi;
                return RedirectToAction("Index");
            }

            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGrup = _nobetUstGrupService.GetListByUser(user).FirstOrDefault();
            var nobetGruplar = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrup.Id);
            var eczaneler = _eczaneService.GetList(nobetUstGrup.Id).OrderBy(s => s.Adi).ToList();

            ViewBag.EczaneId = new SelectList(eczaneler, "Id", "Adi", eczaneNobetGrupCoklu.EczaneId);
            ViewBag.NobetGrupGorevTipId = new SelectList(nobetGruplar, "Id", "NobetGrupGorevTipAdi", eczaneNobetGrupCoklu.NobetGrupGorevTipId);
            return View(); //eczaneNobetGrup
        }

        // GET: EczaneNobet/EczaneNobetGrup/Edit/5
        [Authorize(Roles = "Admin,Oda,Üst Grup")]
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EczaneNobetGrup eczaneNobetGrup = _eczaneNobetGrupService.GetById(id);
            if (eczaneNobetGrup == null)
            {
                return HttpNotFound();
            }
            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGrup = _nobetUstGrupService.GetListByUser(user).FirstOrDefault();
            var nobetGruplar = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrup.Id);
            var eczaneler = _eczaneService.GetList(nobetUstGrup.Id).OrderBy(s => s.Adi).ToList();

            ViewBag.EczaneId = new SelectList(eczaneler, "Id", "Adi", eczaneNobetGrup.EczaneId);
            ViewBag.NobetGrupGorevTipId = new SelectList(nobetGruplar, "Id", "NobetGrupGorevTipAdi", eczaneNobetGrup.NobetGrupGorevTipId);
            return View(eczaneNobetGrup);
        }

        // POST: EczaneNobet/EczaneNobetGrup/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Oda,Üst Grup")]
        public ActionResult Edit([Bind(Include = "Id,EczaneId,NobetGrupGorevTipId,BaslangicTarihi,BitisTarihi,Aciklama")] EczaneNobetGrup eczaneNobetGrup)
        {
            if (ModelState.IsValid)
            {
                _eczaneNobetGrupService.Update(eczaneNobetGrup);
                return RedirectToAction("Index");
            }
            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGrup = _nobetUstGrupService.GetListByUser(user).FirstOrDefault();
            var nobetGruplar = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrup.Id);
            var eczaneler = _eczaneService.GetList(nobetUstGrup.Id).OrderBy(s => s.Adi).ToList();

            ViewBag.EczaneId = new SelectList(eczaneler, "Id", "Adi", eczaneNobetGrup.EczaneId);
            ViewBag.NobetGrupGorevTipId = new SelectList(nobetGruplar, "Id", "NobetGrupGorevTipAdi", eczaneNobetGrup.NobetGrupGorevTipId);
            return View(eczaneNobetGrup);
        }

        // GET: EczaneNobet/EczaneNobetGrup/Delete/5
        [Authorize(Roles = "Admin,Oda")]
        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EczaneNobetGrupDetay eczaneNobetGrup = _eczaneNobetGrupService.GetDetayById(id);
            if (eczaneNobetGrup == null)
            {
                return HttpNotFound();
            }
            return View(eczaneNobetGrup);
        }

        // POST: EczaneNobet/EczaneNobetGrup/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EczaneNobetGrupDetay eczaneNobetGrup = _eczaneNobetGrupService.GetDetayById(id);
            _eczaneNobetGrupService.Delete(id);
            return RedirectToAction("Index");
        }

    }
}
