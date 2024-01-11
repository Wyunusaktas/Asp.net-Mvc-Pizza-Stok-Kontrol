﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StokKontrolSistemi.Entities;

#nullable disable

namespace StokKontrolSistemi.Migrations
{
    [DbContext(typeof(DataBaseContext))]
    partial class DataBaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("StokKontrolSistemi.Entities.Kategori", b =>
                {
                    b.Property<int>("KategoriID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("KategoriID"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("KategoriID");

                    b.ToTable("Kategori");
                });

            modelBuilder.Entity("StokKontrolSistemi.Entities.Kullanici", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("Locked")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("SurName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Kullanici");
                });

            modelBuilder.Entity("StokKontrolSistemi.Entities.MalzemeTarif", b =>
                {
                    b.Property<int>("MalzemeTarifID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MalzemeTarifID"));

                    b.Property<int>("MalzemeID")
                        .HasColumnType("int");

                    b.Property<double>("Miktar")
                        .HasColumnType("float");

                    b.Property<int>("TarifID")
                        .HasColumnType("int");

                    b.HasKey("MalzemeTarifID");

                    b.HasIndex("MalzemeID");

                    b.HasIndex("TarifID");

                    b.ToTable("MalzemeTarif");
                });

            modelBuilder.Entity("StokKontrolSistemi.Entities.MalzemeTedarikci", b =>
                {
                    b.Property<int>("MalzemeTedarikciID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MalzemeTedarikciID"));

                    b.Property<int>("MalzemeID")
                        .HasColumnType("int");

                    b.Property<int>("TedarikciID")
                        .HasColumnType("int");

                    b.Property<int>("TedarikciMiktar")
                        .HasColumnType("int");

                    b.HasKey("MalzemeTedarikciID");

                    b.HasIndex("MalzemeID");

                    b.HasIndex("TedarikciID");

                    b.ToTable("MalzemeTedarikciler");
                });

            modelBuilder.Entity("StokKontrolSistemi.Entities.Malzemeler", b =>
                {
                    b.Property<int>("MalzemeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MalzemeID"));

                    b.Property<DateTime>("AlımTarihi")
                        .HasColumnType("datetime2");

                    b.Property<string>("MalzemeAdi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MalzemeTuru")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Stok")
                        .HasColumnType("float");

                    b.HasKey("MalzemeID");

                    b.ToTable("Malzemeler");
                });

            modelBuilder.Entity("StokKontrolSistemi.Entities.Satislar", b =>
                {
                    b.Property<int>("SatislarID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SatislarID"));

                    b.Property<int>("KullaniciID")
                        .HasColumnType("int");

                    b.Property<DateTime>("SiparisTarihi")
                        .HasColumnType("datetime2");

                    b.HasKey("SatislarID");

                    b.HasIndex("KullaniciID");

                    b.ToTable("Satislar");
                });

            modelBuilder.Entity("StokKontrolSistemi.Entities.Tarifler", b =>
                {
                    b.Property<int>("TarifID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TarifID"));

                    b.Property<string>("TarifAdı")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TarifID");

                    b.ToTable("Tarif");
                });

            modelBuilder.Entity("StokKontrolSistemi.Entities.Tedarikci", b =>
                {
                    b.Property<int>("TedarikciID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TedarikciID"));

                    b.Property<string>("TedarikEdilenUrun")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TedarikciAdres")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TedarikciName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TedarikciID");

                    b.ToTable("Tedarikci");
                });

            modelBuilder.Entity("StokKontrolSistemi.Entities.UrunSatis", b =>
                {
                    b.Property<int>("UrunSatisID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UrunSatisID"));

                    b.Property<int>("Adet")
                        .HasColumnType("int");

                    b.Property<int>("SatislarID")
                        .HasColumnType("int");

                    b.Property<int>("UrunID")
                        .HasColumnType("int");

                    b.HasKey("UrunSatisID");

                    b.HasIndex("SatislarID");

                    b.HasIndex("UrunID");

                    b.ToTable("UrunSatislar");
                });

            modelBuilder.Entity("StokKontrolSistemi.Entities.Urunler", b =>
                {
                    b.Property<int>("UrunID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UrunID"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Icindekiler")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("KategoriID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Picture")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Price")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TarifID")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("UrunID");

                    b.HasIndex("KategoriID");

                    b.HasIndex("TarifID")
                        .IsUnique();

                    b.ToTable("Urunler");
                });

            modelBuilder.Entity("StokKontrolSistemi.Entities.MalzemeTarif", b =>
                {
                    b.HasOne("StokKontrolSistemi.Entities.Malzemeler", "Malzeme")
                        .WithMany("MalzemeTarifler")
                        .HasForeignKey("MalzemeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StokKontrolSistemi.Entities.Tarifler", "Tarif")
                        .WithMany("MalzemeTarifler")
                        .HasForeignKey("TarifID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Malzeme");

                    b.Navigation("Tarif");
                });

            modelBuilder.Entity("StokKontrolSistemi.Entities.MalzemeTedarikci", b =>
                {
                    b.HasOne("StokKontrolSistemi.Entities.Malzemeler", "Malzeme")
                        .WithMany("MalzemeTedarikciler")
                        .HasForeignKey("MalzemeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StokKontrolSistemi.Entities.Tedarikci", "Tedarikci")
                        .WithMany("MalzemeTedarikciler")
                        .HasForeignKey("TedarikciID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Malzeme");

                    b.Navigation("Tedarikci");
                });

            modelBuilder.Entity("StokKontrolSistemi.Entities.Satislar", b =>
                {
                    b.HasOne("StokKontrolSistemi.Entities.Kullanici", "Kullanici")
                        .WithMany("Satislar")
                        .HasForeignKey("KullaniciID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Kullanici");
                });

            modelBuilder.Entity("StokKontrolSistemi.Entities.UrunSatis", b =>
                {
                    b.HasOne("StokKontrolSistemi.Entities.Satislar", "Satislar")
                        .WithMany("UrunSatislar")
                        .HasForeignKey("SatislarID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StokKontrolSistemi.Entities.Urunler", "Urunler")
                        .WithMany("UrunSatislar")
                        .HasForeignKey("UrunID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Satislar");

                    b.Navigation("Urunler");
                });

            modelBuilder.Entity("StokKontrolSistemi.Entities.Urunler", b =>
                {
                    b.HasOne("StokKontrolSistemi.Entities.Kategori", "Kategori")
                        .WithMany("Urunler")
                        .HasForeignKey("KategoriID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StokKontrolSistemi.Entities.Tarifler", "Tarif")
                        .WithOne()
                        .HasForeignKey("StokKontrolSistemi.Entities.Urunler", "TarifID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Kategori");

                    b.Navigation("Tarif");
                });

            modelBuilder.Entity("StokKontrolSistemi.Entities.Kategori", b =>
                {
                    b.Navigation("Urunler");
                });

            modelBuilder.Entity("StokKontrolSistemi.Entities.Kullanici", b =>
                {
                    b.Navigation("Satislar");
                });

            modelBuilder.Entity("StokKontrolSistemi.Entities.Malzemeler", b =>
                {
                    b.Navigation("MalzemeTarifler");

                    b.Navigation("MalzemeTedarikciler");
                });

            modelBuilder.Entity("StokKontrolSistemi.Entities.Satislar", b =>
                {
                    b.Navigation("UrunSatislar");
                });

            modelBuilder.Entity("StokKontrolSistemi.Entities.Tarifler", b =>
                {
                    b.Navigation("MalzemeTarifler");
                });

            modelBuilder.Entity("StokKontrolSistemi.Entities.Tedarikci", b =>
                {
                    b.Navigation("MalzemeTedarikciler");
                });

            modelBuilder.Entity("StokKontrolSistemi.Entities.Urunler", b =>
                {
                    b.Navigation("UrunSatislar");
                });
#pragma warning restore 612, 618
        }
    }
}
