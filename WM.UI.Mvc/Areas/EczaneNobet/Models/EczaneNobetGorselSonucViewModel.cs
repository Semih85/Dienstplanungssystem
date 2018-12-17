using System.Collections.Generic;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.UI.Mvc.Areas.EczaneNobet.Models
{
    public class EczaneNobetGorselSonucViewModel
    {
        public List<EczaneNobetSonucNode> EczaneNobetSonucNodes { get; internal set; }
        public List<EczaneNobetSonucEdge> EczaneNobetSonucEdges { get; internal set; }
        public List<EczaneGrupNode> EczaneGrupNodes { get; internal set; }
        public List<EczaneGrupEdge> EczaneGrupEdges { get; internal set; }
    }
}