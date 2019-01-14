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
    public class NobetGrupGorevTipTakvimOzelGunDetay : IComplexType
    {
        public int Id { get; set; }
        public int TakvimId { get; set; }
        public int NobetUstGrupGunGrupId { get; set; }
        public int GunGrupId { get; set; }
        public string GunGrupAdi { get; set; }
        public int NobetGunKuralId { get; set; }
        public int NobetGrupGorevTipGunKuralId { get; set; }
        public int NobetGrupGorevTipId { get; set; }
        public int NobetGorevTipId { get; set; }
        public string NobetGorevTipAdi { get; set; }
        public int NobetOzelGunId { get; set; }
        public int NobetUstGrupId { get; set; }
        public int NobetGrupId { get; set; }
        public string NobetGrupAdi { get; set; }
        public DateTime Tarih { get; set; }
        public string NobetGunKuralAdi { get; set; }
        public string NobetGrupGorevTipGunKuralAdi { get; set; }
        public string NobetOzelGunAdi { get; set; }
        public bool FarkliGunGosterilsinMi { get; set; }//true ise NobetGunKuralId, değilse -normalde- NobetGrupGorevTipGunKuralId
        public double AgirlikDegeri { get; set; }

    }
}