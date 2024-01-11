using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StokKontrolSistemi.Entities
{
    public class UrunSatis
    {
        [Key]
        [Required]
        public int UrunSatisID { get; set; }

        // İlişkiyi ifade eden dış anahtarlar
        [ForeignKey("Urunler")]
        public int UrunID { get; set; }

        [ForeignKey("Satislar")]
        public int SatislarID { get; set; }

        public int Adet { get; set; }

        // İlişkiyi ifade eden navigasyon property
        public virtual Urunler Urunler { get; set; }
        public virtual Satislar Satislar { get; set; }
    }
}
