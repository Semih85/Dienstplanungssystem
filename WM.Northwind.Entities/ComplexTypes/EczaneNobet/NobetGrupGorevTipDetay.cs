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
    public class NobetGrupGorevTipDetay : IComplexType
    {
        public int Id { get; set; }
        public int NobetGrupId { get; set; }
        public int NobetUstGrupId { get; set; }
        public int NobetGorevTipId { get; set; }
        public string NobetGrupAdi { get; set; }
        public string NobetUstGrupAdi { get; set; }
        public string NobetGorevTipAdi { get; set; }
        public string NobetGrupGorevTipAdi => $"{NobetGrupAdi} ({NobetGorevTipAdi})";
        public DateTime BaslamaTarihi { get; set; }
        public DateTime? BitisTarihi { get; set; }
    } 
} 