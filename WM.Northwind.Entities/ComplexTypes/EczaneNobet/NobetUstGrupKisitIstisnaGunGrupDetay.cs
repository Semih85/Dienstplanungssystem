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
    public class NobetUstGrupKisitIstisnaGunGrupDetay: IComplexType
 { 
        public int Id { get; set; }
        public int NobetUstGrupKisitId { get; set; }
        public int NobetUstGrupGunGrupId { get; set; }
        public string Aciklama { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime? BitisTarihi { get; set; }
        public string NobetUstGrupKisitAdi { get; set; }
        public string NobetUstGrupGunGrupAdi { get; set; }
        public int GunGrupId { get; set; }
        public int KisitId { get; set; }
        public string GunGrupAdi { get; set; }
        public string KisitAdi { get; set; }


    }
} 