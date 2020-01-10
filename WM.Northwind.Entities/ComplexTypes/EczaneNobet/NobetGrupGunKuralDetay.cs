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
    public class NobetGrupGunKuralDetay: IComplexType
 { 
        public int Id { get; set; }
        public int NobetGrupId { get; set; }
        public int NobetGunKuralId { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Başlangıç Tarihi")]
        public DateTime BaslangicTarihi { get; set; }
        [Display(Name = "Bitiş Tarihi")]
        public DateTime? BitisTarihi { get; set; }
        [Display(Name = "Nöbet Grubu")]
        public string NobetGrupAdi { get; set; }
        [Display(Name = "Nöbet Gün Kuralı")]
        public string NobetGunKuralAdi { get; set; }

    } 
} 