using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.Entities.ComplexTypes.EczaneNobet
{
    public class EczaneNobetGrupIstatistik : IComplexType
    {
        public int NobetUstGrupId { get; set; }
        public int NobetGrupId { get; set; }
        public string NobetGrupGorevTipAdi { get; set; }
        public string NobetGrupAdi { get; set; }
        public int NobetGrupGorevTipId { get; set; }
        public int NobeGorevTipId { get; set; }
        public string NobeGorevTipAdi { get; set; }
        public int EczaneSayisi { get; set; }
        public string NobetUstGrupAdi { get; set; }
    }
}
