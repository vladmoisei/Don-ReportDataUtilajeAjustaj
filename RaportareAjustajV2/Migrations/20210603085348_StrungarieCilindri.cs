using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RaportareAjustajV2.Migrations
{
    public partial class StrungarieCilindri : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StrungarieCilindriModel",
                columns: table => new
                {
                    StrungarieCilindriModelId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DataIntroducere = table.Column<DateTime>(nullable: false),
                    CodCartellino = table.Column<string>(maxLength: 30, nullable: true),
                    Furnizor = table.Column<string>(maxLength: 30, nullable: true),
                    Sarja = table.Column<string>(maxLength: 30, nullable: true),
                    Diametru = table.Column<string>(maxLength: 10, nullable: true),
                    Calitate = table.Column<string>(maxLength: 30, nullable: true),
                    Lungime = table.Column<string>(maxLength: 10, nullable: true),
                    Greutate = table.Column<string>(maxLength: 10, nullable: true),
                    MotivNeexpediere = table.Column<string>(maxLength: 100, nullable: true),
                    DescrSdF = table.Column<string>(maxLength: 100, nullable: true),
                    IsInLucru = table.Column<bool>(nullable: false),
                    DiametruFinal = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    DataBifatInLucru = table.Column<DateTime>(nullable: false),
                    DataDiamFinal = table.Column<DateTime>(nullable: false),
                    ComentariuStrungarie = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StrungarieCilindriModel", x => x.StrungarieCilindriModelId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StrungarieCilindriModel");
        }
    }
}
