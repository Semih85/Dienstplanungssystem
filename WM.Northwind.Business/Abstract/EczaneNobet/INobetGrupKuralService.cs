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
    public interface INobetGrupKuralService
    {
        NobetGrupKural GetById(int nobetGrupKuralId);
        List<NobetGrupKural> GetList();
        //List<NobetGrupKural> GetByCategory(int categoryId);
        void Insert(NobetGrupKural nobetGrupKural);
        void Update(NobetGrupKural nobetGrupKural);
        void Delete(int nobetGrupKuralId);
        void CokluEkle(List<NobetGrupKural> NobetGrupKurallar);
        void CokluDegistir(List<NobetGrupKural> NobetGrupKurallar);

        NobetGrupKuralDetay GetDetayById(int nobetGrupKuralId);
        NobetGrupKuralDetay GetDetay(int nobetGrupGorevTipId, int nobetKuralId);

        List<NobetGrupKuralDetay> GetDetaylar();
        List<NobetGrupKuralDetay> GetDetaylar(List<int> nobetGrupIdList);
        List<NobetGrupKuralDetay> GetDetaylarByNobetUstGrup(List<int> nobetUstGrupIdList);
        List<NobetGrupKuralDetay> GetDetaylarByNobetUstGrup(int nobetUstGrupId);
        List<NobetGrupKuralDetay> GetDetaylarByNobetGrupGorevTipIdList(List<int> nobetGrupGorevTipIdList);
        List<NobetGrupKuralDetay> GetDetaylar(int nobetGrupGorevTipId, int nobetKuralId, List<int> nobetUstGrupIdList);
        List<NobetGrupKuralDetay> GetDetaylar(int nobetGrupGorevTipId, int nobetKuralId, int nobetUstGrupId);
        List<NobetGrupKuralDetay> GetDetaylar(List<int> nobetGrupGorevTipIdList, int nobetKuralId);
        List<NobetGrupKuralDetay> GetDetaylar(int nobetGrupGorevTipId, int nobetKuralId);
    }
} 