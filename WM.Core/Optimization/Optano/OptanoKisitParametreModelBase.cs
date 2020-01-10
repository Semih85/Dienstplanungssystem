using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OPTANO.Modeling.Optimization;
using WM.Core.Entities;

namespace WM.Core.Optimization.Optano
{
    public abstract class OptanoKisitParametreModelBase<TModel, TKararDegisKeni> : IKisitParametre //: IKisitParametreBase<TModel, TKararDegisKeni>
        where TModel : class, new()
        where TKararDegisKeni : class, IComplexType, new()
    {
        public abstract OptanoKisitParametreModelBase<TModel, TKararDegisKeni> Clone();

        public TModel Model { get; set; }
        public VariableCollection<TKararDegisKeni> KararDegiskeni { get; set; }
    }
}
