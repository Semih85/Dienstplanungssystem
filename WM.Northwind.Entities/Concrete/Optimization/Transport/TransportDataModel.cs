using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Entities.Concrete.Transport;

namespace WM.Northwind.Entities.Concrete.Optimization.Transport
{
    public class TransportDataModel
    {
        public int LowerBound { get; set; }
        public int UpperBound { get; set; }
        public List<Depo> Depolar { get; set; }
        public List<Fabrika> Fabrikalar { get; set; }
        public List<TransportMaliyet> Maliyetler { get; set; }
    }
}
