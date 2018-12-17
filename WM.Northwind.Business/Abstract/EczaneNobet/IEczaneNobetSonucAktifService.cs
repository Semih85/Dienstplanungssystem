using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Entities.ComplexTypes;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.Business.Abstract.EczaneNobet
{
    public interface IEczaneNobetSonucAktifService
    {   
        List<EczaneNobetSonucAktif> GetList();

        //List<EczaneNobetSonucDetay> GetDetaylar();
        //List<EczaneNobetSonucDetay> GetDetaylar(int nobetUstGrupId);
        //List<EczaneNobetSonucDetay> GetDetaylarByNobetGrupId(int nobetGrupId);

        List<EczaneNobetSonucDetay2> GetDetaylar2();
        List<EczaneNobetSonucDetay2> GetDetaylar2(int nobetUstGrupId);
        List<EczaneNobetSonucDetay2> GetDetaylar2(int nobetGrupId, int yil, int ay);
        List<EczaneNobetSonucDetay2> GetDetaylar2ByNobetUstGrup(int yil, int ay, int nobetUstGrupId);

        EczaneNobetSonucAktif GetById(int Id);
        //
        void Insert(EczaneNobetSonucAktif sonuc);
        void Update(EczaneNobetSonucAktif sonuc);
        void Delete(int Id);

        void CokluSil(int[] ids);
        void CokluEkle(List<EczaneNobetCozum> eczaneNobetCozumler);

        // complex types
        //List<EczaneNobetSonucListe> GetSonuclar(Expression<Func<EczaneNobetSonucListe, bool>> filter = null);
        //List<EczaneNobetSonucListe> GetSonuclar(int nobetUstGrupId);
        //List<EczaneNobetSonucListe> GetSonuclar(int yil, int ay);
        //List<EczaneNobetSonucListe> GetSonuclar(int nobetGrupId, int yil, int ay);
        //List<EczaneNobetSonucListe> GetSonuclarAylik(int yil, int ay, int nobetUstGrupId);
        //List<EczaneNobetSonucListe> GetSonuclarYillikKumulatif(int yil, int ay, int nobetUstGrupId);
        //List<EczaneNobetSonucListe> GetSonuclarByNobetGrupId(int nobetGrupId);

        List<EczaneNobetIstatistik> GetEczaneNobetIstatistik(Expression<Func<EczaneNobetIstatistik, bool>> filter = null);
        List<EczaneNobetIstatistik> GetEczaneNobetIstatistik(List<int> nobetGrupIdList);

        List<EczaneNobetSonucListe2> GetSonuclar2(int yil, int ay, int nobetUstGrupId);
        List<EczaneNobetSonucListe2> GetSonuclar2(int nobetUstGrupId);
        List<EczaneNobetSonucListe2> GetSonuclar2(DateTime baslangicTarihi, DateTime bitisTarihi, int nobetUstGrupId);

        List<EczaneNobetCozum> GetCozumler(int nobetUstGrupId);
        List<EczaneNobetCozum> GetCozumler(int nobetGrupId, int yil, int ay);
        List<EczaneNobetCozum> GetCozumler(int[] nobetGrupIdList, int yil, int ay);
        List<EczaneNobetCozum> GetCozumler(int[] nobetGrupIdList, DateTime baslangicTarihi, DateTime bitisTarihi);
    }
}
