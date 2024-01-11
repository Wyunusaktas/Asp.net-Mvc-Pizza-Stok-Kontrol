using System.ComponentModel.DataAnnotations;

namespace StokKontrolSistemi.Models
{
    public class TarifModel
    {
        [Required(ErrorMessage = "Tarif Adı Zorunludur.")]
        public string TarifAdi { get; set; }
    }
}
