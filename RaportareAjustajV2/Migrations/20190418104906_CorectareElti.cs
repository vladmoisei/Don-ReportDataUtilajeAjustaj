using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RaportareAjustajV2.Migrations
{
    public partial class CorectareElti : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ElindModel",
                table: "ElindModel");

            migrationBuilder.RenameTable(
                name: "ElindModel",
                newName: "ElindModels");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ElindModels",
                table: "ElindModels",
                column: "ElindModelId");

            migrationBuilder.CreateTable(
                name: "EltiModels",
                columns: table => new
                {
                    EltiModelId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(maxLength: 100, nullable: false),
                    DataIntroducere = table.Column<string>(maxLength: 100, nullable: true),
                    Cuptor = table.Column<int>(nullable: false),
                    TratamentTermic = table.Column<int>(nullable: false),
                    Diametru = table.Column<int>(nullable: false),
                    Calitate = table.Column<string>(maxLength: 100, nullable: false),
                    Sarja = table.Column<string>(maxLength: 100, nullable: false),
                    NumarBare = table.Column<int>(nullable: false),
                    LungimeBare = table.Column<int>(nullable: false),
                    Masa = table.Column<double>(nullable: false),
                    DataIncarcare = table.Column<string>(maxLength: 100, nullable: true),
                    OraIncarcare = table.Column<string>(maxLength: 100, nullable: true),
                    DataDescarcare = table.Column<string>(maxLength: 100, nullable: true),
                    OraDescarcare = table.Column<string>(maxLength: 100, nullable: true),
                    ConsumGaz = table.Column<int>(nullable: false),
                    ConsumElectricitate = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EltiModels", x => x.EltiModelId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EltiModels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ElindModels",
                table: "ElindModels");

            migrationBuilder.RenameTable(
                name: "ElindModels",
                newName: "ElindModel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ElindModel",
                table: "ElindModel",
                column: "ElindModelId");
        }
    }
}
