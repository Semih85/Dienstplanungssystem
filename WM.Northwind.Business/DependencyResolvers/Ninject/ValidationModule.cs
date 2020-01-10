using FluentValidation;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Business.ValidationRules.FluentValidation;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.Business.DependencyResolvers.Ninject
{
    public class ValidationModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IValidator<LoginItem>>().To<LoginItemValidator>().InSingletonScope();
            //Bind<IValidator<Eczane>>().To<EczaneValidator>().InSingletonScope();
            Bind<IValidator<EczaneGrup>>().To<EczaneGrupValidator>().InSingletonScope();
            Bind<IValidator<EczaneGrupTanim>>().To<EczaneGrupTanimValidator>().InSingletonScope();
            Bind<IValidator<EczaneNobetGrup>>().To<EczaneNobetGrupValidator>().InSingletonScope();
            Bind<IValidator<EczaneNobetGrupCoklu>>().To<EczaneNobetGrupCokluValidator>().InSingletonScope();
            Bind<IValidator<EczaneNobetMazeret>>().To<EczaneNobetMazeretValidator>().InSingletonScope();
            Bind<IValidator<EczaneNobetIstek>>().To<EczaneNobetIstekValidator>().InSingletonScope();
            Bind<IValidator<EczaneNobetMazeretCoklu>>().To<EczaneNobetMazeretCokluValidator>().InSingletonScope();
            Bind<IValidator<EczaneNobetIstekCoklu>>().To<EczaneNobetIstekCokluValidator>().InSingletonScope();
        }
    }
}
