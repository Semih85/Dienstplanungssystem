using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.Entities.ComplexTypes.EczaneNobet
{
    public class EczaneGrupCoklu : IComplexType
    {
        public int Id { get; set; }
        public int EczaneGrupTanimId { get; set; }
        //public int NobetGorevTipId { get; set; }
        public int[] EczaneId { get; set; }
        //Aynı gün nöbet yazılması istenmeyen eczaneler için false, aynı gün nöbet yazılması istenen eczaneler için true
        public bool PasifMi { get; set; }
        public bool BirlikteNobetYazilsinMi { get; set; }
    }
}
