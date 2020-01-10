using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;

namespace WM.Northwind.Entities.Concrete.EczaneNobet
{
    public class NobetGrup: IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Adı")]
        public string Adi { get; set; }
        public int NobetUstGrupId { get; set; }
        [Display(Name = "Başlama Tarihi")]
        public DateTime BaslamaTarihi { get; set; }
        [Display(Name = "Bitiş Tarihi")]
        public DateTime? BitisTarihi { get; set; }

        public virtual NobetUstGrup NobetUstGrup { get; set; }        
        public virtual List<NobetGrupGorevTip> NobetGrupGorevTipler { get; set; }
        public virtual List<NobetGrupGunKural> NobetGrupGunKurallar { get; set; }
    } 
} 