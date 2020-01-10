using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Optimization;
using WM.Northwind.Entities.Concrete;
using WM.Northwind.Entities.Concrete.Optimization.Transport;

namespace WM.Optimization.Abstract.Samples
{
    public interface ITransportOptimization: IOptimization
    {
        void Model();
        TransportSonucModel Solve(TransportDataModel data);
    }
}
