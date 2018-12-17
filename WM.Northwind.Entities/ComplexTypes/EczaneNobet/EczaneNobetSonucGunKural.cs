using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.Entities.ComplexTypes.EczaneNobet
{
    public class EczaneNobetSonucGunKural : IComplexType
    {
        public int Yil { get; set; }
        public int Ay { get; set; }
        public int EczaneNobetGrupId { get; set; }
        public int EczaneId { get; set; }
        public int NobetGrupId { get; set; }
        public int NobetGorevTipId { get; set; }
        public int TakvimId { get; set; }
        public int NobetGunKuralId { get; set; }
    }
}