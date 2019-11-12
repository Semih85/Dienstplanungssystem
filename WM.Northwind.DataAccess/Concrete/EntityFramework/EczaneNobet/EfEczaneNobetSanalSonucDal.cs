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
    public class EfEczaneNobetSanalSonucDal : EfEntityRepositoryBase<EczaneNobetSanalSonuc, EczaneNobetContext>, IEczaneNobetSanalSonucDal
    {
        public EczaneNobetSanalSonucDetay GetDetay(Expression<Func<EczaneNobetSanalSonucDetay, bool>> filter)
        {
            using (var ctx = new EczaneNobetContext())
            {
                return ctx.EczaneNobetSanalSonuclar
                    .Select(s => new EczaneNobetSanalSonucDetay
                    {
                        Id = s.EczaneNobetSonucId,
                        EczaneNobetSonucId = s.EczaneNobetSonucId,
                        NobetTarihi = s.EczaneNobetSonuc.Takvim.Tarih,
                        EczaneAdi = s.EczaneNobetSonuc.EczaneNobetGrup.Eczane.Adi,
                        NobetGrupAdi = s.EczaneNobetSonuc.EczaneNobetGrup.NobetGrupGorevTip.NobetGrup.Adi,
                        NobetGorevTipAdi = s.EczaneNobetSonuc.EczaneNobetGrup.NobetGrupGorevTip.NobetGorevTip.Adi,
                        UserId = s.UserId,
                        UserAdi = s.User.UserName,
                        KayitTarihi = s.KayitTarihi,
                        Aciklama = s.Aciklama,
                        NobetUstGrupId = s.EczaneNobetSonuc.EczaneNobetGrup.NobetGrupGorevTip.NobetGrup.NobetUstGrupId,
                        NobetGrupGorevTipId = s.EczaneNobetSonuc.EczaneNobetGrup.NobetGrupGorevTipId,
                        EczaneNobetGrupId = s.EczaneNobetSonuc.EczaneNobetGrupId
                    }).SingleOrDefault(filter);
            }
        }
        public List<EczaneNobetSanalSonucDetay> GetDetayList(Expression<Func<EczaneNobetSanalSonucDetay, bool>> filter = null)
        {
            using (var ctx = new EczaneNobetContext())
            {
                var liste = ctx.EczaneNobetSanalSonuclar
                    .Select(s => new EczaneNobetSanalSonucDetay
                    {
                        Id = s.EczaneNobetSonucId,
                        EczaneNobetSonucId = s.EczaneNobetSonucId,
                        NobetTarihi = s.EczaneNobetSonuc.Takvim.Tarih,
                        EczaneAdi = s.EczaneNobetSonuc.EczaneNobetGrup.Eczane.Adi,
                        NobetGrupAdi = s.EczaneNobetSonuc.EczaneNobetGrup.NobetGrupGorevTip.NobetGrup.Adi,
                        NobetGorevTipAdi = s.EczaneNobetSonuc.EczaneNobetGrup.NobetGrupGorevTip.NobetGorevTip.Adi,
                        UserId = s.UserId,
                        UserAdi = s.User.UserName,
                        KayitTarihi = s.KayitTarihi,
                        Aciklama = s.Aciklama,
                        NobetUstGrupId = s.EczaneNobetSonuc.EczaneNobetGrup.NobetGrupGorevTip.NobetGrup.NobetUstGrupId,
                        NobetGrupGorevTipId = s.EczaneNobetSonuc.EczaneNobetGrup.NobetGrupGorevTipId,
                        EczaneNobetGrupId = s.EczaneNobetSonuc.EczaneNobetGrupId
                    });

                return filter == null
                    ? liste.ToList()
                    : liste.Where(filter).ToList();
            }
        }

    }
}