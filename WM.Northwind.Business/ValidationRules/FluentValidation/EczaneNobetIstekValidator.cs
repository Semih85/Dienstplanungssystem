using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.Business.ValidationRules.FluentValidation
{
    public class EczaneNobetIstekValidator : AbstractValidator<EczaneNobetIstek>
    {
        public EczaneNobetIstekValidator()
        {
            RuleFor(p => p.EczaneNobetGrupId).NotEmpty();
            RuleFor(p => p.IstekId).NotEmpty();
            RuleFor(p => p.TakvimId).NotEmpty().GreaterThan(0);
            RuleFor(p => p.Aciklama).NotEmpty().Length(2, 150);
        }
    }
}