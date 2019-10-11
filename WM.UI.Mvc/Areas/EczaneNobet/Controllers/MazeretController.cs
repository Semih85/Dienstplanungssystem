using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WM.Northwind.Business.Abstract.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;
using WM.UI.Mvc.Models;

namespace WM.UI.Mvc.Areas.EczaneNobet.Controllers
{
    //[Authorize]
    [Authorize(Roles = "Admin,Oda,Üst Grup")]
    [HandleError]
    public class MazeretController : Controller
    {
        private IMazeretService _mazeretService;
        private IMazeretTurService _mazeretTurService;

        public MazeretController(IMazeretService mazeretService,
                                 IMazeretTurService mazeretTurService)
        {
            _mazeretService = mazeretService;
            _mazeretTurService = mazeretTurService;
        }

        // GET: EczaneNobet/Mazeret
        [Authorize(Roles = "Admin,Oda,Üst Grup")]
        public ActionResult Index()
        {
            var model = _mazeretService.GetList();
            //var mazerets = db.Mazerets.Include(m => m.MazeretTur);
            return View(model);
        }

        // GET: EczaneNobet/Mazeret/Details/5
        [Authorize(Roles = "Admin,Oda,Üst Grup")]
        public ActionResult Details(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mazeret mazeret = _mazeretService.GetById(id);
            if (mazeret == null)
            {
                return HttpNotFound();
            }
            return View(mazeret);
        }

        // GET: EczaneNobet/Mazeret/Create
        [Authorize(Roles = "Admin,Oda,Üst Grup")]
        public ActionResult Create()
        {
            ViewBag.MazeretTurId = new SelectList(_mazeretTurService.GetList().Select(s => new { s.Id, s.Adi }), "Id", "Adi");
            return View();
        }

        // POST: EczaneNobet/Mazeret/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Adi,MazeretTurId")] Mazeret mazeret)
        {
            if (ModelState.IsValid)
            {
                _mazeretService.Insert(mazeret);
                return RedirectToAction("Index");
            }

            ViewBag.MazeretTurId = new SelectList(_mazeretTurService.GetList().Select(s => new { s.Id, s.Adi }), "Id", "Adi", mazeret.MazeretTurId);
            return View(mazeret);
        }

        // GET: EczaneNobet/Mazeret/Edit/5
        public ActionResult Edit(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mazeret mazeret = _mazeretService.GetById(id);
            if (mazeret == null)
            {
                return HttpNotFound();
            }
            ViewBag.MazeretTurId = new SelectList(_mazeretTurService.GetList().Select(s => new { s.Id, s.Adi }), "Id", "Adi", mazeret.MazeretTurId);
            return View(mazeret);
        }

        // POST: EczaneNobet/Mazeret/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Adi,MazeretTurId")] Mazeret mazeret)
        {
            if (ModelState.IsValid)
            {
                _mazeretService.Update(mazeret);
                return RedirectToAction("Index");
            }
            ViewBag.MazeretTurId = new SelectList(_mazeretTurService.GetList().Select(s => new { s.Id, s.Adi }), "Id", "Adi", mazeret.MazeretTurId);
            return View(mazeret);
        }

        // GET: EczaneNobet/Mazeret/Delete/5
        public ActionResult Delete(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mazeret mazeret = _mazeretService.GetById(id);
            if (mazeret == null)
            {
                return HttpNotFound();
            }
            return View(mazeret);
        }

        // POST: EczaneNobet/Mazeret/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Mazeret mazeret = _mazeretService.GetById(id);
            _mazeretService.Delete(id);
            return RedirectToAction("Index");
        }

    }
}
