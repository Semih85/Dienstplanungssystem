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
        List<AyniGunNobetTutanEczane> GetAyniGunNobetTutanEczaneler(List<EczaneNobetSonucListe2> ayniGunNobetTutanEczaneler);
        List<AyniGunNobetTutanEczane> GetAyniGunNobetTutanEczanelerGiresun(List<EczaneNobetSonucListe2> ayniGunNobetTutanEczaneler, List<EczaneGrupDetay> eczaneGrupDetaylar);

        List<EczaneGrupDetay> AltGruplarlaSiraliNobetListesiniOlustur(
            List<EczaneNobetSonucListe2> eczaneNobetSonuclar,
            List<EczaneNobetGrupDetay> eczaneNobetGruplar,
            List<EczaneNobetGrupAltGrupDetay> eczaneNobetGrupAltGruplar,
            NobetUstGrupKisitDetay nobetUstGrupKisitDetay,
            DateTime nobetUstGrupBaslamaTarihi,
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

        List<EczaneNobetTarihAralik> AmacFonksiyonuKatsayisiBelirle(List<EczaneNobetTarihAralik> eczaneNobetTarihAralik,
            List<EczaneNobetGrupGunKuralIstatistikYatay> eczaneNobetGrupGunKuralIstatistikYatay,
            List<KalibrasyonYatay> kalibrasyonDetaylar = null);

        void KurallariKontrolEt(int nobetUstGrupId, List<EczaneNobetGrupGunKuralIstatistikYatay> eczaneNobetSonuclarYatay);
    }
}
