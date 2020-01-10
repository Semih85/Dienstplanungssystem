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
    public interface INobetUstGrupKisitService
    {
        NobetUstGrupKisit GetById(int nobetUstGrupKisitId);
        List<NobetUstGrupKisit> GetList();
        
        void Insert(NobetUstGrupKisit nobetUstGrupKisit);
        void Update(NobetUstGrupKisit nobetUstGrupKisit);
        void Delete(int nobetUstGrupKisitId);
        void CokluEkle(List<NobetUstGrupKisit> nobetUstGrupKisitlar);

        NobetUstGrupKisitDetay GetDetayById(int nobetUstGrupKisitId);
        NobetUstGrupKisitDetay GetDetay(string kisitAdi, int nobetUstGrupId);

        List<NobetUstGrupKisitDetay> GetDetaylar();
        List<NobetUstGrupKisitDetay> GetDetaylar(int nobetUstGrupId);
        List<NobetUstGrupKisitDetay> GetVarsayilandanFarkliOlanlar(int nobetUstGrupId);
        bool GetVarsayilandanFarkliMi(int nobetUstGrupKisitId);
        bool GetKisitPasifMi(string kisitAdi, int nobetUstGrupId);
        List<NobetUstGrupKisitDetay> GetAktifKisitlar(int nobetUstGrupId);
        int GetDegisenKisitlar(int nobetUstGrupId);
        NobetUstGrupKisitDetay GetDetay(int kisitId, int nobetUstGrupId);
    }
}