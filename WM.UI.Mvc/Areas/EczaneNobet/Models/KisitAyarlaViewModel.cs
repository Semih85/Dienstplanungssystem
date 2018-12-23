using System.Collections.Generic;
using System.Linq;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;

namespace WM.UI.Mvc.Areas.EczaneNobet.Models
{
    public class KisitAyarlaViewModel
    {
        public List<NobetUstGrupKisitDetay> Kisitlar { get; set; }
        public string[] KisitAyarlaSonuc { get; set; }
    }
}