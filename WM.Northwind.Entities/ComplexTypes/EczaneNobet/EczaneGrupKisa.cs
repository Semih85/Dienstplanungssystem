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
    public class EczaneGrupKisa : IComplexType
    {
        public int EczaneGrupTanimId { get; set; }
        public string EczaneGrupTanimAdi { get; set; }
        public int NobetGrupId { get; set; }
        public string NobetGrupAdi { get; set; }
    }
}