using System.Collections.Generic;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;

namespace WM.UI.Mvc.Areas.EczaneNobet.Models
{
    public class NobetAltGrupViewModel
    {
        public List<NobetAltGrupDetay> NobetAltGruplar { get; internal set; }
        public List<NobetAltGruptakiEczane> NobetAltGruptakiEczaneSayilari { get; internal set; }
    }
}