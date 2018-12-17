using System.Collections.Generic;

namespace WM.UI.Mvc.Areas.Optimization.Models
{
    public class TransportDataPartialViewModel
    {
        public List<KeyValuePair<int, string>> Categories { get; set; }
        public int CurrentCategory { get; set; }
    }
}