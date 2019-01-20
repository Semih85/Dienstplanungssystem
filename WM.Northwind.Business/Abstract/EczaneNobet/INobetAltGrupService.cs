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
    public interface INobetAltGrupService
    {
        NobetAltGrup GetById(int nobetAltGrupId);
        List<NobetAltGrup> GetList();
        //List<NobetAltGrup> GetByCategory(int categoryId);
        void Insert(NobetAltGrup nobetAltGrup);
        void Update(NobetAltGrup nobetAltGrup);
        void Delete(int nobetAltGrupId);
        NobetAltGrupDetay GetDetayById(int nobetAltGrupId);
        List<NobetAltGrupDetay> GetDetaylar();
        List<NobetAltGrupDetay> GetDetaylar(int nobetUstGrupId);
        List<NobetGruptakiAltGrup> GetNobetGruptakiAltGrupSayisi(int nobetUstGrupId);
        List<NobetAltGrupDetay> GetDetaylarByNobetGrupId(int nobetGrupId);
        List<NobetAltGrupDetay> GetDetaylar(List<int> nobetGrupGorevTipIdList);
        List<NobetAltGrupDetay> GetDetaylarByNobetUstGrup(List<int> nobetUstGrupIdList);
    }
}