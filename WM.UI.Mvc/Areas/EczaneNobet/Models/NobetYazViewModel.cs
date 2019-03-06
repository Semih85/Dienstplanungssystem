using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.UI.Mvc.Areas.EczaneNobet.Models
{
    public class NobetYazViewModel
    {
        [Display(Name = "Nöbet Üst Grubu")]
        public int NobetUstGrupId { get; set; }
        [Display(Name = "Rol")]
        public int RolId { get; set; }
        [Display(Name = "Yıl")]
        public int Yil { get; set; }
        [Display(Name = "Ay")]
        public int Ay { get; set; }
        [Display(Name = "Nöbet Grubu")]
        public int[] NobetGrupGorevTipId { get; set; }
        public bool BuAyVeSonrasi { get; set; }

        public int TimeLimit { get; set; }
        [Range(0, 3, ErrorMessage = "Lütfen geçerli bir tamsayı giriniz.")]
        public int CalismaSayisi { get; set; }

        public int CozumTercih { get; set; }
        public bool SonrakiAylarRasgele { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy.MM.dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Başlangıç Tarihi")]
        [Required(ErrorMessage = "Başlangıç Tarihi gereklidir.")]
        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }
        public DateTime SonNobetTarihi { get; set; }
    }
}