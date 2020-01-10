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
    public class EczaneCiftGrup: IComplexType
    {
        [Display(Name = "Grup Id")]
        public int Id { get; set; }
        [Display(Name = "Gruptaki Eczane Id")]
        public int EczaneId { get; set; }
        public int EczaneNobetGrupId { get; set; }
        public string EczaneAdi { get; set; }
        public string NobetGrupAdi { get; set; }
        public string NobetUstGrupAdi { get; set; }
        public int NobetAltGrupId { get; set; }
        [Display(Name = "Birlikte Nöbet Tutma Sayısı")]
        public int BirlikteNobetTutmaSayisi { get; set; }
    }
}
