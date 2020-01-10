using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    public class EfEczaneNobetSonucAktifDal : EfEntityRepositoryBase<EczaneNobetSonucAktif, EczaneNobetContext>, IEczaneNobetSonucAktifDal
    {
        public void CokluSil(int[] ids)
        {
            using (var context = new EczaneNobetContext())
            {
                var deletedEntity = context.EczaneNobetSonucAktifler.RemoveRange(context.EczaneNobetSonucAktifler.Where(w => ids.Contains(w.Id)));

                context.SaveChanges();
            }
        }

        public void CokluEkle(List<EczaneNobetCozum> eczaneNobetCozumler)
        {
            using (var context = new EczaneNobetContext())
            {
                foreach (var nobetCozum in eczaneNobetCozumler)
                {
                    var aktifSonuc = new EczaneNobetSonucAktif
                    {
                        TakvimId = nobetCozum.TakvimId,
                        EczaneNobetGrupId = nobetCozum.EczaneNobetGrupId,
                        NobetGorevTipId = nobetCozum.NobetGorevTipId
                    };

                    context.EczaneNobetSonucAktifler.Add(aktifSonuc);
                }
                context.SaveChanges();
            }
        }

        public EczaneNobetSonucDetay2 GetDetay(Expression<Func<EczaneNobetSonucDetay2, bool>> filter)
        {
            using (var ctx = new EczaneNobetContext())
            {
                return ctx.EczaneNobetSonucAktifler
                    .Select(s => new EczaneNobetSonucDetay2
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
                        NobetUstGrupBaslamaTarihi = s.EczaneNobetGrup.NobetGrupGorevTip.NobetGrup.NobetUstGrup.BaslangicTarihi,
                        NobetGrupGorevTipId = s.EczaneNobetGrup.NobetGrupGorevTipId
                    }).SingleOrDefault(filter);
            }
        }

        public List<EczaneNobetSonucDetay2> GetDetayList(Expression<Func<EczaneNobetSonucDetay2, bool>> filter = null)
        {
            using (var ctx = new EczaneNobetContext())
            {
                var liste = ctx.EczaneNobetSonucAktifler
                    .Select(s => new EczaneNobetSonucDetay2
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
                        NobetUstGrupBaslamaTarihi = s.EczaneNobetGrup.NobetGrupGorevTip.NobetGrup.NobetUstGrup.BaslangicTarihi,
                        NobetGrupGorevTipId = s.EczaneNobetGrup.NobetGrupGorevTipId
                    });

                return filter == null
                   ? liste.ToList()
                   : liste.Where(filter).ToList();
            }
        }
    }
}
