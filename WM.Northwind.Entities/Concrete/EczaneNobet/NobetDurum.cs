using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;

namespace WM.Northwind.Entities.Concrete.EczaneNobet
{
    public class NobetDurum : IEntity
    {
        public int Id { get; set; }
        public int NobetAltGrupId1 { get; set; }
        public int NobetAltGrupId2 { get; set; }
        public int NobetAltGrupId3 { get; set; }
        public int NobetDurumTipId { get; set; }

        public virtual NobetAltGrup NobetAltGrupl { get; set; }
        public virtual NobetAltGrup NobetAltGrup2 { get; set; }
        public virtual NobetAltGrup NobetAltGrup3 { get; set; }
        public virtual NobetDurumTip NobetDurumTip { get; set; }

    }
}