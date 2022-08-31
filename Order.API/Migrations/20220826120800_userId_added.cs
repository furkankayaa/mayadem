using Microsoft.EntityFrameworkCore.Migrations;

namespace Order.API.Migrations
{
    public partial class userId_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Publisher",
                table: "OrderedGamesDetails",
                type: "varchar",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "OrderDetails",
                type: "varchar",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Publisher",
                table: "OrderedGamesDetails");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "OrderDetails");
        }
    }
}
