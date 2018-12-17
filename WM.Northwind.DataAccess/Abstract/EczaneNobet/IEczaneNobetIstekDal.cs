using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.DAL;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.DataAccess.Abstract.EczaneNobet
{
    public interface IEczaneNobetIstekDal : IEntityRepository<EczaneNobetIstek>, IEntityDetayRepository<EczaneNobetIstekDetay>
    {
        //List<EczaneNobetIstekListe> GetEczaneNobetIstekListe(int yil, int ay, List<int> ecznaneIdList);
        //List<EczaneNobetIstekListe> GetEczaneNobetIstekListe(int yil, int ay, int nobetGrupId);
        //List<EczaneNobetIstekListe> GetEczaneNobetIstekListe(int yil, int ay);
        //List<EczaneNobetIstekListe> GetEczaneNobetIstekListe(int yil, int ayBaslangic, int ayBitis, int nobetGrupId);

        //List<EczaneNobetIstekDetay> GetEczaneNobetIstekDetaylar();
    }
}
