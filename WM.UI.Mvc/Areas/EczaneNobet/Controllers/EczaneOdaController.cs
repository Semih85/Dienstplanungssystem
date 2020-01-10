using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WM.Northwind.Business.Abstract.Authorization;
using WM.Northwind.Business.Abstract.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;
using WM.UI.Mvc.Models;

namespace WM.UI.Mvc.Areas.EczaneNobet.Controllers
{
    [Authorize]
    [HandleError]
    public class EczaneOdaController : Controller
    {
        private IEczaneOdaService _eczaneOdaService;
        private IUserService _userService;
        
        public EczaneOdaController(IEczaneOdaService eczaneOdaService,
                                   IUserService userService)
        {
            _eczaneOdaService = eczaneOdaService;
            _userService = userService;
        }

        // GET: EczaneNobet/EczaneOda
        public ActionResult Index()
        {
            //giriş yapan user
            var user = _userService.GetByUserName(User.Identity.Name);
            //yetkili olduğu odalar
            var eczaneOdalar = _eczaneOdaService.GetListByUser(user);
           
            return View(eczaneOdalar);
        }

        // GET: EczaneNobet/EczaneOda/Details/5
        public ActionResult Details(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EczaneOda eczaneOda = _eczaneOdaService.GetById(id);
            if (eczaneOda == null)
            {
                return HttpNotFound();
            }
            return View(eczaneOda);
        }

        // GET: EczaneNobet/EczaneOda/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EczaneNobet/EczaneOda/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Adi,Adres,TelefonNo,MailAdresi,WebSitesi")] EczaneOda eczaneOda)
        {
            if (ModelState.IsValid)
            {
                _eczaneOdaService.Insert(eczaneOda);
                return RedirectToAction("Index");
            }

            return View(eczaneOda);
        }

        // GET: EczaneNobet/EczaneOda/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EczaneOda eczaneOda = _eczaneOdaService.GetById(id);
            if (eczaneOda == null)
            {
                return HttpNotFound();
            }
            return View(eczaneOda);
        }

        // POST: EczaneNobet/EczaneOda/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Adi,Adres,TelefonNo,MailAdresi,WebSitesi")] EczaneOda eczaneOda)
        {
            if (ModelState.IsValid)
            {
                _eczaneOdaService.Update(eczaneOda);
                return RedirectToAction("Index");
            }
            return View(eczaneOda);
        }

        // GET: EczaneNobet/EczaneOda/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EczaneOda eczaneOda = _eczaneOdaService.GetById(id);
            if (eczaneOda == null)
            {
                return HttpNotFound();
            }
            return View(eczaneOda);
        }

        // POST: EczaneNobet/EczaneOda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EczaneOda eczaneOda = _eczaneOdaService.GetById(id);
            _eczaneOdaService.Delete(id);
            return RedirectToAction("Index");
        }


    }
}
