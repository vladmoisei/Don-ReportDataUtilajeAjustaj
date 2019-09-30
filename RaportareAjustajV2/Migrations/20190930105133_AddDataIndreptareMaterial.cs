using Microsoft.EntityFrameworkCore.Migrations;

namespace RaportareAjustajV2.Migrations
{
    public partial class AddDataIndreptareMaterial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DataIndreptareMaterial",
                table: "PresaValdoraModels",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataIndreptareMaterial",
                table: "PresaValdoraModels");
        }
    }
}
