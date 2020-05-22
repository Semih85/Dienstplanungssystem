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
    public interface IEczaneNobetDegisimArzService
    {
        EczaneNobetDegisimArz GetById(int eczaneNobetDegisimArzId);
        List<EczaneNobetDegisimArz> GetList();
        //List<EczaneNobetDegisimArz> GetByCategory(int categoryId);
        void Insert(EczaneNobetDegisimArz eczaneNobetDegisimArz);
        void Update(EczaneNobetDegisimArz eczaneNobetDegisimArz);
        void Delete(int eczaneNobetDegisimArzId);
        EczaneNobetDegisimArzDetay GetDetayById(int eczaneNobetDegisimArzId);
        List <EczaneNobetDegisimArzDetay> GetDetaylar();
        List<EczaneNobetDegisimArzDetay> GetDetaylar(int nobetUstGrupId);
        List<EczaneNobetDegisimArzDetay> GetDetaylarByEczaneSonucId(int eczaneNobetSonucId);

    }
} 