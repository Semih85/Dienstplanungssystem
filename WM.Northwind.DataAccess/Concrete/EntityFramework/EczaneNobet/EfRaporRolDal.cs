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
    public class EfRaporRolDal : EfEntityRepositoryBase<RaporRol, EczaneNobetContext>, IRaporRolDal
    {
        public RaporRolDetay GetDetay(Expression<Func<RaporRolDetay, bool>> filter)
        {
            using (var ctx = new EczaneNobetContext())
            {
                return ctx.RaporRoller
                    .Select(s => new RaporRolDetay
                    {
                        RaporId = s.RaporId,
                        RaporAdi = s.Rapor.Adi,
                        RoleId = s.RoleId,
                        RoleAdi = s.Role.Name,
                        Id = s.Id,
                        RaporKategoriId = s.Rapor.RaporKategoriId,
                        RaporKategoriAdi = s.Rapor.RaporKategori.Adi
                    }).SingleOrDefault(filter);
            }
        }
        public List<RaporRolDetay> GetDetayList(Expression<Func<RaporRolDetay, bool>> filter = null)
        {
            using (var ctx = new EczaneNobetContext())
            {
                var liste = ctx.RaporRoller
                    .Select(s => new RaporRolDetay
                    {
                        RaporId = s.RaporId,
                        RaporAdi = s.Rapor.Adi,
                        RoleId = s.RoleId,
                        RoleAdi = s.Role.Name,
                        Id = s.Id,
                        RaporKategoriId = s.Rapor.RaporKategoriId,
                        RaporKategoriAdi = s.Rapor.RaporKategori.Adi
                    });

                return filter == null
                    ? liste.ToList()
                    : liste.Where(filter).ToList();
            }
        }

    }
}