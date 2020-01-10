using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WM.Northwind.Business.Abstract;
using WM.Northwind.Business.Abstract.EczaneNobet;

namespace WM.UI.Mvc.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private IAdminService _adminService;
        private ILogService _logService;
        // GET: Admin
        public AdminController(IAdminService adminService,
            ILogService logService)
        {
            _adminService = adminService;
            _logService = logService;
        }

        public ActionResult Index()
        {
            //var alanya = new int[] { 20, 20, 18 };
            //var antalyaMerkez = new int[] { 56, 43, 31, 33, 45, 47, 29, 44, 28, 27, 44 };

            //ViewBag.NobetGruplarAlanya = alanya;
            //ViewBag.NobetGruplarAntalyaMerkez = antalyaMerkez; WIN-Q7ERUVAK1T4\\IWPD_1(nobetyaz)
            //var loglarTumu = _logService.GetList();

            return View();
        }

        public JsonResult GetLogs()
        {
            var loglar = _logService.GetList("Semih");

            var jsonResult = Json(loglar, JsonRequestBehavior.AllowGet);

            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

    }
}