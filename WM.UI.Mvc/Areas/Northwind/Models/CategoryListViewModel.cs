using System.Collections.Generic;
using WM.Northwind.Entities.Concrete;

namespace WM.UI.Mvc.Areas.Northwind.Models
{
    public class CategoryListViewModel
    {
        public List<Category> Categories { get; internal set; }
        public int CurrentCategory { get; internal set; }

    }
}