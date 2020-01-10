using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.Entities.ComplexTypes.EczaneNobet
{
    public class EczaneNobetTarihAralikIkili
    {
        public DateTime Tarih { get; set; }
        public int TakvimId { get; set; }
        public int AyniGunTutulanNobetId { get; set; }
        public int Yil { get; set; }
        public int Ay { get; set; }
        public int Gun { get; set; }
        public int GunDegerId { get; set; }
        public bool CumartesiGunuMu { get; set; }
        public bool PazarGunuMu { get; set; }
        public bool BayramMi { get; set; }
        public bool HaftaIciMi { get; set; }
        public bool ArifeMi { get; set; }

        public int EczaneId1 { get; set; }
        public int NobetGrupId1 { get; set; }
        public string EczaneAdi1 { get; set; }
        public string NobetGrupAdi1 { get; set; }
        public int EczaneNobetGrupId1 { get; set; }

        public int EczaneId2 { get; set; }
        public int NobetGrupId2 { get; set; }
        public string EczaneAdi2 { get; set; }
        public string NobetGrupAdi2 { get; set; }
        public int EczaneNobetGrupId2 { get; set; }

        public int NobetGorevTipId { get; set; }
        public int HaftaninGunu { get; set; }
    }
}
