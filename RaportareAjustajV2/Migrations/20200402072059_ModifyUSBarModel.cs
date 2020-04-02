using Microsoft.EntityFrameworkCore.Migrations;

namespace RaportareAjustajV2.Migrations
{
    public partial class ModifyUSBarModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConformASTM388",
                table: "UsBarModels",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NeconformASTM388",
                table: "UsBarModels",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConformASTM388",
                table: "UsBarModels");

            migrationBuilder.DropColumn(
                name: "NeconformASTM388",
                table: "UsBarModels");
        }
    }
}
