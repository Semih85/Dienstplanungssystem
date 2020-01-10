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
    public class EfNobetGrupDal : EfEntityRepositoryBase<NobetGrup, EczaneNobetContext>, INobetGrupDal
    {
        public NobetGrupDetay GetDetay(Expression<Func<NobetGrupDetay, bool>> filter)
        {
            using (var ctx = new EczaneNobetContext())
            {
                return ctx.NobetGruplar
                    .Select(s => new NobetGrupDetay
                    {
                        NobetUstGrupId = s.NobetUstGrupId,
                        NobetUstGrupAdi = s.NobetUstGrup.Adi,
                        Adi = s.Adi,
                        BaslamaTarihi = s.BaslamaTarihi,
                        BitisTarihi = s.BitisTarihi,
                        Id = s.Id
                    }).SingleOrDefault(filter);
            }
        }
        public List<NobetGrupDetay> GetDetayList(Expression<Func<NobetGrupDetay, bool>> filter = null)
        {
            using (var ctx = new EczaneNobetContext())
            {
                var liste = ctx.NobetGruplar
                    .Select(s => new NobetGrupDetay
                    {
                        NobetUstGrupId = s.NobetUstGrupId,
                        NobetUstGrupAdi = s.NobetUstGrup.Adi,
                        Adi = s.Adi,
                        BaslamaTarihi = s.BaslamaTarihi,
                        BitisTarihi = s.BitisTarihi,
                        Id = s.Id
                    });

                return filter == null
                    ? liste.ToList()
                    : liste.Where(filter).ToList();
            }
        }
    }
}