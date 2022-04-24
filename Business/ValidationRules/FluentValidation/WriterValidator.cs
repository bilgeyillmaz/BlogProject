using Entity.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class WriterValidator:AbstractValidator<Writer>
    {
        public WriterValidator()
        {
            RuleFor(w=> w.FullName).NotEmpty().WithMessage("Yazar Adı-Soyadı kısmı boş geçilemez."); 
            RuleFor(w=> w.Email).NotEmpty().WithMessage("Yazar E-Posta Adresi boş geçilemez."); 
            RuleFor(w=> w.Password).NotEmpty().WithMessage("Yazar Şifresi boş geçilemez.");
            RuleFor(w => w.FullName).MinimumLength(2).WithMessage("Lütfen en az 2 karakter girişi yapınız.");
            RuleFor(w => w.FullName).MaximumLength(50).WithMessage("Lütfen en fazla 50 karakter girişi yapınız.");


        }
    }
}
