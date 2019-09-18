using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WM.Northwind.Business.Abstract.Authorization;
using WM.Northwind.Business.Abstract.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.UI.Mvc.Areas.EczaneNobet.Controllers
{
    [Authorize]
    public class UserEczaneController : Controller
    {
        #region ctor
        private IUserEczaneService _userEczaneService;
        private IUserNobetUstGrupService _userNobetUstGrupService;
        private IEczaneService _eczaneService;
        private IUserService _userService;
        private INobetUstGrupService _nobetUstGrupService;

        public UserEczaneController(IUserEczaneService userEczaneService,
                                    IUserNobetUstGrupService userNobetUstGrupService,
                                    IEczaneService eczaneService,
                                    IUserService userService,
                                    INobetUstGrupService nobetUstGrupService)
        {
            _userEczaneService = userEczaneService;
            _userNobetUstGrupService = userNobetUstGrupService;
            _eczaneService = eczaneService;
            _userService = userService;
            _nobetUstGrupService = nobetUstGrupService;
        }
        #endregion

        // GET: EczaneNobet/UserEczane
        public ActionResult Index()
        {
            var user = _userService.GetByUserName(User.Identity.Name);
            var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);

            var userEczaneler = _userEczaneService.GetDetaylar(nobetUstGruplar.Select(s => s.Id).ToList());

            return View(userEczaneler);
        }

        // GET: EczaneNobet/UserEczane/Details/5
        public ActionResult Details(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userEczane = _userEczaneService.GetDetayById(id);
            if (userEczane == null)
            {
                return HttpNotFound();
            }
            return View(userEczane);
        }

        // GET: EczaneNobet/UserEczane/Create
        public ActionResult Create()
        {
            var user = _userService.GetByUserName(User.Identity.Name);
            var eczaneler = _eczaneService.GetListByUser(user).Select(s => new { s.Id, s.Adi }).OrderBy(w => w.Adi);
            var nobetUstGruplar = _nobetUstGrupService.GetListByUser(user);
            var userNobetUstGruplar = _userNobetUstGrupService.GetDetaylar(nobetUstGruplar.Select(s => s.Id).ToList());

            ViewBag.EczaneId = new SelectList(eczaneler, "Id", "Adi");
            ViewBag.UserId = new SelectList(userNobetUstGruplar.Select(s => new { s.Id, s.KullaniciAdi }), "Id", "KullaniciAdi");
            return View();
        }

        // POST: EczaneNobet/UserEczane/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EczaneId,UserId,BaslamaTarihi,BitisTarihi")] UserEczane userEczane)
        {
            if (ModelState.IsValid)
            {
                _userEczaneService.Insert(userEczane);
                return RedirectToAction("Index");
            }

            ViewBag.EczaneId = new SelectList(_eczaneService.GetList().Select(s => new { s.Id, s.Adi }), "Id", "Adi", userEczane.EczaneId);
            ViewBag.UserId = new SelectList(_userService.GetList().Select(s => new { s.Id, s.UserName }), "Id", "UserName", userEczane.UserId);
            return View(userEczane);
        }

        // GET: EczaneNobet/UserEczane/Edit/5
        public ActionResult Edit(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userEczane = _userEczaneService.GetDetayById(id);
            if (userEczane == null)
            {
                return HttpNotFound();
            }
            var user = _userService.GetByUserName(User.Identity.Name);
            var eczaneler = _eczaneService.GetListByUser(user).Select(s => new { s.Id, s.Adi }).OrderBy(w => w.Adi);
            ViewBag.EczaneId = new SelectList(eczaneler, "Id", "Adi", userEczane.EczaneId);
            ViewBag.UserId = new SelectList(_userService.GetList().Select(s => new { s.Id, s.UserName }), "Id", "UserName", userEczane.UserId);
            return View(userEczane);
        }

        // POST: EczaneNobet/UserEczane/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EczaneId,UserId,BaslamaTarihi,BitisTarihi")] UserEczane userEczane)
        {
            if (ModelState.IsValid)
            {
                _userEczaneService.Update(userEczane);
                return RedirectToAction("Index");
            }
            ViewBag.EczaneId = new SelectList(_eczaneService.GetList().Select(s => new { s.Id, s.Adi }), "Id", "Adi", userEczane.EczaneId);
            ViewBag.UserId = new SelectList(_userService.GetList().Select(s => new { s.Id, s.UserName }), "Id", "UserName", userEczane.UserId);
            return View(userEczane);
        }

        // GET: EczaneNobet/UserEczane/Delete/5
        public ActionResult Delete(int id)
        {
            if (id < 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userEczane = _userEczaneService.GetDetayById(id);
            if (userEczane == null)
            {
                return HttpNotFound();
            }
            return View(userEczane);
        }

        // POST: EczaneNobet/UserEczane/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var userEczane = _userEczaneService.GetDetayById(id);
            _userEczaneService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
