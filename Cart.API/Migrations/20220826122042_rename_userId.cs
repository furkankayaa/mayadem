using Microsoft.EntityFrameworkCore.Migrations;

namespace Cart.API.Migrations
{
    public partial class rename_userId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "CartItemDetails",
                newName: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "CartItemDetails",
                newName: "UserName");
        }
    }
}
