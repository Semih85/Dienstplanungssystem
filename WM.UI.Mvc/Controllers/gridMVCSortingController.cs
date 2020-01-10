using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WM.Northwind.Business.Abstract.EczaneNobet;
using WM.UI.Mvc.HtmlHelpers;

namespace WM.UI.Mvc.Controllers
{
    public class gridMVCSortingController : Controller
    {
        IEczaneGrupService _eczaneGrupService;

        private IGridMvcHelper gridMvcHelper;
        //private IDemoData data;

        
    
            //this.data = new FootballersData();

        

        public gridMVCSortingController(IEczaneGrupService eczaneGrupService)
        {
            this.gridMvcHelper = new GridMvcHelper();
            _eczaneGrupService = eczaneGrupService;
        }
        // GET: gridMVCSorting
        public ActionResult Index()
        {
            return View();
        }
        [ChildActionOnly]
        public ActionResult GridMVCSortingPartial()
        {
            var items = this._eczaneGrupService.GetDetaylar().AsQueryable().OrderBy(f => f.Id);
            var grid = this.gridMvcHelper.GetAjaxGrid(items);

            return PartialView("GridMVCSortingPartial", grid);
        }

        [HttpGet]
        public ActionResult GridMVCSortingPager(int? page)
        {
            var items = this._eczaneGrupService.GetDetaylar().AsQueryable().OrderBy(f => f.Id);
            var grid = this.gridMvcHelper.GetAjaxGrid(items, page);

            object jsonData = this.gridMvcHelper.GetGridJsonData(grid, "GridMVCSortingPartial", this);

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
    }
}