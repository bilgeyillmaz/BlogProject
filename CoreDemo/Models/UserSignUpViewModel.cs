using System.ComponentModel.DataAnnotations;

namespace CoreDemo.Models
{
    public class UserSignUpViewModel
    {
        [Display(Name="İsim Soyad")]
        [Required(ErrorMessage ="Lütfen isim ve soyad bilgilerinizi giriniz.")]
        public string FullName { get; set; }

        [Display(Name = "Şifre")]
        [Required(ErrorMessage = "Lütfen şifre bilgilerinizi giriniz.")]
        public string Password { get; set; }

        [Display(Name = "Şifre Tekrarı")]
        [Compare("Password", ErrorMessage = "Girdiğiniz şifreler uyuşmamaktadır.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "E-Posta Adresi")]
        [Required(ErrorMessage = "Lütfen E-posta adresinizi giriniz.")]
        public string Email { get; set; }

        [Display(Name = "Kullanıcı Adı")]
        [Required(ErrorMessage = "Lütfen kullanıcı adınızı giriniz.")]
        public string UserName { get; set; }

    }
}
