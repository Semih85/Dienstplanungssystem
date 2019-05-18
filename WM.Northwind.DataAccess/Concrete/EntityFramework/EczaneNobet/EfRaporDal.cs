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
    public class EfRaporDal : EfEntityRepositoryBase<Rapor, EczaneNobetContext>, IRaporDal
    {
        public RaporDetay GetDetay(Expression<Func<RaporDetay, bool>> filter)
        {
            using (var ctx = new EczaneNobetContext())
            {
                return ctx.Raporlar
                    .Select(s => new RaporDetay
                    {
                        Id = s.Id,
                        Adi = s.Adi,
                        RaporKategoriId = s.RaporKategoriId,
                        RaporKategoriAdi = s.RaporKategori.Adi,
                        SiraId = s.SiraId
                    }).SingleOrDefault(filter);
            }
        }
        public List<RaporDetay> GetDetayList(Expression<Func<RaporDetay, bool>> filter = null)
        {
            using (var ctx = new EczaneNobetContext())
            {
                var liste = ctx.Raporlar
                    .Select(s => new RaporDetay
                    {
                        Id = s.Id,
                        Adi = s.Adi,
                        RaporKategoriId = s.RaporKategoriId,
                        RaporKategoriAdi = s.RaporKategori.Adi,
                        SiraId = s.SiraId
                    });

                return filter == null
                    ? liste.ToList()
                    : liste.Where(filter).ToList();
            }
        }

    }
}