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
    public class SehirDetay : IComplexType
    {
        public int Id { get; set; }
        [Display(Name = "Şehir")]
        public string Adi { get; set; }
        [Display(Name = "Oda")]
        public string EczaneOdaAdi { get; set; }
        public int EczaneOdaId { get; set; }
    }

}
