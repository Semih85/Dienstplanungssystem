using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;
//using WM.Northwind.Entities.Concrete.Optimization.EczaneNobet;

namespace WM.Northwind.Business.Abstract.EczaneNobet
{
    public interface IDebugEczaneService
    {
        DebugEczane GetById(int debugEczaneId);
        List<DebugEczane> GetList();
        //List<DebugEczane> GetByCategory(int categoryId);
        void Insert(DebugEczane debugEczane);
        void Update(DebugEczane debugEczane);
        void Delete(int debugEczaneId);
        DebugEczaneDetay GetDetayById(int debugEczaneId);
        List<DebugEczaneDetay> GetDetaylar();
        List<DebugEczaneDetay> GetDetaylar(int nobetUstGrupId);
        List<DebugEczaneDetay> GetDetaylarAktifOlanlar(int nobetUstGrupId);
    }
}