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
    public class EfNobetAltGrupDal : EfEntityRepositoryBase<NobetAltGrup, EczaneNobetContext>, INobetAltGrupDal
    {
        public NobetAltGrupDetay GetDetay(Expression<Func<NobetAltGrupDetay, bool>> filter)
        {
            using (var ctx = new EczaneNobetContext())
            {
                return ctx.NobetAltGruplar
                    .Select(s => new NobetAltGrupDetay
                    {
                        NobetGrupId = s.NobetGrupGorevTip.NobetGrupId,
                        NobetGrupAdi = s.NobetGrupGorevTip.NobetGrup.Adi,
                        Adi = s.Adi,
                        BaslamaTarihi = s.BaslamaTarihi,
                        BitisTarihi = s.BitisTarihi,
                        Id = s.Id,
                        NobetUstGrupId = s.NobetGrupGorevTip.NobetGrup.NobetUstGrupId,
                        NobetUstGrupAdi = s.NobetGrupGorevTip.NobetGrup.NobetUstGrup.Adi,
                        NobetGorevTipAdi = s.NobetGrupGorevTip.NobetGorevTip.Adi,
                        NobetGorevTipId = s.NobetGrupGorevTip.NobetGorevTipId,
                        NobetGrupGorevTipId = s.NobetGrupGorevTipId,
                        NobetGrupGorevTipIcinTanimliAltGrupSayisi = s.NobetGrupGorevTip.NobetGorevTip.NobetGrupGorevTipler.Select(g => g.NobetGorevTipId).Distinct().Count()
                    }).SingleOrDefault(filter);
            }
        }
        public List<NobetAltGrupDetay> GetDetayList(Expression<Func<NobetAltGrupDetay, bool>> filter = null)
        {
            using (var ctx = new EczaneNobetContext())
            {
                var liste = ctx.NobetAltGruplar
                    .Select(s => new NobetAltGrupDetay
                    {
                        NobetGrupId = s.NobetGrupGorevTip.NobetGrupId,
                        NobetGrupAdi = s.NobetGrupGorevTip.NobetGrup.Adi,
                        Adi = s.Adi,
                        BaslamaTarihi = s.BaslamaTarihi,
                        BitisTarihi = s.BitisTarihi,
                        Id = s.Id,
                        NobetUstGrupId = s.NobetGrupGorevTip.NobetGrup.NobetUstGrupId,
                        NobetUstGrupAdi = s.NobetGrupGorevTip.NobetGrup.NobetUstGrup.Adi,
                        NobetGorevTipAdi = s.NobetGrupGorevTip.NobetGorevTip.Adi,
                        NobetGorevTipId = s.NobetGrupGorevTip.NobetGorevTipId,
                        NobetGrupGorevTipId = s.NobetGrupGorevTipId,
                        NobetGrupGorevTipIcinTanimliAltGrupSayisi = s.NobetGrupGorevTip.NobetGorevTip.NobetGrupGorevTipler.Select(g => g.NobetGorevTipId).Distinct().Count()
                    });

                return filter == null
                    ? liste.ToList()
                    : liste.Where(filter).ToList();
            }
        }
    }
}