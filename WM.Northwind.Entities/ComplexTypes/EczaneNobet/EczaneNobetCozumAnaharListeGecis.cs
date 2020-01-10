using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.Entities.ComplexTypes.EczaneNobet
{
    public class EczaneNobetCozumAnaharListeGecis : IComplexType
    {
        public EczaneNobetCozumAnaharListeGecis()
        {
            EczaneTipId = 1;
        }

        public int TakvimId { get; set; }
        public int EczaneNobetGrupId { get; set; }
        public int NobetGorevTipId { get; set; }
        public int EczaneTipId { get; set; } //1: eski, 0 yeni
        public DateTime Tarih { get; set; }
    }
}

