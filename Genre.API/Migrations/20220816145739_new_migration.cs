using Microsoft.EntityFrameworkCore.Migrations;

namespace Genre.API.Migrations
{
    public partial class new_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "categoryName",
                table: "GenreDetails",
                newName: "CategoryName");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "GenreDetails",
                newName: "GenreID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CategoryName",
                table: "GenreDetails",
                newName: "categoryName");

            migrationBuilder.RenameColumn(
                name: "GenreID",
                table: "GenreDetails",
                newName: "ID");
        }
    }
}
