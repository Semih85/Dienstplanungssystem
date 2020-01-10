using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WM.Northwind.Entities.Concrete.EczaneNobet;

namespace WM.Northwind.Business.ValidationRules.FluentValidation
{
    public class EczaneNobetMazeretValidator : AbstractValidator<EczaneNobetMazeret>
    {
        public EczaneNobetMazeretValidator()
        {
            RuleFor(p => p.EczaneNobetGrupId).NotEmpty();
            RuleFor(p => p.MazeretId).NotEmpty();
            RuleFor(p => p.TakvimId).NotEmpty();
            RuleFor(p => p.Aciklama).NotEmpty();
        }
    }
}