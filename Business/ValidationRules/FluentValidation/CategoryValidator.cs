using Entity.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class CategoryValidator:AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Kategori adı boş bırakılmamalıdır.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Kategori açıklaması boş bırakılmamalıdır.");
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50).WithMessage("Kategori adı en fazla 50 karakter olmalıdır.");
            RuleFor(x => x.Name).NotEmpty().MinimumLength(2).WithMessage("Kategori adı en az 2 karekter olmalıdır.");
        }
    }
}
