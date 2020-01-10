using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WM.Northwind.Entities.ComplexTypes.Transport
{
    public class TransportSonucDetail
    {
        public int Id { get; set; }
        public string FabrikaAdi { get; set; }
        public string DepoAdi { get; set; }
        public double Sonuc { get; set; }
    }
}
