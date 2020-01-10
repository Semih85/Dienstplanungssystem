using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.Entities.ComplexTypes.EczaneNobet
{
    public class TakvimNobetGrupAltGrup : IComplexType
    {
        public int TakvimId { get; set; }
        public int Yil { get; set; }
        public int Ay { get; set; }
        public int Gun { get; set; }
        public int HaftaninGunu { get; set; }
        public int NobetGrupId { get; set; }
        public int NobetGorevTipId { get; set; }
        public int NobetGunKuralId { get; set; }
        public DateTime Tarih { get; set; }
        public DateTime? BitisTarihi { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public int NobetAltGrupId { get; set; }
    }
}
