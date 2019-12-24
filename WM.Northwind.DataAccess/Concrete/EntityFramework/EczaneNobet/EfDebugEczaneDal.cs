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
    public class EfDebugEczaneDal : EfEntityRepositoryBase<DebugEczane, EczaneNobetContext>, IDebugEczaneDal
    {
        public DebugEczaneDetay GetDetay(Expression<Func<DebugEczaneDetay, bool>> filter)
        {
            using (var ctx = new EczaneNobetContext())
            {
                return ctx.DebugEczaneler
                    .Select(s => new DebugEczaneDetay
                    {
                        EczaneId = s.EczaneNobetGrup.EczaneId,
                        EczaneAdi = s.EczaneNobetGrup.Eczane.Adi,
                        AktifMi = s.AktifMi,
                        EczaneNobetGrupId = s.EczaneNobetGrupId,
                        Id = s.Id,
                        NobetGorevTipAdi = s.EczaneNobetGrup.NobetGrupGorevTip.NobetGorevTip.Adi,
                        NobetGroevTipId = s.EczaneNobetGrup.NobetGrupGorevTipId,
                        NobetGrupAdi = s.EczaneNobetGrup.NobetGrupGorevTip.NobetGrup.Adi,
                        NobetUstGrupId = s.EczaneNobetGrup.NobetGrupGorevTip.NobetGrup.NobetUstGrupId
                    }).SingleOrDefault(filter);
            }
        }
        public List<DebugEczaneDetay> GetDetayList(Expression<Func<DebugEczaneDetay, bool>> filter = null)
        {
            using (var ctx = new EczaneNobetContext())
            {
                var liste = ctx.DebugEczaneler
                    .Select(s => new DebugEczaneDetay
                    {
                        EczaneId = s.EczaneNobetGrup.EczaneId,
                        EczaneAdi = s.EczaneNobetGrup.Eczane.Adi,
                        AktifMi = s.AktifMi,
                        EczaneNobetGrupId = s.EczaneNobetGrupId,
                        Id = s.Id,
                        NobetGorevTipAdi = s.EczaneNobetGrup.NobetGrupGorevTip.NobetGorevTip.Adi,
                        NobetGroevTipId = s.EczaneNobetGrup.NobetGrupGorevTipId,
                        NobetGrupAdi = s.EczaneNobetGrup.NobetGrupGorevTip.NobetGrup.Adi,
                        NobetUstGrupId = s.EczaneNobetGrup.NobetGrupGorevTip.NobetGrup.NobetUstGrupId
                    });

                return filter == null
                    ? liste.ToList()
                    : liste.Where(filter).ToList();
            }
        }
    }
}