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
    public class EczaneNobetMazeretCokluValidator : AbstractValidator<EczaneNobetMazeretCoklu>
    {
        public EczaneNobetMazeretCokluValidator()
        {
            RuleFor(p => p.EczaneNobetGrupId).NotEmpty();
            RuleFor(p => p.EczaneNobetGrupId).NotEmpty();
            RuleFor(p => p.MazeretId).NotEmpty();
            //RuleFor(p => p.MazeretTurId).NotEmpty();
            RuleFor(p => p.BaslangicTarihi).NotEmpty().WithMessage("Lütfen mazeretin başlangıç tarihini giriniz.");
            RuleFor(p => p.BitisTarihi).NotEmpty().WithMessage("Lütfen mazeretin bitiş tarihini giriniz.");
            RuleFor(p => p.Aciklama).NotEmpty().WithMessage("Lütfen mazeretin açıklamasını giriniz.");
        }
    }
}