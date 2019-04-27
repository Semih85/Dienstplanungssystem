using System;
using System.Collections.Generic;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;

namespace WM.UI.Mvc.Areas.EczaneNobet.Models
{
    public class EczaneNobetSonucViewJsonModel
    {
        public List<EczaneNobetSonucDagilimlar> PivotSonuclar { get; set; }
        public List<EczaneNobetIstatistikGunFarki> GunFarklariTumSonuclar { get; set; }
        public List<EczaneNobetIstatistikGunFarkiFrekans> GunFarklariFrekanslar { get; set; }
        public List<EsGrubaAyniGunYazilanNobetler> EsGrubaAyniGunYazilanNobetler { get; internal set; }
        public List<AyniGunNobetTutanEczane> AltGrupAyniGunNobetTutanEczaneler { get; internal set; }
        public List<AyniGunNobetTutanEczane> AyniGunNobetTutanEczaneler { get; internal set; }
        public List<EczaneNobetAlacakVerecek> EczaneNobetAlacakVerecek { get; internal set; }
        //AltGrupIleAyniGunNobetTutanEczane
        public DateTime? BaslangicTarihi { get; internal set; }
        public DateTime? BitisTarihi { get; internal set; }
        public List<NobetGrupGunDagilim> GunDagilimiMaxMin { get; internal set; }
        public int NobetUstGrupId { get; internal set; }
        public List<KalibrasyonDetay> KalibrasyonluToplamlar { get; internal set; }
        public IEnumerable<EczaneNobetSonucListe2> SonuclarPlanlananVeGercek { get; internal set; }
        public List<AyniGunNobetTutanEczane> AyniGunNobetTutanEczanelerOzet { get; internal set; }
    }
}