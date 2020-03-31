using Microsoft.EntityFrameworkCore.Migrations;

namespace RaportareAjustajV2.Migrations
{
    public partial class ModifyUSModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Gain",
                table: "UsBarModels",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Range",
                table: "UsBarModels",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gain",
                table: "UsBarModels");

            migrationBuilder.DropColumn(
                name: "Range",
                table: "UsBarModels");
        }
    }
}
