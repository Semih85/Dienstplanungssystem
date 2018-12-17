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
    public class EfBayramDal : EfEntityRepositoryBase<Bayram, EczaneNobetContext>, IBayramDal
    {
        public BayramDetay GetDetay(Expression<Func<BayramDetay, bool>> filter)
        {
            using (var ctx = new EczaneNobetContext())
            {
                return ctx.Bayramlar
                    .Select(s => new BayramDetay
                    {
                        Id = s.Id,
                        TakvimId = s.TakvimId,
                        NobetGrupGorevTipId = s.NobetGrupGorevTipId,
                        NobetGrupGorevTipAdi = s.NobetGrupGorevTip.NobetGorevTip.Adi,
                        NobetGunKuralAdi = s.NobetGunKural.Adi,
                        NobetGunKuralId = s.NobetGunKuralId,
                        Tarih = s.Takvim.Tarih,
                        BayramTurId = s.BayramTurId,
                        NobetGorevTipId = s.NobetGrupGorevTip.NobetGorevTipId,
                        NobetGrupAdi = s.NobetGrupGorevTip.NobetGrup.Adi,
                        NobetGrupId = s.NobetGrupGorevTip.NobetGrupId,
                        BayramTurAdi = s.BayramTur.Adi,
                        NobetUstGrupId = s.NobetGrupGorevTip.NobetGrup.NobetUstGrupId
                    }).SingleOrDefault(filter);
            }
        }
        public List<BayramDetay> GetDetayList(Expression<Func<BayramDetay, bool>> filter = null)
        {
            using (var ctx = new EczaneNobetContext())
            {
                var liste = ctx.Bayramlar
                    .Select(s => new BayramDetay
                    {
                        Id = s.Id,
                        TakvimId = s.TakvimId,
                        NobetGrupGorevTipId = s.NobetGrupGorevTipId,
                        NobetGrupGorevTipAdi = s.NobetGrupGorevTip.NobetGorevTip.Adi,
                        NobetGunKuralAdi = s.NobetGunKural.Adi,
                        NobetGunKuralId = s.NobetGunKuralId,
                        Tarih = s.Takvim.Tarih,
                        BayramTurId = s.BayramTurId,
                        NobetGorevTipId = s.NobetGrupGorevTip.NobetGorevTipId,
                        NobetGrupAdi = s.NobetGrupGorevTip.NobetGrup.Adi,
                        NobetGrupId = s.NobetGrupGorevTip.NobetGrupId,
                        BayramTurAdi = s.BayramTur.Adi,
                        NobetUstGrupId = s.NobetGrupGorevTip.NobetGrup.NobetUstGrupId
                    });

                return filter == null
                    ? liste.ToList()
                    : liste.Where(filter).ToList();
            }
        }
        public virtual void CokluEkle(List<Bayram> bayramlar)
        {
            using (var context = new EczaneNobetContext())
            {
                foreach (var bayram in bayramlar)
                {
                    context.Bayramlar.Add(bayram);
                }
                context.SaveChanges();
            }
        }

        public virtual void CokluSil(int[] ids)
        {
            using (var context = new EczaneNobetContext())
            {
                var deletedEntity = context.Bayramlar.RemoveRange(context.Bayramlar.Where(w => ids.Contains(w.Id)));

                context.SaveChanges();
            }
        }
    }
}