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
    public class EfEczaneDal : EfEntityRepositoryBase<Eczane, EczaneNobetContext>, IEczaneDal
    {
        public EczaneDetay GetDetay(Expression<Func<EczaneDetay, bool>> filter)
        {
            using (var ctx = new EczaneNobetContext())
            {
                return ctx.Eczaneler
                        .Select(s => new EczaneDetay
                        {
                            Id = s.Id,
                            EczaneAdi = s.Adi,
                            Adres = s.Adres,
                            WebSitesi = s.WebSitesi,
                            MailAdresi = s.MailAdresi,
                            TelefonNo = s.TelefonNo,
                            AcilisTarihi = s.AcilisTarihi,
                            AdresTarifi = s.AdresTarifi,
                            AdresTarifiKisa = s.AdresTarifiKisa,
                            Boylam = s.Boylam,
                            Enlem = s.Enlem,
                            KapanisTarihi = s.KapanisTarihi,
                            NobetUstGrupAdi = s.NobetUstGrup.Adi,
                            NobetUstGrupId = s.NobetUstGrupId
                        }).SingleOrDefault(filter);
            }
        }

        public List<EczaneDetay> GetDetayList(Expression<Func<EczaneDetay, bool>> filter = null)
        {
            using (var ctx = new EczaneNobetContext())
            {
                var liste = ctx.Eczaneler
                        .Select(s => new EczaneDetay
                        {
                            Id = s.Id,
                            EczaneAdi = s.Adi,
                            AcilisTarihi = s.AcilisTarihi,
                            Adres = s.Adres,
                            WebSitesi = s.WebSitesi,
                            MailAdresi = s.MailAdresi,
                            TelefonNo = s.TelefonNo,
                            AdresTarifi = s.AdresTarifi,
                            AdresTarifiKisa = s.AdresTarifiKisa,
                            Boylam = s.Boylam,
                            Enlem = s.Enlem,
                            KapanisTarihi = s.KapanisTarihi,
                            NobetUstGrupAdi = s.NobetUstGrup.Adi,
                            NobetUstGrupId = s.NobetUstGrupId
                        });

                return filter == null
                    ? liste.ToList()
                    : liste.Where(filter).ToList();
            }
        }
    }
}
