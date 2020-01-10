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
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;
using WM.UI.Mvc.Models;

namespace WM.UI.Mvc.Areas.EczaneNobet.Controllers
{
    [Authorize]
    [HandleError]
    public class MenuAltRoleController : Controller
    {
        private IMenuAltRoleService _menuAltRoleService;
        private IMenuAltService _menuAltService;
        private IRoleService _roleService;

        public MenuAltRoleController(IMenuAltRoleService menuAltRoleService,
                                     IMenuAltService menuAltService,
                                     IRoleService roleService)
        {
            _menuAltRoleService = menuAltRoleService;
            _menuAltService = menuAltService;
            _roleService = roleService;
        }
        // GET: EczaneNobet/MenuAltRole
        public ActionResult Index()
        {
            var model = _menuAltRoleService.GetMenuAltRoleDetaylar();
            //var menuAltRoles = db.MenuAltRoles.Include(m => m.MenuAlt).Include(m => m.Role);
            return View(model);
        }

        // GET: EczaneNobet/MenuAltRole/Details/5
        public ActionResult Details(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var menuAltRole = _menuAltRoleService.GetById(id);
            var menuAltRoleDetay = _menuAltRoleService.GetMenuAltRoleDetaylar()
                .Where(s => s.Id == menuAltRole.Id).SingleOrDefault();

            if (menuAltRole == null)
            {
                return HttpNotFound();
            }
            return View(menuAltRoleDetay);
        }

        // GET: EczaneNobet/MenuAltRole/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            var menuAltlar = _menuAltService.GetDetaylar()
                .Select(s => new MyDrop { Id = s.Id, Value = $"{s.LinkText} ({s.MenuLinkText})" })
                .OrderBy(w => w.Value)
                .ToList();
            var roller = _roleService.GetList().Select(s => new MyDrop { Id = s.Id, Value = s.Name });
            ViewBag.MenuAltId = new SelectList(menuAltlar, "Id", "Value");
            ViewBag.RoleId = new SelectList(roller, "Id", "Value");
            return View();
        }

        // POST: EczaneNobet/MenuAltRole/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "Id,MenuAltId,RoleId")] MenuAltRole menuAltRole)
        {
            if (ModelState.IsValid)
            {
                _menuAltRoleService.Insert(menuAltRole);
                return RedirectToAction("Index");
            }

            var menuAltlar = _menuAltService.GetDetaylar()
                .Select(s => new MyDrop { Id = s.Id, Value = $"{s.LinkText} ({s.MenuLinkText})" })
                .OrderBy(w => w.Value)
                .ToList();
            var roller = _roleService.GetList().Select(s => new MyDrop { Id = s.Id, Value = s.Name });
            ViewBag.MenuAltId = new SelectList(menuAltlar, "Id", "Value");
            ViewBag.RoleId = new SelectList(roller, "Id", "Value");
            return View(menuAltRole);
        }

        // GET: EczaneNobet/MenuAltRole/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var menuAltRole = _menuAltRoleService.GetById(id);
            if (menuAltRole == null)
            {
                return HttpNotFound();
            }
            var menuAltlar = _menuAltService.GetDetaylar()
                .Select(s => new MyDrop { Id = s.Id, Value = $"{s.LinkText} ({s.MenuLinkText})" })
                .OrderBy(w => w.Value)
                .ToList();
            var roller = _roleService.GetList().Select(s => new MyDrop { Id = s.Id, Value = s.Name });
            ViewBag.MenuAltId = new SelectList(menuAltlar, "Id", "Value");
            ViewBag.RoleId = new SelectList(roller, "Id", "Value");
            return View(menuAltRole);
        }

        // POST: EczaneNobet/MenuAltRole/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "Id,MenuAltId,RoleId")] MenuAltRole menuAltRole)
        {
            if (ModelState.IsValid)
            {
                _menuAltRoleService.Update(menuAltRole);
                return RedirectToAction("Index");
            }
            var menuAltlar = _menuAltService.GetDetaylar()
                .Select(s => new MyDrop { Id = s.Id, Value = $"{s.LinkText} ({s.MenuLinkText})" })
                .OrderBy(w => w.Value)
                .ToList();

            var roller = _roleService.GetList().Select(s => new MyDrop { Id = s.Id, Value = s.Name });
            ViewBag.MenuAltId = new SelectList(menuAltlar, "Id", "Value");
            ViewBag.RoleId = new SelectList(roller, "Id", "Value");
            return View(menuAltRole);
        }

        // GET: EczaneNobet/MenuAltRole/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var menuAltRole = _menuAltRoleService.GetById(id);
            var menuAltRoleDetay = _menuAltRoleService.GetMenuAltRoleDetaylar().Where(s => s.Id == menuAltRole.Id).SingleOrDefault();
            if (menuAltRole == null)
            {
                return HttpNotFound();
            }
            return View(menuAltRoleDetay);
        }

        // POST: EczaneNobet/MenuAltRole/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            var menuAltRole = _menuAltRoleService.GetById(id);
            _menuAltRoleService.Delete(id);
            return RedirectToAction("Index");
        }       
    }
}
