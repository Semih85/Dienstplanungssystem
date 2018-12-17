using System.Collections.Generic;
using WM.Core.DAL;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.DataAccess.Abstract.EczaneNobet
{
    public interface IEczaneNobetGrupDal : IEntityRepository<EczaneNobetGrup>, IEntityDetayRepository<EczaneNobetGrupDetay>
    {
        void CokluEkle(List<EczaneNobetGrup> eczaneNobetGruplar);
    }
}
