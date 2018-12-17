using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.Entities.ComplexTypes.EczaneNobet
{
    public class EczaneNobetIstekListe : IComplexType
    {   
        public int EczaneId { get; set; }
        public int NobetGrupId { get; set; }
        public int TakvimId { get; set; }
        public int IstekId { get; set; }
        public int IstekTurId { get; set; }
    }
}
