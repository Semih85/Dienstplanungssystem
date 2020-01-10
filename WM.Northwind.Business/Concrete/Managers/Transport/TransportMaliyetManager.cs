using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Business.Abstract;
using WM.Northwind.Business.Abstract.Transport;
using WM.Northwind.DataAccess.Abstract;
using WM.Northwind.DataAccess.Abstract.Transport;
using WM.Northwind.Entities.ComplexTypes;
using WM.Northwind.Entities.ComplexTypes.Transport;
using WM.Northwind.Entities.Concrete;
using WM.Northwind.Entities.Concrete.Optimization.Transport;

namespace WM.Northwind.Business.Concrete.Managers.Transport
{
    public class TransportMaliyetManager : ITransportMaliyetService
    {
        private ITransportMaliyetDal _transportMaliyetDal;

        public TransportMaliyetManager(ITransportMaliyetDal transportMaliyetDal)
        {
            _transportMaliyetDal = transportMaliyetDal;
        }

        public void Insert(TransportMaliyet TransportMaliyet)
        {
            _transportMaliyetDal.Insert(TransportMaliyet);
        }

        public void Delete(int transportMaliyetId)
        {
            _transportMaliyetDal.Delete(new TransportMaliyet { Id = transportMaliyetId });
        }

        public List<TransportMaliyet> GetList()
        {
            return _transportMaliyetDal.GetList();
        }

        public List<TransportMaliyet> GetByCategory(int depoId, int fabrikaId)
        {
            return _transportMaliyetDal.GetList(p => p.DepoId == depoId || depoId == 0 && p.FabrikaId == fabrikaId || fabrikaId == 0);
        }

        public TransportMaliyet GetById(int transportMaliyetId)
        {
            return _transportMaliyetDal.Get(g => g.Id == transportMaliyetId);
        }

        public void Update(TransportMaliyet TransportMaliyet)
        {
            _transportMaliyetDal.Update(TransportMaliyet);
        }

        //
        public List<MaliyetDetail> GetMaliyetDetails(int? id)
        {
            return _transportMaliyetDal.GetMaliyetDetails(id);
        }

        public MaliyetDetail GetMaliyetDetailsById(int id)
        {
            return _transportMaliyetDal.GetMaliyetDetailsById(id);
        }
    }
}
