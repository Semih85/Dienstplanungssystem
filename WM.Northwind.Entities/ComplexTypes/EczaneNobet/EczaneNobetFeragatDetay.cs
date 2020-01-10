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
    public class EczaneNobetFeragatDetay: IComplexType
 { 
        public int EczaneNobetSonucId { get; set; }
        public int EczaneNobetGrupId { get; set; }
        public int NobetUstGrupId { get; set; }
        public int EczaneId { get; set; }
        public int NobetGrupId { get; set; }
        public int TakvimId { get; set; }
        public int NobetGorevTipId { get; set; }
        public int NobetFeragatTipId { get; set; }

        public string EczaneAdiFeragatEden { get; set; }
        public int EczaneNobetGrupIdFeragatEden { get; set; }

        [Display(Name = "Eczane")]
        public string EczaneAdi { get; set; }
        [Display(Name = "Nöbet Grubu")]
        public string NobetGrupAdi { get; set; }
        [Display(Name = "Nöbet Üst Grubu")]
        public string NobetUstGrupAdi { get; set; }
        public DateTime Tarih { get; set; }
        [Display(Name = "Nöbet Görev Tipi")]
        public string NobetGorevTipAdi { get; set; }
        [Display(Name = "Açıklama")]
        public string Aciklama { get; set; }
        [Display(Name = "Nöbet Feragat Tipi")]
        public string NobetFeragatTipAdi { get; set; }
    } 
} 