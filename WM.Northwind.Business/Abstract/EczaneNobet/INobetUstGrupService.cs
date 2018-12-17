using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.Authorization;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.Business.Abstract.EczaneNobet
{
    public interface INobetUstGrupService
    {
        void Delete(int nobetUstGrupId);
        NobetUstGrup GetById(int nobetUstGrupId);
        List<NobetUstGrup> GetList();
        void Insert(NobetUstGrup nobetUstGrup);
        void Update(NobetUstGrup nobetUstGrup);

        NobetUstGrupDetay GetDetay(int nobetUstGrupId);
        List<NobetUstGrupDetay> GetDetaylar();
        List<NobetUstGrupDetay> GetDetaylar(int nobetUstGrupId);
        List<NobetUstGrup> GetListByUser(User user);

    }
}
