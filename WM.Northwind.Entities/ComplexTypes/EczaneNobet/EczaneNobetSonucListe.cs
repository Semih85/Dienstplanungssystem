using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.Entities.ComplexTypes.EczaneNobet
{
    public class EczaneNobetSonucListe : IComplexType
    {
        public DateTime Tarih { get; set; }
        public int Ay { get; set; }
        public int Gun { get; set; }
        public int BayramTurId { get; set; }
        public int EczaneId { get; set; }
        public int EczaneNobetGrupId { get; set; }
        public int HaftaninGunu { get; set; }
        public int Id { get; set; }
        public int NobetGorevTipId { get; set; }
        public int NobetGrupId { get; set; }
        public int NobetGunKuralId { get; set; }
        public int NobetUstGrupId { get; set; }
        public int TakvimId { get; set; }
        public int Yil { get; set; }
        public string EczaneAdi { get; set; }
        public string GunGrup { get; set; }
        public string GunTanim { get; set; }
        public string NobetGorevTipAdi { get; set; }
        public string NobetGrupAdi { get; set; }
    }
}

