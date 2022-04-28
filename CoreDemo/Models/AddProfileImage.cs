using Microsoft.AspNetCore.Http;

namespace CoreDemo.Models
{
	public class AddProfileImage
	{
        public int Id { get; set; }
        public string FullName { get; set; }
        public string About { get; set; }
        public IFormFile Image { get; set; } // Image daki değişikliği entity üzerinde yapmak istemediğim için, UI katmanında yeni bir model tanımladım.
        //Dosyadan seçilecek bir veri tipi olabilmesi için
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Status { get; set; }
    }
}
