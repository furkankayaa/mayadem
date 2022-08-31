using Microsoft.EntityFrameworkCore.Migrations;

namespace Order.API.Migrations
{
    public partial class newdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderedGamesDetails_OrderDetails_OrderId",
                table: "OrderedGamesDetails");

            migrationBuilder.DropIndex(
                name: "IX_OrderedGamesDetails_OrderId",
                table: "OrderedGamesDetails");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "OrderedGamesDetails");

            migrationBuilder.CreateTable(
                name: "GameOrderLinks",
                columns: table => new
                {
                    GameId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_GameOrderLinks_OrderDetails_OrderId",
                        column: x => x.OrderId,
                        principalTable: "OrderDetails",
                        principalColumn: "OrderNum",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameOrderLinks_OrderedGamesDetails_GameId",
                        column: x => x.GameId,
                        principalTable: "OrderedGamesDetails",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_GameOrderLinks_GameId",
                table: "GameOrderLinks",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GameOrderLinks_OrderId",
                table: "GameOrderLinks",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameOrderLinks");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "OrderedGamesDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrderedGamesDetails_OrderId",
                table: "OrderedGamesDetails",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderedGamesDetails_OrderDetails_OrderId",
                table: "OrderedGamesDetails",
                column: "OrderId",
                principalTable: "OrderDetails",
                principalColumn: "OrderNum",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
