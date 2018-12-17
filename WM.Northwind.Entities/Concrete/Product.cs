using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;

namespace WM.Northwind.Entities.Concrete
{
    public partial class Product: IEntity //, IAuditEntity
    {

        public Product()
        {
            Category = new Category();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal UnitPrice { get; set; }
        public short UnitsInStock { get; set; }

        //navigation
        public Category Category { get; set; }

        // IAuditEntity implementation
        //public string CreatedUserName { get; set; }
        //public DateTime CreatedDate { get; set; }
        //public string UpdatedUsername { get; set; }
        //public DateTime UpdatedDate { get; set; }
    }
}
