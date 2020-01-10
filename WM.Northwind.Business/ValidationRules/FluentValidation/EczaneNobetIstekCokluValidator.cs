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
    public class EczaneNobetIstekCokluValidator : AbstractValidator<EczaneNobetIstekCoklu>
    {
        public EczaneNobetIstekCokluValidator()
        {
            RuleFor(p => p.EczaneNobetGrupId).NotNull();
            RuleFor(p => p.IstekId).NotEmpty();
            //RuleFor(p => p.MazeretTurId).NotEmpty();
            RuleFor(p => p.BaslangicTarihi).NotEmpty().WithMessage("'Başlangıç tarihi' zorunludur.");
            RuleFor(p => p.BitisTarihi).NotEmpty().WithMessage("'Bitiş tarihi' zorunludur.");
            RuleFor(p => p.Aciklama).NotEmpty().WithMessage("Lütfen 'Açıklama' giriniz.");
        }
    }
}