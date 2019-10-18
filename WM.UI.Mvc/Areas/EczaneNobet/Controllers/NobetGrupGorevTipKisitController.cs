using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WM.Northwind.Business.Abstract.Authorization;
using WM.Northwind.Business.Abstract.EczaneNobet;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;
using WM.UI.Mvc.Models;
using WM.UI.Mvc.Services;

namespace WM.UI.Mvc.Areas.EczaneNobet.Controllers
{
    [Authorize]
    [HandleError]
    public class NobetGrupGorevTipKisitController : Controller
    {
        private INobetGrupGorevTipKisitService _nobetGrupGorevTipKisitService;
        private INobetUstGrupService _nobetUstGrupService;
        private INobetUstGrupKisitService _nobetUstGrupKisitService;
        private INobetGrupGorevTipService _nobetGrupGorevTipService;
        private IUserService _userService;
        private INobetUstGrupSessionService _nobetUstGrupSessionService;
        private INobetUstGrupKisitSessionService _nobetUstGrupKisitSessionService;

        public NobetGrupGorevTipKisitController(INobetGrupGorevTipKisitService nobetGrupGorevTipKisitService,
            INobetUstGrupService nobetUstGrupService,
            IUserService userService,
            INobetUstGrupKisitService nobetUstGrupKisitService,
            INobetGrupGorevTipService nobetGrupGorevTipService,
            INobetUstGrupSessionService nobetUstGrupSessionService,
            INobetUstGrupKisitSessionService nobetUstGrupKisitSessionService)
        {
            _nobetGrupGorevTipKisitService = nobetGrupGorevTipKisitService;
            _nobetUstGrupService = nobetUstGrupService;
            _userService = userService;
            _nobetUstGrupKisitService = nobetUstGrupKisitService;
            _nobetGrupGorevTipService = nobetGrupGorevTipService;
            _nobetUstGrupSessionService = nobetUstGrupSessionService;
            _nobetUstGrupKisitSessionService = nobetUstGrupKisitSessionService;
        }

        // GET: EczaneNobet/NobetGrupGorevTipKisit
        public ActionResult Index()
        {
            //var user = _userService.GetByUserName(User.Identity.Name);

            //var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            //var nobetUstGrup = nobetUstGruplar.FirstOrDefault();
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var nobetGrupGorevTipKisitlar = _nobetGrupGorevTipKisitService.GetDetaylar(nobetUstGrup.Id)
                .OrderByDescending(o => o.Id).ToList();

            return View(nobetGrupGorevTipKisitlar);
        }

        // GET: EczaneNobet/NobetGrupGorevTipKisit/Details/5
        public ActionResult Details(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var nobetGrupGorevTipKisit = _nobetGrupGorevTipKisitService.GetDetayById(id);
            if (nobetGrupGorevTipKisit == null)
            {
                return HttpNotFound();
            }
            return View(nobetGrupGorevTipKisit);
        }

        // GET: EczaneNobet/NobetGrupGorevTipKisit/Create
        public ActionResult Create()
        {
            //var user = _userService.GetByUserName(User.Identity.Name);

            //var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            //var nobetUstGrup = nobetUstGruplar.FirstOrDefault();

            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrup.Id);
            var nobetUstGrupKisitlar = _nobetUstGrupKisitService.GetDetaylar(nobetUstGrup.Id);

            ViewBag.NobetGrupGorevTipId = new SelectList(nobetGrupGorevTipler, "Id", "NobetGrupGorevTipAdi");
            ViewBag.NobetUstGrupKisitId = new SelectList(nobetUstGrupKisitlar, "Id", "KisitAdiUzun");
            return View();
        }

        public ActionResult Create2(int kisitId)
        {
            //var user = _userService.GetByUserName(User.Identity.Name);

            //var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            //var nobetUstGrup = nobetUstGruplar.FirstOrDefault();

            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrup.Id);
            var nobetUstGrupKisitlar = _nobetUstGrupKisitService.GetDetaylar(nobetUstGrup.Id)
                .Where(w => w.KisitId == kisitId).ToList();

            var nobetUstGrupKisit = nobetUstGrupKisitlar.SingleOrDefault(x => x.KisitId == kisitId);

            var nobetGrupGorevTipKisit = new NobetGrupGorevTipKisitDetay
            {
                SagTarafDegeri = nobetUstGrupKisit.SagTarafDegeri,
                PasifMi = nobetUstGrupKisit.PasifMi
            };

            ViewBag.NobetGrupGorevTipId = new SelectList(nobetGrupGorevTipler, "Id", "NobetGrupGorevTipAdi");
            ViewBag.NobetUstGrupKisitId = new SelectList(nobetUstGrupKisitlar, "Id", "KisitAdiUzun", nobetUstGrupKisit.Id);

            return View("Create", nobetGrupGorevTipKisit);
        }

        // POST: EczaneNobet/NobetGrupGorevTipKisit/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NobetUstGrupKisitId,NobetGrupGorevTipId,PasifMi,SagTarafDegeri,VarsayilanPasifMi,SagTarafDegeriVarsayilan,Aciklama")] NobetGrupGorevTipKisit nobetGrupGorevTipKisit)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var nobetUstGrupKisit = _nobetUstGrupKisitService.GetById(nobetGrupGorevTipKisit.NobetUstGrupKisitId);

                    var nobetGrupGorevTip = _nobetGrupGorevTipService.GetDetayById(nobetGrupGorevTipKisit.NobetGrupGorevTipId);

                    var nobetUstGrupId = nobetUstGrupKisit.NobetUstGrupId;

                    var kisitOnce = _nobetUstGrupKisitService.GetDetay(nobetUstGrupKisit.KisitId, nobetUstGrupId);

                    kisitOnce.KisitKategoriAdi += $"_{nobetGrupGorevTip.NobetGrupAdi}";

                    var nobetUstGrupKisitSession = _nobetUstGrupKisitSessionService.GetSessionList("nobetUstGrupKisitSession", nobetUstGrupId);

                    _nobetGrupGorevTipKisitService.Insert(nobetGrupGorevTipKisit);

                    var kisitSonra = _nobetUstGrupKisitService.GetDetay(nobetUstGrupKisit.KisitId, nobetUstGrupId);

                    _nobetUstGrupKisitSessionService.AddSessionList(kisitOnce, kisitSonra, "nobetUstGrupKisitSession", nobetUstGrupKisitSession);

                    nobetUstGrupKisitSession = _nobetUstGrupKisitSessionService.GetSessionList("nobetUstGrupKisitSession", kisitOnce.NobetUstGrupId);
                }
                catch (DbUpdateException ex)
                {
                    var hata = ex.InnerException.ToString();

                    string[] dublicateHata = { "Cannot insert dublicate row in object", "with unique index" };

                    var dublicateRowHatasiMi = dublicateHata.Any(h => hata.Contains(h));

                    if (dublicateRowHatasiMi)
                    {
                        throw new Exception("<strong>Bir Nöbet Grubu için iki kural kaydı eklenemez...</strong>");
                    }

                    throw ex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return RedirectToAction("Index");
            }
            //var user = _userService.GetByUserName(User.Identity.Name);

            //var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            //var nobetUstGrup = nobetUstGruplar.FirstOrDefault();
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrup.Id);
            var nobetUstGrupKisitlar = _nobetUstGrupKisitService.GetDetaylar(nobetUstGrup.Id);

            ViewBag.NobetGrupGorevTipId = new SelectList(nobetGrupGorevTipler, "Id", "NobetGrupGorevTipAdi", nobetGrupGorevTipKisit.NobetGrupGorevTipId);
            ViewBag.NobetUstGrupKisitId = new SelectList(nobetUstGrupKisitlar, "Id", "KisitAdiUzun", nobetGrupGorevTipKisit.NobetUstGrupKisitId);

            return View(nobetGrupGorevTipKisit);
        }

        // GET: EczaneNobet/NobetGrupGorevTipKisit/Edit/5
        public ActionResult Edit(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var nobetGrupGorevTipKisit = _nobetGrupGorevTipKisitService.GetDetayById(id);
            if (nobetGrupGorevTipKisit == null)
            {
                return HttpNotFound();
            }
            //var user = _userService.GetByUserName(User.Identity.Name);

            //var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            //var nobetUstGrup = nobetUstGruplar.FirstOrDefault();
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrup.Id);
            var nobetUstGrupKisitlar = _nobetUstGrupKisitService.GetDetaylar(nobetUstGrup.Id);

            ViewBag.NobetGrupGorevTipId = new SelectList(nobetGrupGorevTipler, "Id", "NobetGrupGorevTipAdi", nobetGrupGorevTipKisit.NobetGrupGorevTipId);
            ViewBag.NobetUstGrupKisitId = new SelectList(nobetUstGrupKisitlar, "Id", "KisitAdiUzun", nobetGrupGorevTipKisit.NobetUstGrupKisitId);
            return View(nobetGrupGorevTipKisit);
        }

        // POST: EczaneNobet/NobetGrupGorevTipKisit/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NobetUstGrupKisitId,NobetGrupGorevTipId,PasifMi,SagTarafDegeri,VarsayilanPasifMi,SagTarafDegeriVarsayilan,Aciklama")] NobetGrupGorevTipKisit nobetGrupGorevTipKisit)
        {
            if (ModelState.IsValid)
            {
                var kisitOnce = _nobetGrupGorevTipKisitService.GetDetayById(nobetGrupGorevTipKisit.Id);

                var kisitOnceGrupBazli = GetNobetUstGrupKisitDetay(kisitOnce);

                var nobetUstGrupKisitSession = _nobetUstGrupKisitSessionService.GetSessionList("nobetUstGrupKisitSession", kisitOnce.NobetUstGrupId);

                _nobetGrupGorevTipKisitService.Update(nobetGrupGorevTipKisit);

                var kisitSonra = _nobetGrupGorevTipKisitService.GetDetayById(kisitOnce.Id);

                var kisitSonraGrupBazli = GetNobetUstGrupKisitDetay(kisitSonra);

                _nobetUstGrupKisitSessionService.AddSessionList(kisitOnceGrupBazli, kisitSonraGrupBazli, "nobetUstGrupKisitSession", nobetUstGrupKisitSession);

                nobetUstGrupKisitSession = _nobetUstGrupKisitSessionService.GetSessionList("nobetUstGrupKisitSession", kisitOnce.NobetUstGrupId);

                return RedirectToAction("Index");
            }
            //var user = _userService.GetByUserName(User.Identity.Name);

            //var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            //var nobetUstGrup = nobetUstGruplar.FirstOrDefault();
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrup.Id);
            var nobetUstGrupKisitlar = _nobetUstGrupKisitService.GetDetaylar(nobetUstGrup.Id);

            ViewBag.NobetGrupGorevTipId = new SelectList(nobetGrupGorevTipler, "Id", "NobetGrupGorevTipAdi", nobetGrupGorevTipKisit.NobetGrupGorevTipId);
            ViewBag.NobetUstGrupKisitId = new SelectList(nobetUstGrupKisitlar, "Id", "KisitAdiUzun", nobetGrupGorevTipKisit.NobetUstGrupKisitId);

            return View(nobetGrupGorevTipKisit);
        }

        private static NobetUstGrupKisitDetay GetNobetUstGrupKisitDetay(NobetGrupGorevTipKisitDetay kisitOnce)
        {
            var kisitOnceGrupBazli = new NobetUstGrupKisitDetay
            {
                NobetUstGrupId = kisitOnce.NobetUstGrupId,
                KisitId = kisitOnce.KisitId,
                SagTarafDegeri = kisitOnce.SagTarafDegeri,
                SagTarafDegeriVarsayilan = kisitOnce.SagTarafDegeriVarsayilan,
                PasifMi = kisitOnce.PasifMi,
                VarsayilanPasifMi = kisitOnce.VarsayilanPasifMi,
                DegerPasifMi = kisitOnce.DegerPasifMi,
                Id = kisitOnce.NobetUstGrupKisitId,
                KisitAdi = kisitOnce.KisitAdi,
                KisitAciklama = kisitOnce.KisitAciklama,
                KisitAdiGosterilen = kisitOnce.KisitAdiGosterilen,
                KisitKategoriAdi = $"{kisitOnce.KisitKategoriAdi}_{kisitOnce.NobetGrupAdi}",
                KisitKategoriId = kisitOnce.KisitKategoriId
                //KisitAdiGosterilenKisa = kisitOnce.KisitAdiGosterilen
            };
            return kisitOnceGrupBazli;
        }

        // GET: EczaneNobet/NobetGrupGorevTipKisit/Delete/5
        public ActionResult Delete(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var nobetGrupGorevTipKisit = _nobetGrupGorevTipKisitService.GetDetayById(id);
            if (nobetGrupGorevTipKisit == null)
            {
                return HttpNotFound();
            }
            return View(nobetGrupGorevTipKisit);
        }

        // POST: EczaneNobet/NobetGrupGorevTipKisit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var nobetGrupGorevTipKisit = _nobetGrupGorevTipKisitService.GetById(id);

            var nobetUstGrupKisit = _nobetUstGrupKisitService.GetById(nobetGrupGorevTipKisit.NobetUstGrupKisitId);

            var nobetUstGrupId = nobetUstGrupKisit.NobetUstGrupId;

            var kisitOnce = _nobetUstGrupKisitService.GetDetay(nobetUstGrupKisit.KisitId, nobetUstGrupId);

            var nobetUstGrupKisitSession = _nobetUstGrupKisitSessionService.GetSessionList("nobetUstGrupKisitSession", kisitOnce.NobetUstGrupId);

            _nobetGrupGorevTipKisitService.Delete(id);

            var kisitSonra = _nobetUstGrupKisitService.GetDetay(nobetUstGrupKisit.KisitId, nobetUstGrupId);

            _nobetUstGrupKisitSessionService.AddSessionList(kisitOnce, kisitSonra, "nobetUstGrupKisitSession", nobetUstGrupKisitSession);

            nobetUstGrupKisitSession = _nobetUstGrupKisitSessionService.GetSessionList("nobetUstGrupKisitSession", kisitOnce.NobetUstGrupId);

            return RedirectToAction("Index");
        }


    }
}
