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
    public class AyniGunNobetTutanEczane : IComplexType
    {
        public string Grup { get; set; }
        public string G1Eczane { get; set; }
        public string G2Eczane { get; set; }
        public int G1EczaneNobetGrupId { get; set; }
        public int G2EczaneNobetGrupId { get; set; }
        public string AltGrupAdi { get; set; }
        public string GunGrupAdi { get; set; }
        public int AyniGunNobetSayisi { get; set; }
        public int TakvimId { get; set; }
        public DateTime Tarih { get; set; }
        public string TarihAciklama => String.Format("{0:yyy MM dd, ddd}", Tarih);
        public string YilAy => String.Format("{0:yy-MM}", Tarih);
        public string EczaneBirlesim => $"{G1EczaneNobetGrupId}{G2EczaneNobetGrupId}";

        public string G1NobetGrupAdi { get; set; }
        public string G2NobetGrupAdi { get; set; }
        public string G2NobetAltGrupAdi { get; set; }
        public string G1NobetAltGrupAdi { get; set; }
        public int EnSonAyniGunNobetTakvimId { get; set; }
    }
}
