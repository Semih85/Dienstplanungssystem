using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WM.Northwind.Entities.ComplexTypes.EczaneNobet
{
    public class EczaneNobetSonucMobilUygulama
    {
        public int Id { get; set; }
        public int EczaneNobetGrupId { get; set; }
        public string EczaneAdi { get; set; }
        public string NobetGrupAdi { get; set; }
        public Int32 NobetGrupId { get; set; }
        public Int32 NobetUstGrupId { get; set; }

        public string NobetSaatAraligi { get; set; }
        public string NobetGrubu => $"{NobetGrupGorevTipId} {NobetGrupAdi}";
        public bool YayimlandiMi { get; set; }
        public int NobetGrupGorevTipId { get; set; }
        public DateTime Tarih { get; set; }
        public string GunGrupAdi { get; set; }
        public Int32 GunGrupId { get; set; }
    }
}
