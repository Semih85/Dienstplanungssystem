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
    public interface IEczaneNobetFeragatService
    {
        EczaneNobetFeragat GetById(int eczaneNobetFeragatId);
        List<EczaneNobetFeragat> GetList();
        //List<EczaneNobetFeragat> GetByCategory(int categoryId);
        void Insert(EczaneNobetFeragat eczaneNobetFeragat);
        void Update(EczaneNobetFeragat eczaneNobetFeragat);
        void Delete(int eczaneNobetFeragatId);

        EczaneNobetFeragatDetay GetDetayById(int eczaneNobetFeragatId);
        List<EczaneNobetFeragatDetay> GetDetaylar();
        List<EczaneNobetFeragatDetay> GetDetaylar(int nobetUstGrupId);
    }
}