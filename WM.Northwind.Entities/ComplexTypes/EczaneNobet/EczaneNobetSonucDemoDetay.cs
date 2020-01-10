using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.Entities.ComplexTypes.EczaneNobet
{
    public class EczaneNobetSonucDemoDetay : EczaneNobetSonucListe, IComplexType
    {
        public DateTime EczaneNobetGrupBaslamaTarihi { get; set; }
        public int NobetSonucDemoTipId { get; set; }
        public string NobetSonucDemoTipAdi { get; set; }
    }
}

