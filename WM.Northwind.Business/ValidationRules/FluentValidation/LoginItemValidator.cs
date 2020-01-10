using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.Business.ValidationRules.FluentValidation
{
    public class LoginItemValidator : AbstractValidator<LoginItem>
    {
        public LoginItemValidator()
        {
            RuleFor(p => p.Email)
                    .NotEmpty()
                    .EmailAddress()
                    //.WithMessage("E-mail zorunludur-----")
                    ;

            RuleFor(p => p.Password)
                    .NotEmpty()
                    .MaximumLength(64);
        }
    }
}