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
    public class EfUserNobetUstGrupDal : EfEntityRepositoryBase<UserNobetUstGrup, EczaneNobetContext>, IUserNobetUstGrupDal
    {
        public UserNobetUstGrupDetay GetDetay(Expression<Func<UserNobetUstGrupDetay, bool>> filter)
        {
            using (var ctx = new EczaneNobetContext())
            {
                return ctx.UserNobetUstGruplar
                    .Select(s => new UserNobetUstGrupDetay
                    {
                        Id = s.Id,
                        KullaniciAdi = s.User.UserName,
                        NobetUstGrupAdi = s.NobetUstGrup.Adi,
                        NobetUstGrupId = s.NobetUstGrupId,
                        UserId = s.UserId,
                        BaslamaTarihi = s.BaslamaTarihi,
                        BitisTarihi = s.BitisTarihi
                    }).SingleOrDefault(filter);
            }
        }

        public List<UserNobetUstGrupDetay> GetDetayList(Expression<Func<UserNobetUstGrupDetay, bool>> filter = null)
        {
            using (var ctx = new EczaneNobetContext())
            {
                return filter == null
                   ? ctx.UserNobetUstGruplar
                    .Select(s => new UserNobetUstGrupDetay
                    {
                        Id = s.Id,
                        KullaniciAdi = s.User.UserName,
                        NobetUstGrupAdi = s.NobetUstGrup.Adi,
                        NobetUstGrupId = s.NobetUstGrupId,
                        UserId = s.UserId,
                        BaslamaTarihi = s.BaslamaTarihi,
                        BitisTarihi = s.BitisTarihi
                    }).ToList()
                   : ctx.UserNobetUstGruplar
                    .Select(s => new UserNobetUstGrupDetay
                    {
                        Id = s.Id,
                        KullaniciAdi = s.User.UserName,
                        NobetUstGrupAdi = s.NobetUstGrup.Adi,
                        NobetUstGrupId = s.NobetUstGrupId,
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
