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
    public class EsGrubaAyniGunYazilanNobetler : IComplexType
    {   
        [Display(Name = "Eczane Grubu")]
        public string EczaneGrupTanimAdi { get; set; }
        [Display(Name = "Eczane Grup Tanım Id")]
        public int EczaneGrupTanimId { get; set; }
        [Display(Name = "Nöbet Tarihi")]
        public string NobetTarihi { get; set; }
        public int TakvimId { get; set; }
        public int EczaneGrupTanimTipId { get; set; }
        public string EczaneGrupTanimTipAdi { get; set; }
        public int NobetUstGrupId { get; set; }
        public int AyniGunNobetTutanEczaneSayisi { get; set; }
    }
}

