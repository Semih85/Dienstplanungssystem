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
    public class EczaneNobetSonucPlanlananDetay: IComplexType
 { 
        public int Id { get; set; }
        public int EczaneNobetGrupId { get; set; }
        public int TakvimId { get; set; }
        public int NobetGorevTipId { get; set; }
        public string EczaneNobetGrupAdi { get; set; }
        public string TakvimAdi { get; set; }
        public string NobetGorevTipAdi { get; set; }

    } 
} 