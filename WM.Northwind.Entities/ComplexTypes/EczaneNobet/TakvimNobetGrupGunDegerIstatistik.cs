using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.Entities.ComplexTypes.EczaneNobet
{
    public class TakvimNobetGrupGunDegerIstatistik : IComplexType
    {   
        //public int Yil { get; set; }        
        public int NobetGrupId { get; set; }
        public int NobetGorevTipId { get; set; }
        public int NobetGrupGorevTipId { get; set; }
        public int NobetGunKuralId { get; set; }
        public string NobetGunKuralAdi { get; set; }

        //=> NobetGunKuralId == 1
        //? "Pazar"
        //: NobetGunKuralId == 2
        //? "Pazartesi"
        //: NobetGunKuralId == 3
        //? "Salı"
        //: NobetGunKuralId == 4
        //? "Çarşamba"
        //: NobetGunKuralId == 5
        //? "Perşembe"
        //: NobetGunKuralId == 6
        //? "Cuma"
        //: NobetGunKuralId == 7
        //? "Cumartesi"
        //: NobetGunKuralId == 8
        //? "Dini"
        //: NobetGunKuralId == 9
        //? "Milli"
        //: "Diğer";
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
