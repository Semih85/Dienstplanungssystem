using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;

namespace WM.Northwind.Entities.Concrete.EczaneNobet
{
    public class NobetUstGrupKisitIstisnaGunGrup : IEntity
    {
        public int Id { get; set; }
        public int NobetUstGrupKisitId { get; set; }
        public int NobetUstGrupGunGrupId { get; set; }
        public string Aciklama { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime? BitisTarihi { get; set; }
        public virtual NobetUstGrupGunGrup NobetUstGrupGunGrup { get; set; }
        public virtual NobetUstGrupKisit NobetUstGrupKisit { get; set; }

    }
}