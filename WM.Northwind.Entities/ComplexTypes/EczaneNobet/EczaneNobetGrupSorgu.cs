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
    public class EczaneNobetGrupSorgu : IComplexType
    {
        public int NobetGrupGorevTipId { get; set; }
        public int EczaneId { get; set; }
        public DateTime BaslangicTarihi { get; set; }
    }
}
