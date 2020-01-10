using System.Collections.Generic;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.UI.Mvc.Areas.EczaneNobet.Models
{
    public class EczaneUzaklikMatrisViewModel
    {
        public EczaneUzaklikMatrisViewModel()
        {
        }

        public int NobetUstGrupId { get; set; }
        public List<Eczane> Eczaneler { get; set; }
        public List<EczaneUzaklikMatrisDetay> Uzakliklar { get; internal set; }
    }
}