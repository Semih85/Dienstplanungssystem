using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WM.Core.DAL.EntityFramework;
using WM.Northwind.DataAccess.Abstract.EczaneNobet;
using WM.Northwind.DataAccess.Concrete.EntityFramework.Contexts;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.DataAccess.Concrete.EntityFramework.EczaneNobet
{
    public class EfEczaneUzaklikMatrisDal : EfEntityRepositoryBase<EczaneUzaklikMatris, EczaneNobetContext>, IEczaneUzaklikMatrisDal
    {
        public EczaneUzaklikMatrisDetay GetDetay(Expression<Func<EczaneUzaklikMatrisDetay, bool>> filter)
        {
            using (var ctx = new EczaneNobetContext())
            {
                return ctx.EczaneUzaklikMatrisler
                    .Select(s => new EczaneUzaklikMatrisDetay
                    {
                        Id = s.Id,
                        EczaneIdFrom = s.EczaneIdFrom,
                        EczaneIdTo = s.EczaneIdTo,
                        Mesafe = s.Mesafe,
                        NobetUstGrupId = s.EczaneFrom.NobetUstGrupId,
                        EczaneAdiFrom = s.EczaneFrom.Adi,
                        EczaneAdiTo = s.EczaneTo.Adi
                    }).SingleOrDefault(filter);
            }
        }
        public List<EczaneUzaklikMatrisDetay> GetDetayList(Expression<Func<EczaneUzaklikMatrisDetay, bool>> filter = null)
        {
            using (var ctx = new EczaneNobetContext())
            {
                var liste = ctx.EczaneUzaklikMatrisler
                    .Select(s => new EczaneUzaklikMatrisDetay
                    {
                        Id = s.Id,
                        EczaneIdFrom = s.EczaneIdFrom,
                        EczaneIdTo = s.EczaneIdTo,
                        Mesafe = s.Mesafe,
                        NobetUstGrupId = s.EczaneFrom.NobetUstGrupId,
                        EczaneAdiFrom = s.EczaneFrom.Adi,
                        EczaneAdiTo = s.EczaneTo.Adi
                    });

                return filter == null
                    ? liste.ToList()
                    : liste.Where(filter).ToList();
            }
        }

    }
}