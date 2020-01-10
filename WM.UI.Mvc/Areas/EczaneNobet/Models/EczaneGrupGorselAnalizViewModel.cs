using System.Collections.Generic;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;

namespace WM.UI.Mvc.Areas.EczaneNobet.Models
{
    public class EczaneGrupGorselAnalizViewModel
    {
        public EczaneGrupGorselAnalizViewModel()
        {
        }

        public List<EczaneGrupNode> EczaneGrupNodes { get; set; }
        public List<EczaneGrupEdge> EczaneGrupEdges { get; set; }
    }
}