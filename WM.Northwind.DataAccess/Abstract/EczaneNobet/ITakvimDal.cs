using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WM.Core.DAL;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.DataAccess.Abstract.EczaneNobet
{
    public interface ITakvimDal : IEntityRepository<Takvim>
    {
        List<TakvimDetay> GetTakvimDetaylar(Expression<Func<TakvimDetay, bool>> filter = null);

        //List<EczaneNobetIstatistik> GetEczaneKumulatifHedefler(int yilBaslangic, int yilBitis, int ayBaslangic, int ayBitis, int nobetGrup);
        //List<EczaneNobetIstatistik> GetEczaneKumulatifHedefler(int yilBaslangic, int yilBitis, int ayBaslangic, int ayBitis, List<int> nobetGrupIdList);
        //List<EczaneNobetIstatistik> GetEczaneKumulatifHedeflerTumYillar(int yilBaslangic, int yilBitis, int ayBaslangic, int ayBitis, List<int> nobetGrupIdList);
        //List<EczaneNobetIstatistik> GetEczaneKumulatifHedefler(int yil, int ay, int nobetGrup, int nobetGorevTipId);
        //List<EczaneNobetIstatistik> GetEczaneKumulatifHedefler(int yil, int ay, int nobetGorevTipId);

        //List<NobetTalep> GetGrupIciGunlukNobetTalepler(int yil, int ay, int nobetGrupId);
        //List<EczaneNobetTarihAralik> GetEczaneNobetTarihAralik(int yil, int ay, int nobetGrupId, int nobetGorevTipId);
        //List<EczaneNobetTarihAralik> GetEczaneNobetTarihAralik(int yil, int ay, int nobetGorevTipId, List<int> nobetGrupIdList);

        //Takvimdeki tarih araliğı gün değerleri (hafta ve bayram gün değerleri) ile birlikte getiriliyor.
        //List<TarihAraligi> GetTarihAralik(int yil, int ay, int nobetGorevTipId);
        //List<TarihAraligi> GetTarihAralik(int yilBaslangic, int yilBitis, int ayBaslangic, int ayBitis, int nobetGorevTipId);
        //List<TarihAraligi> GetTarihAralik(DateTime baslangicTarihi, DateTime bitisTarihi);

        //List<TakvimNobetGrup> GetTakvimNobetGruplar(int yilBaslangic, int yilBitis, int ayBaslangic, int ayBitis, int nobetGrupId, int nobetGorevTipId);
        //List<TakvimNobetGrup> GetTakvimNobetGruplar(int yil, int ay, int nobetGrupId, int nobetGorevTipId);
        //List<TakvimNobetGrup> GetTakvimNobetGruplar(int yil = 2018, int ay = 1, List<int> nobetGrupIdList = null, int nobetGorevTipId = 1);
        //List<TakvimNobetGrup> GetTakvimNobetGruplar(int yil, int ay, int nobetGorevTipId);
    }
}
