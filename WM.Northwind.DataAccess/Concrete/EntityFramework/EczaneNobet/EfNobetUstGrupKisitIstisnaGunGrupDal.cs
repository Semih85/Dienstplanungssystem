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
    public class EfNobetUstGrupKisitIstisnaGunGrupDal : EfEntityRepositoryBase<NobetUstGrupKisitIstisnaGunGrup, EczaneNobetContext>, INobetUstGrupKisitIstisnaGunGrupDal
    {
        public NobetUstGrupKisitIstisnaGunGrupDetay GetDetay(Expression<Func<NobetUstGrupKisitIstisnaGunGrupDetay, bool>> filter)
        {
            using (var ctx = new EczaneNobetContext())
            {
                return ctx.NobetUstGrupKisitIstisnaGunGruplar
                    .Select(s => new NobetUstGrupKisitIstisnaGunGrupDetay
                    {
                        Aciklama = s.Aciklama,
                        BaslangicTarihi = s.BaslangicTarihi,
                        BitisTarihi = s.BitisTarihi,
                        GunGrupId = s.NobetUstGrupGunGrup.GunGrupId,
                        KisitId = s.NobetUstGrupKisit.KisitId,
                        Id = s.Id,
                        KisitAdi = s.NobetUstGrupKisit.Kisit.Adi,
                        GunGrupAdi = s.NobetUstGrupGunGrup.GunGrup.Adi,
                        NobetUstGrupKisitId = s.NobetUstGrupKisitId,
                        NobetUstGrupGunGrupAdi = s.NobetUstGrupGunGrup.GunGrup.Adi,
                        NobetUstGrupGunGrupId = s.NobetUstGrupGunGrupId,
                        NobetUstGrupKisitAdi = s.NobetUstGrupKisit.Kisit.Adi,
                        NobetUstGrupAdi = s.NobetUstGrupGunGrup.NobetUstGrup.Adi,
                        NobetUstGrupId = s.NobetUstGrupGunGrup.NobetUstGrupId
                    }).SingleOrDefault(filter);
            }
        }
        public List<NobetUstGrupKisitIstisnaGunGrupDetay> GetDetayList(Expression<Func<NobetUstGrupKisitIstisnaGunGrupDetay, bool>> filter = null)
        {
            using (var ctx = new EczaneNobetContext())
            {
                var liste = ctx.NobetUstGrupKisitIstisnaGunGruplar
                    .Select(s => new NobetUstGrupKisitIstisnaGunGrupDetay
                    {
                        Aciklama = s.Aciklama,
                        BaslangicTarihi = s.BaslangicTarihi,
                        BitisTarihi = s.BitisTarihi,
                        GunGrupId = s.NobetUstGrupGunGrup.GunGrupId,
                        KisitId = s.NobetUstGrupKisit.KisitId,
                        Id = s.Id,
                        KisitAdi = s.NobetUstGrupKisit.Kisit.Adi,
                        GunGrupAdi = s.NobetUstGrupGunGrup.GunGrup.Adi,
                        NobetUstGrupKisitId = s.NobetUstGrupKisitId,
                        NobetUstGrupGunGrupAdi = s.NobetUstGrupGunGrup.GunGrup.Adi,
                        NobetUstGrupGunGrupId = s.NobetUstGrupGunGrupId,
                        NobetUstGrupKisitAdi = s.NobetUstGrupKisit.Kisit.Adi,
                        NobetUstGrupAdi = s.NobetUstGrupGunGrup.NobetUstGrup.Adi,
                        NobetUstGrupId = s.NobetUstGrupGunGrup.NobetUstGrupId
                    });

                return filter == null
                    ? liste.ToList()
                    : liste.Where(filter).ToList();
            }
        }
    }
}