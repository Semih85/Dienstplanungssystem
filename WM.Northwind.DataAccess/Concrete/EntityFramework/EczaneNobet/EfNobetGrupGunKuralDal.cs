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
using System.Data.Entity;

namespace WM.Northwind.DataAccess.Concrete.EntityFramework.EczaneNobet
{
    public class EfNobetGrupGunKuralDal : EfEntityRepositoryBase<NobetGrupGunKural, EczaneNobetContext>, INobetGrupGunKuralDal
    {
        public NobetGrupGunKuralDetay GetDetay(Expression<Func<NobetGrupGunKuralDetay, bool>> filter)
        {
            using (var ctx = new EczaneNobetContext())
            {
                return ctx.NobetGrupGunKurallar
                    .Select(s => new NobetGrupGunKuralDetay
                    {
                        NobetGrupId = s.NobetGrupId,
                        NobetGrupAdi = s.NobetGrup.Adi,
                        NobetGunKuralId = s.NobetGunKuralId,
                        NobetGunKuralAdi = s.NobetGunKural.Adi,
                        BaslangicTarihi = s.BaslangicTarihi,
                        BitisTarihi = s.BitisTarihi,
                        Id = s.Id
                    }).SingleOrDefault(filter);
            }
        }
        public List<NobetGrupGunKuralDetay> GetDetayList(Expression<Func<NobetGrupGunKuralDetay, bool>> filter = null)
        {
            using (var ctx = new EczaneNobetContext())
            {
                var liste = ctx.NobetGrupGunKurallar
                    .Select(s => new NobetGrupGunKuralDetay
                    {
                        NobetGrupId = s.NobetGrupId,
                        NobetGrupAdi = s.NobetGrup.Adi,
                        NobetGunKuralId = s.NobetGunKuralId,
                        NobetGunKuralAdi = s.NobetGunKural.Adi,
                        BaslangicTarihi = s.BaslangicTarihi,
                        BitisTarihi = s.BitisTarihi,
                        Id = s.Id
                    });

                return filter == null
                    ? liste.ToList()
                    : liste.Where(filter).ToList();
            }
        }
        public virtual void CokluAktifPasifYap(List<NobetGrupGunKuralDetay> nobetGrupGunKuralDetaylar, bool pasifMi)
        {
            using (var context = new EczaneNobetContext())
            {
                foreach (var nobetGrupGunKuralDetay in nobetGrupGunKuralDetaylar)
                {
                    if (pasifMi)
                    {
                        var nobetGrupGunKural = Get(x => x.Id == nobetGrupGunKuralDetay.Id);
                        nobetGrupGunKural.BitisTarihi = DateTime.Today;
                        var updatedEntity = context.Entry(nobetGrupGunKural);
                        updatedEntity.State = EntityState.Modified;
                        context.SaveChanges();                        
                    }
                    else
                    {
                        var nobetGrupGunKural = Get(x => x.Id == nobetGrupGunKuralDetay.Id);
                        nobetGrupGunKural.BitisTarihi = null;
                        var updatedEntity = context.Entry(nobetGrupGunKural);
                        updatedEntity.State = EntityState.Modified;
                        context.SaveChanges();
                    }
                }
            }
        }
    }
}