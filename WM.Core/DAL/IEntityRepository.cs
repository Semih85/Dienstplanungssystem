using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;

namespace WM.Core.DAL
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        T Get(Expression<Func<T, bool>> filter);

        List<T> GetList(Expression<Func<T, bool>> filter = null);

        void Insert(T entity);

        void Insert(List<T> entities);

        void Update(T entity);

        void Update(List<T> entities);

        void Delete(T entity);

        void Delete(List<T> entities);

    }
}
