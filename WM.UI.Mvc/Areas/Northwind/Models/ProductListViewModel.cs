using System.Collections.Generic;
using WM.Northwind.Entities.Concrete;

namespace WM.UI.Mvc.Areas.Northwind.Models
{
    public class ProductListViewModel
    {
        public int CurrentCategory { get; internal set; }
        public int CurrentPage { get; internal set; }
        public int PageCount { get; internal set; }
        public int PageSize { get; internal set; }
        public List<Product> Products { get; internal set; }
        public int TotalRows { get; internal set; }
        public string CurrentCategoryName { get; internal set; }
    }
}