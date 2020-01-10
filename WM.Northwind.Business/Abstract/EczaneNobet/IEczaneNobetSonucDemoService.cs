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
    public interface IEczaneNobetSonucDemoService
    {
        EczaneNobetSonucDemo GetById(int eczaneNobetSonucDemoId);
        List<EczaneNobetSonucDemo> GetList();

        void Insert(EczaneNobetSonucDemo eczaneNobetSonucDemo);
        void Update(EczaneNobetSonucDemo eczaneNobetSonucDemo);
        void Delete(int Id);

        EczaneNobetSonucDemoDetay2 GetDetayById(int eczaneNobetSonucId);
        List<EczaneNobetSonucDemoDetay2> GetDetaylar();
        List<EczaneNobetSonucDemoDetay2> GetDetaylar(int nobetUstGrupId);
        //List<EczaneNobetSonucListe> GetSonuclarYillikKumulatif(int yil, int ay, int demoSonucVersiyon, int nobetUstGrupId);
        //List<EczaneNobetSonucListe> GetSonuclar();

        // complex types
        //List<EczaneNobetIstatistik> GetEczaneNobetIstatistik();
        //List<EczaneNobetSonucNode> GetEczaneNobetSonucDemoNodes();
        //List<EczaneNobetSonucNode> GetEczaneNobetSonucDemoNodes(int yil, int ay, int demoTipId, List<int> eczaneIdList);
        //List<EczaneNobetSonucEdge> GetEczaneNobetSonucDemoEdges(int yil, int ay, int ayniGuneDenkGelenNobetSayisi, int demoSonucVersiyon, int nobetUstGrupId);
        List<EczaneNobetSonucListe2> GetSonuclar2(int nobetUstGrupId);

        List<EczaneNobetIstatistikGunFarki> EczaneNobetIstatistikGunFarkiHesapla(int nobetUstGrupId);
        List<EczaneNobetIstatistikGunFarki> EczaneNobetIstatistikGunFarkiHesapla(int nobetUstGrupId, int demoSonucVersiyon);
    }
}
