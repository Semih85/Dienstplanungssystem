using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WM.Northwind.Business.Abstract;
using WM.UI.Mvc.Areas.Northwind.Models;
using WM.UI.Mvc.Models;

namespace WM.UI.Mvc.Areas.Northwind.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;
        private ICategoryService _categoryService;


        public ProductController(IProductService ProductService, ICategoryService categoryService)
        {
            _productService = ProductService;
            _categoryService = categoryService;
        }

        // GET: Product
        public ActionResult Index(int page = 1, int categoryId = 0)
        {
            int pageSize = 10;
            var productsByCategory = _productService.GetByCategory(categoryId);

            var model = new ProductListViewModel
            {
                Products = productsByCategory.Skip((page - 1) * pageSize).Take(pageSize).ToList(),
                PageCount = (int)Math.Ceiling(productsByCategory.Count / (double)pageSize),
                PageSize = pageSize,
                CurrentCategory = categoryId,
                CurrentCategoryName = _categoryService.GetList()
                                    .Where(x => x.CategoryId == categoryId)
                                    .Select(s => s.CategoryName)
                                    .SingleOrDefault(),
                CurrentPage = page,
                TotalRows = productsByCategory.Count
            };

            return View(model);
        }

        public ActionResult IndexHelper()
        {
            return View();
        }
    }
}