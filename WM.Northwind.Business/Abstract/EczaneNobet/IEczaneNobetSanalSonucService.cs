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
    public interface IEczaneNobetSanalSonucService
    {
        EczaneNobetSanalSonuc GetById(int eczaneNobetSanalSonucId);
        List<EczaneNobetSanalSonuc> GetList();
        //List<EczaneNobetSanalSonuc> GetDetaylar(int eczaneNobetGrupId);
        void Insert(EczaneNobetSanalSonuc eczaneNobetSanalSonuc);
        void Update(EczaneNobetSanalSonuc eczaneNobetSanalSonuc);
        void Delete(int eczaneNobetSanalSonucId);
        EczaneNobetSanalSonucDetay GetDetayById(int eczaneNobetSanalSonucId);
        List<EczaneNobetSanalSonucDetay> GetDetaylar();
        List<EczaneNobetSanalSonucDetay> GetDetaylar(int nobetUstGrupId);
    }
}