using StokKontrolSistemi.Entities;

namespace StokKontrolSistemi.Models
{
    public class CreateTarifViewModel
    {
        public IEnumerable<Malzemeler> Malzemeler { get; set; }
        public Dictionary<int, int> Miktarlar { get; set; }
    }
}
