using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Business.Abstract.Transport;
using WM.Northwind.DataAccess.Abstract;
using WM.Northwind.DataAccess.Abstract.Transport;
using WM.Northwind.Entities.Concrete;
using WM.Northwind.Entities.Concrete.Transport;

namespace WM.Northwind.Business.Concrete.Managers.Transport
{
    public class DepoManager : IDepoService
    {
        private IDepoDal _DepoDal;

        public DepoManager(IDepoDal depoDal)
        {
            _DepoDal = depoDal;
        }

        public void Insert(Depo depo)
        {
            _DepoDal.Insert(depo);
        }

        public void Delete(int depoId)
        {
            _DepoDal.Delete(new Depo  { Id = depoId });
        }

        public List<Depo> GetList()
        {
            return _DepoDal.GetList();
        }

        public Depo GetById(int depoId)
        {
            return _DepoDal.Get(g => g.Id == depoId);
        }

        public void Update(Depo depo)
        {
            _DepoDal.Update(depo);
        }
    }
}
