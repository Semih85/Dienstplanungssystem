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
using WM.UI.Mvc.Areas.EczaneNobet.Models;
using WM.UI.Mvc.Models;

namespace WM.UI.Mvc.Areas.EczaneNobet.Controllers
{
    [Authorize]
    [HandleError]
    public class MenuController : Controller
    {
        private IMenuService _menuService;
        private IMenuAltService _menuAltService;
        private IUserService _userService;
        private IMenuRoleService _menuRoleService;
        private IMenuAltRoleService _menuAltRoleService;

        public MenuController(IMenuService menuService,
                              IMenuAltService menuAltService,
                              IUserService userService,
                              IMenuRoleService menuRoleService,
                              IMenuAltRoleService menuAltRoleService)
        {
            _menuService = menuService;
            _menuAltService = menuAltService;
            _userService = userService;
            _menuRoleService = menuRoleService;
            _menuAltRoleService = menuAltRoleService;
        }

        // GET: EczaneNobet/Menu
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var model = _menuService.GetList();
            return View(model);
        }

        [ChildActionOnly]
        public PartialViewResult MenuPartial()
        {
            var user = _userService.GetByUserName(User.Identity.Name);
            var rolIdler = _userService.GetUserRoles(user).OrderBy(s => s.RoleId).Select(u => u.RoleId).ToArray();

            var rolId = rolIdler.FirstOrDefault();
            var menuler = _menuRoleService.GetByRoleId(rolId);
            var menuAltlar = _menuAltRoleService.GetByRoleId(rolId);

            var model = new MenuPartialViewModel()
            {
                Menuler = _menuService.GetList().Where(s => s.PasifMi == false && menuler.Select(m => m.MenuId).Contains(s.Id)).ToList(),
                MenuAltlar = _menuAltService.GetList().Where(s => s.PasifMi == false && menuAltlar.Select(m => m.MenuAltId).Contains(s.Id)).ToList(),
                MenuAltlarTumu = _menuAltService.GetList()
            };

            return PartialView(model);
        }

        // GET: EczaneNobet/Menu/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = _menuService.GetById(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // GET: EczaneNobet/Menu/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: EczaneNobet/Menu/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "Id,LinkText,ActionName,ControllerName,AreaName,SpanCssClass,PasifMi")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                _menuService.Insert(menu);
                return RedirectToAction("Index");
            }

            return View(menu);
        }

        // GET: EczaneNobet/Menu/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = _menuService.GetById(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // POST: EczaneNobet/Menu/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "Id,LinkText,ActionName,ControllerName,AreaName,SpanCssClass,PasifMi")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                _menuService.Update(menu);
                return RedirectToAction("Index");
            }
            return View(menu);
        }

        // GET: EczaneNobet/Menu/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = _menuService.GetById(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // POST: EczaneNobet/Menu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Menu menu = _menuService.GetById(id);
            _menuService.Delete(id);
            return RedirectToAction("Index");
        }


    }
}
