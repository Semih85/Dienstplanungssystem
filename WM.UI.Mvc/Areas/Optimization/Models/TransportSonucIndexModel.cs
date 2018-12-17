using System.Collections.Generic;
using WM.Northwind.Entities.ComplexTypes;
using WM.Northwind.Entities.ComplexTypes.Transport;
using WM.Northwind.Entities.Concrete;

namespace WM.UI.Mvc.Areas.Optimization.Models
{
    public class TransportSonucIndexModel
    {
        public List<TransportSonucDetail> TransportSonucDetails { get; set; }
        public List<TransportSonucNodes> TransportNodes { get; internal set; }
        public List<TransportSonucEdges> TransportEdges { get; internal set; }
    }
}