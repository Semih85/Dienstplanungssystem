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
    public class NobetUstGrupKisitDetay : IComplexType
    {
        public int Id { get; set; }
        public int NobetUstGrupId { get; set; }
        public int KisitId { get; set; }
        [Display(Name = "Sağ Taraf Değeri")]
        public double SagTarafDegeri { get; set; }
        [Display(Name = "Sağ Taraf Değeri Varsayılan")]
        public double SagTarafDegeriVarsayilan { get; set; }
        public bool PasifMi { get; set; }
        public bool VarsayilanPasifMi { get; set; }
        [Display(Name = "Nöbet Üst Grubu")]
        public string NobetUstGrupAdi { get; set; }
        [Display(Name = "Kısıt")]
        public string KisitAdi { get; set; }
        [Display(Name = "Kısıt Adi")]
        public string KisitAdiGosterilen { get; set; }
        [Display(Name = "Kısıt Açıklama")]
        public string KisitAciklama { get; set; }
        [Display(Name = "Kısıt Kategori")]
        public string KisitKategoriAdi { get; set; }
        public string KisitKategorisi => KisitId > 0
            ? $"{(KisitKategoriAdi == "A Genel" ? KisitKategoriAdi.Substring(2) : KisitKategoriAdi)}"
            : "";

        public string KisitTanim => KisitId > 0
            ? $"K{KisitId} ({KisitKategorisi}, {KisitAdiGosterilen}) &raquo;"
            : "";

        public string KisitAdiUzun => $"K{KisitId} ({KisitKategorisi}, {KisitAdiGosterilen})";

        public int KisitKategoriId { get; set; }
    }
}