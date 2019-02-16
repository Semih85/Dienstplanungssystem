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
    public class EfEczaneNobetFeragatDal : EfEntityRepositoryBase<EczaneNobetFeragat, EczaneNobetContext>, IEczaneNobetFeragatDal
    {
        public EczaneNobetFeragatDetay GetDetay(Expression<Func<EczaneNobetFeragatDetay, bool>> filter)
        {
            using (var ctx = new EczaneNobetContext())
            {
                return ctx.EczaneNobetFeragatlar
                    .Select(s => new EczaneNobetFeragatDetay
                    {
                        EczaneNobetSonucId = s.EczaneNobetSonucId,
                        Aciklama = s.Aciklama,
                        EczaneNobetGrupId = s.EczaneNobetSonuc.EczaneNobetGrupId,
                        EczaneAdi = s.EczaneNobetSonuc.EczaneNobetGrup.Eczane.Adi,
                        EczaneId = s.EczaneNobetSonuc.EczaneNobetGrup.EczaneId,
                        NobetGrupId = s.EczaneNobetSonuc.EczaneNobetGrup.NobetGrupGorevTip.NobetGrupId,
                        NobetGrupAdi = s.EczaneNobetSonuc.EczaneNobetGrup.NobetGrupGorevTip.NobetGrup.Adi,
                        TakvimId = s.EczaneNobetSonuc.TakvimId,
                        Tarih = s.EczaneNobetSonuc.Takvim.Tarih,
                        NobetGorevTipId = s.EczaneNobetSonuc.NobetGorevTipId,
                        NobetGorevTipAdi = s.EczaneNobetSonuc.NobetGorevTip.Adi,
                        NobetUstGrupId = s.EczaneNobetSonuc.EczaneNobetGrup.NobetGrupGorevTip.NobetGrup.NobetUstGrupId,
                        NobetUstGrupAdi = s.EczaneNobetSonuc.EczaneNobetGrup.NobetGrupGorevTip.NobetGrup.NobetUstGrup.Adi,
                        NobetFeragatTipId = s.NobetFeragatTipId,
                        NobetFeragatTipAdi = s.NobetFeragatTip.Adi,
                        EczaneNobetGrupIdFeragatEden = s.EczaneNobetGrupId,
                        EczaneAdiFeragatEden = s.EczaneNobetGrup.Eczane.Adi
                    }).SingleOrDefault(filter);
            }
        }

        public List<EczaneNobetFeragatDetay> GetDetayList(Expression<Func<EczaneNobetFeragatDetay, bool>> filter = null)
        {
            using (var ctx = new EczaneNobetContext())
            {
                var liste = ctx.EczaneNobetFeragatlar
                    .Select(s => new EczaneNobetFeragatDetay
                    {
                        EczaneNobetSonucId = s.EczaneNobetSonucId,
                        Aciklama = s.Aciklama,
                        EczaneNobetGrupId = s.EczaneNobetSonuc.EczaneNobetGrupId,
                        EczaneAdi = s.EczaneNobetSonuc.EczaneNobetGrup.Eczane.Adi,
                        EczaneId = s.EczaneNobetSonuc.EczaneNobetGrup.EczaneId,
                        NobetGrupId = s.EczaneNobetSonuc.EczaneNobetGrup.NobetGrupGorevTip.NobetGrupId,
                        NobetGrupAdi = s.EczaneNobetSonuc.EczaneNobetGrup.NobetGrupGorevTip.NobetGrup.Adi,
                        TakvimId = s.EczaneNobetSonuc.TakvimId,
                        Tarih = s.EczaneNobetSonuc.Takvim.Tarih,
                        NobetGorevTipId = s.EczaneNobetSonuc.NobetGorevTipId,
                        NobetGorevTipAdi = s.EczaneNobetSonuc.NobetGorevTip.Adi,
                        NobetUstGrupId = s.EczaneNobetSonuc.EczaneNobetGrup.NobetGrupGorevTip.NobetGrup.NobetUstGrupId,
                        NobetUstGrupAdi = s.EczaneNobetSonuc.EczaneNobetGrup.NobetGrupGorevTip.NobetGrup.NobetUstGrup.Adi,
                        NobetFeragatTipId = s.NobetFeragatTipId,
                        NobetFeragatTipAdi = s.NobetFeragatTip.Adi,
                        EczaneNobetGrupIdFeragatEden = s.EczaneNobetGrupId,
                        EczaneAdiFeragatEden = s.EczaneNobetGrup.Eczane.Adi
                    });

                return filter == null
                    ? liste.ToList()
                    : liste.Where(filter).ToList();
            }
        }
    }
}
