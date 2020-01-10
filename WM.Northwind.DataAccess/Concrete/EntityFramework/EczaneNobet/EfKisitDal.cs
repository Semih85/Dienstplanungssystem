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
    public class EfKisitDal : EfEntityRepositoryBase<Kisit, EczaneNobetContext>, IKisitDal
    {
        public KisitDetay GetDetay(Expression<Func<KisitDetay, bool>> filter)
        {
            using (var ctx = new EczaneNobetContext())
            {
                return ctx.Kisitlar
                        .Select(s => new KisitDetay
                        {
                            Id = s.Id,
                            Aciklama = s.Aciklama,
                            Adi = s.Adi,
                            AdiGosterilen = s.AdiGosterilen,
                            KisitKategoriAdi = s.KisitKategori.Adi,
                            KisitKategoriId = s.KisitKategoriId,
                            OlusturmaTarihi = s.OlusturmaTarihi,
                            DegerPasifMi = s.DegerPasifMi
                        }).SingleOrDefault(filter);
            }
        }

        public List<KisitDetay> GetDetayList(Expression<Func<KisitDetay, bool>> filter = null)
        {
            using (var ctx = new EczaneNobetContext())
            {
                var liste = ctx.Kisitlar
                        .Select(s => new KisitDetay
                        {
                            Id = s.Id,
                            Aciklama = s.Aciklama,
                            Adi = s.Adi,
                            AdiGosterilen = s.AdiGosterilen,
                            KisitKategoriAdi = s.KisitKategori.Adi,
                            KisitKategoriId = s.KisitKategoriId,
                            OlusturmaTarihi = s.OlusturmaTarihi,
                            DegerPasifMi = s.DegerPasifMi
                        });

                return filter == null
                    ? liste.ToList()
                    : liste.Where(filter).ToList();
            }
        }
    }
}