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
    public class EfEczaneNobetMazeretDal : EfEntityRepositoryBase<EczaneNobetMazeret, EczaneNobetContext>, IEczaneNobetMazeretDal
    {
        public EczaneNobetMazeretDetay GetDetay(Expression<Func<EczaneNobetMazeretDetay, bool>> filter)
        {
            using (var ctx = new EczaneNobetContext())
            {
                return ctx.EczaneNobetMazeretler
                    .Select(t => new EczaneNobetMazeretDetay
                    {
                        Id = t.Id,
                        MazeretAdi = t.Mazeret.Adi,
                        EczaneAdi = t.EczaneNobetGrup.Eczane.Adi,
                        MazeretTuru = t.Mazeret.MazeretTur.Adi,
                        Aciklama = t.Aciklama,
                        Tarih = t.Takvim.Tarih,
                        Yil = t.Takvim.Tarih.Year,
                        Ay = t.Takvim.Tarih.Month,
                        Gun = t.Takvim.Tarih.Day,
                        EczaneId = t.EczaneNobetGrup.EczaneId,
                        TakvimId = t.TakvimId,
                        MazeretId = t.MazeretId,
                        MazeretTurId = t.Mazeret.MazeretTurId,
                        NobetGrupId = t.EczaneNobetGrup.NobetGrupGorevTip.NobetGrupId,
                        EczaneNobetGrupId = t.EczaneNobetGrupId,
                        NobetUstGrupId = t.EczaneNobetGrup.NobetGrupGorevTip.NobetGrup.NobetUstGrupId,
                        NobetGrupAdi = t.EczaneNobetGrup.NobetGrupGorevTip.NobetGrup.Adi,
                        NobetGorevTipId = t.EczaneNobetGrup.NobetGrupGorevTip.NobetGorevTipId,
                        NobetGorevTipAdi = t.EczaneNobetGrup.NobetGrupGorevTip.NobetGorevTip.Adi,
                        NobetGrupGorevTipId = t.EczaneNobetGrup.NobetGrupGorevTipId
                    }).SingleOrDefault(filter);
            }
        }

        public List<EczaneNobetMazeretDetay> GetDetayList(Expression<Func<EczaneNobetMazeretDetay, bool>> filter = null)
        {
            using (var context = new EczaneNobetContext())
            {
                var liste = (from t in context.EczaneNobetMazeretler
                             select new EczaneNobetMazeretDetay
                             {
                                 Id = t.Id,
                                 MazeretAdi = t.Mazeret.Adi,
                                 EczaneAdi = t.EczaneNobetGrup.Eczane.Adi,
                                 MazeretTuru = t.Mazeret.MazeretTur.Adi,
                                 Aciklama = t.Aciklama,
                                 Tarih = t.Takvim.Tarih,
                                 Yil = t.Takvim.Tarih.Year,
                                 Ay = t.Takvim.Tarih.Month,
                                 Gun = t.Takvim.Tarih.Day,
                                 EczaneId = t.EczaneNobetGrup.EczaneId,
                                 TakvimId = t.TakvimId,
                                 MazeretId = t.MazeretId,
                                 MazeretTurId = t.Mazeret.MazeretTurId,
                                 NobetGrupId = t.EczaneNobetGrup.NobetGrupGorevTip.NobetGrupId,
                                 EczaneNobetGrupId = t.EczaneNobetGrupId,
                                 NobetUstGrupId = t.EczaneNobetGrup.NobetGrupGorevTip.NobetGrup.NobetUstGrupId,
                                 NobetGrupAdi = t.EczaneNobetGrup.NobetGrupGorevTip.NobetGrup.Adi,
                                 NobetGorevTipId = t.EczaneNobetGrup.NobetGrupGorevTip.NobetGorevTipId,
                                 NobetGorevTipAdi = t.EczaneNobetGrup.NobetGrupGorevTip.NobetGorevTip.Adi,
                                 NobetGrupGorevTipId = t.EczaneNobetGrup.NobetGrupGorevTipId
                             });

                return filter == null
                   ? liste.ToList()
                   : liste.Where(filter).ToList();
            }
        }

        public virtual void CokluEkle(List<EczaneNobetMazeret> eczaneNobetMazeretler)
        {
            using (var context = new EczaneNobetContext())
            {
                foreach (var mazeret in eczaneNobetMazeretler)
                {
                    context.EczaneNobetMazeretler.Add(mazeret);
                }
                context.SaveChanges();
            }
        }
    }
}
