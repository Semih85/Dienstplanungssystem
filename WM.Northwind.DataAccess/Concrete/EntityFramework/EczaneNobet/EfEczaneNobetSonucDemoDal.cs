using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Linq.Expressions;
using WM.Core.DAL.EntityFramework;
using WM.Northwind.DataAccess.Abstract.EczaneNobet;
using WM.Northwind.DataAccess.Concrete.EntityFramework.Contexts;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.DataAccess.Concrete.EntityFramework.EczaneNobet
{
    public class EfEczaneNobetSonucDemoDal : EfEntityRepositoryBase<EczaneNobetSonucDemo, EczaneNobetContext>, IEczaneNobetSonucDemoDal
    {
        public EczaneNobetSonucDemoDetay2 GetDetay(Expression<Func<EczaneNobetSonucDemoDetay2, bool>> filter)
        {
            using (var ctx = new EczaneNobetContext())
            {
                return ctx.EczaneNobetSonucDemolar
                    .Select(s => new EczaneNobetSonucDemoDetay2
                    {
                        Id = s.Id,
                        EczaneAdi = s.EczaneNobetGrup.Eczane.Adi,
                        EczaneId = s.EczaneNobetGrup.EczaneId,
                        EczaneNobetGrupBaslamaTarihi = s.EczaneNobetGrup.BaslangicTarihi,
                        EczaneNobetGrupId = s.EczaneNobetGrupId,
                        NobetGorevTipAdi = s.NobetGorevTip.Adi,
                        NobetGorevTipId = s.NobetGorevTipId,
                        NobetGrupAdi = s.EczaneNobetGrup.NobetGrupGorevTip.NobetGrup.Adi,
                        NobetGrupId = s.EczaneNobetGrup.NobetGrupGorevTip.NobetGrupId,
                        NobetUstGrupId = s.EczaneNobetGrup.NobetGrupGorevTip.NobetGrup.NobetUstGrupId,
                        TakvimId = s.TakvimId,
                        Tarih = s.Takvim.Tarih,
                        NobetSonucDemoTipId = s.NobetSonucDemoTipId,
                        NobetSonucDemoTipAdi = s.NobetSonucDemoTip.Adi,
                        NobetUstGrupBaslamaTarihi = s.EczaneNobetGrup.NobetGrupGorevTip.NobetGrup.NobetUstGrup.BaslangicTarihi
                    }).SingleOrDefault(filter);
            }
        }

        public List<EczaneNobetSonucDemoDetay2> GetDetayList(Expression<Func<EczaneNobetSonucDemoDetay2, bool>> filter = null)
        {
            using (var ctx = new EczaneNobetContext())
            {
                var liste = ctx.EczaneNobetSonucDemolar
                    .Select(s => new EczaneNobetSonucDemoDetay2
                    {
                        Id = s.Id,
                        EczaneAdi = s.EczaneNobetGrup.Eczane.Adi,
                        EczaneId = s.EczaneNobetGrup.EczaneId,
                        EczaneNobetGrupBaslamaTarihi = s.EczaneNobetGrup.BaslangicTarihi,
                        EczaneNobetGrupId = s.EczaneNobetGrupId,
                        NobetGorevTipAdi = s.NobetGorevTip.Adi,
                        NobetGorevTipId = s.NobetGorevTipId,
                        NobetGrupAdi = s.EczaneNobetGrup.NobetGrupGorevTip.NobetGrup.Adi,
                        NobetGrupId = s.EczaneNobetGrup.NobetGrupGorevTip.NobetGrupId,
                        NobetUstGrupId = s.EczaneNobetGrup.NobetGrupGorevTip.NobetGrup.NobetUstGrupId,
                        TakvimId = s.TakvimId,
                        Tarih = s.Takvim.Tarih,
                        NobetSonucDemoTipId = s.NobetSonucDemoTipId,
                        NobetSonucDemoTipAdi = s.NobetSonucDemoTip.Adi,
                        NobetUstGrupBaslamaTarihi = s.EczaneNobetGrup.NobetGrupGorevTip.NobetGrup.NobetUstGrup.BaslangicTarihi
                    });

                return filter == null
                   ? liste.ToList()
                   : liste.Where(filter).ToList();
            }
        }
    }
}
