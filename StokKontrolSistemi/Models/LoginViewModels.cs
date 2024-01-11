using System.ComponentModel.DataAnnotations;

namespace StokKontrolSistemi.Models
{
    public class LoginViewModels
	{
		[Required(ErrorMessage ="Email  Zorunludur.")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Şifre Zorunludur.")]
		public string Password { get; set; }
	}
}

