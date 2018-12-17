using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Entities.Concrete;

namespace WM.Northwind.Entities.ComplexTypes
{
    public class CategoryNbProduct
    {
        public int CategoryId { get; set; }
        public string CategoryName{ get; set; }
        public int ProductsInCategory { get; set; }
    }
}
