using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;

namespace WM.Northwind.Entities.Concrete.EczaneNobet
{
    public class Takvim : IEntity
    {
        public int Id { get; set; }
        public DateTime Tarih { get; set; }
        public string Aciklama { get; set; }
        
        public virtual List<EczaneNobetSonucAktif> EczaneNobetSonucAktifler { get; set; }
        public virtual List<EczaneNobetSonuc> EczaneNobetSonuclar { get; set; }
        public virtual List<EczaneNobetMazeret> EczaneNobetMazeretler { get; set; }
        public virtual List<EczaneNobetIstek> EczaneNobetIstekler { get; set; }
        public virtual List<EczaneNobetSonucDemo> EczaneNobetSonucDemolar { get; set; }
        public virtual List<NobetGrupTalep> NobetGrupTalepler { get; set; }
        public virtual List<EczaneNobetSonucPlanlanan> EczaneNobetSonucPlanlananlar { get; set; }
        public virtual List<AyniGunTutulanNobet> AyniGunTutulanNobetler { get; set; }
        public virtual List<Bayram> Bayramlar { get; set; }
        public virtual List<NobetGrupGorevTipTakvimOzelGun> NobetGrupGorevTipTakvimOzelGunler { get; set; }
    }
}
