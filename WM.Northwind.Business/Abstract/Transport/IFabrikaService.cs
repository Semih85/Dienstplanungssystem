using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Entities.Concrete;
using WM.Northwind.Entities.Concrete.Transport;

namespace WM.Northwind.Business.Abstract.Transport
{
    public interface IFabrikaService
    {
        Fabrika GetById(int fabrikaId);
        List<Fabrika> GetList();
        //List<Fabrika> GetByCategory(int categoryId);
        void Insert(Fabrika fabrika);
        void Update(Fabrika fabrika);
        void Delete(int fabrikaId);

    }
}
