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
        void Insert(EczaneNobetSonucPlanlanan eczaneNobetSonucPlanlanan);
        void Update(EczaneNobetSonucPlanlanan eczaneNobetSonucPlanlanan);
        void Delete(int eczaneNobetSonucPlanlananId);
        EczaneNobetSonucDetay2 GetDetayById(int eczaneNobetSonucPlanlananId);
        List<EczaneNobetSonucDetay2> GetDetaylar();
        List<EczaneNobetSonucDetay2> GetDetaylar(int nobetUstGrupId);
        List<EczaneNobetSonucListe2> GetSonuclar(int nobetGrupGorevTipId, int gunGrupId);
        List<EczaneNobetSonucDetay2> GetDetaylar(DateTime baslangicTarihi, DateTime bitisTarihi, int nobetUstGrupId);
        List<EczaneNobetSonucDetay2> GetDetaylar(int[] nobetGrupGorevTipIdList);
        List<EczaneNobetSonucDetay2> GetDetaylar(DateTime? baslangicTarihi, DateTime? bitisTarihi, int[] nobetGrupGorevTipIdList);
        List<EczaneNobetSonucDetay2> GetDetaylar(DateTime baslangicTarihi, int nobetUstGrupId);
        List<EczaneNobetSonucDetay2> GetDetaylarByNobetGrupGorevTipId(int nobetGrupGorevTipId);
        List<EczaneNobetSonucDetay2> GetDetaylarByNobetGrupGorevTipId(DateTime? baslangicTarihi, DateTime? bitisTarihi, int nobetGrupGorevTipId);
        List<EczaneNobetSonucListe2> SiraliNobetYaz(int nobetUstGrupId);
        List<EczaneNobetSonucListe2> GetSonuclar(int nobetUstGrupId);
        List<EczaneNobetSonucListe2> GetSonuclar(int nobetUstGrupId, int gunGrupId, int alinacakEczaneSayisi);
        List<EczaneNobetSonucListe2> GetSonuclarByEczaneNobetGrupId(int eczaneNobetGrupId, int gunGrupId);
        List<EczaneNobetSonucListe2> GetSonuclar(int[] nobetGrupGorevTipIdList);
        List<EczaneNobetSonucListe2> GetSonuclar(DateTime? baslangicTarihi, DateTime? bitisTarihi, int[] nobetGrupGorevTipIdList);
        List<EczaneNobetSonucListe2> GetSonuclar(DateTime? baslangicTarihi, DateTime? bitisTarihi, int[] nobetGrupGorevTipIdList, bool kapaliEczaneler);
        List<EczaneNobetSonucListe2> GetSonuclar(DateTime? baslangicTarihi, DateTime? bitisTarihi, int nobetGrupGorevTipId, int gunGrupId);
        void CokluSil(int[] ids);
        void CokluEkle(List<EczaneNobetCozum> eczaneNobetCozumler);
    }
}