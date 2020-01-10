using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.Entities.ComplexTypes.EczaneNobet
{
    public class EczaneNobetSanalSonucEkle : EczaneNobetSonucEkle, IComplexType
    {
        public int UserId { get; set; }
        public DateTime KayitTarihi { get; set; }
        public string Aciklama { get; set; }
    }
}

