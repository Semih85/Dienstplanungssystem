using System.Collections.Generic;

namespace WM.Northwind.Entities.ComplexTypes.EczaneNobet
{
    public class CozumItereasyon
    {
        public int Yil { get; set; }
        public int Ay { get; set; }
        public List<string> NobetGruplar { get; set; }
        public int IterasyonSayisi { get; set; }
        public int CiftSayisi { get; set; }
    }
}

