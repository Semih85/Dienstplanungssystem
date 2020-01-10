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
    public class EczaneNobetGrupAltGrupDetay: IComplexType
 { 
        public int Id { get; set; }
        public int EczaneNobetGrupId { get; set; }
        public int EczaneId { get; set; }
        public int NobetGrupId { get; set; }
        public int NobetGorevTipId { get; set; }
        public int NobetGrupGorevTipId { get; set; }
        public int NobetAltGrupId { get; set; }
        public int NobetUstGrupId { get; set; }

        public string EczaneAdi { get; set; }
        public string NobetGrupAdi { get; set; }
        public string NobetGorevTipAdi { get; set; }
        public string NobetAltGrupAdi { get; set; }

        [Display(Name = "Açıklama")]
        public string Aciklama { get; set; }
        [Display(Name = "Başlangıç Tarihi")]
        public DateTime BaslangicTarihi { get; set; }
        [Display(Name = "Bitiş Tarihi")]
        public DateTime? BitisTarihi { get; set; }
    } 
} 