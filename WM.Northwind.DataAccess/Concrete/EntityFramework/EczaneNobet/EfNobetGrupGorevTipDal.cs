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
    public class EfNobetGrupGorevTipDal : EfEntityRepositoryBase<NobetGrupGorevTip, EczaneNobetContext>, INobetGrupGorevTipDal
    {
        public NobetGrupGorevTipDetay GetDetay(Expression<Func<NobetGrupGorevTipDetay, bool>> filter)
        {
            using (var ctx = new EczaneNobetContext())
            {
                return ctx.NobetGrupGorevTipler
                    .Select(s => new NobetGrupGorevTipDetay
                    {
                        NobetGrupId = s.NobetGrupId,
                        NobetGorevTipAdi = s.NobetGorevTip.Adi,
                        NobetGorevTipId = s.NobetGorevTipId,
                        NobetGrupAdi = s.NobetGrup.Adi,
                        NobetUstGrupId = s.NobetGrup.NobetUstGrupId,
                        NobetUstGrupAdi = s.NobetGrup.NobetUstGrup.Adi,
                        Id = s.Id,
                        BaslamaTarihi = s.BaslamaTarihi,
                        BitisTarihi = s.BitisTarihi
                    }).SingleOrDefault(filter);
            }
        }
        public List<NobetGrupGorevTipDetay> GetDetayList(Expression<Func<NobetGrupGorevTipDetay, bool>> filter = null)
        {
            using (var ctx = new EczaneNobetContext())
            {
                var liste = ctx.NobetGrupGorevTipler
                    .Select(s => new NobetGrupGorevTipDetay
                    {
                        NobetGrupId = s.NobetGrupId,
                        NobetGorevTipAdi = s.NobetGorevTip.Adi,
                        NobetGorevTipId = s.NobetGorevTipId,
                        NobetGrupAdi = s.NobetGrup.Adi,
                        NobetUstGrupId = s.NobetGrup.NobetUstGrupId,
                        NobetUstGrupAdi = s.NobetGrup.NobetUstGrup.Adi,
                        Id = s.Id,
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