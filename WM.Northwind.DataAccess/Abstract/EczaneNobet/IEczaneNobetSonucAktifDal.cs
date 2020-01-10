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
    public interface IEczaneNobetSonucAktifDal : IEntityRepository<EczaneNobetSonucAktif>, IEntityDetayRepository<EczaneNobetSonucDetay2>
    {        
        void CokluSil(int[] ids);
        void CokluEkle(List<EczaneNobetCozum> eczaneNobetCozumler);
    }
}
