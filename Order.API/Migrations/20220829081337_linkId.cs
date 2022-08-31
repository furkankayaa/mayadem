using Microsoft.EntityFrameworkCore.Migrations;

namespace Order.API.Migrations
{
    public partial class linkId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "GameOrderLinks",
                newName: "LinkId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LinkId",
                table: "GameOrderLinks",
                newName: "ID");
        }
    }
}
