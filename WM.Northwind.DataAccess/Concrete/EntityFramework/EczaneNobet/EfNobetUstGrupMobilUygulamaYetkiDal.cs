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
    public class EfNobetUstGrupMobilUygulamaYetkiDal : EfEntityRepositoryBase<NobetUstGrupMobilUygulamaYetki, EczaneNobetContext>, INobetUstGrupMobilUygulamaYetkiDal
    {
        public NobetUstGrupMobilUygulamaYetkiDetay GetDetay(Expression<Func<NobetUstGrupMobilUygulamaYetkiDetay, bool>> filter)
        {
            using (var ctx = new EczaneNobetContext())
            {
                return ctx.NobetUstGrupMobilUygulamaYetkiler
                    .Select(s => new NobetUstGrupMobilUygulamaYetkiDetay
                    {
                        Id = s.Id,
                        NobetUstGrupId = s.NobetUstGrupId,
                        MobilUygulamaYetkiAdi = s.MobilUygulamaYetki.Adi,
                        MobilUygulamaYetkiId = s.MobilUygulamaYetkiId,
                        NobetUstGrupAdi = s.NobetUstGrup.Adi,
                        
                    }).SingleOrDefault(filter);
            }
        }
        public List<NobetUstGrupMobilUygulamaYetkiDetay> GetDetayList(Expression<Func<NobetUstGrupMobilUygulamaYetkiDetay, bool>> filter = null)
        {
            using (var ctx = new EczaneNobetContext())
            {
                var liste = ctx.NobetUstGrupMobilUygulamaYetkiler
                    .Select(s => new NobetUstGrupMobilUygulamaYetkiDetay
                    {
                        Id = s.Id,
                        NobetUstGrupId = s.NobetUstGrupId,
                        MobilUygulamaYetkiAdi = s.MobilUygulamaYetki.Adi,
                        MobilUygulamaYetkiId = s.MobilUygulamaYetkiId,
                        NobetUstGrupAdi = s.NobetUstGrup.Adi,
                        
                    });

                return filter == null
                    ? liste.ToList()
                    : liste.Where(filter).ToList();
            }
        }
    }
}