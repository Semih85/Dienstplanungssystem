using System.Collections.Generic;
using WM.Northwind.Entities.ComplexTypes;
using WM.Northwind.Entities.Concrete;

namespace WM.Northwind.Business.Abstract
{
    public interface ICategoryService
    {
        List<Category> GetList();

        List<CategoryNbProduct> GetCategoryNbProduct();
       
            //int NbProductByCategory();

            //List<Category> GetByCategory(int categoryId);
            //void Add(Category category);
            //void Update(Category category);
            //void Delete(Category category);
            //Category GetById(int categoryId);
        }
}
