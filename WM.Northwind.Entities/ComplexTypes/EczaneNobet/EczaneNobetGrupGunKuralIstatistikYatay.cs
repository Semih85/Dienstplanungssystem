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
    public class EczaneNobetGrupGunKuralIstatistikYatay : IComplexType
    {
        public int EczaneNobetGrupId { get; set; }
        public int NobetGrupGorevTipId { get; set; }
        public int EczaneId { get; set; }
        public string EczaneAdi { get; set; }
        public string NobetGrupAdi { get; set; }
        public int NobetGrupId { get; set; }
        public int NobetGorevTipId { get; set; }
        public int NobetAltGrupId { get; set; }

        public int BorcluNobetSayisiHaftaIci { get; set; }
        public int BorcluNobetSayisiPazar { get; set; }

        public int NobetSayisiToplam { get; set; }
        public int NobetSayisiHaftaIci { get; set; }

        public int NobetSayisiPazar { get; set; }
        public int NobetSayisiPazartesi { get; set; }
        public int NobetSayisiSali { get; set; }
        public int NobetSayisiCarsamba { get; set; }
        public int NobetSayisiPersembe { get; set; }
        public int NobetSayisiCuma { get; set; }
        public int NobetSayisiCumartesi { get; set; }
        public int NobetSayisiDiniBayram { get; set; }
        public int NobetSayisiMilliBayram { get; set; }
        public int NobetSayisiBayram { get; set; }
        public int NobetSayisiArife { get; set; }
        public int NobetSayisi1Ocak { get; set; }
        
        public DateTime SonNobetTarihi { get; set; }
        public DateTime SonNobetTarihiHaftaIci { get; set; }
        public DateTime SonNobetTarihiPazar { get; set; }
        public DateTime SonNobetTarihiCumartesi { get; set; }
        public DateTime SonNobetTarihiBayram { get; set; }
        public DateTime SonNobetTarihiArife { get; set; }
        public DateTime SonNobetTarihiDiniBayram { get; set; }
        public DateTime SonNobetTarihiMilliBayram { get; set; }
        public DateTime SonNobetTarihi1Ocak { get; set; }
        public int NobetSayisiYilSonu { get; set; }
        public DateTime SonNobetTarihiYilSonu { get; set; }
        public int BorcluNobetSayisiBayram { get; set; }
        public int BorcluNobetSayisiCumartesi { get; set; }
        public int NobetSayisiHaftaSonu { get; set; }
        public DateTime SonNobetTarihiHaftaSonu { get; set; }
    }
}
