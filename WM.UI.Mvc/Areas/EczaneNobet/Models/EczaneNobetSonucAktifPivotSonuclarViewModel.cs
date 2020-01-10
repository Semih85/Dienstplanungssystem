using System;
using System.Collections.Generic;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.Optimization.EczaneNobet;

namespace WM.UI.Mvc.Areas.EczaneNobet.Models
{
    public class EczaneNobetSonucAktifPivotSonuclarViewModel
    {
        public List<EczaneNobetSonucListe2> TumSonuclar { get; set; }
        public List<NobetGrupBeklenenGunFarki> NobetGrupBeklenenGunFarklari { get; set; }
        public List<EczaneNobetIstatistikGunFarki> GunFarklari { get; internal set; }
        public List<EczaneNobetIstatistikGunFarkiFrekans> GunFarklariFrekanslar { get; internal set; }
        public EczaneNobetSonucModel EczaneNobetSonuclar { get; internal set; }
        public DateTime BaslamaTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }
    }
}