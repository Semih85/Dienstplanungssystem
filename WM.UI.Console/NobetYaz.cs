using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Business.Abstract.Optimization.EczaneNobet;

namespace WM.UI.Console
{
    public class NobetYaz
    {
        private IAntalyaMerkezOptimizationService _antalyaMerkezOptimizationService;

        public NobetYaz(IAntalyaMerkezOptimizationService antalyaMerkezOptimizationService)
        {
            _antalyaMerkezOptimizationService = antalyaMerkezOptimizationService;


        }
    }
}
