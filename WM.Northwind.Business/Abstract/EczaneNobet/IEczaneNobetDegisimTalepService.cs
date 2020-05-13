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
    public interface IEczaneNobetDegisimTalepService
    {
        EczaneNobetDegisimTalep GetById(int eczaneNobetDegisimId);
        List<EczaneNobetDegisimTalep> GetList();
        EczaneNobetDegisimTalep GetBySonucIdVeNobetGrupId(int eczaneNobetSonucId, int eczaneNobetGrupId);
        //List<EczaneNobetDegisimTalep> GetByCategory(int categoryId);
        void Insert(EczaneNobetDegisimTalep eczaneNobetDegisim);
        void Update(EczaneNobetDegisimTalep eczaneNobetDegisim);
        void Delete(int eczaneNobetDegisimId);
        //EczaneNobetDegisimTalepDetay GetDetayById(int eczaneNobetDegisimId);
        //List<EczaneNobetDegisimTalepDetay> GetDetaylar();
        //List<EczaneNobetDegisimTalepDetay> GetDetaylar(int nobetUstGrupId);
    }
}