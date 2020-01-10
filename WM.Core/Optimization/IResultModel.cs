using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WM.Core.Optimization
{
    public interface IResultModel
    {
        string ResultMessage { get; set; }
        string Satatus { get; set; }
        double ObjectiveValue { get; set; }
    }
}
