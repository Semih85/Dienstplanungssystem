using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;
using WM.Northwind.Entities.Concrete.Optimization.EczaneNobet;

namespace WM.Northwind.Business.Abstract.EczaneNobet
{
    public interface IIstekService
    {
        Istek GetById(int istekId);
        List<Istek> GetList();
        //List<Mazeret> GetByCategory(int categoryId);
        void Insert(Istek istek);
        void Update(Istek istek);
        void Delete(int istekId);
    }
}
