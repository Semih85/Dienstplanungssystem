using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;
using WM.Northwind.Entities.Concrete.Optimization.Transport;

namespace WM.Northwind.Entities.Concrete.Transport
{
    public class Fabrika : IEntity
    {
        public Fabrika()
        {
            Maliyetler = new List<TransportMaliyet>();
        }

        public int Id { get; set; }
        public string Adi { get; set; }
        public int Kapasite { get; set; }

        public virtual List<TransportSonuc> TransportSonuclar { get; set; }
        public virtual List<TransportMaliyet> Maliyetler { get; set; }
    }
}
