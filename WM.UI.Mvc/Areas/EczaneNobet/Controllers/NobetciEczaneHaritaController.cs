using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WM.Northwind.Business.Abstract.Authorization;
using WM.Northwind.Business.Abstract.EczaneNobet;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;
using WM.UI.Mvc.Areas.EczaneNobet.Models;
using WM.UI.Mvc.Models;
using System.IO;
using QRCoder;
using WM.UI.Mvc.Services;

namespace WM.UI.Mvc.Areas.EczaneNobet.Controllers
{
    [HandleError]
    public class NobetciEczaneHaritaController : Controller
    {
        #region ctor
        private IEczaneService _eczaneService;
        private IUserService _userService;
        private IEczaneNobetGrupService _eczaneNobetGrupService;
        private INobetUstGrupService _nobetUstGrupService;
        private IEczaneNobetSonucService _eczaneNobetSonucService;
        private INobetUstGrupSessionService _nobetUstGrupSessionService;

        public NobetciEczaneHaritaController(IEczaneService eczaneService,
                                IUserService userService,
                                INobetUstGrupService nobetUstGrupService,
                                IEczaneNobetGrupService eczaneNobetGrupService,
                                IEczaneNobetSonucService eczaneNobetSonucService,
                                INobetUstGrupSessionService nobetUstGrupSessionService)
        {
            _eczaneService = eczaneService;
            _nobetUstGrupService = nobetUstGrupService;
            _eczaneNobetSonucService = eczaneNobetSonucService;
            _userService = userService;
            _eczaneNobetGrupService = eczaneNobetGrupService;
            _nobetUstGrupSessionService = nobetUstGrupSessionService;
        }
        #endregion
        // GET: EczaneNobet/Eczane        
        [Authorize]
        public ActionResult Index(DateTime? tarih)
        {
            //var user = _userService.GetByUserName(User.Identity.Name);
            //var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            //var nobetUstGrup = nobetUstGruplar.FirstOrDefault();
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var _tarih = DateTime.Today;

            var nobetciEczaneler = _eczaneNobetSonucService.GetDetaylarGunluk(_tarih, nobetUstGrup.Id);
            var IPadres = Request.UserHostAddress;
            // var ekraninBulundugueczane = _eczaneService.GetList().Where(w=>w.IPadress == IPadres);

            var model = new NobetciEczaneHaritaViewModel
            {
                NobetciEczaneler = new List<NobetciEczane>(),
                Enlem = nobetUstGrup.Enlem,// _enlem,
                Boylam = nobetUstGrup.Boylam,// _boylam,
                Tarih = _tarih
            };

            foreach (var item in nobetciEczaneler)
            {
                var eczane = _eczaneService.GetById(item.EczaneId);

                var adres = eczane.Adres;
                var enlem = eczane.Enlem;
                var boylam = eczane.Boylam;
                var telefonNo = eczane.TelefonNo;
                var adresTarifi = eczane.AdresTarifi;
                var adresTarifiKisa = eczane.AdresTarifiKisa;

                model.NobetciEczaneler.Add(new NobetciEczane
                {
                    EczaneId = item.EczaneId,
                    NobetUstGrupId = item.NobetUstGrupId,
                    Adi = item.EczaneAdi,
                    NobetGorevTipAdi = item.NobetGorevTipAdi,
                    NobetGrupAdi = item.NobetGrupAdi,
                    Adres = adres,
                    Enlem = enlem,
                    Boylam = boylam,
                    TelefonNo = telefonNo,
                    AdresTarifi = adresTarifi,
                    AdresTarifiKisa = adresTarifiKisa
                });
            }

            return View(model);
        }

        public ActionResult NobetciEczaneler()
        {
            //var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var nobetUstGruplar = _nobetUstGrupService.GetDetaylar();
                //.Where(w => w.Id < 6);

            ViewBag.NobetUstGrupId = new SelectList(nobetUstGruplar.Select(s => new { s.Id, s.Adi }), "Id", "Adi");

            var model = new NobetciEczanelerViewModel
            {
                NobetTarihi = DateTime.Today,
                NobetUstGrupId = 1
            };

            return View(model);
        }

        public JsonResult NobetciEczaneListesi(DateTime? tarih, int nobetUstGrupId)
        {
            var model = GetNobetciler(tarih, nobetUstGrupId);

            var jsonResult = Json(model, JsonRequestBehavior.AllowGet);

            return jsonResult;
        }

        [Authorize]
        public JsonResult GetNobetcilerByTarih(DateTime tarih)
        {
            //var user = _userService.GetByUserName(User.Identity.Name);
            //var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            //var nobetUstGrup = nobetUstGruplar.FirstOrDefault();
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var _tarih = DateTime.Today;

            if (tarih == null)
            {
                tarih = _tarih;
            }
            else
            {
                _tarih = tarih;
            }

            double _enlem = nobetUstGrup.Enlem;
            double _boylam = nobetUstGrup.Boylam;

            var nobetciEczaneler = _eczaneNobetSonucService.GetDetaylarGunluk(tarih, nobetUstGrup.Id);
            var IPadres = Request.UserHostAddress;
            // var ekraninBulundugueczane = _eczaneService.GetList().Where(w=>w.IPadress == IPadres);

            var model = new NobetciEczaneHaritaViewModel
            {
                NobetciEczaneler = new List<NobetciEczane>(),
                Enlem = _enlem,
                Boylam = _boylam,
                Tarih = tarih
            };

            foreach (var item in nobetciEczaneler)
            {
                var eczane = _eczaneService.GetById(item.EczaneId);

                var adres = eczane.Adres;
                var enlem = eczane.Enlem;
                var boylam = eczane.Boylam;
                var telefonNo = eczane.TelefonNo;
                var adresTarifi = eczane.AdresTarifi;
                var adresTarifiKisa = eczane.AdresTarifiKisa;

                model.NobetciEczaneler.Add(new NobetciEczane
                {
                    EczaneId = item.EczaneId,
                    NobetUstGrupId = item.NobetUstGrupId,
                    Adi = item.EczaneAdi,
                    NobetGorevTipAdi = item.NobetGorevTipAdi,
                    NobetGrupAdi = item.NobetGrupAdi,
                    Adres = adres,
                    Enlem = enlem,
                    Boylam = boylam,
                    TelefonNo = telefonNo,
                    AdresTarifi = adresTarifi,
                    AdresTarifiKisa = adresTarifiKisa,
                    NobetAltGrupAdi = item.NobetAltGrupAdi
                });
            }

            var jsonResult = Json(model, JsonRequestBehavior.AllowGet);
            return jsonResult;
        }

        public NobetciEczaneHaritaViewModel GetNobetciler(DateTime? tarih, int nobetUstGrupId)
        {
            var _tarih = DateTime.Today;

            if (tarih != null)
            {
                _tarih = (DateTime)tarih;
            }

            //ay = 8;
            //gun = 30;
            var nobetUstGrup = _nobetUstGrupService.GetById(nobetUstGrupId);

            double _enlem = nobetUstGrup.Enlem;
            double _boylam = nobetUstGrup.Boylam;


            if (nobetUstGrupId == 0)
            {
                //38.84574110292016%2C35.3869628281249
                //38.657041,34.2558455
                //38.84574110292016
                _enlem = 38.84574110292016;// 39.4093233; 
                _boylam = 35.3869628281249; //34.9375269;
            }

            var nobetciEczaneler = _eczaneNobetSonucService.GetDetaylarGunluk(_tarih, nobetUstGrupId)
                .Where(w => w.NobetUstGrupId < 6).ToList();

            var IPadres = Request.UserHostAddress;
            // var ekraninBulundugueczane = _eczaneService.GetList().Where(w=>w.IPadress == IPadres);

            var model = new NobetciEczaneHaritaViewModel
            {
                NobetciEczaneler = new List<NobetciEczane>(),
                Enlem = _enlem,
                Boylam = _boylam,
                Tarih = _tarih
            };

            foreach (var item in nobetciEczaneler)
            {
                var eczane = _eczaneService.GetById(item.EczaneId);

                var adres = eczane.Adres;
                var enlem = eczane.Enlem;
                var boylam = eczane.Boylam;
                var telefonNo = eczane.TelefonNo;
                var adresTarifi = eczane.AdresTarifi;
                var adresTarifiKisa = eczane.AdresTarifiKisa;

                model.NobetciEczaneler.Add(new NobetciEczane
                {
                    EczaneId = item.EczaneId,
                    NobetUstGrupId = item.NobetUstGrupId,
                    Adi = item.EczaneAdi,
                    NobetGorevTipAdi = item.NobetGorevTipAdi,
                    NobetGrupAdi = item.NobetGrupAdi,
                    Adres = adres,
                    Enlem = enlem,
                    Boylam = boylam,
                    TelefonNo = telefonNo,
                    AdresTarifi = adresTarifi,
                    AdresTarifiKisa = adresTarifiKisa,
                    NobetAltGrupAdi = item.NobetAltGrupAdi
                });
            }

            //var jsonResult = Json(model, JsonRequestBehavior.AllowGet);

            return model;
        }

        public JsonResult GetTumEczaneler()
        {
            //var user = _userService.GetByUserName(User.Identity.Name);
            //var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            //var nobetUstGrup = nobetUstGruplar.FirstOrDefault();
            var nobetUstGrup = _nobetUstGrupSessionService.GetSession("nobetUstGrup");
     
            double _enlem = nobetUstGrup.Enlem;
            double _boylam = nobetUstGrup.Boylam;

            var nobetciEczaneler = _eczaneNobetGrupService.GetDetaylar(nobetUstGrup.Id);
            var IPadres = Request.UserHostAddress;
            // var ekraninBulundugueczane = _eczaneService.GetList().Where(w=>w.IPadress == IPadres);

            var model = new NobetciEczaneHaritaViewModel
            {
                NobetciEczaneler = new List<NobetciEczane>(),
                Enlem = _enlem,
                Boylam = _boylam,
                //Tarih = tarih
            };

            foreach (var item in nobetciEczaneler)
            {
                var eczane = _eczaneService.GetById(item.EczaneId);

                var adres = eczane.Adres;
                var enlem = eczane.Enlem;
                var boylam = eczane.Boylam;
                var telefonNo = eczane.TelefonNo;
                var adresTarifi = eczane.AdresTarifi;
                var adresTarifiKisa = eczane.AdresTarifiKisa;

                model.NobetciEczaneler.Add(new NobetciEczane
                {
                    EczaneId = item.EczaneId,
                    NobetUstGrupId = item.NobetUstGrupId,
                    Adi = item.EczaneAdi,
                    NobetGorevTipAdi = item.NobetGorevTipAdi,
                    NobetGrupAdi = item.NobetGrupAdi,
                    Adres = adres,
                    Enlem = enlem,
                    Boylam = boylam,
                    TelefonNo = telefonNo,
                    AdresTarifi = adresTarifi,
                    AdresTarifiKisa = adresTarifiKisa,
                    NobetAltGrupAdi = item.NobetAltGrupAdi
                });
            }

            var jsonResult = Json(model, JsonRequestBehavior.AllowGet);
            return jsonResult;
        }

        [HttpPost]
        public JsonResult QrCodeGeneate(string qrcode)
        {
            string url = "https://www.google.com.tr/maps/search/?api=1&query=" + qrcode;
            using (MemoryStream ms = new MemoryStream())
            {
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                Bitmap qrCodeImage = qrCode.GetGraphic(2);
                // Bitmap bm = new Bitmap((Image)qe.Encode(Data_TO_Encode), new Size(width, height));
                using (Bitmap bitMap = qrCode.GetGraphic(2))
                {
                    bitMap.Save(ms, ImageFormat.Png);
                    var image = Convert.ToBase64String(ms.ToArray());
                    //ViewBag.QRCodeImage = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                    return Json(new { base64imgage = image }, JsonRequestBehavior.AllowGet);


                }
            }

        }
        // GET: EczaneNobet/Eczane/Details/5
        [Authorize]
        public ActionResult Details(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Eczane eczane = _eczaneService.GetById(id);
            if (eczane == null)
            {
                return HttpNotFound();
            }
            return View(eczane);
        }

        // GET: EczaneNobet/Eczane/Create
        [Authorize(Roles = "Admin,Oda,Üst Grup")]
        public ActionResult Create()
        {
            //var eczane = new Eczane { AcilisTarihi = DateTime.Today, Enlem = 0, Boylam = 0 };

            return View();
        }

        // POST: EczaneNobet/Eczane/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Adi,AcilisTarihi,KapanisTarihi,Enlem,Boylam,Adres,TelefonNo,MailAdresi,WebSitesi")] Eczane eczane)
        {
            if (ModelState.IsValid)
            {
                _eczaneService.Insert(eczane);
                return RedirectToAction("Index");
            }

            return View(eczane);
        }

        // GET: EczaneNobet/Eczane/Edit/5
        [Authorize(Roles = "Admin,Oda,Üst Grup")]
        public ActionResult Edit(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Eczane eczane = _eczaneService.GetById(id);
            if (eczane == null)
            {
                return HttpNotFound();
            }
            return View(eczane);
        }

        // POST: EczaneNobet/Eczane/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Adi,AcilisTarihi,KapanisTarihi,Enlem,Boylam,Adres,TelefonNo,MailAdresi,WebSitesi")] Eczane eczane)
        {
            if (ModelState.IsValid)
            {
                _eczaneService.Update(eczane);
                return RedirectToAction("Index");
            }
            return View(eczane);
        }

        // GET: EczaneNobet/Eczane/Delete/5
        [Authorize(Roles = "Admin,Oda,Üst Grup")]
        public ActionResult Delete(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Eczane eczane = _eczaneService.GetById(id);
            if (eczane == null)
            {
                return HttpNotFound();
            }
            return View(eczane);
        }

        // POST: EczaneNobet/Eczane/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Eczane eczane = _eczaneService.GetById(id);
            _eczaneService.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult EczanelerDdlPartialView(int nobetGrupId = 0)
        {
            var user = _userService.GetByUserName(User.Identity.Name);
            var eczaneler = _eczaneService.GetListByUser(user).Select(s => s.Id).ToList();

            var eczaneNobetGruplar = _eczaneNobetGrupService.GetDetaylarByEczaneIdList(eczaneler)
                .Where(w => w.NobetGrupId == nobetGrupId || nobetGrupId == 0)
                .Select(s => new MyDrop { Id = s.EczaneId, Value = $"{s.EczaneAdi}, {s.NobetGrupAdi}" }).Distinct()
                .OrderBy(o => o.Value).ToList();

            ViewBag.EczaneId = new SelectList(items: eczaneNobetGruplar, dataValueField: "Id", dataTextField: "Value");

            return PartialView(eczaneNobetGruplar);
        }
    }
}
