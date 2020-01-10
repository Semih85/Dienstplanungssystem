using System.Web.Mvc;
using WM.Northwind.Business.Abstract;
using WM.Northwind.Entities.Concrete.Optimization.Blend;
using WM.UI.Mvc.Areas.Optimization.Models;

namespace WM.UI.Mvc.Areas.Optimization.Controllers
{
    //[Route("{action=Index}")]
    public class BlendController : Controller
    {
        IBlendService _blendOptimization;

        public BlendController(IBlendService blendOptimization)
        {
            _blendOptimization = blendOptimization;
        }

        // GET: Blend
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult ModelCoz()
        {

            var data1 = new BlendDataModel()
            {
                A = 5,
                NbElements = 3,
                NbRaw = 2,
                NbScrap = 2,
                NbIngot = 1,
                Alloy = 71.0,

                Cm = new double[] { 22.0, 10.0, 13.0 },
                Cr = new double[] { 6.0, 5.0 },
                Cs = new double[] { 7.0, 8.0 },
                Ci = new double[] { 9.0 },
                _p = new double[] { 0.05, 0.30, 0.60 },
                _P = new double[] { 0.10, 0.40, 0.80 },
                PRaw = new double[][] {new double[] {0.20, 0.01},
                                           new double[] {0.05, 0.00},
                                           new double[] {0.05, 0.30} },
                PScrap = new double[][] {new double[] {0.00, 0.01},
                                             new double[] {0.60, 0.00},
                                             new double[] {0.40, 0.70} },
                PIngot = new double[][] {new double[] {0.10},
                                             new double[] {0.45},
                                             new double[] {0.45} }
            };

            //TempData["result"] = _blendOptimization.ResultMessage;

            //var model = new BlendResultViewModel()
            //{
            //    blendResultModel = new BlendResultModel()
            //    {
            //        Satatus = _blendOptimization.ResultModel.Satatus,
            //        ObjectiveValue = _blendOptimization.ResultModel.ObjectiveValue,

            //        ResultMessage = _blendOptimization.ResultModel.ResultMessage,
            //        EVals = _blendOptimization.ResultModel.EVals,
            //        IVals = _blendOptimization.ResultModel.IVals,
            //        MVals = _blendOptimization.ResultModel.MVals,
            //        RVals = _blendOptimization.ResultModel.RVals,
            //        SVals = _blendOptimization.ResultModel.SVals
            //    }

            //};

            var model = new BlendResultViewModel()
            {
                blendResultModel = _blendOptimization.Coz(data1)
            };

            return View(model);
        }

    }

}