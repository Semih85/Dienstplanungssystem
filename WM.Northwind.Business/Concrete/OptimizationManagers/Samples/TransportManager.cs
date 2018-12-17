using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Business.Abstract;
using WM.Northwind.Business.Abstract.Transport;
using WM.Northwind.Entities.Concrete;
using WM.Northwind.Entities.Concrete.Optimization.Transport;
using WM.Optimization.Abstract.Samples;

namespace WM.Northwind.Business.Concrete.OptimizationModels.Samples
{
    public class TransportManager : ITransportService
    {
        ITransportOptimization _transportOptimization;
        ITransportSonucService _transportSonucService;

        public TransportSonucModel Results { get; set; }

        public TransportManager(ITransportOptimization transportOptimization,
                                ITransportSonucService transportSonucService)
        {
            _transportOptimization = transportOptimization;
            _transportSonucService = transportSonucService;
        }

        public TransportSonucModel Solve(TransportDataModel data)
        {
            var _mevcutSonuclar = _transportSonucService.GetList();
            Results = new TransportSonucModel();

            Results = _transportOptimization.Solve(data);

            //önceki sonuçları sil
            foreach (var result in _mevcutSonuclar)
            {
                _transportSonucService.Delete(result.Id);
            }

            //yeni sonuçları ekle
            foreach (var result in Results.ResultModel)
            {
                _transportSonucService.Insert(result);
            }

            return Results;
        }
    }
}
