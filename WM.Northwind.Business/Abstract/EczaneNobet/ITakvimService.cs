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
        TakvimDetay GetDetay(DateTime tarih);
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
        
        List<EczaneNobetTarihAralik> GetEczaneNobetTarihAralik(DateTime baslangicTarihi, DateTime bitisTarihi, int nobetGorevTipId, List<int> nobetGrupIdList);
        List<EczaneNobetTarihAralik> GetEczaneNobetTarihAralik(DateTime baslangicTarihi, DateTime bitisTarihi, int nobetGorevTipId, List<int> nobetGrupIdList, List<int> nobetGunKuralIdList);
        List<EczaneNobetTarihAralik> GetEczaneNobetTarihAralik(DateTime baslangicTarihi, DateTime bitisTarihi, List<NobetGrupGorevTipDetay> nobetGrupGorevTipler);
        List<EczaneNobetTarihAralikIkili> GetIkiliEczaneNobetTarihAralik(DateTime baslangicTarihi, DateTime bitisTarihi, int nobetGorevTipId, List<int> nobetGrupIdList);

        List<TakvimNobetGrup> GetTakvimNobetGruplar(DateTime baslangicTarihi, DateTime bitisTarihi, List<int> nobetGrupIdList, int nobetGorevTipId);
        List<TakvimNobetGrup> GetTakvimNobetGruplar(DateTime baslangicTarihi, DateTime bitisTarihi, List<NobetGrupGorevTipDetay> nobetGrupGorevTipler);
        List<TakvimNobetGrup> GetTakvimNobetGruplar(DateTime baslangicTarihi, DateTime bitisTarihi, int nobetUstGrupId);
        TakvimNobetGrup GetTakvimNobetGruplar(DateTime baslangicTarihi, NobetGrupGorevTipDetay nobetGrupGorevTip, int gunGrupId, int gunSayisi);
        List<EczaneNobetTarihAralik> GetEczaneNobetTarihAralik(DateTime baslangicTarihi, DateTime bitisTarihi, int nobetUstGrupId);
        List<EczaneNobetTarihAralik> GetEczaneNobetTarihAralik(DateTime baslangicTarihi, DateTime bitisTarihi, List<NobetGrupGorevTipDetay> nobetGrupGorevTipler, List<int> nobetGunKuralIdList);
        List<TakvimNobetGrupPeriyot> GetTakvimNobetGruplar(DateTime baslangicTarihi, List<int> ayFarklari, int uzunluk, List<int> nobetGrupIdList, int nobetGorevTipId, string gunGrup);
        List<TakvimNobetGrupGunDegerIstatistik> GetTakvimNobetGrupGunDegerIstatistikler(DateTime baslangicTarihi, DateTime bitisTarihi, List<int> nobetGrupIdList, int nobetGorevTipId);
        List<TakvimNobetGrupGunDegerIstatistik> GetTakvimNobetGrupGunDegerIstatistikler(DateTime baslangicTarihi, DateTime bitisTarihi, List<NobetGrupGorevTipDetay> nobetGrupGorevTipler);
        List<TakvimNobetGrupGunDegerIstatistik> GetTakvimNobetGrupGunDegerIstatistikler(DateTime baslangicTarihi, DateTime bitisTarihi, int nobetGrupGorevTipId);
        List<TakvimNobetGrupGunDegerIstatistik> GetTakvimNobetGrupGunDegerIstatistikler(
              DateTime baslangicTarihi,
              DateTime bitisTarihi,
              int nobetGrupGorevTipId,
              int[] takvimIdList);

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

        List<EczaneNobetGrupGunGrupIstatistik> GetNobetTutamayacaklariGunAraligi(List<EczaneNobetGrupGunGrupIstatistik> eczaneNobetGrupGunGrupIstatistik,
            List<EczaneNobetGrupDetay> eczaneNobetGrupDetaylar,
            List<NobetGrupKuralDetay> nobetGrupKuralDetaylar);

        List<AnahtarListe> AnahtarListeyiBuGuneTasi(List<NobetGrupGorevTipDetay> nobetGrupGorevTipDetaylar,
            List<EczaneNobetGrupDetay> eczaneNobetGruplarTumu,
            List<EczaneNobetGrupGunKuralIstatistikYatay> eczaneNobetGrupGunKuralIstatistikYatayTumu,
            List<EczaneNobetSonucListe2> anahtarListeTumu,
            string gunGrubu);

        List<AnahtarListe> AnahtarListeyiBuGuneTasiAntalya(List<NobetGrupGorevTipDetay> nobetGrupGorevTipler, List<EczaneNobetGrupDetay> eczaneNobetGruplar, List<EczaneNobetGrupGunKuralIstatistikYatay> eczaneNobetGrupGunKuralIstatistikYatay, List<EczaneNobetSonucListe2> anahtarListe,
            string gunGrubu);

        void SiraliNobetYaz(
            List<NobetGrupGorevTipDetay> nobetGrupGorevTipler,
            List<EczaneNobetGrupDetay> eczaneNobetGruplarTumu,
            DateTime nobetBaslangicTarihi,
            DateTime nobetBitisTarihi,
            int nobetUstGrupId);

        void SiraliNobetYazGrupBazinda(NobetGrupGorevTipDetay nobetGrupGorevTip,
            List<EczaneNobetGrupDetay> eczaneNobetGruplarTumu,
            DateTime nobetBaslangicTarihi,
            DateTime nobetBitisTarihi);

        void SiraliNobetYazGrupBazindaOncekiNobetGrubaGore(NobetGrupGorevTipDetay nobetGrupGorevTip,
            List<EczaneNobetGrupDetay> eczaneNobetGruplarTumu,
            DateTime nobetBaslangicTarihi,
            DateTime nobetBitisTarihi);

        void SiraliNobetYazGrupBazindaOncekiNobetGrubaGore(NobetGrupGorevTipDetay nobetGrupGorevTip,
            List<EczaneNobetGrupDetay> eczaneNobetGruplarTumu,
            DateTime nobetBaslangicTarihi,
            DateTime nobetBitisTarihi,
            EczaneNobetGrup eczaneNobetGrup);

        void SiraliNobetYazGunGrupBazinda(
            NobetGrupGorevTipDetay nobetGrupGorevTip,
            List<EczaneNobetGrupDetay> eczaneNobetGruplarTumu,
            DateTime nobetBaslangicTarihi,
            DateTime nobetBitisTarihi,
            int gunGrupId);

        List<EczaneNobetCozum> SiraliNobetYazGrupBazindaEczaneNobetSonuclar(NobetGrupGorevTipDetay nobetGrupGorevTip,
            List<EczaneNobetGrupDetay> eczaneNobetGruplarTumu,
            DateTime nobetBaslangicTarihi,
            DateTime nobetBitisTarihi);

        List<EczaneNobetAlacakVerecek> EczaneNobetAlacakVerecekHesaplaAntalya(
               List<NobetGrupGorevTipDetay> nobetGrupGorevTipler,
               List<EczaneNobetSonucListe2> anahtarListeTumu,
               List<EczaneNobetGrupDetay> eczaneNobetGruplarTumu,
               List<EczaneNobetGrupGunKuralIstatistikYatay> eczaneNobetGrupGunKuralIstatistikYatayTumu);

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
