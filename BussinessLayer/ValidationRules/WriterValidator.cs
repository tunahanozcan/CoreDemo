using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BussinessLayer.ValidationRules
{
   public class WriterValidator:AbstractValidator<Writer>
    {
        public WriterValidator()
        {
            RuleFor(x => x.WriterName).NotEmpty().WithMessage("Yazar adı soyadı kısmı boş geçilemez.");
            RuleFor(x=>x.WriterMail).NotEmpty().WithMessage("Mail kısmı boş geçilemez.");
            RuleFor(x=>x.WriterPassword).NotEmpty().WithMessage("Şifre boş geçilemez.");
            RuleFor(x => x.WriterName).MinimumLength(2).WithMessage("Lütfen en az 2 karakter girişi yapınız.");
            RuleFor(x=>x.WriterName).MaximumLength(50).WithMessage("Lütfen en fazla 50 karakter girişi yapınız.");
            RuleFor(x => x.WriterPassword).Must(IsPasswordValid).WithMessage("Şifreniz en az 1 büyük harf, 1 küçük harf, 1 rakam içermeli ve en az 6 haneli olmalıdır.");
        }
        private bool IsPasswordValid(string ps)
        {
            try
            {
                Regex regex = new Regex(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$");
                return regex.IsMatch(ps);
            }
            catch 
            {
                return false;
            }
        }
    }
}
