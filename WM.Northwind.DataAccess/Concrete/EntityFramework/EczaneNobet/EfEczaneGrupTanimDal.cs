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
    public class EfEczaneGrupTanimDal : EfEntityRepositoryBase<EczaneGrupTanim, EczaneNobetContext>, IEczaneGrupTanimDal
    {
        public EczaneGrupTanimDetay GetDetay(Expression<Func<EczaneGrupTanimDetay, bool>> filter)
        {
            using (var ctx = new EczaneNobetContext())
            {
                return ctx.EczaneGrupTanimlar
                    .Select(s => new EczaneGrupTanimDetay
                    {
                        NobetUstGrupId = s.NobetUstGrupId,
                        NobetUstGrupAdi = s.NobetUstGrup.Adi,
                        ArdisikNobetSayisi = s.ArdisikNobetSayisi,
                        Aciklama = s.Aciklama,
                        Adi = s.Adi,
                        BaslangicTarihi = s.BaslangicTarihi,
                        BitisTarihi = s.BitisTarihi,
                        Id = s.Id,
                        EczaneGrupTanimTipId = s.EczaneGrupTanimTipId,
                        EczaneGrupTanimTipAdi = s.EczaneGrupTanimTip.Adi,
                        PasifMi = s.PasifMi,
                        NobetGorevTipId = s.NobetGorevTipId,
                        NobetGorevTipAdi = s.NobetGorevTip.Adi,
                        AyniGunNobetTutabilecekEczaneSayisi = s.AyniGunNobetTutabilecekEczaneSayisi,
                        GruptakiEczaneSayisi = s.EczaneGruplar.Count
                    }).SingleOrDefault(filter);
            }
        }
        public List<EczaneGrupTanimDetay> GetDetayList(Expression<Func<EczaneGrupTanimDetay, bool>> filter = null)
        {
            using (var ctx = new EczaneNobetContext())
            {
                var liste = ctx.EczaneGrupTanimlar
                    .Select(s => new EczaneGrupTanimDetay
                    {
                        NobetUstGrupId = s.NobetUstGrupId,
                        NobetUstGrupAdi = s.NobetUstGrup.Adi,
                        ArdisikNobetSayisi = s.ArdisikNobetSayisi,
                        Aciklama = s.Aciklama,
                        Adi = s.Adi,
                        BaslangicTarihi = s.BaslangicTarihi,
                        BitisTarihi = s.BitisTarihi,
                        Id = s.Id,
                        EczaneGrupTanimTipId = s.EczaneGrupTanimTipId,
                        EczaneGrupTanimTipAdi = s.EczaneGrupTanimTip.Adi,
                        PasifMi = s.PasifMi,
                        NobetGorevTipId = s.NobetGorevTipId,
                        NobetGorevTipAdi = s.NobetGorevTip.Adi,
                        AyniGunNobetTutabilecekEczaneSayisi = s.AyniGunNobetTutabilecekEczaneSayisi,
                        GruptakiEczaneSayisi = s.EczaneGruplar.Count,
                        //GruptakiEczaneler = s.EczaneGruplar
                    });

                return filter == null
                    ? liste.ToList()
                    : liste.Where(filter).ToList();
            }
        }
    }
}