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
    public class EczaneNobetSonucEskiDetay: IComplexType
 { 
        public int Id { get; set; }
        public int EczaneNobetGrupId { get; set; }
        public int EczaneId { get; set; }
        public int NobetUstGrupId { get; set; }
        public int NobetGrupId { get; set; }
        public int HaftaninGunu { get; set; }
        public string EczaneAdi { get; set; }
        public string NobetGrupAdi { get; set; }
        public DateTime Tarih { get; set; }
    } 
} 