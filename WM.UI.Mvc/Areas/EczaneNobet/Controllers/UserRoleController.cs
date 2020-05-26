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
using WM.Northwind.Entities.Concrete.Authorization;
using WM.UI.Mvc.Models;
using WM.UI.Mvc.Services;

namespace WM.UI.Mvc.Areas.EczaneNobet.Controllers
{
    [Authorize]
    [HandleError]
    public class UserRoleController : Controller
    {
        #region ctor
        private IUserRoleService _userRoleService;
        private IUserService _userService;
        private IRoleService _roleService;
        private IUserNobetUstGrupService _userNobetUstGrupService;
        private IUserEczaneOdaService _userEczaneOdaService;
        private INobetUstGrupService _nobetUstGrupService;
        private INobetUstGrupSessionService _nobetUstGrupSessionService;

        public UserRoleController(IUserRoleService userRoleService,
                                  IUserService userService,
                                  IRoleService roleService,
            IUserNobetUstGrupService userNobetUstGrupService,
            IUserEczaneOdaService userEczaneOdaService,
            INobetUstGrupService nobetUstGrupService,
            INobetUstGrupSessionService nobetUstGrupSessionService)
        {
            _userRoleService = userRoleService;
            _userService = userService;
            _roleService = roleService;
            _userNobetUstGrupService = userNobetUstGrupService;
            _userEczaneOdaService = userEczaneOdaService;
            _nobetUstGrupService = nobetUstGrupService;
            _nobetUstGrupSessionService = nobetUstGrupSessionService;
        }
        #endregion
        [Authorize(Roles = "Admin,Oda,Üst Grup")]
        // GET: EczaneNobet/UserRole
        public ActionResult Index()
        {
            var user = _userService.GetByUserName(User.Identity.Name);
            var rolIdler = _userService.GetUserRoles(user).OrderBy(s => s.RoleId).Select(u => u.RoleId).ToArray();
            var rolId = rolIdler.FirstOrDefault();

            var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            var userNobetUstGruplar = _userNobetUstGrupService.GetDetaylar(nobetUstGruplar.Select(s => s.Id).ToList());

            var ustGrupSession = _nobetUstGrupSessionService.GetSession("nobetUstGrup");

            var userRolDetaylar = new List<UserRoleDetay>();
            var userRoleIdler = new List<int>();
            var userIdler = new List<int>();

            if (rolId == 1)
            {//Admin için tüm kullanıclar
                userRolDetaylar = _userRoleService.GetDetaylar();
            }
            else if (rolId == 2 || rolId == 3)
            {//Oda ve üst grup yetkilisi için seçili nöbet üst grup kullanıcıları gelecek
                userIdler = _userNobetUstGrupService.GetListByNobetUstGrupId(ustGrupSession.Id)
                    .OrderBy(o => o.KullaniciAdi)
                    .Select(s => s.UserId)
                    .ToList();
                foreach (var item in userIdler)
                {
                    userRoleIdler = _userRoleService.GetDetayListByUserId(item)
                        .Select(s=>s.Id).ToList();
                    foreach (var item2 in userRoleIdler)
                    {
                        userRolDetaylar.Add(_userRoleService.GetDetayById(item2));

                    }

                }
            }

            //var model = _userRoleService.GetDetaylar();
            return View(userRolDetaylar);
        }

        // GET: EczaneNobet/UserRole/Details/5
        public ActionResult Details(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userRole = _userRoleService.GetDetayById(id);
            if (userRole == null)
            {
                return HttpNotFound();
            }
            return View(userRole);
        }

        // GET: EczaneNobet/UserRole/Create
        public ActionResult Create()
        {
            var ustGrupSession = _nobetUstGrupSessionService.GetSession("nobetUstGrup");
            var user = _userService.GetByUserName(User.Identity.Name);
            var rolIdler = _userService.GetUserRoles(user).OrderBy(s => s.RoleId).Select(u => u.RoleId).ToArray();
            var rolId = rolIdler.FirstOrDefault();

            List<int> userIdler = _userNobetUstGrupService.GetListByNobetUstGrupId(ustGrupSession.Id)
                   .OrderBy(o => o.KullaniciAdi)
                   .Select(s => s.UserId)
                   .ToList();

            List<User> users = new List<User>();
            foreach (var item in userIdler)
            {
                users.Add(_userService.GetById(item));
            }
            if(rolId == 1)// admin , admin yetkisi verebilsin
                ViewBag.RoleId = new SelectList(_roleService.GetList().Select(s => new { s.Id, s.Name }), "Id", "Name");
            else//admin olamyan sadece altına yetki verebilsin
                ViewBag.RoleId = new SelectList(_roleService.GetList().Where(w => w.Id > rolId).Select(s => new { s.Id, s.Name }), "Id", "Name");

            ViewBag.UserId = new SelectList(users.Select(s => new { s.Id, s.UserName }), "Id", "UserName");
            return View();
        }

        // POST: EczaneNobet/UserRole/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RoleId,UserId,BaslamaTarihi,BitisTarihi")] UserRole userRole)
        {
            if (ModelState.IsValid)
            {
                _userRoleService.Insert(userRole);
                return RedirectToAction("Index");
            }

            ViewBag.RoleId = new SelectList(_roleService.GetList().Select(s => new { s.Id, s.Name }), "Id", "Name", userRole.RoleId);
            ViewBag.UserId = new SelectList(_userService.GetList().Select(s => new { s.Id, s.UserName }), "Id", "UserName", userRole.UserId);
            return View(userRole);
        }

        // GET: EczaneNobet/UserRole/Edit/5
        public ActionResult Edit(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userRole = _userRoleService.GetDetayById(id);
            if (userRole == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleId = new SelectList(_roleService.GetList().Select(s => new { s.Id, s.Name }), "Id", "Name", userRole.RoleId);
            ViewBag.UserId = new SelectList(_userService.GetList().Select(s => new { s.Id, s.UserName }), "Id", "UserName", userRole.UserId);
            return View(userRole);
        }

        // POST: EczaneNobet/UserRole/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RoleId,UserId,BaslamaTarihi,BitisTarihi")] UserRole userRole)
        {
            if (ModelState.IsValid)
            {
                _userRoleService.Update(userRole);
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(_roleService.GetList().Select(s => new { s.Id, s.Name }), "Id", "Name", userRole.RoleId);
            ViewBag.UserId = new SelectList(_userService.GetList().Select(s => new { s.Id, s.UserName }), "Id", "UserName", userRole.UserId);
            return View(userRole);
        }

        // GET: EczaneNobet/UserRole/Delete/5
        public ActionResult Delete(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userRole = _userRoleService.GetDetayById(id);
            if (userRole == null)
            {
                return HttpNotFound();
            }
            return View(userRole);
        }

        // POST: EczaneNobet/UserRole/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var userRole = _userRoleService.GetDetayById(id);
            _userRoleService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
