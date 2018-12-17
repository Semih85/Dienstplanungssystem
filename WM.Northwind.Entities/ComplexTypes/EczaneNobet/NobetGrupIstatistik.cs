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
    public class NobetGrupIstatistik : IComplexType
    {
        public int Id { get; set; }
        //[Display(Name = "Adı")]
        public string Adi { get; set; }
        public int EczaneSayisi { get; set; }
    }
}