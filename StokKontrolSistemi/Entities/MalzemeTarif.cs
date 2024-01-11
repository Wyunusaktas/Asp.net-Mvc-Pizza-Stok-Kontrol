using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StokKontrolSistemi.Entities
{
    public class MalzemeTarif
    {

        [Key]
        public int MalzemeTarifID { get; set; }

        // İlişkiyi ifade eden dış anahtarlar
        [ForeignKey("Malzemeler")]
        public int MalzemeID { get; set; }
        public virtual Malzemeler Malzeme { get; set; }

        [ForeignKey("Tarifler")]
        public int TarifID { get; set; }
        public virtual Tarifler Tarif { get; set; }

        public double  Miktar { get; set; }
    }
}
