using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.Business.Abstract.EczaneNobet
{
    public interface IEczaneNobetIstekService
    {
        EczaneNobetIstek GetById(int eczaneNobetIstekId);
        EczaneNobetIstekDetay GetDetayById(int eczaneNobetIstekId);

        List<EczaneNobetIstek> GetList();
        List<EczaneNobetIstek> GetByCategory(int ustGrupId);
        void Insert(EczaneNobetIstek eczaneNobetIstek);
        void Update(EczaneNobetIstek eczaneNobetIstek);
        void Delete(int eczaneNobetIstekId);

        //complex types
        List<EczaneNobetIstekDetay> GetDetaylar(int yil, int ay, int nobetGrupId);
        List<EczaneNobetIstekDetay> GetDetaylar(int yil, int ay, List<int> ecznaneIdList);
        List<EczaneNobetIstekDetay> GetDetaylarByNobetGrupIdList(int yil, int ay, List<int> nobetGrupIdList);
        List<EczaneNobetIstekDetay> GetDetaylarByNobetGrupIdList(DateTime baslangicTarihi, DateTime bitisTarihi, List<int> nobetGrupIdList);
        List<EczaneNobetIstekDetay> GetDetaylarByNobetUstGrupId(DateTime baslangicTarihi, DateTime bitisTarihi, List<int> nobetUstGrupIdList);
        List<EczaneNobetIstekDetay> GetDetaylarByNobetUstGrupId(DateTime baslangicTarihi, DateTime bitisTarihi, int nobetUstGrupId);

        List<EczaneNobetIstekDetay> GetDetaylar(DateTime? baslangicTarihi, DateTime? bitisTarihi, int[] nobetGrupGorevTipIdList);
        List<EczaneNobetIstekDetay> GetDetaylar(DateTime? baslangicTarihi, DateTime? bitisTarihi, int[] nobetGrupGorevTipIdList, int nobetUstGrupId);
        List<EczaneNobetIstekDetay> GetDetaylar(DateTime? baslangicTarihi, DateTime? bitisTarihi, int nobetGrupGorevTipId);
        List<EczaneNobetIstekDetay> GetDetaylar(int yil, int ay);
        List<EczaneNobetIstekDetay> GetDetaylar(int yil, int ayBaslangic, int ayBitis, int nobetGrupId);
        List<EczaneNobetIstekDetay> GetDetaylar(int nobetUstGrupId);
        List<EczaneNobetIstekDetay> GetDetaylarByTakvimId(int takvimId, int nobetUstGrupId);
        List<EczaneGrupDetay> GetDetaylar(List<EczaneNobetIstekDetay> eczaneNobetIstekDetaylar, List<EczaneGrupDetay> eczaneGrupDetayalar);
        List<EczaneGrupDetay> SonrakiAylardaAyniGunIstekGirilenEczaneler(List<EczaneNobetIstekDetay> eczaneNobetIstekDetaylar);

        List<EczaneNobetIstekDetay> GetDetaylar();
        void CokluEkle(List<EczaneNobetIstek> eczaneNobetIstekler);
    }
}
