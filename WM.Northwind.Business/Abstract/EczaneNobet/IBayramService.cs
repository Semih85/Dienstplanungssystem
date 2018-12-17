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
    public interface IBayramService
    {
        Bayram GetById(int takvimId);
        List<Bayram> GetList();
        //List<Bayram> GetByCategory(int categoryId);
        void Insert(Bayram bayram);
        void Update(Bayram bayram);
        void Delete(int id);
        void CokluEkle(List<Bayram> bayramlar);
        void CokluSil(int[] ids);

        BayramDetay GetDetayById(int id);
        List<BayramDetay> GetDetaylar();
        //List<BayramDetay> GetDetaylar(int yil, int ay, int nobetGorevTipId);
        List<BayramDetay> GetDetaylar(int takvimId, int nobetGrupId, int nobetGorevTipId);
        List<BayramDetay> GetDetaylar(int yil, int ay, int nobetGrupId, int nobetGorevTipId);
        List<BayramDetay> GetDetaylar(int yil, int ay, List<int> nobetGrupIdList, int nobetGorevTipId);
        List<BayramDetay> GetDetaylar(int yil, List<int> nobetGrupIdList, int nobetGorevTipId);
        List<BayramDetay> GetDetaylar(DateTime baslangicTarihi, DateTime bitisTarihi, int nobetGrupId, int nobetGorevTipId);
        List<BayramDetay> GetDetaylar(DateTime baslangicTarihi, DateTime bitisTarihi, List<int> nobetGrupIdList, int nobetGorevTipId);
        List<BayramDetay> GetDetaylar(DateTime baslangicTarihi, DateTime bitisTarihi, int nobetUstGrupId);
        List<BayramDetay> GetDetaylar(DateTime baslangicTarihi, List<int> nobetGrupIdList, int nobetGorevTipId);
        List<BayramDetay> GetDetaylar(List<int> nobetGrupGorevtipIdList);
        List<BayramDetay> GetDetaylar(int nobetUstGrupId);
    }
} 