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
    public class EfIlceDal : EfEntityRepositoryBase<Ilce, EczaneNobetContext>, IIlceDal
    {
        public IlceDetay GetDetay(Expression<Func<IlceDetay, bool>> filter)
        {
            using (var ctx = new EczaneNobetContext())
            {
                var ilceDetay = ctx.Ilceler
                    .Select(s => new IlceDetay
                    {
                        Id = s.Id,
                        Adi = s.Adi,
                        SehirAdi = s.Sehir.Adi,
                        SehirId = s.SehirId
                    }).SingleOrDefault(filter);

                return ilceDetay;
            }
        }

        public List<IlceDetay> GetListDetay()
        {
            using (var ctx = new EczaneNobetContext())
            {
                var ilceDetay = ctx.Ilceler
                    .Select(s => new IlceDetay
                    {
                        Id = s.Id,
                        Adi = s.Adi,
                        SehirAdi = s.Sehir.Adi,
                        SehirId = s.SehirId
                    }).ToList();

                return ilceDetay;
            }
        }
    }
}
