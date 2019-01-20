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
    public class EfNobetDurumDal : EfEntityRepositoryBase<NobetDurum, EczaneNobetContext>, INobetDurumDal
    {
        public NobetDurumDetay GetDetay(Expression<Func<NobetDurumDetay, bool>> filter)
        {
            using (var ctx = new EczaneNobetContext())
            {
                return ctx.NobetDurumlar
                    .Select(s => new NobetDurumDetay
                    {
                        Id = s.Id,
                        NobetAltGrupId1 = s.NobetAltGrupId1,
                        NobetAltGrupId2 = s.NobetAltGrupId2,
                        NobetAltGrupId3 = s.NobetAltGrupId3,
                        NobetAltGrupAdi1 = s.NobetAltGrupl.Adi,
                        NobetAltGrupAdi2 = s.NobetAltGrup2.Adi,
                        NobetAltGrupAdi3 = s.NobetAltGrup3.Adi,
                        NobetDurumTipId = s.NobetDurumTipId,
                        NobetDurumTipAdi = s.NobetDurumTip.Adi,
                        NobetUstGrupId = s.NobetAltGrupl.NobetGrupGorevTip.NobetGrup.NobetUstGrupId
                    }).SingleOrDefault(filter);
            }
        }
        public List<NobetDurumDetay> GetDetayList(Expression<Func<NobetDurumDetay, bool>> filter = null)
        {
            using (var ctx = new EczaneNobetContext())
            {
                var liste = ctx.NobetDurumlar
                    .Select(s => new NobetDurumDetay
                    {
                        Id = s.Id,
                        NobetAltGrupId1 = s.NobetAltGrupId1,
                        NobetAltGrupId2 = s.NobetAltGrupId2,
                        NobetAltGrupId3 = s.NobetAltGrupId3,
                        NobetAltGrupAdi1 = s.NobetAltGrupl.Adi,
                        NobetAltGrupAdi2 = s.NobetAltGrup2.Adi,
                        NobetAltGrupAdi3 = s.NobetAltGrup3.Adi,
                        NobetDurumTipId = s.NobetDurumTipId,
                        NobetDurumTipAdi = s.NobetDurumTip.Adi,
                        NobetUstGrupId = s.NobetAltGrupl.NobetGrupGorevTip.NobetGrup.NobetUstGrupId
                    });

                return filter == null
                    ? liste.ToList()
                    : liste.Where(filter).ToList();
            }
        }

    }
}