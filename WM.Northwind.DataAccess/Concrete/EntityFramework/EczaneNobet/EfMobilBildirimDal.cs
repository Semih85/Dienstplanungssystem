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
    public class EfMobilBildirimDal : EfEntityRepositoryBase<MobilBildirim, EczaneNobetContext>, IMobilBildirimDal
    {
        public MobilBildirimDetay GetDetay(Expression<Func<MobilBildirimDetay, bool>> filter)
        {
            using (var ctx = new EczaneNobetContext())
            {
                return ctx.MobilBildirimler
                    .Select(s => new MobilBildirimDetay
                    {
                        NobetUstGrupId = s.NobetUstGrupId,
                        Id = s.Id,
                        Aciklama = s.Aciklama,
                        Baslik= s.Baslik,
                        GonderimTarihi = s.GonderimTarihi,
                        Metin = s.Metin,
                        NobetUstGrupAdi = s.NobetUstGrup.Adi
                        
                    }).SingleOrDefault(filter);
            }
        }
        public List<MobilBildirimDetay> GetDetayList(Expression<Func<MobilBildirimDetay, bool>> filter = null)
        {
            using (var ctx = new EczaneNobetContext())
            {
                var liste = ctx.MobilBildirimler
                    .Select(s => new MobilBildirimDetay
                    {
                        NobetUstGrupId = s.NobetUstGrupId,
                        Id = s.Id,
                        Aciklama = s.Aciklama,
                        Baslik = s.Baslik,
                        GonderimTarihi = s.GonderimTarihi,
                        Metin = s.Metin,
                        NobetUstGrupAdi = s.NobetUstGrup.Adi

                    });

                return filter == null
                    ? liste.ToList()
                    : liste.Where(filter).ToList();
            }
        }
    }
}