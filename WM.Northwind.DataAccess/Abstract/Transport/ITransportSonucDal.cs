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
    public interface ITransportSonucDal : IEntityRepository<TransportSonuc>
    {
        //Custom Operations

        List<TransportSonucDetail> GetSonucDetails(int? id);
        TransportSonucDetail GetSonucDetailsById(int id);
        List<TransportSonucNodes> GetTransportSonucNodes();
        List<TransportSonucEdges> GetTransportSonucEdges();
    }
}
