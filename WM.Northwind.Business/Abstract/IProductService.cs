using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Entities.Concrete;

namespace WM.Northwind.Business.Abstract
{
    public interface IProductService
    {
        Product GetById(int productId);
        List<Product> GetList();
        List<Product> GetByCategory(int categoryId);
        void Insert(Product product);
        void Update(Product product);
        void Delete(int productId);

    }
}
