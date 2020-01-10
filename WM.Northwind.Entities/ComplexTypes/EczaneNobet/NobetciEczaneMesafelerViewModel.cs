using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Entities;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.Entities.ComplexTypes.EczaneNobet
{
    public class NobetciEczaneMesafelerViewModel : IComplexType
    {           
        public DateTime NobetTarihi { get; set; }
        public int Mesafe { get; set; }
    }
}

