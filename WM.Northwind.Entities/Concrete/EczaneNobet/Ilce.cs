using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;

namespace WM.Northwind.Entities.Concrete.EczaneNobet
{
    public class Ilce:  IEntity
    {
        public int Id { get; set; }
        [Display(Name = "İlçe Adı")]
        public string Adi { get; set; }
        public int SehirId { get; set; }

        public virtual List<EczaneIlce> EczaneIlceler { get; set; }
        public virtual Sehir Sehir { get; set; }

    }
}
