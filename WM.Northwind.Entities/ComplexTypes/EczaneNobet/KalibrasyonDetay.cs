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
    public class KalibrasyonDetay : IComplexType
    {
        public int Id { get; set; }
        public int EczaneNobetGrupId { get; set; }
        public int NobetGrupGorevTipId { get; set; }
        public int NobetGrupId { get; set; }
        public int KalibrasyonTipId { get; set; }
        public int NobetUstGrupGunGrupId { get; set; }
        public int GunGrupId { get; set; }
        public int NobetUstGrupId { get; set; }

        public double Deger { get; set; }

        public string Aciklama { get; set; }
        public string EczaneAdi { get; set; }
        public string NobetGrupAdi { get; set; }
        public string NobetGorevTipAdi { get; set; }
        public string KalibrasyonTipAdi { get; set; }
        public string GunGrupAdi { get; set; }
        public string NobetUstGrupAdi { get; set; }

        public string Durum { get; set; }
    }
}