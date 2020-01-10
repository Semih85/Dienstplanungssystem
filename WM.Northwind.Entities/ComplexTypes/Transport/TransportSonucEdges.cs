using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WM.Northwind.Entities.ComplexTypes.Transport
{
    public class TransportSonucEdges
    {
        public string From { get; set; }
        public string To { get; set; }
        public double Value { get; set; }
        public double Label { get; set; }
        public string Title { get; set; }
    }
}

//{ from: 1, to: 5, value: 1,  label: 1,  title: 'FA->DE: 1 adet' },