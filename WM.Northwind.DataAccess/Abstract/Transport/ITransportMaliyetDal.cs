using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WM.Core.DAL;
using WM.Northwind.Entities.ComplexTypes;
using WM.Northwind.Entities.ComplexTypes.Transport;
using WM.Northwind.Entities.Concrete;
using WM.Northwind.Entities.Concrete.Optimization.Transport;

namespace WM.Northwind.DataAccess.Abstract.Transport
{
    public interface ITransportMaliyetDal : IEntityRepository<TransportMaliyet>
    {
        //Custom Operations

        List<MaliyetDetail> GetMaliyetDetails(int? id);
        MaliyetDetail GetMaliyetDetailsById(int id);

    }
}
