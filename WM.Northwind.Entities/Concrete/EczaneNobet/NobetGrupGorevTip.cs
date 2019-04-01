using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;

namespace WM.Northwind.Entities.Concrete.EczaneNobet
{
    public class NobetGrupGorevTip: IEntity
    {
        public int Id { get; set; }
        public int NobetGrupId { get; set; }
        public int NobetGorevTipId { get; set; }
        public DateTime BaslamaTarihi { get; set; }
        public DateTime? BitisTarihi { get; set; }

        public virtual NobetGrup NobetGrup { get; set; }
        public virtual NobetGorevTip NobetGorevTip { get; set; }

        public virtual List<EczaneNobetGrup> EczaneNobetGruplar { get; set; }
        public virtual List<NobetAltGrup> NobetAltGruplar { get; set; }
        public virtual List<NobetGrupKural> NobetGrupKurallar { get; set; }
        public virtual List<NobetGrupTalep> NobetGrupTalepler { get; set; }
        public virtual List<Bayram> Bayramlar { get; set; }
        public virtual List<NobetGrupGorevTipGunKural> NobetGrupGorevTipGunKurallar { get; set; }
        public virtual List<NobetGrupGorevTipKisit> NobetGrupGorevTipKisitlar { get; set; }
        public virtual List<AyniGunNobetTakipGrupAltGrup> AyniGunNobetTakipGrupAltGruplar { get; set; }
    }
} 