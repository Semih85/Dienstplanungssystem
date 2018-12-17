using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;

namespace WM.Northwind.Entities.Concrete
{
    public partial class Category : IEntity
    {
        public Category()
        {
            Products = new List<Product>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }

        //navigation
        public virtual List<Product> Products { get; set; }
    }
}
