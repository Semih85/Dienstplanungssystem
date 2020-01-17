using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Entities.ComplexTypes;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete;
using WM.Northwind.Entities.Concrete.Authorization;
using WM.Northwind.Entities.Concrete.EczaneNobet;
using WM.Northwind.Entities.Concrete.Enums;
using WM.Northwind.Entities.Concrete.Optimization.EczaneNobet;


namespace WM.Northwind.Business.Abstract.EczaneNobet
{
    public interface IEczaneNobetOrtakService
    {
        List<NobetSonucGrupAnaliz> GetSonuclarEczaneGrupAnalizUcGrup(List<EczaneNobetSonucListe> pSonuclar);
        List<NobetSonucGrupAnaliz> GetSonuclarEczaneGrupAnalizIkiGrup(List<EczaneNobetSonucListe> pSonuclar);

        List<EczaneCiftGrupEsli> GetCiftGrupluEczanelerYanYanaUcGrup(List<EczaneNobetSonucListe> pSonuclar, int ayniGuneDenkGelenNobetSayisi);
        List<EczaneCiftGrupEsli> GetCiftGrupluEczanelerYanYanaIkiGrup(List<EczaneNobetSonucListe> pSonuclar, int ayniGuneDenkGelenNobetSayisi);

        List<EczaneCiftGrupEsli> GrupSayisinaGoreAnalizeGonder(List<EczaneNobetSonucListe> pSonuclar, int ayniGuneDenkGelenNobetSayisi);
        List<EczaneCiftGrup> GetCiftGrupluEczaneler(List<EczaneNobetSonucListe> pSonuclar, int ayniGuneDenkGelenNobetSayisi);
        List<EczaneCiftGrup> GetCiftGrupluEczaneler(List<EczaneNobetSonucListe2> pSonuclar, int ayniGuneDenkGelenNobetSayisi);

        List<MyDrop> GetPivotSekiller();
        List<MyDrop> GetPivotSekillerGunFarki();
        
        List<EczaneNobetIstatistikGunFarki> EczaneNobetIstatistikGunFarkiHesapla(List<EczaneNobetSonucListe2> eczaneNobetSonuclar);
        List<EczaneNobetIstatistikGunFarkiFrekans> EczaneNobetIstatistikGunFarkiFrekans(List<EczaneNobetIstatistikGunFarki> eczaneNobetIstatistikGunFarkiSonuclar);

        List<EsGrubaAyniGunYazilanNobetler> GetEsGrubaAyniGunYazilanNobetler(List<EczaneNobetSonucListe2> eczaneNobetSonuclar);

        List<AyniGunNobetTutanEczane> GetAyniGunNobetTutanAltGrupluEczaneler(List<EczaneNobetSonucListe2> ayniGunNobetTutanEczaneler);
        List<AyniGunTutulanNobetDetay> GetAyniGunNobetTutanEczaneler(List<EczaneNobetSonucListe2> ayniGunNobetTutanEczaneler);
        List<AyniGunNobetTutanEczane> GetAyniGunNobetTutanEczanelerGiresun(List<EczaneNobetSonucListe2> ayniGunNobetTutanEczaneler, List<EczaneGrupDetay> eczaneGrupDetaylar);
        List<AyniGunTutulanNobetDetay> AyniGunTutulanNobetSayisiniHesapla(List<AyniGunTutulanNobetDetay> ayniGunNobetTutanEczaneler);
        //List<EczaneNobetSonucListe2> EczaneNobetSonucBirlesim(
        //    List<NobetGrupGorevTipGunKuralDetay> nobetGrupGorevTipGunKurallar,
        //    List<EczaneNobetSonucDetay2> eczaneNobetSonuclar,
        //    List<NobetGrupGorevTipTakvimOzelGunDetay> nobetGrupGorevTipTakvimOzelGunler,
        //    List<EczaneNobetMazeretDetay> mazeretler,
        //    List<EczaneNobetIstekDetay> istekler,
        //    List<EczaneUzaklikMatrisDetay> eczaneMesafeler,
        //    EczaneNobetSonucTuru sonucTuru);

        List<EczaneNobetSonucListe2> EczaneNobetSonucBirlesim(
            List<NobetGrupGorevTipGunKuralDetay> nobetGrupGorevTipGunKurallar,
            List<EczaneNobetSonucDetay2> eczaneNobetSonuclar,
            List<NobetGrupGorevTipTakvimOzelGunDetay> nobetGrupGorevTipTakvimOzelGunler,
            List<EczaneNobetMazeretDetay> mazeretler,
            List<EczaneNobetIstekDetay> istekler,
            EczaneNobetSonucTuru sonucTuru);

        List<EczaneNobetSonucListe2> EczaneNobetSonucBirlesim(
            List<NobetGrupGorevTipGunKuralDetay> nobetGrupGorevTipGunKurallar,
            List<EczaneNobetSonucDetay2> eczaneNobetSonuclar,
            List<NobetGrupGorevTipTakvimOzelGunDetay> nobetGrupGorevTipTakvimOzelGunler,
            EczaneNobetSonucTuru sonucTuru);

        void VirgulleAyrilanNobetGruplariniAyir(int nobetUstGrupId, List<EczaneNobetSonucListe2> sonuclar);

        List<EczaneNobetGrupGunKuralIstatistik> GetEczaneNobetGrupGunKuralIstatistikEczaneBazli(List<EczaneNobetSonucListe2> eczaneNobetSonuc);

        List<EczaneNobetGrupGunKuralIstatistik> GetEczaneNobetGrupGunKuralIstatistik(List<EczaneNobetGrupDetay> eczaneNobetGruplar, List<EczaneNobetSonucListe2> eczaneNobetSonuc);
        List<EczaneNobetGrupGunGrupIstatistik> GetEczaneNobetGrupGunGrupIstatistik(List<EczaneNobetGrupGunKuralIstatistik> eczaneNobetGrupGunKuralIstatistik);
        List<EczaneNobetGrupGunKuralIstatistik> GetEczaneNobetGrupGunKuralIstatistikEczaneBazli(List<EczaneNobetGrupDetay> eczaneNobetGruplar, List<EczaneNobetSonucListe2> eczaneNobetSonuc);

        List<EczaneNobetGrupGunKuralIstatistikYatay> GetEczaneNobetGrupGunKuralIstatistikYatay(List<EczaneNobetGrupGunKuralIstatistik> eczaneNobetGrupGunKuralIstatistik);
        List<EczaneNobetGrupGunKuralIstatistikYatay> GetEczaneBazliGunKuralIstatistikYatayByGorevTip(List<EczaneNobetGrupGunKuralIstatistik> eczaneNobetGrupGunKuralIstatistik);
        List<EczaneNobetGrupGunKuralIstatistikYatay> GetEczaneBazliGunKuralIstatistikYatay(List<EczaneNobetGrupGunKuralIstatistik> eczaneNobetGrupGunKuralIstatistik);

        List<EczaneNobetGrupGunKuralIstatistikYatay> GetEczaneNobetGrupGunKuralIstatistikYatay(List<EczaneNobetGrupGunKuralIstatistik> eczaneNobetGrupGunKuralIstatistik,
            List<EczaneNobetGrupGunKuralIstatistik> eczaneNobetGrupGunKuralIstatistikPlanlanan);

        List<EczaneNobetAlacakVerecek> GetEczaneNobetGrupGunKuralIstatistikYatay(
            List<EczaneNobetGrupGunKuralIstatistikYatay> eczaneNobetGrupGunKuralIstatistik,
            List<EczaneNobetGrupGunKuralIstatistik> eczaneNobetGrupGunKuralIstatistikPlanlanan);

        List<EczaneNobetAlacakVerecek> EczaneNobetAlacakVerecekHesapla(NobetUstGrupDetay nobetUstGrup,
               List<EczaneNobetGrupGunKuralIstatistikYatay> eczaneNobetGrupGunKuralIstatistikYatayTumu,
               List<EczaneNobetGrupGunKuralIstatistikYatay> eczaneNobetGrupGunKuralIstatistikYatayTumuPlanlanan,
               List<NobetUstGrupGunGrupDetay> nobetUstGrupGunGruplar);

        List<EczaneGrupDetay> AltGruplarlaSiraliNobetListesiniOlustur(
            List<EczaneNobetSonucListe2> eczaneNobetSonuclar,
            List<EczaneNobetGrupDetay> eczaneNobetGruplar,
            List<EczaneNobetGrupAltGrupDetay> eczaneNobetGrupAltGruplar,
            NobetUstGrupKisitDetay nobetUstGrupKisitDetay,
            DateTime nobetUstGrupBaslamaTarihi,
            int[] ayniGunNobetTutmasiTakipEdilecekGruplar,
            int[] altGrubuOlanNobetGruplar,
            int indisId);

        List<EczaneGrupDetay> AltGruplarlaSiraliNobetListesiniOlusturMersin(
            List<EczaneNobetSonucListe2> eczaneNobetSonuclar,
            List<EczaneNobetGrupDetay> eczaneNobetGruplar,
            List<EczaneNobetGrupAltGrupDetay> eczaneNobetGrupAltGruplar,
            NobetUstGrupKisitDetay nobetUstGrupKisitDetay,
            DateTime nobetUstGrupBaslamaTarihi,
            int indisId,
            List<int> ayniGunNobetTutmasiTakipEdilecekNobetGrupIdList,
            List<int> altGrubuOlanNobetGrupIdList,
            int nobetAltGrupId,
            int enSonNobetSayisi);

        List<EczaneGrupDetay> AltGruplarlaSiraliNobetListesiniOlusturGiresun(
            List<EczaneNobetSonucListe2> eczaneNobetSonuclar,
            List<EczaneNobetGrupDetay> eczaneNobetGruplar,
            List<EczaneNobetGrupAltGrupDetay> eczaneNobetGrupAltGruplar,
            NobetUstGrupKisitDetay nobetUstGrupKisitDetay,
            DateTime nobetUstGrupBaslamaTarihi,
            int indisId,
            List<int> ayniGunNobetTutmasiTakipEdilecekAltGruplar,
            List<int> altGrubuOlanNobetGruplar);

        List<EczaneGrupDetay> AltGruplarlaSiraliNobetListesiniOlusturManavgat(
            List<EczaneNobetSonucListe2> eczaneNobetSonuclar,
            List<EczaneNobetGrupDetay> eczaneNobetGruplar,
            List<EczaneNobetGrupAltGrupDetay> eczaneNobetGrupAltGruplar,
            NobetUstGrupKisitDetay nobetUstGrupKisitDetay,
            DateTime nobetUstGrupBaslamaTarihi,
            int[] ayniGunNobetTutmasiTakipEdilecekGruplar,
            int[] altGrubuOlanNobetGrupGorevTipler,
            int indisId);

        List<EczaneNobetTarihAralik> AmacFonksiyonuKatsayisiBelirle(List<EczaneNobetTarihAralik> eczaneNobetTarihAralik,
            List<EczaneNobetGrupGunKuralIstatistikYatay> eczaneNobetGrupGunKuralIstatistikYatay,
            List<KalibrasyonYatay> kalibrasyonDetaylar = null,
            List<EczaneNobetAlacakVerecek> eczaneNobetAlacakVerecekler = null
            );

        List<AyniGunTutulanNobetDetay> MesafelerListesiniOlustur(
                List<EczaneNobetMazeretSayilari> eczaneNobetMazeretNobettenDusenler,
                List<EczaneNobetGrupDetay> eczaneNobetGruplarGorevTip1,
                List<EczaneUzaklikMatrisDetay> kritereUygunSayilar);

        void KurallariKontrolEtHaftaIciEnAzEnCok(int nobetUstGrupId, List<EczaneNobetGrupGunKuralIstatistikYatay> eczaneNobetSonuclarYatay);
        void KurallariKontrolEtMazeretIstek(int nobetUstGrupId, List<EczaneNobetMazeretDetay> eczaneNobetMazeretler, List<EczaneNobetIstekDetay> eczaneNobetIstekler);
        void KurallariKontrolEtIstek(int nobetUstGrupId, List<EczaneNobetIstekDetay> eczaneNobetIstekler, List<NobetGrupKuralDetay> nobetGrupKuralDetaylar);
    }
}
