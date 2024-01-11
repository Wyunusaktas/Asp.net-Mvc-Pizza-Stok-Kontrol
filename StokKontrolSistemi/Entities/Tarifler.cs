using System.ComponentModel.DataAnnotations;

namespace StokKontrolSistemi.Entities
{
    public class Tarifler
    {
        [Key]
        public int TarifID { get; set; }
        public string TarifAdı { get; set; }
        public virtual ICollection<MalzemeTarif> MalzemeTarifler { get; set; }

    }
}
