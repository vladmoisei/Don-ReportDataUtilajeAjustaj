using Microsoft.EntityFrameworkCore.Migrations;

namespace RaportareAjustajV2.Migrations
{
    public partial class MotivEnumModelRullatrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Motiv",
                table: "RullatriceProjectManModels",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Motiv",
                table: "RuillatriceLandgrafModels",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Motiv",
                table: "RullatriceProjectManModels");

            migrationBuilder.DropColumn(
                name: "Motiv",
                table: "RuillatriceLandgrafModels");
        }
    }
}
