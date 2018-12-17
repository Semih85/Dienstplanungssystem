using System.Collections.Generic;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;

namespace WM.UI.Mvc.Areas.EczaneNobet.Models
{
    public class EczaneNobetSonucViewModel
    {        
        public List<EczaneNobetSonucListe2> PivotSonuclar { get; set; }
        public List<EczaneNobetIstatistikGunFarki> GunFarklariTumSonuclar { get; set; }
        public List<EczaneNobetIstatistikGunFarkiFrekans> GunFarklariFrekanslar { get; set; }
        public List<EsGrubaAyniGunYazilanNobetler> EsGrubaAyniGunYazilanNobetler { get; internal set; }
    }
}