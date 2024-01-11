using System.ComponentModel.DataAnnotations;

namespace StokKontrolSistemi.Entities
{
    public class Malzemeler
    {
   [Key]
    public int MalzemeID { get; set; }
    public string MalzemeAdi { get; set; }
    public string MalzemeTuru { get; set; }
    public Double Stok { get; set; }

    public DateTime AlımTarihi { get; set; }
    public virtual ICollection<MalzemeTarif> MalzemeTarifler { get; set; }
    public virtual ICollection<MalzemeTedarikci> MalzemeTedarikciler { get; set; } 

    }

}
