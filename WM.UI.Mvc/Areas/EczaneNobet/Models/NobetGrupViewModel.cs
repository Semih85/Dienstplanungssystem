using System.Collections.Generic;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.UI.Mvc.Areas.EczaneNobet.Models
{
    public class NobetGrupViewModel
    {
        public List<NobetGrupDetay> NobetGruplar { get; set; }
        public List<NobetUstGrup> NobetUstGruplar { get; set; }
        public List<EczaneOda> EczaneOdalar { get; internal set; }
        public List<NobetGruptakiAltGrup> NobetGruptakiAltGruplar { get; internal set; }
    }
}