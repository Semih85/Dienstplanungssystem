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

        public double Pazar { get; set; }
        public double Pazartesi { get; set; }
        public double Sali { get; set; }
        public double Carsamba { get; set; }
        public double Persembe { get; set; }
        public double Cuma { get; set; }
        public double Cumartesi { get; set; }
        public double DiniBayram { get; set; }
        public double MilliBayram { get; set; }
        public double ToplamHaftaIci { get; set; }
        public double ToplamCumaCumartesi { get; set; }
        public double ToplamBayram { get; set; }
        public double Toplam { get; set; }
    }
}
