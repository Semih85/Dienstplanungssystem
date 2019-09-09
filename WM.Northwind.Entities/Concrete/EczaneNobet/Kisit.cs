using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;

namespace WM.Northwind.Entities.Concrete.EczaneNobet
{
    public class Kisit : IEntity
    {
        public int Id { get; set; }
        public string Adi { get; set; }
        public string AdiGosterilen { get; set; }
        public string Aciklama { get; set; }
        public DateTime OlusturmaTarihi { get; set; }
        public int KisitKategoriId { get; set; }
        public bool DegerPasifMi { get; set; }

        public KisitKategori KisitKategori { get; set; }
        public virtual List<NobetUstGrupKisit> NobetUstGrupKisitlar { get; set; }
    }
}