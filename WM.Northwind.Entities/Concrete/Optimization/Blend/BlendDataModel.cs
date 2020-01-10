using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WM.Northwind.Entities.Concrete.Optimization.Blend
{
    public class BlendDataModel
    {
        public int A { get; set; }
        public int NbElements { get; set; }
        public int NbRaw { get; set; }
        public int NbScrap { get; set; }
        public int NbIngot { get; set; }
        public double Alloy { get; set; }

        public double[] Cm { get; set; }
        public double[] Cr { get; set; }
        public double[] Cs { get; set; }
        public double[] Ci { get; set; }
        public double[] _p { get; set; }
        public double[] _P { get; set; }

        public double[][] PRaw { get; set; }
        public double[][] PScrap { get; set; }
        public double[][] PIngot { get; set; }
    }
}
