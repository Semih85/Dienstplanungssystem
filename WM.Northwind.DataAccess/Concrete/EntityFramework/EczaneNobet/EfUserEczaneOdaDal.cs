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
    public class EfUserEczaneOdaDal : EfEntityRepositoryBase<UserEczaneOda, EczaneNobetContext>, IUserEczaneOdaDal
    {
        public UserEczaneOdaDetay GetDetay(Expression<Func<UserEczaneOdaDetay, bool>> filter)
        {
            using (var ctx = new EczaneNobetContext())
            {
                return ctx.UserEczaneOdalar
                    .Select(s => new UserEczaneOdaDetay
                    {
                        Id = s.Id,
                        KullaniciAdi = s.User.UserName,
                        EczaneOdaAdi = s.EczaneOda.Adi,
                        EczaneOdaId = s.EczaneOdaId,
                        UserId=s.UserId,
                        BaslamaTarihi = s.BaslamaTarihi,
                        BitisTarihi = s.BitisTarihi
                    }).SingleOrDefault(filter);
            }
        }

        public List<UserEczaneOdaDetay> GetDetayList(Expression<Func<UserEczaneOdaDetay, bool>> filter = null)
        {
            using (var ctx = new EczaneNobetContext())
            {
                return filter == null
                   ? ctx.UserEczaneOdalar
                      .Select(s => new UserEczaneOdaDetay
                      {
                          Id = s.Id,
                          KullaniciAdi = s.User.UserName,
                          EczaneOdaAdi = s.EczaneOda.Adi,
                          EczaneOdaId = s.EczaneOdaId,
                          UserId = s.UserId,
                          BaslamaTarihi = s.BaslamaTarihi,
                          BitisTarihi = s.BitisTarihi
                      }).ToList()
                   : ctx.UserEczaneOdalar
                      .Select(s => new UserEczaneOdaDetay
                      {
                          Id = s.Id,
                          KullaniciAdi = s.User.UserName,
                          EczaneOdaAdi = s.EczaneOda.Adi,
                          EczaneOdaId = s.EczaneOdaId,
                          UserId = s.UserId,
                          BaslamaTarihi = s.BaslamaTarihi,
                          BitisTarihi = s.BitisTarihi
                      })
                      .Where(filter)
                      .ToList();
            }
        }
    }
}
