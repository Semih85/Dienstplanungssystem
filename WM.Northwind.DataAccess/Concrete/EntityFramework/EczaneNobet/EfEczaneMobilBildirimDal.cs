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
    public class EfEczaneMobilBildirimDal : EfEntityRepositoryBase<EczaneMobilBildirim, EczaneNobetContext>, IEczaneMobilBildirimDal
    {
        public EczaneMobilBildirimDetay GetDetay(Expression<Func<EczaneMobilBildirimDetay, bool>> filter)
        {
            using (var ctx = new EczaneNobetContext())
            {
                return ctx.EczaneMobilBildirimler
                    .Select(s => new EczaneMobilBildirimDetay
                    {
                        EczaneId = s.EczaneId,
                        Id = s.Id,
                        BildirimGormeTarihi = s.BildirimGormeTarihi,
                        EczaneAdi = s.Eczane.Adi,
                        MobilBildirimId = s.MobilBildirimId,
                        MobilBildirimBaslik = s.MobilBildirim.Baslik,
                        MobilBildirimMetin = s.MobilBildirim.Metin
                        
                    }).SingleOrDefault(filter);
            }
        }
        public List<EczaneMobilBildirimDetay> GetDetayList(Expression<Func<EczaneMobilBildirimDetay, bool>> filter = null)
        {
            using (var ctx = new EczaneNobetContext())
            {
                var liste = ctx.EczaneMobilBildirimler
                    .Select(s => new EczaneMobilBildirimDetay
                    {
                        EczaneId = s.EczaneId,
                        Id = s.Id,
                        BildirimGormeTarihi = s.BildirimGormeTarihi,
                        EczaneAdi = s.Eczane.Adi,
                        MobilBildirimId = s.MobilBildirimId,
                        MobilBildirimBaslik = s.MobilBildirim.Baslik,
                        MobilBildirimMetin = s.MobilBildirim.Metin

                    });

                return filter == null
                    ? liste.ToList()
                    : liste.Where(filter).ToList();
            }
        }
    }
}