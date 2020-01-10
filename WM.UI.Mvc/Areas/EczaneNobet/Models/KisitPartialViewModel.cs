using System.Collections.Generic;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.UI.Mvc.Areas.EczaneNobet.Models
{
    public class KisitPartialViewModel
    {
        public NobetUstGrupKisitDetay Kisit { get; internal set; }
        public string KisitTuru { get; set; }
        public string CardBorderColor { get; set; }
    }
}