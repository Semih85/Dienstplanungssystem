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
    public class NobetGrupKuralDetay: IComplexType
 { 
        public int Id { get; set; }
        public int NobetGrupId { get; set; }
        public int NobetUstGrupId { get; set; }
        public int NobetGrupGorevTipId { get; set; }
        public int NobetGorevTipId { get; set; }
        public int NobetKuralId { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime? BitisTarihi { get; set; }
        [Display(Name = "Değer")]
        public double? Deger { get; set; }
        [Display(Name = "Nöbet Grubu")]
        public string NobetGrupAdi { get; set; }
        public string NobetGorevTipAdi { get; set; }
        [Display(Name = "Nöbet Kuralı")]
        public string NobetKuralAdi { get; set; }

    } 
} 