using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WM.Northwind.Business.Abstract;

namespace WM.UI.Mvc.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private IAdminService _adminService;
        // GET: Admin
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public ActionResult Index()
        {
            var alanya = new int[] { 20, 20, 18 };
            var antalyaMerkez = new int[] { 56, 43, 31, 33, 45, 47, 29, 44, 28, 27, 44 };

            ViewBag.NobetGruplarAlanya = alanya;
            ViewBag.NobetGruplarAntalyaMerkez = antalyaMerkez;

            return View();
        }

    }
}