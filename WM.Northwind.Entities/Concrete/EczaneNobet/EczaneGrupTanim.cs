using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;

namespace WM.Northwind.Entities.Concrete.EczaneNobet
{
    public class EczaneGrupTanim : IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Adı")]
        public string Adi { get; set; }
        [Display(Name = "Ardışık Nöbet Sayısı")]
        public int ArdisikNobetSayisi { get; set; }
        [Display(Name = "Açıklama")]
        public string Aciklama { get; set; }
        [Display(Name = "Başlangıç Tarihi")]
        public DateTime BaslangicTarihi { get; set; }

        [Display(Name = "Bitiş Tarihi")]
        public DateTime? BitisTarihi { get; set; }
        public int NobetUstGrupId { get; set; }
        public int EczaneGrupTanimTipId { get; set; }
        public int AyniGunNobetTutabilecekEczaneSayisi { get; set; }
        public bool PasifMi { get; set; }
        public int NobetGorevTipId { get; set; }

        public virtual NobetGorevTip NobetGorevTip { get; set; }
        public virtual EczaneGrupTanimTip EczaneGrupTanimTip { get; set; }
        public virtual NobetUstGrup NobetUstGrup { get; set; }
        public virtual List<EczaneGrup> EczaneGruplar { get; set; }
    }
}