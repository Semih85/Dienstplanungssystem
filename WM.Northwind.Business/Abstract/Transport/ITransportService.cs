using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Entities.Concrete;
using WM.Northwind.Entities.Concrete.Optimization.Transport;

namespace WM.Northwind.Business.Abstract.Transport
{
    public interface ITransportService
    {
        //TransportResultModel ResultModel { get; set; }

        TransportSonucModel Solve(TransportDataModel data);
    }
}
