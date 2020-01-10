using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WM.Northwind.Entities.ComplexTypes.Transport
{
    public class MaliyetDetail
    {
        public int Id { get; set; }
        public string FabrikaAdi { get; set; }
        public string DepoAdi { get; set; }
        public int Deger { get; set; }
    }
}
