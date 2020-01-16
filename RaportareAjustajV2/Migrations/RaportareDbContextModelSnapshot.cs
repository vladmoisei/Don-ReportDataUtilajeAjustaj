﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RaportareAjustajV2;

namespace RaportareAjustajV2.Migrations
{
    [DbContext(typeof(RaportareDbContext))]
    partial class RaportareDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RaportareAjustajV2.ElindModel", b =>
                {
                    b.Property<int>("ElindModelId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Calitate")
                        .HasMaxLength(100);

                    b.Property<string>("DataIntroducere")
                        .HasMaxLength(100);

                    b.Property<int>("Diametru");

                    b.Property<int>("Lungime");

                    b.Property<double>("Masa");

                    b.Property<int>("NrBare");

                    b.Property<string>("Sarja")
                        .HasMaxLength(100);

                    b.Property<int>("Tratament");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("ElindModelId");

                    b.ToTable("ElindModels");
                });

            modelBuilder.Entity("RaportareAjustajV2.EltiModel", b =>
                {
                    b.Property<int>("EltiModelId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Calitate")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("ConsumElectricitate");

                    b.Property<int>("ConsumGaz");

                    b.Property<int>("Cuptor");

                    b.Property<string>("DataDescarcare")
                        .HasMaxLength(100);

                    b.Property<string>("DataIncarcare")
                        .HasMaxLength(100);

                    b.Property<string>("DataIntroducere")
                        .HasMaxLength(100);

                    b.Property<int>("Diametru");

                    b.Property<int>("LungimeBare");

                    b.Property<double>("Masa");

                    b.Property<int>("NumarBare");

                    b.Property<string>("OraDescarcare")
                        .HasMaxLength(100);

                    b.Property<string>("OraIncarcare")
                        .HasMaxLength(100);

                    b.Property<string>("Sarja")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("TratamentTermic");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("EltiModelId");

                    b.ToTable("EltiModels");
                });

            modelBuilder.Entity("RaportareAjustajV2.FierastraieModel", b =>
                {
                    b.Property<int>("FierastraieModelId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Calitate")
                        .HasMaxLength(100);

                    b.Property<string>("DataIntroducere")
                        .HasMaxLength(100);

                    b.Property<int>("Diametru");

                    b.Property<int>("Lungime");

                    b.Property<double>("Masa");

                    b.Property<int>("NrBare");

                    b.Property<string>("Sarja")
                        .HasMaxLength(100);

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("FierastraieModelId");

                    b.ToTable("FierastraieModels");
                });

            modelBuilder.Entity("RaportareAjustajV2.GaddaModel", b =>
                {
                    b.Property<int>("GaddaModelId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Calitate")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("ConsumElectricitate");

                    b.Property<int>("ConsumGaz");

                    b.Property<int>("Cuptor");

                    b.Property<string>("DataDescarcare")
                        .HasMaxLength(100);

                    b.Property<string>("DataIncarcare")
                        .HasMaxLength(100);

                    b.Property<string>("DataIntroducere")
                        .HasMaxLength(100);

                    b.Property<int>("Diametru");

                    b.Property<int>("LungimeBare");

                    b.Property<double>("Masa");

                    b.Property<int>("NumarBare");

                    b.Property<string>("OraDescarcare")
                        .HasMaxLength(100);

                    b.Property<string>("OraIncarcare")
                        .HasMaxLength(100);

                    b.Property<string>("Sarja")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("TratamentTermic");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("GaddaModelId");

                    b.ToTable("GaddaModels");
                });

            modelBuilder.Entity("RaportareAjustajV2.NovafluxModel", b =>
                {
                    b.Property<int>("NovafluxModelId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Calitate")
                        .HasMaxLength(100);

                    b.Property<string>("DataIntroducere")
                        .HasMaxLength(100);

                    b.Property<double>("DefectEtalon");

                    b.Property<int>("Diametru");

                    b.Property<double>("MasaConform");

                    b.Property<double>("MasaNeConform");

                    b.Property<int>("NrBareConform");

                    b.Property<int>("NrBareNeConform");

                    b.Property<string>("Sarja")
                        .HasMaxLength(100);

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("NovafluxModelId");

                    b.ToTable("NovafluxModels");
                });

            modelBuilder.Entity("RaportareAjustajV2.PellatriceLandgrafModel", b =>
                {
                    b.Property<int>("PellatriceLandgrafModelId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Calitate")
                        .HasMaxLength(100);

                    b.Property<string>("DataIntroducere")
                        .HasMaxLength(100);

                    b.Property<double>("DiametruIesire");

                    b.Property<double>("DiametruIntrare");

                    b.Property<string>("Eticheta")
                        .HasMaxLength(100);

                    b.Property<int>("Lungime");

                    b.Property<double>("Masa");

                    b.Property<int>("NrBare");

                    b.Property<string>("Sarja")
                        .HasMaxLength(100);

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("PellatriceLandgrafModelId");

                    b.ToTable("PellatriceLandgrafModels");
                });

            modelBuilder.Entity("RaportareAjustajV2.PresaDunkeModel", b =>
                {
                    b.Property<int>("PresaDunkeModelId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Calitate")
                        .HasMaxLength(100);

                    b.Property<string>("DataIndreptareMaterial");

                    b.Property<string>("DataIntroducere")
                        .HasMaxLength(100);

                    b.Property<int>("Diametru");

                    b.Property<string>("Eticheta")
                        .HasMaxLength(100);

                    b.Property<int>("Lungime");

                    b.Property<double>("Masa");

                    b.Property<int>("NrBare");

                    b.Property<string>("Sarja")
                        .HasMaxLength(100);

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("PresaDunkeModelId");

                    b.ToTable("PresaDunkeModels");
                });

            modelBuilder.Entity("RaportareAjustajV2.PresaValdoraModel", b =>
                {
                    b.Property<int>("PresaValdoraModelId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Calitate")
                        .HasMaxLength(100);

                    b.Property<string>("DataIndreptareMaterial");

                    b.Property<string>("DataIntroducere")
                        .HasMaxLength(100);

                    b.Property<int>("Diametru");

                    b.Property<string>("Eticheta")
                        .HasMaxLength(100);

                    b.Property<int>("Lungime");

                    b.Property<double>("Masa");

                    b.Property<int>("NrBare");

                    b.Property<string>("Sarja")
                        .HasMaxLength(100);

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("PresaValdoraModelId");

                    b.ToTable("PresaValdoraModels");
                });

            modelBuilder.Entity("RaportareAjustajV2.RuillatriceLandgrafModel", b =>
                {
                    b.Property<int>("RuillatriceLandgrafModelId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Calitate")
                        .HasMaxLength(100);

                    b.Property<string>("DataIntroducere")
                        .HasMaxLength(100);

                    b.Property<int>("Diametru");

                    b.Property<int>("Lungime");

                    b.Property<double>("Masa");

                    b.Property<int>("NrBare");

                    b.Property<string>("Sarja")
                        .HasMaxLength(100);

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("RuillatriceLandgrafModelId");

                    b.ToTable("RuillatriceLandgrafModels");
                });

            modelBuilder.Entity("RaportareAjustajV2.RullatriceProjectManModel", b =>
                {
                    b.Property<int>("RullatriceProjectManModelId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Calitate")
                        .HasMaxLength(100);

                    b.Property<string>("DataIntroducere")
                        .HasMaxLength(100);

                    b.Property<int>("Diametru");

                    b.Property<int>("Lungime");

                    b.Property<double>("Masa");

                    b.Property<int>("NrBare");

                    b.Property<string>("Sarja")
                        .HasMaxLength(100);

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("RullatriceProjectManModelId");

                    b.ToTable("RullatriceProjectManModels");
                });

            modelBuilder.Entity("RaportareAjustajV2.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsAdmin");

                    b.Property<bool>("IsEnable");

                    b.Property<string>("Nume")
                        .HasMaxLength(100);

                    b.Property<string>("Password")
                        .HasMaxLength(100);

                    b.Property<string>("Prenume")
                        .HasMaxLength(100);

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("Utilaj");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
