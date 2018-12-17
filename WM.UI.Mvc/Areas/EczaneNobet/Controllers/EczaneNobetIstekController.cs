using System;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WM.Northwind.Business.Abstract.Authorization;
using WM.Northwind.Business.Abstract.EczaneNobet;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.UI.Mvc.Areas.EczaneNobet.Controllers
{
    [HandleError]
    [Authorize]
    public class EczaneNobetIstekController : Controller
    {
        #region ctor
        private IEczaneNobetIstekService _eczaneNobetIstekService;
        private IEczaneNobetGrupService _eczaneNobetGrupService;
        private IEczaneService _eczaneService;
        private IIstekService _istekService;
        private ITakvimService _takvimService;
        private IUserService _userService;
        private IEczaneGrupService _eczaneGrupService;

        public EczaneNobetIstekController(IEczaneNobetIstekService eczaneNobetIstekService,
                                          IEczaneNobetGrupService eczaneNobetGrupService,
                                          IEczaneService eczaneService,
                                          IIstekService istekService,
                                          ITakvimService takvimService,
                                          IUserService userService,
                                          IEczaneGrupService eczaneGrupService)
        {
            _eczaneNobetIstekService = eczaneNobetIstekService;
            _eczaneNobetGrupService = eczaneNobetGrupService;
            _eczaneService = eczaneService;
            _istekService = istekService;
            _takvimService = takvimService;
            _userService = userService;
            _eczaneGrupService = eczaneGrupService;
        }
        #endregion

        // GET: EczaneNobet/EczaneNobetIstek
        public ActionResult Index()
        {
            //var eczaneNobetMazerets = db.EczaneNobetMazerets.Include(e => e.Eczane).Include(e => e.Mazeret).Include(e => e.Takvim);
            //eczaneNobetMazerets.ToList()
            var user = _userService.GetByUserName(User.Identity.Name);
            var eczaneler = _eczaneService.GetListByUser(user).Select(s => s.Id);

            var model = _eczaneNobetIstekService.GetDetaylar()
                .Where(s => eczaneler.Contains(s.EczaneId))
                .OrderByDescending(o => o.Tarih).ThenBy(f => f.EczaneAdi);

            return View(model);
        }

        // GET: EczaneNobet/EczaneNobetIstek/Details/5
        public ActionResult Details(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var eczaneNobetIstek = _eczaneNobetIstekService.GetById(id);
            var eczaneNobetIstekDetay = _eczaneNobetIstekService.GetDetaylar().Where(s => s.Id == eczaneNobetIstek.Id).SingleOrDefault();

            if (eczaneNobetIstek == null)
            {
                return HttpNotFound();
            }
            return View(eczaneNobetIstekDetay);
        }

        // GET: EczaneNobet/EczaneNobetIstek/Create
        public ActionResult Create()
        {
            var user = _userService.GetByUserName(User.Identity.Name);

            //var yil = DateTime.Now.AddMonths(1).Year;
            var bugun = DateTime.Today;
            var tarihKriter = bugun.AddMonths(-3);

            var tarihler = _takvimService.GetDetaylar(tarihKriter)
                            .Select(s => new MyDrop
                            {
                                Id = s.TakvimId,
                                Value = s.Tarih.ToLongDateString()
                            });

            var bugunTakvim = _takvimService.GetByTarih(DateTime.Today);

            var eczaneler = _eczaneService.GetListByUser(user).Select(s => s.Id).ToList();
            ViewBag.EczaneNobetGrupId = new SelectList(_eczaneNobetGrupService.GetDetaylarByEczaneIdList(eczaneler)
                .Select(s => new MyDrop { Id = s.Id, Value = $"{s.EczaneAdi}, {s.NobetGrupGorevTipAdi}" }).OrderBy(s => s.Value), "Id", "Value");
            ViewBag.IstekId = new SelectList(_istekService.GetList(), "Id", "Adi");
            ViewBag.TakvimId = new SelectList(tarihler, "Id", "Value", bugunTakvim.Id);
            return View();
        }

        // POST: EczaneNobet/EczaneNobetIstek/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EczaneNobetGrupId,IstekId,TakvimId,Aciklama")] EczaneNobetIstek eczaneNobetIstek)
        {
            var user = _userService.GetByUserName(User.Identity.Name);
            var eczaneNobetGrup = _eczaneNobetGrupService.GetDetayById(eczaneNobetIstek.EczaneNobetGrupId);
            var eczaneler = _eczaneService.GetListByUser(user).Select(s => s.Id).ToList();
            var tarihler = _takvimService.GetList()
                //.Where(w => w.Tarih.Year < 2020//== yil
                //                               //&& w.Tarih.Month == ay
                //         )
                .Select(s => new MyDrop { Id = s.Id, Value = s.Tarih.ToLongDateString() });
            ViewBag.EczaneNobetGrupId = new SelectList(_eczaneNobetGrupService.GetDetaylarByEczaneIdList(eczaneler)
                .Select(s => new MyDrop { Id = s.Id, Value = $"{s.EczaneAdi}, {s.NobetGrupGorevTipAdi}" }).OrderBy(s => s.Value), "Id", "Value", eczaneNobetIstek.EczaneNobetGrupId);
            ViewBag.IstekId = new SelectList(_istekService.GetList(), "Id", "Adi", eczaneNobetIstek.IstekId);
            ViewBag.TakvimId = new SelectList(tarihler, "Id", "Value", eczaneNobetIstek.TakvimId);

            if (ModelState.IsValid)
            {
                var istekGirilenEczaneninEsOlduguEczaneler = _eczaneGrupService.GetDetaylarEczaneninEsOlduguEczaneler(eczaneNobetGrup.Id);
                var istekGirilenTarihtekiEczaneler = _eczaneNobetIstekService.GetDetaylarByTakvimId(eczaneNobetIstek.TakvimId, eczaneNobetGrup.NobetUstGrupId);

                var istekGirilenTarihtekiEsgrupOlduguEczaneler = _eczaneNobetIstekService.GetDetaylar(istekGirilenTarihtekiEczaneler, istekGirilenEczaneninEsOlduguEczaneler);

                var istekGirilenTarihtekiEsgrupOlduguEczaneSayisi = istekGirilenTarihtekiEsgrupOlduguEczaneler.Count;

                if (istekGirilenTarihtekiEsgrupOlduguEczaneSayisi > 0)
                {
                    ViewBag.IstekGirilenTarihtekiEsgrupOlduguEczaneler = istekGirilenTarihtekiEsgrupOlduguEczaneler;
                    return View(eczaneNobetIstek);
                }
                else
                {
                    try
                    {
                        _eczaneNobetIstekService.Insert(eczaneNobetIstek);
                    }
                    catch (DbUpdateException ex)
                    {
                        var hata = ex.InnerException.ToString();

                        string[] dublicateHata = { "Cannot insert dublicate row in object", "with unique index" };

                        var dublicateRowHatasiMi = dublicateHata.Any(h => hata.Contains(h));

                        if (dublicateRowHatasiMi)
                        {
                            throw new Exception("<strong>Bir eczaneye aynı gün için iki istek kaydı eklenemez...</strong>");
                        }

                        throw ex;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(eczaneNobetIstek);
        }

        // GET: EczaneNobet/EczaneNobetIstek/Edit/5
        public ActionResult Edit(int id)
        {
            var user = _userService.GetByUserName(User.Identity.Name);

            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EczaneNobetIstek eczaneNobetIstek = _eczaneNobetIstekService.GetById(id);
            if (eczaneNobetIstek == null)
            {
                return HttpNotFound();
            }

            var yil = DateTime.Now.AddMonths(1).Year;
            var ay = DateTime.Now.AddMonths(1).Month;

            var tarihler = _takvimService.GetList()
                            //.Where(w => w.Tarih.Year == yil
                            //         //&& w.Tarih.Month == ay
                            //         )
                            .Select(s => new MyDrop { Id = s.Id, Value = s.Tarih.ToLongDateString() });

            var eczaneler = _eczaneService.GetListByUser(user).Select(s => s.Id).ToList();
            ViewBag.EczaneNobetGrupId = new SelectList(_eczaneNobetGrupService.GetDetaylarByEczaneIdList(eczaneler)
                .Select(s => new MyDrop { Id = s.Id, Value = $"{s.EczaneAdi}, {s.NobetGrupGorevTipAdi}" }).OrderBy(s => s.Value), "Id", "Value", eczaneNobetIstek.EczaneNobetGrupId);
            ViewBag.IstekId = new SelectList(_istekService.GetList(), "Id", "Adi", eczaneNobetIstek.IstekId);
            ViewBag.TakvimId = new SelectList(tarihler, "Id", "Value", eczaneNobetIstek.TakvimId);
            return View(eczaneNobetIstek);
        }

        // POST: EczaneNobet/EczaneNobetIstek/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EczaneNobetGrupId,IstekId,TakvimId,Aciklama")] EczaneNobetIstek eczaneNobetIstek)
        {
            var user = _userService.GetByUserName(User.Identity.Name);

            if (ModelState.IsValid)
            {
                _eczaneNobetIstekService.Update(eczaneNobetIstek);
                return RedirectToAction("Index");
            }
            var eczaneler = _eczaneService.GetListByUser(user).Select(s => s.Id).ToList();
            var tarihler = _takvimService.GetList()
                //.Where(w => w.Tarih.Year < 2020//== yil
                //                               //&& w.Tarih.Month == ay
                //         )
                .Select(s => new MyDrop { Id = s.Id, Value = s.Tarih.ToLongDateString() });

            ViewBag.EczaneNobetGrupId = new SelectList(_eczaneNobetGrupService.GetDetaylarByEczaneIdList(eczaneler)
                .Select(s => new MyDrop { Id = s.Id, Value = $"{s.EczaneAdi}, {s.NobetGrupGorevTipAdi}" }).OrderBy(s => s.Value), "Id", "Value", eczaneNobetIstek.EczaneNobetGrupId);
            ViewBag.IstekId = new SelectList(_istekService.GetList(), "Id", "Adi", eczaneNobetIstek.IstekId);
            ViewBag.TakvimId = new SelectList(tarihler, "Id", "Tarih", eczaneNobetIstek.TakvimId);
            return View(eczaneNobetIstek);
        }

        // GET: EczaneNobet/EczaneNobetIstek/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var eczaneNobetIstek = _eczaneNobetIstekService.GetById(id);
            var eczaneNobetIstekDetay = _eczaneNobetIstekService.GetDetaylar().Where(s => s.Id == eczaneNobetIstek.Id).SingleOrDefault();
            if (eczaneNobetIstek == null)
            {
                return HttpNotFound();
            }
            return View(eczaneNobetIstekDetay);
        }

        // POST: EczaneNobet/EczaneNobetIstek/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EczaneNobetIstek eczaneNobetIstek = _eczaneNobetIstekService.GetById(id);
            _eczaneNobetIstekService.Delete(id);
            return RedirectToAction("Index");
        }


    }
}
