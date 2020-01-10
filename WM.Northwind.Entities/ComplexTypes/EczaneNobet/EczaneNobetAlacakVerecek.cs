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
    public class EczaneNobetAlacakVerecek : IComplexType
    {
        public int AnahtarSıra { get; set; }
        public int EczaneNobetGrupId { get; set; }
        public int NobetGrupGorevTipId { get; set; }
        public int NobetGorevTipId { get; set; }
        public string EczaneAdi { get; set; }
        public int NobetGrupId { get; set; }
        public int EczaneId { get; set; }
        public int NobetSayisi { get; set; }
        public int BorcluGunSayisi { get; set; }
        public string NobetGrupAdi { get; set; }
        public string NobetGorevTipAdi { get; set; }
        public DateTime AnahtarTarih { get; set; }
        public DateTime SonNobetTarihi { get; set; }
        public string AnahtarTarihAciklama => String.Format("{0:yyyy MM dd, ddd}", AnahtarTarih);
        public string SonNobetTarihiAciklama => String.Format("{0:yyyy MM dd, ddd}", SonNobetTarihi);
        public string GunGrupAdi { get; set; }
        public int GunGrupId { get; set; }
    }
}
