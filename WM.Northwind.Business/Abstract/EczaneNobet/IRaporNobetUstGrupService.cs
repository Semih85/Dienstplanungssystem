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
    public interface IRaporNobetUstGrupService
    {
        RaporNobetUstGrup GetById(int raporNobetUstGrupId);
        List<RaporNobetUstGrup> GetList();
        //List<RaporNobetUstGrup> GetByCategory(int categoryId);
        void Insert(RaporNobetUstGrup raporNobetUstGrup);
        void Update(RaporNobetUstGrup raporNobetUstGrup);
        void Delete(int raporNobetUstGrupId);
        RaporNobetUstGrupDetay GetDetayById(int raporNobetUstGrupId);
        List<RaporNobetUstGrupDetay> GetDetaylar();
        List<RaporNobetUstGrupDetay> GetDetaylar(int nobetUstGrupId);
    }
}