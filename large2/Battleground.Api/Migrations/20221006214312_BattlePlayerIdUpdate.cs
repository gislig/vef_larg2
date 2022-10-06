using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Battleground.Api.Migrations
{
    public partial class BattlePlayerIdUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlayersInMatchId",
                table: "BattlePlayers");

            migrationBuilder.AddColumn<int>(
                name: "Player",
                table: "Players",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Players_Player",
                table: "Players",
                column: "Player");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_BattlePlayers_Player",
                table: "Players",
                column: "Player",
                principalTable: "BattlePlayers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_BattlePlayers_Player",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_Player",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Player",
                table: "Players");

            migrationBuilder.AddColumn<int>(
                name: "PlayersInMatchId",
                table: "BattlePlayers",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
