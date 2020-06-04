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
    public class MobilBildirimDetay: IComplexType
 { 
        public int Id { get; set; }
        public string Baslik { get; set; }
        public string Metin { get; set; }
        public string Aciklama { get; set; }
        public int NobetUstGrupId { get; set; }
        public DateTime GonderimTarihi { get; set; }
        public string NobetUstGrupAdi { get; set; }
        public string GonderimTarihiFormat => String.Format("{0:dd.MM.yyyy H:mm:ss}", GonderimTarihi);

    }
} 