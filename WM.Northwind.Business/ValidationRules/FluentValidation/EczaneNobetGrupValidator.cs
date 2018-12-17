using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.Business.ValidationRules.FluentValidation
{
    public class EczaneNobetGrupValidator : AbstractValidator<EczaneNobetGrup>
    {
        public EczaneNobetGrupValidator()
        {
            RuleFor(p => p.EczaneId).NotEmpty();
            RuleFor(p => p.NobetGrupGorevTipId).NotEmpty();
            RuleFor(p => p.BaslangicTarihi).NotEmpty();
            //RuleFor(p => p.BitisTarihi);
            RuleFor(p => p.Aciklama).NotEmpty().WithMessage("Lütfen gruba giriş açıklamasını giriniz.").Length(2, 200);
        }
    }
}