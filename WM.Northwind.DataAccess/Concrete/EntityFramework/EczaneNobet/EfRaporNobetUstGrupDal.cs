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
    public class EfRaporNobetUstGrupDal : EfEntityRepositoryBase<RaporNobetUstGrup, EczaneNobetContext>, IRaporNobetUstGrupDal
    {
        public RaporNobetUstGrupDetay GetDetay(Expression<Func<RaporNobetUstGrupDetay, bool>> filter)
        {
            using (var ctx = new EczaneNobetContext())
            {
                return ctx.RaporNobetUstGruplar
                    .Select(s => new RaporNobetUstGrupDetay
                    {
                        Id = s.Id,
                        RaporId = s.RaporId,
                        NobetUstGrupAdi = s.NobetUstGrup.Adi,
                        NobetUstGrupId = s.NobetUstGrupId,
                        RaporAdi = s.Rapor.Adi,
                        RaporKategoriAdi = s.Rapor.RaporKategori.Adi,
                        RaporKategoriId = s.Rapor.RaporKategori.Id,
                        RaporSiraId = s.Rapor.SiraId
                    }).SingleOrDefault(filter);
            }
        }
        public List<RaporNobetUstGrupDetay> GetDetayList(Expression<Func<RaporNobetUstGrupDetay, bool>> filter = null)
        {
            using (var ctx = new EczaneNobetContext())
            {
                var liste = ctx.RaporNobetUstGruplar
                    .Select(s => new RaporNobetUstGrupDetay
                    {
                        Id = s.Id,
                        RaporId = s.RaporId,
                        NobetUstGrupAdi = s.NobetUstGrup.Adi,
                        NobetUstGrupId = s.NobetUstGrupId,
                        RaporAdi = s.Rapor.Adi,
                        RaporKategoriAdi = s.Rapor.RaporKategori.Adi,
                        RaporKategoriId = s.Rapor.RaporKategori.Id,
                        RaporSiraId = s.Rapor.SiraId
                    });

                return filter == null
                    ? liste.ToList()
                    : liste.Where(filter).ToList();
            }
        }

    }
}