using System.Collections.Generic;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;

namespace WM.UI.Mvc.Areas.EczaneNobet.Models
{
    public class KuralKategorilerPartialViewModel
    {
        public KuralKategorilerPartialViewModel()
        {
        }

        public string TabId { get; set; }
        public string KisitTuru { get; set; }
        public IEnumerable<NobetUstGrupKisitDetay> NobetUstGrupKisitDetaylar { get; set; }
    }
}