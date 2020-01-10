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
    public class EczaneNobetDegisimSorgu : IComplexType
    {
        [Required(ErrorMessage = "Nöbet tarihi giriniz..")]
        public DateTime NobetTarihi { get; set; }
        public int NobetGrupId { get; set; }
    }
}

