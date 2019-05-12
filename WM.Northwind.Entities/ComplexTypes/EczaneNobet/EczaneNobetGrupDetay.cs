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
    public class EczaneNobetGrupDetay : IComplexType
    {
        public int Id { get; set; }
        [Display(Name = "Eczane")]
        public string EczaneAdi { get; set; }
        [Display(Name = "Nöbet Grubu")]
        public string NobetGrupAdi { get; set; }
        [Display(Name = "Nöbet Grup Id")]
        public int NobetGrupId { get; set; }
        public int NobetGrupGorevTipId { get; set; }
        public int NobetGorevTipId { get; set; }
        [Display(Name = "Görev Tipi")]
        public string NobetGorevTipAdi { get; set; }
        [Display(Name = "Eczane Id")]
        public int EczaneId { get; set; }
        [Display(Name = "Açıklama")]
        public string Aciklama { get; set; }
        [Display(Name = "Başlama Tarihi")]
        public DateTime BaslangicTarihi { get; set; }
        [Display(Name = "Bitiş Tarihi")]
        public DateTime? BitisTarihi { get; set; }

        public string EczaneGorevTipAdi => $"{EczaneAdi} ({NobetGorevTipAdi})";

        [Display(Name = "Nöbet Grup Tanım")]
        public string NobetGrupGorevTipAdi => $"{NobetGrupAdi} ({NobetGorevTipAdi})";

        public int NobetUstGrupId { get; set; }
        public string NobetUstGrupAdi { get; set; }

        [Display(Name = "Nöbet Grubu")]
        public string EczaneNobetGrupAdi { get; set; }

        public string NobetAltGrupAdi { get; set; }
        public int? NobetAltGrupId { get; set; }
        public DateTime NobetUstGrupBaslamaTarihi { get; set; }
        public DateTime NobetGrupGorevTipBaslamaTarihi { get; set; }

        public bool EnErkenTarihteNobetYazilsinMi { get; set; }
        public DateTime? EczaneKapanmaTarihi { get; set; }
    }
}
