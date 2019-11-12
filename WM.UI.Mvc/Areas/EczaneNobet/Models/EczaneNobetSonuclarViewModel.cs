using System;
using System.Collections.Generic;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.Optimization.EczaneNobet;

namespace WM.UI.Mvc.Areas.EczaneNobet.Models
{
    public class EczaneNobetSonuclarViewModel
    {
        public List<EczaneNobetSonucListe2> PivotSonuclar { get; set; }
        public List<EczaneNobetIstatistikGunFarki> GunFarklariTumSonuclar { get; set; }
        public List<EczaneNobetIstatistikGunFarkiFrekans> GunFarklariFrekanslar { get; set; }
        public List<EsGrubaAyniGunYazilanNobetler> EsGrubaAyniGunYazilanNobetler { get; internal set; }
        public List<AyniGunTutulanNobetDetay> AltGrupAyniGunNobetTutanEczaneler { get; internal set; }
        public List<EczaneNobetAlacakVerecek> EczaneNobetAlacakVerecek { get; internal set; }
        //AltGrupIleAyniGunNobetTutanEczane
        public DateTime? BaslangicTarihi { get; internal set; }
        public DateTime? BitisTarihi { get; internal set; }
        public EczaneNobetSonucModel EczaneNobetSonuclar { get; internal set; }
        public List<AyniGunTutulanNobetDetay> AyniGunNobetTutanEczaneler { get; internal set; }
        public List<NobetGrupGunDagilim> GunDagilimiMaxMin { get; internal set; }
        public int NobetUstGrupId { get; set; }
        public List<RaporDetay> Raporlar { get; internal set; }
        public DateTime SonNobetTarihi { get; internal set; }
        public bool KapaliEczaneler { get; internal set; }
        public bool SanalNobetler { get; internal set; }
    }
}