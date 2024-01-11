using Microsoft.EntityFrameworkCore;

namespace StokKontrolSistemi.Entities
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Kullanici> Kullanici { get; set; }
        public DbSet<Urunler> Urunler { get; set; }
        public DbSet<Kategori> Kategori { get; set; }
        public DbSet<Tedarikci> Tedarikci { get; set; }
        public DbSet<Tarifler> Tarif { get; set; }
        public DbSet<Satislar> Satislar { get; set; }
        public DbSet<Malzemeler> Malzemeler { get; set; }
        public DbSet<UrunSatis> UrunSatislar { get; set; }
        public DbSet<MalzemeTarif> MalzemeTarif{ get; set; }
        public DbSet<MalzemeTedarikci> MalzemeTedarikciler { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //urunler kategori many to one ilişki

            modelBuilder.Entity<Urunler>()
                .HasOne(u => u.Kategori)
                .WithMany(k => k.Urunler)
                .HasForeignKey(u => u.KategoriID)
                .OnDelete(DeleteBehavior.Cascade);
            // UrunSatis tablosu ile Urunler ve Satislar tabloları arasındaki ilişkiyi belirleme
            modelBuilder.Entity<UrunSatis>()
                    .HasKey(us => us.UrunSatisID);

            modelBuilder.Entity<UrunSatis>()
                .HasOne(us => us.Urunler)
                .WithMany(u => u.UrunSatislar)
                .HasForeignKey(us => us.UrunID);

            modelBuilder.Entity<UrunSatis>()
                .HasOne(us => us.Satislar)
                .WithMany(s => s.UrunSatislar)
                .HasForeignKey(us => us.SatislarID);
            //urunler tarifle one to one

            modelBuilder.Entity<Urunler>()
             .HasKey(u => u.UrunID);

            modelBuilder.Entity<Tarifler>()
                .HasKey(t => t.TarifID);

            modelBuilder.Entity<Urunler>()
                .HasOne(u => u.Tarif)
                .WithOne()
                .HasForeignKey<Urunler>(u => u.TarifID);

            //kullanici satislar one to many iliski

            modelBuilder.Entity<Kullanici>()
                .HasKey(k => k.Id);

            modelBuilder.Entity<Satislar>()
                .HasKey(s => s.SatislarID);

           
            modelBuilder.Entity<Kullanici>()
                .HasMany(k => k.Satislar)
                .WithOne(s => s.Kullanici)
                .HasForeignKey(s => s.KullaniciID);


            //malzemeler tarifle many to many

            modelBuilder.Entity<MalzemeTarif>()
                 .HasKey(mt => mt.MalzemeTarifID);

            modelBuilder.Entity<MalzemeTarif>()
                .HasOne(mt => mt.Malzeme)
                .WithMany(m => m.MalzemeTarifler)
                .HasForeignKey(mt => mt.MalzemeID);

            modelBuilder.Entity<MalzemeTarif>()
                .HasOne(mt => mt.Tarif)
                .WithMany(t => t.MalzemeTarifler)
                .HasForeignKey(mt => mt.TarifID);

            // tedarikci malzeme many to many
            modelBuilder.Entity<MalzemeTedarikci>()
                .HasKey(mt => mt.MalzemeTedarikciID);

            modelBuilder.Entity<MalzemeTedarikci>()
                .HasOne(mt => mt.Tedarikci)
                .WithMany(t => t.MalzemeTedarikciler)
                .HasForeignKey(mt => mt.TedarikciID);

            modelBuilder.Entity<MalzemeTedarikci>()
                .HasOne(mt => mt.Malzeme)
                .WithMany(m => m.MalzemeTedarikciler)
                .HasForeignKey(mt => mt.MalzemeID);



        }
    }
}