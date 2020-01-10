using System;
using System.Collections.Generic;
using System.Web.Mvc;
using WM.Northwind.Business.Abstract;
using WM.Northwind.Business.Abstract.Transport;
using WM.Northwind.Entities.Concrete.Optimization.Transport;
using WM.UI.Mvc.Areas.Optimization.Models;

namespace WM.UI.Mvc.Areas.Optimization.Controllers
{
    public class TransportController : Controller
    {
        ITransportService _transportService;
        IFabrikaService _fabrikaService;
        IDepoService _depoService;
        ITransportMaliyetService _transportMaliyetService;
        ITransportSonucService _transportSonucService;

        public TransportController(ITransportService transportService,
                                   IFabrikaService fabrikaService,
                                   IDepoService depoService,
                                   ITransportMaliyetService transportMaliyetService,
                                   ITransportSonucService transportSonucService)
        {
            _transportService = transportService;
            _fabrikaService = fabrikaService;
            _depoService = depoService;
            _transportMaliyetService = transportMaliyetService;
            _transportSonucService = transportSonucService;
        }

        // GET: Optimization/Tranport
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ModelCoz()
        {

            var data = new TransportDataModel()
            {
                Fabrikalar = _fabrikaService.GetList(),
                Depolar = _depoService.GetList(),
                Maliyetler = _transportMaliyetService.GetList(),
                LowerBound = 0,
                UpperBound = 15
            };

            //if (data.Fabrikalar.Count<=3 && data.Depolar.Count <= 3)

            _transportService.Solve(data);

            //TempData["result"] = _blendOptimization.ResultMessage;

            var model = new TransportSonucIndexModel()
            {
                TransportSonucDetails = _transportSonucService.GetSonucDetails(null)
            };

            //return View(model);

            return RedirectToAction("Index", "TransportSonuc", new { area = "Optimization", model });
        }

        [ChildActionOnly]
        public PartialViewResult TransportDataPartialView()
        {
            var categories = new List<KeyValuePair<int, string>>()
            {
                new KeyValuePair<int, string>(1,"Fabrikalar"),
                new KeyValuePair<int, string>(2,"Depolar"),
                new KeyValuePair<int, string>(3,"Taşıma Maliyetleri"),
                new KeyValuePair<int, string>(4,"Sonuçlar")
            };

            var currCtgId = Convert.ToInt32(Request.QueryString["categoryId"]);

            var model = new TransportDataPartialViewModel
            {
                Categories = categories,
                CurrentCategory = currCtgId
            };

            return PartialView("TransportDataPartialView", model);
        }
    }
}