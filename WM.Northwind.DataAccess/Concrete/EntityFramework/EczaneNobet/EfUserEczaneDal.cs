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
    public class EfUserEczaneDal : EfEntityRepositoryBase<UserEczane, EczaneNobetContext>, IUserEczaneDal
    {
        public UserEczaneDetay GetDetay(Expression<Func<UserEczaneDetay, bool>> filter)
        {
            using (var ctx = new EczaneNobetContext())
            {
                return ctx.UserEczaneler
                    .Select(s => new UserEczaneDetay
                    {
                        Id = s.Id,
                        KullaniciAdi = s.User.UserName,
                        EczaneAdi = s.Eczane.Adi,
                        EczaneId = s.EczaneId,
                        UserId = s.UserId,
                        NobetUstGrupId = s.Eczane.NobetUstGrupId,
                        BaslamaTarihi = s.BaslamaTarihi,
                        BitisTarihi = s.BitisTarihi
                    }).SingleOrDefault(filter);
            }
        }

        public List<UserEczaneDetay> GetDetayList(Expression<Func<UserEczaneDetay, bool>> filter = null)
        {
            using (var ctx = new EczaneNobetContext())
            {
                var liste = ctx.UserEczaneler
                      .Select(s => new UserEczaneDetay
                      {
                          Id = s.Id,
                          KullaniciAdi = s.User.UserName,
                          EczaneAdi = s.Eczane.Adi,
                          EczaneId = s.EczaneId,
                          UserId = s.UserId,
                          NobetUstGrupId = s.Eczane.NobetUstGrupId,
                          BaslamaTarihi = s.BaslamaTarihi,
                          BitisTarihi = s.BitisTarihi
                      });

                return filter == null
                    ? liste.ToList()
                    : liste.Where(filter).ToList();                
            }
        }
    }
}
