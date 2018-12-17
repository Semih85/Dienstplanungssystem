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
    public class EczaneGrupIstatistik : IComplexType
    {
        public int NobetGrupId { get; set; }
        public string NobetGrupAdi { get; set; }
        public int GruptakiEczaneSayisi { get; set; }
        public int EsliEczaneSayisi { get; set; }
        public int EsliEczaneYuzdesi { get; set; }
    }
}