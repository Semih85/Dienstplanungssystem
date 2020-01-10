using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.DAL;
using System.Linq.Expressions;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.DataAccess.Abstract.EczaneNobet
{
    public interface INobetGrupGorevTipGunKuralDal : IEntityRepository<NobetGrupGorevTipGunKural>, IEntityDetayRepository<NobetGrupGorevTipGunKuralDetay>
    {
    }
}