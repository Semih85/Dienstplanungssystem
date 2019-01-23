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

namespace WM.UI.Mvc.Areas.EczaneNobet.Controllers
{
    [HandleError]
    //[Authorize]
    public class NobetciEczaneEkraniController : Controller
    {
        #region ctor
        private IEczaneService _eczaneService;
        private IUserService _userService;
        private IEczaneNobetGrupService _eczaneNobetGrupService;
        private INobetUstGrupService _nobetUstGrupService;
        private IEczaneNobetSonucService _eczaneNobetSonucService;

        public NobetciEczaneEkraniController(IEczaneService eczaneService,
                                IUserService userService,
                                INobetUstGrupService nobetUstGrupService,
                                IEczaneNobetGrupService eczaneNobetGrupService,
                                IEczaneNobetSonucService eczaneNobetSonucService)
        {
            _eczaneService = eczaneService;
            _nobetUstGrupService = nobetUstGrupService;
            _eczaneNobetSonucService = eczaneNobetSonucService;
            _userService = userService;
            _eczaneNobetGrupService = eczaneNobetGrupService;
        }
        #endregion
        // GET: EczaneNobet/Eczane        
        public ActionResult Index()
        {
            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user).Select(s => s.Id);
            var nobetUstGrupId = nobetUstGruplar.FirstOrDefault();
            nobetUstGrupId = 3;
            //ay = 10;
            //gun = 10;

            var gosterilecekTarih = DateTime.Today;

            var eczaneId = 69;

            //if (nobetUstGrupId != 2 || nobetUstGrupId != 3
            //    //nobetUstGrupId == 1
            //    )
            //{
            //    nobetUstGrupId = 3;
            //}

            if (nobetUstGrupId == 1)
            {
                eczaneId = 37;
            }
            else if (nobetUstGrupId == 2)
            {
                eczaneId = 69;
            }
            else if (nobetUstGrupId == 3)
            {
                eczaneId = 600;
            }
            else if (nobetUstGrupId == 4)
            {
                eczaneId = 857;
            }
            else if (nobetUstGrupId == 5)
            {
                eczaneId = 917;
            }
            else
            {
                eczaneId = 69;
            }

            var nobetciEczaneler = _eczaneNobetSonucService.GetDetaylarGunluk(gosterilecekTarih, nobetUstGrupId);

            var IPadres = Request.UserHostAddress;
            // var ekraninBulundugueczane = _eczaneService.GetList().Where(w=>w.IPadress == IPadres);
            var ekraninBulundugueczane = _eczaneService.GetById(eczaneId);

            var model = new NobetciEcanelerEkraniViewModel
            {
                NobetciEczaneler = new List<NobetciEczane>(),
                NobetciEcanelerEkraniTipId = 1,
                EkraninBulunduguEczane = new NobetciEczane
                {
                    Adi = ekraninBulundugueczane.Adi,
                    Adres = ekraninBulundugueczane.Adres,
                    TelefonNo = ekraninBulundugueczane.TelefonNo,
                    Enlem = ekraninBulundugueczane.Enlem,
                    Boylam = ekraninBulundugueczane.Boylam,
                    AdresTarifiKisa = ekraninBulundugueczane.AdresTarifiKisa,
                    AdresTarifi = ekraninBulundugueczane.AdresTarifi
                }
            };

            foreach (var item in nobetciEczaneler)
            {
                var adres = _eczaneService.GetById(item.EczaneId).Adres;
                var enlem = _eczaneService.GetById(item.EczaneId).Enlem;
                var boylam = _eczaneService.GetById(item.EczaneId).Boylam;
                var telefonNo = _eczaneService.GetById(item.EczaneId).TelefonNo;
                var adresTarifi = _eczaneService.GetById(item.EczaneId).AdresTarifi;
                var adresTarifiKisa = _eczaneService.GetById(item.EczaneId).AdresTarifiKisa;

                model.NobetciEczaneler.Add(new NobetciEczane
                {
                    EczaneId = item.EczaneId,
                    Adi = item.EczaneAdi,
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
