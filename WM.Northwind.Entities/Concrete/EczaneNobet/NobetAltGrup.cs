using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;

namespace WM.Northwind.Entities.Concrete.EczaneNobet
{
    public class NobetAltGrup : IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Adı")]
        [Required(ErrorMessage = "Alt Grup Adı gereklidir.")]
        public string Adi { get; set; }
        //public int NobetGrupId { get; set; }
        public int NobetGrupGorevTipId { get; set; }

        [Display(Name = "Başlama Tarihi")]
        [Required(ErrorMessage = "Başlama Tarihi gereklidir.")]
        public DateTime BaslamaTarihi { get; set; }
        public DateTime? BitisTarihi { get; set; }

        public virtual NobetGrupGorevTip NobetGrupGorevTip { get; set; }
        public virtual List<EczaneNobetGrupAltGrup> EczaneNobetGrupAltGruplar { get; set; }
        public virtual List<NobetDurum> NobetDurumlar1 { get; set; }
        public virtual List<NobetDurum> NobetDurumlar2 { get; set; }
        public virtual List<NobetDurum> NobetDurumlar3 { get; set; }
        public virtual List<AyniGunNobetTakipGrupAltGrup> AyniGunNobetTakipGrupAltGruplar { get; set; }

    }
}