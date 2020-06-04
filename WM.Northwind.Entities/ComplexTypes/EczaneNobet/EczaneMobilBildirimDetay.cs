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
    public class EczaneMobilBildirimDetay: IComplexType
 { 
        public int Id { get; set; }
        public int EczaneId { get; set; }
        public int MobilBildirimId { get; set; }
        [Display(Name = "Bildirim Görme Tarihi")]
        public DateTime? BildirimGormeTarihi { get; set; }
        [Display(Name = "Eczane Adı")]
        public string EczaneAdi { get; set; }
        [Display(Name = "Bildirim Başlığı")]
        public string MobilBildirimBaslik { get; set; }
        [Display(Name = "Bildirim Metni")]
        public string MobilBildirimMetin { get; set; }
        public string BildirimGormeTarihiFormat => String.Format("{0:dd.MM.yyyy H:mm:ss}", BildirimGormeTarihi);

    }
} 