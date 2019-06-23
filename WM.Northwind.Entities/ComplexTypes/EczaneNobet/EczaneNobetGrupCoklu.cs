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
    public class EczaneNobetGrupCoklu : IComplexType
    {
        public int Id { get; set; }
        [Display(Name = "Nöbet Grubu")]
        public int NobetGrupGorevTipId { get; set; }
        [Display(Name = "Eczane")]
        public int[] EczaneId { get; set; }
        [Display(Name = "Açıklama")]
        public string Aciklama { get; set; }
        [Display(Name = "Başlangıç Tarihi")]
        public DateTime BaslangicTarihi { get; set; }
        [Display(Name = "Bitiş Tarihi")]
        public DateTime? BitisTarihi { get; set; }
    }
}
