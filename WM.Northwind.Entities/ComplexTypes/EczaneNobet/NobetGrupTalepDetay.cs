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
    public class NobetGrupTalepDetay: IComplexType
 { 
        public int Id { get; set; }
        public int TakvimId { get; set; }
        [Display(Name = "Nöbetçi Sayısı")]
        public int NobetciSayisi { get; set; }
        public int NobetGrupGorevTipId { get; set; }
        public DateTime Tarih { get; set; }

        public int NobetGrupId { get; set; }
        public int NobetGorevTipId { get; set; }
        [Display(Name = "Nöbet Grubu")]
        public string NobetGrupAdi { get; set; }
        [Display(Name = "Nöbet Görev Tipi")]
        public string NobetGorevTipAdi { get; set; }
        public int NobetUstGrupId { get; set; }
    }
} 