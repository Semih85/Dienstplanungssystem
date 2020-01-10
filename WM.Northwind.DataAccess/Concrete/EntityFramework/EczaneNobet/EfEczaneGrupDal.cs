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
    public class EfEczaneGrupDal : EfEntityRepositoryBase<EczaneGrup, EczaneNobetContext>, IEczaneGrupDal
    {
        public List<EczaneGrupDetay> GetEczaneGrupDetaylar(Expression<Func<EczaneGrupDetay, bool>> filter = null)
        {
            using (var ctx = new EczaneNobetContext())
            {
                var liste = (from t in ctx.EczaneGruplar
                             from g in ctx.EczaneNobetGruplar
                             where t.EczaneId == g.EczaneId
                                && t.EczaneGrupTanim.NobetGorevTipId == g.NobetGrupGorevTip.NobetGorevTipId
                                && g.BitisTarihi == null
                             select new EczaneGrupDetay
                             {
                                 Id = t.Id,
                                 //EczaneNobetGruplar = t.Eczane.EczaneNobetGruplar,
                                 EczaneAdi = t.Eczane.Adi,
                                 EczaneGrupTanimAdi = t.EczaneGrupTanim.Adi,
                                 EczaneNobetGrupId = g.Id,
                                 NobetGrupAdi = g.NobetGrupGorevTip.NobetGrup.Adi,
                                 NobetGrupId = g.NobetGrupGorevTip.NobetGrupId,
                                 EczaneGrupTanimId = t.EczaneGrupTanimId,
                                 EczaneId = t.EczaneId,
                                 NobetUstGrupId = t.EczaneGrupTanim.NobetUstGrupId,
                                 EczaneGrupTanimBitisTarihi = t.EczaneGrupTanim.BitisTarihi,
                                 EczaneGrupTanimTipId = t.EczaneGrupTanim.EczaneGrupTanimTipId,
                                 EczaneGrupTanimTipAdi = t.EczaneGrupTanim.EczaneGrupTanimTip.Adi,
                                 PasifMi = t.PasifMi,
                                 EczaneGrupTanimPasifMi = t.EczaneGrupTanim.PasifMi,
                                 ArdisikNobetSayisi = t.EczaneGrupTanim.ArdisikNobetSayisi,
                                 NobetGorevTipId = t.EczaneGrupTanim.NobetGorevTipId,
                                 NobetGorevTipAdi = t.EczaneGrupTanim.NobetGorevTip.Adi,
                                 AyniGunNobetTutabilecekEczaneSayisi = t.EczaneGrupTanim.AyniGunNobetTutabilecekEczaneSayisi,
                                 //EczaneGrupBitisTarihi = g.BitisTarihi
                             });

                return filter == null
                               ? liste.ToList()
                               : liste.Where(filter).ToList();
            }
        }

        public virtual void CokluEkle(List<EczaneGrup> eczaneGruplar)
        {
            using (var context = new EczaneNobetContext())
            {
                foreach (var eczaneGrup in eczaneGruplar)
                {
                    context.EczaneGruplar.Add(eczaneGrup);
                }
                context.SaveChanges();
            }
        }
    }
}
