using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Business.Abstract;
using WM.Northwind.DataAccess.Abstract;
using WM.Northwind.Entities.ComplexTypes;
using WM.Northwind.Entities.Concrete;

namespace WM.Northwind.Business.Concrete.Managers
{
    public class CategoryManager : ICategoryService
    {
        private ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal CategoryDal)
        {
            _categoryDal = CategoryDal;
        }

        public List<CategoryNbProduct> GetCategoryNbProduct()
        {
            return _categoryDal.GetCategoryNbProduct();
        }

        public List<Category> GetList()
        {
            return _categoryDal.GetList();
        }

    }
}
