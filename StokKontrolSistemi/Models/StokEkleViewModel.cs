using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace StokKontrolSistemi.Models
{
    public class StokEkleViewModel
    {

        [Required(ErrorMessage = "Stok Miktarı alanı zorunludur.")]

        public float StokMiktari { get; set; }

        [Required(ErrorMessage = "Tedarikçi Seçiniz.")]
        public int SelectedTedarikciID { get; set; }
        public List<SelectListItem> Units { get; set; } = new List<SelectListItem>();
        public string SelectedUnit { get; set; }
       
        public List<SelectListItem> Tedarikciler { get; set; } = new List<SelectListItem>();
    }
}
