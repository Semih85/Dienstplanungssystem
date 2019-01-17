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
    public class EfKalibrasyonDal : EfEntityRepositoryBase<Kalibrasyon, EczaneNobetContext>, IKalibrasyonDal
    {
        public KalibrasyonDetay GetDetay(Expression<Func<KalibrasyonDetay, bool>> filter)
        {
            using (var ctx = new EczaneNobetContext())
            {
                return ctx.Kalibrasyonlar
                    .Select(s => new KalibrasyonDetay
                    {
                        EczaneNobetGrupId = s.EczaneNobetGrupId,
                        EczaneAdi = s.EczaneNobetGrup.Eczane.Adi,
                        NobetGrupAdi = s.EczaneNobetGrup.NobetGrupGorevTip.NobetGrup.Adi,
                        NobetGorevTipAdi = s.EczaneNobetGrup.NobetGrupGorevTip.NobetGorevTip.Adi,
                        Aciklama = s.Aciklama,
                        Deger = s.Deger,
                        Id = s.Id,
                        NobetGrupGorevTipId = s.EczaneNobetGrup.NobetGrupGorevTipId,
                        NobetGrupId = s.EczaneNobetGrup.NobetGrupGorevTip.NobetGrupId,
                        KalibrasyonTipId = s.KalibrasyonTipId,
                        KalibrasyonTipAdi = s.KalibrasyonTip.Adi,
                        GunGrupAdi = s.NobetUstGrupGunGrup.GunGrup.Adi,
                        GunGrupId = s.NobetUstGrupGunGrup.GunGrupId,
                        NobetUstGrupAdi = s.NobetUstGrupGunGrup.NobetUstGrup.Adi,
                        NobetUstGrupGunGrupId = s.NobetUstGrupGunGrupId,
                        NobetUstGrupId = s.NobetUstGrupGunGrup.NobetUstGrupId
                    }).SingleOrDefault(filter);
                //return new KalibrasyonDetay();
            }
        }
        public List<KalibrasyonDetay> GetDetayList(Expression<Func<KalibrasyonDetay, bool>> filter = null)
        {
            using (var ctx = new EczaneNobetContext())
            {
                var liste = ctx.Kalibrasyonlar
                    .Select(s => new KalibrasyonDetay
                    {
                        EczaneNobetGrupId = s.EczaneNobetGrupId,
                        EczaneAdi = s.EczaneNobetGrup.Eczane.Adi,
                        NobetGrupAdi = s.EczaneNobetGrup.NobetGrupGorevTip.NobetGrup.Adi,
                        NobetGorevTipAdi = s.EczaneNobetGrup.NobetGrupGorevTip.NobetGorevTip.Adi,
                        Aciklama = s.Aciklama,
                        Deger = s.Deger,
                        Id = s.Id,
                        NobetGrupGorevTipId = s.EczaneNobetGrup.NobetGrupGorevTipId,
                        NobetGrupId = s.EczaneNobetGrup.NobetGrupGorevTip.NobetGrupId,
                        KalibrasyonTipId = s.KalibrasyonTipId,
                        KalibrasyonTipAdi = s.KalibrasyonTip.Adi,
                        GunGrupAdi = s.NobetUstGrupGunGrup.GunGrup.Adi,
                        GunGrupId = s.NobetUstGrupGunGrup.GunGrupId,
                        NobetUstGrupAdi = s.NobetUstGrupGunGrup.NobetUstGrup.Adi,
                        NobetUstGrupGunGrupId = s.NobetUstGrupGunGrupId,
                        NobetUstGrupId = s.NobetUstGrupGunGrup.NobetUstGrupId
                    });

                return filter == null
                    ? liste.ToList()
                    : liste.Where(filter).ToList();

                //return new List<KalibrasyonDetay>();
            }
        }

    }
}