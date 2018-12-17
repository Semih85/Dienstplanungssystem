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
    public interface IEczaneNobetMuafiyetService
    {
        EczaneNobetMuafiyet GetById(int eczaneNobetMuafiyetId);
        List<EczaneNobetMuafiyet> GetList();
        //List<EczaneNobetMuafiyet> GetByCategory(int categoryId);
        void Insert(EczaneNobetMuafiyet eczaneNobetMuafiyet);
        void Update(EczaneNobetMuafiyet eczaneNobetMuafiyet);
        void Delete(int eczaneNobetMuafiyetId);
        EczaneNobetMuafiyetDetay GetDetayById(int eczaneNobetMuafiyetId);
        List<EczaneNobetMuafiyetDetay> GetDetaylar();
        List<EczaneNobetMuafiyetDetay> GetDetaylar(DateTime baslangicTarihi, DateTime bitisTarihi);
        List<EczaneNobetMuafiyetDetay> GetDetaylar(DateTime baslangicTarihi, DateTime bitisTarihi, List<int> nobetGrupIdList);
    }
}