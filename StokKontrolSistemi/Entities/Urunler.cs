using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StokKontrolSistemi.Entities
{
    public class Urunler
    {
        [Key]
        [Required]
        public int UrunID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public String Price { get; set; }  // decimal tipi genellikle para birimi için kullanılır
        public string Picture { get; set; }

        public string Icindekiler { get; set;}

        [ForeignKey("Kategori")]
        public int KategoriID { get; set; }

        public virtual Kategori Kategori { get; set; }
        public virtual ICollection<UrunSatis> UrunSatislar { get; set; }

        [ForeignKey("Tarif")]
        public int? TarifID { get; set; }  // TarifID nullable olarak değiştirildi
        public virtual Tarifler Tarif { get; set; }
    }
}