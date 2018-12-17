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
    public class EczaneGrupTanimEczane : IComplexType
    {
        public int NobetUstGrupId { get; set; }
        public int EczaneGrupTanimId { get; set; }
        public int NobetGrupId { get; set; }
        public int EczaneId { get; set; }
        public string EczaneGrupTanimAdi { get; set; }
        public string EczaneGrupTanimTuru { get; set; }
        public string NobetGrupAdi { get; set; }
        public string EczaneAdi { get; set; }
    }
}
