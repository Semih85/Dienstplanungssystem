using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;

namespace WM.Northwind.Entities.Concrete.EczaneNobet
{
    public class NobetGorevTip: IEntity
    {
        public int Id { get; set; }
        public string Adi { get; set; }
        public string EczaneninAcikOlduguSaatAraligi { get; set; }
        public string NobetSaatAraligi { get; set; }

        public virtual List<EczaneNobetSonucAktif> EczaneNobetSonucAktifler { get; set; }
        public virtual List<EczaneNobetSonucDemo> EczaneNobetSonucDemolar { get; set; }
        public virtual List<EczaneNobetSonuc> EczaneNobetSonuclar { get; set; }
        public virtual List<EczaneNobetSonucPlanlanan> EczaneNobetSonucPlanlananlar { get; set; }
        public virtual List<NobetGrupGorevTip> NobetGrupGorevTipler { get; set; }
        public virtual List<EczaneGrupTanim> EczaneGrupTanimlar { get; set; }
    }
} 