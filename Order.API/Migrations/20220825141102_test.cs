using Microsoft.EntityFrameworkCore.Migrations;

namespace Order.API.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderedGamesDetails_OrderDetails_OrderDetailId",
                table: "OrderedGamesDetails");

            migrationBuilder.RenameColumn(
                name: "OrderDetailId",
                table: "OrderedGamesDetails",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderedGamesDetails_OrderDetailId",
                table: "OrderedGamesDetails",
                newName: "IX_OrderedGamesDetails_OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderedGamesDetails_OrderDetails_OrderId",
                table: "OrderedGamesDetails",
                column: "OrderId",
                principalTable: "OrderDetails",
                principalColumn: "OrderNum",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderedGamesDetails_OrderDetails_OrderId",
                table: "OrderedGamesDetails");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "OrderedGamesDetails",
                newName: "OrderDetailId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderedGamesDetails_OrderId",
                table: "OrderedGamesDetails",
                newName: "IX_OrderedGamesDetails_OrderDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderedGamesDetails_OrderDetails_OrderDetailId",
                table: "OrderedGamesDetails",
                column: "OrderDetailId",
                principalTable: "OrderDetails",
                principalColumn: "OrderNum",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
