using System.Collections.Generic;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.UI.Mvc.Areas.EczaneNobet.Models
{
    public class EczaneGrupTanimDetaylarViewModel
    {
        public EczaneGrupTanimDetaylarViewModel()
        {
        }

        public string Keyword { get; set; }
        public int? EczaneGruptanimTipId { get; set; }

        public EczaneGrupTanimDetay EczaneGrupTanimDetay { get; set; }
        public List<EczaneGrupDetay> EczaneGrupDetaylar { get; set; }
    }
}