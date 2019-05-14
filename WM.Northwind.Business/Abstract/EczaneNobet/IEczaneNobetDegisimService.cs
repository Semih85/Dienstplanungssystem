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
    public interface IEczaneNobetDegisimService
    {
        EczaneNobetDegisim GetById(int eczaneNobetDegisimId);
        List<EczaneNobetDegisim> GetList();
        EczaneNobetDegisim GetBySonucIdVeNobetGrupId(int eczaneNobetSonucId, int eczaneNobetGrupId);
        //List<EczaneNobetDegisim> GetByCategory(int categoryId);
        void Insert(EczaneNobetDegisim eczaneNobetDegisim);
        void Update(EczaneNobetDegisim eczaneNobetDegisim);
        void Delete(int eczaneNobetDegisimId);
        EczaneNobetDegisimDetay GetDetayById(int eczaneNobetDegisimId);
        List<EczaneNobetDegisimDetay> GetDetaylar();
        List<EczaneNobetDegisimDetay> GetDetaylar(int nobetUstGrupId);
    }
}