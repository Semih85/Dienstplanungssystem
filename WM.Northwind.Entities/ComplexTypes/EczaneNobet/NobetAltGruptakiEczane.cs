using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.Entities.ComplexTypes.EczaneNobet
{
    public class NobetAltGruptakiEczane
    {
        public int NobetGrupId { get; set; }
        public string NobetGrupAdi { get; set; }
        public int NobetAltGrupId { get; set; }
        public string NobetAltGrupAdi { get; set; }
        public int EczaneSayisi { get; set; }
    }
}
