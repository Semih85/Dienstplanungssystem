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
        [Display(Name = "Bildirim Başlığı")]
        public string Baslik { get; set; }
        public string Metin { get; set; }
        [Display(Name = "Açıklama")]
        public string Aciklama { get; set; }
        public int NobetUstGrupId { get; set; }
        [Display(Name = "Bildirim Gönderi Tarihi")]
        public DateTime GonderimTarihi { get; set; }
        [Display(Name = "Nöbet Üst Grubu")]
        public string NobetUstGrupAdi { get; set; }
        public string GonderimTarihiFormat => String.Format("{0:dd.MM.yyyy H:mm:ss}", GonderimTarihi);

    }
} 