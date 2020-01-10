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
    public interface IEczaneGrupTanimService
    {
        EczaneGrupTanim GetById(int eczaneGrupTanimId);
        List<EczaneGrupTanim> GetList();
        List<EczaneGrupTanim> GetAktifTanimList(List<int> eczaneGrupTanimIdList);
        List<EczaneGrupTanim> GetAktifTanimList(int eczaneGrupTanimId);

        void Insert(EczaneGrupTanim eczaneGrupTanim);
        void Update(EczaneGrupTanim eczaneGrupTanim);
        void Delete(int eczaneGrupTanimId);
        EczaneGrupTanimDetay GetDetayById(int eczaneGrupTanimId);

        List<EczaneGrupTanimDetay> GetDetaylar();
        List<EczaneGrupTanimDetay> GetDetaylarAktifTanimList(List<int> eczaneGrupTanimIdList);
        List<EczaneGrupTanimDetay> GetDetaylarAktifTanimList(int eczaneGrupTanimId);

        List<EczaneGrupTanimDetay> GetDetaylar(int nobetUstGrupId);
        List<EczaneGrupTanimDetay> GetDetaylar(List<int> nobetUstGrupIdList);
    }
}