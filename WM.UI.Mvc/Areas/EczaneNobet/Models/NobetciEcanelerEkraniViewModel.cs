using System;
using System.Collections.Generic;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.UI.Mvc.Areas.EczaneNobet.Models
{
    public class NobetciEcanelerEkraniViewModel
    {
        public NobetciEcanelerEkraniViewModel()
        {
        }

        
        public int? NobetciEcanelerEkraniTipId { get; set; }

        public NobetciEczane EkraninBulunduguEczane { get; set; }
        public List<NobetciEczane> NobetciEczaneler { get; set; }
        public DateTime Saat { get; internal set; }
    }
}