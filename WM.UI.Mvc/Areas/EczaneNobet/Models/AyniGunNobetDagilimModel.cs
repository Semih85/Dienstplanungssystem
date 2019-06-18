using System.Collections.Generic;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;

namespace WM.UI.Mvc
{
    class AyniGunNobetDagilimModel
    {
        public List<AyniGunTutulanNobetDetay> AyniGunNobetTutanEczaneler { get; set; }
        public List<AyniGunTutulanNobetDetay> AyniGunNobetTutanEczanelerOzet { get; set; }
    }
}