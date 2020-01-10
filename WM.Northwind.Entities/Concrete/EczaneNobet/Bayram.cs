using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;

namespace WM.Northwind.Entities.Concrete.EczaneNobet
{
    public class Bayram: IEntity
    {
        //[Key, ForeignKey("Takvim")]
        public int Id { get; set; }
        public int TakvimId { get; set; }
        public int NobetGrupGorevTipId { get; set; }
        public int NobetGunKuralId { get; set; }
        public int BayramTurId { get; set; }

        public virtual NobetGrupGorevTip NobetGrupGorevTip { get; set; }
        public virtual NobetGunKural NobetGunKural { get; set; }
        public virtual Takvim Takvim { get; set; }
        public virtual BayramTur BayramTur { get; set; }

    } 
} 