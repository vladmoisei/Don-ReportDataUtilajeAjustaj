using Microsoft.EntityFrameworkCore.Migrations;

namespace RaportareAjustajV2.Migrations
{
    public partial class etichetaToModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Eticheta",
                table: "PresaValdoraModels",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Eticheta",
                table: "PellatriceLandgrafModels",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Eticheta",
                table: "PresaValdoraModels");

            migrationBuilder.DropColumn(
                name: "Eticheta",
                table: "PellatriceLandgrafModels");
        }
    }
}
