using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.Entities.ComplexTypes.EczaneNobet
{
    public class EczaneNobetIstatistik : IComplexType
    {
        public int EczaneNobetGrupId { get; set; }
        public int NobetGorevTipId { get; set; }
        public int NobetGrupId { get; set; }
        public int EczaneId { get; set; }

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
    }
}
