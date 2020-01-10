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
    public interface INobetUstGrupGunGrupService
    {
        NobetUstGrupGunGrup GetById(int nobetUstGrupGunGrupId);
        List<NobetUstGrupGunGrup> GetList();
        //List<NobetUstGrupGunGrup> GetByCategory(int categoryId);
        void Insert(NobetUstGrupGunGrup nobetUstGrupGunGrup);
        void Update(NobetUstGrupGunGrup nobetUstGrupGunGrup);
        void Delete(int nobetUstGrupGunGrupId);
        NobetUstGrupGunGrupDetay GetDetayById(int nobetUstGrupGunGrupId);
        List<NobetUstGrupGunGrupDetay> GetDetaylar();
        List<NobetUstGrupGunGrupDetay> GetDetaylar(int nobetUstGupId);
        List<NobetUstGrupGunGrupDetay> GetDetaylarByNobetUstGupIdList(List<int> nobetUstGupIdList);
        List<MyDrop> GetMyDrop(List<NobetUstGrupGunGrupDetay> nobetUstGrupGunGrupDetaylar);
    }
}