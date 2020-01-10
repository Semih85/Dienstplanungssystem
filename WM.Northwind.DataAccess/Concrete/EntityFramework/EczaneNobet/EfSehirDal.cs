using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WM.Core.DAL.EntityFramework;
using WM.Northwind.DataAccess.Abstract.EczaneNobet;
using WM.Northwind.DataAccess.Concrete.EntityFramework.Contexts;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.DataAccess.Concrete.EntityFramework.EczaneNobet
{
    public class EfSehirDal : EfEntityRepositoryBase<Sehir, EczaneNobetContext>, ISehirDal
    {
        public SehirDetay GetDetay(Expression<Func<SehirDetay, bool>> filter)
        {
            using (var ctx = new EczaneNobetContext())
            {
                return ctx.Sehirler
                    .Select(s => new SehirDetay
                    {
                        Id = s.Id,
                        Adi = s.Adi,
                        EczaneOdaAdi = s.EczaneOda.Adi,
                        EczaneOdaId = s.EczaneOdaId
                    }).SingleOrDefault(filter);
            }
        }

        public List<SehirDetay> GetDetayList(Expression<Func<SehirDetay, bool>> filter = null)
        {
            using (var ctx = new EczaneNobetContext())
            {
                var liste = ctx.Sehirler
                      .Select(s => new SehirDetay
                      {
                          Id = s.Id,
                          Adi = s.Adi,
                          EczaneOdaAdi = s.EczaneOda.Adi,
                          EczaneOdaId = s.EczaneOdaId
                      });

                return filter == null
                   ? liste.ToList()
                   : liste.Where(filter).ToList();
            }
        }
    }
}
