using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WM.Northwind.Entities.ComplexTypes.Transport
{
    public class TransportSonucNodes
    {
        public string Id { get; set; }
        public string Label { get; set; }
        public int Value { get; set; }
        public int Group { get; set; }
        public int Level { get; set; }
    }
}

//id: 1, value: 15, label: 'A', group: 0, level:0