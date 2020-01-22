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
    public class EczaneUzaklikMatrisDetay : IComplexType
    {
        public int Id { get; set; }
        public int EczaneIdFrom { get; set; }
        public string EczaneAdiFrom { get; set; }
        public int EczaneIdTo { get; set; }
        public string EczaneAdiTo { get; set; }
        public int NobetUstGrupId { get; set; }
        public int Mesafe { get; set; }
        public int EczaneNobetGrupIdFrom { get; set; }
        public int EczaneNobetGrupIdTo { get; set; }
        public int NobetGrupGorevTipIdFrom { get; set; }
        public int NobetGrupGorevTipIdTo { get; set; }
    }
}