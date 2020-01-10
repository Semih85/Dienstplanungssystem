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
    public class EczaneGrupNobetSonuc : IComplexType
    {
        public int EczaneId { get; set; }
        public string EczaneAdi { get; set; }
        public int EczaneNobetGrupId { get; set; }
        public int NobetGrupId { get; set; }
        public string NobetGrupAdi { get; set; }
        public int NobetAltGrupId { get; set; }
        public int NobetUstGrupId { get; set; }
        public int TakvimId { get; set; }
        public string GunGrup { get; set; }
        public int NobetGunKuralId { get; set; }
        public DateTime Tarih { get; set; }
        public DateTime NobetUstGrupBaslamaTarihi { get; set; }
        public int NobetGorevTipId { get; set; }
        public int EczaneGrupTanimId { get; set; }
    }
}