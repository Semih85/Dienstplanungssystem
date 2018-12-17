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
    public class EczaneNobetDegisimDetay : IComplexType
    {
        public int Id { get; set; }
        public int EczaneNobetSonucId { get; set; }
        [Display(Name = "Eski Nöbetçi")]
        public string EskiNobetciEczaneAdi { get; set; }
        public int EczaneNobetGrupId { get; set; }
        public int NobetUstGrupId { get; set; }
        public int NobetGrupId { get; set; }
        public int EczaneId { get; set; }
        public int UserId { get; set; }
        [Display(Name = "Kayıt Tarihi")]
        public DateTime KayitTarihi { get; set; }
        [Display(Name = "Nöbet Tarihi")]
        public DateTime NobetTarihi { get; set; }
        [Display(Name = "Açıklama")]
        public string Aciklama { get; set; }
        public string Kaydeden { get; set; }
        [Display(Name = "Nöbet Grubu")]
        public string NobetGrupAdi { get; set; }
        [Display(Name = "Yeni Nöbetçi")]
        public string EczaneAdi { get; set; }
    }
}