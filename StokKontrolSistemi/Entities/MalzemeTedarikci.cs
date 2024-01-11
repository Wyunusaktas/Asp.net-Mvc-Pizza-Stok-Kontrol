using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StokKontrolSistemi.Entities
{
    public class MalzemeTedarikci
    {
        [Key]
        public int MalzemeTedarikciID { get; set; }

        // Foreign keys for the many-to-many relationship
        [ForeignKey("Tedarikci")]
        public int TedarikciID { get; set; }
        public virtual Tedarikci Tedarikci { get; set; }

        [ForeignKey("Malzeme")]
        public int MalzemeID { get; set; }
        public virtual Malzemeler Malzeme { get; set; }

        public int TedarikciMiktar { get; set; }
    }
}
