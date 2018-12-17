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
    public interface INobetGrupGunKuralService
    {
        NobetGrupGunKural GetById(int nobetGrupGunKuralId);
        List<NobetGrupGunKural> GetList();
        List<NobetGrupGunKural> GetList(List<int> nobetGrupIdList);
        List<NobetGrupGunKural> GetAktifList(List<int> nobetGrupIdList);
        void Insert(NobetGrupGunKural nobetGrupGunKural);
        void Update(NobetGrupGunKural nobetGrupGunKural);
        void Delete(int nobetGrupGunKuralId);

        NobetGrupGunKuralDetay GetDetayById(int nobetGrupGunKuralId);
        List<NobetGrupGunKuralDetay> GetDetaylar();
        List<NobetGrupGunKuralDetay> GetDetaylar(int nobetGrupId, int nobetGunKuralId);
        List<NobetGrupGunKuralDetay> GetDetaylar(List<int> nobetGrupIdList);
        List<NobetGrupGunKuralDetay> GetDetaylarAktifList(List<int> nobetGrupIdList);

        void CokluAktifPasifYap(List<NobetGrupGunKuralDetay> nobetGrupGunKuralDetaylar, bool pasifMi);
    }
} 