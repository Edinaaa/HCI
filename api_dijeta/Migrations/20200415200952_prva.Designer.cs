﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using api_dijeta_data;

namespace api_dijeta_data.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20200415200952_prva")]
    partial class prva
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("api_dijeta_data.Models.Dan", b =>
                {
                    b.Property<int>("DanId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("BrojKalorija")
                        .HasColumnType("real");

                    b.Property<int>("DijetaId")
                        .HasColumnType("int");

                    b.HasKey("DanId");

                    b.HasIndex("DijetaId");

                    b.ToTable("dani");
                });

            modelBuilder.Entity("api_dijeta_data.Models.Dijeta", b =>
                {
                    b.Property<int>("DijetaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BrojDana")
                        .HasColumnType("int");

                    b.Property<float>("Kalorije")
                        .HasColumnType("real");

                    b.Property<int>("KorisnikId")
                        .HasColumnType("int");

                    b.Property<string>("Naziv")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DijetaId");

                    b.HasIndex("KorisnikId");

                    b.ToTable("dijete");
                });

            modelBuilder.Entity("api_dijeta_data.Models.Korisnik", b =>
                {
                    b.Property<int>("KorisnikId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KorisnickoIme")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lozinka")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prezime")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("KorisnikId");

                    b.ToTable("korisnici");
                });

            modelBuilder.Entity("api_dijeta_data.Models.Namirnica", b =>
                {
                    b.Property<int>("NamirnicaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("JM")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Kalorije")
                        .HasColumnType("real");

                    b.Property<int>("Kolicina")
                        .HasColumnType("int");

                    b.Property<float>("Masti")
                        .HasColumnType("real");

                    b.Property<string>("Naziv")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Proteini")
                        .HasColumnType("real");

                    b.Property<float>("Ugljikohidrati")
                        .HasColumnType("real");

                    b.HasKey("NamirnicaId");

                    b.ToTable("namirnice");
                });

            modelBuilder.Entity("api_dijeta_data.Models.NamirniceObroka", b =>
                {
                    b.Property<int>("NamirniceObrokaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("Kalorije")
                        .HasColumnType("real");

                    b.Property<int>("Kolicina")
                        .HasColumnType("int");

                    b.Property<int>("NamirnicaId")
                        .HasColumnType("int");

                    b.Property<int>("ObrokId")
                        .HasColumnType("int");

                    b.HasKey("NamirniceObrokaId");

                    b.HasIndex("NamirnicaId");

                    b.HasIndex("ObrokId");

                    b.ToTable("namirniceObroka");
                });

            modelBuilder.Entity("api_dijeta_data.Models.Obrok", b =>
                {
                    b.Property<int>("ObrokId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("BrojKalorija")
                        .HasColumnType("real");

                    b.Property<int>("DanId")
                        .HasColumnType("int");

                    b.Property<string>("NazivObroka")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ObrokId");

                    b.HasIndex("DanId");

                    b.ToTable("obroci");
                });

            modelBuilder.Entity("api_dijeta_data.Models.Dan", b =>
                {
                    b.HasOne("api_dijeta_data.Models.Dijeta", "Dijeta")
                        .WithMany()
                        .HasForeignKey("DijetaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("api_dijeta_data.Models.Dijeta", b =>
                {
                    b.HasOne("api_dijeta_data.Models.Korisnik", "Korisnik")
                        .WithMany()
                        .HasForeignKey("KorisnikId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("api_dijeta_data.Models.NamirniceObroka", b =>
                {
                    b.HasOne("api_dijeta_data.Models.Namirnica", "Namirnica")
                        .WithMany()
                        .HasForeignKey("NamirnicaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api_dijeta_data.Models.Obrok", "Obrok")
                        .WithMany()
                        .HasForeignKey("ObrokId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("api_dijeta_data.Models.Obrok", b =>
                {
                    b.HasOne("api_dijeta_data.Models.Dan", "Dan")
                        .WithMany()
                        .HasForeignKey("DanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
