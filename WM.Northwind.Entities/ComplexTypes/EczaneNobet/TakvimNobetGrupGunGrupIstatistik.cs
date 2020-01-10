using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.Entities.ComplexTypes.EczaneNobet
{
    public class TakvimNobetGrupGunGrupIstatistik : IComplexType
    {   
        public int NobetGrupId { get; set; }
        public int NobetGorevTipId { get; set; }
        public int NobetGrupGorevTipId { get; set; }
        public DateTime IstatistikBaslamaTarihi { get; set; }
        public DateTime IstatistikBitisTarihi { get; set; }
        public int TalepEdilenNobetciSayisi { get; set; }
        public int GunSayisi { get; set; }
        public string GunGrupAdi { get; set; }
        public int GunGrupId { get; set; }
        public DateTime? NobetGunKuralKapanmaTarihi { get; set; }
        public string NobetGorevTipAdi { get; set; }
        public string NobetGrupAdi { get; set; }
    }
}
