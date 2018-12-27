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
    public class EczaneNobetDegistir : IComplexType
    {        
        public int EczaneNobetSonucId { get; set; }
        public int EczaneNobetSonucIdYeniNobetci { get; set; }
        public int EczaneNobetGrupIdEski { get; set; }
        public int EczaneNobetGrupId { get; set; }
        public int NobetGorevTipId { get; set; }
        public int TakvimId { get; set; }
        public bool KarsilikliNobetDegistir { get; set; }
        [Display(Name = "Açıklama")]
        [Required(ErrorMessage = "Lütfen nöbet değişim açıklamasını giriniz.")]
        public string Aciklama { get; set; }
    }
}