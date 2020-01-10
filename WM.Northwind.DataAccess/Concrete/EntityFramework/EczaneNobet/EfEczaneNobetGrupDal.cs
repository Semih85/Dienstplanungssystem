using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WM.Core.DAL.EntityFramework;
using WM.Northwind.DataAccess.Abstract.EczaneNobet;
using WM.Northwind.DataAccess.Concrete.EntityFramework.Contexts;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.DataAccess.Concrete.EntityFramework.EczaneNobet
{
    public class EfEczaneNobetGrupDal : EfEntityRepositoryBase<EczaneNobetGrup, EczaneNobetContext>, IEczaneNobetGrupDal
    {
        public EczaneNobetGrupDetay GetDetay(Expression<Func<EczaneNobetGrupDetay, bool>> filter)
        {
            using (var ctx = new EczaneNobetContext())
            {
                return ctx.EczaneNobetGruplar
                    .Select(s => new EczaneNobetGrupDetay
                    {
                        Id = s.Id,
                        EczaneId = s.EczaneId,
                        NobetGrupId = s.NobetGrupGorevTip.NobetGrupId,
                        EczaneAdi = s.Eczane.Adi,
                        NobetGrupAdi = s.NobetGrupGorevTip.NobetGrup.Adi,
                        EczaneNobetGrupAdi = s.Eczane.Adi + ", " + s.NobetGrupGorevTip.NobetGrup.Adi,
                        NobetUstGrupId = s.NobetGrupGorevTip.NobetGrup.NobetUstGrupId,
                        BaslangicTarihi = s.BaslangicTarihi,
                        BitisTarihi = s.BitisTarihi,
                        EczaneKapanmaTarihi = s.Eczane.KapanisTarihi,
                        Aciklama = s.Aciklama,
                        NobetUstGrupAdi = s.NobetGrupGorevTip.NobetGrup.NobetUstGrup.Adi,
                        NobetGorevTipAdi = s.NobetGrupGorevTip.NobetGorevTip.Adi,
                        NobetGorevTipId = s.NobetGrupGorevTip.NobetGorevTipId,
                        NobetGrupGorevTipId = s.NobetGrupGorevTipId,
                        NobetAltGrupAdi = s.EczaneNobetGrupAltGruplar.Where(w => w.BitisTarihi == null).FirstOrDefault() != null
                        ? s.EczaneNobetGrupAltGruplar.Where(w => w.BitisTarihi == null).FirstOrDefault().NobetAltGrup.Adi
                        : "",
                        NobetAltGrupId = s.EczaneNobetGrupAltGruplar.Where(w => w.BitisTarihi == null).FirstOrDefault() != null
                        ? s.EczaneNobetGrupAltGruplar.Where(w => w.BitisTarihi == null).FirstOrDefault().NobetAltGrup.Id
                        : 0,
                        NobetUstGrupBaslamaTarihi = s.NobetGrupGorevTip.NobetGrup.NobetUstGrup.BaslangicTarihi,
                        NobetGrupGorevTipBaslamaTarihi = s.NobetGrupGorevTip.BaslamaTarihi,
                        EnErkenTarihteNobetYazilsinMi = s.EnErkenTarihteNobetYazilsinMi
                    }).SingleOrDefault(filter);
            }
        }

        public List<EczaneNobetGrupDetay> GetDetayList(Expression<Func<EczaneNobetGrupDetay, bool>> filter = null)
        {
            using (var ctx = new EczaneNobetContext())
            {
                var liste = ctx.EczaneNobetGruplar
                    .Select(s => new EczaneNobetGrupDetay
                    {
                        Id = s.Id,
                        EczaneId = s.EczaneId,
                        NobetGrupId = s.NobetGrupGorevTip.NobetGrupId,
                        EczaneAdi = s.Eczane.Adi,
                        NobetGrupAdi = s.NobetGrupGorevTip.NobetGrup.Adi,
                        EczaneNobetGrupAdi = s.Eczane.Adi + ", " + s.NobetGrupGorevTip.NobetGrup.Adi,
                        NobetUstGrupId = s.NobetGrupGorevTip.NobetGrup.NobetUstGrupId,
                        BaslangicTarihi = s.BaslangicTarihi,
                        BitisTarihi = s.BitisTarihi,
                        EczaneKapanmaTarihi = s.Eczane.KapanisTarihi,
                        Aciklama = s.Aciklama,
                        NobetUstGrupAdi = s.NobetGrupGorevTip.NobetGrup.NobetUstGrup.Adi,
                        NobetGorevTipAdi = s.NobetGrupGorevTip.NobetGorevTip.Adi,
                        NobetGorevTipId = s.NobetGrupGorevTip.NobetGorevTipId,
                        NobetGrupGorevTipId = s.NobetGrupGorevTipId,

                        NobetAltGrupAdi = s.EczaneNobetGrupAltGruplar.Where(w => w.BitisTarihi == null).FirstOrDefault() != null
                        ? s.EczaneNobetGrupAltGruplar.Where(w => w.BitisTarihi == null).FirstOrDefault().NobetAltGrup.Adi
                        : "",
                        NobetAltGrupId = s.EczaneNobetGrupAltGruplar.Where(w => w.BitisTarihi == null).FirstOrDefault() != null
                        ? s.EczaneNobetGrupAltGruplar.Where(w => w.BitisTarihi == null).FirstOrDefault().NobetAltGrup.Id
                        : 0,
                        NobetUstGrupBaslamaTarihi = s.NobetGrupGorevTip.NobetGrup.NobetUstGrup.BaslangicTarihi,
                        NobetGrupGorevTipBaslamaTarihi = s.NobetGrupGorevTip.BaslamaTarihi,
                        EnErkenTarihteNobetYazilsinMi = s.EnErkenTarihteNobetYazilsinMi
                    });

                return filter == null
                       ? liste.ToList()
                       : liste.Where(filter).ToList();
            }
        }

        public virtual void CokluEkle(List<EczaneNobetGrup> eczaneNobetGruplar)
        {
            using (var context = new EczaneNobetContext())
            {
                foreach (var eczaneNobetGrup in eczaneNobetGruplar)
                {
                    context.EczaneNobetGruplar.Add(eczaneNobetGrup);
                }
                context.SaveChanges();
            }
        }
    }

}
