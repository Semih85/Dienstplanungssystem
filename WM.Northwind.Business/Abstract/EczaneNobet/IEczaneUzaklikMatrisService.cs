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
    public interface IEczaneUzaklikMatrisService
    {
        EczaneUzaklikMatris GetById(int eczaneUzaklikMatrisId);
        List<EczaneUzaklikMatris> GetList();
        //List<EczaneUzaklikMatris> GetByCategory(int categoryId);
        void Insert(EczaneUzaklikMatris eczaneUzaklikMatris);
        void Update(EczaneUzaklikMatris eczaneUzaklikMatris);
        void Delete(int eczaneUzaklikMatrisId);
        EczaneUzaklikMatrisDetay GetDetayById(int eczaneUzaklikMatrisId);
        EczaneUzaklikMatrisDetay GetDetay(int eczaneIdFrom, int eczaneIdTo);
        EczaneUzaklikMatrisDetay GetDetay(int eczaneIdFrom, int eczaneIdTo, List<EczaneUzaklikMatrisDetay> eczaneUzaklikMatrisDetaylar);
        List <EczaneUzaklikMatrisDetay> GetDetaylar();
        List<EczaneUzaklikMatrisDetay> GetDetaylar(int nobetUstGrupId);
        List<EczaneUzaklikMatrisDetay> GetDetaylar(int nobetUstGrupId, int mesafeKriteri);
        List<EczaneUzaklikMatrisDetay> GetDetaylarByEczaneId(int eczaneId);
        List<EczaneUzaklikMatrisDetay> GetDetaylarByEczaneId(int eczaneId, int mesafe);
        List<EczaneGrupDetay> GetMesafeKriterineGoreKontrolEdilecekEczaneGruplar(int mesafeKriter, List<EczaneUzaklikMatrisDetay> eczaneMesafeler);

        void CokluEkle(List<EczaneUzaklikMatrisDetay> eczaneUzaklikMatrisDetaylar);
    }
}