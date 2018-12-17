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
    public class EfEczaneNobetMuafiyetDal : EfEntityRepositoryBase<EczaneNobetMuafiyet, EczaneNobetContext>, IEczaneNobetMuafiyetDal
    {
        public EczaneNobetMuafiyetDetay GetDetay(Expression<Func<EczaneNobetMuafiyetDetay, bool>> filter)
        {
            using (var ctx = new EczaneNobetContext())
            {
                return ctx.EczaneNobetMuafiyetler
                    .Select(s => new EczaneNobetMuafiyetDetay
                    {
                        Aciklama = s.Aciklama,
                        BaslamaTarihi = s.BaslamaTarihi,
                        BitisTarihi = s.BitisTarihi,
                        Id = s.Id,
                        EczaneId = s.EczaneId,
                        EczaneAdi = s.Eczane.Adi,
                        NobetGrupId = ctx.EczaneNobetGruplar
                                        .Where(w => w.EczaneId == s.EczaneId 
                                                 && w.BitisTarihi == null)
                                        .Select(d => d.NobetGrupGorevTip.NobetGrupId).FirstOrDefault(),
                        NobetGrupAdi = ctx.EczaneNobetGruplar
                                        .Where(w => w.EczaneId == s.EczaneId 
                                                 && w.BitisTarihi == null)
                                        .Select(d => d.NobetGrupGorevTip.NobetGrup.Adi).FirstOrDefault()

                    }).SingleOrDefault(filter);
            }
        }
        public List<EczaneNobetMuafiyetDetay> GetDetayList(Expression<Func<EczaneNobetMuafiyetDetay, bool>> filter = null)
        {
            using (var ctx = new EczaneNobetContext())
            {
                var liste = ctx.EczaneNobetMuafiyetler
                    .Select(s => new EczaneNobetMuafiyetDetay
                    {
                        Aciklama = s.Aciklama,
                        BaslamaTarihi = s.BaslamaTarihi,
                        BitisTarihi = s.BitisTarihi,
                        Id = s.Id,
                        EczaneId = s.EczaneId,
                        EczaneAdi = s.Eczane.Adi,
                        NobetGrupId = ctx.EczaneNobetGruplar
                                        .Where(w => w.EczaneId == s.EczaneId
                                                 && w.BitisTarihi == null)
                                        .Select(d => d.NobetGrupGorevTip.NobetGrupId).FirstOrDefault(),
                        NobetGrupAdi = ctx.EczaneNobetGruplar
                                        .Where(w => w.EczaneId == s.EczaneId
                                                 && w.BitisTarihi == null)
                                        .Select(d => d.NobetGrupGorevTip.NobetGrup.Adi).FirstOrDefault()
                    });

                return filter == null
                    ? liste.ToList()
                    : liste.Where(filter).ToList();
            }
        }
    }
}