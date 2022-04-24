using Entity.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
	public class BlogValidator:AbstractValidator<Blog>
	{
		public BlogValidator()
		{
			RuleFor(b => b.Title).NotEmpty().WithMessage("Blog başlığı boş bırakılmamalıdır.");
			RuleFor(b => b.Content).NotEmpty().WithMessage("Blog içeriği boş bırakılmamalıdır.");
			RuleFor(b => b.Image).NotEmpty().WithMessage("Blog görseli boş bırakılmamalıdır.");
			RuleFor(b => b.Title).MaximumLength(150).WithMessage("Karakter sayısı 150'yi geçmemelidir.");
			RuleFor(b => b.Title).MinimumLength(5).WithMessage("Karakter sayısı 5'ten az olmamalıdır.");
			RuleFor(b => b.Content).MinimumLength(5).WithMessage("Karakter sayısı 5'ten az olmamalıdır.");
		}
	}
}
