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
    public class EczaneNobetIstekCoklu : IComplexType
    {
        public int Id { get; set; }

        [Display(Name = "Eczane")]
        public int[] EczaneNobetGrupId { get; set; }

        [Display(Name = "Mazeret")]
        public int IstekId { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Başlangıç Tarihi")]
        public DateTime BaslangicTarihi { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Bitiş Tarihi")]
        public DateTime BitisTarihi { get; set; }

        [Display(Name = "Haftanın Günü")]
        public int[] HaftaninGunu { get; set; }

        [Display(Name = "Açıklama")]
        public string Aciklama { get; set; }
        public bool YinedeEklensinMi { get; set; }
    }
}
