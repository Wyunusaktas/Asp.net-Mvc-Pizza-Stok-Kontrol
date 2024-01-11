using System.ComponentModel.DataAnnotations;

namespace StokKontrolSistemi.Entities
{
    public partial class Kullanici
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string SurName { get; set; }
        [Required]
        [StringLength(50)]
        public string Email { get; set; }
        [Required]
        [StringLength(50)]
        public string Password { get; set; }
        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [StringLength(50)]
        public string State { get; set; }
        public bool Locked { get; set; } = false;
        public virtual ICollection<Satislar> Satislar { get; set; }


    }
}
