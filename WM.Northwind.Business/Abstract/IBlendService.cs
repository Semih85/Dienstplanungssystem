using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Optimization;
using WM.Northwind.Entities.Concrete;
using WM.Northwind.Entities.Concrete.Optimization.Blend;

namespace WM.Northwind.Business.Abstract
{
    public interface IBlendService
    {
        //BlendResultModel ResultModel { get; set; }

        BlendResultModel Coz(BlendDataModel data);
    }
}
