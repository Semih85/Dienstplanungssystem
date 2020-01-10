using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.UI.Mvc.Areas.EczaneNobet.Models
{
    public class AmacFonksiyonKatsayiViewModel
    {
        public int NobetUstGrupId { get; set; }        

        [DisplayFormat(DataFormatString = "{0:yyyy.MM.dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Başlangıç Tarihi")]
        [Required(ErrorMessage = "Başlangıç Tarihi gereklidir.")]
        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }
        public DateTime SonNobetTarihi { get; set; }
    }
}