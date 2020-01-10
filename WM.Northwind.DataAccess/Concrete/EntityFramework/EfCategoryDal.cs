using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.DAL.EntityFramework;
using WM.Northwind.DataAccess.Abstract;
using WM.Northwind.DataAccess.Concrete.EntityFramework.Contexts;
using WM.Northwind.Entities.ComplexTypes;
using WM.Northwind.Entities.Concrete;

namespace WM.Northwind.DataAccess.Concrete.EntityFramework
{
    public class EfCategoryDal : EfEntityRepositoryBase<Category, NorthwindContext>, ICategoryDal
    {
        public List<CategoryNbProduct> GetCategoryNbProduct()
        {
            using (var context = new NorthwindContext())
            {
                var kategoriler = context.Categories
                                    .Select(c => new CategoryNbProduct()
                                    {
                                        CategoryId = c.CategoryId,
                                        CategoryName = c.CategoryName,
                                        ProductsInCategory = c.Products.Count
                                    }).ToList();

                return kategoriler;
            }

        }
    }
}
