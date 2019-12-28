using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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
using WM.UI.Mvc.Services;

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
        private ITakvimService _takvimService;
        private INobetUstGrupSessionService _nobetUstGrupSessionService;
        private IEczaneNobetSonucPlanlananService _eczaneNobetSonucPlanlananService;
        private INobetUstGrupGunGrupService _nobetUstGrupGunGrupService;

        public EczaneNobetGrupController(IEczaneNobetGrupService eczaneNobetGrupService,
                                         IEczaneService eczaneService,
                                         INobetGrupGorevTipService nobetGrupGorevTipService,
                                         INobetUstGrupService nobetUstGrupService,
                                         IUserService userService,
                                         IAyniGunTutulanNobetService ayniGunTutulanNobetService,
                                         ITakvimService takvimService,
                                         INobetUstGrupSessionService nobetUstGrupSessionService,
                                         IEczaneNobetSonucPlanlananService eczaneNobetSonucPlanlananService,
                                         INobetUstGrupGunGrupService nobetUstGrupGunGrupService)
        {
            _eczaneNobetGrupService = eczaneNobetGrupService;
            _eczaneService = eczaneService;
            _nobetGrupGorevTipService = nobetGrupGorevTipService;
            _userService = userService;
            _nobetUstGrupService = nobetUstGrupService;
            _ayniGunTutulanNobetService = ayniGunTutulanNobetService;
            _takvimService = takvimService;
            _nobetUstGrupSessionService = nobetUstGrupSessionService;
            _eczaneNobetSonucPlanlananService = eczaneNobetSonucPlanlananService;
            _nobetUstGrupGunGrupService = nobetUstGrupGunGrupService;
        }
        #endregion

        // GET: EczaneNobet/EczaneNobetGrup
        [Authorize(Roles = "Admin,Oda,Üst Grup")]
        public ActionResult Index()
        {
            //var user = _userService.GetByUserName(User.Identity.Name);

            var ustGrupSession = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            //if (ustGrupSession.Id != 0)
            //{
            //    nobetUstGrup = ustGrupSession;
            //}

            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(ustGrupSession.Id);

            ViewBag.NobetGrupGorevTipId = new SelectList(nobetGrupGorevTipler, "Id", "NobetGrupGorevTipAdi");

            var model = _eczaneNobetGrupService.GetDetaylar(ustGrupSession.Id)
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
            //var user = _userService.GetByUserName(User.Identity.Name);
            //var nobetUstGrup = _nobetUstGrupService.GetListByUser(user).FirstOrDefault();

            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

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
            //var user = _userService.GetByUserName(User.Identity.Name);
            //var nobetGruplar = _nobetGrupGorevTipService.GetListByUser(user);

            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var model = _eczaneNobetGrupService.GetDetaylar(nobetUstGrup.Id)
                .Where(x => Regex.Split(Keywords, @"\s")
                            .Any(y => x.EczaneAdi.ToLower().Contains(y.ToLower())
                                   || x.NobetGrupAdi.ToLower().Contains(y.ToLower())
                            )
                            )
                .OrderBy(s => s.NobetGrupAdi).ThenBy(s => s.EczaneAdi);

            return View("Index", model);//result:model
        }

        public ActionResult SearchWithNobetGrupGorevTipId(int? nobetGrupGorevTipId = 0)
        {
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrup.Id)
                .Where(w => w.Id == nobetGrupGorevTipId || nobetGrupGorevTipId == 0);

            ViewBag.NobetGrupGorevTipId = new SelectList(nobetGrupGorevTipler, "Id", "Adi");

            var eczaneNobetGruplar = _eczaneNobetGrupService.GetDetaylarByNobetGrupGorevTipler(nobetGrupGorevTipler.Select(s => s.Id).ToList())
                .OrderBy(s => s.NobetGorevTipId)
                .ThenBy(s => s.NobetGrupAdi)
                .ThenBy(s => s.EczaneAdi).ToList();

            return PartialView("EczaneNobetGrupPartialView", eczaneNobetGruplar);
        }

        // POST: EczaneNobet/EczaneNobetGrup/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Oda,Üst Grup")]
        public ActionResult Create([Bind(Include = "Id,EczaneId,NobetGrupGorevTipId,BaslangicTarihi,BitisTarihi,Aciklama,EnErkenTarihteNobetYazilsinMi")] EczaneNobetGrupCoklu eczaneNobetGrupCoklu)
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
                var eklenenNobetGrupGorevTip = _nobetGrupGorevTipService.GetDetayById(eczaneNobetGrupCoklu.NobetGrupGorevTipId);

                TempData["EklenenEczane"] = $"{eklenenNobetGrupGorevTip.NobetGrupGorevTipAdi} nöbet grubuna {eczaneNobetGrupCoklu.EczaneId.Count()} adet eczane başarılı bir şekilde eklenmiştir.";

                _eczaneNobetGrupService.CokluEkle(eczaneNobetGruplar);

                var gruptakiEczaneler = _eczaneNobetGrupService.GetDetaylarByNobetGrupGorevTipler(eczaneNobetGrupCoklu.NobetGrupGorevTipId);
                var eczaneIdList = eczaneNobetGruplar.Select(s => s.EczaneId).ToList();
                var eczaneNobetGrupDetaylar = _eczaneNobetGrupService.GetDetaylar(eczaneIdList, eczaneNobetGrupCoklu.NobetGrupGorevTipId);

                if (eklenenNobetGrupGorevTip.NobetUstGrupId == 2)
                {//antalya'da planlanan nöbetleri yazmak için
                    if (eczaneNobetGrupDetaylar.Count > 0)
                    {//grupta eczaneler var. grup yeni değil. tekli olarak eklenen eczaneler için planlanan nöbetler yeniden yazılacak.
                        #region planlanan nöbetler - sıralı nöbet yazma (gün grubu bazında)

                        var baslangicTarihi = eczaneNobetGrupDetaylar.Min(s => s.BaslangicTarihi);

                        var sonTarih = baslangicTarihi.AddYears(1);

                        var planlananNobetlerinYazilacagiSonTarih = new DateTime(sonTarih.Year, 12, 31);

                        var planlananNobetlerinYazilacagiNobetGrubu = _nobetGrupGorevTipService.GetDetayById(eczaneNobetGrupCoklu.NobetGrupGorevTipId);

                        _takvimService.SiraliNobetYazGrupBazindaOncekiNobetGrubaGore(planlananNobetlerinYazilacagiNobetGrubu, gruptakiEczaneler, baslangicTarihi, planlananNobetlerinYazilacagiSonTarih);

                        #endregion
                    }
                    else
                    {//gruba ilk kez eczane ekleniyor

                    }
                }

                var nobetGrupGorevTip = _nobetGrupGorevTipService.GetDetayById(eczaneNobetGrupCoklu.NobetGrupGorevTipId);
                var nobetUstGrupId = nobetGrupGorevTip.NobetUstGrupId;

                if (nobetUstGrupId == 1   //alanya
                    || nobetUstGrupId == 3//mersin
                    || nobetUstGrupId == 4//giresun
                    || nobetUstGrupId == 5//osmaniye
                    )
                {
                    var eklenenIkiliEczaneler = _ayniGunTutulanNobetService.IkiliEczaneleriOlustur(eczaneNobetGrupDetaylar);
                }
                //else if (nobetUstGrupId == 1)
                //{

                //}

                ViewBag.EklenenEczaneSayisi = eklenecekEczaneSayisi;
                ViewBag.EklenenNobetGrupAdi = _nobetGrupGorevTipService.GetDetayById(eczaneNobetGrupCoklu.NobetGrupGorevTipId).NobetGrupAdi;
                return RedirectToAction("Index");
            }

            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            //var user = _userService.GetByUserName(User.Identity.Name);
            //var nobetUstGrup = _nobetUstGrupService.GetListByUser(user).FirstOrDefault();
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
            //var user = _userService.GetByUserName(User.Identity.Name);
            //var nobetUstGrup = _nobetUstGrupService.GetListByUser(user).FirstOrDefault();
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrup.Id);

            var eczaneler = _eczaneService.GetList(nobetUstGrup.Id)
                .OrderBy(s => s.Adi).ToList();

            ViewBag.EczaneId = new SelectList(eczaneler, "Id", "Adi", eczaneNobetGrup.EczaneId);
            ViewBag.NobetGrupGorevTipId = new SelectList(nobetGrupGorevTipler, "Id", "NobetGrupGorevTipAdi", eczaneNobetGrup.NobetGrupGorevTipId);
            return View(eczaneNobetGrup);
        }

        // POST: EczaneNobet/EczaneNobetGrup/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Oda,Üst Grup")]
        public ActionResult Edit([Bind(Include = "Id,EczaneId,NobetGrupGorevTipId,BaslangicTarihi,BitisTarihi,Aciklama,EnErkenTarihteNobetYazilsinMi")] EczaneNobetGrup eczaneNobetGrup)
        {
            if (ModelState.IsValid)
            {
                var degisecekEczaneNobetGrupOncekiHali = _eczaneNobetGrupService.GetDetayById(eczaneNobetGrup.Id);

                _eczaneNobetGrupService.Update(eczaneNobetGrup);

                PlanlananNobetleriYazdir(eczaneNobetGrup, degisecekEczaneNobetGrupOncekiHali);

                return RedirectToAction("Index");
            }
            //var user = _userService.GetByUserName(User.Identity.Name);
            //var nobetUstGrup = _nobetUstGrupService.GetListByUser(user).FirstOrDefault();

            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var nobetGrupGorevTipler = _nobetGrupGorevTipService.GetDetaylar(nobetUstGrup.Id);

            var eczaneler = _eczaneService.GetList(nobetUstGrup.Id)
                .OrderBy(s => s.Adi).ToList();

            ViewBag.EczaneId = new SelectList(eczaneler, "Id", "Adi", eczaneNobetGrup.EczaneId);
            ViewBag.NobetGrupGorevTipId = new SelectList(nobetGrupGorevTipler, "Id", "NobetGrupGorevTipAdi", eczaneNobetGrup.NobetGrupGorevTipId);

            return View(eczaneNobetGrup);
        }

        public void PlanlananNobetleriYazdir(EczaneNobetGrup eczaneNobetGrup, EczaneNobetGrupDetay degisecekEczaneNobetGrupOncekiHali)
        {
            #region planlanan nöbet yazdırma seçenekleri

            var eklenenNobetGrupGorevTip = _nobetGrupGorevTipService.GetDetayById(eczaneNobetGrup.NobetGrupGorevTipId);

            var bitisTarihiDegistiMi = degisecekEczaneNobetGrupOncekiHali.BitisTarihi != eczaneNobetGrup.BitisTarihi;
            //var baslangicTarihiDegistiMi = degisecekEczaneNobetGrupOncekiHali.BaslangicTarihi != eczaneNobetGrup.BaslangicTarihi;

            if (eklenenNobetGrupGorevTip.NobetUstGrupId == 2
                && (bitisTarihiDegistiMi
                //|| baslangicTarihiDegistiMi
                )
                )
            {//antalya'da planlanan nöbetleri yazmak için
             //grupta eczaneler var. grup yeni değil. tekli olarak eklenen eczaneler için planlanan nöbetler yeniden yazılacak.
                #region planlanan nöbetler - sıralı nöbet yazma (gün grubu bazında)

                var gruptakiEczaneler = _eczaneNobetGrupService.GetDetaylarByNobetGrupGorevTipler(eczaneNobetGrup.NobetGrupGorevTipId);

                var baslangicTarihi = eczaneNobetGrup.BaslangicTarihi < degisecekEczaneNobetGrupOncekiHali.NobetGrupGorevTipBaslamaTarihi
                    ? degisecekEczaneNobetGrupOncekiHali.NobetGrupGorevTipBaslamaTarihi
                    : eczaneNobetGrup.BaslangicTarihi;

                var baslangicTarihiVarsayilan = baslangicTarihi;

                //if (baslangicTarihiDegistiMi)
                //{
                //    baslangicTarihi = eczaneNobetGrup.BaslangicTarihi < degisecekEczaneNobetGrupOncekiHali.NobetGrupGorevTipBaslamaTarihi 
                //        ? degisecekEczaneNobetGrupOncekiHali.NobetGrupGorevTipBaslamaTarihi
                //        : eczaneNobetGrup.BaslangicTarihi;
                //}

                if (bitisTarihiDegistiMi
                    && EczaneGruptaKapaniyorMu(eczaneNobetGrup.BitisTarihi))
                {
                    baslangicTarihi = (DateTime)eczaneNobetGrup.BitisTarihi;
                }

                var sonTarih = baslangicTarihi.AddYears(1);

                var planlananNobetlerinYazilacagiSonTarih = new DateTime(sonTarih.Year, 12, 31);

                var planlananNobetlerinYazilacagiNobetGrubu = _nobetGrupGorevTipService.GetDetayById(eczaneNobetGrup.NobetGrupGorevTipId);

                var nobetUstGrupGunGruplar = _nobetUstGrupGunGrupService.GetDetaylar(degisecekEczaneNobetGrupOncekiHali.NobetUstGrupId)
                        .OrderBy(o => o.GunGrupId).ToList();

                foreach (var gunGrup in nobetUstGrupGunGruplar)
                {
                    if (EczaneGruptaKapaniyorMu(eczaneNobetGrup.BitisTarihi))
                    {
                        baslangicTarihi = (DateTime)eczaneNobetGrup.BitisTarihi;
                    }
                    else
                    {
                        var planlananSonNobetTarihi = _eczaneNobetSonucPlanlananService.GetSonuclarByEczaneNobetGrupId(eczaneNobetGrup.Id, gunGrup.GunGrupId)
                            .Where(w => w.Tarih >= w.NobetGrupGorevTipBaslamaTarihi)
                            .OrderByDescending(o => o.Tarih).FirstOrDefault();

                        baslangicTarihi = planlananSonNobetTarihi != null ? planlananSonNobetTarihi.Tarih : baslangicTarihiVarsayilan;
                    }

                    _takvimService.SiraliNobetYazGunGrupBazinda(
                        planlananNobetlerinYazilacagiNobetGrubu,
                        gruptakiEczaneler,
                        baslangicTarihi,
                        planlananNobetlerinYazilacagiSonTarih,
                        gunGrup.GunGrupId);
                }

                #endregion
            }

            #endregion
        }

        private bool EczaneGruptaKapaniyorMu(DateTime? eczaneNobetGrupAyrilisTarihi)
        {//ayrılış tarihi null değilse eczane grupta kapanır.
            return eczaneNobetGrupAyrilisTarihi != null;
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
            var eczaneNobetGrupDetay = _eczaneNobetGrupService.GetDetayById(id);
            //EczaneNobetGrup eczaneNobetGrup = _eczaneNobetGrupService.GetById(id);

            try
            {
                //PlanlananNobetleriYazdir(eczaneNobetGrup);

                _eczaneNobetGrupService.Delete(id);
            }
            catch (DbUpdateException ex)
            {
                var hata = ex.InnerException.ToString();

                string[] referansKayitHatasi = { "The DELETE statement conflicted with the REFERENCE constraint", "with unique index" };

                var referansKayitHatasiMi = referansKayitHatasi.Any(h => hata.Contains(h));

                if (referansKayitHatasiMi)
                {
                    throw new Exception($"Gruptan silmek istediğiniz <strong>{eczaneNobetGrupDetay.EczaneAdi} eczanesine ait başka kayıtlar bulunmaktadır.</strong> " +
                        "Lütfen bu kayıtları sildikten sonra tekrar deneyiniz..." +
                        "<br /> " +
                        "<strong >(Önce referans kayıtlar silinmelidir!)</strong>", ex.InnerException);
                }

                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return RedirectToAction("Index");
        }

    }
}
