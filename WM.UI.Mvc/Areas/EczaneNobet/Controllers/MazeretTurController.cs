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
    [Authorize]
    public class MazeretTurController : Controller
    {   
        private IMazeretTurService _mazeretTurService;

        public MazeretTurController(IMazeretTurService mazeretTurService)
        {
            _mazeretTurService = mazeretTurService;
        }
        // GET: EczaneNobet/MazeretTur
        public ActionResult Index()
        {
            var model = _mazeretTurService.GetList();
            return View(model);
        }

        // GET: EczaneNobet/MazeretTur/Details/5
        public ActionResult Details(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MazeretTur mazeretTur = _mazeretTurService.GetById(id);
            if (mazeretTur == null)
            {
                return HttpNotFound();
            }
            return View(mazeretTur);
        }

        // GET: EczaneNobet/MazeretTur/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EczaneNobet/MazeretTur/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Adi,Aciklama")] MazeretTur mazeretTur)
        {
            if (ModelState.IsValid)
            {
                _mazeretTurService.Insert(mazeretTur);
                return RedirectToAction("Index");
            }

            return View(mazeretTur);
        }

        // GET: EczaneNobet/MazeretTur/Edit/5
        public ActionResult Edit(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MazeretTur mazeretTur = _mazeretTurService.GetById(id);
            if (mazeretTur == null)
            {
                return HttpNotFound();
            }
            return View(mazeretTur);
        }

        // POST: EczaneNobet/MazeretTur/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Adi,Aciklama")] MazeretTur mazeretTur)
        {
            if (ModelState.IsValid)
            {
                _mazeretTurService.Update(mazeretTur);
                return RedirectToAction("Index");
            }
            return View(mazeretTur);
        }

        // GET: EczaneNobet/MazeretTur/Delete/5
        public ActionResult Delete(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MazeretTur mazeretTur = _mazeretTurService.GetById(id);
            if (mazeretTur == null)
            {
                return HttpNotFound();
            }
            return View(mazeretTur);
        }

        // POST: EczaneNobet/MazeretTur/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MazeretTur mazeretTur = _mazeretTurService.GetById(id);
            _mazeretTurService.Delete(id);
            return RedirectToAction("Index");
        }
        
    }
}
