﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RaportareAjustajV2;

namespace RaportareAjustajV2.Migrations
{
    [DbContext(typeof(RaportareDbContext))]
    [Migration("20200331114028_ModifyUSModel")]
    partial class ModifyUSModel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RaportareAjustajV2.CalitateOtelModel", b =>
                {
                    b.Property<int>("CalitateOtelModelId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Valoare")
                        .HasMaxLength(100);

                    b.HasKey("CalitateOtelModelId");

                    b.ToTable("CalitateOtelModels");
                });

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

            modelBuilder.Entity("RaportareAjustajV2.UsBarModel", b =>
                {
                    b.Property<int>("UsBarModelId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CalitateOtelModelId");

                    b.Property<int>("Clasa1");

                    b.Property<int>("Clasa2");

                    b.Property<int>("Clasa2Plus");

                    b.Property<int>("Clasa3");

                    b.Property<DateTime>("DataControl");

                    b.Property<DateTime>("DataIntroducere");

                    b.Property<int>("Diametru");

                    b.Property<int>("Furnizor");

                    b.Property<string>("Gain");

                    b.Property<double>("MarimeDefect1");

                    b.Property<double>("MarimeDefect2");

                    b.Property<double>("MarimeDefect2Plus");

                    b.Property<double>("MarimeDefect3");

                    b.Property<double>("MarimeDefectSS");

                    b.Property<string>("Observatii")
                        .HasMaxLength(250);

                    b.Property<string>("Range");

                    b.Property<int>("SS");

                    b.Property<string>("Sarja")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("StareMaterial");

                    b.Property<int>("TipDiscontinuitate");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("UsBarModelId");

                    b.HasIndex("CalitateOtelModelId");

                    b.ToTable("UsBarModels");
                });

            modelBuilder.Entity("RaportareAjustajV2.UsBlumModel", b =>
                {
                    b.Property<int>("UsBlumModelId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Blum1");

                    b.Property<int>("Blum2");

                    b.Property<int>("Blum3");

                    b.Property<int>("Blum4");

                    b.Property<int>("CalitateOtelModelId");

                    b.Property<DateTime>("DataControl");

                    b.Property<DateTime>("DataIntroducere");

                    b.Property<string>("FiProgramat")
                        .HasMaxLength(100);

                    b.Property<int>("Fir1");

                    b.Property<int>("Fir2");

                    b.Property<int>("Fir3");

                    b.Property<int>("Fir4");

                    b.Property<string>("FormatBlum")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("Furnizor");

                    b.Property<double>("MarimeDefect1");

                    b.Property<double>("MarimeDefect2");

                    b.Property<double>("MarimeDefect3");

                    b.Property<double>("MarimeDefect4");

                    b.Property<string>("Observatii")
                        .HasMaxLength(250);

                    b.Property<string>("Sarja")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("UsBlumModelId");

                    b.HasIndex("CalitateOtelModelId");

                    b.ToTable("UsBlumModels");
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

            modelBuilder.Entity("RaportareAjustajV2.UsBarModel", b =>
                {
                    b.HasOne("RaportareAjustajV2.CalitateOtelModel", "CalitateOtel")
                        .WithMany()
                        .HasForeignKey("CalitateOtelModelId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RaportareAjustajV2.UsBlumModel", b =>
                {
                    b.HasOne("RaportareAjustajV2.CalitateOtelModel", "CalitateOtel")
                        .WithMany()
                        .HasForeignKey("CalitateOtelModelId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
