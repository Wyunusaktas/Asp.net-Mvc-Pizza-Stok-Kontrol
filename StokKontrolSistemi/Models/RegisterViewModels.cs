using System.ComponentModel.DataAnnotations;

namespace StokKontrolSistemi.Models
{
	public class RegisterViewModels
	{
		[Required(ErrorMessage = "Kullanıcı Adı Zorunludur.")]
		public string Username { get; set; }
        [Required(ErrorMessage = "Ad Zorunludur.")]
        public string name { get; set; }
        [Required(ErrorMessage = "Soyad Zorunludur.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Şifre Zorunludur.")]
		public string Password { get; set; }

		[Required(ErrorMessage = "Şifre Tekrarı Zorunludur.")]
        [Compare(nameof(Password), ErrorMessage = "Şifreler aynı değil")]
        public string RePassword { get; set; }
       


        [Required(ErrorMessage = "Email Zorunludur.")]
		public string Email { get; set; }
	}
}
