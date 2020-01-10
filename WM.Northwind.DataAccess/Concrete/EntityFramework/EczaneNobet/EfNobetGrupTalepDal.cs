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
    public class EfNobetGrupTalepDal : EfEntityRepositoryBase<NobetGrupTalep, EczaneNobetContext>, INobetGrupTalepDal
    {
        public NobetGrupTalepDetay GetDetay(Expression<Func<NobetGrupTalepDetay, bool>> filter)
        {
            using (var ctx = new EczaneNobetContext())
            {
                return ctx.NobetGrupTalepler
                    .Select(s => new NobetGrupTalepDetay
                    {
                        Id = s.Id,
                        TakvimId = s.TakvimId,
                        NobetGrupGorevTipId = s.NobetGrupGorevTipId,
                        Tarih = s.Takvim.Tarih,
                        NobetciSayisi = s.NobetciSayisi,
                        NobetGorevTipAdi = s.NobetGrupGorevTip.NobetGorevTip.Adi,
                        NobetGrupAdi = s.NobetGrupGorevTip.NobetGrup.Adi,
                        NobetGorevTipId = s.NobetGrupGorevTip.NobetGorevTipId,
                        NobetGrupId = s.NobetGrupGorevTip.NobetGrupId,
                        NobetUstGrupId = s.NobetGrupGorevTip.NobetGrup.NobetUstGrupId
                    }).SingleOrDefault(filter);
            }
        }
        public List<NobetGrupTalepDetay> GetDetayList(Expression<Func<NobetGrupTalepDetay, bool>> filter = null)
        {
            using (var ctx = new EczaneNobetContext())
            {
                var liste = ctx.NobetGrupTalepler
                    .Select(s => new NobetGrupTalepDetay
                    {
                        Id = s.Id,
                        TakvimId = s.TakvimId,
                        NobetGrupGorevTipId = s.NobetGrupGorevTipId,
                        Tarih = s.Takvim.Tarih,
                        NobetciSayisi = s.NobetciSayisi,
                        NobetGorevTipAdi = s.NobetGrupGorevTip.NobetGorevTip.Adi,
                        NobetGrupAdi = s.NobetGrupGorevTip.NobetGrup.Adi,
                        NobetGorevTipId = s.NobetGrupGorevTip.NobetGorevTipId,
                        NobetGrupId = s.NobetGrupGorevTip.NobetGrupId,
                        NobetUstGrupId = s.NobetGrupGorevTip.NobetGrup.NobetUstGrupId
                    });

                return filter == null
                    ? liste.ToList()
                    : liste.Where(filter).ToList();
            }
        }
    }
}