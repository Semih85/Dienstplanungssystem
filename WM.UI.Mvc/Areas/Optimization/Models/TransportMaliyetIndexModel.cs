using System.Collections.Generic;
using WM.Northwind.Entities.ComplexTypes;
using WM.Northwind.Entities.ComplexTypes.Transport;

namespace WM.UI.Mvc.Areas.Optimization.Models
{
    public class TransportMaliyetIndexModel
    {
        public List<MaliyetDetail> MaliyetDetail { get; set; }
    }
}