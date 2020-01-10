using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;
using WM.Northwind.Entities.Concrete.EczaneNobet;


namespace WM.Northwind.Entities.ComplexTypes.EczaneNobet
{
    public class AyniGunTutulanNobetDetay : IComplexType
    {
        public int Id { get; set; }
        public string Grup { get; set; }
        public int NobetUstGrupId { get; set; }
        public string NobetUstGrupAdi { get; set; }
        public int EczaneNobetGrupId1 { get; set; }
        public int EczaneId1 { get; set; }
        public string EczaneAdi1 { get; set; }
        public int NobetGrupId1 { get; set; }
        public int NobetGrupGorevTipId1 { get; set; }
        public int NobetGrupGorevTipId2 { get; set; }
        public string NobetGrupAdi1 { get; set; }
        //public int NobetUstGrupId1 { get; set; }
        //public string NobetUstGrupAdi1 { get; set; }

        public int EczaneNobetGrupId2 { get; set; }
        public int EczaneId2 { get; set; }
        public string EczaneAdi2 { get; set; }
        public int NobetGrupId2 { get; set; }
        public string NobetGrupAdi2 { get; set; }
        //public int NobetUstGrupId2 { get; set; }
        //public string NobetUstGrupAdi2 { get; set; }

        public int EnSonAyniGunNobetTakvimId { get; set; }
        public int AyniGunNobetSayisi { get; set; }
        public int AyniGunNobetTutamayacaklariGunSayisi { get; set; }
        public DateTime EnSonAyniGunNobetTarihi { get; set; }
        public string GunGrupAdi { get; set; }
        public string NobetAltGrupAdi1 { get; set; }
        public string NobetAltGrupAdi2 { get; set; }

        public string TarihAciklama => String.Format("{0:yyy MM dd, ddd}", EnSonAyniGunNobetTarihi);
        public string YilAy => String.Format("{0:yy-MM}", EnSonAyniGunNobetTarihi);
        public string EczaneBirlesim => $"{EczaneNobetGrupId1}-{EczaneNobetGrupId2}";

        public string NobetGorevTipAdi2 { get; set; }
        public string NobetGorevTipAdi1 { get; set; }
        public int NobetGorevTipId1 { get; set; }
        public int NobetGorevTipId2 { get; set; }
    }
}