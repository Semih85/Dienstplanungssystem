using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WM.Northwind.Business.Abstract;
using WM.Northwind.Entities.ComplexTypes;
using WM.UI.Mvc.Areas.Northwind.Models;
using WM.UI.Mvc.Models;

namespace WM.UI.Mvc.Areas.Northwind.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category

        private ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        public ActionResult Index()
        {
            var categories = _categoryService.GetList();

            var model = new CategoryListViewModel
            {
                Categories = categories
            };

            return View(model);
        }


        [ChildActionOnly]
        public PartialViewResult CategoryPartialView()
        {
            var categories = _categoryService.GetCategoryNbProduct();
            var currCtgId = Convert.ToInt32(Request.QueryString["categoryId"]);

            var model = new CategoryListPartialViewModel
            {
                CategoryNbProducts = categories,
                CurrentCategory = currCtgId
            };

            return PartialView("CategoryPartialView", model);
        }
    }

}