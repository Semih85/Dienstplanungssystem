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
    public class EfNobetUstGrupDal : EfEntityRepositoryBase<NobetUstGrup, EczaneNobetContext>, INobetUstGrupDal
    {
        public NobetUstGrupDetay GetDetay(Expression<Func<NobetUstGrupDetay, bool>> filter)
        {
            using (var ctx = new EczaneNobetContext())
            {
                return ctx.NobetUstGruplar
                        .Select(x => new NobetUstGrupDetay
                        {
                            Id = x.Id,
                            Adi = x.Adi,
                            Aciklama = x.Aciklama,
                            EczaneOdaAdi = x.EczaneOda.Adi,
                            EczaneOdaId = x.EczaneOdaId,
                            BaslangicTarihi = x.BaslangicTarihi,
                            BitisTarihi = x.BitisTarihi,
                            Enlem = x.Enlem,
                            Boylam = x.Boylam,
                            TimeLimit = x.TimeLimit,
                            OneedeGosterilecekEnUzakMesafe = x.OneedeGosterilecekEnUzakMesafe,
                            BaslamaTarihindenOncekiSonuclarGosterilsinMi = x.BaslamaTarihindenOncekiSonuclarGosterilsinMi
                        }).SingleOrDefault(filter);
            }
        }

        public List<NobetUstGrupDetay> GetDetayList(Expression<Func<NobetUstGrupDetay, bool>> filter = null)
        {
            using (var ctx = new EczaneNobetContext())
            {
                var list = ctx.NobetUstGruplar
                        .Select(x => new NobetUstGrupDetay
                        {
                            Id = x.Id,
                            Adi = x.Adi,
                            Aciklama = x.Aciklama,
                            EczaneOdaAdi = x.EczaneOda.Adi,
                            EczaneOdaId = x.EczaneOdaId,
                            BaslangicTarihi = x.BaslangicTarihi,
                            BitisTarihi = x.BitisTarihi,
                            Enlem = x.Enlem,
                            Boylam = x.Boylam,
                            TimeLimit = x.TimeLimit,
                            OneedeGosterilecekEnUzakMesafe = x.OneedeGosterilecekEnUzakMesafe,
                            BaslamaTarihindenOncekiSonuclarGosterilsinMi = x.BaslamaTarihindenOncekiSonuclarGosterilsinMi
                        });

                return filter == null
                    ? list.ToList()
                    : list.Where(filter).ToList();
            }
        }
    }
}
