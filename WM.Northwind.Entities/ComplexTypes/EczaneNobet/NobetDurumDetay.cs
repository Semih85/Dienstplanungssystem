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
    public class NobetDurumDetay: IComplexType
 { 
        public int Id { get; set; }
        public int NobetAltGrupId1 { get; set; }
        public int NobetAltGrupId2 { get; set; }
        public int NobetAltGrupId3 { get; set; }

        public int NobetUstGrupId { get; set; }

        public string NobetAltGrupAdi1 { get; set; }
        public string NobetAltGrupAdi2 { get; set; }
        public string NobetAltGrupAdi3 { get; set; }
        
        public int NobetDurumTipId { get; set; }
        public string NobetDurumTipAdi { get; set; }

    } 
} 