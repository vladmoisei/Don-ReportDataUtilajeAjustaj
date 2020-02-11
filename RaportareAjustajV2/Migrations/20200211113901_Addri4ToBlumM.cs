using Microsoft.EntityFrameworkCore.Migrations;

namespace RaportareAjustajV2.Migrations
{
    public partial class Addri4ToBlumM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Blum4",
                table: "UsBlumModels",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Fir4",
                table: "UsBlumModels",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "MarimeDefect4",
                table: "UsBlumModels",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Blum4",
                table: "UsBlumModels");

            migrationBuilder.DropColumn(
                name: "Fir4",
                table: "UsBlumModels");

            migrationBuilder.DropColumn(
                name: "MarimeDefect4",
                table: "UsBlumModels");
        }
    }
}
