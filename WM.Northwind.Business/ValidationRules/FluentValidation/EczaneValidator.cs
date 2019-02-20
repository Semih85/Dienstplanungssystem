using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.Business.ValidationRules.FluentValidation
{
    public class EczaneValidator : AbstractValidator<Eczane>
    {
        public EczaneValidator()
        {
            RuleFor(p => p.Adi)
                .NotEmpty()
                .WithMessage("Eczane Adı gereklidir.")
                .Length(0, 20);

            RuleFor(p => p.Adres).MaximumLength(150);
            RuleFor(p => p.AcilisTarihi)
                .NotNull()
                .WithMessage("Açılış tarihi gereklidir.");
            
            RuleFor(p => p.MailAdresi).MaximumLength(40).EmailAddress();
            RuleFor(p => p.TelefonNo).MaximumLength(10);
            RuleFor(p => p.WebSitesi).MaximumLength(30);

            RuleFor(p => p.Enlem).NotNull();
            //.LessThanOrEqualTo(0).WithMessage("Enlem en az 0 olabilir.");

            RuleFor(p => p.Boylam).NotNull();
                //.LessThanOrEqualTo(0).WithMessage("Boylam en az 0 olabilir.");
        }
    }
}