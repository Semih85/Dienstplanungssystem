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
    public class TransportSonucManager : ITransportSonucService
    {
        private ITransportSonucDal _transportSonucDal;

        public TransportSonucManager(ITransportSonucDal transportSonucDal)
        {
            _transportSonucDal = transportSonucDal;
        }

        public void Insert(TransportSonuc TransportSonuc)
        {
            _transportSonucDal.Insert(TransportSonuc);
        }

        public void Delete(int TransportSonucId)
        {
            _transportSonucDal.Delete(new TransportSonuc { Id = TransportSonucId });
        }

        public List<TransportSonuc> GetList()
        {
            return _transportSonucDal.GetList();
        }

        public List<TransportSonuc> GetByCategory(int depoId, int fabrikaId)
        {
            return _transportSonucDal.GetList(p => p.DepoId == depoId || depoId == 0 && p.FabrikaId == fabrikaId || fabrikaId == 0);
        }

        public TransportSonuc GetById(int TransportSonucId)
        {
            return _transportSonucDal.Get(g => g.Id == TransportSonucId);
        }

        public void Update(TransportSonuc TransportSonuc)
        {
            _transportSonucDal.Update(TransportSonuc);
        }

        //
        public List<TransportSonucDetail> GetSonucDetails(int? id)
        {
            return _transportSonucDal.GetSonucDetails(id);
        }

        public TransportSonucDetail GetSonucDetailsById(int id)
        {
            return _transportSonucDal.GetSonucDetailsById(id);
        }

        public List<TransportSonucNodes> GetTransportSonucNodes()
        {
            return _transportSonucDal.GetTransportSonucNodes();
        }

        public List<TransportSonucEdges> GetTransportSonucEdges()
        {
            return _transportSonucDal.GetTransportSonucEdges();
        }
    }
}
