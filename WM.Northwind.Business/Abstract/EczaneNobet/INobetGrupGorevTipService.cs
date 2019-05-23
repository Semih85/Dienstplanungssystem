using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.Authorization;
using WM.Northwind.Entities.Concrete.EczaneNobet;
using WM.Northwind.Entities.Concrete.Optimization.EczaneNobet;

namespace WM.Northwind.Business.Abstract.EczaneNobet
{
    public interface INobetGrupGorevTipService
    {
        NobetGrupGorevTip GetById(int nobetGrupGorevTipId);
        List<NobetGrupGorevTip> GetList();
        List<NobetGrupGorevTip> GetList(int nobetGorevTipId);
        List<NobetGrupGorevTip> GetList(int nobetGorevTipId, int nobetGrupId);
        List<NobetGrupGorevTip> GetList(int nobetGorevTipId, List<int> nobetGrupIdList);
        List<NobetGrupGorevTip> GetListByUser(User user);

        void Insert(NobetGrupGorevTip nobetGrupGorevTip);
        void Update(NobetGrupGorevTip nobetGrupGorevTip);
        void Delete(int nobetGrupGorevTipId);

        NobetGrupGorevTipDetay GetDetayById(int nobetGrupGorevTipId);
        List<NobetGrupGorevTipDetay> GetDetaylar();
        List<NobetGrupGorevTipDetay> GetDetaylarByNobetGorevTipId(int nobetGorevTipId);
        List<NobetGrupGorevTipDetay> GetDetaylar(int nobetUstGrupId);
        List<NobetGrupGorevTipDetay> GetDetaylar(List<int> nobetGrupIdList);
        List<NobetGrupGorevTipDetay> GetDetaylar(int nobetGorevTipId, List<int> nobetGrupIdList);
        List<NobetGrupGorevTipDetay> GetDetaylarByIdList(List<int> idList);
        List<NobetGrupGorevTipDetay> GetDetaylarByNobetUstGrupIdList(List<int> nobetUstGrupIdList);
        List<NobetGrupGorevTipDetay> GetDetaylarByNobetGrupGorevTipId(int nobetGrupGorevTipId);
        List<MyDrop> GetMyDrop(int nobetUstGrupId);
    }
} 