using System.Collections.Generic;
using WM.Northwind.Entities.Concrete;
using WM.Northwind.Entities.Concrete.Optimization.Transport;

namespace WM.UI.Mvc.Areas.Optimization.Models
{
    public class TransportResultViewModel
    {
        public List<TransportSonuc> TransportSonuclar { get; set; }
    }
}