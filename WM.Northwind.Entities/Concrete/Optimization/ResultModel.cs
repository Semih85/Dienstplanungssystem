using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Optimization;

namespace WM.Northwind.Entities.Concrete.Optimization
{
    public class ResultModel : IResultModel
    {
        /// <summary>
        /// Statik olarak problemin çözülme durumuna göre gösterilmek istenen sonuç için mesaj metni.
        /// </summary>
        public string ResultMessage { get; set; }
        /// <summary>
        /// Obtimizasyon modelinin çözüm durumunu verir. (infeasible, optimal vs)
        /// </summary>
        public string Satatus { get; set; }
        /// <summary>
        /// Amaç fonksiyonu değerini ifade eder
        /// </summary>
        public double ObjectiveValue { get; set; }
    }
}
