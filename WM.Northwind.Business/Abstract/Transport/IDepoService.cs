using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Entities.Concrete;
using WM.Northwind.Entities.Concrete.Transport;

namespace WM.Northwind.Business.Abstract.Transport
{
    public interface IDepoService
    {
        Depo GetById(int depoId);
        List<Depo> GetList();
        //List<Depo> GetByCategory(int categoryId);
        void Insert(Depo depo);
        void Update(Depo depo);
        void Delete(int depoId);

    }
}
