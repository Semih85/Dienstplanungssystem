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
    public interface IEczaneNobetGrupKisitService
    {
        EczaneNobetGrupKisit GetById(int eczaneNobetGrupKisitId);
        List<EczaneNobetGrupKisit> GetList();
        //List<EczaneNobetGrupKisit> GetByCategory(int categoryId);
        void Insert(EczaneNobetGrupKisit eczaneNobetGrupKisit);
        void Update(EczaneNobetGrupKisit eczaneNobetGrupKisit);
        void Delete(int eczaneNobetGrupKisitId);
        EczaneNobetGrupKisitDetay GetDetayById(int eczaneNobetGrupKisitId);
        List<EczaneNobetGrupKisitDetay> GetDetaylar();
        List<EczaneNobetGrupKisitDetay> GetDetaylar(int nobetUstGrupId);
    }
}