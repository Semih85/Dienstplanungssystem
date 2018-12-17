using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;
using WM.Northwind.Entities.Concrete.Transport;

namespace WM.Northwind.Entities.Concrete.Optimization.Transport
{
    public class TransportMaliyet : IEntity
    {
        public int Id { get; set; }
        public int FabrikaId { get; set; }
        public int DepoId { get; set; }
        public int Deger { get; set; }

        public virtual Fabrika Fabrika { get; set; }
        public virtual Depo Depo { get; set; }
    }
}
