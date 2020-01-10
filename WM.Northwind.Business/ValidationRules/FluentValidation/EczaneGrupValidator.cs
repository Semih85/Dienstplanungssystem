using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.Business.ValidationRules.FluentValidation
{
    public class EczaneGrupValidator : AbstractValidator<EczaneGrup>
    {
        public EczaneGrupValidator()
        {
            //RuleFor(p => p.EczaneId).NotEmpty();
            //RuleFor(p => p.EczaneGrupTanimId).NotEmpty();
        }
    }
}