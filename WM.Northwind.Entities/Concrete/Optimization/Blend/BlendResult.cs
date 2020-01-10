using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WM.Northwind.Entities.Concrete.Optimization.Blend
{
    public class BlendResult
    {
        public double[] MVals { get; set; }
        public double[] RVals { get; set; }
        public double[] SVals { get; set; }
        public double[] IVals { get; set; }
        public double[] EVals { get; set; }
    }
}
