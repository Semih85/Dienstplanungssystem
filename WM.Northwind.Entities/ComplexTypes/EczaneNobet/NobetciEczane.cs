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
    public class NobetciEczane : IComplexType
    {
        public int EczaneId { get; set; }
        public int NobetUstGrupId { get; set; }
        public string Adi { get; set; }
        public string Adres { get; set; }
        public string TelefonNo { get; set; }
        public double Enlem { get; set; }
        public double Boylam { get; set; }
        public string AdresTarifi { get; set; }
        public string AdresTarifiKisa { get; set; }
        public string NobetGorevTipAdi { get; set; }
        public string NobetGrupAdi { get; set; }
    }
}
