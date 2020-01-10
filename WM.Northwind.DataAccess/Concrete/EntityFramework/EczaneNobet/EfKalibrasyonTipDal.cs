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
    public class EfKalibrasyonTipDal : EfEntityRepositoryBase<KalibrasyonTip, EczaneNobetContext>, IKalibrasyonTipDal
    {
        public KalibrasyonTipDetay GetDetay(Expression<Func<KalibrasyonTipDetay, bool>> filter)
        {
            using (var ctx = new EczaneNobetContext())
            {
                return ctx.KalibrasyonTipler
                    .Select(s => new KalibrasyonTipDetay
                    {
                        NobetUstGrupId = s.NobetUstGrupId,
                        NobetUstGrupAdi = s.NobetUstGrup.Adi,
                        Aciklama = s.Aciklama,
                        Adi = s.Adi,
                        Id = s.Id
                    }).SingleOrDefault(filter);
            }
        }
        public List<KalibrasyonTipDetay> GetDetayList(Expression<Func<KalibrasyonTipDetay, bool>> filter = null)
        {
            using (var ctx = new EczaneNobetContext())
            {
                var liste = ctx.KalibrasyonTipler
                    .Select(s => new KalibrasyonTipDetay
                    {
                        NobetUstGrupId = s.NobetUstGrupId,
                        NobetUstGrupAdi = s.NobetUstGrup.Adi,
                        Aciklama = s.Aciklama,
                        Adi = s.Adi,
                        Id = s.Id
                    });

                return filter == null
                    ? liste.ToList()
                    : liste.Where(filter).ToList();
            }
        }

    }
}