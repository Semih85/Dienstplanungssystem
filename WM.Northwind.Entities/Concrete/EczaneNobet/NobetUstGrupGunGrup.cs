using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;

namespace WM.Northwind.Entities.Concrete.EczaneNobet
{
    public class NobetUstGrupGunGrup : IEntity
    {
        public int Id { get; set; }
        public int GunGrupId { get; set; }
        public int NobetUstGrupId { get; set; }
        public int AmacFonksiyonuKatsayisi { get; set; }
        public string Aciklama { get; set; }

        public virtual GunGrup GunGrup { get; set; }
        public virtual NobetUstGrup NobetUstGrup { get; set; }
        public virtual List<NobetGrupGorevTipGunKural> NobetGrupGorevTipGunKurallar { get; set; }
        public virtual List<Kalibrasyon> Kalibrasyonlar { get; set; }        
    }
}