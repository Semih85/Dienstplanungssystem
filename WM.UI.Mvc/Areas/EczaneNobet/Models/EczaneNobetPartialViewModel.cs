using System.Collections.Generic;

namespace WM.UI.Mvc.Areas.EczaneNobet.Models
{
    public class EczaneNobetPartialViewModel
    {
        public List<KeyValuePair<int, string>> Categories { get; set; }
        public int CurrentCategory { get; set; }
    }
}