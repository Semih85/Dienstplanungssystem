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
    public class EczaneNobetDegisimArzDetay: IComplexType
 { 
        public int Id { get; set; }
        public int EczaneNobetSonucId { get; set; }
        public int EczaneNobetGrupId { get; set; }
        public int UserId { get; set; }
        public DateTime KayitTarihi { get; set; }
        public int NobetUstGrupId { get; set; }
        public string Aciklama { get; set; }
        public string EczaneNobetSonucAdi { get; set; }
        public string EczaneNobetGrupAdi { get; set; }
        public string UserAdi { get; set; }

        public int NobetGrupId { get; set; }
        public int EczaneId { get; set; }
        
        [Display(Name = "Nöbet Tarihi")]
        public DateTime NobetTarihi { get; set; }
        public string Kaydeden { get; set; }
        [Display(Name = "Nöbet Grubu")]
        public string NobetGrupAdi { get; set; }
        [Display(Name = "Yeni Nöbetçi")]
        public string EczaneAdi { get; set; }
    } 
} 