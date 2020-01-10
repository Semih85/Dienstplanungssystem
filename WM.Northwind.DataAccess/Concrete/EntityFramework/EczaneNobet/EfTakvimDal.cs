using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Linq.Expressions;
using WM.Core.DAL.EntityFramework;
using WM.Northwind.DataAccess.Abstract.EczaneNobet;
using WM.Northwind.DataAccess.Concrete.EntityFramework.Contexts;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.DataAccess.Concrete.EntityFramework.EczaneNobet
{
    public class EfTakvimDal : EfEntityRepositoryBase<Takvim, EczaneNobetContext>, ITakvimDal
    {
        public TakvimDetay GetTakvimDetay(Expression<Func<TakvimDetay, bool>> filter = null)
        {
            using (var context = new EczaneNobetContext())
            {
                return context.Takvimler
                              .Select(t => new TakvimDetay
                              {
                                  TakvimId = t.Id,
                                  Tarih = t.Tarih,
                                  Yil = t.Tarih.Year,
                                  Ay = t.Tarih.Month,
                                  Gun = t.Tarih.Day,
                                  HaftaninGunu = (int)SqlFunctions.DatePart("weekday", t.Tarih)
                              }).SingleOrDefault(filter);
            }
        }

        public List<TakvimDetay> GetTakvimDetaylar(Expression<Func<TakvimDetay, bool>> filter = null)
        {
            using (var context = new EczaneNobetContext())
            {
                var takvim = (from t in context.Takvimler
                              select new TakvimDetay
                              {
                                  TakvimId = t.Id,
                                  Tarih = t.Tarih,
                                  Yil = t.Tarih.Year,
                                  Ay = t.Tarih.Month,
                                  Gun = t.Tarih.Day,
                                  HaftaninGunu = (int)SqlFunctions.DatePart("weekday", t.Tarih)
                              });

                return filter == null
                   ? takvim.ToList()
                   : takvim.Where(filter).ToList();
            }
        }
    }
}
