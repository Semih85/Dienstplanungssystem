using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WM.Core.DAL;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.DataAccess.Abstract.EczaneNobet
{
    public interface ITakvimDal : IEntityRepository<Takvim>
    {
        List<TakvimDetay> GetTakvimDetaylar(Expression<Func<TakvimDetay, bool>> filter = null);
        TakvimDetay GetTakvimDetay(Expression<Func<TakvimDetay, bool>> filter = null);      
    }
}
