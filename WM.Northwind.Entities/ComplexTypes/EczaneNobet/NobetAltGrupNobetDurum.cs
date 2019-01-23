using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.Entities.ComplexTypes.EczaneNobet
{
    public class NobetAltGrupNobetDurum
    {
        public string NobetAltGrupAdi { get; set; }
        public int NobetDurumTipId { get; set; }
        public double UstLimit { get; set; }
    }
}
