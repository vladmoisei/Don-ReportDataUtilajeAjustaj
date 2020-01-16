using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RaportareAjustajV2.Migrations
{
    public partial class AddPresaDunkeModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PresaDunkeModels",
                columns: table => new
                {
                    PresaDunkeModelId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(maxLength: 100, nullable: false),
                    DataIntroducere = table.Column<string>(maxLength: 100, nullable: true),
                    DataIndreptareMaterial = table.Column<string>(nullable: true),
                    Diametru = table.Column<int>(nullable: false),
                    Calitate = table.Column<string>(maxLength: 100, nullable: true),
                    Sarja = table.Column<string>(maxLength: 100, nullable: true),
                    Eticheta = table.Column<string>(maxLength: 100, nullable: true),
                    NrBare = table.Column<int>(nullable: false),
                    Lungime = table.Column<int>(nullable: false),
                    Masa = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PresaDunkeModels", x => x.PresaDunkeModelId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PresaDunkeModels");
        }
    }
}
