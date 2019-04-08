using Microsoft.EntityFrameworkCore.Migrations;

namespace RaportareAjustajV2.Migrations
{
    public partial class userUtilaj : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Utilaj",
                table: "Users",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Utilaj",
                table: "Users");
        }
    }
}
