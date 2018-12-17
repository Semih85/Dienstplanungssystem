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
    public class NobetGrupKuralCoklu : IComplexType
    {
        public string Id { get; set; }
        [Display(Name = "Nöbet Grubu")]
        [Required]
        public int[] NobetGrupGorevTipId { get; set; }
        [Display(Name = "Kural")]
        [Required]
        public int NobetKuralId { get; set; }
        [Display(Name = "Başlangıç Tarihi")]
        [Required(ErrorMessage = "Başlangıç tarihi gereklidir.")]
        public DateTime BaslangicTarihi { get; set; }

        [Display(Name = "Bitiş Tarihi")]
        //[Required(ErrorMessage = "Lütfen mazeretin bitiş tarihini giriniz.")]
        public DateTime? BitisTarihi { get; set; }
        [Required]
        [Display(Name = "Değer")]
        public int Deger { get; set; }
        
    }
}
