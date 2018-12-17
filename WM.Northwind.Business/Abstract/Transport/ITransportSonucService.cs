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
    public interface ITransportSonucService
    {
        TransportSonuc GetById(int id);
        List<TransportSonuc> GetList();
        List<TransportSonuc> GetByCategory(int depoId, int fabrikaId);

        //
        void Insert(TransportSonuc transportSonuc);
        void Update(TransportSonuc transportSonuc);
        void Delete(int transportSonucId);

        // complex types
        List<TransportSonucDetail> GetSonucDetails(int? id);
        TransportSonucDetail GetSonucDetailsById(int id);
        List<TransportSonucNodes> GetTransportSonucNodes();
        List<TransportSonucEdges> GetTransportSonucEdges();
    }
}
