using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.Entities.ComplexTypes.EczaneNobet
{
    public class EczaneNobetSanalSonucDuzenle : IComplexType
    {
        public int EczaneNobetSonucId { get; set; }
        public int UserId { get; set; }
        public int EczaneNobetGrupId { get; set; }
        public DateTime NobetTarihi { get; set; }
        public DateTime KayitTarihi { get; set; }
        public string Aciklama { get; set; }
    }
}

