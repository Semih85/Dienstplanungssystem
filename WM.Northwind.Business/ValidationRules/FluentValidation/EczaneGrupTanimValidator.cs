using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.Business.ValidationRules.FluentValidation
{
    public class EczaneGrupTanimValidator : AbstractValidator<EczaneGrupTanim>
    {
        public EczaneGrupTanimValidator()
        {
            RuleFor(p => p.Adi).NotEmpty().Length(2, 101);
            RuleFor(p => p.Aciklama).NotEmpty().Length(2, 251);
            RuleFor(p => p.ArdisikNobetSayisi).ExclusiveBetween(-1, 300);
            RuleFor(p => p.AyniGunNobetTutabilecekEczaneSayisi).GreaterThan(0);
            RuleFor(p => p.BaslangicTarihi).NotEmpty();
            //RuleFor(p => p.BitisTarihi).Empty();
        }
    }
}