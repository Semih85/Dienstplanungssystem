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
    public class NobetGrupGorevTipGunKuralDetay : IComplexType
    {
        public int Id { get; set; }
        public int NobetGrupGorevTipId { get; set; }
        public int NobetGorevTipId { get; set; }
        public int NobetGunKuralId { get; set; }
        public int NobetUstGrupId { get; set; }
        public int NobetGrupId { get; set; }
        public int NobetUstGrupGunGrupId { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime? BitisTarihi { get; set; }
        public string NobetGorevTipAdi { get; set; }
        public string NobetGunKuralAdi { get; set; }
        public string NobetGrupAdi { get; set; }
        public string GunGrupAdi { get; set; }
        public int GunGrupId { get; set; }
        public int NobetciSayisi { get; set; }
    }
}