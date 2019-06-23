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
    public interface INobetGrupTalepService
    {
        NobetGrupTalep GetById(int nobetGrupTalepId);
        List<NobetGrupTalep> GetList();
        //List<NobetGrupTalep> GetByCategory(int categoryId);
        void Insert(NobetGrupTalep nobetGrupTalep);
        void Update(NobetGrupTalep nobetGrupTalep);
        void Delete(int nobetGrupTalepId);

        NobetGrupTalepDetay GetDetayById(int nobetGrupTalepId);
        List<NobetGrupTalepDetay> GetDetaylar();
        List<NobetGrupTalepDetay> GetDetaylar(List<int> nobetGrupIdList);
        List<NobetGrupTalepDetay> GetDetaylar(List<int> nobetGrupIdList, DateTime baslamaTarihi, DateTime bitisTarihi);
        List<NobetGrupTalepDetay> GetDetaylarByNobetUstGrupIdList(List<int> nobetUstGrupId);
        List<NobetGrupTalepDetay> GetDetaylar(DateTime baslamaTarihi, DateTime bitisTarihi, List<int> nobetGrupGorevTipIdList);
        List<NobetGrupTalepDetay> GetDetaylar(DateTime baslamaTarihi, List<int> nobetGrupGorevTipIdList);
        List<NobetGrupTalepDetay> GetDetaylarByNobetGrupGorevTipId(int nobetGrupGorevTipId);
        List<NobetGrupTalepDetay> GetDetaylar(DateTime baslamaTarihi, DateTime bitisTarihi, int nobetGrupGorevTipId);
        List<NobetGrupTalepDetay> GetDetaylar(DateTime? baslamaTarihi, DateTime? bitisTarihi, int nobetGrupGorevTipId);
        List<NobetGrupTalepDetay> GetDetaylar(int nobetUstGrupId);
        List<NobetGrupTalepDetay> GetDetaylarSonrasi(DateTime baslamaTarihi, int nobetGrupGorevTipId);
        List<NobetGrupTalepDetay> GetDetaylarOncesi(DateTime baslamaTarihi, int nobetGrupGorevTipId);
    }
} 