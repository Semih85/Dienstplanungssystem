using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;

namespace WM.Northwind.Entities.Concrete.EczaneNobet
{
    public class NobetGrupTalep: IEntity
    {
        public int Id { get; set; }
        public int TakvimId { get; set; }
        public int NobetGrupGorevTipId { get; set; }
        public int NobetciSayisi { get; set; }

        public virtual Takvim Takvim { get; set; }
        public virtual NobetGrupGorevTip NobetGrupGorevTip { get; set; }
    } 
} 