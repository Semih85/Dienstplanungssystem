using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WM.UI.Mvc.Services
{
    public class MvcModule: NinjectModule
    {
        public override void Load()
        {
            Bind<INobetUstGrupSessionService>().To<NobetUstGrupSessionService>().InSingletonScope();
            Bind<INobetUstGrupKisitSessionService>().To<NobetUstGrupKisitSessionService>().InSingletonScope();
        }
    }
}