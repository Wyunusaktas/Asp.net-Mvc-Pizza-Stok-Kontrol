using Microsoft.AspNetCore.Mvc.Rendering;
using StokKontrolSistemi.Entities;
using System.ComponentModel.DataAnnotations;

namespace StokKontrolSistemi.Models
{
    public class TedarikciViewModel
    {
        [Required(ErrorMessage = "Tedarikci Adı Zorunludur.")]
        public string TedarikciName { get; set; }
        [Required(ErrorMessage = "Tedarikci Adresi girmek Zorunludur.")]
        public string TedarikciAdres { get; set; }
        [Required(ErrorMessage = "Tedarik Edilen Ürünü girmek zorunludur..")]
        public string TedarikEdilenUrun { get; set; }

        [Required(ErrorMessage = "Tedarik Edilen Miktarı Girmek Zorunludur.")]
        public float TedarikMiktari { get; set; }
        public List<SelectListItem> Units { get; set; } = new List<SelectListItem>();
        public string SelectedUnit { get; set; }



    }
}
