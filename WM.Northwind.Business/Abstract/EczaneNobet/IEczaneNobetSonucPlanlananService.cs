using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;
using WM.Northwind.Entities.Concrete.Optimization.EczaneNobet;

namespace WM.Northwind.Business.Abstract.EczaneNobet
{
    public interface IEczaneNobetSonucPlanlananService
    {
        EczaneNobetSonucPlanlanan GetById(int eczaneNobetSonucPlanlananId);
        List<EczaneNobetSonucPlanlanan> GetList();
        //List<EczaneNobetSonucPlanlanan> GetByCategory(int categoryId);
        void Insert(EczaneNobetSonucPlanlanan eczaneNobetSonucPlanlanan);
        void Update(EczaneNobetSonucPlanlanan eczaneNobetSonucPlanlanan);
        void Delete(int eczaneNobetSonucPlanlananId);
        EczaneNobetSonucDetay2 GetDetayById(int eczaneNobetSonucPlanlananId);
        List<EczaneNobetSonucDetay2> GetDetaylar();
        List<EczaneNobetSonucDetay2> GetDetaylar(int nobetUstGrupId);
        List<EczaneNobetSonucListe2> SiraliNobetYaz(int nobetUstGrupId);
        void CokluSil(int[] ids);
        void CokluEkle(List<EczaneNobetCozum> eczaneNobetCozumler);
    }
}