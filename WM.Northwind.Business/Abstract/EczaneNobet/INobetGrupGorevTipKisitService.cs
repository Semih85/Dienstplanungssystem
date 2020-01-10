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
    public interface INobetGrupGorevTipKisitService
    {
        NobetGrupGorevTipKisit GetById(int nobetGrupGorevTipKisitId);
        List<NobetGrupGorevTipKisit> GetList();
        //List<NobetGrupGorevTipKisit> GetByCategory(int categoryId);
        void Insert(NobetGrupGorevTipKisit nobetGrupGorevTipKisit);
        void Update(NobetGrupGorevTipKisit nobetGrupGorevTipKisit);
        void Delete(int nobetGrupGorevTipKisitId);
        NobetGrupGorevTipKisitDetay GetDetayById(int nobetGrupGorevTipKisitId);
        List<NobetGrupGorevTipKisitDetay> GetDetaylar();
        List<NobetGrupGorevTipKisitDetay> GetDetaylar(int nobetUstGrupId);
        List<NobetGrupGorevTipKisitDetay> GetDetaylar(int kisitId, List<int> nobetGrupGorevTipIdList);

        NobetGrupGorevTipKisitDetay GetDetay(int kisitId, int nobetGrupGorevTipId);
        List<NobetGrupGorevTipKisitDetay> GetDetaylarByNobetGrupGorevTipId(int nobetGrupGorevTipId);
        List<NobetGrupGorevTipKisitDetay> GetDetaylarByKisitId(int kisitId);
    }
}