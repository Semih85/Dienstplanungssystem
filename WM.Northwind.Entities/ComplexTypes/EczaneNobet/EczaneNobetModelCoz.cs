using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.Entities.ComplexTypes.EczaneNobet
{
    public class EczaneNobetModelCoz : IComplexType
    {
        public int NobetUstGrupId { get; set; }
        public int RolId { get; set; }
        public List<NobetGrupGorevTipDetay> NobetGrupGorevTipler { get; set; }
        public int[] NobetGrupId { get; set; }
        public bool BuAyVeSonrasi { get; set; }

        public int CozumTercih { get; set; }
        public bool SonrakiAylarRasgele { get; set; }

        public int NobetGorevTipId { get; set; }
        public DateTime NobetUstGrupBaslangicTarihi { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }
        public int AyBitis { get; set; }
        public int TimeLimit { get; set; }
        public int CalismaSayisi { get; set; }
    }
}
