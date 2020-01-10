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
    public class EfEczaneNobetGrupAltGrupDal : EfEntityRepositoryBase<EczaneNobetGrupAltGrup, EczaneNobetContext>, IEczaneNobetGrupAltGrupDal
    {
        public void CokluEkle(List<EczaneNobetGrupAltGrup> eczaneNobetGrupAltGruplar)
        {
            using (var context = new EczaneNobetContext())
            {
                foreach (var eczaneNobetGrupAltGrup in eczaneNobetGrupAltGruplar)
                {
                    context.EczaneNobetGrupAltGruplar.Add(eczaneNobetGrupAltGrup);
                }
                context.SaveChanges();
            }
        }

        public EczaneNobetGrupAltGrupDetay GetDetay(Expression<Func<EczaneNobetGrupAltGrupDetay, bool>> filter)
        {
            using (var ctx = new EczaneNobetContext())
            {
                return ctx.EczaneNobetGrupAltGruplar
                    .Select(s => new EczaneNobetGrupAltGrupDetay
                    {
                        Id = s.Id,
                        EczaneNobetGrupId = s.EczaneNobetGrupId,
                        NobetAltGrupId = s.NobetAltGrupId,
                        NobetAltGrupAdi = s.NobetAltGrup.Adi,
                        EczaneAdi = s.EczaneNobetGrup.Eczane.Adi,
                        NobetGrupAdi = s.EczaneNobetGrup.NobetGrupGorevTip.NobetGrup.Adi,
                        EczaneId = s.EczaneNobetGrup.EczaneId,
                        NobetGrupId = s.EczaneNobetGrup.NobetGrupGorevTip.NobetGrupId,
                        NobetUstGrupId = s.EczaneNobetGrup.NobetGrupGorevTip.NobetGrup.NobetUstGrupId,
                        NobetGorevTipAdi = s.EczaneNobetGrup.NobetGrupGorevTip.NobetGorevTip.Adi,
                        NobetGorevTipId = s.EczaneNobetGrup.NobetGrupGorevTip.NobetGorevTipId,
                        NobetGrupGorevTipId = s.EczaneNobetGrup.NobetGrupGorevTipId,
                        BaslangicTarihi = s.BaslangicTarihi,
                        BitisTarihi = s.BitisTarihi,
                        Aciklama = s.Aciklama
                    }).SingleOrDefault(filter);
            }
        }
        public List<EczaneNobetGrupAltGrupDetay> GetDetayList(Expression<Func<EczaneNobetGrupAltGrupDetay, bool>> filter = null)
        {
            using (var ctx = new EczaneNobetContext())
            {
                var liste = ctx.EczaneNobetGrupAltGruplar
                    .Select(s => new EczaneNobetGrupAltGrupDetay
                    {
                        Id = s.Id,
                        EczaneNobetGrupId = s.EczaneNobetGrupId,
                        NobetAltGrupId = s.NobetAltGrupId,
                        NobetAltGrupAdi = s.NobetAltGrup.Adi,
                        EczaneAdi = s.EczaneNobetGrup.Eczane.Adi,
                        NobetGrupAdi = s.EczaneNobetGrup.NobetGrupGorevTip.NobetGrup.Adi,
                        EczaneId = s.EczaneNobetGrup.EczaneId,
                        NobetGrupId = s.EczaneNobetGrup.NobetGrupGorevTip.NobetGrupId,
                        NobetUstGrupId = s.EczaneNobetGrup.NobetGrupGorevTip.NobetGrup.NobetUstGrupId,
                        NobetGorevTipAdi = s.EczaneNobetGrup.NobetGrupGorevTip.NobetGorevTip.Adi,
                        NobetGorevTipId = s.EczaneNobetGrup.NobetGrupGorevTip.NobetGorevTipId,
                        NobetGrupGorevTipId = s.EczaneNobetGrup.NobetGrupGorevTipId,
                        BaslangicTarihi = s.BaslangicTarihi,
                        BitisTarihi = s.BitisTarihi,
                        Aciklama = s.Aciklama
                    });

                return filter == null
                    ? liste.ToList()
                    : liste.Where(filter).ToList();
            }
        }
    }
}