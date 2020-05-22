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
    public interface IEczaneNobetDegisimTalepService
    {
        EczaneNobetDegisimTalep GetById(int eczaneNobetDegisimTalepId);
        List<EczaneNobetDegisimTalep> GetList();
        //List<EczaneNobetDegisimTalep> GetByCategory(int categoryId);
        void Insert(EczaneNobetDegisimTalep eczaneNobetDegisimTalep);
        void Update(EczaneNobetDegisimTalep eczaneNobetDegisimTalep);
        void Delete(int eczaneNobetDegisimTalepId);
        EczaneNobetDegisimTalepDetay GetDetayById(int eczaneNobetDegisimTalepId);
        List <EczaneNobetDegisimTalepDetay> GetDetaylar();
        List<EczaneNobetDegisimTalepDetay> GetDetaylar(int nobetUstGrupId);

    }
} 