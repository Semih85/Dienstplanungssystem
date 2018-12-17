using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WM.Core.DAL.EntityFramework;
using WM.Northwind.DataAccess.Abstract.Transport;
using WM.Northwind.DataAccess.Concrete.EntityFramework.Contexts;
using WM.Northwind.Entities.ComplexTypes;
using WM.Northwind.Entities.ComplexTypes.Transport;
using WM.Northwind.Entities.Concrete;
using WM.Northwind.Entities.Concrete.Optimization.Transport;

namespace WM.Northwind.DataAccess.Concrete.EntityFramework.Transport
{
    public class EfTransportMaliyetDal : EfEntityRepositoryBase<TransportMaliyet, TransportContext>, ITransportMaliyetDal
    {
        public List<MaliyetDetail> GetMaliyetDetails(int? id)
        {
            using (var context = new TransportContext())
            {

                var kategoriler = new List<MaliyetDetail>();

                if (id == null)
                {
                    kategoriler = context.Maliyetler
                    .Select(x => new MaliyetDetail
                    {
                        Id = x.Id,
                        DepoAdi = x.Depo.Adi,
                        FabrikaAdi = x.Fabrika.Adi,
                        Deger = x.Deger
                    }).ToList();
                }
                else
                {
                    kategoriler = context.Maliyetler
                        .Where(x => x.Id == id)
                        .Select(x => new MaliyetDetail
                        {
                            Id = x.Id,
                            DepoAdi = x.Depo.Adi,
                            FabrikaAdi = x.Fabrika.Adi,
                            Deger = x.Deger
                        }).ToList();
                }


                return kategoriler;
            }

        }

        public MaliyetDetail GetMaliyetDetailsById(int id)
        {

            using (var context = new TransportContext())
            {
                var kategoriler = new MaliyetDetail();

                kategoriler = context.Maliyetler
                    .Where(x => x.Id == id)
                    .Select(x => new MaliyetDetail
                    {
                        Id = x.Id,
                        DepoAdi = x.Depo.Adi,
                        FabrikaAdi = x.Fabrika.Adi,
                        Deger = x.Deger
                    }).SingleOrDefault();

                return kategoriler;
            }
        }
    }
}
