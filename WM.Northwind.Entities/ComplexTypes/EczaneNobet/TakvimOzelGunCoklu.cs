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
    public class TakvimOzelGunCoklu : IComplexType
    {
        public int Id { get; set; }
        [Display(Name = "Nöbet Grubu")]
        [Required]
        public int[] NobetGrupGorevTipGunKuralId { get; set; }
        [Display(Name = "Gün Kural")]
        [Required]
        public int NobetGunKuralId { get; set; }   
        [Display(Name = "Tarih")]
        [Required(ErrorMessage = "Lütfen bayram tarihini giriniz.")]
        public DateTime Tarih { get; set; }
        [Display(Name = "Bayram Türü")]
        [Required]
        public int NobetOzelGunId { get; set; }
        public bool FarkliGunGosterilsinMi { get; set; }//true ise NobetGunKuralId, değilse -normalde- NobetGrupGorevTipGunKuralId
        public double AgirlikDegeri { get; set; }
        [Display(Name = "Gün Kategori")]
        public int NobetOzelGunKategoriId { get; set; }

    }
}
