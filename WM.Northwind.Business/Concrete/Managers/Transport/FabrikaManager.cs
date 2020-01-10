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
    public class FabrikaManager : IFabrikaService
    {
        private IFabrikaDal _fabrikaDal;

        public FabrikaManager(IFabrikaDal fabrikaDal)
        {
            _fabrikaDal = fabrikaDal;
        }

        public void Insert(Fabrika fabrika)
        {
            _fabrikaDal.Insert(fabrika);
        }

        public void Delete(int fabrikaId)
        {
            _fabrikaDal.Delete(new Fabrika {  Id  = fabrikaId });
        }

        public List<Fabrika> GetList()
        {
            return _fabrikaDal.GetList();
        }


        public Fabrika GetById(int fabrikaId)
        {
            return _fabrikaDal.Get(g => g.Id == fabrikaId);
        }

        public void Update(Fabrika Fabrika)
        {
            _fabrikaDal.Update(Fabrika);
        }
    }
}
