using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.Business.Abstract.EczaneNobet
{
    public interface ITakvimService
    {
        Takvim GetById(int takvimId);
        Takvim GetByTarih(DateTime tarih);
        List<Takvim> GetList();        
        void Insert(Takvim takvim);
        void Update(Takvim takvim);
        void Delete(int takvimId);

        List<MyDrop> GetAylar();
        List<MyDrop> GetAylar(int yil);
        List<MyDrop> GetAylar(DateTime baslangicTarihi, int yil);
        List<MyDrop> GetAylarDdl();
        List<MyDrop> GetGelecekAy();
        List<MyDrop> GetHaftaninGunleri();

        List<EczaneNobetIstatistik> GetEczaneKumulatifHedefler(int yilBaslangic, int yilBitis, int ayBaslangic, int ayBitis, List<int> nobetGrupIdList, int nobetGorevTipId);

        //List<EczaneNobetTarihAralik> GetEczaneNobetTarihAralik(int yil, int ay, int nobetGrupId, int nobetGorevTipId);
        //List<EczaneNobetTarihAralik> GetEczaneNobetTarihAralik(int yil, int ay, int nobetGorevTipId, List<int> nobetGrupIdList);
        List<EczaneNobetTarihAralik> GetEczaneNobetTarihAralik(DateTime baslangicTarihi, DateTime bitisTarihi, int nobetGorevTipId, List<int> nobetGrupIdList);
        List<EczaneNobetTarihAralik> GetEczaneNobetTarihAralik(DateTime baslangicTarihi, DateTime bitisTarihi, int nobetGorevTipId, List<int> nobetGrupIdList, List<int> nobetGunKuralIdList);
        List<EczaneNobetTarihAralik> GetEczaneNobetTarihAralik(DateTime baslangicTarihi, DateTime bitisTarihi, List<NobetGrupGorevTipDetay> nobetGrupGorevTipler);
        List<EczaneNobetTarihAralikIkili> GetIkiliEczaneNobetTarihAralik(DateTime baslangicTarihi, DateTime bitisTarihi, int nobetGorevTipId, List<int> nobetGrupIdList);

        //List<TakvimNobetGrup> GetTakvimNobetGruplar(int yilBaslangic, int yilBitis, int ayBaslangic, int ayBitis, int nobetGrupId, int nobetGorevTipId);
        //List<TakvimNobetGrup> GetTakvimNobetGruplar(int yil, int ay, int nobetGrupId, int nobetGorevTipId);
        //List<TakvimNobetGrup> GetTakvimNobetGruplar(int yil, int ay, List<int> nobetGrupIdList, int nobetGorevTipId);
        //List<TakvimNobetGrup> GetTakvimNobetGruplar(int yil, int ay, int nobetGorevTipId);
        //List<TakvimNobetGrup> GetTakvimNobetGruplar(int yil, List<int> nobetGrupIdList, int nobetGorevTipId);
        //List<TakvimNobetGrup> GetTakvimNobetGruplarHaftaIci(DateTime baslangicTarihi, int ayFarki, int uzunluk, List<int> nobetGrupIdList, int nobetGorevTipId);
        //List<TakvimNobetGrup> GetTakvimNobetGruplar(DateTime baslangicTarihi, int ayFarki, int uzunluk, List<int> nobetGrupIdList, int nobetGorevTipId, string gunGrup);

        List<TakvimNobetGrup> GetTakvimNobetGruplar(DateTime baslangicTarihi, DateTime bitisTarihi, List<int> nobetGrupIdList, int nobetGorevTipId);
        List<TakvimNobetGrup> GetTakvimNobetGruplar(DateTime baslangicTarihi, DateTime bitisTarihi, List<NobetGrupGorevTipDetay> nobetGrupGorevTipler);
        List<TakvimNobetGrup> GetTakvimNobetGruplar(DateTime baslangicTarihi, DateTime bitisTarihi, int nobetUstGrupId);

        List<EczaneNobetTarihAralik> GetEczaneNobetTarihAralik(DateTime baslangicTarihi, DateTime bitisTarihi, int nobetUstGrupId);
        List<EczaneNobetTarihAralik> GetEczaneNobetTarihAralik(DateTime baslangicTarihi, DateTime bitisTarihi, List<NobetGrupGorevTipDetay> nobetGrupGorevTipler, List<int> nobetGunKuralIdList);


        List<TakvimNobetGrupPeriyot> GetTakvimNobetGruplar(DateTime baslangicTarihi, List<int> ayFarklari, int uzunluk, List<int> nobetGrupIdList, int nobetGorevTipId, string gunGrup);

        //List<TakvimNobetGrupPeriyot> GetTakvimNobetGruplarHaftaIci(DateTime baslangicTarihi, List<int> ayFarklari, int uzunluk, List<int> nobetGrupIdList, int nobetGorevTipId);

        //List<TakvimNobetGrupGunDegerIstatistik> GetTakvimNobetGrupGunDegerIstatistikler(int yil, List<int> nobetGrupIdList, int nobetGorevTipId);
        List<TakvimNobetGrupGunDegerIstatistik> GetTakvimNobetGrupGunDegerIstatistikler(DateTime baslangicTarihi, DateTime bitisTarihi, List<int> nobetGrupIdList, int nobetGorevTipId);
        List<TakvimNobetGrupGunDegerIstatistik> GetTakvimNobetGrupGunDegerIstatistikler(DateTime baslangicTarihi, DateTime bitisTarihi, List<NobetGrupGorevTipDetay> nobetGrupGorevTipler);

        List<TakvimDetay> GetDetaylar();
        List<TakvimDetay> GetDetaylar(int yil);
        List<TakvimDetay> GetDetaylar(int yil, int ay);
        List<TakvimDetay> GetDetaylar(DateTime baslangicTarihi, DateTime BitisTarihi);
        List<TakvimDetay> GetDetaylar(DateTime baslangicTarihi);
        List<TakvimDetay> GetDetaylar(DateTime baslangicTarihi, DateTime bitisTarihi, int haftaninGunu);
        List<TakvimDetay> GetDetaylar(DateTime baslangicTarihi, DateTime bitisTarihi, int[] haftaninGunleri);

        List<TakvimNobetGrupAltGrup> GetTakvimNobetGrupAltGruplar(DateTime baslangicTarihi, DateTime bitisTarihi, List<int> nobetGrupIdList, int nobetGorevTipId);
        List<EczaneNobetAltGrupTarihAralik> GetEczaneNobetAltGrupTarihAralik(DateTime baslangicTarihi, DateTime bitisTarihi, int nobetGorevTipId, List<int> nobetGrupIdList);

        int AyFarkiHesapla(DateTime sonTarih, DateTime ilkTarih);
        List<AnahtarListe> AnahtarListeyiBuGuneTasi(List<int> nobetGrupIdListe, int nobetGorevTipId, DateTime nobetUstGrupBaslangicTarihi, List<EczaneNobetGrupDetay> eczaneNobetGruplar, List<EczaneNobetGrupGunKuralIstatistikYatay> eczaneNobetGrupGunKuralIstatistikYatay, List<EczaneNobetSonucListe2> anahtarListe,
            string gunGrubu);

        void SiraliNobetYaz(List<EczaneNobetSonucListe2> eczaneNobetSonuclar,
            List<NobetGrupGorevTipDetay> nobetGrupGorevTipler,
            List<EczaneNobetGrupDetay> eczaneNobetGruplarTumu,
            DateTime nobetBaslangicTarihi,
            DateTime nobetBitisTarihi);

        //List<AnahtarListe> AnahtarListeyiBuGuneTasi2(List<int> nobetGrupIdListe, 
        //    int nobetGorevTipId, 
        //    DateTime nobetUstGrupBaslangicTarihi, 
        //    List<EczaneNobetGrupDetay> eczaneNobetGruplar, 
        //    List<EczaneNobetGrupGunKuralIstatistikYatay> eczaneNobetGrupGunKuralIstatistikYatay, 
        //    List<EczaneNobetSonucListe2> anahtarListe,
        //    List<NobetUstGrupGunGrupDetay> nobetUstGrupGunGruplar,
        //    DateTime nobetBaslangicTarihi, 
        //    DateTime nobetBitisTarihi);
    }
}
