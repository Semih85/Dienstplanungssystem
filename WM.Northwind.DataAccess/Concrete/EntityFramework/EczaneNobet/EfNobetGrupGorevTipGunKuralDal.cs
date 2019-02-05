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
    public class EfNobetGrupGorevTipGunKuralDal : EfEntityRepositoryBase<NobetGrupGorevTipGunKural, EczaneNobetContext>, INobetGrupGorevTipGunKuralDal
    {
        public NobetGrupGorevTipGunKuralDetay GetDetay(Expression<Func<NobetGrupGorevTipGunKuralDetay, bool>> filter)
        {
            using (var ctx = new EczaneNobetContext())
            {
                return ctx.NobetGrupGorevTipGunKurallar
                    .Select(s => new NobetGrupGorevTipGunKuralDetay
                    {
                        Id = s.Id,
                        NobetGrupGorevTipId = s.NobetGrupGorevTipId,
                        NobetGorevTipAdi = s.NobetGrupGorevTip.NobetGorevTip.Adi,
                        NobetGorevTipId = s.NobetGrupGorevTip.NobetGorevTipId,
                        NobetGrupAdi = s.NobetGrupGorevTip.NobetGrup.Adi,
                        NobetGrupId = s.NobetGrupGorevTip.NobetGrupId,
                        NobetGunKuralId = s.NobetGunKuralId,
                        NobetGunKuralAdi = s.NobetGunKural.Adi,
                        GunGrupId = s.NobetUstGrupGunGrup.GunGrup.Id,
                        GunGrupAdi = s.NobetUstGrupGunGrup.GunGrup.Adi,
                        NobetUstGrupGunGrupId = s.NobetUstGrupGunGrupId,
                        NobetUstGrupId = s.NobetGrupGorevTip.NobetGrup.NobetUstGrupId,
                        BaslangicTarihi = s.BaslangicTarihi,
                        BitisTarihi = s.BitisTarihi,
                        NobetciSayisi = s.NobetciSayisi
                    }).SingleOrDefault(filter);
            }
        }
        public List<NobetGrupGorevTipGunKuralDetay> GetDetayList(Expression<Func<NobetGrupGorevTipGunKuralDetay, bool>> filter = null)
        {
            using (var ctx = new EczaneNobetContext())
            {
                var liste = ctx.NobetGrupGorevTipGunKurallar
                    .Select(s => new NobetGrupGorevTipGunKuralDetay
                    {
                        Id = s.Id,
                        NobetGrupGorevTipId = s.NobetGrupGorevTipId,
                        NobetGorevTipAdi = s.NobetGrupGorevTip.NobetGorevTip.Adi,
                        NobetGorevTipId = s.NobetGrupGorevTip.NobetGorevTipId,
                        NobetGrupAdi = s.NobetGrupGorevTip.NobetGrup.Adi,
                        NobetGrupId = s.NobetGrupGorevTip.NobetGrupId,
                        NobetGunKuralId = s.NobetGunKuralId,
                        NobetGunKuralAdi = s.NobetGunKural.Adi,
                        GunGrupId = s.NobetUstGrupGunGrup.GunGrup.Id,
                        GunGrupAdi = s.NobetUstGrupGunGrup.GunGrup.Adi,
                        NobetUstGrupGunGrupId = s.NobetUstGrupGunGrupId,
                        NobetUstGrupId = s.NobetGrupGorevTip.NobetGrup.NobetUstGrupId,
                        BaslangicTarihi = s.BaslangicTarihi,
                        BitisTarihi = s.BitisTarihi,
                        NobetciSayisi = s.NobetciSayisi
                    });

                return filter == null
                    ? liste.ToList()
                    : liste.Where(filter).ToList();
            }
        }
    }
}