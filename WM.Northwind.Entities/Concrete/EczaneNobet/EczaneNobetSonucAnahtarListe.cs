using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;

namespace WM.Northwind.Entities.Concrete.EczaneNobet
{
    public class EczaneNobetSonucAnahtarListe : IEntity
    {
        public int Id { get; set; }
        public int EczaneNobetGrupId { get; set; }
        public int NobetUstGrupGunGrupId { get; set; }
        public bool KullanildiMi { get; set; }
        public int AnahtarListeTanimId { get; set; }
        public virtual EczaneNobetGrup EczaneNobetGrup { get; set; }
        public virtual AnahtarListeTanim AnahtarListeTanim { get; set; }
        public virtual NobetUstGrupGunGrup NobetUstGrupGunGrup { get; set; }

    }
}