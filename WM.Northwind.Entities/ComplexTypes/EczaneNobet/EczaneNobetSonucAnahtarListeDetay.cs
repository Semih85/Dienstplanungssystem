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
    public class EczaneNobetSonucAnahtarListeDetay: IComplexType
 { 
        public int Id { get; set; }
        public int EczaneNobetGrupId { get; set; }
        public int NobetUstGrupGunGrupId { get; set; }
        public bool KullanildiMi { get; set; }
        public int AnahtarListeTanimId { get; set; }
        public string EczaneNobetGrupAdi { get; set; }
        public string NobetUstGrupGunGrupAdi { get; set; }
        public string AnahtarListeTanimAdi { get; set; }

    } 
} 