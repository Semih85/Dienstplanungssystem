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
    public interface IEczaneNobetGrupAltGrupService
    {
        EczaneNobetGrupAltGrup GetById(int eczaneNobetGrupAltGrupId);
        List<EczaneNobetGrupAltGrup> GetList();
        //List<EczaneNobetGrupAltGrup> GetByCategory(int categoryId);
        void Insert(EczaneNobetGrupAltGrup eczaneNobetGrupAltGrup);
        void Update(EczaneNobetGrupAltGrup eczaneNobetGrupAltGrup);
        void Delete(int eczaneNobetGrupAltGrupId);
        void CokluEkle(List<EczaneNobetGrupAltGrup> eczaneNobetGrupAltGruplar);

        EczaneNobetGrupAltGrupDetay GetDetayById(int eczaneNobetGrupAltGrupId);
        List<EczaneNobetGrupAltGrupDetay> GetDetaylar();
        List<EczaneNobetGrupAltGrupDetay> GetDetaylar(int nobetUstGrupId);
        List<EczaneNobetGrupAltGrupDetay> GetDetaylarByNobetGrupId(int nobetGrupId);
        List<EczaneNobetGrupAltGrupDetay> GetDetaylar(List<int> nobetUstGrupIdList);
        List<EczaneNobetGrupAltGrupDetay> GetDetaylar(int nobetUstGrupId, int? nobetAltGrupId);
        List<EczaneNobetGrupAltGrupDetay> GetDetaylarByNobetGrupGorevTipId(int nobetGrupGorevTipId);
        EczaneNobetGrupAltGrupDetay GetDetayByEczaneNobetGrupId(int eczaneNobetGrupId);
        List<EczaneNobetGrupAltGrupDetay> GetDetaylarByNobetGrupGorevTipId(int[] nobetGrupGorevTipIdler);

        List<NobetAltGruptakiEczane> GetNobetAltGruptakiEczaneSayisi(int nobetUstGrupId);
    }
}