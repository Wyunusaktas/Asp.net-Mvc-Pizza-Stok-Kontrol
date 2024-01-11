using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StokKontrolSistemi.Entities
{
    public class Satislar
    {
        [Key]
       public int SatislarID { get; set; }
        public  DateTime SiparisTarihi { get; set; }
        public virtual ICollection<UrunSatis> UrunSatislar { get; set; }

        [ForeignKey("Kullanici")]
        public int KullaniciID { get; set; }

        // İlişkiyi ifade eden navigasyon property
        public virtual Kullanici Kullanici { get; set; }


    }
}
