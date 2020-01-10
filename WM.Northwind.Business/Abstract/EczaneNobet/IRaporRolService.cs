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
    public interface IRaporRolService
    {
        RaporRol GetById(int raporRolId);
        List<RaporRol> GetList();
        //List<RaporRol> GetByCategory(int categoryId);
        void Insert(RaporRol raporRol);
        void Update(RaporRol raporRol);
        void Delete(int raporRolId);
        RaporRolDetay GetDetayById(int raporRolId);
        List<RaporRolDetay> GetDetaylar();
        List<RaporRolDetay> GetDetaylar(int rolId);
    }
}