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
    public class EczaneGrupTanimDetay: IComplexType
 { 
        public int Id { get; set; }
        [Display(Name = "Adı")]
        public string Adi { get; set; }
        [Display(Name = "Ardışık Boş Gün Sayısı")]
        public int ArdisikNobetSayisi { get; set; }
        [Display(Name = "Aynı Gündeki Nöbetçi Sayısı")]
        public int AyniGunNobetTutabilecekEczaneSayisi { get; set; }

        [Display(Name = "Açıklama")]
        public string Aciklama { get; set; }
        [Display(Name = "Başlama Tarihi")]
        public DateTime BaslangicTarihi { get; set; }
        [Display(Name = "Bitiş Tarihi")]
        public DateTime? BitisTarihi { get; set; }
        public int NobetUstGrupId { get; set; }
        public int EczaneGrupTanimTipId { get; set; }
        [Display(Name = "Nöbet Üst Grubu")]
        public string NobetUstGrupAdi { get; set; }
        [Display(Name = "Grup Tipi")]
        public string EczaneGrupTanimTipAdi { get; set; }
        public string EczaneGrupTanimAdi => $"{Adi}, {NobetGorevTipAdi}";
        public bool PasifMi { get; set; }
        public bool Checked { get; set; }
        public bool Expanded { get; set; }
        public int NobetGorevTipId { get; set; }
        [Display(Name = "Görev Tipi")]
        public string NobetGorevTipAdi { get; set; }

        public int GruptakiEczaneSayisi { get; set; }
        //public List<EczaneGrup> GruptakiEczaneler { get; set; }
    }
} 