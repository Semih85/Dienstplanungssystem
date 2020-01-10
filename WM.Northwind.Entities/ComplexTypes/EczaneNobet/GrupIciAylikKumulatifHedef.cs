using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.Entities.ComplexTypes.EczaneNobet
{
    public class GrupIciAylikKumulatifHedef
    {
        public int Yil { get; set; }
        public int Ay { get; set; }
        public int NobetGrupId { get; set; }
        public int NobetGorevTipId { get; set; }

        public int Pazar { get; set; }
        public int Pazartesi { get; set; }
        public int Sali { get; set; }
        public int Carsamba { get; set; }
        public int Persembe { get; set; }
        public int Cuma { get; set; }
        public int Cumartesi { get; set; }
        public int DiniBayram { get; set; }
        public int MilliBayram { get; set; }
        public int ToplamHaftaIci { get; set; }
        public int ToplamCumaCumartesi { get; set; }
        public int ToplamBayram { get; set; }
        public int Toplam { get; set; }
        public int Arife { get; set; }
        public int NobetGrupGorevTipId { get; set; }
    }
}
