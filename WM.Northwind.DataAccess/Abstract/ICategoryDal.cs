using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.DAL;
using WM.Northwind.Entities.ComplexTypes;
using WM.Northwind.Entities.Concrete;

namespace WM.Northwind.DataAccess.Abstract
{
    public interface ICategoryDal: IEntityRepository<Category>
    {
        List<CategoryNbProduct> GetCategoryNbProduct();
    }
}
