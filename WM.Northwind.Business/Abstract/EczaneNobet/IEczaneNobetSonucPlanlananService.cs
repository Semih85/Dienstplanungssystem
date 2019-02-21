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
    public interface IEczaneNobetSonucPlanlananService
    {
        EczaneNobetSonucPlanlanan GetById(int eczaneNobetSonucPlanlananId);
        List<EczaneNobetSonucPlanlanan> GetList();
        //List<EczaneNobetSonucPlanlanan> GetByCategory(int categoryId);
        void Insert(EczaneNobetSonucPlanlanan eczaneNobetSonucPlanlanan);
        void Update(EczaneNobetSonucPlanlanan eczaneNobetSonucPlanlanan);
        void Delete(int eczaneNobetSonucPlanlananId);
        EczaneNobetSonucDetay2 GetDetayById(int eczaneNobetSonucPlanlananId);
        List<EczaneNobetSonucDetay2> GetDetaylar();
        List<EczaneNobetSonucDetay2> GetDetaylar(int nobetUstGrupId);
        //List<EczaneNobetSonucDetay2> GetDetaylar(int nobetUstGrupId, int gunGrupId, int alinacakEczaneSayisi);
        List<EczaneNobetSonucListe2> GetSonuclar(int nobetGrupGorevTipId, int gunGrupId);
        List<EczaneNobetSonucDetay2> GetDetaylar(DateTime baslangicTarihi, DateTime bitisTarihi, int nobetUstGrupId);
        List<EczaneNobetSonucDetay2> GetDetaylar(DateTime baslangicTarihi, int nobetUstGrupId);
        List<EczaneNobetSonucListe2> SiraliNobetYaz(int nobetUstGrupId);

        List<EczaneNobetSonucListe2> GetSonuclar(int nobetUstGrupId);
        List<EczaneNobetSonucListe2> GetSonuclar(int nobetUstGrupId, int gunGrupId, int alinacakEczaneSayisi);
        void CokluSil(int[] ids);
        void CokluEkle(List<EczaneNobetCozum> eczaneNobetCozumler);
    }
}