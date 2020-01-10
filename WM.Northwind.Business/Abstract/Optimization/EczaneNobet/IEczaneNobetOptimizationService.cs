using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.Authorization;
using WM.Northwind.Entities.Concrete.EczaneNobet;
using WM.Northwind.Entities.Concrete.Optimization.EczaneNobet;

namespace WM.Northwind.Business.Abstract.Optimization.EczaneNobet
{
    public interface IEczaneNobetOptimizationService
    {
        //EczaneNobetSonucModel Solve(EczaneNobetTekGrupDataModel data);
        //void EczaneNobetCozAktifiGuncelle(EczaneNobetTekGrupDataModel data);
        //void EczaneNobetCozAktifiGuncelle(EczaneNobetCokGrupDataModel data);
        //void ModelCoz(int nobetUstGrupId, int yilBaslangic = 2018, int yilBitis = 2018, int ayBaslangic = 1, int ayBitis = 0, int nobetGrupId = 0, int nobetGorevTipId = 1);
        //void Kesinlestir();
        void Kesinlestir(int nobetUstGrupId);
        void Kesinlestir(int nobetGrupId, int yil, int ay);
        void Kesinlestir(int[] nobetGrupIdList, int yil, int ay);
        void Kesinlestir(int[] nobetGrupIdList, DateTime baslangicTarihi, DateTime bitisTarihi);
    }
}
