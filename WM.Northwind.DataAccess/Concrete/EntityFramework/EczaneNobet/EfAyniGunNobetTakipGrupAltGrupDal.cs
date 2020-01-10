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
    public class EfAyniGunNobetTakipGrupAltGrupDal : EfEntityRepositoryBase<AyniGunNobetTakipGrupAltGrup, EczaneNobetContext>, IAyniGunNobetTakipGrupAltGrupDal
    {
        public AyniGunNobetTakipGrupAltGrupDetay GetDetay(Expression<Func<AyniGunNobetTakipGrupAltGrupDetay, bool>> filter)
        {
            using (var ctx = new EczaneNobetContext())
            {
                return ctx.AyniGunNobetTakipGrupAltGruplar
                    .Select(s => new AyniGunNobetTakipGrupAltGrupDetay
                    {
                        NobetGrupGorevTipId = s.NobetGrupGorevTipId,
                        NobetAltGrupAdi = s.NobetAltGrup.Adi,
                        NobetAltGrupId = s.NobetAltGrupId,
                        NobetGrupAdi = s.NobetGrupGorevTip.NobetGrup.Adi,
                        NobetGorevTipAdi = s.NobetGrupGorevTip.NobetGorevTip.Adi,
                        BaslamaTarihi = s.BaslamaTarihi,
                        BitisTarihi = s.BitisTarihi,
                        Id = s.Id,
                        NobetUstGrupId = s.NobetGrupGorevTip.NobetGrup.NobetUstGrupId,
                        KumulatifToplamNobetSayisi = s.KumulatifToplamNobetSayisi,
                        NobetGrupAdiAltGruplu = s.NobetAltGrup.NobetGrupGorevTip.NobetGrup.Adi,
                        NobetGrupGorevTipIcinTanimliAltGrupSayisi = s.NobetGrupGorevTip.NobetAltGruplar.Select(g => g.NobetGrupGorevTip.NobetGorevTipId).Distinct().Count()
                    }).SingleOrDefault(filter);
            }
        }
        public List<AyniGunNobetTakipGrupAltGrupDetay> GetDetayList(Expression<Func<AyniGunNobetTakipGrupAltGrupDetay, bool>> filter = null)
        {
            using (var ctx = new EczaneNobetContext())
            {
                var liste = ctx.AyniGunNobetTakipGrupAltGruplar
                    .Select(s => new AyniGunNobetTakipGrupAltGrupDetay
                    {
                        NobetGrupGorevTipId = s.NobetGrupGorevTipId,
                        NobetAltGrupAdi = s.NobetAltGrup.Adi,
                        NobetAltGrupId = s.NobetAltGrupId,
                        NobetGrupAdi = s.NobetGrupGorevTip.NobetGrup.Adi,
                        NobetGorevTipAdi = s.NobetGrupGorevTip.NobetGorevTip.Adi,
                        BaslamaTarihi = s.BaslamaTarihi,
                        BitisTarihi = s.BitisTarihi,
                        Id = s.Id,
                        NobetUstGrupId = s.NobetGrupGorevTip.NobetGrup.NobetUstGrupId,
                        KumulatifToplamNobetSayisi = s.KumulatifToplamNobetSayisi,
                        NobetGrupAdiAltGruplu = s.NobetAltGrup.NobetGrupGorevTip.NobetGrup.Adi,
                        //NobetGrupGorevTipIcinTanimliAltGrupSayisi = s.NobetGrupGorevTip.NobetAltGruplar.Count
                        NobetGrupGorevTipIcinTanimliAltGrupSayisi = s.NobetGrupGorevTip.NobetAltGruplar.Select(g => g.NobetGrupGorevTip.NobetGorevTipId).Distinct().Count()
                    });

                return filter == null
                    ? liste.ToList()
                    : liste.Where(filter).ToList();
            }
        }

    }
}