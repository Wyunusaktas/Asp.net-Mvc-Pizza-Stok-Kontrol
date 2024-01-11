using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StokKontrolSistemi.Migrations
{
    /// <inheritdoc />
    public partial class FinalSql : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kategori",
                columns: table => new
                {
                    KategoriID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategori", x => x.KategoriID);
                });

            migrationBuilder.CreateTable(
                name: "Kullanici",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SurName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    State = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Locked = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kullanici", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Malzemeler",
                columns: table => new
                {
                    MalzemeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MalzemeAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MalzemeTuru = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Stok = table.Column<double>(type: "float", nullable: false),
                    AlımTarihi = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Malzemeler", x => x.MalzemeID);
                });

            migrationBuilder.CreateTable(
                name: "Tarif",
                columns: table => new
                {
                    TarifID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TarifAdı = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarif", x => x.TarifID);
                });

            migrationBuilder.CreateTable(
                name: "Tedarikci",
                columns: table => new
                {
                    TedarikciID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TedarikciName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TedarikciAdres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TedarikEdilenUrun = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tedarikci", x => x.TedarikciID);
                });

            migrationBuilder.CreateTable(
                name: "Satislar",
                columns: table => new
                {
                    SatislarID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SiparisTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KullaniciID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Satislar", x => x.SatislarID);
                    table.ForeignKey(
                        name: "FK_Satislar_Kullanici_KullaniciID",
                        column: x => x.KullaniciID,
                        principalTable: "Kullanici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MalzemeTarif",
                columns: table => new
                {
                    MalzemeTarifID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MalzemeID = table.Column<int>(type: "int", nullable: false),
                    TarifID = table.Column<int>(type: "int", nullable: false),
                    Miktar = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MalzemeTarif", x => x.MalzemeTarifID);
                    table.ForeignKey(
                        name: "FK_MalzemeTarif_Malzemeler_MalzemeID",
                        column: x => x.MalzemeID,
                        principalTable: "Malzemeler",
                        principalColumn: "MalzemeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MalzemeTarif_Tarif_TarifID",
                        column: x => x.TarifID,
                        principalTable: "Tarif",
                        principalColumn: "TarifID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Urunler",
                columns: table => new
                {
                    UrunID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Icindekiler = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KategoriID = table.Column<int>(type: "int", nullable: false),
                    TarifID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Urunler", x => x.UrunID);
                    table.ForeignKey(
                        name: "FK_Urunler_Kategori_KategoriID",
                        column: x => x.KategoriID,
                        principalTable: "Kategori",
                        principalColumn: "KategoriID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Urunler_Tarif_TarifID",
                        column: x => x.TarifID,
                        principalTable: "Tarif",
                        principalColumn: "TarifID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MalzemeTedarikciler",
                columns: table => new
                {
                    MalzemeTedarikciID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TedarikciID = table.Column<int>(type: "int", nullable: false),
                    MalzemeID = table.Column<int>(type: "int", nullable: false),
                    TedarikciMiktar = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MalzemeTedarikciler", x => x.MalzemeTedarikciID);
                    table.ForeignKey(
                        name: "FK_MalzemeTedarikciler_Malzemeler_MalzemeID",
                        column: x => x.MalzemeID,
                        principalTable: "Malzemeler",
                        principalColumn: "MalzemeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MalzemeTedarikciler_Tedarikci_TedarikciID",
                        column: x => x.TedarikciID,
                        principalTable: "Tedarikci",
                        principalColumn: "TedarikciID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UrunSatislar",
                columns: table => new
                {
                    UrunSatisID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UrunID = table.Column<int>(type: "int", nullable: false),
                    SatislarID = table.Column<int>(type: "int", nullable: false),
                    Adet = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrunSatislar", x => x.UrunSatisID);
                    table.ForeignKey(
                        name: "FK_UrunSatislar_Satislar_SatislarID",
                        column: x => x.SatislarID,
                        principalTable: "Satislar",
                        principalColumn: "SatislarID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UrunSatislar_Urunler_UrunID",
                        column: x => x.UrunID,
                        principalTable: "Urunler",
                        principalColumn: "UrunID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MalzemeTarif_MalzemeID",
                table: "MalzemeTarif",
                column: "MalzemeID");

            migrationBuilder.CreateIndex(
                name: "IX_MalzemeTarif_TarifID",
                table: "MalzemeTarif",
                column: "TarifID");

            migrationBuilder.CreateIndex(
                name: "IX_MalzemeTedarikciler_MalzemeID",
                table: "MalzemeTedarikciler",
                column: "MalzemeID");

            migrationBuilder.CreateIndex(
                name: "IX_MalzemeTedarikciler_TedarikciID",
                table: "MalzemeTedarikciler",
                column: "TedarikciID");

            migrationBuilder.CreateIndex(
                name: "IX_Satislar_KullaniciID",
                table: "Satislar",
                column: "KullaniciID");

            migrationBuilder.CreateIndex(
                name: "IX_Urunler_KategoriID",
                table: "Urunler",
                column: "KategoriID");

            migrationBuilder.CreateIndex(
                name: "IX_Urunler_TarifID",
                table: "Urunler",
                column: "TarifID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UrunSatislar_SatislarID",
                table: "UrunSatislar",
                column: "SatislarID");

            migrationBuilder.CreateIndex(
                name: "IX_UrunSatislar_UrunID",
                table: "UrunSatislar",
                column: "UrunID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MalzemeTarif");

            migrationBuilder.DropTable(
                name: "MalzemeTedarikciler");

            migrationBuilder.DropTable(
                name: "UrunSatislar");

            migrationBuilder.DropTable(
                name: "Malzemeler");

            migrationBuilder.DropTable(
                name: "Tedarikci");

            migrationBuilder.DropTable(
                name: "Satislar");

            migrationBuilder.DropTable(
                name: "Urunler");

            migrationBuilder.DropTable(
                name: "Kullanici");

            migrationBuilder.DropTable(
                name: "Kategori");

            migrationBuilder.DropTable(
                name: "Tarif");
        }
    }
}
