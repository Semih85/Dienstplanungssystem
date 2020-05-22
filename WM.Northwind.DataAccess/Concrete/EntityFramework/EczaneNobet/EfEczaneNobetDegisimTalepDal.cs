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
    public class EfEczaneNobetDegisimTalepDal : EfEntityRepositoryBase<EczaneNobetDegisimTalep, EczaneNobetContext>, IEczaneNobetDegisimTalepDal
    {
        public EczaneNobetDegisimTalepDetay GetDetay(Expression<Func<EczaneNobetDegisimTalepDetay, bool>> filter)
        {
            using (var ctx = new EczaneNobetContext())
            {
                return ctx.EczaneNobetDegisimTalepler
                    .Select(s => new EczaneNobetDegisimTalepDetay
                    {
                        //EczaneNobetSonucId = s.EczaneNobetSonucId,
                        NobetGrupAdi = s.EczaneNobetGrup.NobetGrupGorevTip.NobetGrup.Adi,
                        NobetGrupId = s.EczaneNobetGrup.NobetGrupGorevTip.NobetGrup.Id,
                        EczaneAdi = s.EczaneNobetDegisimArz.EczaneNobetSonuc.EczaneNobetGrup.Eczane.Adi,
                        EczaneId = s.EczaneNobetDegisimArz.EczaneNobetSonuc.EczaneNobetGrup.Eczane.Id,
                        EczaneNobetGrupId = s.EczaneNobetGrupId,
                        UserId = s.UserId,
                        Kaydeden = s.User.Email,
                        Aciklama = s.Aciklama,
                        Id = s.Id,
                        KayitTarihi = s.KayitTarihi,
                        NobetTarihi = s.EczaneNobetDegisimArz.EczaneNobetSonuc.Takvim.Tarih,
                        NobetUstGrupId = s.EczaneNobetGrup.NobetGrupGorevTip.NobetGrup.NobetUstGrupId,
                        EczaneNobetDegisimArzId = s.EczaneNobetDegisimArzId

                    }).SingleOrDefault(filter);
            }
        }
        public List<EczaneNobetDegisimTalepDetay> GetDetayList(Expression<Func<EczaneNobetDegisimTalepDetay, bool>> filter = null)
        {
            using (var ctx = new EczaneNobetContext())
            {
                var liste = ctx.EczaneNobetDegisimTalepler
                    .Select(s => new EczaneNobetDegisimTalepDetay
                    {
                        //EczaneNobetSonucId = s.EczaneNobetSonucId,
                        NobetGrupAdi = s.EczaneNobetGrup.NobetGrupGorevTip.NobetGrup.Adi,
                        NobetGrupId = s.EczaneNobetGrup.NobetGrupGorevTip.NobetGrup.Id,
                        EczaneAdi = s.EczaneNobetDegisimArz.EczaneNobetSonuc.EczaneNobetGrup.Eczane.Adi,
                        EczaneId = s.EczaneNobetDegisimArz.EczaneNobetSonuc.EczaneNobetGrup.Eczane.Id,
                        EczaneNobetGrupId = s.EczaneNobetGrupId,
                        UserId = s.UserId,
                        Kaydeden = s.User.Email,
                        Aciklama = s.Aciklama,
                        Id = s.Id,
                        KayitTarihi = s.KayitTarihi,
                        NobetTarihi = s.EczaneNobetDegisimArz.EczaneNobetSonuc.Takvim.Tarih,
                        NobetUstGrupId = s.EczaneNobetGrup.NobetGrupGorevTip.NobetGrup.NobetUstGrupId,
                        EczaneNobetDegisimArzId = s.EczaneNobetDegisimArzId

                    });

                return filter == null
                    ? liste.ToList()
                    : liste.Where(filter).ToList();
            }
        }
    }
}