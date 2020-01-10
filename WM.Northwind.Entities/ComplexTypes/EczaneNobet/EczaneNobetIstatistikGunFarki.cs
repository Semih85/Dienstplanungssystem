using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.Entities.ComplexTypes.EczaneNobet
{
    public class EczaneNobetIstatistikGunFarki : IComplexType
    {
        public int EczaneNobetGrupId { get; set; }
        public int NobetGorevTipId { get; set; }
        public string NobetGorevTipAdi { get; set; }
        public int NobetSonucDemoTipId { get; set; }
        public int Index { get; set; }
        public int NobetGrupId { get; set; }
        public DateTime Nobet1Tarih { get; set; }
        public string Nobet1 { get; set; }
        public string Nobet1Gun { get; set; }
        //public long Nobet1 { get; set; }
        public int Nobet1Yil { get; set; }
        public int Nobet1Ay { get; set; }
        public DateTime Nobet2Tarih { get; set; }
        public string Nobet2 { get; set; }
        public string Nobet2Gun { get; set; }
        public int Nobet2Yil { get; set; }
        public int Nobet2Ay { get; set; }
        //public long Nobet2 { get; set; }
        public string NobetGrupAdi { get; set; }
        public int EczaneId { get; set; }
        public string EczaneAdi { get; set; }
        public string GunGrupAdi { get; set; }
        public int GunFarki { get; set; }
        public int AyFarki => (int)Math.Ceiling((double)GunFarki / 30);
        public int HaftaFarki => (int)Math.Ceiling((double)GunFarki / 7);
    }
}
