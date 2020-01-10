using System;
using System.Collections.Generic;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.Optimization.EczaneNobet;

namespace WM.UI.Mvc.Areas.EczaneNobet.Models
{
    public class PivotSonuclarParams
    {
        //public PivotSonuclarParams()
        //{
        //    this.Yil = DateTime.Now.Month == 12 
        //        ? DateTime.Now.Year + 1
        //        : DateTime.Now.Year;
        //    this.Ay = DateTime.Now.Month == 12 
        //        ? 1
        //        : DateTime.Now.Month + 1;
        //}

        public string NobetGrupIdList { get; set; }
        //public int Yil { get; set; }
        //public int Ay { get; set; }
        public string Area { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }
        //public EczaneNobetSonucModel EczaneNobetSonuclar { get; internal set; }
    }
}