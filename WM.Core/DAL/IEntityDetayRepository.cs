using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;

namespace WM.Core.DAL
{
    public interface IEntityDetayRepository<T> where T : class, IComplexType, new()
    {
        T GetDetay(Expression<Func<T, bool>> filter);

        List<T> GetDetayList(Expression<Func<T, bool>> filter = null);
    }
}
