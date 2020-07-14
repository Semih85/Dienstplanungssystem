using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;
//using WM.Northwind.Entities.Concrete.Optimization.EczaneNobet;

namespace WM.Northwind.Business.Abstract.EczaneNobet
{
    public interface IEczaneNobetSonucAnahtarListeService
    {
        EczaneNobetSonucAnahtarListe GetById(int eczaneNobetSonucAnahtarListeId);
        List<EczaneNobetSonucAnahtarListe> GetList();
        //List<EczaneNobetSonucAnahtarListe> GetByCategory(int categoryId);
        void Insert(EczaneNobetSonucAnahtarListe eczaneNobetSonucAnahtarListe);
        void Update(EczaneNobetSonucAnahtarListe eczaneNobetSonucAnahtarListe);
        void Delete(int eczaneNobetSonucAnahtarListeId);
                        EczaneNobetSonucAnahtarListeDetay GetDetayById(int eczaneNobetSonucAnahtarListeId);
                                   List <EczaneNobetSonucAnahtarListeDetay> GetDetaylar();
    }
} 