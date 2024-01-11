namespace StokKontrolSistemi.Models
{
    public class PizzaSatisOraniViewModel
    {
        public int Ay { get; set; }
        public int Yil { get; set; }
        public string PizzaCesidi { get; set; }
        public int SatilanAdet { get; set; }
        public int ToplamAdet { get; set; }

        public double SatilmaOrani { get; set; }
    }
}
