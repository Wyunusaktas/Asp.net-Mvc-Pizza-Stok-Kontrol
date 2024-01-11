using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StokKontrolSistemi.Entities
{
    public class Kategori
    {
        [Key]
        [Required]
        public int KategoriID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Urunler> Urunler { get; set; }

    }
}
