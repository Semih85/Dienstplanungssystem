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
    public class EfNobetUstGrupGunGrupDal : EfEntityRepositoryBase<NobetUstGrupGunGrup, EczaneNobetContext>, INobetUstGrupGunGrupDal
    {
        public NobetUstGrupGunGrupDetay GetDetay(Expression<Func<NobetUstGrupGunGrupDetay, bool>> filter)
        {
            using (var ctx = new EczaneNobetContext())
            {
                return ctx.NobetUstGrupGunGruplar
                    .Select(s => new NobetUstGrupGunGrupDetay
                    {
                        Id = s.Id,
                        Aciklama = s.Aciklama,
                        GunGrupId = s.GunGrupId,
                        GunGrupAdi = s.GunGrup.Adi,
                        NobetUstGrupId = s.NobetUstGrupId,
                        NobetUstGrupAdi = s.NobetUstGrup.Adi,
                        AmacFonksiyonuKatsayisi = s.AmacFonksiyonuKatsayisi
                    }).SingleOrDefault(filter);
            }
        }
        public List<NobetUstGrupGunGrupDetay> GetDetayList(Expression<Func<NobetUstGrupGunGrupDetay, bool>> filter = null)
        {
            using (var ctx = new EczaneNobetContext())
            {
                var liste = ctx.NobetUstGrupGunGruplar
                    .Select(s => new NobetUstGrupGunGrupDetay
                    {
                        Id = s.Id,
                        Aciklama = s.Aciklama,
                        GunGrupId = s.GunGrupId,
                        GunGrupAdi = s.GunGrup.Adi,
                        NobetUstGrupId = s.NobetUstGrupId,
                        NobetUstGrupAdi = s.NobetUstGrup.Adi,
                        AmacFonksiyonuKatsayisi = s.AmacFonksiyonuKatsayisi
                    });

                return filter == null
                    ? liste.ToList()
                    : liste.Where(filter).ToList();
            }
        }
    }
}