using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.Authorization;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.Business.Abstract.EczaneNobet
{
    public interface IEczaneNobetGrupService
    {
        EczaneNobetGrup GetById(int eczaneNobetGrupId);
        List<EczaneNobetGrup> GetList();
        List<EczaneNobetGrup> GetList(int nobetGrupGorevTipId);
        List<EczaneNobetGrup> GetList(List<int> nobetGrupGorevTipIdList);
        List<EczaneNobetGrup> GetListByNobetUstGrupId(int nobetUstGrupId);
        List<EczaneNobetGrup> GetAktifEczaneGrupList(List<int> nobetGrupGorevTipIdList);
        List<EczaneNobetGrup> GetList(int eczaneNobetGrupId, DateTime baslangicTarihi, DateTime bitisTarihi);
        List<EczaneNobetGrup> GetList(List<int> nobetGrupGorevTipIdList, DateTime baslangicTarihi, DateTime bitisTarihi);

        void Insert(EczaneNobetGrup eczaneNobetGrup);
        void Update(EczaneNobetGrup eczaneNobetGrup);
        void Delete(int eczaneNobetGrupId);

        //complexTypes
        EczaneNobetGrupDetay GetDetayById(int eczaneNobetGrupId);
        EczaneNobetGrupDetay GetDetayByEczaneId(int eczaneId);

        EczaneNobetGrupDetay GetEczaneninOncekiNobetGrubu(int eczaneId);

        List<EczaneNobetGrupDetay> GetDetaylar();
        List<EczaneNobetGrupDetay> GetDetaylar(int nobetGrupId, int eczaneId);
        List<EczaneNobetGrupDetay> GetDetaylar(int nobetUstGrupId);
        List<EczaneNobetGrupDetay> GetDetaylar(List<int> nobetGrupIdList);
        List<EczaneNobetGrupDetay> GetDetaylarByEczaneIdList(List<int> eczaneIdList);
        List<EczaneNobetGrupDetay> GetDetaylarByNobetGrupGorevTipler(List<int> nobetGrupGorevTipIdList);
        List<EczaneNobetGrupDetay> GetDetaylarByNobetGrupGorevTipler(List<int> nobetGrupGorevTipIdList, bool tumEczaneler);
        List<EczaneNobetGrupDetay> GetDetaylarByNobetGrupGorevTipler(DateTime baslangicTarihi, DateTime bitisTarihi, List<int> nobetGrupGorevTipIdList);
        List<EczaneNobetGrupDetay> GetDetaylar(List<int> nobetGrupIdList, DateTime baslangicTarihi, DateTime bitisTarihi);
        List<EczaneNobetGrupDetay> GetDetaylarTarihAralik(List<int> nobetGrupIdList, DateTime baslangicTarihi, DateTime bitisTarihi);

        List<EczaneNobetGrupDetay> GetDetaylar(int nobetGrupId, DateTime baslangicTarihi, DateTime bitisTarihi);
        List<EczaneNobetGrupDetay> GetDetaylar(int[] eczaneNobetGrupIdList);
        List<EczaneNobetGrupDetay> GetAktifEczaneNobetGrupList(List<int> nobetGrupIdList);
        List<EczaneNobetGrupDetay> GetAktifEczaneNobetGrup(int nobetGrupId);
        List<EczaneNobetGrupDetay> GetDetaylar(DateTime baslangicTarihi, DateTime bitisTarihi, int nobetGrupGorevTipId);
        List<EczaneNobetGrupDetay> GetDetaylarNobetUstGrupId(DateTime baslangicTarihi, DateTime bitisTarihi, int nobetUstGrupId);
        List<EczaneNobetGrupDetay> GetDetaylar(List<int> eczaneIdList, int nobetGrupGorevTipId);
        List<EczaneNobetGrupDetay> GetAktifEczaneGrupListByNobetGrupGorevTipIdList(List<int> nobetGrupGorevTipIdList);
        List<EczaneNobetGrupDetay> GetDetaylarByNobetGrupGorevTipler(int nobetGrupGorevTipId);

        List<EczaneNobetGrupIstatistik> NobetGruplarDDL(int nobetUstGrupId);
        void CokluEkle(List<EczaneNobetGrup> eczaneNobetGruplar);

        List<MyDrop> GetMyDrop(List<EczaneNobetGrupDetay> czaneNobetGrupDetaylar);
        List<EczaneNobetGrupDetay> GetDetaylarByNobetUstGrupIdList(List<int> nobetUstGrupIdList);

        int GetEczaneninGrubaGirdigiTarihtekiEczaneSayisi(int eczaneNobetGrupId);
    }
}
