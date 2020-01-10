using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.Entities.ComplexTypes.EczaneNobet
{
    public class EczaneNobetSonucDemoDetay2 : IComplexType
    {
        public int Id { get; set; }
        public DateTime Tarih { get; set; }
        public int TakvimId { get; set; }
        public int EczaneNobetGrupId { get; set; }
        public int NobetGorevTipId { get; set; }
        public int NobetSonucDemoTipId { get; set; }
        public int EczaneId { get; set; }
        public int NobetGrupId { get; set; }
        public int NobetUstGrupId { get; set; }
        public DateTime EczaneNobetGrupBaslamaTarihi { get; set; }
        public DateTime NobetUstGrupBaslamaTarihi { get; set; }
        
        public string NobetSonucDemoTipAdi { get; set; }
        public string NobetGorevTipAdi { get; set; }
        public string NobetGrupAdi { get; set; }
        public string EczaneAdi { get; set; }
    }
}

