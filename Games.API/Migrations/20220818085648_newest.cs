using Microsoft.EntityFrameworkCore.Migrations;

namespace Games.API.Migrations
{
    public partial class newest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "gamePrice",
                table: "GameDetails",
                newName: "GamePrice");

            migrationBuilder.RenameColumn(
                name: "gameName",
                table: "GameDetails",
                newName: "GameName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GamePrice",
                table: "GameDetails",
                newName: "gamePrice");

            migrationBuilder.RenameColumn(
                name: "GameName",
                table: "GameDetails",
                newName: "gameName");
        }
    }
}
