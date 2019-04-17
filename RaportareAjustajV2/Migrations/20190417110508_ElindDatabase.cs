using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RaportareAjustajV2.Migrations
{
    public partial class ElindDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ElindModel",
                columns: table => new
                {
                    ElindModelId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(maxLength: 100, nullable: false),
                    DataIntroducere = table.Column<string>(maxLength: 100, nullable: true),
                    Tratament = table.Column<int>(nullable: false),
                    Diametru = table.Column<int>(nullable: false),
                    Calitate = table.Column<string>(maxLength: 100, nullable: true),
                    Sarja = table.Column<string>(maxLength: 100, nullable: true),
                    NrBare = table.Column<int>(nullable: false),
                    Lungime = table.Column<int>(nullable: false),
                    Masa = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElindModel", x => x.ElindModelId);
                });

            migrationBuilder.CreateTable(
                name: "FierastraieModels",
                columns: table => new
                {
                    FierastraieModelId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(maxLength: 100, nullable: false),
                    DataIntroducere = table.Column<string>(maxLength: 100, nullable: true),
                    Diametru = table.Column<int>(nullable: false),
                    Calitate = table.Column<string>(maxLength: 100, nullable: true),
                    Sarja = table.Column<string>(maxLength: 100, nullable: true),
                    NrBare = table.Column<int>(nullable: false),
                    Lungime = table.Column<int>(nullable: false),
                    Masa = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FierastraieModels", x => x.FierastraieModelId);
                });

            migrationBuilder.CreateTable(
                name: "NovafluxModels",
                columns: table => new
                {
                    NovafluxModelId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(maxLength: 100, nullable: false),
                    DataIntroducere = table.Column<string>(maxLength: 100, nullable: true),
                    Diametru = table.Column<int>(nullable: false),
                    Calitate = table.Column<string>(maxLength: 100, nullable: true),
                    Sarja = table.Column<string>(maxLength: 100, nullable: true),
                    DefectEtalon = table.Column<double>(nullable: false),
                    NrBareConform = table.Column<int>(nullable: false),
                    MasaConform = table.Column<double>(nullable: false),
                    NrBareNeConform = table.Column<int>(nullable: false),
                    MasaNeConform = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NovafluxModels", x => x.NovafluxModelId);
                });

            migrationBuilder.CreateTable(
                name: "PellatriceLandgrafModels",
                columns: table => new
                {
                    PellatriceLandgrafModelId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(maxLength: 100, nullable: false),
                    DataIntroducere = table.Column<string>(maxLength: 100, nullable: true),
                    DiametruIntrare = table.Column<double>(nullable: false),
                    DiametruIesire = table.Column<double>(nullable: false),
                    Calitate = table.Column<string>(maxLength: 100, nullable: true),
                    Sarja = table.Column<string>(maxLength: 100, nullable: true),
                    NrBare = table.Column<int>(nullable: false),
                    Lungime = table.Column<int>(nullable: false),
                    Masa = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PellatriceLandgrafModels", x => x.PellatriceLandgrafModelId);
                });

            migrationBuilder.CreateTable(
                name: "PresaValdoraModels",
                columns: table => new
                {
                    PresaValdoraModelId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(maxLength: 100, nullable: false),
                    DataIntroducere = table.Column<string>(maxLength: 100, nullable: true),
                    Diametru = table.Column<int>(nullable: false),
                    Calitate = table.Column<string>(maxLength: 100, nullable: true),
                    Sarja = table.Column<string>(maxLength: 100, nullable: true),
                    NrBare = table.Column<int>(nullable: false),
                    Lungime = table.Column<int>(nullable: false),
                    Masa = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PresaValdoraModels", x => x.PresaValdoraModelId);
                });

            migrationBuilder.CreateTable(
                name: "RuillatriceLandgrafModels",
                columns: table => new
                {
                    RuillatriceLandgrafModelId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(maxLength: 100, nullable: false),
                    DataIntroducere = table.Column<string>(maxLength: 100, nullable: true),
                    Diametru = table.Column<int>(nullable: false),
                    Calitate = table.Column<string>(maxLength: 100, nullable: true),
                    Sarja = table.Column<string>(maxLength: 100, nullable: true),
                    NrBare = table.Column<int>(nullable: false),
                    Lungime = table.Column<int>(nullable: false),
                    Masa = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RuillatriceLandgrafModels", x => x.RuillatriceLandgrafModelId);
                });

            migrationBuilder.CreateTable(
                name: "RullatriceProjectManModels",
                columns: table => new
                {
                    RullatriceProjectManModelId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(maxLength: 100, nullable: false),
                    DataIntroducere = table.Column<string>(maxLength: 100, nullable: true),
                    Diametru = table.Column<int>(nullable: false),
                    Calitate = table.Column<string>(maxLength: 100, nullable: true),
                    Sarja = table.Column<string>(maxLength: 100, nullable: true),
                    NrBare = table.Column<int>(nullable: false),
                    Lungime = table.Column<int>(nullable: false),
                    Masa = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RullatriceProjectManModels", x => x.RullatriceProjectManModelId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ElindModel");

            migrationBuilder.DropTable(
                name: "FierastraieModels");

            migrationBuilder.DropTable(
                name: "NovafluxModels");

            migrationBuilder.DropTable(
                name: "PellatriceLandgrafModels");

            migrationBuilder.DropTable(
                name: "PresaValdoraModels");

            migrationBuilder.DropTable(
                name: "RuillatriceLandgrafModels");

            migrationBuilder.DropTable(
                name: "RullatriceProjectManModels");
        }
    }
}
