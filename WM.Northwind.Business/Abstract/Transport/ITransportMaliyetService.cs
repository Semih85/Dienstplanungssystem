using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Entities.ComplexTypes;
using WM.Northwind.Entities.ComplexTypes.Transport;
using WM.Northwind.Entities.Concrete;
using WM.Northwind.Entities.Concrete.Optimization.Transport;

namespace WM.Northwind.Business.Abstract.Transport
{
    public interface ITransportMaliyetService
    {
        TransportMaliyet GetById(int transportMaliyetId);
        List<TransportMaliyet> GetList();
        List<TransportMaliyet> GetByCategory(int depoId, int fabrikaId);

        //
        void Insert(TransportMaliyet transportMaliyet);
        void Update(TransportMaliyet transportMaliyet);
        void Delete(int transportMaliyetId);

        //
        List<MaliyetDetail> GetMaliyetDetails(int? id);
        MaliyetDetail GetMaliyetDetailsById(int id);
    }
}
