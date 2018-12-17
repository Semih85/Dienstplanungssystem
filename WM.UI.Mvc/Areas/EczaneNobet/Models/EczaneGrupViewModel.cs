using System.Collections.Generic;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;

namespace WM.UI.Mvc.Areas.EczaneNobet.Models
{
    public class EczaneGrupViewModel
    {
        public EczaneGrupViewModel()
        {
        }

        public List<EczaneGrupDetay> EczaneGrupDetaylar { get; set; }
    }
}