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
    public class AltGrupIleAyniGunNobetTutanEczane : IComplexType
    {
        public int NobetAltGrupId { get; set; }
        public string NobetAltGrupAdi { get; set; }
        public string Y1EczaneAdi { get; set; }
        public string Y2EczaneAdi { get; set; }
        public string Y3EczaneAdi { get; set; }
        public string Y4EczaneAdi { get; set; }
        public int TakvimId { get; set; }
        public DateTime Tarih { get; set; }
    }
}

