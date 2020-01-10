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
    public class NobetGrupGorevTipKisitDetay : IComplexType
    {
        public int Id { get; set; }
        public int NobetUstGrupId { get; set; }
        public int NobetGrupGorevTipId { get; set; }
        public int NobetUstGrupKisitId { get; set; }
        public bool PasifMi { get; set; }
        public double SagTarafDegeri { get; set; }
        public bool VarsayilanPasifMi { get; set; }
        public double SagTarafDegeriVarsayilan { get; set; }
        public string Aciklama { get; set; }
        public string NobetGrupAdi { get; set; }
        public string NobetGorevTipAdi { get; set; }
        public int NobetGorevTipId { get; set; }
        public int NobetGrupId { get; set; }

        public int KisitId { get; set; }
        public double SagTarafDegeriUstGrup { get; set; }
        public double SagTarafDegeriVarsayilanUstGrup { get; set; }
        public bool PasifMiUstGrup { get; set; }
        public bool VarsayilanPasifMiUstGrup { get; set; }
        public string NobetUstGrupAdi { get; set; }
        public string KisitAdi { get; set; }
        public string KisitAdiGosterilen { get; set; }
        public string KisitAciklama { get; set; }
        public string KisitKategoriAdi { get; set; }
        public int KisitKategoriId { get; set; }
        public string AciklamaUstGrup { get; set; }
        public string KisitKategorisi { get; set; }
        public string KisitKodu => KisitId < 10 
            ? $"K0{KisitId} ({KisitKategorisi}, {KisitAdiGosterilen})"
            : $"K{KisitId} ({KisitKategorisi}, {KisitAdiGosterilen})";
        public bool DegerPasifMi { get; set; }
    }
}