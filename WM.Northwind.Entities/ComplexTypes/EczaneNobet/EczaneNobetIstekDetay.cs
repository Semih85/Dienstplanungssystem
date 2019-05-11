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
    public class EczaneNobetIstekDetay : IComplexType
    {
        public int Id { get; set; }
        public int EczaneNobetGrupId { get; set; }
        public int NobetGrupId { get; set; }
        public int NobetUstGrupId { get; set; }
        public int EczaneId { get; set; }
        public int IstekId { get; set; }
        public int TakvimId { get; set; }
        public int Yil { get; set; }
        public int Ay { get; set; }
        public int Gun { get; set; }

        public int NobetGorevTipId { get; set; }
        public int NobetGrupGorevTipId { get; set; }
        [Display(Name = "Görev Tipi")]
        public string NobetGorevTipAdi { get; set; }

        [Display(Name = "Grup")]
        public string NobetGrupAdi { get; set; }
        [Display(Name = "Eczane")]
        public string EczaneAdi { get; set; }
        [Display(Name = "İstek")]
        public string IstekAdi { get; set; }
        public int IstekTurId { get; set; }
        [Display(Name = "İstek Türü")]
        public string IstekTuru { get; set; }
        public DateTime Tarih { get; set; }
        public string TarihKisa => Tarih.ToShortDateString();
        public string TarihUzun => Tarih.ToLongDateString();

        [Display(Name = "Açıklama")]
        public string Aciklama { get; set; }
    }
}
