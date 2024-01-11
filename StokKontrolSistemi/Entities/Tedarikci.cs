using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StokKontrolSistemi.Entities
{
    public class Tedarikci
    {
        [Key]
        [Required]
        public int TedarikciID { get; set; }

        public string TedarikciName { get; set; }
       
        public  string TedarikciAdres {  get; set; }

        public string  TedarikEdilenUrun {  get; set; }

        public virtual ICollection<MalzemeTedarikci> MalzemeTedarikciler { get; set; } 


    }
}
