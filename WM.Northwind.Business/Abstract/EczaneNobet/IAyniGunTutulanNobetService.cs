using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;
using WM.Northwind.Entities.Concrete.Enums;
using WM.Northwind.Entities.Concrete.Optimization.EczaneNobet;

namespace WM.Northwind.Business.Abstract.EczaneNobet
{
    public interface IAyniGunTutulanNobetService
    {
        AyniGunTutulanNobet GetById(int ayniGunTutulanNobetId);
        List<AyniGunTutulanNobet> GetList();
        List<AyniGunTutulanNobet> GetList(int nobetUstGrupId);
        List<AyniGunTutulanNobet> GetListSifirdanBuyukler(int nobetUstGrupId);
        List<AyniGunTutulanNobet> GetListSifirdanFarkli(int nobetUstGrupId);
        //List<AyniGunTutulanNobet> GetByCategory(int categoryId);
        void Insert(AyniGunTutulanNobet ayniGunTutulanNobet);
        void Update(AyniGunTutulanNobet ayniGunTutulanNobet);
        void Delete(int ayniGunTutulanNobetId);
        AyniGunTutulanNobetDetay GetDetayById(int ayniGunTutulanNobetId);
        AyniGunTutulanNobetDetay GetDetay(int eczaneNobetGrupId1, int eczaneNobetGrupId2);
        List<AyniGunTutulanNobetDetay> GetDetaylar();
        List<AyniGunTutulanNobetDetay> GetDetaylar(int nobetUstGrupId);
        List<AyniGunTutulanNobetDetay> GetDetaylar(int nobetUstGrupId, int ayniGunNobetSayisi);
        List<AyniGunTutulanNobetDetay> GetDetaylar(List<int> nobetGrupIdList);
        List<AyniGunTutulanNobetDetay> GetDetaylar(int[] nobetGrupGorevTipIdList);
        List<AyniGunTutulanNobetDetay> IkiliEczaneleriOlustur(int nobetUstGrupId);
        List<AyniGunTutulanNobetDetay> IkiliEczaneleriOlustur(List<EczaneNobetGrupDetay> eczaneNobetGruplar);
        void AyniGunNobetTutanlariTabloyaEkle(List<AyniGunTutulanNobetDetay> ayniGunNobetTutanEczaneler);
        void AyniGunNobetSayisiniGuncelle(List<AyniGunTutulanNobetDetay> ayniGunNobetTutanEczaneler, AyniGunNobetEklemeTuru ayniGunNobetEklemeTuru);
        List<EczaneGrupDetay> GetArasinda2FarkOlanIkiliEczaneleri(List<EczaneNobetGrupDetay> eczaneNobetGruplar, int nobetUstGrupId, int nobetFarki);
        List<EczaneGrupDetay> GetArasinda2FarkOlanIkiliEczaneleri(List<EczaneNobetGrupDetay> eczaneNobetGruplar, int[] nobetGrupGorevTipIdList, int nobetFarki = 2);
        List<EczaneGrupDetay> ArasindaKritereGoreFarkOlanEczaneler(
            List<EczaneNobetGrupDetay> eczaneNobetGruplar,
            List<AyniGunTutulanNobetDetay> ikiliEczanelerTumu,
            int nobetFarki);
        void IkiliEczaneIstatistiginiSifirla(int nobetUstGrupId);
    }
}