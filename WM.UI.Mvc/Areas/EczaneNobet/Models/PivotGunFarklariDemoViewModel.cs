using System.Collections.Generic;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;

namespace WM.UI.Mvc.Areas.EczaneNobet.Models
{
    public class PivotGunFarklariDemoViewModel
    {
        public List<EczaneNobetIstatistikGunFarki> GunFarklariTumSonuclar { get; set; }
        public List<EczaneNobetIstatistikGunFarkiFrekans> GunFarklariFrekanslar { get; set; }
    }
}