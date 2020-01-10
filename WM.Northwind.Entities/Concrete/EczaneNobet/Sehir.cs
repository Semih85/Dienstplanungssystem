using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;

namespace WM.Northwind.Entities.Concrete.EczaneNobet
{
    public class Sehir:  IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Şehir Adı")]
        public string Adi { get; set; }
        public int EczaneOdaId { get; set; }

        public virtual List<Ilce> Ilceler { get; set; }
        public virtual EczaneOda EczaneOda { get; set; }
    }
}
