using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WM.Core.DAL.EntityFramework;
using WM.Northwind.DataAccess.Abstract.EczaneNobet;
using WM.Northwind.DataAccess.Concrete.EntityFramework.Contexts;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.DataAccess.Concrete.EntityFramework.EczaneNobet
{
    public class EfEczaneNobetIstekDal : EfEntityRepositoryBase<EczaneNobetIstek, EczaneNobetContext>, IEczaneNobetIstekDal
    {
        public void CokluEkle(List<EczaneNobetIstek> eczaneNobetIstekler)
        {
            using (var context = new EczaneNobetContext())
            {
                foreach (var istek in eczaneNobetIstekler)
                {
                    context.EczaneNobetIstekler.Add(istek);
                }
                context.SaveChanges();
            }
        }

        public EczaneNobetIstekDetay GetDetay(Expression<Func<EczaneNobetIstekDetay, bool>> filter)
        {
            using (var ctx = new EczaneNobetContext())
            {
                return ctx.EczaneNobetIstekler
                    .Select(t => new EczaneNobetIstekDetay
                    {
                        Id = t.Id,
                        IstekAdi = t.Istek.Adi,
                        EczaneAdi = t.EczaneNobetGrup.Eczane.Adi,
                        IstekTuru = t.Istek.IstekTur.Adi,
                        Aciklama = t.Aciklama,
                        Tarih = t.Takvim.Tarih,
                        Yil = t.Takvim.Tarih.Year,
                        Ay = t.Takvim.Tarih.Month,
                        Gun = t.Takvim.Tarih.Day,
                        EczaneNobetGrupId = t.EczaneNobetGrupId,
                        NobetGrupId = t.EczaneNobetGrup.NobetGrupGorevTip.NobetGrupId,
                        EczaneId = t.EczaneNobetGrup.EczaneId,
                        TakvimId = t.TakvimId,
                        IstekId = t.IstekId,
                        IstekTurId = t.Istek.IstekTurId,
                        NobetGrupAdi = t.EczaneNobetGrup.NobetGrupGorevTip.NobetGrup.Adi,
                        NobetUstGrupId = t.EczaneNobetGrup.NobetGrupGorevTip.NobetGrup.NobetUstGrupId,
                        NobetGorevTipId = t.EczaneNobetGrup.NobetGrupGorevTip.NobetGorevTipId,
                        NobetGorevTipAdi = t.EczaneNobetGrup.NobetGrupGorevTip.NobetGorevTip.Adi,
                        NobetGrupGorevTipId = t.EczaneNobetGrup.NobetGrupGorevTipId
                    }).SingleOrDefault(filter);
            }
        }

        public List<EczaneNobetIstekDetay> GetDetayList(Expression<Func<EczaneNobetIstekDetay, bool>> filter = null)
        {
            using (var context = new EczaneNobetContext())
            {
                var liste = (from t in context.EczaneNobetIstekler
                             select new EczaneNobetIstekDetay
                             {
                                 Id = t.Id,
                                 IstekAdi = t.Istek.Adi,
                                 EczaneAdi = t.EczaneNobetGrup.Eczane.Adi,
                                 IstekTuru = t.Istek.IstekTur.Adi,
                                 Aciklama = t.Aciklama,
                                 Tarih = t.Takvim.Tarih,
                                 Yil = t.Takvim.Tarih.Year,
                                 Ay = t.Takvim.Tarih.Month,
                                 Gun = t.Takvim.Tarih.Day,
                                 EczaneNobetGrupId = t.EczaneNobetGrupId,
                                 NobetGrupId = t.EczaneNobetGrup.NobetGrupGorevTip.NobetGrupId,
                                 EczaneId = t.EczaneNobetGrup.EczaneId,
                                 TakvimId = t.TakvimId,
                                 IstekId = t.IstekId,
                                 IstekTurId = t.Istek.IstekTurId,
                                 NobetGrupAdi = t.EczaneNobetGrup.NobetGrupGorevTip.NobetGrup.Adi,
                                 NobetUstGrupId = t.EczaneNobetGrup.NobetGrupGorevTip.NobetGrup.NobetUstGrupId,
                                 NobetGorevTipId = t.EczaneNobetGrup.NobetGrupGorevTip.NobetGorevTipId,
                                 NobetGorevTipAdi = t.EczaneNobetGrup.NobetGrupGorevTip.NobetGorevTip.Adi,
                                 NobetGrupGorevTipId = t.EczaneNobetGrup.NobetGrupGorevTipId
                             });
                return filter == null
                   ? liste.ToList()
                   : liste.Where(filter).ToList();
            }
        }
    }
}
