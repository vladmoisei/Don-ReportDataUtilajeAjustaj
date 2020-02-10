using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RaportareAjustajV2.Migrations
{
    public partial class BarAndBlumAndCalitateModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CalitateOtelModels",
                columns: table => new
                {
                    CalitateOtelModelId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Valoare = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalitateOtelModels", x => x.CalitateOtelModelId);
                });

            migrationBuilder.CreateTable(
                name: "UsBarModels",
                columns: table => new
                {
                    UsBarModelId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(maxLength: 100, nullable: false),
                    DataIntroducere = table.Column<DateTime>(nullable: false),
                    DataControl = table.Column<DateTime>(nullable: false),
                    Diametru = table.Column<int>(nullable: false),
                    Furnizor = table.Column<int>(nullable: false),
                    Sarja = table.Column<string>(maxLength: 100, nullable: false),
                    CalitateOtelModelId = table.Column<int>(nullable: false),
                    StareMaterial = table.Column<int>(maxLength: 100, nullable: false),
                    Clasa3 = table.Column<int>(nullable: false),
                    MarimeDefect3 = table.Column<double>(nullable: false),
                    Clasa2 = table.Column<int>(nullable: false),
                    MarimeDefect2 = table.Column<double>(nullable: false),
                    Clasa2Plus = table.Column<int>(nullable: false),
                    MarimeDefect2Plus = table.Column<double>(nullable: false),
                    Clasa1 = table.Column<int>(nullable: false),
                    MarimeDefect1 = table.Column<double>(nullable: false),
                    SS = table.Column<int>(nullable: false),
                    MarimeDefectSS = table.Column<double>(nullable: false),
                    TipDiscontinuitate = table.Column<int>(nullable: false),
                    Observatii = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsBarModels", x => x.UsBarModelId);
                    table.ForeignKey(
                        name: "FK_UsBarModels_CalitateOtelModels_CalitateOtelModelId",
                        column: x => x.CalitateOtelModelId,
                        principalTable: "CalitateOtelModels",
                        principalColumn: "CalitateOtelModelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsBlumModels",
                columns: table => new
                {
                    UsBlumModelId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(maxLength: 100, nullable: false),
                    DataIntroducere = table.Column<DateTime>(nullable: false),
                    DataControl = table.Column<DateTime>(nullable: false),
                    Sarja = table.Column<string>(maxLength: 100, nullable: false),
                    FormatBlum = table.Column<string>(maxLength: 100, nullable: false),
                    Furnizor = table.Column<int>(nullable: false),
                    CalitateOtelModelId = table.Column<int>(nullable: false),
                    FiProgramat = table.Column<string>(maxLength: 100, nullable: true),
                    Fir1 = table.Column<int>(nullable: false),
                    Blum1 = table.Column<int>(nullable: false),
                    MarimeDefect1 = table.Column<double>(nullable: false),
                    Fir2 = table.Column<int>(nullable: false),
                    Blum2 = table.Column<int>(nullable: false),
                    MarimeDefect2 = table.Column<double>(nullable: false),
                    Fir3 = table.Column<int>(nullable: false),
                    Blum3 = table.Column<int>(nullable: false),
                    MarimeDefect3 = table.Column<double>(nullable: false),
                    Observatii = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsBlumModels", x => x.UsBlumModelId);
                    table.ForeignKey(
                        name: "FK_UsBlumModels_CalitateOtelModels_CalitateOtelModelId",
                        column: x => x.CalitateOtelModelId,
                        principalTable: "CalitateOtelModels",
                        principalColumn: "CalitateOtelModelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsBarModels_CalitateOtelModelId",
                table: "UsBarModels",
                column: "CalitateOtelModelId");

            migrationBuilder.CreateIndex(
                name: "IX_UsBlumModels_CalitateOtelModelId",
                table: "UsBlumModels",
                column: "CalitateOtelModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsBarModels");

            migrationBuilder.DropTable(
                name: "UsBlumModels");

            migrationBuilder.DropTable(
                name: "CalitateOtelModels");
        }
    }
}
