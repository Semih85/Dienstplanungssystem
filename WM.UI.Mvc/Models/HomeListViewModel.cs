using System.Collections.Generic;
using WM.Northwind.Entities.Concrete;

namespace WM.UI.Mvc.Models
{
    public class HomeListViewModel
    {
        public HomeListViewModel()
        {
        }

        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
    }
}