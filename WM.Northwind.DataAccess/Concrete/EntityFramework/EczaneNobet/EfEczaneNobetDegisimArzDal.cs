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
    public class EfEczaneNobetDegisimArzDal : EfEntityRepositoryBase<EczaneNobetDegisimArz, EczaneNobetContext>, IEczaneNobetDegisimArzDal
    {
        public EczaneNobetDegisimArzDetay GetDetay(Expression<Func<EczaneNobetDegisimArzDetay, bool>> filter)
        {
            using (var ctx = new EczaneNobetContext())
            {
                return ctx.EczaneNobetDegisimArzlar
                    .Select(s => new EczaneNobetDegisimArzDetay
                    {
                        EczaneNobetSonucId = s.EczaneNobetSonucId,
                        NobetGrupAdi = s.EczaneNobetGrup.NobetGrupGorevTip.NobetGrup.Adi,
                        NobetGrupId = s.EczaneNobetGrup.NobetGrupGorevTip.NobetGrup.Id,
                        EczaneAdi = s.EczaneNobetSonuc.EczaneNobetGrup.Eczane.Adi,
                        EczaneId = s.EczaneNobetSonuc.EczaneNobetGrup.Eczane.Id,
                        EczaneNobetGrupId = s.EczaneNobetGrupId,
                        UserId = s.UserId,
                        Kaydeden = s.User.Email,
                        Aciklama = s.Aciklama,
                        Id = s.Id,
                        KayitTarihi = s.KayitTarihi,
                        NobetTarihi = s.EczaneNobetSonuc.Takvim.Tarih,
                        NobetUstGrupId = s.EczaneNobetGrup.NobetGrupGorevTip.NobetGrup.NobetUstGrupId

                    }).SingleOrDefault(filter);
            }
        }
        public List<EczaneNobetDegisimArzDetay> GetDetayList(Expression<Func<EczaneNobetDegisimArzDetay, bool>> filter = null)
        {
            using (var ctx = new EczaneNobetContext())
            {
                var liste = ctx.EczaneNobetDegisimArzlar
                    .Select(s => new EczaneNobetDegisimArzDetay
                    {
                        EczaneNobetSonucId = s.EczaneNobetSonucId,
                        NobetGrupAdi = s.EczaneNobetGrup.NobetGrupGorevTip.NobetGrup.Adi,
                        NobetGrupId = s.EczaneNobetGrup.NobetGrupGorevTip.NobetGrup.Id,
                        EczaneAdi = s.EczaneNobetSonuc.EczaneNobetGrup.Eczane.Adi,
                        EczaneId = s.EczaneNobetSonuc.EczaneNobetGrup.Eczane.Id,
                        EczaneNobetGrupId = s.EczaneNobetGrupId,
                        UserId = s.UserId,
                        Kaydeden = s.User.Email,
                        Aciklama = s.Aciklama,
                        Id = s.Id,
                        KayitTarihi = s.KayitTarihi,
                        NobetTarihi = s.EczaneNobetSonuc.Takvim.Tarih,
                        NobetUstGrupId = s.EczaneNobetGrup.NobetGrupGorevTip.NobetGrup.NobetUstGrupId

                    });

                return filter == null
                    ? liste.ToList()
                    : liste.Where(filter).ToList();
            }
        }

    }
}