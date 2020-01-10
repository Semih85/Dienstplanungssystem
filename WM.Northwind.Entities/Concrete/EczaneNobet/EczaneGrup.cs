using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;

namespace WM.Northwind.Entities.Concrete.EczaneNobet
{
    /// <summary>
    /// Bu nesne beraber nöbet tutulması istenen Eczanelerin gruplanması için oluşturulmuştur.
    /// </summary>
    public class EczaneGrup: IEntity
    {
        public int Id { get; set; }
        public int EczaneGrupTanimId { get; set; }
        public int EczaneId { get; set; }
        //public int NobetGorevTipId { get; set; }
        //Aynı gün nöbet yazılması istenmeyen eczaneler için false, aynı gün nöbet yazılması istenen eczaneler için true
        public bool PasifMi { get; set; }
        public bool BirlikteNobetYazilsinMi { get; set; }

        //public virtual NobetGorevTip NobetGorevTip { get; set; }
        public virtual EczaneGrupTanim EczaneGrupTanim { get; set; }
        public virtual Eczane Eczane { get; set; }
    }
}
