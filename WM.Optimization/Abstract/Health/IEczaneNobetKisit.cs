using OPTANO.Modeling.Optimization;
using System;
using System.Collections.Generic;
using WM.Core.Optimization;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Optimization.Entities.KisitParametre;

namespace WM.Optimization.Abstract.Health
{
    public interface IEczaneNobetKisit : IOptimization
    //where T : class, IDataModel, new() 
    {
        void TalebiKarsila(KpTalebiKarsila talebiKarsilaKisitParametreModel);
        void TalebiKarsila(KpTalebiKarsila kpTalebiKarsila, TakvimNobetGrup tarih);
        void HerGunAyniAltGruptanEnFazla1NobetciOlsun(
            List<TakvimNobetGrup> tarihler,
            List<EczaneNobetTarihAralik> eczaneNobetTarihAralikGrupBazli,
            List<NobetAltGrupDetay> altGruplar,
            KpTarihVeAltGrupBazliEnFazla tarihVeAltGrupBazliEnFazla);

        void TarihAraligiOrtalamaEnFazla(KpTarihAraligiOrtalamaEnFazla tarihAraligiOrtalamaEnFazlaKisitParametreModel);
        void TarihVeAltGrupBazliEnFazla(KpTarihVeAltGrupBazliEnFazla kpTarihVeAltGrupBazliEnFazla);
        void PesPeseGorevEnAz(KpPesPeseGorevEnAz kisitParametreModel);
        void EsGruptakiEczanelereAyniGunNobetYazma(KpEsGrubaAyniGunNobetYazma esGrubaAyniGunNobetYazmaKisitParametreModel);
        void KumulatifToplamEnFazla(KpKumulatifToplam kumulatifToplamEnFazlaKisitParametreModel);
        void HerAyPespeseGorev(KpHerAyPespeseGorev herAyPespeseGorevKisitParametreModel);
        void MazereteGorevYazma(KpMazereteGorevYazma mazereteGorevYazmaKisitParametreModel);
        void IstegiKarsila(KpIstegiKarsila istegiKarsilaKisitParametreModel);
        void PespeseFarkliTurNobetYaz(KpPespeseFarkliTurNobet bayramPespeseFarkliTurKisitParametreModel);

        void HerAyHaftaIciPespeseGorev(KpHerAyHaftaIciPespeseGorev herAyHaftaIciPespeseGorevKisitParametreModel);
        void TarihAraligindaEnAz1NobetYaz(KpTarihAraligindaEnAz1NobetYaz tarihAraligindaEnAz1NobetYazKisitParametreModel);
        void BirEczaneyeAyniGunSadece1GorevYaz(KpAyniGunSadece1NobetTuru ayniGunSadece1NobetTuruKisitParametreModel);
        void NobetGorevTipineGoreDagilimYap(KpGorevTipineGorevDagilim kisitParametreModel);
        void IstenenEczanelerinNobetGunleriniKisitla(KpIstenenEczanelerinNobetGunleriniKisitla kpIstenenEczanelerinNobetGunleriniKisitla);
        void AyIcindeSadece1KezAyniGunNobetTutulsun(KpAyIcindeSadece1KezAyniGunNobet ayIcindeSadece1KezAyniGunNobetKisitParametreModel);
        void AyIcindeSadece1KezAyniGunNobetTutulsunEczaneBazli(KpAyIcindeSadece1KezAyniGunNobet ayIcindeSadece1KezAyniGunNobetKisitParametreModel);
        void AyIcindeSadece1KezAyniGunNobetTutulsunGiresunAltGrup(KpAyIcindeSadece1KezAyniGunNobetGiresunAltGrup p);

        void AyIcindeSadece1KezAyniGunNobetTutulsun(KpAyIcindeSadece1KezAyniGunNobetDegiskenDonusumlu kpAyIcindeSadece1KezAyniGunNobetDegiskenDonusumlu);

        void AltGruplarlaAyniGunNobetGrupAltGrup(KpAltGruplarlaAyniGunNobetGrupAltGrup p);

        void AltGruplarlaSiraliNobetTutulsun(Model model,
            List<EczaneNobetTarihAralik> eczaneNobetTarihAralik,
            List<EczaneNobetSonucListe2> eczaneNobetSonuclar,
            List<EczaneNobetGrupDetay> eczaneNobetGruplar,
            List<EczaneNobetGrupAltGrupDetay> eczaneNobetGrupAltGruplar,
            NobetUstGrupKisitDetay nobetUstGrupKisitDetay,
            NobetGrupGorevTipDetay nobetGrupGorevTip,
            DateTime nobetUstGrupBaslamaTarihi,
            List<TakvimNobetGrup> bayramlar,
            List<TakvimNobetGrup> pazarGunleri,
            List<TakvimNobetGrup> haftaIciGunleri,
            VariableCollection<EczaneNobetTarihAralik> _x);
        NobetUstGrupKisitDetay NobetUstGrupKisit(List<NobetUstGrupKisitDetay> nobetUstGrupKisitlar, string kisitAdi, int nobetUstGrupId);

        void TalepleriTakvimeIsle(List<NobetGrupTalepDetay> nobetGrupTalepler, int varsayilanGunlukNobetciSayisi, List<TakvimNobetGrup> tarihler);
        double OrtalamaNobetSayisi(int gunlukNobetciSayisi, int gruptakiNobetciSayisi, int gunSayisi);
        double OrtalamaNobetSayisi(int talepEdilenToplamNobetciSayisi, int gruptakiNobetciSayisi);

        int GetToplamGunKuralNobetSayisi(EczaneNobetGrupGunKuralIstatistikYatay eczaneNobetIstatistik, int nobetGunKuralId);
        List<NobetGunKuralNobetSayisi> GetNobetGunKuralNobetSayilari(List<TakvimNobetGrupGunDegerIstatistik> nobetGunKuralIstatistikler,
            EczaneNobetGrupGunKuralIstatistikYatay eczaneNobetIstatistik);

        List<EczaneGrupDetay> GetEczaneGruplarByEczaneGrupTanimTipId(List<EczaneGrupDetay> eczaneGruplar, int eczaneGrupTanimTipId);
        List<EczaneNobetSonucListe2> GetSonuclarByGunGrup(List<EczaneNobetSonucListe2> sonuclar, string gunGrup);
        string CeliskileriEkle(Solution solution);
        string CeliskileriTabloyaAktar(DateTime baslangicTarihi, DateTime bitisTarihi, int calismaSayisi, string iterasyonMesaj, List<NobetGrupGorevTipDetay> nobetGrupGorevTipler, string[] celiskiler);
        string CozulenGruplariYazdir(List<NobetGrupGorevTipDetay> nobetGrupGorevTipDetaylar);
        double GetNobetGunKural(List<NobetGrupKuralDetay> nobetGrupKurallar, int nobetKuralId);
        NobetUstGrupKisitDetay GetNobetGunKuralIlgiliKisitTarihAraligi(List<NobetUstGrupKisitDetay> kisitlarAktif, int nobetGunKuralId);
        List<NobetUstGrupKisitDetay> GetKisitlarNobetGrupBazli(List<NobetUstGrupKisitDetay> kisitlarUstGrupBazli, List<NobetGrupGorevTipKisitDetay> kisitlarGrupBazli);
        void NobetGrupBuyuklugunuTakvimeEkle(List<TakvimNobetGrup> tarihler, int eczaneSayisi);
        double GetArdisikBosGunSayisi(int pespeseNobetSayisi, double altLimit);
        List<AyniGunTutulanNobetDetay> GetAyniGunNobetTutanEczaneler(List<EczaneNobetTarihAralik> sonuclar);

        KalibrasyonYatay GetKalibrasyonDegeri(List<KalibrasyonYatay> eczaneKalibrasyon);

        int GetKumulatifToplamNobetSayisi(List<EczaneNobetGrupGunKuralIstatistikYatay> eczaneNobetGrupGunKuralIstatistikler, int nobetGunKuralId);

        List<NobetGunKuralTarihAralik> OrtalamaNobetSayilariniHesapla(List<TakvimNobetGrup> tarihler,
            int gruptakiEczaneSayisi,
            List<EczaneNobetGrupGunKuralIstatistikYatay> eczaneNobetGrupGunKuralIstatistikler,
            List<TakvimNobetGrupGunDegerIstatistik> nobetGunKuralIstatistikler);

        bool KumulatifEnfazlaHafIciDagilimiArasindaFarkVarmi(
            NobetUstGrupKisitDetay herAyEnFazlaIlgiliKisit,
            NobetUstGrupKisitDetay kumulatifEnfazlaHaftaIciDagilimi,
            TakvimNobetGrupGunDegerIstatistik nobetGunKural,
            int gunKuralNobetSayisi,
            int haftaIciEnCokVeGunKuralNobetleriArasindakiFark,
            int haftaIciEnAzVeEnCokNobetSayisiArasindakiFark);
    }
}

// gereksiz kısıtlar

//void HaftaIciGunleri(Model model,
//List<TakvimNobetGrup> tarihler,
//EczaneNobetGrupDetay eczaneNobetGrup,
//List<EczaneNobetTarihAralik> eczaneNobetTarihAralikEczaneBazli,
//double yazilabilecekHaftaIciNobetSayisi,
//int toplamNobetSayisi,
//NobetUstGrupKisitDetay haftaninGunleriDagilimi,
//VariableCollection<EczaneNobetTarihAralik> _x);

//void PazarPespeseGorevEnAz(Model model,
//    List<EczaneNobetSonucListe2> eczaneGrupNobetSonuclar,
//    List<EczaneNobetTarihAralik> eczaneNobetTarihAralik,
//    NobetUstGrupKisitDetay nobetUstGrupKisitDetay,
//    List<TakvimNobetGrup> pazarGunleri,
//    int gruptakiNobetciSayisi,
//    EczaneNobetGrupDetay eczaneNobetGrup,
//    VariableCollection<EczaneNobetTarihAralik> _x);

//void HaftaIciPespeseGorevEnAz(Model model,
//    List<EczaneNobetSonucListe2> eczaneGrupNobetSonuclar,
//    List<EczaneNobetTarihAralik> eczaneNobetTarihAralik,
//    NobetUstGrupKisitDetay nobetUstGrupKisitDetay,
//    List<TakvimNobetGrup> haftaIciGunleri,
//    EczaneNobetGrupDetay eczaneNobetGrup,
//    double altLimit,
//    VariableCollection<EczaneNobetTarihAralik> _x);

//void HerAyEnFazlaGorev(Model model,
//    double ortalamaNobetSayisi,
//    EczaneNobetGrupDetay eczaneNobetGrup,
//    List<EczaneNobetTarihAralik> eczaneNobetTarihAralik,
//    NobetUstGrupKisitDetay nobetUstGrupKisitDetaylar,
//    VariableCollection<EczaneNobetTarihAralik> _x);

//void HerAyEnFazlaHaftaIci(Model model,
//    List<TakvimNobetGrup> haftaIciGunleri,
//    double haftaIciOrtamalaNobetSayisi,
//    EczaneNobetGrupDetay eczaneNobetGrup,
//    List<EczaneNobetTarihAralik> eczaneNobetTarihAralik,
//    NobetUstGrupKisitDetay nobetUstGrupKisitDetay,
//    VariableCollection<EczaneNobetTarihAralik> _x);

//void HerAyEnFazla1Gunler(Model model,
//    EczaneNobetGrupDetay eczaneNobetGrup,
//    List<EczaneNobetTarihAralik> eczaneNobetTarihAralik,
//    int gunDegerId,
//    VariableCollection<EczaneNobetTarihAralik> _x);

//void BayramToplamEnFazla(Model model,
//    List<TakvimNobetGrup> bayramlar,
//    EczaneNobetGrupDetay eczaneNobetGrup,
//    List<EczaneNobetTarihAralik> eczaneNobetTarihAralik,
//    int toplamBayramNobetSayisi,
//    double yillikOrtalamaGunKuralSayisi,
//    VariableCollection<EczaneNobetTarihAralik> _x);

//void HerAyEnFazla1Bayram(Model model,
//    List<TakvimNobetGrup> bayramlar,
//    EczaneNobetGrupDetay eczaneNobetGrup,
//    List<EczaneNobetTarihAralik> eczaneNobetTarihAralik,
//    VariableCollection<EczaneNobetTarihAralik> _x,
//    bool kisitAktifMi);

//void EczaneGrup(
//    Model model,
//    List<EczaneNobetTarihAralik> eczaneNobetTarihAralikTumu,
//    List<EczaneNobetSonucListe2> eczaneNobetSonuclarTumu,
//    NobetUstGrupKisitDetay nobetUstGrupKisitDetay,
//    List<EczaneGrupTanimDetay> eczaneGrupTanimlar,
//    List<EczaneGrupDetay> eczaneGruplarTumu,
//    NobetGrupGorevTipDetay nobetGrupGorevTip,
//    List<TakvimNobetGrup> tarihler,
//    VariableCollection<EczaneNobetTarihAralik> _x);

//void EczaneGrupCokluCozum(
//    Model model,
//    List<EczaneNobetTarihAralik> eczaneNobetTarihAralikTumu,
//    NobetUstGrupKisitDetay nobetUstGrupKisitDetay,
//    List<EczaneGrupTanimDetay> eczaneGrupTanimlar,
//    List<EczaneGrupDetay> eczaneGruplarTumu,
//    List<NobetGrupGorevTipDetay> nobetGrupGorevTipler,
//    List<TakvimNobetGrup> tarihlerTumu,
//    VariableCollection<EczaneNobetTarihAralik> _x);

//void HerAyEnAz1Gorev(Model model,
//    NobetUstGrupKisitDetay nobetUstGrupKisitDetay,
//    List<TakvimNobetGrup> tarihler,
//    int gruptakiNobetciSayisi,
//    EczaneNobetGrupDetay eczaneNobetGrup,
//    List<EczaneNobetTarihAralik> eczaneNobetTarihAralikEczaneBazli,
//    VariableCollection<EczaneNobetTarihAralik> _x);

//void HerAyEnAz1HaftaIciGorev(Model model,
//    NobetUstGrupKisitDetay nobetUstGrupKisitDetay,
//    List<TakvimNobetGrup> haftaIciGunleri,
//    int gruptakiNobetciSayisi,
//    EczaneNobetGrupDetay eczaneNobetGrup,
//    List<EczaneNobetTarihAralik> eczaneNobetTarihAralikEczaneBazli,
//    VariableCollection<EczaneNobetTarihAralik> _x);

//void FarkliAyPespeseGorev(Model model,
//    List<EczaneNobetSonucListe2> eczaneGrupNobetSonuclar,
//    List<EczaneNobetTarihAralik> eczaneNobetTarihAralik,
//    NobetUstGrupKisitDetay nobetUstGrupKisitDetay,
//    List<TakvimNobetGrup> tarihler,
//    int pespeseNobetSayisi,
//    EczaneNobetGrupDetay eczaneNobetGrup,
//    VariableCollection<EczaneNobetTarihAralik> _x);

//primitive types
/*
        void TarihAraligiOrtalamaEnFazla(Model model,
        List<TakvimNobetGrup> tarihler,
        int gunSayisi,
        double ortalamaNobetSayisi,
        EczaneNobetGrupDetay eczaneNobetGrup,
        List<EczaneNobetTarihAralik> eczaneNobetTarihAralik,
        NobetUstGrupKisitDetay nobetUstGrupKisitDetay,
        VariableCollection<EczaneNobetTarihAralik> _x);
 */

/*
        void PesPeseGorevEnAz(Model model,
        int nobetSayisi,
        List<EczaneNobetTarihAralik> eczaneNobetTarihAralik,
        NobetUstGrupKisitDetay nobetUstGrupKisitDetay,
        List<TakvimNobetGrup> tarihler,
        DateTime nobetYazilabilecekIlkTarih,
        EczaneNobetGrupDetay eczaneNobetGrup,
        VariableCollection<EczaneNobetTarihAralik> _x);        
 */

/*
        void EsGruptakiEczanelereAyniGunNobetYazma(Model model,
        List<EczaneNobetTarihAralik> eczaneNobetTarihAralikTumu,
        List<EczaneNobetSonucListe2> eczaneNobetSonuclarTumu,
        NobetUstGrupKisitDetay nobetUstGrupKisitDetay,
        List<EczaneGrupDetay> eczaneGruplarTumu,
        List<TakvimNobetGrup> tarihlerTumu,
        VariableCollection<EczaneNobetTarihAralik> _x);
 */


/*
       void KumulatifToplamEnFazla(Model model,
       List<TakvimNobetGrup> tarihler,
       EczaneNobetGrupDetay eczaneNobetGrup,
       List<EczaneNobetTarihAralik> eczaneNobetTarihAralik,
       NobetUstGrupKisitDetay nobetUstGrupKisitDetay,
       double kumulatifOrtalamaGunKuralSayisi,
       int toplamNobetSayisi,
       VariableCollection<EczaneNobetTarihAralik> _x);     
 */

/*
       void TalebiKarsila(Model model,
       List<EczaneNobetTarihAralik> eczaneNobetTarihAralikTumu,
       int gunlukNobetciSayisi,
       List<NobetGrupTalepDetay> nobetGrupTalepler,
       NobetGrupGorevTipDetay nobetGrupGorevTip,
       List<TakvimNobetGrup> tarihler,
       VariableCollection<EczaneNobetTarihAralik> variableCollectionEczaneNobetTarihAralik);
 */

/*
void HerAyPespeseGorev(Model model,
    List<EczaneNobetTarihAralik> eczaneNobetTarihAralik,
    NobetUstGrupKisitDetay nobetUstGrupKisitDetay,
    List<TakvimNobetGrup> tarihler,
    int pespeseNobetSayisi,
    EczaneNobetGrupDetay eczaneNobetGrup,
    VariableCollection<EczaneNobetTarihAralik> _x);     
*/
/*
 void MazereteGorevYazma(Model model,
            List<EczaneNobetTarihAralik> eczaneNobetTarihAralikTumu,
            NobetUstGrupKisitDetay nobetUstGrupKisitDetay,
            List<EczaneNobetMazeretDetay> eczaneNobetMazeretler,
            VariableCollection<EczaneNobetTarihAralik> _x);
     */
/*
 void IstegiKarsila(Model model,
            List<EczaneNobetTarihAralik> eczaneNobetTarihAralikTumu,
            NobetUstGrupKisitDetay nobetUstGrupKisitDetay,
            List<EczaneNobetIstekDetay> eczaneNobetIstekler,
            VariableCollection<EczaneNobetTarihAralik> _x); 
     */
/*
 void PespeseFarkliTurNobetYaz(Model model,
            List<TakvimNobetGrup> bayramlar,
            EczaneNobetGrupDetay eczaneNobetGrup,
            List<EczaneNobetTarihAralik> eczaneNobetTarihAralik,
            int sonBayramTuru,
            VariableCollection<EczaneNobetTarihAralik> _x);        
     */
/*
 void HerAyHaftaIciPespeseGorev(Model model,
            List<TakvimNobetGrup> tarihler,
            List<TakvimNobetGrup> haftaIciGunleri,
            double haftaIciOrtamalaNobetSayisi,
            EczaneNobetGrupDetay eczaneNobetGrup,
            List<EczaneNobetTarihAralik> eczaneNobetTarihAralik,
            double pespeseNobetSayisiAltLimit,
            VariableCollection<EczaneNobetTarihAralik> _x,
            bool kisitAktifMi);*/
/*
 void TarihAraligindaEnAz1NobetYaz(Model model,
            NobetUstGrupKisitDetay nobetUstGrupKisitDetay,
            List<TakvimNobetGrup> tarihler,
            int gruptakiNobetciSayisi,
            EczaneNobetGrupDetay eczaneNobetGrup,
            List<EczaneNobetTarihAralik> eczaneNobetTarihAralikEczaneBazli,
            VariableCollection<EczaneNobetTarihAralik> _x);
     */

/*
         void AyIcindeSadece1KezAyniGunNobetTutulsun(Model model,
            List<EczaneNobetTarihAralik> eczaneNobetTarihAralik,
            List<AyniGunTutulanNobetDetay> ikiliEczaneler,
            NobetUstGrupKisitDetay nobetUstGrupKisitDetay,
            List<TakvimNobetGrup> tarihler,
            VariableCollection<EczaneNobetTarihAralik> _x);
     */
/*
 void BirEczaneyeAyniGunSadece1GorevYaz(Model model,
            List<EczaneNobetTarihAralik> eczaneNobetTarihAralik,
            List<EczaneNobetGrupDetay> eczaneNobetGruplar,
            List<TakvimNobetGrup> tarihler,
            NobetUstGrupKisitDetay nobetUstGrupKisitDetay,
            VariableCollection<EczaneNobetTarihAralik> _x);
 */
/*
 void IstenenEczanelerinNobetGunleriniKisitla(Model model,
            List<int> nobetYazilmayacakGunKuralIdList,
            List<EczaneNobetGrupDetay> eczaneNobetGruplar,
            List<EczaneNobetTarihAralik> eczaneNobetTarihAralikTumu,
            NobetUstGrupKisitDetay nobetUstGrupKisitDetay,
            VariableCollection<EczaneNobetTarihAralik> _x);*/
