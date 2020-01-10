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
    public class NobetUstGrupKisitDetay : IComplexType, ICloneable
    {
        string AdDuzenle(string kisitAdiGosterilen)
        {
            var ilgiliKisitlar = new int[] { 23,38,43
                ,57,75,76,77,78,79,80,81,82,83,20,21,34};

            if (ilgiliKisitlar.Contains(KisitId))
            {
                kisitAdiGosterilen = kisitAdiGosterilen.Substring(3);
            }

            return kisitAdiGosterilen;
        }

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

        [Display(Name = "Kısıt Adı")]
        public string KisitAdiGosterilen { get; set; }

        [Display(Name = "Kısıt Adi Kısa")]
        public string KisitAdiGosterilenKisa => AdDuzenle(KisitAdiGosterilen);

        [Display(Name = "Kısıt Açıklama")]
        public string KisitAciklama { get; set; }

        [Display(Name = "Kısıt Kategori")]
        public string KisitKategoriAdi { get; set; }

        public string KisitKodu => KisitId < 10
            ? $"K0{KisitId}"
            : $"K{KisitId}";

        public string KisitKategorisi => KisitId != 0
            ? $"{(KisitKategoriAdi == "A Genel" ? KisitKategoriAdi.Substring(2) : KisitKategoriAdi)}"
            : "";

        public string KisitTanim => KisitId != 0
            ? $"{KisitKodu}, {KisitKategorisi}, {KisitAdiGosterilen},"
            : "";

        public string KisitTanimKisa => KisitId != 0
            ? $"{KisitKodu}, {KisitKategorisi}, {KisitAdiGosterilenKisa},"
            : "";

        public string KisitAdiUzun => $"{KisitKodu} ({KisitKategorisi}, {KisitAdiGosterilen})";

        public int KisitKategoriId { get; set; }
        public int NobetGrupGorevtipKisitSayisi { get; set; }

        public bool DegerPasifMi { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}