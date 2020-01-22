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
    public class EfEczaneUzaklikMatrisDal : EfEntityRepositoryBase<EczaneUzaklikMatris, EczaneNobetContext>, IEczaneUzaklikMatrisDal
    {
        public EczaneUzaklikMatrisDetay GetDetay(Expression<Func<EczaneUzaklikMatrisDetay, bool>> filter)
        {
            using (var ctx = new EczaneNobetContext())
            {
                return (from s in ctx.EczaneUzaklikMatrisler
                            //let eczaneNobetGrupFrom = s.EczaneFrom.EczaneNobetGruplar.Where(w => w.BitisTarihi != null || w.BitisTarihi <= DateTime.Now).SingleOrDefault() ?? new EczaneNobetGrup()
                            //let eczaneNobetGrupTo = s.EczaneTo.EczaneNobetGruplar.Where(w => w.BitisTarihi != null || w.BitisTarihi <= DateTime.Now).SingleOrDefault() ?? new EczaneNobetGrup()
                        select new EczaneUzaklikMatrisDetay
                        {
                            Id = s.Id,
                            EczaneIdFrom = s.EczaneIdFrom,
                            EczaneIdTo = s.EczaneIdTo,
                            Mesafe = s.Mesafe,
                            NobetUstGrupId = s.EczaneFrom.NobetUstGrupId,
                            EczaneAdiFrom = s.EczaneFrom.Adi,
                            EczaneAdiTo = s.EczaneTo.Adi,
                            //EczaneNobetGrupIdFrom = eczaneNobetGrupFrom.Id,
                            //EczaneNobetGrupIdTo = eczaneNobetGrupTo.Id,
                            //NobetGrupGorevTipIdFrom = eczaneNobetGrupFrom.NobetGrupGorevTipId,
                            //NobetGrupGorevTipIdTo = eczaneNobetGrupTo.NobetGrupGorevTipId
                            //NobetGrupAdiFrom = eczaneNobetGrupFrom.NobetGrupGorevTip.NobetGrup.Adi,
                        }).SingleOrDefault(filter);
            }
        }
        public List<EczaneUzaklikMatrisDetay> GetDetayList(Expression<Func<EczaneUzaklikMatrisDetay, bool>> filter = null)
        {
            using (var ctx = new EczaneNobetContext())
            {
                var liste = from s in ctx.EczaneUzaklikMatrisler
                                //let eczaneNobetGrupFrom = s.EczaneFrom.EczaneNobetGruplar.Where(w => w.BitisTarihi != null || w.BitisTarihi <= DateTime.Now).SingleOrDefault() ?? new EczaneNobetGrup()
                                //let eczaneNobetGrupTo = s.EczaneTo.EczaneNobetGruplar.Where(w => w.BitisTarihi != null || w.BitisTarihi <= DateTime.Now).SingleOrDefault() ?? new EczaneNobetGrup()
                            select new EczaneUzaklikMatrisDetay
                            {
                                Id = s.Id,
                                EczaneIdFrom = s.EczaneIdFrom,
                                EczaneIdTo = s.EczaneIdTo,
                                Mesafe = s.Mesafe,
                                NobetUstGrupId = s.EczaneFrom.NobetUstGrupId,
                                EczaneAdiFrom = s.EczaneFrom.Adi,
                                EczaneAdiTo = s.EczaneTo.Adi,
                                //EczaneNobetGrupIdFrom = eczaneNobetGrupFrom.Id,
                                //EczaneNobetGrupIdTo = eczaneNobetGrupTo.Id,
                                //NobetGrupGorevTipIdFrom = eczaneNobetGrupFrom.NobetGrupGorevTipId,
                                //NobetGrupGorevTipIdTo = eczaneNobetGrupTo.NobetGrupGorevTipId
                                //NobetGrupAdiFrom = eczaneNobetGrupFrom.NobetGrupGorevTip.NobetGrup.Adi,
                            };

                return filter == null
                    ? liste.ToList()
                    : liste.Where(filter).ToList();
            }
        }

        public virtual void CokluEkle(List<EczaneUzaklikMatrisDetay> eczaneUzaklikMatrisDetaylar)
        {
            var liste = new List<EczaneUzaklikMatris>();

            using (var ctx = new EczaneNobetContext())
            {
                foreach (var eczaneUzaklik in eczaneUzaklikMatrisDetaylar)
                {
                    var nobetSonuc = new EczaneUzaklikMatris
                    {
                        EczaneIdFrom = eczaneUzaklik.EczaneIdFrom,
                        EczaneIdTo = eczaneUzaklik.EczaneIdTo,
                        Mesafe = eczaneUzaklik.Mesafe
                    };

                    liste.Add(nobetSonuc);
                }

                ctx.EczaneUzaklikMatrisler.AddRange(liste);
                ctx.SaveChanges();
            }
        }

    }
}