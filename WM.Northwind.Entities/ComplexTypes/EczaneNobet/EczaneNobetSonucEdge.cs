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
    public class EczaneNobetSonucEdge : IComplexType
    {
        public int NobetUstGrupId { get; set; }
        public int From { get; set; }
        public int To { get; set; }
        public double Value { get; set; }
        public double Label { get; set; }
        public string Title { get; set; }
    }
}
