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
    public interface IEczaneNobetMazeretService
    {
        EczaneNobetMazeretDetay GetDetayById(int eczaneNobetMazeretId);
        EczaneNobetMazeret GetById(int eczaneNobetMazeretId);
        List<EczaneNobetMazeret> GetList();

        void Insert(EczaneNobetMazeret eczaneNobetMazeret);
        void Update(EczaneNobetMazeret eczaneNobetMazeret);
        void Delete(int eczaneNobetMazeretId);
        void CokluEkle(List<EczaneNobetMazeret> EczaneNobetMazeretler);

        //complex types
        List<EczaneNobetMazeretDetay> GetDetaylar();
        List<EczaneNobetMazeretDetay> GetListByUser(User user);
        List<EczaneNobetMazeretDetay> GetDetaylar(int yil, int ay);
        List<EczaneNobetMazeretDetay> GetDetaylar(int yil, int ay, int nobetGrupId);
        List<EczaneNobetMazeretDetay> GetDetaylar(int yil, int ay, List<int> ecznaneIdList);
        List<EczaneNobetMazeretDetay> GetDetaylar(int nobetUstGrupId);
        List<EczaneNobetMazeretDetay> GetDetaylar(DateTime baslangicTarihi, DateTime bitisTarihi, int nobetUstGrupId);
        List<EczaneNobetMazeretDetay> GetDetaylar(DateTime baslangicTarihi, DateTime bitisTarihi, List<int> nobetGrupIdList);
        List<EczaneNobetMazeretDetay> GetDetaylar(DateTime? baslangicTarihi, DateTime? bitisTarihi, int[] nobetGrupGorevTipIdList);
        List<EczaneNobetMazeretDetay> GetDetaylar(DateTime? baslangicTarihi, DateTime? bitisTarihi, int[] nobetGrupGorevTipIdList, int nobetUstGrupId);
        List<EczaneNobetMazeretDetay> GetDetaylarNobetGrupId(DateTime baslangicTarihi, DateTime bitisTarihi, int nobetGrupId);

        List<EczaneNobetMazeretDetay> GetDetaylarByEczaneIdList(DateTime baslangicTarihi, DateTime bitisTarihi, List<int> ecznaneIdList);

        List<EczaneNobetMazeretSayilari> GetEczaneNobetMazeretSayilari(DateTime baslangicTarihi, DateTime bitisTarihi, List<int> nobetGrupIdList);
        List<EczaneNobetMazeretSayilari> GetEczaneNobetMazeretSayilari(DateTime baslangicTarihi, DateTime bitisTarihi, int nobetGrupId);

        List<EczaneNobetMazeretDetay> GetDetaylarByNobetGrupGorevTipId(DateTime? baslangicTarihi, DateTime? bitisTarihi, int nobetGrupGorevTipId);
    }
}
