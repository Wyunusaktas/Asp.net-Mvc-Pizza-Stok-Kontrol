using System.ComponentModel.DataAnnotations;

namespace StokKontrolSistemi.Models
{
    public class ProfileViewModels
	{
        [Required(ErrorMessage = "Kullanıcı Adı Zorunludur.")]
        public string NewUsername { get; set; }

        [Required(ErrorMessage = "Şifre Zorunludur.")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Şifre Tekrarı Zorunludur.")]
        [Compare(nameof(NewPassword), ErrorMessage = "Şifreler aynı değil")]
        public string NewRePassword { get; set; }

    }
}

