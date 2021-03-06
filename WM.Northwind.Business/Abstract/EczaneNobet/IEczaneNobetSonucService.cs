﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
    public interface IEczaneNobetSonucService
    {
        List<EczaneNobetSonuc> GetList();
        List<EczaneNobetSonuc> GetList(int[] ids);
        List<EczaneNobetSonucGunKural> GetDetaylarForIstatistik();
        EczaneNobetSonuc GetById(int Id);
        EczaneNobetSonucDetay2 GetDetay2ById(int Id);
        EczaneNobetSonucDetay2 GetDetay(int takvimId, int eczaneNobetGrupId);
        void Insert(EczaneNobetSonuc sonuc);
        void Update(EczaneNobetSonuc sonuc);
        void Delete(int Id);
        void Delete(int[] ids);
        void CokluSil(int[] ids);
        void CokluEkle(List<EczaneNobetCozum> eczaneNobetCozumler);
        DateTime GetSonNobetTarihi(int nobetUstGrupId);
        void CokluNobetYayimla(int[] ids, bool yayimlandiMi);
        List<MyDrop> GetNobetGrupSonYayimNobetTarihleri(int nobetUstGrupId);
        List<EczaneNobetIstatistik> GetEczaneNobetIstatistik2(List<int> nobetGrupIdList);
        List<EczaneNobetSonucNode> GetEczaneNobetSonucNodes();
        List<EczaneNobetSonucNode> GetEczaneNobetSonucNodes(int nobetUstGrupId);
        List<EczaneNobetSonucNode> GetEczaneNobetSonucNodes(int yil, int ay, List<int> eczaneIdList);
        List<EczaneNobetSonucEdge> GetEczaneNobetSonucEdges(int yil, int ay, int ayniGuneDenkGelenNobetSayisi, int nobetUstGrupId);
        List<EczaneNobetSonucDetay2> GetDetaylar(DateTime baslamaTarihi, int nobetGrupGorevTipId);
        List<EczaneNobetSonucDetay2> GetDetaylar(DateTime baslangicTarihi, DateTime bitisTarihi, int nobetUstGrupId);
        List<EczaneNobetSonucDetay2> GetDetaylar(DateTime baslangicTarihi, DateTime bitisTarihi, int[] nobetGrupIdList);
        List<EczaneNobetSonucDetay2> GetDetaylarByNobetGrupGorevTipIdList(DateTime? baslangicTarihi, DateTime? bitisTarihi, int[] nobetGrupGorevTipIdList);
        List<EczaneNobetSonucDetay2> GetDetaylarByNobetGrupGorevTipIdList(DateTime? baslangicTarihi, DateTime? bitisTarihi, int[] nobetGrupGorevTipIdList, bool kapaliEczaneler);
        List<EczaneNobetSonucDetay2> GetDetaylarByNobetGrupGorevTipIdList(DateTime? baslangicTarihi, DateTime? bitisTarihi, int[] nobetGrupGorevTipIdList, bool kapaliEczaneler, bool sanalNobetler);
        List<EczaneNobetSonucDetay2> GetDetaylar(int nobetUstGrupId);
        List<EczaneNobetSonucDetay2> GetDetaylarByNobetGrupGorevTipId(int nobetGrupGorevTipId);
        List<EczaneNobetSonucDetay2> GetDetaylar(List<int> nobetGrupIdList);
        List<EczaneNobetSonucDetay2> GetDetaylar(int yil, int ay, int nobetUstGrupId);
        List<EczaneNobetSonucDetay2> GetDetaylar2ByYilAyNobetGrup(int yil, int ay, int nobetGrupId, bool buAyVeSonrasi);
        List<EczaneNobetSonucDetay2> GetDetaylarAylik2(int yil, int ay, int nobetUstGrupId);
        List<EczaneNobetSonucDetay2> GetDetaylarUstGrupBaslamaTarihindenSonra(int[] nobetGrupIdList);
        List<EczaneNobetSonucDetay2> GetDetaylarUstGrupBaslamaTarihindenSonra(int nobetUstGrupId);
        List<EczaneNobetSonucDetay2> GetDetaylarUstGrupBaslamaTarihindenSonraEczaneNobetGrupId(int eczaneNobetGrupId);
        List<EczaneNobetSonucDetay2> GetDetaylarById(int[] ids);
        List<EczaneNobetSonucDetay2> GetDetaylarUstGrupBaslamaTarihindenOnce(int nobetUstGrupId);
        List<EczaneNobetSonucDetay2> GetDetaylarGunluk(DateTime nobetTarihi, int nobetUstGrupId);
        List<EczaneNobetSonucDetay2> GetDetaylarByEczaneNobetGrupId(int eczaneNobetGrupId);
        List<EczaneNobetSonucListe2> GetSonuclar();
        List<EczaneNobetSonucListe2> GetSonuclar(int nobetUstGrupId);
        List<EczaneNobetSonucListe2> GetSonuclar(int[] nobetGrupGorevTipIdList);
        List<EczaneNobetSonucListe2> GetSonuclar(int[] nobetGrupGorevTipIdList, DateTime? baslangicTarihi, DateTime? bitisTarihi);
        List<EczaneNobetSonucListe2> GetSonuclar(int yil, int ay, int nobetUstGrupId);
        List<EczaneNobetSonucListe2> GetSonuclar(int nobetGrupGorevTipId, int gunGrupId);
        List<EczaneNobetSonucListe2> GetSonuclar(DateTime baslangicTarihi, DateTime bitisTarihi, int nobetUstGrupId);
        List<EczaneNobetSonucListe2> GetSonuclar(DateTime baslangicTarihi, DateTime bitisTarihi, List<EczaneNobetSonucListe2> eczaneNobetSonuclar);
        List<EczaneNobetSonucListe2> GetSonuclar(List<EczaneNobetSonucDetay2> eczaneNobetSonucDetaylar, int nobetUstGrupId);
        List<EczaneNobetSonucListe2> GetSonuclar(List<EczaneNobetSonucListe2> eczaneNobetSonuclar, List<NobetDurumDetay> nobetDurumDetaylar);
        List<EczaneNobetSonucListe2> GetSonuclarUstGrupBaslamaTarihindenSonra(int[] nobetGrupIdList);
        List<EczaneNobetSonucListe2> GetSonuclarUstGrupBaslamaTarihindenSonra(int nobetUstGrupId);
        List<EczaneGrupNobetSonuc> GetEczaneGrupNobetSonuc(int nobetUstGrupId);
        List<EczaneGrupNobetSonuc> GetEczaneGrupNobetSonuc(List<int> nobetGrupIdList);

        List<EczaneNobetGrupGunKuralIstatistik> GetEczaneNobetGrupGunKuralIstatistik(int nobetUstGrupId);
        List<EczaneNobetGrupGunKuralIstatistik> GetEczaneNobetGrupGunKuralIstatistik(List<EczaneGrupNobetSonuc> eczaneGrupNobetSonuc);
        List<EczaneNobetGrupGunKuralIstatistik> GetEczaneNobetGrupGunKuralIstatistik(List<EczaneNobetGrupDetay> eczaneNobetGruplar, List<EczaneGrupNobetSonuc> eczaneGrupNobetSonuc);
        List<EczaneNobetIstatistikGunFarki> EczaneNobetIstatistikGunFarkiHesapla(int nobetUstGrupId);
        void UpdateSonuclarInsertDegisim(EczaneNobetSonuc eczaneNobetSonuc, EczaneNobetDegisim eczaneNobetDegisim);
        void UpdateSonuclarInsertDegisim(List<NobetDegisim> nobetDegisimler);
        List<EczaneGrupDetay> OncekiAylardaAyniGunNobetTutanlar(DateTime baslangicTarihi, List<EczaneNobetSonucListe2> eczaneNobetSonuclarOncekiAylar, int indisId, int oncekiBakilacakAylar);
        void InsertSonuclarInsertSanalSonuclar(EczaneNobetSonuc eczaneNobetSonuc, EczaneNobetSanalSonuc eczaneNobetSanalSonuc);
        void UpdateSonuclarUpdateSanalSonuclar(EczaneNobetSonuc eczaneNobetSonuc, EczaneNobetSanalSonuc eczaneNobetSanalSonuc);
        void SilSonuclarSilSanalSonuclar(int eczaneNobetSonucId);
        List<EczaneNobetSonucMobilUygulama> GetSonuclarMobilUygulama(DateTime baslangicTarihi, DateTime bitisTarihi, int nobetUstGrupId);
        List<EczaneGrupNobetSonuc> GetEczaneGrupNobetSonuc(DateTime baslangicTarihi, DateTime bitisTarihi, int nobetUstGrupId);

    }
}
