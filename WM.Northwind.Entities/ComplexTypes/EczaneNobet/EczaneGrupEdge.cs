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
    public class EczaneGrupEdge : IComplexType
    {
        public int NobetUstGrupId { get; set; }
        public int From { get; set; }
        public int To { get; set; }
        public int FromNobetGrupId { get; set; }
        public string FromNobetGrupAdi { get; set; }
        public string FromEczaneAdi { get; set; }
        public int ToNobetGrupId { get; set; }
        public string ToNobetGrupAdi { get; set; }
        public string ToEczaneAdi { get; set; }
        public double Value { get; set; }
        //public double Label { get; set; }
        public string Title { get; set; }
        public string GrupTanimAdi { get; set; }
        public string GrupTuru { get; set; }
    }
}
