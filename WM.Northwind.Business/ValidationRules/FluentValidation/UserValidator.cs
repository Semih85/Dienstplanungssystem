using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Entities.ComplexTypes.EczaneNobet;
using WM.Northwind.Entities.Concrete.Authorization;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.Business.ValidationRules.FluentValidation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(p => p.FirstName)
                    .NotEmpty()
                    .MinimumLength(3)
                    .WithMessage("'Ad' zorunludur.");

            RuleFor(p => p.LastName)
                    .NotEmpty()
                    .MinimumLength(3)
                    .WithMessage("'Soyad' zorunludur.");

            RuleFor(p => p.Email)
                    .NotEmpty()
                    .EmailAddress()
                    .WithMessage("'E-mail' zorunludur.");

            RuleFor(p => p.Password)
                    .NotEmpty()
                    .MaximumLength(64)
                    .WithMessage("'Şifre' zorunludur.");
        }
    }
}