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
    public interface IEczaneNobetSonucDal : IEntityRepository<EczaneNobetSonuc>, IEntityDetayRepository<EczaneNobetSonucDetay2>
    {
        //List<EczaneNobetSonucDetay2> GetDetayList2(Expression<Func<EczaneNobetSonucDetay2, bool>> filter = null);
        //EczaneNobetSonucDetay2 GetDetay2(Expression<Func<EczaneNobetSonucDetay2, bool>> filter);
        void CokluSil(int[] ids);
        void CokluEkle(List<EczaneNobetCozum> eczaneNobetCozumler);
        void CokluYayimla(List<EczaneNobetSonuc> eczaneNobetSonuclar, bool yayimlandiMi);
    }
}
