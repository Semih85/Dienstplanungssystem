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
using WM.UI.Mvc.Services;

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
        private INobetGrupGorevTipTakvimOzelGunService _nobetGrupGorevTipTakvimOzelGunService;
        private INobetUstGrupSessionService _nobetUstGrupSessionService;

        public EczaneNobetIstekController(IEczaneNobetIstekService eczaneNobetIstekService,
                                          IEczaneNobetGrupService eczaneNobetGrupService,
                                          IEczaneService eczaneService,
                                          IIstekService istekService,
                                          ITakvimService takvimService,
                                          IUserService userService,
                                          IEczaneGrupService eczaneGrupService,
                                          INobetGrupGorevTipTakvimOzelGunService nobetGrupGorevTipTakvimOzelGunService,
                                          INobetUstGrupSessionService nobetUstGrupSessionService
            )
        {
            _eczaneNobetIstekService = eczaneNobetIstekService;
            _eczaneNobetGrupService = eczaneNobetGrupService;
            _eczaneService = eczaneService;
            _istekService = istekService;
            _takvimService = takvimService;
            _userService = userService;
            _eczaneGrupService = eczaneGrupService;
            _nobetGrupGorevTipTakvimOzelGunService = nobetGrupGorevTipTakvimOzelGunService;
            _nobetUstGrupSessionService = nobetUstGrupSessionService;
        }
        #endregion

        // GET: EczaneNobet/EczaneNobetIstek
        public ActionResult Index()
        {
            //var eczaneNobetMazerets = db.EczaneNobetMazerets.Include(e => e.Eczane).Include(e => e.Mazeret).Include(e => e.Takvim);
            //eczaneNobetMazerets.ToList()
            //var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            //var eczaneler = _eczaneService.GetListByUser(user).Select(s => s.Id);

            var model = _eczaneNobetIstekService.GetDetaylar(nobetUstGrup.Id)
                //.Where(s => eczaneler.Contains(s.EczaneId))
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
            //var user = _userService.GetByUserName(User.Identity.Name);

            //var eczaneler = _eczaneService.GetListByUser(user).Select(s => s.Id).ToList();

            //var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            //var eczaneler = _eczaneService.GetListByUser(user).Select(s => s.Id).ToList();
            var eczaneNobetGruplar = _eczaneNobetGrupService.GetDetaylar(nobetUstGrup.Id);

            ViewBag.EczaneNobetGrupId = new SelectList(_eczaneNobetGrupService.GetMyDrop(eczaneNobetGruplar),
                //.Select(s => new MyDrop { Id = s.Id, Value = $"{s.EczaneAdi}, {s.NobetGrupGorevTipAdi}" })
                //.OrderBy(s => s.Value), 
                "Id", "Value");

            ViewBag.IstekId = new SelectList(_istekService.GetList(), "Id", "Adi");
            ViewBag.HaftaninGunu = new SelectList(_takvimService.GetHaftaninGunleri(), "Id", "Value");
            ViewBag.SecilenHaftaninGunuSayisi = 0;

            return View();
        }

        // POST: EczaneNobet/EczaneNobetIstek/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EczaneNobetGrupId,IstekId,BaslangicTarihi,BitisTarihi,HaftaninGunu,Aciklama,YinedeEklensinMi")] EczaneNobetIstekCoklu eczaneNobetIstekCoklu)
        {            
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");
            var eczaneNobetGruplar = _eczaneNobetGrupService.GetDetaylar(eczaneNobetIstekCoklu.EczaneNobetGrupId);
            var nobetUstGrupId = eczaneNobetGruplar.Select(s => s.NobetUstGrupId).Distinct().SingleOrDefault();

            var tarihler = _takvimService.GetList()
                .Select(s => new MyDrop { Id = s.Id, Value = s.Tarih.ToLongDateString() });

            if (eczaneNobetIstekCoklu.HaftaninGunu == null)
            {
                eczaneNobetIstekCoklu.HaftaninGunu = new int[1] { 0 };
            }

            var haftaninGunu = eczaneNobetIstekCoklu.HaftaninGunu;

            var bayramlar = _nobetGrupGorevTipTakvimOzelGunService.GetDetaylar(eczaneNobetIstekCoklu.BaslangicTarihi, eczaneNobetIstekCoklu.BitisTarihi, eczaneNobetGruplar.Select(s => s.NobetGrupId).ToList(), 1)
                .Where(w => eczaneNobetIstekCoklu.HaftaninGunu.Contains(w.NobetGunKuralId)).ToList();

            var tarihAraligi = _takvimService.GetDetaylar(eczaneNobetIstekCoklu.BaslangicTarihi, eczaneNobetIstekCoklu.BitisTarihi);

            if (eczaneNobetIstekCoklu.HaftaninGunu.Count() > 0)
            {
                tarihAraligi = tarihAraligi.Where(w => eczaneNobetIstekCoklu.HaftaninGunu.Contains(w.HaftaninGunu)
                                                    || bayramlar.Select(s => s.TakvimId).Contains(w.TakvimId)).ToList();
            }


            var eczaneler = _eczaneService.GetDetaylar(nobetUstGrup.Id).Select(s => s.Id).ToList();

            var baslangicTarihi = _takvimService.GetByTarih(eczaneNobetIstekCoklu.BaslangicTarihi);
            var bitisTarihi = _takvimService.GetByTarih(eczaneNobetIstekCoklu.BitisTarihi);

            ViewBag.EczaneNobetGrupId = new SelectList(_eczaneNobetGrupService.GetDetaylarByEczaneIdList(eczaneler)
                .Select(s => new MyDrop { Id = s.Id, Value = $"{s.EczaneAdi}, {s.NobetGrupGorevTipAdi}" }).OrderBy(s => s.Value), "Id", "Value", eczaneNobetIstekCoklu.EczaneNobetGrupId);
            ViewBag.IstekId = new SelectList(_istekService.GetList(), "Id", "Adi", eczaneNobetIstekCoklu.IstekId);
            ViewBag.HaftaninGunu = new SelectList(_takvimService.GetHaftaninGunleri(), "Id", "Value", eczaneNobetIstekCoklu.HaftaninGunu);
            ViewBag.SecilenHaftaninGunuSayisi = eczaneNobetIstekCoklu.HaftaninGunu.Count();

            //seçilen tarih aralığı takvimde olmalıdır.
            if (baslangicTarihi == null || bitisTarihi == null)
            {
                var minYil = _takvimService.GetList().Min(x => x.Tarih.Year);
                var maxYil = _takvimService.GetList().Max(x => x.Tarih.Year);
                ViewBag.minYil = minYil;
                ViewBag.maxYil = maxYil;

                ViewBag.Mesaj = $"Başlangıç-Bitiş tarih aralığı enaz {minYil} ila ençok {maxYil} arasında olmalıdır...";

                return View(eczaneNobetIstekCoklu);
            }

            //Başlangıç tarihi Bitiş tarihinden büyük olamaz.
            if (baslangicTarihi.Id > bitisTarihi.Id)
            {
                ViewBag.Mesaj2 = $"Başlangıç tarihi ({baslangicTarihi.Tarih}) Bitiş tarihinden ({bitisTarihi.Tarih}) büyük olamaz...";

                return View(eczaneNobetIstekCoklu);
            }

            var eczaneNobetIstekler = new List<EczaneNobetIstek>();

            foreach (var eczaneNobetGrupId in eczaneNobetIstekCoklu.EczaneNobetGrupId)
            {
                foreach (var item in tarihAraligi)
                {
                    eczaneNobetIstekler.Add(new EczaneNobetIstek
                    {
                        IstekId = eczaneNobetIstekCoklu.IstekId,
                        EczaneNobetGrupId = eczaneNobetGrupId,
                        TakvimId = item.TakvimId,
                        Aciklama = eczaneNobetIstekCoklu.Aciklama,
                    });
                }
            }

            var eklenenEczaneler = new List<EczaneNobetIstekDetay>();

            if (ModelState.IsValid && eczaneNobetIstekler.Count > 0)
            {
                var istekGirilenEczaneninEsOlduguEczaneler = _eczaneGrupService.GetDetaylarEczaneninEsOlduguEczaneler(eczaneNobetGruplar.Select(s => s.Id).ToList());

                var istekGirilenTarihtekiEczaneler = _eczaneNobetIstekService.GetDetaylarByNobetUstGrupId(eczaneNobetIstekCoklu.BaslangicTarihi, eczaneNobetIstekCoklu.BitisTarihi, nobetUstGrupId);

                var istekGirilenTarihtekiEsgrupOlduguEczaneler = _eczaneNobetIstekService.GetDetaylar(istekGirilenTarihtekiEczaneler, istekGirilenEczaneninEsOlduguEczaneler);

                var istekGirilenTarihtekiEsgrupOlduguEczanelerTumu = istekGirilenEczaneninEsOlduguEczaneler
                    .Union(istekGirilenTarihtekiEsgrupOlduguEczaneler)
                    .OrderBy(o => o.EczaneGrupTanimId)
                    .ThenBy(o => o.EczaneAdi)
                    .ToList();

                var istekGirilenTarihtekiEsgrupOlduguEczaneSayisi = istekGirilenTarihtekiEsgrupOlduguEczanelerTumu.Count;

                if (istekGirilenTarihtekiEsgrupOlduguEczaneSayisi > 0)
                {
                    ViewBag.IstekGirilenTarihtekiEsgrupOlduguEczaneler = istekGirilenTarihtekiEsgrupOlduguEczanelerTumu;

                    return View(eczaneNobetIstekCoklu);
                }
                else
                {
                    try
                    {
                        _eczaneNobetIstekService.CokluEkle(eczaneNobetIstekler);

                        foreach (var item in eczaneNobetIstekler)
                        {
                            eklenenEczaneler.Add(new EczaneNobetIstekDetay
                            {
                                EczaneAdi = _eczaneNobetGrupService.GetDetayById(item.EczaneNobetGrupId).EczaneAdi,
                                Tarih = _takvimService.GetById(item.TakvimId).Tarih
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
                            throw new Exception("<strong>Bir eczaneye aynı gün için iki istek kaydı eklenemez...</strong>");
                        }

                        throw ex;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

                TempData["EklenenIstekSayisi"] = eklenenEczaneler.Count;
                TempData["EklenenIstekler"] = eklenenEczaneler;

                return View(eczaneNobetIstekCoklu);
            }
            else
            {
                //bayram ve hafta günleri kontrol
                if (bayramlar.Count == 0)
                {
                    if (eczaneNobetIstekCoklu.HaftaninGunu.Where(w => w == 8 && w == 9).Count() > 0)
                    {
                        ViewBag.MesajBayram = $"Girilen tarih aralığına uygun bayram bulunmamaktadır.";
                    }
                    else if (eczaneNobetIstekCoklu.HaftaninGunu.Where(w => w == 8).Count() > 0)
                    {
                        ViewBag.MesajBayram = $"Girilen tarih aralığına uygun dini bayram bulunmamaktadır.";
                    }
                    else if (eczaneNobetIstekCoklu.HaftaninGunu.Where(w => w == 9).Count() > 0)
                    {
                        ViewBag.MesajBayram = $"Girilen tarih aralığına uygun milli bayram bulunmamaktadır.";
                    }
                    else if (eczaneNobetIstekCoklu.HaftaninGunu.Where(w => w <= 7).Count() > 0)
                    {
                        ViewBag.MesajBayram = $"Girilen tarih aralığına uygun hafta günü bulunmamaktadır.";
                    }
                }
                else
                {
                    if (eczaneNobetIstekCoklu.HaftaninGunu.Count() == 1)
                    {
                        ViewBag.MesajBayram = $"Seçilen hafta gününe uygun tarih aralığı bulunmamaktadır.";
                    }
                    else
                    {
                        ViewBag.MesajBayram = $"Seçilen hafta günlerine uygun tarih aralığı bulunmamaktadır.";
                    }
                }
            }
            return View(eczaneNobetIstekCoklu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePartial([Bind(Include = "Id,EczaneNobetGrupId,IstekId,BaslangicTarihi,BitisTarihi,HaftaninGunu,Aciklama,YinedeEklensinMi")] EczaneNobetIstekCoklu eczaneNobetIstekCoklu)
        {
            //var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var eczaneNobetGruplar = _eczaneNobetGrupService.GetDetaylar(eczaneNobetIstekCoklu.EczaneNobetGrupId);

            var nobetUstGrupId = eczaneNobetGruplar.Select(s => s.NobetUstGrupId).Distinct().SingleOrDefault();

            if (eczaneNobetIstekCoklu.HaftaninGunu == null)
            {
                eczaneNobetIstekCoklu.HaftaninGunu = new int[1] { 0 };
            }

            var haftaninGunu = eczaneNobetIstekCoklu.HaftaninGunu;

            var bayramlar = _nobetGrupGorevTipTakvimOzelGunService.GetDetaylar(eczaneNobetIstekCoklu.BaslangicTarihi, eczaneNobetIstekCoklu.BitisTarihi, eczaneNobetGruplar.Select(s => s.NobetGrupId).ToList(), 1)
                .Where(w => eczaneNobetIstekCoklu.HaftaninGunu.Contains(w.NobetGunKuralId)).ToList();

            var tarihAraligi = _takvimService.GetDetaylar(eczaneNobetIstekCoklu.BaslangicTarihi, eczaneNobetIstekCoklu.BitisTarihi);

            if (eczaneNobetIstekCoklu.HaftaninGunu.Count() > 0)
            {
                tarihAraligi = tarihAraligi.Where(w => eczaneNobetIstekCoklu.HaftaninGunu.Contains(w.HaftaninGunu)
                                                    || bayramlar.Select(s => s.TakvimId).Contains(w.TakvimId)).ToList();
            }

            var baslangicTarihi = _takvimService.GetByTarih(eczaneNobetIstekCoklu.BaslangicTarihi);
            var bitisTarihi = _takvimService.GetByTarih(eczaneNobetIstekCoklu.BitisTarihi);

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

            var eczaneNobetIstekler = new List<EczaneNobetIstek>();

            foreach (var eczaneNobetGrupId in eczaneNobetIstekCoklu.EczaneNobetGrupId)
            {
                foreach (var item in tarihAraligi)
                {
                    eczaneNobetIstekler.Add(new EczaneNobetIstek
                    {
                        IstekId = eczaneNobetIstekCoklu.IstekId,
                        EczaneNobetGrupId = eczaneNobetGrupId,
                        TakvimId = item.TakvimId,
                        Aciklama = eczaneNobetIstekCoklu.Aciklama,
                    });
                }
            }

            var eklenecekIstekSayisi = eczaneNobetIstekler.Count;
            var eklenenEczaneler = new List<EczaneNobetIstekDetay>();

            if (ModelState.IsValid && eklenecekIstekSayisi > 0)
            {
                var istekGirilmekIstenenEczaneler = eczaneNobetGruplar.Select(s => s.EczaneId).ToList();

                var istekGirilenEczaneninEsOlduguEczaneler = _eczaneGrupService.GetDetaylarEczaneninEsOlduguEczaneler(eczaneNobetGruplar.Select(s => s.Id).ToList());

                var eklenecekEczanelerinEsGrupTanimlari = istekGirilenEczaneninEsOlduguEczaneler.Select(s => new { s.EczaneGrupTanimId, s.EczaneGrupTanimAdi }).Distinct().ToList();

                var eklenecekEczanelerdenAyniEsGruptaOlanlar = new List<EczaneGrupDetay>();

                foreach (var eklenecekEczanelerinEsGrupTanim in eklenecekEczanelerinEsGrupTanimlari)
                {
                    var ayniGruptaOlanEklenmekIstenenEczaneler = (from a in istekGirilenEczaneninEsOlduguEczaneler
                                                                  from b in istekGirilmekIstenenEczaneler
                                                                  where a.EczaneId == b
                                                                     && a.EczaneGrupTanimId == eklenecekEczanelerinEsGrupTanim.EczaneGrupTanimId
                                                                  select a).ToList();

                    if (ayniGruptaOlanEklenmekIstenenEczaneler.Count > 1)
                    {
                        eklenecekEczanelerdenAyniEsGruptaOlanlar.AddRange(ayniGruptaOlanEklenmekIstenenEczaneler);
                    }
                }

                var istekGirilenTarihteNobetciOlanEczaneler = _eczaneNobetIstekService.GetDetaylarByNobetUstGrupId(eczaneNobetIstekCoklu.BaslangicTarihi, eczaneNobetIstekCoklu.BitisTarihi, nobetUstGrupId);

                var istekGirilenTarihtekiEsgrupOlduguEczaneler = _eczaneNobetIstekService.GetDetaylar(istekGirilenTarihteNobetciOlanEczaneler, istekGirilenEczaneninEsOlduguEczaneler);

                var istekGirilenTarihtekiEsgrupOlduguEczanelerTumu = istekGirilenTarihtekiEsgrupOlduguEczaneler
                    .Union(eklenecekEczanelerdenAyniEsGruptaOlanlar)
                    .OrderBy(o => o.EczaneGrupTanimAdi)
                    .ThenBy(o => o.EczaneAdi)
                    .ToList();

                var istekGirilenTarihtekiEsgrupOlduguEczaneSayisi = istekGirilenTarihtekiEsgrupOlduguEczanelerTumu.Count;

                var esGrupTanimlar = istekGirilenTarihtekiEsgrupOlduguEczanelerTumu.Select(s => new
                {
                    s.EczaneGrupTanimId,
                    s.EczaneGrupTanimAdi,
                    s.AyniGunNobetTutabilecekEczaneSayisi
                }).Distinct().ToList();

                if (!eczaneNobetIstekCoklu.YinedeEklensinMi)
                {
                    foreach (var esGrupTanim in esGrupTanimlar)
                    {
                        var gruptakiEczaneler = istekGirilenTarihtekiEsgrupOlduguEczanelerTumu.Where(w => w.EczaneGrupTanimId == esGrupTanim.EczaneGrupTanimId).ToList();

                        if (gruptakiEczaneler.Count > esGrupTanim.AyniGunNobetTutabilecekEczaneSayisi)
                        {
                            ViewBag.IstekGirilenTarihtekiEsgrupOlduguEczaneler = gruptakiEczaneler;

                            return PartialView();
                        }
                    }
                }

                //ViewBag.IstekGirilenTarihtekiEsgrupOlduguEczaneler = istekGirilenTarihtekiEsgrupOlduguEczanelerTumu;

                //return PartialView();

                try
                {
                    _eczaneNobetIstekService.CokluEkle(eczaneNobetIstekler);

                    foreach (var item in eczaneNobetIstekler)
                    {
                        eklenenEczaneler.Add(new EczaneNobetIstekDetay
                        {
                            EczaneAdi = _eczaneNobetGrupService.GetDetayById(item.EczaneNobetGrupId).EczaneAdi,
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
                        //throw new Exception("<strong>Bir eczaneye aynı gün için iki istek kaydı eklenemez...</strong>");
                        return PartialView("ErrorDublicateRowPartial");
                    }

                    // throw ex;
                }
                catch (Exception)
                {
                    return PartialView("ErrorPartial");
                    //throw ex;
                }


                TempData["EklenenIstekSayisi"] = eklenenEczaneler.Count;

                TempData["EklenenIstekler"] = eklenenEczaneler;

                ViewBag.SecilenHaftaninGunuSayisi = eczaneNobetIstekCoklu.HaftaninGunu.Count();

                return PartialView();
            }
            else
            {
                //bayram ve hafta günleri kontrol
                if (bayramlar.Count == 0)
                {
                    if (eczaneNobetIstekCoklu.HaftaninGunu.Where(w => w == 8 && w == 9).Count() > 0)
                    {
                        ViewBag.MesajBayram = $"Girilen tarih aralığına uygun bayram bulunmamaktadır.";
                    }
                    else if (eczaneNobetIstekCoklu.HaftaninGunu.Where(w => w == 8).Count() > 0)
                    {
                        ViewBag.MesajBayram = $"Girilen tarih aralığına uygun dini bayram bulunmamaktadır.";
                    }
                    else if (eczaneNobetIstekCoklu.HaftaninGunu.Where(w => w == 9).Count() > 0)
                    {
                        ViewBag.MesajBayram = $"Girilen tarih aralığına uygun milli bayram bulunmamaktadır.";
                    }
                    else if (eczaneNobetIstekCoklu.HaftaninGunu.Where(w => w <= 7).Count() > 0)
                    {
                        ViewBag.MesajBayram = $"Girilen tarih aralığına uygun hafta günü bulunmamaktadır.";
                    }
                }
                else
                {
                    if (eczaneNobetIstekCoklu.HaftaninGunu.Count() == 1)
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
