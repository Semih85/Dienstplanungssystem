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
    public class MenuRoleController : Controller
    {        
        private IMenuRoleService _menuRoleService;
        private IMenuService _menuService;
        private IRoleService _roleService;

        public MenuRoleController(IMenuRoleService menuRoleService,
                                  IMenuService menuService,
                                  IRoleService roleService)
        {
            _menuRoleService = menuRoleService;
            _menuService = menuService;
            _roleService = roleService;
        }
        // GET: EczaneNobet/MenuRole
        public ActionResult Index()
        {
            var model = _menuRoleService.GetMenuRoleDetaylar();
            return View(model);
        }

        // GET: EczaneNobet/MenuRole/Details/5
        public ActionResult Details(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var menuRole = _menuRoleService.GetById(id);
            MenuRoleDetay menuRoleDetay = _menuRoleService.GetMenuRoleDetaylar().Where(s => s.Id == menuRole.Id).SingleOrDefault();
            if (menuRole == null)
            {
                return HttpNotFound();
            }
            return View(menuRole);
        }

        // GET: EczaneNobet/MenuRole/Create
        public ActionResult Create()
        {
            ViewBag.MenuId = new SelectList(_menuService.GetList(), "Id", "LinkText");
            ViewBag.RoleId = new SelectList(_roleService.GetList(), "Id", "Name");
            return View();
        }

        // POST: EczaneNobet/MenuRole/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MenuId,RoleId")] MenuRole menuRole)
        {
            if (ModelState.IsValid)
            {
                _menuRoleService.Insert(menuRole);
                return RedirectToAction("Index");
            }

            ViewBag.MenuId = new SelectList(_menuService.GetList(), "Id", "LinkText", menuRole.MenuId);
            ViewBag.RoleId = new SelectList(_roleService.GetList(), "Id", "Name", menuRole.RoleId);
            return View(menuRole);
        }

        // GET: EczaneNobet/MenuRole/Edit/5
        public ActionResult Edit(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var menuRole = _menuRoleService.GetById(id);
            MenuRoleDetay menuRoleDetay = _menuRoleService.GetMenuRoleDetaylar().Where(s => s.Id == menuRole.Id).SingleOrDefault();
            if (menuRole == null)
            {
                return HttpNotFound();
            }
            ViewBag.MenuId = new SelectList(_menuService.GetList(), "Id", "LinkText", menuRole.MenuId);
            ViewBag.RoleId = new SelectList(_roleService.GetList(), "Id", "Name", menuRole.RoleId);
            return View(menuRole);
        }

        // POST: EczaneNobet/MenuRole/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MenuId,RoleId")] MenuRole menuRole)
        {
            if (ModelState.IsValid)
            {
                _menuRoleService.Update(menuRole);
                return RedirectToAction("Index");
            }
            ViewBag.MenuId = new SelectList(_menuService.GetList(), "Id", "LinkText", menuRole.MenuId);
            ViewBag.RoleId = new SelectList(_roleService.GetList(), "Id", "Name", menuRole.RoleId);
            return View(menuRole);
        }

        // GET: EczaneNobet/MenuRole/Delete/5
        public ActionResult Delete(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var menuRole = _menuRoleService.GetById(id);

            var menuRoleDetay = _menuRoleService.GetMenuRoleDetaylar().SingleOrDefault(s => s.Id == menuRole.Id);

            if (menuRole == null)
            {
                return HttpNotFound();
            }
            return View(menuRoleDetay);
        }

        // POST: EczaneNobet/MenuRole/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var menuRole = _menuRoleService.GetById(id);
            var menuRoleDetay = _menuRoleService.GetMenuRoleDetaylar().SingleOrDefault(s => s.Id == menuRole.Id);
            _menuRoleService.Delete(id);
            return RedirectToAction("Index");
        }
       
    }
}
