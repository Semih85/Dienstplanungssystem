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
    public class EfEczaneNobetSonucAnahtarListeDal : EfEntityRepositoryBase<EczaneNobetSonucAnahtarListe, EczaneNobetContext>, IEczaneNobetSonucAnahtarListeDal
    {
        public EczaneNobetSonucAnahtarListeDetay GetDetay(Expression<Func<EczaneNobetSonucAnahtarListeDetay, bool>> filter)
        {
            using (var ctx = new EczaneNobetContext())
            {
                return ctx.EczaneNobetSonucAnahtarListeler
                    .Select(s => new EczaneNobetSonucAnahtarListeDetay
                    {
                        AnahtarListeTanimAdi = s.AnahtarListeTanim.Adi,
                        AnahtarListeTanimId = s.AnahtarListeTanimId,
                        EczaneNobetGrupAdi = s.EczaneNobetGrup.Eczane.Adi,
                        EczaneNobetGrupId = s.EczaneNobetGrupId,
                        KullanildiMi = s.KullanildiMi,
                        NobetUstGrupGunGrupId = s.NobetUstGrupGunGrupId,
                        NobetUstGrupGunGrupAdi = s.NobetUstGrupGunGrup.GunGrup.Adi,

                    }).SingleOrDefault(filter);
            }
        }
        public List<EczaneNobetSonucAnahtarListeDetay> GetDetayList(Expression<Func<EczaneNobetSonucAnahtarListeDetay, bool>> filter = null)
        {
            using (var ctx = new EczaneNobetContext())
            {
                var liste = ctx.EczaneNobetSonucAnahtarListeler
                    .Select(s => new EczaneNobetSonucAnahtarListeDetay
                    {
                        AnahtarListeTanimAdi = s.AnahtarListeTanim.Adi,
                        AnahtarListeTanimId = s.AnahtarListeTanimId,
                        EczaneNobetGrupAdi = s.EczaneNobetGrup.Eczane.Adi,
                        EczaneNobetGrupId = s.EczaneNobetGrupId,
                        KullanildiMi = s.KullanildiMi,
                        NobetUstGrupGunGrupId = s.NobetUstGrupGunGrupId,
                        NobetUstGrupGunGrupAdi = s.NobetUstGrupGunGrup.GunGrup.Adi,
                        
                    });

                return filter == null
                    ? liste.ToList()
                    : liste.Where(filter).ToList();
            }
        }
    }
}