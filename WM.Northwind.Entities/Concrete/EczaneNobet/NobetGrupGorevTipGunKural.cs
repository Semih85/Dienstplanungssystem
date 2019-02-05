using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;

namespace WM.Northwind.Entities.Concrete.EczaneNobet
{
    public class NobetGrupGorevTipGunKural : IEntity
    {
        public int Id { get; set; }
        public int NobetGrupGorevTipId { get; set; }
        public int NobetGunKuralId { get; set; }
        public int NobetUstGrupGunGrupId { get; set; }
        public int NobetciSayisi { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime? BitisTarihi { get; set; }

        public virtual NobetGrupGorevTip NobetGrupGorevTip { get; set; }
        public virtual NobetGunKural NobetGunKural { get; set; }
        public virtual NobetUstGrupGunGrup NobetUstGrupGunGrup { get; set; }
        public virtual List<NobetGrupGorevTipTakvimOzelGun> NobetGrupGorevTipTakvimOzelGunler { get; set; }
        
    }
}