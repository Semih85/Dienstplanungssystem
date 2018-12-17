using System.Data;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WM.Northwind.Business.Abstract.Authorization;
using WM.Northwind.Business.Abstract.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.UI.Mvc.Areas.EczaneNobet.Controllers
{
    public class EczaneNobetMuafiyetController : Controller
    {
        #region ctor
        private IEczaneNobetMuafiyetService _eczaneNobetMuafiyetService;
        private IEczaneService _eczaneService;
        private IUserService _userService;

        public EczaneNobetMuafiyetController(IEczaneNobetMuafiyetService eczaneNobetMuafiyetService,
            IEczaneService eczaneService,
            IUserService userService)
        {
            _eczaneNobetMuafiyetService = eczaneNobetMuafiyetService;
            _eczaneService = eczaneService;
            _userService = userService;
        } 
        #endregion

        // GET: EczaneNobet/EczaneNobetMuafiyet
        public ActionResult Index()
        {
            var user = _userService.GetByUserName(User.Identity.Name);

            var eczaneler = _eczaneService.GetListByUser(user)
                .Where(w => w.KapanisTarihi == null)
                .Select(s => new { s.Id, s.Adi })
                .OrderBy(o => o.Adi);

            var model = _eczaneNobetMuafiyetService.GetDetaylar()
                .Where(w => eczaneler.Select(s => s.Id).Contains(w.EczaneId)).ToList();

            return View(model);
        }

        // GET: EczaneNobet/EczaneNobetMuafiyet/Details/5
        public ActionResult Details(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var eczaneNobetMuafiyet = _eczaneNobetMuafiyetService.GetDetayById(id);
            if (eczaneNobetMuafiyet == null)
            {
                return HttpNotFound();
            }
            return View(eczaneNobetMuafiyet);
        }

        // GET: EczaneNobet/EczaneNobetMuafiyet/Create
        public ActionResult Create()
        {
            var user = _userService.GetByUserName(User.Identity.Name);

            var eczaneler = _eczaneService.GetListByUser(user)
                .Where(s => s.KapanisTarihi == null)
                .OrderBy(s => s.Adi);

            ViewBag.EczaneId = new SelectList(eczaneler, "Id", "Adi");
            return View();
        }

        // POST: EczaneNobet/EczaneNobetMuafiyet/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EczaneId,BaslamaTarihi,BitisTarihi,Aciklama")] EczaneNobetMuafiyet eczaneNobetMuafiyet)
        {
            if (ModelState.IsValid)
            {
                _eczaneNobetMuafiyetService.Insert(eczaneNobetMuafiyet);
                return RedirectToAction("Index");
            }
            var user = _userService.GetByUserName(User.Identity.Name);

            var eczaneler = _eczaneService.GetListByUser(user)
                .Where(s => s.KapanisTarihi == null)
                .OrderBy(s => s.Adi);

            ViewBag.EczaneId = new SelectList(eczaneler, "Id", "Adi", eczaneNobetMuafiyet.EczaneId);
            return View(eczaneNobetMuafiyet);
        }

        // GET: EczaneNobet/EczaneNobetMuafiyet/Edit/5
        public ActionResult Edit(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var eczaneNobetMuafiyet = _eczaneNobetMuafiyetService.GetDetayById(id);
            if (eczaneNobetMuafiyet == null)
            {
                return HttpNotFound();
            }

            var user = _userService.GetByUserName(User.Identity.Name);

            var eczaneler = _eczaneService.GetListByUser(user)
                .Where(s => s.KapanisTarihi == null)
                .OrderBy(s => s.Adi);

            ViewBag.EczaneId = new SelectList(eczaneler, "Id", "Adi", eczaneNobetMuafiyet.EczaneId);
            return View(eczaneNobetMuafiyet);
        }

        // POST: EczaneNobet/EczaneNobetMuafiyet/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EczaneId,BaslamaTarihi,BitisTarihi,Aciklama")] EczaneNobetMuafiyet eczaneNobetMuafiyet)
        {
            if (ModelState.IsValid)
            {
                _eczaneNobetMuafiyetService.Update(eczaneNobetMuafiyet);
                return RedirectToAction("Index");
            }

            var user = _userService.GetByUserName(User.Identity.Name);

            var eczaneler = _eczaneService.GetListByUser(user)
                .Where(s => s.KapanisTarihi == null)
                .OrderBy(s => s.Adi);

            ViewBag.EczaneId = new SelectList(eczaneler, "Id", "Adi", eczaneNobetMuafiyet.EczaneId);
            return View(eczaneNobetMuafiyet);
        }

        // GET: EczaneNobet/EczaneNobetMuafiyet/Delete/5
        public ActionResult Delete(int id)
        {
            if (id < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var eczaneNobetMuafiyet = _eczaneNobetMuafiyetService.GetDetayById(id);
            if (eczaneNobetMuafiyet == null)
            {
                return HttpNotFound();
            }
            return View(eczaneNobetMuafiyet);
        }

        // POST: EczaneNobet/EczaneNobetMuafiyet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var eczaneNobetMuafiyet = _eczaneNobetMuafiyetService.GetDetayById(id);
            _eczaneNobetMuafiyetService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
