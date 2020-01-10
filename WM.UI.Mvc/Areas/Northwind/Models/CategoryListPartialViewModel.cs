using System.Collections.Generic;
using WM.Northwind.Entities.ComplexTypes;

namespace WM.UI.Mvc.Areas.Northwind.Models
{
    public class CategoryListPartialViewModel
    {
        public List<CategoryNbProduct> CategoryNbProducts { get; set; }
        public int CurrentCategory { get; set; }
    }
}