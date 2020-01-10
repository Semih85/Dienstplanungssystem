using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.Entities.ComplexTypes.EczaneNobet
{
    public class NobetSonucGrupAnaliz
    {
        [Display(Name = "Takvim Id")]
        public int TakvimId { get; set; }
        [Display(Name = "Grup-1 Eczane Id")]
        public int G1EczaneId { get; set; }
        [Display(Name = "Grup-2 Eczane Id")]
        public int G2EczaneId { get; set; }
        [Display(Name = "Grup-3 Eczane Id")]
        public int G3EczaneId { get; set; }
    }
}
