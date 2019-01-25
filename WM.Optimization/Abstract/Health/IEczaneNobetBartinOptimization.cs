using OPTANO.Modeling.Optimization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Core.Optimization;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.Optimization.EczaneNobet;

namespace WM.Optimization.Abstract.Health
{
    public interface IEczaneNobetBartinOptimization : IOptimization//, IEczaneNobetKisit
    {
        EczaneNobetSonucModel Solve(BartinDataModel data);
    }
}
