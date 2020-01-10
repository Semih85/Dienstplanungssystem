using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;

namespace WM.Northwind.Entities.Concrete.EczaneNobet
{
    public class AyniGunNobetTakipGrupAltGrup : IEntity
    {
        public int Id { get; set; }
        public int NobetGrupGorevTipId { get; set; }
        public int NobetAltGrupId { get; set; }
        public int KumulatifToplamNobetSayisi { get; set; }
        public DateTime BaslamaTarihi { get; set; }
        public DateTime? BitisTarihi { get; set; }

        public virtual NobetAltGrup NobetAltGrup { get; set; }
        public virtual NobetGrupGorevTip NobetGrupGorevTip { get; set; }
    }
}