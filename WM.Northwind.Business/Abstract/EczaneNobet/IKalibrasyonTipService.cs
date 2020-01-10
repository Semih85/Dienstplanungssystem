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
    public interface IKalibrasyonTipService
    {
        KalibrasyonTip GetById(int kalibrasyonTipId);
        List<KalibrasyonTip> GetList();
        //List<KalibrasyonTip> GetByCategory(int categoryId);
        void Insert(KalibrasyonTip kalibrasyonTip);
        void Update(KalibrasyonTip kalibrasyonTip);
        void Delete(int kalibrasyonTipId);
        KalibrasyonTipDetay GetDetayById(int kalibrasyonTipId);
        List<KalibrasyonTipDetay> GetDetaylar();
        List<MyDrop> GetMyDrop(List<KalibrasyonTipDetay> kalibrasyonTipDetaylar);
        List<KalibrasyonTipDetay> GetDetaylar(List<int> nobetUstGrupIdList);
        List<KalibrasyonTipDetay> GetDetaylar(int nobetUstGrupId);
    }
}