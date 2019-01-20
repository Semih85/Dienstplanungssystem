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
    public class KalibrasyonYatay : IComplexType
    {
        public int EczaneNobetGrupId { get; set; }
        public string EczaneAdi { get; set; }

        //public int GunGrupId { get; set; }
        //public string GunGrupAdi { get; set; }
        public int NobetUstGrupId { get; set; }
        public string NobetUstGrupAdi { get; set; }
        public double KalibrasyonCumartesi { get; set; }
        public double KalibrasyonPazar { get; set; }
        public double KalibrasyonHaftaIci { get; set; }
        public double KalibrasyonToplamCumartesi { get; set; }
        public double KalibrasyonToplamPazar { get; set; }
        public double KalibrasyonToplamHaftaIci { get; set; }

        public int KalibrasyonTipId { get; set; }
        public string KalibrasyonTipAdi { get; set; }

        
    }
}