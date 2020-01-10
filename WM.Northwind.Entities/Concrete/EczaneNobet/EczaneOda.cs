using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;

namespace WM.Northwind.Entities.Concrete.EczaneNobet
{
    public class EczaneOda: Iletisim, IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Eczane Oda Adı")]
        public string Adi { get; set; }

        public virtual List<NobetUstGrup> NobetUstGruplar { get; set; }
        public virtual List<UserEczaneOda> UserEczaneOdalar { get; set; }
        public virtual List<Sehir> Sehirler { get; set; }
    }
}
