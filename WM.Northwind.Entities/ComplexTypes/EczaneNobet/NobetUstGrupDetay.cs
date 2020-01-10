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
    public class NobetUstGrupDetay : IComplexType
    {
        public int Id { get; set; }
        [Display(Name = "Adı")]
        public string Adi { get; set; }
        [Display(Name = "Açıklama")]
        public string Aciklama { get; set; }
        [Display(Name = "Eczane Odası")]
        public string EczaneOdaAdi { get; set; }
        [Display(Name = "Başlangıç Tarihi")]
        public DateTime BaslangicTarihi { get; set; }
        //Bitiş tarihi null ise, bu eczane o grupta aktiftir.
        [Display(Name = "Bitiş Tarihi")]
        public DateTime? BitisTarihi { get; set; }
        [Display(Name = "Eczane Oda Id")]
        public int EczaneOdaId { get; set; }

        public double Enlem { get; set; }
        public double Boylam { get; set; }
        public int TimeLimit { get; set; }
        public int OneedeGosterilecekEnUzakMesafe { get; set; }
        public bool BaslamaTarihindenOncekiSonuclarGosterilsinMi { get; set; }
    }
}
