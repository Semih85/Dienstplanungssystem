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
    public class EfNobetGrupKuralDal : EfEntityRepositoryBase<NobetGrupKural, EczaneNobetContext>, INobetGrupKuralDal
    {

        public NobetGrupKuralDetay GetDetay(Expression<Func<NobetGrupKuralDetay, bool>> filter)
        {
            using (var ctx = new EczaneNobetContext())
            {
                return ctx.NobetGrupKurallar
                    .Select(s => new NobetGrupKuralDetay
                    {
                        NobetGrupId = s.NobetGrupGorevTip.NobetGrupId,
                        NobetGrupAdi = s.NobetGrupGorevTip.NobetGrup.Adi,
                        NobetKuralId = s.NobetKuralId,
                        NobetKuralAdi = s.NobetKural.Adi,
                        BaslangicTarihi = s.BaslangicTarihi,
                        BitisTarihi = s.BitisTarihi,
                        Deger = s.Deger,
                        Id = s.Id,
                        NobetGorevTipId = s.NobetGrupGorevTip.NobetGorevTipId,
                        NobetGorevTipAdi = s.NobetGrupGorevTip.NobetGorevTip.Adi,
                        NobetGrupGorevTipId = s.NobetGrupGorevTipId,
                        NobetUstGrupId = s.NobetGrupGorevTip.NobetGrup.NobetUstGrupId
                    }).SingleOrDefault(filter);
            }
        }

        public List<NobetGrupKuralDetay> GetDetayList(Expression<Func<NobetGrupKuralDetay, bool>> filter = null)
        {
            using (var ctx = new EczaneNobetContext())
            {
                var liste = ctx.NobetGrupKurallar
                    .Select(s => new NobetGrupKuralDetay
                    {
                        NobetGrupId = s.NobetGrupGorevTip.NobetGrupId,
                        NobetGrupAdi = s.NobetGrupGorevTip.NobetGrup.Adi,
                        NobetKuralId = s.NobetKuralId,
                        NobetKuralAdi = s.NobetKural.Adi,
                        BaslangicTarihi = s.BaslangicTarihi,
                        BitisTarihi = s.BitisTarihi,
                        Deger = s.Deger,
                        Id = s.Id,
                        NobetGorevTipId = s.NobetGrupGorevTip.NobetGorevTipId,
                        NobetGorevTipAdi = s.NobetGrupGorevTip.NobetGorevTip.Adi,
                        NobetGrupGorevTipId = s.NobetGrupGorevTipId,
                        NobetUstGrupId = s.NobetGrupGorevTip.NobetGrup.NobetUstGrupId
                    });

                return filter == null
                    ? liste.ToList()
                    : liste.Where(filter).ToList();
            }
        }
        public virtual void CokluEkle(List<NobetGrupKural> nobetGrupKurallar)
        {
            using (var context = new EczaneNobetContext())
            {
                foreach (var nobetGrupKural in nobetGrupKurallar)
                {
                    context.NobetGrupKurallar.Add(nobetGrupKural);
                }
                context.SaveChanges();
            }
        }
        public virtual void CokluDegistir(List<NobetGrupKural> nobetGrupKurallar)
        {
            using (var context = new EczaneNobetContext())
            {
                context.SaveChanges();
            }
        }
    }
}