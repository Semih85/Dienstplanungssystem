using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Business.Abstract;
using WM.Northwind.Entities.Concrete;
using WM.Northwind.Entities.Concrete.Optimization.Blend;
using WM.Optimization.Abstract.Samples;
using WM.Optimization.Concrete.IbmCplex.Samples;

namespace WM.Northwind.Business.Concrete.OptimizationModels
{
    public class BlendManager : IBlendService
    {
        IBlendOptimization _blendOptimization;

        public BlendManager(IBlendOptimization blendOptimization)
        {
            _blendOptimization = blendOptimization;
        }

        public BlendDataModel _data { get; set; }
        public BlendResultModel ResultModel { get; set; }

        public BlendResultModel Coz(BlendDataModel data)
        {
            _data = data;
            ResultModel = _blendOptimization.Solve(_data);

            return ResultModel;
        }
    }
}
