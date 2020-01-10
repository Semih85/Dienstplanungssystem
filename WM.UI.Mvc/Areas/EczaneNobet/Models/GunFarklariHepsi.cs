using System.Collections.Generic;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;

namespace WM.UI.Mvc.Areas.EczaneNobet.Models
{
    internal class GunFarklariHepsi
    {
        public List<EczaneNobetIstatistikGunFarki> GunFarklariTumSonuclar { get; set; }
        public List<EczaneNobetIstatistikGunFarkiFrekans> GunFarklariFrekanslar { get; set; }
    }
}