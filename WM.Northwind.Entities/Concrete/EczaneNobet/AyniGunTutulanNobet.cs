using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;

namespace WM.Northwind.Entities.Concrete.EczaneNobet
{
    public class AyniGunTutulanNobet : IEntity
    {
        public int Id { get; set; }
        //public int NobetUstGrupId { get; set; }
        public int EczaneNobetGrupId1 { get; set; }
        public int EczaneNobetGrupId2 { get; set; }
        public int EnSonAyniGunNobetTakvimId { get; set; }
        public int AyniGunNobetSayisi { get; set; }
        public int AyniGunNobetTutamayacaklariGunSayisi { get; set; }

        public virtual EczaneNobetGrup EczaneNobetGrupl { get; set; }
        //public virtual NobetUstGrup NobetUstGrup { get; set; }
        public virtual EczaneNobetGrup EczaneNobetGrup2 { get; set; }
        public virtual Takvim Takvim { get; set; }
    }
}