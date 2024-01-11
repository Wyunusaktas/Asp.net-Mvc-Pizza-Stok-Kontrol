using Microsoft.AspNetCore.Mvc.Rendering;
using StokKontrolSistemi.Entities;
using System.ComponentModel.DataAnnotations;

namespace StokKontrolSistemi.Models
{
    public class UrunViewModel
    {
        [Required(ErrorMessage = "Ürün Adı Zorunludur.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Açıklma girmek Zorunludur.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Fiyat Zorunludur.")]
        public string Price { get; set; }
        [Required(ErrorMessage = "Resmi girmek zorunludur.")]
        public IFormFile ResimDosyasi { get; set; }
        [Required(ErrorMessage = "Stok adedi zorunludur.")]
        public int Stok { get; set; }
        [Required(ErrorMessage = "Kategori seçmek zorunludur.")]
        public int SecilenKategoriId { get; set; }
       public string Icındekilerr { get; set; }

        public Urunler Data { get; set; } =new Urunler();
        public List<SelectListItem> Kategoriler { get; set; }
        [Required(ErrorMessage = "Recete seçmek zorunludur.")]
        public int SecilenIcindekiID { get; set; }
        public List<SelectListItem> Icındekiler { get; set; }
        public List<MalzemeViewModel> Malzemeler { get; set; }=new List<MalzemeViewModel>();







    }

}
